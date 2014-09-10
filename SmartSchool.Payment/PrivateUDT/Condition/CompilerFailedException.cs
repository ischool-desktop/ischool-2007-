using System;
using System.Collections.Generic;
using System.Text;

namespace Private.UDT.Condition
{
    /// <summary>
    /// 例外狀況，編譯時期發生錯誤
    /// </summary>
    public class CompilerFailedException:Exception
    {
        internal CompilerFailedException(string query,string errorcode)
        {
            Query = query;
            ErrorCode = errorcode;
            Index = 0;
            Text = "";
            Reason = "";
        }
        internal CompilerFailedException(string query, string errorcode, string reason)
        {
            Query = query;
            ErrorCode = errorcode;
            Index = 0;
            Text = "";
            Reason = reason;
        }
        internal CompilerFailedException(string query, string errorcode, Symbol symbol)
        {
            Query = query;
            ErrorCode = errorcode;
            Index = symbol.Index;
            Text = symbol.Value;
            Reason="";
        }
        internal CompilerFailedException(string query, string errorcode, Symbol symbol, string reason)
        {
            Query = query;
            ErrorCode = errorcode;
            Index = symbol.Index;
            Text = symbol.Value;
            Reason = reason;
        }
        /// <summary>
        /// 取得，條件式
        /// </summary>
        public string Query { get; private set; }
        /// <summary>
        /// 取得，發生錯誤的原因
        /// </summary>
        public string Reason { get; private set; }
        /// <summary>
        /// 取得，發生錯誤的位置
        /// </summary>
        public int Index { get; private set; }
        /// <summary>
        /// 取得，產生錯誤的文字片段
        /// </summary>
        public string Text { get; private set; }
        /// <summary>
        /// 取得錯誤訊息
        /// </summary>
        public override string Message
        {
            get
            {
                return Query+"\n於第" + Index + "字元 " + Text + " 處發生錯誤。" + Reason;
            }
        }
        /// <summary>
        /// 取得錯誤代碼
        /// </summary>
        public string ErrorCode { get; private set; }
    }
}
