using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;

namespace SmartSchool.ClassRelated.RibbonBars.Reports
{
    public partial class SelectTypeForm : BaseForm
    {
        private string _preferenceElementName;
        private BackgroundWorker _BGWAbsenceAndPeriodList;

        private List<string> typeList = new List<string>();
        private List<string> absenceList = new List<string>();

        bool valueOnChange=false;

        public SelectTypeForm(string name)
        {
            InitializeComponent();

            _preferenceElementName = name;

            _BGWAbsenceAndPeriodList = new BackgroundWorker();
            _BGWAbsenceAndPeriodList.DoWork += new DoWorkEventHandler(_BGWAbsenceAndPeriodList_DoWork);
            _BGWAbsenceAndPeriodList.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BGWAbsenceAndPeriodList_RunWorkerCompleted);
            _BGWAbsenceAndPeriodList.RunWorkerAsync();
        }

        void _BGWAbsenceAndPeriodList_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBox1.Visible = false;

            System.Windows.Forms.DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn();
            colName.HeaderText = "�`������";
            colName.MinimumWidth = 70;
            colName.Name = "colName";
            colName.ReadOnly = true;
            colName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            colName.Width = 70;
            this.dataGridViewX1.Columns.Add(colName);

            foreach (string absence in absenceList)
            {
                System.Windows.Forms.DataGridViewCheckBoxColumn newCol=new DataGridViewCheckBoxColumn();
                newCol.HeaderText = absence;
                newCol.Width = 55;
                newCol.ReadOnly = false;
                newCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
                newCol.Tag = absence ;
                newCol.ValueType=typeof(bool);
                this.dataGridViewX1.Columns.Add(newCol);
            }
            foreach (string type in typeList)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewX1, type);
                row.Tag = type;
                dataGridViewX1.Rows.Add(row);
            }


            #region Ū���C�L�]�w Preference
            valueOnChange = true;
            XmlElement config = CurrentUser.Instance.Preference[_preferenceElementName];
            if (config != null)
            {
                #region �w���]�w�ɫh�N�]�w�ɤ��e��^�e���W
                foreach (XmlElement type in config.SelectNodes("Type"))
                {
                    string typeName = type.GetAttribute("Text");
                    foreach (DataGridViewRow row in dataGridViewX1.Rows)
                    {
                        if (typeName == ("" + row.Tag))
                        {
                            foreach (XmlElement absence in type.SelectNodes("Absence"))
                            {
                                string absenceName = absence.GetAttribute("Text");
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    if (cell.OwningColumn is DataGridViewCheckBoxColumn && ("" + cell.OwningColumn.Tag) == absenceName)
                                    {
                                        cell.Value = true;
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region ���ͪťճ]�w��
                config = new XmlDocument().CreateElement(_preferenceElementName);
                CurrentUser.Instance.Preference[_preferenceElementName] = config;
                #endregion
            }
            valueOnChange = false;

            #endregion
        }

        void _BGWAbsenceAndPeriodList_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (XmlElement var in SmartSchool.Feature.Basic.Config.GetPeriodList().GetContent().GetElements("Period"))
            {
                if (!typeList.Contains(var.GetAttribute("Type")))
                    typeList.Add(var.GetAttribute("Type"));
            }

            foreach (XmlElement var in SmartSchool.Feature.Basic.Config.GetAbsenceList().GetContent().GetElements("Absence"))
            {
                if (!absenceList.Contains(var.GetAttribute("Name")))
                    absenceList.Add(var.GetAttribute("Name"));
            }
        }

        //private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        //{
        //    TreeNode checkedNode = e.Node;

        //    if (typeList.Contains(checkedNode.Text) && checkedNode.Parent == null)
        //    {
        //        foreach (TreeNode subnode in checkedNode.Nodes)
        //        {
        //            subnode.Checked = checkedNode.Checked;
        //        }
        //    }
        //}

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (!CheckColumnNumber())
                return;

            #region ��s�C�L�]�w Preference

            XmlElement config = CurrentUser.Instance.Preference[_preferenceElementName];

            if (config == null)
            {
                config = new XmlDocument().CreateElement(_preferenceElementName);
            }

            config.RemoveAll();

            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                bool needToAppend = false;
                XmlElement type = config.OwnerDocument.CreateElement("Type");
                type.SetAttribute("Text", "" + row.Tag);
                foreach (DataGridViewCell cell in row.Cells)
                {
                    XmlElement absence = config.OwnerDocument.CreateElement("Absence");
                    absence.SetAttribute("Text", ""+cell.OwningColumn.Tag);
                    if (cell.Value is bool && ((bool)cell.Value))
                    {
                        needToAppend = true;
                        type.AppendChild(absence);
                    }
                }
                if(needToAppend)
                    config.AppendChild(type);
            }

            //foreach (TreeNode typeNode in treeView1.Nodes)
            //{
            //    XmlElement type = config.OwnerDocument.CreateElement("Type");
            //    type.SetAttribute("Text", typeNode.Text);
            //    type.SetAttribute("Checked", typeNode.Checked.ToString());

            //    foreach (TreeNode absenceNode in typeNode.Nodes)
            //    {
            //        if (absenceNode.Checked == true)
            //        {
            //            XmlElement absence = config.OwnerDocument.CreateElement("Absence");
            //            absence.SetAttribute("Text", absenceNode.Text);
            //            type.AppendChild(absence);
            //        }
            //    }
            //    config.AppendChild(type);
            //}


            CurrentUser.Instance.Preference[_preferenceElementName] = config;

            #endregion

            this.Close();
        }

        internal bool CheckColumnNumber()
        {
            int limit = 253;
            int columnNumber = 0;
            int block = 9;

            //foreach (TreeNode type in treeView1.Nodes)
            //{
            //    foreach (TreeNode var in type.Nodes)
            //    {
            //        if (var.Checked == true)
            //            columnNumber++;
            //    }
            //}
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value is bool &&((bool)cell.Value ))
                        columnNumber++;
                }
            }

            if (columnNumber * block > limit)
            {
                MsgBox.Show("�z�ҿ�ܪ����O�W�X Excel ���̤j���A�д�ֳ������O");
                return false;
            }
            else
                return true;
        }

        private void dataGridViewX1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        //private void dataGridViewX1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex < 0 || e.RowIndex < 0)
        //        return;
        //    if ( valueOnChange)
        //        return;
        //    else
        //        valueOnChange = true;
        //    DataGridViewCell checkedCell=dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex];
        //    foreach (DataGridViewCell  cell in dataGridViewX1.SelectedCells)
        //    {
        //        if (cell.OwningColumn is DataGridViewCheckBoxColumn && cell != checkedCell)
        //            cell.Value = checkedCell.Value;
        //    }
        //    valueOnChange = false;
        //}
    }
}