using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.GovernmentalDocument.NameList
{
    public interface INameListProvider
    {
        string Title { get;}
        List<XmlElement> GetExpectantList();
        XmlElement CreateNameList(string schoolYear,string semester,List<XmlElement> list);
    }
}
