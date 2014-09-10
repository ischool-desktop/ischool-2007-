using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.Payment.Interfaces;
using DevComponents.Editors;

namespace SmartSchool.Payment.BankManagement
{
    public partial class BankModuleSelect : BaseForm
    {
        public BankModuleSelect()
        {
            InitializeComponent();

            foreach (IBankService each in BankServiceProvider.GetServices())
            {
                ComboItem item = new ComboItem();
                item.Text = each.Name;
                item.Tag = each;
                cboBanks.Items.Add(item);
            }
        }

        private IBankService _selected_bank;
        public IBankService SelectedBank
        {
            get { return _selected_bank; }
        }

        private void cboBanks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboItem item = cboBanks.SelectedItem as ComboItem;

            if (item != null)
                _selected_bank = item.Tag as IBankService;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (SelectedBank == null)
            {
                MsgBox.Show("請選擇一個銀行模組。");
                DialogResult = DialogResult.None;
            }
            else
                DialogResult = DialogResult.OK;
        }
    }
}