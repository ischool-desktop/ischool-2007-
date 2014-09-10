using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature.Basic;
using System.Xml;
using DevComponents.DotNetBar;
using DevComponents.Editors;
using Aspose.Cells;

namespace SmartSchool.Others.Configuration.WordCommentMapping
{
    public partial class TextCommentForm : BaseForm
    {
        private Dictionary<string, CodeItemCollection> _allcomments;

        public TextCommentForm()
        {
            InitializeComponent();
            _allcomments = new Dictionary<string, CodeItemCollection>();
            InitialList();
        }

        public Dictionary<string, CodeItemCollection> AllComments
        {
            get { return _allcomments; }
            set { _allcomments = value; }
        }

        private ButtonItem _current_selected = null;
        public ButtonItem CurrentSelected
        {
            get { return _current_selected; }
            set { _current_selected = value; }
        }

        private void InitialList()
        {
            DSResponse dsrsp = Config.GetWordCommentList();
            foreach (XmlElement var in dsrsp.GetContent().GetElements("Content/Morality"))
            {
                string face = var.GetAttribute("Face");
                CodeItemCollection codes = new CodeItemCollection();
                foreach (XmlElement eachItem in var.SelectNodes("Item"))
                {
                    string code = eachItem.GetAttribute("Code");
                    string comment = eachItem.GetAttribute("Comment");
                    codes.Add(new CodeItem(code, comment));
                }

                AllComments.Add(face, codes);
            }

            ipMoralities.Items.Clear();
            foreach (KeyValuePair<string, CodeItemCollection> each in AllComments)
            {
                ButtonItem bitem = AddMoralityFace(each.Key);
            }
        }

        private ButtonItem AddMoralityFace(string faceName)
        {
            ButtonItem bitem = new ButtonItem();
            bitem.Text = faceName;
            bitem.OptionGroup = "Morality";
            bitem.Click += new EventHandler(bitem_Click);

            ipMoralities.Items.Add(bitem);
            ipMoralities.Refresh();

            return bitem;
        }

        private void dataview_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            SyncData();
        }

        private void dataview_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (dataview.Columns[e.ColumnIndex] != chCode) return;

            ValidateFace();
        }

        private void ValidateFace()
        {
            List<string> codes = new List<string>();
            foreach (DataGridViewRow row in dataview.Rows)
            {
                DataGridViewCell cell = row.Cells[chCode.Index];
                string code = "" + cell.Value;
                cell.ErrorText = string.Empty;
                if (!codes.Contains(code)) codes.Add(code);
                else cell.ErrorText = "代碼不能重覆";
            }
        }

        private void SyncData()
        {
            try
            {
                if (CurrentSelected == null)
                    return;

                string face = CurrentSelected.Text;
                CodeItemCollection codes = AllComments[face];

                codes.Clear();
                foreach (DataGridViewRow each in dataview.Rows)
                {
                    if (each.IsNewRow) continue;

                    string code = each.Cells[chCode.Name].Value + "";
                    string comment = each.Cells[chComment.Name].Value + "";

                    codes.Add(new CodeItem(code, comment));
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
        }

        private void bitem_Click(object sender, EventArgs e)
        {
            try
            {
                ButtonItem bitem = sender as ButtonItem;
                string face = bitem.Text;

                CodeItemCollection codes = AllComments[face];

                dataview.Rows.Clear();
                foreach (CodeItem each in codes)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataview, each.Code, each.Comment);

                    dataview.Rows.Add(row);
                }

                CurrentSelected = bitem;
                peTemplateName1.Text = CurrentSelected.Text;

                ValidateFace();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
        }

        public class CodeItemCollection : List<CodeItem>
        {
            public CodeItemCollection()
            {
            }

            public CodeItemCollection(IEnumerable<CodeItem> copyList)
                : base(copyList)
            {
            }
        }

        public class CodeItem
        {
            public CodeItem(string code, string comment)
            {
                Code = code;
                Comment = comment;
            }

            private string _code;
            public string Code
            {
                get { return _code; }
                set { _code = value; }
            }

            private string _comment;
            public string Comment
            {
                get { return _comment; }
                set { _comment = value; }
            }
        }

        #region CRUD Button Event
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CurrentSelected == null)
                return;

            string face = CurrentSelected.Text;

            string msg = string.Format("確定要刪除{0}項目？", face);
            if (MsgBox.Show(msg, Application.ProductName, MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            CurrentSelected.Click -= new EventHandler(bitem_Click);
            AllComments.Remove(face);
            ipMoralities.Items.Remove(CurrentSelected);
            ipMoralities.Refresh();

            CurrentSelected = null;
            dataview.Rows.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<string> faces = GetInvalidFaces();
            if (faces.Count > 0)
            {
                StringBuilder builder = new StringBuilder("");
                builder.AppendLine("下列項目的代碼有重覆，請修正。");
                builder.AppendLine("");
                foreach (string face in faces)
                    builder.AppendLine("．" + face);
                MsgBox.Show(builder.ToString(), "錯誤");
                return;
            }

            try
            {
                DSXmlHelper request = new DSXmlHelper("Request");
                DSXmlHelper content = new DSXmlHelper(request.AddElement("Content"));

                foreach (KeyValuePair<string, CodeItemCollection> each in AllComments)
                {
                    DSXmlHelper morality = new DSXmlHelper(content.AddElement("Morality"));
                    morality.SetAttribute(".", "Face", each.Key);

                    foreach (CodeItem eachItem in each.Value)
                    {
                        XmlElement code = morality.AddElement("Item");
                        code.SetAttribute("Code", eachItem.Code);
                        code.SetAttribute("Comment", eachItem.Comment);
                    }
                }

                Config.SetWordCommentList(request.BaseElement);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
        }

        private List<string> GetInvalidFaces()
        {
            List<string> faces = new List<string>();
            foreach (string face in AllComments.Keys)
            {
                CodeItemCollection collection = AllComments[face];
                List<string> list = new List<string>();

                foreach (CodeItem item in collection)
                {
                    if (!list.Contains(item.Code)) list.Add(item.Code);
                    else
                    {
                        faces.Add(face);
                        break;
                    }
                }
            }
            return faces;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddTextCommentFace addface = new AddTextCommentFace(AllComments);

            if (addface.ShowDialog() == DialogResult.OK)
            {
                AddMoralityFace(addface.NewName);

                CodeItemCollection codes = new CodeItemCollection();
                if (addface.SelectedItem != null && addface.SelectedItem is ComboItem)
                {
                    codes = (addface.SelectedItem as ComboItem).Tag as CodeItemCollection;
                    codes = new CodeItemCollection(codes); //用複制的。
                }
                AllComments.Add(addface.NewName, codes);
            }
        }
        #endregion

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (CurrentSelected == null)
                return;

            string face = CurrentSelected.Text;

            Workbook wb = new Workbook();
            wb.Worksheets.Clear();
            Worksheet ws = wb.Worksheets[wb.Worksheets.Add()];
            ws.Name = face;

            ws.Cells.CreateRange(0, 1, true).ColumnWidth = 10;
            ws.Cells.CreateRange(1, 1, true).ColumnWidth = 40;

            ws.Cells[0, 0].PutValue("代碼");
            ws.Cells[0, 1].PutValue("文字評量");

            int rowIndex = 1;

            CodeItemCollection codes = AllComments[face];

            foreach (CodeItem each in codes)
            {
                ws.Cells[rowIndex, 0].PutValue(each.Code);
                ws.Cells[rowIndex, 1].PutValue(each.Comment);
                rowIndex++;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = face + "_文字評量代碼表.xls";
            sfd.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    wb.Save(sfd.FileName, FileFormatType.Excel2003);
                    MsgBox.Show("匯出成功。");
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            Workbook wb = new Workbook();
            Dictionary<string, string> importCodeList = new Dictionary<string, string>();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "選擇要匯入的文字評量代碼表";
            ofd.Filter = "Excel檔案 (*.xls)|*.xls";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    wb.Open(ofd.FileName);
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "開啟檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
                return;

            if (wb.Worksheets[0].Cells[0, 0].StringValue != "代碼" || wb.Worksheets[0].Cells[0, 1].StringValue != "文字評量")
            {
                MsgBox.Show("匯入格式不符合。");
                return;
            }

            OverrideConfirm form = new OverrideConfirm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                int rowIndex = 1;
                Worksheet ws = wb.Worksheets[0];

                while (!string.IsNullOrEmpty(ws.Cells[rowIndex, 0].StringValue))
                {
                    string code = ws.Cells[rowIndex, 0].StringValue;
                    string comment = ws.Cells[rowIndex, 1].StringValue;

                    if (!importCodeList.ContainsKey(code))
                        importCodeList.Add(code, comment);
                    else
                        importCodeList[code] = comment;
                    rowIndex++;
                }

                if (form.Overwrite)
                {
                    dataview.Rows.Clear();
                    foreach (string key in importCodeList.Keys)
                    {
                        int index = dataview.Rows.Add();
                        DataGridViewRow row = dataview.Rows[index];
                        row.Cells[chCode.Name].Value = key;
                        row.Cells[chComment.Name].Value = importCodeList[key];

                    }
                }
                else
                {
                    Dictionary<string, int> OriginalCodeListIndex = new Dictionary<string, int>();
                    List<int> delete = new List<int>();

                    foreach (DataGridViewRow row in dataview.Rows)
                    {
                        if (row.IsNewRow) continue;
                        if (row.Cells[chCode.Name].Value != null)
                        {
                            string code = row.Cells[chCode.Name].Value.ToString();
                            if (!OriginalCodeListIndex.ContainsKey(code))
                                OriginalCodeListIndex.Add(code, row.Index);
                            else
                            {
                                delete.Add(OriginalCodeListIndex[code]);
                                OriginalCodeListIndex[code] = row.Index;
                            }
                        }
                    }

                    foreach (string key in importCodeList.Keys)
                    {
                        if (OriginalCodeListIndex.ContainsKey(key))
                            dataview.Rows[OriginalCodeListIndex[key]].Cells[chComment.Name].Value = importCodeList[key];
                        else
                        {
                            int index = dataview.Rows.Add();
                            DataGridViewRow row = dataview.Rows[index];
                            row.Cells[chCode.Name].Value = key;
                            row.Cells[chComment.Name].Value = importCodeList[key];
                        }
                    }

                    foreach (int var in delete)
                    {
                        dataview.Rows.RemoveAt(var);
                    }
                }

                SyncData();
                MsgBox.Show("匯入完成");
            }
        }


    }
}