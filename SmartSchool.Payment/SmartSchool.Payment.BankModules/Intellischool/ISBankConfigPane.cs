using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Payment.Interfaces;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Payment.BankModules.Intellischool
{
    public partial class ISBankConfigPane : BankConfigPane
    {
        public ISBankConfigPane()
        {
            InitializeComponent();
        }

        public override void SetConfig(BankConfig config)
        {
            base.SetConfig(config);
        }

        public override BankConfig GetConfig()
        {
            return Config;
        }
    }
}
