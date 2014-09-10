using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.AccessControl;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.ApplicationLog;

namespace SmartSchool.StudentRelated.Palmerworm
{
    [FeatureCode("Content0135")]
    public partial class ExtensionValuesPalmerwormItem : SmartSchool.PalmerwormItemBase
    {
        private Dictionary<string, string> _Values = new Dictionary<string, string>();

        private string _RunningID, _CurrentID;

        private BackgroundWorker _BKW;

        public ExtensionValuesPalmerwormItem()
        {
            InitializeComponent();
            this.SaveButtonVisible = false;
            _BKW = new BackgroundWorker();
            _BKW.DoWork += delegate
            {
                XmlElement setting = Customization.Data.SystemInformation.Configuration["延伸欄位設定值"];
                foreach ( XmlElement var in setting.SelectNodes("延伸欄位") )
                {
                    _Values.Add(var.GetAttribute("欄位名稱"), "");
                }
                DSResponse resp = Feature.QueryStudent.GetExtension("", new string[0], new string[] { _RunningID });
                foreach ( XmlElement element in resp.GetContent().GetElements("Student") )
                {
                    foreach ( XmlNode node in element.ChildNodes )
                    {
                        if ( node is XmlElement )
                        {
                            XmlElement ele = (XmlElement)node;
                            if ( _Values.ContainsKey(ele.Name) )
                                _Values[ele.Name] = ele.InnerText;
                            else
                                _Values.Add(ele.Name, ele.InnerText);
                        }
                    }
                }
            };
            _BKW.RunWorkerCompleted += delegate
            {
                this.SaveButtonVisible = false;
                this.CancelButtonVisible = false;
                if ( this.IsDisposed )
                    return;
                if ( _RunningID != _CurrentID )
                {
                    _Values.Clear();
                    _RunningID = _CurrentID;
                    _BKW.RunWorkerAsync();
                    return;
                }
                WaitingPicVisible = false;
                foreach ( string key in _Values.Keys )
                {
                    dataGridViewEx1.Rows.Add(key, _Values[key]);
                }
            };
        }

        public override void LoadContent(string id)
        {
            this.SaveButtonVisible = false;
            this.CancelButtonVisible = false;
            _CurrentID = id;
            if ( !_BKW.IsBusy )
            {
                WaitingPicVisible = true;
                _RunningID = _CurrentID;
                _Values.Clear();
                dataGridViewEx1.Rows.Clear();
                _BKW.RunWorkerAsync();
            }
        }

        public override void Save()
        {
            StringBuilder desc = new StringBuilder("");
            desc.AppendLine("學生姓名：" + Student.Instance.Items[_CurrentID].Name + " ");
            try
            {
                List<string> removeField = new List<string>(_Values.Keys);
                SortedList<string, string> items = new SortedList<string, string>(1);
                items.Add(_CurrentID, "");
                foreach ( DataGridViewRow row in dataGridViewEx1.Rows )
                {
                    if ( row.IsNewRow ) continue;
                    string k = "" + row.Cells[0].Value, v = "" + row.Cells[1].Value;
                    if ( !_Values.ContainsKey(k) || _Values[k] != v )
                    {
                        items[_CurrentID] = v;
                        Feature.EditStudent.SetExtend("", k, items);
                        if ( _Values.ContainsKey(k) )
                            desc.AppendLine("欄位 「" + k + "」的值由「" + _Values[k] + "」變更為「" + v + "」");
                        else
                            desc.AppendLine("新增欄位 「" + k + "」的值為「" + v + "」");
                    }
                    if ( removeField.Contains(k) )
                    {
                        removeField.Remove(k);
                    }
                }
                items[_CurrentID] = "";
                foreach ( string removeKey in removeField )
                {
                    Feature.EditStudent.SetExtend("", removeKey, items);
                    desc.AppendLine("移除欄位 「" + removeKey + "」(原值為「" + _Values[removeKey] + "」)");
                }
            }
            catch { }
            CurrentUser.Instance.AppLog.Write(EntityType.Student, "修改自訂資料", _CurrentID, desc.ToString(), "", "");
            LoadContent(_CurrentID);
        }

        public override void Undo()
        {
            LoadContent(_CurrentID);
        }

        public override object Clone()
        {
            return new ExtensionValuesPalmerwormItem();
        }

        private void dataGridViewEx1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ( e.Button == MouseButtons.Left )
            {
                dataGridViewEx1.EndEdit();
            }
        }

        private void dataGridViewEx1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if ( dataGridViewEx1.SelectedCells.Count == 1 )
                dataGridViewEx1.BeginEdit(true);
        }

        private void dataGridViewEx1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dataGridViewEx1.EndEdit();
            CancelButtonVisible = CheckChanged();
            bool validated = true;
            if ( CancelButtonVisible )
            {
                #region 驗證資料
                List<string> keys = new List<string>();
                foreach ( DataGridViewRow row in dataGridViewEx1.Rows )
                {
                    if ( row.IsNewRow ) continue;
                    string k = "" + row.Cells[0].Value, v = "" + row.Cells[1].Value;
                    bool kPass = true ;
                    #region 驗欄位名稱
                    if ( row.Cells[0].ErrorText != "" )
                    {
                        row.Cells[0].ErrorText = "";
                        dataGridViewEx1.UpdateCellErrorText(0, row.Index);
                    }
                    foreach ( string var in new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" } )
                    {
                        if ( k.StartsWith(var) )
                        {
                            row.Cells[0].ErrorText = "欄位名稱開頭不可為數字。";
                            dataGridViewEx1.UpdateCellErrorText(0, row.Index);
                            kPass = false;
                        }
                    }
                    if ( kPass )
                    {
                        if ( k == "" )
                        {
                            row.Cells[0].ErrorText = "欄位名稱不可空白。";
                            dataGridViewEx1.UpdateCellErrorText(0, row.Index);
                            kPass = false;
                        }
                    }
                    if ( kPass )
                    {
                        if ( keys.Contains(k) )
                        {
                            row.Cells[0].ErrorText = "欄位名稱不得重複。";
                            dataGridViewEx1.UpdateCellErrorText(0, row.Index);
                            kPass = false;
                        }
                        else
                            keys.Add(k);
                    } 
                    #endregion
                    bool vPass= true;
                    #region 驗值
                    if ( v != "" )
                    {
                        XmlElement setting = Customization.Data.SystemInformation.Configuration["延伸欄位設定值"];
                        if ( row.Cells[1].ErrorText != "" )
                        {
                            row.Cells[1].ErrorText = "";
                            dataGridViewEx1.UpdateCellErrorText(1, row.Index);
                        }
                        foreach ( XmlElement var in setting.SelectNodes("延伸欄位[@欄位名稱='"+k+"']") )
                        {
                            switch ( var.GetAttribute("格式") )
                            { 
                                case "數字":
                                    decimal d;
                                    if ( !decimal.TryParse(v, out d) )
                                    {
                                        row.Cells[1].ErrorText = "必需輸入數字格式。";
                                        dataGridViewEx1.UpdateCellErrorText(1,row.Index);
                                        vPass = false;
                                    }
                                    break;
                                case "日期":
                                    DateTime t = DateTime.Now;
                                    if ( !DateTime.TryParse(v, out t) )
                                    {
                                        row.Cells[1].ErrorText = "必需輸入日期格式(西元年////月////日)。";
                                        dataGridViewEx1.UpdateCellErrorText(1, row.Index);
                                        vPass = false;
                                    }
                                    break;
                                case "文字":
                                default:
                                    break;
                            }
                        }
                    }
                    #endregion
                    validated = kPass & vPass;
                }
                #endregion
            }
            SaveButtonVisible = CancelButtonVisible & validated;
            dataGridViewEx1.BeginEdit(false);
        }

        private void dataGridViewEx1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            SaveButtonVisible = CancelButtonVisible = CheckChanged();
        }

        private bool CheckChanged()
        {
            List<string> removeField = new List<string>(_Values.Keys);
            foreach ( DataGridViewRow row in dataGridViewEx1.Rows )
            {
                if ( row.IsNewRow ) continue;
                string k = "" + row.Cells[0].Value, v = "" + row.Cells[1].Value;
                if ( !_Values.ContainsKey(k) || _Values[k] != v )
                {
                    return true;
                }
                if ( removeField.Contains(k) )
                    removeField.Remove(k);
                else
                    return  true;
            }
            if ( removeField.Count > 0 )
                return true;

            return false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if ( new ExtensionTemplateSetup().ShowDialog() == DialogResult.OK && this.CancelButtonVisible == false )
            {
                LoadContent(_CurrentID);
            }
        }
    }
}