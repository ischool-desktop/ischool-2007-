using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.CourseRelated;
using DevComponents.DotNetBar;
using SmartSchool.TeacherRelated;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature.Course;
using SmartSchool.Common;
using SmartSchool.Security;

namespace SmartSchool.CourseRelated.RibbonBars.OtherTab
{
    public partial class AssignTeacherTeachCourse : RibbonBarBase
    {
        FeatureAccessControl assignCtrl;

        public AssignTeacherTeachCourse()
        {
            InitializeComponent();
            btnAssign.Enabled = false;
            SmartSchool.CourseRelated.CourseEntity.Instance.TemporalChanged += new EventHandler(Instance_TemporalChanged);
            SmartSchool.TeacherRelated.Teacher.Instance.SelectionChanged += new EventHandler(Instance_SelectionChanged);

            //權限判斷 - 指定/授課
            assignCtrl = new FeatureAccessControl("Button0480");
            assignCtrl.Inspect(btnAssign);
        }

        void Instance_SelectionChanged(object sender, EventArgs e)
        {
            btnAssign.Enabled = IsButtonEnable();

            assignCtrl.Inspect(btnAssign);
        }

        void Instance_TemporalChanged(object sender, EventArgs e)
        {
            btnAssign.Enabled = IsButtonEnable();

            assignCtrl.Inspect(btnAssign);
        }

        private bool IsButtonEnable()
        {
            CourseCollection collection = SmartSchool.CourseRelated.CourseEntity.Instance.TemporalCourse;
            List<BriefTeacherData> teachers = SmartSchool.TeacherRelated.Teacher.Instance.SelectionTeachers;
            if (collection.Count == 0)
            {
                btnAssign.Tooltip = "必須至少有一筆待處理課程";
                return false;
            }
            if (teachers.Count > 1)
            {
                btnAssign.Tooltip = "只可以有一名指派教師";
                return false;
            }
            if (teachers.Count == 0)
            {
                btnAssign.Tooltip = "必須選擇一名教師";
                return false;
            }
            btnAssign.Tooltip = "可按此處將目前所選擇的教師分派待處理課程";
            return true;
        }
        public override string ProcessTabName
        {
            get
            {
                return "教師";
            }
        }

        private void btnAssign_PopupShowing(object sender, EventArgs e)
        {
            CourseCollection collection = SmartSchool.CourseRelated.CourseEntity.Instance.TemporalCourse;
            btnAssign.SubItems.Clear();

            if (collection.Count > 1)
            {
                ButtonItem btnCourseAll = new ButtonItem("btnCourseAll", "加入所有待處理課程");
                btnCourseAll.AutoCheckOnClick = false;
                btnCourseAll.Tooltip = "將目前待處理中所有課程分配予所選教師";
                btnCourseAll.Click += new EventHandler(btnCourseAll_Click);
                btnAssign.InsertItemAt(btnCourseAll, 0, true);
            }

            foreach (CourseInformation info in collection)
            {
                ButtonItem item = new ButtonItem(info.Identity.ToString(), info.CourseName);
                item.AutoCheckOnClick = true;
                item.Tooltip = "指派課程【" + info.CourseName + "】給所選教師";
                item.Click += new EventHandler(item_Click);
                btnAssign.InsertItemAt(item, 0, false);
            }

            if (btnAssign.SubItems.Count > 1)
                btnAssign.SubItems[1].BeginGroup = true;
        }

        void btnCourseAll_Click(object sender, EventArgs e)
        {
            CourseCollection collection = SmartSchool.CourseRelated.CourseEntity.Instance.TemporalCourse;
            List<BriefTeacherData> teachers = SmartSchool.TeacherRelated.Teacher.Instance.SelectionTeachers;
            BriefTeacherData teacher = teachers[0];

            DSXmlHelper helper = new DSXmlHelper("UpdateRequest");

            DSXmlHelper removeBySequence = new DSXmlHelper("Request");
            DSXmlHelper removeByTeacher = new DSXmlHelper("Request");
            DSXmlHelper addnew = new DSXmlHelper("Request");
            foreach (CourseInformation info in collection)
            {
                removeBySequence.AddElement("Course");
                removeBySequence.AddElement("Course", "CourseID", info.Identity.ToString());
                removeBySequence.AddElement("Course", "Sequence", "1");

                removeByTeacher.AddElement("Course");
                removeByTeacher.AddElement("Course", "CourseID", info.Identity.ToString());
                removeByTeacher.AddElement("Course", "RefTeacherID", teacher.ID);

                addnew.AddElement("CourseTeacher");
                addnew.AddElement("CourseTeacher", "RefCourseID", info.Identity.ToString());
                addnew.AddElement("CourseTeacher", "RefTeacherID", teacher.ID);
                addnew.AddElement("CourseTeacher", "Sequence", "1");

            }
            try
            {
                EditCourse.RemoveCourseTeachers(removeBySequence);
                EditCourse.RemoveCourseTeachers(removeByTeacher);
                EditCourse.AddCourseTeacher(addnew);

                List<int> courseIdList = new List<int>();
                foreach (CourseInformation each in collection)
                    courseIdList.Add(each.Identity);

                CourseEntity.Instance.InvokeAfterCourseChange(courseIdList.ToArray());

                //Log
                StringBuilder course_texts = new StringBuilder("");
                foreach (CourseInformation info in collection)
                {
                    if (!string.IsNullOrEmpty(course_texts.ToString())) course_texts.Append("、");
                    course_texts.Append(info.CourseName);
                }
                CurrentUser.Instance.AppLog.Write(
                    SmartSchool.ApplicationLog.EntityType.Teacher,
                    "教師授課",
                    teacher.ID,
                    "指定「" + teacher.TeacherName + (string.IsNullOrEmpty(teacher.Nickname) ? "" : "(" + teacher.Nickname + ")") + "」課程：" + course_texts.ToString(),
                    "教師",
                    "");

                MsgBox.Show("指派完成");
            }
            catch (Exception ex)
            {
                MsgBox.Show("指派失敗:" + ex.Message);
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            ButtonItem item = sender as ButtonItem;
            string courseid = item.Name;

            List<BriefTeacherData> teachers = SmartSchool.TeacherRelated.Teacher.Instance.SelectionTeachers;
            BriefTeacherData teacher = teachers[0];

            DSXmlHelper removeBySequence = new DSXmlHelper("Request");
            removeBySequence.AddElement("Course");
            removeBySequence.AddElement("Course", "CourseID", courseid);
            removeBySequence.AddElement("Course", "Sequence", "1");

            DSXmlHelper removeByTeacher = new DSXmlHelper("Request");
            removeByTeacher.AddElement("Course");
            removeByTeacher.AddElement("Course", "CourseID", courseid);
            removeByTeacher.AddElement("Course", "RefTeacherID", teacher.ID);

            DSXmlHelper addnew = new DSXmlHelper("Request");
            addnew.AddElement("CourseTeacher");
            addnew.AddElement("CourseTeacher", "RefCourseID", courseid);
            addnew.AddElement("CourseTeacher", "RefTeacherID", teacher.ID);
            addnew.AddElement("CourseTeacher", "Sequence", "1");

            try
            {
                EditCourse.RemoveCourseTeachers(removeBySequence);
                EditCourse.RemoveCourseTeachers(removeByTeacher);
                EditCourse.AddCourseTeacher(addnew);

                CourseEntity.Instance.InvokeAfterCourseChange(int.Parse(courseid));

                //Log
                CurrentUser.Instance.AppLog.Write(
                    SmartSchool.ApplicationLog.EntityType.Teacher,
                    "教師授課",
                    teacher.ID,
                    "指定「" + teacher.TeacherName + (string.IsNullOrEmpty(teacher.Nickname) ? "" : "(" + teacher.Nickname + ")") + "」課程：" + item.Text,
                    "教師",
                    "");

                MsgBox.Show("指派完成");
            }
            catch (Exception ex)
            {
                MsgBox.Show("指派失敗:" + ex.Message);
            }
        }
    }
}
