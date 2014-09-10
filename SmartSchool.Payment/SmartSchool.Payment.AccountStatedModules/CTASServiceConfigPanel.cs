using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Payment.AccountStatedService.Interfaces;
using IntelliSchool.DSA30.Util;
using System.Xml;

namespace SmartSchool.Payment.AccountStatedModules
{
    public partial class CTASServiceConfigPanel : ASServiceConfigPanel
    {
        public CTASServiceConfigPanel()
        {
            InitializeComponent();
        }

        public override void SetConfig(ASServiceConfig config)
        {
            Config = config;

            if (Config.Content == null)
                return;

            DSXmlHelper content = new DSXmlHelper(Config.Content);

            XmlElement[] schools = content.GetElements("School");

            if (schools.Length <= 0)
            {
                string lic = content.GetText("LicenseFile");
                string pin = content.GetText("PinCode");

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgSchool, lic, pin, "");
                dgSchool.Rows.Add(row);
            }
            else
            {
                foreach (XmlElement each in schools)
                {
                    DSXmlHelper hlpeach = new DSXmlHelper(each);
                    DataGridViewRow row = new DataGridViewRow();

                    string lic = each.GetAttribute("LicenseFile");
                    string pin = each.GetAttribute("PinCode");
                    string scode = each.GetAttribute("SchoolCode");
                    string enabled = string.IsNullOrEmpty(each.GetAttribute("Enabled")) ? "false" : each.GetAttribute("Enabled");

                    row.CreateCells(dgSchool, lic, pin, scode, enabled);
                    dgSchool.Rows.Add(row);
                }
            }

            txtEnterpirseCode.Text = content.GetText("EnterpriseCode");
            txtFTPUrl.Text = content.GetText("FTPUrl");
            txtFTPAccount.Text = content.GetText("FTPAccount");
            txtFTPPassword.Text = content.GetText("FTPPassword");
        }

        public override ASServiceConfig GetConfig()
        {
            DSXmlHelper content = new DSXmlHelper("Content");

            foreach (DataGridViewRow each in dgSchool.Rows)
            {
                if (each.IsNewRow) continue;

                DSXmlHelper school = new DSXmlHelper(content.AddElement(".", "School"));

                school.SetAttribute(".", "LicenseFile", each.Cells["chLicense"].Value + "");
                school.SetAttribute(".", "PinCode", each.Cells["chPin"].Value + "");
                school.SetAttribute(".", "SchoolCode", each.Cells["chSchoolCode"].Value + "");
                school.SetAttribute(".", "Enabled", each.Cells["chEnabled"].Value + "");
            }

            content.AddElement(".", "EnterpriseCode", txtEnterpirseCode.Text);
            content.AddElement(".", "FTPUrl", txtFTPUrl.Text);
            content.AddElement(".", "FTPAccount", txtFTPAccount.Text);
            content.AddElement(".", "FTPPassword", txtFTPPassword.Text);

            Config.Content = content.BaseElement;

            return Config;
        }

        public override bool ConfigIsValid()
        {
            return base.ConfigIsValid();
        }
    }
}
