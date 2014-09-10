using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using DevComponents.DotNetBar;
using System.Security.Cryptography;
using System.IO;
using System.Xml;

namespace SmartSchool.SysAdmin
{
    public partial class SetLicense : BaseForm
    {
        internal static byte[] CryptoKey = Encoding.UTF8.GetBytes("IntelliSchool SmartSchool Cryptography Key");

        public SetLicense()
        {
            InitializeComponent();
        }

        private void lnkGenLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new LicenseManager().ShowDialog();
        }

        public static byte[] EncryptLicense(byte[] data, string pin)
        {
            //cipher
            SHA256Managed hasher = new SHA256Managed();
            TripleDES des = TripleDES.Create();
            byte[] pinHash = hasher.ComputeHash(Encoding.UTF8.GetBytes(pin));
            byte[] pinIV = new byte[8];
            byte[] pinKey = new byte[24];
            Array.Copy(pinHash, 0, pinIV, 0, 8);
            Array.Copy(pinHash, 8, pinKey, 0, 24);

            des.KeySize = 192;
            des.IV = pinIV;
            des.Key = pinKey;

            ICryptoTransform encryptor = des.CreateEncryptor();
            byte[] result = encryptor.TransformFinalBlock(data, 0, data.Length);

            des.Clear();
            hasher.Clear();

            return result;
        }

        public static byte[] DecryptLicense(byte[] cipherData, string pin)
        {
            //cipher
            SHA256Managed hasher = new SHA256Managed();
            TripleDES des = TripleDES.Create();
            byte[] pinHash = hasher.ComputeHash(Encoding.UTF8.GetBytes(pin));
            byte[] pinIV = new byte[8];
            byte[] pinKey = new byte[24];
            Array.Copy(pinHash, 0, pinIV, 0, 8);
            Array.Copy(pinHash, 8, pinKey, 0, 24);

            des.KeySize = 192;
            des.IV = pinIV;
            des.Key = pinKey;

            ICryptoTransform encryptor = des.CreateDecryptor();

            return encryptor.TransformFinalBlock(cipherData, 0, cipherData.Length);
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            if (ofdLicenseFile.ShowDialog() == DialogResult.OK)
                txtLicenseFile.Text = ofdLicenseFile.FileName;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new FileStream(txtLicenseFile.Text, FileMode.Open);
                byte[] cipher = new byte[fs.Length];
                fs.Read(cipher, 0, Convert.ToInt32(fs.Length));
                fs.Close();

                byte[] plain = Decrypt(cipher);

                string xmlString = Encoding.UTF8.GetString(plain);

                DSXmlHelper hlplicense = new DSXmlHelper(DSXmlHelper.LoadXml(xmlString));
                DSXmlHelper apptoken = new DSXmlHelper("SecurityToken");
                apptoken.SetAttribute(".", "Type", "Application");
                apptoken.AddElement(".", hlplicense.GetElement("ApplicationKey"));
                string accessPoint = hlplicense.GetText("AccessPoint");

                ValidApplicationKey(apptoken, accessPoint);

                byte[] cipher1 = ProtectedData.Protect(plain, CryptoKey, DataProtectionScope.CurrentUser);

                FileStream fs1 = new FileStream(Path.Combine(Application.StartupPath, "SmartSchoolLicense.key"), FileMode.Create);
                fs1.Write(cipher1, 0, cipher1.Length);
                fs1.Close();

                MsgBox.Show("安裝授權檔完成");
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
                DialogResult = DialogResult.None;
            }
        }

        private void ValidApplicationKey(DSXmlHelper apptoken, string accessPoint)
        {
            try
            {
                DSConnection conn = new DSConnection();
                conn.UseSession = true;
                conn.Connect(accessPoint, new ApplicationToken(apptoken.BaseElement));
            }
            catch (Exception ex)
            {
                string msg = string.Format("驗證授權資訊失敗\n\n{0}", ArrangeExceptionMessage(ex));
                throw new Exception(msg);
            }
        }

        private byte[] Decrypt(byte[] cipher)
        {
            try
            {
                return SetLicense.DecryptLicense(cipher, txtPinCode.Text);
            }
            catch (CryptographicException ex)
            {
                throw new DecryptException("解密授權檔失敗，請確認授權碼是否正確。", ex);
            }
        }

        private string ArrangeExceptionMessage(Exception ex)
        {
            string msg = string.Empty;
            int level = 0;
            Exception temp = ex;

            while (temp != null)
            {
                if (msg != string.Empty)
                    msg += "\n".PadRight(level * 5, ' ') + temp.Message;
                else
                    msg = temp.Message;

                temp = temp.InnerException;
                level++;
            }

            return msg;
        }

        [global::System.Serializable]
        public class DecryptException : Exception
        {
            public DecryptException(string message) : base(message) { }
            public DecryptException(string message, Exception inner) : base(message, inner) { }
        }
    }
}