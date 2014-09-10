using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature;
using System.Xml;
using SmartSchool.AccessControl;
using SmartSchool.Common;
using DevComponents.DotNetBar.Controls;

namespace SmartSchool.GovernmentalDocument.Content
{
    public partial class UpdateRecordForm : SmartSchool.Common.BaseForm
    {
        private string _id;
        private string _updateid;
        public event EventHandler DataSaved;
        private bool _saved;

        private Dictionary<UpdateRecordType, XmlElement> _tempInfo;
        private UpdateRecordType _previousType;

        public UpdateRecordForm(string id, string updateid)
        {
            _id = id;
            _updateid = updateid;
            _saved = false;
            _tempInfo = new Dictionary<UpdateRecordType, XmlElement>();
            _tempInfo.Add(UpdateRecordType.���~����, null);
            _tempInfo.Add(UpdateRecordType.�s�Ͳ���, null);
            _tempInfo.Add(UpdateRecordType.���y����, null);
            _tempInfo.Add(UpdateRecordType.��J����, null);
            InitializeComponent();
            Initialize();

            FeatureAce ace = CurrentUser.Acl[UpdatePalmerwormItem.FeatureCode];

            btnOK.Visible = ace.Editable;

            if (!ace.Editable)
                LockAllControl(this);
        }

        private void LockAllControl(Control parent)
        {
            foreach (Control each in parent.Controls)
            {
                if (each is TextBoxX)
                    (each as TextBoxX).ReadOnly = true;

                if (each is ComboBoxEx)
                    (each as ComboBoxEx).Enabled = false;

                if (each.Controls.Count > 0)
                    LockAllControl(each);
            }
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //����ثe����Ʀs�_��          
            _tempInfo[_previousType] = updateRecordInfo1.GetElement();


            switch (comboBoxEx1.SelectedIndex)
            {
                default:
                case 0:
                    updateRecordInfo1.Style = UpdateRecordType.���y����;
                    break;
                case 1:
                    updateRecordInfo1.Style = UpdateRecordType.��J����;
                    break;
                case 2:
                    updateRecordInfo1.Style = UpdateRecordType.�s�Ͳ���;
                    break;
                case 3:
                    updateRecordInfo1.Style = UpdateRecordType.���~����;
                    break;
            }

            XmlElement typeRec = _tempInfo[updateRecordInfo1.Style];
            if (typeRec != null)
                BindDataFromElement(typeRec);

            _previousType = updateRecordInfo1.Style;
        }

        private void Initialize()
        {
            if (!string.IsNullOrEmpty(_updateid))
            {
                updateRecordInfo1.StudentID = _id;
                updateRecordInfo1.SetUpdateValue(_updateid);
            }
            else
            {
                updateRecordInfo1.SetDefaultValue(_id);
            }
            switch (updateRecordInfo1.Style)
            {
                case UpdateRecordType.���y����:
                    comboBoxEx1.SelectedIndex = 0;
                    break;
                case UpdateRecordType.��J����:
                    comboBoxEx1.SelectedIndex = 1;

                    break;
                case UpdateRecordType.�s�Ͳ���:
                    comboBoxEx1.SelectedIndex = 2;

                    break;
                case UpdateRecordType.���~����:
                    comboBoxEx1.SelectedIndex = 3;
                    break;
                default:
                    break;
            }
            _previousType = updateRecordInfo1.Style;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (updateRecordInfo1.Save())
            {
                _saved = true;
                if (DataSaved != null)
                    DataSaved(this, null);
                this.Close();
            }

            #region ���e���x�s�覡
            //if (_updateid == null)
            //{
            //    DSXmlHelper helper = new DSXmlHelper("InsertRequest");
            //    helper.AddElement("UpdateRecord");
            //    helper.AddElement("UpdateRecord", "Field");
            //    helper.AddElement("UpdateRecord/Field", "RefStudentID", _id);
            //    helper.AddElement("UpdateRecord/Field", "ContextInfo");

            //    XmlDocument contextInfo = new XmlDocument();
            //    XmlElement root = contextInfo.CreateElement("ContextInfo");
            //    contextInfo.AppendChild(root);

            //    foreach (XmlNode node in updateRecordInfo1.GetElement().ChildNodes)
            //    {
            //        // �p�G�O Previous �}�Y������� ContextInfo ��

            //        if (node.Name.StartsWith("Previous") || node.Name.StartsWith("Graduate") || node.Name == "NewStudentNumber" || node.Name == "LastUpdateCode")
            //        {
            //            XmlNode importNode = node.Clone();
            //            importNode = contextInfo.ImportNode(importNode, true);
            //            root.AppendChild(importNode);
            //        }
            //        else
            //            helper.AddElement("UpdateRecord/Field", node.Name, node.InnerText);
            //    }

            //    // �N contextInfo �o�� document ����ƶ�i ContextInfo �� CdataSection ��
            //    helper.AddXmlString("UpdateRecord/Field/ContextInfo", root.OuterXml);
            //    DSRequest dsreq = new DSRequest(helper);
            //    try
            //    {
            //        if (IsValid())
            //        {
            //            EditStudent.InsertUpdateRecord(dsreq);
            //            _saved = true;
            //            if (DataSaved != null) DataSaved(this, null);
            //            this.Close();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MsgBox.Show("�s�W���ʸ�ƥ��ѡG" + ex.Message);
            //    }
            //}
            //else
            //{
            //    DSXmlHelper helper = new DSXmlHelper("UpdateRequest");
            //    helper.AddElement("UpdateRecord");
            //    helper.AddElement("UpdateRecord", "Field");
            //    helper.AddElement("UpdateRecord/Field", "RefStudentID", _id);
            //    helper.AddElement("UpdateRecord/Field", "ContextInfo");

            //    XmlDocument contextInfo = new XmlDocument();
            //    XmlElement root = contextInfo.CreateElement("ContextInfo");
            //    contextInfo.AppendChild(root);

            //    foreach (XmlNode node in updateRecordInfo1.GetElement().ChildNodes)
            //    {
            //        if (node.Name.StartsWith("Previous") || node.Name.StartsWith("Graduate") || node.Name == "NewStudentNumber" || node.Name == "LastUpdateCode")
            //        {
            //            XmlNode importNode = node.Clone();
            //            importNode = contextInfo.ImportNode(importNode, true);
            //            root.AppendChild(importNode);
            //        }
            //        else
            //            helper.AddElement("UpdateRecord/Field", node.Name, node.InnerText);
            //    }

            //    // �N contextInfo �o�� document ����ƶ�i ContextInfo �� CdataSection ��
            //    helper.AddXmlString("UpdateRecord/Field/ContextInfo", root.OuterXml);

            //    // �ɤW����
            //    helper.AddElement("UpdateRecord", "Condition");
            //    helper.AddElement("UpdateRecord/Condition", "ID", _updateid);

            //    try
            //    {
            //        if (IsValid())
            //        {
            //            EditStudent.ModifyUpdateRecord(new DSRequest(helper));
            //            _saved = true;
            //            if (DataSaved != null) DataSaved(this, null);
            //            this.Close();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MsgBox.Show("�קﲧ�ʸ�ƥ��ѡG" + ex.Message);
            //    }
            //    //if (DataSaved != null)
            //    //    DataSaved(this, null);
            //    //this.Close();
            //}
            ////_saved = true;
            #endregion
        }

        private void UpdateRecordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CurrentUser.Acl[UpdatePalmerwormItem.FeatureCode].Editable)
                return;

            if (!_saved)
            {
                if (MsgBox.Show("�o�Ӱʧ@�N���ثe�s�褤����ơA�O�_�T�w���}?", "����", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// ���Ҹ�ƬO�_���~
        /// </summary>
        /// <returns></returns>
        private bool IsValid()
        {
            bool valid = updateRecordInfo1.IsValid();
            if (!valid)
                MsgBox.Show("��ƿ��~�A���ˬd��J���", "���~", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return valid;
        }

        private string GetElementValue(XmlElement element, string xpath)
        {
            if (element == null) return "";
            if (element.SelectSingleNode(xpath) == null)
                return "";
            return element.SelectSingleNode(xpath).InnerText;
        }

        private void BindDataFromElement(XmlElement element)
        {
            updateRecordInfo1.Name = GetElementValue(element, "Name");
            updateRecordInfo1.StudentNumber = GetElementValue(element, "StudentNumber");
            updateRecordInfo1.IDNumber = GetElementValue(element, "IDNumber");
            updateRecordInfo1.Gender = GetElementValue(element, "Gender");
            updateRecordInfo1.Birthdate = GetElementValue(element, "Birthdate");
            updateRecordInfo1.UpdateDate = GetElementValue(element, "UpdateDate");
            updateRecordInfo1.UpdateCode = GetElementValue(element, "UpdateCode");
            updateRecordInfo1.UpdateDescription = GetElementValue(element, "UpdateDescription");
            updateRecordInfo1.Comment = GetElementValue(element, "Comment");
            updateRecordInfo1.GradeYear = GetElementValue(element, "GradeYear");
            updateRecordInfo1.ADDate = GetElementValue(element, "ADDate");
            updateRecordInfo1.ADNumber = GetElementValue(element, "ADNumber");
            updateRecordInfo1.Department = GetElementValue(element, "Department");
            updateRecordInfo1.LastADDate = GetElementValue(element, "LastADDate");
            updateRecordInfo1.LastADNumber = GetElementValue(element, "LastADNumber");
            updateRecordInfo1.LastUpdateCode = GetElementValue(element, "LastUpdateCode");
            updateRecordInfo1.PreviousDepartment = GetElementValue(element, "PreviousDepartment");
            updateRecordInfo1.PreviousGradeYear = GetElementValue(element, "PreviousGradeYear");
            updateRecordInfo1.PreviousSchool = GetElementValue(element, "PreviousSchool");
            updateRecordInfo1.PreviousSchoolLastADDate = GetElementValue(element, "PreviousSchoolLastADDate");
            updateRecordInfo1.PreviousSchoolLastADNumber = GetElementValue(element, "PreviousSchoolLastADNumber");
            updateRecordInfo1.PreviousStudentNumber = GetElementValue(element, "PreviousStudentNumber");
            updateRecordInfo1.GraduateCertificateNumber = GetElementValue(element, "GraduateCertificateNumber");
            updateRecordInfo1.GraduateSchool = GetElementValue(element, "GraduateSchool");
            updateRecordInfo1.GraduateSchoolLocationCode = GetElementValue(element, "GraduateSchoolLocationCode");            
        }

        /// <summary>
        /// �P�_�ثe��ƬO�ݩ��ز������O��
        /// ���]���ثe��Ƥ����A�ҥH�٤��T�w�n���P�_
        /// ���g���@��function , �N�û��Ǧ^ "���y����";
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>�������O</returns>
        private UpdateRecordType GetUpdateRecordType(object arg)
        {
            return UpdateRecordType.���y����;
        }
    }
}