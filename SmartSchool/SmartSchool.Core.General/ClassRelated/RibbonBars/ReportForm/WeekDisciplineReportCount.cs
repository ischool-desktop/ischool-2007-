using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SmartSchool.Common;

namespace SmartSchool.ClassRelated.RibbonBars.Reports
{
    public partial class WeekDisciplineReportCount : SelectWeekForm
    {
        private int _sizeIndex = 0;

        public int PaperSize
        {
            get { return _sizeIndex; }
        }

        public WeekDisciplineReportCount()
        {
            InitializeComponent();
            LoadPreference();
        }

        private void LoadPreference()
        {
            #region 讀取 Preference

            XmlElement config = CurrentUser.Instance.Preference["獎懲週報表_列印設定"];

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
                    CurrentUser.Instance.Preference["獎懲週報表_列印設定"] = config;
                }
            }
            else
            {
                #region 產生空白設定檔
                config = new XmlDocument().CreateElement("獎懲週報表_列印設定");
                XmlElement printSetup = config.OwnerDocument.CreateElement("Print");
                printSetup.SetAttribute("PaperSize", "0");
                config.AppendChild(printSetup);
                CurrentUser.Instance.Preference["獎懲週報表_列印設定"] = config;
                #endregion
            }

            #endregion
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WeekReportCountConfig config = new WeekReportCountConfig("獎懲週報表_列印設定", _sizeIndex);
            if (config.ShowDialog() == DialogResult.OK)
            {
                LoadPreference();
            }
        }
    }
}