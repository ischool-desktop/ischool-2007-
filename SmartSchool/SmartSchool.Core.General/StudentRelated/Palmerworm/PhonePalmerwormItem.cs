using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.StudentRelated.Palmerworm;
using SmartSchool.Feature.Basic;
using SmartSchool.Feature;
using SmartSchool.Common;
using SmartSchool.AccessControl;

namespace SmartSchool.StudentRelated.Palmerworm
{
    [FeatureCode("Content0040")]
    internal partial class PhonePalmerwormItem : PalmerwormItem
    {
        private SMSForm smsForm;
        private SkypeControl.SkypeProxy skypeProxy;
        private string oPhone1;
        private string oPhone2;
        private string oPhone3;
        private bool _isInitialized = false;

        public PhonePalmerwormItem()
            : base()
        {
            InitializeComponent();

            Title = "電話資料";
            skypeProxy = new SkypeControl.SkypeProxy();
            skypeProxy.CountryInfo = new SkypeControl.CountryInfo("886", "台灣");
            skypeProxy.OnSkypeStatusChange += new SkypeControl.SkypeStatusChangeHandler(skypeProxy_OnSkypeStatusChange);
            skypeProxy_OnSkypeStatusChange(null, null);
        }


        protected override object OnBackgroundWorkerWorking()
        {
            return QueryStudent.GetPhoneDetail(RunningID).GetContent();
        }

        protected override void OnBackgroundWorkerCompleted(object result)
        {
            Initialize();

            DSXmlHelper helper = result as DSXmlHelper;

            //XmlNode phoneNode = helper.GetElement("Student/PhoneNumbers");
            //if (phoneNode != null)
            //{
            //    XmlNode contactPhone = phoneNode.SelectSingleNode("Phone/ContactPhoneList/ContactPhone");
            //    XmlNode everPhone = phoneNode.SelectSingleNode("Phone/PermanentPhoneList/PermanentPhone");
            //    XmlNode otherPhone1 = phoneNode.SelectSingleNode("Phone/OtherPhoneList/OtherPhone[1]");
            //    XmlNode otherPhone2 = phoneNode.SelectSingleNode("Phone/OtherPhoneList/OtherPhone[2]");
            //    XmlNode otherPhone3 = phoneNode.SelectSingleNode("Phone/OtherPhoneList/OtherPhone[3]");
            //    DSXmlHelper helper = new DSXmlHelper(phoneNode);

            //    if (contactPhone != null) cPhone = contactPhone.InnerText;
            //    if (everPhone != null) ePhone = everPhone.InnerText;
            //    if (otherPhone1 != null) oPhone1 = otherPhone1.InnerText;
            //    if (otherPhone2 != null) oPhone2 = otherPhone2.InnerText;
            //    if (otherPhone3 != null) oPhone3 = otherPhone3.InnerText;

            //}


            string cPhone = helper.GetText("Student/ContactPhone");
            string ePhone = helper.GetText("Student/PermanentPhone");
            string sPhone = helper.GetText("Student/SMSPhone");
            oPhone1 = helper.GetText("Student/OtherPhones/PhoneList/PhoneNumber[1]");
            oPhone2 = helper.GetText("Student/OtherPhones/PhoneList/PhoneNumber[2]");
            oPhone3 = helper.GetText("Student/OtherPhones/PhoneList/PhoneNumber[3]");

            txtContactPhone.Text = cPhone;
            txtEverPhone.Text = ePhone;
            txtSMS.Text = sPhone;

            _valueManager.AddValue("ContactPhone", cPhone, "聯絡電話");
            _valueManager.AddValue("PermanentPhone", ePhone, "戶籍電話");
            _valueManager.AddValue("SMSPhone", sPhone, "行動電話");
            _valueManager.AddValue("OtherPhone1", oPhone1, "其它電話1");
            _valueManager.AddValue("OtherPhone2", oPhone2, "其它電話2");
            _valueManager.AddValue("OtherPhone3", oPhone3, "其它電話3");

            LoadOtherPhone1();
        }

        private void Initialize()
        {
            if (_isInitialized) return;
            _isInitialized = true;
        }

        public override void Save()
        {
            //DSXmlHelper xml = new DSXmlHelper("Phone");
            //xml.AddElement("ContactPhoneList");
            //xml.AddElement("ContactPhoneList", "ContactPhone", txtContactPhone.Text);

            //xml.AddElement("PermanentPhoneList");
            //xml.AddElement("PermanentPhoneList", "PermanentPhone", txtEverPhone.Text);

            //xml.AddElement("OtherPhoneList");
            //xml.AddElement("OtherPhoneList", "OtherPhone", oPhone1);
            //xml.AddElement("OtherPhoneList", "OtherPhone", oPhone2);
            //xml.AddElement("OtherPhoneList", "OtherPhone", oPhone3);

            DSXmlHelper xml = new DSXmlHelper("UpdatePhoneRequest");
            xml.AddElement("UpdateFields");
            xml.AddElement("UpdateFields", "PermanentPhone", txtEverPhone.Text);
            //xml.AddElement("UpdateFields/PermanentPhone", "PhoneList");
            //xml.AddElement("UpdateFields/PermanentPhone/PhoneList", "PhoneNumber",txtEverPhone.Text);
            xml.AddElement("UpdateFields", "ContactPhone", txtContactPhone.Text);
            //xml.AddElement("UpdateFields/ContactPhone", "PhoneList");
            //xml.AddElement("UpdateFields/ContactPhone/PhoneList", "PhoneNumber", txtContactPhone.Text);
            xml.AddElement("UpdateFields", "SMSPhone", txtSMS.Text);
            xml.AddElement("UpdateFields", "OtherPhones");
            xml.AddElement("UpdateFields/OtherPhones", "PhoneList");
            xml.AddElement("UpdateFields/OtherPhones/PhoneList", "PhoneNumber", oPhone1);
            xml.AddElement("UpdateFields/OtherPhones/PhoneList", "PhoneNumber", oPhone2);
            xml.AddElement("UpdateFields/OtherPhones/PhoneList", "PhoneNumber", oPhone3);
            xml.AddElement("Condition");
            xml.AddElement("Condition", "ID", RunningID);

            EditStudent.UpdatePhone(xml);

            LogUtility.LogChange(_valueManager, RunningID, GetStudentName());

            SaveButtonVisible = false;
        }

        private string GetStudentName()
        {
            try
            {
                BriefStudentData student = Student.Instance.Items[RunningID];
                return student.Name;
            }
            catch (Exception)
            {
                return "<" + RunningID + ">";
            }
        }

        private void txtEverPhone_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("PermanentPhone", txtEverPhone.Text);
        }

        private void txtContactPhone_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("ContactPhone", txtContactPhone.Text);
        }

        private void txtSMS_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("SMSPhone", txtSMS.Text);
        }

        private void txtOtherPhone_TextChanged(object sender, EventArgs e)
        {
            if (btnOthers.Text.EndsWith("1"))
            {
                oPhone1 = txtOtherPhone.Text;
                OnValueChanged("OtherPhone1", txtOtherPhone.Text);
            }
            else if (btnOthers.Text.EndsWith("2"))
            {
                oPhone2 = txtOtherPhone.Text;
                OnValueChanged("OtherPhone2", txtOtherPhone.Text);
            }
            else
            {
                oPhone3 = txtOtherPhone.Text;
                OnValueChanged("OtherPhone3", txtOtherPhone.Text);
            }
        }

        private void btnOther1_Click(object sender, EventArgs e)
        {
            LoadOtherPhone1();
        }

        private void btnOther2_Click(object sender, EventArgs e)
        {
            btnOther1.Enabled = true;
            btnOther2.Enabled = false;
            btnOther3.Enabled = true;
            btnOthers.Text = btnOther2.Text;

            txtOtherPhone.Text = oPhone2;
        }

        private void btnOther3_Click(object sender, EventArgs e)
        {
            btnOther1.Enabled = true;
            btnOther2.Enabled = true;
            btnOther3.Enabled = false;
            btnOthers.Text = btnOther3.Text;

            txtOtherPhone.Text = oPhone3;
        }

        private void LoadOtherPhone1()
        {
            btnOther1.Enabled = false;
            btnOther2.Enabled = true;
            btnOther3.Enabled = true;
            btnOthers.Text = btnOther1.Text;

            txtOtherPhone.Text = oPhone1;
        }

        private void CallEverPhone(object sender, EventArgs e)
        {
            skypeProxy.Call(txtEverPhone.Text);
        }

        private void CallContactPhone(object sender, EventArgs e)
        {
            skypeProxy.Call(txtContactPhone.Text);
        }

        private void CallOtherPhone(object sender, EventArgs e)
        {
            skypeProxy.Call(txtOtherPhone.Text);
        }

        void skypeProxy_OnSkypeStatusChange(object theSender, SkypeControl.SkypeStatusChangeEventArgs theEventArgs)
        {
            bool enabled = (skypeProxy.SkypeStatus == SkypeControl.SkypeStatusEnum.Ready);
            buttonItem1.Enabled = buttonItem3.Enabled = buttonItem5.Enabled = enabled;
            buttonItem2.Enabled = enabled;
        }

        private void btnPSMS_Click(object sender, EventArgs e)
        {
            if (smsForm == null)
                smsForm = new SMSForm();
            smsForm.SetNumber(this.txtEverPhone.Text);
            smsForm.ShowDialog();
        }

        private void btnCSMS_Click(object sender, EventArgs e)
        {
            if (smsForm == null)
                smsForm = new SMSForm();
            smsForm.SetNumber(this.txtContactPhone.Text);
            smsForm.ShowDialog();
        }

        private void btnOSMS_Click(object sender, EventArgs e)
        {
            if (smsForm == null)
                smsForm = new SMSForm();
            smsForm.SetNumber(this.txtOtherPhone.Text);
            smsForm.ShowDialog();
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            if (smsForm == null)
                smsForm = new SMSForm();
            smsForm.SetNumber(this.txtSMS.Text);
            smsForm.ShowDialog();
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            skypeProxy.Call(txtSMS.Text);
        }
    }
}
