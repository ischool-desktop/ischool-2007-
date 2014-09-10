using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.Placing.Rule;

namespace SmartSchool.StudentRelated.Placing.Export
{
    public class ExcelSheetInfo
    {
        private string _title;

        public string Title
        {
            get { return _title; }        
        }

        private IList<PlacingInfo> _placingList;

        public IList<PlacingInfo> PlacingList
        {
            get { return _placingList; }          
        }

        private SheetType _sheetType;

        public SheetType SheetType
        {
            get { return _sheetType; }            
        }

        public ExcelSheetInfo(string title, IList<PlacingInfo> placingList, SheetType type)
        {
            _title = title;
            _placingList = placingList;
            _sheetType = type;
        }

        
    }

    public enum SheetType
    {
        Nomal
    }
}
