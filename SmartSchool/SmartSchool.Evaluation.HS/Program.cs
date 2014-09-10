﻿using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Evaluation.Configuration;
using SmartSchool.Evaluation.Reports;
using DevComponents.DotNetBar;
using SmartSchool.Evaluation.Reports.MultiSemesterScore;
using SmartSchool.Evaluation.Content;
using System.ComponentModel;
using System.Threading;
using IntelliSchool.DSA30.Util;
using Aspose.Cells;
using System.IO;
using System.Windows.Forms;
using SmartSchool;
using System.Xml;
using SmartSchool.AccessControl;
using SmartSchool.Common;
using SmartSchool.Customization.Data;

namespace SmartSchool.Evaluation
{
    public static class Program
    {
        [MainMethod()]
        public static void Main()
        {
            if ( File.Exists(System.IO.Path.Combine(Application.StartupPath, "阿寶萬歲萬歲萬萬歲")) ) return;
            //讀取課程規畫表、成績計算規則
            SmartSchool.Evaluation.GraduationPlan.GraduationPlan.CreateInstance();
            SmartSchool.Evaluation.ScoreCalcRule.ScoreCalcRule.CreateInstance();

            //載入成績系統相關設定
            if (CurrentUser.Acl["Button0830"].Executable)
                SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.AddConfigurationItem(new ScoreCalcConfiguration());
            if (CurrentUser.Acl["Button0780"].Executable)
                SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.AddConfigurationItem(new MoralConductConfiguration());
            if (CurrentUser.Acl["Button0860"].Executable)
                SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.AddConfigurationItem(new GraduationPlanConfiguration());
            if (CurrentUser.Acl["Button0870"].Executable)
                SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.AddConfigurationItem(new CommonPlanConfiguration());
            if (CurrentUser.Acl["Button0840"].Executable)
                SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.AddConfigurationItem(new SubjectTableConfiguration("核心科目表"));
            if (CurrentUser.Acl["Button0850"].Executable)
                SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.AddConfigurationItem(new SubjectTableConfiguration("學程科目表"));

            //載入有的沒有的按鈕
            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonItem>.Instance[@"學生/教務作業"].Add((ButtonItem)new Process.Calculation().ProcessRibbon.Items[0]);
            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonItem>.Instance[@"學生/學務作業"].Add((ButtonItem)new Process.Calculation().ProcessRibbon.Items[1]);

            ButtonItem bi = (ButtonItem)new Process.CalculationBatch().ProcessRibbon.Items[0];
            bi.Enabled = CurrentUser.Acl["Button0670"].Executable;
            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonItem>.Instance[@"教務作業/成績作業"].Add(bi);

            bi = (ButtonItem)new Process.Retake().ProcessRibbon.Items[0];
            bi.Enabled = CurrentUser.Acl["Button0680"].Executable;
            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonItem>.Instance[@"教務作業/成績作業"].Add(bi);

            bi = (ButtonItem)new Process.Resit().ProcessRibbon.Items[0];
            bi.Enabled = CurrentUser.Acl["Button0690"].Executable;
            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonItem>.Instance[@"教務作業/成績作業"].Add(bi);

            bi = (ButtonItem)new Process.CalculationBatch().ProcessRibbon.Items[1];
            bi.Enabled = CurrentUser.Acl["Button0705"].Executable;
            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonItem>.Instance[@"學務作業/成績作業"].Add(bi);

            new SmartSchool.Evaluation.Process.AssignClass();//指定班級課程規劃&計算規則
            new SmartSchool.Evaluation.Process.AssignStudent();//指定學生課程規劃&計算規則
            //載入匯出匯入按鈕
            SmartSchool.API.PlugIn.PlugInManager.Student.Exporters.Add(new ImportExport.ExportSemesterSubjectScore());
            SmartSchool.API.PlugIn.PlugInManager.Student.Importers.Add(new ImportExport.ImportSemesterSubjectScore());
            SmartSchool.API.PlugIn.PlugInManager.Student.Exporters.Add(new ImportExport.ExportSemesterEntryScore());
            SmartSchool.Customization.PlugIn.ImportExport.ImportStudent.AddProcess(new ImportExport.ImportSemesterEntryScore());
            SmartSchool.API.PlugIn.PlugInManager.Student.Exporters.Add(new ImportExport.ExportSchoolYearSubjectScore());
            SmartSchool.Customization.PlugIn.ImportExport.ImportStudent.AddProcess(new ImportExport.ImportSchoolYearSubjectScore());
            SmartSchool.API.PlugIn.PlugInManager.Student.Exporters.Add(new ImportExport.ExportSchoolYearEntryScore());
            SmartSchool.Customization.PlugIn.ImportExport.ImportStudent.AddProcess(new ImportExport.ImportSchoolYearEntryScore());
            SmartSchool.Customization.PlugIn.ImportExport.ExportStudent.AddProcess(new ImportExport.ExportGradScore());
            //鍵至順便載入報表按鈕
            new SemesterMoralScoreCalc();
            new SemesterMoralScoreTotal();
            new SemesterScoreReport();
            SemesterScoreReportNew.RegistryFeature();
            new ClassSemesterScore();
            new MultiSemesterScore();
            new Reports.CreditStatistic.CreditStatistic(); //學分統計表
            new Button1();//成績相關報表/建議重修名單
            new SmartSchool.Evaluation.Process.CreateCourceForClass();
            SmartSchool.Customization.PlugIn.Report.StudentReport.AddReport(new SmartSchool.Evaluation.Reports.Retake.RetakeWithCourseList());

            //載入毛毛蟲
            SmartSchool.Customization.PlugIn.ExtendedContent.ExtendStudentContent.AddItem(new SemesterScorePalmerworm());
            SmartSchool.Customization.PlugIn.ExtendedContent.ExtendStudentContent.AddItem(new SchoolYearScorePalmerworm());
            SmartSchool.Customization.PlugIn.ExtendedContent.ExtendStudentContent.AddItem(new GradScorePalmerworm());
            //插入資料行
            SmartSchool.Customization.PlugIn.ExtendedColumn.ExtendClassColumn.AddItem(new SmartSchool.Evaluation.ExtandColumn.ClassScoreCalcRule());
            SmartSchool.Customization.PlugIn.ExtendedColumn.ExtendStudentColumn.AddItem(new SmartSchool.Evaluation.ExtandColumn.StudentCalcRule());
            SmartSchool.Customization.PlugIn.ExtendedColumn.ExtendClassColumn.AddItem(new SmartSchool.Evaluation.ExtandColumn.ClassGraduationPlan());
            SmartSchool.Customization.PlugIn.ExtendedColumn.ExtendStudentColumn.AddItem(new SmartSchool.Evaluation.ExtandColumn.StudentGraduationPlan());
            //處理取得學生資料
            SmartSchool.Customization.Data.StudentHelper.FillingField += new EventHandler<FillFieldEventArgs<StudentRecord>>(StudentHelper_FillingField);
            //SmartSchool.API.Provider.StudentProvider.FillData += new EventHandler<SmartSchool.API.Provider.FillStudentEventArgs>(StudentProvider_FillData);
            //處理取得系統相關資訊
            SmartSchool.Customization.Data.SystemInformation.GettingField += new EventHandler<GetFieldEventArgs>(SystemInformation_GettingField);
            //SmartSchool.API.Provider.SystemProvider.GetField += new EventHandler<SmartSchool.API.Provider.GetSystemFieldEventArgs>(SystemProvider_GetField);
        }

        static void SystemInformation_GettingField(object sender, GetFieldEventArgs e)
        {
            switch (e.FieldName)
            {
                case "缺曠扣分方式":
                    //整理現有的假別跟節次
                    List<string> periodList = new List<string>();
                    List<string> absenceList = new List<string>();
                    #region 取得節次類別與缺曠對照表

                    //取得節次對照表
                    foreach (XmlElement var in SmartSchool.Feature.Basic.Config.GetPeriodList().GetContent().GetElements("Period"))
                    {
                        string name = var.GetAttribute("Type");
                        if (!periodList.Contains(name))
                            periodList.Add(name);
                    }
                    //取得假別對照表
                    foreach (XmlElement var in SmartSchool.Feature.Basic.Config.GetAbsenceList().GetContent().GetElements("Absence"))
                    {
                        //假別清單
                        string name = var.GetAttribute("Name");
                        if (!absenceList.Contains(name))
                            absenceList.Add(name);
                    }
                    #endregion
                    #region 填入缺曠扣分方式
                    XmlElement _MoralConductElement = SmartSchool.Feature.ScoreCalcRule.QueryScoreCalcRule.GetMoralConductCalcRule();
                    DSXmlHelper _MoralConductHelper;
                    if (_MoralConductElement != null)
                        _MoralConductHelper = new DSXmlHelper(_MoralConductElement);
                    else
                        _MoralConductHelper = new DSXmlHelper();

                    XmlDocument doc = new XmlDocument();
                    XmlElement root = doc.CreateElement("缺曠扣分方式");
                    foreach (XmlElement element in _MoralConductHelper.GetElements("PeriodAbsenceCalcRule/Rule"))
                    {
                        XmlElement ele = doc.CreateElement("扣分設定");
                        ele.SetAttribute("假別", element.GetAttribute("Absence"));
                        ele.SetAttribute("節次類別", element.GetAttribute("Period"));
                        decimal subtract = 0;
                        decimal.TryParse(element.GetAttribute("Subtract"), out subtract);
                        int aggregated = 0;
                        int.TryParse(element.GetAttribute("Aggregated"), out aggregated);
                        ele.SetAttribute("累計次數", "" + aggregated);
                        ele.SetAttribute("扣分", "" + subtract);
                        if (periodList.Contains(element.GetAttribute("Period")) && absenceList.Contains(element.GetAttribute("Absence")))//是現有的假別跟節次
                            root.AppendChild(ele);
                    }
                    //e.Result = root;
                    if (!SmartSchool.Customization.Data.SystemInformation.Fields.ContainsKey("缺曠扣分方式"))
                        SmartSchool.Customization.Data.SystemInformation.Fields.Add("缺曠扣分方式", root);
                    else
                        SmartSchool.Customization.Data.SystemInformation.Fields["缺曠扣分方式"] = root;
                    break;
                    #endregion
            }
        }

        static void StudentHelper_FillingField(object sender, FillFieldEventArgs<StudentRecord> e)
        {
            switch (e.FieldName)
            {
                case "補考標準":
                    #region 填入補考標準
                    foreach (StudentRecord var in e.List)
                    {
                        //補考標準<年及,補考標準>
                        Dictionary<int, decimal> resitLimit = new Dictionary<int, decimal>();
                        #region 取得成績年級跟計算規則
                        {
                            #region 處理計算規則
                            XmlElement scoreCalcRule = ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID) == null ? null : ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID).ScoreCalcRuleElement;
                            if (scoreCalcRule != null)
                            {
                                #region 有設定計算規則
                                DSXmlHelper helper = new DSXmlHelper(scoreCalcRule);
                                bool tryParsebool;
                                int tryParseint;
                                decimal tryParseDecimal;
                                foreach (XmlElement element in helper.GetElements("及格標準/學生類別"))
                                {
                                    string cat = element.GetAttribute("類別");
                                    bool useful = false;
                                    //掃描學生的類別作比對
                                    foreach (CategoryInfo catinfo in var.StudentCategorys)
                                    {
                                        if (catinfo.Name == cat || catinfo.FullName == cat)
                                            useful = true;
                                    }
                                    //學生是指定的類別或類別為"預設"
                                    if (cat == "預設" || useful)
                                    {
                                        for (int gyear = 1; gyear <= 4; gyear++)
                                        {
                                            switch (gyear)
                                            {
                                                case 1:
                                                    if (decimal.TryParse(element.GetAttribute("一年級補考標準"), out tryParseDecimal))
                                                    {
                                                        if (!resitLimit.ContainsKey(gyear))
                                                            resitLimit.Add(gyear, tryParseDecimal);
                                                        if (resitLimit[gyear] > tryParseDecimal)
                                                            resitLimit[gyear] = tryParseDecimal;
                                                    }
                                                    break;
                                                case 2:
                                                    if (decimal.TryParse(element.GetAttribute("二年級補考標準"), out tryParseDecimal))
                                                    {
                                                        if (!resitLimit.ContainsKey(gyear))
                                                            resitLimit.Add(gyear, tryParseDecimal);
                                                        if (resitLimit[gyear] > tryParseDecimal)
                                                            resitLimit[gyear] = tryParseDecimal;
                                                    }
                                                    break;
                                                case 3:
                                                    if (decimal.TryParse(element.GetAttribute("三年級補考標準"), out tryParseDecimal))
                                                    {
                                                        if (!resitLimit.ContainsKey(gyear))
                                                            resitLimit.Add(gyear, tryParseDecimal);
                                                        if (resitLimit[gyear] > tryParseDecimal)
                                                            resitLimit[gyear] = tryParseDecimal;
                                                    }
                                                    break;
                                                case 4:
                                                    if (decimal.TryParse(element.GetAttribute("四年級補考標準"), out tryParseDecimal))
                                                    {
                                                        if (!resitLimit.ContainsKey(gyear))
                                                            resitLimit.Add(gyear, tryParseDecimal);
                                                        if (resitLimit[gyear] > tryParseDecimal)
                                                            resitLimit[gyear] = tryParseDecimal;
                                                    }
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                        for (int i = 1; i <= 4; i++)//1-4年級如果某個年級沒有補考標準就填入40
                        {
                            if (!resitLimit.ContainsKey(i))
                                resitLimit.Add(i, 40);
                        }
                        //填入補考標準
                        if (!var.Fields.ContainsKey("補考標準"))
                            var.Fields.Add("補考標準", resitLimit);
                        else
                            var.Fields["補考標準"] = resitLimit;
                    }
                    break;
                    #endregion
                case "及格標準":
                    #region 填入及格標準
                    foreach (StudentRecord var in e.List)
                    {
                        //補考標準<年及,補考標準>
                        Dictionary<int, decimal> applyLimit = new Dictionary<int, decimal>();
                        #region 取得成績年級跟計算規則
                        {
                            #region 處理計算規則
                            XmlElement scoreCalcRule = ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID) == null ? null : ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID).ScoreCalcRuleElement;
                            if (scoreCalcRule != null)
                            {
                                #region 有設定計算規則
                                DSXmlHelper helper = new DSXmlHelper(scoreCalcRule);
                                bool tryParsebool;
                                int tryParseint;
                                decimal tryParseDecimal;
                                foreach (XmlElement element in helper.GetElements("及格標準/學生類別"))
                                {
                                    string cat = element.GetAttribute("類別");
                                    bool useful = false;
                                    //掃描學生的類別作比對
                                    foreach (CategoryInfo catinfo in var.StudentCategorys)
                                    {
                                        if (catinfo.Name == cat || catinfo.FullName == cat)
                                            useful = true;
                                    }
                                    //學生是指定的類別或類別為"預設"
                                    if (cat == "預設" || useful)
                                    {
                                        for (int gyear = 1; gyear <= 4; gyear++)
                                        {
                                            switch (gyear)
                                            {
                                                case 1:
                                                    if (decimal.TryParse(element.GetAttribute("一年級及格標準"), out tryParseDecimal))
                                                    {
                                                        if (!applyLimit.ContainsKey(gyear))
                                                            applyLimit.Add(gyear, tryParseDecimal);
                                                        if (applyLimit[gyear] > tryParseDecimal)
                                                            applyLimit[gyear] = tryParseDecimal;
                                                    }
                                                    break;
                                                case 2:
                                                    if (decimal.TryParse(element.GetAttribute("二年級及格標準"), out tryParseDecimal))
                                                    {
                                                        if (!applyLimit.ContainsKey(gyear))
                                                            applyLimit.Add(gyear, tryParseDecimal);
                                                        if (applyLimit[gyear] > tryParseDecimal)
                                                            applyLimit[gyear] = tryParseDecimal;
                                                    }
                                                    break;
                                                case 3:
                                                    if (decimal.TryParse(element.GetAttribute("三年級及格標準"), out tryParseDecimal))
                                                    {
                                                        if (!applyLimit.ContainsKey(gyear))
                                                            applyLimit.Add(gyear, tryParseDecimal);
                                                        if (applyLimit[gyear] > tryParseDecimal)
                                                            applyLimit[gyear] = tryParseDecimal;
                                                    }
                                                    break;
                                                case 4:
                                                    if (decimal.TryParse(element.GetAttribute("四年級及格標準"), out tryParseDecimal))
                                                    {
                                                        if (!applyLimit.ContainsKey(gyear))
                                                            applyLimit.Add(gyear, tryParseDecimal);
                                                        if (applyLimit[gyear] > tryParseDecimal)
                                                            applyLimit[gyear] = tryParseDecimal;
                                                    }
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                        for (int i = 1; i <= 4; i++)//1-4年級如果某個年級沒有及格標準就填入60
                        {
                            if (!applyLimit.ContainsKey(i))
                                applyLimit.Add(i, 60);
                        }
                        //填入及格標準
                        if (!var.Fields.ContainsKey("及格標準"))
                            var.Fields.Add("及格標準", applyLimit);
                        else
                            var.Fields["及格標準"] = applyLimit;
                    }
                    break;
                    #endregion
                case "本學期德行表現成績":
                    #region 填入當學期德行表現成績
                    new AngelDemonComputer().FillDemonScore(
                                        new AccessHelper(),
                                        SmartSchool.Customization.Data.SystemInformation.SchoolYear,
                                        SmartSchool.Customization.Data.SystemInformation.Semester,
                                        new List<StudentRecord>(e.List)
                                        );
                    foreach (StudentRecord var in e.List)
                    {
                        if (var.Fields.ContainsKey("DemonScore"))
                        {
                            var.Fields.Add("本學期德行表現成績", var.Fields["DemonScore"]);
                            var.Fields.Remove("DemonScore");
                        }
                        else
                        {
                            var.Fields.Add("本學期德行表現成績", new XmlDocument().CreateElement("DemonScore"));
                        }
                    }
                    break;
                    #endregion
                case "畢業成績":
                    #region 填入畢業成績
                    Dictionary<string, StudentRecord> students = new Dictionary<string, StudentRecord>();
                    foreach (StudentRecord var in e.List)
                    {
                        if (!students.ContainsKey(var.StudentID))
                            students.Add(var.StudentID, var);
                    }
                    Dictionary<string, Dictionary<string, decimal>> sid_entry_score = new Dictionary<string, Dictionary<string, decimal>>();
                    foreach (XmlElement studentElement in SmartSchool.Feature.QueryStudent.GetDetailList(new string[] { "ID", "GradScore" }, new List<string>(students.Keys).ToArray()).GetContent().GetElements("Student"))
                    {
                        string ID = studentElement.GetAttribute("ID");
                        foreach (XmlElement scoreElement in studentElement.SelectNodes("GradScore/GradScore/EntryScore"))
                        {
                            if (!sid_entry_score.ContainsKey(ID))
                                sid_entry_score.Add(ID, new Dictionary<string, decimal>());
                            string entry = scoreElement.GetAttribute("Entry");
                            decimal dec;
                            if (decimal.TryParse(scoreElement.GetAttribute("Score"), out dec))
                            {
                                if (!sid_entry_score[ID].ContainsKey(entry))
                                    sid_entry_score[ID].Add(entry, dec);
                            }
                        }
                    }
                    foreach (StudentRecord student in students.Values)
                    {
                        Dictionary<string, decimal> entryScore = sid_entry_score.ContainsKey(student.StudentID) ? sid_entry_score[student.StudentID] : new Dictionary<string, decimal>();
                        if (student.Fields.ContainsKey("畢業成績"))
                            student.Fields["畢業成績"] = entryScore;
                        else
                            student.Fields.Add("畢業成績", entryScore);
                    }
                    break;
                    #endregion
                case "核心科目表":
                    #region 填入核心科目表
                    foreach (StudentRecord student in e.List)
                    {
                        XmlDocument doc = new XmlDocument();
                        XmlElement root = doc.CreateElement("核心科目表");
                        #region 預設學程核心科目表
                        if (
                            student.Fields.ContainsKey("SubDepartment") &&//科別有子項
                            SubjectTable.Items["學程科目表"].Contains("" + student.Fields["SubDepartment"]) //子項是一個學程名稱
                            )
                        {
                            //預設學程科目表
                            XmlElement contentElement = (XmlElement)SubjectTable.Items["學程科目表"]["" + student.Fields["SubDepartment"]].Content.SelectSingleNode("SubjectTableContent");
                            foreach (XmlElement snode in contentElement.SelectNodes("Subject"))
                            {
                                string name = ((XmlElement)snode).GetAttribute("Name");
                                bool iscore = false;
                                bool.TryParse(snode.GetAttribute("IsCore"), out iscore);
                                if (iscore)
                                {
                                    if (snode.SelectNodes("Level").Count == 0)
                                    {
                                        XmlElement subjectElement = doc.CreateElement("核心科目");
                                        subjectElement.SetAttribute("科目", name);
                                        subjectElement.SetAttribute("級別", "");
                                        subjectElement.SetAttribute("來源", "預設學程科目表");
                                        root.AppendChild(subjectElement);
                                    }
                                    else
                                    {
                                        foreach (XmlNode lnode in snode.SelectNodes("Level"))
                                        {
                                            XmlElement subjectElement = doc.CreateElement("核心科目");
                                            subjectElement.SetAttribute("科目", name);
                                            subjectElement.SetAttribute("級別", lnode.InnerText);
                                            subjectElement.SetAttribute("來源", "預設學程科目表");
                                            root.AppendChild(subjectElement);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        #region 設定的核心科目表
                        XmlElement scoreCalcRule = ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(student.StudentID) == null ? null : ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(student.StudentID).ScoreCalcRuleElement;
                        if (scoreCalcRule != null)
                        {
                            List<string> checkedList = new List<string>();
                            #region 整理要選用的科目表
                            foreach (XmlNode st in scoreCalcRule.SelectNodes("核心科目表/科目表"))
                            {
                                checkedList.Add(st.InnerText);
                            }
                            #endregion
                            foreach (SubjectTableItem subjectTable in SubjectTable.Items["核心科目表"])
                            {
                                #region 是要選用的科目表就進行判斷
                                if (checkedList.Contains(subjectTable.Name) && subjectTable.Content.SelectSingleNode("SubjectTableContent") != null)
                                {
                                    XmlElement contentElement = (XmlElement)subjectTable.Content.SelectSingleNode("SubjectTableContent");
                                    #region 整理在科目表中所有的科目級別
                                    foreach (XmlNode snode in contentElement.SelectNodes("Subject"))
                                    {
                                        string name = ((XmlElement)snode).GetAttribute("Name");
                                        if (snode.SelectNodes("Level").Count == 0)
                                        {
                                            XmlElement subjectElement = doc.CreateElement("核心科目");
                                            subjectElement.SetAttribute("科目", name);
                                            subjectElement.SetAttribute("級別", "");
                                            subjectElement.SetAttribute("來源", subjectTable.Name);
                                            root.AppendChild(subjectElement);
                                        }
                                        else
                                        {
                                            foreach (XmlNode lnode in snode.SelectNodes("Level"))
                                            {
                                                XmlElement subjectElement = doc.CreateElement("核心科目");
                                                subjectElement.SetAttribute("科目", name);
                                                subjectElement.SetAttribute("級別", lnode.InnerText);
                                                subjectElement.SetAttribute("來源", subjectTable.Name);
                                                root.AppendChild(subjectElement);
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                        }
                        #endregion
                        if (student.Fields.ContainsKey("核心科目表"))
                            student.Fields["核心科目表"] = root;
                        else
                            student.Fields.Add("核心科目表", root);
                    }
                    break;
                    #endregion
            }

        }

        //static void SystemProvider_GetField(object sender, SmartSchool.API.Provider.GetSystemFieldEventArgs e)
        //{
        //    switch ( e.FieldName )
        //    {
        //        case "缺曠扣分方式":
        //            //整理現有的假別跟節次
        //            List<string> periodList = new List<string>();
        //            List<string> absenceList = new List<string>();
        //            #region 取得節次類別與缺曠對照表

        //            //取得節次對照表
        //            foreach ( XmlElement var in SmartSchool.Feature.Basic.Config.GetPeriodList().GetContent().GetElements("Period") )
        //            {
        //                string name = var.GetAttribute("Type");
        //                if ( !periodList.Contains(name) )
        //                    periodList.Add(name);
        //            }
        //            //取得假別對照表
        //            foreach ( XmlElement var in SmartSchool.Feature.Basic.Config.GetAbsenceList().GetContent().GetElements("Absence") )
        //            {
        //                //假別清單
        //                string name = var.GetAttribute("Name");
        //                if ( !absenceList.Contains(name) )
        //                    absenceList.Add(name);
        //            }
        //            #endregion
        //            #region 填入缺曠扣分方式
        //            XmlElement _MoralConductElement = SmartSchool.Feature.ScoreCalcRule.QueryScoreCalcRule.GetMoralConductCalcRule();
        //            DSXmlHelper _MoralConductHelper;
        //            if ( _MoralConductElement != null )
        //                _MoralConductHelper = new DSXmlHelper(_MoralConductElement);
        //            else
        //                _MoralConductHelper = new DSXmlHelper();

        //            XmlDocument doc = new XmlDocument();
        //            XmlElement root = doc.CreateElement("缺曠扣分方式");
        //            foreach ( XmlElement element in _MoralConductHelper.GetElements("PeriodAbsenceCalcRule/Rule") )
        //            {
        //                XmlElement ele = doc.CreateElement("扣分設定");
        //                ele.SetAttribute("假別", element.GetAttribute("Absence"));
        //                ele.SetAttribute("節次類別", element.GetAttribute("Period"));
        //                decimal subtract = 0;
        //                decimal.TryParse(element.GetAttribute("Subtract"), out subtract);
        //                int aggregated = 0;
        //                int.TryParse(element.GetAttribute("Aggregated"), out aggregated);
        //                ele.SetAttribute("累計次數", "" + aggregated);
        //                ele.SetAttribute("扣分", "" + subtract);
        //                if ( periodList.Contains(element.GetAttribute("Period")) && absenceList.Contains(element.GetAttribute("Absence")) )//是現有的假別跟節次
        //                    root.AppendChild(ele);
        //            }
        //            e.Result = root;
        //            break; 
        //            #endregion
        //    }
        //}

        //static void StudentProvider_FillData(object sender, SmartSchool.API.Provider.FillStudentEventArgs e)
        //{
        //    if ( e.FillMethod == "FillField" )
        //    {
        //        switch ( e.Args[0] as string )
        //        {
        //            case "補考標準":
        //                #region 填入補考標準
        //                foreach ( StudentRecord var in e.Items )
        //                {
        //                    //補考標準<年及,補考標準>
        //                    Dictionary<int, decimal> resitLimit = new Dictionary<int, decimal>();
        //                    #region 取得成績年級跟計算規則
        //                    {
        //                        #region 處理計算規則
        //                        XmlElement scoreCalcRule = ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID) == null ? null : ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID).ScoreCalcRuleElement;
        //                        if ( scoreCalcRule != null )
        //                        {
        //                            #region 有設定計算規則
        //                            DSXmlHelper helper = new DSXmlHelper(scoreCalcRule);
        //                            bool tryParsebool;
        //                            int tryParseint;
        //                            decimal tryParseDecimal;
        //                            foreach ( XmlElement element in helper.GetElements("及格標準/學生類別") )
        //                            {
        //                                string cat = element.GetAttribute("類別");
        //                                bool useful = false;
        //                                //掃描學生的類別作比對
        //                                foreach ( CategoryInfo catinfo in var.StudentCategorys )
        //                                {
        //                                    if ( catinfo.Name == cat || catinfo.FullName == cat )
        //                                        useful = true;
        //                                }
        //                                //學生是指定的類別或類別為"預設"
        //                                if ( cat == "預設" || useful )
        //                                {
        //                                    for ( int gyear = 1 ; gyear <= 4 ; gyear++ )
        //                                    {
        //                                        switch ( gyear )
        //                                        {
        //                                            case 1:
        //                                                if ( decimal.TryParse(element.GetAttribute("一年級補考標準"), out tryParseDecimal) )
        //                                                {
        //                                                    if ( !resitLimit.ContainsKey(gyear) )
        //                                                        resitLimit.Add(gyear, tryParseDecimal);
        //                                                    if ( resitLimit[gyear] > tryParseDecimal )
        //                                                        resitLimit[gyear] = tryParseDecimal;
        //                                                }
        //                                                break;
        //                                            case 2:
        //                                                if ( decimal.TryParse(element.GetAttribute("二年級補考標準"), out tryParseDecimal) )
        //                                                {
        //                                                    if ( !resitLimit.ContainsKey(gyear) )
        //                                                        resitLimit.Add(gyear, tryParseDecimal);
        //                                                    if ( resitLimit[gyear] > tryParseDecimal )
        //                                                        resitLimit[gyear] = tryParseDecimal;
        //                                                }
        //                                                break;
        //                                            case 3:
        //                                                if ( decimal.TryParse(element.GetAttribute("三年級補考標準"), out tryParseDecimal) )
        //                                                {
        //                                                    if ( !resitLimit.ContainsKey(gyear) )
        //                                                        resitLimit.Add(gyear, tryParseDecimal);
        //                                                    if ( resitLimit[gyear] > tryParseDecimal )
        //                                                        resitLimit[gyear] = tryParseDecimal;
        //                                                }
        //                                                break;
        //                                            case 4:
        //                                                if ( decimal.TryParse(element.GetAttribute("四年級補考標準"), out tryParseDecimal) )
        //                                                {
        //                                                    if ( !resitLimit.ContainsKey(gyear) )
        //                                                        resitLimit.Add(gyear, tryParseDecimal);
        //                                                    if ( resitLimit[gyear] > tryParseDecimal )
        //                                                        resitLimit[gyear] = tryParseDecimal;
        //                                                }
        //                                                break;
        //                                            default:
        //                                                break;
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            #endregion
        //                        }
        //                        #endregion
        //                    }
        //                    #endregion
        //                    for ( int i = 1 ; i <= 4 ; i++ )//1-4年級如果某個年級沒有補考標準就填入40
        //                    {
        //                        if ( !resitLimit.ContainsKey(i) )
        //                            resitLimit.Add(i, 40);
        //                    }
        //                    //填入補考標準
        //                    if ( !var.Fields.ContainsKey("補考標準") )
        //                        var.Fields.Add("補考標準", resitLimit);
        //                    else
        //                        var.Fields["補考標準"] = resitLimit;
        //                }
        //                break;
        //                #endregion
        //            case "及格標準":
        //                #region 填入及格標準
        //                foreach ( StudentRecord var in e.Items )
        //                {
        //                    //補考標準<年及,補考標準>
        //                    Dictionary<int, decimal> applyLimit = new Dictionary<int, decimal>();
        //                    #region 取得成績年級跟計算規則
        //                    {
        //                        #region 處理計算規則
        //                        XmlElement scoreCalcRule = ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID) == null ? null : ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID).ScoreCalcRuleElement;
        //                        if ( scoreCalcRule != null )
        //                        {
        //                            #region 有設定計算規則
        //                            DSXmlHelper helper = new DSXmlHelper(scoreCalcRule);
        //                            bool tryParsebool;
        //                            int tryParseint;
        //                            decimal tryParseDecimal;
        //                            foreach ( XmlElement element in helper.GetElements("及格標準/學生類別") )
        //                            {
        //                                string cat = element.GetAttribute("類別");
        //                                bool useful = false;
        //                                //掃描學生的類別作比對
        //                                foreach ( CategoryInfo catinfo in var.StudentCategorys )
        //                                {
        //                                    if ( catinfo.Name == cat || catinfo.FullName == cat )
        //                                        useful = true;
        //                                }
        //                                //學生是指定的類別或類別為"預設"
        //                                if ( cat == "預設" || useful )
        //                                {
        //                                    for ( int gyear = 1 ; gyear <= 4 ; gyear++ )
        //                                    {
        //                                        switch ( gyear )
        //                                        {
        //                                            case 1:
        //                                                if ( decimal.TryParse(element.GetAttribute("一年級及格標準"), out tryParseDecimal) )
        //                                                {
        //                                                    if ( !applyLimit.ContainsKey(gyear) )
        //                                                        applyLimit.Add(gyear, tryParseDecimal);
        //                                                    if ( applyLimit[gyear] > tryParseDecimal )
        //                                                        applyLimit[gyear] = tryParseDecimal;
        //                                                }
        //                                                break;
        //                                            case 2:
        //                                                if ( decimal.TryParse(element.GetAttribute("二年級及格標準"), out tryParseDecimal) )
        //                                                {
        //                                                    if ( !applyLimit.ContainsKey(gyear) )
        //                                                        applyLimit.Add(gyear, tryParseDecimal);
        //                                                    if ( applyLimit[gyear] > tryParseDecimal )
        //                                                        applyLimit[gyear] = tryParseDecimal;
        //                                                }
        //                                                break;
        //                                            case 3:
        //                                                if ( decimal.TryParse(element.GetAttribute("三年級及格標準"), out tryParseDecimal) )
        //                                                {
        //                                                    if ( !applyLimit.ContainsKey(gyear) )
        //                                                        applyLimit.Add(gyear, tryParseDecimal);
        //                                                    if ( applyLimit[gyear] > tryParseDecimal )
        //                                                        applyLimit[gyear] = tryParseDecimal;
        //                                                }
        //                                                break;
        //                                            case 4:
        //                                                if ( decimal.TryParse(element.GetAttribute("四年級及格標準"), out tryParseDecimal) )
        //                                                {
        //                                                    if ( !applyLimit.ContainsKey(gyear) )
        //                                                        applyLimit.Add(gyear, tryParseDecimal);
        //                                                    if ( applyLimit[gyear] > tryParseDecimal )
        //                                                        applyLimit[gyear] = tryParseDecimal;
        //                                                }
        //                                                break;
        //                                            default:
        //                                                break;
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            #endregion
        //                        }
        //                        #endregion
        //                    }
        //                    #endregion
        //                    for ( int i = 1 ; i <= 4 ; i++ )//1-4年級如果某個年級沒有補考標準就填入40
        //                    {
        //                        if ( !applyLimit.ContainsKey(i) )
        //                            applyLimit.Add(i, 40);
        //                    }
        //                    //填入及格標準
        //                    if ( !var.Fields.ContainsKey("及格標準") )
        //                        var.Fields.Add("及格標準", applyLimit);
        //                    else
        //                        var.Fields["及格標準"] = applyLimit;
        //                }
        //                break;
        //                #endregion
        //            case "本學期德行表現成績":
        //                #region 填入當學期德行表現成績
        //                new AngelDemonComputer().FillDemonScore(
        //                                    new AccessHelper(),
        //                                    SmartSchool.Customization.Data.SystemInformation.SchoolYear,
        //                                    SmartSchool.Customization.Data.SystemInformation.Semester,
        //                                    e.Items
        //                                    );
        //                foreach ( StudentRecord var in e.Items )
        //                {
        //                    if ( var.Fields.ContainsKey("DemonScore") )
        //                    {
        //                        var.Fields.Add("本學期德行表現成績", var.Fields["DemonScore"]);
        //                        var.Fields.Remove("DemonScore");
        //                    }
        //                    else
        //                    {
        //                        var.Fields.Add("本學期德行表現成績", new XmlDocument().CreateElement("DemonScore"));
        //                    }
        //                }
        //                break;
        //                #endregion
        //            case "畢業成績":
        //                #region 填入畢業成績
        //                Dictionary<string, StudentRecord> students = new Dictionary<string, StudentRecord>();
        //                foreach ( StudentRecord var in e.Items )
        //                {
        //                    if ( !students.ContainsKey(var.StudentID) )
        //                        students.Add(var.StudentID, var);
        //                }
        //                Dictionary<string, Dictionary<string, decimal>> sid_entry_score = new Dictionary<string, Dictionary<string, decimal>>();
        //                foreach ( XmlElement studentElement in SmartSchool.Feature.QueryStudent.GetDetailList(new string[] { "ID", "GradScore" }, new List<string>(students.Keys).ToArray()).GetContent().GetElements("Student") )
        //                {
        //                    string ID = studentElement.GetAttribute("ID");
        //                    foreach ( XmlElement scoreElement in studentElement.SelectNodes("GradScore/GradScore/EntryScore") )
        //                    {
        //                        if ( !sid_entry_score.ContainsKey(ID) )
        //                            sid_entry_score.Add(ID, new Dictionary<string, decimal>());
        //                        string entry = scoreElement.GetAttribute("Entry");
        //                        decimal dec;
        //                        if ( decimal.TryParse(scoreElement.GetAttribute("Score"), out dec) )
        //                        {
        //                            if ( !sid_entry_score[ID].ContainsKey(entry) )
        //                                sid_entry_score[ID].Add(entry, dec);
        //                        }
        //                    }
        //                }
        //                foreach ( StudentRecord student in students.Values )
        //                {
        //                    Dictionary<string, decimal> entryScore = sid_entry_score.ContainsKey(student.StudentID) ? sid_entry_score[student.StudentID] : new Dictionary<string, decimal>();
        //                    if ( student.Fields.ContainsKey("畢業成績") )
        //                        student.Fields["畢業成績"] = entryScore;
        //                    else
        //                        student.Fields.Add("畢業成績", entryScore);
        //                }
        //                break;
        //                #endregion
        //            case "核心科目表":
        //                #region 填入核心科目表
        //                foreach ( StudentRecord student in e.Items )
        //                {
        //                    XmlDocument doc = new XmlDocument();
        //                    XmlElement root = doc.CreateElement("核心科目表");
        //                    #region 預設學程核心科目表
        //                    if (
        //                        student.Fields.ContainsKey("SubDepartment")&&//科別有子項
        //                        SubjectTable.Items["學程科目表"].Contains("" + student.Fields["SubDepartment"]) //子項是一個學程名稱
        //                        )
        //                    {
        //                        //預設學程科目表
        //                        XmlElement contentElement = (XmlElement)SubjectTable.Items["學程科目表"]["" + student.Fields["SubDepartment"]].Content.SelectSingleNode("SubjectTableContent");
        //                        foreach ( XmlElement snode in contentElement.SelectNodes("Subject") )
        //                        {
        //                            string name = ( (XmlElement)snode ).GetAttribute("Name");
        //                            bool iscore = false;
        //                            bool.TryParse(snode.GetAttribute("IsCore"), out iscore);
        //                            if ( iscore )
        //                            {
        //                                if ( snode.SelectNodes("Level").Count == 0 )
        //                                {
        //                                    XmlElement subjectElement = doc.CreateElement("核心科目");
        //                                    subjectElement.SetAttribute("科目", name);
        //                                    subjectElement.SetAttribute("級別", "");
        //                                    subjectElement.SetAttribute("來源", "預設學程科目表");
        //                                    root.AppendChild(subjectElement);
        //                                }
        //                                else
        //                                {
        //                                    foreach ( XmlNode lnode in snode.SelectNodes("Level") )
        //                                    {
        //                                        XmlElement subjectElement = doc.CreateElement("核心科目");
        //                                        subjectElement.SetAttribute("科目", name);
        //                                        subjectElement.SetAttribute("級別", lnode.InnerText);
        //                                        subjectElement.SetAttribute("來源", "預設學程科目表");
        //                                        root.AppendChild(subjectElement);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    #endregion
        //                    #region 設定的核心科目表
        //                    XmlElement scoreCalcRule = ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(student.StudentID) == null ? null : ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(student.StudentID).ScoreCalcRuleElement;
        //                    if ( scoreCalcRule != null )
        //                    {
        //                        List<string> checkedList = new List<string>();
        //                        #region 整理要選用的科目表
        //                        foreach ( XmlNode st in scoreCalcRule.SelectNodes("核心科目表/科目表") )
        //                        {
        //                            checkedList.Add(st.InnerText);
        //                        }
        //                        #endregion
        //                        foreach ( SubjectTableItem subjectTable in SubjectTable.Items["核心科目表"] )
        //                        {
        //                            #region 是要選用的科目表就進行判斷
        //                            if ( checkedList.Contains(subjectTable.Name) && subjectTable.Content.SelectSingleNode("SubjectTableContent") != null )
        //                            {
        //                                XmlElement contentElement = (XmlElement)subjectTable.Content.SelectSingleNode("SubjectTableContent");
        //                                #region 整理在科目表中所有的科目級別
        //                                foreach ( XmlNode snode in contentElement.SelectNodes("Subject") )
        //                                {
        //                                    string name = ( (XmlElement)snode ).GetAttribute("Name");
        //                                    if ( snode.SelectNodes("Level").Count == 0 )
        //                                    {
        //                                        XmlElement subjectElement = doc.CreateElement("核心科目");
        //                                        subjectElement.SetAttribute("科目", name);
        //                                        subjectElement.SetAttribute("級別", "");
        //                                        subjectElement.SetAttribute("來源", subjectTable.Name);
        //                                        root.AppendChild(subjectElement);
        //                                    }
        //                                    else
        //                                    {
        //                                        foreach ( XmlNode lnode in snode.SelectNodes("Level") )
        //                                        {
        //                                            XmlElement subjectElement = doc.CreateElement("核心科目");
        //                                            subjectElement.SetAttribute("科目", name);
        //                                            subjectElement.SetAttribute("級別", lnode.InnerText);
        //                                            subjectElement.SetAttribute("來源", subjectTable.Name);
        //                                            root.AppendChild(subjectElement);
        //                                        }
        //                                    }
        //                                }
        //                                #endregion
        //                            }
        //                            #endregion
        //                        }
        //                    }
        //                    #endregion
        //                    if ( student.Fields.ContainsKey("核心科目表") )
        //                        student.Fields["核心科目表"] = root;
        //                    else
        //                        student.Fields.Add("核心科目表", root);
        //                }
        //                break;
        //                #endregion
        //        }
        //    }
        //}

        class Button1 : ButtonAdapter, IFeature
        {
            public Button1()
            {
                this.Path = "成績相關報表";
                this.Text = "建議重修名單";
                this.OnClick += new EventHandler(buttonItem12_Click);
                SmartSchool.Customization.PlugIn.Report.ClassReport.AddReport(this);
            }
            #region 科目不及格名單
            private const int _PackageSize = 200;

            private void buttonItem12_Click(object sender, EventArgs e)
            {
                if (SmartSchool.ClassRelated.Class.Instance.SelectionClasses.Count == 0)
                    return;

                List<SmartSchool.StudentRelated.BriefStudentData> allStudent = new List<SmartSchool.StudentRelated.BriefStudentData>();
                foreach (SmartSchool.ClassRelated.ClassInfo aClass in SmartSchool.ClassRelated.Class.Instance.SelectionClasses)
                {
                    foreach (SmartSchool.StudentRelated.BriefStudentData aStudent in aClass.Students)
                    {
                        allStudent.Add(aStudent);
                    }
                }

                List<List<SmartSchool.StudentRelated.BriefStudentData>> splitList = new List<List<SmartSchool.StudentRelated.BriefStudentData>>();
                Dictionary<List<SmartSchool.StudentRelated.BriefStudentData>, ManualResetEvent> handle = new Dictionary<List<SmartSchool.StudentRelated.BriefStudentData>, ManualResetEvent>();
                Dictionary<List<SmartSchool.StudentRelated.BriefStudentData>, DSResponse> response = new Dictionary<List<SmartSchool.StudentRelated.BriefStudentData>, DSResponse>();

                //把全部在校生以_PackageSize人分一包
                #region 把全部在校生以_PackageSize人分一包
                int count = 0;
                List<SmartSchool.StudentRelated.BriefStudentData> package = new List<SmartSchool.StudentRelated.BriefStudentData>();
                foreach (SmartSchool.StudentRelated.BriefStudentData student in allStudent)
                {
                    if (student.IsNormal)
                    {
                        if (count == 0)
                        {
                            count = _PackageSize;
                            package = new List<SmartSchool.StudentRelated.BriefStudentData>(_PackageSize);
                            splitList.Add(package);
                        }
                        package.Add(student);
                        count--;
                    }
                }
                #endregion
                //每一包一個ManualResetEvent一個DSResponse
                #region 每一包一個ManualResetEvent(預設為不可通過)一個DSResponse
                foreach (List<SmartSchool.StudentRelated.BriefStudentData> p in splitList)
                {
                    handle.Add(p, new ManualResetEvent(false));
                    response.Add(p, new DSResponse());
                }
                #endregion
                //在背景執行取得資料
                BackgroundWorker bkwDataLoader = new BackgroundWorker();
                bkwDataLoader.DoWork += new DoWorkEventHandler(bkwDataLoader_DoWork);
                bkwDataLoader.RunWorkerAsync(new object[] { handle, response });
                //在背景計算不及格名單
                BackgroundWorker bkwNotPassComputer = new BackgroundWorker();
                bkwNotPassComputer.WorkerReportsProgress = true;
                bkwNotPassComputer.DoWork += new DoWorkEventHandler(bkwNotPassComputer_DoWork);
                bkwNotPassComputer.ProgressChanged += new ProgressChangedEventHandler(bkwNotPassComputer_ProgressChanged);
                bkwNotPassComputer.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkwNotPassComputer_RunWorkerCompleted);
                bkwNotPassComputer.RunWorkerAsync(new object[] { handle, response });
            }

            private void bkwNotPassComputer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {
                //MotherForm.Instance.SetBarMessage("科目不及格名單產生完成。");

                if (e.Error == null)
                {
                    Workbook report = (Workbook)e.Result;
                    //儲存 Excel
                    #region 儲存 Excel
                    string path = System.IO.Path.Combine(Application.StartupPath, "Reports");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    path = System.IO.Path.Combine(path, "科目不及格名單.xlt");

                    if (File.Exists(path))
                    {
                        bool needCount = true;
                        try
                        {
                            File.Delete(path);
                            needCount = false;
                        }
                        catch { }
                        int i = 1;
                        while (needCount)
                        {
                            string newPath = System.IO.Path.GetDirectoryName(path) + "\\" + System.IO.Path.GetFileNameWithoutExtension(path) + (i++) + System.IO.Path.GetExtension(path);
                            if (!File.Exists(newPath))
                            {
                                path = newPath;
                                break;
                            }
                            else
                            {
                                try
                                {
                                    File.Delete(newPath);
                                    path = newPath;
                                    break;
                                }
                                catch { }
                            }
                        }
                    }
                    try
                    {
                        File.Create(path).Close();
                    }
                    catch
                    {
                        SaveFileDialog sd = new SaveFileDialog();
                        sd.Title = "另存新檔";
                        sd.FileName = System.IO.Path.GetFileNameWithoutExtension(path) + ".xls";
                        sd.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                        if (sd.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                File.Create(sd.FileName);
                                path = sd.FileName;
                            }
                            catch
                            {
                                MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    report.Save(path, FileFormatType.Excel2003);
                    #endregion
                    MotherForm.Instance.SetBarMessage("科目不及格名單產生完成。");
                    System.Diagnostics.Process.Start(path);
                }
                else
                    MotherForm.Instance.SetBarMessage("科目不及格名單產生發生未預期錯誤。");
            }

            private void bkwNotPassComputer_ProgressChanged(object sender, ProgressChangedEventArgs e)
            {
                MotherForm.Instance.SetBarMessage("" + e.UserState, e.ProgressPercentage);
            }

            private void bkwNotPassComputer_DoWork(object sender, DoWorkEventArgs e)
            {
                //科目不及格學生清單(keyFormat:;<subject 科目='' 科目級別='' 學分數='' />)
                Dictionary<string, Dictionary<SmartSchool.StudentRelated.BriefStudentData, XmlElement>> notPassList = new Dictionary<string, Dictionary<SmartSchool.StudentRelated.BriefStudentData, XmlElement>>();
                #region 整理資料
                Dictionary<List<SmartSchool.StudentRelated.BriefStudentData>, ManualResetEvent> handle = (Dictionary<List<SmartSchool.StudentRelated.BriefStudentData>, ManualResetEvent>)((object[])e.Argument)[0];
                Dictionary<List<SmartSchool.StudentRelated.BriefStudentData>, DSResponse> response = (Dictionary<List<SmartSchool.StudentRelated.BriefStudentData>, DSResponse>)((object[])e.Argument)[1];
                double totleProgress = 0.0;
                double currentProgress = 80.0 / handle.Count;
                ((BackgroundWorker)sender).ReportProgress(1, "科目不及格名單資料整理中...");
                foreach (List<SmartSchool.StudentRelated.BriefStudentData> splitList in handle.Keys)
                {
                    //等待這包的成績資料載下來
                    handle[splitList].WaitOne();
                    //載下來的資料
                    DSResponse resp = response[splitList];
                    double miniProgress = currentProgress / splitList.Count;
                    double miniProgressCount = 0.0;
                    //每一個學生
                    foreach (SmartSchool.StudentRelated.BriefStudentData student in splitList)
                    {
                        List<string> studentPassedList = new List<string>();
                        //每學期成績
                        foreach (XmlElement scoreElement in resp.GetContent().GetElements("SemesterSubjectScore[RefStudentId='" + student.ID + "']"))
                        {
                            DSXmlHelper helper = new DSXmlHelper(scoreElement);
                            //每一個科目成績
                            foreach (XmlElement subjectScoreElement in helper.GetElements("ScoreInfo/SemesterSubjectScoreInfo/Subject"))
                            {
                                string subject = "<subject 科目='" + subjectScoreElement.GetAttribute("科目") + "' 科目級別='" + subjectScoreElement.GetAttribute("科目級別") + "' 學分數='" + subjectScoreElement.GetAttribute("開課學分數") + "' />";
                                if (subjectScoreElement.GetAttribute("是否取得學分") == "是" || studentPassedList.Contains(subject))//如果該科目有取得學分獲該科目在其他學期已取得學分
                                {
                                    //加入已取得學分科目清單
                                    if (!studentPassedList.Contains(subject))
                                        studentPassedList.Add(subject);
                                    //從未取得學分科目清單中移除
                                    if (notPassList.ContainsKey(subject) && notPassList[subject].ContainsKey(student))
                                    {
                                        notPassList[subject].Remove(student);
                                    }
                                }
                                else
                                {
                                    //把學年度學期加上去
                                    subjectScoreElement.SetAttribute("學年度", scoreElement.SelectSingleNode("SchoolYear").InnerText);
                                    subjectScoreElement.SetAttribute("學期", scoreElement.SelectSingleNode("Semester").InnerText);
                                    subjectScoreElement.SetAttribute("年級", scoreElement.SelectSingleNode("GradeYear").InnerText);
                                    //加入至未取得學分科目清單
                                    if (!notPassList.ContainsKey(subject))
                                        notPassList.Add(subject, new Dictionary<SmartSchool.StudentRelated.BriefStudentData, XmlElement>());
                                    if (!notPassList[subject].ContainsKey(student))
                                        notPassList[subject].Add(student, subjectScoreElement);
                                }
                            }
                        }
                        miniProgressCount += miniProgress;
                        ((BackgroundWorker)sender).ReportProgress((int)(totleProgress + miniProgressCount), "科目不及格名單資料整理中...");
                    }
                    totleProgress += currentProgress;
                    ((BackgroundWorker)sender).ReportProgress((int)totleProgress, "科目不及格名單資料整理中...");
                }
                #endregion

                #region 產生報表
                currentProgress = 20.0 / notPassList.Count;
                Workbook template = new Workbook();
                #region 建立樣板
                template.Open(new MemoryStream(Properties.Resources.科目重修學生清單), FileFormatType.Excel2003);
                template.Worksheets[0].Cells[0, 0].PutValue(SmartSchool.CurrentUser.Instance.SchoolChineseName + "  科目重修學生清單");
                #endregion

                Workbook report = new Workbook();
                report.Open(new MemoryStream(Properties.Resources.科目重修學生清單), FileFormatType.Excel2003);

                Workbook wb = new Workbook();
                int index = 0;
                foreach (string subjectKey in notPassList.Keys)
                {
                    if (notPassList[subjectKey].Count > 0)
                    {
                        report.Worksheets[0].Cells.CopyRow(template.Worksheets[0].Cells, 0, index);
                        report.Worksheets[0].Cells.CopyRow(template.Worksheets[0].Cells, 1, index + 1);
                        report.Worksheets[0].Cells.CopyRow(template.Worksheets[0].Cells, 2, index + 2);
                        report.Worksheets[0].Cells.CopyRow(template.Worksheets[0].Cells, 3, index + 3);
                        report.Worksheets[0].Cells.SetRowHeight(index, template.Worksheets[0].Cells.GetRowHeight(0));
                        report.Worksheets[0].Cells.SetRowHeight(index + 1, template.Worksheets[0].Cells.GetRowHeight(1));
                        report.Worksheets[0].Cells.SetRowHeight(index + 2, template.Worksheets[0].Cells.GetRowHeight(2));
                        report.Worksheets[0].Cells.SetRowHeight(index + 3, template.Worksheets[0].Cells.GetRowHeight(3));

                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(subjectKey);
                        int level = 0;
                        int.TryParse(doc.DocumentElement.GetAttribute("科目級別"), out level);
                        report.Worksheets[0].Cells[index + 1, 2].PutValue(doc.DocumentElement.GetAttribute("科目") + (level == 0 ? "" : " " + GetNumber(level)));
                        report.Worksheets[0].Cells[index + 1, 6].PutValue(doc.DocumentElement.GetAttribute("學分數"));
                        index += 4;
                        foreach (SmartSchool.StudentRelated.BriefStudentData student in notPassList[subjectKey].Keys)
                        {
                            XmlElement subjectElement = notPassList[subjectKey][student];
                            report.Worksheets[0].Cells.CopyRow(template.Worksheets[0].Cells, 4, index);
                            report.Worksheets[0].Cells[index, 0].PutValue("");//編號
                            report.Worksheets[0].Cells[index, 1].PutValue(student.ClassName);//班級
                            report.Worksheets[0].Cells[index, 2].PutValue(student.SeatNo);//座號
                            report.Worksheets[0].Cells[index, 3].PutValue(student.Name);//姓名
                            report.Worksheets[0].Cells[index, 4].PutValue(student.StudentNumber);//學號
                            report.Worksheets[0].Cells[index, 5].PutValue(subjectElement.GetAttribute("修課必選修"));//必/選修
                            report.Worksheets[0].Cells[index, 6].PutValue(subjectElement.GetAttribute("修課校部訂"));//校/部訂
                            report.Worksheets[0].Cells[index, 7].PutValue(subjectElement.GetAttribute("學年度"));//學年度
                            report.Worksheets[0].Cells[index, 8].PutValue(subjectElement.GetAttribute("學期"));//學期

                            int gradeyear;
                            if (ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(student.ID) != null && int.TryParse(subjectElement.GetAttribute("年級"), out gradeyear))
                                report.Worksheets[0].Cells[index, 9].PutValue(ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(student.ID).GetStudentPassScore(student, gradeyear));//及格基分
                            else
                                report.Worksheets[0].Cells[index, 9].PutValue("--");//及格基分                        

                            #region 取得最高分數
                            decimal maxScore = 0;
                            decimal tryParseDecimal;
                            if (decimal.TryParse(subjectElement.GetAttribute("原始成績"), out tryParseDecimal))
                                maxScore = tryParseDecimal;
                            if (decimal.TryParse(subjectElement.GetAttribute("學年調整成績"), out tryParseDecimal) && maxScore < tryParseDecimal)
                                maxScore = tryParseDecimal;
                            if (decimal.TryParse(subjectElement.GetAttribute("擇優採計成績"), out tryParseDecimal) && maxScore < tryParseDecimal)
                                maxScore = tryParseDecimal;
                            if (decimal.TryParse(subjectElement.GetAttribute("補考成績"), out tryParseDecimal) && maxScore < tryParseDecimal)
                                maxScore = tryParseDecimal;
                            if (decimal.TryParse(subjectElement.GetAttribute("重修成績"), out tryParseDecimal) && maxScore < tryParseDecimal)
                                maxScore = tryParseDecimal;
                            #endregion

                            report.Worksheets[0].Cells[index, 10].PutValue("" + maxScore);//學期成績

                            index++;
                        }
                        //留一行空白
                        index++;
                        report.Worksheets[0].HPageBreaks.Add(index, 11);
                        totleProgress += currentProgress;
                        ((BackgroundWorker)sender).ReportProgress((int)totleProgress, "科目不及格名單報表產生中...");
                    }
                }
                e.Result = report;
                #endregion
            }

            private string GetNumber(int p)
            {
                string levelNumber;
                switch (p)
                {
                    #region 對應levelNumber
                    case 1:
                        levelNumber = "Ⅰ";
                        break;
                    case 2:
                        levelNumber = "Ⅱ";
                        break;
                    case 3:
                        levelNumber = "Ⅲ";
                        break;
                    case 4:
                        levelNumber = "Ⅳ";
                        break;
                    case 5:
                        levelNumber = "Ⅴ";
                        break;
                    case 6:
                        levelNumber = "Ⅵ";
                        break;
                    case 7:
                        levelNumber = "Ⅶ";
                        break;
                    case 8:
                        levelNumber = "Ⅷ";
                        break;
                    case 9:
                        levelNumber = "Ⅸ";
                        break;
                    case 10:
                        levelNumber = "Ⅹ";
                        break;
                    default:
                        levelNumber = "" + (p);
                        break;
                    #endregion
                }
                return levelNumber;
            }

            private void bkwDataLoader_DoWork(object sender, DoWorkEventArgs e)
            {
                Dictionary<List<SmartSchool.StudentRelated.BriefStudentData>, ManualResetEvent> handle = (Dictionary<List<SmartSchool.StudentRelated.BriefStudentData>, ManualResetEvent>)((object[])e.Argument)[0];
                Dictionary<List<SmartSchool.StudentRelated.BriefStudentData>, DSResponse> response = (Dictionary<List<SmartSchool.StudentRelated.BriefStudentData>, DSResponse>)((object[])e.Argument)[1];
                foreach (List<SmartSchool.StudentRelated.BriefStudentData> splitList in handle.Keys)
                {
                    List<string> idList = new List<string>();
                    foreach (SmartSchool.StudentRelated.BriefStudentData var in splitList)
                    {
                        idList.Add(var.ID);
                    }
                    DSResponse resp = SmartSchool.Feature.Score.QueryScore.GetSemesterSubjectScore(idList.ToArray());
                    response[splitList] = resp;
                    handle[splitList].Set();
                }
            }
            #endregion

            #region IFeature 成員

            public string FeatureCode
            {
                get { return "Report0130"; }
            }

            #endregion
        }
    }
}
