using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Payment.Interfaces;
using IntelliSchool.DSA30.Util;
using System.Xml;

namespace FirstBankPayment.FirstBank
{
    internal partial class FcbBankConfigPane : BankConfigPane
    {
        public FcbBankConfigPane()
        {
            InitializeComponent();
            cboChargeOnus.DisplayMember = "Text";
        }

        private void dgEnterpriseCode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgEnterpriseCode.BeginEdit(true);
        }

        private void dgEnterpriseCode_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow rowPrevious = e.RowIndex == 0 ? null : dgEnterpriseCode.Rows[e.RowIndex - 1];
            DataGridViewRow rowCurrent = dgEnterpriseCode.Rows[e.RowIndex];
            DataGridViewRow rowNext;

            if (rowCurrent.IsNewRow)
                rowNext = null;
            else
                rowNext = dgEnterpriseCode.Rows[e.RowIndex + 1];

            DataGridViewCell cellPrevious = rowPrevious == null ? null : rowPrevious.Cells["chEnd"];
            DataGridViewCell cellCurrent1 = rowCurrent.Cells["chStart"];
            DataGridViewCell cellCurrent2 = rowCurrent.Cells["chEnd"];
            DataGridViewCell cellNext = rowNext == null ? null : rowNext.Cells["chStart"];

            int currentStart = -1, nextStart = -1;

            if (rowPrevious != null && !rowCurrent.IsNewRow)
            {
                int previousEndValue;
                if (int.TryParse(cellPrevious.Value + "", out previousEndValue))
                    currentStart = previousEndValue + 1;
            }
            else if (!rowCurrent.IsNewRow)
                currentStart = 1;

            if (!rowCurrent.IsNewRow && !rowNext.IsNewRow)
            {
                int currentEndValue;
                if (int.TryParse(cellCurrent2.Value + "", out currentEndValue))
                    nextStart = currentEndValue + 1;
            }

            if (currentStart > 0)
                cellCurrent1.Value = currentStart;

            if (nextStart > 0)
                cellNext.Value = nextStart;
        }

        /// <summary>
        /// �D�{���|�I�s����k�A���Ʈw�x�s���Ȧ�պA�]�w�i�ӡA�H��ܦb�e���W�C
        /// </summary>
        /// <param name="config"></param>
        public override void SetConfig(BankConfig config)
        {
            Config = config;

            if (Config.Content == null)
                return;

            DSXmlHelper content = new DSXmlHelper(Config.Content);

            string chargeOnus = content.GetText("@ChainChargeOnus");

            if (chargeOnus == "Payee")
                cboChargeOnus.SelectedIndex = 0; //�Ǯխt��C
            else if (chargeOnus == "Payer")
                cboChargeOnus.SelectedIndex = 1; //�ǥͭt��C
            else
                cboChargeOnus.SelectedIndex = -1;//�L�H�t��C

            txtSchoolCode.Text = content.GetText("@SchoolCode");

            dgEnterpriseCode.Rows.Clear();
            foreach (XmlElement eachLevel in content.GetElements("AccountConfig/AmountLevel"))
            {
                string lower = eachLevel.GetAttribute("LowerLimit");
                string upper = eachLevel.GetAttribute("UpperLimit");
                string shopCharge = eachLevel.GetAttribute("ShopCharge");
                string postCharge = eachLevel.GetAttribute("PostCharge"); //���d�̡C
                string enpriCode = eachLevel.GetAttribute("EnterpriseCode");

                //�Ҧ��� Post Charge ���@�ˡC
                txtPostCharge.Text = postCharge;

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgEnterpriseCode, lower, upper, shopCharge, enpriCode);
                dgEnterpriseCode.Rows.Add(row);
            }
        }

        /// <summary>
        /// �D�{���|�I�s����k�A���o�ϥΪ̳]�w���Ȧ�պA�ȡA�g�^��Ʈw�C
        /// </summary>
        /// <returns></returns>
        public override BankConfig GetConfig()
        {
            DSXmlHelper content = new DSXmlHelper("Content"); ;

            if (cboChargeOnus.SelectedIndex == 0)
                content.SetAttribute(".", "ChainChargeOnus", "Payee"); //�Ǯխt��C
            else if (cboChargeOnus.SelectedIndex == 1)
                content.SetAttribute(".", "ChainChargeOnus", "Payer"); //�ǥͭt��C
            else
                content.SetAttribute(".", "ChainChargeOnus", ""); //�L�H�t��C

            //�ǮեN�X�C
            content.SetAttribute(".", "SchoolCode", txtSchoolCode.Text);

            DSXmlHelper account = new DSXmlHelper(content.AddElement("AccountConfig"));
            foreach (DataGridViewRow eachRow in dgEnterpriseCode.Rows)
            {
                if (eachRow.IsNewRow) continue;

                string lower = eachRow.Cells["chStart"].Value + "";
                string upper = eachRow.Cells["chEnd"].Value + "";
                string shopCharge = eachRow.Cells["chShop"].Value + "";
                string postCharge = txtPostCharge.Text;
                string enpriCode = eachRow.Cells["chEnterCode"].Value + "";

                XmlElement eachAmount = account.AddElement("AmountLevel");

                eachAmount.SetAttribute("EnterpriseCode", enpriCode);
                eachAmount.SetAttribute("LowerLimit", lower);
                eachAmount.SetAttribute("UpperLimit", upper);
                eachAmount.SetAttribute("ShopCharge", shopCharge);
                eachAmount.SetAttribute("PostCharge", postCharge);
            }

            Config.Content = content.BaseElement;

            return Config;
        }

        /// <summary>
        /// �ˬd�պA�ȬO�_���T���޿�C
        /// </summary>
        /// <returns></returns>
        public override bool ConfigIsValid()
        {
            List<int> code = new List<int>();
            List<Range> ranges = new List<Range>();

            foreach (DataGridViewRow row in dgEnterpriseCode.Rows)
            {
                if (row.IsNewRow) continue;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (string.IsNullOrEmpty("" + cell.Value))
                        return ConfigError("��줣�i�ť�");

                    int temp;
                    if (int.TryParse("" + cell.Value, out temp))
                    {
                        if (temp < 0)
                            return ConfigError("�Ʀr����p��s");
                        if (cell.OwningColumn == chEnterCode && temp.ToString().Length != 5)
                            return ConfigError("���~�N�X�����������");
                    }
                    else
                        return ConfigError("��쥲�����Ʀr");
                }

                if (code.Contains(int.Parse("" + row.Cells[chEnterCode.Name].Value)))
                    return ConfigError("���~�N�X����");
                else
                    code.Add(int.Parse("" + row.Cells[chEnterCode.Name].Value));

                ranges.Add(new Range(int.Parse("" + row.Cells[chStart.Name].Value), int.Parse("" + row.Cells[chEnd.Name].Value)));
            }

            ranges.Sort(SortRangeByLowerLimit);
            System.Collections.IEnumerator enumerator = ranges.GetEnumerator();
            int upper_limit = int.MinValue;
            while (enumerator.MoveNext())
            {
                Range range = enumerator.Current as Range;
                if (range.LowerLimit > range.UpperLimit)
                    return ConfigError("���B�d����~");

                int current_lower_limit = range.LowerLimit;
                if (current_lower_limit <= upper_limit)
                    return ConfigError("���B�d���|");
                upper_limit = range.UpperLimit;
            }

            if (!string.IsNullOrEmpty(txtSchoolCode.Text))
            {
                if (txtSchoolCode.Text.Length > 1)
                    return ConfigError("�ǮեN�X���ݬO�@��ƪ��Ʀr�C");

                if (!char.IsDigit(txtSchoolCode.Text[0]))
                    return ConfigError("�ǮեN�X���ݬO�@��ƪ��Ʀr�C");
            }

            return true;
        }

        private bool ConfigError(string msg)
        {
            MessageBox.Show(msg);
            return false;
        }

        private static int SortRangeByLowerLimit(Range x, Range y)
        {
            return x.LowerLimit.CompareTo(y.LowerLimit);
        }

        /// <summary>
        /// ���B�d��
        /// </summary>
        internal class Range
        {
            private int _lowerLimit;
            public int LowerLimit
            {
                get { return _lowerLimit; }
            }

            private int _upperLimit;
            public int UpperLimit
            {
                get { return _upperLimit; }
            }

            public Range(int lower, int upper)
            {
                _lowerLimit = lower;
                _upperLimit = upper;
            }
        }
    }
}
