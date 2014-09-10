using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SmartSchool.StudentRelated.SmallCard
{
    internal partial class UCStudentCard : DevComponents.DotNetBar.RibbonBar//UserControl
    {
        protected override void OnClientSizeChanged(EventArgs e)
        {
            //base.OnClientSizeChanged(e);
        }
        protected override void OnPaddingChanged(EventArgs e)
        {
            //base.OnPaddingChanged(e);
        }
        protected override void OnParentChanged(EventArgs e)
        {
            //base.OnParentChanged(e);
        }
        protected override void OnMarginChanged(EventArgs e)
        {
            //base.OnMarginChanged(e);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            //base.OnSizeChanged(e);
        }
        public UCStudentCard()
        {
            InitializeComponent();
            lblSeatNo.Text = lblName.Text = lblClass.Text = this.Text = "";
        }
        public string StudentName
        {
            get { return lblName.Text; }
            set 
            {
                lblName.Text = value; 
            }
        }
        public string StudentID
        {
            get { return this.Text; }
            set 
            { 
                this.Text = value; 
            }
        }
        public string StudentClass
        {
            get { return lblClass.Text.TrimEnd("¯Z ".ToCharArray()); }
            set 
            { 
                lblClass.Text = value+"¯Z "; 
            }
        }
        public string StudentSeatNo
        {
            get { return lblSeatNo.Text.TrimEnd("¸¹".ToCharArray()); }
            set
            {
                lblSeatNo.Text = value + "¸¹";
            }
        }
    }
}
