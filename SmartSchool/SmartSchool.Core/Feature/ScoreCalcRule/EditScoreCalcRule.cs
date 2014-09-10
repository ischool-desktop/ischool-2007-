using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;
using System.Xml;

namespace SmartSchool.Feature.ScoreCalcRule
{
    [QueryRequest()]
    public class EditScoreCalcRule
    {
        public static void Update(string id, string name, XmlElement scrElement)
        {
            DSRequest dsreq = new DSRequest("<UpdateRequest><ScoreCalcRule><Field><Name>"+name+"</Name><Content>"+scrElement.OuterXml+"</Content></Field><Condition><ID>"+id+"</ID></Condition></ScoreCalcRule></UpdateRequest>");
            CurrentUser.Instance.CallService("SmartSchool.ScoreCalcRule.UpdateScoreCalcRule", dsreq);
        }
        public static void SetMoralConductCalcRule(XmlElement moralConductCalcRule)
        {
            DSRequest dsreq = new DSRequest("<SetMoralConductScoreCalcRuleRequest><MoralConductScoreCalcRule><Content>" + moralConductCalcRule .OuterXml+ "</Content></MoralConductScoreCalcRule></SetMoralConductScoreCalcRuleRequest>");
            CurrentUser.Instance.CallService("SmartSchool.ScoreCalcRule.SetMoralConductScoreCalcRule", dsreq);
        }
    }
}
