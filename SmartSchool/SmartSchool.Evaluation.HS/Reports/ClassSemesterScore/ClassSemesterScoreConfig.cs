using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.Common;
using System.Xml;

namespace SmartSchool.Evaluation.Reports
{
    public partial class ClassSemesterScoreConfig : BaseForm
    {
        public ClassSemesterScoreConfig(bool over100, int papersize)
        {
            InitializeComponent();

            checkBoxX1.Checked = over100;
            comboBoxEx1.SelectedIndex = papersize;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            #region �x�s Preference

            XmlElement config = CurrentUser.Instance.Preference["�Z�žǥ;Ǵ����Z�@����"];

            if (config == null)
            {
                config = new XmlDocument().CreateElement("�Z�žǥ;Ǵ����Z�@����");
            }

            XmlElement print = config.OwnerDocument.CreateElement("Print");
            print.SetAttribute("AllowMoralScoreOver100", checkBoxX1.Checked.ToString());
            print.SetAttribute("PaperSize", comboBoxEx1.SelectedIndex.ToString());

            if (config.SelectSingleNode("Print") == null)
                config.AppendChild(print);
            else
                config.ReplaceChild(print, config.SelectSingleNode("Print"));

            CurrentUser.Instance.Preference["�Z�žǥ;Ǵ����Z�@����"] = config;

            #endregion

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ClassSemesterScoreConfig_Load(object sender, EventArgs e)
        {

        }
    }
}