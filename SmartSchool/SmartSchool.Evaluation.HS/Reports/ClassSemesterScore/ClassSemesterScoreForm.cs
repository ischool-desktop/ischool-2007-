using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SmartSchool.Common;

namespace SmartSchool.Evaluation.Reports
{
    public partial class ClassSemesterScoreForm : SelectSemesterForm
    {
        private bool _over100 = false;
        public bool AllowMoralScoreOver100
        {
            get { return _over100; }
        }

        private int _paperSize = 0;

        public int PaperSize
        {
            get { return _paperSize; }
        }
	

        public ClassSemesterScoreForm()
        {
            InitializeComponent();
            LoadPreference();
        }

        void LoadPreference()
        {
            #region Ū�� Preference

            XmlElement config = CurrentUser.Instance.Preference["�Z�žǥ;Ǵ����Z�@����"];
            if (config != null)
            {
                //�C�L��T
                XmlElement print = (XmlElement)config.SelectSingleNode("Print");
                if (print != null)
                {
                    if (!string.IsNullOrEmpty(print.GetAttribute("AllowMoralScoreOver100")))
                        _over100 = bool.Parse(print.GetAttribute("AllowMoralScoreOver100"));
                    if (!string.IsNullOrEmpty(print.GetAttribute("PaperSize")))
                        _paperSize = int.Parse(print.GetAttribute("PaperSize"));
                }
                else
                {
                    XmlElement newPrint = config.OwnerDocument.CreateElement("Print");
                    newPrint.SetAttribute("AllowMoralScoreOver100", "False");
                    newPrint.SetAttribute("PaperSize", "0");
                    _over100 = false;
                    _paperSize = 0;
                    config.AppendChild(newPrint);
                    CurrentUser.Instance.Preference["�Z�žǥ;Ǵ����Z�@����"] = config;
                }
            }
            else
            {
                #region ���ͪťճ]�w��
                config = new XmlDocument().CreateElement("�Z�žǥ;Ǵ����Z�@����");
                CurrentUser.Instance.Preference["�Z�žǥ;Ǵ����Z�@����"] = config;
                #endregion
            }

            #endregion
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClassSemesterScoreConfig form = new ClassSemesterScoreConfig(_over100, _paperSize);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadPreference();
            }
        }
    }
}