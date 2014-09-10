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
            //�����p��f�Ьݬ���O
            ManualResetEvent _waitInit = new ManualResetEvent(true);
            //�ܬ��O
            _waitInit.Reset();
            #region �I�����J�ǥͭ׽үŽҵ{�Ҹո�T
            BackgroundWorker bkwLoader = new BackgroundWorker();
            bkwLoader.DoWork += new DoWorkEventHandler(bkwLoader_DoWork);
            bkwLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkwLoader_RunWorkerCompleted);
            bkwLoader.RunWorkerAsync(_waitInit);
            #endregion
            //��l�ƪ��
            InitializeComponent();
            this.UseWaitCursor = true;
            comboBoxEx1.Items.Add("��ƤU����...");
            comboBoxEx1.SelectedIndex = 0;
            #region �p�G�t�Ϊ�Renderer�OOffice2007Renderer�A�P��_ClassTeacherView,_CategoryView���C��
            if (GlobalManager.Renderer is Office2007Renderer)
            {
                ((Office2007Renderer)GlobalManager.Renderer).ColorTableChanged += new EventHandler(ExamScoreListSubjectSelector_ColorTableChanged);
                SetForeColor(this);
            }
            #endregion
            this.controlContainerItem1.Control = this.panelEx3;
            this.controlContainerItem2.Control = this.panelEx2;
            #region Ū��Preference
            XmlElement config = SmartSchool.Customization.Data.SystemInformation.Preference["�Z�ŦҸզ��Z��"];
            ////XmlElement config = SmartSchool.Customization.Data.SystemInformation.Configuration["�C�L�Z�ŦҸզ��Z��"];
            ////if ( config == null )
            ////    config = SmartSchool.Customization.Data.SystemInformation.Preference["�C�L�Z�ŦҸզ��Z��"];
            if (config != null)
            {
                #region �ۭq�˪O
                XmlElement customize1 = (XmlElement)config.SelectSingleNode("�ۭq�˪O");

                if (customize1 != null)
                {
                    string templateBase64 = customize1.InnerText;
                    customizeTemplateBuffer = Convert.FromBase64String(templateBase64);
                    radioBtn2.Enabled = linkLabel2.Enabled = true;
                }
                #endregion
                if (config.HasAttribute("�C�L�˪O") && config.GetAttribute("�C�L�˪O") == "�ۭq")
                    radioBtn2.Checked = true;

                #region �έp���
                summaryFieldsChanging = true;
                bool check = false;
                if (bool.TryParse(config.GetAttribute("�`��"), out check)) checkBox1.Checked = check;
                if (bool.TryParse(config.GetAttribute("�`���ƦW"), out check)) checkBox2.Checked = check;
                if (bool.TryParse(config.GetAttribute("�[�v�`��"), out check)) checkBox3.Checked = check;
                if (bool.TryParse(config.GetAttribute("�[�v����"), out check)) checkBox4.Checked = check;
                if (bool.TryParse(config.GetAttribute("�[�v�����ƦW"), out check)) checkBox5.Checked = check;
                if (bool.TryParse(config.GetAttribute("�[�v�`���ƦW"), out check)) checkBox6.Checked = check;
                if (bool.TryParse(config.GetAttribute("�q�l����ǥ�"), out check)) checkBoxStudent.Checked = check;
                if (bool.TryParse(config.GetAttribute("�q�l����Z��"), out check)) checkBoxClass.Checked = check;
                summaryFieldsChanging = false;
                #endregion
            }
            #endregion
            //�ܺ�O
            _waitInit.Set();
        }

        #region ��l�Ƭ���

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
            #region �^�ФW��������էO
            XmlElement PreferenceData = SmartSchool.Customization.Data.SystemInformation.Preference["�C�L�Z�ŦҸզ��Z��"];
            //XmlElement PreferenceData = SmartSchool.Customization.Data.SystemInformation.Configuration["�C�L�Z�ŦҸզ��Z��"];
            //if ( PreferenceData == null )
            //    PreferenceData = SmartSchool.Customization.Data.SystemInformation.Preference["�C�L�Z�ŦҸզ��Z��"];
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
            #region ���o����ǥͭ׽�
            //�x�s����Z�Ť��]�t�ǥͪ��׽Ҹ��
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
            #region ���o�ҵ{�Ҹ�
            MultiThreadWorker<CourseRecord> examLoader = new MultiThreadWorker<CourseRecord>();
            examLoader.MaxThreads = 2;
            examLoader.PackageSize = 200;
            examLoader.PackageWorker += new EventHandler<PackageWorkEventArgs<CourseRecord>>(examLoader_PackageWorker);
            examLoader.Run(courseRecs);
            #endregion
            #region ��z�էO
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
            //���ܺ�O�~�i�H�~��
            _waitInit.WaitOne();
        }

        void examLoader_PackageWorker(object sender, PackageWorkEventArgs<CourseRecord> e)
        {
            accessHelper.CourseHelper.FillExam(e.List);
        }

        void courseLoader_PackageWorker(object sender, PackageWorkEventArgs<SmartSchool.Customization.Data.StudentRecord> e)
        {
            accessHelper.StudentHelper.FillAttendCourse(SmartSchool.Customization.Data.SystemInformation.SchoolYear, SmartSchool.Customization.Data.SystemInformation.Semester, e.List);//���Ǧ~�׾Ǵ�
            List<CourseRecord> courseRecs = (List<CourseRecord>)e.Argument;
            //��z�C�Ӿǥͪ��׽Ҧ�courseRecs
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
            //�Ӭ�ح��s�Ƨ�
            subjects.Sort(new SmartSchool.Common.StringComparer("���", "�^��", "�ƾ�", "���z", "�ƾ�", "�ͪ�", "�a�z", "���v", "����"));
            foreach (string s in subjects)
            {
                listView1.Items.Add(s);
            }
            #region �s�J����էO
            if (comboBoxEx1.Text != "" && comboBoxEx1.Text != "��ƤU����...")
            {
                XmlElement PreferenceData = SmartSchool.Customization.Data.SystemInformation.Preference["�C�L�Z�ŦҸզ��Z��"];
                //XmlElement PreferenceData = SmartSchool.Customization.Data.SystemInformation.Configuration["�C�L�Z�ŦҸզ��Z��"];
                //if ( PreferenceData == null )
                //    PreferenceData = SmartSchool.Customization.Data.SystemInformation.Preference["�C�L�Z�ŦҸզ��Z��"];
                if (PreferenceData == null)
                {
                    PreferenceData = new XmlDocument().CreateElement("�C�L�Z�ŦҸզ��Z��");
                }
                PreferenceData.SetAttribute("LastPrintExam", comboBoxEx1.Text);
                SmartSchool.Customization.Data.SystemInformation.Preference["�C�L�Z�ŦҸզ��Z��"] = PreferenceData;
                //SmartSchool.Customization.Data.SystemInformation.Configuration["�C�L�Z�ŦҸզ��Z��"] = PreferenceData;
            }
            #endregion
            SetListViewChecked();
            this.UseWaitCursor = false;
            checkedOnChange = false;
        }

        private void SetListViewChecked()
        {
            Dictionary<string, bool> checkedSubjects = new Dictionary<string, bool>();
            XmlElement PreferenceData = SmartSchool.Customization.Data.SystemInformation.Preference["�C�L�Z�ŦҸզ��Z��"];
            //XmlElement PreferenceData = SmartSchool.Customization.Data.SystemInformation.Configuration["�C�L�Z�ŦҸզ��Z��"];
            //if ( PreferenceData == null )
            //    PreferenceData = SmartSchool.Customization.Data.SystemInformation.Preference["�C�L�Z�ŦҸզ��Z��"];
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
            //�ܦ�
            e.Item.ForeColor = e.Item.Checked ? listView1.ForeColor : Color.BlueViolet;
            //���\�C�L
            buttonX1.Enabled = listView1.CheckedItems.Count > 0;

            #region �x�s������A
            if (checkedOnChange) return;
            XmlElement PreferenceData = SmartSchool.Customization.Data.SystemInformation.Preference["�C�L�Z�ŦҸզ��Z��"];
            //XmlElement PreferenceData = SmartSchool.Customization.Data.SystemInformation.Configuration["�C�L�Z�ŦҸզ��Z��"];
            //if ( PreferenceData == null )
            //    PreferenceData = SmartSchool.Customization.Data.SystemInformation.Preference["�C�L�Z�ŦҸզ��Z��"];
            if (PreferenceData == null)
            {
                PreferenceData = new XmlDocument().CreateElement("�C�L�Z�ŦҸզ��Z��");
            }
            XmlElement node = (XmlElement)PreferenceData.SelectSingleNode("Subject[@Name='" + e.Item.Text + "']");
            if (node == null)
            {
                node = (XmlElement)PreferenceData.AppendChild(PreferenceData.OwnerDocument.CreateElement("Subject"));
                node.SetAttribute("Name", e.Item.Text);
            }
            node.SetAttribute("Checked", "" + e.Item.Checked);
            SmartSchool.Customization.Data.SystemInformation.Preference["�C�L�Z�ŦҸզ��Z��"] = PreferenceData;
            //SmartSchool.Customization.Data.SystemInformation.Configuration["�C�L�Z�ŦҸզ��Z��"] = PreferenceData;
            #endregion
        }

        #endregion

        #region �C�L����

        private void buttonX1_Click(object se, EventArgs ea)
        {
            bool epaper = checkBoxStudent.Checked || checkBoxClass.Checked;
            SmartSchool.ePaper.ElectronicPaper paperForStudent = new SmartSchool.ePaper.ElectronicPaper(comboBoxEx1.Text + "�Ҹզ��Z��",
                SmartSchool.Customization.Data.SystemInformation.SchoolYear.ToString(),
                SmartSchool.Customization.Data.SystemInformation.Semester.ToString(),
                 SmartSchool.ePaper.ViewerType.Student);
            SmartSchool.ePaper.ElectronicPaper paperForClass = new SmartSchool.ePaper.ElectronicPaper(comboBoxEx1.Text + "�Ҹզ��Z��",
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
                MemoryStream template = new MemoryStream(radioBtn1.Checked ? Properties.Resources.�Z�ŦҸզ��Z�� : customizeTemplateBuffer);
                Document doc = new Document();
                doc.Sections.Clear();

                double progress = 0;
                foreach (ClassRecord classRec in selectedClasses)
                {
                    #region �C�ӯZ�Ť��O�p��
                    //�ǥͪ��U�����
                    Dictionary<SmartSchool.Customization.Data.StudentRecord, Dictionary<string, string>> classExamScoreTable = new Dictionary<SmartSchool.Customization.Data.StudentRecord, Dictionary<string, string>>();
                    //��ǥͦҸզ��Z
                    accessHelper.StudentHelper.FillExamScore(SmartSchool.Customization.Data.SystemInformation.SchoolYear, SmartSchool.Customization.Data.SystemInformation.Semester, classRec.Students);
                    //��z�C�L���+�ŧO+�Ǥ���
                    List<string> groups = new List<string>();
                    #region ��z�ǥͭ׽Ҭ���
                    //�Ѥ��ƧǪ��ǥ�
                    Dictionary<SmartSchool.Customization.Data.StudentRecord, decimal> canRankList = new Dictionary<SmartSchool.Customization.Data.StudentRecord, decimal>();
                    Dictionary<SmartSchool.Customization.Data.StudentRecord, decimal> canRankList2 = new Dictionary<SmartSchool.Customization.Data.StudentRecord, decimal>();
                    Dictionary<SmartSchool.Customization.Data.StudentRecord, decimal> canRankList3 = new Dictionary<SmartSchool.Customization.Data.StudentRecord, decimal>();
                    //�O���C�Ӿǥͦ���������KEY(�p�G��Z���������N���ޡA���p�G�o�{�������N�n��ǥͱq�i�ƧǲM�椤����)
                    Dictionary<SmartSchool.Customization.Data.StudentRecord, List<string>> nonScoreKeys = new Dictionary<SmartSchool.Customization.Data.StudentRecord, List<string>>();
                    foreach (SmartSchool.Customization.Data.StudentRecord studentRec in classRec.Students)
                    {
                        //�[�Jtable
                        classExamScoreTable.Add(studentRec, new Dictionary<string, string>());
                        //�[�v�`��
                        decimal scoreCount = 0;
                        //�ѥ[�ƦW
                        bool canRank = true;
                        //�`�Ǥ���
                        int CreditCount = 0;
                        //�`��
                        decimal sum = 0;

                        foreach (StudentAttendCourseRecord attendRec in studentRec.AttendCourseList)
                        {
                            if (printSubjects.Contains(attendRec.Subject) && attendRec.ExamList.Contains(printExam))
                            {
                                //���ءB�ŧO�B�Ǥ��ư¦� "_���_�ŧO_�Ǥ���_"���r��A�o�Ӧr��b���P��دŧO�Ǥ��Ʒ|�����ߤ@��
                                string key = attendRec.Subject + "^_^" + attendRec.SubjectLevel + "^_^" + attendRec.Credit;
                                bool hasScore = false;
                                #region �ˬd�o��KEY���S�������P�ɭp���`�������άO�_�i�ƦW
                                foreach (ExamScoreInfo examScore in studentRec.ExamScoreList)
                                {
                                    if (examScore.ExamName == printExam && key == examScore.Subject + "^_^" + examScore.SubjectLevel + "^_^" + examScore.Credit)
                                    {
                                        //�O�n�C�L�����
                                        if (!groups.Contains(key))
                                            groups.Add(key);
                                        hasScore = true;
                                        if (examScore.SpecialCase == "")//�@�륿�`���Z
                                        {
                                            if (!classExamScoreTable[studentRec].ContainsKey(key))
                                                classExamScoreTable[studentRec].Add(key, examScore.ExamScore.ToString());
                                            else
                                                classExamScoreTable[studentRec][key] = examScore.ExamScore.ToString();
                                            //�[�v�`��
                                            scoreCount += examScore.ExamScore * examScore.Credit;
                                            //�Ǥ�
                                            CreditCount += examScore.Credit;
                                            //�`��
                                            sum += examScore.ExamScore;
                                        }
                                        else//�S���Z���p
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
                                //�o�{�S������
                                if (!hasScore)
                                {
                                    #region �[�J�ǥͥ��������
                                    if (!nonScoreKeys.ContainsKey(studentRec))
                                        nonScoreKeys.Add(studentRec, new List<string>());
                                    if (!nonScoreKeys[studentRec].Contains(key))
                                        nonScoreKeys[studentRec].Add(key);
                                    #endregion
                                    classExamScoreTable[studentRec].Add(key, "����J");
                                }
                            }
                        }
                        classExamScoreTable[studentRec].Add("�[�v�`��", scoreCount.ToString());
                        classExamScoreTable[studentRec].Add("�[�v����", (scoreCount / (CreditCount == 0 ? 1 : CreditCount)).ToString(".00"));
                        classExamScoreTable[studentRec].Add("�`��", sum.ToString());
                        if (canRank)
                        {
                            canRankList.Add(studentRec, decimal.Parse((scoreCount / (CreditCount == 0 ? 1 : CreditCount)).ToString(".00")));
                            canRankList2.Add(studentRec, sum);
                            canRankList3.Add(studentRec, scoreCount);
                        }
                    }
                    //�p�G�ǥͦb�n�C�L��ؤ��o�{���������ثh�q�i�ƦW�M�椤����
                    #region �p�G�ǥͦb�n�C�L��ؤ��o�{���������ثh�q�i�ƦW�M�椤����
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
                            classExamScoreTable[stuRec].Add("�[�v�����ƦW", "" + (rankScoreList.IndexOf(decimal.Parse(classExamScoreTable[stuRec]["�[�v����"])) + 1));
                        else
                            classExamScoreTable[stuRec].Add("�[�v�����ƦW", "");

                        if (canRankList2.ContainsKey(stuRec))
                            classExamScoreTable[stuRec].Add("�`���ƦW", "" + (rankScoreList2.IndexOf(decimal.Parse(classExamScoreTable[stuRec]["�`��"])) + 1));
                        else
                            classExamScoreTable[stuRec].Add("�`���ƦW", "");

                        if (canRankList3.ContainsKey(stuRec))
                            classExamScoreTable[stuRec].Add("�[�v�`���ƦW", "" + (rankScoreList3.IndexOf(decimal.Parse(classExamScoreTable[stuRec]["�[�v�`��"])) + 1));
                        else
                            classExamScoreTable[stuRec].Add("�[�v�`���ƦW", "");
                    }
                    #endregion
                    //�Ƨǭn�C�L�����
                    groups.Sort(new SmartSchool.Common.StringComparer("���", "�^��", "�ƾ�", "���z", "�ƾ�", "�ͪ�", "�a�z", "���v", "����"));

                    #region �@�ӯZ�Ų��ͤ@�ӷs��Document
                    //�@�ӾǥͲ��ͤ@�ӷs��Document
                    Document each_page = new Document(template, "", LoadFormat.Doc, "");
                    #region �إߦ��Z�Ū����Z��
                    //�X�ְ򥻸��
                    //�䥦�έp���
                    List<string> otherList = new List<string>();
                    foreach (CheckBox var in new CheckBox[] { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6 })
                    {
                        if (var.Checked)
                            otherList.Add(var.Text);
                    }
                    string[] merge_keys = new string[] { "�ǮզW��", "�Z�ŦW��", "�ҸզW��", "�Ҹզ��Z" };
                    object[] merge_values = new object[] { 
                        SmartSchool.Customization.Data.SystemInformation.SchoolChineseName ,
                        classRec.ClassName, 
                        printExam, 
                        new object[] { groups, classExamScoreTable ,otherList} };

                    each_page.MailMerge.MergeField += new Aspose.Words.Reporting.MergeFieldEventHandler(MailMerge_MergeField);
                    each_page.MailMerge.Execute(merge_keys, merge_values);
                    #endregion
                    //�X�֦�doc
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
            bkw.ProgressChanged += delegate(object sender, ProgressChangedEventArgs e) { SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage("�Z�ŦҸզ��Z�沣�ͤ�...", e.ProgressPercentage); };
            bkw.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
            {
                #region �x�s
                SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage("�Z�ŦҸզ��Z�沣�ͧ���");
                Document doc = (Document)e.Result;

                string reportName = "�Z�ŦҸզ��Z��";
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
                    sd.Title = "�t�s�s��";
                    sd.FileName = reportName + ".doc";
                    sd.Filter = "Word�ɮ� (*.doc)|*.doc|�Ҧ��ɮ� (*.*)|*.*";
                    if (sd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            doc.Save(sd.FileName, SaveFormat.AsposePdf);
                        }
                        catch
                        {
                            MsgBox.Show("���w���|�L�k�s���C", "�إ��ɮץ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (e.FieldName == "�Ҹզ��Z")
            {
                e.Text = string.Empty;
                //��z�C�L���+�ŧO+�Ǥ���
                List<string> groups = (List<string>)((object[])e.FieldValue)[0];
                //���Z�ǥͦ��Z���
                Dictionary<SmartSchool.Customization.Data.StudentRecord, Dictionary<string, string>> classExamScoreTable = (Dictionary<SmartSchool.Customization.Data.StudentRecord, Dictionary<string, string>>)((object[])e.FieldValue)[1];
                //�䥦�έp���
                List<string> otherList = (List<string>)((object[])e.FieldValue)[2];
                //�C�Ӭ�ت��`��
                Dictionary<string, decimal> groupSum = new Dictionary<string, decimal>();
                //�C�Ӭ�ئ����ƪ��H��
                Dictionary<string, int> groupCount = new Dictionary<string, int>();

                DocumentBuilder builder = new DocumentBuilder(e.Document);
                Cell currentCell;
                builder.RowFormat.AllowBreakAcrossPages = true;
                builder.MoveToField(e.Field, false);
                #region ���o�~�ؼe�רíp����e
                Cell SCell = (Cell)builder.CurrentParagraph.ParentNode;
                double Swidth = SCell.CellFormat.Width;
                double microUnit = Swidth / (groups.Count + otherList.Count + 3); //�m�W*2�B�y����C�즨�Z�U�@��
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
                #region ����Y
                builder.InsertCell().CellFormat.Width = microUnit * 2;
                builder.CellFormat.Borders.Right.LineWidth = 0.25;
                builder.Write("�m�W");
                builder.InsertCell().CellFormat.Width = microUnit;
                builder.CellFormat.Borders.Right.LineWidth = 0.25;
                builder.Write("�y��");
                foreach (string key in groups)
                {
                    #region �C�쵹�@��
                    builder.InsertCell().CellFormat.Width = microUnit;
                    builder.CellFormat.VerticalMerge = CellMerge.None;
                    builder.CellFormat.Borders.Right.LineWidth = 0.25;
                    string[] list = key.Split(new string[] { "^_^" }, StringSplitOptions.None);
                    builder.Write(list[0] + GetNumberString(list[1]));
                    #endregion
                }
                foreach (string key in otherList)
                {
                    #region �έp���
                    builder.InsertCell().CellFormat.Width = microUnit;
                    builder.CellFormat.VerticalMerge = CellMerge.First;
                    if (otherList[otherList.Count - 1] != key)
                        builder.CellFormat.Borders.Right.LineWidth = 0.25;
                    builder.Write(key);
                    #endregion
                }
                builder.EndRow();
                #endregion

                #region ��Ǥ���
                currentCell = builder.InsertCell();
                currentCell.CellFormat.Width = microUnit * 3;
                builder.CellFormat.Borders.Right.LineWidth = 0.25;
                currentCell.FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Right;
                builder.Write("�Ǥ���");
                foreach (string key in groups)
                {
                    #region �C�쵹�@��
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
                    #region �έp���

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
                //�e���u
                foreach (Cell cell in table.LastRow.Cells)
                    cell.CellFormat.Borders.Bottom.LineStyle = LineStyle.Double;

                builder.CellFormat.VerticalMerge = CellMerge.None;
                foreach (SmartSchool.Customization.Data.StudentRecord studentRec in classExamScoreTable.Keys)
                {
                    #region ��ǥ͸��
                    //�m�W
                    builder.InsertCell().CellFormat.Width = microUnit * 2;
                    builder.CellFormat.Borders.Right.LineWidth = 0.25;
                    builder.Write(studentRec.StudentName);
                    //�y��
                    builder.InsertCell().CellFormat.Width = microUnit;
                    builder.CellFormat.Borders.Right.LineWidth = 0.25;
                    builder.Write(studentRec.SeatNo);
                    foreach (string key in groups)
                    {
                        #region �U�즨�Z
                        builder.InsertCell().CellFormat.Width = microUnit;
                        if (otherList.Count > 0 || groups[groups.Count - 1] != key)
                            builder.CellFormat.Borders.Right.LineWidth = 0.25;
                        if (classExamScoreTable[studentRec].ContainsKey(key))
                        {
                            builder.Write(classExamScoreTable[studentRec][key]);
                            #region ��U�쥭��
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
                        #region �έp���
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
                //�e���u
                foreach (Cell cell in table.LastRow.Cells)
                    cell.CellFormat.Borders.Bottom.LineStyle = LineStyle.Double;

                #region �񥭧�
                currentCell = builder.InsertCell();
                currentCell.CellFormat.Width = microUnit * 3;
                currentCell.FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Right;
                builder.CellFormat.Borders.Right.LineWidth = 0.25;
                builder.Write("����");
                foreach (string key in groups)
                {
                    #region �U�쥭��
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
                #region �h�����|�䪺�u
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
                #region ����levelNumber
                case "1":
                    levelNumber = "��";
                    break;
                case "2":
                    levelNumber = "��";
                    break;
                case "3":
                    levelNumber = "��";
                    break;
                case "4":
                    levelNumber = "��";
                    break;
                case "5":
                    levelNumber = "��";
                    break;
                case "6":
                    levelNumber = "��";
                    break;
                case "7":
                    levelNumber = "��";
                    break;
                case "8":
                    levelNumber = "��";
                    break;
                case "9":
                    levelNumber = "��";
                    break;
                case "10":
                    levelNumber = "��";
                    break;
                default:
                    levelNumber = p;
                    break;
                #endregion
            }
            return levelNumber;
        }

        #endregion

        #region �C�L�d������
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            buttonX2.Expanded = false;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "�t�s�s��";
            sfd.FileName = "�Z�ŦҸզ��Z��.doc";
            sfd.Filter = "Word�ɮ� (*.doc)|*.doc|�Ҧ��ɮ� (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    fs.Write(Properties.Resources.�Z�ŦҸզ��Z��, 0, Properties.Resources.�Z�ŦҸզ��Z��.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    MsgBox.Show("���w���|�L�k�s���C", "�t�s�ɮץ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            buttonX2.Expanded = false;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "��ܦۭq���Z�ŦҸզ��Z��d��";
            ofd.Filter = "Word�ɮ� (*.doc)|*.doc";
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
                        #region �s�J�˪O
                        customizeTemplateBuffer = tempBuffer;
                        radioBtn2.Enabled = linkLabel2.Enabled = true;
                        XmlElement config = SmartSchool.Customization.Data.SystemInformation.Preference["�Z�ŦҸզ��Z��"];
                        //XmlElement config = SmartSchool.Customization.Data.SystemInformation.Configuration["�Z�ŦҸզ��Z��"];
                        //if ( config == null )
                        //    config = SmartSchool.Customization.Data.SystemInformation.Preference["�Z�ŦҸզ��Z��"];
                        if (config == null)
                            config = new XmlDocument().CreateElement("�Z�ŦҸզ��Z��");
                        XmlElement customize1 = (XmlElement)config.SelectSingleNode("�ۭq�˪O");
                        if (customize1 == null)
                            customize1 = (XmlElement)config.AppendChild(config.OwnerDocument.CreateElement("�ۭq�˪O"));
                        customize1.InnerText = Convert.ToBase64String(customizeTemplateBuffer);

                        SmartSchool.Customization.Data.SystemInformation.Preference["�Z�ŦҸզ��Z��"] = config;
                        //SmartSchool.Customization.Data.SystemInformation.Configuration["�Z�ŦҸզ��Z��"] = config;
                        #endregion

                        MsgBox.Show("�W�Ǧ��\�C");
                    }
                    else
                        MsgBox.Show("�W���ɮ׮榡����");
                }
                catch
                {
                    MsgBox.Show("���w���|�L�k�s���C", "�}���ɮץ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            buttonX2.Expanded = true;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            buttonX2.Expanded = false;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "�t�s�s��";
            sfd.FileName = "�ۭq�Z�ŦҸզ��Z��d��.doc";
            sfd.Filter = "Word�ɮ� (*.doc)|*.doc|�Ҧ��ɮ� (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    if (Aspose.Words.Document.DetectFileFormat(new MemoryStream(customizeTemplateBuffer)) == Aspose.Words.LoadFormat.Doc)
                        fs.Write(customizeTemplateBuffer, 0, customizeTemplateBuffer.Length);
                    else
                        fs.Write(Properties.Resources.�Z�ŦҸզ��Z��, 0, Properties.Resources.�Z�ŦҸզ��Z��.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    MsgBox.Show("���w���|�L�k�s���C", "�t�s�ɮץ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void radioBtn_CheckedChanged(object sender, EventArgs e)
        {
            #region �s�J�˪O
            XmlElement config = SmartSchool.Customization.Data.SystemInformation.Preference["�Z�ŦҸզ��Z��"];
            //XmlElement config = SmartSchool.Customization.Data.SystemInformation.Configuration["�Z�ŦҸզ��Z��"];
            //if ( config == null )
            //    config = SmartSchool.Customization.Data.SystemInformation.Preference["�Z�ŦҸզ��Z��"];
            if (config == null)
                config = new XmlDocument().CreateElement("�Z�ŦҸզ��Z��");
            config.SetAttribute("�C�L�˪O", radioBtn1.Checked ? "�w�]" : "�ۭq");

            SmartSchool.Customization.Data.SystemInformation.Preference["�Z�ŦҸզ��Z��"] = config;
            //SmartSchool.Customization.Data.SystemInformation.Configuration["�Z�ŦҸզ��Z��"] = config;
            #endregion
        }
        #endregion
        //�]�w�έp���
        private void summaryChanged(object sender, EventArgs e)
        {
            if (summaryFieldsChanging) return;
            #region �s�J�˪O
            XmlElement config = SmartSchool.Customization.Data.SystemInformation.Configuration["�Z�ŦҸզ��Z��"];
            if (config == null)
                config = SmartSchool.Customization.Data.SystemInformation.Preference["�Z�ŦҸզ��Z��"];
            if (config == null)
                config = new XmlDocument().CreateElement("�Z�ŦҸզ��Z��");

            config.SetAttribute("�`��", "" + checkBox1.Checked);
            config.SetAttribute("�`���ƦW", "" + checkBox2.Checked);
            config.SetAttribute("�[�v�`��", "" + checkBox3.Checked);
            config.SetAttribute("�[�v����", "" + checkBox4.Checked);
            config.SetAttribute("�[�v�����ƦW", "" + checkBox5.Checked);
            config.SetAttribute("�[�v�`���ƦW", "" + checkBox6.Checked);
            config.SetAttribute("�q�l����ǥ�", "" + checkBoxStudent.Checked);
            config.SetAttribute("�q�l����Z��", "" + checkBoxClass.Checked);

            SmartSchool.Customization.Data.SystemInformation.Preference["�Z�ŦҸզ��Z��"] = config;
            //SmartSchool.Customization.Data.SystemInformation.Configuration["�Z�ŦҸզ��Z��"] = config;
            #endregion
        }
    }
}

