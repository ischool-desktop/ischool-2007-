using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar.Controls;

namespace SmartSchool.Common
{
    /// <summary>
    /// 輸入日期格式的TextBox
    /// </summary>
    public class DateTimeTextBox : TextBoxX
    {
        private DateTime _date;
        private string _dateString;
        /// <summary>
        /// 取回符合日期格式之標準日期文字
        /// </summary>
        public string DateString
        {
            get { return _dateString; }
        }

        private bool _allowNull;
        /// <summary>
        /// 是否允許空白
        /// </summary>
        public bool AllowNull
        {
            get { return _allowNull; }
            set { _allowNull = value; }
        }

        private string _emptyString;
        /// <summary>
        /// 當內容不符日期格式時的傳回值
        /// </summary>
        public string EmptyString
        {
            get { return _emptyString; }
            set { _emptyString = value; }
        }

        /// <summary>
        /// 建構子
        /// </summary>
        public DateTimeTextBox()
            : base()
        {
            _allowNull = true;
            _emptyString = "";
            WatermarkText = "請輸入西元日期";
        }

        protected override void OnTextChanged(EventArgs e)
        {
            DateTime date;
            if ( DateTime.TryParse(this.Text, out date) )
            {
                _dateString = date.ToShortDateString();
                _date = date;
            }
            else
            {
                _dateString = _emptyString;
                _date = DateTime.Today;
            }
            base.OnTextChanged(e);
        }

        /// <summary>
        /// 判斷是否合法
        /// </summary>
        public bool IsValid
        {
            get
            {
                if ( !_allowNull )
                {
                    DateTime date;
                    bool b = DateTime.TryParse(this.Text, out date);
                    if ( b )
                        _date = date;
                    return b;
                }
                else
                {
                    DateTime date;
                    if ( string.IsNullOrEmpty(Text.Trim()) )
                        return true;
                    else if ( DateTime.TryParse(this.Text, out date) )
                    {
                        _date = date;
                        return true;
                    }
                    else
                        return false;
                }
            }
        }

        protected override void OnValidated(EventArgs e)
        {
            base.OnValidated(e);

            if ( IsValid )
            {
                if ( !string.IsNullOrEmpty(Text.ToString()) )
                {
                    string startYear = GetStartText(Text);
                    int sy;
                    if ( int.TryParse(startYear, out sy) )
                    {
                        if ( sy < 1000 && _date.Year.ToString().EndsWith(startYear) )
                        {
                            sy += 1911;
                            _date = new DateTime(sy, _date.Month, _date.Day);
                            _dateString = _date.ToShortDateString();
                        }
                    }
                    Text = _date.ToShortDateString();
                }
            }
        }

        private string GetStartText(string Text)
        {
            string y = string.Empty;
            foreach ( char s in Text )
            {
                int i;
                if ( int.TryParse(s.ToString(), out i) )
                    y += s;
                else
                    return y;
            }
            return y;
        }

        /// <summary>
        /// 設定初始值
        /// </summary>
        /// <param name="value">初始值</param>
        public void SetDate(string value)
        {
            Text = GetDateString(value);
        }

        /// <summary>
        /// 取得轉換後的日期字串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetDateString(string value)
        {
            DateTime date;
            if ( DateTime.TryParse(value, out date) )
                return date.ToShortDateString();
            return EmptyString;
        }
        /// <summary>
        /// 取得輸入的日期
        /// </summary>
        /// <returns></returns>
        public DateTime GetDate()
        {
            DateTime date;
            if ( DateTime.TryParse(Text, out date) )
                return date;
            return DateTime.Now;
        }
    }
}
