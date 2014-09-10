using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.Feature.Basic;
using IntelliSchool.DSA30.Util;
using System.Xml;
using DevComponents.DotNetBar;
using SmartSchool.Feature;
using SmartSchool.ApplicationLog;
using SmartSchool.ExceptionHandler;
using SmartSchool.Properties;
using SmartSchool.AccessControl;

namespace SmartSchool.StudentRelated.Palmerworm
{
    [FeatureCode("Content0050")]
    internal partial class AddressPalmerwormItem : PalmerwormItem
    {
        private enum AddressType
        {
            Permanent,
            Mailing,
            Other
        }

        private Address _permanent_address;
        private Address _mailing_address;
        private Address _other_addresses;
        private AddressType _address_type;

        private EnhancedErrorProvider _errors;
        private EnhancedErrorProvider _warnings;

        private BackgroundWorker _getCountyBackgroundWorker;

        //Town -> ZipCode
        private Dictionary<string, string> _zip_code_mapping = new Dictionary<string, string>();

        private bool _isInitialized = false;
        public AddressPalmerwormItem()
        {
            InitializeComponent();
            Title = "�a�}���";

            _errors = new EnhancedErrorProvider();
            _errors.Icon = Resources.error;
            _warnings = new EnhancedErrorProvider();
            _warnings.Icon = Resources.warning;

            _address_type = AddressType.Permanent;

            _getCountyBackgroundWorker = new BackgroundWorker();
            _getCountyBackgroundWorker.DoWork += new DoWorkEventHandler(_getCountyBackgroundWorker_DoWork);
            _getCountyBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_getCountyBackgroundWorker_RunWorkerCompleted);
            _getCountyBackgroundWorker.RunWorkerAsync();

        }

        void _getCountyBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<string> countyList = e.Result as List<string>;
            foreach (string county in countyList)
            {
                cboCounty.AddItem(county);
            }
        }

        void _getCountyBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = Config.GetCountyList();
        }

        private void Initialize()
        {
            _errors.Clear();
            _warnings.Clear();

            if (_isInitialized) return;

            _isInitialized = true;
        }

        private XmlElement _current_response;
        protected override void OnBackgroundWorkerCompleted(object result)
        {
            Initialize();

            DSXmlHelper helper = result as DSXmlHelper;
            _current_response = helper.BaseElement;

            XmlElement permanentAddress = helper.GetElement("Student/PermanentAddress/AddressList/Address");
            if (permanentAddress == null)
                _permanent_address = new Address("���y�a�}");
            else
                _permanent_address = new Address(permanentAddress, "���y�a�}");

            XmlElement mailingAddress = helper.GetElement("Student/MailingAddress/AddressList/Address");
            if (mailingAddress == null)
                _mailing_address = new Address("�p���a�}");
            else
                _mailing_address = new Address(mailingAddress, "�p���a�}");

            XmlElement otherAddress = helper.GetElement("Student/OtherAddresses/AddressList/Address");
            if (otherAddress == null)
                _other_addresses = new Address("�䥦�a�}");
            else
                _other_addresses = new Address(otherAddress, "�䥦�a�}");

            _valueManager.AddValue("pCounty", _permanent_address.County, "���y����");
            _valueManager.AddValue("pTown", _permanent_address.Town, "���y�m��");
            _valueManager.AddValue("pAddress", _permanent_address.DetailAddress, "���y������");
            _valueManager.AddValue("pZipCode", _permanent_address.ZipCode, "���y�l���ϸ�");
            _valueManager.AddValue("pLongitude", _permanent_address.Longitude, "���y�g��");
            _valueManager.AddValue("pLatitude", _permanent_address.Latitude, "���y�n��");

            _valueManager.AddValue("fCounty", _mailing_address.County, "�p������");
            _valueManager.AddValue("fTown", _mailing_address.Town, "�p���m��");
            _valueManager.AddValue("fAddress", _mailing_address.DetailAddress, "�p��������");
            _valueManager.AddValue("fZipCode", _mailing_address.ZipCode, "�p���l���ϸ�");
            _valueManager.AddValue("fLongitude", _mailing_address.Longitude, "�p���g��");
            _valueManager.AddValue("fLatitude", _mailing_address.Latitude, "�p���n��");

            _valueManager.AddValue("oCounty", _other_addresses.County, "��L����");
            _valueManager.AddValue("oTown", _other_addresses.Town, "��L�m��");
            _valueManager.AddValue("oAddress", _other_addresses.DetailAddress, "��L������");
            _valueManager.AddValue("oZipCode", _other_addresses.ZipCode, "��L�l���ϸ�");
            _valueManager.AddValue("oLongitude", _other_addresses.Longitude, "��L�g��");
            _valueManager.AddValue("oLatitude", _other_addresses.Latitude, "��L�n��");

            DisplayAddress(GetCurrentAddress());

            SaveButtonVisible = false;
        }

        public override void Save()
        {
            if (!IsValid())
            {
                MsgBox.Show("��ƿ��~�A�Эץ����", "���~", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool oChanged = false;
            bool fChanged = false;
            bool pChanged = false;

            Dictionary<string, string> changes = _valueManager.GetDirtyItems();
            foreach (string key in changes.Keys)
            {
                if (key.StartsWith("p"))
                    pChanged = true;
                if (key.StartsWith("o"))
                    oChanged = true;
                if (key.StartsWith("f"))
                    fChanged = true;
            }
            DSXmlHelper helper = new DSXmlHelper("UpdateStudentList");
            helper.AddElement("Student");
            helper.AddElement("Student", "Field");
            if (pChanged)
            {
                helper.AddElement("Student/Field", "PermanentAddress");
                helper.AddElement("Student/Field/PermanentAddress", "AddressList");
                helper.AddElement("Student/Field/PermanentAddress/AddressList", "Address");
                helper.AddElement("Student/Field/PermanentAddress/AddressList/Address", "County", _permanent_address.County);
                helper.AddElement("Student/Field/PermanentAddress/AddressList/Address", "Town", _permanent_address.Town);
                helper.AddElement("Student/Field/PermanentAddress/AddressList/Address", "ZipCode", _permanent_address.ZipCode);
                helper.AddElement("Student/Field/PermanentAddress/AddressList/Address", "DetailAddress", _permanent_address.DetailAddress);
                helper.AddElement("Student/Field/PermanentAddress/AddressList/Address", "Longitude", _permanent_address.Longitude);
                helper.AddElement("Student/Field/PermanentAddress/AddressList/Address", "Latitude", _permanent_address.Latitude);
            }
            if (fChanged)
            {
                helper.AddElement("Student/Field", "MailingAddress");
                helper.AddElement("Student/Field/MailingAddress", "AddressList");
                helper.AddElement("Student/Field/MailingAddress/AddressList", "Address");
                helper.AddElement("Student/Field/MailingAddress/AddressList/Address", "County", _mailing_address.County);
                helper.AddElement("Student/Field/MailingAddress/AddressList/Address", "Town", _mailing_address.Town);
                helper.AddElement("Student/Field/MailingAddress/AddressList/Address", "ZipCode", _mailing_address.ZipCode);
                helper.AddElement("Student/Field/MailingAddress/AddressList/Address", "DetailAddress", _mailing_address.DetailAddress);
                helper.AddElement("Student/Field/MailingAddress/AddressList/Address", "Longitude", _mailing_address.Longitude);
                helper.AddElement("Student/Field/MailingAddress/AddressList/Address", "Latitude", _mailing_address.Latitude);
            }
            if (oChanged)
            {
                helper.AddElement("Student/Field", "OtherAddresses");
                helper.AddElement("Student/Field/OtherAddresses", "AddressList");
                helper.AddElement("Student/Field/OtherAddresses/AddressList", "Address");
                helper.AddElement("Student/Field/OtherAddresses/AddressList/Address", "County", _other_addresses.County);
                helper.AddElement("Student/Field/OtherAddresses/AddressList/Address", "Town", _other_addresses.Town);
                helper.AddElement("Student/Field/OtherAddresses/AddressList/Address", "ZipCode", _other_addresses.ZipCode);
                helper.AddElement("Student/Field/OtherAddresses/AddressList/Address", "DetailAddress", _other_addresses.DetailAddress);
                helper.AddElement("Student/Field/OtherAddresses/AddressList/Address", "Longitude", _other_addresses.Longitude);
                helper.AddElement("Student/Field/OtherAddresses/AddressList/Address", "Latitude", _other_addresses.Latitude);
            }

            helper.AddElement("Student", "Condition");
            helper.AddElement("Student/Condition", "ID", RunningID);
            EditStudent.Update(new DSRequest(helper));

            LogAction();

            SaveButtonVisible = false;
        }

        private void LogAction()
        {
            CurrentUser user = CurrentUser.Instance;
            try
            {
                UpdateStudentLog log = new UpdateStudentLog(RunningID);
                log.StudentName = GetStudentName();
                foreach (KeyValuePair<string, string> each in _valueManager.GetDirtyItems())
                {
                    string displayText = _valueManager.GetDisplayText(each.Key);
                    string originValue = _valueManager.GetOldValue(each.Key);
                    log.AddChangeField(displayText, originValue, each.Value);
                }

                user.AppLog.Write(log);
            }
            catch (Exception ex)
            {
                BugReporter.ReportException(user.SystemName, user.SystemVersion, ex, false);
            }
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

        protected override object OnBackgroundWorkerWorking()
        {
            return QueryStudent.GetAddress(RunningID).GetContent();
        }

        private void cboCounty_TextChanged(object sender, EventArgs e)
        {
            cboTown.SelectedItem = null;
            cboTown.Items.Clear();
            if (cboCounty.GetText() != "")
            {
                XmlElement[] townList = Config.GetTownList(cboCounty.GetText());
                _zip_code_mapping = new Dictionary<string, string>();
                foreach (XmlElement each in townList)
                {
                    string name = each.GetAttribute("Name");

                    if (!_zip_code_mapping.ContainsKey(name))
                        _zip_code_mapping.Add(name, each.GetAttribute("Code"));

                    cboTown.AddItem(name);
                }
            }

            Address addr = GetCurrentAddress();
            addr.County = cboCounty.GetText();

            if (_address_type == AddressType.Permanent)
                OnValueChanged("pCounty", addr.County);
            else if (_address_type == AddressType.Mailing)
                OnValueChanged("fCounty", addr.County);
            else if (_address_type == AddressType.Other)
                OnValueChanged("oCounty", addr.County);
            else
                throw new Exception("�S������ Address Type�C");

            ShowFullAddress();
        }

        private void cboTown_TextChanged(object sender, EventArgs e)
        {
            if (_date_updating) return;
            CheckTownChange();
        }

        private void CheckTownChange()
        {
            string value = cboTown.GetText();
            if (!string.IsNullOrEmpty(value))
            {
                if (_zip_code_mapping.ContainsKey(value))
                    txtZipcode.Text = _zip_code_mapping[value];
            }

            Address addr = GetCurrentAddress();
            addr.Town = cboTown.GetText();

            if (_address_type == AddressType.Permanent)
                OnValueChanged("pTown", addr.Town);
            else if (_address_type == AddressType.Mailing)
                OnValueChanged("fTown", addr.Town);
            else if (_address_type == AddressType.Other)
                OnValueChanged("oTown", addr.Town);
            else
                throw new Exception("�S������ Address Type�C");

            ShowFullAddress();
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            ShowFullAddress();

            Address addr = GetCurrentAddress();
            addr.DetailAddress = txtAddress.Text;

            if (_address_type == AddressType.Permanent)
                OnValueChanged("pAddress", addr.DetailAddress);
            else if (_address_type == AddressType.Mailing)
                OnValueChanged("fAddress", addr.DetailAddress);
            else if (_address_type == AddressType.Other)
                OnValueChanged("oAddress", addr.DetailAddress);
            else
                throw new Exception("�S������ Address Type�C");
        }

        private void ShowFullAddress()
        {
            string fullAddress = "";
            if (txtZipcode.Text != "")
                fullAddress += "[" + txtZipcode.Text + "]";
            fullAddress += cboCounty.GetText();
            fullAddress += cboTown.GetText();
            fullAddress += txtAddress.Text;
            this.lblFullAddress.Text = fullAddress;
        }

        private void txtLongtitude_TextChanged(object sender, EventArgs e)
        {
            decimal d;
            if (!string.IsNullOrEmpty(txtLongtitude.Text) && !decimal.TryParse(txtLongtitude.Text, out d))
            {
                _errors.SetError(txtLongtitude, "�g�ץ������Ʀr�κA�C");
                return;
            }
            else
                _errors.SetError(txtLongtitude, string.Empty);

            Address addr = GetCurrentAddress();
            addr.Longitude = txtLongtitude.Text;

            if (_address_type == AddressType.Permanent)
                OnValueChanged("pLongitude", addr.Longitude);
            else if (_address_type == AddressType.Mailing)
                OnValueChanged("fLongitude", addr.Longitude);
            else if (_address_type == AddressType.Other)
                OnValueChanged("oLongitude", addr.Longitude);
            else
                throw new Exception("�S������ Address Type�C");
        }

        private void txtLatitude_TextChanged(object sender, EventArgs e)
        {
            decimal d;
            if (!string.IsNullOrEmpty(txtLatitude.Text) && !decimal.TryParse(txtLatitude.Text, out d))
            {
                _errors.SetError(txtLatitude, "�n�ץ������Ʀr�κA�C");
                return;
            }
            else
                _errors.SetError(txtLatitude, string.Empty);

            Address addr = GetCurrentAddress();
            addr.Latitude = txtLatitude.Text;

            if (_address_type == AddressType.Permanent)
                OnValueChanged("pLatitude", addr.Latitude);
            else if (_address_type == AddressType.Mailing)
                OnValueChanged("fLatitude", addr.Latitude);
            else if (_address_type == AddressType.Other)
                OnValueChanged("oLatitude", addr.Latitude);
            else
                throw new Exception("�S������ Address Type�C");
        }

        private void btnPAddress_Click(object sender, EventArgs e)
        {
            if (_errors.HasError)
            {
                MsgBox.Show("��ƿ��~�A�Эץ����");
                return;
            }

            _address_type = AddressType.Permanent;
            DisplayAddress(GetCurrentAddress());
        }

        private void btnFAddress_Click(object sender, EventArgs e)
        {
            if (_errors.HasError)
            {
                MsgBox.Show("��ƿ��~�A�Эץ����");
                return;
            }

            _address_type = AddressType.Mailing;
            DisplayAddress(GetCurrentAddress());
        }

        private void btnOAddress_Click(object sender, EventArgs e)
        {
            if (_errors.HasError)
            {
                MsgBox.Show("��ƿ��~�A�Эץ����");
                return;
            }

            _address_type = AddressType.Other;
            DisplayAddress(GetCurrentAddress());
        }

        private void txtZipcode_TextChanged(object sender, EventArgs e)
        {
            ShowFullAddress();
            if (_date_updating) return;

            decimal d;
            if (!string.IsNullOrEmpty(txtZipcode.Text) && !decimal.TryParse(txtZipcode.Text, out d))
            {
                _errors.SetError(txtZipcode, "�l���ϸ��������Ʀr�κA");
                return;
            }
            else
                _errors.SetError(txtZipcode, ""); //�M�����~�C

            Address addr = GetCurrentAddress();
            addr.ZipCode = txtZipcode.Text;

            if (_address_type == AddressType.Permanent)
                OnValueChanged("pZipCode", addr.ZipCode);
            else if (_address_type == AddressType.Mailing)
                OnValueChanged("fZipCode", addr.ZipCode);
            else if (_address_type == AddressType.Other)
                OnValueChanged("oZipCode", addr.ZipCode);
            else
                throw new Exception("�S������ Address Type�C");

            
        }

        private void txtZipcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (_date_updating) return;

            if (e.KeyCode == Keys.Enter)
                CheckZipCode();
        }

        private void txtZipcode_Validated(object sender, EventArgs e)
        {
            if (_date_updating) return;

            if (!_errors.ContainError(txtZipcode))
                CheckZipCode();
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            MapForm.ShowMap(txtLatitude.Text, txtLongtitude.Text, txtAddress.Text);
        }

        private void CheckZipCode()
        {
            KeyValuePair<string, string> ctPair = Config.FindTownByZipCode(txtZipcode.Text);
            if (ctPair.Key == null)
                _warnings.SetError(txtZipcode, "�d�L���l���ϸ����������m���ơC");
            else
            {
                _warnings.SetError(txtZipcode, string.Empty);

                string county = ctPair.Key;
                string town = ctPair.Value;

                cboCounty.SetComboBoxText(county);
                cboTown.SetComboBoxText(town);
            }
        }

        private bool IsValid()
        {
            return !_errors.HasError;
        }

        private Address GetCurrentAddress()
        {
            if (_address_type == AddressType.Permanent)
                return _permanent_address;
            else if (_address_type == AddressType.Mailing)
                return _mailing_address;
            else if (_address_type == AddressType.Other)
                return _other_addresses;
            else
                throw new ArgumentException("�S������ Address Type�C");
        }

        private bool _date_updating = false;
        private void DisplayAddress(Address addr)
        {
            _date_updating = true;
            btnAddressType.Text = addr.Title;
            cboCounty.SetComboBoxText(addr.County);
            cboTown.SetComboBoxText(addr.Town);
            txtAddress.Text = addr.DetailAddress;
            txtLongtitude.Text = addr.Longitude;
            txtLatitude.Text = addr.Latitude;
            txtZipcode.Text = addr.ZipCode;
            _date_updating = false;
        }

        private void btnQueryPoint_Click(object sender, EventArgs e)
        {
            try
            {
                DSXmlHelper h = new DSXmlHelper("Request");
                string address = cboCounty.GetText() + cboTown.GetText() + txtAddress.Text;
                h.AddText(".", address);
                DSResponse rsp = FeatureBase.CallService("SmartSchool.Common.QueryCoordinates", new DSRequest(h));
                h = rsp.GetContent();
                if (h.GetElement("Error") != null)
                    MsgBox.Show("�L�k�d�ߦ��a�}�y�Ь�����T");
                else
                {
                    string latitude = h.GetText("Latitude");
                    string longitude = h.GetText("Longitude");

                    if (!string.IsNullOrEmpty(txtLatitude.Text) || !string.IsNullOrEmpty(txtLongtitude.Text))
                    {
                        string msg = "�w�d�ߥX���a�}�y�Ь��G\n\n(" + longitude + "," + latitude + ")\n\n�n���N�ثe�y�г]�w�ܡH";
                        if (MsgBox.Show(msg, Application.ProductName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            txtLatitude.Text = latitude;
                            txtLongtitude.Text = longitude;
                        }
                    }
                    else
                    {
                        txtLatitude.Text = latitude;
                        txtLongtitude.Text = longitude;
                    }
                }
            }
            catch (Exception ex)
            {
                CurrentUser user = CurrentUser.Instance;
                BugReporter.ReportException(user.SystemName, user.SystemVersion, ex, false);
                MsgBox.Show("�d�߮y�и�T���~�C");
            }
        }

        private void AddressPalmerwormItem_DoubleClick(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
                XmlBox.ShowXml(_current_response);
        }

        #region Address
        private class Address
        {
            private DSXmlHelper _address;
            private string _title;

            public Address(string title)
            {
                _address = new DSXmlHelper("Address");
                _address.AddElement("County");
                _address.AddElement("Town");
                _address.AddElement("ZipCode");
                _address.AddElement("DetailAddress");
                _address.AddElement("Longitude");
                _address.AddElement("Latitude");

                _title = title;
            }

            public Address(XmlElement address, string title)
            {
                _address = new DSXmlHelper(address);
                _title = title;
            }

            public string Title
            {
                get { return _title; }
            }

            public string County
            {
                get { return _address.GetText("County"); }
                set { SetText("County", value); }
            }

            public string Town
            {
                get { return _address.GetText("Town"); }
                set { SetText("Town", value); }
            }

            public string ZipCode
            {
                get { return _address.GetText("ZipCode"); }
                set { SetText("ZipCode", value); }
            }

            public string DetailAddress
            {
                get { return _address.GetText("DetailAddress"); }
                set { SetText("DetailAddress", value); }
            }

            public string Longitude
            {
                get { return _address.GetText("Longitude"); }
                set { SetText("Longitude", value); }
            }

            public string Latitude
            {
                get { return _address.GetText("Latitude"); }
                set { SetText("Latitude", value); }
            }

            private void SetText(string name, string text)
            {
                XmlElement elm = _address.GetElement(name);

                if (elm == null) elm = _address.AddElement(name);

                elm.InnerText = text;
            }
        }
        #endregion
    }
}
