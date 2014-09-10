using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace SmartSchool.Payment.AccountStatedModules
{
    internal class ApplicationToken : ISecurityToken
    {
        public ApplicationToken(XmlElement token)
        {
            _token_content = token;
        }

        private XmlElement _token_content;

        #region ISecurityToken 成員

        public System.Xml.XmlElement GetTokenContent()
        {
            return _token_content;
        }

        public string TokenType
        {
            get { return "Application"; }
        }

        public bool Reuseable
        {
            get { return false; }
        }

        #endregion
    }

    internal class SchoolBridge
    {
        private string _school_code;
        private bool _enabled;
        private string _access_point;
        private ApplicationToken _app_token;
        private DSConnection _conn;
        private List<string> _enterpirse_code_list;
        private DSXmlHelper _request;

        public SchoolBridge(string schoolCode, string enabled)
        {
            _school_code = schoolCode;
            _enabled = bool.Parse(enabled);
        }

        public string SchoolCode
        {
            get { return _school_code; }
        }

        public bool Enabled
        {
            get { return _enabled; }
        }

        public string AccessPoint
        {
            get { return _access_point; }
        }

        public void SetLicense(string licFile, string pin)
        {
            try
            {
                FileStream fs = new FileStream(licFile, FileMode.Open);
                byte[] cipher = new byte[fs.Length];
                fs.Read(cipher, 0, Convert.ToInt32(fs.Length));
                fs.Close();

                byte[] plain = Crypto.DecryptLicense(cipher, pin);

                string xmlString = Encoding.UTF8.GetString(plain);

                DSXmlHelper hlplicense = new DSXmlHelper(DSXmlHelper.LoadXml(xmlString));
                DSXmlHelper apptoken = new DSXmlHelper("SecurityToken");
                apptoken.SetAttribute(".", "Type", "Application");
                apptoken.AddElement(".", hlplicense.GetElement("ApplicationKey"));

                _access_point = hlplicense.GetText("AccessPoint");
                _app_token = new ApplicationToken(apptoken.BaseElement);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("解密授權檔失敗(訊息：{0})。", ex.Message), ex);
            }
        }

        public void OpenBridge()
        {
            _conn = new DSConnection();
            _conn.Connect(_access_point, _app_token);
        }

        public XmlElement NewShip()
        {
            if (_request == null)
                _request = new DSXmlHelper("Request");

            return _request.AddElement(".", "Transaction");
        }

        public void ShipTransactions()
        {
            if (_request == null) return;

            _conn.SendRequest("BankAccess.InsertTransactionLog", _request);
        }
    }
}
