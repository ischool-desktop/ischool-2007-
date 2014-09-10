using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SmartSchool.Customization.PlugIn.Configure
{
    /// <summary>
    /// 系統設定介面
    /// </summary>
    public partial class ConfigurationItem : UserControl,IConfigurationItem
    {
        private string _TabGroup = "";
        private bool _HasControlPanel;
        private string _Caption="";
        private string _Category="";
        private Image _Image=null;

        public ConfigurationItem()
        {
            InitializeComponent();
        }

        #region IConfigurationItem 成員
        public string TabGroup
        {
            get { return _TabGroup; }
            set { _TabGroup = value; }
        }
        /// <summary>
        /// 擁有ControlPanel
        /// </summary>
        public bool HasControlPanel
        {
            get { return _HasControlPanel; }
            set { _HasControlPanel = value; }
        }
        
        public Panel ControlPanel
        {
            get { return controlPanel; }
        }

        public Panel ContentPanel
        {
            get { return contentPanel; }
        }
        /// <summary>
        /// 項目名稱
        /// </summary>
        public string Caption
        {
            get { return _Caption; }
            set { _Caption = value; }
        }
        /// <summary>
        /// 項目類別
        /// </summary>
        public string Category
        {
            get { return _Category; }
            set { _Category = value; }
        }
        /// <summary>
        /// 照片
        /// </summary>
        public Image Image
        {
            get { return _Image; }
            set { _Image = value; }
        }
                
        public virtual void Active()
        {
            OnActive();
        }

        #endregion
        /// <summary>
        /// 當此項目第一次被開啟時
        /// </summary>
        protected virtual void OnActive()
        {
        
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.splitter1.Visible = this.controlPanel.Visible = _HasControlPanel;
            this.captionLabel.Text = _Category + (_Category == "" ? "" : "/") + _Caption;
            base.OnPaint(e);
        }
    }
}
