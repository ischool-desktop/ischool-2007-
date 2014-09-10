using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using IntelliSchool.DSA30.Util;
using Aspose.Words;
using System.Drawing;

namespace SmartSchool.ClassRelated.RibbonBars.Reports
{
    internal class AbsenceNotificationReport
    {
        private BackgroundWorker _BGWAbsenceNotification;

        public AbsenceNotificationReport()
        {
            AbsenceNotificationSelectDateRangeForm form = new AbsenceNotificationSelectDateRangeForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                //讀取缺曠別 Preference
                Dictionary<string, List<string>> config = new Dictionary<string, List<string>>();

                XmlElement preferenceData = CurrentUser.Instance.Preference["缺曠通知單_缺曠別設定"];

                if (preferenceData != null)
                {
                    foreach (XmlElement type in preferenceData.SelectNodes("Type"))
                    {
                        string prefix = type.GetAttribute("Text");
                        if (!config.ContainsKey(prefix))
                            config.Add(prefix, new List<string>());

                        foreach (XmlElement absence in type.SelectNodes("Absence"))
                        {
                            if (!config[prefix].Contains(absence.GetAttribute("Text")))
                                config[prefix].Add(absence.GetAttribute("Text"));
                        }
                    }
                }

                MotherForm.Instance.SetBarMessage("正在初始化缺曠通知單...");

                object[] args = new object[] { form.StartDate, form.EndDate, form.PrintHasRecordOnly, form.Template, config, form.ReceiveName, form.ReceiveAddress };

                _BGWAbsenceNotification = new BackgroundWorker();
                _BGWAbsenceNotification.DoWork += new DoWorkEventHandler(_BGWAbsenceNotification_DoWork);
                _BGWAbsenceNotification.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CommonMethods.WordReport_RunWorkerCompleted);
                _BGWAbsenceNotification.ProgressChanged += new ProgressChangedEventHandler(CommonMethods.Report_ProgressChanged);
                _BGWAbsenceNotification.WorkerReportsProgress = true;
                _BGWAbsenceNotification.RunWorkerAsync(args);
            }
        }

        private void _BGWAbsenceNotification_DoWork(object sender, DoWorkEventArgs e)
        {
            string reportName = "缺曠通知單";

            object[] args = e.Argument as object[];

            DateTime startDate = (DateTime)args[0];
            DateTime endDate = (DateTime)args[1];
            bool printHasRecordOnly = (bool)args[2];
            MemoryStream templateStream = (MemoryStream)args[3];
            Dictionary<string, List<string>> userDefinedConfig = (Dictionary<string, List<string>>)args[4];
            string receiveName = (string)args[5];
            string receiveAddress = (string)args[6];

            #region 快取資料

            List<SmartSchool.ClassRelated.ClassInfo> selectedClass = SmartSchool.ClassRelated.Class.Instance.SelectionClasses;

            //學生資訊
            Dictionary<string, Dictionary<string, string>> studentInfo = new Dictionary<string, Dictionary<string, string>>();

            //缺曠累計資料
            Dictionary<string, Dictionary<string, Dictionary<string, int>>> studentAbsence = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>();

            //學期缺曠累計資料
            Dictionary<string, Dictionary<string, Dictionary<string, int>>> studentSemesterAbsence = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>();

            //缺曠明細
            //Dictionary<string, List<string>> studentAbsenceDetail = new Dictionary<string, List<string>>();
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> studentAbsenceDetail = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

            //所有學生ID
            List<string> allStudentID = new List<string>();

            //學生人數
            int currentStudentCount = 1;
            int totalStudentNumber = 0;

            //Period List
            List<string> periodList = new List<string>();

            //Absence List
            Dictionary<string, string> absenceList = new Dictionary<string, string>();

            //使用者所選取的所有假別種類
            List<string> userDefinedAbsenceList = new List<string>();
            foreach (string kind in userDefinedConfig.Keys)
            {
                foreach (string type in userDefinedConfig[kind])
                {
                    if (!userDefinedAbsenceList.Contains(type))
                        userDefinedAbsenceList.Add(type);
                }
            }

            //取得所有學生ID
            foreach (SmartSchool.ClassRelated.ClassInfo aClass in selectedClass)
            {
                foreach (SmartSchool.StudentRelated.BriefStudentData aStudent in aClass.Students)
                {
                    //建立學生資訊，班級、座號、學號、姓名、導師
                    string studentID = aStudent.ID;
                    if (!studentInfo.ContainsKey(studentID))
                        studentInfo.Add(studentID, new Dictionary<string, string>());
                    if (!studentInfo[studentID].ContainsKey("ClassName"))
                        studentInfo[studentID].Add("ClassName", aStudent.ClassName);
                    if (!studentInfo[studentID].ContainsKey("SeatNo"))
                        studentInfo[studentID].Add("SeatNo", aStudent.SeatNo);
                    if (!studentInfo[studentID].ContainsKey("StudentNumber"))
                        studentInfo[studentID].Add("StudentNumber", aStudent.StudentNumber);
                    if (!studentInfo[studentID].ContainsKey("Name"))
                        studentInfo[studentID].Add("Name", aStudent.Name);
                    if (!studentInfo[studentID].ContainsKey("Teacher"))
                        studentInfo[studentID].Add("Teacher", aClass.TeacherName);

                    if (!allStudentID.Contains(studentID))
                        allStudentID.Add(studentID);
                }
                totalStudentNumber += aClass.Students.Count;
            }

            //取得 Period List
            DSResponse dsrsp = SmartSchool.Feature.Basic.Config.GetPeriodList();
            foreach (XmlElement var in dsrsp.GetContent().GetElements("Period"))
            {
                if (!periodList.Contains(var.GetAttribute("Name")))
                    periodList.Add(var.GetAttribute("Name"));
            }

            //取得 Absence List
            dsrsp = SmartSchool.Feature.Basic.Config.GetAbsenceList();
            foreach (XmlElement var in dsrsp.GetContent().GetElements("Absence"))
            {
                if (!absenceList.ContainsKey(var.GetAttribute("Name")))
                    absenceList.Add(var.GetAttribute("Name"), var.GetAttribute("Abbreviation"));
            }

            //取得所有學生缺曠紀錄，日期區間
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
            dsrsp = SmartSchool.Feature.Student.QueryAttendance.GetAttendance(new DSRequest(helper));
            foreach (XmlElement var in dsrsp.GetContent().GetElements("Attendance"))
            {
                string studentID = var.SelectSingleNode("RefStudentID").InnerText;
                DateTime occurDate = DateTime.Parse(var.SelectSingleNode("OccurDate").InnerText);
                string occurDateString = occurDate.Year + "/" + occurDate.Month + "/" + occurDate.Day;

                //累計資料
                if (!studentAbsence.ContainsKey(studentID))
                    studentAbsence.Add(studentID, new Dictionary<string, Dictionary<string, int>>());

                //明細資料
                if (!studentAbsenceDetail.ContainsKey(studentID))
                    studentAbsenceDetail.Add(studentID, new Dictionary<string, Dictionary<string, string>>());
                if (!studentAbsenceDetail[studentID].ContainsKey(occurDateString))
                    studentAbsenceDetail[studentID].Add(occurDateString, new Dictionary<string, string>());

                foreach (XmlElement period in var.SelectNodes("Detail/Attendance/Period"))
                {
                    //<Period AbsenceType="事假" AttendanceType="一般">一</Period>

                    string absenceType = period.GetAttribute("AbsenceType");
                    string attendanceType = period.GetAttribute("AttendanceType");
                    string innerText = period.InnerText;

                    if (userDefinedConfig.ContainsKey(attendanceType))
                    {
                        if (userDefinedConfig[attendanceType].Contains(absenceType))
                        {
                            if (!studentAbsence[studentID].ContainsKey(attendanceType))
                                studentAbsence[studentID].Add(attendanceType, new Dictionary<string, int>());
                            if (!studentAbsence[studentID][attendanceType].ContainsKey(absenceType))
                                studentAbsence[studentID][attendanceType].Add(absenceType, 0);
                            studentAbsence[studentID][attendanceType][absenceType]++;
                        }
                    }

                    if (userDefinedAbsenceList.Contains(absenceType))
                    {
                        if (!studentAbsenceDetail[studentID][occurDateString].ContainsKey(innerText) && absenceList.ContainsKey(absenceType))
                            studentAbsenceDetail[studentID][occurDateString].Add(innerText, absenceList[absenceType]);
                    }
                }

                if (studentAbsenceDetail[studentID][occurDateString].Count <= 0)
                    studentAbsenceDetail[studentID].Remove(occurDateString);
            }

            //取得所有學生缺曠紀錄，學期累計
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
            dsrsp = SmartSchool.Feature.Student.QueryAttendance.GetAttendance(new DSRequest(helper));
            foreach (XmlElement var in dsrsp.GetContent().GetElements("Attendance"))
            {
                string studentID = var.SelectSingleNode("RefStudentID").InnerText;
                DateTime occurDate = DateTime.Parse(var.SelectSingleNode("OccurDate").InnerText);
                string occurDateString = occurDate.Year + "/" + occurDate.Month + "/" + occurDate.Day;

                //累計資料
                if (!studentSemesterAbsence.ContainsKey(studentID))
                    studentSemesterAbsence.Add(studentID, new Dictionary<string, Dictionary<string, int>>());

                foreach (XmlElement period in var.SelectNodes("Detail/Attendance/Period"))
                {
                    string absenceType = period.GetAttribute("AbsenceType");
                    string attendanceType = period.GetAttribute("AttendanceType");
                    string innerText = period.InnerText;

                    if (userDefinedConfig.ContainsKey(attendanceType))
                    {
                        if (userDefinedConfig[attendanceType].Contains(absenceType))
                        {
                            if (!studentSemesterAbsence[studentID].ContainsKey(attendanceType))
                                studentSemesterAbsence[studentID].Add(attendanceType, new Dictionary<string, int>());
                            if (!studentSemesterAbsence[studentID][attendanceType].ContainsKey(absenceType))
                                studentSemesterAbsence[studentID][attendanceType].Add(absenceType, 0);
                            studentSemesterAbsence[studentID][attendanceType][absenceType]++;
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

            #region 產生範本

            Document template = new Document(templateStream, "", LoadFormat.Doc, "");
            DocumentBuilder builder = new DocumentBuilder(template);

            //缺曠類別部份
            #region 缺曠類別部份
            builder.MoveToMergeField("缺曠類別");
            Table table = template.Sections[0].Body.Tables[0];
            Cell startCell = (Cell)builder.CurrentParagraph.ParentNode;
            Row startRow = (Row)startCell.ParentNode;

            double totalWidth = startCell.CellFormat.Width;
            int startRowIndex = table.IndexOf(startRow);
            int columnNumber = 0;

            foreach (List<string> var in userDefinedConfig.Values)
            {
                columnNumber += var.Count;
            }
            double columnWidth = totalWidth / columnNumber;

            for (int i = startRowIndex; i < startRowIndex + 4; i++)
            {
                table.Rows[i].RowFormat.HeightRule = HeightRule.Exactly;
                table.Rows[i].RowFormat.Height = 12;
            }

            foreach (string attendanceType in userDefinedConfig.Keys)
            {
                Cell newCell = new Cell(template);
                newCell.CellFormat.Width = userDefinedConfig[attendanceType].Count * columnWidth;
                newCell.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                newCell.CellFormat.WrapText = true;
                newCell.Paragraphs.Add(new Paragraph(template));
                newCell.Paragraphs[0].ParagraphFormat.Alignment = ParagraphAlignment.Center;
                newCell.Paragraphs[0].ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
                newCell.Paragraphs[0].ParagraphFormat.LineSpacing = 12;
                newCell.Paragraphs[0].Runs.Add(new Run(template, attendanceType));
                newCell.Paragraphs[0].Runs[0].Font.Size = 8;
                table.Rows[startRowIndex].Cells.Add(newCell.Clone(true));
                foreach (string absenceType in userDefinedConfig[attendanceType])
                {
                    newCell.CellFormat.Width = columnWidth;
                    newCell.Paragraphs[0].Runs[0].Text = absenceType;
                    table.Rows[startRowIndex + 1].Cells.Add(newCell.Clone(true));
                    newCell.Paragraphs[0].Runs[0].Text = "0";
                    table.Rows[startRowIndex + 2].Cells.Add(newCell.Clone(true));
                    table.Rows[startRowIndex + 3].Cells.Add(newCell.Clone(true));
                }
            }

            for (int i = startRowIndex; i < startRowIndex + 4; i++)
            {
                if (userDefinedConfig.Count > 0)
                    table.Rows[i].Cells[1].Remove();
                table.Rows[i].LastCell.CellFormat.Borders.Right.Color = Color.Black;
                table.Rows[i].LastCell.CellFormat.Borders.Right.LineWidth = 2.25;
            }
            #endregion

            #endregion

            #region 產生報表

            Document doc = new Document();
            doc.Sections.Clear();

            foreach (string studentID in studentInfo.Keys)
            {
                if (printHasRecordOnly)
                {
                    if (!studentAbsenceDetail.ContainsKey(studentID) || studentAbsenceDetail[studentID].Count == 0)
                    {
                        currentStudentCount++;
                        continue;
                    }
                }

                Document eachSection = new Document();
                eachSection.Sections.Clear();
                eachSection.Sections.Add(eachSection.ImportNode(template.Sections[0], true));

                //合併列印的資料
                Dictionary<string, object> mapping = new Dictionary<string, object>();
                Dictionary<string, string> eachStudentInfo = studentInfo[studentID];

                //學校資訊
                mapping.Add("學校名稱", CurrentUser.Instance.SchoolInfo.ChineseName);
                mapping.Add("學校地址", CurrentUser.Instance.SchoolInfo.Address);
                mapping.Add("學校電話", CurrentUser.Instance.SchoolInfo.Telephone);

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

                //缺曠明細
                if (studentAbsenceDetail.ContainsKey(studentID))
                {
                    object[] objectValues = new object[] { studentAbsenceDetail[studentID], periodList };
                    mapping.Add("缺曠明細", objectValues);

                }
                else
                    mapping.Add("缺曠明細", null);

                string[] keys = new string[mapping.Count];
                object[] values = new object[mapping.Count];
                int i = 0;
                foreach (string key in mapping.Keys)
                {
                    keys[i] = key;
                    values[i++] = mapping[key];
                }

                //合併列印
                eachSection.MailMerge.MergeField += new Aspose.Words.Reporting.MergeFieldEventHandler(AbsenceNotification_MailMerge_MergeField);
                eachSection.MailMerge.RemoveEmptyParagraphs = true;
                eachSection.MailMerge.Execute(keys, values);

                //填寫缺曠記錄
                Table eachTable = eachSection.Sections[0].Body.Tables[0];
                int columnIndex = 1;
                foreach (string attendanceType in userDefinedConfig.Keys)
                {
                    foreach (string absenceType in userDefinedConfig[attendanceType])
                    {
                        string dataValue = "0";
                        string semesterDataValue = "0";
                        if (studentAbsence.ContainsKey(studentID) && studentAbsence[studentID].ContainsKey(attendanceType))
                        {
                            if (studentAbsence[studentID][attendanceType].ContainsKey(absenceType))
                                dataValue = studentAbsence[studentID][attendanceType][absenceType].ToString();
                        }
                        if (studentSemesterAbsence.ContainsKey(studentID) && studentSemesterAbsence[studentID].ContainsKey(attendanceType))
                        {
                            if (studentSemesterAbsence[studentID][attendanceType].ContainsKey(absenceType))
                                semesterDataValue = studentSemesterAbsence[studentID][attendanceType][absenceType].ToString();
                        }
                        eachTable.Rows[startRowIndex + 3].Cells[columnIndex].Paragraphs[0].Runs[0].Text = dataValue;
                        eachTable.Rows[startRowIndex + 2].Cells[columnIndex].Paragraphs[0].Runs[0].Text = semesterDataValue;
                        columnIndex++;
                    }
                }

                doc.Sections.Add(doc.ImportNode(eachSection.Sections[0], true));

                //回報進度
                _BGWAbsenceNotification.ReportProgress((int)(((double)currentStudentCount++ * 100.0) / (double)totalStudentNumber));
            }

            #endregion

            string path = Path.Combine(Application.StartupPath, "Reports");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, reportName + ".doc");
            e.Result = new object[] { reportName, path, doc };
        }

        private void AbsenceNotification_MailMerge_MergeField(object sender, Aspose.Words.Reporting.MergeFieldEventArgs e)
        {
            #region 缺曠明細
            if (e.FieldName == "缺曠明細")
            {
                if (e.FieldValue == null)
                    return;

                object[] objectValues = (object[])e.FieldValue;
                Dictionary<string, Dictionary<string, string>> studentAbsenceDetail = (Dictionary<string, Dictionary<string, string>>)objectValues[0];
                List<string> periodList = (List<string>)objectValues[1];

                DocumentBuilder builder = new DocumentBuilder(e.Document);

                #region 缺曠明細部份
                builder.MoveToField(e.Field, false);
                Cell detailStartCell = (Cell)builder.CurrentParagraph.ParentNode;
                Row detailStartRow = (Row)detailStartCell.ParentNode;
                int detailStartRowIndex = e.Document.Sections[0].Body.Tables[0].IndexOf(detailStartRow);

                Table detailTable = builder.StartTable();
                builder.CellFormat.Borders.Left.LineWidth = 0.5;
                builder.CellFormat.Borders.Right.LineWidth = 0.5;

                builder.RowFormat.HeightRule = HeightRule.Auto;
                builder.RowFormat.Height = 12;
                builder.RowFormat.Alignment = RowAlignment.Center;

                int rowNumber = 4;
                if (studentAbsenceDetail.Count > rowNumber * 3)
                {
                    rowNumber = studentAbsenceDetail.Count / 3;
                    if (studentAbsenceDetail.Count % 3 > 0)
                        rowNumber++;
                }

                builder.InsertCell();

                for (int i = 0; i < 3; i++)
                {
                    builder.CellFormat.Borders.Right.Color = Color.Black;
                    builder.CellFormat.Borders.Left.Color = Color.Black;
                    builder.CellFormat.Width = 20;
                    builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                    builder.Write("日期");
                    builder.InsertCell();

                    for (int j = 0; j < periodList.Count; j++)
                    {
                        builder.CellFormat.Borders.Right.Color = Color.White;
                        builder.CellFormat.Borders.Left.Color = Color.White;
                        builder.CellFormat.Width = 9;
                        builder.CellFormat.WrapText = true;
                        builder.CellFormat.LeftPadding = 0.5;
                        if (j < periodList.Count)
                            builder.Write(periodList[j]);
                        builder.InsertCell();
                    }
                }

                builder.EndRow();

                for (int x = 0; x < rowNumber; x++)
                {
                    builder.CellFormat.Borders.Right.Color = Color.Black;
                    builder.CellFormat.Borders.Left.Color = Color.Black;
                    builder.CellFormat.Borders.Left.LineWidth = 0.5;
                    builder.CellFormat.Borders.Right.LineWidth = 0.5;
                    builder.CellFormat.Borders.Top.LineWidth = 0.5;
                    builder.CellFormat.Borders.Bottom.LineWidth = 0.5;
                    builder.CellFormat.Borders.LineStyle = LineStyle.Dot;
                    builder.RowFormat.HeightRule = HeightRule.Exactly;
                    builder.RowFormat.Height = 12;
                    builder.RowFormat.Alignment = RowAlignment.Center;
                    builder.InsertCell();

                    for (int i = 0; i < 3; i++)
                    {
                        builder.CellFormat.Borders.Left.LineStyle = LineStyle.Single;
                        builder.CellFormat.Width = 20;
                        builder.Write("");
                        builder.InsertCell();

                        builder.CellFormat.Borders.LineStyle = LineStyle.Dot;

                        for (int j = 0; j < periodList.Count; j++)
                        {
                            builder.CellFormat.Width = 9;
                            builder.Write("");
                            builder.InsertCell();
                        }
                    }

                    builder.EndRow();
                }
                builder.EndTable();

                foreach (Cell var in detailTable.Rows[0].Cells)
                {
                    var.Paragraphs[0].ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
                    var.Paragraphs[0].ParagraphFormat.LineSpacing = 9;
                }
                #endregion

                #region 填寫缺曠明細
                int eachDetailRowIndex = 0;
                int eachDetailColIndex = 0;

                foreach (string date in studentAbsenceDetail.Keys)
                {
                    int eachDetailPeriodColIndex = eachDetailColIndex + 1;
                    string[] splitDate = date.Split('/');
                    Paragraph dateParagraph = detailTable.Rows[eachDetailRowIndex + 1].Cells[eachDetailColIndex].Paragraphs[0];
                    dateParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                    dateParagraph.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
                    dateParagraph.ParagraphFormat.LineSpacing = 9;
                    dateParagraph.Runs.Clear();
                    dateParagraph.Runs.Add(new Run(e.Document));
                    dateParagraph.Runs[0].Font.Size = 8;
                    dateParagraph.Runs[0].Text = splitDate[1] + "/" + splitDate[2];

                    foreach (string period in periodList)
                    {
                        string dataValue = "";
                        if (studentAbsenceDetail[date].ContainsKey(period))
                            dataValue = studentAbsenceDetail[date][period];
                        Cell miniCell = detailTable.Rows[eachDetailRowIndex + 1].Cells[eachDetailPeriodColIndex];
                        miniCell.Paragraphs.Clear();
                        miniCell.Paragraphs.Add(dateParagraph.Clone(true));
                        miniCell.Paragraphs[0].Runs[0].Font.Size = 14 - (int)(periodList.Count / 2); //依表格多寡縮小文字
                        miniCell.Paragraphs[0].Runs[0].Text = dataValue;
                        eachDetailPeriodColIndex++;
                    }
                    eachDetailRowIndex++;
                    if (eachDetailRowIndex >= rowNumber)
                    {
                        eachDetailRowIndex = 0;
                        eachDetailColIndex += (periodList.Count + 1);
                    }
                }
                #endregion

                e.Text = string.Empty;
            }
            #endregion
        }   
    }
}
