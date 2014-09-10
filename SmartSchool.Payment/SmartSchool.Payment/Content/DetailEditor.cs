using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using System.Xml;

namespace SmartSchool.Payment.Content
{
    public partial class DetailEditor : BaseForm
    {
        private string _payment_detail_id;

        private string ID
        {
            get { return _payment_detail_id; }
        }

        public DetailEditor(string payment_name, string amount, string payment_detail_id)
        {
            InitializeComponent();
            lblPaymentName.Text = payment_name;
            lblAmount.Text = amount;
            _payment_detail_id = payment_detail_id;
        }

        private void DetailEditor_Load(object sender, EventArgs e)
        {
            DSXmlHelper helper = SmartSchool.Feature.Payment.QueryPayment.GetPaymentHistories(_payment_detail_id);

            dataGridViewX1.Rows.Clear();
            dataGridViewX1.SuspendLayout();

            foreach (XmlElement his in helper.GetElements("PaymentHistory"))
            {
                DSXmlHelper hisHelper = new DSXmlHelper(his);
                DataGridViewRow row = new DataGridViewRow();

                string va = hisHelper.GetText("VirtualAccount");
                string amount = hisHelper.GetText("Amount");
                string paid = hisHelper.GetText("Paid") == "1" ? "是" : "否";

                row.CreateCells(dataGridViewX1, va, amount, paid);
                dataGridViewX1.Rows.Add(row);
            }

            dataGridViewX1.ResumeLayout();
        }
    }
}