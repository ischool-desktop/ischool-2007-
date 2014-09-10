using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Payment.AccountStatedService.Interfaces;
using IntelliSchool.DSA30.Util;
using System.IO;
using System.Xml;
using System.Threading;
using System.Collections;
using System.Diagnostics;

namespace SmartSchool.Payment.AccountStatedService
{
    public partial class ConfigMain : Form
    {
        public ConfigMain()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ASServiceModuleSelector selector = new ASServiceModuleSelector();
            if (selector.ShowDialog() == DialogResult.OK)
            {
                ASServiceConfigForm config = new ASServiceConfigForm(selector.SelectedService);
                if (config.ShowDialog() == DialogResult.OK)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = config.ASServiceConfig.Name;
                    item.Tag = config.ASServiceConfig;
                    lstConfigs.Items.Add(item);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lstConfigs.FocusedItem == null) return;

            ListViewItem item = lstConfigs.FocusedItem;
            ASServiceConfig config = item.Tag as ASServiceConfig;
            IAccountStatedService service = ASServiceProvider.GetService(config.ModuleCode);
            ASServiceConfigForm confForm = new ASServiceConfigForm(service, config);

            if (confForm.ShowDialog() == DialogResult.OK)
            {
                item.Text = confForm.ASServiceConfig.Name;
                item.Tag = confForm.ASServiceConfig;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstConfigs.FocusedItem == null) return;

            DialogResult dr = MessageBox.Show("確定要刪除嗎？", Application.ProductName, MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
                lstConfigs.FocusedItem.Remove();
        }

        private void ConfigMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            string basePath = Application.StartupPath;
            DSXmlHelper configs = new DSXmlHelper("ASServiceConfigs");

            foreach (ListViewItem each in lstConfigs.Items)
            {
                ASServiceConfig config = each.Tag as ASServiceConfig;
                configs.AddElement(".", config.BaseXml);
            }

            configs.Save(Path.Combine(basePath, "ASServiceConfigs.xml"));
        }

        private void ConfigMain_Load(object sender, EventArgs e)
        {
            string basePath = Application.StartupPath;
            FileInfo file = new FileInfo(Path.Combine(basePath, "ASServiceConfigs.xml"));

            if (!file.Exists) return;

            DSXmlHelper configs = new DSXmlHelper(DSXmlHelper.LoadXml(file));

            lstConfigs.Items.Clear();
            foreach (XmlElement each in configs.GetElements("ASServiceConfig"))
            {
                ASServiceConfig config = new ASServiceConfig(each);
                ListViewItem item = new ListViewItem();
                item.Text = config.Name;
                item.Tag = config;

                lstConfigs.Items.Add(item);
            }
        }

        private void btnServiceStatus_Click(object sender, EventArgs e)
        {
            ASServiceStatus status = new ASServiceStatus();
            status.ShowDialog();
        }

        private void ctxiRunNow_Click(object sender, EventArgs e)
        {
            ListViewItem item = lstConfigs.FocusedItem;

            if (item == null) return;

            ASServiceConfig config = item.Tag as ASServiceConfig;
            ASServiceRunner runner = new ASServiceRunner(Path.Combine(ServiceProgram.WorkingFolder, "Log"));
            runner.Add(config);
            runner.Run();
        }
    }
}