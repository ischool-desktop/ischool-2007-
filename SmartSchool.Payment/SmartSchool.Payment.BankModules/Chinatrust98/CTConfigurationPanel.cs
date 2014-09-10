using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using IntelliSchool.DSA30.Util;
using SmartSchool.Payment.Interfaces;

namespace AccountsReceivalbe.BuildinBank.Chinatrust98
{
    public partial class CTConfigurationPanel : BankConfigPane
    {
        public CTConfigurationPanel(BankConfig config)
        {
            InitializeComponent();
            cboChargeOnus.DisplayMember = "Text";

            Config = config;

            if (Config.Content == null)
                return;

            DSXmlHelper content = new DSXmlHelper(Config.Content);

            string chargeOnus = content.GetText("@ChainChargeOnus");

            if (chargeOnus == "Payee")
                cboChargeOnus.SelectedIndex = 0; //非付款人負擔。
            else if (chargeOnus == "Payer")
                cboChargeOnus.SelectedIndex = 1; //付款人負擔。
            else
                cboChargeOnus.SelectedIndex = -1;//無人負擔。

            txtSchoolCode.Text = content.GetText("@SchoolCode");

            dgEnterpriseCode.Rows.Clear();
            foreach (XmlElement eachLevel in content.GetElements("AccountConfig/AmountLevel"))
            {
                string lower = eachLevel.GetAttribute("LowerLimit");
                string upper = eachLevel.GetAttribute("UpperLimit");
                string shopCode = eachLevel.GetAttribute("ShopCode");
                string shopCharge = eachLevel.GetAttribute("ShopCharge");
                string postCode = eachLevel.GetAttribute("PostCode");
                string enpriCode = eachLevel.GetAttribute("EnterpriseCode");

                //所有的 Post Charge 都一樣。
                //txtPostCharge.Text = postCharge;

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgEnterpriseCode, lower, upper, shopCode, shopCharge, postCode, enpriCode);
                dgEnterpriseCode.Rows.Add(row);
            }
        }

        public override BankConfig GetConfig()
        {
            DSXmlHelper content = new DSXmlHelper("Content"); ;

            if (cboChargeOnus.SelectedIndex == 0)
                content.SetAttribute(".", "ChainChargeOnus", "Payee"); //收款人負擔。
            else if (cboChargeOnus.SelectedIndex == 1)
                content.SetAttribute(".", "ChainChargeOnus", "Payer"); //付款人負擔。
            else
                content.SetAttribute(".", "ChainChargeOnus", ""); //無人負擔。

            //學校代碼。
            content.SetAttribute(".", "SchoolCode", txtSchoolCode.Text);

            DSXmlHelper account = new DSXmlHelper(content.AddElement("AccountConfig"));
            foreach (DataGridViewRow eachRow in dgEnterpriseCode.Rows)
            {
                if (eachRow.IsNewRow) continue;

                string lower = eachRow.Cells["chStart"].Value + "";
                string upper = eachRow.Cells["chEnd"].Value + "";
                string shopCode = eachRow.Cells["chShopCode"].Value + "";
                string shopCharge = eachRow.Cells["chShop"].Value + "";
                string postCode = eachRow.Cells["chPostCode"].Value + "";
                string enpriCode = eachRow.Cells["chEnterCode"].Value + "";

                XmlElement eachAmount = account.AddElement("AmountLevel");

                eachAmount.SetAttribute("EnterpriseCode", enpriCode);
                eachAmount.SetAttribute("LowerLimit", lower);
                eachAmount.SetAttribute("UpperLimit", upper);
                eachAmount.SetAttribute("ShopCode", shopCode);
                eachAmount.SetAttribute("ShopCharge", shopCharge);
                eachAmount.SetAttribute("PostCode", postCode);
                //eachAmount.SetAttribute("PostCharge", postCharge);  //郵局手續費是固定的。
            }

            Config.Content = content.BaseElement;

            return Config;
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
                currentStart = 0;

            if (!rowCurrent.IsNewRow && !rowNext.IsNewRow)
            {
                int currentEndValue;
                if (int.TryParse(cellCurrent2.Value + "", out currentEndValue))
                    nextStart = currentEndValue + 1;
            }

            if (currentStart >= 0)
                cellCurrent1.Value = currentStart;

            if (nextStart >= 0)
                cellNext.Value = nextStart;
        }

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
                        return ConfigError("欄位不可空白");

                    if (IsShopCodeField(cell)) continue;
                    if (IsPostCodeField(cell)) continue;

                    int temp;
                    if (int.TryParse("" + cell.Value, out temp))
                    {
                        if (temp < 0)
                            return ConfigError("數字不能小於零");
                        if (cell.OwningColumn == chEnterCode && temp.ToString().Length != 5)
                            return ConfigError("企業代碼必須為五位數");
                    }
                    else
                        return ConfigError("欄位必須為數字");
                }

                if (code.Contains(int.Parse("" + row.Cells[chEnterCode.Name].Value)))
                    return ConfigError("企業代碼重覆");
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
                    return ConfigError("金額範圍錯誤");

                int current_lower_limit = range.LowerLimit;
                if (current_lower_limit <= upper_limit)
                    return ConfigError("金額範圍重疊");
                upper_limit = range.UpperLimit;
            }

            if (!string.IsNullOrEmpty(txtSchoolCode.Text))
            {
                if (txtSchoolCode.Text.Length > 1)
                    return ConfigError("學校代碼必需是一位數的數字。");

                if (!char.IsDigit(txtSchoolCode.Text[0]))
                    return ConfigError("學校代碼必需是一位數的數字。");
            }

            return true;
        }

        private bool IsPostCodeField(DataGridViewCell cell)
        {
            return cell.OwningColumn.Name == "chPostCode";
        }

        private static bool IsShopCodeField(DataGridViewCell cell)
        {
            return cell.OwningColumn.Name == "chShopCode";
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
        /// 金額範圍
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
