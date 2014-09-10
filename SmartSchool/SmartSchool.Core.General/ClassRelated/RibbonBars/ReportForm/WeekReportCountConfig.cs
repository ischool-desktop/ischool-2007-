using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Xml;
using SmartSchool.Common;

namespace SmartSchool.ClassRelated.RibbonBars.Reports
{
    public partial class WeekReportCountConfig : BaseForm
    {
        private string _reportName = "";
        public WeekReportCountConfig(string reportname,int sizeIndex)
        {
            InitializeComponent();

            _reportName = reportname;
            comboBoxEx1.SelectedIndex = sizeIndex;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            #region Àx¦s Preference

            XmlElement config = CurrentUser.Instance.Preference[_reportName];

            if (config == null)
                config = new XmlDocument().CreateElement(_reportName);

            XmlElement print = config.OwnerDocument.CreateElement("Print");
            print.SetAttribute("PaperSize", comboBoxEx1.SelectedIndex.ToString());

            if (config.SelectSingleNode("Print") == null)
                config.AppendChild(print);
            else
                config.ReplaceChild(print, config.SelectSingleNode("Print"));

            CurrentUser.Instance.Preference[_reportName] = config;

            #endregion

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}