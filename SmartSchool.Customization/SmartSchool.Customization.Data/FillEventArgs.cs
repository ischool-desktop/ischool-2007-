using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    /// <summary>
    /// Fill資料相關資訊
    /// </summary>
    public class FillEventArgs<T> : EventArgs
    {
        private AccessHelper _AccessHelper;
        private System.Collections.Generic.IEnumerable<T> _List;
        /// <summary>
        /// 初始
        /// </summary>
        internal FillEventArgs(AccessHelper accessHelper, IEnumerable<T> list)
        {
            _AccessHelper = accessHelper;
            _List = list;
        }
        /// <summary>
        /// 觸發事件的AccessHelper實體
        /// </summary>
        public AccessHelper AccessHelper { get { return _AccessHelper; } }
        /// <summary>
        /// 填入資料的對象
        /// </summary>
        public IEnumerable<T> List { get { return _List; } }
    }
}
