using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SmartSchool.StudentRelated.Palmerworm
{
    public class DateTimePickerAdv:DateTimePicker
    {
        private Label _watermark;
        public DateTimePickerAdv()
        {
            this.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ShowCheckBox = true;
            this.Checked = false;
            
            this._watermark = new Label();
            this._watermark.BackColor = this.CalendarMonthBackground;
            this._watermark.Font = this.Font;
            this._watermark.ForeColor = System.Drawing.Color.Gray;
            this._watermark.Location = new System.Drawing.Point(this.Location.X+20,this.Location.Y+1);
            this._watermark.Name = "Watermark";
            this._watermark.Size = new System.Drawing.Size(this.Width - 95, this.Height-7);            
            this._watermark.Text = "";
            this._watermark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;            
            this.Controls.Add(_watermark);
        }
        
        /// <summary>
        /// 浮水印顯示文字
        /// </summary>
        public string WatermarkText
        {
            get { return _watermark.Text; }
            set{_watermark.Text = value;}
        }

        /// <summary>
        /// 取得日期(不含時間)
        /// </summary>
        /// <returns></returns>
        public string GetShortDateString()
        {
            if (Checked)
                return Value.ToShortDateString();
            return "";
        }

        /// <summary>
        /// 以日期格式的文字設定此控制項日期
        /// </summary>
        /// <param name="date">日期格式的文字</param>
        public void SetDateString(string date)
        {            
            DateTime d;                       
            if (DateTime.TryParse(date, out d))
            {
                Value = d;
            }
            else
            {                
                d = DateTime.Today;
                Value = d;
                Checked = false;
                this._watermark.Visible = true;
            }            
        }

        /// <summary>
        /// 清除日期，恢復無日期模式
        /// </summary>
        public void Clear()
        {
            SetDateString("");
        }

        /// <summary>
        /// 覆寫值變更事件，主要是處理若checkbox 沒被選取時,將浮水印顯示出來
        /// </summary>
        /// <param name="eventargs"></param>
        protected override void OnValueChanged(EventArgs eventargs)
        {
            base.OnValueChanged(eventargs);
            this._watermark.Visible = !Checked;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);          
            this._watermark.Location = new System.Drawing.Point(this.Location.X + 20, this.Location.Y + 1);           
            this._watermark.Size = new System.Drawing.Size(this.Width - 95, this.Height - 7);           
        }
    }
}
