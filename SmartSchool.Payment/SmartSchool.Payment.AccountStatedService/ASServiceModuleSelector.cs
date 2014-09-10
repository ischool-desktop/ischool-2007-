using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Payment.AccountStatedService.Interfaces;

namespace SmartSchool.Payment.AccountStatedService
{
    public partial class ASServiceModuleSelector : Form
    {
        public ASServiceModuleSelector()
        {
            InitializeComponent();

            cboBridges.DisplayMember = "Name";
            cboBridges.ValueMember = "Service";

            cboBridges.Items.Add("");
            foreach (IAccountStatedService each in ASServiceProvider.GetServices())
            {
                ServiceItem item = new ServiceItem(each.Name, each);
                cboBridges.Items.Add(item);
            }
        }

        private IAccountStatedService _selected_service;
        public IAccountStatedService SelectedService
        {
            get { return _selected_service; }
        }

        private void cboBanks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ServiceItem item = cboBridges.SelectedItem as ServiceItem;

            if (item != null)
                _selected_service = item.Service;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (SelectedService == null)
            {
                MessageBox.Show("請選擇一個銀行模組。");
                DialogResult = DialogResult.None;
            }
            else
                DialogResult = DialogResult.OK;
        }

        private class ServiceItem
        {
            public ServiceItem(string name, IAccountStatedService service)
            {
                _name = name;
                _service = service;
            }

            private string _name;
            public string Name
            {
                get { return _name; }
            }

            private IAccountStatedService _service;
            public IAccountStatedService Service
            {
                get { return _service; }
            }

        }
    }
}