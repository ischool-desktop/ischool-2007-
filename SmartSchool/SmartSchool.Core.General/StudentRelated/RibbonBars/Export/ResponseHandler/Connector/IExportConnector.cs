using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler;

namespace SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.Connector
{
    public interface IExportConnector
    {      
        void SetSelectedFields(FieldCollection exportFields);
        void AddCondition(string argument);        
        ExportTable Export();
    }
}
