using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using System.Reflection;
using System.Xml;
using System.IO;
using SmartSchool.Payment.Interfaces;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Payment.BankManagement
{
    public partial class ManageForm : BaseForm
    {
        public ManageForm()
        {
            InitializeComponent();
        }

        private void ManageForm_Load(object sender, EventArgs e)
        {
            //將各個銀行組態列出到 ListView 控制項中，並將每個 Item 與組態進行關連(利用 Tag 屬性 )。
            DisplayConfigsToListView();
        }

        #region Method DisplayConfigsToListView
        private void DisplayConfigsToListView()
        {
            lstBankConfigs.Items.Clear();
            foreach (BankConfig each in BankConfigManager.GetConfigList())
            {
                ListViewItem item = new ListViewItem();
                item.Text = each.Name;
                item.Tag = each;
                lstBankConfigs.Items.Add(item);
            }
        }
        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
            BankModuleSelect modselect = new BankModuleSelect();
            DialogResult dr = modselect.ShowDialog();

            if (dr == DialogResult.OK)
            {
                ConfigForm config = new ConfigForm(modselect.SelectedBank);
                DialogResult cdr = config.ShowDialog();

                if (cdr == DialogResult.OK)
                {
                    BankConfigManager.SaveConfig(config.BankConfig);
                    CurrentUser.Instance.AppLog.Write("新增銀行組態", string.Format("新增「{0}」組態", config.BankConfig.Name), "銀行組態設定", config.BankConfig.BaseXml.OuterXml);

                    DisplayConfigsToListView(); //重新整理銀行組態清單。
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            ListViewItem item = lstBankConfigs.SelectedItems[0];
            BankConfig config = item.Tag as BankConfig;
            IBankService service = null;
            string moduleCode = config.ModuleCode;

            foreach (IBankService each in BankServiceProvider.GetServices())
            {
                if (each.ModuleCode == moduleCode)
                {
                    service = each;
                    break;
                }
            }

            if (service == null)
            {
                MsgBox.Show(string.Format("找不到此組態所指定的銀行模組。"));
                return;
            }

            ConfigForm cform = new ConfigForm(service, config);
            DialogResult dr = cform.ShowDialog();

            if (dr == DialogResult.OK)
            {
                BankConfig bc = cform.BankConfig;
                string name = bc.Name;

                BankConfigManager.SaveConfig(bc);
                CurrentUser.Instance.AppLog.Write("修改銀行組態", string.Format("修改「{0}」組態", bc.Name), "銀行組態設定", bc.BaseXml.OuterXml);

                item.Text = name;
                item.Tag = bc;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ListViewItem item = lstBankConfigs.SelectedItems[0];
            BankConfig config = item.Tag as BankConfig;
            string name = config.Name;

            DialogResult dr = MsgBox.Show(string.Format("你確定要刪除「{0}」組態？", name), Application.ProductName, MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                BankConfigManager.DeleteConfig(config);
                CurrentUser.Instance.AppLog.Write("刪除銀行組態", string.Format("刪除「{0}」組態", name), "銀行組態設定", config.BaseXml.OuterXml);

                lstBankConfigs.Items.Remove(item);
            }
        }

        private void lstBankConfigs_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnModify.Enabled = (lstBankConfigs.SelectedItems.Count > 0);
            btnDelete.Enabled = (lstBankConfigs.SelectedItems.Count > 0);
        }
    }
}