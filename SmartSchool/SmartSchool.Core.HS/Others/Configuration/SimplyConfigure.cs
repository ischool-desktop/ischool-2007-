using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SmartSchool.Others.Configuration
{
    class SimplyConfigure:SmartSchool.Customization.PlugIn.Configure.IConfigurationItem
    {
        private string _Caption = "";

        private string _Category = "";

        private Panel _ContentPanel = new DevComponents.DotNetBar.PanelEx();

        private Image _Image = null;

        private string _TabGroup = "";

        public SimplyConfigure()
        {
            _ContentPanel.VisibleChanged += new EventHandler(_ContentPanel_VisibleChanged);
        }
        private bool _LastVisible = false;
        private void _ContentPanel_VisibleChanged(object sender, EventArgs e)
        {
            if ( _ContentPanel.Visible!=_LastVisible &&_ContentPanel.Visible && OnShown != null )
                OnShown.Invoke(this, new EventArgs());
            _LastVisible = _ContentPanel.Visible;
        }

        public event EventHandler OnShown;

        #region IConfigurationItem 成員

        public void Active()
        {
        }

        public string Caption
        {
            get { return _Caption; }
            set { _Caption = value; }
        }

        public string Category
        {
            get { return _Category; }
            set { _Category = value; }
        }

        public System.Windows.Forms.Panel ContentPanel
        {
            get { return _ContentPanel; }
            set
            {
                _ContentPanel.VisibleChanged -= new EventHandler(_ContentPanel_VisibleChanged);
                _ContentPanel = value;
                _ContentPanel.VisibleChanged += new EventHandler(_ContentPanel_VisibleChanged);
            }
        }

        public System.Windows.Forms.Panel ControlPanel
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public bool HasControlPanel
        {
            get { return false; }
        }

        public System.Drawing.Image Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        public string TabGroup
        {
            get { return _TabGroup; }
            set { _TabGroup = value; }
        }

        #endregion
    }
}
