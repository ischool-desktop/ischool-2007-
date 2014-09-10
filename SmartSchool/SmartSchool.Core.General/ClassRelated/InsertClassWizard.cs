using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.Feature.Class;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;

namespace SmartSchool.ClassRelated
{
    public partial class InsertClassWizard : BaseForm
    {
        private string _NewClassID;

        public string NewClassID { get { return _NewClassID; } }

        public InsertClassWizard()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtClassName.Text))
            {
                MsgBox.Show("�п�J�Z�ŦW�١C");
                return;
            }

            if (!Class.Instance.ValidClassName(txtClassName.Text))
            {
                MsgBox.Show("�Z�ŦW�٭��СC");
                return;
            }

            DSXmlHelper helper = new DSXmlHelper("InsertRequest");
            helper.AddElement("Class");
            helper.AddElement("Class", "Field");
            helper.AddElement("Class/Field", "ClassName", txtClassName.Text);

            try
            {
                _NewClassID = AddClass.Insert(new DSRequest(helper));
                CurrentUser.Instance.AppLog.Write(SmartSchool.ApplicationLog.EntityType.Class, "�s�W�Z��", _NewClassID, "�s�Z�ŦW�١G" + txtClassName.Text, "�Z��", _NewClassID);

                if (!string.IsNullOrEmpty(_NewClassID))
                {
                    InsertClassEventArgs arg = new InsertClassEventArgs();
                    arg.InsertClassID = _NewClassID;
                    Class.Instance.InvokClassInserted(arg);
                    Close();

                    if (chkUpdateOther.Checked)                    
                        this.DialogResult = DialogResult.Yes;
                }
                else
                    MsgBox.Show("�s�W����");
            }
            catch (Exception)
            {
                MsgBox.Show("�Z�ŦW�٭��СC");
            }
        }

        private void InsertClassWizard_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}