using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using StudentEntity = SmartSchool.StudentRelated.Student;
using SmartSchool.StudentRelated;
using DevComponents.DotNetBar;
using SmartSchool.TagManage;
using SmartSchool.Feature.Tag;
using SmartSchool.Common;
using SmartSchool.CourseRelated;
using SmartSchool.StudentRelated.Validate;
using SmartSchool.Common.Validate;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.ApplicationLog;
using SmartSchool.Security;

namespace SmartSchool.CourseRelated.RibbonBars.OtherTab
{
    public partial class AssignStudentAttendCourse : RibbonBarBase
    {
        public ButtonItem Button { get { return btnAttend; } }

        FeatureAccessControl attendCtrl;

        public AssignStudentAttendCourse()
        {
            InitializeComponent();

            SmartSchool.Broadcaster.Events.Items["學生/選取變更"].Handler += new EventHandler<SmartSchool.Broadcaster.EventArguments>(Category_Handler);
            SmartSchool.CourseRelated.CourseEntity.Instance.TemporalChanged += new EventHandler(CheckEnable);

            //權限判斷 - 指定/修課	Button0110
            attendCtrl = new FeatureAccessControl("Button0110");

            attendCtrl.Inspect(btnAttend);
        }

        void Category_Handler(object sender, SmartSchool.Broadcaster.EventArguments e)
        {
            CheckEnable(null, null);
        }


        void CheckEnable(object sender, EventArgs e)
        {
            CourseCollection collection = SmartSchool.CourseRelated.CourseEntity.Instance.TemporalCourse;
            btnAttend.Enabled = collection.Count > 0 && SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count > 0;
            attendCtrl.Inspect(btnAttend);
        }

        #region 指定修課

        private void buttonItem64_PopupShowing(object sender, EventArgs e)
        {

            CourseCollection collection = SmartSchool.CourseRelated.CourseEntity.Instance.TemporalCourse;
            btnAttend.SubItems.Clear();

            if (collection.Count > 1)
            {
                ButtonItem btnAll = new ButtonItem("btnAll", "加入所有待處理課程");
                btnAll.AutoCheckOnClick = false;
                btnAll.Tooltip = "將所選學生加入目前待處理中所有課程";
                btnAll.Click += new EventHandler(btnAll_Click);
                btnAttend.InsertItemAt(btnAll, 0, true);
            }

            foreach (CourseInformation course in collection)
            {
                ButtonItem citem = new ButtonItem(course.Identity.ToString(), course.CourseName);
                citem.Tag = course;
                citem.AutoCheckOnClick = true;
                citem.Tooltip = "將所選學生指派課程【" + course.CourseName + "】";
                citem.Click += new EventHandler(citem_Click);
                btnAttend.InsertItemAt(citem, 0, false);
            }

            if (btnAttend.SubItems.Count > 1)
                btnAttend.SubItems[1].BeginGroup = true;
        }

        void btnAll_Click(object sender, EventArgs e)
        {
            CourseCollection collection = SmartSchool.CourseRelated.CourseEntity.Instance.TemporalCourse;
            List<BriefStudentData> students = SmartSchool.StudentRelated.Student.Instance.SelectionStudents;

            if (students.Count == 0)
            {
                MsgBox.Show("您必須選擇至少一位學生");
                return;
            }

            //驗證學生資料
            //AbstractValidateStudent validate = new ValidateBasic(new ValidateGradeYear(), new ValidateGraduationPlan());
            //ErrorViewer viewer = new ErrorViewer();
            //viewer.Text = "學生資料錯誤無法自動指定修課";
            //bool IsValidated = true;
            //foreach ( BriefStudentData var in students )
            //{
            //    IsValidated &= validate.Validate(var, viewer);
            //}
            //if ( !IsValidated )
            //{
            //    viewer.Show();
            //    return;
            //}

            DSXmlHelper helper = new DSXmlHelper("SelectRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "RefCourseID");
            helper.AddElement("Field", "RefStudentID");
            helper.AddElement("Condition");
            helper.AddElement("Condition", "Or");
            foreach (BriefStudentData student in students)
            {
                foreach (CourseInformation info in collection)
                {
                    helper.AddElement("Condition/Or", "And");
                    helper.AddElement("Condition/Or/And", "StudentID", student.ID);
                    helper.AddElement("Condition/Or/And", "CourseID", info.Identity.ToString());
                }
            }
            DSResponse dsrsp = SmartSchool.Feature.Course.QueryCourse.GetSCAttend(new DSRequest(helper));

            int insertCount = 0;
            helper = new DSXmlHelper("InsertSCAttend");

            foreach (BriefStudentData student in students)
            {
                foreach (CourseInformation info in collection)
                {
                    XmlElement element = null;
                    foreach (XmlElement ele in dsrsp.GetContent().GetElements("Student"))
                    {
                        string courseid = ele.SelectSingleNode("RefCourseID").InnerText;
                        string studentid = ele.SelectSingleNode("RefStudentID").InnerText;
                        if (courseid == info.Identity.ToString() && studentid == student.ID)
                        {
                            element = ele;
                            break;
                        }
                    }
                    if (element == null)
                    {
                        // 沒有Element 表示該學生沒有修過這個課程，要新增
                        helper.AddElement("Attend");
                        helper.AddElement("Attend", "RefCourseID", info.Identity.ToString());
                        helper.AddElement("Attend", "RefStudentID", student.ID);

                        //// 查出必選修
                        //string required = "選";
                        //string requiredby = "校訂";
                        ////if (student.GraduationPlanInfo != null)
                        ////{
                        ////    foreach (GraduationPlanSubject subject in student.GraduationPlanInfo.Subjects)
                        ////    {
                        ////        if (info.Subject == subject.SubjectName && info.SubjectLevel == subject.Level)
                        ////        {
                        ////            required = subject.Required;
                        ////            requiredby = subject.RequiredBy;
                        ////        }
                        ////    }
                        ////}
                        //GraduationPlanSubject subject = student.GraduationPlanInfo.GetSubjectInfo(info.Subject, info.SubjectLevel);
                        //required = subject.Required;
                        //requiredby = subject.RequiredBy;


                        //helper.AddElement("Attend", "IsRequired", InjectionRequired(required));
                        //helper.AddElement("Attend", "RequiredBy", requiredby);
                        //helper.AddElement("Attend", "GradeYear", student.GradeYear);
                        insertCount++;
                    }
                }
            }
            if (insertCount > 0)
                SmartSchool.Feature.Course.AddCourse.AttendCourse(helper);
            MsgBox.Show("處理完畢");
        }

        void citem_Click(object sender, EventArgs e)
        {
            ButtonItem item = sender as ButtonItem;
            CourseInformation info = item.Tag as CourseInformation;
            string courseid = item.Name;
            List<BriefStudentData> students = SmartSchool.StudentRelated.Student.Instance.SelectionStudents;
            if (students.Count == 0)
            {
                MsgBox.Show("您必須選擇至少一位學生");
                return;
            }

            //驗證學生資料
            //AbstractValidateStudent validate = new ValidateBasic(new ValidateGradeYear(), new ValidateGraduationPlan());
            //ErrorViewer viewer = new ErrorViewer();
            //viewer.Text = "學生資料錯誤無法自動指定修課";
            //bool IsValidated = true;
            //foreach ( BriefStudentData var in students )
            //{
            //    IsValidated &= validate.Validate(var, viewer);
            //}
            //if ( !IsValidated )
            //{
            //    viewer.Show();
            //    return;
            //}

            DSXmlHelper helper = new DSXmlHelper("SelectRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "RefStudentID");
            helper.AddElement("Condition");
            helper.AddElement("Condition", "Or");
            foreach (BriefStudentData student in students)
            {
                helper.AddElement("Condition/Or", "And");
                helper.AddElement("Condition/Or/And", "StudentID", student.ID);
                helper.AddElement("Condition/Or/And", "CourseID", courseid);
            }
            DSResponse dsrsp = SmartSchool.Feature.Course.QueryCourse.GetSCAttend(new DSRequest(helper));

            int insertCount = 0;
            helper = new DSXmlHelper("InsertSCAttend");

            foreach (BriefStudentData student in students)
            {
                XmlElement element = dsrsp.GetContent().GetElement("Student[RefStudentID='" + student.ID + "']");
                if (element == null)
                {
                    // 沒有Element 表示該學生沒有修過這個課程，要新增
                    helper.AddElement("Attend");
                    helper.AddElement("Attend", "RefCourseID", courseid);
                    helper.AddElement("Attend", "RefStudentID", student.ID);

                    // 查出必選修
                    //string required = "選";
                    //string requiredby = "校訂";
                    ////if (student.GraduationPlanInfo != null)
                    ////{
                    ////    foreach (GraduationPlanSubject subject in student.GraduationPlanInfo.Subjects)
                    ////    {
                    ////        if (info.Subject == subject.SubjectName && info.SubjectLevel == subject.Level)
                    ////        {
                    ////            required = subject.Required;
                    ////            requiredby = subject.RequiredBy;
                    ////        }
                    ////    }
                    ////}
                    //GraduationPlanSubject subject = student.GraduationPlanInfo.GetSubjectInfo(info.Subject, info.SubjectLevel);
                    //required = subject.Required;
                    //requiredby = subject.RequiredBy;

                    //helper.AddElement("Attend", "IsRequired", InjectionRequired(required));
                    //helper.AddElement("Attend", "RequiredBy", requiredby);
                    //helper.AddElement("Attend", "GradeYear", student.GradeYear);
                    insertCount++;
                }
            }
            if (insertCount > 0)
                SmartSchool.Feature.Course.AddCourse.AttendCourse(helper);
            MsgBox.Show("處理完畢");
        }

        private string InjectionRequired(string required)
        {
            if (required == "必" || required == "選")
                return required;
            else
            {
                if (required == "必修")
                    return "必";
                else if (required == "選修")
                    return "選";
                else
                    throw new ArgumentException("只能允許「必」或「選」，沒有此種選項：" + required);
            }
        }
        #endregion
    }
}