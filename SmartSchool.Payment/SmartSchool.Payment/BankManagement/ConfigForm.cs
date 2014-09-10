
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using System.IO;
using System.Reflection;
using System.Xml;
using SmartSchool.Payment.Interfaces;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;

namespace SmartSchool.Payment.BankManagement
{
    public partial class ConfigForm : BaseForm
    {
        public ConfigForm(IBankService bank)
        {
            InitializeComponent();

            _bank = bank;
            _bank_config = new BankConfig();
            BankConfig.ConfigID = Guid.NewGuid().ToString();
        }

        public ConfigForm(IBankService bank, BankConfig conf)
        {
            InitializeComponent();

            _bank = bank;
            _bank_config = conf;

            txtName.Text = BankConfig.Name;
        }

        #region Properties
        private IBankService _bank;
        public IBankService Bank
        {
            get { return _bank; }
            private set { _bank = value; }
        }

        private BankConfig _bank_config;
        public BankConfig BankConfig
        {
            get { return _bank_config; }
            private set { _bank_config = value; }
        }
        #endregion

        private BankConfigPane _current_config_pane;
        public BankConfigPane CurrentConfigPane
        {
            get { return _current_config_pane; }
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            BankConfig clone = new BankConfig(BankConfig.BaseXml.CloneNode(true) as XmlElement);

            BankConfigPane cpane = Bank.CreateBankConfigPane(clone);
            _current_config_pane = cpane;
            cpane.Dock = DockStyle.Fill;
            pConfig.Controls.Clear();
            pConfig.Controls.Add(cpane);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CurrentConfigPane.ConfigIsValid())
            {
                BankConfig = CurrentConfigPane.GetConfig();
                BankConfig.Name = txtName.Text;
                BankConfig.ModuleCode = Bank.ModuleCode;

                DialogResult = DialogResult.OK;
            }
            else
                DialogResult = DialogResult.None;
        }
    }
}