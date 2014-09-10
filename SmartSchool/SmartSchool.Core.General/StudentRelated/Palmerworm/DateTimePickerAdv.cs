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
        /// �B���L��ܤ�r
        /// </summary>
        public string WatermarkText
        {
            get { return _watermark.Text; }
            set{_watermark.Text = value;}
        }

        /// <summary>
        /// ���o���(���t�ɶ�)
        /// </summary>
        /// <returns></returns>
        public string GetShortDateString()
        {
            if (Checked)
                return Value.ToShortDateString();
            return "";
        }

        /// <summary>
        /// �H����榡����r�]�w��������
        /// </summary>
        /// <param name="date">����榡����r</param>
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
        /// �M������A��_�L����Ҧ�
        /// </summary>
        public void Clear()
        {
            SetDateString("");
        }

        /// <summary>
        /// �мg���ܧ�ƥ�A�D�n�O�B�z�Ycheckbox �S�Q�����,�N�B���L��ܥX��
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
