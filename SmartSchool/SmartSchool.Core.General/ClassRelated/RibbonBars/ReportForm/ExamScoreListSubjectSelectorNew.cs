using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.Customization.Data;
using System.Threading;
using System.Xml;
using System.IO;
using Aspose.Words;
using DevComponents.DotNetBar.Rendering;
using SmartSchool.Common;


namespace SmartSchool.ClassRelated.RibbonBars.Reports
{
    public partial class ExamScoreListSubjectSelectorNew : BaseForm
    {
        private bool checkedOnChange = false;

        private AccessHelper accessHelper = new AccessHelper();

        private List<ClassRecord> selectedClasses = new List<ClassRecord>();

        private byte[] customizeTemplateBuffer = null;

        private bool summaryFieldsChanging = false;

        public ExamScoreListSubjectSelectorNew()
        {
            //馬路如虎口請看紅綠燈
            ManualResetEvent _waitInit = new ManualResetEvent(true);
            //變紅燈
            _waitInit.Reset();
            #region 背景載入學生修課級課程考試資訊
            BackgroundWorker bkwLoader = new BackgroundWorker();
            bkwLoader.DoWork += new DoWorkEventHandler(bkwLoader_DoWork);
            bkwLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkwLoader_RunWorkerCompleted);
            bkwLoader.RunWorkerAsync(_waitInit);
            #endregion
            //初始化表單
            InitializeComponent();
            this.UseWaitCursor = true;
            comboBoxEx1.Items.Add("資料下載中...");
            comboBoxEx1.SelectedIndex = 0;
            #region 如果系統的Renderer是Office2007Renderer，同化_ClassTeacherView,_CategoryView的顏色
            if (GlobalManager.Renderer is Office2007Renderer)
            {
                ((Office2007Renderer)GlobalManager.Renderer).ColorTableChanged += new EventHandler(ExamScoreListSubjectSelector_ColorTableChanged);
                SetForeColor(this);
            }
            #endregion
            this.controlContainerItem1.Control = this.panelEx3;
            this.controlContainerItem2.Control = this.panelEx2;
            #region 讀取Preference
            XmlElement config = SmartSchool.Customization.Data.SystemInformation.Preference["班級考試成績單"];
            ////XmlElement config = SmartSchool.Customization.Data.SystemInformation.Configuration["列印班級考試成績單"];
            ////if ( config == null )
            ////    config = SmartSchool.Customization.Data.SystemInformation.Preference["列印班級考試成績單"];
            if (config != null)
            {
                #region 自訂樣板
                XmlElement customize1 = (XmlElement)config.SelectSingleNode("自訂樣板");

                if (customize1 != null)
                {
                    string templateBase64 = customize1.InnerText;
                    customizeTemplateBuffer = Convert.FromBase64String(templateBase64);
                    radioBtn2.Enabled = linkLabel2.Enabled = true;
                }
                #endregion
                if (config.HasAttribute("列印樣板") && config.GetAttribute("列印樣板") == "自訂")
                    radioBtn2.Checked = true;

                #region 統計欄位
                summaryFieldsChanging = true;
                bool check = false;
                if (bool.TryParse(config.GetAttribute("總分"), out check)) checkBox1.Checked = check;
                if (bool.TryParse(config.GetAttribute("總分排名"), out check)) checkBox2.Checked = check;
                if (bool.TryParse(config.GetAttribute("加權總分"), out check)) checkBox3.Checked = check;
                if (bool.TryParse(config.GetAttribute("加權平均"), out check)) checkBox4.Checked = check;
                if (bool.TryParse(config.GetAttribute("加權平均排名"), out check)) checkBox5.Checked = check;
                if (bool.TryParse(config.GetAttribute("加權總分排名"), out check)) checkBox6.Checked = check;
                if (bool.TryParse(config.GetAttribute("電子報表學生"), out check)) checkBoxStudent.Checked = check;
                if (bool.TryParse(config.GetAttribute("電子報表班級"), out check)) checkBoxClass.Checked = check;
                summaryFieldsChanging = false;
                #endregion
            }
            #endregion
            //變綠燈
            _waitInit.Set();
        }

        #region 初始化相關

        void ExamScoreListSubjectSelector_ColorTableChanged(object sender, EventArgs e)
        {
            SetForeColor(this);
        }

        private void SetForeColor(Control parent)
        {
            foreach (Control var in parent.Controls)
            {
                if (var is RadioButton || var is CheckBox)
                    var.ForeColor = ((Office2007Renderer)GlobalManager.Renderer).ColorTable.CheckBoxItem.Default.Text;
                SetForeColor(var);
            }
        }

        void bkwLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //comboBoxEx1.DroppedDown=false;
            comboBoxEx1.SelectedItem = null;
            comboBoxEx1.Items.Clear();
            List<string> exams = (List<string>)e.Result;
            comboBoxEx1.Items.AddRange(exams.ToArray());
            #region 回覆上次選取的試別
            XmlElement PreferenceData = SmartSchool.Customization.Data.SystemInformation.Preference["列印班級考試成績單"];
            //XmlElement PreferenceData = SmartSchool.Customization.Data.SystemInformation.Configuration["列印班級考試成績單"];
            //if ( PreferenceData == null )
            //    PreferenceData = SmartSchool.Customization.Data.SystemInformation.Preference["列印班級考試成績單"];
            if (PreferenceData != null)
            {
                if (comboBoxEx1.Items.Contains(PreferenceData.GetAttribute("LastPrintExam")))
                {
                    comboBoxEx1.SelectedIndex = comboBoxEx1.Items.IndexOf(PreferenceData.GetAttribute("LastPrintExam"));
                }
            }
            #endregion
            this.UseWaitCursor = false;
        }

        void bkwLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            ManualResetEvent _waitInit = (ManualResetEvent)e.Argument;
            #region 取得選取學生修課
            //儲存選取班級中包含學生的修課資料
            List<CourseRecord> courseRecs = new List<CourseRecord>();

            List<SmartSchool.Customization.Data.StudentRecord> students = new List<SmartSchool.Customization.Data.StudentRecord>();
            selectedClasses.AddRange(accessHelper.ClassHelper.GetSelectedClass());
            foreach (ClassRecord c in selectedClasses)
            {
                foreach (SmartSchool.Customization.Data.StudentRecord s in c.Students)
                {
                    if (!students.Contains(s))
                        students.Add(s);
                }
            }

            MultiThreadWorker<SmartSchool.Customization.Data.StudentRecord> courseLoader = new MultiThreadWorker<SmartSchool.Customization.Data.StudentRecord>();
            courseLoader.MaxThreads = 3;
            courseLoader.PackageSize = 125;
            courseLoader.PackageWorker += new EventHandler<PackageWorkEventArgs<SmartSchool.Customization.Data.StudentRecord>>(courseLoader_PackageWorker);
            courseLoader.Run(students, courseRecs);
            #endregion
            #region 取得課程考試
            MultiThreadWorker<CourseRecord> examLoader = new MultiThreadWorker<CourseRecord>();
            examLoader.MaxThreads = 2;
            examLoader.PackageSize = 200;
            examLoader.PackageWorker += new EventHandler<PackageWorkEventArgs<CourseRecord>>(examLoader_PackageWorker);
            examLoader.Run(courseRecs);
            #endregion
            #region 整理試別
            List<string> exams = new List<string>();
            foreach (CourseRecord c in courseRecs)
            {
                for (int i = 0; i < c.ExamList.Count; i++)
                {
                    if (!exams.Contains(c.ExamList[i]))
                    {
                        exams.Add(c.ExamList[i]);
                    }
                }
            }
            exams.Sort();
            #endregion
            e.Result = exams;
            //等變綠燈才可以繼續
            _waitInit.WaitOne();
        }

        void examLoader_PackageWorker(object sender, PackageWorkEventArgs<CourseRecord> e)
        {
            accessHelper.CourseHelper.FillExam(e.List);
        }

        void courseLoader_PackageWorker(object sender, PackageWorkEventArgs<SmartSchool.Customization.Data.StudentRecord> e)
        {
            accessHelper.StudentHelper.FillAttendCourse(SmartSchool.Customization.Data.SystemInformation.SchoolYear, SmartSchool.Customization.Data.SystemInformation.Semester, e.List);//抓當學年度學期
            List<CourseRecord> courseRecs = (List<CourseRecord>)e.Argument;
            //整理每個學生的修課至courseRecs
            foreach (SmartSchool.Customization.Data.StudentRecord studentRec in e.List)
            {
                foreach (StudentAttendCourseRecord attendRec in studentRec.AttendCourseList)
                {
                    CourseRecord courseRec = accessHelper.CourseHelper.GetCourse("" + attendRec.CourseID)[0];
                    lock (courseRec)
                    {
                        if (!courseRecs.Contains(courseRec))
                            courseRecs.Add(courseRec);
                    }
                }
            }
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedOnChange = true;
            this.UseWaitCursor = true;
            listView1.Items.Clear();
            List<string> subjects = new List<string>();
            foreach (ClassRecord c in selectedClasses)
            {
                foreach (SmartSchool.Customization.Data.StudentRecord s in c.Students)
                {
                    foreach (CourseRecord course in s.AttendCourseList)
                    {
                        if (course.ExamList.Contains(comboBoxEx1.Text) && !subjects.Contains(course.Subject))
                            subjects.Add(course.Subject);
                    }
                }
            }
            //照科目重新排序
            subjects.Sort(new SmartSchool.Common.StringComparer("國文", "英文", "數學", "物理", "化學", "生物", "地理", "歷史", "公民"));
            foreach (string s in subjects)
            {
                listView1.Items.Add(s);
            }
            #region 存入選取試別
            if (comboBoxEx1.Text != "" && comboBoxEx1.Text != "資料下載中...")
            {
                XmlElement PreferenceData = SmartSchool.Customization.Data.SystemInformation.Preference["列印班級考試成績單"];
                //XmlElement PreferenceData = SmartSchool.Customization.Data.SystemInformation.Configuration["列印班級考試成績單"];
                //if ( PreferenceData == null )
                //    PreferenceData = SmartSchool.Customization.Data.SystemInformation.Preference["列印班級考試成績單"];
                if (PreferenceData == null)
                {
                    PreferenceData = new XmlDocument().CreateElement("列印班級考試成績單");
                }
                PreferenceData.SetAttribute("LastPrintExam", comboBoxEx1.Text);
                SmartSchool.Customization.Data.SystemInformation.Preference["列印班級考試成績單"] = PreferenceData;
                //SmartSchool.Customization.Data.SystemInformation.Configuration["列印班級考試成績單"] = PreferenceData;
            }
            #endregion
            SetListViewChecked();
            this.UseWaitCursor = false;
            checkedOnChange = false;
        }

        private void SetListViewChecked()
        {
            Dictionary<string, bool> checkedSubjects = new Dictionary<string, bool>();
            XmlElement PreferenceData = SmartSchool.Customization.Data.SystemInformation.Preference["列印班級考試成績單"];
            //XmlElement PreferenceData = SmartSchool.Customization.Data.SystemInformation.Configuration["列印班級考試成績單"];
            //if ( PreferenceData == null )
            //    PreferenceData = SmartSchool.Customization.Data.SystemInformation.Preference["列印班級考試成績單"];
            if (PreferenceData != null)
            {
                foreach (XmlNode node in PreferenceData.SelectNodes("Subject"))
                {
                    XmlElement element = (XmlElement)node;
                    if (!checkedSubjects.ContainsKey(element.GetAttribute("Name")))
                        checkedSubjects.Add(element.GetAttribute("Name"), bool.Parse(element.GetAttribute("Checked")));
                }
            }
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = (checkedSubjects.ContainsKey(item.Text) ? checkedSubjects[item.Text] : true);
            }
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //變色
            e.Item.ForeColor = e.Item.Checked ? listView1.ForeColor : Color.BlueViolet;
            //允許列印
            buttonX1.Enabled = listView1.CheckedItems.Count > 0;

            #region 儲存選取狀態
            if (checkedOnChange) return;
            XmlElement PreferenceData = SmartSchool.Customization.Data.SystemInformation.Preference["列印班級考試成績單"];
            //XmlElement PreferenceData = SmartSchool.Customization.Data.SystemInformation.Configuration["列印班級考試成績單"];
            //if ( PreferenceData == null )
            //    PreferenceData = SmartSchool.Customization.Data.SystemInformation.Preference["列印班級考試成績單"];
            if (PreferenceData == null)
            {
                PreferenceData = new XmlDocument().CreateElement("列印班級考試成績單");
            }
            XmlElement node = (XmlElement)PreferenceData.SelectSingleNode("Subject[@Name='" + e.Item.Text + "']");
            if (node == null)
            {
                node = (XmlElement)PreferenceData.AppendChild(PreferenceData.OwnerDocument.CreateElement("Subject"));
                node.SetAttribute("Name", e.Item.Text);
            }
            node.SetAttribute("Checked", "" + e.Item.Checked);
            SmartSchool.Customization.Data.SystemInformation.Preference["列印班級考試成績單"] = PreferenceData;
            //SmartSchool.Customization.Data.SystemInformation.Configuration["列印班級考試成績單"] = PreferenceData;
            #endregion
        }

        #endregion

        #region 列印報表

        private void buttonX1_Click(object se, EventArgs ea)
        {
            bool epaper = checkBoxStudent.Checked || checkBoxClass.Checked;
            SmartSchool.ePaper.ElectronicPaper paperForStudent = new SmartSchool.ePaper.ElectronicPaper(comboBoxEx1.Text + "考試成績單",
                SmartSchool.Customization.Data.SystemInformation.SchoolYear.ToString(),
                SmartSchool.Customization.Data.SystemInformation.Semester.ToString(),
                 SmartSchool.ePaper.ViewerType.Student);
            SmartSchool.ePaper.ElectronicPaper paperForClass = new SmartSchool.ePaper.ElectronicPaper(comboBoxEx1.Text + "考試成績單",
                SmartSchool.Customization.Data.SystemInformation.SchoolYear.ToString(),
                SmartSchool.Customization.Data.SystemInformation.Semester.ToString(),
                 SmartSchool.ePaper.ViewerType.Class);

            List<string> printSubjects = new List<string>();
            string printExam = comboBoxEx1.Text;
            foreach (ListViewItem var in listView1.CheckedItems)
            {
                printSubjects.Add(var.Text);
            }
            BackgroundWorker bkw = new BackgroundWorker();
            bkw.WorkerReportsProgress = true;
            bkw.DoWork += delegate(object sender, DoWorkEventArgs e)
            {
                bkw.ReportProgress(1);
                MemoryStream template = new MemoryStream(radioBtn1.Checked ? Properties.Resources.班級考試成績單 : customizeTemplateBuffer);
                Document doc = new Document();
                doc.Sections.Clear();

                double progress = 0;
                foreach (ClassRecord classRec in selectedClasses)
                {
                    #region 每個班級分別計算
                    //學生的各欄位資料
                    Dictionary<SmartSchool.Customization.Data.StudentRecord, Dictionary<string, string>> classExamScoreTable = new Dictionary<SmartSchool.Customization.Data.StudentRecord, Dictionary<string, string>>();
                    //抓學生考試成績
                    accessHelper.StudentHelper.FillExamScore(SmartSchool.Customization.Data.SystemInformation.SchoolYear, SmartSchool.Customization.Data.SystemInformation.Semester, classRec.Students);
                    //整理列印科目+級別+學分數
                    List<string> groups = new List<string>();
                    #region 整理學生修課紀錄
                    //參予排序的學生
                    Dictionary<SmartSchool.Customization.Data.StudentRecord, decimal> canRankList = new Dictionary<SmartSchool.Customization.Data.StudentRecord, decimal>();
                    Dictionary<SmartSchool.Customization.Data.StudentRecord, decimal> canRankList2 = new Dictionary<SmartSchool.Customization.Data.StudentRecord, decimal>();
                    Dictionary<SmartSchool.Customization.Data.StudentRecord, decimal> canRankList3 = new Dictionary<SmartSchool.Customization.Data.StudentRecord, decimal>();
                    //記錄每個學生有未評分的KEY(如果整班都未評分就不管，但如果發現有評分就要把學生從可排序清單中移除)
                    Dictionary<SmartSchool.Customization.Data.StudentRecord, List<string>> nonScoreKeys = new Dictionary<SmartSchool.Customization.Data.StudentRecord, List<string>>();
                    foreach (SmartSchool.Customization.Data.StudentRecord studentRec in classRec.Students)
                    {
                        //加入table
                        classExamScoreTable.Add(studentRec, new Dictionary<string, string>());
                        //加權總分
                        decimal scoreCount = 0;
                        //參加排名
                        bool canRank = true;
                        //總學分數
                        int CreditCount = 0;
                        //總分
                        decimal sum = 0;

                        foreach (StudentAttendCourseRecord attendRec in studentRec.AttendCourseList)
                        {
                            if (printSubjects.Contains(attendRec.Subject) && attendRec.ExamList.Contains(printExam))
                            {
                                //把科目、級別、學分數兜成 "_科目_級別_學分數_"的字串，這個字串在不同科目級別學分數會成為唯一值
                                string key = attendRec.Subject + "^_^" + attendRec.SubjectLevel + "^_^" + attendRec.Credit;
                                bool hasScore = false;
                                #region 檢查這個KEY有沒有評分同時計算總分平均及是否可排名
                                foreach (ExamScoreInfo examScore in studentRec.ExamScoreList)
                                {
                                    if (examScore.ExamName == printExam && key == examScore.Subject + "^_^" + examScore.SubjectLevel + "^_^" + examScore.Credit)
                                    {
                                        //是要列印的科目
                                        if (!groups.Contains(key))
                                            groups.Add(key);
                                        hasScore = true;
                                        if (examScore.SpecialCase == "")//一般正常成績
                                        {
                                            if (!classExamScoreTable[studentRec].ContainsKey(key))
                                                classExamScoreTable[studentRec].Add(key, examScore.ExamScore.ToString());
                                            else
                                                classExamScoreTable[studentRec][key] = examScore.ExamScore.ToString();
                                            //加權總分
                                            scoreCount += examScore.ExamScore * examScore.Credit;
                                            //學分
                                            CreditCount += examScore.Credit;
                                            //總分
                                            sum += examScore.ExamScore;
                                        }
                                        else//特殊成績狀況
                                        {
                                            canRank = false;
                                            if (!classExamScoreTable[studentRec].ContainsKey(key))
                                                classExamScoreTable[studentRec].Add(key, examScore.SpecialCase);
                                            else
                                                classExamScoreTable[studentRec][key] = examScore.SpecialCase;
                                        }
                                    }
                                }
                                #endregion
                                //發現沒有評分
                                if (!hasScore)
                                {
                                    #region 加入學生未評分科目
                                    if (!nonScoreKeys.ContainsKey(studentRec))
                                        nonScoreKeys.Add(studentRec, new List<string>());
                                    if (!nonScoreKeys[studentRec].Contains(key))
                                        nonScoreKeys[studentRec].Add(key);
                                    #endregion
                                    classExamScoreTable[studentRec].Add(key, "未輸入");
                                }
                            }
                        }
                        classExamScoreTable[studentRec].Add("加權總分", scoreCount.ToString());
                        classExamScoreTable[studentRec].Add("加權平均", (scoreCount / (CreditCount == 0 ? 1 : CreditCount)).ToString(".00"));
                        classExamScoreTable[studentRec].Add("總分", sum.ToString());
                        if (canRank)
                        {
                            canRankList.Add(studentRec, decimal.Parse((scoreCount / (CreditCount == 0 ? 1 : CreditCount)).ToString(".00")));
                            canRankList2.Add(studentRec, sum);
                            canRankList3.Add(studentRec, scoreCount);
                        }
                    }
                    //如果學生在要列印科目中發現未評分項目則從可排名清單中移除
                    #region 如果學生在要列印科目中發現未評分項目則從可排名清單中移除
                    foreach (SmartSchool.Customization.Data.StudentRecord stu in nonScoreKeys.Keys)
                    {
                        foreach (string key in nonScoreKeys[stu])
                        {
                            if (groups.Contains(key) && canRankList.ContainsKey(stu))
                            {
                                canRankList.Remove(stu);
                                canRankList2.Remove(stu);
                                canRankList3.Remove(stu);
                            }
                        }
                    }
                    #endregion
                    List<decimal> rankScoreList = new List<decimal>();
                    rankScoreList.AddRange(canRankList.Values);
                    rankScoreList.Sort();
                    rankScoreList.Reverse();
                    List<decimal> rankScoreList2 = new List<decimal>();
                    rankScoreList2.AddRange(canRankList2.Values);
                    rankScoreList2.Sort();
                    rankScoreList2.Reverse();
                    List<decimal> rankScoreList3 = new List<decimal>();
                    rankScoreList3.AddRange(canRankList3.Values);
                    rankScoreList3.Sort();
                    rankScoreList3.Reverse();
                    foreach (SmartSchool.Customization.Data.StudentRecord stuRec in classExamScoreTable.Keys)
                    {
                        if (canRankList.ContainsKey(stuRec))
                            classExamScoreTable[stuRec].Add("加權平均排名", "" + (rankScoreList.IndexOf(decimal.Parse(classExamScoreTable[stuRec]["加權平均"])) + 1));
                        else
                            classExamScoreTable[stuRec].Add("加權平均排名", "");

                        if (canRankList2.ContainsKey(stuRec))
                            classExamScoreTable[stuRec].Add("總分排名", "" + (rankScoreList2.IndexOf(decimal.Parse(classExamScoreTable[stuRec]["總分"])) + 1));
                        else
                            classExamScoreTable[stuRec].Add("總分排名", "");

                        if (canRankList3.ContainsKey(stuRec))
                            classExamScoreTable[stuRec].Add("加權總分排名", "" + (rankScoreList3.IndexOf(decimal.Parse(classExamScoreTable[stuRec]["加權總分"])) + 1));
                        else
                            classExamScoreTable[stuRec].Add("加權總分排名", "");
                    }
                    #endregion
                    //排序要列印的科目
                    groups.Sort(new SmartSchool.Common.StringComparer("國文", "英文", "數學", "物理", "化學", "生物", "地理", "歷史", "公民"));

                    #region 一個班級產生一個新的Document
                    //一個學生產生一個新的Document
                    Document each_page = new Document(template, "", LoadFormat.Doc, "");
                    #region 建立此班級的成績單
                    //合併基本資料
                    //其它統計欄位
                    List<string> otherList = new List<string>();
                    foreach (CheckBox var in new CheckBox[] { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6 })
                    {
                        if (var.Checked)
                            otherList.Add(var.Text);
                    }
                    string[] merge_keys = new string[] { "學校名稱", "班級名稱", "考試名稱", "考試成績" };
                    object[] merge_values = new object[] { 
                        SmartSchool.Customization.Data.SystemInformation.SchoolChineseName ,
                        classRec.ClassName, 
                        printExam, 
                        new object[] { groups, classExamScoreTable ,otherList} };

                    each_page.MailMerge.MergeField += new Aspose.Words.Reporting.MergeFieldEventHandler(MailMerge_MergeField);
                    each_page.MailMerge.Execute(merge_keys, merge_values);
                    #endregion
                    //合併至doc
                    doc.Sections.Add(doc.ImportNode(each_page.Sections[0], true));

                    if (epaper)
                    {
                        MemoryStream stream = new MemoryStream();
                        each_page.Save(stream, SaveFormat.Doc);
                        paperForClass.Append(new SmartSchool.ePaper.PaperItem(SmartSchool.ePaper.PaperFormat.Office2003Doc, stream, classRec.ClassID));
                        foreach (StudentRecord studentRec in classRec.Students)
                        {
                            paperForStudent.Append(new SmartSchool.ePaper.PaperItem(SmartSchool.ePaper.PaperFormat.Office2003Doc, stream, studentRec.StudentID));
                        }
                    }
                    #endregion
                    bkw.ReportProgress((int)(++progress * 100.0d / selectedClasses.Count));
                    #endregion
                }
                e.Result = doc;
            };
            bkw.ProgressChanged += delegate(object sender, ProgressChangedEventArgs e) { SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage("班級考試成績單產生中...", e.ProgressPercentage); };
            bkw.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
            {
                #region 儲存
                SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage("班級考試成績單產生完成");
                Document doc = (Document)e.Result;

                string reportName = "班級考試成績單";
                string path = Path.Combine(Application.StartupPath, "Reports");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = Path.Combine(path, reportName + ".doc");

                if (File.Exists(path))
                {
                    int i = 1;
                    while (true)
                    {
                        string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + (i++) + Path.GetExtension(path);
                        if (!File.Exists(newPath))
                        {
                            path = newPath;
                            break;
                        }
                    }
                }

                try
                {
                    doc.Save(path, SaveFormat.Doc);
                    System.Diagnostics.Process.Start(path);
                }
                catch
                {
                    SaveFileDialog sd = new SaveFileDialog();
                    sd.Title = "另存新檔";
                    sd.FileName = reportName + ".doc";
                    sd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
                    if (sd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            doc.Save(sd.FileName, SaveFormat.AsposePdf);
                        }
                        catch
                        {
                            MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                #endregion
                if (epaper)
                {
                    if (checkBoxStudent.Checked) SmartSchool.ePaper.DispatcherProvider.Dispatch(paperForStudent);
                    if (checkBoxClass.Checked) SmartSchool.ePaper.DispatcherProvider.Dispatch(paperForClass);
                }
            };
            bkw.RunWorkerAsync();
            this.Close();
        }

        void MailMerge_MergeField(object sender, Aspose.Words.Reporting.MergeFieldEventArgs e)
        {
            if (e.FieldName == "考試成績")
            {
                e.Text = string.Empty;
                //整理列印科目+級別+學分數
                List<string> groups = (List<string>)((object[])e.FieldValue)[0];
                //全班學生成績資料
                Dictionary<SmartSchool.Customization.Data.StudentRecord, Dictionary<string, string>> classExamScoreTable = (Dictionary<SmartSchool.Customization.Data.StudentRecord, Dictionary<string, string>>)((object[])e.FieldValue)[1];
                //其它統計欄位
                List<string> otherList = (List<string>)((object[])e.FieldValue)[2];
                //每個科目的總分
                Dictionary<string, decimal> groupSum = new Dictionary<string, decimal>();
                //每個科目有分數的人數
                Dictionary<string, int> groupCount = new Dictionary<string, int>();

                DocumentBuilder builder = new DocumentBuilder(e.Document);
                Cell currentCell;
                builder.RowFormat.AllowBreakAcrossPages = true;
                builder.MoveToField(e.Field, false);
                #region 取得外框寬度並計算欄寬
                Cell SCell = (Cell)builder.CurrentParagraph.ParentNode;
                double Swidth = SCell.CellFormat.Width;
                double microUnit = Swidth / (groups.Count + otherList.Count + 3); //姓名*2、座號跟每科成績各一份
                #endregion
                Table table = builder.StartTable();

                builder.CellFormat.ClearFormatting();
                builder.CellFormat.Borders.LineWidth = 0.5;

                builder.RowFormat.HeightRule = HeightRule.Auto;
                builder.RowFormat.Height = builder.Font.Size * 1.2d;
                builder.RowFormat.Alignment = RowAlignment.Center;
                builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                builder.CellFormat.LeftPadding = 3.0;
                builder.CellFormat.RightPadding = 3.0;
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                builder.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
                builder.ParagraphFormat.LineSpacing = 10;
                #region 填表頭
                builder.InsertCell().CellFormat.Width = microUnit * 2;
                builder.CellFormat.Borders.Right.LineWidth = 0.25;
                builder.Write("姓名");
                builder.InsertCell().CellFormat.Width = microUnit;
                builder.CellFormat.Borders.Right.LineWidth = 0.25;
                builder.Write("座號");
                foreach (string key in groups)
                {
                    #region 每科給一欄
                    builder.InsertCell().CellFormat.Width = microUnit;
                    builder.CellFormat.VerticalMerge = CellMerge.None;
                    builder.CellFormat.Borders.Right.LineWidth = 0.25;
                    string[] list = key.Split(new string[] { "^_^" }, StringSplitOptions.None);
                    builder.Write(list[0] + GetNumberString(list[1]));
                    #endregion
                }
                foreach (string key in otherList)
                {
                    #region 統計欄位
                    builder.InsertCell().CellFormat.Width = microUnit;
                    builder.CellFormat.VerticalMerge = CellMerge.First;
                    if (otherList[otherList.Count - 1] != key)
                        builder.CellFormat.Borders.Right.LineWidth = 0.25;
                    builder.Write(key);
                    #endregion
                }
                builder.EndRow();
                #endregion

                #region 填學分數
                currentCell = builder.InsertCell();
                currentCell.CellFormat.Width = microUnit * 3;
                builder.CellFormat.Borders.Right.LineWidth = 0.25;
                currentCell.FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Right;
                builder.Write("學分數");
                foreach (string key in groups)
                {
                    #region 每科給一欄
                    currentCell = builder.InsertCell();
                    currentCell.CellFormat.Width = microUnit;
                    builder.CellFormat.VerticalMerge = CellMerge.None;
                    currentCell.FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                    if (otherList.Count > 0 || groups[groups.Count - 1] != key)
                        builder.CellFormat.Borders.Right.LineWidth = 0.25;
                    string[] list = key.Split(new string[] { "^_^" }, StringSplitOptions.None);
                    builder.Write(list[2]);
                    #endregion
                }
                foreach (string key in otherList)
                {
                    #region 統計欄位

                    currentCell = builder.InsertCell();
                    currentCell.CellFormat.Width = microUnit;
                    builder.CellFormat.VerticalMerge = CellMerge.Previous;
                    currentCell.FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                    if (otherList[otherList.Count - 1] != key)
                        builder.CellFormat.Borders.Right.LineWidth = 0.25;
                    #endregion
                }
                builder.EndRow();
                #endregion
                //畫雙線
                foreach (Cell cell in table.LastRow.Cells)
                    cell.CellFormat.Borders.Bottom.LineStyle = LineStyle.Double;

                builder.CellFormat.VerticalMerge = CellMerge.None;
                foreach (SmartSchool.Customization.Data.StudentRecord studentRec in classExamScoreTable.Keys)
                {
                    #region 填學生資料
                    //姓名
                    builder.InsertCell().CellFormat.Width = microUnit * 2;
                    builder.CellFormat.Borders.Right.LineWidth = 0.25;
                    builder.Write(studentRec.StudentName);
                    //座號
                    builder.InsertCell().CellFormat.Width = microUnit;
                    builder.CellFormat.Borders.Right.LineWidth = 0.25;
                    builder.Write(studentRec.SeatNo);
                    foreach (string key in groups)
                    {
                        #region 各科成績
                        builder.InsertCell().CellFormat.Width = microUnit;
                        if (otherList.Count > 0 || groups[groups.Count - 1] != key)
                            builder.CellFormat.Borders.Right.LineWidth = 0.25;
                        if (classExamScoreTable[studentRec].ContainsKey(key))
                        {
                            builder.Write(classExamScoreTable[studentRec][key]);
                            #region 算各科平均
                            decimal score;
                            if (decimal.TryParse(classExamScoreTable[studentRec][key], out score))
                            {
                                if (!groupSum.ContainsKey(key))
                                    groupSum.Add(key, 0m);
                                if (!groupCount.ContainsKey(key))
                                    groupCount.Add(key, 0);
                                groupCount[key]++;
                                groupSum[key] += score;
                            }
                            #endregion
                        }
                        else
                            builder.Write("--");
                        #endregion
                    }
                    foreach (string key in otherList)
                    {
                        #region 統計欄位
                        builder.InsertCell().CellFormat.Width = microUnit;
                        if (otherList[otherList.Count - 1] != key)
                            builder.CellFormat.Borders.Right.LineWidth = 0.25;
                        if (classExamScoreTable[studentRec].ContainsKey(key))
                        {
                            builder.Write(classExamScoreTable[studentRec][key]);
                        }
                        else
                            builder.Write("--");
                        #endregion
                    }
                    builder.EndRow();
                    #endregion
                }
                //畫雙線
                foreach (Cell cell in table.LastRow.Cells)
                    cell.CellFormat.Borders.Bottom.LineStyle = LineStyle.Double;

                #region 填平均
                currentCell = builder.InsertCell();
                currentCell.CellFormat.Width = microUnit * 3;
                currentCell.FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Right;
                builder.CellFormat.Borders.Right.LineWidth = 0.25;
                builder.Write("平均");
                foreach (string key in groups)
                {
                    #region 各科平均
                    currentCell = builder.InsertCell();
                    currentCell.CellFormat.Width = microUnit;
                    currentCell.FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                    if (otherList.Count > 0 || groups[groups.Count - 1] != key)
                        builder.CellFormat.Borders.Right.LineWidth = 0.25;
                    if (groupSum.ContainsKey(key))
                    {
                        builder.Write((groupSum[key] / (groupCount[key] == 0 ? 1 : groupCount[key])).ToString(".0"));
                    }
                    else
                        builder.Write("--");
                    #endregion
                }
                if (otherList.Count > 0)
                {
                    builder.InsertCell().CellFormat.Width = microUnit * otherList.Count;
                    builder.CellFormat.Borders.Right.LineWidth = 0.25;
                }
                builder.EndRow();
                #endregion
                #region 去除表格四邊的線
                foreach (Cell cell in table.FirstRow.Cells)
                    cell.CellFormat.Borders.Top.LineStyle = LineStyle.None;

                //foreach ( Cell cell in table.LastRow.Cells )
                //    cell.CellFormat.Borders.Bottom.LineStyle = LineStyle.None;

                foreach (Row row in table.Rows)
                {
                    row.FirstCell.CellFormat.Borders.Left.LineStyle = LineStyle.None;
                    row.LastCell.CellFormat.Borders.Right.LineStyle = LineStyle.None;
                }
                #endregion
            }
        }

        private string GetNumberString(string p)
        {
            string levelNumber;
            switch (p.Trim())
            {
                #region 對應levelNumber
                case "1":
                    levelNumber = "Ⅰ";
                    break;
                case "2":
                    levelNumber = "Ⅱ";
                    break;
                case "3":
                    levelNumber = "Ⅲ";
                    break;
                case "4":
                    levelNumber = "Ⅳ";
                    break;
                case "5":
                    levelNumber = "Ⅴ";
                    break;
                case "6":
                    levelNumber = "Ⅵ";
                    break;
                case "7":
                    levelNumber = "Ⅶ";
                    break;
                case "8":
                    levelNumber = "Ⅷ";
                    break;
                case "9":
                    levelNumber = "Ⅸ";
                    break;
                case "10":
                    levelNumber = "Ⅹ";
                    break;
                default:
                    levelNumber = p;
                    break;
                #endregion
            }
            return levelNumber;
        }

        #endregion

        #region 列印範本相關
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            buttonX2.Expanded = false;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "班級考試成績單.doc";
            sfd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    fs.Write(Properties.Resources.班級考試成績單, 0, Properties.Resources.班級考試成績單.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            buttonX2.Expanded = false;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "選擇自訂的班級考試成績單範本";
            ofd.Filter = "Word檔案 (*.doc)|*.doc";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (Document.DetectFileFormat(ofd.FileName) == LoadFormat.Doc)
                    {
                        FileStream fs = new FileStream(ofd.FileName, FileMode.Open);

                        byte[] tempBuffer = new byte[fs.Length];
                        fs.Read(tempBuffer, 0, tempBuffer.Length);
                        fs.Close();
                        #region 存入樣板
                        customizeTemplateBuffer = tempBuffer;
                        radioBtn2.Enabled = linkLabel2.Enabled = true;
                        XmlElement config = SmartSchool.Customization.Data.SystemInformation.Preference["班級考試成績單"];
                        //XmlElement config = SmartSchool.Customization.Data.SystemInformation.Configuration["班級考試成績單"];
                        //if ( config == null )
                        //    config = SmartSchool.Customization.Data.SystemInformation.Preference["班級考試成績單"];
                        if (config == null)
                            config = new XmlDocument().CreateElement("班級考試成績單");
                        XmlElement customize1 = (XmlElement)config.SelectSingleNode("自訂樣板");
                        if (customize1 == null)
                            customize1 = (XmlElement)config.AppendChild(config.OwnerDocument.CreateElement("自訂樣板"));
                        customize1.InnerText = Convert.ToBase64String(customizeTemplateBuffer);

                        SmartSchool.Customization.Data.SystemInformation.Preference["班級考試成績單"] = config;
                        //SmartSchool.Customization.Data.SystemInformation.Configuration["班級考試成績單"] = config;
                        #endregion

                        MsgBox.Show("上傳成功。");
                    }
                    else
                        MsgBox.Show("上傳檔案格式不符");
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "開啟檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            buttonX2.Expanded = true;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            buttonX2.Expanded = false;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "自訂班級考試成績單範本.doc";
            sfd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    if (Aspose.Words.Document.DetectFileFormat(new MemoryStream(customizeTemplateBuffer)) == Aspose.Words.LoadFormat.Doc)
                        fs.Write(customizeTemplateBuffer, 0, customizeTemplateBuffer.Length);
                    else
                        fs.Write(Properties.Resources.班級考試成績單, 0, Properties.Resources.班級考試成績單.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void radioBtn_CheckedChanged(object sender, EventArgs e)
        {
            #region 存入樣板
            XmlElement config = SmartSchool.Customization.Data.SystemInformation.Preference["班級考試成績單"];
            //XmlElement config = SmartSchool.Customization.Data.SystemInformation.Configuration["班級考試成績單"];
            //if ( config == null )
            //    config = SmartSchool.Customization.Data.SystemInformation.Preference["班級考試成績單"];
            if (config == null)
                config = new XmlDocument().CreateElement("班級考試成績單");
            config.SetAttribute("列印樣板", radioBtn1.Checked ? "預設" : "自訂");

            SmartSchool.Customization.Data.SystemInformation.Preference["班級考試成績單"] = config;
            //SmartSchool.Customization.Data.SystemInformation.Configuration["班級考試成績單"] = config;
            #endregion
        }
        #endregion
        //設定統計欄位
        private void summaryChanged(object sender, EventArgs e)
        {
            if (summaryFieldsChanging) return;
            #region 存入樣板
            XmlElement config = SmartSchool.Customization.Data.SystemInformation.Configuration["班級考試成績單"];
            if (config == null)
                config = SmartSchool.Customization.Data.SystemInformation.Preference["班級考試成績單"];
            if (config == null)
                config = new XmlDocument().CreateElement("班級考試成績單");

            config.SetAttribute("總分", "" + checkBox1.Checked);
            config.SetAttribute("總分排名", "" + checkBox2.Checked);
            config.SetAttribute("加權總分", "" + checkBox3.Checked);
            config.SetAttribute("加權平均", "" + checkBox4.Checked);
            config.SetAttribute("加權平均排名", "" + checkBox5.Checked);
            config.SetAttribute("加權總分排名", "" + checkBox6.Checked);
            config.SetAttribute("電子報表學生", "" + checkBoxStudent.Checked);
            config.SetAttribute("電子報表班級", "" + checkBoxClass.Checked);

            SmartSchool.Customization.Data.SystemInformation.Preference["班級考試成績單"] = config;
            //SmartSchool.Customization.Data.SystemInformation.Configuration["班級考試成績單"] = config;
            #endregion
        }
    }
}

