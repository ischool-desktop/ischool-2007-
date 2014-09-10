using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.PlugIn.ImportExport;
using SmartSchool.Customization.Data;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using System.Xml;

namespace SmartSchool.CourseRelated.RibbonBars.Import
{
    class ImportAttendCourse : ImportProcess
    {
        private AccessHelper _AccessHelper;

        private int _SchoolYear, _Semester;

        private List<CourseRecord> courseList;

        private Dictionary<SmartSchool.Customization.Data.StudentRecord, List<CourseRecord>> _StudentAttendCourse;

        public ImportAttendCourse()
        {
            this.Image = null;
            this.Title = "匯入學生修課";
            this.Group = "學生修課紀錄";
            this.PackageLimit = 3000;
            foreach ( string field in new string[] { "學年度", "學期","課程名稱","必選修","校部訂" } )
            {
                this.ImportableFields.Add(field);
            }
            foreach ( string field in new string[] { "學年度", "學期", "課程名稱" } )
            {
                this.RequiredFields.Add(field);
            }
        }

        protected override void OnBeginValidate(BeginValidateEventArgs args)
        {
            _AccessHelper = new AccessHelper();
            _StudentAttendCourse = new Dictionary<SmartSchool.Customization.Data.StudentRecord, List<CourseRecord>>();
            _SchoolYear = _Semester = -1;
        }

        protected override void OnValidateRow(RowDataValidatedEventArgs args)
        {
            string schoolYearString = args.Data["學年度"];
            string semesterString = args.Data["學期"];
            string courseName = args.Data["課程名稱"];

            int schoolYear, semester;
            CourseRecord course = null;
            //轉換學年度學期為數字
            #region 轉換、驗證學年度學期
            bool hasSemester = true;
            if ( !int.TryParse(schoolYearString, out schoolYear) )
            {
                args.ErrorFields.Add("學年度", "必須輸入數字"); 
                hasSemester &= false;
            }
            if ( !int.TryParse(semesterString, out semester) )
            {
                args.ErrorFields.Add("學期", "必須輸入數字");
                hasSemester &= false;
            }
            if ( !hasSemester ) return;
            if ( _SchoolYear == -1&&_Semester == -1 )
            {
                _SchoolYear = schoolYear;
                _Semester = semester;
                courseList = _AccessHelper.CourseHelper.GetAllCourse(_SchoolYear, _Semester);
            }
            else
            {
                if ( _SchoolYear != schoolYear || _Semester != semester )
                {
                    args.ErrorMessage = "學年度學期與其他紀錄不同。不同學期的修課紀錄必須分開匯入。";
                    return;
                }
            }
            #endregion
            //確認課程存在
            #region 確認課程存在
            bool hasCourse = false;
            foreach ( CourseRecord courseRec in courseList )
            {
                if ( courseRec.CourseName == courseName )
                {
                    course = courseRec;
                    hasCourse = true;
                    break;
                }
            }
            if ( !hasCourse )
            {
                args.ErrorFields.Add("課程名稱", "這個課程不存在");
                return;
            }
            SmartSchool.Customization.Data.StudentRecord studentRec = _AccessHelper.StudentHelper.GetStudents(args.Data.ID)[0];
            if ( !_StudentAttendCourse.ContainsKey(studentRec) )
                _StudentAttendCourse.Add(studentRec, new List<CourseRecord>());
            if ( _StudentAttendCourse[studentRec].Contains(course) )
            {
                args.ErrorMessage = "同學生修同一堂課的紀錄在同一檔案中不得重複出現。";
                return;
            }
            else
                _StudentAttendCourse[studentRec].Add(course);
            #endregion
            //驗證必選修
            #region 驗證必選修
            if ( args.SelectFields.Contains("必選修") )
            {
                if ( args.Data["必選修"] != "選修" && args.Data["必選修"] != "必修" && args.Data["必選修"] != "" )
                {
                    args.ErrorFields.Add("必選修", "允許值為\"\"或\"必修\"或\"選修\"。");
                }
            }
            #endregion
            //驗證校部訂
            #region 驗證校部訂
            if ( args.SelectFields.Contains("校部訂") )
            {
                if ( args.Data["校部訂"] != "部訂" && args.Data["校部訂"] != "校訂" && args.Data["校部訂"] != "" )
                {
                    args.ErrorFields.Add("校部訂", "允許值為\"\"或\"部訂\"或\"校訂\"。");
                }
            }
            #endregion
        }

        protected override void OnBeginImport()
        {
        }
        protected override void OnEndValidate()
        {
            MultiThreadWorker<SmartSchool.Customization.Data.StudentRecord> worker = new MultiThreadWorker<SmartSchool.Customization.Data.StudentRecord>();
            worker.MaxThreads = 3;
            worker.PackageSize = 150;
            worker.PackageWorker += new EventHandler<PackageWorkEventArgs<SmartSchool.Customization.Data.StudentRecord>>(worker_PackageWorker);
            worker.Run(_StudentAttendCourse.Keys);
        }

        void worker_PackageWorker(object sender, PackageWorkEventArgs<SmartSchool.Customization.Data.StudentRecord> e)
        {
            //取得學生已有的修課紀錄
            _AccessHelper.StudentHelper.FillAttendCourse(_SchoolYear, _Semester, e.List);
        }
        protected override void OnDataImport(DataImportEventArgs args)
        {
            MultiThreadWorker<RowData> worker = new MultiThreadWorker<RowData>();
            worker.MaxThreads = 3;
            worker.PackageSize = 350;
            worker.PackageWorker += new EventHandler<PackageWorkEventArgs<RowData>>(worker_PackageWorker);
            worker.Run(args.Items, args.ImportFields);
        }

        void worker_PackageWorker(object sender, PackageWorkEventArgs<RowData> e)
        {
            List<string> selectFields = (List<string>)e.Argument;
            //新增資料清單<資料,課程編號>
            Dictionary<RowData, string[]> insertList = new Dictionary<RowData, string[]>();
            //修改資料清單
            Dictionary<StudentAttendCourseRecord, RowData> updateList = new Dictionary<StudentAttendCourseRecord, RowData>();

            foreach ( RowData rowData in e.List )
            {
                #region 整理成新增或修改清單
                string courseName = rowData["課程名稱"];
                SmartSchool.Customization.Data.StudentRecord studentRec = _AccessHelper.StudentHelper.GetStudents(rowData.ID)[0];
                bool isInsert = true;
                foreach ( StudentAttendCourseRecord attendInfo in studentRec.AttendCourseList )
                {
                    if ( attendInfo.CourseName == courseName )
                    {
                        isInsert &= false;
                        if ( rowData.ContainsKey("必選修") || rowData.ContainsKey("校部訂") )
                        {
                            updateList.Add(attendInfo, rowData);
                        }
                    }
                }
                if ( isInsert )
                {
                    foreach ( CourseRecord course in _StudentAttendCourse[studentRec] )
                    {
                        if ( course.CourseName == courseName )
                        {
                            insertList.Add(rowData, new string[] { ""+course.CourseID, studentRec.StudentID });
                            break;
                        }
                    }
                }
                #endregion
            }
            #region 新增資料
            if ( insertList.Count > 0 )
            {
                DSXmlHelper helper = new DSXmlHelper("InsertSCAttend");
                foreach ( RowData row in insertList.Keys )
                {
                    helper.AddElement("Attend");
                    helper.AddElement("Attend", "RefCourseID", insertList[row][0]);
                    helper.AddElement("Attend", "RefStudentID", insertList[row][1]);
                    if ( selectFields.Contains("必選修") )
                        helper.AddElement("Attend", "IsRequired", InjectionRequired(row["必選修"]));
                    if ( selectFields.Contains("校部訂") )
                        helper.AddElement("Attend", "RequiredBy", row["校部訂"]);
                }
                SmartSchool.Feature.Course.AddCourse.AttendCourse(helper);
            }
            #endregion
            #region 修改資料
            if ( updateList.Count > 0 )
            {
                //DSXmlHelper helper = new DSXmlHelper("UpdateSCAttend");
                //foreach ( StudentAttendCourseRecord each in updateList .Keys)
                //{
                //    XmlElement attend = helper.AddElement("Attend");
                //    DSXmlHelper.AppendChild(attend, "<ID>" + each.Identity + "</ID>");
                //    DSXmlHelper.AppendChild(attend, "<IsRequired>" + each.IsRequired + "</IsRequired>");
                //    DSXmlHelper.AppendChild(attend, "<RequiredBy>" + each.RequiredBy + "</RequiredBy>");

                //    helper.AddElement(".", attend);
                //}
                //    SmartSchool.Feature.Course.EditCourse.UpdateAttend(helper);
            }
            #endregion
        }
        private string InjectionRequired(string required)
        {
            if ( required == "必" || required == "選" ||required=="")
                return required;
            else
            {
                if ( required == "必修" )
                    return "必";
                else if ( required == "選修" )
                    return "選";
                else
                    throw new ArgumentException("只能允許「必」或「選」，沒有此種選項：" + required);
            }
        }
    }
}