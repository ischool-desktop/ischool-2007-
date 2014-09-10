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
    public partial class DisciplineDetailForm : SelectDateRangeForm
    {
        private int _sizeIndex = 0;

        public int PaperSize
        {
            get { return _sizeIndex; }
        }

        public DisciplineDetailForm()
        {
            InitializeComponent();
            LoadPreference();
        }

        private void LoadPreference()
        {
            #region Ū�� Preference

            XmlElement config = CurrentUser.Instance.Preference["�Z�ż��g���Ӫ�"];

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
                    CurrentUser.Instance.Preference["�Z�ż��g���Ӫ�"] = config;
                }
            }
            else
            {
                #region ���ͪťճ]�w��
                config = new XmlDocument().CreateElement("�Z�ż��g���Ӫ�");
                XmlElement printSetup = config.OwnerDocument.CreateElement("Print");
                printSetup.SetAttribute("PaperSize", "0");
                config.AppendChild(printSetup);
                CurrentUser.Instance.Preference["�Z�ż��g���Ӫ�"] = config;
                #endregion
            }

            #endregion
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WeekReportCountConfig config = new WeekReportCountConfig("�Z�ż��g���Ӫ�", _sizeIndex);
            if (config.ShowDialog() == DialogResult.OK)
            {
                LoadPreference();
            }
        }
    }
}