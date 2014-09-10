using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler;
using System.Xml;

namespace SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.Formater
{
    public interface IResponseFormater
    {
        ExportFieldCollection Format(XmlElement element);
    }
}
