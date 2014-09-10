using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using System.Xml;
using SmartSchool.Feature.ExamTemplate;
using DevComponents.DotNetBar;
using SmartSchool.ExceptionHandler;
using SmartSchool.Feature.Course;
using System.Collections;
using DevComponents.DotNetBar.Controls;
using System.Globalization;
using IntelliSchool.DSA30.Util;
//using SmartSchool.SmartPlugIn.Properties;
using SmartSchool.Common.DateTimeProcess;
using SmartSchool.CourseRelated;

namespace SmartSchool.Others.Configuration.ExamTemplate
{
    public partial class TemplateManager : BaseForm
    {
        private const string DirtyMarkString = "<b><font color=\"#ED1C24\">(已修改)</font></b>";
        private const string OutputFormat = "yyyy/MM/dd HH:mm";

        private EnhancedErrorProvider errors;
        private TemplateItem _current_template;
        private bool _grid_clearing = false;

        public TemplateManager()
        {
            InitializeComponent();

            npLeft.NavigationBar.Visible = false;

            errors = new EnhancedErrorProvider();

            npLeft.TitlePanel.Font = new Font(FontStyles.GeneralFontFamily, 12);

            ExamRow row = new ExamRow();
            dataview.RowTemplate = row;
            dataview.AllowUserToAddRows = true;
        }

        private void TemplateManager_Load(object sender, EventArgs e)
        {
            LoadTemplates();
        }

        private TemplateItem CurrentTemplate
        {
            get { return _current_template; }
            set { _current_template = value; }
        }

        private IEnumerable EachExamRow
        {
            get { return (IEnumerable)dataview.Rows; }
        }

        private void TemplateItem_Click(object sender, EventArgs e)
        {
            if (CurrentTemplate == sender) return;

            if (CurrentTemplate != null && CurrentTemplate.IsDirty)
            {
                string msg = "「" + CurrentTemplate.TemplateName + "」評分樣版已修改，是否儲存？";
                DialogResult dr = MsgBox.Show(msg, "", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    if (ContainsError())
                    {
                        MsgBox.Show("資料有錯誤，請修改後再儲存。");
                        CurrentTemplate.Checked = true;
                        return;
                    }

                    SaveCurrentTemplate();
                }
            }

            SelectTemplateItem(sender as TemplateItem);
        }

        private void TemplateItem_DoubleClick(object sender, EventArgs e)
        {
            TemplateNameEditor editor = new TemplateNameEditor(this, CurrentTemplate.TemplateName);
            DialogResult dr = editor.ShowDialog();

            if (dr == DialogResult.OK)
            {
                try
                {
                    EditTemplate.Rename(CurrentTemplate.Identity, editor.TemplateName);

                    CurrentTemplate.TemplateName = editor.TemplateName;
                    CourseEntity.Instance.InvokeForeignTableChanged();

                    RefreshDirtyStatus();
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message);
                    CurrentUser user = CurrentUser.Instance;
                    BugReporter.ReportException(user.SystemName, user.SystemVersion, ex, false);
                }
            }
        }

        private void SaveCurrentTemplate()
        {
            List<ExamItem> insert, delete, update;
            insert = new List<ExamItem>();
            delete = new List<ExamItem>();
            update = new List<ExamItem>();

            foreach (ExamRow each in EachExamRow)
            {
                if (!each.IsNewRow && each.IsDirty)
                {
                    switch (each.UpdateAction)
                    {
                        case ExamItemUpdateAction.Add:
                            insert.Add(each.ExamInformation);
                            break;
                        case ExamItemUpdateAction.Update:
                            update.Add(each.ExamInformation);
                            break;
                        case ExamItemUpdateAction.Delete:
                            delete.Add(each.ExamInformation);
                            break;
                    }
                }
            }

            try
            {
                DSXmlHelper insertReq = new DSXmlHelper("InsertIncludeExamRequest");
                DSXmlHelper updateReq = new DSXmlHelper("UpdateIncludeExamRequest");
                DSXmlHelper deleteReq = new DSXmlHelper("DeleteIncludeExamRequest");

                foreach (ExamItem each in insert)
                {
                    insertReq.AddElement("IncludeExam");
                    insertReq.AddElement("IncludeExam", "RefExamTemplateID", CurrentTemplate.Identity);
                    insertReq.AddElement("IncludeExam", "RefExamID", each.RefExamID);
                    insertReq.AddElement("IncludeExam", "UseScore", each.UseScore);
                    insertReq.AddElement("IncludeExam", "UseText", each.UseText);
                    insertReq.AddElement("IncludeExam", "Weight", each.Weight);
                    insertReq.AddElement("IncludeExam", "OpenTeacherAccess", each.OpenTeacherAccess);
                    insertReq.AddElement("IncludeExam", "StartTime", each.StartTimeStdFormat);
                    insertReq.AddElement("IncludeExam", "EndTime", each.EndTimeStdFormat);
                }

                foreach (ExamItem each in update)
                {
                    updateReq.AddElement("IncludeExam");
                    updateReq.AddElement("IncludeExam", "RefExamID", each.RefExamID);
                    updateReq.AddElement("IncludeExam", "UseScore", each.UseScore);
                    updateReq.AddElement("IncludeExam", "UseText", each.UseText);
                    updateReq.AddElement("IncludeExam", "Weight", each.Weight);
                    updateReq.AddElement("IncludeExam", "OpenTeacherAccess", each.OpenTeacherAccess);
                    updateReq.AddElement("IncludeExam", "StartTime", each.StartTimeStdFormat);
                    updateReq.AddElement("IncludeExam", "EndTime", each.EndTimeStdFormat);

                    updateReq.AddElement("IncludeExam", "Condition", "<ID>" + each.Identity + "</ID>", true);
                }

                foreach (ExamItem each in delete)
                    deleteReq.AddElement(".", "IncludeExam", "<ID>" + each.Identity + "</ID>", true);

                if (delete.Count > 0)
                    EditTemplate.DeleteIncludeExam(deleteReq.BaseElement);

                if (update.Count > 0)
                    EditTemplate.UpdateIncludeExam(updateReq.BaseElement);

                if (insert.Count > 0)
                    EditTemplate.InsertIncludeExam(insertReq.BaseElement);

                SelectTemplateItem(CurrentTemplate); //重新載入
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, Application.ProductName);

                CurrentUser user = CurrentUser.Instance;
                BugReporter.ReportException(user.SystemName, user.SystemVersion, ex, false);
            }
        }

        private void SelectTemplateItem(TemplateItem item)
        {
            CurrentTemplate = item;

            if (CurrentTemplate == null)
                ClearBindingControlContent();
            else
                LoadBindingControlContent();

            ResetDirtyStatus();
        }

        private void LoadTemplates()
        {
            try
            {
                XmlElement templats = QueryTemplate.GetAbstractList();

                ipTemplateList.Items.Clear();
                foreach (XmlElement each in templats.SelectNodes("ExamTemplate"))
                {
                    TemplateItem item;
                    string id = each.GetAttribute("ID");
                    string name = each.SelectSingleNode("TemplateName").InnerText;

                    item = new TemplateItem(id, name);
                    item.OptionGroup = "TemplateItems";
                    item.Click += new EventHandler(TemplateItem_Click);
                    item.DoubleClick += new EventHandler(TemplateItem_DoubleClick);

                    ipTemplateList.Items.Add(item);
                }

                ipTemplateList.RecalcLayout();
                SelectTemplateItem(null);

            }
            catch (Exception ex)
            {
                CurrentUser user = CurrentUser.Instance;
                BugReporter.ReportException(user.SystemName, user.SystemVersion, ex, false);
                MsgBox.Show(ex.Message);
            }
        }

        private void LoadExamList()
        {
            try
            {
                XmlElement exams = QueryCourse.GetExamList().GetContent().BaseElement;

                ExamNameColumn.Items.Clear();
                ExamNameColumn.Items.Add(new KeyValuePair<string, string>("-1", string.Empty));
                foreach (XmlElement each in exams.SelectNodes("Exam"))
                {
                    string id = each.GetAttribute("ID");
                    string name = each.SelectSingleNode("ExamName").InnerText;

                    KeyValuePair<string, string> exam = new KeyValuePair<string, string>(id, name);
                    ExamNameColumn.Items.Add(exam);
                }
                ExamNameColumn.DisplayMember = "Value";
                ExamNameColumn.ValueMember = "Key";

            }
            catch (Exception ex)
            {
                CurrentUser user = CurrentUser.Instance;
                BugReporter.ReportException(user.SystemName, user.SystemVersion, ex, false);
                MsgBox.Show(ex.Message);
            }
        }

        private void ClearBindingControlContent()
        {
            _grid_clearing = true;

            peTemplateName1.Text = string.Empty;
            dataview.Rows.Clear();
            dataview.Enabled = false;

            _grid_clearing = false;
        }

        private void LoadBindingControlContent()
        {
            try
            {
                peTemplateName1.Text = CurrentTemplate.TemplateName;
                dataview.Enabled = true;

                LoadExamList();

                XmlElement elmExams = QueryTemplate.GetIncludeExamList(CurrentTemplate.Identity);
                ExamItemCollection collExams = new ExamItemCollection();

                foreach (XmlElement each in elmExams.SelectNodes("IncludeExam"))
                {
                    ExamItem item = new ExamItem(each);
                    collExams.Add(item);
                }

                DisplayInGridView(collExams);
            }
            catch (Exception ex)
            {
                CurrentUser user = CurrentUser.Instance;
                BugReporter.ReportException(user.SystemName, user.SystemVersion, ex, false);
                MsgBox.Show(ex.Message);
            }
        }

        private void DisplayInGridView(ExamItemCollection exams)
        {
            _grid_clearing = true;
            dataview.Rows.Clear();
            _grid_clearing = false;

            foreach (ExamItem each in exams)
            {
                ExamRow row = new ExamRow(each);
                dataview.Rows.Add(row);
            }

            foreach (ExamRow row in dataview.Rows)
                row.ShowItemData();
        }

        private void dgvExamList_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            ExamRow row = e.Row as ExamRow;
            row.ShowItemData();

            RefreshDirtyStatus();
        }

        private void dgvExamList_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            ExamRow row = e.Row as ExamRow;

            if (row.UpdateAction != ExamItemUpdateAction.Add) //如果是新增的「試別」就要刪除就直接刪掉。
            {
                row.Delete();
                RefreshDirtyStatus();
                e.Cancel = true;
            }
        }

        private void dgvExamList_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            RefreshDirtyStatus();
        }

        private void dgvExamList_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (_grid_clearing) return;

            ExamRow row = GetRow(e.RowIndex);

            if (row.IsNewRow) return;

            row.ErrorText = string.Empty;

            row.ValidWeightValue();

            if (row.ExamNameCell.Value == null)
            {
                row.ErrorText = "必須指定「試別」。";
                return;
            }

            CheckExamNameDuplicate();
            CheckExamWeightFull();

            DateTime? dt;
            string StdTime = ExamItem.StdTimeFormat;

            string startTime = "" + row.StartTimeColumn.Value;
            row.StartTimeColumn.ErrorText = string.Empty;
            if (startTime != string.Empty)
            {
                dt = DateTimeHelper.ParseGregorian(startTime, PaddingMethod.First);
                if (dt.HasValue)
                    row.StartTimeColumn.Value = dt.Value.ToString(StdTime);
                else
                    row.StartTimeColumn.ErrorText = "日期格式錯誤，例：2007/09/25 08:00";
            }

            string endTime = "" + row.EndTimeColumn.Value;
            row.EndTimeColumn.ErrorText = string.Empty;
            if (endTime != string.Empty)
            {
                dt = DateTimeHelper.ParseGregorian(endTime, PaddingMethod.Last);
                if (dt.HasValue)
                    row.EndTimeColumn.Value = dt.Value.ToString(StdTime);
                else
                    row.EndTimeColumn.ErrorText = "日期格式錯誤，例：2007/10/01 16:30";
            }
        }

        private void dgvExamList_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (_grid_clearing) return;

            ExamRow row = GetRow(e.RowIndex);

            if (row.IsNewRow) return;

            row.SaveDataToItem();
            RefreshDirtyStatus();
        }

        private void RefreshDirtyStatus()
        {
            if (CurrentTemplate == null) return;

            peTemplateName1.Text = CurrentTemplate.TemplateName;

            foreach (ExamRow each in EachExamRow)
            {
                if (each.IsNewRow) continue;

                if (each.IsDirty)
                {
                    each.RefreshAction();
                    peTemplateName1.Text = CurrentTemplate.TemplateName + DirtyMarkString;
                    CurrentTemplate.IsDirty = true;

                    break;
                }
            }
        }

        private void ResetDirtyStatus()
        {
            if (CurrentTemplate == null) return;

            CurrentTemplate.IsDirty = false;

            peTemplateName1.Text = CurrentTemplate.TemplateName;
        }

        private ExamRow GetRow(int index)
        {
            return dataview.Rows[index] as ExamRow;
        }

        private DataGridViewCell GetCell(int rowIndex, int columnIndex)
        {
            return dataview.Rows[rowIndex].Cells[columnIndex];
        }

        private void CheckExamNameDuplicate()
        {
            List<object> dupcheck = new List<object>();

            foreach (ExamRow row in dataview.Rows)
            {
                if (row.UpdateAction == ExamItemUpdateAction.Delete) continue;
                if (row.IsNewRow) continue;

                if (dupcheck.Contains("" + row.ExamNameCell.Value))
                {
                    row.ExamNameCell.ErrorText = "在同一評分樣版中，試別不可重複。";
                }
                else
                {
                    dupcheck.Add("" + row.ExamNameCell.Value);
                    row.ExamNameCell.ErrorText = string.Empty;
                }
            }
        }

        private void CheckExamWeightFull()
        {

            float totalWeight = 0;
            DataGridViewCell lastCell = null;
            string errMsg = "比重總共必須等於 100。";

            foreach (ExamRow row in dataview.Rows)
            {
                if (row.UpdateAction == ExamItemUpdateAction.Delete) continue;
                if (row.IsNewRow) continue;

                if (row.IsWeightValueValid)
                    totalWeight += float.Parse("" + row.WeightCell.Value);

                lastCell = row.WeightCell;

                if (lastCell.ErrorText == errMsg)
                    lastCell.ErrorText = string.Empty;
            }

            if (lastCell == null) return;

            if (totalWeight != 100)
                lastCell.ErrorText = errMsg;
            else
            {
                if (lastCell.ErrorText == errMsg)
                    lastCell.ErrorText = string.Empty;
            }
        }

        private void peTemplateName1_DoubleClick(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                string rowstatus = string.Empty;
                foreach (ExamRow each in dataview.Rows)
                {
                    ExamItem item = each.ExamInformation;

                    rowstatus += each.IsNewRow + item.RefExamID + ":" + item.ExamName + ":" + each.UpdateAction.ToString() + "\n";
                }

                MsgBox.Show(rowstatus);
            }
        }

        private bool IsValidTimeFormat(TextBoxX ctl, out string output)
        {

            if (string.IsNullOrEmpty(ctl.Text))
            {
                output = string.Empty;
                return true;
            }

            DateTime result;
            if (DateTime.TryParse(ctl.Text, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out result))
            {
                errors.SetError(ctl, string.Empty);
                output = result.ToString(OutputFormat, DateTimeFormatInfo.InvariantInfo);
                return true;
            }
            else
            {
                errors.SetError(ctl, "日期格式不正確。");
                output = string.Empty;
                return false;
            }
        }

        private string FormatTime(string time)
        {
            if (string.IsNullOrEmpty(time)) return string.Empty;

            DateTime result;
            if (DateTime.TryParse(time, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out result))
                return result.ToString(OutputFormat, DateTimeFormatInfo.InvariantInfo);

            return string.Empty;
        }

        private DateTime? GetTimeObject(string time)
        {
            if (string.IsNullOrEmpty(time)) return null;

            DateTime result;
            if (DateTime.TryParse(time, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out result))
                return result;

            return null;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                TemplateNameEditor editor = new TemplateNameEditor(this);
                DialogResult dr = editor.ShowDialog();
                string newid = string.Empty;

                if (dr == DialogResult.OK)
                {
                    newid = EditTemplate.Insert(editor.TemplateName);
                    CourseEntity.Instance.InvokeForeignTableChanged();

                    TemplateItem item = new TemplateItem(newid, editor.TemplateName);
                    item.OptionGroup = "TemplateItems";
                    item.Click += new EventHandler(TemplateItem_Click);
                    item.DoubleClick += new EventHandler(TemplateItem_DoubleClick);

                    ipTemplateList.Items.Add(item);
                    ipTemplateList.RecalcLayout();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);

                CurrentUser user = CurrentUser.Instance;
                BugReporter.ReportException(user.SystemName, user.SystemVersion, ex, false);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ContainsError())
            {
                MsgBox.Show("資料有錯誤，請修改後再儲存。", Application.ProductName);
                return;
            }

            SaveCurrentTemplate();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentTemplate == null) return;

                string msg = "確定要刪除「" + CurrentTemplate.TemplateName + "」評分樣版？\n";
                msg += "刪除後，使用此評分樣版的「課程」將會自動變成未設定評分樣版狀態。";

                DialogResult dr = MsgBox.Show(msg, Application.ProductName, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    EditTemplate.Delete(CurrentTemplate.Identity);
                    CourseEntity.Instance.InvokeForeignTableChanged();

                    CurrentTemplate.Click -= new EventHandler(TemplateItem_Click);
                    CurrentTemplate.DoubleClick -= new EventHandler(TemplateItem_DoubleClick);

                    ipTemplateList.Items.Remove(CurrentTemplate);
                    ipTemplateList.RecalcLayout();

                    SelectTemplateItem(null);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);

                CurrentUser user = CurrentUser.Instance;
                BugReporter.ReportException(user.SystemName, user.SystemVersion, ex, false);
            }
        }

        public bool ContainsTemplateName(string name)
        {
            foreach (TemplateItem each in ipTemplateList.Items)
            {
                if (each.TemplateName == name)
                    return true;
            }

            return false;
        }

        private bool ContainsError()
        {
            foreach (ExamRow each in EachExamRow)
            {
                if (!string.IsNullOrEmpty(each.ErrorText))
                    return true;

                foreach (DataGridViewCell cell in each.Cells)
                {
                    if (!string.IsNullOrEmpty(cell.ErrorText))
                        return true;
                }
            }

            return false;
        }
    }
}