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
using SmartSchool.StudentRelated.RibbonBars.AttendanceEditor;
using System.Text.RegularExpressions;
using DevComponents.DotNetBar;
using SmartSchool.Feature.Basic;

namespace SmartSchool.Others.Configuration.AbsenceMapping
{
    public partial class AbsenceForm : BaseForm
    {
        private Dictionary<string, AbsenceInfo> _absenceList;

        public AbsenceForm()
        {
            InitializeComponent();

            _absenceList = new Dictionary<string, AbsenceInfo>();
            DSResponse dsrsp = SmartSchool.Feature.Basic.Config.GetAbsenceList();
            DSXmlHelper helper = dsrsp.GetContent();
            foreach (XmlElement element in helper.GetElements("Absence"))
            {
                AbsenceInfo info = new AbsenceInfo(element);
                _absenceList.Add(info.Hotkey.ToUpper(), info);

                int rowIndex = dataGridView.Rows.Add();
                DataGridViewRow row = dataGridView.Rows[rowIndex];
                row.Cells[colName.Name].Value = info.Name;
                row.Cells[colHotKey.Name].Value = info.Hotkey;
                row.Cells[colAbbreviation.Name].Value = info.Abbreviation;
                row.Cells[colNoabsence.Name].Value = info.Noabsence;

                ValidateRow(row);
            }
        }

        //private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    DataGridViewColumn column = dataGridView.Columns[e.ColumnIndex];
        //    DataGridViewRow currentRow = dataGridView.Rows[e.RowIndex];
        //    DataGridViewCell cell = currentRow.Cells[e.ColumnIndex];

        //    cell.ErrorText = string.Empty;

        //    // �q�q����ť�
        //    if (cell.Value == null)
        //    {
        //        cell.ErrorText = "���i�ť�";
        //        return;
        //    }

        //    // �q�q���㭫��
        //    foreach (DataGridViewRow row in dataGridView.Rows)
        //    {
        //        if (row.Index == e.RowIndex) continue;
        //        if (row.Cells[e.ColumnIndex].Value != cell.Value) continue;
        //        cell.ErrorText = "���Ȥw�Q�䥦�����ϥ�";
        //    }

        //    if (column == colName)
        //    {

        //        if (cell.ErrorText == string.Empty)
        //        {
        //            currentRow.Cells[colAbbreviation.Name].Value = cell.Value.ToString().Substring(0, 1);
        //        }
        //    }
        //    else if (column == colAbbreviation)
        //    {

        //    }
        //    else
        //    {
        //        string value = cell.Value.ToString();
        //        if (value.Length > 1)
        //        {
        //            cell.ErrorText = "���i�W�L 1 �Ӧr��";
        //            return;
        //        }

        //        Regex reg = new Regex("[0-9a-zA-Z]{1,}");
        //        Match match = reg.Match(value);
        //        if (!match.Success)
        //        {
        //            cell.ErrorText = "�������^��μƦr";
        //            return;
        //        }

        //        cell.Value = value.ToLower(); ;
        //    }
        //}

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("AbsenceList");
            doc.AppendChild(root);

            bool valid = true;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow) continue;

                valid &= ValidateRow(row);


                XmlElement absence = doc.CreateElement("Absence");
                root.AppendChild(absence);
                absence.SetAttribute("Name", ""+row.Cells[colName.Index].Value);
                absence.SetAttribute("Abbreviation", "" + row.Cells[colAbbreviation.Index].Value);
                absence.SetAttribute("HotKey", "" + row.Cells[colHotKey.Index].Value);
                absence.SetAttribute("Noabsence", "" + row.Cells[colNoabsence.Index].Value);

            }
            if (!valid)
            {
                MsgBox.Show("��J��Ʀ��~�A�Эץ���A���x�s�C", "���e���~", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            string warningMsg = "�ܧ���m���O�W�ٱN�|�ϱo�w�ϥθӦW�٤���ƵL�k���T��ܩ󤶭��W�A���ä��|�v�T�w�x�s��Ƥ����T�ʡI\n�O�_�x�s�ܧ�H";
            if (MsgBox.Show(warningMsg, "ĵ�i", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            DSXmlHelper helper = new DSXmlHelper("Lists");
            helper.AddElement("List");
            helper.AddElement("List", "Content", root.OuterXml, true);
            helper.AddElement("List", "Condition");
            helper.AddElement("List/Condition", "ID", Config.LIST_ABSENCE_NUMBER);
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
                Config.Reset(Config.LIST_ABSENCE);
                MsgBox.Show("��ƭ��]���\�A�s�]�w�w�ͮġC", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MsgBox.Show("��ƭ��]���ѡA�s�]�w�ȱN��U���Ұʨt�Ϋ�ͮ�!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

        private void dataGridView_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            ValidateRow(dataGridView.Rows[e.RowIndex]);
        }

        private bool ValidateRow(DataGridViewRow row)
        {
            bool pass = true;
            if (row.IsNewRow)
                return true;
            //�����\�ť�
            #region �����\�ť�
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell is System.Windows.Forms.DataGridViewCheckBoxCell)
                    continue;

                if ("" + cell.Value == "")
                {
                    cell.ErrorText = "�����\�ť�";
                    dataGridView.UpdateCellErrorText(cell.ColumnIndex, row.Index);
                    pass &= false;
                }
                else if (cell.ErrorText == "�����\�ť�")
                {
                    cell.ErrorText = "";
                    dataGridView.UpdateCellErrorText(cell.ColumnIndex, row.Index);
                }
            } 
            #endregion
            //���o����(�W�١@�Y�g�@����)
            #region ���o����(�W�١@�Y�g�@����)
            foreach (DataGridViewRow r in dataGridView.Rows)
            {
                if (r != row)
                {
                    foreach (int index in new int[] { colName.Index, colHotKey.Index, colAbbreviation.Index })
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
            return pass;
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.SelectedCells.Count == 1)
                dataGridView.BeginEdit(true);
        }

        private void dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView.EndEdit();
        }
    }
}