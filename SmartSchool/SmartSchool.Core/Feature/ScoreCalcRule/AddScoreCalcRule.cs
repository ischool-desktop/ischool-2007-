using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;
using System.Xml;

namespace SmartSchool.Feature.ScoreCalcRule
{
    public class AddScoreCalcRule
    {
        public static void Insert(string name)
        {
            DSRequest dsreq = new DSRequest("<InsertRequest><ScoreCalcRule><Name>" + name + "</Name><Content><ScoreCalcRule/></Content></ScoreCalcRule></InsertRequest>");
            CurrentUser.Instance.CallService("SmartSchool.ScoreCalcRule.InsertScoreCalcRule", dsreq);
        }
        public static void Insert(string name, XmlElement scrElement)
        {
            DSRequest dsreq = new DSRequest("<InsertRequest><ScoreCalcRule><Name>"+name+"</Name><Content>"+scrElement.OuterXml+"</Content></ScoreCalcRule></InsertRequest>");
            CurrentUser.Instance.CallService("SmartSchool.ScoreCalcRule.InsertScoreCalcRule", dsreq);
        }
    }
}
