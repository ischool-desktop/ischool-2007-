using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using IntelliSchool.DSA30.Util;
using System.Xml;
using System.Drawing;

namespace SmartSchool.ClassRelated.RibbonBars.Reports
{
    internal class DisciplineNotificationReport
    {
        private BackgroundWorker _BGWDisciplineNotification;

        public DisciplineNotificationReport()
        {
            DisciplineNotificationSelectDateRangeForm form = new DisciplineNotificationSelectDateRangeForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                MotherForm.Instance.SetBarMessage("正在初始化獎懲通知單...");

                object[] args = new object[] { form.StartDate, form.EndDate, form.PrintHasRecordOnly, form.Template, form.ReceiveName, form.ReceiveAddress, form.ConditionName, form.ConditionNumber };

                _BGWDisciplineNotification = new BackgroundWorker();
                _BGWDisciplineNotification.DoWork += new DoWorkEventHandler(_BGWDisciplineNotification_DoWork);
                _BGWDisciplineNotification.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CommonMethods.WordReport_RunWorkerCompleted);
                _BGWDisciplineNotification.ProgressChanged += new ProgressChangedEventHandler(CommonMethods.Report_ProgressChanged);
                _BGWDisciplineNotification.WorkerReportsProgress = true;
                _BGWDisciplineNotification.RunWorkerAsync(args);
            }
        }

        private void _BGWDisciplineNotification_DoWork(object sender, DoWorkEventArgs e)
        {
            string reportName = "獎懲通知單";

            object[] args = e.Argument as object[];

            DateTime startDate = (DateTime)args[0];
            DateTime endDate = (DateTime)args[1];
            bool printHasRecordOnly = (bool)args[2];
            MemoryStream templateStream = (MemoryStream)args[3];
            string receiveName = (string)args[4];
            string receiveAddress = (string)args[5];
            string condName = (string)args[6];
            int condNumber = int.Parse((string)args[7]);

            Dictionary<string, int> MDMapping = new Dictionary<string, int>();
            MDMapping.Add("大功", 0);
            MDMapping.Add("小功", 1);
            MDMapping.Add("嘉獎", 2);
            MDMapping.Add("大過", 3);
            MDMapping.Add("小過", 4);
            MDMapping.Add("警告", 5);

            int flag = 2;
            if (!string.IsNullOrEmpty(condName))
                flag = (MDMapping[condName] < 3) ? 1 : 0;

            MDFilter filter = new MDFilter();
            if (flag < 2)
                filter.SetCondition(MDMapping[condName], condNumber);

            #region 快取資訊

            List<SmartSchool.ClassRelated.ClassInfo> selectedClass = SmartSchool.ClassRelated.Class.Instance.SelectionClasses;

            //學生資訊
            Dictionary<string, Dictionary<string, string>> studentInfo = new Dictionary<string, Dictionary<string, string>>();

            //獎懲累計資料
            Dictionary<string, Dictionary<string, int>> studentDiscipline = new Dictionary<string, Dictionary<string, int>>();

            //獎懲明細
            Dictionary<string, List<string>> studentDisciplineDetail = new Dictionary<string, List<string>>();

            //所有學生ID
            List<string> allStudentID = new List<string>();

            //學生人數
            int currentStudentCount = 1;
            int totalStudentNumber = 0;

            //獎勵項目
            Dictionary<string, string> meritTable = new Dictionary<string, string>();
            meritTable.Add("大功", "A");
            meritTable.Add("小功", "B");
            meritTable.Add("嘉獎", "C");

            //懲戒項目
            Dictionary<string, string> demeritTable = new Dictionary<string, string>();
            demeritTable.Add("大過", "A");
            demeritTable.Add("小過", "B");
            demeritTable.Add("警告", "C");

            //依據 ClassID 建立班級學生清單
            foreach (SmartSchool.ClassRelated.ClassInfo aClass in selectedClass)
            {
                List<SmartSchool.StudentRelated.BriefStudentData> classStudent = aClass.Students;

                foreach (SmartSchool.StudentRelated.BriefStudentData aStudent in classStudent)
                {
                    string aStudentID = aStudent.ID;

                    if (!studentInfo.ContainsKey(aStudentID))
                        studentInfo.Add(aStudentID, new Dictionary<string, string>());

                    studentInfo[aStudentID].Add("Name", aStudent.Name);
                    studentInfo[aStudentID].Add("ClassName", aClass.ClassName);
                    studentInfo[aStudentID].Add("SeatNo", aStudent.SeatNo);
                    studentInfo[aStudentID].Add("StudentNumber", aStudent.StudentNumber);
                    studentInfo[aStudentID].Add("Teacher", aClass.TeacherName);

                    if (!studentDiscipline.ContainsKey(aStudentID))
                        studentDiscipline.Add(aStudentID, new Dictionary<string, int>());
                    if (!studentDisciplineDetail.ContainsKey(aStudentID))
                        studentDisciplineDetail.Add(aStudentID, new List<string>());

                    if (!allStudentID.Contains(aStudentID))
                        allStudentID.Add(aStudentID);
                }
            }

            //取得獎懲資料 日期區間
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            foreach (string var in allStudentID)
            {
                helper.AddElement("Condition", "RefStudentID", var);
            }
            helper.AddElement("Condition", "StartDate", startDate.ToShortDateString());
            helper.AddElement("Condition", "EndDate", endDate.ToShortDateString());
            helper.AddElement("Order");
            helper.AddElement("Order", "OccurDate", "asc");
            DSResponse dsrsp = SmartSchool.Feature.Student.QueryDiscipline.GetDiscipline(new DSRequest(helper));

            foreach (XmlElement var in dsrsp.GetContent().GetElements("Discipline"))
            {
                string studentID = var.SelectSingleNode("RefStudentID").InnerText;
                DateTime occurDate = DateTime.Parse(var.SelectSingleNode("OccurDate").InnerText);
                string occurMonthDay = occurDate.Month + "/" + occurDate.Day;
                string reason = var.SelectSingleNode("Reason").InnerText;

                if (!studentDisciplineDetail.ContainsKey(studentID))
                    studentDisciplineDetail.Add(studentID, new List<string>());

                if (!studentDiscipline.ContainsKey(studentID))
                    studentDiscipline.Add(studentID, new Dictionary<string, int>());

                if (var.SelectSingleNode("MeritFlag").InnerText == "1")
                {
                    XmlElement meritElement = (XmlElement)var.SelectSingleNode("Detail/Discipline/Merit");
                    if (meritElement == null) continue;

                    bool comma = false;
                    StringBuilder detailString = new StringBuilder("");
                    detailString.Append(occurMonthDay + " ");
                    if (!string.IsNullOrEmpty(reason))
                        detailString.Append(reason + " ");

                    foreach (string merit in meritTable.Keys)
                    {
                        int tryTimes;
                        int times = int.TryParse(meritElement.GetAttribute(meritTable[merit]), out tryTimes) ? tryTimes : 0;
                        if (times > 0)
                        {
                            if (!studentDiscipline[studentID].ContainsKey("Range" + merit))
                                studentDiscipline[studentID].Add("Range" + merit, 0);
                            studentDiscipline[studentID]["Range" + merit] += times;
                            if (comma)
                                detailString.Append(",");
                            detailString.Append(merit + times + "次");
                            comma = true;
                        }
                    }

                    studentDisciplineDetail[studentID].Add(detailString.ToString());

                }
                else
                {
                    XmlElement demeritElement = (XmlElement)var.SelectSingleNode("Detail/Discipline/Demerit");
                    if (demeritElement == null) continue;

                    bool cleared = false;
                    if (demeritElement.GetAttribute("Cleared") == "是")
                        cleared = true;

                    bool comma = false;
                    StringBuilder detailString = new StringBuilder("");
                    detailString.Append(occurMonthDay + " ");
                    if (!string.IsNullOrEmpty(reason))
                        detailString.Append(reason + " ");

                    foreach (string demerit in demeritTable.Keys)
                    {
                        int tryTimes;
                        int times = int.TryParse(demeritElement.GetAttribute(demeritTable[demerit]), out tryTimes) ? tryTimes : 0;
                        if (times > 0)
                        {
                            if (!studentDiscipline[studentID].ContainsKey("Range" + demerit))
                                studentDiscipline[studentID].Add("Range" + demerit, 0);
                            if (!cleared)
                            {
                                studentDiscipline[studentID]["Range" + demerit] += times;
                                if (comma)
                                    detailString.Append(",");
                                detailString.Append(demerit + times + "次");
                                comma = true;
                            }
                        }
                    }

                    if (!cleared)
                        studentDisciplineDetail[studentID].Add(detailString.ToString());
                }
            }

            //取得獎懲資料 學期累計
            helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            foreach (string var in allStudentID)
            {
                helper.AddElement("Condition", "RefStudentID", var);
            }
            helper.AddElement("Condition", "SchoolYear", CurrentUser.Instance.SchoolYear.ToString());
            helper.AddElement("Condition", "Semester", CurrentUser.Instance.Semester.ToString());
            helper.AddElement("Order");
            helper.AddElement("Order", "OccurDate", "asc");
            dsrsp = SmartSchool.Feature.Student.QueryDiscipline.GetDiscipline(new DSRequest(helper));

            foreach (XmlElement var in dsrsp.GetContent().GetElements("Discipline"))
            {
                string studentID = var.SelectSingleNode("RefStudentID").InnerText;

                if (!studentDiscipline.ContainsKey(studentID))
                    studentDiscipline.Add(studentID, new Dictionary<string, int>());

                if (var.SelectSingleNode("MeritFlag").InnerText == "1")
                {
                    XmlElement meritElement = (XmlElement)var.SelectSingleNode("Detail/Discipline/Merit");
                    if (meritElement == null) continue;

                    foreach (string merit in meritTable.Keys)
                    {
                        int tryTimes;
                        int times = int.TryParse(meritElement.GetAttribute(meritTable[merit]), out tryTimes) ? tryTimes : 0;
                        if (times > 0)
                        {
                            if (!studentDiscipline[studentID].ContainsKey("Semester" + merit))
                                studentDiscipline[studentID].Add("Semester" + merit, 0);
                            studentDiscipline[studentID]["Semester" + merit] += times;
                        }

                    }
                }
                else
                {
                    XmlElement demeritElement = (XmlElement)var.SelectSingleNode("Detail/Discipline/Demerit");
                    if (demeritElement == null) continue;

                    bool cleared = false;
                    if (demeritElement.GetAttribute("Cleared") == "是")
                        cleared = true;

                    foreach (string demerit in demeritTable.Keys)
                    {
                        int tryTimes;
                        int times = int.TryParse(demeritElement.GetAttribute(demeritTable[demerit]), out tryTimes) ? tryTimes : 0;
                        if (times > 0)
                        {
                            if (!studentDiscipline[studentID].ContainsKey("Semester" + demerit))
                                studentDiscipline[studentID].Add("Semester" + demerit, 0);
                            if (!cleared)
                                studentDiscipline[studentID]["Semester" + demerit] += times;
                        }
                    }
                }
            }

            //取得學生通訊地址資料
            dsrsp = SmartSchool.Feature.QueryStudent.GetAddressWithID(allStudentID.ToArray());
            foreach (XmlElement var in dsrsp.GetContent().GetElements("Student"))
            {
                string studentID = var.GetAttribute("ID");

                XmlElement address = null;
                if (receiveAddress == "戶籍地址")
                    address = (XmlElement)var.SelectSingleNode("PermanentAddress/AddressList/Address");
                else if (receiveAddress == "聯絡地址")
                    address = (XmlElement)var.SelectSingleNode("MailingAddress/AddressList/Address");
                else if (receiveAddress == "其他地址")
                    address = (XmlElement)var.SelectSingleNode("OtherAddress/AddressList/Address");

                studentInfo[studentID].Add("Address", "");
                studentInfo[studentID].Add("ZipCode", "");
                studentInfo[studentID].Add("ZipCode1", "");
                studentInfo[studentID].Add("ZipCode2", "");
                studentInfo[studentID].Add("ZipCode3", "");

                if (address != null)
                {
                    string zipCode = address.SelectSingleNode("ZipCode").InnerText;
                    string addressString = address.SelectSingleNode("County").InnerText + address.SelectSingleNode("Town").InnerText + address.SelectSingleNode("DetailAddress").InnerText;

                    studentInfo[studentID]["Address"] = addressString;
                    studentInfo[studentID]["ZipCode"] = zipCode;
                    if (!string.IsNullOrEmpty(zipCode) && zipCode.Length >= 3)
                    {
                        studentInfo[studentID]["ZipCode1"] = zipCode.Substring(0, 1);
                        studentInfo[studentID]["ZipCode2"] = zipCode.Substring(1, 1);
                        studentInfo[studentID]["ZipCode3"] = zipCode.Substring(2, 1);
                    }
                }
            }

            //取得學生監護人父母親資料
            dsrsp = SmartSchool.Feature.QueryStudent.GetMultiParentInfo(allStudentID.ToArray());
            foreach (XmlElement var in dsrsp.GetContent().GetElements("ParentInfo"))
            {
                string studentID = var.GetAttribute("StudentID");

                studentInfo[studentID].Add("CustodianName", var.SelectSingleNode("CustodianName").InnerText);
                studentInfo[studentID].Add("FatherName", var.SelectSingleNode("FatherName").InnerText);
                studentInfo[studentID].Add("MotherName", var.SelectSingleNode("MotherName").InnerText);
            }

            #endregion

            #region 產生報表

            Aspose.Words.Document template = new Aspose.Words.Document(templateStream, "", Aspose.Words.LoadFormat.Doc, "");
            template.MailMerge.Execute(
                new string[] { "學校名稱", "學校地址", "學校電話" },
                new object[] { CurrentUser.Instance.SchoolInfo.ChineseName, CurrentUser.Instance.SchoolInfo.Address, CurrentUser.Instance.SchoolInfo.Telephone }
                );

            Aspose.Words.Document doc = new Aspose.Words.Document();
            doc.RemoveAllChildren();

            Aspose.Words.Node sectionNode = template.Sections[0].Clone();

            totalStudentNumber = studentDiscipline.Count;

            foreach (string student in studentDiscipline.Keys)
            {
                if (printHasRecordOnly)
                {
                    if (studentDisciplineDetail[student].Count == 0)
                        continue;
                }

                #region 過濾不需要列印的學生

                if (flag < 2)
                {
                    int A = 0, B = 0, C = 0;

                    if (flag == 1)
                    {
                        int tryM;
                        A = studentDiscipline[student].TryGetValue("Range大功", out tryM) ? tryM : 0;
                        B = studentDiscipline[student].TryGetValue("Range小功", out tryM) ? tryM : 0;
                        C = studentDiscipline[student].TryGetValue("Range嘉獎", out tryM) ? tryM : 0;
                    }
                    else if (flag == 0)
                    {
                        int tryD;
                        A = studentDiscipline[student].TryGetValue("Range大過", out tryD) ? tryD : 0;
                        B = studentDiscipline[student].TryGetValue("Range小過", out tryD) ? tryD : 0;
                        C = studentDiscipline[student].TryGetValue("Range警告", out tryD) ? tryD : 0;
                    }

                    if (filter.IsFilter(A, B, C))
                        continue;
                }

                #endregion

                Aspose.Words.Document eachDoc = new Aspose.Words.Document();
                eachDoc.RemoveAllChildren();
                eachDoc.Sections.Add(eachDoc.ImportNode(sectionNode, true));

                //合併列印的資料
                Dictionary<string, object> mapping = new Dictionary<string, object>();

                Dictionary<string, string> eachStudentInfo = studentInfo[student];

                //學生資料
                mapping.Add("學生姓名", eachStudentInfo["Name"]);
                mapping.Add("班級", eachStudentInfo["ClassName"]);
                mapping.Add("座號", eachStudentInfo["SeatNo"]);
                mapping.Add("學號", eachStudentInfo["StudentNumber"]);
                mapping.Add("導師", eachStudentInfo["Teacher"]);
                mapping.Add("資料期間", startDate.ToShortDateString() + " 至 " + endDate.ToShortDateString());

                //收件人資料
                if (receiveName == "監護人姓名")
                    mapping.Add("收件人姓名", eachStudentInfo["CustodianName"]);
                else if (receiveName == "父親姓名")
                    mapping.Add("收件人姓名", eachStudentInfo["FatherName"]);
                else if (receiveName == "母親姓名")
                    mapping.Add("收件人姓名", eachStudentInfo["MotherName"]);
                else
                    mapping.Add("收件人姓名", eachStudentInfo["Name"]);

                //收件人地址資料
                mapping.Add("收件人地址", eachStudentInfo["Address"]);
                mapping.Add("郵遞區號", eachStudentInfo["ZipCode"]);
                mapping.Add("郵遞區號第一碼", eachStudentInfo["ZipCode1"]);
                mapping.Add("郵遞區號第二碼", eachStudentInfo["ZipCode2"]);
                mapping.Add("郵遞區號第三碼", eachStudentInfo["ZipCode3"]);
                mapping.Add("郵遞區號第四碼", "");
                mapping.Add("郵遞區號第五碼", "");

                Dictionary<string, int> eachStudentDiscipline = studentDiscipline[student];

                //學生獎懲累計資料
                int count;
                mapping.Add("學期累計大功", eachStudentDiscipline.TryGetValue("Semester大功", out count) ? "" + count : "0");
                mapping.Add("學期累計小功", eachStudentDiscipline.TryGetValue("Semester小功", out count) ? "" + count : "0");
                mapping.Add("學期累計嘉獎", eachStudentDiscipline.TryGetValue("Semester嘉獎", out count) ? "" + count : "0");
                mapping.Add("學期累計大過", eachStudentDiscipline.TryGetValue("Semester大過", out count) ? "" + count : "0");
                mapping.Add("學期累計小過", eachStudentDiscipline.TryGetValue("Semester小過", out count) ? "" + count : "0");
                mapping.Add("學期累計警告", eachStudentDiscipline.TryGetValue("Semester警告", out count) ? "" + count : "0");
                mapping.Add("本期累計大功", eachStudentDiscipline.TryGetValue("Range大功", out count) ? "" + count : "0");
                mapping.Add("本期累計小功", eachStudentDiscipline.TryGetValue("Range小功", out count) ? "" + count : "0");
                mapping.Add("本期累計嘉獎", eachStudentDiscipline.TryGetValue("Range嘉獎", out count) ? "" + count : "0");
                mapping.Add("本期累計大過", eachStudentDiscipline.TryGetValue("Range大過", out count) ? "" + count : "0");
                mapping.Add("本期累計小過", eachStudentDiscipline.TryGetValue("Range小過", out count) ? "" + count : "0");
                mapping.Add("本期累計警告", eachStudentDiscipline.TryGetValue("Range警告", out count) ? "" + count : "0");

                //獎懲明細
                object[] objectValues = new object[] { studentDisciplineDetail[student] };
                mapping.Add("獎懲明細", objectValues);

                string[] keys = new string[mapping.Count];
                object[] values = new object[mapping.Count];
                int i = 0;
                foreach (string key in mapping.Keys)
                {
                    keys[i] = key;
                    values[i++] = mapping[key];
                }

                //合併列印
                eachDoc.MailMerge.MergeField += new Aspose.Words.Reporting.MergeFieldEventHandler(DisciplineNotification_MailMerge_MergeField);
                eachDoc.MailMerge.RemoveEmptyParagraphs = true;
                eachDoc.MailMerge.Execute(keys, values);

                Aspose.Words.Node eachSectionNode = eachDoc.Sections[0].Clone();
                doc.Sections.Add(doc.ImportNode(eachSectionNode, true));

                //回報進度
                _BGWDisciplineNotification.ReportProgress((int)(((double)currentStudentCount++ * 100.0) / (double)totalStudentNumber));
            }

            #endregion

            string path = Path.Combine(Application.StartupPath, "Reports");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, reportName + ".doc");
            e.Result = new object[] { reportName, path, doc };
        }

        private void DisciplineNotification_MailMerge_MergeField(object sender, Aspose.Words.Reporting.MergeFieldEventArgs e)
        {
            if (e.FieldName == "獎懲明細")
            {
                object[] objectValues = (object[])e.FieldValue;
                List<string> eachStudentDisciplineDetail = (List<string>)objectValues[0];

                Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(e.Document);

                builder.MoveToField(e.Field, false);
                builder.StartTable();
                builder.CellFormat.ClearFormatting();
                builder.CellFormat.Borders.ClearFormatting();
                builder.CellFormat.VerticalAlignment = Aspose.Words.CellVerticalAlignment.Center;
                builder.CellFormat.LeftPadding = 3.0;
                builder.RowFormat.LeftIndent = 0.0;
                builder.RowFormat.Height = 15.0;

                int rowNumber = 4;
                if (eachStudentDisciplineDetail.Count > rowNumber * 2)
                {
                    rowNumber = eachStudentDisciplineDetail.Count / 2;
                    rowNumber += eachStudentDisciplineDetail.Count % 2;
                }

                if (eachStudentDisciplineDetail.Count > rowNumber * 2)
                {
                    rowNumber += (eachStudentDisciplineDetail.Count - (rowNumber * 2)) / 2;
                    rowNumber += (eachStudentDisciplineDetail.Count - (rowNumber * 2)) % 2;
                }

                for (int j = 0; j < rowNumber; j++)
                {
                    builder.InsertCell();
                    builder.CellFormat.Borders.Right.LineStyle = Aspose.Words.LineStyle.Single;
                    builder.CellFormat.Borders.Right.Color = Color.Black;
                    if (j < eachStudentDisciplineDetail.Count)
                        builder.Write(eachStudentDisciplineDetail[j]);
                    builder.InsertCell();
                    if (j + rowNumber < eachStudentDisciplineDetail.Count)
                        builder.Write(eachStudentDisciplineDetail[j + rowNumber]);
                    builder.EndRow();
                }

                builder.EndTable();

                e.Text = string.Empty;
            }
        }

        /// <summary>
        /// 功過條件過濾
        /// </summary>
        class MDFilter
        {
            private int _condName = 0; //0:A, 1:B, 2:C
            private int _condNumber = 0;

            public MDFilter()
            {
            }

            public void SetCondition(int name, int number)
            {
                _condName = name;
                _condNumber = number;
            }

            public bool IsFilter(int A, int B, int C)
            {
                bool filtered = false;

                switch (_condName)
                {
                    case 5:
                    case 2:
                        if ((A + B) > 0)
                            filtered = false;
                        else if (C >= _condNumber)
                            filtered = false;
                        else
                            filtered = true;
                        break;
                    case 4:
                    case 1:
                        if (A > 0)
                            filtered = false;
                        else if (B >= _condNumber)
                            filtered = false;
                        else
                            filtered = true;
                        break;
                    case 3:
                    case 0:
                        if (A >= _condNumber)
                            filtered = false;
                        else
                            filtered = true;
                        break;
                    default:
                        break;
                }

                return filtered;
            }
        }
    }
}
