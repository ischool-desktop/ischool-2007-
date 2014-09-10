using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DevComponents.DotNetBar;
using SmartSchool.Common;

namespace SmartSchool.ClassRelated.RibbonBars.Reports
{
    public partial class SelectPeriodForm : BaseForm
    {
        private BackgroundWorker _BGWPeriodList;
        private string _key;

        private List<string> periodList = new List<string>();

        public SelectPeriodForm(string key)
        {
            InitializeComponent();
            _key = key;
        }

        void _BGWPeriodList_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBox1.Visible = false;

            foreach (string period in periodList)
            {
                ListViewItem item = new ListViewItem(period);
                listViewEx1.Items.Add(item);
            }

            #region 讀取列印設定 Preference

            //XmlElement config = CurrentUser.Instance.Preference["缺曠週報表_依節次統計_列印設定"];
            XmlElement config = CurrentUser.Instance.Preference[_key];

            if (config != null)
            {
                #region 已有設定檔則將設定檔內容填回畫面上

                foreach (XmlElement period in config.SelectNodes("Period"))
                {
                    string name = period.GetAttribute("Name");
                    foreach (ListViewItem item in listViewEx1.Items)
                    {
                        if (item.Text == name)
                            item.Checked = true;
                    }
                }

                #endregion
            }
            else
            {
                #region 產生空白設定檔
                config = new XmlDocument().CreateElement(_key);
                CurrentUser.Instance.Preference[_key] = config;
                #endregion
            }

            #endregion
        }

        void _BGWPeriodList_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (XmlElement var in SmartSchool.Feature.Basic.Config.GetPeriodList().GetContent().GetElements("Period"))
            {
                if (!periodList.Contains(var.GetAttribute("Name")))
                    periodList.Add(var.GetAttribute("Name"));
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            #region 更新列印設定 Preference

            XmlElement config = CurrentUser.Instance.Preference[_key];

            if (config == null)
            {
                config = new XmlDocument().CreateElement(_key);
            }

            config.RemoveAll();

            foreach (ListViewItem item in listViewEx1.Items)
            {
                XmlElement period = config.OwnerDocument.CreateElement("Period");
                period.SetAttribute("Name", item.Text);
                if (item.Checked == true)
                    config.AppendChild(period);
            }

            CurrentUser.Instance.Preference[_key] = config;

            #endregion

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SelectPeriodForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {

                _BGWPeriodList = new BackgroundWorker();
                _BGWPeriodList.DoWork += new DoWorkEventHandler(_BGWPeriodList_DoWork);
                _BGWPeriodList.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BGWPeriodList_RunWorkerCompleted);
                _BGWPeriodList.RunWorkerAsync();
                listViewEx1.Items.Clear();
            }
        }
    }
}