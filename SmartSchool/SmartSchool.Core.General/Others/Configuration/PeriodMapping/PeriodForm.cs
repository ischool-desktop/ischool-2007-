using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using SmartSchool.StudentRelated.RibbonBars.AttendanceEditor;
using System.Xml;
using DevComponents.DotNetBar;
using SmartSchool.Feature.Basic;

namespace SmartSchool.Others.Configuration.PeriodMapping
{
    public partial class PeriodForm : BaseForm
    {
        public PeriodForm()
        {
            InitializeComponent();
            DSResponse dsrsp = Config.GetPeriodList();
            DSXmlHelper helper = dsrsp.GetContent();
            List<PeriodInfo> collection = new List<PeriodInfo>();
            foreach (XmlElement element in helper.GetElements("Period"))
            {
                PeriodInfo info = new PeriodInfo(element);
                collection.Add(info);
            }
            collection.Sort(SortByOrder);
            foreach (PeriodInfo info in collection)
            {
                int index = dataGridView.Rows.Add();
                DataGridViewRow row = dataGridView.Rows[index];
                row.Cells[colPeriodName.Name].Value = info.Name;
                row.Cells[colType.Name].Value = info.Type;
                row.Cells[colOrder.Name].Value = info.Sort.ToString();
                row.Cells[colAggregated.Name].Value = info.Aggregated;

                ValidateRow(row);
            }
        }
        private static int SortByOrder(PeriodInfo info1, PeriodInfo info2)
        {
            return info1.Sort.CompareTo(info2.Sort);
        }
        //private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    DataGridViewCell cell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
        //    cell.ErrorText = string.Empty;

        //    if (e.ColumnIndex == 0)
        //    {
        //        if (cell.Value == null)
        //        {
        //            cell.ErrorText = "節次名稱不可空白";
        //            return;
        //        }
        //        string value = cell.Value.ToString();
        //        foreach (DataGridViewRow row in dataGridView.Rows)
        //        {
        //            DataGridViewCell compareCell = row.Cells[e.ColumnIndex];
        //            if (compareCell.Value == null) continue;
        //            if (compareCell == cell) continue;
        //            if (compareCell.Value.ToString() != value) continue;
        //            cell.ErrorText = "此節次名稱與已有其它節次使用";
        //            return;
        //        }
        //    }
        //    else if (e.ColumnIndex == 1)
        //    {
        //        if (cell.Value == null)
        //        {
        //            cell.ErrorText = "節次類型不可空白";
        //            return;
        //        }
        //    }
        //    else if (e.ColumnIndex == 2)
        //    {
        //        if (cell.Value == null)
        //        {
        //            cell.ErrorText = "顯示順序不可空白";
        //            return;
        //        }
        //        string value = cell.Value.ToString();
        //        int sort;
        //        if (!int.TryParse(value, out sort))
        //        {
        //            cell.ErrorText = "顯示順序必須填入數字";
        //            return;
        //        }
        //        foreach (DataGridViewRow row in dataGridView.Rows)
        //        {
        //            DataGridViewCell compareCell = row.Cells[e.ColumnIndex];
        //            if (compareCell.Value == null) continue;
        //            if (compareCell == cell) continue;
        //            if (compareCell.Value.ToString() != value) continue;
        //            cell.ErrorText = "此順序與已有其它節次使用";
        //            return;
        //        }
        //    }
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Periods");
            doc.AppendChild(root);

            bool valid = true;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow) continue;
                valid &= ValidateRow(row);

                XmlElement period = doc.CreateElement("Period");
                root.AppendChild(period);
                period.SetAttribute("Name", ""+row.Cells[colPeriodName.Index].Value);
                period.SetAttribute("Type", "" + row.Cells[colType.Index].Value);
                period.SetAttribute("Sort", "" + row.Cells[colOrder.Index].Value);
                period.SetAttribute("Aggregated", "" + row.Cells[colAggregated.Index].Value);
                //foreach (DataGridViewCell cell in row.Cells)
                //{
                //    if (cell.ErrorText == string.Empty)
                //    {
                //        if (cell.ColumnIndex == 0)
                //        {
                //            period.SetAttribute("Name", cell.Value.ToString());
                //        }
                //        else if (cell.ColumnIndex == 1)
                //        {
                //            period.SetAttribute("Type", cell.Value.ToString());
                //        }
                //        else
                //        {
                //            period.SetAttribute("Sort", cell.Value.ToString());
                //        }
                //        continue;
                //    }
                //    valid = false;
                //}
            }
            if (!valid)
            {
                MsgBox.Show("輸入資料有誤，請修正後再行儲存。", "內容錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            string warningMsg = "變更節次名稱將會使得已使用該名稱之資料無法正確顯示於介面上，但並不會影響已儲存資料之正確性！\n是否儲存變更？";
            if (MsgBox.Show(warningMsg, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            DSXmlHelper helper = new DSXmlHelper("Lists");
            helper.AddElement("List");
            helper.AddElement("List", "Content", root.OuterXml, true);
            helper.AddElement("List", "Condition");
            helper.AddElement("List/Condition", "ID", Config.LIST_PERIODS_NUMBER);
            try
            {
                Config.Update(new DSRequest(helper));
            }
            catch (Exception exception)
            {
                MsgBox.Show("更新失敗 :" + exception.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Config.Reset(Config.LIST_PERIODS);
                MsgBox.Show("資料重設成功，新設定已生效。", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MsgBox.Show("資料重設失敗，新設定值將於下次啟動系統後生效!", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateRow(DataGridViewRow row)
        {
            bool pass = true;
            if (row.IsNewRow)
                return true;
            //不得空白
            #region 不得空白
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.OwningColumn == colAggregated) continue;
                if ("" + cell.Value == "")
                {
                    cell.ErrorText = "不得空白";
                    pass &= false;
                    dataGridView.UpdateCellErrorText(cell.ColumnIndex, row.Index);
                }
                else if (cell.ErrorText == "不得空白")
                {
                    cell.ErrorText = "";
                    dataGridView.UpdateCellErrorText(cell.ColumnIndex, row.Index);
                }
            }
            #endregion
            //不得重複
            #region 不得重複
            foreach (DataGridViewRow r in dataGridView.Rows)
            {
                if (r != row)
                {
                    foreach (int index in new int[] { colPeriodName.Index, colOrder.Index })
                    {
                        if ("" + r.Cells[index].Value == "" + row.Cells[index].Value)
                        {
                            row.Cells[index].ErrorText = "不得重複";
                            dataGridView.UpdateCellErrorText(index, row.Index);
                            pass &= false;
                        }
                        else if (row.Cells[index].ErrorText == "不得重複")
                        {
                            row.Cells[index].ErrorText = "";
                            dataGridView.UpdateCellErrorText(index, row.Index);
                        }
                    }
                }
            }
            #endregion
            //順序必須為正整數
            #region 順序必須為正整數
            int integer;
            if (!int.TryParse("" + row.Cells[colOrder.Index].Value, out integer) || integer <= 0)
            {
                row.Cells[colOrder.Index].ErrorText = "必須輸入正整數";
                dataGridView.UpdateCellErrorText(colOrder.Index, row.Index);
                pass &= false;
            }
            else if (row.Cells[colOrder.Index].ErrorText == "必須輸入正整數")
            {
                row.Cells[colOrder.Index].ErrorText = "";
                dataGridView.UpdateCellErrorText(colOrder.Index, row.Index);
            } 
            #endregion
            //節次對照必須為數值
            #region 節次對照必須為數值
            //decimal dec;
            //if (!decimal.TryParse("" + row.Cells[this.colAggregated.Index].Value, out dec) || dec < 0)
            //{
            //    row.Cells[colAggregated.Index].ErrorText = "必須輸入０或正數";
            //    dataGridView.UpdateCellErrorText(colAggregated.Index, row.Index);
            //    pass &= false;
            //}
            //else if (row.Cells[colAggregated.Index].ErrorText == "必須輸入０或正數")
            //{
            //    row.Cells[colAggregated.Index].ErrorText = "";
            //    dataGridView.UpdateCellErrorText(colAggregated.Index, row.Index);
            //}
            #endregion
            return pass;
        }

        private void dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.SelectedCells.Count == 1)
                dataGridView.BeginEdit(true);
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

            e.Cancel = true;
        }

        private void dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView.EndEdit();
        }

        private void dataGridView_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            ValidateRow(dataGridView.Rows[e.RowIndex]);
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == colAggregated.Index)
            //{
            //    foreach (DataGridViewRow row in dataGridView.Rows)
            //    {
            //        if (row.Index == e.RowIndex || row.IsNewRow)
            //            continue;
            //        if ("" + row.Cells[colType.Index].Value == ""+dataGridView.Rows[e.RowIndex].Cells[colType.Index].Value)
            //        {
            //            row.Cells[colAggregated.Index].Value = dataGridView.Rows[e.RowIndex].Cells[colAggregated.Index].Value;
            //            ValidateRow(row);
            //        }
            //    }
            //}
            //if (e.ColumnIndex == colType.Index)
            //{
            //    foreach (DataGridViewRow row in dataGridView.Rows)
            //    {
            //        if (row.Index == e.RowIndex || row.IsNewRow)
            //            continue;
            //        if ("" + row.Cells[colType.Index].Value == ""+dataGridView.Rows[e.RowIndex].Cells[colType.Index].Value)
            //        {
            //            dataGridView.Rows[e.RowIndex].Cells[colAggregated.Index].Value=row.Cells[colAggregated.Index].Value ;
            //            ValidateRow(dataGridView.Rows[e.RowIndex]);
            //            break;
            //        }
            //    }
            //}
        }
    }
}