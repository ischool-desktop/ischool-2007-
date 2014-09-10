using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SmartSchool.Security
{
    public partial class ConfirmMsgBox : Office2007Form
    {
        public event EventHandler Button1Click;
        public event EventHandler Button2Click;
        public event EventHandler Button3Click;

        private Result _DialogResult = Result.None;
        public Result DialogResult
        {
            get { return _DialogResult; }
        }

        public ConfirmMsgBox(string title, string message, string button1_text, string button2_text, string button3_text)
        {
            InitializeComponent();

            Text = title;
            labelX1.Text = message;
            buttonX1.Text = button1_text;
            buttonX2.Text = button2_text;
            buttonX3.Text = button3_text;
            tableLayoutPanel2.Visible = false;
        }

        public ConfirmMsgBox(string title, string message, string button1_text, string button2_text)
        {
            InitializeComponent();

            Text = title;
            labelX1.Text = message;
            buttonX4.Text = button1_text;
            buttonX5.Text = button2_text;
            tableLayoutPanel1.Visible = false;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (Button1Click != null)
                Button1Click.Invoke(this, new EventArgs());
            _DialogResult = Result.Button1;
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (Button2Click != null)
                Button2Click.Invoke(this, new EventArgs());
            _DialogResult = Result.Button2;
            this.Close();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (Button3Click != null)
                Button3Click.Invoke(this, new EventArgs());
            _DialogResult = Result.Button3;
            this.Close();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (Button1Click != null)
                Button1Click.Invoke(this, new EventArgs());
            _DialogResult = Result.Button1;
            this.Close();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            if (Button2Click != null)
                Button2Click.Invoke(this, new EventArgs());
            _DialogResult = Result.Button2;
            this.Close();
        }

        public enum Result
        {
            Button1, Button2, Button3, None
        }
    }
}