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
using DevComponents.DotNetBar;
using SmartSchool.Feature.Basic;

namespace SmartSchool.Others.Configuration.Setup
{
    public partial class DeptSetup : BaseForm
    {
        private Dictionary<string, XmlElement> _idList;
        private Dictionary<string, string> _usedList;
        public DeptSetup()
        {
            _idList = new Dictionary<string, XmlElement>();
            _usedList = new Dictionary<string, string>();
            InitializeComponent();
        }

        private void DeptSetup_Load(object sender, EventArgs e)
        {
            DSResponse dsrsp = SmartSchool.Feature.Department.QueryDepartment.GetAbstractList();
            DSXmlHelper helper = dsrsp.GetContent();
            foreach (XmlElement element in helper.GetElements("Department"))
            {
                int index = dataGridView.Rows.Add();
                DataGridViewRow row = dataGridView.Rows[index];
                row.Cells[colCode.Name].Value = element.SelectSingleNode("Code").InnerText;
                row.Cells[colChiName.Name].Value = element.SelectSingleNode("Name").InnerText;
                row.Cells[colEngName.Name].Value = element.SelectSingleNode("EnglishName").InnerText;
                row.Tag = element.GetAttribute("ID");
                _idList.Add(element.GetAttribute("ID"), element);
            }

            dsrsp = SmartSchool.Feature.Department.QueryDepartment.GetUsedDepartment();
            foreach (XmlElement element in dsrsp.GetContent().GetElements("Department"))
            {
                _usedList.Add(element.GetAttribute("ID"), element.GetAttribute("Name"));
            }
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 1) return;
            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            if (row.IsNewRow) return;
            DataGridViewCell cell = row.Cells[1];
            cell.ErrorText = string.Empty;
            if (cell.Value == null)
                cell.ErrorText = "不可空白";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                MsgBox.Show("輸入資料有誤，請修正後再行儲存。", "驗證失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int insertCount = 0, updateCount = 0, deleteCount = 0;
            DSXmlHelper ihelper = new DSXmlHelper("Request");
            DSXmlHelper uhelper = new DSXmlHelper("Request");
            DSXmlHelper dhelper = new DSXmlHelper("Request");
            List<string> deleteKey = new List<string>();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Tag == null || row.Tag.ToString() == string.Empty)
                {
                    ihelper.AddElement("Department");
                    ihelper.AddElement("Department", "Name", GetValue(row.Cells[colChiName.Name]));
                    ihelper.AddElement("Department", "EnglishName", GetValue(row.Cells[colEngName.Name]));
                    ihelper.AddElement("Department", "Code", GetValue(row.Cells[colCode.Name]));
                    insertCount++;
                }
                else
                {
                    uhelper.AddElement("Department");
                    uhelper.AddElement("Department", "Name", GetValue(row.Cells[colChiName.Name]));
                    uhelper.AddElement("Department", "EnglishName", GetValue(row.Cells[colEngName.Name]));
                    uhelper.AddElement("Department", "Code", GetValue(row.Cells[colCode.Name]));
                    uhelper.AddElement("Department", "ID", row.Tag.ToString());
                    updateCount++;
                }
            }

            dhelper.AddElement("Department");
            dhelper.AddElement("Department", "ID", "-1");
            foreach (string id in _idList.Keys)
            {
                bool isdeleted = true;
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Tag == null || row.Tag.ToString() == string.Empty) continue;
                    if (id == row.Tag.ToString())
                    {
                        isdeleted = false;
                        break;
                    }
                }
                if (isdeleted)
                {
                    dhelper.AddElement("Department", "ID", id);
                    deleteCount++;
                }
            }

            try
            {
                if (insertCount > 0)
                    SmartSchool.Feature.Department.AddDepartment.Insert(new DSRequest(ihelper));
                if (updateCount > 0)
                    SmartSchool.Feature.Department.EditDepartment.Update(new DSRequest(uhelper));
                if (deleteCount > 0)
                    SmartSchool.Feature.Department.RemoveDepartment.Delete(new DSRequest(dhelper));

                DataCacheManager.Remove("GetDepartment");//把快取刪除。
            }
            catch (Exception ex)
            {
                MsgBox.Show("資料儲存失敗:" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Close();
        }

        private bool IsValid()
        {
            bool valid = true;
            List<string> deptdump = new List<string>();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow) continue;

                DataGridViewCell cell = row.Cells[colChiName.Name];
                cell.ErrorText = string.Empty;

                if (cell.Value == null)
                {
                    cell.ErrorText = "不可空白";
                    valid = false;
                }

                string name = cell.Value + string.Empty;
                if (name == string.Empty)
                    continue;

                if (deptdump.Contains(name))
                {
                    cell.ErrorText = "科別名稱重覆。";
                    valid = false;
                }
                else
                    deptdump.Add(name);
            }
            return valid;
        }

        private string GetValue(DataGridViewCell cell)
        {
            if (cell.Value == null)
                return string.Empty;
            return cell.Value.ToString();
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row.Tag == null) return;
            if (_usedList.ContainsKey(e.Row.Tag.ToString()))
            {
                MsgBox.Show("已有班級使用【" + _usedList[e.Row.Tag.ToString()] + "】，無法刪除！", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                e.Cancel = true;
            }
        }
    }
}