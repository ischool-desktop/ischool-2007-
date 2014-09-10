using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.API.PlugIn.Export
{

    public class ExportPackageEventArgs : EventArgs
    {
        private List<string> _ExportFields = new List<string>();
        private List<string> _List = new List<string>();
        private List<RowData> _Items = new List<RowData>();
        /// <summary>
        /// 要匯出資料的編號
        /// </summary>
        public List<string> List { get { return _List; } }
        /// <summary>
        /// 被選取要匯出的欄位
        /// </summary>
        public List<string> ExportFields { get { return _ExportFields; } }
        /// <summary>
        /// 與這些編號相關的匯出資料
        /// </summary>
        public List<RowData> Items { get { return _Items; } }

    }
}
