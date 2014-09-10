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
                #region �ˬd���a����J�ť�
                if ("" + row.Cells[0].Value == "")
                {
                    allPass &= false;
                    row.Cells[0].ErrorText = "���Ĥ��i�ť�";
                    dataGridView.UpdateCellErrorText(0, row.Index);
                }
                else if (row.Cells[0].ErrorText == "���Ĥ��i�ť�")
                {
                    row.Cells[0].ErrorText = "";
                    dataGridView.UpdateCellErrorText(0, row.Index);
                }
                #endregion
                #region �ˬd�̧C���ƿ�J�����Ʀr(�̫�@�泡��)
                double tryPD;
                if (!double.TryParse("" + row.Cells[1].Value, out tryPD) && row.Index < dataGridView.Rows.Count - 2)
                {
                    allPass &= false;
                    row.Cells[1].ErrorText = "������J�ƭ�";
                    dataGridView.UpdateCellErrorText(1, row.Index);
                }
                else if (row.Cells[1].ErrorText == "������J�ƭ�")
                {
                    row.Cells[1].ErrorText = "";
                    dataGridView.UpdateCellErrorText(1, row.Index);
                }
                #endregion
                #region �ˬd���Ƥ�W�@��p
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
                        row.Cells[1].ErrorText = "���ƥ�����e�@���C";
                        dataGridView.UpdateCellErrorText(1, row.Index);
                    }
                    else if (row.Cells[1].ErrorText == "���ƥ�����e�@���C")
                    {
                        row.Cells[1].ErrorText = "";
                        dataGridView.UpdateCellErrorText(1, row.Index);
                    }
                }
                #endregion
                #region �ˬd���Ŀ�J����
                if (levelStrings.Contains(("" + row.Cells[0].Value).Trim() ))
                {
                    allPass &= false;
                    row.Cells[0].ErrorText = "���Ĥ��i����";
                    dataGridView.UpdateCellErrorText(0, row.Index);
                }
                else if (row.Cells[0].ErrorText == "���Ĥ��i����")
                {
                    row.Cells[0].ErrorText = "";
                    dataGridView.UpdateCellErrorText(0, row.Index);
                }
                #endregion
                levelStrings.Add(("" + row.Cells[0].Value).Trim());                
            }

            #region ������
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow)
                    break;
                if (row.Index == 0)
                {
                    row.Cells[1].Style.ForeColor = dataGridView.ForeColor;
                    row.Cells[2].Value = ("" + row.Cells[1].Value).Trim() + "���H�W��" + ("" + row.Cells[0].Value).Trim() + "��";
                }
                else if (row.Index == dataGridView.Rows.Count - 2)
                {
                    row.Cells[1].Style.ForeColor=Color.LightGray;
                    row.Cells[2].Value = "����" + ("" + this.dataGridView.Rows[row.Index - 1].Cells[1].Value).Trim() + "����" + ("" + row.Cells[0].Value).Trim() + "��";
                }
                else
                {
                    row.Cells[1].Style.ForeColor = dataGridView.ForeColor;
                    row.Cells[2].Value = ("" + row.Cells[1].Value).Trim() + "���H�W����" + ("" + this.dataGridView.Rows[row.Index - 1].Cells[1].Value).Trim() + "����" + ("" + row.Cells[0].Value).Trim() + "��";
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
            //        cell.ErrorText = "���ĦW�٤��i�ť�";
            //        return;
            //    }
            //    string value = cell.Value.ToString();
            //    foreach (DataGridViewRow row in dataGridView.Rows)
            //    {
            //        DataGridViewCell compareCell = row.Cells[e.ColumnIndex];
            //        if (compareCell.Value == null) continue;
            //        if (compareCell == cell) continue;
            //        if (compareCell.Value.ToString() != value) continue;
            //        cell.ErrorText = "�����ĦW�ٻP�w���䥦���Ĩϥ�";
            //        return;
            //    }
            //}
            //else if (column == colHigh || column == colLow)
            //{
            //    if (cell.Value == null)
            //    {
            //        cell.ErrorText = "���Ƴ]�w���i�ť�";
            //        return;
            //    }
            //    // �������Ʀr
            //    decimal score;
            //    if (!decimal.TryParse(cell.Value.ToString(), out score))
            //    {
            //        cell.ErrorText = "�������Ʀr";
            //        return;
            //    }

            //    // �ˬd�j�������p���p, �p�������j���j                
            //    if (column == colHigh)
            //    {
            //        decimal compareScore;
            //        DataGridViewCell compareCell = currentRow.Cells[colLow.Name];
            //        if (compareCell.ErrorText != string.Empty && compareCell.ErrorText != "���i�j��̰���") return;
            //        if (!decimal.TryParse(""+compareCell.Value, out compareScore))
            //        {
            //            compareCell.ErrorText = "�������Ʀr";
            //            return;
            //        }
            //        if (compareScore > score)
            //        {
            //            cell.ErrorText = "���i�p��̧C��";
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
            //        if (compareCell.ErrorText != string.Empty && compareCell.ErrorText != "���i�p��̧C��") return;
            //        if (!decimal.TryParse(compareCell.Value.ToString(), out compareScore))
            //        {
            //            compareCell.ErrorText = "�������Ʀr";
            //            return;
            //        }
            //        if (compareScore < score)
            //        {
            //            cell.ErrorText = "���i�j��̰���";
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
                    MsgBox.Show("��s���� :" + exception.Message, "���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    Config.Reset(Config.LIST_DEGREE);
                    MsgBox.Show("��ƭ��]���\�A�s�]�w�w�ͮġC", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MsgBox.Show("��ƭ��]���ѡA�s�]�w�ȱN��U���Ұʨt�Ϋ�ͮ�!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Close();
            }
            //bool valid = true;
            //// �ˬd�O�_�����ץ����
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
            //    MsgBox.Show("��J��Ʀ��~�A�Эץ���A���x�s�C", "���e���~", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //    return;
            //}

            ////��X�̤j�ȩM�̤p��
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
            //        MsgBox.Show("�]�w���ƽd��i�঳�ҿ�| :" + d.ToString(), "���e���~", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
            //    MsgBox.Show("��s���� :" + exception.Message, "���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //try
            //{
            //    Config.Reset(Config.LIST_DEGREE);
            //    MsgBox.Show("��ƭ��]���\�A�s�]�w�w�ͮġC", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //catch
            //{
            //    MsgBox.Show("��ƭ��]���ѡA�s�]�w�ȱN��U���Ұʨt�Ϋ�ͮ�!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    //    //        throw new Exception("�̰����Ƥ����Ʀr");
    //    //    if (!decimal.TryParse(ls, out l))
    //    //        throw new Exception("�̧C���Ƥ����Ʀr");
    //    //    _high = h;
    //    //    _low = l;
    //    //}

    //    //public DegreeInfo(string name, object high, object low)
    //    //{
    //    //    if (high == null || low == null)
    //    //        throw new Exception("���Ƥ��i���ŭ�");

    //    //    decimal h, l;
    //    //    if (!decimal.TryParse(high.ToString(), out h))
    //    //        throw new Exception("���ƥ������Ʀr");

    //    //    if (!decimal.TryParse(low.ToString(), out l))
    //    //        throw new Exception("���ƥ������Ʀr");
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