using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Aspose.Cells;
using IntelliSchool.DSA30.Util;
using System.IO;
using SmartSchool.Common;
using System.Xml;
using SmartSchool;
using SmartSchool.StudentRelated;
using SmartSchool.Feature.Score;
using SmartSchool.Customization.PlugIn;
using SmartSchool.StudentRelated.RibbonBars.Reports;
using SmartSchool.Security;

namespace SmartSchool.StudentRelated.RibbonBars
{
    public partial class Report : RibbonBarBase, SmartSchool.Customization.PlugIn.Report.IReportManager
    {
        #region FeatureAccessControl

        //統計
        FeatureAccessControl statisticsCtrl;

        //報表

        //學生個人缺曠明細	Report0010
        FeatureAccessControl buttonItem2Ctrl;
        //學生個人獎懲明細	Report0020
        FeatureAccessControl buttonItem3Ctrl;
        //歷年功過及出席統計表	Report0030
        FeatureAccessControl buttonItem1Ctrl;

        #endregion

        BackgroundWorker _BGWAbsenceDetail;
        BackgroundWorker _BGWDisciplineDetail;
        BackgroundWorker _BGWTotalDisciplineAndAbsence;

        private ButtonAdapterPlugInManager reportManager;

        public Report()
        {
            InitializeComponent();

            //SmartSchool.StudentRelated.Student.Instance.SelectionChanged += new EventHandler(Instance_SelectionChanged);

            #region 設定為學生的報表外掛處理者
            reportManager = new ButtonAdapterPlugInManager(this.buttonItem86);
            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonAdapter>.Instance.Add("學生/統計報表/報表", reportManager);
            SmartSchool.Customization.PlugIn.Report.StudentReport.SetManager(this);
            #endregion
            #region 設定為學生的統計外掛處理者
            ButtonAdapterPlugInManager 統計管理 = new ButtonAdapterPlugInManager(btnStatistics);
            統計管理.ItemsChanged += new EventHandler(統計管理_ItemsChanged);
            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonAdapter>.Instance.Add("學生/統計報表/統計", 統計管理);
            #endregion

            //權限判斷 - 統計報表/統計
            statisticsCtrl = new FeatureAccessControl("Button0120");
            statisticsCtrl.Inspect(btnStatistics);

            //學生個人缺曠明細	Report0010
            buttonItem2Ctrl = new FeatureAccessControl("Report0010");
            //學生個人獎懲明細	Report0020
            buttonItem3Ctrl = new FeatureAccessControl("Report0020");
            //歷年功過及出席統計表	Report0030
            buttonItem1Ctrl = new FeatureAccessControl("Report0030");

            buttonItem2Ctrl.Inspect(buttonItem2);
            buttonItem3Ctrl.Inspect(buttonItem3);
            buttonItem1Ctrl.Inspect(buttonItem1);

            SmartSchool.Broadcaster.Events.Items["學生/選取變更"].Handler += delegate
            {
                buttonItem86.Enabled = (SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count > 0);
                statisticsCtrl.Inspect(btnStatistics);
            };
        }


        void 統計管理_ItemsChanged(object sender, EventArgs e)
        {
            this.btnStatistics.Enabled = btnStatistics.SubItems.Count > 0;

            statisticsCtrl.Inspect(btnStatistics);
        }

        #region IReportManager 成員

        public void AddButton(SmartSchool.Customization.PlugIn.ButtonAdapter button)
        {
            reportManager.Add(button);
        }

        public void RemoveButton(SmartSchool.Customization.PlugIn.ButtonAdapter button)
        {
            reportManager.Remove(button);
        }

        #endregion

        public override string ProcessTabName
        {
            get
            {
                return "學生";
            }
        }

        void Instance_SelectionChanged(object sender, EventArgs e)
        {
            buttonItem86.Enabled = (SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count > 0);

            statisticsCtrl.Inspect(btnStatistics);
        }

        void Report_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            MotherForm.Instance.SetBarMessage("" + e.UserState + "產生中...", e.ProgressPercentage);
        }

        void Report_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string reportName;
            string path;
            Workbook wb;

            object[] result = (object[])e.Result;
            reportName = (string)result[0];
            path = (string)result[1];
            wb = (Workbook)result[2];

            if (File.Exists(path))
            {
                int i = 1;
                while (true)
                {
                    string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + (i++) + Path.GetExtension(path);
                    if (!File.Exists(newPath))
                    {
                        path = newPath;
                        break;
                    }
                }
            }

            try
            {
                wb.Save(path, FileFormatType.Excel2003);
                MotherForm.Instance.SetBarMessage(reportName + "產生完成");
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Title = "另存新檔";
                sd.FileName = reportName + ".xls";
                sd.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        wb.Save(sd.FileName, FileFormatType.Excel2003);
                    }
                    catch
                    {
                        MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        void ReportWord_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string reportName;
            string path;
            Aspose.Words.Document doc;

            object[] result = (object[])e.Result;
            reportName = (string)result[0];
            path = (string)result[1];
            doc = (Aspose.Words.Document)result[2];

            if (File.Exists(path))
            {
                int i = 1;
                while (true)
                {
                    string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + (i++) + Path.GetExtension(path);
                    if (!File.Exists(newPath))
                    {
                        path = newPath;
                        break;
                    }
                }
            }

            try
            {
                doc.Save(path, Aspose.Words.SaveFormat.Doc);
                MotherForm.Instance.SetBarMessage(reportName + "產生完成");
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Title = "另存新檔";
                sd.FileName = reportName + ".doc";
                sd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        doc.Save(sd.FileName, Aspose.Words.SaveFormat.Doc);

                    }
                    catch
                    {
                        MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        internal string GetChineseDayOfWeek(DateTime date)
        {
            string dayOfWeek = "";

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dayOfWeek = "一";
                    break;
                case DayOfWeek.Tuesday:
                    dayOfWeek = "二";
                    break;
                case DayOfWeek.Wednesday:
                    dayOfWeek = "三";
                    break;
                case DayOfWeek.Thursday:
                    dayOfWeek = "四";
                    break;
                case DayOfWeek.Friday:
                    dayOfWeek = "五";
                    break;
                case DayOfWeek.Saturday:
                    dayOfWeek = "六";
                    break;
                case DayOfWeek.Sunday:
                    dayOfWeek = "日";
                    break;
            }

            return dayOfWeek;
        }

        #region 學生個人缺曠明細
        private void buttonItem2_Click(object sender, EventArgs e)
        {
            if (SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count == 0)
                return;

            //警告使用者別做傻事
            if (SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count > 1500)
            {
                MsgBox.Show("您選取的學生超過 1500 個，可能會發生意想不到的錯誤，請減少選取的學生。");
                return;
            }

            SelectSchoolYearSemesterForm form = new SelectSchoolYearSemesterForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                MotherForm.Instance.SetBarMessage("正在初始化學生個人缺曠明細...");

                object[] args = new object[] { form.SchoolYear, form.Semester };

                _BGWAbsenceDetail = new BackgroundWorker();
                _BGWAbsenceDetail.DoWork += new DoWorkEventHandler(_BGWAbsenceDetail_DoWork);
                _BGWAbsenceDetail.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Report_RunWorkerCompleted);
                _BGWAbsenceDetail.ProgressChanged += new ProgressChangedEventHandler(Report_ProgressChanged);
                _BGWAbsenceDetail.WorkerReportsProgress = true;
                _BGWAbsenceDetail.RunWorkerAsync(args);
            }
        }

        void _BGWAbsenceDetail_DoWork(object sender, DoWorkEventArgs e)
        {
            string reportName = "學生個人缺曠明細";

            object[] args = e.Argument as object[];
            string userDefineSchoolYear = (string)args[0];
            string userDefineSemester = (string)args[1];

            #region 快取相關資料

            //選擇的學生
            List<SmartSchool.StudentRelated.BriefStudentData> selectedStudents = SmartSchool.StudentRelated.Student.Instance.SelectionStudents;

            //紀錄所有學生ID
            List<string> allStudentID = new List<string>();

            //對照表
            Dictionary<string, string> absenceList = new Dictionary<string, string>();
            Dictionary<string, string> periodList = new Dictionary<string, string>();

            //根據節次類型統計每一種缺曠別的次數
            Dictionary<string, Dictionary<string, Dictionary<string, int>>> periodStatisticsByType = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>();

            //每一位學生的缺曠明細
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> studentAbsenceDetail = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

            //紀錄每一個節次在報表中的 column index
            Dictionary<string, int> columnTable = new Dictionary<string, int>();

            //取得所有學生ID
            foreach (SmartSchool.StudentRelated.BriefStudentData var in selectedStudents)
            {
                allStudentID.Add(var.ID);
            }

            //取得 Absence List
            DSResponse dsrsp = SmartSchool.Feature.Basic.Config.GetAbsenceList();
            foreach (XmlElement var in dsrsp.GetContent().GetElements("Absence"))
            {
                string name = var.GetAttribute("Name");

                if (!absenceList.ContainsKey(name))
                    absenceList.Add(name, var.GetAttribute("Abbreviation"));
            }

            //取得 Period List
            dsrsp = SmartSchool.Feature.Basic.Config.GetPeriodList();
            foreach (XmlElement var in dsrsp.GetContent().GetElements("Period"))
            {
                if (!var.HasAttribute("Name"))
                    continue;

                string name = var.GetAttribute("Name");
                string type = var.GetAttribute("Type");

                if (!periodList.ContainsKey(name))
                    periodList.Add(name, type);
            }

            //產生 DSRequest，取得缺曠明細
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            foreach (string var in allStudentID)
            {
                helper.AddElement("Condition", "RefStudentID", var);
            }
            if (!string.IsNullOrEmpty(userDefineSchoolYear))
            {
                helper.AddElement("Condition", "SchoolYear", userDefineSchoolYear);
            }
            if (!string.IsNullOrEmpty(userDefineSemester))
            {
                helper.AddElement("Condition", "Semester", userDefineSemester);
            }
            helper.AddElement("Order");
            helper.AddElement("Order", "OccurDate", "asc");
            dsrsp = SmartSchool.Feature.Student.QueryAttendance.GetAttendance(new DSRequest(helper));

            foreach (XmlElement var in dsrsp.GetContent().GetElements("Attendance"))
            {
                string studentID = var.SelectSingleNode("RefStudentID").InnerText;
                string schoolYear = var.SelectSingleNode("SchoolYear").InnerText;
                string semester = var.SelectSingleNode("Semester").InnerText;
                string occurDate = DateTime.Parse(var.SelectSingleNode("OccurDate").InnerText).ToShortDateString();
                string sso = schoolYear + "_" + semester + "_" + occurDate;

                //累計資料
                if (!periodStatisticsByType.ContainsKey(studentID))
                    periodStatisticsByType.Add(studentID, new Dictionary<string, Dictionary<string, int>>());
                foreach (string value in periodList.Values)
                {
                    if (!periodStatisticsByType[studentID].ContainsKey(value))
                        periodStatisticsByType[studentID].Add(value, new Dictionary<string, int>());
                    foreach (string absence in absenceList.Keys)
                    {
                        if (!periodStatisticsByType[studentID][value].ContainsKey(absence))
                            periodStatisticsByType[studentID][value].Add(absence, 0);
                    }
                }

                //每一位學生缺曠資料
                if (!studentAbsenceDetail.ContainsKey(studentID))
                    studentAbsenceDetail.Add(studentID, new Dictionary<string, Dictionary<string, string>>());
                if (!studentAbsenceDetail[studentID].ContainsKey(sso))
                    studentAbsenceDetail[studentID].Add(sso, new Dictionary<string, string>());

                foreach (XmlElement period in var.SelectNodes("Detail/Attendance/Period"))
                {
                    string absenceType = period.GetAttribute("AbsenceType");
                    string inner = period.InnerText;
                    if (!periodList.ContainsKey(inner))
                        continue;
                    string periodType = periodList[inner];

                    if (!studentAbsenceDetail[studentID][sso].ContainsKey(inner) && absenceList.ContainsKey(absenceType))
                        studentAbsenceDetail[studentID][sso].Add(inner, absenceList[absenceType]);

                    if (periodStatisticsByType[studentID][periodType].ContainsKey(absenceType))
                        periodStatisticsByType[studentID][periodType][absenceType]++;
                }
            }

            #endregion

            #region 產生範本

            Workbook template = new Workbook();
            template.Open(new MemoryStream(Properties.Resources.學生個人缺曠明細), FileFormatType.Excel2003);

            Range tempEachColumn = template.Worksheets[0].Cells.CreateRange(4, 1, true);

            Workbook prototype = new Workbook();
            prototype.Copy(template);
            Worksheet ptws = prototype.Worksheets[0];
            int startPage = 1;
            int pageNumber = 1;

            int colIndex = 4;

            int startPeriodIndex = colIndex;
            int endPeriodIndex;

            //產生每一個節次的欄位
            foreach (string periodName in periodList.Keys)
            {
                ptws.Cells.CreateRange(colIndex, 1, true).Copy(tempEachColumn);
                ptws.Cells[3, colIndex].PutValue(periodName);
                columnTable.Add(periodName, colIndex);
                colIndex++;
            }
            endPeriodIndex = colIndex;

            ptws.Cells.CreateRange(2, startPeriodIndex, 1, endPeriodIndex - startPeriodIndex).Merge();
            ptws.Cells[2, startPeriodIndex].PutValue("節次");

            //合併標題列
            ptws.Cells.CreateRange(0, 0, 1, endPeriodIndex).Merge();
            ptws.Cells.CreateRange(1, 0, 1, endPeriodIndex).Merge();

            Range ptHeader = ptws.Cells.CreateRange(0, 4, false);
            Range ptEachRow = ptws.Cells.CreateRange(4, 1, false);

            //current++;

            #endregion

            #region 產生報表

            Workbook wb = new Workbook();
            wb.Copy(prototype);
            Worksheet ws = wb.Worksheets[0];

            int index = 0;
            int dataIndex = 0;

            int studentCount = 1;

            foreach (SmartSchool.StudentRelated.BriefStudentData studentInfo in selectedStudents)
            {
                //回報進度
                _BGWAbsenceDetail.ReportProgress((int)(((double)studentCount++ * 100.0) / (double)selectedStudents.Count));

                if (!studentAbsenceDetail.ContainsKey(studentInfo.ID))
                    continue;

                //如果不是第一頁，就在上一頁的資料列下邊加黑線
                if (index != 0)
                    ws.Cells.CreateRange(index - 1, 0, 1, endPeriodIndex).SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Medium, Color.Black);

                //複製 Header
                ws.Cells.CreateRange(index, 4, false).Copy(ptHeader);

                //填寫基本資料
                ws.Cells[index, 0].PutValue(CurrentUser.Instance.SchoolChineseName + " 個人缺曠明細");
                ws.Cells[index + 1, 0].PutValue("班級：" + ((studentInfo.ClassName == "") ? "　　　" : studentInfo.ClassName) + "　　座號：" + ((studentInfo.SeatNo == "") ? "　" : studentInfo.SeatNo) + "　　姓名：" + studentInfo.Name + "　　學號：" + studentInfo.StudentNumber);

                dataIndex = index + 4;
                int recordCount = 0;

                Dictionary<string, Dictionary<string, string>> absenceDetail = studentAbsenceDetail[studentInfo.ID];

                foreach (string sso in absenceDetail.Keys)
                {
                    string[] ssoSplit = sso.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);

                    //複製每一個 row
                    ws.Cells.CreateRange(dataIndex, 1, false).Copy(ptEachRow);

                    //填寫學生缺曠資料
                    ws.Cells[dataIndex, 0].PutValue(ssoSplit[0]);
                    ws.Cells[dataIndex, 1].PutValue(ssoSplit[1]);
                    ws.Cells[dataIndex, 2].PutValue(ssoSplit[2]);
                    ws.Cells[dataIndex, 3].PutValue(GetChineseDayOfWeek(DateTime.Parse(ssoSplit[2])));

                    Dictionary<string, string> record = absenceDetail[sso];
                    foreach (string periodName in record.Keys)
                    {
                        ws.Cells[dataIndex, columnTable[periodName]].PutValue(record[periodName]);
                    }

                    dataIndex++;
                    recordCount++;
                }

                //缺曠統計資訊
                Range absenceStatisticsRange = ws.Cells.CreateRange(dataIndex, 0, 1, endPeriodIndex);
                absenceStatisticsRange.Merge();
                absenceStatisticsRange.SetOutlineBorder(BorderType.TopBorder, CellBorderType.Double, Color.Black);
                absenceStatisticsRange.SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Double, Color.Black);
                absenceStatisticsRange.SetOutlineBorder(BorderType.LeftBorder, CellBorderType.Medium, Color.Black);
                absenceStatisticsRange.RowHeight = 20.0;
                ws.Cells[dataIndex, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
                ws.Cells[dataIndex, 0].Style.VerticalAlignment = TextAlignmentType.Center;
                ws.Cells[dataIndex, 0].Style.Font.Size = 10;
                ws.Cells[dataIndex, 0].PutValue("缺曠總計");
                dataIndex++;

                int typeNumber = dataIndex;
                foreach (string periodType in periodStatisticsByType[studentInfo.ID].Keys)
                {
                    Dictionary<string, int> byType = periodStatisticsByType[studentInfo.ID][periodType];
                    int printable = 0;
                    foreach (string type in byType.Keys)
                    {
                        printable += byType[type];
                    }
                    if (printable == 0)
                        continue;

                    ws.Cells.CreateRange(dataIndex, 0, 1, 2).Merge();
                    ws.Cells.CreateRange(dataIndex, 2, 1, endPeriodIndex - 2).Merge();
                    ws.Cells[dataIndex, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
                    ws.Cells[dataIndex, 0].Style.VerticalAlignment = TextAlignmentType.Center;
                    ws.Cells.CreateRange(dataIndex, 0, 1, endPeriodIndex).RowHeight = 27.0;
                    ws.Cells.CreateRange(dataIndex, 0, 1, 2).SetOutlineBorder(BorderType.RightBorder, CellBorderType.Thin, Color.Black);
                    ws.Cells.CreateRange(dataIndex, 0, 1, 2).SetOutlineBorder(BorderType.LeftBorder, CellBorderType.Medium, Color.Black);
                    ws.Cells.CreateRange(dataIndex, 0, 1, endPeriodIndex).SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Thin, Color.Black);

                    ws.Cells[dataIndex, 0].Style.Font.Size = 10;
                    ws.Cells[dataIndex, 0].PutValue(periodType);

                    StringBuilder text = new StringBuilder("");

                    foreach (string type in byType.Keys)
                    {
                        if (byType[type] > 0)
                        {
                            if (text.ToString() != "")
                                text.Append("　");
                            text.Append(type + "：" + byType[type]);
                        }
                    }

                    ws.Cells[dataIndex, 2].Style.Font.Size = 10;
                    ws.Cells[dataIndex, 2].Style.ShrinkToFit = true;
                    ws.Cells[dataIndex, 2].PutValue(text.ToString());

                    dataIndex++;
                }
                typeNumber = dataIndex - typeNumber;

                //資料列上邊加上黑線
                ws.Cells.CreateRange(index + 3, 0, 1, endPeriodIndex).SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Medium, Color.Black);

                //表格最右邊加上黑線
                ws.Cells.CreateRange(index + 2, endPeriodIndex - 1, recordCount + typeNumber + 3, 1).SetOutlineBorder(BorderType.RightBorder, CellBorderType.Medium, Color.Black);

                index = dataIndex;

                //設定分頁
                if (pageNumber < 500)
                {
                    ws.HPageBreaks.Add(index, endPeriodIndex);
                    pageNumber++;
                }
                else
                {
                    ws.Name = startPage + " ~ " + (pageNumber + startPage - 1);
                    ws = wb.Worksheets[wb.Worksheets.Add()];
                    ws.Copy(prototype.Worksheets[0]);
                    startPage += pageNumber;
                    pageNumber = 1;
                    index = 0;
                }
            }


            if (dataIndex > 0)
            {
                //最後一頁的資料列下邊加上黑線
                ws.Cells.CreateRange(dataIndex - 1, 0, 1, endPeriodIndex).SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Medium, Color.Black);
                ws.Name = startPage + " ~ " + (pageNumber + startPage - 2);
            }
            else
                wb = new Workbook();

            #endregion

            string path = Path.Combine(Application.StartupPath, "Reports");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, reportName + ".xlt");
            e.Result = new object[] { reportName, path, wb };
        }
        #endregion

        #region 學生個人獎懲明細
        private void buttonItem3_Click(object sender, EventArgs e)
        {
            if (SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count == 0)
                return;

            //警告使用者別做傻事
            if (SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count > 1500)
            {
                MsgBox.Show("您選取的學生超過 1500 個，可能會發生意想不到的錯誤，請減少選取的學生。");
                return;
            }

            SelectSchoolYearSemesterForm form = new SelectSchoolYearSemesterForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                MotherForm.Instance.SetBarMessage("正在初始化學生個人獎懲明細...");

                object[] args = new object[] { form.SchoolYear, form.Semester };

                _BGWDisciplineDetail = new BackgroundWorker();
                _BGWDisciplineDetail.DoWork += new DoWorkEventHandler(_BGWDisciplineDetail_DoWork);
                _BGWDisciplineDetail.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Report_RunWorkerCompleted);
                _BGWDisciplineDetail.ProgressChanged += new ProgressChangedEventHandler(Report_ProgressChanged);
                _BGWDisciplineDetail.WorkerReportsProgress = true;
                _BGWDisciplineDetail.RunWorkerAsync(args);
            }
        }

        void _BGWDisciplineDetail_DoWork(object sender, DoWorkEventArgs e)
        {
            string reportName = "學生個人獎懲明細";

            object[] args = e.Argument as object[];
            string userDefineSchoolYear = (string)args[0];
            string userDefineSemester = (string)args[1];

            #region 快取相關資料

            //選擇的學生
            List<SmartSchool.StudentRelated.BriefStudentData> selectedStudents = SmartSchool.StudentRelated.Student.Instance.SelectionStudents;

            //紀錄所有學生ID
            List<string> allStudentID = new List<string>();

            //每一位學生的獎懲明細
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> studentDisciplineDetail = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

            //每一位學生的獎懲累計資料
            Dictionary<string, Dictionary<string, int>> studentDisciplineStatistics = new Dictionary<string, Dictionary<string, int>>();

            //紀錄每一種獎懲在報表中的 column index
            Dictionary<string, int> columnTable = new Dictionary<string, int>();

            //取得所有學生ID
            foreach (SmartSchool.StudentRelated.BriefStudentData var in selectedStudents)
            {
                allStudentID.Add(var.ID);
            }

            //對照表
            Dictionary<string, string> meritTable = new Dictionary<string, string>();
            meritTable.Add("A", "大功");
            meritTable.Add("B", "小功");
            meritTable.Add("C", "嘉獎");

            Dictionary<string, string> demeritTable = new Dictionary<string, string>();
            demeritTable.Add("A", "大過");
            demeritTable.Add("B", "小過");
            demeritTable.Add("C", "警告");

            Dictionary<string, string> demeritOtherTable = new Dictionary<string, string>();
            demeritOtherTable.Add("ClearDate", "銷過日期");
            demeritOtherTable.Add("ClearReason", "銷過事由");
            demeritOtherTable.Add("Cleared", "銷過");

            //初始化
            string[] columnString = new string[] { "嘉獎", "小功", "大功", "警告", "小過", "大過", "留察", "銷過", "銷過日期", "事由" };
            int i = 4;
            foreach (string s in columnString)
            {
                columnTable.Add(s, i++);
            }


            //產生 DSRequest，取得缺曠明細
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            foreach (string var in allStudentID)
            {
                helper.AddElement("Condition", "RefStudentID", var);
            }
            if (!string.IsNullOrEmpty(userDefineSchoolYear))
            {
                helper.AddElement("Condition", "SchoolYear", userDefineSchoolYear);
            }
            if (!string.IsNullOrEmpty(userDefineSemester))
            {
                helper.AddElement("Condition", "Semester", userDefineSemester);
            }
            helper.AddElement("Order");
            helper.AddElement("Order", "OccurDate", "asc");
            DSResponse dsrsp = SmartSchool.Feature.Student.QueryDiscipline.GetDiscipline(new DSRequest(helper));

            foreach (XmlElement var in dsrsp.GetContent().GetElements("Discipline"))
            {
                string studentID = var.SelectSingleNode("RefStudentID").InnerText;
                string schoolYear = var.SelectSingleNode("SchoolYear").InnerText;
                string semester = var.SelectSingleNode("Semester").InnerText;
                string occurDate = DateTime.Parse(var.SelectSingleNode("OccurDate").InnerText).ToShortDateString();
                string reason = var.SelectSingleNode("Reason").InnerText;
                string disciplineID = var.GetAttribute("ID");
                string sso = schoolYear + "_" + semester + "_" + occurDate + "_" + disciplineID;

                //初始化累計資料
                if (!studentDisciplineStatistics.ContainsKey(studentID))
                    studentDisciplineStatistics.Add(studentID, new Dictionary<string, int>());

                //每一位學生缺曠資料
                if (!studentDisciplineDetail.ContainsKey(studentID))
                    studentDisciplineDetail.Add(studentID, new Dictionary<string, Dictionary<string, string>>());
                if (!studentDisciplineDetail[studentID].ContainsKey(sso))
                    studentDisciplineDetail[studentID].Add(sso, new Dictionary<string, string>());

                //加入事由
                if (!studentDisciplineDetail[studentID][sso].ContainsKey("事由"))
                    studentDisciplineDetail[studentID][sso].Add("事由", reason);

                if (var.SelectSingleNode("MeritFlag").InnerText == "1")
                {
                    XmlElement discipline = (XmlElement)var.SelectSingleNode("Detail/Discipline/Merit");
                    foreach (XmlAttribute attr in discipline.Attributes)
                    {
                        if (meritTable.ContainsKey(attr.Name))
                        {
                            string name = meritTable[attr.Name];

                            if (!studentDisciplineStatistics[studentID].ContainsKey(name))
                                studentDisciplineStatistics[studentID].Add(name, 0);
                            studentDisciplineStatistics[studentID][name] += int.Parse(attr.InnerText);

                            if (!studentDisciplineDetail[studentID][sso].ContainsKey(name))
                                studentDisciplineDetail[studentID][sso].Add(name, attr.InnerText);
                        }
                    }
                }
                else if (var.SelectSingleNode("MeritFlag").InnerText == "0")
                {
                    XmlElement discipline = (XmlElement)var.SelectSingleNode("Detail/Discipline/Demerit");

                    foreach (XmlAttribute attr in discipline.Attributes)
                    {
                        if (demeritTable.ContainsKey(attr.Name))
                        {
                            string name = demeritTable[attr.Name];

                            //累加
                            if (!studentDisciplineStatistics[studentID].ContainsKey(name))
                                studentDisciplineStatistics[studentID].Add(name, 0);
                            if (discipline.GetAttribute("Cleared") != "是")
                                studentDisciplineStatistics[studentID][name] += int.Parse(attr.InnerText);

                            if (!studentDisciplineDetail[studentID][sso].ContainsKey(name))
                                studentDisciplineDetail[studentID][sso].Add(name, attr.InnerText);
                        }
                        else if (demeritOtherTable.ContainsKey(attr.Name))
                        {
                            string name = demeritOtherTable[attr.Name];

                            if (!studentDisciplineDetail[studentID][sso].ContainsKey(name))
                                studentDisciplineDetail[studentID][sso].Add(name, attr.InnerText);
                        }
                    }
                }
                else
                {
                    if (!studentDisciplineStatistics[studentID].ContainsKey("留察"))
                        studentDisciplineStatistics[studentID].Add("留察", 0);
                    if (!studentDisciplineDetail[studentID][sso].ContainsKey("留察"))
                        studentDisciplineDetail[studentID][sso].Add("留察", "是");
                }
            }

            #endregion

            #region 產生範本

            Workbook template = new Workbook();
            template.Open(new MemoryStream(Properties.Resources.學生個人獎懲明細), FileFormatType.Excel2003);

            Workbook prototype = new Workbook();
            prototype.Copy(template);

            Worksheet ptws = prototype.Worksheets[0];

            int startPage = 1;
            int pageNumber = 1;

            int columnNumber = 14;

            //合併標題列
            ptws.Cells.CreateRange(0, 0, 1, columnNumber).Merge();
            ptws.Cells.CreateRange(1, 0, 1, columnNumber).Merge();

            Range ptHeader = ptws.Cells.CreateRange(0, 4, false);
            Range ptEachRow = ptws.Cells.CreateRange(4, 1, false);

            #endregion

            #region 產生報表

            Workbook wb = new Workbook();
            wb.Copy(prototype);
            Worksheet ws = wb.Worksheets[0];

            int index = 0;
            int dataIndex = 0;

            int studentCount = 1;

            foreach (SmartSchool.StudentRelated.BriefStudentData studentInfo in selectedStudents)
            {
                //回報進度
                _BGWDisciplineDetail.ReportProgress((int)(((double)studentCount++ * 100.0) / (double)selectedStudents.Count));

                if (!studentDisciplineDetail.ContainsKey(studentInfo.ID))
                    continue;

                //如果不是第一頁，就在上一頁的資料列下邊加黑線
                if (index != 0)
                    ws.Cells.CreateRange(index - 1, 0, 1, columnNumber).SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Medium, Color.Black);

                //複製 Header
                ws.Cells.CreateRange(index, 4, false).Copy(ptHeader);

                //填寫基本資料
                ws.Cells[index, 0].PutValue(CurrentUser.Instance.SchoolChineseName + " 個人獎懲明細");
                ws.Cells[index + 1, 0].PutValue("班級：" + ((studentInfo.ClassName == "") ? "　　　" : studentInfo.ClassName) + "　　座號：" + ((studentInfo.SeatNo == "") ? "　" : studentInfo.SeatNo) + "　　姓名：" + studentInfo.Name + "　　學號：" + studentInfo.StudentNumber);

                dataIndex = index + 4;
                int recordCount = 0;

                Dictionary<string, Dictionary<string, string>> disciplineDetail = studentDisciplineDetail[studentInfo.ID];

                foreach (string sso in disciplineDetail.Keys)
                {
                    string[] ssoSplit = sso.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);

                    //複製每一個 row
                    ws.Cells.CreateRange(dataIndex, 1, false).Copy(ptEachRow);

                    //填寫學生獎懲資料
                    ws.Cells[dataIndex, 0].PutValue(ssoSplit[0]);
                    ws.Cells[dataIndex, 1].PutValue(ssoSplit[1]);
                    ws.Cells[dataIndex, 2].PutValue(ssoSplit[2]);
                    ws.Cells[dataIndex, 3].PutValue(GetChineseDayOfWeek(DateTime.Parse(ssoSplit[2])));

                    Dictionary<string, string> record = disciplineDetail[sso];
                    foreach (string name in record.Keys)
                    {
                        if (meritTable.ContainsValue(name) || demeritTable.ContainsValue(name))
                        {
                            if (int.Parse(record[name]) > 0)
                                ws.Cells[dataIndex, columnTable[name]].PutValue(record[name]);
                        }
                        else
                        {
                            if (columnTable.ContainsKey(name))
                                ws.Cells[dataIndex, columnTable[name]].PutValue(record[name]);
                        }
                    }

                    dataIndex++;
                    recordCount++;
                }

                //獎懲統計資訊
                Range disciplineStatisticsRange = ws.Cells.CreateRange(dataIndex, 0, 1, columnNumber);
                disciplineStatisticsRange.Copy(ptEachRow);
                disciplineStatisticsRange.Merge();
                disciplineStatisticsRange.SetOutlineBorder(BorderType.TopBorder, CellBorderType.Double, Color.Black);
                disciplineStatisticsRange.SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Double, Color.Black);
                disciplineStatisticsRange.RowHeight = 20.0;
                ws.Cells[dataIndex, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
                ws.Cells[dataIndex, 0].Style.VerticalAlignment = TextAlignmentType.Center;
                ws.Cells[dataIndex, 0].Style.Font.Size = 10;
                ws.Cells[dataIndex, 0].PutValue("獎懲總計");
                dataIndex++;

                //獎懲統計內容
                ws.Cells.CreateRange(dataIndex, 0, 1, columnNumber).Copy(ptEachRow);
                ws.Cells.CreateRange(dataIndex, 0, 1, columnNumber).RowHeight = 27.0;
                ws.Cells.CreateRange(dataIndex, 0, 1, columnNumber).Merge();
                ws.Cells[dataIndex, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
                ws.Cells[dataIndex, 0].Style.VerticalAlignment = TextAlignmentType.Center;
                ws.Cells[dataIndex, 0].Style.Font.Size = 10;
                ws.Cells[dataIndex, 0].Style.ShrinkToFit = true;

                StringBuilder text = new StringBuilder("");
                Dictionary<string, int> disciplineStatistics = studentDisciplineStatistics[studentInfo.ID];

                foreach (string type in disciplineStatistics.Keys)
                {
                    if (disciplineStatistics[type] > 0)
                    {
                        if (text.ToString() != "")
                            text.Append("　");
                        text.Append(type + "：" + disciplineStatistics[type]);
                    }
                }

                ws.Cells[dataIndex, 0].PutValue(text.ToString());

                dataIndex++;

                //資料列上邊加上黑線
                ws.Cells.CreateRange(index + 3, 0, 1, columnNumber).SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Medium, Color.Black);

                //表格最右邊加上黑線
                ws.Cells.CreateRange(index + 2, columnNumber - 1, recordCount + 4, 1).SetOutlineBorder(BorderType.RightBorder, CellBorderType.Medium, Color.Black);

                index = dataIndex;

                //設定分頁
                if (pageNumber < 500)
                {
                    ws.HPageBreaks.Add(index, columnNumber);
                    pageNumber++;
                }
                else
                {
                    ws.Name = startPage + " ~ " + (pageNumber + startPage - 1);
                    ws = wb.Worksheets[wb.Worksheets.Add()];
                    ws.Copy(prototype.Worksheets[0]);
                    startPage += pageNumber;
                    pageNumber = 1;
                    index = 0;
                }

            }


            if (dataIndex > 0)
            {
                //最後一頁的資料列下邊加上黑線
                ws.Cells.CreateRange(dataIndex - 1, 0, 1, columnNumber).SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Medium, Color.Black);
                ws.Name = startPage + " ~ " + (pageNumber + startPage - 2);
            }
            else
                wb = new Workbook();


            #endregion

            string path = Path.Combine(Application.StartupPath, "Reports");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, reportName + ".xlt");
            e.Result = new object[] { reportName, path, wb };
        }
        #endregion

        #region 歷年功過及出席統計
        private void buttonItem1_Click(object sender, EventArgs e)
        {
            if (SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count == 0)
                return;

            ////警告使用者別做傻事
            //if (SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count > 1500)
            //{
            //    MsgBox.Show("您選取的學生超過 1500 個，可能會發生意想不到的錯誤，請減少選取的學生。");
            //    return;
            //}

            OverTheYearsStatisticsForm form = new OverTheYearsStatisticsForm();
            form.ShowDialog();
        }
        #endregion
    }
}