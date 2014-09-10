using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.StudentRelated.RibbonBars.Import.ValidateModel
{
    public interface IMessageOutput
    {
        void Output(RowMessage message);
    }
}
