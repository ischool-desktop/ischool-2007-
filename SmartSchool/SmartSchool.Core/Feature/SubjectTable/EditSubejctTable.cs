using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;

namespace SmartSchool.Feature.SubjectTable
{
    [QueryRequest()]
    public class EditSubejctTable
    {
        public static void UpdateSubject(string id, XmlElement content)
        {
            DSRequest dsreq = new DSRequest("<Request><SubjectTable><Field><Content>" + content.OuterXml + "</Content></Field><Condition><ID>" + id + "</ID></Condition></SubjectTable></Request>");
            CurrentUser.Instance.CallService("SmartSchool.SubjectTable.Update", dsreq);
        }
    }
}
