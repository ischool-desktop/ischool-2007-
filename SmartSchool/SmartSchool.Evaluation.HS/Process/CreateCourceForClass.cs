using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.Customization.Data;
using SmartSchool.Evaluation.GraduationPlan;
using SystemInformation = SmartSchool.Customization.Data.SystemInformation;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.Security;
using SmartSchool.Common;

namespace SmartSchool.Evaluation.Process
{
    public partial class CreateCourceForClass : UserControl
    {
        FeatureAccessControl createCourseButtonCtl;

        public CreateCourceForClass()
        {
            InitializeComponent();
            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonItem>.Instance["班級/教務作業"].Add(buttonItem1);

            SmartSchool.Broadcaster.Events.Items["班級/選取變更"].Handler += delegate
            {
                buttonItem1.Enabled = SmartSchool.ClassRelated.Class.Instance.SelectionClasses.Count > 0;
                createCourseButtonCtl.Inspect(buttonItem1);
            };

            //班級開課權限
            createCourseButtonCtl = new FeatureAccessControl("Button0365");
            createCourseButtonCtl.Inspect(buttonItem1);
        }


        #region 課程規劃表開課
        private void buttonItem3_Click(object sender, EventArgs e)
        {
            SelectSemesterForm form = new SelectSemesterForm();
            if (form.ShowDialog() != DialogResult.OK)
                return;
            
            if (MsgBox.Show("目前學期為\"" + SystemInformation.SchoolYear + "\"學年度第\"" + SystemInformation.Semester + "\"學期。\n新建課程將屬於此學期課程，\n如欲新建之課程為下學期課程，\n需先修改系統之學年度學期設定。\n\n確定為選取班級開課？", "新建課程", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                BackgroundWorker bkw = new BackgroundWorker();
                bkw.DoWork += new DoWorkEventHandler(bkw_DoWork);
                bkw.ProgressChanged += new ProgressChangedEventHandler(bkw_ProgressChanged);
                bkw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkw_RunWorkerCompleted);
                bkw.WorkerReportsProgress = true;
                bkw.RunWorkerAsync(form);
            }
        }

        void bkw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bkw = (BackgroundWorker)sender;
            SelectSemesterForm form = e.Argument as SelectSemesterForm;

            AccessHelper accessHelper = new AccessHelper();
            bkw.ReportProgress(1);
            double totleClass = accessHelper.ClassHelper.GetSelectedClass().Count;
            if ( totleClass <= 0 )
                totleClass = 0;
            double processedClass = 0;
            foreach ( ClassRecord classRec in accessHelper.ClassHelper.GetSelectedClass() )
            {
                #region 班級開課
                int gradeYear = 0;
                if ( !int.TryParse(classRec.GradeYear, out gradeYear) ) continue;
                //班級內每個學生的課程規劃表
                Dictionary<GraduationPlanInfo, List<StudentRecord>> graduations = new Dictionary<GraduationPlanInfo, List<StudentRecord>>();
                #region 整理班級內每個學生的課程規劃表
                foreach ( StudentRecord studentRec in classRec.Students )
                {
                    //取得學生的課程規劃表
                    GraduationPlanInfo info = GraduationPlan.GraduationPlan.Instance.GetStudentGraduationPlan(studentRec.StudentID);
                    if ( info != null )
                    {
                        if ( !graduations.ContainsKey(info) )
                            graduations.Add(info, new List<StudentRecord>());
                        graduations[info].Add(studentRec);
                    }
                }
                #endregion
                //所有課程規劃表中要開的課程
                Dictionary<string, GraduationPlanSubject> courseList = new Dictionary<string, GraduationPlanSubject>();
                //課程的科目
                Dictionary<string, string> subjectList = new Dictionary<string, string>();
                //課程的級別
                Dictionary<string, string> levelList = new Dictionary<string, string>();
                //有此課程的課程規劃表
                Dictionary<string, List<GraduationPlanInfo>> graduationList = new Dictionary<string, List<GraduationPlanInfo>>();
                #region 整裡所有要開的課程
                foreach ( GraduationPlanInfo gplan in graduations.Keys )
                {
                    foreach ( GraduationPlanSubject gplanSubject in gplan.SemesterSubjects(gradeYear, form.Semester) )
                    {
                        string key = gplanSubject.SubjectName.Trim() + "^_^" + gplanSubject.Level;
                        if ( !courseList.ContainsKey(key) )
                        {
                            //新增一個要開的課程
                            courseList.Add(key, gplanSubject);
                            subjectList.Add(key, gplanSubject.SubjectName.Trim());
                            levelList.Add(key, gplanSubject.Level);
                            graduationList.Add(key, new List<GraduationPlanInfo>());
                        }
                        graduationList[key].Add(gplan);
                    }
                }
                #endregion
                //本學期已開的課程
                Dictionary<string, CourseRecord> existSubject = new Dictionary<string, CourseRecord>();
                #region 整裡本學期已開的課程
                foreach ( CourseRecord courseRec in accessHelper.CourseHelper.GetClassCourse(form.SchoolYear, form.Semester, classRec) )
                {
                    string key = courseRec.Subject + "^_^" + courseRec.SubjectLevel;
                    if ( !existSubject.ContainsKey(key) )
                        existSubject.Add(key, courseRec);
                }
                #endregion
                #region 開課
                List<SmartSchool.Feature.Course.AddCourse.InsertCourse> newCourses = new List<SmartSchool.Feature.Course.AddCourse.InsertCourse>();
                foreach ( string key in courseList.Keys )
                {
                    //是原來沒有的課程
                    if ( !existSubject.ContainsKey(key) )
                    {
                        GraduationPlanSubject cinfo = courseList[key];
                        newCourses.Add(new SmartSchool.Feature.Course.AddCourse.InsertCourse(
                            classRec.ClassName + " " + cinfo.FullName,
                                cinfo.SubjectName.Trim(),
                                cinfo.Level,
                                classRec.ClassID,
                                form.SchoolYear.ToString(),
                                form.Semester.ToString(),
                                cinfo.Credit,
                                ( cinfo.NotIncludedInCredit ) ? "是" : "否",
                                ( cinfo.NotIncludedInCalc ) ? "是" : "否",
                                cinfo.Entry,
                                cinfo.Required == "必修" ? "必" : "選",
                                cinfo.RequiredBy
                            ));
                    }
                }
                if ( newCourses.Count > 0 )
                {
                    SmartSchool.Feature.Course.AddCourse.Insert(newCourses);
                    SmartSchool.Broadcaster.Events.Items["課程/新增"].Invoke();
                }
                #endregion
                #region 重新整理已開的課程
                existSubject.Clear();
                foreach ( CourseRecord courseRec in accessHelper.CourseHelper.GetClassCourse(form.SchoolYear, form.Semester, classRec) )
                {
                    string key = courseRec.Subject + "^_^" + courseRec.SubjectLevel;
                    if ( !existSubject.ContainsKey(key) )
                        existSubject.Add(key, courseRec);
                }
                //填入修課學生
                accessHelper.CourseHelper.FillStudentAttend(existSubject.Values);
                #endregion
                #region 加入學生修課
                DSXmlHelper insertSCAttendHelper = new DSXmlHelper("InsertSCAttend");
                bool addAttend = false;
                foreach ( StudentRecord studentRec in classRec.Students )
                {
                    if ( GraduationPlan.GraduationPlan.Instance.GetStudentGraduationPlan(studentRec.StudentID) != null )
                    {
                        foreach ( GraduationPlanSubject subject in GraduationPlan.GraduationPlan.Instance.GetStudentGraduationPlan(studentRec.StudentID).SemesterSubjects(gradeYear, form.Semester) )
                        {
                            string key = subject.SubjectName.Trim() + "^_^" + subject.Level;
                            bool found = false;
                            foreach ( StudentAttendCourseRecord attend in existSubject[key].StudentAttendList )
                            {
                                if ( attend.StudentID == studentRec.StudentID )
                                    found = true;
                            }
                            if ( !found )
                            {
                                XmlElement attend = insertSCAttendHelper.AddElement("Attend");
                                DSXmlHelper.AppendChild(attend, "<RefStudentID>" + studentRec.StudentID + "</RefStudentID>");
                                DSXmlHelper.AppendChild(attend, "<RefCourseID>" + existSubject[key].CourseID + "</RefCourseID>");

                                //insertSCAttendHelper.AddElement(".", attend);
                                addAttend = true;
                            }
                        }
                    }
                }
                if ( addAttend )
                    SmartSchool.Feature.Course.AddCourse.AttendCourse(insertSCAttendHelper);
                #endregion 
                #endregion
                //回報進度
                bkw.ReportProgress((int)( processedClass * 100d / totleClass ));
            }
        }

        void bkw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage("依課程規劃表開課中...", e.ProgressPercentage);
        }

        void bkw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ( e.Error == null )
            {
                SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage("班級開課完成。");
            }
            else
            {
                SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage("班級開課發生未預期的錯誤，開課或學生修課動作可能僅部分完成。");
                ExceptionHandler.BugReporter.ReportException(e.Error, false);
            }
        }
        #endregion

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            new SmartSchool.Evaluation.Process.Wizards.CreateClassCourse().ShowDialog();
        }
    }
}
