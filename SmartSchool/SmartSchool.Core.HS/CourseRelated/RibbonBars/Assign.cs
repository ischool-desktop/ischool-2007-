using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.TeacherRelated;
using DevComponents.DotNetBar;
using IntelliSchool.DSA30.Util;
using SmartSchool.CourseRelated;
using SmartSchool.StudentRelated;
using System.Xml;
using SmartSchool.StudentRelated.Validate;
using SmartSchool.Common.Validate;
using SmartSchool.Feature.Course;
using SmartSchool.Feature.ExamTemplate;
using SmartSchool.Common;
using SmartSchool.ExceptionHandler;
using SmartSchool.Security;

namespace SmartSchool.CourseRelated.RibbonBars
{
    public partial class Assign : SmartSchool.CourseRelated.RibbonBars.RibbonBarBase
    {
        //�v���P�_
        FeatureAccessControl attendStudentCtrl;
        FeatureAccessControl assignTeacherCtrl;
        FeatureAccessControl scoresCtrl;

        private ButtonItem _quick_shortcut;
        private List<ButtonItem> _items;
        public Assign()
        {
            InitializeComponent();

            _quick_shortcut = new ButtonItem();
            _quick_shortcut.Text = "�]�w�ֳt�I��˪�";
            _quick_shortcut.Visible = false;

            if (Site != null && Site.DesignMode)
                return;

            btnAssignTeacher.Enabled = false;
            btnAttendStudent.Enabled = false;
            SmartSchool.StudentRelated.Student.Instance.TemporalChanged += new EventHandler(Student_TemporalChanged);
            SmartSchool.TeacherRelated.Teacher.Instance.TemporalChanged += new EventHandler(Teacher_TemporalChanged);
            CourseEntity.Instance.ForeignTableChanged += new EventHandler(Instance_ForeignTableChanged);
            CourseEntity.Instance.SelectionChanged += new EventHandler(Course_SelectionChanged);

            //�v���P�_
            attendStudentCtrl = new FeatureAccessControl("Button0570");
            assignTeacherCtrl = new FeatureAccessControl("Button0580");
            scoresCtrl = new FeatureAccessControl("Button0590");

            attendStudentCtrl.Inspect(btnAttendStudent);
            assignTeacherCtrl.Inspect(btnAssignTeacher);
            scoresCtrl.Inspect(btnScores);
        }

        void Student_TemporalChanged(object sender, EventArgs e)
        {
            List<BriefStudentData> students = SmartSchool.StudentRelated.Student.Instance.TemporaStudent;
            btnAttendStudent.Enabled = students.Count > 0 && CourseEntity.Instance.SelectionCourse.Count>0;

            attendStudentCtrl.Inspect(btnAttendStudent);
        }

        void Teacher_TemporalChanged(object sender, EventArgs e)
        {
            List<BriefTeacherData> teachers = SmartSchool.TeacherRelated.Teacher.Instance.TemporaTeacher;
            btnAssignTeacher.Enabled = teachers.Count > 0 && CourseEntity.Instance.SelectionCourse.Count > 0;

            assignTeacherCtrl.Inspect(btnAssignTeacher);
        }

        void item_Click(object sender, EventArgs e)
        {
            ButtonItem item = sender as ButtonItem;
            string teacherid = item.Name;

            CourseCollection courses = SmartSchool.CourseRelated.CourseEntity.Instance.SelectionCourse;
            if (courses.Count == 0)
            {
                MsgBox.Show("�z������ܽҵ{");
                return;
            }

            DSXmlHelper removeBySequence = new DSXmlHelper("Request");
            foreach (CourseInformation info in courses)
            {
                removeBySequence.AddElement("Course");
                removeBySequence.AddElement("Course", "CourseID", info.Identity.ToString());
                removeBySequence.AddElement("Course", "Sequence", "1");
            }

            DSXmlHelper removeByTeacher = new DSXmlHelper("Request");
            foreach (CourseInformation info in courses)
            {
                removeByTeacher.AddElement("Course");
                removeByTeacher.AddElement("Course", "CourseID", info.Identity.ToString());
                removeByTeacher.AddElement("Course", "RefTeacherID", teacherid);
            }

            DSXmlHelper addnew = new DSXmlHelper("Request");
            foreach (CourseInformation info in courses)
            {
                addnew.AddElement("CourseTeacher");
                addnew.AddElement("CourseTeacher", "RefCourseID", info.Identity.ToString());
                addnew.AddElement("CourseTeacher", "RefTeacherID", teacherid);
                addnew.AddElement("CourseTeacher", "Sequence", "1");
            }

            try
            {
                EditCourse.RemoveCourseTeachers(removeBySequence);
                EditCourse.RemoveCourseTeachers(removeByTeacher);
                EditCourse.AddCourseTeacher(addnew);

                List<int> courseIdList = new List<int>();
                foreach (CourseInformation each in courses)
                    courseIdList.Add(each.Identity);

                CourseEntity.Instance.InvokeAfterCourseChange(courseIdList.ToArray());

                MsgBox.Show("��������");
            }
            catch (Exception ex)
            {
                MsgBox.Show("��������:" + ex.Message);
            }
        }

        private void btnAssignTeacher_PopupShowing(object sender, EventArgs e)
        {
            List<BriefTeacherData> teacherList = SmartSchool.TeacherRelated.Teacher.Instance.TemporaTeacher;
            btnAssignTeacher.SubItems.Clear();

            //if (teacherList.Count > 1)
            //{
            //    ButtonItem btnTeacherAll = new ButtonItem("btnTeacherAll", "�[�J�Ҧ��ݳB�z�Юv");
            //    btnTeacherAll.AutoCheckOnClick = false;
            //    btnTeacherAll.Tooltip = "�N�ҿ�ǥͥ[�J�ثe�ݳB�z���Ҧ��ҵ{";
            //    btnTeacherAll.Click += new EventHandler(btnTeacherAll_Click);
            //    btnAssignTeacher.InsertItemAt(btnTeacherAll, 0, true);
            //}

            foreach (BriefTeacherData teacher in teacherList)
            {
                ButtonItem item = new ButtonItem(teacher.ID, teacher.UniqName);
                item.Tooltip = "�N�ҿ�ҵ{�������i" + teacher.UniqName + "�j�Юv";
                item.Click += new EventHandler(item_Click);
                item.AutoCollapseOnClick = true;
                btnAssignTeacher.InsertItemAt(item, 0, false);
            }

            //if (btnAssignTeacher.SubItems.Count > 1)
            //    btnAssignTeacher.SubItems[1].BeginGroup = true;
        }

        //void btnTeacherAll_Click(object sender, EventArgs e)
        //{
        //    List<BriefTeacherData> teachers = SmartSchool.TeacherRelated.Teacher.Instance.TemporaTeacher;
        //    CourseCollection collection = SmartSchool.CourseRelated.CourseEntity.Instance.SelectionCourse;
        //    if (collection.Count == 0)
        //    {
        //        MsgBox.Show("�z������ܦܤ֤@���ҵ{");
        //        return;
        //    }
        //    DSXmlHelper helper = new DSXmlHelper("UpdateRequest");
        //    foreach (CourseInformation info in collection)
        //    {
        //        foreach (BriefTeacherData teacher in teachers)
        //        {
        //            helper.AddElement("Course");
        //            helper.AddElement("Course", "Field");
        //            helper.AddElement("Course/Field", "RefTeacherID", teacher.ID);
        //            helper.AddElement("Course", "Condition");
        //            helper.AddElement("Course/Condition", "ID", info.Identity.ToString());
        //        }
        //    }
        //    try
        //    {
        //        SmartSchool.Feature.Course.EditCourse.UpdateCourse(new DSRequest(helper));
        //        MsgBox.Show("��������");
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgBox.Show("��������:" + ex.Message);
        //    }
        //}

        private void buttonItem54_PopupShowing(object sender, EventArgs e)
        {
            List<BriefStudentData> students = SmartSchool.StudentRelated.Student.Instance.TemporaStudent;
            btnAttendStudent.SubItems.Clear();

            if (students.Count > 1)
            {
                ButtonItem btnStudentAll = new ButtonItem("btnAll", "�[�J�Ҧ��ݳB�z�ǥ�");
                btnStudentAll.AutoCheckOnClick = false;
                btnStudentAll.Tooltip = "�N�ҿ�ǥͥ[�J�ثe�ݳB�z���Ҧ��ҵ{";
                btnStudentAll.Click += new EventHandler(btnStudentAll_Click);
                btnAttendStudent.InsertItemAt(btnStudentAll, 0, true);
            }

            foreach (BriefStudentData student in students)
            {
                ButtonItem sitem = new ButtonItem(student.ID, "�i" + student.ClassName + "�j" + student.Name);
                sitem.Tag = student;
                sitem.AutoCheckOnClick = true;
                sitem.Tooltip = "�N�ҿ�ҵ{�������ǥ͡i" + student.Name + "�j";
                sitem.Click += new EventHandler(sitem_Click);
                btnAttendStudent.InsertItemAt(sitem, 0, false);
            }

            if (btnAttendStudent.SubItems.Count > 1)
                btnAttendStudent.SubItems[1].BeginGroup = true;
        }

        void btnStudentAll_Click(object sender, EventArgs e)
        {
            CourseCollection collection = SmartSchool.CourseRelated.CourseEntity.Instance.SelectionCourse;
            if (collection.Count == 0)
            {
                MsgBox.Show("�z������ܦܤ֤@���ҵ{");
                return;
            }
            //List<BriefStudentData> invalidStudents = SmartSchool.StudentRelated.Student.Instance.TemporaStudent;
            List<BriefStudentData> students = SmartSchool.StudentRelated.Student.Instance.TemporaStudent;

            //���Ҿǥ͸��
            //#region ���Ҿǥ͸��
            //AbstractValidateStudent validate = new ValidateBasic(new ValidateGradeYear(), new ValidateGraduationPlan());
            //ErrorViewer viewer = new ErrorViewer();
            //viewer.Text = "����ǥ͸�ƿ��~�L�k�[�J�׽�";
            //bool pass = true;
            //foreach (BriefStudentData var in students)
            //{
            //    pass &= validate.Validate(var, viewer);
            //}
            //if (!pass)
            //{
            //    viewer.Show();
            //    return;
            //}
            //#endregion


            DSXmlHelper helper = new DSXmlHelper("SelectRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "RefStudentID");
            helper.AddElement("Field", "RefCourseID");
            helper.AddElement("Condition");
            helper.AddElement("Condition", "Or");
            foreach (CourseInformation info in collection)
            {
                foreach (BriefStudentData student in students)
                {
                    helper.AddElement("Condition/Or", "And");
                    helper.AddElement("Condition/Or/And", "StudentID", student.ID);
                    helper.AddElement("Condition/Or/And", "CourseID", info.Identity.ToString());
                }
            }
            DSResponse dsrsp = SmartSchool.Feature.Course.QueryCourse.GetSCAttend(new DSRequest(helper));

            int insertCount = 0;
            helper = new DSXmlHelper("InsertSCAttend");


            foreach (CourseInformation info in collection)
            {
                foreach (BriefStudentData student in students)
                {
                    //�ǥͥ����O�b�եͤ~��[�J�ҵ{�C
                    if (!student.IsNormal) continue;

                    XmlElement element = null;
                    foreach (XmlElement ele in dsrsp.GetContent().GetElements("Student"))
                    {
                        string courseid = info.Identity.ToString();
                        string studentid = student.ID;
                        if (courseid == ele.SelectSingleNode("RefCourseID").InnerText && studentid == ele.SelectSingleNode("RefStudentID").InnerText)
                            element = ele;
                    }

                    if (element == null)
                    {
                        // �S��Element ��ܸӾǥͨS���׹L�o�ӽҵ{�A�n�s�W
                        helper.AddElement("Attend");
                        helper.AddElement("Attend", "RefCourseID", info.Identity.ToString());
                        helper.AddElement("Attend", "RefStudentID", student.ID);

                        //// �d�X�����
                        //string required = "��";
                        //string requiredby = "�խq";
                        ////foreach (GraduationPlanSubject subject in student.GraduationPlanInfo.Subjects)
                        ////{
                        ////    if (info.Subject == subject.SubjectName && info.SubjectLevel == subject.Level)
                        ////    {
                        ////        required = subject.Required;
                        ////        requiredby = subject.RequiredBy;
                        ////    }
                        ////}
                        //GraduationPlanSubject subject = student.GraduationPlanInfo.GetSubjectInfo(info.Subject, info.SubjectLevel);
                        //required = subject.Required;
                        //requiredby = subject.RequiredBy;
                        //helper.AddElement("Attend", "IsRequired", GetRequiredString(required));
                        //helper.AddElement("Attend", "RequiredBy", requiredby);
                        //helper.AddElement("Attend", "GradeYear", student.GradeYear);
                        insertCount++;
                    }
                }
            }
            if (insertCount > 0)
                SmartSchool.Feature.Course.AddCourse.AttendCourse(helper);
            MsgBox.Show("�B�z����");
            SmartSchool.Broadcaster.Events.Items["�ҵ{/�ǥͭ׽�"].Invoke(collection);
        }

        void sitem_Click(object sender, EventArgs e)
        {
            ButtonItem item = sender as ButtonItem;
            BriefStudentData data = item.Tag as BriefStudentData;
            string studentid = item.Name;

            CourseCollection collection = SmartSchool.CourseRelated.CourseEntity.Instance.SelectionCourse;
            if (collection.Count == 0)
            {
                MsgBox.Show("�z������ܦܤ֤@���ҵ{");
                return;
            }

            //���ҳ浧�ǥ͸��
            //#region ���Ҿǥ͸��
            //AbstractValidateStudent validate = new ValidateBasic(new ValidateGradeYear(), new ValidateGraduationPlan());
            //ErrorViewer viewer = new ErrorViewer();
            //viewer.Text = "�ǥ͸�ƿ��~�L�k�[�J�׽�";
            //bool pass = true;
            //pass &= validate.Validate(data, viewer);
            //if (!pass)
            //{
            //    viewer.Show();
            //    return;
            //}
            //#endregion

            DSXmlHelper helper = new DSXmlHelper("SelectRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "RefCourseID");
            helper.AddElement("Condition");
            helper.AddElement("Condition", "Or");
            foreach (CourseInformation info in collection)
            {
                helper.AddElement("Condition/Or", "And");
                helper.AddElement("Condition/Or/And", "StudentID", studentid);
                helper.AddElement("Condition/Or/And", "CourseID", info.Identity.ToString());
            }
            DSResponse dsrsp = SmartSchool.Feature.Course.QueryCourse.GetSCAttend(new DSRequest(helper));

            int insertCount = 0;
            helper = new DSXmlHelper("InsertSCAttend");

            foreach (CourseInformation info in collection)
            {
                XmlElement element = dsrsp.GetContent().GetElement("Student[RefCourseID='" + info.Identity + "']");
                if (element == null)
                {
                    // �S��Element ��ܸӾǥͨS���׹L�o�ӽҵ{�A�n�s�W
                    helper.AddElement("Attend");
                    helper.AddElement("Attend", "RefCourseID", info.Identity.ToString());
                    helper.AddElement("Attend", "RefStudentID", studentid);

                    //// �d�X�����
                    //string required = "��";
                    //string requiredby = "�խq";
                    ////foreach (GraduationPlanSubject subject in data.GraduationPlanInfo.Subjects)
                    ////{
                    ////    if (info.Subject == subject.SubjectName && info.SubjectLevel == subject.Level)
                    ////    {
                    ////        required = subject.Required;
                    ////        requiredby = subject.RequiredBy;
                    ////    }
                    ////}
                    //GraduationPlanSubject subject = data.GraduationPlanInfo.GetSubjectInfo(info.Subject, info.SubjectLevel);
                    //required = subject.Required;
                    //requiredby = subject.RequiredBy;
                    //helper.AddElement("Attend", "IsRequired", GetRequiredString(required));
                    //helper.AddElement("Attend", "RequiredBy", requiredby);
                    //helper.AddElement("Attend", "GradeYear", data.GradeYear);
                    insertCount++;
                }
            }
            if (insertCount > 0)
                SmartSchool.Feature.Course.AddCourse.AttendCourse(helper);
            MsgBox.Show("�B�z����");
            SmartSchool.Broadcaster.Events.Items["�ҵ{/�ǥͭ׽�"].Invoke(collection);
        }

        private string GetRequiredString(string input)
        {
            if (input == "��" || input == "��")
                return input;
            else
            {
                if (input == "����")
                    return "��";
                else if (input == "���")
                    return "��";
                else
                    throw new ArgumentException("�u���\�u���v�Ρu��v�A�������u" + input + "�v");
            }
        }


        private void Course_SelectionChanged(object sender, EventArgs e)
        {
            CourseCollection courses = CourseEntity.Instance.SelectionCourse;

            bool singleSel = (courses.Count == 1);
            bool multiSel = (courses.Count >= 1);

            btnAssignTeacher.Enabled = multiSel && SmartSchool.TeacherRelated.Teacher.Instance.TemporaTeacher.Count > 0;
            btnAttendStudent.Enabled = multiSel && SmartSchool.StudentRelated.Student.Instance.TemporaStudent.Count > 0;
            btnScores.Enabled = multiSel;

            attendStudentCtrl.Inspect(btnAttendStudent);
            assignTeacherCtrl.Inspect(btnAssignTeacher);
            scoresCtrl.Inspect(btnScores);
        }

        private void Instance_ForeignTableChanged(object sender, EventArgs e)
        {
            _items = null;
        }

        private void btnScores_PopupOpen(object sender, PopupOpenEventArgs e)
        {
            try
            {
                if (_items == null)
                {
                    _items = new List<ButtonItem>();

                    XmlElement templates = QueryTemplate.GetAbstractList();
                    foreach (XmlElement each in templates.SelectNodes("ExamTemplate"))
                    {
                        TemplateButton button = new TemplateButton(each);
                        button.Click += new EventHandler(TemplateButton_Click);
                        _items.Add(button);
                    }
                }

                btnScores.SubItems.Clear();
                foreach (TemplateButton button in _items)
                {
                    btnScores.SubItems.Add(button);
                }
                btnScores.SubItems.Add(_quick_shortcut);

                btnScores.ShowSubItems = true;
                btnScores.RecalcSize();
            }
            catch (Exception ex)
            {
                CurrentUser user = CurrentUser.Instance;
                BugReporter.ReportException(user.SystemName, user.SystemVersion, ex, false);

                MsgBox.Show(ex.Message, Application.ProductName);
            }
        }

        private void TemplateButton_Click(object sender, EventArgs e)
        {
            try
            {
                TemplateButton button = sender as TemplateButton;
                string templateId = button.Identity;

                CourseCollection courses = CourseEntity.Instance.SelectionCourse;

                if (courses.Count > 0)
                {
                    DSXmlHelper req = new DSXmlHelper("UpdateRequest");

                    foreach (CourseInformation each in courses)
                    {
                        req.AddElement("Course");
                        req.AddElement("Course", "Field", "<RefExamTemplateID>" + templateId + "</RefExamTemplateID>", true);
                        req.AddElement("Course", "Condition", "<ID>" + each.Identity + "</ID>", true);
                    }

                    EditCourse.UpdateCourse(new DSRequest(req));

                    List<int> courseids = new List<int>();
                    foreach (CourseInformation each in courses)
                        courseids.Add(each.Identity);

                    CourseEntity.Instance.InvokeAfterCourseChange(courseids.ToArray());

                    MsgBox.Show("�ҵ{�����˪����w�����C\n���w�ҵ{�ơG" + courses.Count, Application.ProductName);
                }
            }
            catch (Exception ex)
            {
                CurrentUser user = CurrentUser.Instance;
                BugReporter.ReportException(user.SystemName, user.SystemVersion, ex, false);

                MsgBox.Show(ex.Message, Application.ProductName);
            }
        }

        private class TemplateButton : ButtonItem
        {
            private string _identity;

            public TemplateButton(XmlElement templateInfo)
            {
                Text = templateInfo.SelectSingleNode("TemplateName").InnerText;
                _identity = templateInfo.GetAttribute("ID");
            }

            public string Identity
            {
                get { return _identity; }
            }
        }
    }
}

