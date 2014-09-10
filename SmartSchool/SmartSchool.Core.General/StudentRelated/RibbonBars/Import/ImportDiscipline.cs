using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.API.PlugIn;
using SmartSchool.AccessControl;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.Data.StudentExtension;
using IntelliSchool.DSA30.Util;
using System.Xml;

namespace SmartSchool.StudentRelated.RibbonBars.Import
{
    //[FeatureCode("Button0285")]
    class ImportDiscipline : API.PlugIn.Import.Importer
    {
        public ImportDiscipline()
        {
            this.Text = "匯入獎懲紀錄";
        }

        public override void InitializeImport(SmartSchool.API.PlugIn.Import.ImportWizard wizard)
        {
            AccessHelper accessHelper = new AccessHelper();
            VirtualRadioButton chose1 = new VirtualRadioButton("比對事由變更獎懲次數", false);
            VirtualRadioButton chose2 = new VirtualRadioButton("比對獎懲次數變更事由", false);
            chose1.CheckedChanged += delegate
            {
                if ( chose1.Checked )
                {
                    wizard.RequiredFields.Clear();
                    wizard.RequiredFields.AddRange("學年度", "學期", "日期", "事由");
                }
            };
            chose2.CheckedChanged += delegate
            {
                if ( chose2.Checked )
                {
                    wizard.RequiredFields.Clear();
                    wizard.RequiredFields.AddRange("學年度", "學期", "日期", "大功", "小功", "嘉獎", "大過", "小過", "警告");
                }
            };
            wizard.ImportableFields.AddRange("學年度", "學期", "日期", "地點", "大功", "小功", "嘉獎", "大過", "小過", "警告", "事由", "是否銷過", "銷過日期", "銷過事由", "留校察看");
            wizard.Options.AddRange(chose1, chose2);
            chose1.Checked = true;
            wizard.PackageLimit = 400;
            bool allPass=true;
            wizard.ValidateStart += delegate(object sender, SmartSchool.API.PlugIn.Import.ValidateStartEventArgs e)
            {
                accessHelper.StudentHelper.FillReward(accessHelper.StudentHelper.GetStudents(e.List));
                allPass = true;
            };
            int insertRecords = 0;
            wizard.ValidateRow += delegate(object sender, SmartSchool.API.PlugIn.Import.ValidateRowEventArgs e)
            {
                #region 驗證資料
                bool pass = true;
                int schoolYear, semester;
                DateTime occurdate;
                bool isInsert = false;
                #region 驗共同必填欄位
                if ( !int.TryParse(e.Data["學年度"], out schoolYear) )
                {
                    e.ErrorFields.Add("學年度", "必需輸入數字");
                    pass = false;
                }
                if ( !int.TryParse(e.Data["學期"], out semester) )
                {
                    e.ErrorFields.Add("學期", "必需輸入數字");
                    pass = false;
                }
                if ( !DateTime.TryParse(e.Data["日期"], out occurdate) )
                {
                    e.ErrorFields.Add("日期", "輸入格式為 西元年//月//日");
                    pass = false;
                }
                #endregion
                if ( !pass )
                {
                    allPass = false;
                    return;
                }
                if ( chose1.Checked )
                {
                    #region 以事由為Key更新
                    string reason = e.Data["事由"];
                    int match = 0;
                    foreach ( RewardInfo rewardInfo in accessHelper.StudentHelper.GetStudent(e.Data.ID).RewardList )
                    {
                        if ( rewardInfo.SchoolYear == schoolYear && rewardInfo.Semester == semester && rewardInfo.OccurDate == occurdate && rewardInfo.OccurReason == reason )
                            match++;
                    }
                    if ( match > 1 )
                    {
                        e.ErrorMessage = "系統發現此事由在同一天中存在兩筆重複資料，無法進行更新，建議您手動處裡此筆變更。";
                        pass = false;
                    }
                    if ( match == 0 )
                        isInsert = true;
                    #endregion
                }
                if ( chose2.Checked )
                {
                    #region 以次數為Key更新
                    int awardA = 0;
                    int awardB = 0;
                    int awardC = 0;
                    int faultA = 0;
                    int faultB = 0;
                    int faultC = 0;
                    #region 驗證必填欄位
                    if ( e.Data["大功"] != "" && !int.TryParse(e.Data["大功"], out awardA) )
                    {
                        e.ErrorFields.Add("大功", "必需輸入數字");
                        pass = false;
                    }
                    if ( e.Data["小功"] != "" && !int.TryParse(e.Data["小功"], out awardB) )
                    {
                        e.ErrorFields.Add("小功", "必需輸入數字");
                        pass = false;
                    }
                    if ( e.Data["嘉獎"] != "" && !int.TryParse(e.Data["嘉獎"], out awardC) )
                    {
                        e.ErrorFields.Add("嘉獎", "必需輸入數字");
                        pass = false;
                    }
                    if ( e.Data["大過"] != "" && !int.TryParse(e.Data["大過"], out faultA) )
                    {
                        e.ErrorFields.Add("大過", "必需輸入數字");
                        pass = false;
                    }
                    if ( e.Data["小過"] != "" && !int.TryParse(e.Data["小過"], out faultB) )
                    {
                        e.ErrorFields.Add("小過", "必需輸入數字");
                        pass = false;
                    }
                    if ( e.Data["警告"] != "" && !int.TryParse(e.Data["警告"], out faultC) )
                    {
                        e.ErrorFields.Add("警告", "必需輸入數字");
                        pass = false;
                    }
                    #endregion
                    if ( !pass )
                    {
                        return;
                    }
                    int match = 0;
                    #region 檢查重複
                    foreach ( RewardInfo rewardInfo in accessHelper.StudentHelper.GetStudent(e.Data.ID).RewardList )
                    {
                        if ( rewardInfo.SchoolYear == schoolYear &&
                            rewardInfo.Semester == semester &&
                            rewardInfo.OccurDate == occurdate &&
                            rewardInfo.AwardA == awardA &&
                            rewardInfo.AwardB == awardB &&
                            rewardInfo.AwardC == awardC &&
                            rewardInfo.FaultA == faultA &&
                            rewardInfo.FaultB == faultB &&
                            rewardInfo.FaultC == faultC )
                            match++;
                    }
                    #endregion
                    if ( match > 1 )
                    {
                        e.ErrorMessage = "系統發現此獎懲次數在同一天中存在兩筆重複資料，無法進行更新，建議您手動處裡此筆變更。";
                        pass = false;
                    }
                    if ( match == 0 )
                        isInsert = true;
                    #endregion
                }
                if ( !pass )
                {
                    allPass = false;
                    return;
                }
                #region 驗證可選則欄位值
                int integer;
                DateTime dateTime;
                bool hasAward = false, hasFault = false;
                foreach ( string field in e.SelectFields )
                {
                    switch ( field )
                    {
                        case "大功":
                        case "小功":
                        case "嘉獎":
                            if ( e.Data[field] != "" )
                            {
                                if ( !int.TryParse(e.Data[field], out integer) )
                                {
                                    e.ErrorFields.Add(field, "必需輸入數字");
                                    pass = false;
                                }
                                else
                                    hasAward |= integer > 0;
                            }
                            break;
                        case "大過":
                        case "小過":
                        case "警告":
                            if ( e.Data[field] != "" )
                            {
                                if ( !int.TryParse(e.Data[field], out integer) )
                                {
                                    e.ErrorFields.Add(field, "必需輸入數字");
                                    pass = false;
                                }
                                else
                                    hasFault |= integer > 0;
                            }
                            break;
                        case "銷過日期":
                            if ( e.Data[field] != "" && !DateTime.TryParse(e.Data[field], out dateTime) )
                            {
                                e.ErrorFields.Add(field, "輸入格式為 西元年//月//日");
                                pass = false;
                            }
                            break;
                        case "是否銷過":
                        case "留校察看":
                            if ( e.Data[field] != "" && e.Data[field] != "是" && e.Data[field] != "否" )
                            {
                                e.ErrorFields.Add(field, "如果為是請填入\"是\"否則請保留空白或填入\"否\"");
                                pass = false;
                            }
                            break;
                    }
                }
                if ( hasAward && hasFault )
                {
                    e.ErrorMessage = "系統愚昧無法理解同時記功又記過的情況。";
                    pass = false;
                }
                if ( !pass && isInsert && ( !e.SelectFields.Contains("留校察看") || e.Data["留校察看"] != "是" ) && ( !hasFault && !hasAward ) )
                {
                    e.ErrorMessage = "無法新增沒有獎懲的記錄。";
                    pass = false;
                }
                if ( pass && isInsert )
                    insertRecords++;
                #endregion
                if ( !pass )
                {
                    allPass = false;
                }
                #endregion
            };
            wizard.ValidateComplete += delegate
            {
                if ( allPass && insertRecords > 0 )
                {
                    Common.MsgBox.Show("資料匯入將會新增" + insertRecords + "筆獎懲記錄，\n因為目前無法批次刪除獎懲，\n如與新增資料筆數不符請勿繼續。", "新增獎懲", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
            };
            wizard.ImportPackage += delegate(object sender, SmartSchool.API.PlugIn.Import.ImportPackageEventArgs e)
            {
                bool hasUpdate = false, hasInsert = false;
                DSXmlHelper updateHelper = new DSXmlHelper("UpdateRequest");
                DSXmlHelper insertHelper = new DSXmlHelper("InsertRequest");
                foreach ( RowData row in e.Items )
                {
                    int schoolYear = int.Parse(row["學年度"]);
                    int semester = int.Parse(row["學期"]);
                    DateTime occurdate = DateTime.Parse(row["日期"]);
                    if ( chose1.Checked )
                    {
                        #region 以事由為Key更新
                        bool isAward;
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

                        if ( row.ContainsKey("大功") )
                            awardA = ( row["大功"] == "" ) ? 0 : int.Parse(row["大功"]);
                        if ( row.ContainsKey("小功") )
                            awardB = ( row["小功"] == "" ) ? 0 : int.Parse(row["小功"]);
                        if ( row.ContainsKey("嘉獎") )
                            awardC = ( row["嘉獎"] == "" ) ? 0 : int.Parse(row["嘉獎"]);
                        if ( row.ContainsKey("大過") )
                            faultA = ( row["大過"] == "" ) ? 0 : int.Parse(row["大過"]);
                        if ( row.ContainsKey("小過") )
                            faultB = ( row["小過"] == "" ) ? 0 : int.Parse(row["小過"]);
                        if ( row.ContainsKey("警告") )
                            faultC = ( row["警告"] == "" ) ? 0 : int.Parse(row["警告"]);
                        cleared = e.ImportFields.Contains("是否銷過") ?
                            ( ( row["是否銷過"] == "是" ) ? true : false ) : false;

                        cleardate = ( e.ImportFields.Contains("銷過日期") && row["銷過日期"]!="" ) ?
                            DateTime.Parse(row["銷過日期"]) : DateTime.Now;

                        clearreason = e.ImportFields.Contains("銷過事由") ?
                            row["銷過事由"] : "";

                        ultimateAdmonition = e.ImportFields.Contains("留校察看") ?
                            ( ( row["留校察看"] == "是" ) ? true : false ) : false;

                        string reason = row["事由"];
                        bool match = false;
                        foreach ( RewardInfo rewardInfo in accessHelper.StudentHelper.GetStudent(row.ID).RewardList )
                        {
                            if ( rewardInfo.SchoolYear == schoolYear && rewardInfo.Semester == semester && rewardInfo.OccurDate == occurdate && rewardInfo.OccurReason == reason )
                            {
                                match = true;
                                #region 其他項目
                                cleared = e.ImportFields.Contains("是否銷過") ?
                                    ( ( row["是否銷過"] == "是" ) ? true : false ) :
                                    rewardInfo.Cleared;

                                cleardate = ( e.ImportFields.Contains("銷過日期") && row["銷過日期"] != "" ) ?
                                    DateTime.Parse(row["銷過日期"]) :
                                    rewardInfo.ClearDate;

                                clearreason = e.ImportFields.Contains("銷過事由") ?
                                    row["銷過事由"] :
                                    rewardInfo.ClearReason;

                                ultimateAdmonition = e.ImportFields.Contains("留校察看") ?
                                    ( ( row["留校察看"] == "是" ) ? true : false ) :
                                    rewardInfo.UltimateAdmonition;
                                #endregion
                                DSXmlHelper h = new DSXmlHelper("Discipline");
                                isAward = awardA + awardB + awardC > 0;
                                if ( isAward )
                                {
                                    XmlElement element = h.AddElement("Merit");
                                    element.SetAttribute("A", awardA.ToString());
                                    element.SetAttribute("B", awardB.ToString());
                                    element.SetAttribute("C", awardC.ToString());
                                }
                                else
                                {
                                    XmlElement element = h.AddElement("Demerit");
                                    element.SetAttribute("A", faultA.ToString());
                                    element.SetAttribute("B", faultB.ToString());
                                    element.SetAttribute("C", faultC.ToString());
                                    element.SetAttribute("Cleared", cleared ? "是" : "否");
                                    element.SetAttribute("ClearDate", cleared ? cleardate.ToShortDateString() : "");
                                    element.SetAttribute("ClearReason", clearreason);
                                }
                                updateHelper.AddElement("Discipline");
                                updateHelper.AddElement("Discipline", "Field");
                                updateHelper.AddElement("Discipline/Field", "MeritFlag", ultimateAdmonition ? "2" : isAward ? "1" : "0");
                                updateHelper.AddElement("Discipline/Field", "Detail", h.GetRawXml(), true);
                                updateHelper.AddElement("Discipline", "Condition");
                                updateHelper.AddElement("Discipline/Condition", "ID", rewardInfo.Detail.GetAttribute("ID"));

                                hasUpdate = true;
                                break;
                            }
                        }
                        if ( !match )
                        {
                            DSXmlHelper h = new DSXmlHelper("Discipline");
                            isAward = awardA + awardB + awardC > 0;
                            if ( isAward )
                            {
                                XmlElement element = h.AddElement("Merit");
                                element.SetAttribute("A", awardA.ToString());
                                element.SetAttribute("B", awardB.ToString());
                                element.SetAttribute("C", awardC.ToString());
                            }
                            else
                            {
                                XmlElement element = h.AddElement("Demerit");
                                element.SetAttribute("A", faultA.ToString());
                                element.SetAttribute("B", faultB.ToString());
                                element.SetAttribute("C", faultC.ToString());
                                element.SetAttribute("Cleared", cleared ? "是" : "否");
                                element.SetAttribute("ClearDate", cleared?cleardate.ToShortDateString():"");
                                element.SetAttribute("ClearReason", clearreason);
                            }
                            insertHelper.AddElement("Discipline");
                            insertHelper.AddElement("Discipline", "RefStudentID", row.ID);
                            insertHelper.AddElement("Discipline", "SchoolYear", schoolYear.ToString());
                            insertHelper.AddElement("Discipline", "Semester", semester.ToString());
                            insertHelper.AddElement("Discipline", "OccurDate", occurdate.ToShortDateString());
                            insertHelper.AddElement("Discipline", "Reason", reason);
                            insertHelper.AddElement("Discipline", "MeritFlag", ultimateAdmonition ? "2" : isAward ? "1" : "0");
                            insertHelper.AddElement("Discipline", "Type", "1");
                            insertHelper.AddElement("Discipline", "Detail", h.GetRawXml(), true);

                            hasInsert = true;
                        }
                        #endregion
                    }
                    if ( chose2.Checked )
                    {
                        #region 以次數為Key更新
                        bool isAward;
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
                        string reason = row.ContainsKey("事由") ? row["事由"] : "";

                        if ( row.ContainsKey("大功") )
                            awardA = ( row["大功"] == "" ) ? 0 : int.Parse(row["大功"]);
                        if ( row.ContainsKey("小功") )
                            awardB = ( row["小功"] == "" ) ? 0 : int.Parse(row["小功"]);
                        if ( row.ContainsKey("嘉獎") )
                            awardC = ( row["嘉獎"] == "" ) ? 0 : int.Parse(row["嘉獎"]);
                        if ( row.ContainsKey("大過") )
                            faultA = ( row["大過"] == "" ) ? 0 : int.Parse(row["大過"]);
                        if ( row.ContainsKey("小過") )
                            faultB = ( row["小過"] == "" ) ? 0 : int.Parse(row["小過"]);
                        if ( row.ContainsKey("警告") )
                            faultC = ( row["警告"] == "" ) ? 0 : int.Parse(row["警告"]);
                        cleared = e.ImportFields.Contains("是否銷過") ?
                            ( ( row["是否銷過"] == "是" ) ? true : false ) : false;

                        cleardate = (e.ImportFields.Contains("銷過日期") && row["銷過日期"] != "") ?
                            DateTime.Parse(row["銷過日期"]) : DateTime.Now;

                        clearreason = e.ImportFields.Contains("銷過事由") ?
                            row["銷過事由"] : "";

                        ultimateAdmonition = e.ImportFields.Contains("留校察看") ?
                            ( ( row["留校察看"] == "是" ) ? true : false ) : false;

                        bool match = false;
                        foreach ( RewardInfo rewardInfo in accessHelper.StudentHelper.GetStudent(row.ID).RewardList )
                        {
                            if ( rewardInfo.SchoolYear == schoolYear &&
                                rewardInfo.Semester == semester &&
                                rewardInfo.OccurDate == occurdate &&
                                rewardInfo.AwardA == awardA &&
                                rewardInfo.AwardB == awardB &&
                                rewardInfo.AwardC == awardC &&
                                rewardInfo.FaultA == faultA &&
                                rewardInfo.FaultB == faultB &&
                                rewardInfo.FaultC == faultC )
                            {
                                match = true;
                                #region 其他項目
                                reason = e.ImportFields.Contains("事由") ? row["事由"] : rewardInfo.OccurReason;

                                cleared = e.ImportFields.Contains("是否銷過") ?
                                    ( ( row["是否銷過"] == "是" ) ? true : false ) :
                                    rewardInfo.Cleared;

                                cleardate = ( e.ImportFields.Contains("銷過日期") && row["銷過日期"] != "" ) ?
                                    DateTime.Parse(row["銷過日期"]) :
                                    rewardInfo.ClearDate;

                                clearreason = e.ImportFields.Contains("銷過事由") ?
                                    row["銷過事由"] :
                                    rewardInfo.ClearReason;

                                ultimateAdmonition = e.ImportFields.Contains("留校察看") ?
                                    ( ( row["留校察看"] == "是" ) ? true : false ) :
                                    rewardInfo.UltimateAdmonition;
                                #endregion
                                DSXmlHelper h = new DSXmlHelper("Discipline");
                                isAward = awardA + awardB + awardC > 0;
                                if ( isAward )
                                {
                                    XmlElement element = h.AddElement("Merit");
                                    element.SetAttribute("A", awardA.ToString());
                                    element.SetAttribute("B", awardB.ToString());
                                    element.SetAttribute("C", awardC.ToString());
                                }
                                else
                                {
                                    XmlElement element = h.AddElement("Demerit");
                                    element.SetAttribute("A", faultA.ToString());
                                    element.SetAttribute("B", faultB.ToString());
                                    element.SetAttribute("C", faultC.ToString());
                                    element.SetAttribute("Cleared", cleared ? "是" : "否");
                                    element.SetAttribute("ClearDate", cleared ? cleardate.ToShortDateString() : "");
                                    element.SetAttribute("ClearReason", clearreason);
                                }
                                updateHelper.AddElement("Discipline");
                                updateHelper.AddElement("Discipline", "Field");
                                updateHelper.AddElement("Discipline/Field", "MeritFlag", ultimateAdmonition ? "2" : isAward ? "1" : "0");
                                updateHelper.AddElement("Discipline/Field", "Detail", h.GetRawXml(), true);
                                updateHelper.AddElement("Discipline/Field", "Reason", reason);
                                updateHelper.AddElement("Discipline", "Condition");
                                updateHelper.AddElement("Discipline/Condition", "ID", rewardInfo.Detail.GetAttribute("ID"));

                                hasUpdate = true;
                                break;
                            }
                        }
                        if ( !match )
                        {
                            DSXmlHelper h = new DSXmlHelper("Discipline");
                            isAward = awardA + awardB + awardC > 0;
                            if ( isAward )
                            {
                                XmlElement element = h.AddElement("Merit");
                                element.SetAttribute("A", awardA.ToString());
                                element.SetAttribute("B", awardB.ToString());
                                element.SetAttribute("C", awardC.ToString());
                            }
                            else
                            {
                                XmlElement element = h.AddElement("Demerit");
                                element.SetAttribute("A", faultA.ToString());
                                element.SetAttribute("B", faultB.ToString());
                                element.SetAttribute("C", faultC.ToString());
                                element.SetAttribute("Cleared", cleared ? "是" : "否");
                                element.SetAttribute("ClearDate", cleared ? cleardate.ToShortDateString() : "");
                                element.SetAttribute("ClearReason", clearreason);
                            }
                            insertHelper.AddElement("Discipline");
                            insertHelper.AddElement("Discipline", "RefStudentID", row.ID);
                            insertHelper.AddElement("Discipline", "SchoolYear", schoolYear.ToString());
                            insertHelper.AddElement("Discipline", "Semester", semester.ToString());
                            insertHelper.AddElement("Discipline", "OccurDate", occurdate.ToShortDateString());
                            insertHelper.AddElement("Discipline", "Reason", reason);
                            insertHelper.AddElement("Discipline", "MeritFlag", ultimateAdmonition ? "2" : isAward ? "1" : "0");
                            insertHelper.AddElement("Discipline", "Type", "1");
                            insertHelper.AddElement("Discipline", "Detail", h.GetRawXml(), true);

                            hasInsert = true;
                        }
                        #endregion
                    }
                }
                if ( hasUpdate )
                    SmartSchool.Feature.Student.EditDiscipline.Update(new DSRequest(updateHelper));
                if ( hasInsert )
                    SmartSchool.Feature.Student.EditDiscipline.Insert(new DSRequest(insertHelper));
            };
        }
    }
}
