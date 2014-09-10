using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.Output
{
    public interface IOutput<T>
    {
        void SetSource(ExportTable dataSource);
        T GetOutput();
        void Save(string fileName);        
    }
}
