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

namespace SmartSchool.ClassRelated.RibbonBars.Reports
{
    public partial class WeekAbsenceReportCountByPeriod : SelectWeekForm
    {
        private int _sizeIndex = 0;

        public int PaperSize
        {
            get { return _sizeIndex; }
        }

        public WeekAbsenceReportCountByPeriod()
        {
            InitializeComponent();
            LoadPreference();
            if (CurrentUser.Instance.Preference["���m�g����_�̸`���έp_�C�L�]�w"] == null)
            {
                new SelectPeriodForm("���m�g����_�̸`���έp_�C�L�]�w").ShowDialog();
            }
        }

        private void LoadPreference()
        {
            #region Ū�� Preference

            XmlElement config = CurrentUser.Instance.Preference["���m�g����_�̸`���έp_�C�L�]�w"];

            if (config != null)
            {
                XmlElement print = (XmlElement)config.SelectSingleNode("Print");

                if (print != null)
                {
                    if (print.HasAttribute("PaperSize"))
                        _sizeIndex = int.Parse(print.GetAttribute("PaperSize"));
                }
                else
                {
                    XmlElement newPrint = config.OwnerDocument.CreateElement("Print");
                    newPrint.SetAttribute("PaperSize", "0");
                    config.AppendChild(newPrint);
                    CurrentUser.Instance.Preference["���m�g����_�̸`���έp_�C�L�]�w"] = config;
                }
            }
            else
            {
                #region ���ͪťճ]�w��
                config = new XmlDocument().CreateElement("���m�g����_�̸`���έp_�C�L�]�w");
                XmlElement printSetup = config.OwnerDocument.CreateElement("Print");
                printSetup.SetAttribute("PaperSize", "0");
                config.AppendChild(printSetup);
                CurrentUser.Instance.Preference["���m�g����_�̸`���έp_�C�L�]�w"] = config;
                #endregion
            }

            #endregion
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new SelectPeriodForm("���m�g����_�̸`���έp_�C�L�]�w").ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WeekReportCountConfig config = new WeekReportCountConfig("���m�g����_�̸`���έp_�C�L�]�w", _sizeIndex);
            if (config.ShowDialog() == DialogResult.OK)
            {
                LoadPreference();
            }
        }
    }
}