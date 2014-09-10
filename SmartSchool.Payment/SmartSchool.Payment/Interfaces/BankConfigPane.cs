using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SmartSchool.Payment.Interfaces
{
    public partial class BankConfigPane : UserControl
    {
        public BankConfigPane()
        {
            InitializeComponent();
        }

        protected BankConfig Config;

        public virtual void SetConfig(BankConfig config)
        {
            Config = config;
        }

        public virtual BankConfig GetConfig()
        {
            return Config;
        }

        public virtual bool ConfigIsValid()
        {
            return true;
        }
    }
}
