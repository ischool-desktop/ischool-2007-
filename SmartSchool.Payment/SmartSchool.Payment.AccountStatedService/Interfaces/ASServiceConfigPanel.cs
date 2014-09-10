using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SmartSchool.Payment.AccountStatedService.Interfaces
{
    public partial class ASServiceConfigPanel : UserControl
    {
        public ASServiceConfigPanel()
        {
            InitializeComponent();
        }

        protected ASServiceConfig Config;

        public virtual void SetConfig(ASServiceConfig config)
        {
            Config = config;
        }

        public virtual ASServiceConfig GetConfig()
        {
            return Config;
        }

        public virtual bool ConfigIsValid()
        {
            return true;
        }
    }
}
