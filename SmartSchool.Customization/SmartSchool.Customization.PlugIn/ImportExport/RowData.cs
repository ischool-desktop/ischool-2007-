using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.PlugIn.ImportExport
{
    /// <summary>
    /// 要匯入的一筆資料
    /// </summary>
    public class RowData : Dictionary<string, string>
    {
        private string _ID="";

        /// <summary>
        /// 編號
        /// </summary>
        public string ID { get { return _ID; } set { _ID = value; } }
    }
}
