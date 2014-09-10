using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Xml;
using SmartSchool.Payment.AccountStatedService.Interfaces;

namespace SmartSchool.Payment.AccountStatedService
{
    public partial class ASServiceConfigForm : Form
    {
        public ASServiceConfigForm(IAccountStatedService asservice)
        {
            InitializeComponent();

            _asservice = asservice;
            _asservice_config = new ASServiceConfig();
            ASServiceConfig.ConfigID = Guid.NewGuid().ToString();
        }

        public ASServiceConfigForm(IAccountStatedService asservice, ASServiceConfig conf)
        {
            InitializeComponent();

            _asservice = asservice;
            _asservice_config = conf;

            txtName.Text = ASServiceConfig.Name;
        }

        #region Properties
        private IAccountStatedService _asservice;
        public IAccountStatedService ASService
        {
            get { return _asservice; }
            private set { _asservice = value; }
        }

        private ASServiceConfig _asservice_config;
        public ASServiceConfig ASServiceConfig
        {
            get { return _asservice_config; }
            private set { _asservice_config = value; }
        }
        #endregion

        private ASServiceConfigPanel _current_config_pane;
        public ASServiceConfigPanel CurrentConfigPane
        {
            get { return _current_config_pane; }
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            ASServiceConfig clone = new ASServiceConfig(ASServiceConfig.BaseXml.CloneNode(true) as XmlElement);

            ASServiceConfigPanel cpane = ASService.CreateASServiceConfigPanel(clone);
            _current_config_pane = cpane;
            cpane.Dock = DockStyle.Fill;
            pConfig.Controls.Clear();
            pConfig.Controls.Add(cpane);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CurrentConfigPane.ConfigIsValid())
            {
                ASServiceConfig = CurrentConfigPane.GetConfig();
                ASServiceConfig.Name = txtName.Text;
                ASServiceConfig.ModuleCode = ASService.ModuleCode;

                DialogResult = DialogResult.OK;
            }
            else
                DialogResult = DialogResult.None;
        }
    }
}