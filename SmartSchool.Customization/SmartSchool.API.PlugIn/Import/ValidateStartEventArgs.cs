using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.API.PlugIn.Import
{
    /// <summary>
    /// 開始驗證
    /// </summary>
    public class ValidateStartEventArgs: EventArgs
    {
        private string[] _IDList = new string[0];
        /// <summary>
        /// 所有相關資料的系統編號
        /// </summary>
        public string[] List
        {
            get { return _IDList; }
            set { _IDList = value; }
        }
    }
}
