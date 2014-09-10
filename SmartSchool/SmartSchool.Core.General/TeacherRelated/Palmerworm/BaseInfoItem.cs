using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.Feature.Teacher;
using IntelliSchool.DSA30.Util;
using System.IO;
using DevComponents.DotNetBar;
using SmartSchool.ApplicationLog;
using SmartSchool.AccessControl;

namespace SmartSchool.TeacherRelated.Palmerworm
{
    [FeatureCode("Content0170")]
    internal partial class BaseInfoItem : PalmerwormItem
    {
        ErrorProvider epName = new ErrorProvider();
        ErrorProvider epNick = new ErrorProvider();
        ErrorProvider epGender = new ErrorProvider();

        public BaseInfoItem()
        {
            InitializeComponent();
            Title = "�Юv�򥻸��";
        }

        #region Log �Ψ쪺����
        TeacherBaseLogMachine machine = new TeacherBaseLogMachine();
        #endregion

        public override void Save()
        {
            if (IsValid())
            {
                DSXmlHelper helper = new DSXmlHelper("UpdateRequest");
                helper.AddElement("Teacher");
                helper.AddElement("Teacher", "Field");
                foreach (string key in _valueManager.GetDirtyItems().Keys)
                    helper.AddElement("Teacher/Field", key, _valueManager.GetDirtyItems()[key]);
                helper.AddElement("Teacher", "Condition");
                helper.AddElement("Teacher/Condition", "ID", RunningID);

                //�p��K�X��~!
                if (helper.PathExist("Teacher/Field/SmartTeacherPassword"))
                {
                    if (!string.IsNullOrEmpty(helper.GetElement("Teacher/Field/SmartTeacherPassword").InnerText))
                        helper.GetElement("Teacher/Field/SmartTeacherPassword").InnerText = PasswordHash.Compute(helper.GetElement("Teacher/Field/SmartTeacherPassword").InnerText);
                }

                EditTeacher.Update(new DSRequest(helper));

                #region Log

                #region Log �O���ק�᪺���
                machine.AddAfter(label1.Text.Replace("�@", ""), txtName.Text);
                machine.AddAfter(label2.Text.Replace("�@", ""), cboGender.Text);
                machine.AddAfter(label3.Text.Replace("�@", ""), txtIDNumber.Text);
                machine.AddAfter(label4.Text.Replace("�@", ""), txtPhone.Text);
                machine.AddAfter(label5.Text.Replace("�@", ""), txtEmail.Text);
                machine.AddAfter(label6.Text.Replace("�@", ""), txtCategory.Text);
                machine.AddAfter(label7.Text.Replace("�@", ""), txtSTLoginAccount.Text);
                machine.AddAfter(label8.Text.Replace("�@", ""), txtSTLoginPwd.Text);
                machine.AddAfter(label9.Text.Replace("�@", ""), cboAccountType.Text);
                machine.AddAfter(label10.Text.Replace("�@", ""), txtNickname.Text);
                #endregion

                #region Log �n�J�K�X����ܸԲӭק�O��
                machine.HideSomething(label8.Text.Replace("�@", ""));
                #endregion

                StringBuilder desc = new StringBuilder("");
                desc.AppendLine("�Юv�m�W�G" + Teacher.Instance.Items[RunningID].TeacherName + " ");
                desc.AppendLine(machine.GetDescription());

                CurrentUser.Instance.AppLog.Write(EntityType.Teacher, EntityAction.Update, RunningID, desc.ToString(), "�Юv�򥻸��", "");

                #endregion

                Teacher.Instance.InvokTeacherDataChanged(RunningID);
                SaveButtonVisible = false;
            }
            else
            {
                MsgBox.Show("��J��Ʀ��~�A�Э��s��z��A�x�s");
            }
        }

        private bool IsValid()
        {
            bool valid = true;
            foreach (Control c in Controls)
            {
                if (c.Tag != null && c.Tag.ToString() == "error")
                    valid = false;
            }
            return valid;
        }

        protected override object OnBackgroundWorkerWorking()
        {
            return QueryTeacher.GetTeacherDetail(RunningID);
        }

        protected override void OnBackgroundWorkerCompleted(object result)
        {
            cboGender.Tag = null;
            epGender.Clear();
            txtName.Tag = null;
            epName.Clear();
            epNick.Clear();
            errors.SetError(txtSTLoginAccount, "");
            txtSTLoginAccount.Tag = null;

            DSResponse dsrsp = result as DSResponse;
            DSXmlHelper helper = dsrsp.GetContent();

            txtCategory.Text = helper.GetText("Teacher/Category");
            txtEmail.Text = helper.GetText("Teacher/Email");
            txtIDNumber.Text = helper.GetText("Teacher/IDNumber");
            txtName.Text = helper.GetText("Teacher/TeacherName");
            txtNickname.Text = helper.GetText("Teacher/Nickname");
            txtPhone.Text = helper.GetText("Teacher/ContactPhone");
            cboAccountType.Text = helper.GetText("Teacher/RemoteAccount");
            txtSTLoginAccount.Text = helper.GetText("Teacher/SmartTeacherLoginName");
            txtSTLoginPwd.Text = helper.GetText("Teacher/SmartTeacherPassword");
            cboGender.Text = helper.GetText("Teacher/Gender");

            string picString = helper.GetText("Teacher/Photo");
            byte[] bs = Convert.FromBase64String(picString);
            MemoryStream ms = new MemoryStream(bs);
            try
            {
                pictureBox.Image = Bitmap.FromStream(ms);
            }
            catch (Exception)
            {
                pictureBox.Image = Properties.Resources.studentsPic;
            }

            _valueManager.AddValue("Category", txtCategory.Text);
            _valueManager.AddValue("Email", txtEmail.Text);
            _valueManager.AddValue("IDNumber", txtIDNumber.Text);
            _valueManager.AddValue("TeacherName", txtName.Text);
            _valueManager.AddValue("Nickname", txtNickname.Text);
            _valueManager.AddValue("ContactPhone", txtPhone.Text);
            _valueManager.AddValue("RemoteAccount", cboAccountType.Text);
            _valueManager.AddValue("SmartTeacherLoginName", txtSTLoginAccount.Text);
            _valueManager.AddValue("SmartTeacherPassword", txtSTLoginPwd.Text);
            _valueManager.AddValue("Gender", cboGender.Text);
            SaveButtonVisible = false;

            #region Log �O���ק�e�����
            machine.AddBefore(label1.Text.Replace("�@", ""), txtName.Text);
            machine.AddBefore(label2.Text.Replace("�@", ""), cboGender.Text);
            machine.AddBefore(label3.Text.Replace("�@", ""), txtIDNumber.Text);
            machine.AddBefore(label4.Text.Replace("�@", ""), txtPhone.Text);
            machine.AddBefore(label5.Text.Replace("�@", ""), txtEmail.Text);
            machine.AddBefore(label6.Text.Replace("�@", ""), txtCategory.Text);
            machine.AddBefore(label7.Text.Replace("�@", ""), txtSTLoginAccount.Text);
            machine.AddBefore(label8.Text.Replace("�@", ""), txtSTLoginPwd.Text);
            machine.AddBefore(label9.Text.Replace("�@", ""), cboAccountType.Text);
            machine.AddBefore(label10.Text.Replace("�@", ""), txtNickname.Text);
            #endregion
        }

        private void txtSTLoginAccount_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("SmartTeacherLoginName", txtSTLoginAccount.Text);
        }

        private void cboAccountType_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("RemoteAccount", cboAccountType.Text);
        }

        private void txtSTLoginPwd_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("SmartTeacherPassword", txtSTLoginPwd.Text);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("TeacherName", txtName.Text);
        }

        private void txtNickname_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("Nickname", txtNickname.Text);
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("ContactPhone", txtPhone.Text);
        }

        private void cboGender_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("Gender", cboGender.Text);
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("Email", txtEmail.Text);
        }

        private void txtIDNumber_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("IDNumber", txtIDNumber.Text);
        }

        private void txtCategory_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("Category", txtCategory.Text);
        }

        private void txtName_Validated(object sender, EventArgs e)
        {
            try
            {
                epName.Clear();
                txtName.Tag = null;
                epNick.Clear();
                txtNickname.Tag = null;

                if (string.IsNullOrEmpty(txtName.Text))
                {
                    epName.SetError(txtName, "�m�W���i�ťաC");
                    txtName.Tag = "error";
                }

                if (QueryTeacher.NameExists(RunningID, txtName.Text, txtNickname.Text))
                {
                    epName.SetError(txtName, "�u�m�W+�ʺ١v���i�P��L�H�ۦP�C");
                    txtName.Tag = "error";
                    epNick.SetError(txtNickname, "�u�m�W+�ʺ١v���i�P��L�H�ۦP�C");
                    txtNickname.Tag = "error";
                }
            }
            catch (Exception ex)
            {
                epName.SetError(txtName, string.Format("{0}", ex.Message));
            }

        }

        private void txtNickname_Validated(object sender, EventArgs e)
        {
            try
            {
                epName.Clear();
                txtName.Tag = null;
                epNick.Clear();
                txtNickname.Tag = null;

                if (QueryTeacher.NameExists(RunningID, txtName.Text, txtNickname.Text))
                {
                    epName.SetError(txtName, "�u�m�W+�ʺ١v���i�P��L�H�ۦP�C");
                    txtName.Tag = "error";
                    epNick.SetError(txtNickname, "�u�m�W+�ʺ١v���i�P��L�H�ۦP�C");
                    txtNickname.Tag = "error";
                }
            }
            catch (Exception ex)
            {
                epNick.SetError(txtNickname, string.Format("{0}", ex.Message));
            }
        }

        private void cboGender_Validated(object sender, EventArgs e)
        {
            try
            {
                epGender.Clear();
                cboGender.Tag = null;

                if (string.IsNullOrEmpty(cboGender.Text)) return;

                if (cboGender.Text != "�k" && cboGender.Text != "�k")
                {
                    epGender.SetError(cboGender, "�ʧO�u���y�k�z�Ρy�k�z�C");
                    cboGender.Tag = "error";
                }
            }
            catch (Exception ex)
            {
                epGender.SetError(cboGender, string.Format("{0}", ex.Message));
            }
        }

        private void txtSTLoginAccount_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                errors.SetError(txtSTLoginAccount, "");
                txtSTLoginAccount.Tag = string.Empty;

                if (string.IsNullOrEmpty(txtSTLoginAccount.Text)) return;

                if (QueryTeacher.LoginNameExists(RunningID, txtSTLoginAccount.Text))
                {
                    errors.SetError(txtSTLoginAccount, "�b���W�٭��СC");
                    txtSTLoginAccount.Tag = "error";
                }
            }
            catch
            {
                errors.SetError(txtSTLoginAccount, "�ˬd�b�����Х���");
            }
        }
    }

    /// <summary>
    /// �O���Юv�򥻸�ƪ��L��
    /// </summary>
    class TeacherBaseLogMachine
    {
        Dictionary<string, string> beforeData = new Dictionary<string, string>();
        Dictionary<string, string> afterData = new Dictionary<string, string>();
        List<string> hidden = new List<string>();

        public void AddBefore(string key, string value)
        {
            if (!beforeData.ContainsKey(key))
                beforeData.Add(key, value);
            else
                beforeData[key] = value;
        }

        public void AddAfter(string key, string value)
        {
            if (!afterData.ContainsKey(key))
                afterData.Add(key, value);
            else
                afterData[key] = value;
        }

        public void HideSomething(string key)
        {
            if (!hidden.Contains(key))
                hidden.Add(key);
        }

        public string GetDescription()
        {
            //�u�v
            StringBuilder desc = new StringBuilder("");

            foreach (string key in beforeData.Keys)
            {
                if (afterData.ContainsKey(key) && afterData[key] != beforeData[key])
                {
                    if (hidden.Contains(key))
                        desc.AppendLine("���u" + key + "�v�w�ܧ�");
                    else
                        desc.AppendLine("���u" + key + "�v�ѡu" + beforeData[key] + "�v�ܧ󬰡u" + afterData[key] + "�v");
                }
            }

            return desc.ToString();
        }
    }
}
