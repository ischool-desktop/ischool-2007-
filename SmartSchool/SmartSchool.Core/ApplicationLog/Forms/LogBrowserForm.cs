using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.Feature.ApplicationLog;
using DevComponents.DotNetBar.Controls;
using SmartSchool.ExceptionHandler;
//using SmartSchool.StudentRelated;
using System.Globalization;
using SmartSchool.Common.DateTimeProcess;
using DevComponents.DotNetBar.Rendering;
using SmartSchool.AccessControl;

namespace SmartSchool.ApplicationLog.Forms
{
    public partial class LogBrowserForm : BaseForm
    {
        private const string DateTimeFormat = "yyyy/MM/dd HH:mm:ss";

        private EnhancedErrorProvider _errors;
        private string _entity;
        private string[] _entityId = new string[0];
        private string _user_name;

        [FeatureCode("System0020")]
        public LogBrowserForm()
        {
            ConstructorLogBrowserFrom();

            Text = "日誌瀏覽";

            if ( GlobalManager.Renderer is Office2007Renderer )
            {
                ( GlobalManager.Renderer as Office2007Renderer ).ColorTableChanged += new EventHandler(ContentInfo_ColorTableChanged);
                this.dgvLogs.AlternatingRowsDefaultCellStyle.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.MouseOver.TopBackground.End;
            }
        }

        [FeatureCode("System0010")]
        public LogBrowserForm(string userName)
        {
            ConstructorLogBrowserFrom();

            Text = "日誌瀏覽 帳號：" + userName;
            _user_name = userName;

            if ( GlobalManager.Renderer is Office2007Renderer )
            {
                ( GlobalManager.Renderer as Office2007Renderer ).ColorTableChanged += new EventHandler(ContentInfo_ColorTableChanged);
                this.dgvLogs.AlternatingRowsDefaultCellStyle.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.MouseOver.TopBackground.End;
            }
        }

        void ContentInfo_ColorTableChanged(object sender, EventArgs e)
        {
            this.dgvLogs.AlternatingRowsDefaultCellStyle.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.MouseOver.TopBackground.End;
        }


        public LogBrowserForm(EntityType entity, params string[] entityId)
        {
            ConstructorLogBrowserFrom();

            Text = "日誌瀏覽";

            _entity = EntityTypeName.Get(entity);
            _entityId = entityId;
        }

        private void ConstructorLogBrowserFrom()
        {
            InitializeComponent();

            _errors = new EnhancedErrorProvider();
            txtStartTime.Text = DateTime.Now.ToString(DateTimeHelper.StdDateFormat, DateTimeFormatInfo.InvariantInfo);
            txtEndTime.Text = DateTime.Now.ToString(DateTimeHelper.StdDateFormat, DateTimeFormatInfo.InvariantInfo);
        }

        private void btnRefresn_Click(object sender, EventArgs e)
        {
            RefreshLogList();
        }

        private void LogBrowserForm_Load(object sender, EventArgs e)
        {
            RefreshLogList();
        }

        private void RefreshLogList()
        {
            try
            {
                DSXmlHelper request = new DSXmlHelper("QueryRequest");
                request.AddElement("Field");
                request.AddElement("Field", "All");

                request.AddElement("Condition");
                if (chkByTime.Checked) //加入時間條件
                {
                    if (_errors.HasError)
                    {
                        MsgBox.Show("日期格式有錯誤，請修正。");
                        return;
                    }

                    DateTime dt = (DateTime)DateTimeHelper.ParseGregorian(txtStartTime.Text, PaddingMethod.First);
                    request.AddElement("Condition", "StartServerTimestamp", dt.ToString(DateTimeHelper.StdDateTimeFormat));

                    dt = (DateTime)DateTimeHelper.ParseGregorian(txtEndTime.Text, PaddingMethod.Last);
                    request.AddElement("Condition", "EndServerTimestamp", dt.ToString(DateTimeHelper.StdDateTimeFormat));
                }
                else
                {
                    string msg = "您未指定時間作為條件，回傳資料量可能很大，會造成系統停止回應，你確定要執行？";
                    DialogResult dr = MsgBox.Show(msg, Application.ProductName, MessageBoxButtons.YesNo);
                    if (dr == DialogResult.No)
                    {
                        dgvLogs.Rows.Clear();
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(_user_name))
                {
                    request.AddElement("Condition", "UserName", _user_name);
                }

                if (!string.IsNullOrEmpty(_entity))
                {
                    request.AddElement("Condition", "Entity", _entity);
                }

                foreach (string each in _entityId)
                {
                    if ((!string.IsNullOrEmpty(each)))
                    {
                        request.AddElement("Condition", "EntityID", each);
                    }
                }

                request.AddElement("Order");
                request.AddElement("Order", "ServerTimestamp", "Desc");

                if (Control.ModifierKeys == Keys.Shift)
                    MsgBox.Show(DSXmlHelper.Format(request.ToString()));

                XmlElement response = Manage.QueryLog(request.BaseElement);

                dgvLogs.Rows.Clear();
                foreach (XmlElement each in response.SelectNodes("Log"))
                {
                    string time, user, ip, action, desc;
                    time = each.SelectSingleNode("ServerTimestamp").InnerText;
                    time = DateTime.Parse(time, DateTimeFormatInfo.InvariantInfo).ToString(DateTimeFormat, DateTimeFormatInfo.InvariantInfo);
                    user = each.SelectSingleNode("UserName").InnerText;
                    ip = each.SelectSingleNode("Location").InnerText;
                    action = each.SelectSingleNode("Action").InnerText;
                    desc = each.SelectSingleNode("Description").InnerText;

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgvLogs, time, user, ip, action, desc);
                    row.Tag = each;

                    dgvLogs.Rows.Add(row);
                    row.Cells["colDesc"].ToolTipText = desc;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show("查詢日誌錯誤(錯誤已回報)。");

                CurrentUser user = CurrentUser.Instance;
                BugReporter.ReportException(user.SystemName, user.SystemVersion, ex, false);
            }
        }

        private void txtTime_Validating(object sender, CancelEventArgs e)
        {
            if (!IsValidDateTime((sender as TextBoxX).Text))
                _errors.SetError(sender as Control, "日格式應為「年/月/日 時/分/秒」，例：2007/1/5 14:10:10");
            else
                _errors.SetError(sender as Control, "");
        }

        private static bool IsValidDateTime(string p)
        {
            try
            {
                DateTime? dt = DateTimeHelper.ParseGregorian(p, PaddingMethod.Now);
                return dt.HasValue;
            }
            catch
            {
                return false;
            }
        }

        private void dgvLogs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLogs.Rows[e.RowIndex];
                XmlBox.ShowXml(row.Tag as XmlElement, this);
            }
        }

        private void txtTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                RefreshLogList();
        }

        private void chkListAll_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkListAll.Checked) return;

            RefreshLogList();
            Console.WriteLine((sender as Control).Name);
        }

        private void chkByTime_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkByTime.Checked) return;

            RefreshLogList();
            Console.WriteLine((sender as Control).Name);
        }
    }
}