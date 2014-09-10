using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature.Basic;
using System.Xml;
using System.Collections;

namespace SmartSchool.Others.Configuration.DegreeMapping
{
    public partial class DegreeForm : BaseForm
    {
        //private List<DegreeInfo> _list;
        private int _SelectedRowIndex;

        public DegreeForm()
        {
            //_list = new List<DegreeInfo>();
            InitializeComponent();            

            DSResponse dsrsp = Config.GetDegreeList();
            DSXmlHelper helper = dsrsp.GetContent();
            foreach (XmlElement element in helper.GetElements("Degree"))
            {
                int rowIndex = dataGridView.Rows.Add();
                DataGridViewRow row = dataGridView.Rows[rowIndex];
                row.Cells[colName.Name].Value = element.GetAttribute("Name");
                row.Cells[colLow.Name].Value = element.GetAttribute("Low");
            }
            CheckAndReflash();
        }

        private bool CheckAndReflash()
        {
            dataGridView.EndEdit();
            bool allPass = true;
            List<string> levelStrings = new List<string>();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow)
                    break;
                #region 檢查等地欄位輸入空白
                if ("" + row.Cells[0].Value == "")
                {
                    allPass &= false;
                    row.Cells[0].ErrorText = "等第不可空白";
                    dataGridView.UpdateCellErrorText(0, row.Index);
                }
                else if (row.Cells[0].ErrorText == "等第不可空白")
                {
                    row.Cells[0].ErrorText = "";
                    dataGridView.UpdateCellErrorText(0, row.Index);
                }
                #endregion
                #region 檢查最低分數輸入不為數字(最後一行部算)
                double tryPD;
                if (!double.TryParse("" + row.Cells[1].Value, out tryPD) && row.Index < dataGridView.Rows.Count - 2)
                {
                    allPass &= false;
                    row.Cells[1].ErrorText = "必須輸入數值";
                    dataGridView.UpdateCellErrorText(1, row.Index);
                }
                else if (row.Cells[1].ErrorText == "必須輸入數值")
                {
                    row.Cells[1].ErrorText = "";
                    dataGridView.UpdateCellErrorText(1, row.Index);
                }
                #endregion
                #region 檢查分數比上一行小
                double d1, d2;
                if (row.Index > 0 && row.Index < dataGridView.Rows.Count - 1)
                {
                    if (!double.TryParse("" + this.dataGridView.Rows[row.Index - 1].Cells[1].Value, out d1))
                        d1 = double.MaxValue;
                    if (!double.TryParse("" + row.Cells[1].Value, out d2))
                        d2 = double.MinValue;
                    if (d1<=d2)
                    {
                        allPass &= false;
                        row.Cells[1].ErrorText = "分數必須比前一項低";
                        dataGridView.UpdateCellErrorText(1, row.Index);
                    }
                    else if (row.Cells[1].ErrorText == "分數必須比前一項低")
                    {
                        row.Cells[1].ErrorText = "";
                        dataGridView.UpdateCellErrorText(1, row.Index);
                    }
                }
                #endregion
                #region 檢查等第輸入重複
                if (levelStrings.Contains(("" + row.Cells[0].Value).Trim() ))
                {
                    allPass &= false;
                    row.Cells[0].ErrorText = "等第不可重複";
                    dataGridView.UpdateCellErrorText(0, row.Index);
                }
                else if (row.Cells[0].ErrorText == "等第不可重複")
                {
                    row.Cells[0].ErrorText = "";
                    dataGridView.UpdateCellErrorText(0, row.Index);
                }
                #endregion
                levelStrings.Add(("" + row.Cells[0].Value).Trim());                
            }

            #region 重整資料
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow)
                    break;
                if (row.Index == 0)
                {
                    row.Cells[1].Style.ForeColor = dataGridView.ForeColor;
                    row.Cells[2].Value = ("" + row.Cells[1].Value).Trim() + "分以上為" + ("" + row.Cells[0].Value).Trim() + "等";
                }
                else if (row.Index == dataGridView.Rows.Count - 2)
                {
                    row.Cells[1].Style.ForeColor=Color.LightGray;
                    row.Cells[2].Value = "未滿" + ("" + this.dataGridView.Rows[row.Index - 1].Cells[1].Value).Trim() + "分為" + ("" + row.Cells[0].Value).Trim() + "等";
                }
                else
                {
                    row.Cells[1].Style.ForeColor = dataGridView.ForeColor;
                    row.Cells[2].Value = ("" + row.Cells[1].Value).Trim() + "分以上未滿" + ("" + this.dataGridView.Rows[row.Index - 1].Cells[1].Value).Trim() + "分為" + ("" + row.Cells[0].Value).Trim() + "等";
                }

            } 
            #endregion

            return allPass;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CheckAndReflash();
            //DataGridViewColumn column = dataGridView.Columns[e.ColumnIndex];
            //DataGridViewRow currentRow = dataGridView.Rows[e.RowIndex];
            //DataGridViewCell cell = currentRow.Cells[e.ColumnIndex];
            //cell.ErrorText = string.Empty;

            //if (column == colName)
            //{
            //    if (cell.Value == null)
            //    {
            //        cell.ErrorText = "等第名稱不可空白";
            //        return;
            //    }
            //    string value = cell.Value.ToString();
            //    foreach (DataGridViewRow row in dataGridView.Rows)
            //    {
            //        DataGridViewCell compareCell = row.Cells[e.ColumnIndex];
            //        if (compareCell.Value == null) continue;
            //        if (compareCell == cell) continue;
            //        if (compareCell.Value.ToString() != value) continue;
            //        cell.ErrorText = "此等第名稱與已有其它等第使用";
            //        return;
            //    }
            //}
            //else if (column == colHigh || column == colLow)
            //{
            //    if (cell.Value == null)
            //    {
            //        cell.ErrorText = "分數設定不可空白";
            //        return;
            //    }
            //    // 必須為數字
            //    decimal score;
            //    if (!decimal.TryParse(cell.Value.ToString(), out score))
            //    {
            //        cell.ErrorText = "必須為數字";
            //        return;
            //    }

            //    // 檢查大的不能比小的小, 小的不能比大的大                
            //    if (column == colHigh)
            //    {
            //        decimal compareScore;
            //        DataGridViewCell compareCell = currentRow.Cells[colLow.Name];
            //        if (compareCell.ErrorText != string.Empty && compareCell.ErrorText != "不可大於最高分") return;
            //        if (!decimal.TryParse(""+compareCell.Value, out compareScore))
            //        {
            //            compareCell.ErrorText = "必須為數字";
            //            return;
            //        }
            //        if (compareScore > score)
            //        {
            //            cell.ErrorText = "不可小於最低分";
            //            return;
            //        }
            //        else 
            //        {
            //            compareCell.ErrorText = string.Empty;
            //        }
            //    }
            //    else if (column == colLow)
            //    {
            //        decimal compareScore;
            //        DataGridViewCell compareCell = currentRow.Cells[colHigh.Name];
            //        if (compareCell.ErrorText != string.Empty && compareCell.ErrorText != "不可小於最低分") return;
            //        if (!decimal.TryParse(compareCell.Value.ToString(), out compareScore))
            //        {
            //            compareCell.ErrorText = "必須為數字";
            //            return;
            //        }
            //        if (compareScore < score)
            //        {
            //            cell.ErrorText = "不可大於最高分";
            //            return;
            //        }
            //        else
            //        {
            //            compareCell.ErrorText = string.Empty;
            //        }
            //    }
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count - 2 >= 0)
                dataGridView.Rows[dataGridView.Rows.Count -2].Cells[1].Value = "";
            if (CheckAndReflash())
            {
                XmlDocument doc = new XmlDocument();
                XmlElement root = doc.CreateElement("DegreeList");
                doc.AppendChild(root);



                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.IsNewRow)
                        break;
                    XmlElement element = doc.CreateElement("Degree");
                    root.AppendChild(element);
                    element.SetAttribute("Name", (""+row.Cells[0].Value).Trim());
                    element.SetAttribute("Low", ("" + row.Cells[1].Value).Trim());
                }
                //foreach (DegreeInfo info in _list)
                //{
                //    XmlElement element = doc.CreateElement("Degree");
                //    root.AppendChild(element);
                //    element.SetAttribute("Name", info.Name);
                //    element.SetAttribute("High", info.High.ToString());
                //    element.SetAttribute("Low", info.Low.ToString());
                //}

                DSXmlHelper helper = new DSXmlHelper("Lists");
                helper.AddElement("List");
                helper.AddElement("List", "Content", root.OuterXml, true);
                helper.AddElement("List", "Condition");
                helper.AddElement("List/Condition", "Name", Config.LIST_DEGREE_NAME);
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
                    Config.Reset(Config.LIST_DEGREE);
                    MsgBox.Show("資料重設成功，新設定已生效。", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MsgBox.Show("資料重設失敗，新設定值將於下次啟動系統後生效!", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Close();
            }
            //bool valid = true;
            //// 檢查是否有未修正欄位
            //foreach (DataGridViewRow row in dataGridView.Rows)
            //{
            //    if (row.IsNewRow) continue;
            //    foreach (DataGridViewCell cell in row.Cells)
            //    {
            //        if (cell.ErrorText != string.Empty)
            //        {
            //            valid = false;
            //            break;
            //        }
            //    }
            //}
            //if (!valid)
            //{
            //    MsgBox.Show("輸入資料有誤，請修正後再行儲存。", "內容錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //    return;
            //}

            ////找出最大值和最小值
            //decimal max = decimal.MinValue, min = decimal.MaxValue;
            //_list = new List<DegreeInfo>();
            //foreach (DataGridViewRow row in dataGridView.Rows)
            //{
            //    if (row.IsNewRow) continue;
            //    string ns = row.Cells[colLow.Name].Value.ToString();
            //    string name = row.Cells[colName.Name].Value.ToString();
            //    decimal nd, xd;
            //    if (decimal.TryParse(ns, out  nd))
            //    {
            //        if (min == decimal.MaxValue)
            //            min = nd;
            //        else
            //            min = Math.Min(min, nd);
            //    }
            //    if (decimal.TryParse(xs, out  xd))
            //    {
            //        if (max == decimal.MinValue)
            //            max = xd;
            //        else
            //            max = Math.Max(max, xd);
            //    }
            //    _list.Add(new DegreeInfo(name, xd, nd));
            //}

            //for (decimal d = min; d < max; d++)
            //{
            //    bool belong = false;
            //    foreach (DegreeInfo info in _list)
            //    {
            //        if (info.BelongTo(d))
            //        {
            //            belong = true;
            //            break;
            //        }
            //    }
            //    if (!belong)
            //    {
            //        MsgBox.Show("設定分數範圍可能有所遺漏 :" + d.ToString(), "內容錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //        valid = false;
            //        return;
            //    }
            //}

            //XmlDocument doc = new XmlDocument();
            //XmlElement root = doc.CreateElement("DegreeList");
            //doc.AppendChild(root);

            //foreach (DegreeInfo info in _list)
            //{
            //    XmlElement element = doc.CreateElement("Degree");
            //    root.AppendChild(element);
            //    element.SetAttribute("Name", info.Name);
            //    element.SetAttribute("High", info.High.ToString());
            //    element.SetAttribute("Low", info.Low.ToString());
            //}

            //DSXmlHelper helper = new DSXmlHelper("Lists");
            //helper.AddElement("List");
            //helper.AddElement("List", "Content", root.OuterXml, true);
            //helper.AddElement("List", "Condition");
            //helper.AddElement("List/Condition", "ID", Config.LIST_DEGREE_NUMBER);
            //try
            //{
            //    Config.Update(new DSRequest(helper));
            //}
            //catch (Exception exception)
            //{
            //    MsgBox.Show("更新失敗 :" + exception.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //try
            //{
            //    Config.Reset(Config.LIST_DEGREE);
            //    MsgBox.Show("資料重設成功，新設定已生效。", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //catch
            //{
            //    MsgBox.Show("資料重設失敗，新設定值將於下次啟動系統後生效!", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //this.Close();
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.SelectedCells.Count == 1)
                dataGridView.BeginEdit(true);
        }

        private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CheckAndReflash();
        }

        private void dataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CheckAndReflash();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dataGridView.Rows.Insert(_SelectedRowIndex, new DataGridViewRow());
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (_SelectedRowIndex >= 0 && dataGridView.Rows.Count - 1 > _SelectedRowIndex)
                dataGridView.Rows.RemoveAt(_SelectedRowIndex);
        }

        private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex < 0 && e.Button == MouseButtons.Right)
            {
                dataGridView.EndEdit();
                _SelectedRowIndex = e.RowIndex;
                foreach (DataGridViewRow var in dataGridView.SelectedRows)
                {
                    if (var.Index != _SelectedRowIndex)
                        var.Selected = false;
                }
                dataGridView.Rows[_SelectedRowIndex].Selected = true;
                contextMenuStrip1.Show(dataGridView, dataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location);
            }
        }
    }

    //public class DegreeInfo
    //{
    //    private string _name;

    //    public string Name
    //    {
    //        get { return _name; }
    //        set { _name = value; }
    //    }

    //    private decimal _high;

    //    public decimal High
    //    {
    //        get { return _high; }
    //        set { _high = value; }
    //    }

    //    private decimal _low;

    //    public decimal Low
    //    {
    //        get { return _low; }
    //        set { _low = value; }
    //    }

    //    //public DegreeInfo(XmlElement element)
    //    //{
    //    //    string hs = element.GetAttribute("High");
    //    //    string ls = element.GetAttribute("Low");
    //    //    _name = element.GetAttribute("Name");

    //    //    decimal h, l;
    //    //    if (!decimal.TryParse(hs, out h))
    //    //        throw new Exception("最高分數不為數字");
    //    //    if (!decimal.TryParse(ls, out l))
    //    //        throw new Exception("最低分數不為數字");
    //    //    _high = h;
    //    //    _low = l;
    //    //}

    //    //public DegreeInfo(string name, object high, object low)
    //    //{
    //    //    if (high == null || low == null)
    //    //        throw new Exception("分數不可為空值");

    //    //    decimal h, l;
    //    //    if (!decimal.TryParse(high.ToString(), out h))
    //    //        throw new Exception("分數必須為數字");

    //    //    if (!decimal.TryParse(low.ToString(), out l))
    //    //        throw new Exception("分數必須為數字");
    //    //    _name = name;
    //    //    _high = h;
    //    //    _low = l;
    //    //}

    //    //public DegreeInfo(string name, decimal high, decimal low)
    //    //{
    //    //    _name = name;
    //    //    _high = high;
    //    //    _low = low;
    //    //}

    //    //public bool Between(decimal score)
    //    //{
    //    //    return (score < _high && score > _low);
    //    //}

    //    //public bool BelongTo(decimal score)
    //    //{
    //    //    return (score < _high && score >= _low);
    //    //}
    //}
}