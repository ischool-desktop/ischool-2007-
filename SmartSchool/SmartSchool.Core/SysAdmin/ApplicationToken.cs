using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace SmartSchool.SysAdmin
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

    internal class LicenseInfo
    {
        private string LicenseFile = Path.Combine(Application.StartupPath, "SmartSchoolLicense.key");

        public bool LicenseExists()
        {
            return File.Exists(LicenseFile);
        }

        public void DecryptLicense()
        {
            try
            {
                FileStream fs = new FileStream(LicenseFile, FileMode.Open);
                byte[] cipher = new byte[fs.Length];
                fs.Read(cipher, 0, Convert.ToInt32(fs.Length));
                fs.Close();

                byte[] plain = ProtectedData.Unprotect(cipher, SetLicense.CryptoKey, DataProtectionScope.CurrentUser);
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
                throw new Exception("解密授權檔失敗", ex);
            }
        }

        private string _access_point;
        public string AccessPoint
        {
            get { return _access_point; }
        }

        private ApplicationToken _app_token;
        public ApplicationToken ApplicationToken
        {
            get { return _app_token; }
        }
    }
}
