using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.PlugIn.ImportExport
{
    /// <summary>
    /// 分批次匯入資料的單一封包資訊
    /// </summary>
    public class DataImportEventArgs : EventArgs
    {
        private List<string> _ImportFields = new List<string>();
        private List<RowData> _Items = new List<RowData>();
        /// <summary>
        /// 要匯入的資料
        /// </summary>
        public List<RowData> Items { get { return _Items; } }
        /// <summary>
        /// 被選取要匯入的欄位
        /// </summary>
        public List<string> ImportFields { get { return _ImportFields; } }
    }
}
