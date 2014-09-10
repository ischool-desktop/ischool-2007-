using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common.DateTimeProcess;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography;
using System.IO;

namespace SmartSchool.SysAdmin
{
    public partial class LicenseManager : BaseForm
    {
        private const string DateFormat = "yyyy/MM/dd";

        public LicenseManager()
        {
            InitializeComponent();

            //txtExpireDate.Text = DateTime.Today.AddYears(2).ToString(DateFormat);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                DSConnection conn = ConnectToApplication();

                if (string.IsNullOrEmpty(txtIPScope.Text.Trim()))
                {
                    string msg = "你未指定 IP 範圍，這將會不限制授權檔的使用位置，您確定嗎？";
                    if (MsgBox.Show(msg, Application.ProductName, MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        return;
                }

                if (txtPinCode.Text != txtConfirmPin.Text)
                    throw new Exception("確認授權碼失敗，請在兩個文字方塊中輸入相同的授權碼。");

                DSXmlHelper request = new DSXmlHelper("ApplicationKey");
                request.AddElement(".", "Name", "SmartSchoolClient");
                request.AddElement(".", "InspireDate", DateTime.Today.ToString(DateFormat));
                request.AddElement(".", "ExpireDate", ParseDate());
                XmlElement limit = request.AddElement(".", "LocationLimit");
                InsertIpEntry(limit);

                string srvName = "SmartSchool.SystemAdministration.SignLicense";
                DSXmlHelper response = conn.SendRequest(srvName, request);

                DSXmlHelper license = new DSXmlHelper("SmartSchoolLicense");
                license.AddElement(".", response.BaseElement);
                license.AddElement(".", "AccessPoint", txtAccessPoint.Text);

                byte[] cipher = SetLicense.EncryptLicense(license.GetRawBinary(), txtPinCode.Text);

                if (sfdLicenseFile.ShowDialog() == DialogResult.OK)
                {
                    string filename = sfdLicenseFile.FileName;

                    FileStream fs = new FileStream(filename, FileMode.Create);
                    fs.Write(cipher, 0, cipher.Length);
                    fs.Close();

                    Close();
                }
            }
            catch (DSAServerException ex)
            {
                ShowErrorMessage(ex);
            }
            catch (ConnectException ex)
            {
                MsgBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ArrangeExceptionMessage(ex));
            }
        }


        #region Connection Process
        private static void ShowErrorMessage(DSAServerException ex)
        {
            switch (ex.ServerStatus)
            {
                case DSAServerStatus.AccessDeny:
                    MsgBox.Show(DSAServerStatus.AccessDeny.ToString() + " 拒絕存取");
                    break;
                case DSAServerStatus.ApplicationUnavailable:
                    MsgBox.Show(DSAServerStatus.ApplicationUnavailable + " DSA Application 組態或狀態不正確");
                    break;
                case DSAServerStatus.CredentialInvalid:
                    MsgBox.Show("登入失敗，請確認帳號密碼");
                    break;
                case DSAServerStatus.InvalidRequestDocument:
                    MsgBox.Show(DSAServerStatus.InvalidRequestDocument + " 不合法的申請文件");
                    break;
                case DSAServerStatus.InvalidResponseDocument:
                    MsgBox.Show(DSAServerStatus.InvalidResponseDocument + " 不合法的回覆文件");
                    break;
                case DSAServerStatus.PassportExpire:
                    MsgBox.Show(DSAServerStatus.PassportExpire + " DSA Passport 過期");
                    break;
                case DSAServerStatus.ServerUnavailable:
                    MsgBox.Show(DSAServerStatus.ServerUnavailable + " DSA Server 組態或狀態不正確");
                    break;
                case DSAServerStatus.ServiceActivationError:
                    MsgBox.Show(DSAServerStatus.ServiceActivationError + " 服務啟動錯誤");
                    break;
                case DSAServerStatus.ServiceBusy:
                    MsgBox.Show(DSAServerStatus.ServiceBusy + " 服務忙碌");
                    break;
                case DSAServerStatus.ServiceExecutionError:
                    MsgBox.Show(DSAServerStatus.ServiceExecutionError + " 服務內部錯誤");
                    break;
                case DSAServerStatus.ServiceNotFound:
                    MsgBox.Show(DSAServerStatus.ServiceNotFound + " 服務不存在");
                    break;
                case DSAServerStatus.SessionExpire:
                    MsgBox.Show(DSAServerStatus.SessionExpire + " Session 過期");
                    break;
                case DSAServerStatus.Successful:
                    MsgBox.Show("成功");
                    break;
                case DSAServerStatus.UnhandledException:
                    MsgBox.Show(DSAServerStatus.UnhandledException + " DSA Server 未預期處理的 Exception");
                    break;
                case DSAServerStatus.Unknow:
                    MsgBox.Show(DSAServerStatus.Unknow + " 未知的狀態");
                    break;
                default:
                    switch (ex.ServerStatus.ToString())
                    {
                        case "513":
                            MsgBox.Show("連線到 DSNS 主機錯誤");
                            break;
                    }
                    break;
            }
        }

        private DSConnection ConnectToApplication()
        {
            DSConnection conn = new DSConnection();
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(SSLCertificateValidation);

            conn = new DSConnection(txtAccessPoint.Text, txtLoginName.Text, txtPassword.Text);
            conn.Connect();

            return conn;
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

        private bool SSLCertificateValidation(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            //只要是「CN=intellischool Root Authority」發的憑證都信任。
            return (certificate.Issuer == "CN=intellischool Root Authority");
        }
        #endregion

        private string ParseDate()
        {
            DateTime dt;

            if (DateTime.TryParse(txtExpireDate.Text, out dt))
                return dt.ToString(DateFormat);
            else
                throw new ArgumentException("授權到期日格式不正確。");
        }

        private void InsertIpEntry(XmlElement limit)
        {
            string[] ipentries = txtIPScope.Text.Split(',');
            DSXmlHelper hlplimit = new DSXmlHelper(limit);

            if (string.IsNullOrEmpty(txtIPScope.Text)) //不限制使用範圍。
            {
                hlplimit.AddElement(".", "IP").SetAttribute("Address", "0.0.0.0/0");
                return;
            }

            foreach (string ipentry in ipentries)
                hlplimit.AddElement(".", "IP").SetAttribute("Address", ipentry);
        }
    }
}