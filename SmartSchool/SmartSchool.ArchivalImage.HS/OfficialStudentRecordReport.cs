using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.Data.StudentExtension;
using SmartSchool.Customization.PlugIn;
using Aspose.Words;
using SmartSchool.Common;
using System.Windows.Forms;
using System.ComponentModel;
using System.Xml;
using Aspose.Words.Drawing;

namespace SmartSchool.ArchivalImage
{
    class OfficialStudentRecordReport
    {
        ButtonAdapter button_student, button_class;
        BackgroundWorker _BGWStudentRecord;

        public OfficialStudentRecordReport()
        {
            string reportName = "學生學籍表";
            string reportPath = "成績相關報表";

            button_student = new SecureButtonAdapter("Report0040");
            button_student.Text = reportName;
            button_student.Path = reportPath;
            button_student.OnClick += new EventHandler(button_student_OnClick);
            SmartSchool.Customization.PlugIn.Report.StudentReport.AddReport(button_student);

            button_class = new SecureButtonAdapter("Report0140");
            button_class.Text = reportName;
            button_class.Path = reportPath;
            button_class.OnClick += new EventHandler(button_class_OnClick);
            SmartSchool.Customization.PlugIn.Report.ClassReport.AddReport(button_class);
        }

        private void button_student_OnClick(object sender, EventArgs e)
        {
            AccessHelper helper = new AccessHelper();
            List<StudentRecord> allStudent = helper.StudentHelper.GetSelectedStudent();

            Clicked(helper, allStudent);
        }

        private void button_class_OnClick(object sender, EventArgs e)
        {
            AccessHelper helper = new AccessHelper();
            List<StudentRecord> allStudent = new List<StudentRecord>();
            foreach (ClassRecord aClass in helper.ClassHelper.GetSelectedClass())
            {
                allStudent.AddRange(aClass.Students);
            }

            Clicked(helper, allStudent);
        }

        private void Clicked(AccessHelper helper, List<StudentRecord> allStudent)
        {
            FrontForm form = new FrontForm();

            MemoryStream selectedTemplate = new MemoryStream();
            int templateNumber = 1;
            string text1 = "";
            string text2 = "";

            int custodian = 0;
            int address = 0;
            int phone = 0;

            bool over100 = false;
            string coreSubjectSign = "";
            string resitSign = "";
            string retakeSign = "";
            string failedSign = "";
            string schoolYearAdjustSign = "";
            string manualAdjustSign = "";

            if (form.ShowDialog() == DialogResult.OK)
            {
                selectedTemplate = form.Template;
                templateNumber = form.TemplateNumber;
                text1 = form.Text1;
                text2 = form.Text2;
                custodian = form.Custodian;
                address = form.Address;
                phone = form.Phone;
                over100 = form.AllowMoralScoreOver100;
                coreSubjectSign = form.SignCoreSubject;
                resitSign = form.SignResit;
                retakeSign = form.SignRetake;
                failedSign = form.SignFailed;
                schoolYearAdjustSign = form.SignSchoolYearAdjust;
                manualAdjustSign = form.SignManualAdjust;
            }
            else
                return;

            int serialNumber = 0;
            int.TryParse(text2, out serialNumber);

            if (allStudent.Count > 500)
            {
                MsgBox.Show("選取的學生超過 500 人，很有可能導致系統效率減緩，請減少選取人數");
                return;
            }

            SmartSchool.Customization.Data.SystemInformation.getField("SchoolConfig");

            helper.StudentHelper.FillParentInfo(allStudent);
            helper.StudentHelper.FillContactInfo(allStudent);
            helper.StudentHelper.FillUpdateRecord(allStudent);
            helper.StudentHelper.FillSchoolYearEntryScore(true, allStudent);
            helper.StudentHelper.FillSchoolYearSubjectScore(true, allStudent);
            helper.StudentHelper.FillSemesterEntryScore(true, allStudent);
            helper.StudentHelper.FillSemesterSubjectScore(true, allStudent);
            helper.StudentHelper.FillField("SemesterEntryClassRating", allStudent); //學期分項班排名。
            helper.StudentHelper.FillField("SchoolYearEntryClassRating", allStudent); //學年分項班排名。
            helper.StudentHelper.FillField("FreshmanPhoto", allStudent); //入學照片
            helper.StudentHelper.FillField("GraduatePhoto", allStudent); //畢業照片
            helper.StudentHelper.FillField("畢業成績", allStudent);
            helper.StudentHelper.FillField("核心科目表", allStudent);
            helper.StudentHelper.FillField("DiplomaNumber", allStudent); //畢業證書字號

            Document doc = new Document();
            doc.Sections.Clear();

            object[] bgwObject = new object[] {
                allStudent,
                doc,
                selectedTemplate,
                templateNumber,
                text1,
                text2,
                serialNumber,
                custodian,
                address,
                phone,
                over100,
                coreSubjectSign,
                resitSign,
                retakeSign,
                failedSign,
                schoolYearAdjustSign,
                manualAdjustSign,
                form.TagList};

            _BGWStudentRecord = new BackgroundWorker();
            _BGWStudentRecord.WorkerReportsProgress = true;
            _BGWStudentRecord.DoWork += new DoWorkEventHandler(_BGWStudentRecord_DoWork);
            _BGWStudentRecord.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BGWStudentRecord_RunWorkerCompleted);
            _BGWStudentRecord.ProgressChanged += new ProgressChangedEventHandler(_BGWStudentRecord_ProgressChanged);
            _BGWStudentRecord.RunWorkerAsync(bgwObject);
        }

        private void _BGWStudentRecord_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button_student.SetBarMessage("學生學籍表產生完成");
            CommonMethod.Completed("學生學籍表", (Document)e.Result);
        }

        private void _BGWStudentRecord_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            button_student.SetBarMessage("學生學籍表產生中...", e.ProgressPercentage);
        }

        private void _BGWStudentRecord_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] bgwObject = (object[])e.Argument;
            List<StudentRecord> allStudent = (List<StudentRecord>)bgwObject[0];
            Document doc = (Document)bgwObject[1];
            MemoryStream selectedTemplate = (MemoryStream)bgwObject[2];
            int templateNumber = (int)bgwObject[3];
            string text1 = (string)bgwObject[4];
            string text2 = (string)bgwObject[5];
            int serialNumber = (int)bgwObject[6];
            int custodian = (int)bgwObject[7];
            int address = (int)bgwObject[8];
            int phone = (int)bgwObject[9];
            bool over100 = (bool)bgwObject[10];
            string coreSubjectSign = (string)bgwObject[11];
            string resitSign = (string)bgwObject[12];
            string retakeSign = (string)bgwObject[13];
            string failedSign = (string)bgwObject[14];
            string schoolYearAdjustSign = (string)bgwObject[15];
            string manualAdjustSign = (string)bgwObject[16];
            Dictionary<string, string> tagList = (Dictionary<string, string>)bgwObject[17];

            #region 取得資料

            Dictionary<string, Dictionary<string, string>> studentInfo = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, List<Dictionary<string, string>>> studentUpdateRecord = new Dictionary<string, List<Dictionary<string, string>>>();
            Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> studentSubjectScore = new Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>>();
            Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> studentEntryScore = new Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>>();
            Dictionary<string, Dictionary<string, Dictionary<string, int>>> studentCredit = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>();
            Dictionary<string, XmlElement> studentCoreSubject = new Dictionary<string, XmlElement>();

            Dictionary<string, byte[]> studentPhoto = new Dictionary<string, byte[]>();

            Dictionary<string, Dictionary<string, string>> mergeField = new Dictionary<string, Dictionary<string, string>>();

            double totalStudent = allStudent.Count;
            double currentStudent = 1;

            foreach (StudentRecord var in allStudent)
            {
                #region 學生基本資料

                if (!studentInfo.ContainsKey(var.StudentID))
                    studentInfo.Add(var.StudentID, new Dictionary<string, string>());

                studentInfo[var.StudentID].Add("學號", var.StudentNumber);
                studentInfo[var.StudentID].Add("姓名", var.StudentName);
                studentInfo[var.StudentID].Add("性別", var.Gender);
                studentInfo[var.StudentID].Add("出生日期", CommonMethod.CDATE(var.Birthday));

                //studentInfo[var.StudentID].Add("年級", (var.RefClass != null) ? var.RefClass.GradeYear : "");
                studentInfo[var.StudentID].Add("班級", (var.RefClass != null) ? var.RefClass.ClassName : "");
                studentInfo[var.StudentID].Add("離校班級", var.Fields["LeaveClassName"] as string);
                studentInfo[var.StudentID].Add("座號", var.SeatNo);

                if (custodian == 0)
                {
                    studentInfo[var.StudentID].Add("家長", "監護人");
                    studentInfo[var.StudentID].Add("家長內容", var.ParentInfo.CustodianName);
                    studentInfo[var.StudentID].Add("稱謂", var.ParentInfo.CustodianRelationship);
                }
                else if (custodian == 1)
                {
                    studentInfo[var.StudentID].Add("家長", "父親");
                    studentInfo[var.StudentID].Add("家長內容", var.ParentInfo.FatherName);
                    studentInfo[var.StudentID].Add("稱謂", "父");
                }
                else if (custodian == 2)
                {
                    studentInfo[var.StudentID].Add("家長", "母親");
                    studentInfo[var.StudentID].Add("家長內容", var.ParentInfo.MotherName);
                    studentInfo[var.StudentID].Add("稱謂", "母");
                }

                string cate = "";

                #region 處理學生學籍身分

                XmlElement schoolConfig = SmartSchool.Customization.Data.SystemInformation.Fields["SchoolConfig"] as XmlElement;
                XmlElement identityMapping = schoolConfig.SelectSingleNode("學籍身分對照表") as XmlElement;
                if (identityMapping != null)
                {

                    List<string> cateList = new List<string>();

                    foreach (CategoryInfo info in var.StudentCategorys)
                    {
                        foreach (XmlElement each_identity in identityMapping.SelectNodes("Identity[Tag/@FullName='" + info.FullName + "']"))
                        {
                            string each_name = each_identity.GetAttribute("Name");
                            if (!cateList.Contains(each_name))
                                cateList.Add(each_name);
                        }
                    }

                    foreach (string each_cate in cateList)
                    {
                        if (!string.IsNullOrEmpty(cate)) cate += ",";
                        cate += each_cate;
                    }
                }

                //if (string.IsNullOrEmpty(cate))
                //    cate = "一般生";

                #region 收起來不用
                //foreach (CategoryInfo info in var.StudentCategorys)
                //{
                //    if (tagList.ContainsValue(info.FullName))
                //    {
                //        if (!string.IsNullOrEmpty(cate))
                //            cate += ",";
                //        if (!string.IsNullOrEmpty(info.SubCategory))
                //            cate += info.SubCategory;
                //        else
                //            cate += info.Name;
                //    }
                //}
                #endregion

                #endregion

                studentInfo[var.StudentID].Add("身分", cate);
                studentInfo[var.StudentID].Add("科別", var.Department);
                studentInfo[var.StudentID].Add("身分證號", var.IDNumber);

                if (address == 0)
                {
                    studentInfo[var.StudentID].Add("地址", "戶籍地址");
                    studentInfo[var.StudentID].Add("地址內容", var.ContactInfo.PermanentAddress.FullAddress);
                }
                else if (address == 1)
                {
                    studentInfo[var.StudentID].Add("地址", "聯絡地址");
                    studentInfo[var.StudentID].Add("地址內容", var.ContactInfo.MailingAddress.FullAddress);
                }

                if (phone == 0)
                {
                    studentInfo[var.StudentID].Add("電話", "戶籍電話");
                    studentInfo[var.StudentID].Add("電話內容", var.ContactInfo.PermenantPhone);
                }
                else if (phone == 1)
                {
                    studentInfo[var.StudentID].Add("電話", "聯絡電話");
                    studentInfo[var.StudentID].Add("電話內容", var.ContactInfo.ContactPhone);
                }

                if (!studentUpdateRecord.ContainsKey(var.StudentID))
                    studentUpdateRecord.Add(var.StudentID, new List<Dictionary<string, string>>());

                if (!mergeField.ContainsKey(var.StudentID))
                    mergeField.Add(var.StudentID, new Dictionary<string, string>());

                //入學畢業欄位
                foreach (string key in new string[] { "入學學校", "入學資格", "入學文號", "畢業證書", "畢業文號" })
                    mergeField[var.StudentID].Add(key, "");


                //如果沒有畢業照片，則用入學照片
                if (!studentPhoto.ContainsKey(var.StudentID))
                {
                    byte[] graduatePhoto = var.Fields["GraduatePhoto"] as byte[];
                    if (graduatePhoto != null)
                        studentPhoto.Add(var.StudentID, graduatePhoto);
                    else
                        studentPhoto.Add(var.StudentID, var.Fields["FreshmanPhoto"] as byte[]);
                }

                //核心科目表
                if (!studentCoreSubject.ContainsKey(var.StudentID))
                {
                    if (var.Fields.ContainsKey("核心科目表"))
                        studentCoreSubject.Add(var.StudentID, var.Fields["核心科目表"] as XmlElement);
                }

                #endregion

                #region 學生畢業成績

                if (var.Fields.ContainsKey("畢業成績"))
                {
                    Dictionary<string, decimal> graduationScore = var.Fields["畢業成績"] as Dictionary<string, decimal>;
                    studentInfo[var.StudentID].Add("畢業學業成績", graduationScore.ContainsKey("學業") ? graduationScore["學業"] + "" : "");
                    studentInfo[var.StudentID].Add("畢業體育成績", graduationScore.ContainsKey("體育") ? graduationScore["體育"] + "" : "");
                    studentInfo[var.StudentID].Add("畢業國防通識成績", graduationScore.ContainsKey("國防通識") ? graduationScore["國防通識"] + "" : "");
                    studentInfo[var.StudentID].Add("畢業德行成績", graduationScore.ContainsKey("德行") ? graduationScore["德行"] + "" : "");
                }

                #endregion

                #region 學生異動資料

                const string 畢業代碼 = "501";
                const string 新生 = "0";
                const string 轉入 = "1";

                UpdateRecordInfo lastEnrollInfo = null;

                foreach (UpdateRecordInfo urInfo in var.UpdateRecordList)
                {
                    string code = urInfo.UpdateCode;
                    if (code.Substring(0, 1) == 新生 || code.Substring(0, 1) == 轉入)
                    {
                        if (lastEnrollInfo == null)
                            lastEnrollInfo = urInfo;
                        else
                        {
                            if (DateTime.Parse(urInfo.UpdateDate) > DateTime.Parse(lastEnrollInfo.UpdateDate))
                                lastEnrollInfo = urInfo;
                        }
                    }
                    else if (code == 畢業代碼)
                    {
                        if (!string.IsNullOrEmpty(urInfo.ADNumber))
                        {
                            mergeField[var.StudentID]["畢業證書"] = urInfo.GraduateCertificateNumber;
                            mergeField[var.StudentID]["畢業文號"] = urInfo.ADNumber + " " + urInfo.ADDate;
                        }
                    }
                    else
                    {
                        Dictionary<string, string> record = new Dictionary<string, string>();
                        record.Add("異動日期", urInfo.UpdateDate);
                        record.Add("異動事項", urInfo.UpdateCode + " " + urInfo.UpdateDescription);
                        record.Add("異動核准文號", urInfo.ADNumber + " " + urInfo.ADDate);
                        studentUpdateRecord[var.StudentID].Add(record);
                    }
                }
                if (lastEnrollInfo != null)
                {
                    if (lastEnrollInfo.UpdateCode.Substring(0, 1) == 新生)
                        mergeField[var.StudentID]["入學學校"] = lastEnrollInfo.GraduateSchool;
                    else if (lastEnrollInfo.UpdateCode.Substring(0, 1) == 轉入)
                    {
                        XmlHelper helper = new XmlHelper(lastEnrollInfo.Detail);
                        mergeField[var.StudentID]["入學學校"] = helper.GetText("ContextInfo/ContextInfo/PreviousSchool");
                    }
                    mergeField[var.StudentID]["入學資格"] = lastEnrollInfo.UpdateCode + lastEnrollInfo.UpdateDescription;
                    mergeField[var.StudentID]["入學文號"] = lastEnrollInfo.ADNumber + " " + lastEnrollInfo.ADDate;
                }

                if (string.IsNullOrEmpty(mergeField[var.StudentID]["畢業證書"]) && string.IsNullOrEmpty(mergeField[var.StudentID]["畢業文號"]))
                {
                    XmlElement dn = var.Fields["DiplomaNumber"] as XmlElement;
                    if (dn.SelectSingleNode("DiplomaNumber") != null)
                        mergeField[var.StudentID]["畢業證書"] = dn.SelectSingleNode("DiplomaNumber").InnerText;
                }
                if (!string.IsNullOrEmpty(mergeField[var.StudentID]["畢業證書"]) && string.IsNullOrEmpty(mergeField[var.StudentID]["畢業文號"]))
                {
                    mergeField[var.StudentID]["畢業證書"] += "***";
                }

                #endregion

                #region 科目成績

                //加入科目成績
                if (!studentSubjectScore.ContainsKey(var.StudentID))
                    studentSubjectScore.Add(var.StudentID, new Dictionary<string, Dictionary<string, Dictionary<string, string>>>());

                var.SemesterSubjectScoreList.Sort(CommonMethod.SortSubjectByLevel);

                //學期科目成績
                foreach (SemesterSubjectScoreInfo info in var.SemesterSubjectScoreList)
                {
                    string studentID = var.StudentID;
                    string gradeYear = info.GradeYear.ToString();
                    string subject = info.Subject;

                    string score = "";
                    string sign = "";

                    if (studentCoreSubject.ContainsKey(studentID))
                    {
                        XmlElement coreXml = studentCoreSubject[studentID] as XmlElement;
                        if (coreXml.SelectSingleNode("核心科目[@科目='" + info.Subject + "' and @級別='" + info.Level + "']") != null)
                            sign = coreSubjectSign;
                    }

                    string invalidCredit = info.Detail.GetAttribute("不計學分");
                    string noScoreString = info.Detail.GetAttribute("不需評分");
                    bool noScore = (noScoreString != "是");

                    if (invalidCredit == "是")
                        continue;

                    if (!studentSubjectScore[studentID].ContainsKey(gradeYear))
                        studentSubjectScore[studentID].Add(gradeYear, new Dictionary<string, Dictionary<string, string>>());

                    #region 判斷分數

                    string current_sign = "";
                    decimal originalScore = -1;
                    decimal schoolYearAdjustScore = -1;
                    decimal betterScore = -1; //表面上，這個已經改名為「手動調整成績」
                    decimal resitScore = -1;
                    decimal retakeScore = -1;

                    XmlElement detail = info.Detail;
                    StringBuilder scoreBuilder = new StringBuilder("");

                    if (detail.HasAttribute("原始成績") && !string.IsNullOrEmpty(detail.GetAttribute("原始成績")))
                        originalScore = decimal.Parse(detail.GetAttribute("原始成績"));
                    if (detail.HasAttribute("學年調整成績") && !string.IsNullOrEmpty(detail.GetAttribute("學年調整成績")))
                        schoolYearAdjustScore = decimal.Parse(detail.GetAttribute("學年調整成績"));
                    if (detail.HasAttribute("擇優採計成績") && !string.IsNullOrEmpty(detail.GetAttribute("擇優採計成績")))
                        betterScore = decimal.Parse(detail.GetAttribute("擇優採計成績"));
                    if (detail.HasAttribute("補考成績") && !string.IsNullOrEmpty(detail.GetAttribute("補考成績")))
                        resitScore = decimal.Parse(detail.GetAttribute("補考成績"));
                    if (detail.HasAttribute("重修成績") && !string.IsNullOrEmpty(detail.GetAttribute("重修成績")))
                        retakeScore = decimal.Parse(detail.GetAttribute("重修成績"));

                    decimal highestScore = -1;

                    if (highestScore < originalScore)
                    {
                        highestScore = originalScore;
                    }
                    if (highestScore < schoolYearAdjustScore)
                    {
                        highestScore = schoolYearAdjustScore;
                        current_sign = schoolYearAdjustSign;
                    }
                    if (highestScore < betterScore)
                    {
                        highestScore = betterScore;
                        current_sign = manualAdjustSign;
                    }
                    if (highestScore < resitScore)
                    {
                        highestScore = resitScore;
                        current_sign = resitSign;
                    }

                    if (highestScore >= 0)
                    {
                        scoreBuilder.Append(current_sign + highestScore);
                    }

                    //判斷重修
                    if (retakeScore >= 0)
                    {
                        if (highestScore >= 0)
                            scoreBuilder.Append("/");
                        scoreBuilder.Append(retakeSign + retakeScore);
                    }

                    score = scoreBuilder.ToString();

                    #endregion

                    #region 判斷上下學期
                    string semesterString = "";
                    if (info.Semester == 1)
                        semesterString = "上";
                    else if (info.Semester == 2)
                        semesterString = "下";
                    else
                        semesterString = info.Semester.ToString();
                    #endregion

                    if (!studentSubjectScore[studentID][gradeYear].ContainsKey(subject))
                    {
                        #region 新增科目到資料中
                        studentSubjectScore[studentID][gradeYear].Add(subject, new Dictionary<string, string>());

                        studentSubjectScore[studentID][gradeYear][subject].Add("級別", info.Level.ToString());

                        studentSubjectScore[studentID][gradeYear][subject].Add(semesterString + "學期級別", info.Level);
                        studentSubjectScore[studentID][gradeYear][subject].Add(semesterString + "學期學年", info.SchoolYear.ToString());
                        studentSubjectScore[studentID][gradeYear][subject].Add(semesterString + "學期學分", info.Credit.ToString());
                        studentSubjectScore[studentID][gradeYear][subject].Add(semesterString + "學期分數", noScore ? score : "");
                        studentSubjectScore[studentID][gradeYear][subject].Add(semesterString + "學期必修", (info.Require) ? "必" : "選");
                        studentSubjectScore[studentID][gradeYear][subject].Add(semesterString + "學期通過", (info.Pass) ? "1" : "0");
                        studentSubjectScore[studentID][gradeYear][subject].Add(semesterString + "學期核心科目標示", sign);
                        studentSubjectScore[studentID][gradeYear][subject].Add(semesterString + "學期未取得學分標示", (info.Pass) ? "" : failedSign);

                        #endregion
                    }
                    else
                    {
                        Dictionary<string, Dictionary<string, string>> gradeYearSubjects = studentSubjectScore[studentID][gradeYear];
                        InsertReduplicationSubject(gradeYearSubjects, info, info.Subject, semesterString, score, sign, failedSign, noScore);
                    }

                    #region 統計學分

                    if (!studentInfo[studentID].ContainsKey("畢業必修學分"))
                        studentInfo[studentID].Add("畢業必修學分", "0");
                    if (!studentInfo[studentID].ContainsKey("畢業選修學分"))
                        studentInfo[studentID].Add("畢業選修學分", "0");

                    if (info.Pass)
                    {
                        int requiredCredit = int.Parse(studentInfo[studentID]["畢業必修學分"]);
                        int optionalCredit = int.Parse(studentInfo[studentID]["畢業選修學分"]);

                        if (info.Require)
                            requiredCredit += info.Credit;
                        else
                            optionalCredit += info.Credit;

                        studentInfo[studentID]["畢業必修學分"] = requiredCredit + "";
                        studentInfo[studentID]["畢業選修學分"] = optionalCredit + "";
                    }

                    //統計學分
                    if (!studentCredit.ContainsKey(studentID))
                        studentCredit.Add(studentID, new Dictionary<string, Dictionary<string, int>>());

                    if (!studentCredit[studentID].ContainsKey(gradeYear))
                        studentCredit[studentID].Add(gradeYear, new Dictionary<string, int>());

                    if (!studentCredit[studentID][gradeYear].ContainsKey("實得學分"))
                        studentCredit[studentID][gradeYear].Add("實得學分", 0);
                    if (!studentCredit[studentID][gradeYear].ContainsKey("應得學分"))
                        studentCredit[studentID][gradeYear].Add("應得學分", 0);

                    if (info.Semester == 1)
                    {
                        if (!studentCredit[studentID][gradeYear].ContainsKey("上學期實得學分"))
                            studentCredit[studentID][gradeYear].Add("上學期實得學分", 0);
                        if (!studentCredit[studentID][gradeYear].ContainsKey("上學期應得學分"))
                            studentCredit[studentID][gradeYear].Add("上學期應得學分", 0);

                        studentCredit[studentID][gradeYear]["上學期實得學分"] += (info.Pass) ? info.Credit : 0;
                        studentCredit[studentID][gradeYear]["上學期應得學分"] += info.Credit;

                        studentCredit[studentID][gradeYear]["實得學分"] += (info.Pass) ? info.Credit : 0;
                        studentCredit[studentID][gradeYear]["應得學分"] += info.Credit;
                    }
                    else
                    {
                        if (!studentCredit[studentID][gradeYear].ContainsKey("下學期實得學分"))
                            studentCredit[studentID][gradeYear].Add("下學期實得學分", 0);
                        if (!studentCredit[studentID][gradeYear].ContainsKey("下學期應得學分"))
                            studentCredit[studentID][gradeYear].Add("下學期應得學分", 0);

                        studentCredit[studentID][gradeYear]["下學期實得學分"] += (info.Pass) ? info.Credit : 0;
                        studentCredit[studentID][gradeYear]["下學期應得學分"] += info.Credit;

                        studentCredit[studentID][gradeYear]["實得學分"] += (info.Pass) ? info.Credit : 0;
                        studentCredit[studentID][gradeYear]["應得學分"] += info.Credit;
                    }

                    #endregion
                }

                //學年科目成績
                foreach (SchoolYearSubjectScoreInfo info in var.SchoolYearSubjectScoreList)
                {
                    string studentID = var.StudentID;
                    string gradeYear = info.GradeYear.ToString();
                    string subject = info.Subject;

                    if (!studentSubjectScore[studentID].ContainsKey(gradeYear))
                        studentSubjectScore[studentID].Add(gradeYear, new Dictionary<string, Dictionary<string, string>>());
                    if (studentSubjectScore[studentID][gradeYear].ContainsKey(subject))
                    {
                        //計算學年學分
                        int schoolYearCredit = 0;
                        schoolYearCredit = CalcSchoolYearCredit(studentSubjectScore[studentID][gradeYear], subject, schoolYearCredit);

                        studentSubjectScore[studentID][gradeYear][subject]["學年分數"] = info.Score.ToString();
                        studentSubjectScore[studentID][gradeYear][subject]["學年學分"] = schoolYearCredit.ToString();
                    }

                }

                #endregion

                #region 分項成績

                //加入分項成績
                if (!studentEntryScore.ContainsKey(var.StudentID))
                    studentEntryScore.Add(var.StudentID, new Dictionary<string, Dictionary<string, Dictionary<string, string>>>());

                SemesterEntryRating rating = new SemesterEntryRating(var);

                //學期分項成績
                foreach (SemesterEntryScoreInfo info in var.SemesterEntryScoreList)
                {
                    string studentID = var.StudentID;
                    string gradeYear = info.GradeYear.ToString();
                    string entry = info.Entry;

                    if (!studentEntryScore[studentID].ContainsKey(gradeYear))
                    {
                        studentEntryScore[studentID].Add(gradeYear, new Dictionary<string, Dictionary<string, string>>());

                        //偷偷加入名次，再偷偷加入功能。
                        studentEntryScore[studentID][gradeYear].Add("學業成績名次", new Dictionary<string, string>());
                        studentEntryScore[studentID][gradeYear]["學業成績名次"].Add("上學期名次", rating.GetPlace(gradeYear, "1"));
                        studentEntryScore[studentID][gradeYear]["學業成績名次"].Add("下學期名次", rating.GetPlace(gradeYear, "2"));
                        studentEntryScore[studentID][gradeYear]["學業成績名次"].Add("學年名次", rating.GetPlace(gradeYear));
                    }
                    if (!studentEntryScore[studentID][gradeYear].ContainsKey(entry))
                        studentEntryScore[studentID][gradeYear].Add(entry, new Dictionary<string, string>());

                    if (info.Semester == 1)
                    {
                        studentEntryScore[studentID][gradeYear][entry].Add("上學期分數", info.Score.ToString());
                        studentEntryScore[studentID][gradeYear][entry].Add("上學期學年", info.SchoolYear.ToString());
                    }
                    else
                    {
                        studentEntryScore[studentID][gradeYear][entry].Add("下學期分數", info.Score.ToString());
                        studentEntryScore[studentID][gradeYear][entry].Add("下學期學年", info.SchoolYear.ToString());
                    }
                }

                //學年分項成績
                foreach (SchoolYearEntryScoreInfo info in var.SchoolYearEntryScoreList)
                {
                    string studentID = var.StudentID;
                    string gradeYear = info.GradeYear.ToString();
                    string entry = info.Entry;

                    if (!studentEntryScore[studentID].ContainsKey(gradeYear))
                        studentEntryScore[studentID].Add(gradeYear, new Dictionary<string, Dictionary<string, string>>());
                    if (studentEntryScore[studentID][gradeYear].ContainsKey(entry))
                    {
                        studentEntryScore[studentID][gradeYear][entry]["學年分數"] = info.Score.ToString();
                    }
                }

                #endregion

                _BGWStudentRecord.ReportProgress((int)(currentStudent++ * 50.0 / totalStudent));
            }

            #endregion

            #region 產生報表並寫入資料

            Document template = new Document();
            template = new Document(selectedTemplate, "", LoadFormat.Doc, "");
            DocumentBuilder updateRecordBuilder = new DocumentBuilder(template);
            DocumentBuilder builder = new DocumentBuilder(template);

            currentStudent = 1;

            foreach (string studentID in studentInfo.Keys)
            {
                Document eachStudentDoc = new Document();
                eachStudentDoc.Sections.Clear();
                eachStudentDoc.Sections.Add(eachStudentDoc.ImportNode(template.Sections[0], true));

                #region 合併列印

                List<string> keys = new List<string>();
                List<object> values = new List<object>();

                keys.Add("學校名稱"); //包含學校代碼: ex. 耀明高中 (611004)
                values.Add(SmartSchool.Customization.Data.SystemInformation.SchoolChineseName + " (" + SmartSchool.Customization.Data.SystemInformation.SchoolCode + ") ");


                //合併列印，基本資料
                foreach (string var in studentInfo[studentID].Keys)
                {
                    keys.Add(var);
                    values.Add(studentInfo[studentID][var]);
                }

                //合併列印，入學畢業
                foreach (string var in mergeField[studentID].Keys)
                {
                    keys.Add(var);
                    values.Add(mergeField[studentID][var]);
                }

                //合併列印，異動資料
                keys.Add("異動");
                if (studentUpdateRecord.ContainsKey(studentID))
                {
                    object[] objectValues = new object[] { studentUpdateRecord[studentID] };
                    values.Add(objectValues);
                }
                else
                {
                    values.Add(null);
                }

                //合併列印，成績     1, 2 是高中   3, 4 是高職
                if (templateNumber <= 2)
                    keys.Add("高中成績");
                else
                    keys.Add("高職成績");

                object objectSubjectScore = null;
                if (studentSubjectScore.ContainsKey(studentID))
                    objectSubjectScore = studentSubjectScore[studentID];

                object objectEntryScore = null;
                if (studentEntryScore.ContainsKey(studentID))
                    objectEntryScore = studentEntryScore[studentID];

                object objectCredit = null;
                if (studentCredit.ContainsKey(studentID))
                    objectCredit = studentCredit[studentID];

                object[] objectValues2 = new object[] { objectSubjectScore, objectEntryScore, objectCredit, over100, retakeSign };

                values.Add(objectValues2);

                //合併列印，證明書字號
                keys.Add("證明書字");
                values.Add(text1);
                keys.Add("證明書流水號");
                if (!string.IsNullOrEmpty(text2))
                {
                    string serialString = serialNumber.ToString();
                    while (serialString.Length < text2.Length)
                        serialString = "0" + serialString;
                    values.Add(serialString);
                    serialNumber++;
                }
                else
                    values.Add("");

                //合併入學照片
                keys.Add("照片");
                values.Add(studentPhoto[studentID]);

                //合併畢業成績
                keys.Add("畢業學業成績");
                keys.Add("畢業體育成績");
                keys.Add("畢業國防通識成績");
                keys.Add("畢業德行成績");
                values.Add(studentInfo[studentID].ContainsKey("畢業學業成績") ? studentInfo[studentID]["畢業學業成績"] : "");
                values.Add(studentInfo[studentID].ContainsKey("畢業體育成績") ? studentInfo[studentID]["畢業體育成績"] : "");
                values.Add(studentInfo[studentID].ContainsKey("畢業國防通識成績") ? studentInfo[studentID]["畢業國防通識成績"] : "");
                values.Add(studentInfo[studentID].ContainsKey("畢業德行成績") ? studentInfo[studentID]["畢業德行成績"] : "");

                //合併學分
                keys.Add("畢業必修學分");
                keys.Add("畢業選修學分");
                values.Add(studentInfo[studentID].ContainsKey("畢業必修學分") ? studentInfo[studentID]["畢業必修學分"] : "");
                values.Add(studentInfo[studentID].ContainsKey("畢業選修學分") ? studentInfo[studentID]["畢業選修學分"] : "");
                keys.Add("畢業學分");
                if (studentInfo[studentID].ContainsKey("畢業必修學分") && studentInfo[studentID].ContainsKey("畢業選修學分"))
                    values.Add(int.Parse(studentInfo[studentID]["畢業必修學分"]) + int.Parse(studentInfo[studentID]["畢業選修學分"]) + "");
                else
                    values.Add("");

                //合併列印
                eachStudentDoc.MailMerge.MergeField += new Aspose.Words.Reporting.MergeFieldEventHandler(MailMerge_MergeField);
                eachStudentDoc.MailMerge.RemoveEmptyParagraphs = true;
                eachStudentDoc.MailMerge.Execute(keys.ToArray(), values.ToArray());

                #endregion

                doc.Sections.Add(doc.ImportNode(eachStudentDoc.Sections[0], true));

                #region 清除已經合併的資料
                if (studentInfo.ContainsKey(studentID))
                    studentInfo[studentID].Clear();
                if (studentUpdateRecord.ContainsKey(studentID))
                    studentUpdateRecord[studentID].Clear();
                if (studentSubjectScore.ContainsKey(studentID))
                    studentSubjectScore[studentID].Clear();
                if (studentEntryScore.ContainsKey(studentID))
                    studentEntryScore[studentID].Clear();
                if (studentCredit.ContainsKey(studentID))
                    studentCredit[studentID].Clear();
                #endregion

                _BGWStudentRecord.ReportProgress((int)(currentStudent++ * 50.0 / totalStudent) + 50);
            }
            #endregion

            e.Result = doc;
        }

        private void InsertReduplicationSubject(Dictionary<string, Dictionary<string, string>> gradeYearSubjects, SemesterSubjectScoreInfo info, string subject, string semesterString, string score, string sign, string failedSign, bool noScore)
        {
            if (gradeYearSubjects[subject].ContainsKey(semesterString + "學期分數") &&
                    !string.IsNullOrEmpty(gradeYearSubjects[subject][semesterString + "學期分數"]))
            {
                string newSubject = subject + " ";
                if (!gradeYearSubjects.ContainsKey(newSubject))
                {
                    gradeYearSubjects.Add(newSubject, new Dictionary<string, string>());

                    gradeYearSubjects[newSubject].Add("級別", info.Level.ToString());

                    gradeYearSubjects[newSubject].Add(semesterString + "學期級別", info.Level);
                    gradeYearSubjects[newSubject].Add(semesterString + "學期學年", info.SchoolYear.ToString());
                    gradeYearSubjects[newSubject].Add(semesterString + "學期學分", info.Credit.ToString());
                    gradeYearSubjects[newSubject].Add(semesterString + "學期分數", noScore ? score : "");
                    gradeYearSubjects[newSubject].Add(semesterString + "學期必修", (info.Require) ? "必" : "選");
                    gradeYearSubjects[newSubject].Add(semesterString + "學期通過", (info.Pass) ? "1" : "0");
                    gradeYearSubjects[newSubject].Add(semesterString + "學期核心科目標示", sign);
                    gradeYearSubjects[newSubject].Add(semesterString + "學期未取得學分標示", (info.Pass) ? "" : failedSign);
                }
                else
                {
                    InsertReduplicationSubject(gradeYearSubjects, info, newSubject, semesterString, score, sign, failedSign, noScore);
                }
            }
            else
            {
                if (semesterString == "上")
                    gradeYearSubjects[subject]["級別"] = info.Level.ToString() + "," + gradeYearSubjects[subject]["級別"];
                else if (semesterString == "下")
                    gradeYearSubjects[subject]["級別"] = gradeYearSubjects[subject]["級別"] + "," + info.Level.ToString();

                gradeYearSubjects[subject][semesterString + "學期級別"] = info.Level;
                gradeYearSubjects[subject][semesterString + "學期學年"] = info.SchoolYear.ToString();
                gradeYearSubjects[subject][semesterString + "學期學分"] = info.Credit.ToString();
                gradeYearSubjects[subject][semesterString + "學期分數"] = score;
                gradeYearSubjects[subject][semesterString + "學期必修"] = (info.Require) ? "必" : "選";
                gradeYearSubjects[subject][semesterString + "學期通過"] = (info.Pass) ? "1" : "0";
                gradeYearSubjects[subject][semesterString + "學期核心科目標示"] = sign;
                gradeYearSubjects[subject][semesterString + "學期未取得學分標示"] = (info.Pass) ? "" : failedSign;
            }
        }

        private int CalcSchoolYearCredit(Dictionary<string, Dictionary<string, string>> gradeYearSubjects, string subject, int schoolYearCredit)
        {
            if (gradeYearSubjects[subject].ContainsKey("上學期學分"))
                schoolYearCredit += int.Parse(gradeYearSubjects[subject]["上學期學分"]);
            if (gradeYearSubjects[subject].ContainsKey("下學期學分"))
                schoolYearCredit += int.Parse(gradeYearSubjects[subject]["下學期學分"]);

            if (gradeYearSubjects.ContainsKey(subject + " "))
                return CalcSchoolYearCredit(gradeYearSubjects, subject + " ", schoolYearCredit);
            else
                return schoolYearCredit;
        }

        private void MailMerge_MergeField(object sender, Aspose.Words.Reporting.MergeFieldEventArgs e)
        {
            #region 畢業證書
            if (e.FieldName == "畢業證書")
            {
                DocumentBuilder builder = new DocumentBuilder(e.Document);
                builder.MoveToField(e.Field, true);
                e.Field.Remove();

                string dn = e.FieldValue as string;
                if (dn.Length > 3 && dn.Substring(dn.Length - 3, 3) == "***")
                {
                    builder.Font.Color = System.Drawing.Color.Gray;
                    dn = dn.Substring(0, dn.Length - 3);
                }
                builder.Write(dn);
            }
            #endregion

            #region 身分
            if (e.FieldName == "身分")
            {
                double std = 11.0;
                double count = 0.0;
                int resize = 0;
                string categories = e.FieldValue as string;
                foreach (char var in categories.ToCharArray())
                {
                    if (var == ',')
                        count += 0.25;
                    else
                        count += 1.0;
                }
                if ((count - std) > 0)
                    resize = (int)Math.Ceiling((count - std) / 2);

                DocumentBuilder builder = new DocumentBuilder(e.Document);
                builder.MoveToField(e.Field, true);
                e.Field.Remove();

                if (resize > 0 && resize < 9)
                    builder.Font.Size -= resize;
                builder.Write(e.FieldValue as string);
            }
            #endregion

            #region 異動
            if (e.FieldName == "異動")
            {
                object[] objectValues = (object[])e.FieldValue;
                List<Dictionary<string, string>> studentUpdateRecord = (List<Dictionary<string, string>>)objectValues[0];

                DocumentBuilder builder = new DocumentBuilder(e.Document);

                #region 產生異動表格

                int URrowNumber = 5;
                if (studentUpdateRecord.Count > URrowNumber)
                    URrowNumber = studentUpdateRecord.Count;

                builder.MoveToField(e.Field, false);
                string fontName = builder.CurrentParagraph.Runs[0].Font.Name;
                Cell URCell = (Cell)builder.CurrentParagraph.ParentNode;

                double URwidth = URCell.CellFormat.Width;
                double URHeight = (URCell.ParentNode as Row).RowFormat.Height;

                double URrowHeight = (URHeight * 6.0 + 4.0) / (URrowNumber + 1.0);

                builder.StartTable();
                builder.CellFormat.ClearFormatting();
                builder.RowFormat.HeightRule = HeightRule.Exactly;
                builder.RowFormat.Height = URrowHeight;
                builder.RowFormat.Alignment = RowAlignment.Center;
                builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                builder.CellFormat.LeftPadding = 0.0;
                builder.CellFormat.RightPadding = 0.0;
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                builder.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
                builder.ParagraphFormat.LineSpacing = 10;
                builder.Font.Size = 10;
                builder.Font.Name = fontName;

                builder.InsertCell().CellFormat.Width = 55.0;
                builder.Write("異動日期");
                builder.InsertCell().CellFormat.Width = 175.0;
                builder.Write("異動事項");
                builder.InsertCell().CellFormat.Width = URwidth - 230.0;
                builder.Write("異動核准文號");
                builder.EndRow();

                for (int i = 0; i < URrowNumber; i++)
                {
                    builder.InsertCell().CellFormat.Width = 55.0;
                    builder.InsertCell().CellFormat.Width = 175.0;
                    builder.InsertCell().CellFormat.Width = URwidth - 230.0;
                    builder.EndRow();
                }

                Table URtable = builder.EndTable();

                foreach (Cell cell in URtable.FirstRow.Cells)
                    cell.CellFormat.Borders.Top.LineStyle = LineStyle.None;

                foreach (Cell cell in URtable.LastRow.Cells)
                    cell.CellFormat.Borders.Bottom.LineStyle = LineStyle.None;

                foreach (Row row in URtable.Rows)
                {
                    row.FirstCell.CellFormat.Borders.Left.LineStyle = LineStyle.None;
                    row.LastCell.CellFormat.Borders.Right.LineStyle = LineStyle.None;
                }

                #endregion

                #region 寫入異動資料

                int URrowIndex = 1;
                foreach (Dictionary<string, string> var in studentUpdateRecord)
                {
                    URtable.Rows[URrowIndex].Cells[0].Paragraphs[0].Runs.Add(new Run(e.Document, var["異動日期"]));
                    URtable.Rows[URrowIndex].Cells[0].Paragraphs[0].Runs[0].Font.Size = 8;
                    URtable.Rows[URrowIndex].Cells[0].Paragraphs[0].Runs[0].Font.Name = fontName;
                    URtable.Rows[URrowIndex].Cells[1].Paragraphs[0].Runs.Add(new Run(e.Document, var["異動事項"]));
                    URtable.Rows[URrowIndex].Cells[1].Paragraphs[0].Runs[0].Font.Size = 8;
                    URtable.Rows[URrowIndex].Cells[1].Paragraphs[0].Runs[0].Font.Name = fontName;
                    URtable.Rows[URrowIndex].Cells[2].Paragraphs[0].Runs.Add(new Run(e.Document, var["異動核准文號"]));
                    URtable.Rows[URrowIndex].Cells[2].Paragraphs[0].Runs[0].Font.Size = 8;
                    URtable.Rows[URrowIndex].Cells[2].Paragraphs[0].Runs[0].Font.Name = fontName;
                    URrowIndex++;
                }
                #endregion

                e.Text = string.Empty;
            }
            #endregion

            #region 高中成績
            if (e.FieldName == "高中成績")
            {
                object[] objectValues = (object[])e.FieldValue;
                Dictionary<string, Dictionary<string, Dictionary<string, string>>> studentSubjectScore = (Dictionary<string, Dictionary<string, Dictionary<string, string>>>)objectValues[0];
                Dictionary<string, Dictionary<string, Dictionary<string, string>>> studentEntryScore = (Dictionary<string, Dictionary<string, Dictionary<string, string>>>)objectValues[1];
                Dictionary<string, Dictionary<string, int>> studentCredit = (Dictionary<string, Dictionary<string, int>>)objectValues[2];
                bool over100 = (bool)objectValues[3];
                string retakeSign = (string)objectValues[4];

                List<string> wroteSubjectList = new List<string>();
                DocumentBuilder builder = new DocumentBuilder(e.Document);

                #region 產生高中成績表格
                Table table = null;

                int subjectRowNumber = 25;
                int entryRowNumber = 7;
                int year = 3;

                //檢查學生年級
                if (studentSubjectScore.Count > 3)
                    year = studentSubjectScore.Count;

                //檢查學生科目
                int maxSubject = 0;
                foreach (string gradeYear in studentSubjectScore.Keys)
                {
                    if (studentSubjectScore[gradeYear].Count > maxSubject)
                        maxSubject = studentSubjectScore[gradeYear].Count;
                }
                if (maxSubject > subjectRowNumber)
                    subjectRowNumber = maxSubject;

                //檢查學生分項
                int maxEntry = 0;
                foreach (string gradeYear in studentEntryScore.Keys)
                {
                    if (studentEntryScore[gradeYear].Count > maxSubject)
                        maxEntry = studentEntryScore[gradeYear].Count;
                }
                if (maxEntry > 7)
                    entryRowNumber = maxEntry;

                builder.MoveToField(e.Field, false);
                string fontName = builder.CurrentParagraph.Runs[0].Font.Name; //取得目前範本上的字型
                int totalRow = subjectRowNumber + entryRowNumber + 4;
                int yearColumn = 10;
                Cell SCell = (Cell)builder.CurrentParagraph.ParentNode;
                double Swidth = SCell.CellFormat.Width;
                double SHeight = (SCell.ParentNode as Row).RowFormat.Height;

                // 20080502 阿寶
                // microUnit 加上 0.12 是為了更精細的調整欄寬，以至於上下格子不會歪掉。
                double microUnit = (Swidth / (year * yearColumn)) + 0.12;
                double rowHeight = (double)(SHeight / totalRow);

                table = builder.StartTable();

                builder.CellFormat.ClearFormatting();
                builder.CellFormat.Borders.LineWidth = 0.5;

                builder.RowFormat.HeightRule = HeightRule.Exactly;
                builder.RowFormat.Height = rowHeight;
                builder.RowFormat.Alignment = RowAlignment.Center;
                builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                builder.CellFormat.LeftPadding = 3.0;
                builder.CellFormat.RightPadding = 3.0;
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                builder.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
                builder.ParagraphFormat.LineSpacing = 10;
                builder.Font.Size = 8;
                builder.Font.Name = fontName;

                //builder.InsertCell().CellFormat.Width = microUnit * 10;
                //for (int i = 1; i < year; i++)
                //{
                //    builder.InsertCell();
                //}
                //builder.EndRow();

                for (int i = 0; i < year; i++)
                {
                    builder.InsertCell().CellFormat.Width = microUnit * 4;
                    builder.CellFormat.Borders.Right.LineWidth = 0.25;
                    builder.Write("科目");
                    builder.CellFormat.VerticalMerge = CellMerge.First;
                    builder.InsertCell().CellFormat.Width = microUnit * 2;
                    builder.CellFormat.VerticalMerge = CellMerge.None;
                    builder.InsertCell();
                    builder.InsertCell();
                    builder.Write("學年成績");
                    builder.CellFormat.Borders.Right.LineWidth = 1.5;
                }

                builder.EndRow();

                for (int i = 0; i < year; i++)
                {
                    builder.InsertCell().CellFormat.Width = microUnit * 4;
                    builder.CellFormat.Borders.Right.LineWidth = 0.25;
                    builder.CellFormat.VerticalMerge = CellMerge.Previous;
                    builder.InsertCell().CellFormat.Width = microUnit;
                    builder.CellFormat.VerticalMerge = CellMerge.None;
                    builder.Write("學分");
                    builder.InsertCell();
                    builder.Write("分數");
                    builder.InsertCell();
                    builder.Write("學分");
                    builder.InsertCell();
                    builder.Write("分數");
                    builder.InsertCell();
                    builder.Write("學分");
                    builder.InsertCell();
                    builder.Write("分數");
                    builder.CellFormat.Borders.Right.LineWidth = 1.5;
                }

                builder.EndRow();

                for (int j = 0; j < subjectRowNumber; j++)
                {
                    for (int i = 0; i < year; i++)
                    {
                        builder.InsertCell().CellFormat.Width = microUnit * 4;
                        builder.CellFormat.Borders.Right.LineWidth = 0.25;
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                        builder.InsertCell().CellFormat.Width = microUnit;
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Right;
                        builder.InsertCell();
                        builder.InsertCell();
                        builder.InsertCell();
                        builder.InsertCell();
                        builder.InsertCell();
                        builder.CellFormat.Borders.Right.LineWidth = 1.5;
                    }
                    builder.EndRow();
                }

                for (int j = 0; j < entryRowNumber; j++)
                {
                    for (int i = 0; i < year; i++)
                    {
                        builder.InsertCell().CellFormat.Width = microUnit * 4;
                        builder.CellFormat.Borders.Right.LineWidth = 0.25;
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                        builder.InsertCell().CellFormat.Width = microUnit;
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Right;
                        builder.InsertCell();
                        builder.InsertCell();
                        builder.InsertCell();
                        builder.InsertCell();
                        builder.InsertCell();
                        builder.CellFormat.Borders.Right.LineWidth = 1.5;
                    }
                    builder.EndRow();
                }

                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;

                for (int i = 0; i < year; i++)
                {
                    builder.InsertCell().CellFormat.Width = microUnit * 4;
                    builder.Write("實得學分 / 應得學分");
                    builder.CellFormat.Borders.Right.LineWidth = 0.25;
                    builder.InsertCell().CellFormat.Width = microUnit * 2;
                    builder.InsertCell();
                    builder.InsertCell();
                    builder.CellFormat.Borders.Right.LineWidth = 1.5;
                }
                builder.EndRow();

                for (int i = 0; i < year; i++)
                {
                    builder.InsertCell().CellFormat.Width = microUnit * 8;
                    builder.Write("累計實得學分 / 累計應得學分");
                    builder.CellFormat.Borders.Right.LineWidth = 0.25;
                    builder.InsertCell().CellFormat.Width = microUnit * 2;
                    builder.CellFormat.Borders.Right.LineWidth = 1.5;
                }
                builder.EndRow();

                builder.EndTable();

                //加上邊線
                foreach (Cell cell in table.Rows[2].Cells)
                    cell.CellFormat.Borders.Top.LineWidth = 1.5;

                foreach (Cell cell in table.Rows[subjectRowNumber + 2].Cells)
                {
                    cell.CellFormat.Borders.Top.LineStyle = LineStyle.Double;
                }

                //去除表格四邊的線
                foreach (Cell cell in table.FirstRow.Cells)
                    cell.CellFormat.Borders.Top.LineStyle = LineStyle.None;

                foreach (Cell cell in table.LastRow.Cells)
                    cell.CellFormat.Borders.Bottom.LineStyle = LineStyle.None;

                foreach (Row row in table.Rows)
                {
                    row.FirstCell.CellFormat.Borders.Left.LineStyle = LineStyle.None;
                    row.LastCell.CellFormat.Borders.Right.LineStyle = LineStyle.None;
                }
                //foreach (Cell cell in table.FirstRow.Cells)
                //    cell.CellFormat.Borders.Top.LineWidth = 1.5;

                //foreach (Cell cell in table.LastRow.Cells)
                //    cell.CellFormat.Borders.Bottom.LineWidth = 1.5;

                //foreach (Row row in table.Rows)
                //{
                //    row.FirstCell.CellFormat.Borders.Left.LineStyle = LineStyle.None;
                //    row.LastCell.CellFormat.Borders.Right.LineWidth = 1.5;
                //}
                #endregion

                #region 寫入高中成績

                int scoreRowIndex = 2;
                int scoreColIndex = 0;
                int semesterColIndex = 0;
                int creditColIndex = 0;
                int totalCreditIndex = 0;

                //寫入科目成績
                if (studentSubjectScore != null)
                {
                    List<string> gradeYearList = new List<string>(studentSubjectScore.Keys);
                    gradeYearList.Sort();

                    foreach (string gradeYear in gradeYearList)
                    {
                        List<string> sortSubject = new List<string>();
                        foreach (string subject in studentSubjectScore[gradeYear].Keys)
                        {
                            sortSubject.Add(subject);
                        }
                        sortSubject.Sort(CommonMethod.SortBySubjectName);

                        //填寫學年度學期
                        bool firstFilled = false;
                        bool secondFilled = false;

                        foreach (string subject in sortSubject)
                        {
                            Dictionary<string, string> subjectDict = studentSubjectScore[gradeYear][subject];
                            Dictionary<string, string> firstSubjectDict;
                            if (studentSubjectScore[gradeYear].ContainsKey(subject.Trim()))
                                firstSubjectDict = studentSubjectScore[gradeYear][subject.Trim()];
                            else
                                firstSubjectDict = studentSubjectScore[gradeYear][subject];

                            //記錄出現過的科目與級別
                            int semesterIndex = 0;
                            foreach (string each_level in subjectDict["級別"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                if (!wroteSubjectList.Contains(subject.Trim() + " >_< " + each_level))
                                    wroteSubjectList.Add(subject.Trim() + " >_< " + each_level);
                                else
                                {
                                    if (semesterIndex == 0 && subjectDict.ContainsKey("上學期分數"))
                                        subjectDict["上學期分數"] = retakeSign + subjectDict["上學期分數"];
                                    else if (semesterIndex == 1 && subjectDict.ContainsKey("下學期分數"))
                                        subjectDict["下學期分數"] = retakeSign + subjectDict["下學期分數"];
                                }
                                semesterIndex++;
                            }

                            if (!firstFilled && subjectDict.ContainsKey("上學期學年"))
                            {
                                table.Rows[0].Cells[semesterColIndex + 1].Paragraphs[0].Runs.Add(new Run(e.Document, subjectDict["上學期學年"] + "上學期"));
                                table.Rows[0].Cells[semesterColIndex + 1].Paragraphs[0].Runs[0].Font.Size = 8;
                                table.Rows[0].Cells[semesterColIndex + 1].Paragraphs[0].Runs[0].Font.Name = fontName;
                                firstFilled = true;
                            }

                            if (!secondFilled && subjectDict.ContainsKey("下學期學年"))
                            {
                                table.Rows[0].Cells[semesterColIndex + 2].Paragraphs[0].Runs.Add(new Run(e.Document, subjectDict["下學期學年"] + "下學期"));
                                table.Rows[0].Cells[semesterColIndex + 2].Paragraphs[0].Runs[0].Font.Size = 8;
                                table.Rows[0].Cells[semesterColIndex + 2].Paragraphs[0].Runs[0].Font.Name = fontName;
                                secondFilled = true;
                            }

                            string level = "";
                            foreach (char eachChar in subjectDict["級別"].ToCharArray())
                            {
                                int c;
                                if (int.TryParse(eachChar.ToString(), out c))
                                    level += CommonMethod.GetNumber(c);
                                else
                                    level += eachChar.ToString();
                            }
                            Run run = new Run(e.Document);
                            run.Font.Size = 8;
                            run.Font.Name = fontName;
                            run.Text = subject.Trim() + " " + level;
                            table.Rows[scoreRowIndex].Cells[scoreColIndex].Paragraphs[0].Runs.Add(run.Clone(true));

                            run.Text = subjectDict.ContainsKey("上學期學分") ? subjectDict["上學期核心科目標示"] + subjectDict["上學期未取得學分標示"] + subjectDict["上學期必修"] + subjectDict["上學期學分"] : "";
                            table.Rows[scoreRowIndex].Cells[scoreColIndex + 1].Paragraphs[0].Runs.Add(run.Clone(true));

                            run.Text = subjectDict.ContainsKey("上學期分數") ? subjectDict["上學期分數"] : "";
                            table.Rows[scoreRowIndex].Cells[scoreColIndex + 2].Paragraphs[0].Runs.Add(run.Clone(true));

                            run.Text = subjectDict.ContainsKey("下學期學分") ? subjectDict["下學期核心科目標示"] + subjectDict["下學期未取得學分標示"] + subjectDict["下學期必修"] + subjectDict["下學期學分"] : "";
                            table.Rows[scoreRowIndex].Cells[scoreColIndex + 3].Paragraphs[0].Runs.Add(run.Clone(true));

                            run.Text = subjectDict.ContainsKey("下學期分數") ? subjectDict["下學期分數"] : "";
                            table.Rows[scoreRowIndex].Cells[scoreColIndex + 4].Paragraphs[0].Runs.Add(run.Clone(true));


                            run.Text = firstSubjectDict.ContainsKey("學年學分") ? firstSubjectDict["學年學分"] : "";
                            table.Rows[scoreRowIndex].Cells[scoreColIndex + 5].Paragraphs[0].Runs.Add(run.Clone(true));

                            run.Text = firstSubjectDict.ContainsKey("學年分數") ? firstSubjectDict["學年分數"] : "";
                            table.Rows[scoreRowIndex].Cells[scoreColIndex + 6].Paragraphs[0].Runs.Add(run.Clone(true));

                            scoreRowIndex++;
                        }
                        scoreRowIndex = 2;
                        scoreColIndex += 7;
                        semesterColIndex += 4;
                    }
                }

                //寫入分項成績

                scoreRowIndex = subjectRowNumber + 2;
                scoreColIndex = 0;

                if (studentEntryScore != null)
                {
                    List<string> gradeYearList = new List<string>(studentEntryScore.Keys);
                    gradeYearList.Sort();

                    foreach (string gradeYear in gradeYearList)
                    {
                        List<string> sortEntry = new List<string>();
                        foreach (string subject in studentEntryScore[gradeYear].Keys)
                        {
                            sortEntry.Add(subject);
                        }
                        sortEntry.Sort(CommonMethod.SortByEntryName);

                        foreach (string entry in sortEntry)
                        {
                            Dictionary<string, string> entryDict = studentEntryScore[gradeYear][entry];
                            Run run = new Run(e.Document);
                            run.Font.Size = 8;
                            run.Font.Name = fontName;

                            if (entry == "德行" && !over100)
                            {
                                if (entryDict.ContainsKey("上學期分數") && decimal.Parse(entryDict["上學期分數"]) > 100)
                                    entryDict["上學期分數"] = "100";
                                if (entryDict.ContainsKey("下學期分數") && decimal.Parse(entryDict["下學期分數"]) > 100)
                                    entryDict["下學期分數"] = "100";
                                if (entryDict.ContainsKey("學年分數") && decimal.Parse(entryDict["學年分數"]) > 100)
                                    entryDict["學年分數"] = "100";
                            }

                            run.Text = entry;
                            table.Rows[scoreRowIndex].Cells[scoreColIndex].Paragraphs[0].Runs.Add(run.Clone(true));

                            string scoreOrRank = "分數";

                            if (entry == "學業成績名次")
                                scoreOrRank = "名次";

                            run.Text = entryDict.ContainsKey("上學期" + scoreOrRank) ? entryDict["上學期" + scoreOrRank] : "";
                            table.Rows[scoreRowIndex].Cells[scoreColIndex + 2].Paragraphs[0].Runs.Add(run.Clone(true));

                            run.Text = entryDict.ContainsKey("下學期" + scoreOrRank) ? entryDict["下學期" + scoreOrRank] : "";
                            table.Rows[scoreRowIndex].Cells[scoreColIndex + 4].Paragraphs[0].Runs.Add(run.Clone(true));

                            run.Text = entryDict.ContainsKey("學年" + scoreOrRank) ? entryDict["學年" + scoreOrRank] : "";
                            table.Rows[scoreRowIndex].Cells[scoreColIndex + 6].Paragraphs[0].Runs.Add(run.Clone(true));

                            scoreRowIndex++;
                        }
                        scoreRowIndex = subjectRowNumber + 2;
                        scoreColIndex += 7;
                    }
                }

                //寫入實得學分/應得學分

                scoreRowIndex = subjectRowNumber + entryRowNumber + 2;

                int totalPassCredit = 0;
                int totalAllCredit = 0;

                if (studentCredit != null)
                {
                    List<string> gradeYearList = new List<string>(studentCredit.Keys);
                    gradeYearList.Sort();

                    foreach (string gradeYear in gradeYearList)
                    {
                        Dictionary<string, int> creditDict = studentCredit[gradeYear];

                        if (creditDict.ContainsKey("上學期應得學分"))
                        {
                            table.Rows[scoreRowIndex].Cells[creditColIndex + 1].Paragraphs[0].Runs.Add(new Run(e.Document, creditDict["上學期實得學分"] + " / " + creditDict["上學期應得學分"]));
                            table.Rows[scoreRowIndex].Cells[creditColIndex + 1].Paragraphs[0].Runs[0].Font.Size = 8;
                            table.Rows[scoreRowIndex].Cells[creditColIndex + 1].Paragraphs[0].Runs[0].Font.Name = fontName;
                        }
                        if (creditDict.ContainsKey("下學期實得學分"))
                        {
                            table.Rows[scoreRowIndex].Cells[creditColIndex + 2].Paragraphs[0].Runs.Add(new Run(e.Document, creditDict["下學期實得學分"] + " / " + creditDict["下學期應得學分"]));
                            table.Rows[scoreRowIndex].Cells[creditColIndex + 2].Paragraphs[0].Runs[0].Font.Size = 8;
                            table.Rows[scoreRowIndex].Cells[creditColIndex + 2].Paragraphs[0].Runs[0].Font.Name = fontName;
                        }
                        if (creditDict.ContainsKey("實得學分"))
                        {
                            table.Rows[scoreRowIndex].Cells[creditColIndex + 3].Paragraphs[0].Runs.Add(new Run(e.Document, creditDict["實得學分"] + " / " + creditDict["應得學分"]));
                            table.Rows[scoreRowIndex].Cells[creditColIndex + 3].Paragraphs[0].Runs[0].Font.Size = 8;
                            table.Rows[scoreRowIndex].Cells[creditColIndex + 3].Paragraphs[0].Runs[0].Font.Name = fontName;

                            totalPassCredit += creditDict["實得學分"];
                            totalAllCredit += creditDict["應得學分"];

                            table.Rows[scoreRowIndex + 1].Cells[totalCreditIndex + 1].Paragraphs[0].Runs.Add(new Run(e.Document, totalPassCredit + " / " + totalAllCredit));
                            table.Rows[scoreRowIndex + 1].Cells[totalCreditIndex + 1].Paragraphs[0].Runs[0].Font.Size = 8;
                            table.Rows[scoreRowIndex + 1].Cells[totalCreditIndex + 1].Paragraphs[0].Runs[0].Font.Name = fontName;
                        }
                        creditColIndex += 4;
                        totalCreditIndex += 2;

                    }
                }

                #endregion

                e.Text = string.Empty;
            }
            #endregion

            #region 高職成績
            if (e.FieldName == "高職成績")
            {
                object[] objectValues = (object[])e.FieldValue;
                Dictionary<string, Dictionary<string, Dictionary<string, string>>> studentSubjectScore = (Dictionary<string, Dictionary<string, Dictionary<string, string>>>)objectValues[0];
                Dictionary<string, Dictionary<string, Dictionary<string, string>>> studentEntryScore = (Dictionary<string, Dictionary<string, Dictionary<string, string>>>)objectValues[1];
                Dictionary<string, Dictionary<string, int>> studentCredit = (Dictionary<string, Dictionary<string, int>>)objectValues[2];
                bool over100 = (bool)objectValues[3];
                string retakeSign = (string)objectValues[4];

                List<string> wroteSubjectList = new List<string>();
                DocumentBuilder builder = new DocumentBuilder(e.Document);

                #region 產生高職成績表格

                Table table = null;

                int subjectRowNumber = 25;
                int entryRowNumber = 7;
                int semester = 6;

                //檢查學生年級
                if (studentSubjectScore.Count > 3)
                    semester = studentSubjectScore.Count * 2;

                //檢查學生科目
                int maxSubject = 0;
                foreach (string gradeYear in studentSubjectScore.Keys)
                {
                    if (studentSubjectScore[gradeYear].Count > maxSubject)
                        maxSubject = studentSubjectScore[gradeYear].Count;
                }
                if (maxSubject > subjectRowNumber)
                    subjectRowNumber = maxSubject;

                //檢查學生分項
                int maxEntry = 0;
                foreach (string gradeYear in studentEntryScore.Keys)
                {
                    if (studentEntryScore[gradeYear].Count > maxEntry)
                        maxEntry = studentEntryScore[gradeYear].Count;
                }
                if (maxEntry >= 7)
                {
                    entryRowNumber = maxEntry + 1;
                }

                builder.MoveToField(e.Field, false);
                string fontName = builder.CurrentParagraph.Runs[0].Font.Name; //取得目前範本上的字型
                int totalRow = subjectRowNumber + entryRowNumber + 4;
                int yearColumn = 5;

                Cell SCell = (Cell)builder.CurrentParagraph.ParentNode;
                double Swidth = SCell.CellFormat.Width;
                double SHeight = (SCell.ParentNode as Row).RowFormat.Height;

                double microUnit = Swidth / (semester * yearColumn);
                double rowHeight = (double)(SHeight / totalRow);

                table = builder.StartTable();

                builder.CellFormat.ClearFormatting();
                builder.CellFormat.Borders.LineWidth = 0.5;

                builder.RowFormat.HeightRule = HeightRule.Exactly;
                builder.RowFormat.Height = rowHeight;
                builder.RowFormat.Alignment = RowAlignment.Center;
                builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                builder.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
                builder.ParagraphFormat.LineSpacing = 10;
                builder.Font.Size = 8;
                builder.Font.Name = fontName;

                for (int i = 0; i < semester; i++)
                {
                    builder.InsertCell().CellFormat.Width = microUnit * 5;
                    builder.CellFormat.Borders.Right.LineWidth = 1.0;
                    builder.CellFormat.Borders.Right.LineStyle = LineStyle.Double;
                }

                //builder.InsertCell().CellFormat.Width = microUnit * 5;
                //builder.Write("延修成績");
                //builder.CellFormat.Borders.Right.LineWidth = 1.5;
                //builder.CellFormat.Borders.Right.LineStyle = LineStyle.Double;

                builder.EndRow();

                for (int i = 0; i < semester; i++)
                {
                    builder.InsertCell().CellFormat.Width = microUnit * 3;
                    builder.CellFormat.LeftPadding = 3.0;
                    builder.CellFormat.RightPadding = 3.0;
                    builder.CellFormat.Borders.Right.LineWidth = 0.25;
                    builder.CellFormat.Borders.Right.LineStyle = LineStyle.Single;
                    builder.Write("科目");
                    builder.InsertCell().CellFormat.Width = microUnit;
                    builder.CellFormat.LeftPadding = 0.0;
                    builder.CellFormat.RightPadding = 0.0;
                    builder.Write("學分");
                    builder.InsertCell();
                    builder.Write("分數");
                    builder.CellFormat.Borders.Right.LineWidth = 1.0;
                    builder.CellFormat.Borders.Right.LineStyle = LineStyle.Double;
                }
                builder.RowFormat.Height = rowHeight * 2;
                builder.EndRow();
                builder.RowFormat.Height = rowHeight;
                builder.CellFormat.LeftPadding = 3.0;
                builder.CellFormat.RightPadding = 3.0;

                for (int j = 0; j < subjectRowNumber; j++)
                {
                    for (int i = 0; i < semester; i++)
                    {
                        builder.InsertCell().CellFormat.Width = microUnit * 3;
                        //builder.CellFormat.LeftPadding = 0.19;
                        //builder.CellFormat.RightPadding = 0.19;
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                        builder.CellFormat.Borders.Right.LineWidth = 0.25;
                        builder.CellFormat.Borders.Right.LineStyle = LineStyle.Single;
                        builder.InsertCell().CellFormat.Width = microUnit;
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Right;
                        builder.InsertCell();
                        builder.CellFormat.Borders.Right.LineWidth = 1.0;
                        builder.CellFormat.Borders.Right.LineStyle = LineStyle.Double;
                    }
                    builder.EndRow();
                }

                for (int j = 0; j < entryRowNumber; j++)
                {
                    for (int i = 0; i < semester; i++)
                    {
                        builder.InsertCell().CellFormat.Width = microUnit * 3;
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                        builder.CellFormat.Borders.Right.LineWidth = 0.25;
                        builder.CellFormat.Borders.Right.LineStyle = LineStyle.Single;
                        builder.InsertCell().CellFormat.Width = microUnit;
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Right;
                        builder.InsertCell();
                        builder.CellFormat.Borders.Right.LineWidth = 1.0;
                        builder.CellFormat.Borders.Right.LineStyle = LineStyle.Double;
                    }
                    builder.EndRow();
                }

                for (int i = 0; i < semester; i++)
                {
                    builder.InsertCell().CellFormat.Width = microUnit * 3;
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                    builder.Write("實得/應得學分");
                    builder.CellFormat.Borders.Right.LineWidth = 0.25;
                    builder.CellFormat.Borders.Right.LineStyle = LineStyle.Single;
                    builder.InsertCell().CellFormat.Width = microUnit * 2;
                    builder.CellFormat.Borders.Right.LineWidth = 1.0;
                    builder.CellFormat.Borders.Right.LineStyle = LineStyle.Double;
                }
                builder.EndRow();

                for (int i = 0; i < semester; i++)
                {
                    builder.InsertCell().CellFormat.Width = microUnit * 3;
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                    builder.Write("實得/應得累計學分");
                    builder.CellFormat.Borders.Right.LineWidth = 0.25;
                    builder.CellFormat.Borders.Right.LineStyle = LineStyle.Single;
                    builder.InsertCell().CellFormat.Width = microUnit * 2;
                    builder.CellFormat.Borders.Right.LineWidth = 1.0;
                    builder.CellFormat.Borders.Right.LineStyle = LineStyle.Double;
                }
                builder.EndRow();

                builder.EndTable();


                //加上邊線
                foreach (Cell cell in table.Rows[2].Cells)
                    cell.CellFormat.Borders.Top.LineWidth = 1.5;

                foreach (Cell cell in table.Rows[subjectRowNumber + 2].Cells)
                {
                    cell.CellFormat.Borders.Top.LineStyle = LineStyle.Double;
                }

                //去除表格四邊的線
                foreach (Cell cell in table.FirstRow.Cells)
                    cell.CellFormat.Borders.Top.LineStyle = LineStyle.None;

                foreach (Cell cell in table.LastRow.Cells)
                    cell.CellFormat.Borders.Bottom.LineStyle = LineStyle.None;

                foreach (Row row in table.Rows)
                {
                    row.FirstCell.CellFormat.Borders.Left.LineStyle = LineStyle.None;
                    row.LastCell.CellFormat.Borders.Right.LineStyle = LineStyle.None;
                }

                #endregion

                #region 寫入高職成績

                int scoreRowIndex = 2;
                int scoreRowIndex2 = 2;
                int scoreColIndex = 0;
                int semesterColIndex = 0;
                int creditColIndex = 0;
                int totalCreditIndex = 0;

                //寫入科目成績
                if (studentSubjectScore != null)
                {
                    List<string> gradeYearList = new List<string>(studentSubjectScore.Keys);
                    gradeYearList.Sort();

                    foreach (string gradeYear in gradeYearList)
                    {
                        List<string> sortSubject = new List<string>();
                        foreach (string subject in studentSubjectScore[gradeYear].Keys)
                        {
                            sortSubject.Add(subject);
                        }
                        sortSubject.Sort(CommonMethod.SortBySubjectName);

                        //填寫學年度學期
                        bool firstFilled = false;
                        bool secondFilled = false;

                        foreach (string subject in sortSubject)
                        {
                            Dictionary<string, string> subjectDict = studentSubjectScore[gradeYear][subject];

                            //記錄出現過的科目與級別
                            int semesterIndex = 0;
                            foreach (string each_level in subjectDict["級別"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                if (!wroteSubjectList.Contains(subject.Trim() + " >_< " + each_level))
                                    wroteSubjectList.Add(subject.Trim() + " >_< " + each_level);
                                else
                                {
                                    if (semesterIndex == 0 && subjectDict.ContainsKey("上學期分數"))
                                        subjectDict["上學期分數"] = retakeSign + subjectDict["上學期分數"];
                                    else if (semesterIndex == 1 && subjectDict.ContainsKey("下學期分數"))
                                        subjectDict["下學期分數"] = retakeSign + subjectDict["下學期分數"];
                                }
                                semesterIndex++;
                            }

                            if (!firstFilled && subjectDict.ContainsKey("上學期學年"))
                            {
                                table.Rows[0].Cells[semesterColIndex].Paragraphs[0].Runs.Add(new Run(e.Document, subjectDict["上學期學年"] + "上學期"));
                                table.Rows[0].Cells[semesterColIndex].Paragraphs[0].Runs[0].Font.Size = 8;
                                table.Rows[0].Cells[semesterColIndex].Paragraphs[0].Runs[0].Font.Name = fontName;
                                firstFilled = true;
                            }

                            if (!secondFilled && subjectDict.ContainsKey("下學期學年"))
                            {
                                table.Rows[0].Cells[semesterColIndex + 1].Paragraphs[0].Runs.Add(new Run(e.Document, subjectDict["下學期學年"] + "下學期"));
                                table.Rows[0].Cells[semesterColIndex + 1].Paragraphs[0].Runs[0].Font.Size = 8;
                                table.Rows[0].Cells[semesterColIndex + 1].Paragraphs[0].Runs[0].Font.Name = fontName;
                                secondFilled = true;
                            }

                            if (subjectDict.ContainsKey("上學期分數"))
                            {
                                table.Rows[scoreRowIndex].Cells[scoreColIndex].Paragraphs[0].Runs.Add(new Run(e.Document, subject.Trim() + " " + (subjectDict.ContainsKey("上學期級別") && !string.IsNullOrEmpty(subjectDict["上學期級別"]) ? CommonMethod.GetNumber(int.Parse(subjectDict["上學期級別"])) : "")));
                                table.Rows[scoreRowIndex].Cells[scoreColIndex].Paragraphs[0].Runs[0].Font.Size = 8;
                                table.Rows[scoreRowIndex].Cells[scoreColIndex].Paragraphs[0].Runs[0].Font.Name = fontName;
                                table.Rows[scoreRowIndex].Cells[scoreColIndex + 1].Paragraphs[0].Runs.Add(new Run(e.Document, subjectDict.ContainsKey("上學期學分") ? subjectDict["上學期核心科目標示"] + subjectDict["上學期未取得學分標示"] + subjectDict["上學期必修"] + subjectDict["上學期學分"] : ""));
                                table.Rows[scoreRowIndex].Cells[scoreColIndex + 1].Paragraphs[0].Runs[0].Font.Size = 8;
                                table.Rows[scoreRowIndex].Cells[scoreColIndex + 1].Paragraphs[0].Runs[0].Font.Name = fontName;
                                table.Rows[scoreRowIndex].Cells[scoreColIndex + 2].Paragraphs[0].Runs.Add(new Run(e.Document, subjectDict["上學期分數"]));
                                table.Rows[scoreRowIndex].Cells[scoreColIndex + 2].Paragraphs[0].Runs[0].Font.Size = 8;
                                table.Rows[scoreRowIndex].Cells[scoreColIndex + 2].Paragraphs[0].Runs[0].Font.Name = fontName;
                                scoreRowIndex++;
                            }

                            if (subjectDict.ContainsKey("下學期分數"))
                            {
                                table.Rows[scoreRowIndex2].Cells[scoreColIndex + 3].Paragraphs[0].Runs.Add(new Run(e.Document, subject.Trim() + " " + (subjectDict.ContainsKey("下學期級別") && !string.IsNullOrEmpty(subjectDict["下學期級別"]) ? CommonMethod.GetNumber(int.Parse(subjectDict["下學期級別"])) : "")));
                                table.Rows[scoreRowIndex2].Cells[scoreColIndex + 3].Paragraphs[0].Runs[0].Font.Size = 8;
                                table.Rows[scoreRowIndex2].Cells[scoreColIndex + 3].Paragraphs[0].Runs[0].Font.Name = fontName;
                                table.Rows[scoreRowIndex2].Cells[scoreColIndex + 4].Paragraphs[0].Runs.Add(new Run(e.Document, subjectDict.ContainsKey("下學期學分") ? subjectDict["下學期核心科目標示"] + subjectDict["下學期未取得學分標示"] + subjectDict["下學期必修"] + subjectDict["下學期學分"] : ""));
                                table.Rows[scoreRowIndex2].Cells[scoreColIndex + 4].Paragraphs[0].Runs[0].Font.Size = 8;
                                table.Rows[scoreRowIndex2].Cells[scoreColIndex + 4].Paragraphs[0].Runs[0].Font.Name = fontName;
                                table.Rows[scoreRowIndex2].Cells[scoreColIndex + 5].Paragraphs[0].Runs.Add(new Run(e.Document, subjectDict["下學期分數"]));
                                table.Rows[scoreRowIndex2].Cells[scoreColIndex + 5].Paragraphs[0].Runs[0].Font.Size = 8;
                                table.Rows[scoreRowIndex2].Cells[scoreColIndex + 5].Paragraphs[0].Runs[0].Font.Name = fontName;
                                scoreRowIndex2++;
                            }
                        }
                        scoreRowIndex = 2;
                        scoreRowIndex2 = 2;
                        scoreColIndex += 6;
                        semesterColIndex += 2;
                    }
                }

                //寫入分項成績

                scoreRowIndex = subjectRowNumber + 2;
                scoreColIndex = 0;

                if (studentEntryScore != null)
                {
                    List<string> gradeYearList = new List<string>(studentEntryScore.Keys);
                    gradeYearList.Sort();

                    foreach (string gradeYear in gradeYearList)
                    {
                        List<string> sortEntry = new List<string>();
                        foreach (string subject in studentEntryScore[gradeYear].Keys)
                        {
                            sortEntry.Add(subject);
                        }
                        sortEntry.Sort(CommonMethod.SortByEntryName);

                        foreach (string entry in sortEntry)
                        {

                            Dictionary<string, string> entryDict = studentEntryScore[gradeYear][entry];

                            if (entry == "德行" && !over100)
                            {
                                if (entryDict.ContainsKey("上學期分數") && decimal.Parse(entryDict["上學期分數"]) > 100)
                                    entryDict["上學期分數"] = "100";
                                if (entryDict.ContainsKey("下學期分數") && decimal.Parse(entryDict["下學期分數"]) > 100)
                                    entryDict["下學期分數"] = "100";
                                if (entryDict.ContainsKey("學年分數") && decimal.Parse(entryDict["學年分數"]) > 100)
                                    entryDict["學年分數"] = "100";
                            }

                            if (entry == "學業成績名次")
                            {
                                if (entryDict.ContainsKey("上學期名次") && !string.IsNullOrEmpty(entryDict["上學期名次"]))
                                {
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex].Paragraphs[0].Runs.Add(new Run(e.Document, entry));
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex].Paragraphs[0].Runs[0].Font.Size = 8;
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex].Paragraphs[0].Runs[0].Font.Name = fontName;
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex + 2].Paragraphs[0].Runs.Add(new Run(e.Document, entryDict.ContainsKey("上學期名次") ? entryDict["上學期名次"] : ""));
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex + 2].Paragraphs[0].Runs[0].Font.Size = 8;
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex + 2].Paragraphs[0].Runs[0].Font.Name = fontName;
                                }
                                if (entryDict.ContainsKey("下學期名次") && !string.IsNullOrEmpty(entryDict["下學期名次"]))
                                {
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex + 3].Paragraphs[0].Runs.Add(new Run(e.Document, entry));
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex + 3].Paragraphs[0].Runs[0].Font.Size = 8;
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex + 3].Paragraphs[0].Runs[0].Font.Name = fontName;
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex + 5].Paragraphs[0].Runs.Add(new Run(e.Document, entryDict.ContainsKey("下學期名次") ? entryDict["下學期名次"] : ""));
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex + 5].Paragraphs[0].Runs[0].Font.Size = 8;
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex + 5].Paragraphs[0].Runs[0].Font.Name = fontName;
                                }
                            }
                            else
                            {
                                if (entryDict.ContainsKey("上學期分數"))
                                {
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex].Paragraphs[0].Runs.Add(new Run(e.Document, entry));
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex].Paragraphs[0].Runs[0].Font.Size = 8;
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex].Paragraphs[0].Runs[0].Font.Name = fontName;
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex + 2].Paragraphs[0].Runs.Add(new Run(e.Document, entryDict.ContainsKey("上學期分數") ? entryDict["上學期分數"] : ""));
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex + 2].Paragraphs[0].Runs[0].Font.Size = 8;
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex + 2].Paragraphs[0].Runs[0].Font.Name = fontName;
                                }
                                if (entryDict.ContainsKey("下學期分數"))
                                {
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex + 3].Paragraphs[0].Runs.Add(new Run(e.Document, entry));
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex + 3].Paragraphs[0].Runs[0].Font.Size = 8;
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex + 3].Paragraphs[0].Runs[0].Font.Name = fontName;
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex + 5].Paragraphs[0].Runs.Add(new Run(e.Document, entryDict.ContainsKey("下學期分數") ? entryDict["下學期分數"] : ""));
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex + 5].Paragraphs[0].Runs[0].Font.Size = 8;
                                    table.Rows[scoreRowIndex].Cells[scoreColIndex + 5].Paragraphs[0].Runs[0].Font.Name = fontName;

                                    if (entry == "德行")
                                    {
                                        scoreRowIndex++;
                                        table.Rows[scoreRowIndex].Cells[scoreColIndex + 3].Paragraphs[0].Runs.Add(new Run(e.Document, "學年德行"));
                                        table.Rows[scoreRowIndex].Cells[scoreColIndex + 3].Paragraphs[0].Runs[0].Font.Size = 8;
                                        table.Rows[scoreRowIndex].Cells[scoreColIndex + 3].Paragraphs[0].Runs[0].Font.Name = fontName;
                                        table.Rows[scoreRowIndex].Cells[scoreColIndex + 5].Paragraphs[0].Runs.Add(new Run(e.Document, entryDict.ContainsKey("學年分數") ? entryDict["學年分數"] : ""));
                                        table.Rows[scoreRowIndex].Cells[scoreColIndex + 5].Paragraphs[0].Runs[0].Font.Size = 8;
                                        table.Rows[scoreRowIndex].Cells[scoreColIndex + 5].Paragraphs[0].Runs[0].Font.Name = fontName;
                                    }
                                }
                            }
                            scoreRowIndex++;
                        }
                        scoreRowIndex = subjectRowNumber + 2;
                        scoreColIndex += 6;
                    }
                }

                //寫入實得學分/應得學分

                scoreRowIndex = subjectRowNumber + entryRowNumber + 2;

                int totalPassCredit = 0;
                int totalAllCredit = 0;

                if (studentCredit != null)
                {
                    List<string> gradeYearList = new List<string>(studentCredit.Keys);
                    gradeYearList.Sort();

                    foreach (string gradeYear in gradeYearList)
                    {
                        Dictionary<string, int> creditDict = studentCredit[gradeYear];

                        if (creditDict.ContainsKey("上學期應得學分"))
                        {
                            table.Rows[scoreRowIndex].Cells[creditColIndex + 1].Paragraphs[0].Runs.Add(new Run(e.Document, creditDict["上學期實得學分"] + " / " + creditDict["上學期應得學分"]));
                            table.Rows[scoreRowIndex].Cells[creditColIndex + 1].Paragraphs[0].Runs[0].Font.Size = 8;
                            table.Rows[scoreRowIndex].Cells[creditColIndex + 1].Paragraphs[0].Runs[0].Font.Name = fontName;

                            totalPassCredit += creditDict["上學期實得學分"];
                            totalAllCredit += creditDict["上學期應得學分"];

                            table.Rows[scoreRowIndex + 1].Cells[totalCreditIndex + 1].Paragraphs[0].Runs.Add(new Run(e.Document, totalPassCredit + " / " + totalAllCredit));
                            table.Rows[scoreRowIndex + 1].Cells[totalCreditIndex + 1].Paragraphs[0].Runs[0].Font.Size = 8;
                            table.Rows[scoreRowIndex + 1].Cells[totalCreditIndex + 1].Paragraphs[0].Runs[0].Font.Name = fontName;
                        }

                        if (creditDict.ContainsKey("下學期應得學分"))
                        {
                            table.Rows[scoreRowIndex].Cells[creditColIndex + 3].Paragraphs[0].Runs.Add(new Run(e.Document, creditDict["下學期實得學分"] + " / " + creditDict["下學期應得學分"]));
                            table.Rows[scoreRowIndex].Cells[creditColIndex + 3].Paragraphs[0].Runs[0].Font.Size = 8;
                            table.Rows[scoreRowIndex].Cells[creditColIndex + 3].Paragraphs[0].Runs[0].Font.Name = fontName;

                            totalPassCredit += creditDict["下學期實得學分"];
                            totalAllCredit += creditDict["下學期應得學分"];

                            table.Rows[scoreRowIndex + 1].Cells[totalCreditIndex + 3].Paragraphs[0].Runs.Add(new Run(e.Document, totalPassCredit + " / " + totalAllCredit));
                            table.Rows[scoreRowIndex + 1].Cells[totalCreditIndex + 3].Paragraphs[0].Runs[0].Font.Size = 8;
                            table.Rows[scoreRowIndex + 1].Cells[totalCreditIndex + 3].Paragraphs[0].Runs[0].Font.Name = fontName;
                        }
                        creditColIndex += 4;
                        totalCreditIndex += 4;
                    }
                }

                #endregion

                e.Text = string.Empty;

            }
            #endregion

            #region 照片
            if (e.FieldName == "照片")
            {
                byte[] photoBytes = e.FieldValue as byte[];
                if (photoBytes == null)
                    return;
                DocumentBuilder builder = new DocumentBuilder(e.Document);
                builder.MoveToField(e.Field, true);
                e.Field.Remove();

                Shape photoShape = new Shape(e.Document, ShapeType.Image);
                photoShape.ImageData.SetImage(photoBytes);
                photoShape.WrapType = WrapType.Inline;

                #region AutoResize

                double origHWRate = photoShape.ImageData.ImageSize.HeightPoints / photoShape.ImageData.ImageSize.WidthPoints;
                double shapeHeight = (builder.CurrentParagraph.ParentNode.ParentNode as Row).RowFormat.Height;
                double shapeWidth = (builder.CurrentParagraph.ParentNode as Cell).CellFormat.Width;
                if ((shapeHeight / shapeWidth) < origHWRate)
                    shapeWidth = shapeHeight / origHWRate;
                else
                    shapeHeight = shapeWidth * origHWRate;

                #endregion

                photoShape.Height = shapeHeight;
                photoShape.Width = shapeWidth;

                builder.InsertNode(photoShape);
            }
            #endregion
        }

        /// <summary>
        /// 只處理學業成績的排名。
        /// </summary>
        class SemesterEntryRating
        {
            private XmlElement _sems_ratings = null, _year_ratings = null;

            public SemesterEntryRating(StudentRecord student)
            {
                if (student.Fields.ContainsKey("SemesterEntryClassRating"))
                    _sems_ratings = student.Fields["SemesterEntryClassRating"] as XmlElement;

                if (student.Fields.ContainsKey("SchoolYearEntryClassRating"))
                    _year_ratings = student.Fields["SchoolYearEntryClassRating"] as XmlElement;
            }

            public string GetPlace(string gradeYear, string semester)
            {
                if (_sems_ratings == null) return string.Empty;

                string path = string.Format("SemesterEntryScore[GradeYear='{0}' and Semester='{1}']/ClassRating/Rating/Item[@分項='學業']/@排名", gradeYear, semester);
                XmlNode result = _sems_ratings.SelectSingleNode(path);

                if (result != null)
                    return result.InnerText;
                else
                    return string.Empty;
            }

            public string GetPlace(string gradeYear)
            {
                if (_year_ratings == null) return string.Empty;

                string path = string.Format("SchoolYearEntryScore[GradeYear='{0}']/ClassRating/Rating/Item[@分項='學業']/@排名", gradeYear);
                XmlNode result = _year_ratings.SelectSingleNode(path);

                if (result != null)
                    return result.InnerText;
                else
                    return string.Empty;
            }
        }
    }
}