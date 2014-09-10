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
using DevComponents.DotNetBar.Controls;
using System.Threading;

namespace SmartSchool.StudentRelated.Divider
{
    partial class SelectTypeForm : BaseForm, SmartSchool.StudentRelated.Divider.IAttendanceItemShown
    {
        private string _preferenceElementName;
        private BackgroundWorker _BGWAbsenceAndPeriodList;
        private ManualResetEvent _Wait = new ManualResetEvent(false);
        private List<string> typeList = new List<string>();
        private List<string> absenceList = new List<string>();

        bool valueOnChange = false;

        public SelectTypeForm()
        {
            _preferenceElementName = "StudentAttendanceViewSetting";
            _BGWAbsenceAndPeriodList = new BackgroundWorker();
            _BGWAbsenceAndPeriodList.DoWork += new DoWorkEventHandler(_BGWAbsenceAndPeriodList_DoWork);
            _BGWAbsenceAndPeriodList.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BGWAbsenceAndPeriodList_RunWorkerCompleted);
            _BGWAbsenceAndPeriodList.RunWorkerAsync();

            InitializeComponent();

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

            foreach ( string absence in absenceList )
            {
                System.Windows.Forms.DataGridViewCheckBoxColumn newCol = new DataGridViewCheckBoxColumn();
                newCol.HeaderText = absence;
                newCol.Width = 55;
                newCol.ReadOnly = false;
                newCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                newCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
                newCol.Tag = absence;
                newCol.ValueType = typeof(bool);
                this.dataGridViewX1.Columns.Add(newCol);
            }
            foreach ( string type in typeList )
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewX1, type);
                row.Tag = type;
                dataGridViewX1.Rows.Add(row);
            }


            #region Ū���C�L�]�w Preference
            valueOnChange = true;
            XmlElement config = CurrentUser.Instance.Preference[_preferenceElementName];
            if ( config != null )
            {
                foreach ( DataGridViewRow row in dataGridViewX1.Rows )
                    foreach ( DataGridViewCell cell in row.Cells )
                        if ( cell.ValueType == typeof(bool) )
                            cell.Value = false;
                #region �w���]�w�ɫh�N�]�w�ɤ��e��^�e���W
                foreach ( XmlElement type in config.SelectNodes("Type") )
                {
                    string typeName = type.GetAttribute("Text");
                    foreach ( DataGridViewRow row in dataGridViewX1.Rows )
                    {
                        if ( typeName == ( "" + row.Tag ) )
                        {
                            foreach ( XmlElement absence in type.SelectNodes("Absence") )
                            {
                                string absenceName = absence.GetAttribute("Text");
                                foreach ( DataGridViewCell cell in row.Cells )
                                {
                                    if ( cell.OwningColumn is DataGridViewCheckBoxColumn && ( "" + cell.OwningColumn.Tag ) == absenceName )
                                    {
                                        cell.Value = true;
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
                foreach ( CheckBoxX var in new CheckBoxX[] { ���~�����վǥ�, ��Ǿǥ�, �w�R���ǥ�, ���ץ�, �@��� } )
                {
                    var.Checked = ( config.SelectSingleNode(var.Text + "[@Value='" + true + "']") != null );
                }
                #endregion
            }
            else
            {
                #region ���ͳ]�w��
                config = new XmlDocument().CreateElement(_preferenceElementName);
                foreach ( DataGridViewRow row in dataGridViewX1.Rows )
                {
                    XmlElement type = config.OwnerDocument.CreateElement("Type");
                    type.SetAttribute("Text", "" + row.Tag);
                    bool needToAppend = false;
                    foreach ( DataGridViewCell cell in row.Cells )
                    {
                        if ( cell.ValueType ==typeof( bool ))
                        {
                            XmlElement absence = config.OwnerDocument.CreateElement("Absence");
                            absence.SetAttribute("Text", "" + cell.OwningColumn.Tag);
                            cell.Value = true;
                            needToAppend = true;
                            type.AppendChild(absence);
                        }
                    }
                    if ( needToAppend )
                        config.AppendChild(type);
                }
                foreach ( CheckBoxX var in new CheckBoxX[] { ���~�����վǥ�, ��Ǿǥ�, �w�R���ǥ�, ���ץ�, �@��� } )
                {
                    XmlElement chk = config.OwnerDocument.CreateElement(var.Text);
                    chk.SetAttribute("Value", "" + var.Checked);
                    config.AppendChild(chk);
                }
                CurrentUser.Instance.Preference[_preferenceElementName] = config;
                #endregion
            }
            valueOnChange = false;

            #endregion
        }

        void _BGWAbsenceAndPeriodList_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                foreach ( XmlElement var in SmartSchool.Feature.Basic.Config.GetPeriodList().GetContent().GetElements("Period") )
                {
                    if ( !typeList.Contains(var.GetAttribute("Type")) )
                        typeList.Add(var.GetAttribute("Type"));
                }

                foreach ( XmlElement var in SmartSchool.Feature.Basic.Config.GetAbsenceList().GetContent().GetElements("Absence") )
                {
                    if ( !absenceList.Contains(var.GetAttribute("Name")) )
                        absenceList.Add(var.GetAttribute("Name"));
                }
            }
            catch { }
            finally { _Wait.Set(); }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

            #region ��s�]�w Preference

            XmlElement config = CurrentUser.Instance.Preference[_preferenceElementName];

            if ( config == null )
            {
                config = new XmlDocument().CreateElement(_preferenceElementName);
            }

            config.RemoveAll();

            foreach ( DataGridViewRow row in dataGridViewX1.Rows )
            {
                bool needToAppend = false;
                XmlElement type = config.OwnerDocument.CreateElement("Type");
                type.SetAttribute("Text", "" + row.Tag);
                foreach ( DataGridViewCell cell in row.Cells )
                {
                    XmlElement absence = config.OwnerDocument.CreateElement("Absence");
                    absence.SetAttribute("Text", "" + cell.OwningColumn.Tag);
                    if ( cell.ValueType ==typeof( bool ) && ( (bool)cell.Value ) )
                    {
                        needToAppend = true;
                        type.AppendChild(absence);
                    }
                }
                if ( needToAppend )
                    config.AppendChild(type);
            }
            foreach ( CheckBoxX var in new CheckBoxX[] { ���~�����վǥ�, ��Ǿǥ�, �w�R���ǥ�, ���ץ�, �@��� } )
            {
                XmlElement chk = config.OwnerDocument.CreateElement(var.Text);
                chk.SetAttribute("Value", "" + var.Checked);
                config.AppendChild(chk);
            }
            CurrentUser.Instance.Preference[_preferenceElementName] = config;

            #endregion
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dataGridViewX1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SelectTypeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            valueOnChange = true;
            XmlElement config = CurrentUser.Instance.Preference[_preferenceElementName];
            if ( config != null )
            {
                foreach ( DataGridViewRow row in dataGridViewX1.Rows )
                    foreach ( DataGridViewCell cell in row.Cells )
                        if ( cell.ValueType == typeof(bool) )
                            cell.Value = false;
                #region �w���]�w�ɫh�N�]�w�ɤ��e��^�e���W
                foreach ( XmlElement type in config.SelectNodes("Type") )
                {
                    string typeName = type.GetAttribute("Text");
                    foreach ( DataGridViewRow row in dataGridViewX1.Rows )
                    {
                        if ( typeName == ( "" + row.Tag ) )
                        {
                            foreach ( XmlElement absence in type.SelectNodes("Absence") )
                            {
                                string absenceName = absence.GetAttribute("Text");
                                foreach ( DataGridViewCell cell in row.Cells )
                                {
                                    if ( cell.OwningColumn is DataGridViewCheckBoxColumn && ( "" + cell.OwningColumn.Tag ) == absenceName )
                                    {
                                        cell.Value = true;
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
                foreach ( CheckBoxX var in new CheckBoxX[] { ���~�����վǥ�, ��Ǿǥ�, �w�R���ǥ�, ���ץ�, �@��� } )
                {
                    var.Checked = ( config.SelectSingleNode(var.Text + "[@Value='" + true + "']") != null );
                }
                #endregion
            }
            valueOnChange = false;

        }

        bool SmartSchool.StudentRelated.Divider.IAttendanceItemShown.Shown(BriefStudentData student, API.StudentExtension.Attendance attendance)
        {
            _Wait.WaitOne();
            if ( typeList.Contains(attendance.PeriodType) && absenceList.Contains(attendance.Absence) )
            {
                XmlElement config = CurrentUser.Instance.Preference[_preferenceElementName];
                if ( config != null )
                {
                    //#region �ǥͪ��A�z��
                    ////���ݤ@���
                    //if ( student.Status == "�@��" && ( config.SelectSingleNode("�@���[@Value='" + false + "']") != null ) )
                    //    return false;
                    ////���ݩ��ץ�
                    //if ( student.Status == "����" && ( config.SelectSingleNode("���ץ�[@Value='" + false + "']") != null ) )
                    //    return false;
                    ////���ݥ�Ǿǥ�
                    //if ( student.Status == "���" && ( config.SelectSingleNode("��Ǿǥ�") != null ) && ( config.SelectSingleNode("��Ǿǥ�[@Value='" + false + "']") != null ) )
                    //    return false;
                    ////���ݲ��~�����վǥ�
                    //if ( student.Status == "���~������" && ( config.SelectSingleNode("���~�����վǥ�") != null ) && ( config.SelectSingleNode("���~�����վǥ�[@Value='" + true + "']") != null ) )
                    //    return false;
                    ////���ݤw�R���ǥ�
                    //if ( student.Status == "�R��" && ( config.SelectSingleNode("�w�R���ǥ�") != null ) && ( config.SelectSingleNode("�w�R���ǥ�[@Value='" + true + "']") != null ) )
                    //    return false;
                    //#endregion
                    return config.SelectSingleNode("Type[@Text='" + attendance.PeriodType + "']/Absence[@Text='" + attendance.Absence + "']") != null;
                }
                else
                {
                    //#region �ǥͪ��A�z��
                    ////���ݥ�Ǿǥ�
                    //if ( student.Status == "���" )
                    //    return false;
                    ////���ݲ��~�����վǥ�
                    //if ( student.Status == "���~������" )
                    //    return false;
                    ////���ݤw�R���ǥ�
                    //if ( student.Status == "�R��" )
                    //    return false;
                    //#endregion
                    return true;
                }
            }
            else
                return false;
        }

        private void dataGridViewX1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dataGridViewX1.EndEdit();
            if ( valueOnChange ) return;
            valueOnChange = true;
            if ( dataGridViewX1.CurrentCell != null && dataGridViewX1.CurrentCell.ValueType == typeof(bool) )
            {
                foreach ( DataGridViewCell cell in dataGridViewX1.SelectedCells )
                {
                    if ( cell.ValueType == typeof(bool) && cell != dataGridViewX1.CurrentCell )
                    {
                        cell.Value = dataGridViewX1.CurrentCell.Value;
                    }
                }
            }
            valueOnChange = false;
        }
    }
}