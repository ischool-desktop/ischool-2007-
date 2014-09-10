using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Feature.Score;
using System.Xml;
using SmartSchool.API.StudentExtension;
using SmartSchool.Customization.Data.StudentExtension;
using IntelliSchool.DSA30.Util;
using IStudentRecord = SmartSchool.Customization.Data.StudentRecord;
using SmartSchool.Feature.Score.Rating;
using SmartSchool.Customization.Data;

namespace SmartSchool.API.Provider
{
    public class StudentProvider : Customization.Data.StudentInformationProvider
    {
        private System.Collections.Hashtable _CashePool;

        private AccessHelper _AccessHelper;

        private const int _PackageLimit = 500;


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

        #region StudentInformationProvider 成員

        void Customization.Data.StudentInformationProvider.FillAttendCourse(int schoolYear, int semester, IEnumerable<SmartSchool.Customization.Data.StudentRecord> students)
        {
            ////先清除學生修課資訊
            //foreach ( SmartSchool.Customization.Data.StudentRecord var in students )
            //{
            //    var.AttendCourseList.Clear();
            //}
            ////分批次處理
            //foreach (List<SmartSchool.Customization.Data.StudentRecord> studentList in SplitPackage<Customization.Data.StudentRecord>(GetList<SmartSchool.Customization.Data.StudentRecord>(students)))
            //{
            //    #region 下載及填入學生修課資料
            //    //確保快取課程
            //    CouseRelated.CourseEntity.Instance.EnsureCourse(schoolYear, semester);
            //    Dictionary<string, SmartSchool.Customization.Data.StudentRecord> studentMapping = new Dictionary<string, SmartSchool.Customization.Data.StudentRecord>();
            //    foreach (SmartSchool.Customization.Data.StudentRecord var in studentList)
            //    {
            //        var.AttendCourseList.Clear();
            //        if (!studentMapping.ContainsKey(var.StudentID))
            //            studentMapping.Add(var.StudentID, var);
            //    }
            //    #region 取得修課資料
            //    DSXmlHelper helper = new DSXmlHelper("SelectRequest");
            //    helper.AddElement("Field");
            //    helper.AddElement("Field", "All");
            //    helper.AddElement("Condition");
            //    bool hasCourse = false;
            //    foreach (SmartSchool.CourseRelated.CourseInformation cinfo in CouseRelated.CourseEntity.Instance.Items[schoolYear, semester])
            //    {
            //        helper.AddElement("Condition", "CourseID", "" + cinfo.Identity);
            //        hasCourse = true;
            //    }
            //    //如果該學期本來就沒有開任何課程就不用抓了
            //    if ( !hasCourse )
            //        break;
            //    foreach (SmartSchool.Customization.Data.StudentRecord sinfo in studentList)
            //    {
            //        helper.AddElement("Condition", "StudentID", sinfo.StudentID);
            //    }
            //    helper.AddElement("Order");
            //    DSRequest dsreq = new DSRequest(helper);
            //    DSResponse rsp = Feature.Course.QueryCourse.GetSCAttend(new DSRequest(helper));
            //    #endregion
            //    foreach (XmlElement scElement in rsp.GetContent().GetElements("Student"))
            //    {
            //        helper = new DSXmlHelper(scElement);
            //        Customization.Data.CourseRecord course;
            //        Customization.Data.StudentRecord student;
            //        StudentRelated.BriefStudentData sinfo = StudentRelated.Student.Instance.Items[helper.GetText("RefStudentID")];
            //        CouseRelated.CourseInformation cinfo = CouseRelated.CourseEntity.Instance.Items[helper.GetText("RefCourseID")];
            //        if (sinfo != null && cinfo != null)
            //        {
            //            #region 產生修課紀錄
            //            #region 抓學生
            //            lock ( sinfo )
            //            {
            //                if ( CachePool.ContainsKey(sinfo) )
            //                {
            //                    student = (Customization.Data.StudentRecord)CachePool[sinfo];
            //                }
            //                else
            //                {
            //                    student = new StudentRecord(sinfo, CachePool);
            //                    CachePool.Add(sinfo, student);
            //                }
            //            }
            //            #endregion
            //            #region 抓課程
            //            lock (cinfo )
            //            {
            //                if ( CachePool.ContainsKey(cinfo) )
            //                {
            //                    course = (Customization.Data.CourseRecord)CachePool[cinfo];
            //                }
            //                else
            //                {
            //                    course = new CourseRecord(cinfo);
            //                    CachePool.Add(cinfo, course);
            //                }
            //            }
            //            #endregion
            //            decimal finalscore = 0;
            //            int gradeyear = 0;
            //            int.TryParse(helper.GetText("GradeYear"), out gradeyear);
            //            bool? required = null;
            //            string requiredby = null;
            //            switch ( helper.GetText("IsRequired") )
            //            {
            //                case "必":
            //                    required = true;
            //                    break;
            //                case "選":
            //                    required = false;
            //                    break;
            //                default:
            //                    required = null;
            //                    break;
            //            }
            //            switch ( helper.GetText("RequiredBy") )
            //            {
            //                case "部訂":
            //                case "校訂":
            //                    requiredby = helper.GetText("RequiredBy");
            //                    break;
            //                default:
            //                    requiredby = null;
            //                    break;
            //            }
            //            StudentAttendCourseRecord record = new StudentAttendCourseRecord(student, course, (decimal.TryParse(helper.GetText("Score"), out finalscore) ? (decimal?)finalscore : null), required, requiredby);
            //            #endregion
            //            //加到學生資料中
            //            studentMapping[student.StudentID].AttendCourseList.Add(record);
            //        }
            //    }
            //    #endregion
            //}
        }

        void Customization.Data.StudentInformationProvider.FillAttendance(IEnumerable<SmartSchool.Customization.Data.StudentRecord> students)
        {
            ( (Customization.Data.StudentInformationProvider)this ).FillAttendance(0, 0, students);
        }

        void Customization.Data.StudentInformationProvider.FillAttendance(int schoolYear, int semester, IEnumerable<SmartSchool.Customization.Data.StudentRecord> students)
        {
            #region 取得節次與缺曠對照表
            List<string> periodList = new List<string>();
            List<string> absenceList = new List<string>();

            //取得節次對照表
            foreach ( XmlElement var in SmartSchool.Feature.Basic.Config.GetPeriodList().GetContent().GetElements("Period") )
            {
                string name = var.GetAttribute("Name");
                if ( !periodList.Contains(name) )
                    periodList.Add(name);
            }

            //取得假別對照表
            foreach ( XmlElement var in SmartSchool.Feature.Basic.Config.GetAbsenceList().GetContent().GetElements("Absence") )
            {
                string name = var.GetAttribute("Name");
                if ( !absenceList.Contains(name) )
                    absenceList.Add(name);
            }
            #endregion

            //分批次處理
            foreach ( List<SmartSchool.Customization.Data.StudentRecord> studentList in SplitPackage<Customization.Data.StudentRecord>(GetList<SmartSchool.Customization.Data.StudentRecord>(students)) )
            {
                Dictionary<string, List<SmartSchool.Customization.Data.StudentExtension.AttendanceInfo>> studentAttendanceInfo = new Dictionary<string, List<AttendanceInfo>>();

                #region 取得編號
                string[] idList = new string[studentList.Count];
                for ( int i = 0 ; i < idList.Length ; i++ )
                {
                    idList[i] = studentList[i].StudentID;
                }
                if ( idList.Length == 0 )
                    continue;
                #endregion
                #region 取得缺曠資料
                DSXmlHelper helper = new DSXmlHelper("Request");
                helper.AddElement("Field");
                helper.AddElement("Field", "All");
                helper.AddElement("Condition");
                foreach ( string id in idList )
                {
                    helper.AddElement("Condition", "RefStudentID", id);
                }
                if ( schoolYear > 0 )
                    helper.AddElement("Condition", "SchoolYear", schoolYear.ToString());
                if ( semester > 0 )
                    helper.AddElement("Condition", "Semester", semester.ToString());
                helper.AddElement("Order");
                helper.AddElement("Order", "OccurDate", "desc");

                foreach ( XmlElement var in SmartSchool.Feature.Student.QueryAttendance.GetAttendance(new DSRequest(helper)).GetContent().GetElements("Attendance") )
                {
                    int schoolyear = 0;
                    int.TryParse(var.SelectSingleNode("SchoolYear").InnerText, out schoolyear);
                    int semester2 = 0;
                    int.TryParse(var.SelectSingleNode("Semester").InnerText, out semester2);
                    DateTime occurdate;
                    DateTime.TryParse(var.SelectSingleNode("OccurDate").InnerText, out occurdate);
                    string studentID = var.SelectSingleNode("RefStudentID").InnerText;

                    foreach ( XmlElement element in var.SelectNodes("Detail/Attendance/Period") )
                    {
                        string period = element.InnerText;
                        string periodtype = element.GetAttribute("AttendanceType");
                        string attendance = element.GetAttribute("AbsenceType");

                        if ( !periodList.Contains(period) || !absenceList.Contains(attendance) )
                            continue;

                        StudentExtension.Attendance attendanceInfo = new StudentExtension.Attendance(schoolyear, semester2, occurdate, period, periodtype, attendance, var);

                        if ( !studentAttendanceInfo.ContainsKey(studentID) )
                            studentAttendanceInfo.Add(studentID, new List<AttendanceInfo>());
                        studentAttendanceInfo[studentID].Add(attendanceInfo);
                    }
                }
                #endregion
                #region 填入缺曠資料
                foreach ( Customization.Data.StudentRecord student in studentList )
                {
                    student.AttendanceList.Clear();
                    if ( studentAttendanceInfo.ContainsKey(student.StudentID) )
                        student.AttendanceList.AddRange(studentAttendanceInfo[student.StudentID]);
                }
                #endregion
            }
        }

        void Customization.Data.StudentInformationProvider.FillContactInfo(IEnumerable<SmartSchool.Customization.Data.StudentRecord> students)
        {
            //分批次處理
            foreach ( List<SmartSchool.Customization.Data.StudentRecord> studentList in SplitPackage<Customization.Data.StudentRecord>(GetList<SmartSchool.Customization.Data.StudentRecord>(students)) )
            {
                Dictionary<string, Contact> studentContactInfo = new Dictionary<string, Contact>();
                //取得編號
                #region 取得編號
                string[] idList = new string[studentList.Count];
                for ( int i = 0 ; i < idList.Length ; i++ )
                {
                    idList[i] = studentList[i].StudentID;
                }
                if ( idList.Length == 0 )
                    continue;
                #endregion
                //抓家長資料
                #region 抓家長資料
                foreach ( XmlElement element in SmartSchool.Feature.QueryStudent.GetDetailList(new string[] { "ID", "ContactPhone", "PermanentPhone", "MailingAddress", "PermanentAddress" }, idList).GetContent().GetElements("Student") )
                {
                    string studentID = element.GetAttribute("ID");
                    DSXmlHelper helper = new DSXmlHelper(element);
                    string contactPhone = helper.GetText("ContactPhone");
                    string permanentPhone = helper.GetText("PermanentPhone");
                    XmlElement mailingaddress = helper.GetElement("MailingAddress/AddressList/Address");
                    XmlElement permanentAddress = helper.GetElement("PermanentAddress/AddressList/Address");

                    if ( !studentContactInfo.ContainsKey(studentID) )
                        studentContactInfo.Add(studentID, new Contact(contactPhone, permanentPhone, mailingaddress, permanentAddress));
                    else
                        studentContactInfo[studentID] = new Contact(contactPhone, permanentPhone, mailingaddress, permanentAddress);
                }
                #endregion
                //填入學生的家長
                #region 填入學生的家長
                foreach ( Customization.Data.StudentRecord student in studentList )
                {
                    if ( studentContactInfo.ContainsKey(student.StudentID) )
                        student.ContactInfo = studentContactInfo[student.StudentID];
                    else
                        student.ContactInfo = null;
                }
                #endregion
            }
        }

        void Customization.Data.StudentInformationProvider.FillExamScore(int schoolYear, int semester, IEnumerable<SmartSchool.Customization.Data.StudentRecord> students)
        {
            ////先清除學生修課資訊
            //foreach ( SmartSchool.Customization.Data.StudentRecord var in students )
            //{
            //    var.ExamScoreList.Clear();
            //}

            ////確保快取課程
            //CouseRelated.CourseEntity.Instance.EnsureCourse(schoolYear, semester);

            ////分批次處理
            //foreach ( List<SmartSchool.Customization.Data.StudentRecord> studentList in SplitPackage<Customization.Data.StudentRecord>(GetList<SmartSchool.Customization.Data.StudentRecord>(students)) )
            //{
            //    #region 下載及填入學生修課資料
            //    Dictionary<string, SmartSchool.Customization.Data.StudentRecord> studentMapping = new Dictionary<string, SmartSchool.Customization.Data.StudentRecord>();
            //    foreach ( SmartSchool.Customization.Data.StudentRecord var in studentList )
            //    {
            //        if ( !studentMapping.ContainsKey(var.StudentID) )
            //            studentMapping.Add(var.StudentID, var);
            //    }
            //    #region 取得修課資料
            //    List<string> courseid = new List<string>();
            //    List<string> studentid = new List<string>();
            //    bool hasCourse = false;
            //    foreach ( SmartSchool.CourseRelated.CourseInformation cinfo in CouseRelated.CourseEntity.Instance.Items[schoolYear, semester] )
            //    {
            //        if ( !courseid.Contains("" + cinfo.Identity) )
            //            courseid.Add("" + cinfo.Identity);
            //        hasCourse = true;
            //    }
            //    foreach ( StudentRecord sinfo in studentList )
            //    {
            //        if ( !studentid.Contains(sinfo.StudentID) )
            //            studentid.Add(sinfo.StudentID);
            //    }
            //    //如果該學期本來就沒有開任何課程就不用抓了
            //    if ( !hasCourse )
            //        break;
            //    DSResponse rsp = Feature.Course.QueryCourse.GetSECTake(courseid, studentid);
            //    #endregion
            //    foreach ( XmlElement scElement in rsp.GetContent().GetElements("Score") )
            //    {
            //        DSXmlHelper helper = new DSXmlHelper(scElement);
            //        Customization.Data.CourseRecord course;
            //        Customization.Data.StudentRecord student;
            //        StudentRelated.BriefStudentData sinfo = StudentRelated.Student.Instance.Items[helper.GetText("RefStudentID")];
            //        CouseRelated.CourseInformation cinfo = CouseRelated.CourseEntity.Instance.Items[helper.GetText("RefCourseID")];
            //        if ( sinfo != null && cinfo != null )
            //        {
            //            #region 產生修課紀錄
            //            #region 抓學生
            //            lock ( sinfo )
            //            {
            //                if ( CachePool.ContainsKey(sinfo) )
            //                {
            //                    student = (Customization.Data.StudentRecord)CachePool[sinfo];
            //                }
            //                else
            //                {
            //                    student = new StudentRecord(sinfo, CachePool);
            //                    CachePool.Add(sinfo, student);
            //                }
            //            }
            //            #endregion
            //            #region 抓課程
            //            lock ( cinfo )
            //            {
            //                if ( CachePool.ContainsKey(cinfo) )
            //                {
            //                    course = (Customization.Data.CourseRecord)CachePool[cinfo];
            //                }
            //                else
            //                {
            //                    course = new CourseRecord(cinfo);
            //                    CachePool.Add(cinfo, course);
            //                }
            //            }
            //            #endregion

            //            decimal tryParsedecimal = 0;
            //            int gradeyear = 0;
            //            bool required = false;
            //            string requiredby = "";
            //            int.TryParse(helper.GetText("GradeYear"), out gradeyear);
            //            required = ( helper.GetText("IsRequired") == "必" );
            //            requiredby = helper.GetText("RequiredBy");
            //            decimal? aScore = ( decimal.TryParse(helper.GetText("AttendScore"), out tryParsedecimal) ? (decimal?)tryParsedecimal : null );

            //            string examName = helper.GetText("ExamName");
            //            string score = helper.GetText("Score");
            //            decimal examScore;
            //            string specialCase = "";
            //            if ( decimal.TryParse(score, out tryParsedecimal) )
            //            {
            //                examScore = tryParsedecimal;
            //            }
            //            else
            //            {
            //                examScore = 0;
            //                specialCase = score;
            //            }
            //            ExamScore escore = new ExamScore(student, course, aScore, required, requiredby, examName, examScore, specialCase);
            //            #endregion
            //            //加到學生資料中
            //            studentMapping[student.StudentID].ExamScoreList.Add(escore);
            //        }
            //    }
            //    #endregion
            //}
        }

        void Customization.Data.StudentInformationProvider.FillParentInfo(IEnumerable<SmartSchool.Customization.Data.StudentRecord> students)
        {
            //分批次處理
            foreach ( List<SmartSchool.Customization.Data.StudentRecord> studentList in SplitPackage<Customization.Data.StudentRecord>(GetList<SmartSchool.Customization.Data.StudentRecord>(students)) )
            {
                Dictionary<string, ParentInfo> studentParentInfo = new Dictionary<string, ParentInfo>();
                //取得編號
                #region 取得編號
                string[] idList = new string[studentList.Count];
                for ( int i = 0 ; i < idList.Length ; i++ )
                {
                    idList[i] = studentList[i].StudentID;
                }
                if ( idList.Length == 0 )
                    continue;
                #endregion
                //抓家長資料
                #region 抓家長資料
                foreach ( XmlElement element in SmartSchool.Feature.QueryStudent.GetDetailList(new string[] { "ID", "FatherName", "FatherLiving", "MotherName", "MotherLiving", "CustodianName", "CustodianRelationship" }, idList).GetContent().GetElements("Student") )
                {
                    string studentID = element.GetAttribute("ID");
                    string fatherName = element.SelectSingleNode("FatherName").InnerText;
                    string motherName = element.SelectSingleNode("MotherName").InnerText;
                    string custodianName = element.SelectSingleNode("CustodianName").InnerText;
                    string custodianRelationship = element.SelectSingleNode("CustodianRelationship").InnerText;
                    bool fatherLiving = ( element.SelectSingleNode("FatherLiving").InnerText == "殁" );
                    bool motherLiving = ( element.SelectSingleNode("MotherLiving").InnerText == "殁" );

                    if ( !studentParentInfo.ContainsKey(studentID) )
                        studentParentInfo.Add(studentID, new Parent(custodianName, custodianRelationship, fatherName, motherName, fatherLiving, motherLiving));
                    else
                        studentParentInfo[studentID] = new Parent(custodianName, custodianRelationship, fatherName, motherName, fatherLiving, motherLiving);
                }
                #endregion
                //填入學生的家長
                #region 填入學生的家長
                foreach ( Customization.Data.StudentRecord student in studentList )
                {
                    if ( studentParentInfo.ContainsKey(student.StudentID) )
                        student.ParentInfo = studentParentInfo[student.StudentID];
                    else
                        student.ParentInfo = null;
                }
                #endregion
            }
        }

        void Customization.Data.StudentInformationProvider.FillReward(IEnumerable<SmartSchool.Customization.Data.StudentRecord> students)
        {
            ( (Customization.Data.StudentInformationProvider)this ).FillReward(0, 0, students);
        }

        void Customization.Data.StudentInformationProvider.FillReward(int schoolYear, int semester, IEnumerable<SmartSchool.Customization.Data.StudentRecord> students)
        {
            //分批次處理
            foreach ( List<SmartSchool.Customization.Data.StudentRecord> studentList in SplitPackage<Customization.Data.StudentRecord>(GetList<SmartSchool.Customization.Data.StudentRecord>(students)) )
            {
                Dictionary<string, List<SmartSchool.Customization.Data.StudentExtension.RewardInfo>> studentRewardInfo = new Dictionary<string, List<RewardInfo>>();

                #region 取得編號
                string[] idList = new string[studentList.Count];
                for ( int i = 0 ; i < idList.Length ; i++ )
                {
                    idList[i] = studentList[i].StudentID;
                }
                if ( idList.Length == 0 )
                    continue;
                #endregion
                #region 取得獎懲資料
                DSXmlHelper helper = new DSXmlHelper("Request");
                helper.AddElement("Field");
                helper.AddElement("Field", "All");
                helper.AddElement("Condition");
                foreach ( string id in idList )
                {
                    helper.AddElement("Condition", "RefStudentID", id);
                }
                if ( schoolYear > 0 )
                    helper.AddElement("Condition", "SchoolYear", schoolYear.ToString());
                if ( semester > 0 )
                    helper.AddElement("Condition", "Semester", semester.ToString());
                helper.AddElement("Order");
                helper.AddElement("Order", "OccurDate", "asc");
                foreach ( XmlElement var in SmartSchool.Feature.Student.QueryDiscipline.GetDiscipline(new DSRequest(helper)).GetContent().GetElements("Discipline") )
                {
                    int schoolyear = 0;
                    int.TryParse(var.SelectSingleNode("SchoolYear").InnerText, out schoolyear);
                    int semester2 = 0;
                    int.TryParse(var.SelectSingleNode("Semester").InnerText, out semester2);
                    DateTime occurdate;
                    DateTime.TryParse(var.SelectSingleNode("OccurDate").InnerText, out occurdate);
                    string studentID = var.SelectSingleNode("RefStudentID").InnerText;

                    string occurplace = "";
                    string occurreason = var.SelectSingleNode("Reason").InnerText;

                    int awardA = 0;
                    int awardB = 0;
                    int awardC = 0;
                    int faultA = 0;
                    int faultB = 0;
                    int faultC = 0;
                    bool cleared = false;
                    DateTime cleardate = DateTime.Today;
                    string clearreason = "";
                    bool ultimateAdmonition = false;

                    DSXmlHelper helper2 = new DSXmlHelper(var);
                    switch ( var.SelectSingleNode("MeritFlag").InnerText )
                    {
                        case "1":
                            int.TryParse(helper2.GetText("Detail/Discipline/Merit/@A"), out awardA);
                            int.TryParse(helper2.GetText("Detail/Discipline/Merit/@B"), out awardB);
                            int.TryParse(helper2.GetText("Detail/Discipline/Merit/@C"), out awardC);
                            break;
                        case "0":
                            int.TryParse(helper2.GetText("Detail/Discipline/Demerit/@A"), out faultA);
                            int.TryParse(helper2.GetText("Detail/Discipline/Demerit/@B"), out faultB);
                            int.TryParse(helper2.GetText("Detail/Discipline/Demerit/@C"), out faultC);
                            cleared = ( helper2.GetText("Detail/Discipline/Demerit/@Cleared") == "是" ) ? true : false;
                            DateTime.TryParse(helper2.GetText("Detail/Discipline/Demerit/@ClearDate"), out cleardate);
                            clearreason = helper2.GetText("Detail/Discipline/Demerit/@ClearReason");
                            break;
                        case "2":
                            ultimateAdmonition = true;
                            break;
                        default:
                            break;
                    }

                    StudentExtension.Reward rewardInfo = new Reward(schoolyear, semester2, occurdate, occurplace, occurreason, new int[] { awardA, awardB, awardC }, new int[] { faultA, faultB, faultC }, cleared, cleardate, clearreason, ultimateAdmonition, var);

                    if ( !studentRewardInfo.ContainsKey(studentID) )
                        studentRewardInfo.Add(studentID, new List<RewardInfo>());
                    studentRewardInfo[studentID].Add(rewardInfo);
                }
                #endregion
                #region 填入獎懲資料
                foreach ( Customization.Data.StudentRecord student in studentList )
                {
                    student.RewardList.Clear();
                    if ( studentRewardInfo.ContainsKey(student.StudentID) )
                        student.RewardList.AddRange(studentRewardInfo[student.StudentID]);
                }
                #endregion
            }
        }

        void Customization.Data.StudentInformationProvider.FillSchoolYearEntryScore(IEnumerable<SmartSchool.Customization.Data.StudentRecord> students, bool filterRepeat)
        {
            //分批次處理
            foreach ( List<SmartSchool.Customization.Data.StudentRecord> studentList in SplitPackage<Customization.Data.StudentRecord>(GetList<SmartSchool.Customization.Data.StudentRecord>(students)) )
            {
                Dictionary<string, List<Customization.Data.StudentExtension.SchoolYearEntryScoreInfo>> scoreDictionary = new Dictionary<string, List<Customization.Data.StudentExtension.SchoolYearEntryScoreInfo>>();
                //<學生,<學年度,<分項群組,編號>>>
                Dictionary<string, Dictionary<int, Dictionary<string, string>>> scoreIDDictionary = new Dictionary<string, Dictionary<int, Dictionary<string, string>>>();
                #region 填入分項成績資料
                //取得編號
                string[] idList = new string[studentList.Count];
                for ( int i = 0 ; i < idList.Length ; i++ )
                {
                    idList[i] = studentList[i].StudentID;
                }
                if ( idList.Length == 0 )
                    continue;
                //抓成績資料
                foreach ( XmlElement var in QueryScore.GetSchoolYearEntryScore(idList).GetContent().GetElements("SchoolYearEntryScore") )
                {
                    int schoolyear = 0;
                    int.TryParse(var.SelectSingleNode("SchoolYear").InnerText, out schoolyear);
                    int gradeyear = 0;
                    int.TryParse(var.SelectSingleNode("GradeYear").InnerText, out gradeyear);
                    string id = var.GetAttribute("ID");
                    string entryGroup = var.SelectSingleNode("EntryGroup").InnerText;
                    string studentID = var.SelectSingleNode("RefStudentId").InnerText;

                    if ( !scoreIDDictionary.ContainsKey(studentID) )
                        scoreIDDictionary.Add(studentID, new Dictionary<int, Dictionary<string, string>>());
                    if ( !scoreIDDictionary[studentID].ContainsKey(schoolyear) )
                        scoreIDDictionary[studentID].Add(schoolyear, new Dictionary<string, string>());
                    if ( !scoreIDDictionary[studentID][schoolyear].ContainsKey(entryGroup) )
                        scoreIDDictionary[studentID][schoolyear].Add(entryGroup, id);

                    foreach ( XmlNode scoreNode in var.SelectNodes("ScoreInfo/SchoolYearEntryScore/Entry") )
                    {
                        XmlElement element = (XmlElement)scoreNode;
                        string entry = element.GetAttribute("分項");
                        decimal score;
                        if ( decimal.TryParse(element.GetAttribute("成績"), out score) )
                        {
                            StudentExtension.SchoolYearEntryScore scoreInfo = new SmartSchool.API.StudentExtension.SchoolYearEntryScore(entry, gradeyear, schoolyear, score, element);
                            if ( !scoreDictionary.ContainsKey(studentID) )
                                scoreDictionary.Add(studentID, new List<Customization.Data.StudentExtension.SchoolYearEntryScoreInfo>());
                            scoreDictionary[studentID].Add(scoreInfo);
                        }
                    }
                }
                #endregion
                #region 過濾重讀成績
                if ( filterRepeat )
                {
                    foreach ( Customization.Data.StudentRecord student in studentList )
                    {
                        if ( scoreDictionary.ContainsKey(student.StudentID) )
                        {
                            List<Customization.Data.StudentExtension.SchoolYearEntryScoreInfo> scoreList = scoreDictionary[student.StudentID];

                            Dictionary<int, int> ApplySchoolYear = new Dictionary<int, int>();
                            //先將此學生此時的學年度學期資料加入
                            int gradeYear = 0;
                            if ( student.RefClass != null && int.TryParse(student.RefClass.GradeYear, out gradeYear) )
                            {
                                ApplySchoolYear.Add(gradeYear, CurrentUser.Instance.SchoolYear);
                            }
                            else
                                gradeYear = int.MaxValue;

                            //先掃一遍抓出每個年級最高的學年度
                            foreach ( SchoolYearEntryScore scoreInfo in scoreList )
                            {
                                //成績年級比現在大的不理會
                                if ( scoreInfo.GradeYear <= gradeYear )
                                {
                                    if ( !ApplySchoolYear.ContainsKey(scoreInfo.GradeYear) )
                                        ApplySchoolYear.Add(scoreInfo.GradeYear, scoreInfo.SchoolYear);
                                    if ( scoreInfo.SchoolYear > ApplySchoolYear[scoreInfo.GradeYear] )
                                        ApplySchoolYear[scoreInfo.GradeYear] = scoreInfo.SchoolYear;
                                }
                            }
                            //如果成績資料的年級學年度不在清單中就移掉
                            List<Customization.Data.StudentExtension.SchoolYearEntryScoreInfo> removeList = new List<Customization.Data.StudentExtension.SchoolYearEntryScoreInfo>();
                            foreach ( Customization.Data.StudentExtension.SchoolYearEntryScoreInfo scoreInfo in scoreList )
                            {
                                //成績年級比現在大或成績的學年度部是最新的
                                if ( !ApplySchoolYear.ContainsKey(scoreInfo.GradeYear) || ApplySchoolYear[scoreInfo.GradeYear] != scoreInfo.SchoolYear )
                                    removeList.Add(scoreInfo);
                            }
                            foreach ( SchoolYearEntryScore scoreInfo in removeList )
                            {
                                scoreList.Remove(scoreInfo);
                            }
                        }
                    }
                }
                #endregion
                #region 將成績填入學生資料內
                foreach ( Customization.Data.StudentRecord student in studentList )
                {
                    //填入成績
                    student.SchoolYearEntryScoreList.Clear();
                    if ( scoreDictionary.ContainsKey(student.StudentID) )
                        student.SchoolYearEntryScoreList.AddRange(scoreDictionary[student.StudentID]);
                    //填入成績編號
                    if ( student.Fields.ContainsKey("SchoolYearEntryScoreID") )
                        student.Fields.Remove("SchoolYearEntryScoreID");
                    if ( scoreIDDictionary.ContainsKey(student.StudentID) )
                        student.Fields.Add("SchoolYearEntryScoreID", scoreIDDictionary[student.StudentID]);
                    else
                        student.Fields.Add("SchoolYearEntryScoreID", new Dictionary<int, Dictionary<string, string>>());
                }
                #endregion
            }
        }

        void Customization.Data.StudentInformationProvider.FillSchoolYearSubjectScore(IEnumerable<SmartSchool.Customization.Data.StudentRecord> students, bool filterRepeat)
        {
            //分批次處理
            foreach ( List<SmartSchool.Customization.Data.StudentRecord> studentList in SplitPackage<Customization.Data.StudentRecord>(GetList<SmartSchool.Customization.Data.StudentRecord>(students)) )
            {
                Dictionary<string, List<Customization.Data.StudentExtension.SchoolYearSubjectScoreInfo>> scoreDictionary = new Dictionary<string, List<Customization.Data.StudentExtension.SchoolYearSubjectScoreInfo>>();
                //<學生,<學年度,編號>>
                Dictionary<string, Dictionary<int, string>> scoreIDDictionary = new Dictionary<string, Dictionary<int, string>>();
                #region 填入分項成績資料
                //取得編號
                string[] idList = new string[studentList.Count];
                for ( int i = 0 ; i < idList.Length ; i++ )
                {
                    idList[i] = studentList[i].StudentID;
                }
                if ( idList.Length == 0 )
                    continue;
                //抓成績資料
                foreach ( XmlElement var in QueryScore.GetSchoolYearSubjectScore(idList).GetContent().GetElements("SchoolYearSubjectScore") )
                {
                    int schoolyear = 0;
                    int.TryParse(var.SelectSingleNode("SchoolYear").InnerText, out schoolyear);
                    int gradeyear = 0;
                    int.TryParse(var.SelectSingleNode("GradeYear").InnerText, out gradeyear);
                    string id = var.GetAttribute("ID");
                    string studentID = var.SelectSingleNode("RefStudentId").InnerText;

                    if ( !scoreIDDictionary.ContainsKey(studentID) )
                        scoreIDDictionary.Add(studentID, new Dictionary<int, string>());
                    if ( !scoreIDDictionary[studentID].ContainsKey(schoolyear) )
                        scoreIDDictionary[studentID].Add(schoolyear, id);

                    foreach ( XmlNode scoreNode in var.SelectNodes("ScoreInfo/SchoolYearSubjectScore/Subject") )
                    {
                        XmlElement element = (XmlElement)scoreNode;
                        string subject = element.GetAttribute("科目");
                        decimal score;
                        if ( decimal.TryParse(element.GetAttribute("學年成績"), out score) )
                        {
                            StudentExtension.SchoolYearSubjectScore scoreInfo = new SmartSchool.API.StudentExtension.SchoolYearSubjectScore(schoolyear, subject, score, gradeyear, element);
                            if ( !scoreDictionary.ContainsKey(studentID) )
                                scoreDictionary.Add(studentID, new List<Customization.Data.StudentExtension.SchoolYearSubjectScoreInfo>());
                            scoreDictionary[studentID].Add(scoreInfo);
                        }
                    }
                }
                #endregion
                #region 過濾重讀成績
                if ( filterRepeat )
                {
                    foreach ( Customization.Data.StudentRecord student in studentList )
                    {
                        if ( scoreDictionary.ContainsKey(student.StudentID) )
                        {
                            List<Customization.Data.StudentExtension.SchoolYearSubjectScoreInfo> scoreList = scoreDictionary[student.StudentID];

                            Dictionary<int, int> ApplySchoolYear = new Dictionary<int, int>();
                            //先將此學生此時的學年度學期資料加入
                            int gradeYear = 0;
                            if ( student.RefClass != null && int.TryParse(student.RefClass.GradeYear, out gradeYear) )
                            {
                                ApplySchoolYear.Add(gradeYear, CurrentUser.Instance.SchoolYear);
                            }
                            else
                                gradeYear = int.MaxValue;

                            //先掃一遍抓出每個年級最高的學年度
                            foreach ( SchoolYearSubjectScoreInfo scoreInfo in scoreList )
                            {

                                //成績年級比現在大的不理會
                                if ( scoreInfo.GradeYear <= gradeYear )
                                {
                                    if ( !ApplySchoolYear.ContainsKey(scoreInfo.GradeYear) )
                                        ApplySchoolYear.Add(scoreInfo.GradeYear, scoreInfo.SchoolYear);
                                    if ( scoreInfo.SchoolYear > ApplySchoolYear[scoreInfo.GradeYear] )
                                        ApplySchoolYear[scoreInfo.GradeYear] = scoreInfo.SchoolYear;
                                }
                            }
                            //如果成績資料的年級學年度不在清單中就移掉
                            List<Customization.Data.StudentExtension.SchoolYearSubjectScoreInfo> removeList = new List<Customization.Data.StudentExtension.SchoolYearSubjectScoreInfo>();
                            foreach ( Customization.Data.StudentExtension.SchoolYearSubjectScoreInfo scoreInfo in scoreList )
                            {
                                //成績年級比現在大或成績的學年度部是最新的
                                if ( !ApplySchoolYear.ContainsKey(scoreInfo.GradeYear) || ApplySchoolYear[scoreInfo.GradeYear] != scoreInfo.SchoolYear )
                                    removeList.Add(scoreInfo);
                            }
                            foreach ( SchoolYearSubjectScoreInfo scoreInfo in removeList )
                            {
                                scoreList.Remove(scoreInfo);
                            }
                        }
                    }
                }
                #endregion
                #region 將成績填入學生資料內
                foreach ( Customization.Data.StudentRecord student in studentList )
                {
                    student.SchoolYearSubjectScoreList.Clear();
                    if ( scoreDictionary.ContainsKey(student.StudentID) )
                        student.SchoolYearSubjectScoreList.AddRange(scoreDictionary[student.StudentID]);
                    //填入成績編號
                    if ( student.Fields.ContainsKey("SchoolYearSubjectScoreID") )
                        student.Fields.Remove("SchoolYearSubjectScoreID");
                    if ( scoreIDDictionary.ContainsKey(student.StudentID) )
                        student.Fields.Add("SchoolYearSubjectScoreID", scoreIDDictionary[student.StudentID]);
                    else
                        student.Fields.Add("SchoolYearSubjectScoreID", new Dictionary<int, string>());
                }
                #endregion
            }
        }

        void Customization.Data.StudentInformationProvider.FillSemesterEntryScore(IEnumerable<SmartSchool.Customization.Data.StudentRecord> students, bool filterRepeat)
        {
            //分批次處理
            foreach ( List<SmartSchool.Customization.Data.StudentRecord> studentList in SplitPackage<Customization.Data.StudentRecord>(GetList<SmartSchool.Customization.Data.StudentRecord>(students)) )
            {
                Dictionary<string, List<Customization.Data.StudentExtension.SemesterEntryScoreInfo>> scoreDictionary = new Dictionary<string, List<Customization.Data.StudentExtension.SemesterEntryScoreInfo>>();
                //<學年度<學期<分類群組,編號>>>
                Dictionary<string, Dictionary<int, Dictionary<int, Dictionary<string, string>>>> scoreIDDictionary = new Dictionary<string, Dictionary<int, Dictionary<int, Dictionary<string, string>>>>();
                #region 填入分項成績資料
                //取得編號
                string[] idList = new string[studentList.Count];
                for ( int i = 0 ; i < idList.Length ; i++ )
                {
                    idList[i] = studentList[i].StudentID;
                }
                if ( idList.Length == 0 )
                    continue;
                //抓成績資料
                foreach ( XmlElement var in QueryScore.GetSemesterEntryScore(idList).GetContent().GetElements("SemesterEntryScore") )
                {
                    int schoolyear = 0;
                    int.TryParse(var.SelectSingleNode("SchoolYear").InnerText, out schoolyear);
                    int semester = 0;
                    int.TryParse(var.SelectSingleNode("Semester").InnerText, out semester);
                    int gradeyear = 0;
                    int.TryParse(var.SelectSingleNode("GradeYear").InnerText, out gradeyear);
                    string id = var.GetAttribute("ID");
                    string entryGroup = var.SelectSingleNode("EntryGroup").InnerText;
                    string studentID = var.SelectSingleNode("RefStudentId").InnerText;

                    if ( !scoreIDDictionary.ContainsKey(studentID) )
                        scoreIDDictionary.Add(studentID, new Dictionary<int, Dictionary<int, Dictionary<string, string>>>());
                    if ( !scoreIDDictionary[studentID].ContainsKey(schoolyear) )
                        scoreIDDictionary[studentID].Add(schoolyear, new Dictionary<int, Dictionary<string, string>>());
                    if ( !scoreIDDictionary[studentID][schoolyear].ContainsKey(semester) )
                        scoreIDDictionary[studentID][schoolyear].Add(semester, new Dictionary<string, string>());
                    if ( !scoreIDDictionary[studentID][schoolyear][semester].ContainsKey(entryGroup) )
                        scoreIDDictionary[studentID][schoolyear][semester].Add(entryGroup, id);

                    foreach ( XmlNode scoreNode in var.SelectNodes("ScoreInfo/SemesterEntryScore/Entry") )
                    {
                        XmlElement element = (XmlElement)scoreNode;
                        string entry = element.GetAttribute("分項");
                        decimal score;
                        if ( decimal.TryParse(element.GetAttribute("成績"), out score) )
                        {
                            StudentExtension.SemesterEntryScore scoreInfo = new SmartSchool.API.StudentExtension.SemesterEntryScore(entry, gradeyear, schoolyear, semester, score, element);
                            if ( !scoreDictionary.ContainsKey(studentID) )
                                scoreDictionary.Add(studentID, new List<Customization.Data.StudentExtension.SemesterEntryScoreInfo>());
                            scoreDictionary[studentID].Add(scoreInfo);
                        }
                    }
                }
                #endregion
                #region 過濾重讀成績
                if ( filterRepeat )
                {
                    foreach ( Customization.Data.StudentRecord student in studentList )
                    {
                        if ( scoreDictionary.ContainsKey(student.StudentID) )
                        {
                            List<Customization.Data.StudentExtension.SemesterEntryScoreInfo> scoreList = scoreDictionary[student.StudentID];

                            Dictionary<int, Dictionary<int, int>> ApplySemesterSchoolYear = new Dictionary<int, Dictionary<int, int>>();
                            //先將此學生此時的學年度學期資料加入
                            int gradeYear = 0;
                            if ( student.RefClass != null && int.TryParse(student.RefClass.GradeYear, out gradeYear) )
                            {
                                ApplySemesterSchoolYear.Add(gradeYear, new Dictionary<int, int>());
                                ApplySemesterSchoolYear[gradeYear].Add(CurrentUser.Instance.Semester, CurrentUser.Instance.SchoolYear);
                            }
                            else
                                gradeYear = int.MaxValue;

                            //先掃一遍抓出每個年級最高的學年度
                            foreach ( SemesterEntryScoreInfo scoreInfo in scoreList )
                            {
                                //如果成績年級學期比現在大就不理會
                                if ( scoreInfo.GradeYear < gradeYear || ( scoreInfo.GradeYear == gradeYear && scoreInfo.Semester <= SmartSchool.CurrentUser.Instance.Semester ) )
                                {
                                    if ( !ApplySemesterSchoolYear.ContainsKey(scoreInfo.GradeYear) )
                                        ApplySemesterSchoolYear.Add(scoreInfo.GradeYear, new Dictionary<int, int>());
                                    if ( !ApplySemesterSchoolYear[scoreInfo.GradeYear].ContainsKey(scoreInfo.Semester) )
                                        ApplySemesterSchoolYear[scoreInfo.GradeYear].Add(scoreInfo.Semester, scoreInfo.SchoolYear);
                                    if ( scoreInfo.SchoolYear > ApplySemesterSchoolYear[scoreInfo.GradeYear][scoreInfo.Semester] )
                                        ApplySemesterSchoolYear[scoreInfo.GradeYear][scoreInfo.Semester] = scoreInfo.SchoolYear;
                                }
                            }
                            //如果成績資料的年級學年度不在清單中就移掉
                            List<Customization.Data.StudentExtension.SemesterEntryScoreInfo> removeList = new List<Customization.Data.StudentExtension.SemesterEntryScoreInfo>();
                            foreach ( Customization.Data.StudentExtension.SemesterEntryScoreInfo scoreInfo in scoreList )
                            {
                                //成績年級比現在大或成績的學年度部是最新的
                                if ( !ApplySemesterSchoolYear.ContainsKey(scoreInfo.GradeYear) || !ApplySemesterSchoolYear[scoreInfo.GradeYear].ContainsKey(scoreInfo.Semester) || ApplySemesterSchoolYear[scoreInfo.GradeYear][scoreInfo.Semester] != scoreInfo.SchoolYear )
                                    removeList.Add(scoreInfo);
                            }
                            foreach ( SemesterEntryScoreInfo scoreInfo in removeList )
                            {
                                scoreList.Remove(scoreInfo);
                            }
                        }
                    }
                }
                #endregion
                #region 將成績填入學生資料內
                foreach ( Customization.Data.StudentRecord student in studentList )
                {
                    //填入成績
                    student.SemesterEntryScoreList.Clear();
                    if ( scoreDictionary.ContainsKey(student.StudentID) )
                        student.SemesterEntryScoreList.AddRange(scoreDictionary[student.StudentID]);
                    //填入成績編號
                    if ( student.Fields.ContainsKey("SemesterEntryScoreID") )
                        student.Fields.Remove("SemesterEntryScoreID");
                    if ( scoreIDDictionary.ContainsKey(student.StudentID) )
                        student.Fields.Add("SemesterEntryScoreID", scoreIDDictionary[student.StudentID]);
                    else
                        student.Fields.Add("SemesterEntryScoreID", new Dictionary<int, Dictionary<int, Dictionary<string, string>>>());
                }
                #endregion
            }
        }

        void Customization.Data.StudentInformationProvider.FillSemesterSubjectScore(IEnumerable<SmartSchool.Customization.Data.StudentRecord> students, bool filterRepeat)
        {
            //分批次處理
            foreach ( List<SmartSchool.Customization.Data.StudentRecord> studentList in SplitPackage<Customization.Data.StudentRecord>(GetList<SmartSchool.Customization.Data.StudentRecord>(students)) )
            {
                //學生成績資料
                Dictionary<string, List<Customization.Data.StudentExtension.SemesterSubjectScoreInfo>> scoreDictionary = new Dictionary<string, List<Customization.Data.StudentExtension.SemesterSubjectScoreInfo>>();
                //學生學期成績編號<學年度<學期,編號>>
                Dictionary<string, Dictionary<int, Dictionary<int, string>>> semesterIDDictionary = new Dictionary<string, Dictionary<int, Dictionary<int, string>>>();
                #region 填入科目成績資料
                //取得編號
                string[] idList = new string[studentList.Count];
                for ( int i = 0 ; i < idList.Length ; i++ )
                {
                    idList[i] = studentList[i].StudentID;
                }
                if ( idList.Length == 0 )
                    continue;
                //抓成績資料
                foreach ( XmlElement var in QueryScore.GetSemesterSubjectScore(idList).GetContent().GetElements("SemesterSubjectScore") )
                {
                    int schoolyear = 0;
                    int.TryParse(var.SelectSingleNode("SchoolYear").InnerText, out schoolyear);
                    int semester = 0;
                    int.TryParse(var.SelectSingleNode("Semester").InnerText, out semester);
                    int gradeyear = 0;
                    int.TryParse(var.SelectSingleNode("GradeYear").InnerText, out gradeyear);
                    string studentID = var.SelectSingleNode("RefStudentId").InnerText;

                    #region 填入學生學期成績編號
                    string id = var.GetAttribute("ID");
                    if ( !semesterIDDictionary.ContainsKey(studentID) )
                        semesterIDDictionary.Add(studentID, new Dictionary<int, Dictionary<int, string>>());
                    if ( !semesterIDDictionary[studentID].ContainsKey(schoolyear) )
                        semesterIDDictionary[studentID].Add(schoolyear, new Dictionary<int, string>());
                    semesterIDDictionary[studentID][schoolyear].Add(semester, id);
                    #endregion

                    Dictionary<string, XmlElement> subjectElements = new Dictionary<string, XmlElement>();
                    foreach ( XmlNode scoreNode in var.SelectNodes("ScoreInfo/SemesterSubjectScoreInfo/Subject") )
                    {
                        XmlElement element = (XmlElement)scoreNode;
                        string key = element.GetAttribute("科目") + "_" + element.GetAttribute("科目級別");

                        if ( !subjectElements.ContainsKey(key) )
                            subjectElements.Add(key, element);
                        else
                        {
                            XmlElement existElement = subjectElements[key];
                            string[] scoreNames = new string[] { "原始成績", "學年調整成績", "擇優採計成績", "補考成績", "重修成績" };
                            foreach ( string scorename in scoreNames )
                            {
                                decimal score;
                                decimal origScore;
                                if ( decimal.TryParse(element.GetAttribute(scorename), out score) )
                                {
                                    if ( decimal.TryParse(existElement.GetAttribute(scorename), out origScore) && score > origScore )
                                        existElement.SetAttribute(scorename, score.ToString());
                                    else
                                        existElement.SetAttribute(scorename, score.ToString());
                                }
                            }
                        }
                    }

                    foreach ( XmlElement element in subjectElements.Values )
                    {
                        //XmlElement element = (XmlElement)scoreNode;
                        string subject = element.GetAttribute("科目");
                        string level = element.GetAttribute("科目級別");
                        int credit = 0;
                        int.TryParse(element.GetAttribute("開課學分數"), out credit);
                        bool require = element.GetAttribute("修課必選修") == "必修";
                        bool pass = element.GetAttribute("是否取得學分") == "是";
                        XmlElement detail = element;
                        decimal score = 0;
                        bool hasScore = false;
                        string[] scoreNames = new string[] { "原始成績", "學年調整成績", "擇優採計成績", "補考成績", "重修成績" };
                        //抓最高分
                        foreach ( string scorename in scoreNames )
                        {
                            decimal s;
                            if ( decimal.TryParse(element.GetAttribute(scorename), out s) )
                            {
                                hasScore = true;
                                if ( s > score )
                                {
                                    score = s;
                                }
                            }
                        }

                        if ( hasScore )
                        {
                            StudentExtension.SemesterSubjectScore scoreInfo = new SmartSchool.API.StudentExtension.SemesterSubjectScore(schoolyear, semester, gradeyear, subject, level, credit, require, score, detail, pass);
                            if ( !scoreDictionary.ContainsKey(studentID) )
                                scoreDictionary.Add(studentID, new List<Customization.Data.StudentExtension.SemesterSubjectScoreInfo>());
                            scoreDictionary[studentID].Add(scoreInfo);
                        }
                        else
                        {
                            StudentExtension.SemesterSubjectScore scoreInfo = new SmartSchool.API.StudentExtension.SemesterSubjectScore(schoolyear, semester, gradeyear, subject, level, credit, require, 0, detail, pass);
                            if ( !scoreDictionary.ContainsKey(studentID) )
                                scoreDictionary.Add(studentID, new List<Customization.Data.StudentExtension.SemesterSubjectScoreInfo>());
                            scoreDictionary[studentID].Add(scoreInfo);
                        }
                    }
                }
                #endregion
                #region 過濾重讀成績
                if ( filterRepeat )
                {
                    foreach ( Customization.Data.StudentRecord student in studentList )
                    {
                        if ( scoreDictionary.ContainsKey(student.StudentID) )
                        {
                            List<Customization.Data.StudentExtension.SemesterSubjectScoreInfo> scoreList = scoreDictionary[student.StudentID];

                            Dictionary<int, Dictionary<int, int>> ApplySemesterSchoolYear = new Dictionary<int, Dictionary<int, int>>();
                            //先將此學生此時的學年度學期資料加入
                            int gradeYear = 0;
                            if ( student.RefClass != null && int.TryParse(student.RefClass.GradeYear, out gradeYear) )
                            {
                                ApplySemesterSchoolYear.Add(gradeYear, new Dictionary<int, int>());
                                ApplySemesterSchoolYear[gradeYear].Add(CurrentUser.Instance.Semester, CurrentUser.Instance.SchoolYear);
                            }
                            else
                                gradeYear = int.MaxValue;

                            //先掃一遍抓出每個年級最高的學年度
                            foreach ( SemesterSubjectScoreInfo scoreInfo in scoreList )
                            {
                                //如果成績年級學期比現在大就不理會
                                if ( scoreInfo.GradeYear < gradeYear || ( scoreInfo.GradeYear == gradeYear && scoreInfo.Semester <= SmartSchool.CurrentUser.Instance.Semester ) )
                                {
                                    if ( !ApplySemesterSchoolYear.ContainsKey(scoreInfo.GradeYear) )
                                        ApplySemesterSchoolYear.Add(scoreInfo.GradeYear, new Dictionary<int, int>());
                                    if ( !ApplySemesterSchoolYear[scoreInfo.GradeYear].ContainsKey(scoreInfo.Semester) )
                                        ApplySemesterSchoolYear[scoreInfo.GradeYear].Add(scoreInfo.Semester, scoreInfo.SchoolYear);
                                    if ( scoreInfo.SchoolYear > ApplySemesterSchoolYear[scoreInfo.GradeYear][scoreInfo.Semester] )
                                        ApplySemesterSchoolYear[scoreInfo.GradeYear][scoreInfo.Semester] = scoreInfo.SchoolYear;
                                }
                            }
                            //如果成績資料的年級學年度不在清單中就移掉
                            List<Customization.Data.StudentExtension.SemesterSubjectScoreInfo> removeList = new List<Customization.Data.StudentExtension.SemesterSubjectScoreInfo>();
                            foreach ( Customization.Data.StudentExtension.SemesterSubjectScoreInfo scoreInfo in scoreList )
                            {
                                //成績年級比現在大或成績的學年度部是最新的
                                if ( !ApplySemesterSchoolYear.ContainsKey(scoreInfo.GradeYear) || !ApplySemesterSchoolYear[scoreInfo.GradeYear].ContainsKey(scoreInfo.Semester) || ApplySemesterSchoolYear[scoreInfo.GradeYear][scoreInfo.Semester] != scoreInfo.SchoolYear )
                                    removeList.Add(scoreInfo);
                            }
                            foreach ( SemesterSubjectScoreInfo scoreInfo in removeList )
                            {
                                scoreList.Remove(scoreInfo);
                            }
                        }
                    }
                }
                #endregion
                #region 將成績填入學生資料內
                foreach ( Customization.Data.StudentRecord student in studentList )
                {
                    //填入學生成績
                    student.SemesterSubjectScoreList.Clear();
                    if ( scoreDictionary.ContainsKey(student.StudentID) )
                        student.SemesterSubjectScoreList.AddRange(scoreDictionary[student.StudentID]);
                    //填入學生學期成績編號
                    if ( student.Fields.ContainsKey("SemesterSubjectScoreID") )
                        student.Fields.Remove("SemesterSubjectScoreID");
                    if ( semesterIDDictionary.ContainsKey(student.StudentID) )
                        student.Fields.Add("SemesterSubjectScoreID", semesterIDDictionary[student.StudentID]);
                    else
                        student.Fields.Add("SemesterSubjectScoreID", new Dictionary<int, Dictionary<int, string>>());
                }
                #endregion
            }
        }

        void Customization.Data.StudentInformationProvider.FillUpdateRecord(IEnumerable<SmartSchool.Customization.Data.StudentRecord> students)
        {
            #region 搬到 GeneralSHUpdateRecord
            ////取得代碼對照表
            //XmlElement updateCodeMappingElement = Feature.Basic.Config.GetUpdateCodeSynopsis().GetContent().BaseElement;
            ////分批次處理
            //foreach (List<SmartSchool.Customization.Data.StudentRecord> studentList in SplitPackage<Customization.Data.StudentRecord>(GetList<SmartSchool.Customization.Data.StudentRecord>(students)))
            //{
            //    Dictionary<string, List<UpdateRecordInfo>> studentUpdateRecords = new Dictionary<string, List<UpdateRecordInfo>>();
            //    //取得編號
            //    #region 取得編號
            //    string[] idList = new string[studentList.Count];
            //    for (int i = 0; i < idList.Length; i++)
            //    {
            //        idList[i] = studentList[i].StudentID;
            //    }
            //    if (idList.Length == 0)
            //        continue;
            //    #endregion
            //    //抓成績資料
            //    #region 抓成績資料
            //    foreach (XmlElement element in SmartSchool.Feature.QueryStudent.GetUpdateRecordByStudentIDList(idList).GetContent().GetElements("UpdateRecord"))
            //    {
            //        string RefStudentID = element.GetAttribute("RefStudentID");
            //        if (!studentUpdateRecords.ContainsKey(RefStudentID))
            //            studentUpdateRecords.Add(RefStudentID, new List<UpdateRecordInfo>());
            //        studentUpdateRecords[RefStudentID].Add(new UpdateRecord(updateCodeMappingElement, element));
            //    }
            //    #endregion
            //    //填入學生的異動資料清單
            //    #region 填入學生的異動資料清單
            //    foreach (Customization.Data.StudentRecord student in studentList)
            //    {
            //        student.UpdateRecordList.Clear();
            //        if (studentUpdateRecords.ContainsKey(student.StudentID))
            //        {
            //            foreach (UpdateRecordInfo updateRecord in studentUpdateRecords[student.StudentID])
            //            {
            //                student.UpdateRecordList.Add(updateRecord);
            //            }
            //        }
            //    }
            //    #endregion
            //}
            #endregion
        }

        List<SmartSchool.Customization.Data.StudentRecord> Customization.Data.StudentInformationProvider.GetSelectedStudent()
        {
            List<SmartSchool.Customization.Data.StudentRecord> list = new List<SmartSchool.Customization.Data.StudentRecord>();
            foreach ( StudentRelated.BriefStudentData sinfo in StudentRelated.Student.Instance.SelectionStudents )
            {
                lock ( sinfo )
                {
                    if ( CachePool.ContainsKey(sinfo) )
                    {
                        list.Add((StudentRecord)CachePool[sinfo]);
                    }
                    else
                    {
                        StudentRecord newitem = new StudentRecord(sinfo, CachePool);
                        CachePool.Add(sinfo, newitem);
                        list.Add(newitem);
                    }
                }
            }
            return list;
        }

        List<SmartSchool.Customization.Data.StudentRecord> Customization.Data.StudentInformationProvider.GetStudents(IEnumerable<string> identities)
        {
            List<SmartSchool.Customization.Data.StudentRecord> list = new List<SmartSchool.Customization.Data.StudentRecord>();
            foreach ( string id in identities )
            {
                StudentRelated.BriefStudentData sinfo = StudentRelated.Student.Instance.Items[id];
                if ( sinfo != null )
                {
                    lock ( sinfo )
                    {
                        if ( CachePool.ContainsKey(sinfo) )
                        {
                            list.Add((StudentRecord)CachePool[sinfo]);
                        }
                        else
                        {
                            StudentRecord newitem = new StudentRecord(sinfo, CachePool);
                            CachePool.Add(sinfo, newitem);
                            list.Add(newitem);
                        }
                    }
                }
            }
            return list;
        }

        List<SmartSchool.Customization.Data.StudentRecord> Customization.Data.StudentInformationProvider.GetAllStudent()
        {
            List<SmartSchool.Customization.Data.StudentRecord> list = new List<SmartSchool.Customization.Data.StudentRecord>();
            foreach ( StudentRelated.BriefStudentData sinfo in StudentRelated.Student.Instance.Items )
            {
                if ( sinfo.IsNormal )
                {
                    lock ( sinfo )
                    {
                        if ( CachePool.ContainsKey(sinfo) )
                        {
                            list.Add((StudentRecord)CachePool[sinfo]);
                        }
                        else
                        {
                            StudentRecord newitem = new StudentRecord(sinfo, CachePool);
                            CachePool.Add(sinfo, newitem);
                            list.Add(newitem);
                        }
                    }
                }
            }
            return list;
        }

        void Customization.Data.StudentInformationProvider.FillField(string fieldName, IEnumerable<SmartSchool.Customization.Data.StudentRecord> students)
        {
            switch ( fieldName )
            {
                //case "ScoreCalcRule":
                //    #region 填入課程規畫表XML
                //    foreach (StudentRecord var in students)
                //    {
                //        SmartSchool.StudentRelated.BriefStudentData data = SmartSchool.StudentRelated.Student.Instance.Items[var.StudentID];
                //        if (data != null && data.ScoreCalcRuleInfo != null)
                //        {
                //            if (var.Fields.ContainsKey(fieldName))
                //                var.Fields[fieldName] = data.ScoreCalcRuleInfo.ScoreCalcRuleElement;
                //            else
                //                var.Fields.Add(fieldName, data.ScoreCalcRuleInfo.ScoreCalcRuleElement);
                //        }
                //        else
                //        {
                //            if (var.Fields.ContainsKey(fieldName))
                //                var.Fields[fieldName] = null;
                //            else
                //                var.Fields.Add(fieldName, null);
                //        }
                //    }
                //    #endregion
                //    break;
                case "SemesterHistory":
                    #region 填入學期歷程
                    List<string> idlist = new List<string>();
                    foreach ( Customization.Data.StudentRecord var in students )
                    {
                        idlist.Add(var.StudentID);
                    }
                    DSXmlHelper helper = Feature.QueryStudent.GetDetailList(new string[] { "ID", "SemesterHistory" }, idlist.ToArray()).GetContent();
                    foreach ( Customization.Data.StudentRecord var in students )
                    {
                        if ( var.Fields.ContainsKey("SemesterHistory") )
                            var.Fields["SemesterHistory"] = helper.GetElement("Student[@ID='" + var.StudentID + "']/SemesterHistory");
                        else
                            var.Fields.Add("SemesterHistory", helper.GetElement("Student[@ID='" + var.StudentID + "']/SemesterHistory"));
                    }
                    #endregion
                    break;
                case "FreshmanPhoto":
                    #region 填入入學照片
                    List<string> idlist2 = new List<string>();
                    foreach ( Customization.Data.StudentRecord var in students )
                        idlist2.Add(var.StudentID);
                    DSXmlHelper helper2 = Feature.QueryStudent.GetDetailList(new string[] { "ID", "FreshmanPhoto" }, idlist2.ToArray()).GetContent();
                    foreach ( Customization.Data.StudentRecord var in students )
                    {
                        string photoString = helper2.GetText("Student[@ID='" + var.StudentID + "']/FreshmanPhoto");
                        byte[] photoBytes = string.IsNullOrEmpty(photoString) ? null : Convert.FromBase64String(photoString);
                        if ( var.Fields.ContainsKey("FreshmanPhoto") )
                            var.Fields["FreshmanPhoto"] = photoBytes;
                        else
                            var.Fields.Add("FreshmanPhoto", photoBytes);
                    }
                    #endregion
                    break;
                case "GraduatePhoto":
                    #region 填入畢業照片
                    List<string> idlist3 = new List<string>();
                    foreach ( Customization.Data.StudentRecord var in students )
                        idlist3.Add(var.StudentID);
                    DSXmlHelper helper3 = Feature.QueryStudent.GetDetailList(new string[] { "ID", "GraduatePhoto" }, idlist3.ToArray()).GetContent();
                    foreach ( Customization.Data.StudentRecord var in students )
                    {
                        string photoString = helper3.GetText("Student[@ID='" + var.StudentID + "']/GraduatePhoto");
                        byte[] photoBytes = string.IsNullOrEmpty(photoString) ? null : Convert.FromBase64String(photoString);
                        if ( var.Fields.ContainsKey("GraduatePhoto") )
                            var.Fields["GraduatePhoto"] = photoBytes;
                        else
                            var.Fields.Add("GraduatePhoto", photoBytes);
                    }
                    #endregion
                    break;
                case "DiplomaNumber":
                    #region 填入畢業證書字號
                    List<string> idlist4 = new List<string>();
                    foreach ( Customization.Data.StudentRecord var in students )
                        idlist4.Add(var.StudentID);
                    DSXmlHelper helper4 = Feature.QueryStudent.GetDetailList(new string[] { "ID", "DiplomaNumber" }, idlist4.ToArray()).GetContent();
                    foreach ( Customization.Data.StudentRecord var in students )
                    {
                        XmlElement element = helper4.GetElement("Student[@ID='" + var.StudentID + "']/DiplomaNumber");
                        if ( var.Fields.ContainsKey("DiplomaNumber") )
                            var.Fields["DiplomaNumber"] = element;
                        else
                            var.Fields.Add("DiplomaNumber", element);
                    }
                    #endregion
                    break;
                case "EnglishName":
                    #region 填入英文姓名
                    List<string> idlist5 = new List<string>();
                    foreach ( Customization.Data.StudentRecord var in students )
                        idlist5.Add(var.StudentID);
                    DSXmlHelper helper5 = Feature.QueryStudent.GetDetailList(new string[] { "ID", "EnglishName" }, idlist5.ToArray()).GetContent();
                    foreach ( Customization.Data.StudentRecord var in students )
                    {
                        XmlElement element = helper5.GetElement("Student[@ID='" + var.StudentID + "']/EnglishName");
                        if ( var.Fields.ContainsKey("EnglishName") )
                            var.Fields["EnglishName"] = element;
                        else
                            var.Fields.Add("EnglishName", element);
                    }
                    #endregion
                    break;
                #region 各種固定排名
                case SemesterSubjectRating.ClassRating: //學期科目班排名。
                    new SemesterSubjectRating(students).FillClassRating();
                    break;
                case SemesterSubjectRating.DeptRating://學期科目科排名。
                    new SemesterSubjectRating(students).FillDeptRating();
                    break;
                case SemesterSubjectRating.YearRating://學期科目年排名。
                    new SemesterSubjectRating(students).FillYearRating();
                    break;
                case SchoolYearSubjectRating.ClassRating://學年科目班排名。
                    new SchoolYearSubjectRating(students).FillClassRating();
                    break;
                case SchoolYearSubjectRating.DeptRating://學年科目科排名。
                    new SchoolYearSubjectRating(students).FillDeptRating();
                    break;
                case SchoolYearSubjectRating.YearRating://學年科目年排名。
                    new SchoolYearSubjectRating(students).FillYearRating();
                    break;
                case SemesterEntryRating.ClassRating://學期分項班排名(學業、德行)。
                    new SemesterEntryRating(students).FillClassRating();
                    break;
                case SemesterEntryRating.DeptRating://學期分項科排名(學業、德行)。
                    new SemesterEntryRating(students).FillDeptRating();
                    break;
                case SemesterEntryRating.YearRating://學期分項年排名(學業、德行) 。
                    new SemesterEntryRating(students).FillYearRating();
                    break;
                case SchoolYearEntryRating.ClassRating://學年分項班排名(學業、德行) 。
                    new SchoolYearEntryRating(students).FillClassRating();
                    break;
                case SchoolYearEntryRating.DeptRating://學年分項科排名(學業、德行)。
                    new SchoolYearEntryRating(students).FillDeptRating();
                    break;
                case SchoolYearEntryRating.YearRating://學年分項年排名(學業、德行)。
                    new SchoolYearEntryRating(students).FillYearRating();
                    break;
                #endregion
                default:
                    break;
            }
            //InvokeFillData(this,new FillStudentEventArgs(_CashePool,"FillField",students,fieldName));
        }

        void Customization.Data.StudentInformationProvider.FillSemesterMoralScore(IEnumerable<SmartSchool.Customization.Data.StudentRecord> students, bool filterRepeat)
        {
            #region 取得德行加減分項目對照表
            List<string> moralDiffList = new List<string>();
            foreach ( XmlElement var in SmartSchool.Feature.Basic.Config.GetMoralDiffItemList().GetContent().GetElements("DiffItem") )
            {
                string name = var.GetAttribute("Name");
                if ( !moralDiffList.Contains(name) )
                    moralDiffList.Add(name);
            }
            #endregion

            //分批次處理
            foreach ( List<SmartSchool.Customization.Data.StudentRecord> studentList in SplitPackage<Customization.Data.StudentRecord>(GetList<SmartSchool.Customization.Data.StudentRecord>(students)) )
            {
                Dictionary<string, List<SmartSchool.Customization.Data.StudentExtension.SemesterMoralScoreInfo>> studentSemesterMoralScoreInfo = new Dictionary<string, List<SemesterMoralScoreInfo>>();
                #region 取得編號
                string[] idList = new string[studentList.Count];
                for ( int i = 0 ; i < idList.Length ; i++ )
                {
                    idList[i] = studentList[i].StudentID;
                }
                if ( idList.Length == 0 )
                    continue;
                #endregion
                #region 取得學期德行成績資料
                DSXmlHelper helper = new DSXmlHelper("Request");
                helper.AddElement("Field");
                helper.AddElement("Field", "All");
                helper.AddElement("Condition");
                helper.AddElement("Condition", "StudentIDList");
                foreach ( string id in idList )
                {
                    helper.AddElement("Condition/StudentIDList", "ID", id);
                }
                helper.AddElement("Order");
                helper.AddElement("Order", "OccurDate", "desc");

                foreach ( XmlElement var in SmartSchool.Feature.Score.QueryScore.GetSemesterMoralScore(new DSRequest(helper)).GetContent().GetElements("SemesterMoralScore") )
                {
                    // <Name>韓亞倫</Name>
                    // <OtherDiffList>
                    //     <OtherDiff Name="社團加減分" />
                    //     <OtherDiff Name="科主任加減分" />
                    //     <OtherDiff Name="神秘加減分" />
                    // </OtherDiffList>
                    // <Semester>1</Semester>
                    // <SupervisedByDiff />
                    // <RefStudentID>168695</RefStudentID>
                    // <SupervisedByComment />
                    // <SchoolYear>96</SchoolYear>

                    int schoolyear = 0;
                    int.TryParse(var.SelectSingleNode("SchoolYear").InnerText, out schoolyear);
                    int semester = 0;
                    int.TryParse(var.SelectSingleNode("Semester").InnerText, out semester);
                    int gradeyear = 0;
                    // 沒有 GradeYear ??
                    decimal supervisedbydiff = 0;
                    decimal.TryParse(var.SelectSingleNode("SupervisedByDiff").InnerText, out supervisedbydiff);
                    string supervisedbycomment = var.SelectSingleNode("SupervisedByComment").InnerText;

                    string studentID = var.SelectSingleNode("RefStudentID").InnerText;

                    Dictionary<string, decimal> otherdiff = new Dictionary<string, decimal>();

                    foreach ( XmlElement element in var.SelectNodes("OtherDiffList/OtherDiff") )
                    {
                        string diffname = element.GetAttribute("Name");
                        decimal diffvalue = 0;
                        decimal.TryParse(element.InnerText, out diffvalue);

                        if ( !otherdiff.ContainsKey(diffname) && moralDiffList.Contains(diffname) )
                            otherdiff.Add(diffname, diffvalue);
                    }

                    StudentExtension.SemesterMoralScore semesterMoralScoreInfo = new SemesterMoralScore(schoolyear, semester, gradeyear, supervisedbycomment, supervisedbydiff, otherdiff, var);

                    if ( !studentSemesterMoralScoreInfo.ContainsKey(studentID) )
                        studentSemesterMoralScoreInfo.Add(studentID, new List<SemesterMoralScoreInfo>());
                    studentSemesterMoralScoreInfo[studentID].Add(semesterMoralScoreInfo);
                }
                #endregion
                #region 填入學期德行成績資料
                foreach ( Customization.Data.StudentRecord student in studentList )
                {
                    student.SemesterMoralScoreList.Clear();
                    if ( studentSemesterMoralScoreInfo.ContainsKey(student.StudentID) )
                        student.SemesterMoralScoreList.AddRange(studentSemesterMoralScoreInfo[student.StudentID]);
                }
                #endregion
            }
        }

        void Customization.Data.StudentInformationProvider.FillSemesterHistory(IEnumerable<SmartSchool.Customization.Data.StudentRecord> students)
        {
            //分批次處理
            foreach ( List<SmartSchool.Customization.Data.StudentRecord> studentList in SplitPackage<Customization.Data.StudentRecord>(GetList<SmartSchool.Customization.Data.StudentRecord>(students)) )
            {
                #region 取得編號
                string[] idList = new string[studentList.Count];
                for ( int i = 0 ; i < idList.Length ; i++ )
                {
                    idList[i] = studentList[i].StudentID;
                }
                if ( idList.Length == 0 )
                    continue;
                #endregion
                DSXmlHelper helper = Feature.QueryStudent.GetDetailList(new string[] { "ID", "SemesterHistory" }, idList).GetContent();
                foreach ( SmartSchool.Customization.Data.StudentRecord var in studentList )
                {
                    var.SemesterHistoryList.Clear();
                    foreach ( XmlElement element in helper.GetElements("Student[@ID='" + var.StudentID + "']/SemesterHistory/History") )
                    {
                        var.SemesterHistoryList.Add(new SmartSchool.API.StudentExtension.SemesterHistory(element));
                    }
                }
            }
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

        public SmartSchool.Customization.Data.StudentRecord GetStudentByStudentNumber(string studentNumber)
        {
            throw new Exception("The method or operation is not implemented.");
        }

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
        #endregion

        #region ICloneable 成員

        object ICloneable.Clone()
        {
            return new StudentProvider();
        }

        #endregion

        #region 排名資料取得
        internal class SemesterSubjectRating
        {
            public const string ClassRating = "SemesterSubjectClassRating";
            public const string DeptRating = "SemesterSubjectDeptRating";
            public const string YearRating = "SemesterSubjectYearRating";

            private Dictionary<string, IStudentRecord> _students;

            public SemesterSubjectRating(IEnumerable<SmartSchool.Customization.Data.StudentRecord> students)
            {
                //_students = new Dictionary<string, SmartSchool.Customization.Data.StudentRecord>();
                //foreach (SmartSchool.Customization.Data.StudentRecord eachStudent in students)
                //    _students.Add(eachStudent.StudentID, eachStudent);
            }

            public void FillClassRating()
            {
                throw new NotImplementedException("此資料取得方式未實作。");
            }

            public void FillDeptRating()
            {
                throw new NotImplementedException("此資料取得方式未實作。");
            }

            public void FillYearRating()
            {
                throw new NotImplementedException("此資料取得方式未實作。");
            }
        }

        internal class SchoolYearSubjectRating
        {
            public const string ClassRating = "SchoolYearSubjectClassRating";
            public const string DeptRating = "SchoolYearSubjectDeptRating";
            public const string YearRating = "SchoolYearSubjectYearRating";

            private Dictionary<string, IStudentRecord> _students;

            public SchoolYearSubjectRating(IEnumerable<SmartSchool.Customization.Data.StudentRecord> students)
            {
                //_students = new Dictionary<string, SmartSchool.Customization.Data.StudentRecord>();
                //foreach (SmartSchool.Customization.Data.StudentRecord eachStudent in students)
                //    _students.Add(eachStudent.StudentID, eachStudent);
            }

            public void FillClassRating()
            {
                throw new NotImplementedException("此資料取得方式未實作。");
            }

            public void FillDeptRating()
            {
                throw new NotImplementedException("此資料取得方式未實作。");
            }

            public void FillYearRating()
            {
                throw new NotImplementedException("此資料取得方式未實作。");
            }
        }

        internal class SemesterEntryRating
        {
            public const string ClassRating = "SemesterEntryClassRating";
            public const string DeptRating = "SemesterEntryDeptRating";
            public const string YearRating = "SemesterEntryYearRating";

            private Dictionary<string, IStudentRecord> _students;

            public SemesterEntryRating(IEnumerable<SmartSchool.Customization.Data.StudentRecord> students)
            {
                _students = new Dictionary<string, SmartSchool.Customization.Data.StudentRecord>();
                foreach ( SmartSchool.Customization.Data.StudentRecord eachStudent in students )
                    _students.Add(eachStudent.StudentID, eachStudent);
            }

            public void FillClassRating()
            {
                //先將所有學生設定一個空的資料。
                XmlElement emptyRating = DSXmlHelper.LoadXml("<SemesterEntryClassRating/>");
                foreach ( IStudentRecord eachStudent in _students.Values )
                    SetRating(ClassRating, eachStudent, emptyRating);

                //分批取得資料並填入物件。
                foreach ( List<IStudentRecord> package in SplitPackage<IStudentRecord>(new List<IStudentRecord>(_students.Values)) )
                {
                    List<string> idlist = new List<string>();
                    foreach ( IStudentRecord eachStudent in package )
                        idlist.Add(eachStudent.StudentID);

                    DSXmlHelper hlpRatings = QueryRating.GetSemesterEntryRating(true, false, false, idlist.ToArray());
                    Dictionary<string, DSXmlHelper> ratings = new Dictionary<string, DSXmlHelper>();

                    //將每筆成績資料放入到相對應的學生資料中。
                    foreach ( XmlElement eachRating in hlpRatings.GetElements("SemesterEntryScore") )
                    {
                        DSXmlHelper hlpRatingSet; //代表一學生所有學期的排名資料。
                        DSXmlHelper hlpEntryScore = new DSXmlHelper(eachRating);
                        string studentId = hlpEntryScore.GetText("RefStudentId");

                        if ( !ratings.TryGetValue(studentId, out hlpRatingSet) )
                        {
                            hlpRatingSet = new DSXmlHelper("SemesterEntryClassRating");
                            ratings.Add(studentId, hlpRatingSet);
                        }

                        hlpRatingSet.AddElement(".", eachRating);
                    }

                    foreach ( KeyValuePair<string, DSXmlHelper> eachRating in ratings )
                    {
                        if ( _students.ContainsKey(eachRating.Key) )
                            SetRating(ClassRating, _students[eachRating.Key], eachRating.Value.BaseElement);
                    }
                }
            }

            public void FillDeptRating()
            {
                throw new NotImplementedException("此資料取得方式未實作。");
            }

            public void FillYearRating()
            {
                throw new NotImplementedException("此資料取得方式未實作。");
            }

            private static void SetRating(string key, IStudentRecord eachStudent, XmlElement emptyRating)
            {
                if ( eachStudent.Fields.ContainsKey(key) )
                    eachStudent.Fields[key] = emptyRating;
                else
                    eachStudent.Fields.Add(key, emptyRating);
            }
        }

        internal class SchoolYearEntryRating
        {
            public const string ClassRating = "SchoolYearEntryClassRating";
            public const string DeptRating = "SchoolYearEntryDeptRating";
            public const string YearRating = "SchoolYearEntryYearRating";

            private Dictionary<string, IStudentRecord> _students;

            public SchoolYearEntryRating(IEnumerable<SmartSchool.Customization.Data.StudentRecord> students)
            {
                _students = new Dictionary<string, SmartSchool.Customization.Data.StudentRecord>();
                foreach ( SmartSchool.Customization.Data.StudentRecord eachStudent in students )
                    _students.Add(eachStudent.StudentID, eachStudent);
            }

            public void FillClassRating()
            {
                //先將所有學生設定一個空的資料。
                XmlElement emptyRating = DSXmlHelper.LoadXml("<SchoolYearEntryClassRating/>");
                foreach ( IStudentRecord eachStudent in _students.Values )
                    SetRating(ClassRating, eachStudent, emptyRating);

                //分批取得資料並填入物件。
                foreach ( List<IStudentRecord> package in SplitPackage<IStudentRecord>(new List<IStudentRecord>(_students.Values)) )
                {
                    List<string> idlist = new List<string>();
                    foreach ( IStudentRecord eachStudent in package )
                        idlist.Add(eachStudent.StudentID);

                    DSXmlHelper hlpRatings = QueryRating.GetSchoolYearEntryRating(true, false, false, idlist.ToArray());
                    Dictionary<string, DSXmlHelper> ratings = new Dictionary<string, DSXmlHelper>();

                    //將每筆成績資料放入到相對應的學生資料中。
                    foreach ( XmlElement eachRating in hlpRatings.GetElements("SchoolYearEntryScore") )
                    {
                        DSXmlHelper hlpRatingSet; //代表一學生所有學期的排名資料。
                        DSXmlHelper hlpEntryScore = new DSXmlHelper(eachRating);
                        string studentId = hlpEntryScore.GetText("RefStudentId");

                        if ( !ratings.TryGetValue(studentId, out hlpRatingSet) )
                        {
                            hlpRatingSet = new DSXmlHelper("SchoolYearEntryClassRating");
                            ratings.Add(studentId, hlpRatingSet);
                        }

                        hlpRatingSet.AddElement(".", eachRating);
                    }

                    foreach ( KeyValuePair<string, DSXmlHelper> eachRating in ratings )
                    {
                        if ( _students.ContainsKey(eachRating.Key) )
                            SetRating(ClassRating, _students[eachRating.Key], eachRating.Value.BaseElement);
                    }
                }
            }

            public void FillDeptRating()
            {
                throw new NotImplementedException("此資料取得方式未實作。");
            }

            public void FillYearRating()
            {
                throw new NotImplementedException("此資料取得方式未實作。");
            }

            private static void SetRating(string key, IStudentRecord eachStudent, XmlElement emptyRating)
            {
                if ( eachStudent.Fields.ContainsKey(key) )
                    eachStudent.Fields[key] = emptyRating;
                else
                    eachStudent.Fields.Add(key, emptyRating);
            }
        }

        #endregion

        //public static event EventHandler<FillStudentEventArgs> FillData;

        //private static void InvokeFillData(object sender,FillStudentEventArgs args)
        //{
        //    if ( FillData != null )
        //        FillData.Invoke(sender, args);
        //}


        #region StudentInformationProvider 成員

        public Dictionary<SmartSchool.Customization.Data.StudentRecord, Dictionary<string, string>> GetExtensionFields(IEnumerable<SmartSchool.Customization.Data.StudentRecord> students, string nameSpace, string[] fields)
        {
            Dictionary<SmartSchool.Customization.Data.StudentRecord, Dictionary<string, string>> result = new Dictionary<SmartSchool.Customization.Data.StudentRecord, Dictionary<string, string>>();
            //分批次處理
            foreach ( List<SmartSchool.Customization.Data.StudentRecord> studentList in SplitPackage<Customization.Data.StudentRecord>(GetList<SmartSchool.Customization.Data.StudentRecord>(students)) )
            {
                List<string> ids = new List<string>();
                SortedList<string, StudentRecord> dicStudent = new SortedList<string, StudentRecord>();
                foreach ( StudentRecord stu in studentList )
                {
                    ids.Add(stu.StudentID);
                    dicStudent.Add(stu.StudentID, stu);
                }
                DSResponse resp = Feature.QueryStudent.GetExtension(nameSpace, fields, ids.ToArray());
                foreach ( XmlElement element in resp.GetContent().GetElements("Student") )
                {
                    StudentRecord stuRec = dicStudent[element.GetAttribute("ID")];
                    //stuRec.ExamScoreList.
                    result.Add(stuRec, new Dictionary<string, string>());
                    foreach ( XmlNode node in element.ChildNodes )
                    {
                        if ( node is XmlElement )
                        {
                            XmlElement ele = (XmlElement)node;
                            result[stuRec].Add(ele.Name, ele.InnerText);
                        }
                    }
                }
            }
            return result;
        }

        public void SetExtensionFields(string nameSpace, string field, IDictionary<SmartSchool.Customization.Data.StudentRecord, string> list)
        {
            Dictionary<string, string> items = new Dictionary<string, string>();
            foreach ( SmartSchool.Customization.Data.StudentRecord var in list.Keys )
            {
                items.Add(var.StudentID, list[var]);
            }
            Feature.EditStudent.SetExtend(nameSpace, field, items);
        }

        #endregion
    }
}
