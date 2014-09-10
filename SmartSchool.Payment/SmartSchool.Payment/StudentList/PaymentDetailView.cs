using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.Feature.Payment;
using SmartSchool.StudentRelated;
using SmartSchool.Payment.GT;
using System.Text.RegularExpressions;

namespace SmartSchool.Payment.StudentList
{
    public partial class PaymentDetailView : UserControl
    {
        /// <summary>
        /// 記錄目前 Grid 中的所有欄位資料。
        /// </summary>
        private Dictionary<int, string> _fields = new Dictionary<int, string>();
        private GT.Payment _payment;

        public PaymentDetailView()
        {
            InitializeComponent();

            int index = -1;
            _fields.Add(++index, "班級");
            _fields.Add(++index, "姓名");
            _fields.Add(++index, "學號");
            _fields.Add(++index, "應繳金額");
            _fields.Add(++index, "已繳金額");
        }

        internal void PopulatePaymentDetail(GT.Payment payment)
        {
            _payment = payment;

            ClearDataGridView();

            DataGridViewColumn gridcolumnt = null;
            foreach (string each in _fields.Values)
            {
                gridcolumnt = new DataGridViewTextBoxColumn();
                gridcolumnt.HeaderText = each;
                gridcolumnt.ReadOnly = true;
                gridcolumnt.Width = 100;

                dgPaymentDetail.Columns.Add(gridcolumnt);
            }
            if (gridcolumnt != null)
                gridcolumnt.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //顯示 DataGridView 的資料列。
            foreach (PaymentDetail each in payment.Details)
            {
                DataGridViewRow row = new DataGridViewRow();

                List<string> values = new List<string>();
                values.Add(each.ClassName);
                values.Add(each.StudentName);
                values.Add(each.StudentNumber);
                values.Add(each.Amount.ToString("C"));
                values.Add(each.GetPaidAmount().ToString("C"));

                if (each.IsDirtyRecord)
                {
                    row.DefaultCellStyle = new DataGridViewCellStyle(row.DefaultCellStyle);
                    row.DefaultCellStyle.BackColor = Color.Blue;
                    row.DefaultCellStyle.ForeColor = Color.White;
                    row.DefaultCellStyle.SelectionBackColor = Color.Blue;
                    row.DefaultCellStyle.SelectionForeColor = Color.White;
                }

                row.CreateCells(dgPaymentDetail, values.ToArray());

                //建立雙向參考。
                row.Tag = each;

                dgPaymentDetail.Rows.Add(row);
            }
        }

        public void ClearDataGridView()
        {
            dgPaymentDetail.Columns.Clear();
            dgPaymentDetail.Rows.Clear();
        }

        public void FilterData(string pattern)
        {
            Regex rx = null;

            try
            {
                rx = new Regex(pattern);
            }
            catch
            {
                rx = new Regex("\\.");
            }

            foreach (DataGridViewRow each in dgPaymentDetail.Rows)
            {
                PaymentDetail pd = each.Tag as PaymentDetail;

                if (pd == null)
                    continue;

                bool visible = false;

                if (rx.Match(pd.StudentName).Success)
                    visible |= true;

                if (rx.Match(pd.ClassName).Success)
                    visible |= true;

                if (rx.Match(pd.StudentNumber).Success)
                    visible |= true;

                each.Visible = visible;
            }
        }

        private void dgPaymentDetail_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        }
    }
}
