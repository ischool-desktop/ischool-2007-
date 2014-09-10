using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.Feature;
using IntelliSchool.DSA30.Util;
using System.Xml;
using DevComponents.DotNetBar;
using SmartSchool.ApplicationLog;

// Obsolete
namespace SmartSchool.StudentRelated.Palmerworm
{
    internal partial class UpdatePalmerwormItem : PalmerwormItem
    {
        private Dictionary<string,string> _headers;
        private bool _isInitialized = false;
        private DSXmlHelper _current_response;

        public UpdatePalmerwormItem()
        {
            InitializeComponent();
            Title = "異動資料";            
            Student.Instance.NewUpdateRecord += new EventHandler(Instance_NewUpdateRecord);
        }

        void Instance_NewUpdateRecord(object sender, EventArgs e)
        {
            LoadContent(RunningID);
        }

        private void Initialize()
        {
            if (_isInitialized) return;
            _headers = new Dictionary<string,string>();    
            _headers.Add("UpdateDate", "異動日期");
            _headers.Add("UpdateDescription", "異動事項");
            _headers.Add("ADNumber", "異動核准文號");
            _isInitialized = true;
        }

        protected override object OnBackgroundWorkerWorking()
        {
            return QueryStudent.GetUpdateInfoList(RunningID).GetContent();
        }

        protected override void OnBackgroundWorkerCompleted(object result)
        {
            Initialize();
            lstRecord.Clear();
            foreach (string key in _headers.Keys)
            {
                ColumnHeader ch = new ColumnHeader();
                ch.Text = _headers[key];
                ch.Tag = key;
                if (key == "UpdateDate")
                    ch.Width = 100;
                else if (key == "ADNumber")
                    ch.Width = 160;
                else
                    ch.Width = lstRecord.Width - 260;


                lstRecord.Columns.Add(ch);
            }

            DSXmlHelper helper = result as DSXmlHelper;
            _current_response = helper;
            foreach (XmlNode node in helper.GetElements("UpdateRecord"))
            {
                bool first = true;
                ListViewItem item = null;
                foreach (ColumnHeader ch in lstRecord.Columns)
                {
                    string value = node.SelectSingleNode(ch.Tag.ToString()).InnerText;
                    if (first)
                    {
                        item = lstRecord.Items.Add(value);
                        item.Tag = node.SelectSingleNode("@ID").InnerText;
                        first = false;
                    }
                    else
                        item.SubItems.Add(value);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {            
            UpdateRecordForm form = new UpdateRecordForm(RunningID, null);
            form.DataSaved += new EventHandler(form_DataSaved);
            form.ShowDialog();
        }

        void form_DataSaved(object sender, EventArgs e)
        {
            //_bgWorker.RunWorkerAsync();
            this.LoadContent(RunningID);
        }

        private void bthUpdate_Click(object sender, EventArgs e)
        {
            if (lstRecord.SelectedItems.Count < 1)
                MsgBox.Show("您必須先選擇一筆資料");
            if(lstRecord.SelectedItems.Count == 1)
            {
                string updateID = lstRecord.SelectedItems[0].Tag.ToString();
                UpdateRecordForm form = new UpdateRecordForm(RunningID, updateID);
                form.DataSaved += new EventHandler(form_DataSaved);
                form.ShowDialog();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstRecord.SelectedItems.Count < 1)
                MsgBox.Show("您必須先選擇一筆資料");
            if (lstRecord.SelectedItems.Count == 1)
            {
                string updateID = lstRecord.SelectedItems[0].Tag.ToString();
                if (MsgBox.Show("您確定將此筆異動資料永久刪除?", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        EditStudent.RemoveUpdateRecord(updateID);

                        //紀錄 Log，刪除異動紀錄
                        StringBuilder deleteDesc = new StringBuilder("");
                        deleteDesc.AppendLine("學生姓名："+Student.Instance.Items[RunningID].Name+" ");
                        deleteDesc.AppendLine("刪除異動紀錄： " + lstRecord.SelectedItems[0].SubItems[0].Text + " " + lstRecord.SelectedItems[0].SubItems[1].Text);
                        CurrentUser.Instance.AppLog.Write(EntityType.Student, EntityAction.Delete, RunningID, deleteDesc.ToString(), Title, "");

                        _bgWorker.RunWorkerAsync();
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("異動資料刪除失敗：" + ex.Message);
                    }
                }                
            }
        }
             

        void adn_DataSaved(object sender, EventArgs e)
        {
            LoadContent(RunningID);
        }

        private void UpdatePalmerwormItem_DoubleClick(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
                XmlBox.ShowXml(_current_response.BaseElement);
        }
    }
}
