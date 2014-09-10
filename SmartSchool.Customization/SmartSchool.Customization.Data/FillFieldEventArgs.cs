using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    /// <summary>
    /// FillField事件相關資訊
    /// </summary>
    public class FillFieldEventArgs<T> : FillEventArgs<T>
    {
        private string _FieldName;
        /// <summary>
        /// 初始
        /// </summary>
        internal FillFieldEventArgs(AccessHelper accessHelper, string fieldName, IEnumerable<T> list)
            : base(accessHelper, list)
        {
            _FieldName = fieldName;
        }
        /// <summary>
        /// 填入的欄位名稱
        /// </summary>
        public string FieldName { get { return _FieldName; } }
    }
}
