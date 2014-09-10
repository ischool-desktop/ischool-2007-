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
        //            cell.ErrorText = "�`���W�٤��i�ť�";
        //            return;
        //        }
        //        string value = cell.Value.ToString();
        //        foreach (DataGridViewRow row in dataGridView.Rows)
        //        {
        //            DataGridViewCell compareCell = row.Cells[e.ColumnIndex];
        //            if (compareCell.Value == null) continue;
        //            if (compareCell == cell) continue;
        //            if (compareCell.Value.ToString() != value) continue;
        //            cell.ErrorText = "���`���W�ٻP�w���䥦�`���ϥ�";
        //            return;
        //        }
        //    }
        //    else if (e.ColumnIndex == 1)
        //    {
        //        if (cell.Value == null)
        //        {
        //            cell.ErrorText = "�`���������i�ť�";
        //            return;
        //        }
        //    }
        //    else if (e.ColumnIndex == 2)
        //    {
        //        if (cell.Value == null)
        //        {
        //            cell.ErrorText = "��ܶ��Ǥ��i�ť�";
        //            return;
        //        }
        //        string value = cell.Value.ToString();
        //        int sort;
        //        if (!int.TryParse(value, out sort))
        //        {
        //            cell.ErrorText = "��ܶ��ǥ�����J�Ʀr";
        //            return;
        //        }
        //        foreach (DataGridViewRow row in dataGridView.Rows)
        //        {
        //            DataGridViewCell compareCell = row.Cells[e.ColumnIndex];
        //            if (compareCell.Value == null) continue;
        //            if (compareCell == cell) continue;
        //            if (compareCell.Value.ToString() != value) continue;
        //            cell.ErrorText = "�����ǻP�w���䥦�`���ϥ�";
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
                MsgBox.Show("��J��Ʀ��~�A�Эץ���A���x�s�C", "���e���~", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            string warningMsg = "�ܧ�`���W�ٱN�|�ϱo�w�ϥθӦW�٤���ƵL�k���T��ܩ󤶭��W�A���ä��|�v�T�w�x�s��Ƥ����T�ʡI\n�O�_�x�s�ܧ�H";
            if (MsgBox.Show(warningMsg, "ĵ�i", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
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
                MsgBox.Show("��s���� :" + exception.Message, "���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Config.Reset(Config.LIST_PERIODS);
                MsgBox.Show("��ƭ��]���\�A�s�]�w�w�ͮġC", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MsgBox.Show("��ƭ��]���ѡA�s�]�w�ȱN��U���Ұʨt�Ϋ�ͮ�!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //���o�ť�
            #region ���o�ť�
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.OwningColumn == colAggregated) continue;
                if ("" + cell.Value == "")
                {
                    cell.ErrorText = "���o�ť�";
                    pass &= false;
                    dataGridView.UpdateCellErrorText(cell.ColumnIndex, row.Index);
                }
                else if (cell.ErrorText == "���o�ť�")
                {
                    cell.ErrorText = "";
                    dataGridView.UpdateCellErrorText(cell.ColumnIndex, row.Index);
                }
            }
            #endregion
            //���o����
            #region ���o����
            foreach (DataGridViewRow r in dataGridView.Rows)
            {
                if (r != row)
                {
                    foreach (int index in new int[] { colPeriodName.Index, colOrder.Index })
                    {
                        if ("" + r.Cells[index].Value == "" + row.Cells[index].Value)
                        {
                            row.Cells[index].ErrorText = "���o����";
                            dataGridView.UpdateCellErrorText(index, row.Index);
                            pass &= false;
                        }
                        else if (row.Cells[index].ErrorText == "���o����")
                        {
                            row.Cells[index].ErrorText = "";
                            dataGridView.UpdateCellErrorText(index, row.Index);
                        }
                    }
                }
            }
            #endregion
            //���ǥ����������
            #region ���ǥ����������
            int integer;
            if (!int.TryParse("" + row.Cells[colOrder.Index].Value, out integer) || integer <= 0)
            {
                row.Cells[colOrder.Index].ErrorText = "������J�����";
                dataGridView.UpdateCellErrorText(colOrder.Index, row.Index);
                pass &= false;
            }
            else if (row.Cells[colOrder.Index].ErrorText == "������J�����")
            {
                row.Cells[colOrder.Index].ErrorText = "";
                dataGridView.UpdateCellErrorText(colOrder.Index, row.Index);
            } 
            #endregion
            //�`����ӥ������ƭ�
            #region �`����ӥ������ƭ�
            //decimal dec;
            //if (!decimal.TryParse("" + row.Cells[this.colAggregated.Index].Value, out dec) || dec < 0)
            //{
            //    row.Cells[colAggregated.Index].ErrorText = "������J���Υ���";
            //    dataGridView.UpdateCellErrorText(colAggregated.Index, row.Index);
            //    pass &= false;
            //}
            //else if (row.Cells[colAggregated.Index].ErrorText == "������J���Υ���")
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