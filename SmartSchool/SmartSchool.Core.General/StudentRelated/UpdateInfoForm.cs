using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.Feature.Basic;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.Feature;
using SmartSchool.Common;

// Obsolete
namespace SmartSchool.StudentRelated
{
    public partial class UpdateInfoForm : SmartSchool.Common.BaseForm
    {
        private string _updateID;
        private string _id;
        public event EventHandler DataSaved;

        /// <summary>
        /// 管理學生異動表單的建構子
        /// </summary>
        /// <param name="id">學生編號</param>
        /// <param name="updateID">異動編號, 若無編號為新增模式 , 若有編號為修改模式</param>
        public UpdateInfoForm(string id, string updateID)
        {
            _id = id;
            _updateID = updateID;
            InitializeComponent();
            Text = Text + (updateID == null ? "(新增)" : "(編輯)");
            //DecidePnlInVisibility();
            //InitializeData();
            //GetStudentData();
        }

        /// <summary>
        /// 初始化選項資料
        /// </summary>
        private void InitializeData()
        {
            // 代碼
            DSResponse dsrsp = Config.GetUpdateCodeList();
            foreach (XmlNode node in dsrsp.GetContent().GetElements("UpdateCode"))
            {
                cboUpdateCode.Items.Add(node.InnerText);
            }

            // 學年度
            int thisYear = DateTime.Today.Year;
            for (int i = thisYear; i > thisYear - 10; i--)
            {
                cboSchoolYear.Items.Add(i);
            }

            // 學期
            KeyValuePair<string, string> kvp1 = new KeyValuePair<string, string>("1", "上學期");
            KeyValuePair<string, string> kvp2 = new KeyValuePair<string, string>("2", "下學期");
            cboSemester.Items.Add(kvp1);
            cboSemester.Items.Add(kvp2);
            cboSemester.DisplayMember = "Value";
            cboSemester.ValueMember = "Key";

            // 年級
            KeyValuePair<string, string> gy1 = new KeyValuePair<string, string>("1", "一年級");
            KeyValuePair<string, string> gy2 = new KeyValuePair<string, string>("2", "二年級");
            KeyValuePair<string, string> gy3 = new KeyValuePair<string, string>("3", "三年級");
            cboGradeYear.Items.Add(gy1);
            cboGradeYear.Items.Add(gy2);
            cboGradeYear.Items.Add(gy3);
            cboGradeYear.DisplayMember = "Value";
            cboGradeYear.ValueMember = "Key";
        }

        private void cboUpdateCode_TextChanged(object sender, EventArgs e)
        {
            //載入異動類別
            DSResponse dsrsp = Config.GetUpdateTypeList(cboUpdateCode.Text);
            if (dsrsp.GetContent() != null)
            {
                foreach (XmlNode node in dsrsp.GetContent().GetElements("UpdateType"))
                {
                    cboUpdateType.Items.Add(node.InnerText);
                }
            }

            // 載入異動原因
        }

        private void cboUpdateType_TextChanged(object sender, EventArgs e)
        {
            DecidePnlInVisibility();
        }

        private void DecidePnlInVisibility()
        {
            if (cboUpdateType.Text.IndexOf("轉入") > -1)
                pnlIn.Show();
            else
                pnlIn.Hide();
        }

        private void cboGradeYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeyValuePair<string,string> kvp = ( KeyValuePair<string, string>)cboGradeYear.Items[cboGradeYear.SelectedIndex] ;
            string gradeYear = kvp.Key;
            DSResponse dsrsp = Class.GetDepartmentList(gradeYear);

            foreach (XmlNode node in dsrsp.GetContent().GetElement("Department"))
            {
                cboDept.Items.Add(node.InnerText);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MsgBox.Show("這個動作將放棄目前編輯中的資料，是否確定離開?", "提醒", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                e.Cancel = true;  
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_updateID == null)
            {
                DSXmlHelper helper = new DSXmlHelper("InsertUpdateRecordRequest");
                helper.AddElement("UpdateRecord");
                helper.AddElement("UpdateRecord","Fields");
                helper.AddElement("UpdateRecord/Fields", "Student", _id);
                helper.AddElement("UpdateRecord/Fields", "SchoolYear", cboSchoolYear.Text);
               // helper.AddElement("UpdateRecord/Fields", "Comments", cboSchoolYear.Text);
                helper.AddElement("UpdateRecord/Fields", "Semester", GetComboBoxValue(cboSemester));
                helper.AddElement("UpdateRecord/Fields", "GradeYear", GetComboBoxValue(cboGradeYear));
                helper.AddElement("UpdateRecord/Fields", "DepartmentName", cboDept.Text);
                helper.AddElement("UpdateRecord/Fields", "UpdateDate", dtpUpdateDate.Value.ToShortDateString());
                helper.AddElement("UpdateRecord/Fields", "UpdateCode", cboUpdateCode.Text);
                helper.AddElement("UpdateRecord/Fields", "UpdateType", cboUpdateType.Text);
                helper.AddElement("UpdateRecord/Fields", "UpdateReason", cboUpdateReason.Text);
                helper.AddElement("UpdateRecord/Fields", "UpdateDescription", txtUpdateDescription.Text);                
                helper.AddElement("UpdateRecord/Fields", "Name", txtName.Text);
                helper.AddElement("UpdateRecord/Fields", "StudentID", txtStudentID.Text);
                helper.AddElement("UpdateRecord/Fields", "Gender", (rbtBoy.Checked?"男":"女"));
                helper.AddElement("UpdateRecord/Fields", "IDNumber", txtIDNumber.Text);
                helper.AddElement("UpdateRecord/Fields", "BirthDate", dtpBirthdate.GetShortDateString());
                DSRequest dsreq = new DSRequest(helper);
                EditStudent.InsertUpdateRecord(dsreq);
            }
        }

        private string GetComboBoxValue(ComboBox combo)
        {
            KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)combo.SelectedItem;
            return kvp.Key;
        }

    }
}