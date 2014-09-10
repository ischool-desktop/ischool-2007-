using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.RibbonBars.Import.SheetModel;

namespace SmartSchool.StudentRelated.RibbonBars.Import.ValidateModel
{
    public class ValidateColumn
    {
        private string _name;
        private byte _index;
        private SheetColumn _column;

        public ValidateColumn(SheetColumn bindColumn, byte index)
        {
            _name = bindColumn.Name;
            _index = index;
            _column = bindColumn;
        }

        public string Name
        {
            get { return _name; }
        }

        public byte Index
        {
            get { return _index; }
        }
        public SheetColumn BindingSheetColumn
        {
            get { return _column; }
        }
    }
}
