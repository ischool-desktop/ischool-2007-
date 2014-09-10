using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;
using System.Xml;
using SmartSchool.Common;

namespace SmartSchool
{
    internal partial class DSNSForm : SmartSchool.Common.BaseForm
    {
        private LoginInfoManager _loginManager;
        private LoginForm _parentForm;

        public DSNSForm(LoginForm parent)
        {
            InitializeComponent();
            _loginManager = new LoginInfoManager();
            txtDSNS.Text = _loginManager.GetDomainName();
            _parentForm = parent;
            _parentForm.FormClosing += new FormClosingEventHandler(_parentForm_FormClosing);

            Font = SmartSchool.Common.FontStyles.General;
        }

        void _parentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtDSNS.Text.Trim().Length > 0)
            {
                _loginManager.SaveDomainName(this.txtDSNS.Text);
                this.Hide();
            }
            else
            {
                MsgBox.Show("此欄不可空白");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public string GetDomainName()
        {
            return _loginManager.GetDomainName();
        }

        public List<string> GetHistoryLoginNames()
        {
            return _loginManager.GetHistoryLoginNames();
        }

        public void AddHistoryLoginName(string name)
        {
            _loginManager.AddHistoryLoginName(name);
        }

        public void RemoveHistoryLoginName(string name)
        {
            _loginManager.RemoveHistoryLoginName(name);
        }

        private void DSNSForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }

    internal class LoginInfoManager
    {
        private string filename;
        private bool hasLoginInfo;
        private XmlNode _info;

        public LoginInfoManager()
        {
            filename = Application.StartupPath + "\\" + "Lucifer.dll";
            hasLoginInfo = File.Exists(filename);
            if (hasLoginInfo)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filename);
                _info = doc.SelectSingleNode("Informations");
            }
            else
            {
                CreateEmptyInfo();
            }
        }

        public bool HasContent()
        {
            return _info != null;
        }

        public XmlNode GetInfo()
        {
            if (!hasLoginInfo) return null;
            return _info;
        }

        public void SaveInfo(XmlNode node)
        {
            _info = node;
            _info.OwnerDocument.Save(filename);
        }

        public void CreateEmptyInfo()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Informations");
            doc.AppendChild(root);
            XmlElement e1 = doc.CreateElement("DSNS");
            root.AppendChild(e1);
            SaveInfo(root);
        }

        public void SaveDomainName(string dsns)
        {
            string nodeName = "DSNS";

            if (_info.SelectSingleNode(nodeName) == null)
                _info.AppendChild(_info.OwnerDocument.CreateElement(nodeName));

            _info.SelectSingleNode(nodeName).InnerText = dsns;
            SaveInfo(_info);
        }

        public string GetDomainName()
        {
            if (_info != null)
            {
                XmlNode node = _info.SelectSingleNode("DSNS");
                if (node != null) return node.InnerText;
            }
            return null;
        }

        public List<string> GetHistoryLoginNames()
        {
            List<string> names = new List<string>();
            foreach (XmlNode node in _info.SelectNodes("LoginAccountList/LoginAccount/LoginName"))
            {
                names.Add(node.InnerText);
            }
            return Resort(names);
        }

        public void AddHistoryLoginName(string name)
        {
            if (GetHistoryLoginNames().Contains(name))
            {
                XmlNode parentNode = _info.SelectSingleNode("LoginAccountList");
                foreach (XmlNode node in parentNode.SelectNodes("LoginAccount"))
                {
                    if ( node.SelectSingleNode("LoginName") != null && node.SelectSingleNode("LoginName").InnerText == name )
                    {
                        parentNode.RemoveChild(node);
                        AddHistoryLoginName(name);
                    }
                }
            }
            else if (!GetHistoryLoginNames().Contains(name))
            {
                XmlNode node = _info.SelectSingleNode("LoginAccountList");
                if (node == null)
                {
                    XmlElement element = _info.OwnerDocument.CreateElement("LoginAccountList");
                    _info.AppendChild(element);
                }
                node = _info.SelectSingleNode("LoginAccountList");
                XmlElement e = _info.OwnerDocument.CreateElement("LoginAccount");
                node.AppendChild(e);
                XmlElement e2 = _info.OwnerDocument.CreateElement("LoginName");
                e2.InnerText = name;
                e.AppendChild(e2);
                SaveInfo(_info);
            }
        }

        public void RemoveHistoryLoginName(string name)
        {
            if (GetHistoryLoginNames().Contains(name))
            {
                XmlNode parentNode = _info.SelectSingleNode("LoginAccountList");
                foreach (XmlNode node in parentNode.SelectNodes("LoginAccount"))
                {
                    if (node.SelectSingleNode("LoginName").InnerText == name)
                    {
                        parentNode.RemoveChild(node);
                    }
                }
            }
        }


        private List<string> Resort(List<string> names)
        {
            List<string> values = new List<string>();
            int lastIndex = names.Count - 1;

            for (int i = lastIndex; i >= 0; i--)
            {
                values.Add(names[i]);
            }
            return values;
        }
    }
}