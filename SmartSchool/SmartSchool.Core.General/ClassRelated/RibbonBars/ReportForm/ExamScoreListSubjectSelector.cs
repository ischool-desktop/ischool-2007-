using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.ClassRelated;
using SmartSchool.StudentRelated;
using SmartSchool.Common;
using System.Threading;
using SmartSchool;
using Aspose.Cells;
using System.IO;
using System.Diagnostics;

namespace SmartSchool.ClassRelated.RibbonBars.Reports
{
    public partial class ExamScoreListSubjectSelector : BaseForm
    {
        Dictionary<string, List<XmlElement>> _InExamCourse = new Dictionary<string, List<XmlElement>>();
        Dictionary<string, List<XmlElement>> _StudentAttends = new Dictionary<string, List<XmlElement>>();
        BackgroundWorker _bkw = new BackgroundWorker();
        BackgroundWorker _Reporter = new BackgroundWorker();
        ManualResetEvent _waitForCourseInExam = new ManualResetEvent(true); 

        public ExamScoreListSubjectSelector()
        {
            InitializeComponent();
            List<BriefStudentData> students = new List<BriefStudentData>();
            foreach (ClassInfo classInfo in SmartSchool.ClassRelated.Class.Instance.SelectionClasses)
            {
                students.AddRange(classInfo.Students);
            }
            _waitForCourseInExam.Reset();
            _bkw.DoWork += new DoWorkEventHandler(_bkw_DoWork);
            _bkw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bkw_RunWorkerCompleted);
            _bkw.RunWorkerAsync(students);
            comboBoxProxy1.NodeList.AddRange(SmartSchool.Feature.Course.QueryCourse.GetExamList().GetContent().BaseElement, "Exam");
            
            _Reporter = new BackgroundWorker();
            _Reporter.DoWork += new DoWorkEventHandler(_Reporter_DoWork);
            _Reporter.ProgressChanged += new ProgressChangedEventHandler(_Reporter_ProgressChanged);
            _Reporter.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_Reporter_RunWorkerCompleted);
            _Reporter.WorkerReportsProgress = true;
        }

        void _bkw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (comboBoxProxy1.SelectedValue == "")
            {
                XmlElement PreferenceData = CurrentUser.Instance.Preference["列印班級考試成績單"];
                if (PreferenceData != null)
                {
                    if (PreferenceData.HasAttribute("LastPrintExamID"))
                    {
                        comboBoxProxy1.SelectedValue =  PreferenceData.GetAttribute("LastPrintExamID");
                    }
                }
            }
        }

        void _bkw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<BriefStudentData> students = (List<BriefStudentData>)e.Argument;
                Dictionary<string, XmlElement> _RelatedCourse = new Dictionary<string, XmlElement>();
                #region 取修課紀錄
                DSXmlHelper helper = new DSXmlHelper("SelectRequest");
                helper.AddElement("Field");
                helper.AddElement("Field", "ID");
                helper.AddElement("Field", "RefStudentID");
                helper.AddElement("Field", "RefCourseID");
                helper.AddElement("Condition");
                foreach (BriefStudentData var in students)
                {
                    helper.AddElement("Condition", "StudentID", var.ID);
                }
                helper.AddElement("Order");
                DSRequest dsreq = new DSRequest(helper);
                DSResponse scAttend = SmartSchool.Feature.Course.QueryCourse.GetSCAttend(dsreq);
                #endregion
                #region 取得課程資料，過濾非本學期之課程
                List<string> courseIds = new List<string>();
                foreach (XmlElement attendElement in scAttend.GetContent().GetElements("Student/RefCourseID"))
                {
                    if (!courseIds.Contains(attendElement.InnerText))
                        courseIds.Add(attendElement.InnerText);
                }
                if (courseIds.Count > 0)//如果根本沒有學生修課紀錄就不用抓課程了
                {
                    DSResponse course = SmartSchool.Feature.Course.QueryCourse.GetCourseById(courseIds.ToArray());
                    foreach (XmlElement courseElement in course.GetContent().GetElements("Course"))
                    {
                        if (int.Parse(courseElement.SelectSingleNode("SchoolYear").InnerText) == CurrentUser.Instance.SchoolYear &&
                           int.Parse(courseElement.SelectSingleNode("Semester").InnerText) == CurrentUser.Instance.Semester
                            )
                        {
                            _RelatedCourse.Add(courseElement.GetAttribute("ID"), courseElement);
                        }
                    }
                }
                #endregion
                #region 將學生修課紀錄及課程一併儲存
                foreach (XmlElement attendElement in scAttend.GetContent().GetElements("Student"))
                {
                    string cid = attendElement.SelectSingleNode("RefCourseID").InnerText;
                    if (_RelatedCourse.ContainsKey(cid))
                    {
                        string sid = attendElement.SelectSingleNode("RefStudentID").InnerText;
                        if (!_StudentAttends.ContainsKey(sid))
                            _StudentAttends.Add(sid, new List<XmlElement>());
                        //將課程直接加到修課紀錄的element中
                        attendElement.AppendChild(attendElement.OwnerDocument.ImportNode(_RelatedCourse[cid], true));
                        _StudentAttends[sid].Add(attendElement);
                    }
                } 
                #endregion
                #region 取得試別
                helper = new DSXmlHelper("Request");
                helper.AddElement("Field");
                helper.AddElement("Field", "CourseID");
                helper.AddElement("Field", "ExamID");
                helper.AddElement("Condition");
                foreach (string   key in _RelatedCourse.Keys)
                {
                    helper.AddElement("Condition", "RefCourseID", key);
                }
                dsreq = new DSRequest(helper);
                DSResponse exam = SmartSchool.Feature.Course.QueryCourse.GetExamMapping(dsreq);
                foreach (XmlElement examElement in exam.GetContent().GetElements("Mapping"))
                {
                    string examid = examElement.GetAttribute("ExamID");
                    string courseid = examElement.GetAttribute("CourseID");
                    if (!_InExamCourse.ContainsKey(examid))
                        _InExamCourse.Add(examid, new List<XmlElement>());
                    _InExamCourse[examid].Add(_RelatedCourse[courseid]);
                }
                #endregion
            }
            catch
            {
            }
            finally
            {
                _waitForCourseInExam.Set(); 
            }
        }

        private string GetNumber(int p)
        {
            string levelNumber;
            switch (p)
            {
                #region 對應levelNumber
                case 1:         
                    levelNumber = "Ⅰ";
                    break;
                case 2:
                    levelNumber = "Ⅱ";
                    break;
                case 3:
                    levelNumber = "Ⅲ";
                    break;
                case 4:
                    levelNumber = "Ⅳ";
                    break;
                case 5:
                    levelNumber = "Ⅴ";
                    break;
                case 6:
                    levelNumber = "Ⅵ";
                    break;
                case 7:
                    levelNumber = "Ⅶ";
                    break;
                case 8:
                    levelNumber = "Ⅷ";
                    break;
                case 9:
                    levelNumber = "Ⅸ";
                    break;
                case 10:
                    levelNumber = "Ⅹ";
                    break;
                default:
                    levelNumber = "" + (p);
                    break;
                #endregion
            }
            return levelNumber;
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MsgBox.Show(comboBoxProxy1.SelectedValue);
            listView1.Items.Clear();
            MotherForm.Instance.SetWaitCursor();
            _waitForCourseInExam.WaitOne();
            if (_InExamCourse.ContainsKey(comboBoxProxy1.SelectedValue))
            {
                List<string> subjects = new List<string>();
                foreach (XmlElement element in _InExamCourse[comboBoxProxy1.SelectedValue])
                {
                    string subjectName = element.SelectSingleNode("Subject").InnerText;
                    if (!subjects.Contains(subjectName))
                    {
                        subjects.Add(subjectName);
                    }
                }
                foreach (string subjectName in subjects)
                {
                    listView1.Items.Add(new ListViewItem(subjectName));
                }
            }
            SetListViewChecked();
            MotherForm.Instance.ResetWaitCursor();
            buttonX1.Enabled = true;
        }

        private void SetListViewChecked()
        {
            Dictionary<string, bool> checkedSubjects = new Dictionary<string, bool>();
            XmlElement PreferenceData = CurrentUser.Instance.Preference["列印班級考試成績單"];
            if (PreferenceData != null)
            {
                if (comboBoxProxy1.SelectedValue != "")
                {
                    PreferenceData.SetAttribute("LastPrintExamID", comboBoxProxy1.SelectedValue);
                }
                foreach (XmlNode node in PreferenceData.SelectNodes("Subject"))
                {
                    XmlElement element = (XmlElement)node;
                    if (!checkedSubjects.ContainsKey(element.GetAttribute("Name")))
                        checkedSubjects.Add(element.GetAttribute("Name"), bool.Parse(element.GetAttribute("Checked")));
                }
            }
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked=(checkedSubjects.ContainsKey(item.Text)?checkedSubjects[item.Text]:true);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            #region 設定科目選取
            #region 取得PreferenceElement
            XmlElement PreferenceElement = CurrentUser.Instance.Preference["列印班級考試成績單"];
            if (PreferenceElement == null)
            {
                PreferenceElement = new XmlDocument().CreateElement("列印班級考試成績單");
            }
            #endregion
            foreach (ListViewItem item in listView1.Items)
            {
                XmlNode subjectNode = PreferenceElement.SelectSingleNode("Subject[@Name='" + item.Text + "']");
                if (subjectNode != null)
                {
                    ((XmlElement)subjectNode).SetAttribute("Checked",item.Checked.ToString());
                }
                else
                {
                    XmlElement subjectElement = PreferenceElement.OwnerDocument.CreateElement("Subject");
                    subjectElement.SetAttribute("Name", item.Text);
                    subjectElement.SetAttribute("Checked", item.Checked.ToString());
                    PreferenceElement.AppendChild(subjectElement);
                }
            }
            CurrentUser.Instance.Preference["列印班級考試成績單"] = PreferenceElement;

            #endregion
            List<string> printSubjects = new List<string>();
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Checked)
                    printSubjects.Add(item.Text);
            }
            _Reporter.RunWorkerAsync(new object[] { printSubjects, 
                SmartSchool.ClassRelated.Class.Instance.SelectionClasses,
                comboBoxEx1.Text ,
                comboBoxProxy1.SelectedValue
            });
            Close();
        }


        void _Reporter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {            
            if (e.Error == null)
            {
               Workbook  report =(Workbook) e.Result;
                //儲存 Excel
                #region 儲存 Excel
                string path = Path.Combine(Application.StartupPath, "Reports");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = Path.Combine(path, "定期評量班級成績單.xlt");

                if (File.Exists(path))
                {
                    bool needCount = true;
                    try
                    {
                        File.Delete(path);
                        needCount = false;
                    }
                    catch { }
                    int i = 1;
                    while (needCount)
                    {
                        string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + (i++) + Path.GetExtension(path);
                        if (!File.Exists(newPath))
                        {
                            path = newPath;
                            break;
                        }
                        else
                        {
                            try
                            {
                                File.Delete(newPath);
                                path = newPath;
                                break;
                            }
                            catch { }
                        }
                    }
                }
                try
                {
                    File.Create(path).Close();
                }
                catch
                {
                    SaveFileDialog sd = new SaveFileDialog();
                    sd.Title = "另存新檔";
                    sd.FileName = Path.GetFileNameWithoutExtension(path) + ".xls";
                    sd.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                    if (sd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            File.Create(sd.FileName);
                            path = sd.FileName;
                        }
                        catch
                        {
                            MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                report.Save(path, FileFormatType.Excel2003);
                #endregion
                MotherForm.Instance.SetBarMessage("班級成績單產生完成");
                Process.Start(path);
            }
            else
                MotherForm.Instance.SetBarMessage("班級成績單產生發生未預期錯誤");
        }

        void _Reporter_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            MotherForm.Instance.SetBarMessage("班級成績單產生中...", e.ProgressPercentage);
        }

        void _Reporter_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> printSubjects = (List<string>)((object[])e.Argument)[0];
            List<SmartSchool.ClassRelated.ClassInfo> selectedClasses = (List<SmartSchool.ClassRelated.ClassInfo>)((object[])e.Argument)[1];
            string examName = (string)((object[])e.Argument)[2];
            string examID = (string)((object[])e.Argument)[3];
            //每個班級包含的學生
            Dictionary<ClassInfo, List<BriefStudentData>> classStudentList = new Dictionary<ClassInfo, List<BriefStudentData>>();
            //每個班要列印的科目(科目級別學分數)
            Dictionary<ClassInfo, List<string>> classPrintList = new Dictionary<ClassInfo, List<string>>();
            //所有需要去取得考試成績的課程
            List<string> allCourseID = new List<string>();
            //修課紀錄的參照，填入考試成績時使用
            Dictionary<string, XmlElement> attendElementMapping = new Dictionary<string, XmlElement>();
            //本次列印同個班要印的最多科目數
            int maxSubject = 0;
            //學生加權平均
            Dictionary<string, double> studentAVGScore = new Dictionary<string, double>();
            //學生是否排名
            Dictionary<string, bool> studentCanRank = new Dictionary<string, bool>();
            //學生的排名
            Dictionary<BriefStudentData, string> studentRank = new Dictionary<BriefStudentData, string>();
            //總分
            Dictionary<string, double> studentSum = new Dictionary<string, double>();

            #region 整理每一個班級需要印的科目及班級學生
            foreach (ClassInfo classinfo in selectedClasses)
            {
                classPrintList.Add(classinfo, new List<string>());
                classStudentList.Add(classinfo, new List<BriefStudentData>(classinfo.Students));
                foreach (BriefStudentData student in classStudentList[classinfo])
                {
                    if (_StudentAttends.ContainsKey(student.ID))//該學生沒有修課紀錄時不處理
                    {
                        foreach (XmlElement attendElement in _StudentAttends[student.ID])
                        {
                            //是要被列印的修課紀錄
                            if (printSubjects.Contains( attendElement.SelectSingleNode("Course/Subject").InnerText))
                            {
                                //建立用id找到修課紀錄
                                if (!attendElementMapping.ContainsKey(attendElement.GetAttribute("ID")))
                                    attendElementMapping.Add(attendElement.GetAttribute("ID"), attendElement);
                                #region 加入取得考試分數清單
                                string courseID = attendElement.SelectSingleNode("Course/@ID").InnerText;
                                if (!allCourseID.Contains(courseID))
                                    allCourseID.Add(courseID);
                                #endregion
                                #region 加入班級科目(依科目級別學分數)
                                string subjectKey = "<Subject Name='" + attendElement.SelectSingleNode("Course/Subject").InnerText + "' Level='"
                                                            + attendElement.SelectSingleNode("Course/SubjectLevel").InnerText + "' Credit='"
                                                            + attendElement.SelectSingleNode("Course/Credit").InnerText + "' />";
                                if (!classPrintList[classinfo].Contains(subjectKey))
                                    classPrintList[classinfo].Add(subjectKey); 
                                #endregion
                            }
                        }
                    }
                }
                classPrintList[classinfo].Sort(SortBySubjectName);
            } 
            #endregion
            #region 取得考試成績
            DSResponse examScore= SmartSchool.Feature.Course.QueryCourse.GetSECTake(allCourseID.ToArray());
            foreach (XmlElement scoreElement in examScore.GetContent().GetElements("Score"))
            {
                if (scoreElement.SelectSingleNode("ExamID").InnerText == examID && attendElementMapping.ContainsKey(scoreElement.SelectSingleNode("AttendID").InnerText))
                {
                    attendElementMapping[scoreElement.SelectSingleNode("AttendID").InnerText].SetAttribute("Score", scoreElement.SelectSingleNode("Score").InnerText);
                }
            }
            #endregion
            #region 計算平均及是否排名
            foreach (string studentid in _StudentAttends.Keys)
            {
                double scoreCount = 0;
                bool canRank = true;
                int CreditCount = 0;
                double score;
                int Credit;
                double sum = 0;
                foreach (XmlElement attendInfo in _StudentAttends[studentid])
                {
                    if (printSubjects.Contains(attendInfo.SelectSingleNode("Course/Subject").InnerText))
                    {
                        if (attendInfo.HasAttribute("Score") && double.TryParse(attendInfo.GetAttribute("Score"), out score) && int.TryParse(attendInfo.SelectSingleNode("Course/Credit").InnerText, out Credit))
                        {
                            scoreCount += score*Credit;
                            CreditCount += Credit;
                            sum += score;
                        }
                        else
                            canRank = false;
                    }
                }
                studentSum.Add(studentid, sum);
                if (CreditCount > 0)
                {
                    //加權平均算到小數第二位
                    studentAVGScore.Add(studentid, double.Parse(( scoreCount / CreditCount).ToString(".00")));
                }
                else
                    canRank = false;
                studentCanRank.Add(studentid, canRank);
            }
            #endregion
            #region 班級成績排序
            foreach (ClassInfo classinfo in classPrintList.Keys)
            {
                List<double> sortedScore = new List<double>();
                foreach (BriefStudentData studentInfo in classStudentList[classinfo])
                {
                    if (studentCanRank.ContainsKey(studentInfo.ID) && studentCanRank[studentInfo.ID] && studentAVGScore.ContainsKey(studentInfo.ID))
                        sortedScore.Add(studentAVGScore[studentInfo.ID]);
                }
                sortedScore.Sort();
                sortedScore.Reverse();
                foreach (BriefStudentData  studentInfo in classStudentList[classinfo])
                {
                    if (studentCanRank.ContainsKey(studentInfo.ID) && studentCanRank[studentInfo.ID] && studentAVGScore.ContainsKey(studentInfo.ID))
                        studentRank.Add(studentInfo,""+( sortedScore.IndexOf(studentAVGScore[studentInfo.ID]) + 1));
                    else
                        studentRank.Add(studentInfo, "");
                }
            }
            #endregion
            #region 計算要列印的最大數量
            foreach (List<string> subjectList in classPrintList.Values)
            {
                maxSubject = (subjectList.Count > maxSubject ? subjectList.Count : maxSubject);
            } 
            #endregion

            Workbook template = new Workbook();
            #region 建立樣板
            template.Open(new MemoryStream(Properties.Resources.定期評量班級成績單), FileFormatType.Excel2003);
            //擴展到最大科目數含加權成績跟排名
            for (int i = 0; i < maxSubject +3; i++)
            {
                template.Worksheets[0].Cells.CopyColumn(template.Worksheets[0].Cells, 2, 2 + i);
            }
            template.Worksheets[0].Cells[1, maxSubject + 2].PutValue("總分");
            template.Worksheets[0].Cells[1, maxSubject+3].PutValue("加權平均");
            template.Worksheets[0].Cells[1, maxSubject + 4].PutValue("班排名");
            template.Worksheets[0].Cells.CreateRange(0, 0, 1, maxSubject + 5).Merge();
            #endregion

            Workbook report = new Workbook();
            report.Open(new MemoryStream(Properties.Resources.定期評量班級成績單), FileFormatType.Excel2003);
            report.Copy(template);
            int classIndex = 0;
            int maxNumberStudentsInClass = 50;
            #region 計算要列印的班級中學生最多者
            foreach (List<BriefStudentData> list in classStudentList.Values)
            {
                if (maxNumberStudentsInClass < list.Count)
                    maxNumberStudentsInClass = list.Count;
            } 
            #endregion

            #region 列印
            //取出classPrintList中的班及科目用
            XmlDocument doc = new XmlDocument();
            double allProgress = 0.0f;
            double classProgress = 100.0f / classPrintList.Keys.Count;
            Style hamid = report.Styles[report.Styles.Add()];
            hamid.Font.Name = template.Worksheets[0].Cells[3, 0].Style.Font.Name;
            hamid.Font.Size = 9;
            hamid.HorizontalAlignment = TextAlignmentType.Center;
            hamid.VerticalAlignment = TextAlignmentType.Top;
            Style haleft = report.Styles[report.Styles.Add()];
            haleft.Font.Name = template.Worksheets[0].Cells[3, 0].Style.Font.Name;
            haleft.Font.Size = 12;
            haleft.HorizontalAlignment = TextAlignmentType.Left;
            Style haright = report.Styles[report.Styles.Add()];
            haright.Font.Name = template.Worksheets[0].Cells[3, 0].Style.Font.Name;
            haright.Font.Size = 12;
            haright.HorizontalAlignment = TextAlignmentType.Right;
            foreach (ClassInfo classinfo in classPrintList.Keys)
            {
                int studentIndex = 0;
                #region 表頭
                //複製表頭
                for (int i = 0; i < 3; i++)
                {
                    //複製內容
                    report.Worksheets[0].Cells.CopyRow(template.Worksheets[0].Cells, i, classIndex + i);
                    //複製列高
                    report.Worksheets[0].Cells.SetRowHeight(classIndex + i, template.Worksheets[0].Cells.GetRowHeight(i));
                }
                //標題
                report.Worksheets[0].Cells[classIndex + studentIndex++, 0].PutValue(CurrentUser.Instance.SchoolChineseName + " " + classinfo.ClassName + " " + examName + " 班級成績單");
                //科目及學分數
                for (int i = 0; i < classPrintList[classinfo].Count; i++)
                {
                    doc.LoadXml(classPrintList[classinfo][i]);
                    int level = 0;
                    report.Worksheets[0].Cells[classIndex + studentIndex, i + 2].PutValue(doc.DocumentElement.GetAttribute("Name") +
                        (int.TryParse(doc.DocumentElement.GetAttribute("Level"), out level) ? GetNumber(level) : "")
                        );
                    report.Worksheets[0].Cells[classIndex + studentIndex + 1, i + 2].PutValue(doc.DocumentElement.GetAttribute("Credit"));
                }
                studentIndex++;
                studentIndex++;
                #endregion
                //學生
                double studentProgress = classProgress / classStudentList[classinfo].Count;
                double studentProgressCount = 0;
                for (int i = 0; i < maxNumberStudentsInClass; i++)
                {
                    //複製列
                    report.Worksheets[0].Cells.CopyRow(template.Worksheets[0].Cells, 3, classIndex + studentIndex);
                    //複製列高
                    report.Worksheets[0].Cells.SetRowHeight(classIndex + studentIndex, template.Worksheets[0].Cells.GetRowHeight(3));
                    if (classStudentList[classinfo].Count > i)
                    {
                        #region 如果這一列有學生
                        BriefStudentData studentInfo = classStudentList[classinfo][i];
                        #region 姓名座號
                        report.Worksheets[0].Cells[classIndex + studentIndex, 0].PutValue(studentInfo.SeatNo);
                        report.Worksheets[0].Cells[classIndex + studentIndex, 1].PutValue(studentInfo.Name);
                        #endregion
                        #region 修課成績
                        if (_StudentAttends.ContainsKey(studentInfo.ID))
                        {
                            foreach (XmlElement attendElement in _StudentAttends[studentInfo.ID])
                            {
                                if (printSubjects.Contains(attendElement.SelectSingleNode("Course/Subject").InnerText))
                                {
                                    string subjectKey = "<Subject Name='" + attendElement.SelectSingleNode("Course/Subject").InnerText + "' Level='"
                                                                       + attendElement.SelectSingleNode("Course/SubjectLevel").InnerText + "' Credit='"
                                                                       + attendElement.SelectSingleNode("Course/Credit").InnerText + "' />";
                                    string FildValue = "" + report.Worksheets[0].Cells[classIndex + studentIndex, 2 + classPrintList[classinfo].IndexOf(subjectKey)].Value;
                                    if (attendElement.HasAttribute("Score"))
                                        report.Worksheets[0].Cells[classIndex + studentIndex, 2 + classPrintList[classinfo].IndexOf(subjectKey)].PutValue((FildValue == "--" ? "" : FildValue + ",") + attendElement.GetAttribute("Score"));
                                    else
                                        report.Worksheets[0].Cells[classIndex + studentIndex, 2 + classPrintList[classinfo].IndexOf(subjectKey)].PutValue((FildValue == "--" ? "" : FildValue + ",") + "未輸入");
                                }
                            }
                        }
                        #endregion
                        #region 空白科目行處理
                        for (int j = classPrintList[classinfo].Count; j < maxSubject; j++)
                        {
                            report.Worksheets[0].Cells[classIndex + studentIndex, 2 + j].PutValue("");
                        }
                        #endregion
                        #region 總分
                        if (studentSum.ContainsKey(studentInfo.ID))
                            report.Worksheets[0].Cells[classIndex + studentIndex, maxSubject + 2].PutValue(studentSum[studentInfo.ID]);
                        #endregion
                        #region 加權平均
                        if (studentAVGScore.ContainsKey(studentInfo.ID))
                            report.Worksheets[0].Cells[classIndex + studentIndex, maxSubject + 3].PutValue(studentAVGScore[studentInfo.ID].ToString(".00"));
                        #endregion
                        #region 排名
                        if (studentRank.ContainsKey(studentInfo))
                            report.Worksheets[0].Cells[classIndex + studentIndex, maxSubject + 4].PutValue(studentRank[studentInfo]);
                        #endregion
                        studentProgressCount += studentProgress;
                        _Reporter.ReportProgress((int)(allProgress + studentProgressCount));
                        #endregion
                    }
                    else
                    {
                        //清空補齊的列
                        for (int j = 0; j < maxSubject + 5; j++)
                        {
                            report.Worksheets[0].Cells[classIndex + studentIndex, j].PutValue(null);
                        }
                    }
                    #region 每5列就加特殊邊
                    if (studentIndex > 3 && (studentIndex - 3) % 5 == 0)
                    {
                        Range eachFiveRow = report.Worksheets[0].Cells.CreateRange(classIndex + studentIndex, 0, 1, maxSubject + 5);
                        eachFiveRow.SetOutlineBorder(BorderType.TopBorder, CellBorderType.Double, Color.Black);
                    }
                    #endregion
                    studentIndex++;
                }
                //班級科目平均
                #region 班級科目平均
                #region 做一列科目平均列
                //複製內容
                report.Worksheets[0].Cells.CopyRow(template.Worksheets[0].Cells, 2, classIndex + studentIndex);
                //複製列高
                report.Worksheets[0].Cells.SetRowHeight(classIndex + studentIndex, template.Worksheets[0].Cells.GetRowHeight(2));
                report.Worksheets[0].Cells[classIndex + studentIndex, 0].PutValue("平均"); 
                #endregion
                for (int subject = 2; subject < classPrintList[classinfo].Count+2; subject++)
                {
                    double sum=0;
                    int counts = 0;
                    for (int i = classIndex + 3; i < classIndex + maxNumberStudentsInClass + 3; i++)
                    {
                        double dtest;
                        if (double.TryParse("" + report.Worksheets[0].Cells[i, subject].Value, out dtest))
                        {
                            sum += dtest;
                            counts++;
                        }
                    }
                    report.Worksheets[0].Cells[classIndex + studentIndex, subject].PutValue(counts == 0 ? "" : (sum / counts).ToString(".00"));
                }
                studentIndex++;
                #endregion

                //回條
                #region 回條
                studentIndex++;
                #region 分隔線
                Range r = report.Worksheets[0].Cells.CreateRange(classIndex + studentIndex, 0, 1, maxSubject + 5);
                report.Worksheets[0].Cells.SetRowHeight(classIndex + studentIndex,12);
                r.Merge();
                r.Style = hamid;
                r.SetOutlineBorder(BorderType.TopBorder, CellBorderType.Dotted, Color.Black);
                report.Worksheets[0].Cells[classIndex + studentIndex, 0].PutValue("本回單請簽章後，由此線撕下交由貴弟子送繳該班導師存查");
                studentIndex++;
                #endregion
                report.Worksheets[0].Cells.SetRowHeight(classIndex + studentIndex, 3);
                studentIndex++; 
                #region 成績單回條列
                report.Worksheets[0].Cells.SetRowHeight(classIndex + studentIndex, 18);
                r = report.Worksheets[0].Cells.CreateRange(classIndex + studentIndex, 0, 1, maxSubject + 5);
                r.Merge();
                r.Style = haleft;
                report.Worksheets[0].Cells[classIndex + studentIndex, 0].PutValue("" + examName + " 班級成績單回條             班級：                              姓名：                           座號：");
                studentIndex++; 
                #endregion
                report.Worksheets[0].Cells.SetRowHeight(classIndex + studentIndex, 8);
                studentIndex++; 
                #region 此至貴家長
                report.Worksheets[0].Cells.SetRowHeight(classIndex + studentIndex, 18);
                r = report.Worksheets[0].Cells.CreateRange(classIndex + studentIndex, 0, 1, maxSubject + 5);
                r.Merge();
                r.Style = haleft;
                report.Worksheets[0].Cells[classIndex + studentIndex, 0].PutValue("此致  貴家長");
                studentIndex++; 
                #endregion
                #region 簽名
                report.Worksheets[0].Cells.SetRowHeight(classIndex + studentIndex, 18);
                r = report.Worksheets[0].Cells.CreateRange(classIndex + studentIndex, 0, 1, maxSubject + 5);
                r.Merge();
                r.Style = haright;
                report.Worksheets[0].Cells[classIndex + studentIndex, 0].PutValue("________________________(家長簽章)     ________________________(導師簽章)");
                studentIndex++; 
                #endregion
                #endregion
                report.Worksheets[0].HPageBreaks.Add(classIndex + studentIndex, maxSubject + 4);
                allProgress += classProgress;
                _Reporter.ReportProgress((int)allProgress);
                classIndex += studentIndex;
            }
            #endregion
            e.Result = report;
        }
        private static int SortBySubjectName(string a, string b)
        {
            string a1 = a.Length > 0 ? a.Substring(0, 1) : "";
            string b1 = b.Length > 0 ? b.Substring(0, 1) : "";
            #region 第一個字一樣的時候
            if (a1 == b1)
            {
                if (a.Length > 1 && b.Length > 1)
                    return SortBySubjectName(a.Substring(1), b.Substring(1));
                else
                    return a.Length.CompareTo(b.Length);
            }
            #endregion
            #region 第一個字不同，分別取得在設定順序中的數字，如果都不在設定順序中就用單純字串比較
            int ai = getInt(a1), bi = getInt(b1);
            if (ai > 0 || bi > 0)
            {
                return ai.CompareTo(bi);
            }
            else
                return a1.CompareTo(b1); 
            #endregion
        }

        private static int getInt(string a1)
        {
            int x = 0;
            switch (a1)
            { 
                case "國":
                    x = 1;
                    break;
                case "英":
                    x =2;
                    break;
                case "數":
                    x =3;
                    break;
                case "物":
                    x =4;
                    break;
                case "化":
                    x =5;
                    break;
                case "生":
                    x = 6;
                    break;
                case "基":
                    x =7;
                    break;
                case "歷":
                    x =8;
                    break;
                case "地":
                    x =9;
                    break;
                case "公":
                    x =10;
                    break;
                default:
                    break;
            }
            return x;
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            e.Item.ForeColor = e.Item.Checked ? listView1.ForeColor : Color.BlueViolet;
        }
    }
}