using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler.Generator.Condition;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler.Generator.Orders;

namespace SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler.Generator
{
    public interface IRequestGenerator
    {
        void AddCondition(ICondition condition);
        void AddOrder(Order order);
        void SetSelectedFields(FieldCollection selectedFields);
        DSRequest Generate();
    }
}
