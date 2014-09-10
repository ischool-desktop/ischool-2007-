﻿using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data;
using System.Xml;
using IntelliSchool.DSA30.Util;
using SmartSchool.Customization.Data.StudentExtension;

namespace SmartSchool.Evaluation
{
    public class WearyDogComputer
    {
        internal enum RoundMode { 四捨五入, 無條件進位, 無條件捨去 }
        internal static decimal GetRoundScore(decimal score, int decimals, RoundMode mode)
        {
            decimal seed = Convert.ToDecimal(Math.Pow(0.1, Convert.ToDouble(decimals)));
            switch (mode)
            {
                default:
                case RoundMode.四捨五入:
                    score = decimal.Round(score, decimals, MidpointRounding.AwayFromZero);
                    break;
                case RoundMode.無條件捨去:
                    score /= seed;
                    score = decimal.Floor(score);
                    score *= seed;
                    break;
                case RoundMode.無條件進位:
                    decimal d2 = GetRoundScore(score, decimals, RoundMode.無條件捨去);
                    if ( d2 != score )
                        score = d2 + seed;
                    else
                        score = d2;
                    break;
            }
            string ss = "0.";
            for ( int i = 0 ; i < decimals ; i++ )
            {
                ss += "0";
            }
            return Convert.ToDecimal(Math.Round(score, decimals).ToString(ss));
        }
        
        public Dictionary<StudentRecord,List<string>> FillSemesterSubjectCalcScore(int schoolyear,int semester,AccessHelper accesshelper, List<StudentRecord> students)
        {
            Dictionary<StudentRecord, List<string>> _ErrorList = new Dictionary<StudentRecord, List<string>>();
            //抓學生學期歷程
            accesshelper.StudentHelper.FillField("SemesterHistory", students);
            //抓學生學期修課紀錄
            accesshelper.StudentHelper.FillAttendCourse(schoolyear, semester, students);
            //抓學生歷年學期科目成績
            accesshelper.StudentHelper.FillSemesterSubjectScore(false, students);
            foreach (StudentRecord var in students)
            {
                //成績年級
                int? gradeYear = null;
                //精準位數
                int decimals = 2;
                //進位模式
                RoundMode mode = RoundMode.四捨五入;
                //使用擇優採計成績
                bool choseBetter = true;
                //重修登錄至原學期
                bool writeToFirstSemester = false;
                //及格標準<年及,及格標準>
                Dictionary<int, decimal> applyLimit = new Dictionary<int, decimal>();
                //applyLimit.Add(1, 60);
                //applyLimit.Add(2, 60);
                //applyLimit.Add(3, 60);
                //applyLimit.Add(4, 60);
                //成績年級及計算規則皆存在，允許計算成績
                bool canCalc = true;
                #region 取得成績年級跟計算規則
                {
                    #region 處理成績年級
                    XmlElement semesterHistory = (XmlElement)var.Fields["SemesterHistory"];
                    if (semesterHistory == null)
                    {
                        LogError(var, _ErrorList, "沒有學期歷程紀錄，無法判斷成績年級。");
                        canCalc &= false;
                    }
                    else
                    {
                        foreach (XmlElement history in new DSXmlHelper(semesterHistory).GetElements("History"))
                        {
                            int year, sems, gradeyear;
                            if (
                                int.TryParse(history.GetAttribute("SchoolYear"), out year) &&
                                int.TryParse(history.GetAttribute("Semester"), out sems) &&
                                int.TryParse(history.GetAttribute("GradeYear"), out gradeyear) &&
                                year == schoolyear && sems == semester
                                )
                                gradeYear = gradeyear;
                        }
                        if (gradeYear == null)
                        {
                            LogError(var, _ErrorList, "學期歷程中沒有" + schoolyear + "學年度第" + semester + "學期的紀錄，無法判斷成績年級。");
                            canCalc &= false;
                        }
                    }
                    #endregion                
                    #region 處理計算規則
                    XmlElement scoreCalcRule = ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID) == null ? null : ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID).ScoreCalcRuleElement;
                    if (scoreCalcRule == null)
                    {
                        LogError(var, _ErrorList, "沒有設定成績計算規則。");
                        canCalc &= false;
                    }
                    else
                    {
                        DSXmlHelper helper = new DSXmlHelper(scoreCalcRule);
                        bool tryParsebool;
                        int tryParseint;
                        decimal tryParseDecimal;

                        if (scoreCalcRule.SelectSingleNode("各項成績計算位數/科目成績計算位數") != null)
                        {
                            if (int.TryParse(helper.GetText("各項成績計算位數/科目成績計算位數/@位數"), out tryParseint))
                                decimals = tryParseint;
                            if (bool.TryParse(helper.GetText("各項成績計算位數/科目成績計算位數/@四捨五入"),out tryParsebool) && tryParsebool)
                                mode = RoundMode.四捨五入;
                            if (bool.TryParse(helper.GetText("各項成績計算位數/科目成績計算位數/@無條件捨去"), out tryParsebool) && tryParsebool)
                                mode = RoundMode.無條件捨去;
                            if (bool.TryParse(helper.GetText("各項成績計算位數/科目成績計算位數/@無條件進位"), out tryParsebool) && tryParsebool)
                                mode = RoundMode.無條件進位;
                        }
                        if (scoreCalcRule.SelectSingleNode("延修及重讀成績處理規則/重讀成績") != null)
                        {
                            if (bool.TryParse(helper.GetText("延修及重讀成績處理規則/重讀成績/@擇優採計成績"),out tryParsebool))
                                choseBetter = tryParsebool;
                        }
                        if (bool.TryParse(helper.GetText("重修成績/@登錄至原學期"), out tryParsebool))
                            writeToFirstSemester = tryParsebool;
                        foreach (XmlElement element in helper.GetElements("及格標準/學生類別"))
                        {
                            string cat = element.GetAttribute("類別");
                            bool useful=false;
                            //掃描學生的類別作比對
                            foreach (CategoryInfo catinfo in var.StudentCategorys)
                            {
                                if (catinfo.Name == cat || catinfo.FullName == cat)
                                    useful = true;
                            }
                            //學生是指定的類別或類別為"預設"
                            if (cat == "預設" || useful)
                            {
                                for (int gyear = 1  ; gyear <=4; gyear++)
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
                    }
                    #endregion
                }
                #endregion

                XmlDocument doc = new XmlDocument();
                if (var.Fields.ContainsKey("SemesterSubjectCalcScore"))
                    var.Fields.Remove("SemesterSubjectCalcScore");
                XmlElement semesterSubjectCalcScoreElement = doc.CreateElement("SemesterSubjectCalcScore");
                if (canCalc)
                {
                    //已存在的學期科目成績<學年度<學期,<科目+級別,成績>>>
                    Dictionary<int, Dictionary<int, Dictionary<string, SemesterSubjectScoreInfo>>> semesterSubjectScoreList = new Dictionary<int, Dictionary<int, Dictionary<string, SemesterSubjectScoreInfo>>>();
                    //當學年度已紀錄之成績
                    Dictionary<SemesterSubjectScoreInfo, string> currentSubjectScoreList = new Dictionary<SemesterSubjectScoreInfo, string>();
                    //重讀成績
                    Dictionary<SemesterSubjectScoreInfo, string> repeatSubjectScoreList = new Dictionary<SemesterSubjectScoreInfo, string>();
                    //重修成績
                    Dictionary<SemesterSubjectScoreInfo, string> restudySubjectScoreList =new  Dictionary<SemesterSubjectScoreInfo, string>();
                    #region 先掃一遍把學生成績分類
                    foreach (SemesterSubjectScoreInfo scoreinfo in var.SemesterSubjectScoreList)
                    {
                        string key = scoreinfo.Subject.Trim() + "_" + scoreinfo.Level.Trim();
                        if (scoreinfo.SchoolYear == schoolyear)
                        {
                            if (scoreinfo.Semester == semester)
                                currentSubjectScoreList.Add(scoreinfo, key);
                            else if (scoreinfo.Semester < semester)
                            {
                                if (scoreinfo.GradeYear == (int)gradeYear)
                                    repeatSubjectScoreList.Add(scoreinfo, key);
                                else
                                    restudySubjectScoreList.Add(scoreinfo, key);
                            }
                        }
                        else if (scoreinfo.SchoolYear < schoolyear)
                        {
                            if (scoreinfo.GradeYear == (int)gradeYear)
                                repeatSubjectScoreList.Add(scoreinfo, key);
                            else
                                restudySubjectScoreList.Add(scoreinfo, key);
                        }
                        if (!semesterSubjectScoreList.ContainsKey(scoreinfo.SchoolYear))
                            semesterSubjectScoreList.Add(scoreinfo.SchoolYear, new Dictionary<int, Dictionary<string, SemesterSubjectScoreInfo>>());
                        if (!semesterSubjectScoreList[scoreinfo.SchoolYear].ContainsKey(scoreinfo.Semester))
                            semesterSubjectScoreList[scoreinfo.SchoolYear].Add(scoreinfo.Semester, new Dictionary<string, SemesterSubjectScoreInfo>());
                        if (!semesterSubjectScoreList[scoreinfo.SchoolYear][scoreinfo.Semester].ContainsKey(key))
                            semesterSubjectScoreList[scoreinfo.SchoolYear][scoreinfo.Semester].Add(key, scoreinfo);
                        else
                            semesterSubjectScoreList[scoreinfo.SchoolYear][scoreinfo.Semester][key]= scoreinfo;
                    }
                    #endregion
                    #region 移除重讀跟重修成績重複年級的學期成績
                    CleanUpRepeat(repeatSubjectScoreList);
                    CleanUpRepeat(restudySubjectScoreList); 
                    #endregion
                    //新增的學期成績資料
                    Dictionary<int, Dictionary<int, Dictionary<string, XmlElement>>> insertSemesterSubjectScoreList = new Dictionary<int, Dictionary<int, Dictionary<string, XmlElement>>>();
                    //修改的學期成績資料
                    Dictionary<int, Dictionary<int, Dictionary<string, XmlElement>>> updateSemesterSubjectScoreList = new Dictionary<int, Dictionary<int, Dictionary<string, XmlElement>>>();
                    #region 掃描修課紀錄填入新增或修改的清單中
                    foreach (StudentAttendCourseRecord sacRecord in var.AttendCourseList)
                    {
                        if (!sacRecord.HasFinalScore && !sacRecord.NotIncludedInCalc)
                        {
                            LogError(var, _ErrorList, "" + sacRecord.CourseName + "沒有修課總成績，無法計算。");
                            continue;
                        }
                        string key = sacRecord.Subject.Trim() + "_" + sacRecord.SubjectLevel.Trim();
                        //發現為重修科目
                        if ( writeToFirstSemester&&restudySubjectScoreList.ContainsValue(key))
                        {
                                #region 寫入重修成績回原學期
                                int sy = 0, se = 0;
                                SemesterSubjectScoreInfo updateScoreInfo = null;
                                #region 找到最近一次修課紀錄
                                foreach (SemesterSubjectScoreInfo si in restudySubjectScoreList.Keys)
                                {
                                    if (restudySubjectScoreList[si] == key)
                                    {
                                        if (si.SchoolYear > sy || (si.SchoolYear == sy && si.Semester > se))
                                        {
                                            sy = si.SchoolYear;
                                            se = si.Semester;
                                            updateScoreInfo = si;
                                        }
                                    }
                                }
                                #endregion
                                if (!updateSemesterSubjectScoreList.ContainsKey(sy) || !updateSemesterSubjectScoreList[sy].ContainsKey(se) || !updateSemesterSubjectScoreList[sy][se].ContainsKey(key))
                                {
                                    //寫入重修紀錄
                                    XmlElement updateScoreElement = updateScoreInfo.Detail;
                                    updateScoreElement.SetAttribute("重修成績", "" + GetRoundScore(sacRecord.FinalScore, decimals, mode));
                                    //做取得學分判斷
                                    #region 做取得學分判斷
                                    //最高分
                                    decimal maxScore = sacRecord.FinalScore;
                                    #region 抓最高分
                                    string[] scoreNames = new string[] { "原始成績", "學年調整成績", "擇優採計成績", "補考成績", "重修成績" };
                                    foreach (string scorename in scoreNames)
                                    {
                                        decimal s;
                                        if (decimal.TryParse(updateScoreElement.GetAttribute(scorename), out s))
                                        {
                                            if (s > maxScore)
                                            {
                                                maxScore = s;
                                            }
                                        }
                                    }
                                    #endregion
                                    decimal passscore;
                                    if (!applyLimit.ContainsKey(updateScoreInfo.GradeYear))
                                        passscore = 60;
                                    else
                                        passscore = applyLimit[updateScoreInfo.GradeYear];
                                    updateScoreElement.SetAttribute("是否取得學分", (updateScoreElement.GetAttribute("不需評分") == "是" || maxScore >= passscore) ? "是" : "否");
                                    #endregion
                                    if (!updateSemesterSubjectScoreList.ContainsKey(sy)) updateSemesterSubjectScoreList.Add(sy, new Dictionary<int, Dictionary<string, XmlElement>>());
                                    if (!updateSemesterSubjectScoreList[sy].ContainsKey(se)) updateSemesterSubjectScoreList[sy].Add(se, new Dictionary<string, XmlElement>());
                                    updateSemesterSubjectScoreList[sy][se].Add(key, updateScoreElement);
                                }
                                #endregion
                        }
                        else
                        { 
                            //填入本學期科目成績
                            if (currentSubjectScoreList.ContainsValue(key))
                            {
                                #region 修改此學期已存在之成績
                                SemesterSubjectScoreInfo updateScoreInfo=null;
                                foreach (SemesterSubjectScoreInfo s in currentSubjectScoreList.Keys)
                                {
                                    if (currentSubjectScoreList[s] == key)
                                    {
                                        updateScoreInfo = s;
                                        break;
                                    }
                                }
                                int sy = schoolyear, se = semester;
                                if (!updateSemesterSubjectScoreList.ContainsKey(sy) || !updateSemesterSubjectScoreList[sy].ContainsKey(se) || !updateSemesterSubjectScoreList[sy][se].ContainsKey(key))
                                {
                                    //修改成績
                                    XmlElement updateScoreElement = updateScoreInfo.Detail;
                                    #region 重新填入課程資料
                                    updateScoreElement.SetAttribute("不計學分", sacRecord.NotIncludedInCredit ? "是" : "否");
                                    updateScoreElement.SetAttribute("不需評分", sacRecord.NotIncludedInCalc ? "是" : "否");
                                    updateScoreElement.SetAttribute("修課必選修", sacRecord.Required ? "必修" : "選修");
                                    updateScoreElement.SetAttribute("修課校部訂", (sacRecord.RequiredBy == "部訂" ? sacRecord.RequiredBy : "校訂"));
                                    updateScoreElement.SetAttribute("科目", sacRecord.Subject);
                                    updateScoreElement.SetAttribute("科目級別", sacRecord.SubjectLevel);
                                    updateScoreElement.SetAttribute("開課分項類別", sacRecord.Entry);
                                    updateScoreElement.SetAttribute("開課學分數", "" + sacRecord.Credit); 
                                    #endregion
                                    updateScoreElement.SetAttribute("原始成績", (sacRecord.NotIncludedInCalc ? "" : "" + GetRoundScore(sacRecord.FinalScore, decimals, mode)));
                                    //做取得學分判斷
                                    #region 做取得學分判斷及填入擇優採計成績
                                    //最高分
                                    decimal maxScore = sacRecord.FinalScore;
                                    #region 抓最高分
                                    string[] scoreNames = new string[] { "原始成績", "學年調整成績", "擇優採計成績", "補考成績", "重修成績" };
                                    foreach (string scorename in scoreNames)
                                    {
                                        decimal s;
                                        if (decimal.TryParse(updateScoreElement.GetAttribute(scorename), out s))
                                        {
                                            if (s > maxScore)
                                            {
                                                maxScore = s;
                                            }
                                        }
                                    }
                                    #endregion
                                    //如果有擇優採計成績且重讀學期有修過課
                                    if (choseBetter && repeatSubjectScoreList.ContainsValue(key))
                                    {
                                        #region 填入擇優採計成績
                                        foreach (SemesterSubjectScoreInfo s in repeatSubjectScoreList.Keys)
                                        {
                                            //之前的成績比現在的成績好
                                            if (repeatSubjectScoreList[s] == key && s.Score > maxScore)
                                            {
                                                updateScoreElement.SetAttribute("原始成績", "" + GetRoundScore(s.Score, decimals, mode));
                                                updateScoreElement.SetAttribute("註記", "修課成績：" + sacRecord.FinalScore);
                                                maxScore = s.Score;
                                            }
                                        }
                                        #endregion
                                    }
                                    decimal passscore;
                                    if (!applyLimit.ContainsKey(updateScoreInfo.GradeYear))
                                        passscore = 60;
                                    else
                                        passscore = applyLimit[updateScoreInfo.GradeYear];
                                    updateScoreElement.SetAttribute("是否取得學分", (sacRecord.NotIncludedInCalc || maxScore >= passscore) ? "是" : "否");
                                    #endregion
                                    if (!updateSemesterSubjectScoreList.ContainsKey(sy)) updateSemesterSubjectScoreList.Add(sy, new Dictionary<int, Dictionary<string, XmlElement>>());
                                    if (!updateSemesterSubjectScoreList[sy].ContainsKey(se)) updateSemesterSubjectScoreList[sy].Add(se, new Dictionary<string, XmlElement>());
                                    updateSemesterSubjectScoreList[sy][se].Add(key, updateScoreElement);
                                } 
                                #endregion
                            }
                            else
                            {
                                #region 新增一筆成績
                                int sy = schoolyear, se = semester;
                                if (!insertSemesterSubjectScoreList.ContainsKey(sy) || !insertSemesterSubjectScoreList[sy].ContainsKey(se) || !insertSemesterSubjectScoreList[sy][se].ContainsKey(key))
                                {
                                    #region 加入新的資料
                                    XmlElement newScoreInfo = doc.CreateElement("Subject");
                                    newScoreInfo.SetAttribute("不計學分", sacRecord.NotIncludedInCredit ? "是" : "否");
                                    newScoreInfo.SetAttribute("不需評分", sacRecord.NotIncludedInCalc ? "是" : "否");
                                    newScoreInfo.SetAttribute("修課必選修", sacRecord.Required ? "必修" : "選修");
                                    newScoreInfo.SetAttribute("修課校部訂", (sacRecord.RequiredBy == "部訂" ? sacRecord.RequiredBy : "校訂"));
                                    newScoreInfo.SetAttribute("科目", sacRecord.Subject);
                                    newScoreInfo.SetAttribute("科目級別", sacRecord.SubjectLevel);
                                    newScoreInfo.SetAttribute("開課分項類別", sacRecord.Entry);
                                    newScoreInfo.SetAttribute("開課學分數", "" + sacRecord.Credit);
                                    newScoreInfo.SetAttribute("原始成績", (sacRecord.NotIncludedInCalc?"":"" + GetRoundScore(sacRecord.FinalScore, decimals, mode)));
                                    newScoreInfo.SetAttribute("重修成績", "");
                                    newScoreInfo.SetAttribute("學年調整成績", "");
                                    newScoreInfo.SetAttribute("擇優採計成績", "" );
                                    newScoreInfo.SetAttribute("補考成績", "");
                                    //做取得學分判斷
                                    #region 做取得學分判斷及填入擇優採計成績
                                    //最高分
                                    decimal maxScore = sacRecord.FinalScore;
                                    #region 抓最高分
                                    string[] scoreNames = new string[] { "原始成績", "學年調整成績", "擇優採計成績", "補考成績", "重修成績" };
                                    foreach (string scorename in scoreNames)
                                    {
                                        decimal s;
                                        if (decimal.TryParse(newScoreInfo.GetAttribute(scorename), out s))
                                        {
                                            if (s > maxScore)
                                            {
                                                maxScore = s;
                                            }
                                        }
                                    }
                                    #endregion

                                    //如果有擇優採計成績且重讀學期有修過課
                                    if (choseBetter && repeatSubjectScoreList.ContainsValue(key))
                                    {
                                        #region 填入擇優採計成績
                                        foreach (SemesterSubjectScoreInfo s in repeatSubjectScoreList.Keys)
                                        {
                                            //之前的成績比現在的成績好
                                            if (repeatSubjectScoreList[s] == key && s.Score > maxScore)
                                            {
                                                //newScoreInfo.SetAttribute("擇優採計成績", "" + GetRoundScore(s.Score, decimals, mode));
                                                newScoreInfo.SetAttribute("原始成績", "" + GetRoundScore(s.Score, decimals, mode));
                                                newScoreInfo.SetAttribute("註記", "修課成績：" + sacRecord.FinalScore);
                                                maxScore = s.Score;
                                            }
                                        }
                                        #endregion
                                    }
                                    decimal passscore;
                                    if (!applyLimit.ContainsKey((int)gradeYear))
                                        passscore = 60;
                                    else
                                        passscore = applyLimit[(int)gradeYear];
                                    #endregion
                                    newScoreInfo.SetAttribute("是否取得學分", (sacRecord.NotIncludedInCalc || maxScore >= passscore) ? "是" : "否");
                                    #endregion
                                    if (!insertSemesterSubjectScoreList.ContainsKey(sy)) insertSemesterSubjectScoreList.Add(sy, new Dictionary<int, Dictionary<string, XmlElement>>());
                                    if (!insertSemesterSubjectScoreList[sy].ContainsKey(se)) insertSemesterSubjectScoreList[sy].Add(se, new Dictionary<string, XmlElement>());
                                    insertSemesterSubjectScoreList[sy][se].Add(key, newScoreInfo);
                                }
                                #endregion
                            }
                        }
                    } 
                    #endregion
                    #region 從新增跟修改清單中產生變動資料
                    //抓取暗藏的學生學期成績編號資料
                    Dictionary<int, Dictionary<int, string>> semeScoreID = (Dictionary<int, Dictionary<int, string>>)var.Fields["SemesterSubjectScoreID"];
                    foreach (int sy in updateSemesterSubjectScoreList.Keys)
                    {
                        foreach (int se in updateSemesterSubjectScoreList[sy].Keys)
                        {
                            List<string> appendedKeys = new List<string>();
                            XmlElement parentNode;
                            parentNode = doc.CreateElement("UpdateSemesterScore");
                            parentNode.SetAttribute("ID", semeScoreID[sy][se]);
                            if ( sy == schoolyear && se == semester )
                                parentNode.SetAttribute("GradeYear", "" + gradeYear);
                            //加入修改過的成績資料
                            foreach (string key in updateSemesterSubjectScoreList[sy][se].Keys)
                            {
                                appendedKeys.Add(key);
                                parentNode.AppendChild(doc.ImportNode(updateSemesterSubjectScoreList[sy][se][key], true));
                            }
                            if (insertSemesterSubjectScoreList.ContainsKey(sy) && insertSemesterSubjectScoreList[sy].ContainsKey(se))
                            {
                                //加入新增的成績資料
                                foreach (string key in insertSemesterSubjectScoreList[sy][se].Keys)
                                {
                                    appendedKeys.Add(key);
                                    parentNode.AppendChild(doc.ImportNode(insertSemesterSubjectScoreList[sy][se][key], true));
                                }
                                insertSemesterSubjectScoreList[sy].Remove(se);
                                if (insertSemesterSubjectScoreList[sy].Count == 0)
                                    insertSemesterSubjectScoreList.Remove(sy);
                            }
                            if (semesterSubjectScoreList.ContainsKey(sy) && semesterSubjectScoreList[sy].ContainsKey(se))
                            {
                                //加入此學期沒有變動的成績資料
                                foreach (string key in semesterSubjectScoreList[sy][se].Keys)
                                {
                                    if (!appendedKeys.Contains(key))
                                        parentNode.AppendChild(doc.ImportNode(semesterSubjectScoreList[sy][se][key].Detail, true));
                                }
                            }
                            semesterSubjectCalcScoreElement.AppendChild(parentNode);
                        }
                    }
                    //如果還有新增成績，必定為此學期之成績，且此學期尚無任何成績紀錄
                    foreach (int sy in insertSemesterSubjectScoreList.Keys)
                    {
                        foreach (int se in insertSemesterSubjectScoreList[sy].Keys)
                        {
                            if (insertSemesterSubjectScoreList[sy][se].Count > 0)
                            {
                                XmlElement parentNode;
                                if (semeScoreID.ContainsKey(sy) && semeScoreID[sy].ContainsKey(se))
                                {
                                    parentNode = doc.CreateElement("UpdateSemesterScore");
                                    parentNode.SetAttribute("ID", semeScoreID[sy][se]);
                                    if ( sy == schoolyear && se == semester )
                                        parentNode.SetAttribute("GradeYear", "" + gradeYear);
                                }
                                else
                                {
                                    parentNode = doc.CreateElement("InsertSemesterScore");
                                    parentNode.SetAttribute("GradeYear", "" + gradeYear);
                                    parentNode.SetAttribute("SchoolYear", "" + sy);
                                    parentNode.SetAttribute("Semester", "" + se);
                                }
                                foreach (XmlElement   ele in insertSemesterSubjectScoreList[sy][se].Values)
                                {
                                    parentNode.AppendChild(doc.ImportNode(ele, true));
                                }
                                //如果此學期有新增成績卻沒有更新成績，需將原無變動之成績填回
                                if ( ( !updateSemesterSubjectScoreList.ContainsKey(sy) || !updateSemesterSubjectScoreList[sy].ContainsKey(se) ) && semesterSubjectScoreList.ContainsKey(sy) && semesterSubjectScoreList[sy].ContainsKey(se) )
                                {
                                    //加入此學期沒有變動的成績資料
                                    foreach ( string key in semesterSubjectScoreList[sy][se].Keys )
                                    {
                                        parentNode.AppendChild(doc.ImportNode(semesterSubjectScoreList[sy][se][key].Detail, true));
                                    }
                                }
                                semesterSubjectCalcScoreElement.AppendChild(parentNode);
                            }
                        }
                    }
                    #endregion

                    #region OldWay
                    //List<SemesterSubjectScoreInfo> aforeSemesterScoreList = new List<SemesterSubjectScoreInfo>();
                    //List<SemesterSubjectScoreInfo> currentSemesterScoreList = new List<SemesterSubjectScoreInfo>();
                    //#region 先掃一遍把學生成績分類
                    //foreach (SemesterSubjectScoreInfo scoreinfo in var.SemesterSubjectScoreList)
                    //{
                    //    if (scoreinfo.SchoolYear == schoolyear)
                    //    {
                    //        if (scoreinfo.Semester == semester)
                    //            currentSemesterScoreList.Add(scoreinfo);
                    //        else if (scoreinfo.Semester < semester)
                    //            aforeSemesterScoreList.Add(scoreinfo);
                    //    }
                    //    else if (scoreinfo.SchoolYear < schoolyear)
                    //        aforeSemesterScoreList.Add(scoreinfo);
                    //}
                    //#endregion
                    //#region 針對之前學期的成績做重讀判斷
                    //Dictionary<int, Dictionary<int, int>> ApplySemesterSchoolYear = new Dictionary<int, Dictionary<int, int>>();
                    ////先掃一遍抓出每個年級最高的學年度
                    //foreach (SemesterSubjectScoreInfo scoreInfo in aforeSemesterScoreList)
                    //{
                    //    if (!ApplySemesterSchoolYear.ContainsKey(scoreInfo.GradeYear))
                    //        ApplySemesterSchoolYear.Add(scoreInfo.GradeYear, new Dictionary<int, int>());
                    //    if (!ApplySemesterSchoolYear[scoreInfo.GradeYear].ContainsKey(scoreInfo.Semester))
                    //        ApplySemesterSchoolYear[scoreInfo.GradeYear].Add(scoreInfo.Semester, scoreInfo.SchoolYear);
                    //    if (scoreInfo.SchoolYear > ApplySemesterSchoolYear[scoreInfo.GradeYear][scoreInfo.Semester])
                    //        ApplySemesterSchoolYear[scoreInfo.GradeYear][scoreInfo.Semester] = scoreInfo.SchoolYear;
                    //}
                    ////如果成績資料的年級學年度不在清單中就移掉
                    //List<SemesterSubjectScoreInfo> removeList = new List<SemesterSubjectScoreInfo>();
                    //foreach (SemesterSubjectScoreInfo scoreInfo in aforeSemesterScoreList)
                    //{
                    //    if (ApplySemesterSchoolYear[scoreInfo.GradeYear][scoreInfo.Semester] != scoreInfo.SchoolYear)
                    //        removeList.Add(scoreInfo);
                    //}
                    //foreach (SemesterSubjectScoreInfo scoreInfo in removeList)
                    //{
                    //    aforeSemesterScoreList.Remove(scoreInfo);
                    //}
                    //#endregion

                    //Dictionary<string, List<SemesterSubjectScoreInfo>> currentSLScoreDictionary = new Dictionary<string, List<SemesterSubjectScoreInfo>>();
                    //Dictionary<string, List<SemesterSubjectScoreInfo>> aforeSLScoreDictionary = new Dictionary<string, List<SemesterSubjectScoreInfo>>();
                    //#region 將有用的成績依科目級別填入
                    //foreach (SemesterSubjectScoreInfo scoreinfo in currentSemesterScoreList)
                    //{
                    //    string key = scoreinfo.Subject.Trim() + "_" + scoreinfo.Level.Trim();
                    //    if (!currentSLScoreDictionary.ContainsKey(key))
                    //        currentSLScoreDictionary.Add(key, new List<SemesterSubjectScoreInfo>());
                    //    currentSLScoreDictionary[key].Add(scoreinfo);
                    //}
                    //foreach (SemesterSubjectScoreInfo scoreinfo in aforeSemesterScoreList)
                    //{
                    //    string key = scoreinfo.Subject + "_" + scoreinfo.Level;
                    //    if (!aforeSLScoreDictionary.ContainsKey(key))
                    //        aforeSLScoreDictionary.Add(key, new List<SemesterSubjectScoreInfo>());
                    //    aforeSLScoreDictionary[key].Add(scoreinfo);
                    //}
                    //#endregion

                    //List<SemesterSubjectScoreCalcInfo> semesterSubjectScoreCalcInfoList = new List<SemesterSubjectScoreCalcInfo>();
                    //#region 建立修改清單
                    //foreach (StudentAttendCourseRecord sacRecord in var.AttendCourseList)
                    //{
                    //    if (!sacRecord.HasFinalScore && !sacRecord.NotIncludedInCalc)
                    //    {
                    //        LogError(var, _ErrorList, "" + sacRecord.CourseName + "沒有修課總成績，無法計算。");
                    //        continue;
                    //    }
                    //    string key = sacRecord.Subject.Trim() + "_" + sacRecord.SubjectLevel.Trim();
                    //    SemesterSubjectScoreCalcInfo info = new SemesterSubjectScoreCalcInfo();
                    //    info.SACRecord = sacRecord;
                    //    if (currentSLScoreDictionary.ContainsKey(key))
                    //    {
                    //        //當學期已有紀錄直接寫入當學期
                    //        #region 直接寫入當學期
                    //        info.UpdateSemesterSubjectScoreInfo = currentSLScoreDictionary[key][0];
                    //        //要評分才處理成績
                    //        if (!sacRecord.NotIncludedInCalc)
                    //        {
                    //            info.原始成績 = sacRecord.FinalScore;
                    //            //如果擇優採計重修成績
                    //            if (choseBetter && aforeSLScoreDictionary.ContainsKey(key))
                    //            {
                    //                decimal max = 0;
                    //                foreach (SemesterSubjectScoreInfo semeScore in aforeSLScoreDictionary[key])
                    //                {
                    //                    if (semeScore.Score > max)
                    //                        max = semeScore.Score;
                    //                }
                    //                if (max > info.原始成績)
                    //                    info.擇優採計成績 = max;
                    //            }
                    //        }
                    //        #endregion
                    //    }
                    //    else if (writeToFirstSemester && aforeSLScoreDictionary.ContainsKey(key))
                    //    {
                    //        //當重修成績寫回原學期且之前有修課紀錄時寫回最後一筆紀錄中
                    //        #region 寫回最後一筆紀錄中
                    //        SemesterSubjectScoreInfo lastSLScoreRecord = null;
                    //        foreach (SemesterSubjectScoreInfo semeScore in aforeSLScoreDictionary[key])
                    //        {
                    //            if (lastSLScoreRecord == null)
                    //                lastSLScoreRecord = semeScore;
                    //            else
                    //                if (lastSLScoreRecord.SchoolYear == semeScore.SchoolYear && lastSLScoreRecord.Semester < semeScore.Semester)
                    //                    lastSLScoreRecord = semeScore;
                    //                else
                    //                    if (lastSLScoreRecord.SchoolYear < semeScore.SchoolYear)
                    //                        lastSLScoreRecord = semeScore;
                    //        }
                    //        info.UpdateSemesterSubjectScoreInfo = lastSLScoreRecord;
                    //        info.重修成績 = sacRecord.FinalScore;
                    //        #endregion
                    //    }
                    //    else
                    //    {
                    //        //新增一筆紀錄至當學期
                    //        #region 新增一筆紀錄
                    //        //要評分才處理成績
                    //        if (!sacRecord.NotIncludedInCalc)
                    //        {
                    //            info.原始成績 = sacRecord.FinalScore;
                    //            //如果擇優採計重修成績
                    //            if (choseBetter && aforeSLScoreDictionary.ContainsKey(key))
                    //            {
                    //                decimal max = 0;
                    //                foreach (SemesterSubjectScoreInfo semeScore in aforeSLScoreDictionary[key])
                    //                {
                    //                    if (semeScore.Score > max)
                    //                        max = semeScore.Score;
                    //                }
                    //                if (max > info.原始成績)
                    //                    info.擇優採計成績 = max;
                    //            }
                    //        }
                    //        #endregion
                    //    }
                    //    semesterSubjectScoreCalcInfoList.Add(info);
                    //}
                    //#endregion
                    //#region 建立semesterSubjectCalcScoreElement
                    //Dictionary<int, Dictionary<int, List<SemesterSubjectScoreCalcInfo>>> semesterCalcInfo = new Dictionary<int, Dictionary<int, List<SemesterSubjectScoreCalcInfo>>>();
                    //#region 照學年度學期分開
                    //foreach (SemesterSubjectScoreCalcInfo calcInfo in semesterSubjectScoreCalcInfoList)
                    //{
                    //    int year, sems;
                    //    if (calcInfo.UpdateSemesterSubjectScoreInfo == null)
                    //    {
                    //        year = schoolyear;
                    //        sems = semester;
                    //    }
                    //    else
                    //    {
                    //        year = calcInfo.UpdateSemesterSubjectScoreInfo.SchoolYear;
                    //        sems = calcInfo.UpdateSemesterSubjectScoreInfo.Semester;
                    //    }
                    //    if (!semesterCalcInfo.ContainsKey(year))
                    //        semesterCalcInfo.Add(year, new Dictionary<int, List<SemesterSubjectScoreCalcInfo>>());
                    //    if (!semesterCalcInfo[year].ContainsKey(sems))
                    //        semesterCalcInfo[year].Add(sems, new List<SemesterSubjectScoreCalcInfo>());
                    //    semesterCalcInfo[year][sems].Add(calcInfo);
                    //}
                    //#endregion
                    //Dictionary<int, Dictionary<int, string>> semeScoreID = (Dictionary<int, Dictionary<int, string>>)var.Fields["SemesterSubjectScoreID"];
                    //foreach (int year in semesterCalcInfo.Keys)
                    //{
                    //    foreach (int sems in semesterCalcInfo[year].Keys)
                    //    {
                    //        XmlElement parentNode;
                    //        #region 建立parentNode，判斷是新增或修改，修改會有ID，新增會有年級
                    //        if (semeScoreID.ContainsKey(year) && semeScoreID[year].ContainsKey(sems))
                    //        {
                    //            parentNode = doc.CreateElement("UpdateSemesterScore");
                    //            parentNode.SetAttribute("ID", semeScoreID[year][sems]);
                    //        }
                    //        else
                    //        {
                    //            parentNode = doc.CreateElement("InsertSemesterScore");
                    //            parentNode.SetAttribute("GradeYear", "" + gradeYear);
                    //        }
                    //        #endregion
                    //        semesterSubjectCalcScoreElement.AppendChild(parentNode);
                    //        Dictionary<string, XmlElement> thisSemesterScores = new Dictionary<string, XmlElement>();
                    //        #region 找尋此學期成績中以存在的成績資料
                    //        if (year == schoolyear && sems == semester)
                    //        {
                    //            foreach (SemesterSubjectScoreInfo s in currentSemesterScoreList)
                    //            {
                    //                string key = s.Subject.Trim() + "_" + s.Level.Trim();
                    //                thisSemesterScores.Add(key, s.Detail);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            foreach (SemesterSubjectScoreInfo s in aforeSemesterScoreList)
                    //            {
                    //                if (s.SchoolYear == year && s.Semester == sems)
                    //                {
                    //                    string key = s.Subject.Trim() + "_" + s.Level.Trim();
                    //                    thisSemesterScores.Add(key, s.Detail);
                    //                }
                    //            }
                    //        }
                    //        #endregion
                    //        #region 新增或修改此學期的成績資料
                    //        foreach (SemesterSubjectScoreCalcInfo calcInfo in semesterCalcInfo[year][sems])
                    //        {
                    //            string key = calcInfo.SACRecord.Subject.Trim() + "_" + calcInfo.SACRecord.SubjectLevel.Trim();
                    //            if (thisSemesterScores.ContainsKey(key))
                    //            {
                    //                #region 修改已存在的資料
                    //                if (calcInfo.重修成績 != null)
                    //                    thisSemesterScores[key].SetAttribute("重修成績", "" + calcInfo.重修成績);
                    //                if (calcInfo.原始成績 != null)
                    //                    thisSemesterScores[key].SetAttribute("原始成績", "" + calcInfo.原始成績);
                    //                if (calcInfo.擇優採計成績 != null)
                    //                    thisSemesterScores[key].SetAttribute("擇優採計成績", "" + calcInfo.擇優採計成績);
                    //                if (calcInfo.SACRecord.NotIncludedInCalc || (calcInfo.原始成績 != null && calcInfo.原始成績 >= applyLimit) || (calcInfo.重修成績 != null && calcInfo.重修成績 >= applyLimit) || (calcInfo.擇優採計成績 != null && calcInfo.擇優採計成績 >= applyLimit))
                    //                    thisSemesterScores[key].SetAttribute("是否取得學分", "是");
                    //                #endregion
                    //            }
                    //            else
                    //            {
                    //                #region 加入新的資料
                    //                XmlElement newScoreInfo = doc.CreateElement("Subject");
                    //                newScoreInfo.SetAttribute("不計學分", calcInfo.SACRecord.NotIncludedInCredit ? "是" : "否");
                    //                newScoreInfo.SetAttribute("不需評分", calcInfo.SACRecord.NotIncludedInCalc ? "是" : "否");
                    //                newScoreInfo.SetAttribute("修課必選修", calcInfo.SACRecord.Required ? "必" : "選");
                    //                newScoreInfo.SetAttribute("修課校部訂", (calcInfo.SACRecord.RequiredBy == "部訂" ? calcInfo.SACRecord.RequiredBy : "校訂"));
                    //                newScoreInfo.SetAttribute("原始成績", "" + calcInfo.原始成績);
                    //                newScoreInfo.SetAttribute("學年調整成績", "");
                    //                newScoreInfo.SetAttribute("擇優採計成績", "" + calcInfo.擇優採計成績);
                    //                //不需評分或分數達及格標準
                    //                if (calcInfo.SACRecord.NotIncludedInCalc || (calcInfo.原始成績 != null && calcInfo.原始成績 >= applyLimit) || (calcInfo.重修成績 != null && calcInfo.重修成績 >= applyLimit) || (calcInfo.擇優採計成績 != null && calcInfo.擇優採計成績 >= applyLimit))
                    //                    newScoreInfo.SetAttribute("是否取得學分", "是");
                    //                else
                    //                    newScoreInfo.SetAttribute("是否取得學分", "否");
                    //                newScoreInfo.SetAttribute("科目", calcInfo.SACRecord.Subject);
                    //                newScoreInfo.SetAttribute("科目級別", calcInfo.SACRecord.SubjectLevel);
                    //                newScoreInfo.SetAttribute("補考成績", "");
                    //                newScoreInfo.SetAttribute("重修成績", "" + calcInfo.重修成績);
                    //                newScoreInfo.SetAttribute("開課分項類別", calcInfo.SACRecord.Entry);
                    //                newScoreInfo.SetAttribute("開課學分數", "" + calcInfo.SACRecord.Credit);
                    //                thisSemesterScores.Add(key, newScoreInfo);
                    //                #endregion
                    //            }
                    //        }
                    //        #endregion
                    //        foreach (XmlElement element in thisSemesterScores.Values)
                    //        {
                    //            parentNode.AppendChild(doc.ImportNode(element, true));
                    //        }
                    //        semesterSubjectCalcScoreElement.AppendChild(parentNode);
                    //    }
                    //}
                    //#endregion 
                    #endregion
                }
                var.Fields.Add("SemesterSubjectCalcScore", semesterSubjectCalcScoreElement);
            }
            return _ErrorList;
        }

        public Dictionary<StudentRecord, List<string>> FillSemesterEntryCalcScore(int schoolyear, int semester, AccessHelper accesshelper, List<StudentRecord> students)
        {
            Dictionary<StudentRecord, List<string>> _ErrorList = new Dictionary<StudentRecord, List<string>>();

            accesshelper.StudentHelper.FillSemesterSubjectScore(false,students);
            foreach (StudentRecord var in students)
            {
                Dictionary<string, int> entryCreditCount = new Dictionary<string, int>();
                Dictionary<string, List<decimal>> entrySubjectScores = new Dictionary<string, List<decimal>>();
                Dictionary<string, decimal> entryDividend = new Dictionary<string, decimal>();
                Dictionary<string, bool> calcEntry = new Dictionary<string, bool>();
                Dictionary<string, bool> calcInStudy = new Dictionary<string, bool>();
                //精準位數
                int decimals = 2;
                //進位模式
                RoundMode mode = RoundMode.四捨五入;
                //成績年級及計算規則皆存在，允許計算成績
                bool canCalc = true;
                #region 取得成績年級跟計算規則
                {
                    #region 處理計算規則
                    XmlElement scoreCalcRule = ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID) == null ? null : ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID).ScoreCalcRuleElement;
                    if (scoreCalcRule == null)
                    {
                        LogError(var, _ErrorList, "沒有設定成績計算規則。");
                        canCalc &= false;
                    }
                    else
                    {
                        DSXmlHelper helper = new DSXmlHelper(scoreCalcRule);
                        bool tryParsebool;
                        int tryParseint;
                        decimal tryParseDecimal;

                        #region 精準位數
                        if (scoreCalcRule.SelectSingleNode("各項成績計算位數/學期分項成績計算位數") != null)
                        {
                            if (int.TryParse(helper.GetText("各項成績計算位數/學期分項成績計算位數/@位數"), out tryParseint))
                                decimals = tryParseint;
                            if (bool.TryParse(helper.GetText("各項成績計算位數/學期分項成績計算位數/@四捨五入"), out tryParsebool) && tryParsebool)
                                mode = RoundMode.四捨五入;
                            if (bool.TryParse(helper.GetText("各項成績計算位數/學期分項成績計算位數/@無條件捨去"), out tryParsebool) && tryParsebool)
                                mode = RoundMode.無條件捨去;
                            if (bool.TryParse(helper.GetText("各項成績計算位數/學期分項成績計算位數/@無條件進位"), out tryParsebool) && tryParsebool)
                                mode = RoundMode.無條件進位;
                        }
                        #endregion
                        #region 計算類別
                        foreach ( string entry in new string[] { "體育", "學業", "國防通識", "健康與護理", "實習科目" } )
                        {
                            if (scoreCalcRule.SelectSingleNode("分項成績計算項目") == null || scoreCalcRule.SelectSingleNode("分項成績計算項目/" + entry) ==null|| ((XmlElement)scoreCalcRule.SelectSingleNode("分項成績計算項目/" + entry)).GetAttribute("計算成績") == "True")
                                calcEntry.Add(entry, true);
                            else
                                calcEntry.Add(entry, false);
                            if (scoreCalcRule.SelectSingleNode("分項成績計算項目") == null|| scoreCalcRule.SelectSingleNode("分項成績計算項目/" + entry) == null || ((XmlElement)scoreCalcRule.SelectSingleNode("分項成績計算項目/" + entry)).GetAttribute("併入學期學業成績") != "True")
                                calcInStudy.Add(entry, false);
                            else
                                calcInStudy.Add(entry, true);
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion
                Dictionary<string, decimal> entryScores = new Dictionary<string, decimal>();
                if ( canCalc )
                {
                    #region 將成績分到各分項類別中
                    foreach ( SemesterSubjectScoreInfo subjectNode in var.SemesterSubjectScoreList )
                    {
                        if ( subjectNode.SchoolYear == schoolyear && subjectNode.Semester == semester )
                        {
                            //不計學分或不需評分不用算
                            if ( subjectNode.Detail.GetAttribute("不需評分") == "是" || subjectNode.Detail.GetAttribute("不計學分") == "是" )
                                continue;
                            #region 分項類別跟學分數
                            string entry = subjectNode.Detail.GetAttribute("開課分項類別");
                            int credit = subjectNode.Credit;
                            #endregion
                            decimal maxScore = 0;
                            #region 取得最高分數
                            decimal tryParseDecimal;
                            if ( decimal.TryParse(subjectNode.Detail.GetAttribute("原始成績"), out tryParseDecimal) )
                                maxScore = tryParseDecimal;
                            if ( decimal.TryParse(subjectNode.Detail.GetAttribute("學年調整成績"), out tryParseDecimal) && maxScore < tryParseDecimal )
                                maxScore = tryParseDecimal;
                            if ( decimal.TryParse(subjectNode.Detail.GetAttribute("擇優採計成績"), out tryParseDecimal) && maxScore < tryParseDecimal )
                                maxScore = tryParseDecimal;
                            if ( decimal.TryParse(subjectNode.Detail.GetAttribute("補考成績"), out tryParseDecimal) && maxScore < tryParseDecimal )
                                maxScore = tryParseDecimal;
                            if ( decimal.TryParse(subjectNode.Detail.GetAttribute("重修成績"), out tryParseDecimal) && maxScore < tryParseDecimal )
                                maxScore = tryParseDecimal;
                            #endregion
                            switch ( entry )
                            {
                                case "體育":
                                case "國防通識":
                                case "健康與護理":
                                case "實習科目":
                                    //計算分項成績
                                    if ( calcEntry[entry] )
                                    {
                                        //加總學分數
                                        if ( !entryCreditCount.ContainsKey(entry) )
                                            entryCreditCount.Add(entry, credit);
                                        else
                                            entryCreditCount[entry] += credit;
                                        //加入將成績資料分項
                                        if ( !entrySubjectScores.ContainsKey(entry) ) entrySubjectScores.Add(entry, new List<decimal>());
                                        entrySubjectScores[entry].Add(maxScore);
                                        //加權總計
                                        if ( !entryDividend.ContainsKey(entry) )
                                            entryDividend.Add(entry, maxScore * credit);
                                        else
                                            entryDividend[entry] += ( maxScore * credit );
                                    }
                                    //將科目成績與學業成績一併計算
                                    if ( calcInStudy[entry] )
                                    {
                                        //加總學分數
                                        if ( !entryCreditCount.ContainsKey("學業") )
                                            entryCreditCount.Add("學業", credit);
                                        else
                                            entryCreditCount["學業"] += credit;
                                        //加入將成績資料分項
                                        if ( !entrySubjectScores.ContainsKey("學業") ) entrySubjectScores.Add("學業", new List<decimal>());
                                        entrySubjectScores["學業"].Add(maxScore);
                                        //加權總計
                                        if ( !entryDividend.ContainsKey("學業") )
                                            entryDividend.Add("學業", maxScore * credit);
                                        else
                                            entryDividend["學業"] += ( maxScore * credit );
                                    }
                                    break;

                                case "學業":
                                default:
                                    //加總學分數
                                    if ( !entryCreditCount.ContainsKey("學業") )
                                        entryCreditCount.Add("學業", credit);
                                    else
                                        entryCreditCount["學業"] += credit;
                                    //加入將成績資料分項
                                    if ( !entrySubjectScores.ContainsKey("學業") ) entrySubjectScores.Add("學業", new List<decimal>());
                                    entrySubjectScores["學業"].Add(maxScore);
                                    //加權總計
                                    if ( !entryDividend.ContainsKey("學業") )
                                        entryDividend.Add("學業", maxScore * credit);
                                    else
                                        entryDividend["學業"] += ( maxScore * credit );
                                    break;
                            }
                        }
                    }
                    #endregion
                    #region 處理計算各分項類別的成績
                    foreach ( string entry in entryCreditCount.Keys )
                    {
                        decimal entryScore = 0;
                        #region 計算entryScore
                        if ( entryCreditCount[entry] == 0 )
                        {
                            foreach ( decimal score in entrySubjectScores[entry] )
                            {
                                entryScore += score;
                            }
                            entryScore = ( entryScore / entrySubjectScores[entry].Count );
                        }
                        else
                        {
                            //用加權總分除學分數
                            entryScore = ( entryDividend[entry] / entryCreditCount[entry] );
                        }
                        #endregion
                        //精準位數處理
                        entryScore = GetRoundScore(entryScore, decimals, mode);
                        #region 填入EntryScores
                        entryScores.Add(entry, entryScore);
                        #endregion
                    }
                    #endregion
                }
                if ( var.Fields.ContainsKey("CalcEntryScores") )
                    var.Fields["CalcEntryScores"] = entryScores;
                else
                    var.Fields.Add("CalcEntryScores", entryScores);
            }
            return _ErrorList;
        }

        public Dictionary<StudentRecord, List<string>> FillSchoolYearEntryCalcScore(int schoolyear, AccessHelper accesshelper, List<StudentRecord> students)
        {
            Dictionary<StudentRecord, List<string>> _ErrorList = new Dictionary<StudentRecord, List<string>>();
            //抓成績資料
            accesshelper.StudentHelper.FillSemesterEntryScore(false, students);
            foreach ( StudentRecord var in students )
            {
                //計算結果
                Dictionary<string, decimal> entryCalcScores = new Dictionary<string, decimal>();
                //精準位數
                int decimals = 2;
                //進位模式
                RoundMode mode = RoundMode.四捨五入;
                //成績年級及計算規則皆存在，允許計算成績
                bool canCalc = true;
                Dictionary<string, bool> calcEntry = new Dictionary<string, bool>();
                #region 取得成績年級跟計算規則
                {
                    #region 處理計算規則
                    XmlElement scoreCalcRule = ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID) == null ? null : ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID).ScoreCalcRuleElement;
                    if ( scoreCalcRule == null )
                    {
                        LogError(var, _ErrorList, "沒有設定成績計算規則。");
                        canCalc &= false;
                    }
                    else
                    {
                        DSXmlHelper helper = new DSXmlHelper(scoreCalcRule);
                        bool tryParsebool;
                        int tryParseint;
                        decimal tryParseDecimal;

                        #region 精準位數
                        if ( scoreCalcRule.SelectSingleNode("各項成績計算位數/學年分項成績計算位數") != null )
                        {
                            if ( int.TryParse(helper.GetText("各項成績計算位數/學年分項成績計算位數/@位數"), out tryParseint) )
                                decimals = tryParseint;
                            if ( bool.TryParse(helper.GetText("各項成績計算位數/學年分項成績計算位數/@四捨五入"), out tryParsebool) && tryParsebool )
                                mode = RoundMode.四捨五入;
                            if ( bool.TryParse(helper.GetText("各項成績計算位數/學年分項成績計算位數/@無條件捨去"), out tryParsebool) && tryParsebool )
                                mode = RoundMode.無條件捨去;
                            if ( bool.TryParse(helper.GetText("各項成績計算位數/學年分項成績計算位數/@無條件進位"), out tryParsebool) && tryParsebool )
                                mode = RoundMode.無條件進位;
                        }
                        #endregion
                        #region 計算類別
                        foreach ( string entry in new string[] { "體育", "學業", "國防通識", "健康與護理", "實習科目" } )
                        {
                            if ( scoreCalcRule.SelectSingleNode("分項成績計算項目") == null || scoreCalcRule.SelectSingleNode("分項成績計算項目/" + entry) == null || ( (XmlElement)scoreCalcRule.SelectSingleNode("分項成績計算項目/" + entry) ).GetAttribute("計算成績") == "True" )
                                calcEntry.Add(entry, true);
                            else
                                calcEntry.Add(entry, false);
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion
                if ( canCalc )
                {
                    int? gradeyear = null;
                    #region 抓年級
                    foreach ( SemesterEntryScoreInfo score in var.SemesterEntryScoreList )
                    {
                        if ( calcEntry.ContainsKey(score.Entry) && score.SchoolYear == schoolyear )
                        {
                            if ( gradeyear == null || score.GradeYear > gradeyear )
                                gradeyear = score.GradeYear;
                        }
                    }
                    #endregion
                    if ( gradeyear != null )
                    {
                        #region 移除不需要成績
                        Dictionary<int, int> ApplySemesterSchoolYear = new Dictionary<int, int>();
                        //先掃一遍抓出該年級最高的學年度
                        foreach ( SemesterEntryScoreInfo scoreInfo in var.SemesterEntryScoreList )
                        {
                            if ( scoreInfo.SchoolYear <= schoolyear && scoreInfo.GradeYear == gradeyear )
                            {
                                if ( !ApplySemesterSchoolYear.ContainsKey(scoreInfo.Semester) )
                                    ApplySemesterSchoolYear.Add(scoreInfo.Semester, scoreInfo.SchoolYear);
                                else
                                {
                                    if ( ApplySemesterSchoolYear[scoreInfo.Semester] < scoreInfo.SchoolYear )
                                        ApplySemesterSchoolYear[scoreInfo.Semester] = scoreInfo.SchoolYear;
                                }
                            }
                        }
                        //如果成績資料的年級學年度不在清單中就移掉
                        List<SemesterEntryScoreInfo> removeList = new List<SemesterEntryScoreInfo>();
                        foreach ( SemesterEntryScoreInfo scoreInfo in var.SemesterEntryScoreList )
                        {
                            if ( !ApplySemesterSchoolYear.ContainsKey(scoreInfo.Semester) || ApplySemesterSchoolYear[scoreInfo.Semester] != scoreInfo.SchoolYear )
                                removeList.Add(scoreInfo);
                        }
                        foreach ( SemesterEntryScoreInfo scoreInfo in removeList )
                        {
                            var.SemesterEntryScoreList.Remove(scoreInfo);
                        }
                        #endregion
                        #region 計算該年級的分項成績
                        Dictionary<string, List<decimal>> entryScores = new Dictionary<string, List<decimal>>();
                        foreach ( SemesterEntryScoreInfo score in var.SemesterEntryScoreList )
                        {
                            if ( calcEntry.ContainsKey(score.Entry) && score.SchoolYear <= schoolyear && score.GradeYear == gradeyear )
                            {
                                if ( !entryScores.ContainsKey(score.Entry) )
                                    entryScores.Add(score.Entry, new List<decimal>());
                                entryScores[score.Entry].Add(score.Score);
                            }
                        }
                        foreach ( string key in entryScores.Keys )
                        {
                            decimal sum = 0;
                            decimal count = 0;
                            foreach ( decimal sc in entryScores[key] )
                            {
                                sum += sc;
                                count += 1;
                            }
                            if ( count > 0 )
                                entryCalcScores.Add(key, GetRoundScore(sum / count, decimals, mode));
                        }
                        #endregion
                    }
                }
                if ( var.Fields.ContainsKey("CalcSchoolYearEntryScores") )
                    var.Fields["CalcSchoolYearEntryScores"] = entryCalcScores;
                else
                    var.Fields.Add("CalcSchoolYearEntryScores", entryCalcScores);
            }
            return _ErrorList;
        }

        public Dictionary<StudentRecord, List<string>> FillSchoolYearSubjectCalcScore(int schoolyear, AccessHelper accesshelper, List<StudentRecord> students)
        {
            Dictionary<StudentRecord, List<string>> _ErrorList = new Dictionary<StudentRecord, List<string>>();
            //抓成績資料
            accesshelper.StudentHelper.FillSemesterSubjectScore(false, students);
            foreach ( StudentRecord var in students )
            {
                #region 處理CalcSchoolYearSubjectScores
                //計算結果
                Dictionary<string, decimal> subjectCalcScores = new Dictionary<string, decimal>();
                //精準位數
                int decimals = 2;
                //進位模式
                RoundMode mode = RoundMode.四捨五入;
                //成績年級及計算規則皆存在，允許計算成績
                bool canCalc = true;
                Dictionary<string, bool> calcEntry = new Dictionary<string, bool>();
                #region 取得成績年級跟計算規則
                 #region 處理計算規則
                XmlElement scoreCalcRule = ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID) == null ? null : ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID).ScoreCalcRuleElement;
                    if ( scoreCalcRule == null )
                    {
                        LogError(var, _ErrorList, "沒有設定成績計算規則。");
                        canCalc &= false;
                    }
                    else
                    {
                        DSXmlHelper helper = new DSXmlHelper(scoreCalcRule);
                        bool tryParsebool;
                        int tryParseint;
                        decimal tryParseDecimal;

                        #region 精準位數
                        if ( scoreCalcRule.SelectSingleNode("各項成績計算位數/學年科目成績計算位數") != null )
                        {
                            if ( int.TryParse(helper.GetText("各項成績計算位數/學年科目成績計算位數/@位數"), out tryParseint) )
                                decimals = tryParseint;
                            if ( bool.TryParse(helper.GetText("各項成績計算位數/學年科目成績計算位數/@四捨五入"), out tryParsebool) && tryParsebool )
                                mode = RoundMode.四捨五入;
                            if ( bool.TryParse(helper.GetText("各項成績計算位數/學年科目成績計算位數/@無條件捨去"), out tryParsebool) && tryParsebool )
                                mode = RoundMode.無條件捨去;
                            if ( bool.TryParse(helper.GetText("各項成績計算位數/學年科目成績計算位數/@無條件進位"), out tryParsebool) && tryParsebool )
                                mode = RoundMode.無條件進位;
                        }
                        #endregion
                    }
                    #endregion
                #endregion
                int? gradeyear = null;
                if ( canCalc )
                {
                    #region 抓年級
                    foreach ( SemesterSubjectScoreInfo score in var.SemesterSubjectScoreList )
                    {
                        if ( score.SchoolYear == schoolyear )
                        {
                            if ( gradeyear == null || score.GradeYear > gradeyear )
                                gradeyear = score.GradeYear;
                        }
                    }
                    #endregion
                    if ( gradeyear != null )
                    {
                        #region 移除不需要成績
                        Dictionary<int, int> ApplySemesterSchoolYear = new Dictionary<int, int>();
                        //先掃一遍抓出該年級最高的學年度
                        foreach ( SemesterSubjectScoreInfo scoreInfo in var.SemesterSubjectScoreList )
                        {
                            if ( scoreInfo.SchoolYear <= schoolyear && scoreInfo.GradeYear == gradeyear )
                            {
                                if ( !ApplySemesterSchoolYear.ContainsKey(scoreInfo.Semester) )
                                    ApplySemesterSchoolYear.Add(scoreInfo.Semester, scoreInfo.SchoolYear);
                                else
                                {
                                    if ( ApplySemesterSchoolYear[scoreInfo.Semester] < scoreInfo.SchoolYear )
                                        ApplySemesterSchoolYear[scoreInfo.Semester] = scoreInfo.SchoolYear;
                                }
                            }
                        }
                        //如果成績資料的年級學年度不在清單中就移掉
                        List<SemesterSubjectScoreInfo> removeList = new List<SemesterSubjectScoreInfo>();
                        foreach ( SemesterSubjectScoreInfo scoreInfo in var.SemesterSubjectScoreList )
                        {
                            if ( !ApplySemesterSchoolYear.ContainsKey(scoreInfo.Semester) || ApplySemesterSchoolYear[scoreInfo.Semester] != scoreInfo.SchoolYear )
                                removeList.Add(scoreInfo);
                        }
                        foreach ( SemesterSubjectScoreInfo scoreInfo in removeList )
                        {
                            var.SemesterSubjectScoreList.Remove(scoreInfo);
                        }
                        #endregion
                        #region 計算該年級的科目成績
                        Dictionary<string, List<decimal>> subjectScores = new Dictionary<string, List<decimal>>();
                        foreach ( SemesterSubjectScoreInfo score in var.SemesterSubjectScoreList )
                        {
                            if ( score.SchoolYear <= schoolyear && score.GradeYear == gradeyear )
                            {
                                //先判斷這筆科目成績能不能計算
                                decimal maxscore = decimal.MinValue, tryParsedecimal;
                                bool hasScore = false;
                                foreach ( string scoreType in new string[] { "原始成績", "補考成績","重修成績", "擇優採計成績" } )
                                {
                                    if ( decimal.TryParse(score.Detail.GetAttribute(scoreType), out tryParsedecimal) )
                                    {
                                        hasScore |= true;
                                        if ( tryParsedecimal > maxscore )
                                            maxscore = tryParsedecimal;
                                    }
                                }
                                //可以倍計算
                                if ( hasScore )
                                {
                                    if ( !subjectScores.ContainsKey(score.Subject) )
                                        subjectScores.Add(score.Subject, new List<decimal>());
                                    subjectScores[score.Subject].Add(maxscore);
                                }
                            }
                        }
                        foreach ( string key in subjectScores.Keys )
                        {
                            decimal sum = 0;
                            decimal count = 0;
                            foreach ( decimal sc in subjectScores[key] )
                            {
                                sum += sc;
                                count += 1;
                            }
                            if ( count > 0 )
                                subjectCalcScores.Add(key, GetRoundScore(sum / count, decimals, mode));
                        }
                        #endregion
                    }
                }
                if ( var.Fields.ContainsKey("CalcSchoolYearSubjectScores") )
                    var.Fields["CalcSchoolYearSubjectScores"] = subjectCalcScores;
                else
                    var.Fields.Add("CalcSchoolYearSubjectScores", subjectCalcScores); 
                #endregion
                #region 處理SchoolYearApplyScores
                canCalc = true;
                //及格標準<年及,及格標準>
                Dictionary<int, decimal> applyLimit = new Dictionary<int, decimal>();
                //0:不登錄，1:60分登錄，2:及格標準登錄
                int regWay = 0;
                #region 處理計算規則
                if ( scoreCalcRule == null )
                {
                    canCalc &= false;
                }
                else
                {
                    DSXmlHelper helper = new DSXmlHelper(scoreCalcRule);
                    #region 及格標準
                    foreach ( XmlElement element in helper.GetElements("及格標準/學生類別") )
                    {
                        string cat = element.GetAttribute("類別");
                        bool useful = false;
                        //掃描學生的類別作比對
                        foreach ( CategoryInfo catinfo in var.StudentCategorys )
                        {
                            if ( catinfo.Name == cat || catinfo.FullName == cat )
                                useful = true;
                        }
                        //學生是指定的類別或類別為"預設"
                        if ( cat == "預設" || useful )
                        {
                            decimal tryParseDecimal;
                            for ( int gyear = 1 ; gyear <= 4 ; gyear++ )
                            {
                                switch ( gyear )
                                {
                                    case 1:
                                        if ( decimal.TryParse(element.GetAttribute("一年級及格標準"), out tryParseDecimal) )
                                        {
                                            if ( !applyLimit.ContainsKey(gyear) )
                                                applyLimit.Add(gyear, tryParseDecimal);
                                            if ( applyLimit[gyear] > tryParseDecimal )
                                                applyLimit[gyear] = tryParseDecimal;
                                        }
                                        break;
                                    case 2:
                                        if ( decimal.TryParse(element.GetAttribute("二年級及格標準"), out tryParseDecimal) )
                                        {
                                            if ( !applyLimit.ContainsKey(gyear) )
                                                applyLimit.Add(gyear, tryParseDecimal);
                                            if ( applyLimit[gyear] > tryParseDecimal )
                                                applyLimit[gyear] = tryParseDecimal;
                                        }
                                        break;
                                    case 3:
                                        if ( decimal.TryParse(element.GetAttribute("三年級及格標準"), out tryParseDecimal) )
                                        {
                                            if ( !applyLimit.ContainsKey(gyear) )
                                                applyLimit.Add(gyear, tryParseDecimal);
                                            if ( applyLimit[gyear] > tryParseDecimal )
                                                applyLimit[gyear] = tryParseDecimal;
                                        }
                                        break;
                                    case 4:
                                        if ( decimal.TryParse(element.GetAttribute("四年級及格標準"), out tryParseDecimal) )
                                        {
                                            if ( !applyLimit.ContainsKey(gyear) )
                                                applyLimit.Add(gyear, tryParseDecimal);
                                            if ( applyLimit[gyear] > tryParseDecimal )
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
                    bool tryParseBool=false;
                    if ( bool.TryParse(helper.GetText("學年調整成績/@不登錄學年調整成績"), out tryParseBool) && tryParseBool )
                        regWay = 0;
                    if ( bool.TryParse(helper.GetText("學年調整成績/@以六十分登錄"), out tryParseBool) && tryParseBool )
                        regWay = 1;
                    if ( bool.TryParse(helper.GetText("學年調整成績/@以學生及格標準登錄"), out tryParseBool) && tryParseBool )
                        regWay = 2;
                }
                #endregion
                #region 處理學年調整成績
                List<SemesterSubjectScoreInfo> updateScores = new List<SemesterSubjectScoreInfo>();
                if ( gradeyear != null )
                {
                    decimal applylimit=40;
                    if(applyLimit.ContainsKey((int)gradeyear))
                        applylimit=applyLimit[(int)gradeyear];
                    foreach ( SemesterSubjectScoreInfo score in var.SemesterSubjectScoreList )
                    {
                        if ( !score.Pass && subjectCalcScores.ContainsKey(score.Subject) && subjectCalcScores[score.Subject] >= applylimit )
                        {
                            switch ( regWay )
                            { 
                                default:
                                case 0:
                                    break;
                                case 1:
                                    score.Detail.SetAttribute("學年調整成績", "60");
                                    break;
                                case 2:
                                    score.Detail.SetAttribute("學年調整成績", "" + applylimit);
                                    break;

                            }
                            score.Detail.SetAttribute("是否取得學分", "是");
                            updateScores.Add(score);
                        }
                    }
                }
                if ( var.Fields.ContainsKey("SchoolYearApplyScores") )
                    var.Fields["SchoolYearApplyScores"] = updateScores;
                else
                    var.Fields.Add("SchoolYearApplyScores", updateScores); 
                #endregion
                #endregion
            }
            return _ErrorList;
        }

        public Dictionary<StudentRecord, List<string>> FillStudentGradCalcScore(AccessHelper accesshelper, List<StudentRecord> students)
        {
            Dictionary<StudentRecord, List<string>> _ErrorList = new Dictionary<StudentRecord, List<string>>();
            //抓成績資料
            accesshelper.StudentHelper.FillSemesterSubjectScore(true, students);
            accesshelper.StudentHelper.FillSemesterEntryScore(true, students);
             foreach ( StudentRecord var in students )
             {
                 //可以計算(表示需要的資料都有)
                 bool canCalc = true;
                 //精準位數(預設小數點後2位)
                 int decimals = 2;
                 //進位模式(預設四捨五入)
                 RoundMode mode = RoundMode.四捨五入;
                 //計算資料採用課程規劃(預設採用課程規劃)
                 bool useGPlan = true;
                 //畢業成績使用所有科目成績加權計算(預設為否=>使用分項成績平均 )
                 bool useSubjectAdv = false;

                 XmlElement scoreCalcRule = ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID) == null ? null : ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID).ScoreCalcRuleElement;
                 if ( scoreCalcRule == null )
                 {
                     LogError(var, _ErrorList, "沒有設定成績計算規則。");
                     canCalc &= false;
                 }
                 else
                 {
                     DSXmlHelper helper = new DSXmlHelper(scoreCalcRule);
                     #region 處理精準位數
                     bool tryParsebool;
                     int tryParseint;
                     decimal tryParseDecimal;

                     if ( scoreCalcRule.SelectSingleNode("各項成績計算位數/畢業成績計算位數") != null )
                     {
                         if ( int.TryParse(helper.GetText("各項成績計算位數/畢業成績計算位數/@位數"), out tryParseint) )
                             decimals = tryParseint;
                         if ( bool.TryParse(helper.GetText("各項成績計算位數/畢業成績計算位數/@四捨五入"), out tryParsebool) && tryParsebool )
                             mode = RoundMode.四捨五入;
                         if ( bool.TryParse(helper.GetText("各項成績計算位數/畢業成績計算位數/@無條件捨去"), out tryParsebool) && tryParsebool )
                             mode = RoundMode.無條件捨去;
                         if ( bool.TryParse(helper.GetText("各項成績計算位數/畢業成績計算位數/@無條件進位"), out tryParsebool) && tryParsebool )
                             mode = RoundMode.無條件進位;
                     } 
                     #endregion
                     #region 處理採計方式
                     if ( scoreCalcRule.SelectSingleNode("學期科目成績屬性採計方式") != null )
                     {
                         if ( scoreCalcRule.SelectSingleNode("學期科目成績屬性採計方式").InnerText == "以實際學期科目成績內容為準" )
                         {
                             useGPlan = false;
                         }
                     }
                     if ( useGPlan && GraduationPlan.GraduationPlan.Instance.GetStudentGraduationPlan(var.StudentID) == null )
                     {
                         LogError(var, _ErrorList, "沒有設定課程規劃表，當\"學期科目成績屬性採計方式\"設定使用\"以課程規劃表內容為準\"時，學生必須要有課程規劃表以做參考。");
                         canCalc &= false;
                     }
                     #endregion
                     #region 處理畢業成績計算規則
                     if ( scoreCalcRule.SelectSingleNode("處理畢業成績計算規則") != null )
                     {
                         useSubjectAdv = scoreCalcRule.SelectSingleNode("處理畢業成績計算規則").InnerText == "學期科目成績加權";
                     }
                     #endregion
                 }


                 XmlDocument doc = new XmlDocument();
                 XmlElement gradeCalcScoreElement = doc.CreateElement("GradScore");
                 if ( canCalc )
                 {
                     Dictionary<string, int> entryCount = new Dictionary<string, int>();
                     Dictionary<string, decimal> entrySum = new Dictionary<string, decimal>();
                     //使用所有科目成績加權計算畢業成績(學業)
                     if ( useSubjectAdv )
                     {
                         #region 使用所有科目成績加權計算畢業成績(總分及總數)
                         int creditCount = 0;
                         decimal scoreSum = 0;
                         foreach ( SemesterSubjectScoreInfo subjectScoreInfo in var.SemesterSubjectScoreList )
                         {
                             if ( useGPlan )//相關屬性採用課程規劃
                             {
                                 //不計學分或不需評分不用算
                                 if ( GraduationPlan.GraduationPlan.Instance.GetStudentGraduationPlan(var.StudentID).GetSubjectInfo(subjectScoreInfo.Subject, subjectScoreInfo.Level).NotIncludedInCredit||
                                      GraduationPlan.GraduationPlan.Instance.GetStudentGraduationPlan(var.StudentID).GetSubjectInfo(subjectScoreInfo.Subject, subjectScoreInfo.Level).NotIncludedInCalc
                                     )
                                     continue;
                                 int realCredit = 0;
                                 int.TryParse(GraduationPlan.GraduationPlan.Instance.GetStudentGraduationPlan(var.StudentID).GetSubjectInfo(subjectScoreInfo.Subject, subjectScoreInfo.Level).Credit, out realCredit);
                                 scoreSum += subjectScoreInfo.Score * realCredit;
                                 creditCount += realCredit;
                             }
                             else//相關屬性直接採用成績內容
                             {
                                 //不計學分不用算
                                 if ( subjectScoreInfo.Detail.GetAttribute("不計學分") == "是" || subjectScoreInfo.Detail.GetAttribute("不需評分") == "是" )
                                     continue;
                                 scoreSum += subjectScoreInfo.Score * subjectScoreInfo.Credit;
                                 creditCount += subjectScoreInfo.Credit;
                             }
                         }
                         if ( creditCount != 0 )
                         {
                             entryCount.Add("學業", creditCount);
                             entrySum.Add("學業", scoreSum);
                         }
                         #endregion
                     }
                     //計算各分項畢業成績(總分及總數)
                     foreach ( SemesterEntryScoreInfo entryScore in var.SemesterEntryScoreList )
                     {
                         #region 計算各分項畢業成績
                         if ( entryScore.Entry != "學業" || !useSubjectAdv )//其它分項或者是學業分項但不使用科目成績加權
                         {
                             if ( !entryCount.ContainsKey(entryScore.Entry) )
                                 entryCount.Add(entryScore.Entry, 0);
                             if ( !entrySum.ContainsKey(entryScore.Entry) )
                                 entrySum.Add(entryScore.Entry, 0);

                             entryCount[entryScore.Entry]++;
                             entrySum[entryScore.Entry] += entryScore.Score;
                         }
                         #endregion
                     }
                     //計算&填入分項畢業成績
                     foreach ( string entry in entryCount.Keys )
                     {
                         XmlElement entryScoreElement = doc.CreateElement("EntryScore");
                         entryScoreElement.SetAttribute("Entry", entry);
                         entryScoreElement.SetAttribute("Score",""+ GetRoundScore( entrySum[entry] / entryCount[entry],decimals,mode));
                         gradeCalcScoreElement.AppendChild(entryScoreElement);
                     }
                 }
                 if ( var.Fields.ContainsKey("GradCalcScore") )
                     var.Fields.Remove("GradCalcScore");
                 var.Fields.Add("GradCalcScore", gradeCalcScoreElement);
             }
            return _ErrorList;
        }

        public Dictionary<StudentRecord, List<string>> FillStudentGradCheck(AccessHelper accesshelper, List<StudentRecord> students)
        {
            Dictionary<StudentRecord, List<string>> _ErrorList = new Dictionary<StudentRecord, List<string>>();
            //抓成績資料
            accesshelper.StudentHelper.FillSemesterSubjectScore(true, students);
            accesshelper.StudentHelper.FillSemesterEntryScore(true, students);
            accesshelper.StudentHelper.FillSchoolYearEntryScore(true, students);
            foreach ( StudentRecord var in students )
            {
                //可以計算(表示需要的資料都有)
                bool canCalc = true;

                //計算資料採用課程規劃(預設採用課程規劃)
                bool useGPlan = true;
                //畢業學分需求
                decimal 總學分數 = 0;
                decimal 必修學分數 = 0;
                decimal 選修學分數 = 0;
                decimal 部訂必修學分數 = 0;
                decimal 校訂必修學分數 = 0;
                decimal 實習學分數 = 0;
                //學期德行皆及格||學年德行皆及格
                bool everySemesterIsGoodDog = false;

                XmlElement scoreCalcRule = ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID) == null ? null : ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID).ScoreCalcRuleElement;
                if ( scoreCalcRule == null )
                {
                    LogError(var, _ErrorList, "沒有設定成績計算規則。");
                    canCalc &= false;
                }
                else
                {
                    DSXmlHelper helper = new DSXmlHelper(scoreCalcRule);
                    #region 學期科目成績屬性採計方式
                    if ( scoreCalcRule.SelectSingleNode("學期科目成績屬性採計方式") != null )
                    {
                        if ( scoreCalcRule.SelectSingleNode("學期科目成績屬性採計方式").InnerText == "以實際學期科目成績內容為準" )
                        {
                            useGPlan = false;
                        }
                    }
                    if ( useGPlan && GraduationPlan.GraduationPlan.Instance.GetStudentGraduationPlan(var.StudentID) == null )
                    {
                        LogError(var, _ErrorList, "沒有設定課程規劃表，當\"學期科目成績屬性採計方式\"設定使用\"以課程規劃表內容為準\"時，學生必須要有課程規劃表以做參考。");
                        canCalc &= false;
                    }
                    #endregion
                    #region 畢業學分數
                    //學科累計總學分數
                    #region 總學分數
                    if ( scoreCalcRule.SelectSingleNode("畢業學分數/學科累計總學分數") != null )
                    {
                        string creditstring = scoreCalcRule.SelectSingleNode("畢業學分數/學科累計總學分數").InnerText.Trim();
                        if ( creditstring != "" )
                        {
                            if ( creditstring.Contains("%") )
                            {
                                decimal count = 0;
                                foreach ( GraduationPlan.GraduationPlanSubject gplanSubject in GraduationPlan.GraduationPlan.Instance.GetStudentGraduationPlan(var.StudentID).Subjects )
                                {
                                    decimal credit = 0;
                                    decimal.TryParse(gplanSubject.Credit, out credit);
                                    count += credit;
                                }
                                decimal.TryParse(creditstring.Replace("%", ""), out 總學分數);
                                總學分數 = 總學分數 * count / 100m;
                            }
                            else
                            {
                                decimal.TryParse(creditstring, out 總學分數);
                            }
                        }
                    }
                    #endregion
                    //必修學分數
                    #region 必修學分數
                    if ( scoreCalcRule.SelectSingleNode("畢業學分數/必修學分數") != null )
                    {
                        string creditstring = scoreCalcRule.SelectSingleNode("畢業學分數/必修學分數").InnerText.Trim();
                        if ( creditstring != "" )
                        {
                            if ( creditstring.Contains("%") )
                            {
                                decimal count = 0;
                                foreach ( GraduationPlan.GraduationPlanSubject gplanSubject in GraduationPlan.GraduationPlan.Instance.GetStudentGraduationPlan(var.StudentID).Subjects )
                                {
                                    if ( gplanSubject.Required == "必修" )
                                    {
                                        decimal credit = 0;
                                        decimal.TryParse(gplanSubject.Credit, out credit);
                                        count += credit;
                                    }
                                }
                                decimal.TryParse(creditstring.Replace("%", ""), out 必修學分數);
                                必修學分數 = 必修學分數 * count / 100m;
                            }
                            else
                            {
                                decimal.TryParse(creditstring, out 必修學分數);
                            }
                        }
                    }
                    #endregion
                    //部訂必修學分數
                    #region 部訂必修學分數
                    if ( scoreCalcRule.SelectSingleNode("畢業學分數/部訂必修學分數") != null )
                    {
                        string creditstring = scoreCalcRule.SelectSingleNode("畢業學分數/部訂必修學分數").InnerText.Trim();
                        if ( creditstring != "" )
                        {
                            if ( creditstring.Contains("%") )
                            {
                                decimal count = 0;
                                foreach ( GraduationPlan.GraduationPlanSubject gplanSubject in GraduationPlan.GraduationPlan.Instance.GetStudentGraduationPlan(var.StudentID).Subjects )
                                {
                                    if ( gplanSubject.Required == "必修" && gplanSubject.RequiredBy == "部訂" )
                                    {
                                        decimal credit = 0;
                                        decimal.TryParse(gplanSubject.Credit, out credit);
                                        count += credit;
                                    }
                                }
                                decimal.TryParse(creditstring.Replace("%", ""), out 部訂必修學分數);
                                部訂必修學分數 = 部訂必修學分數 * count / 100m;
                            }
                            else
                            {
                                decimal.TryParse(creditstring, out 部訂必修學分數);
                            }
                        }
                    }
                    #endregion
                    //實習學分數
                    #region 實習學分數
                    if ( scoreCalcRule.SelectSingleNode("畢業學分數/實習學分數") != null )
                    {
                        string creditstring = scoreCalcRule.SelectSingleNode("畢業學分數/實習學分數").InnerText.Trim();
                        if ( creditstring != "" )
                        {
                            if ( creditstring.Contains("%") )
                            {
                                decimal count = 0;
                                foreach ( GraduationPlan.GraduationPlanSubject gplanSubject in GraduationPlan.GraduationPlan.Instance.GetStudentGraduationPlan(var.StudentID).Subjects )
                                {
                                    if ( gplanSubject.Entry == "實習科目" )
                                    {
                                        decimal credit = 0;
                                        decimal.TryParse(gplanSubject.Credit, out credit);
                                        count += credit;
                                    }
                                }
                                decimal.TryParse(creditstring.Replace("%", ""), out 實習學分數);
                                實習學分數 = 實習學分數 * count / 100m;
                            }
                            else
                            {
                                decimal.TryParse(creditstring, out 實習學分數);
                            }
                        }
                    }
                    #endregion
                    //選修學分數
                    #region 選修學分數
                    if ( scoreCalcRule.SelectSingleNode("畢業學分數/選修學分數") != null )
                    {
                        string creditstring = scoreCalcRule.SelectSingleNode("畢業學分數/選修學分數").InnerText.Trim();
                        if ( creditstring != "" )
                        {
                            if ( creditstring.Contains("%") )
                            {
                                decimal count = 0;
                                foreach ( GraduationPlan.GraduationPlanSubject gplanSubject in GraduationPlan.GraduationPlan.Instance.GetStudentGraduationPlan(var.StudentID).Subjects )
                                {
                                    if ( gplanSubject.Required == "選修" )
                                    {
                                        decimal credit = 0;
                                        decimal.TryParse(gplanSubject.Credit, out credit);
                                        count += credit;
                                    }
                                }
                                decimal.TryParse(creditstring.Replace("%", ""), out 選修學分數);
                                選修學分數 = 選修學分數 * count / 100m;
                            }
                            else
                            {
                                decimal.TryParse(creditstring, out 選修學分數);
                            }
                        }
                    }
                    #endregion
                    //校訂必修學分數
                    #region 校訂必修學分數
                    if ( scoreCalcRule.SelectSingleNode("畢業學分數/校訂必修學分數") != null )
                    {
                        string creditstring = scoreCalcRule.SelectSingleNode("畢業學分數/校訂必修學分數").InnerText.Trim();
                        if ( creditstring != "" )
                        {
                            if ( creditstring.Contains("%") )
                            {
                                decimal count = 0;
                                foreach ( GraduationPlan.GraduationPlanSubject gplanSubject in GraduationPlan.GraduationPlan.Instance.GetStudentGraduationPlan(var.StudentID).Subjects )
                                {
                                    if ( gplanSubject.Required == "必修" && gplanSubject.RequiredBy == "校訂" )
                                    {
                                        decimal credit = 0;
                                        decimal.TryParse(gplanSubject.Credit, out credit);
                                        count += credit;
                                    }
                                }
                                decimal.TryParse(creditstring.Replace("%", ""), out 校訂必修學分數);
                                校訂必修學分數 = 校訂必修學分數 * count / 100m;
                            }
                            else
                            {
                                decimal.TryParse(creditstring, out 校訂必修學分數);
                            }
                        }
                    }
                    #endregion
                    #endregion
                    #region 學期德行皆及格||學年德行皆及格
                    if ( scoreCalcRule.SelectSingleNode("德行成績畢業判斷規則") != null && scoreCalcRule.SelectSingleNode("德行成績畢業判斷規則").InnerText == "每學期德行成績均及格" )
                    {
                        everySemesterIsGoodDog = true;
                    }
                    #endregion
                }


                XmlDocument doc = new XmlDocument();
                XmlElement gradeCheckElement = doc.CreateElement("GradCheck");
                if ( canCalc )
                {
                    //判斷畢業學分
                    #region 判斷畢業學分
                    decimal get總學分數 = 0;
                    decimal get必修學分數 = 0;
                    decimal get選修學分數 = 0;
                    decimal get部訂必修學分數 = 0;
                    decimal get校訂必修學分數 = 0;
                    decimal get實習學分數 = 0;
                    foreach ( SemesterSubjectScoreInfo subjectScore in var.SemesterSubjectScoreList )
                    {
                        if ( subjectScore.Pass )
                        {
                            if ( useGPlan )
                            {
                                #region 從課程規劃表取得這個科目的相關屬性
                                GraduationPlan.GraduationPlanSubject gPlanSubject = GraduationPlan.GraduationPlan.Instance.GetStudentGraduationPlan(var.StudentID).GetSubjectInfo(subjectScore.Subject, subjectScore.Level);
                                decimal credit = 0;
                                decimal.TryParse(gPlanSubject.Credit, out credit);

                                //不計學分不用算
                                if ( gPlanSubject.NotIncludedInCredit )
                                    continue;

                                get總學分數 += credit;
                                if ( gPlanSubject.Required == "必修" && gPlanSubject.RequiredBy == "校訂" )
                                    get校訂必修學分數 += credit;
                                if ( gPlanSubject.Required == "選修" )
                                    get選修學分數 += credit;
                                if ( gPlanSubject.Entry == "實習科目" )
                                    get實習學分數 += credit;
                                if ( gPlanSubject.Required == "必修" && gPlanSubject.RequiredBy == "部訂" )
                                    get部訂必修學分數 += credit;
                                if ( gPlanSubject.Required == "必修" )
                                    get必修學分數 += credit;
                                #endregion
                            }
                            else
                            {
                                #region 直接使用科目成績上的屬性
                                //不計學分不用算
                                if ( subjectScore.Detail.GetAttribute("不計學分") == "是" )
                                    continue;

                                get總學分數 += subjectScore.Credit;
                                if ( subjectScore.Require && subjectScore.Detail.GetAttribute("修課校部訂") == "校訂" )
                                    get校訂必修學分數 += subjectScore.Credit;
                                if ( !subjectScore.Require )
                                    get選修學分數 += subjectScore.Credit;
                                if ( subjectScore.Detail.GetAttribute("開課分項類別") == "實習科目" )
                                    get實習學分數 += subjectScore.Credit;
                                if ( subjectScore.Require && subjectScore.Detail.GetAttribute("修課校部訂") == "部訂" )
                                    get部訂必修學分數 += subjectScore.Credit;
                                if ( subjectScore.Require )
                                    get必修學分數 += subjectScore.Credit;
                                #endregion
                            }
                        }
                    }
                    if ( get總學分數 < 總學分數 )
                    {
                        XmlElement unPasselement = doc.CreateElement("UnPassReson");
                        unPasselement.InnerText = "總學分數不足";
                        gradeCheckElement.AppendChild(unPasselement);
                    }
                    if ( get必修學分數 < 必修學分數 )
                    {
                        XmlElement unPasselement = doc.CreateElement("UnPassReson");
                        unPasselement.InnerText = "必修學分數不足";
                        gradeCheckElement.AppendChild(unPasselement);
                    }
                    if ( get選修學分數 < 選修學分數 )
                    {
                        XmlElement unPasselement = doc.CreateElement("UnPassReson");
                        unPasselement.InnerText = "選修學分數不足";
                        gradeCheckElement.AppendChild(unPasselement);
                    }
                    if ( get部訂必修學分數 < 部訂必修學分數 )
                    {
                        XmlElement unPasselement = doc.CreateElement("UnPassReson");
                        unPasselement.InnerText = "部訂必修學分數不足";
                        gradeCheckElement.AppendChild(unPasselement);
                    }
                    if ( get校訂必修學分數 < 校訂必修學分數 )
                    {
                        XmlElement unPasselement = doc.CreateElement("UnPassReson");
                        unPasselement.InnerText = "校訂必修學分數不足";
                        gradeCheckElement.AppendChild(unPasselement);
                    }
                    if ( get實習學分數 < 實習學分數 )
                    {
                        XmlElement unPasselement = doc.CreateElement("UnPassReson");
                        unPasselement.InnerText = "實習學分數不足";
                        gradeCheckElement.AppendChild(unPasselement);
                    }
                    #endregion
                    //判斷德行成績及格
                    #region 判斷德行成績及格
                    bool isGoodDog = true;
                    if ( everySemesterIsGoodDog )
                    {
                        foreach ( SemesterEntryScoreInfo entryScore in var.SemesterEntryScoreList )
                        {
                            if ( entryScore.Entry == "德行" && entryScore.Score < 60 )
                                isGoodDog &= false;
                        }
                    }
                    else
                    {
                        foreach ( SchoolYearEntryScoreInfo entryScore in var.SchoolYearEntryScoreList )
                        {
                            if ( entryScore.Entry == "德行" && entryScore.Score < 60 )
                                isGoodDog &= false;
                        }
                    }
                    if ( !isGoodDog )
                    {
                        XmlElement unPasselement = doc.CreateElement("UnPassReson");
                        unPasselement.InnerText = "德行成績不及格";
                        gradeCheckElement.AppendChild(unPasselement);
                    }
                    #endregion
                    //判斷核心科目表
                    #region 判斷核心科目表
                    List<string> checkedList = new List<string>();
                    #region 整理要選用的科目表
                    foreach ( XmlNode st in scoreCalcRule.SelectNodes("核心科目表/科目表") )
                    {
                        checkedList.Add(st.InnerText);
                    }
                    #endregion
                    foreach ( SubjectTableItem subjectTable in SubjectTable.Items["核心科目表"] )
                    {
                        #region 是要選用的科目表就進行判斷
                        if ( checkedList.Contains(subjectTable.Name) && subjectTable.Content.SelectSingleNode("SubjectTableContent") != null )
                        {
                            XmlElement contentElement = (XmlElement)subjectTable.Content.SelectSingleNode("SubjectTableContent");

                            decimal passLimit;
                            decimal.TryParse(contentElement.GetAttribute("CreditCount"), out passLimit);

                            List<string> subjectLevelsInTable = new List<string>();
                            #region 整理在科目表中所有的科目級別(科目+^_^+級別)
                            foreach ( XmlNode snode in contentElement.SelectNodes("Subject") )
                            {
                                string name = ( (XmlElement)snode ).GetAttribute("Name");
                                if ( snode.SelectNodes("Level").Count == 0 )
                                    subjectLevelsInTable.Add(name + "^_^");
                                else
                                {
                                    foreach ( XmlNode lnode in snode.SelectNodes("Level") )
                                    {
                                        subjectLevelsInTable.Add(name + "^_^" + lnode.InnerText);
                                    }
                                }
                            }
                            #endregion

                            decimal credits = 0;
                            foreach ( SemesterSubjectScoreInfo subjectScore in var.SemesterSubjectScoreList )
                            {
                                if ( subjectScore.Pass && subjectLevelsInTable.Contains(subjectScore.Subject + "^_^" + subjectScore.Level) )
                                {
                                    if ( useGPlan )
                                    {
                                        #region 從課程規劃表取得這個科目的相關屬性
                                        GraduationPlan.GraduationPlanSubject gPlanSubject = GraduationPlan.GraduationPlan.Instance.GetStudentGraduationPlan(var.StudentID).GetSubjectInfo(subjectScore.Subject, subjectScore.Level);
                                        //不計學分不用算
                                        if ( gPlanSubject.NotIncludedInCredit )
                                            continue;

                                        decimal credit = 0;
                                        decimal.TryParse(gPlanSubject.Credit, out credit);
                                        credits += credit;
                                        #endregion
                                    }
                                    else
                                    {
                                        #region 直接使用科目成績上的屬性
                                        //不計學分不用算
                                        if ( subjectScore.Detail.GetAttribute("不計學分") == "是" )
                                            continue;
                                        credits += subjectScore.Credit;
                                        #endregion
                                    }
                                }
                            }
                            if ( credits < passLimit )
                            {
                                XmlElement unPasselement = doc.CreateElement("UnPassReson");
                                unPasselement.InnerText = "未達" + subjectTable.Name + "標準";
                                gradeCheckElement.AppendChild(unPasselement);
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                if ( var.Fields.ContainsKey("GradCheck") )
                    var.Fields.Remove("GradCheck");
                var.Fields.Add("GradCheck", gradeCheckElement);
            }
            return _ErrorList;
        }

        public Dictionary<StudentRecord, List<string>> FillStudentFulfilledProgram(AccessHelper accesshelper, List<StudentRecord> students)
        {
            Dictionary<StudentRecord, List<string>> _ErrorList = new Dictionary<StudentRecord, List<string>>();
            //抓成績資料
            accesshelper.StudentHelper.FillSemesterSubjectScore(true, students);
            foreach ( StudentRecord var in students )
            {
                bool canCalc = true;
                //計算資料採用課程規劃(預設採用課程規劃)
                bool useGPlan = true;

                XmlElement scoreCalcRule = ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID) == null ? null : ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID).ScoreCalcRuleElement;
                if ( scoreCalcRule == null )
                {
                    LogError(var, _ErrorList, "沒有設定成績計算規則。");
                    canCalc &= false;
                }
                else
                {
                    DSXmlHelper helper = new DSXmlHelper(scoreCalcRule);
                    #region 學期科目成績屬性採計方式
                    if ( scoreCalcRule.SelectSingleNode("學期科目成績屬性採計方式") != null )
                    {
                        if ( scoreCalcRule.SelectSingleNode("學期科目成績屬性採計方式").InnerText == "以實際學期科目成績內容為準" )
                        {
                            useGPlan = false;
                        }
                    }
                    if ( useGPlan && GraduationPlan.GraduationPlan.Instance.GetStudentGraduationPlan(var.StudentID) == null )
                    {
                        LogError(var, _ErrorList, "沒有設定課程規劃表，當\"學期科目成績屬性採計方式\"設定使用\"以課程規劃表內容為準\"時，學生必須要有課程規劃表以做參考。");
                        canCalc &= false;
                    }
                    #endregion
                }

                XmlDocument doc = new XmlDocument();
                XmlElement fulfilledProgramElement = doc.CreateElement("FulfilledProgram");//可以計算(表示需要的資料都有)
                if ( canCalc )
                {
                    foreach ( SubjectTableItem subjectTable in SubjectTable.Items["學程科目表"] )
                    {
                        XmlElement contentElement = (XmlElement)subjectTable.Content.SelectSingleNode("SubjectTableContent");

                        decimal passLimit;
                        decimal.TryParse(contentElement.GetAttribute("CreditCount"), out passLimit);

                        decimal coreLimit;
                        decimal.TryParse(contentElement.GetAttribute("CoreCount"), out coreLimit);

                        List<string> subjectLevelsInTable = new List<string>();
                        List<string> subjectLevelsInCore = new List<string>();
                        #region 整理在科目表中所有的科目級別(科目+^_^+級別)
                        foreach ( XmlElement snode in contentElement.SelectNodes("Subject") )
                        {
                            string name = ( (XmlElement)snode ).GetAttribute("Name");
                            bool iscore = false;
                            bool.TryParse(snode.GetAttribute("IsCore"), out iscore);

                            if ( snode.SelectNodes("Level").Count == 0 )
                            {
                                subjectLevelsInTable.Add(name + "^_^");
                                if(iscore)
                                    subjectLevelsInCore.Add(name + "^_^");
                            }
                            else
                            {
                                foreach ( XmlNode lnode in snode.SelectNodes("Level") )
                                {
                                    subjectLevelsInTable.Add(name + "^_^" + lnode.InnerText);
                                    if ( iscore )
                                        subjectLevelsInCore.Add(name + "^_^" + lnode.InnerText);
                                }
                            }
                        }
                        #endregion

                        decimal credits = 0;
                        decimal coreCredits = 0;
                        foreach ( SemesterSubjectScoreInfo subjectScore in var.SemesterSubjectScoreList )
                        {
                            //總學分數
                            if ( subjectScore.Pass && subjectLevelsInTable.Contains(subjectScore.Subject + "^_^" + subjectScore.Level) )
                            {
                                if ( useGPlan )
                                {
                                    #region 從課程規劃表取得這個科目的相關屬性
                                    GraduationPlan.GraduationPlanSubject gPlanSubject = GraduationPlan.GraduationPlan.Instance.GetStudentGraduationPlan(var.StudentID).GetSubjectInfo(subjectScore.Subject, subjectScore.Level);
                                    //不計學分不用算
                                    if ( gPlanSubject.NotIncludedInCredit )
                                        continue;

                                    decimal credit = 0;
                                    decimal.TryParse(gPlanSubject.Credit, out credit);
                                    credits += credit;
                                    #endregion
                                }
                                else
                                {
                                    #region 直接使用科目成績上的屬性
                                    //不計學分不用算
                                    if ( subjectScore.Detail.GetAttribute("不計學分") == "是" )
                                        continue;
                                    credits += subjectScore.Credit;
                                    #endregion
                                }
                            }
                            //核心學分數
                            if ( subjectScore.Pass && subjectLevelsInCore.Contains(subjectScore.Subject + "^_^" + subjectScore.Level) )
                            {
                                if ( useGPlan )
                                {
                                    #region 從課程規劃表取得這個科目的相關屬性
                                    GraduationPlan.GraduationPlanSubject gPlanSubject = GraduationPlan.GraduationPlan.Instance.GetStudentGraduationPlan(var.StudentID).GetSubjectInfo(subjectScore.Subject, subjectScore.Level);
                                    //不計學分不用算
                                    if ( gPlanSubject.NotIncludedInCredit )
                                        continue;

                                    decimal credit = 0;
                                    decimal.TryParse(gPlanSubject.Credit, out credit);
                                    coreCredits += credit;
                                    #endregion
                                }
                                else
                                {
                                    #region 直接使用科目成績上的屬性
                                    //不計學分不用算
                                    if ( subjectScore.Detail.GetAttribute("不計學分") == "是" )
                                        continue;
                                    coreCredits += subjectScore.Credit;
                                    #endregion
                                }
                            }
                        }
                        if ( credits >= passLimit && coreCredits>=coreLimit)
                        {
                            XmlElement programElement = doc.CreateElement("Program");
                            programElement.InnerText = subjectTable.Name;
                            fulfilledProgramElement.AppendChild(programElement);
                        }
                    }
                }
                if ( var.Fields.ContainsKey("FulfilledProgram") )
                    var.Fields.Remove("FulfilledProgram");
                var.Fields.Add("FulfilledProgram", fulfilledProgramElement);

            }
            return _ErrorList;
        }

        public void FillSemesterSubjectScoreInfoWithResit(AccessHelper accesshelper, bool filterRepeat, List<StudentRecord> students)
        {
            //抓科目成績
            accesshelper.StudentHelper.FillSemesterSubjectScore(filterRepeat, students);
            foreach (StudentRecord var in students)
            {
                //補考標準<年及,及格標準>
                Dictionary<int, decimal> resitLimit = new Dictionary<int, decimal>();
                //resitLimit.Add(1, 40);
                //resitLimit.Add(2, 40);
                //resitLimit.Add(3, 40);
                //resitLimit.Add(4, 40);

                #region 處理計算規則
                XmlElement scoreCalcRule = ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID) == null ? null : ScoreCalcRule.ScoreCalcRule.Instance.GetStudentScoreCalcRuleInfo(var.StudentID).ScoreCalcRuleElement;
                if (scoreCalcRule == null)
                {
                }
                else
                {
                    DSXmlHelper helper = new DSXmlHelper(scoreCalcRule);
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
                            decimal tryParseDecimal;
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
                }
                #endregion
                foreach (SemesterSubjectScoreInfo score in var.SemesterSubjectScoreList)
                {
                    bool canResit=false;
                    decimal s = 0;
                    decimal limit = 40;
                    if(decimal.TryParse(score.Detail.GetAttribute("原始成績"),out s))
                    {
                        if(resitLimit.ContainsKey(score.GradeYear))limit=resitLimit[score.GradeYear];
                        canResit = (s >= limit);
                    }
                    score.Detail.SetAttribute("達補考標準", canResit ? "是" : "否");
                    score.Detail.SetAttribute("補考標準", limit.ToString());
                }
            }
        }

        private void CleanUpRepeat(Dictionary<SemesterSubjectScoreInfo, string> subjectScoreList)
        {
            #region 刪除重讀成績
            Dictionary<int, Dictionary<int, int>> ApplySemesterSchoolYear = new Dictionary<int, Dictionary<int, int>>();
            //先掃一遍抓出每個年級最高的學年度
            foreach (SemesterSubjectScoreInfo scoreInfo in subjectScoreList.Keys)
            {
                if (!ApplySemesterSchoolYear.ContainsKey(scoreInfo.GradeYear))
                    ApplySemesterSchoolYear.Add(scoreInfo.GradeYear, new Dictionary<int, int>());
                if (!ApplySemesterSchoolYear[scoreInfo.GradeYear].ContainsKey(scoreInfo.Semester))
                    ApplySemesterSchoolYear[scoreInfo.GradeYear].Add(scoreInfo.Semester, scoreInfo.SchoolYear);
                if (scoreInfo.SchoolYear > ApplySemesterSchoolYear[scoreInfo.GradeYear][scoreInfo.Semester])
                    ApplySemesterSchoolYear[scoreInfo.GradeYear][scoreInfo.Semester] = scoreInfo.SchoolYear;
            }
            //如果成績資料的年級學年度不在清單中就移掉
            List<SemesterSubjectScoreInfo> removeList = new List<SemesterSubjectScoreInfo>();
            foreach (SemesterSubjectScoreInfo scoreInfo in subjectScoreList.Keys)
            {
                if (ApplySemesterSchoolYear[scoreInfo.GradeYear][scoreInfo.Semester] != scoreInfo.SchoolYear)
                    removeList.Add(scoreInfo);
            }
            foreach (SemesterSubjectScoreInfo scoreInfo in removeList)
            {
                subjectScoreList.Remove(scoreInfo);
            }
            #endregion
        }
        private void LogError(StudentRecord var, Dictionary<StudentRecord, List<string>> _ErrorList, string p)
        {
            if (!_ErrorList.ContainsKey(var))
                _ErrorList.Add(var, new List<string>());
            _ErrorList[var].Add(p);
        }
    }
}
