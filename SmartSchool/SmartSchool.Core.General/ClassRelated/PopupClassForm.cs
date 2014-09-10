using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SmartSchool.ClassRelated
{
    public partial class PopupClassForm : SmartSchool.Common.BaseForm
    {
        public event EventHandler<ClassFormClosingEventArgs> ClassFormClosing;

        private string _classID;        
        public string ClassID
        {
            get { return _classID; }
            set { _classID = value; }
        }

        private ClassInfoPanel _panel;
        public PopupClassForm(string classid)
        {
            _classID = classid;
            InitializeComponent();
            _panel = new ClassInfoPanel();
            _panel.Dock = DockStyle.Fill;
            _panel.Initialize(classid);
            this.Controls.Add(_panel);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (ClassFormClosing != null)
                ClassFormClosing.Invoke(this, new ClassFormClosingEventArgs(_classID));
        }

        public void Reload()
        {
            _panel.Initialize(_classID);
        }
    }

    public class ClassFormClosingEventArgs : EventArgs
    {
        public ClassFormClosingEventArgs(string classid)
        {
            _classID = classid;
        }
        private string _classID;
        public string ClassID
        {
            get { return _classID; }
            set { _classID = value; }
        }
    }
}