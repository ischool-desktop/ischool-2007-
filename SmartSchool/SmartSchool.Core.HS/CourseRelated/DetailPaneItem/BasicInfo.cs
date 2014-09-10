using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using System.Threading;
using SmartSchool.Feature.Course;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.Feature.Teacher;
using SmartSchool.Feature.Class;
using SmartSchool.ClassRelated;
using DevComponents.DotNetBar.Controls;
using SmartSchool.Feature.Basic;
using DevComponents.DotNetBar;
using SmartSchool.TeacherRelated;
using ClassEntity = SmartSchool.ClassRelated.Class;
using SmartSchool.Feature.ExamTemplate;
using SmartSchool.ExceptionHandler;
using SmartSchool.ApplicationLog;
using SmartSchool.AccessControl;
using SmartSchool.Broadcaster;

namespace SmartSchool.CourseRelated.DetailPaneItem
{
    [FeatureCode("Content0200")]
    internal partial class BasicInfo : PalmerwormItem
    {
        private static List<TeacherInfo> _teachers;
        private static List<ClassInfo> _classes;
        private static List<ExamTemplateInfo> _template;
        private List<string> _entries;
        private MultiTeacherController _multi_teacher;

        private static bool _init_required;
        private bool _initialing = false;
        private bool _saving = false;

        private EnhancedErrorProvider _errors;
        private DSXmlHelper _current_response;

        #region Log 機器
        CourseBaseLogMachine machine = new CourseBaseLogMachine();
        #endregion

        public BasicInfo()
        {
            InitializeComponent();

            Title = "基本資料";

            _teachers = new List<TeacherInfo>();
            _classes = new List<ClassInfo>();
            _errors = new EnhancedErrorProvider();
            _multi_teacher = new MultiTeacherController(btnTeachers, cboMultiTeacher, _errors);
            _multi_teacher.ValueChanged += new EventHandler(Multi_Teacher_ValueChanged);
            _init_required = true;

            Teacher.Instance.TeacherDataChanged += new EventHandler<TeacherDataChangedEventArgs>(Instance_TeacherDataChanged);
            Teacher.Instance.TeacherInserted += new EventHandler(Instance_TeacherInserted);
            Teacher.Instance.TeacherDeleted += new EventHandler<TeacherDeletedEventArgs>(Instance_TeacherDeleted);
            ClassEntity.Instance.ClassInserted += new EventHandler<InsertClassEventArgs>(Instance_ClassInserted);
            ClassEntity.Instance.ClassUpdated += new EventHandler<UpdateClassEventArgs>(Instance_ClassUpdated);
            ClassEntity.Instance.ClassDeleted += new EventHandler<DeleteClassEventArgs>(Instance_ClassDeleted);
            CourseEntity.Instance.ForeignTableChanged += new EventHandler(Instance_ForeignTableChanged);
            CourseEntity.Instance.CourseChanged += new EventHandler<CourseChangeEventArgs>(Instance_CourseChanged);
            //CourseEntity.Instance.CourseInserted += new EventHandler(Instance_CourseInserted);
            SmartSchool.Broadcaster.Events.Items["課程/新增"].Handler += new EventHandler<SmartSchool.Broadcaster.EventArguments>(Instance_CourseInserted);

            Disposed += new EventHandler(BasicInfo_Disposed);
        }

        private void BasicInfo_Disposed(object sender, EventArgs e)
        {
            Teacher.Instance.TeacherDataChanged -= new EventHandler<TeacherDataChangedEventArgs>(Instance_TeacherDataChanged);
            Teacher.Instance.TeacherInserted -= new EventHandler(Instance_TeacherInserted);
            Teacher.Instance.TeacherDeleted -= new EventHandler<TeacherDeletedEventArgs>(Instance_TeacherDeleted);
            ClassEntity.Instance.ClassInserted -= new EventHandler<InsertClassEventArgs>(Instance_ClassInserted);
            ClassEntity.Instance.ClassUpdated -= new EventHandler<UpdateClassEventArgs>(Instance_ClassUpdated);
            ClassEntity.Instance.ClassDeleted -= new EventHandler<DeleteClassEventArgs>(Instance_ClassDeleted);
            CourseEntity.Instance.ForeignTableChanged -= new EventHandler(Instance_ForeignTableChanged);
            CourseEntity.Instance.CourseChanged -= new EventHandler<CourseChangeEventArgs>(Instance_CourseChanged);
            //CourseEntity.Instance.CourseInserted -= new EventHandler(Instance_CourseInserted);
            SmartSchool.Broadcaster.Events.Items["課程/新增"].Handler -= new EventHandler<SmartSchool.Broadcaster.EventArguments>(Instance_CourseInserted);
        }

        private void Multi_Teacher_ValueChanged(object sender, EventArgs e)
        {
            OnValueChanged("Teachers", _multi_teacher.CurrentValue);
        }

        private void Instance_CourseChanged(object sender, CourseChangeEventArgs e)
        {
            foreach (int each in e.CoursesIdCollection)
            {
                if ((each + "") == RunningID)
                    RefreshCourseInfo();
            }
        }

        private void Instance_CourseInserted(object sender, EventArguments e)
        {
            _init_required = true;
        }

        private void Instance_ForeignTableChanged(object sender, EventArgs e)
        {
            RefreshCourseInfo();
        }

        private void Instance_TeacherDataChanged(object sender, TeacherDataChangedEventArgs e)
        {
            RefreshCourseInfo();
        }

        private void Instance_TeacherInserted(object sender, EventArgs e)
        {
            _init_required = true;
        }

        private void Instance_TeacherDeleted(object sender, TeacherDeletedEventArgs e)
        {
            _init_required = true;
        }

        private void Instance_ClassUpdated(object sender, UpdateClassEventArgs e)
        {
            RefreshCourseInfo();
        }

        private void Instance_ClassInserted(object sender, InsertClassEventArgs e)
        {
            _init_required = true;
        }

        private void Instance_ClassDeleted(object sender, DeleteClassEventArgs e)
        {
            _init_required = true;
        }

        protected override object OnBackgroundWorkerWorking()
        {
            try
            {
                int t1 = Environment.TickCount;
                if (_init_required) //需要 Initial 才執行下列程式碼。
                {
                    DSResponse trsp = QueryTeacher.GetTeacherList();
                    _teachers = new List<TeacherInfo>();
                    _teachers.Add(TeacherInfo.Unknow);
                    foreach (XmlElement each in trsp.GetContent().GetElements("Teacher"))
                        _teachers.Add(new TeacherInfo(each));

                    DSResponse crsp = QueryClass.GetClassList();
                    _classes = new List<ClassInfo>();
                    _classes.Add(ClassInfo.Unknow);
                    foreach (XmlElement each in crsp.GetContent().GetElements("Class"))
                        _classes.Add(new ClassInfo(each));

                    XmlElement ersp = QueryTemplate.GetAbstractList();
                    _template = new List<ExamTemplateInfo>();
                    _template.Add(ExamTemplateInfo.Unknow);
                    foreach (XmlElement each in ersp.SelectNodes("ExamTemplate"))
                        _template.Add(new ExamTemplateInfo(each));

                    _entries = new List<string>();
                    foreach (XmlElement each in Config.GetScoreEntryList().GetContent().GetElements("Entry"))
                        _entries.Add(each.InnerText);

                    _init_required = false;
                }

                DSResponse response = QueryCourse.GetCourseDetail(int.Parse(RunningID));
                Console.WriteLine("Course Response Time：{0}", Environment.TickCount - t1);
                return response;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        protected override void OnBackgroundWorkerCompleted(object result)
        {
            try
            {
                DSXmlHelper content = (result as DSResponse).GetContent();
                _current_response = content;

                _initialing = true;
                _errors.Clear();
                ReloadComboBox();

                txtCourseName.Text = content.GetText("Course/CourseName");
                txtSubject.Text = content.GetText("Course/Subject");
                cboSubjectLevel.Text = content.GetText("Course/SubjectLevel");
                txtCredit.Text = content.GetText("Course/Credit");
                cboSchoolYear.Text = content.GetText("Course/SchoolYear");
                cboSemester.Text = content.GetText("Course/Semester");
                //cboTeacher.SelectedItem = content.GetText("Course/MajorTeacherID"); //ComboBox 奧義
                cboClass.SelectedItem = content.GetText("Course/RefClassID"); //ComboBox 奧義
                cboExamTemplate.SelectedItem = content.GetText("Course/RefExamTemplateID"); //ComboBox 奧義

                if (content.GetText("Course/NotIncludedInCredit") == "否")
                    rdoCreditTrue.Checked = true;
                else
                    rdoCreditFalse.Checked = true;

                if (content.GetText("Course/NotIncludedInCalc") == "否")
                    rdoCalcTrue.Checked = true;
                else
                    rdoCalcFalse.Checked = true;

                cboEntry.Text = content.GetText("Course/ScoreType");
                _multi_teacher.BindData(RunningID, content.GetElement("Course/Teachers"));
                switch (content.GetText("Course/IsRequired"))
                {
                    case "選":
                        cboRequired.SelectedIndex = 2;
                        break;
                    case "必":
                        cboRequired.SelectedIndex = 1;
                        break;
                    default:
                        cboRequired.Text = "";
                        break;
                }

                switch (content.GetText("Course/RequiredBy"))
                {
                    case "部訂":
                        cboRequiredBy.SelectedIndex = 2;
                        break;
                    case "校訂":
                        cboRequiredBy.SelectedIndex = 1;
                        break;
                    default:
                        cboRequiredBy.Text = "";
                        break;
                }

                WatchValue("Teachers", _multi_teacher.CurrentValue);
                WatchValue("CourseName", txtCourseName.Text);
                WatchValue("Subject", txtSubject.Text);
                WatchValue("SubjectLevel", cboSubjectLevel.Text);
                WatchValue("Credit", txtCredit.Text);
                WatchValue("SchoolYear", cboSchoolYear.Text);
                WatchValue("Semester", cboSemester.Text);
                //WatchValue("RefTeacherID", cboTeacher.SelectedItem);
                WatchValue("RefClassID", cboClass.SelectedItem);
                WatchValue("RefExamTemplateID", cboExamTemplate.SelectedItem);
                WatchValue("NotIncludedInCredit", rdoCreditTrue.Checked.ToString());
                WatchValue("NotIncludedInCalc", rdoCalcTrue.Checked.ToString());
                WatchValue("ScoreType", cboEntry.Text);
                WatchValue("RequiredBy", cboRequiredBy.Text);
                WatchValue("Required", cboRequired.Text);

                _initialing = false;

                SaveButtonVisible = false;

                #region Log 記錄修改前資料

                machine.AddBefore(labelX1.Text.Replace("　", "").Replace(" ", ""), txtCourseName.Text);
                machine.AddBefore(labelX2.Text.Replace("　", "").Replace(" ", ""), txtSubject.Text);
                machine.AddBefore(labelX3.Text.Replace("　", "").Replace(" ", ""), cboSubjectLevel.Text);
                machine.AddBefore(labelX4.Text.Replace("　", "").Replace(" ", ""), cboClass.Text);
                machine.AddBefore(labelX5.Text.Replace("　", "").Replace(" ", ""), cboSchoolYear.Text);
                machine.AddBefore(labelX6.Text.Replace("　", "").Replace(" ", ""), cboSemester.Text);
                machine.AddBefore(labelX7.Text.Replace("　", "").Replace(" ", ""), txtCredit.Text);
                machine.AddBefore(labelX8.Text.Replace("　", "").Replace(" ", ""), cboRequired.Text);
                machine.AddBefore(labelX13.Text.Replace("　", "").Replace(" ", ""), cboRequiredBy.Text);
                machine.AddBefore(labelX9.Text.Replace("　", "").Replace(" ", ""), rdoCreditTrue.Checked ? "計入" : "不計入");
                machine.AddBefore(labelX10.Text.Replace("　", "").Replace(" ", ""), rdoCalcTrue.Checked ? "評分" : "不評分");
                machine.AddBefore(labelX11.Text.Replace("　", "").Replace(" ", ""), cboEntry.Text);
                machine.AddBefore(labelX12.Text.Replace("　", "").Replace(" ", ""), cboExamTemplate.Text);

                SmartSchool.CourseRelated.DetailPaneItem.BasicInfo.MultiTeacherController.TeacherItem teacher1 = (SmartSchool.CourseRelated.DetailPaneItem.BasicInfo.MultiTeacherController.TeacherItem)_multi_teacher.Teacher1Button;
                SmartSchool.CourseRelated.DetailPaneItem.BasicInfo.MultiTeacherController.TeacherItem teacher2 = (SmartSchool.CourseRelated.DetailPaneItem.BasicInfo.MultiTeacherController.TeacherItem)_multi_teacher.Teacher2Button;
                SmartSchool.CourseRelated.DetailPaneItem.BasicInfo.MultiTeacherController.TeacherItem teacher3 = (SmartSchool.CourseRelated.DetailPaneItem.BasicInfo.MultiTeacherController.TeacherItem)_multi_teacher.Teacher3Button;

                machine.AddBefore(teacher1.Text, (teacher1.Teacher != null) ? teacher1.Teacher.TeacherName : "");
                machine.AddBefore(teacher2.Text, (teacher2.Teacher != null) ? teacher2.Teacher.TeacherName : "");
                machine.AddBefore(teacher3.Text, (teacher3.Teacher != null) ? teacher3.Teacher.TeacherName : "");

                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Show("發生錯誤：" + ex.Message);
                Enabled = false;
            }
        }

        private void ReloadComboBox()
        {
            cboSchoolYear.SelectedItem = null;
            cboSchoolYear.Items.Clear();
            List<int> years = CourseEntity.Instance.Semesters.GroupSchoolYear();
            cboSchoolYear.Items.Add(string.Empty);
            foreach (int each in years)
                cboSchoolYear.Items.Add(each);

            //cboTeacher.Items.Clear();
            //cboTeacher.Items.AddRange(_teachers.ToArray());
            //cboTeacher.DisplayMember = "TeacherName";
            //cboTeacher.ValueMember = "TeacherID";

            cboMultiTeacher.SelectedItem = null;
            cboMultiTeacher.Items.Clear();
            cboMultiTeacher.Items.AddRange(_teachers.ToArray());
            cboMultiTeacher.DisplayMember = "TeacherName";
            cboMultiTeacher.ValueMember = "TeacherID";

            cboClass.SelectedItem = null;
            cboClass.Items.Clear();
            cboClass.Items.AddRange(_classes.ToArray());
            cboClass.DisplayMember = "ClassName";
            cboClass.ValueMember = "ClassID";

            cboExamTemplate.SelectedItem = null;
            cboExamTemplate.Items.Clear();
            cboExamTemplate.Items.AddRange(_template.ToArray());
            cboExamTemplate.DisplayMember = "TemplateName";
            cboExamTemplate.ValueMember = "TemplateID";

            cboEntry.SelectedItem = null;
            cboEntry.Items.Clear();
            cboEntry.Items.Add("");
            cboEntry.Items.AddRange(_entries.ToArray());
        }

        private void WatchValue(string name, object value)
        {
            if (value is String) //如果是「字串」就一般方法處理。
                _valueManager.AddValue(name, value.ToString());
            else
            { //非字串用「物件」方式處理。
                if (value != null)
                    _valueManager.AddValue(name, value.GetHashCode().ToString());
                else
                    _valueManager.AddValue(name, "");
            }
        }

        private void ChangeValue(string name, object value)
        {
            if (value is String) //如果是「字串」就一般方法處理。
                OnValueChanged(name, value.ToString());
            else
            { //非字串用「物件」方式處理。
                if (value != null)
                    OnValueChanged(name, value.GetHashCode().ToString());
                else
                    OnValueChanged(name, "");
            }
        }

        private string GetRefClassID()
        {
            if (cboClass.SelectedItem == null)
                return "";
            else
            {
                if (cboClass.SelectedItem.Equals(ClassInfo.Unknow))
                    return "";
                else
                    return (cboClass.SelectedItem as ClassInfo).ClassID;
            }
        }

        //private string GetRefTeacherID()
        //{
        //    if (cboTeacher.SelectedItem == null)
        //        return "";
        //    else
        //    {
        //        if (cboTeacher.SelectedItem.Equals(TeacherInfo.Unknow))
        //            return "";
        //        else
        //            return (cboTeacher.SelectedItem as TeacherInfo).TeacherID;
        //    }
        //}

        private string GetRefExamTemplateID()
        {
            if (cboExamTemplate.SelectedItem == null)
                return "";
            else
            {
                if (cboExamTemplate.SelectedItem.Equals(ExamTemplateInfo.Unknow))
                    return "";
                else
                    return (cboExamTemplate.SelectedItem as ExamTemplateInfo).TemplateID;
            }
        }

        public override void Save()
        {
            try
            {
                DSXmlHelper req = new DSXmlHelper("UpdateRequest");
                Dictionary<string, string> items = _valueManager.GetDirtyItems();

                if (items.Count <= 0) //沒有任何更動。
                    return;

                if (_errors.HasError)
                {
                    MsgBox.Show("輸入資料未通過驗證，請修正後再儲存");
                    return;
                }

                #region Log 記錄修改後的資料

                machine.AddAfter(labelX1.Text.Replace("　", "").Replace(" ", ""), txtCourseName.Text);
                machine.AddAfter(labelX2.Text.Replace("　", "").Replace(" ", ""), txtSubject.Text);
                machine.AddAfter(labelX3.Text.Replace("　", "").Replace(" ", ""), cboSubjectLevel.Text);
                machine.AddAfter(labelX4.Text.Replace("　", "").Replace(" ", ""), cboClass.Text);
                machine.AddAfter(labelX5.Text.Replace("　", "").Replace(" ", ""), cboSchoolYear.Text);
                machine.AddAfter(labelX6.Text.Replace("　", "").Replace(" ", ""), cboSemester.Text);
                machine.AddAfter(labelX7.Text.Replace("　", "").Replace(" ", ""), txtCredit.Text);
                machine.AddAfter(labelX8.Text.Replace("　", "").Replace(" ", ""), cboRequired.Text);
                machine.AddAfter(labelX13.Text.Replace("　", "").Replace(" ", ""), cboRequiredBy.Text);
                machine.AddAfter(labelX9.Text.Replace("　", "").Replace(" ", ""), rdoCreditTrue.Checked ? "計入" : "不計入");
                machine.AddAfter(labelX10.Text.Replace("　", "").Replace(" ", ""), rdoCalcTrue.Checked ? "評分" : "不評分");
                machine.AddAfter(labelX11.Text.Replace("　", "").Replace(" ", ""), cboEntry.Text);
                machine.AddAfter(labelX12.Text.Replace("　", "").Replace(" ", ""), cboExamTemplate.Text);

                SmartSchool.CourseRelated.DetailPaneItem.BasicInfo.MultiTeacherController.TeacherItem teacher1 = (SmartSchool.CourseRelated.DetailPaneItem.BasicInfo.MultiTeacherController.TeacherItem)_multi_teacher.Teacher1Button;
                SmartSchool.CourseRelated.DetailPaneItem.BasicInfo.MultiTeacherController.TeacherItem teacher2 = (SmartSchool.CourseRelated.DetailPaneItem.BasicInfo.MultiTeacherController.TeacherItem)_multi_teacher.Teacher2Button;
                SmartSchool.CourseRelated.DetailPaneItem.BasicInfo.MultiTeacherController.TeacherItem teacher3 = (SmartSchool.CourseRelated.DetailPaneItem.BasicInfo.MultiTeacherController.TeacherItem)_multi_teacher.Teacher3Button;

                machine.AddAfter(teacher1.Text, (teacher1.Teacher != null) ? teacher1.Teacher.TeacherName : "");
                machine.AddAfter(teacher2.Text, (teacher2.Teacher != null) ? teacher2.Teacher.TeacherName : "");
                machine.AddAfter(teacher3.Text, (teacher3.Teacher != null) ? teacher3.Teacher.TeacherName : "");

                #endregion

                //先儲存教師資料
                _multi_teacher.Save();

                bool _update_required = false;

                req.AddElement("Course");
                req.AddElement("Course", "Field");

                if (items.ContainsKey("CourseName"))
                {
                    req.AddElement("Course/Field", "CourseName", txtCourseName.Text);
                    _update_required = true;
                }

                if (items.ContainsKey("Subject"))
                {
                    req.AddElement("Course/Field", "Subject", txtSubject.Text);
                    _update_required = true;
                }

                if (items.ContainsKey("SubjectLevel"))
                {
                    req.AddElement("Course/Field", "SubjectLevel", cboSubjectLevel.Text);
                    _update_required = true;
                }

                if (items.ContainsKey("RefClassID"))
                {
                    req.AddElement("Course/Field", "RefClassID", GetRefClassID());
                    _update_required = true;
                }

                if (items.ContainsKey("SchoolYear"))
                {
                    req.AddElement("Course/Field", "SchoolYear", cboSchoolYear.Text);
                    _update_required = true;
                }

                if (items.ContainsKey("Semester"))
                {
                    req.AddElement("Course/Field", "Semester", cboSemester.Text);
                    _update_required = true;
                }

                if (items.ContainsKey("Credit"))
                {
                    req.AddElement("Course/Field", "Credit", txtCredit.Text);
                    _update_required = true;
                }

                if (items.ContainsKey("RequiredBy"))
                {
                    req.AddElement("Course/Field", "RequiredBy", cboRequiredBy.Text);
                    _update_required = true;
                }

                if (items.ContainsKey("Required"))
                {
                    req.AddElement("Course/Field", "IsRequired", cboRequired.Text.Replace("修", ""));
                    _update_required = true;
                }

                //if (items.ContainsKey("RefTeacherID"))
                //    req.AddElement("Course/Field", "RefTeacherID", GetRefTeacherID());

                if (items.ContainsKey("NotIncludedInCredit"))
                {
                    req.AddElement("Course/Field", "NotIncludedInCredit", rdoCreditTrue.Checked ? "否" : "是");
                    _update_required = true;
                }

                if (items.ContainsKey("NotIncludedInCalc"))
                {
                    req.AddElement("Course/Field", "NotIncludedInCalc", rdoCalcTrue.Checked ? "否" : "是");
                    _update_required = true;
                }

                if (items.ContainsKey("ScoreType"))
                {
                    req.AddElement("Course/Field", "ScoreType", cboEntry.Text.Trim());
                    _update_required = true;
                }

                if (items.ContainsKey("RefExamTemplateID"))
                {
                    req.AddElement("Course/Field", "RefExamTemplateID", GetRefExamTemplateID());
                    _update_required = true;
                }

                req.AddElement("Course", "Condition");
                {
                    req.AddElement("Course/Condition", "ID", RunningID.ToString());
                }

                if (_update_required)
                {
                    EditCourse.UpdateCourse(new DSRequest(req));

                    #region Log

                    StringBuilder desc = new StringBuilder("");
                    desc.AppendLine("課程名稱：" + CourseEntity.Instance.Items[RunningID].CourseName + " ");
                    desc.AppendLine(machine.GetDescription());

                    CurrentUser.Instance.AppLog.Write(EntityType.Course, EntityAction.Update, RunningID, desc.ToString(), "課程基本資料", "");

                    #endregion

                }

                _saving = true;
                CourseEntity.Instance.InvokeAfterCourseChange(int.Parse(RunningID));
                _saving = false;

                SaveButtonVisible = false;

                LoadContent(RunningID);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
        }

        #region Field Events
        private void txtCourseName_TextChanged(object sender, EventArgs e)
        {
            if (!_initialing)
                OnValueChanged("CourseName", txtCourseName.Text);
        }

        private void txtSubject_TextChanged(object sender, EventArgs e)
        {
            if (!_initialing)
                OnValueChanged("Subject", txtSubject.Text);
        }

        private void cboSubjectLevel_TextChanged(object sender, EventArgs e)
        {
            if (!_initialing)
                OnValueChanged("SubjectLevel", cboSubjectLevel.Text);
        }

        private void txtCredit_TextChanged(object sender, EventArgs e)
        {
            if (!_initialing)
                OnValueChanged("Credit", txtCredit.Text);
        }

        private void cboClass_TextChanged(object sender, EventArgs e)
        {
            if (!_initialing)
                ChangeValue("RefClassID", cboClass.SelectedItem);
        }

        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_initialing)
                ChangeValue("RefClassID", cboClass.SelectedItem);
        }

        private void cboSchoolYear_TextChanged(object sender, EventArgs e)
        {
            if (!_initialing)
                ChangeValue("SchoolYear", cboSchoolYear.Text);
        }

        private void cboSemester_TextChanged(object sender, EventArgs e)
        {
            if (!_initialing)
                ChangeValue("Semester", cboSemester.Text);
        }

        //private void cboTeacher_TextChanged(object sender, EventArgs e)
        //{
        //    if (!_initialing)
        //    {
        //        ChangeValue("RefTeacherID", cboTeacher.SelectedItem);
        //    }
        //}

        //private void cboTeacher_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!_initialing)
        //        ChangeValue("RefTeacherID", cboTeacher.SelectedItem);
        //}

        private void rdoCreditTrue_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialing)
                ChangeValue("NotIncludedInCredit", rdoCreditTrue.Checked.ToString());
        }

        private void rdoCalcTrue_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialing)
                ChangeValue("NotIncludedInCalc", rdoCalcTrue.Checked.ToString());
        }

        private void cboEntry_TextChanged(object sender, EventArgs e)
        {
            if (!_initialing)
                ChangeValue("ScoreType", cboEntry.Text);
        }

        private void SyncSelectedItem(ComboBoxEx combo)
        {
            _errors.SetError(combo, "");
            int index = combo.FindString(combo.Text);
            if (index >= 0)
                combo.SelectedIndex = index;
            else
            {
                if ((combo.Tag != null) && (combo.Tag.ToString() == "ForceValidate"))
                    _errors.SetError(combo, "你輸入的文字不在清單中。");
            }
        }

        private void cboExamTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_initialing)
                ChangeValue("RefExamTemplateID", cboExamTemplate.SelectedItem);
        }

        private void cboExamTemplate_TextChanged(object sender, EventArgs e)
        {
            if (!_initialing)
            {
                ChangeValue("RefExamTemplateID", cboExamTemplate.SelectedItem);
            }
        }

        private void cboRequiredBy_TextChanged(object sender, EventArgs e)
        {
            if (!_initialing)
                ChangeValue("RequiredBy", cboRequiredBy.Text);
        }

        private void cboRequired_TextChanged(object sender, EventArgs e)
        {
            if (!_initialing)
                ChangeValue("Required", cboRequired.Text);
        }

        private void ComboBoxItem_Validating(object sender, CancelEventArgs e)
        {
            SyncSelectedItem(sender as ComboBoxEx);
        }
        #endregion

        #region MultiTeacherController
        private class MultiTeacherController
        {
            private string _courseID;
            private ButtonX _main_button;
            private ComboBoxEx _combobox;
            private EnhancedErrorProvider _errors;
            private TeacherItem _current_teacher;

            public MultiTeacherController(ButtonX button, ComboBoxEx combobx, EnhancedErrorProvider errors)
            {
                _main_button = button;
                _combobox = combobx;
                _errors = errors;

                _main_button.SubItems.Clear();
                //自定 ButtonItem 類別，讓其可以儲存 Teacher Object 與 Sequence 資訊。
                _main_button.SubItems.Add(new TeacherItem("Teacher1", "教師一(評分)", 1));
                _main_button.SubItems.Add(new TeacherItem("Teacher2", "教師二", 2));
                _main_button.SubItems.Add(new TeacherItem("Teacher3", "教師三", 3));

                Teacher1Button.Click += new EventHandler(TeacherItem_Click);
                Teacher2Button.Click += new EventHandler(TeacherItem_Click);
                Teacher3Button.Click += new EventHandler(TeacherItem_Click);

                _combobox.TextChanged += new EventHandler(Combobox_TextChanged);
                _combobox.Validating += new CancelEventHandler(Combobox_Validating);
                _combobox.SelectedIndexChanged += new EventHandler(Combobox_SelectedIndexChanged);
            }

            private void Combobox_SelectedIndexChanged(object sender, EventArgs e)
            {
                ChangeCurrentSequenceTeacher();
                RaiseValueChanged();
            }

            private void Combobox_Validating(object sender, CancelEventArgs e)
            {
                SyncSelectedItem();
                ChangeCurrentSequenceTeacher();
                RaiseValueChanged();
            }

            private void Combobox_TextChanged(object sender, EventArgs e)
            {
                ChangeCurrentSequenceTeacher();
                RaiseValueChanged();
            }

            private void ChangeCurrentSequenceTeacher()
            {
                try
                {
                    TeacherInfo originTeacher = _current_teacher.Teacher;
                    TeacherInfo teacher = _combobox.SelectedItem as TeacherInfo;

                    if (teacher == TeacherInfo.Unknow)
                        _current_teacher.Teacher = null;
                    else
                        _current_teacher.Teacher = teacher;

                    /*檢查教師是否指定於同一課程兩次。*/
                    Dictionary<string, TeacherItem> dup = new Dictionary<string, TeacherItem>();
                    foreach (TeacherItem each in EachTeacherItem())
                    {
                        if (each.Teacher != null && !each.Teacher.Equals(TeacherInfo.Unknow))
                        {
                            if (!dup.ContainsKey(each.Teacher.TeacherID))
                                dup.Add(each.Teacher.TeacherID, each);
                            else
                            {
                                MsgBox.Show("相同教師不可以指定於此課程兩次。", Application.ProductName);
                                _current_teacher.Teacher = originTeacher;
                                SwitchToTeacher(_current_teacher);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    CurrentUser user = CurrentUser.Instance;
                    BugReporter.ReportException(user.SystemName, user.SystemVersion, ex, false);

                    MsgBox.Show(ex.Message);
                }
            }

            private List<TeacherItem> EachTeacherItem()
            {
                List<TeacherItem> teachers = new List<TeacherItem>();
                teachers.Add(Teacher1Button);
                teachers.Add(Teacher2Button);
                teachers.Add(Teacher3Button);
                return teachers;
            }

            private void SwitchToTeacher(TeacherItem sender)
            {
                _current_teacher = sender as TeacherItem;
                _main_button.Text = _current_teacher.Text;

                _combobox.SelectedItem = _current_teacher.Teacher;
                if (_combobox.SelectedItem == null)
                    _combobox.Text = string.Empty;

            }

            private void TeacherItem_Click(object sender, EventArgs e)
            {
                try
                {
                    SwitchToTeacher(sender as TeacherItem);
                }
                catch (Exception ex)
                {
                    CurrentUser user = CurrentUser.Instance;
                    BugReporter.ReportException(user.SystemName, user.SystemVersion, ex, false);

                    MsgBox.Show(ex.Message);
                }
            }

            public TeacherItem Teacher1Button
            {
                get { return _main_button.SubItems["Teacher1"] as TeacherItem; }
            }

            public TeacherItem Teacher2Button
            {
                get { return _main_button.SubItems["Teacher2"] as TeacherItem; }
            }

            public TeacherItem Teacher3Button
            {
                get { return _main_button.SubItems["Teacher3"] as TeacherItem; }
            }

            public void BindData(string courseID, XmlElement teachers)
            {
                Teacher1Button.Teacher = null;
                Teacher2Button.Teacher = null;
                Teacher3Button.Teacher = null;

                _courseID = courseID;
                foreach (XmlElement each in teachers.SelectNodes("Teacher"))
                {
                    string teacherName = each.GetAttribute("TeacherName");
                    string teacherId = each.GetAttribute("TeacherID");
                    int sequence = int.Parse(each.GetAttribute("Sequence"));

                    TeacherInfo objTeacher = new TeacherInfo(teacherId, teacherName);
                    if (sequence == 1)
                        Teacher1Button.Teacher = objTeacher;
                    else if (sequence == 2)
                        Teacher2Button.Teacher = objTeacher;
                    else
                        Teacher3Button.Teacher = objTeacher;
                }

                //如果拿掉這一行，要記得處理前筆記錄的教師選擇狀態問題。
                SwitchToTeacher(Teacher1Button);
            }

            public string CurrentValue
            {
                get
                {
                    try
                    {
                        string tt = Teacher1Button.Sequence + (Teacher1Button.Teacher == null ? "-1" : Teacher1Button.Teacher.TeacherID);
                        tt += Teacher2Button.Sequence + (Teacher2Button.Teacher == null ? "-1" : Teacher2Button.Teacher.TeacherID);
                        tt += Teacher3Button.Sequence + (Teacher3Button.Teacher == null ? "-1" : Teacher3Button.Teacher.TeacherID);
                        return tt;
                    }
                    catch (Exception ex)
                    {
                        CurrentUser user = CurrentUser.Instance;
                        BugReporter.ReportException(user.SystemName, user.SystemVersion, ex, false);

                        throw ex;
                    }
                }
            }

            public void Save()
            {
                DSXmlHelper helper = new DSXmlHelper("Request");
                bool insert_required = false;

                foreach (TeacherItem each in EachTeacherItem())
                {
                    if (each.Teacher != null && !each.Teacher.Equals(TeacherInfo.Unknow))
                    {
                        insert_required = true;
                        helper.AddElement("CourseTeacher");
                        helper.AddElement("CourseTeacher", "RefCourseID", _courseID);
                        helper.AddElement("CourseTeacher", "RefTeacherID", each.Teacher.TeacherID);
                        helper.AddElement("CourseTeacher", "Sequence", each.Sequence.ToString());
                    }
                }

                EditCourse.RemoveCourseTeachers(_courseID);
                if (insert_required)
                {
                    EditCourse.AddCourseTeacher(helper);
                }
            }

            public event EventHandler ValueChanged;
            protected void RaiseValueChanged()
            {
                if (ValueChanged != null)
                    ValueChanged(this, EventArgs.Empty);
            }

            private void SyncSelectedItem()
            {
                _errors.SetError(_combobox, "");
                int index = _combobox.FindString(_combobox.Text);
                if (index >= 0)
                    _combobox.SelectedIndex = index;
                else
                {
                    if ((_combobox.Tag != null) && (_combobox.Tag.ToString() == "ForceValidate"))
                        _errors.SetError(_combobox, "你輸入的文字不在清單中。");
                }
            }

            public class TeacherItem : ButtonItem
            {
                private int _sequence;
                private TeacherInfo _teacher;

                public TeacherItem(string name, string text, int sequence)
                    : base(name, text)
                {
                    _sequence = sequence;
                }

                public int Sequence
                {
                    get { return _sequence; }
                }

                public TeacherInfo Teacher
                {
                    get { return _teacher; }
                    set { _teacher = value; }
                }

            }
        }
        #endregion

        #region TeacherInfo
        private class TeacherInfo
        {
            private readonly string _teacher_id;

            private readonly string _teacher_name;

            public static TeacherInfo Unknow
            {
                get { return new TeacherInfo(); }
            }

            public TeacherInfo(XmlElement teacher)
            {
                _teacher_id = teacher.GetAttribute("ID");

                string name = teacher.SelectSingleNode("TeacherName").InnerText;
                string nick = teacher.SelectSingleNode("Nickname").InnerText;

                if (string.IsNullOrEmpty(nick))
                    _teacher_name = name;
                else
                    _teacher_name = string.Format("{0}({1})", name, nick);
            }

            public TeacherInfo(string teacherId, string teacherName)
            {
                _teacher_id = teacherId;
                _teacher_name = TeacherName;
            }

            private TeacherInfo()
            {
                _teacher_name = "";
                _teacher_id = "-1";
            }

            public string TeacherID
            {
                get { return _teacher_id; }
            }

            public string TeacherName
            {
                get { return _teacher_name; }
            }

            public override bool Equals(object obj)
            {
                TeacherInfo teacher = obj as TeacherInfo;
                if (teacher != null)
                    return teacher._teacher_id == _teacher_id;
                else
                    return CompareObject(obj);
            }

            private bool CompareObject(object obj)
            {
                if (obj == null || obj.ToString() == string.Empty)
                    return TeacherID == "-1";
                else if (obj is String)
                    return obj.ToString() == _teacher_id;
                else if (obj is Int32)
                    return int.Parse(obj.ToString()) == int.Parse(_teacher_id);
                else return false;
            }

            public override int GetHashCode()
            {
                return int.Parse(_teacher_id);
            }

            public override string ToString()
            {
                return TeacherName + ":" + TeacherID;
            }
        }
        #endregion

        #region ClassInfo
        private class ClassInfo
        {
            public readonly string _class_id;

            public readonly string _class_name;

            public static ClassInfo Unknow
            {
                get { return new ClassInfo(); }
            }

            public ClassInfo(XmlElement classInfo)
            {
                _class_id = classInfo.GetAttribute("ID");
                _class_name = classInfo.SelectSingleNode("ClassName").InnerText;
            }

            private ClassInfo()
            {
                _class_id = "-1";
                _class_name = "";
            }

            public string ClassID
            {
                get { return _class_id; }
            }

            public string ClassName
            {
                get { return _class_name; }
            }

            public override bool Equals(object obj)
            {
                ClassInfo class_info = obj as ClassInfo;
                if (class_info != null)
                    return class_info._class_id == _class_id;
                else
                    return CompareObject(obj);
            }

            private bool CompareObject(object obj)
            {
                if (obj == null || obj.ToString() == string.Empty)
                    return ClassID == "-1"; //Null 代表 -1。
                else if (obj is String)
                    return obj.ToString() == _class_id;
                else if (obj is Int32)
                    return int.Parse(obj.ToString()) == int.Parse(_class_id);
                else return false;
            }

            public override int GetHashCode()
            {
                return int.Parse(_class_id);
            }

            public override string ToString()
            {
                return ClassName + ":" + ClassID;
            }
        }
        #endregion

        #region ExamTemplateInfo
        private class ExamTemplateInfo
        {
            public readonly string _template_id;

            public readonly string _template_name;

            public static ExamTemplateInfo Unknow
            {
                get { return new ExamTemplateInfo(); }
            }

            public ExamTemplateInfo(XmlElement classInfo)
            {
                _template_id = classInfo.GetAttribute("ID");
                _template_name = classInfo.SelectSingleNode("TemplateName").InnerText;
            }

            private ExamTemplateInfo()
            {
                _template_id = "-1";
                _template_name = "";
            }

            public string TemplateID
            {
                get { return _template_id; }
            }

            public string TemplateName
            {
                get { return _template_name; }
            }

            public override bool Equals(object obj)
            {
                ExamTemplateInfo template_info = obj as ExamTemplateInfo;
                if (template_info != null)
                    return template_info._template_id == _template_id;
                else
                    return CompareObject(obj);
            }

            private bool CompareObject(object obj)
            {
                if (obj == null || obj.ToString() == string.Empty)
                    return TemplateID == "-1"; //Null 代表 -1。
                else if (obj is String)
                    return obj.ToString() == _template_id;
                else if (obj is Int32)
                    return int.Parse(obj.ToString()) == int.Parse(_template_id);
                else return false;
            }

            public override int GetHashCode()
            {
                return int.Parse(_template_id);
            }

            public override string ToString()
            {
                return TemplateName + ":" + TemplateID;
            }
        }
        #endregion

        private void BasicInfo_DoubleClick(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
                XmlBox.ShowXml(_current_response.BaseElement);
        }

        private void RefreshCourseInfo()
        {
            if (!_saving)
            {
                _init_required = true;
                LoadContent(RunningID);
            }
        }
    }

    /// <summary>
    /// 記錄課程基本資料的機器
    /// </summary>
    class CourseBaseLogMachine
    {
        Dictionary<string, string> beforeData = new Dictionary<string, string>();
        Dictionary<string, string> afterData = new Dictionary<string, string>();

        public void AddBefore(string key, string value)
        {
            if (!beforeData.ContainsKey(key))
                beforeData.Add(key, value);
            else
                beforeData[key] = value;
        }

        public void AddAfter(string key, string value)
        {
            if (!afterData.ContainsKey(key))
                afterData.Add(key, value);
            else
                afterData[key] = value;
        }

        public string GetDescription()
        {
            //「」
            StringBuilder desc = new StringBuilder("");

            foreach (string key in beforeData.Keys)
            {
                if (afterData.ContainsKey(key) && afterData[key] != beforeData[key])
                {
                    desc.AppendLine("欄位「" + key + "」由「" + beforeData[key] + "」變更為「" + afterData[key] + "」");
                }
            }

            return desc.ToString();
        }
    }
}