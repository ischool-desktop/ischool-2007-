using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.SysAdmin;
using System.Xml;

namespace SmartSchool
{
    public partial class LicenseInfoForm : BaseForm
    {
        internal LicenseInfoForm(LicenseInfo lic)
        {
            InitializeComponent();

            XmlElement token = lic.ApplicationToken.GetTokenContent();

            lblAccessPoint.Text = lic.AccessPoint;
            lblExpiration.Text = token.SelectSingleNode("ApplicationKey/ExpireDate").InnerText;

            foreach (XmlElement each in token.SelectNodes("ApplicationKey/LocationLimit/IP"))
            {
                ListViewItem item = new ListViewItem();
                item.Text = each.GetAttribute("Address");
                lvIPList.Items.Add(item);
            }
        }
    }
}