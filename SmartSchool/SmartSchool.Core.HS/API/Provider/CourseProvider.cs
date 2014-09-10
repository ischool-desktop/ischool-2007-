using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.CourseRelated;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.Customization.Data;

namespace SmartSchool.API.Provider
{
    internal class CourseProvider : Customization.Data.CourseInformationProvider
    {
        private System.Collections.Hashtable _CashePool;

        private AccessHelper _AccessHelper;

        private const int _PackageLimit = 250;

        private static List<T>[] SplitPackage<T>(List<T> list)
        {
            if ( list.Count > 0 )
            {
                int packageCount = ( list.Count / _PackageLimit + 1 );
                int packageSize = list.Count / packageCount + list.Count % packageCount;
                packageCount = 0;
                List<List<T>> packages = new List<List<T>>();
                List<T> packageCurrent = new List<T>();
                foreach ( T var in list )
                {
                    packageCurrent.Add(var);
                    packageCount++;
                    if ( packageCount == packageSize )
                    {
                        packageCount = 0;
                        packages.Add(packageCurrent);
                        packageCurrent = new List<T>();
                    }
                }
                if ( packageCount > 0 )
                    packages.Add(packageCurrent);
                return packages.ToArray();
            }
            else
                return new List<T>[0];
        }

        private List<T> GetList<T>(IEnumerable<T> items)
        {
            List<T> list = new List<T>();
            list.AddRange(items);
            return list;
        }

        #region CourseInformationProvider 成員

        public SmartSchool.Customization.Data.AccessHelper AccessHelper
        {
            get
            {
                return _AccessHelper;
            }
            set
            {
                _AccessHelper = value;
            }
        }

        public void FillField(string fieldName, IEnumerable<SmartSchool.Customization.Data.CourseRecord> courses)
        {
        }

        public void FillStudentAttend(IEnumerable<SmartSchool.Customization.Data.CourseRecord> courses)
        {
            //分批次處理
            foreach ( List<SmartSchool.Customization.Data.CourseRecord> courseList in SplitPackage<Customization.Data.CourseRecord>(GetList<SmartSchool.Customization.Data.CourseRecord>(courses)) )
            {
                #region 下載及填入學生修課資料
                Dictionary<int, SmartSchool.Customization.Data.CourseRecord> courseMapping = new Dictionary<int, SmartSchool.Customization.Data.CourseRecord>();
                foreach ( SmartSchool.Customization.Data.CourseRecord var in courseList )
                {
                    var.StudentAttendList.Clear();
                    if ( !courseMapping.ContainsKey(var.CourseID) )
                        courseMapping.Add(var.CourseID, var);
                }
                #region 取得修課資料
                DSXmlHelper helper = new DSXmlHelper("SelectRequest");
                helper.AddElement("Field");
                helper.AddElement("Field", "All");
                helper.AddElement("Condition");
                foreach ( SmartSchool.Customization.Data.CourseRecord cinfo in courses )
                {
                    helper.AddElement("Condition", "CourseID", "" + cinfo.CourseID);
                }
                helper.AddElement("Order");
                DSRequest dsreq = new DSRequest(helper);
                DSResponse rsp = Feature.Course.QueryCourse.GetSCAttend(new DSRequest(helper));
                #endregion
                foreach ( XmlElement scElement in rsp.GetContent().GetElements("Student") )
                {
                    helper = new DSXmlHelper(scElement);
                    Customization.Data.CourseRecord course;
                    Customization.Data.StudentRecord student = AccessHelper.StudentHelper.GetStudents(helper.GetText("RefStudentID")).Count > 0 ? AccessHelper.StudentHelper.GetStudents(helper.GetText("RefStudentID"))[0] : null;
                    //StudentRelated.BriefStudentData sinfo = StudentRelated.Student.Instance.Items[helper.GetText("RefStudentID")];
                    CourseRelated.CourseInformation cinfo = CourseRelated.CourseEntity.Instance.Items[helper.GetText("RefCourseID")];
                    if ( student != null && cinfo != null )
                    {
                        #region 產生修課紀錄
                        #region 抓課程
                        lock ( cinfo )
                        {
                            if ( CachePool.ContainsKey(cinfo) )
                            {
                                course = (Customization.Data.CourseRecord)CachePool[cinfo];
                            }
                            else
                            {
                                course = new CourseRecord(cinfo);
                                CachePool.Add(cinfo, course);
                            }
                        }
                        #endregion
                        //如果不是在校生就跳過不處理
                        if ( !student.IsNormalStudent )
                            continue;

                        decimal finalscore = 0;
                        int gradeyear = 0;
                        int.TryParse(helper.GetText("GradeYear"), out gradeyear);
                        bool? required = null;
                        string requiredby = null;
                        switch ( helper.GetText("IsRequired") )
                        {
                            case "必":
                                required = true;
                                break;
                            case "選":
                                required = false;
                                break;
                            default:
                                required = null;
                                break;
                        }
                        switch ( helper.GetText("RequiredBy") )
                        { 
                            case"部訂":
                            case "校訂":
                                requiredby = helper.GetText("RequiredBy");
                                break;
                            default:
                                requiredby = null;
                                break;
                        }
                        StudentAttendCourseRecord record = new StudentAttendCourseRecord(student, course, ( decimal.TryParse(helper.GetText("Score"), out finalscore) ? (decimal?)finalscore : null ), required, requiredby);
                        #endregion
                        //加到學生資料中
                        courseMapping[course.CourseID].StudentAttendList.Add(record);
                    }
                }
                #endregion
            }
        }

        public List<SmartSchool.Customization.Data.CourseRecord> GetClassCourse(int schoolyear, int semester, SmartSchool.Customization.Data.ClassRecord classrecord)
        {
            List<Customization.Data.CourseRecord> list = new List<SmartSchool.Customization.Data.CourseRecord>();
            CourseRelated.CourseEntity.Instance.EnsureCourse(schoolyear, semester);
            foreach ( CourseInformation var in CourseRelated.CourseEntity.Instance.Items[schoolyear, semester] )
            {
                int cid;
                if ( int.TryParse(classrecord.ClassID, out cid) )
                {
                    if ( var.ClassID == cid )
                    {
                        lock ( var )
                        {
                            if ( CachePool.ContainsKey(var) )
                            {
                                list.Add((CourseRecord)CachePool[var]);
                            }
                            else
                            {
                                CourseRecord newitem = new CourseRecord(var);
                                CachePool.Add(var, newitem);
                                list.Add(newitem);
                            }
                        }
                    }
                }
            }
            return list;
        }

        public List<SmartSchool.Customization.Data.CourseRecord> GetCourse(IEnumerable<string> identities)
        {
            List<Customization.Data.CourseRecord> list = new List<SmartSchool.Customization.Data.CourseRecord>();
            foreach ( string id in identities )
            {
                CourseInformation cinfo = CourseEntity.Instance.Items[id];
                if ( cinfo != null )
                {
                    lock ( cinfo )
                    {
                        if ( CachePool.ContainsKey(cinfo) )
                        {
                            list.Add((CourseRecord)CachePool[cinfo]);
                        }
                        else
                        {
                            CourseRecord newitem = new CourseRecord(cinfo);
                            CachePool.Add(cinfo, newitem);
                            list.Add(newitem);
                        }
                    }
                }
            }
            return list;
        }

        public List<SmartSchool.Customization.Data.CourseRecord> GetAllCourse(int schoolyear, int semester)
        {
            CourseEntity.Instance.EnsureCourse(schoolyear, semester);
            List<Customization.Data.CourseRecord> list = new List<SmartSchool.Customization.Data.CourseRecord>();
            foreach ( CourseInformation cinfo in CourseEntity.Instance.Items[schoolyear, semester] )
            {
                lock ( cinfo )
                {
                    if ( CachePool.ContainsKey(cinfo) )
                    {
                        list.Add((CourseRecord)CachePool[cinfo]);
                    }
                    else
                    {
                        CourseRecord newitem = new CourseRecord(cinfo);
                        CachePool.Add(cinfo, newitem);
                        list.Add(newitem);
                    }
                }
            }
            return list;
        }

        public List<SmartSchool.Customization.Data.CourseRecord> GetSelectedCourse()
        {
            List<Customization.Data.CourseRecord> list = new List<SmartSchool.Customization.Data.CourseRecord>();
            foreach ( CourseInformation cinfo in CourseEntity.Instance.SelectionCourse )
            {
                lock ( cinfo )
                {
                    if ( CachePool.ContainsKey(cinfo) )
                    {
                        list.Add((CourseRecord)CachePool[cinfo]);
                    }
                    else
                    {
                        CourseRecord newitem = new CourseRecord(cinfo);
                        CachePool.Add(cinfo, newitem);
                        list.Add(newitem);
                    }
                }
            }
            return list;
        }

        public List<SmartSchool.Customization.Data.CourseRecord> GetTeacherCourse(int schoolyear, int semester, SmartSchool.Customization.Data.TeacherRecord teacher)
        {
            List<Customization.Data.CourseRecord> list = new List<SmartSchool.Customization.Data.CourseRecord>();
            CourseRelated.CourseEntity.Instance.EnsureCourse(schoolyear, semester);
            foreach ( CourseInformation var in CourseRelated.CourseEntity.Instance.Items[schoolyear, semester] )
            {
                foreach ( CourseInformation.Teacher t in var.Teachers )
                {
                    int tid;
                    if ( int.TryParse(teacher.TeacherID, out tid) )
                    {
                        if ( t.TeacherID == tid )
                        {
                            lock ( var )
                            {
                                if ( CachePool.ContainsKey(var) )
                                {
                                    list.Add((CourseRecord)CachePool[var]);
                                }
                                else
                                {
                                    CourseRecord newitem = new CourseRecord(var);
                                    CachePool.Add(var, newitem);
                                    list.Add(newitem);
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }

        public System.Collections.Hashtable CachePool
        {
            get
            {
                return _CashePool;
            }
            set
            {
                _CashePool = value;
            }
        }

        public void FillExamScore(IEnumerable<SmartSchool.Customization.Data.CourseRecord> courses)
        {
            //分批次處理
            foreach ( List<SmartSchool.Customization.Data.CourseRecord> courseList in SplitPackage<Customization.Data.CourseRecord>(GetList<SmartSchool.Customization.Data.CourseRecord>(courses)) )
            {
                #region 下載及填入學生修課資料
                Dictionary<int, SmartSchool.Customization.Data.CourseRecord> courseMapping = new Dictionary<int, SmartSchool.Customization.Data.CourseRecord>();
                foreach ( SmartSchool.Customization.Data.CourseRecord var in courseList )
                {
                    var.ExamScoreList.Clear();
                    if ( !courseMapping.ContainsKey(var.CourseID) )
                        courseMapping.Add(var.CourseID, var);
                }
                #region 取得修課資料.
                List<string> idlist = new List<string>();
                foreach ( SmartSchool.Customization.Data.CourseRecord cinfo in courses )
                {
                    if ( !idlist.Contains("" + cinfo.CourseID) )
                        idlist.Add("" + cinfo.CourseID);
                }
                DSResponse rsp = Feature.Course.QueryCourse.GetSECTake(idlist.ToArray());
                #endregion
                foreach ( XmlElement scElement in rsp.GetContent().GetElements("Score") )
                {
                    DSXmlHelper helper = new DSXmlHelper(scElement);
                    Customization.Data.CourseRecord course;
                    Customization.Data.StudentRecord student = AccessHelper.StudentHelper.GetStudents(helper.GetText("RefStudentID")).Count > 0 ? AccessHelper.StudentHelper.GetStudents(helper.GetText("RefStudentID"))[0] : null;
                    //Customization.Data.StudentRecord student;
                    //StudentRelated.BriefStudentData sinfo = StudentRelated.Student.Instance.Items[helper.GetText("RefStudentID")];
                    CourseRelated.CourseInformation cinfo = CourseRelated.CourseEntity.Instance.Items[helper.GetText("RefCourseID")];
                    if ( student != null && cinfo != null )
                    {
                        #region 產生修課紀錄
                        #region 抓課程
                        lock ( cinfo )
                        {
                            if ( CachePool.ContainsKey(cinfo) )
                            {
                                course = (Customization.Data.CourseRecord)CachePool[cinfo];
                            }
                            else
                            {
                                course = new CourseRecord(cinfo);
                                CachePool.Add(cinfo, course);
                            }
                        }
                        #endregion
                        //如果不是在校生就跳過不處理
                        if ( !student.IsNormalStudent )
                            continue;

                        decimal tryParsedecimal = 0;
                        int gradeyear = 0;
                        bool required = false;
                        string requiredby = "";
                        int.TryParse(helper.GetText("GradeYear"), out gradeyear);
                        required = ( helper.GetText("IsRequired") == "必" );
                        requiredby = helper.GetText("RequiredBy");
                        decimal? aScore = ( decimal.TryParse(helper.GetText("AttendScore"), out tryParsedecimal) ? (decimal?)tryParsedecimal : null );

                        string examName = helper.GetText("ExamName");
                        string score = helper.GetText("Score");
                        decimal examScore ;
                        string specialCase = "";
                        if(decimal.TryParse(score, out tryParsedecimal))
                        {
                            examScore =tryParsedecimal;
                        }
                        else
                        {
                            examScore = 0;
                            specialCase = score;
                        }
                        ExamScore escore = new ExamScore(student, course, aScore, required, requiredby,examName,examScore,specialCase);
                         #endregion
                        //加到學生資料中
                        courseMapping[course.CourseID].ExamScoreList.Add(escore);
                    }
                }
                #endregion
            }
        }

        public void FillExam(IEnumerable<SmartSchool.Customization.Data.CourseRecord> courses)
        {
            //分批次處理
            foreach ( List<SmartSchool.Customization.Data.CourseRecord> courseList in SplitPackage<Customization.Data.CourseRecord>(GetList<SmartSchool.Customization.Data.CourseRecord>(courses)) )
            {
                List<string> idList = new List<string>();
                foreach ( SmartSchool.Customization.Data.CourseRecord c in courseList )
                {
                    if ( !idList.Contains("" + c.CourseID) )
                        idList.Add("" + c.CourseID);
                }
                DSXmlHelper helper=Feature.Course.QueryCourse.GetCourseExam(idList.ToArray()).GetContent();
                foreach ( SmartSchool.Customization.Data.CourseRecord var in courseList )
                {
                    var.ExamList.Clear();
                    foreach ( XmlElement element in helper.GetElements("Course[@ID='" + var.CourseID + "']/ExamName") )
                    {
                        var.ExamList.Add(element.InnerText);
                    }
                }
            }
        }

        public SmartSchool.Customization.Data.CourseRecord GetCourseByCourseName(int schoolYear, int semester, string courseName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region ICloneable 成員

        public object Clone()
        {
            return new CourseProvider();
        }

        #endregion

    }
}
