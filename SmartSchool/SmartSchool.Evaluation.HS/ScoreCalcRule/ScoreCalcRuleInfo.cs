using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SmartSchool.StudentRelated;
using IntelliSchool.DSA30.Util;
using SmartSchool.TagManage;

namespace SmartSchool.Evaluation.ScoreCalcRule
{
    public class ScoreCalcRuleInfo
    {
        private readonly string _ID;
        private readonly string _Name;
        private readonly XmlElement _ScoreCalcRuleElement;
        //private readonly bool _DefinedSubjectInfoByGPlan;

        internal ScoreCalcRuleInfo(XmlElement scrElement)
        {
            _ID = scrElement.GetAttribute("ID");
            _Name = scrElement.SelectSingleNode("Name").InnerText;
            _ScoreCalcRuleElement = (XmlElement)scrElement.SelectSingleNode("Content/ScoreCalcRule");
            //_DefinedSubjectInfoByGPlan = false;
            //if (_ScoreCalcRuleElement.SelectSingleNode("�Ǥ��έ׽Ҹ�T�ĭp�覡") == null || ((XmlElement)_ScoreCalcRuleElement.SelectSingleNode("�Ǥ��έ׽Ҹ�T�ĭp�覡")).GetAttribute("�ѽҵ{�W������o") == "True")
            //    _DefinedSubjectInfoByGPlan = true;
        }

        public string ID { get { return _ID; } }
        public string Name { get { return _Name; } }
        //public bool DefinedSubjectInfoByGPlan{get { return _DefinedSubjectInfoByGPlan; }}
        public XmlElement ScoreCalcRuleElement
        {
            get
            {
                return (XmlElement)(new XmlDocument().ImportNode(_ScoreCalcRuleElement, true));
            }
        }

        public XmlElement CalculateSemesterEntryScore(XmlElement semesterSubjectScore)
        {
            Dictionary<string, int> entryCreditCount = new Dictionary<string, int>();
            Dictionary<string, List<decimal>> entrySubjectScores = new Dictionary<string, List<decimal>>();
            Dictionary<string, decimal> entryDividend = new Dictionary<string, decimal>();

            //bool _DefinedSubjectInfoByGPlan = false;

            //if (_ScoreCalcRuleElement.SelectSingleNode("�Ǥ��έ׽Ҹ�T�ĭp�覡")== null ||((XmlElement)_ScoreCalcRuleElement.SelectSingleNode("�Ǥ��έ׽Ҹ�T�ĭp�覡")).GetAttribute("�ѽҵ{�W������o") == "True")
            //    _DefinedSubjectInfoByGPlan = true;

            //if (gPlan == null && _DefinedSubjectInfoByGPlan)
            //    return null;

            #region �N���Z����U�������O��
            foreach (XmlNode subjectNode in semesterSubjectScore.SelectNodes("Subject"))
            {
                XmlElement subjectElement = (XmlElement)subjectNode;
                //���p�Ǥ��Τ��ݵ������κ�
                if (subjectElement.GetAttribute("���ݵ���") == "�O" || subjectElement.GetAttribute("���p�Ǥ�") == "�O")
                    continue;
                #region �������O��Ǥ���
                //string entry = (_DefinedSubjectInfoByGPlan ? gPlan.GetSubjectInfo(subjectElement.GetAttribute("���"), subjectElement.GetAttribute("��دŧO")).Entry : subjectElement.GetAttribute("�}�Ҥ������O"));
                //int credit = 0;
                //int.TryParse((_DefinedSubjectInfoByGPlan ? gPlan.GetSubjectInfo(subjectElement.GetAttribute("���"), subjectElement.GetAttribute("��دŧO")).Credit : subjectElement.GetAttribute("�}�ҾǤ���")), out credit);
                string entry = subjectElement.GetAttribute("�}�Ҥ������O");
                int credit = 0;
                int.TryParse(subjectElement.GetAttribute("�}�ҾǤ���"), out credit);                
                #endregion
                decimal maxScore=0;
                #region ���o�̰�����
                decimal tryParseDecimal;
                if (decimal.TryParse(subjectElement.GetAttribute("��l���Z"), out tryParseDecimal))
                    maxScore = tryParseDecimal;
                if (decimal.TryParse(subjectElement.GetAttribute("�Ǧ~�վ㦨�Z"), out tryParseDecimal) && maxScore < tryParseDecimal)
                    maxScore = tryParseDecimal;
                if (decimal.TryParse(subjectElement.GetAttribute("���u�ĭp���Z"), out tryParseDecimal) && maxScore < tryParseDecimal)
                    maxScore = tryParseDecimal;
                if (decimal.TryParse(subjectElement.GetAttribute("�ɦҦ��Z"), out tryParseDecimal) && maxScore < tryParseDecimal)
                    maxScore = tryParseDecimal;
                if (decimal.TryParse(subjectElement.GetAttribute("���צ��Z"), out tryParseDecimal) && maxScore < tryParseDecimal)
                    maxScore = tryParseDecimal;
                #endregion
                switch (entry)
                {
                    case "��|":
                    case "�꨾�q��":                      
                    case "���d�P�@�z":                       
                    case "��߬��":
                        //�p��������Z
                        if (_ScoreCalcRuleElement.SelectSingleNode("�������Z�p�ⶵ��") == null || ((XmlElement)_ScoreCalcRuleElement.SelectSingleNode("�������Z�p�ⶵ��/" + entry )).GetAttribute("�p�⦨�Z") == "True")
                        {                            
                            //�[�`�Ǥ���
                            if (!entryCreditCount.ContainsKey(entry))
                                entryCreditCount.Add(entry, credit);
                            else
                                entryCreditCount[entry] += credit;
                            //�[�J�N���Z��Ƥ���
                            if (!entrySubjectScores.ContainsKey(entry)) entrySubjectScores.Add(entry, new List<decimal>());
                            entrySubjectScores[entry].Add(maxScore);
                            //�[�v�`�p
                            if (!entryDividend.ContainsKey(entry))
                                entryDividend.Add(entry, maxScore * credit);
                            else
                                entryDividend[entry] += (maxScore * credit);
                        }
                        //�N��ئ��Z�P�Ƿ~���Z�@�֭p��
                        if (_ScoreCalcRuleElement.SelectSingleNode("�������Z�p�ⶵ��") != null && ((XmlElement)_ScoreCalcRuleElement.SelectSingleNode("�������Z�p�ⶵ��/" + entry)).GetAttribute("�֤J�Ǵ��Ƿ~���Z") == "True")
                        {
                            //�[�`�Ǥ���
                            if (!entryCreditCount.ContainsKey("�Ƿ~"))
                                entryCreditCount.Add("�Ƿ~", credit);
                            else
                                entryCreditCount["�Ƿ~"] += credit;
                            //�[�J�N���Z��Ƥ���
                            if (!entrySubjectScores.ContainsKey("�Ƿ~")) entrySubjectScores.Add("�Ƿ~", new List<decimal>());
                            entrySubjectScores["�Ƿ~"].Add(maxScore);
                            //�[�v�`�p
                            if (!entryDividend.ContainsKey("�Ƿ~"))
                                entryDividend.Add("�Ƿ~", maxScore * credit);
                            else
                                entryDividend["�Ƿ~"] += (maxScore*credit);
                        }
                        break;

                    case "�Ƿ~":
                    default:
                        //�[�`�Ǥ���
                        if (!entryCreditCount.ContainsKey("�Ƿ~"))
                            entryCreditCount.Add("�Ƿ~", credit);
                        else
                            entryCreditCount["�Ƿ~"] += credit;
                        //�[�J�N���Z��Ƥ���
                        if (!entrySubjectScores.ContainsKey("�Ƿ~")) entrySubjectScores.Add("�Ƿ~", new List<decimal>());
                        entrySubjectScores["�Ƿ~"].Add(maxScore);
                        //�[�v�`�p
                        if (!entryDividend.ContainsKey("�Ƿ~"))
                            entryDividend.Add("�Ƿ~", maxScore * credit);
                        else
                            entryDividend["�Ƿ~"] += (maxScore * credit);
                        break;
                }
            } 
            #endregion

            XmlDocument doc = new XmlDocument();
            XmlElement entryScoreRoot = doc.CreateElement("SemesterEntryScore");
            #region �B�z�p��U�������O�����Z
            foreach (string entry in entryCreditCount.Keys)
            {
                decimal entryScore=0;
                #region �p��entryScore
                if (entryCreditCount[entry] == 0)
                {
                    //�Ǥ��[�`��0�A�����[�v����������               
                    foreach (decimal score in entrySubjectScores[entry])
                    {
                        entryScore += score;
                    }
                    entryScore = (entryScore / entrySubjectScores[entry].Count);
                }
                else
                {
                    //�Υ[�v�`�����Ǥ���
                    entryScore = (entryDividend[entry] / entryCreditCount[entry]);
                } 
                #endregion
                #region ��Ǧ�ƳB�z
                XmlElement element = (XmlElement)_ScoreCalcRuleElement.SelectSingleNode("�U�����Z�p����/�Ǵ��������Z�p����");
                if (element!= null)
                {
                    int decimals;
                    SmartSchool.Evaluation.WearyDogComputer.RoundMode mode;
                    bool tryParseBool;
                    if (!int.TryParse(element.GetAttribute("���"), out decimals))
                        decimals = 2;
                    if(bool.TryParse(element.GetAttribute("�|�ˤ��J"),out tryParseBool)&&tryParseBool)
                        mode= SmartSchool.Evaluation.WearyDogComputer.RoundMode.�|�ˤ��J;
                    else if (bool.TryParse(element.GetAttribute("�L����i��"), out tryParseBool) && tryParseBool)
                        mode = SmartSchool.Evaluation.WearyDogComputer.RoundMode.�L����i��;
                    else if (bool.TryParse(element.GetAttribute("�L����˥h"), out tryParseBool) && tryParseBool)
                        mode = SmartSchool.Evaluation.WearyDogComputer.RoundMode.�L����˥h;                    
                    else
                        mode = SmartSchool.Evaluation.WearyDogComputer.RoundMode.�|�ˤ��J;
                    entryScore = GetRoundScore(entryScore, decimals, mode);
                }
                else
                    entryScore = GetRoundScore(entryScore,2, SmartSchool.Evaluation.WearyDogComputer.RoundMode.�|�ˤ��J);


                #endregion
                #region ��JXml
                XmlElement entryElement = doc.CreateElement("Entry");
                entryElement.SetAttribute("����", entry);
                entryElement.SetAttribute("���Z", entryScore.ToString());
                entryScoreRoot.AppendChild(entryElement);
                #endregion
            }
            #endregion
            return entryScoreRoot;
        }

        public decimal GetStudentPassScore(BriefStudentData student, int gradeYear)
        {
            string childElement;
            switch ( gradeYear )
            {
                case 1: childElement = "�@�~�Ťή�з�"; break;
                case 2: childElement = "�G�~�Ťή�з�"; break;
                case 3: childElement = "�T�~�Ťή�з�"; break;
                case 4: childElement = "�|�~�Ťή�з�"; break;
                default: childElement = "�W�L�F��"; break;
            }
            decimal passScore = decimal.MaxValue, tryPraseScore;
            DSXmlHelper helper = new DSXmlHelper(_ScoreCalcRuleElement);
            foreach ( XmlElement element in helper.GetElements("�ή�з�/�ǥ����O") )
            {
                string tagName = element.GetAttribute("���O");
                if ( tagName == "�w�]" && decimal.TryParse(element.GetAttribute(childElement), out tryPraseScore) )
                {
                    if ( tryPraseScore < passScore )
                        passScore = tryPraseScore;
                }
                else
                {
                    foreach ( TagInfo tag in student.Tags )
                    {
                        if ( ( ( tag.Prefix == "" && tagName == tag.Name ) || tagName == tag.FullName ) && decimal.TryParse(element.GetAttribute(childElement), out tryPraseScore) )
                        {
                            if ( tryPraseScore < passScore )
                                passScore = tryPraseScore;
                            break;
                        }
                    }
                }
            }
            if ( passScore == decimal.MaxValue )
                passScore = 60;
            return passScore;
        }

        private enum RoundMode { �|�ˤ��J, �L����i��, �L����˥h }
        private decimal GetRoundScore(decimal entryScore, int decimals, SmartSchool.Evaluation.WearyDogComputer.RoundMode mode)
        {
            return WearyDogComputer.GetRoundScore(entryScore,decimals,mode);
        }
    }
}
