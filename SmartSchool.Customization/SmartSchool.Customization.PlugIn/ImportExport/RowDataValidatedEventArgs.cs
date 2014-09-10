using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.PlugIn.ImportExport
{
    /// <summary>
    /// 驗證資料
    /// </summary>
    public class RowDataValidatedEventArgs : EventArgs
    {
        private RowData _Data;
        private List<string> _SelectedFields = new List<string>();
        private Dictionary<string, string> _WarningFields = new Dictionary<string, string>();
        private Dictionary<string, string> _ErrorFields = new Dictionary<string, string>();
        private string _ErrorMessage="";
        /// <summary>
        /// 驗證中的資料
        /// </summary>
        public RowData Data { get { return _Data; } set { _Data = value; } }
        /// <summary>
        /// 被選取要匯入的欄位
        /// </summary>
        public List<string> SelectFields { get { return _SelectedFields; } }
        /// <summary>
        /// 各欄位警示資訊
        /// </summary>
        public Dictionary<string, string> WarningFields { get { return _WarningFields; } }
        /// <summary>
        /// 各欄位錯誤資訊
        /// </summary>
        public Dictionary<string, string> ErrorFields { get { return _ErrorFields; } }
        /// <summary>
        /// 整筆資料錯誤資訊
        /// </summary>
        public string ErrorMessage { get { return _ErrorMessage; } set { _ErrorMessage = value; } }
    }
}
