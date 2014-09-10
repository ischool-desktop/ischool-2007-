using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Customization.Data;
using IntelliSchool.DSA30.Util;
using System.Xml;
using DevComponents.DotNetBar;
using SmartSchool.AccessControl;
using SmartSchool.Common;
using SmartSchool.ApplicationLog;

namespace SmartSchool.StudentRelated.Palmerworm
{
    [FeatureCode("Content0135")]
    public partial class DiplomaInfoPalmerworm : SmartSchool.PalmerwormItemBase
    {
        //<LeaveInfo SchoolYear='96' Reason='畢業' Department='阿里不達科' ClassName='阿里巴巴班' />
        private string _CurrentID;

        private string _RunningID;

        private BackgroundWorker _Loader = new BackgroundWorker();

        private AccessHelper helper = new AccessHelper();

        private XmlElement _CheckEditElement = null;

        public DiplomaInfoPalmerworm()
        {
            InitializeComponent();
            _Loader.DoWork += new DoWorkEventHandler(_Loader_DoWork);
            _Loader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_Loader_RunWorkerCompleted);
        }

        private void _Loader_DoWork(object sender, DoWorkEventArgs e)
        {
            StudentRecord studentRec= helper.StudentHelper.GetStudents(""+e.Argument)[0];
            //填入畢業證書字號
            helper.StudentHelper.FillField("DiplomaNumber", studentRec);
            e.Result = studentRec;
        }

        private void _Loader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ( this.IsDisposed )
                return;
            if ( _RunningID != _CurrentID )
            {
                LoadContent(_CurrentID);
                return;
            }
            this.WaitingPicVisible = false;

            StudentRecord studentRec = (StudentRecord)e.Result;
            if ( studentRec.Fields.ContainsKey("DiplomaNumber") && studentRec.Fields["DiplomaNumber"] != null )
            {
                _CheckEditElement = studentRec.Fields["DiplomaNumber"] as XmlElement;
                DSXmlHelper dnElement = new DSXmlHelper(_CheckEditElement);
                string dnString = dnElement.GetText("DiplomaNumber");
                if ( !string.IsNullOrEmpty(dnString) )
                {
                    this.textBoxX1.Text = dnString;
                }
                foreach ( XmlElement messageElement in dnElement.GetElements("Message") )
                {
                    int index = dataGridViewX1.Rows.Add();
                    DataGridViewRow row = dataGridViewX1.Rows[index];
                    row.Cells[0].Value = messageElement.GetAttribute("Type");
                    row.Cells[1].Value = messageElement.GetAttribute("Value");
                }
            }
            else
            {
                _CheckEditElement = new XmlDocument().CreateElement("DiplomaNumber");
            }

            string leaveSchoolYear = studentRec.Fields.ContainsKey("LeaveSchoolYear") ? "" + studentRec.Fields["LeaveSchoolYear"] : "";
            string leaveReason = studentRec.Fields.ContainsKey("LeaveReason") ? "" + studentRec.Fields["LeaveReason"] : "";
            string leaveClassName = studentRec.Fields.ContainsKey("LeaveClassName") ? "" + studentRec.Fields["LeaveClassName"] : "";
            string leaveDepartment = studentRec.Fields.ContainsKey("LeaveDepartment") ? "" + studentRec.Fields["LeaveDepartment"] : "";

            _CheckEditElement.SetAttribute("LeaveSchoolYear", leaveSchoolYear);
            _CheckEditElement.SetAttribute("LeaveReason", leaveReason);
            _CheckEditElement.SetAttribute("LeaveClassName", leaveClassName);
            _CheckEditElement.SetAttribute("LeaveDepartment", leaveDepartment);

            txtSchoolYear.Text = leaveSchoolYear;
            txtReason.Text = leaveReason;
            txtDept.Text = leaveDepartment;
            txtClass.Text = leaveClassName;

            CheckAll();
        }

        public override void LoadContent(string id)
        {
            _CurrentID = id;
            SaveButtonVisible = false;
            CancelButtonVisible = false;
            _CheckEditElement = null;
            dataGridViewX1.Rows.Clear();
            textBoxX1.Text = "";
            if ( !_Loader.IsBusy )
            {
                _RunningID = _CurrentID;
                _Loader.RunWorkerAsync(_RunningID);
                WaitingPicVisible = true;
            }
        }

        public override void Save()
        {
            DSXmlHelper helper = new DSXmlHelper("UpdateStudentList");

            string dn = textBoxX1.Text;
            string id = _CurrentID;

            XmlElement element = new XmlDocument().CreateElement("DiplomaNumber");
            element.AppendChild(element.OwnerDocument.CreateElement("DiplomaNumber")).InnerText = dn;
            foreach (DataGridViewRow row in this.dataGridViewX1.Rows)
            {
                if (row.IsNewRow)
                    continue;
                XmlElement msg = (XmlElement)element.AppendChild(element.OwnerDocument.CreateElement("Message"));
                msg.SetAttribute("Type", "" + row.Cells[0].Value);
                msg.SetAttribute("Value", "" + row.Cells[1].Value);
            }
            helper.AddElement("Student");
            helper.AddElement("Student", "Field");
            helper.AddElement("Student/Field", element);
            helper.AddElement("Student/Field", "LeaveInfo");
            XmlElement leaveElement = helper.AddElement("Student/Field/LeaveInfo", "LeaveInfo");

            leaveElement.SetAttribute("SchoolYear", txtSchoolYear.Text);
            leaveElement.SetAttribute("Reason", txtReason.Text);
            leaveElement.SetAttribute("ClassName", txtClass.Text);
            leaveElement.SetAttribute("Department", txtDept.Text);

            helper.AddElement("Student", "Condition");
            helper.AddElement("Student/Condition", "ID", id);

            DSRequest req = new DSRequest(helper);

            #region 處理Log

            if (_CheckEditElement != null)
            {
                StringBuilder desc = new StringBuilder("");
                desc.AppendLine("學生姓名：" + Student.Instance.Items[_CurrentID].Name + " ");

                DSXmlHelper dnElement = new DSXmlHelper(_CheckEditElement);
                string dnString = dnElement.GetText("DiplomaNumber");
                if (this.textBoxX1.Text != dnString)
                    desc.AppendLine("修改畢業證書字號由「" + dnString + "」變更為「" + textBoxX1.Text + "」");

                if (this.txtSchoolYear.Text != _CheckEditElement.GetAttribute("LeaveSchoolYear"))
                    desc.AppendLine("修改離校學年度由「" + _CheckEditElement.GetAttribute("LeaveSchoolYear") + "」變更為「" + txtSchoolYear.Text + "」");

                if (this.txtReason.Text != _CheckEditElement.GetAttribute("LeaveReason"))
                    desc.AppendLine("修改離校類別由「" + _CheckEditElement.GetAttribute("LeaveReason") + "」變更為「" + txtReason.Text + "」");

                if (this.txtClass.Text != _CheckEditElement.GetAttribute("LeaveClassName"))
                    desc.AppendLine("修改離校班級由「" + _CheckEditElement.GetAttribute("LeaveClassName") + "」變更為「" + txtClass.Text + "」");

                if (this.txtDept.Text != _CheckEditElement.GetAttribute("LeaveDepartment"))
                    desc.AppendLine("修改離校科別由「" + _CheckEditElement.GetAttribute("LeaveDepartment") + "」變更為「" + txtDept.Text + "」");

                int rowCount = 0;
                foreach (DataGridViewRow row in this.dataGridViewX1.Rows)
                {
                    if (row.IsNewRow)
                        continue;
                    if (dnElement.GetElement("Message[@Type='" + row.Cells[0].Value + "' and @Value='" + row.Cells[1].Value + "']") == null)
                        desc.AppendLine("新增畢業相關資訊「" + row.Cells[0].Value + "：" + row.Cells[1].Value + "」");
                    rowCount++;
                }
                if (rowCount != dnElement.GetElements("Message").Length)
                {
                    foreach (XmlElement var in dnElement.GetElements("Message"))
                    {
                        bool deleted = true;
                        foreach (DataGridViewRow row in dataGridViewX1.Rows)
                        {
                            if (var.GetAttribute("Type") == "" + row.Cells[0].Value && var.GetAttribute("Value") == "" + row.Cells[1].Value)
                                deleted = false;
                        }
                        if (deleted)
                            desc.AppendLine("移除原有畢業相關資訊「" + var.GetAttribute("Type") + "：" + var.GetAttribute("Value") + "」");
                    }
                }

                CurrentUser.Instance.AppLog.Write(EntityType.Student, "修改畢業資訊", _CurrentID, desc.ToString(), "", "");
            }
            #endregion
            try
            {
                SmartSchool.Feature.EditStudent.Update(req);
                //SmartSchool.StudentRelated.Student.Instance.InvokBriefDataChanged(_CurrentID);
                SmartSchool.Broadcaster.Events.Items["學生/資料變更"].Invoke(_CurrentID);
                //MsgBox.Show("儲存完成");
            }
            catch (Exception ex)
            {
                MsgBox.Show("儲存失敗，錯誤訊息：" + ex.Message);
            }
            LoadContent(_CurrentID);
        }

        public override void Undo()
        {
            LoadContent(_CurrentID);
        }

        public override object Clone()
        {
            return new DiplomaInfoPalmerworm();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            CheckAll();
        }

        private void dataGridViewX1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dataGridViewX1.EndEdit();
            CheckAll();
            dataGridViewX1.BeginEdit(false);
        }

        private void CheckAll()
        {
            int q=0;
            bool pass = txtSchoolYear.Text == "" || (int.TryParse(txtSchoolYear.Text, out q) && q > 0);
            if (pass)
            {
                errorProvider1.Clear();
            }
            else
            {
                errorProvider1.SetError(txtSchoolYear, "必須輸入正整數");
            }
            bool hasChanged = false;
            if ( _CheckEditElement != null )
            {
                DSXmlHelper dnElement = new DSXmlHelper(_CheckEditElement);
                string dnString = dnElement.GetText("DiplomaNumber");
                if ( this.textBoxX1.Text != dnString ||
                    this.txtSchoolYear.Text != _CheckEditElement.GetAttribute("LeaveSchoolYear") ||
                    this.txtReason.Text != _CheckEditElement.GetAttribute("LeaveReason") ||
                    this.txtClass.Text != _CheckEditElement.GetAttribute("LeaveClassName") ||
                    this.txtDept.Text != _CheckEditElement.GetAttribute("LeaveDepartment")
                    )

                {
                    hasChanged = true;
                }
                int rowCount = 0;
                foreach ( DataGridViewRow row in this.dataGridViewX1.Rows )
                {
                    if ( row.IsNewRow )
                        continue;
                    if ( dnElement.GetElement("Message[@Type='" + row.Cells[0].Value + "' and @Value='" + row.Cells[1].Value + "']") == null )
                        hasChanged = true;
                    rowCount++;
                }
                if ( rowCount != dnElement.GetElements("Message").Length )
                    hasChanged = true;
            }
            SaveButtonVisible = pass & hasChanged;
            CancelButtonVisible = hasChanged;
        }

        private void dataGridViewX1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridViewX1.EndEdit();
            dataGridViewX1.Rows[e.RowIndex].Selected = true;
        }

        private void dataGridViewX1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ( e.ColumnIndex < 0 )
                dataGridViewX1.EndEdit();
        }

        private void dataGridViewX1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if ( dataGridViewX1.SelectedCells.Count == 1 )
                dataGridViewX1.BeginEdit(true);
        }

        private void dataGridViewX1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CheckAll();
        }
    }
}

