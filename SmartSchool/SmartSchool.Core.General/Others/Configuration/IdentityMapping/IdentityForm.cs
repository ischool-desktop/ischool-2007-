using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Others.Configuration.IdentityMapping
{
    public partial class IdentityForm : BaseForm
    {
        public IdentityForm()
        {
            InitializeComponent();
        }

        private const string RootName = "學籍身分對照表";
        private bool _error = false;

        //private XmlElement SchoolConfig
        //{
        //    get { return CurrentUser.Instance.SchoolConfig.Content; }
        //}

        private XmlElement GetMappingTable()
        {
            //XmlElement table = (XmlElement)SchoolConfig.SelectSingleNode(RootName);
            XmlElement table = SmartSchool.Customization.Data.SystemInformation.Configuration[RootName];

            if (table == null) return DSXmlHelper.LoadXml(Properties.Resources.學籍身分對照表);
            return table;
        }

        private void SaveMappingTable(XmlElement table)
        {
            SmartSchool.Customization.Data.SystemInformation.Configuration[RootName] = table;
            //CurrentUser.Instance.SchoolConfig.Load();
            //XmlElement element = (XmlElement)SchoolConfig.SelectSingleNode(RootName);
            //if (element == null)
            //    SchoolConfig.AppendChild(SchoolConfig.OwnerDocument.ImportNode(table, true));
            //else
            //{
            //    XmlElement temp = table;
            //    if (element.OwnerDocument != table.OwnerDocument)
            //        temp = (XmlElement)element.OwnerDocument.ImportNode(table, true);
            //    SchoolConfig.ReplaceChild(temp, element);
            //}
            //CurrentUser.Instance.SchoolConfig.Save();

        }

        private void IdentityForm_Load(object sender, EventArgs e)
        {
            dataGridViewX1.Rows.Clear();
            dataGridViewX1.SuspendLayout();

            XmlElement config = GetMappingTable();
            DSXmlHelper configHelper = new DSXmlHelper(config);

            foreach (XmlElement element in configHelper.GetElements("Identity"))
            {
                DSXmlHelper helper = new DSXmlHelper(element);
                string name = helper.GetText("@Name");
                string code = helper.GetText("@Code");
                StringBuilder tags = new StringBuilder("");
                foreach (XmlElement tag in helper.GetElements("Tag"))
                {
                    if (!string.IsNullOrEmpty(tag.GetAttribute("FullName")))
                    {
                        if (tags.Length > 0) tags.Append(", ");
                        tags.Append(tag.GetAttribute("FullName"));
                    }
                }

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewX1, name, code, tags.ToString());
                dataGridViewX1.Rows.Add(row);
            }

            dataGridViewX1.ResumeLayout();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DSXmlHelper helper = new DSXmlHelper(RootName);
            dataGridViewX1.ClearSelection();

            if (_error)
                return;

            dataGridViewX1.SuspendLayout();

            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                if (row.IsNewRow) continue;
                DSXmlHelper each = new DSXmlHelper("Identity");
                each.SetAttribute(".", "Name", "" + row.Cells[colName.Name].Value);
                each.SetAttribute(".", "Code", "" + row.Cells[colCode.Name].Value);

                string tags_string = "" + row.Cells[colTags.Name].Value;
                string[] tags = tags_string.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string tag in tags)
                    each.AddElement(".", "Tag").SetAttribute("FullName", tag.Trim());

                helper.AddElement(".", each.BaseElement);
            }

            dataGridViewX1.ResumeLayout();
            SaveMappingTable(helper.BaseElement);
            this.DialogResult = DialogResult.OK;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            dataGridViewX1.BeginEdit(false);
        }

        private void dataGridViewX1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || dataGridViewX1.Columns[e.ColumnIndex] == colTags)
                return;

            DataGridViewRow current = dataGridViewX1.Rows[e.RowIndex];
            if (current.IsNewRow) return;

            current.Cells[colName.Name].ErrorText = current.Cells[colCode.Name].ErrorText = "";
            string current_name = "" + current.Cells[colName.Name].Value;
            string current_code = "" + current.Cells[colCode.Name].Value;

            _error = false;

            if (string.IsNullOrEmpty("" + current.Cells[e.ColumnIndex].Value))
            {
                current.Cells[e.ColumnIndex].ErrorText = "不可空白";
                _error = true;
            }

            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                if (row.IsNewRow || row == current) continue;
                string name = "" + row.Cells[colName.Name].Value;
                string code = "" + row.Cells[colCode.Name].Value;

                if (current_name == name)
                {
                    current.Cells[colName.Name].ErrorText = "身分重覆";
                    _error = true;
                }
                if (current_code == code)
                {
                    current.Cells[colCode.Name].ErrorText = "代號重覆";
                    _error = true;
                }
            }
        }
    }
}