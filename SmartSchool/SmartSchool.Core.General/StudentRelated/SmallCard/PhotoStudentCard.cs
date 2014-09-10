using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SmartSchool.StudentRelated.SmallCard
{
    public partial class PhotoStudentCard : ButtonX
    {
        private static System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PhotoStudentCard));
        private string _Class;
        private string _SeatNo;
        private string _StudentID;
        private string _StudentName;
        private Image _Photo;
        private BriefStudentData _Student;
        private string _ColorNumber;

        private void btn_Click(object sender, EventArgs e)
        {
            //StudentRelated.Student.Instance.UnSelectStudent(this._Student);
        }

        public PhotoStudentCard()
        {
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Image = ((System.Drawing.Image)(resources.GetObject("buttonX1.Image")));
            this.ImageFixedSize = new System.Drawing.Size(78, 90);
            this.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "buttonX1";
            this.Size = new System.Drawing.Size(100, 155); 
            this.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubItemsExpandWidth = 0;
            this.TabIndex = 0;
            this.Text = "１２３４<br/>\r\n２２３４<br/>\r\n<br/>";
            this.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.MouseEnter += new EventHandler(PhotoStudentCard_MouseEnter);
            this.MouseLeave += new EventHandler(PhotoStudentCard_MouseLeave);
            this.MouseClick += new MouseEventHandler(PhotoStudentCard_MouseClick);
        }

        void PhotoStudentCard_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Location.X < 98 && e.Location.X >= 84 && e.Location.Y >= 2 && e.Location.Y < 16)
            {
                //StudentRelated.Student.Instance.UnSelectStudent(_Student);
            }
            else
            {
                //MsgBox.Show("X:"+this.Get+" Y:"+this.Top);
            }
        }

        void PhotoStudentCard_MouseLeave(object sender, EventArgs e)
        {
            b = false;
        }
        bool b = false;

        void PhotoStudentCard_MouseEnter(object sender, EventArgs e)
        {
            b = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (b)
            {
                e.Graphics.DrawImage(((System.Drawing.Image)(resources.GetObject("ico_close1"))), 84F, 2F, 14F, 14F);
            }
        }

        public string Class
        {
            get { return _Class; }
            set { _Class = value; }
        }
        public string SeatNo
        {
            get { return _SeatNo; }
            set { _SeatNo = value;  }
        }
        public string StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value;  }
        }
        public string StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }
        public Image Photo
        {
            get { return _Photo; }
            set
            {
                _Photo = value;
                if (value == null)
                    Image = ((System.Drawing.Image)(resources.GetObject("buttonX1.Image")));
                else
                    Image = value;
            }
        }
        public BriefStudentData Student
        {
            get { return _Student; }
            set { _Student = value; }
        }
        public string ColorNumber
        {
            get { return _ColorNumber; }
            set { _ColorNumber = value; }
        }

        public void SetText()
        {
            StringBuilder sb = new StringBuilder();
            if (_ColorNumber != "")
            {
                sb.Append("<font color='");
                sb.Append(_ColorNumber);
                sb.Append("'>　");
                sb.Append(_Class);
                sb.Append("　");
                sb.Append(_SeatNo);
                sb.Append("<br/>　");
                sb.Append(_StudentID);
                sb.Append("<br/>　");
                sb.Append(_StudentName);
                sb.Append("</font>");
            }
            else
            {
                sb.Append("　");
                sb.Append(_Class);
                sb.Append("　");
                sb.Append(_SeatNo);
                sb.Append("<br/>　");
                sb.Append(_StudentID);
                sb.Append("<br/>　");
                sb.Append(_StudentName);
                Text = "　" + _Class + "　" + _SeatNo + "<br/>　" + _StudentID + "<br/>　" + _StudentName;
            }

            Text = sb.ToString();
        }
    }
}
