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

namespace SmartSchool.ClassRelated.RibbonBars
{
    public partial class AssignSeatNoPicker : BaseForm
    {
        private string _classid;
        private string _seatNo;
        private bool _allowedClosed;
        private string _studentid;

        public string SeatNo
        {
            get { return _seatNo; }       
        }
        private ErrorProvider _errProvider;
        
        public AssignSeatNoPicker(string classid,string studentid)
        {
            _classid = classid;
            _studentid = studentid;
            _allowedClosed = false;
            _errProvider = new ErrorProvider();
            InitializeComponent();
        }

        private void AssignSeatNoPicker_Load(object sender, EventArgs e)
        {
            List<int> list = SmartSchool.Feature.Basic.Class.ListEmptySeatNo(_classid);
            cboSeatNo.SelectedItem = null;
            cboSeatNo.Items.Clear();
            foreach (int seatno in list)
            {
                cboSeatNo.Items.Add(seatno);
            }
        }

        private void AssignSeatNoPicker_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_allowedClosed)
            {
                if (MsgBox.Show("放棄正在執行的編班動作 ?", "確定", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            _allowedClosed = false;
            this.Close();
        }

        private void cboSeatNo_Validating(object sender, CancelEventArgs e)
        {
            _errProvider.SetError(cboSeatNo, null);
            string text = cboSeatNo.Text;
            if (text == "") return;

            int seatNo;
            if (!int.TryParse(text, out seatNo))
            {
                _errProvider.SetError(cboSeatNo, "座號必須為數字");
                _seatNo = "";
            }
            else
                _seatNo = text;
        }
        
        private void btnSubmit_Click(object sender, EventArgs e)
        {            
            if (!string.IsNullOrEmpty(_errProvider.GetError(cboSeatNo)))
            {
                MsgBox.Show("資料不正確，請修正後再試");
                return;
            }
            SeatNoPicked();
            _allowedClosed = true;
            this.Close();
        }

        private void SeatNoPicked()
        {
            DSXmlHelper helper = new DSXmlHelper("UpdateRequest");
            helper.AddElement("Student");
            helper.AddElement("Student", "Field");
            helper.AddElement("Student/Field", "RefClassID", _classid);
            helper.AddElement("Student/Field", "SeatNo", cboSeatNo.Text);
            helper.AddElement("Student", "Condition");
            helper.AddElement("Student/Condition", "ID", _studentid);

            try
            {
                SmartSchool.Feature.EditStudent.Update(new DSRequest(helper));
            }
            catch (Exception ex)
            {
                MsgBox.Show("學生班級分配失敗 : " + ex.Message);
                return;
            }
            MsgBox.Show("學生班級分配完成");
            //SmartSchool.StudentRelated.Student.Instance.InvokBriefDataChanged(_studentid);
            SmartSchool.Broadcaster.Events.Items["學生/資料變更"].Invoke(_studentid);
            SmartSchool.ClassRelated.Class.Instance.InvokClassUpdated(_classid);
        }
    }
}