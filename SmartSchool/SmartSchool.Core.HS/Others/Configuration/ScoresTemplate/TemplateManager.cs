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
using System.Threading;

namespace SmartSchool.Others.Configuration.ScoresTemplate
{
    public partial class TemplateManager : BaseForm
    {
        private const string DirtyMarkString = "<b><font color=\"#ED1C24\">(已修改)</font></b>";
        private Template _current_template, _previous_template;
        private TemplateCollection _templates;
        private BackgroundWorker _workder;
        private XmlElement _raw_templates, _raw_exam_includes, _raw_exams;
        private bool _has_deleted;
        private EnhancedErrorProvider _errors;

        public TemplateManager()
        {
            InitializeComponent();
            HideNavigationBar();

            _errors = new EnhancedErrorProvider();
        }

        private void HideNavigationBar()
        {
            npLeft.NavigationBar.Visible = false;
        }

        private void DisableFunctions()
        {
            npLeft.Enabled = false;
        }

        private void TemplateManager_Load(object sender, EventArgs e)
        {
            LoadTemplates();
            SelectTemplate(null);//不選擇任何 Tempalte
        }

        /// <summary>
        /// 非同步處理，使用時要小心。
        /// </summary>
        private void LoadTemplates()
        {
            _workder = new BackgroundWorker();
            _workder.DoWork += new DoWorkEventHandler(Workder_DoWork);
            _workder.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Workder_RunWorkerCompleted);

            StartLoadTemplate();
            _workder.RunWorkerAsync();
        }

        private void Workder_DoWork(object sender, DoWorkEventArgs e)
        {
            _raw_templates = QueryTemplate.GetAbstractList();
            _raw_exam_includes = QueryTemplate.GetIncludeExamList();
            _raw_exams = QueryCourse.GetExamList().GetContent().BaseElement;
        }

        private void Workder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    CurrentUser.ReportError(e.Error);
                    DisableFunctions();
                    EndLoadTemplate();
                    MsgBox.Show("下載評量樣版資料錯誤。", Application.ProductName);
                    return;
                }

                FillExamRowSource();//將資料填入 Exam 欄位的 RowSource。

                CreateTemplateObjectModel(); //建立 Tempalte 資料物件。

                ShowTemplates(); //顯示 Tempalte 資料物件到畫面上。

                EndLoadTemplate();
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show(ex.Message);
            }
        }

        private void CreateTemplateObjectModel()
        {
            _templates = new TemplateCollection();
            foreach (XmlElement each in _raw_templates.SelectNodes("ExamTemplate"))
            {
                Template item = CreateTemplateObject(each);
                _templates.Add(item.Identity, item);
            }

            foreach (XmlElement each in _raw_exam_includes.SelectNodes("IncludeExam"))
            {
                Exam item = CreateExamObject(each);
                _templates[item.TemplateID].Exams.Add(item);
            }
        }

        private void FillExamRowSource()
        {
            XmlElement exams = _raw_exams;
            ExamID.Items.Clear();
            ExamID.Items.Add(new KeyValuePair<string, string>("-1", string.Empty));
            foreach (XmlElement each in exams.SelectNodes("Exam"))
            {
                string id = each.GetAttribute("ID");
                string name = each.SelectSingleNode("ExamName").InnerText;

                KeyValuePair<string, string> exam = new KeyValuePair<string, string>(id, name);
                ExamID.Items.Add(exam);
            }
            ExamID.DisplayMember = "Value";
            ExamID.ValueMember = "Key";
        }

        private void ShowTemplates()
        {
            foreach (Template each in _templates.Values)
                AddTemplateToList(each);
        }

        private void AddTemplateToList(Template template)
        {
            ipTemplateList.Items.Add(template);
            ipTemplateList.RecalcLayout();
        }

        private Template CurrentTemplate
        {
            get { return _current_template; }
            set { _current_template = value; }
        }

        private Template PreviousTemplate
        {
            get { return _previous_template; }
            set { _previous_template = value; }
        }

        private void SelectTemplate(Template item)
        {
            dataview.Rows.Clear();

            cboScoreSource.Enabled = (item != null);
            txtStartTime.Enabled = (item != null);
            txtEndTime.Enabled = (item != null);

            if (item == null)
            {
                peTemplateName1.Text = string.Empty;
                dataview.AllowUserToAddRows = false;
                panel1.Enabled = false;

                return;
            }
            else
            {
                peTemplateName1.Text = CurrentTemplate.TemplateName;
                dataview.AllowUserToAddRows = true;
                panel1.Enabled = true;

                if (item.AllowUpload == "是")
                {
                    cboScoreSource.SelectedIndex = 0;
                    txtStartTime.Enabled = true;
                    txtEndTime.Enabled = true;
                }
                else
                {
                    cboScoreSource.SelectedIndex = 1;
                    txtStartTime.Enabled = false;
                    txtEndTime.Enabled = false;
                }

                txtStartTime.Text = item.StartTime;
                txtStartTime.Tag = txtStartTime.Text;
                txtEndTime.Text = item.EndTime;
                txtEndTime.Tag = txtEndTime.Text;

                foreach (Exam each in item.Exams)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataview, each.ExamID, each.Weight, each.UseScore, each.UseText, each.OpenTeacherAccess, each.StartTime, each.EndTime, each.InputRequired);
                    dataview.Rows.Add(row);
                }
                item.RaiseClick();
                ResetDirty();
            }
        }

        private void Template_Click(object sender, EventArgs e)
        {
            if (CurrentTemplate == sender) return;

            if (!CanContinue()) return;

            PreviousTemplate = CurrentTemplate;
            CurrentTemplate = sender as Template;
            SelectTemplate(CurrentTemplate);
        }

        private bool CanContinue()
        {
            if (IsDirty())
            {
                DialogResult dr = MsgBox.Show("您未儲存目前資料，是否要儲存？", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (dr == DialogResult.Cancel)
                {
                    CurrentTemplate.RaiseClick();
                    return false;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!SaveTemplate())
                    {
                        CurrentTemplate.RaiseClick();
                        return false;
                    }
                    ReloadTempalte(CurrentTemplate);
                }
            }

            return true;
        }

        private void Template_DoubleClick(object sender, EventArgs e)
        {
            if (!CanContinue()) return;

            TemplateNameEditor editor = new TemplateNameEditor(this, CurrentTemplate.TemplateName);
            DialogResult dr = editor.ShowDialog();

            if (dr == DialogResult.OK)
            {
                try
                {
                    EditTemplate.Rename(CurrentTemplate.Identity, editor.TemplateName);

                    CurrentTemplate.TemplateName = editor.TemplateName;
                    CourseEntity.Instance.InvokeForeignTableChanged();
                }
                catch (Exception ex)
                {
                    CurrentUser.ReportError(ex);
                    MsgBox.Show(ex.Message);
                }
            }
        }

        private void StartLoadTemplate()
        {
            Loading = true;
            ipTemplateList.Items.Clear();
            panel1.Enabled = false;
            btnAddNew.Enabled = false;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void EndLoadTemplate()
        {
            ipTemplateList.RecalcLayout();
            panel1.Enabled = true;
            btnAddNew.Enabled = true;
            btnSave.Enabled = true;
            btnDelete.Enabled = true;
            Loading = false;
        }

        private bool Loading
        {
            get { return loading.Visible; }
            set { loading.Visible = value; }
        }

        private Template CreateTemplateObject(XmlElement templateData)
        {
            Template item;
            string id = templateData.GetAttribute("ID");
            string name = templateData.SelectSingleNode("TemplateName").InnerText;

            item = new Template(id, name);
            item.AllowUpload = templateData.SelectSingleNode("AllowUpload").InnerText;
            item.StartTime = DateToDisplayFormat(templateData.SelectSingleNode("StartTime").InnerText);
            item.EndTime = DateToDisplayFormat(templateData.SelectSingleNode("EndTime").InnerText);

            item.OptionGroup = "TemplateItems";
            item.Click += new EventHandler(Template_Click);
            item.DoubleClick += new EventHandler(Template_DoubleClick);
            return item;
        }

        private Exam CreateExamObject(XmlElement each)
        {
            Exam item = new Exam();
            item.Identity = each.SelectSingleNode("@ID").InnerText;
            item.TemplateID = each.SelectSingleNode("ExamTemplateID").InnerText;
            item.ExamID = each.SelectSingleNode("RefExamID").InnerText;
            item.OpenTeacherAccess = each.SelectSingleNode("OpenTeacherAccess").InnerText;
            item.StartTime = DateToDisplayFormat(each.SelectSingleNode("StartTime").InnerText);
            item.EndTime = DateToDisplayFormat(each.SelectSingleNode("EndTime").InnerText);
            item.UseScore = each.SelectSingleNode("UseScore").InnerText;
            item.UseText = each.SelectSingleNode("UseText").InnerText;
            item.Weight = each.SelectSingleNode("Weight").InnerText;
            item.InputRequired = each.SelectSingleNode("InputRequired").InnerText;

            return item;
        }

        private void ResetDirty()
        {
            foreach (DataGridViewRow row in dataview.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                    cell.Tag = cell.Value;
            }
            cboScoreSource.Tag = cboScoreSource.SelectedIndex;
            txtStartTime.Tag = txtStartTime.Text;
            txtEndTime.Tag = txtEndTime.Text;

            _has_deleted = false;
            _errors.Clear();

            if (CurrentTemplate == null)
                peTemplateName1.Text = string.Empty;
            else
                peTemplateName1.Text = CurrentTemplate.TemplateName;

        }

        private bool IsDirty()
        {
            bool dirty = false;
            foreach (DataGridViewRow row in dataview.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                    dirty = dirty || (cell.Tag + string.Empty != cell.Value + string.Empty);
            }

            dirty |= ((cboScoreSource.Tag == null ? "-1" : cboScoreSource.Tag) + string.Empty != cboScoreSource.SelectedIndex + string.Empty);
            dirty |= (txtStartTime.Tag + string.Empty != txtStartTime.Text + string.Empty);
            dirty |= (txtEndTime.Tag + string.Empty != txtEndTime.Text + string.Empty);

            return dirty || _has_deleted;
        }

        private void dataview_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            dataview.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "資料錯誤！";
            e.Cancel = true;
        }

        private static string DateToDisplayFormat(string source)
        {
            if (source == null || source == string.Empty) return string.Empty;

            DateTime? dt = DateTimeHelper.ParseGregorian(source, PaddingMethod.First);
            if (dt.HasValue)
                return dt.Value.ToString(Consts.TimeFormat);
            else
                return string.Empty;
        }

        private static string DateToSaveFormat(string source)
        {
            if (source == null || source == string.Empty) return string.Empty;

            DateTime? dt = DateTimeHelper.ParseGregorian(source, PaddingMethod.First);
            if (dt.HasValue)
                return dt.Value.ToString(DateTimeHelper.StdDateTimeFormat);
            else
                return string.Empty;
        }

        private void dataview_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataview.Rows[e.RowIndex].IsNewRow) return;

            DataGridViewCell cell = GetCell(e.RowIndex, e.ColumnIndex);

            string columnName = GetColumnName(cell);
            string validValue = e.FormattedValue + string.Empty;

            if (columnName == "Weight")
            {
                float weight;

                if (validValue == string.Empty)
                    cell.ErrorText = string.Empty;
                else
                {
                    if (!float.TryParse(validValue + string.Empty, out weight))
                        cell.ErrorText = "您必須輸入小於(等於) 100 的數字。";
                    else
                        cell.ErrorText = string.Empty;
                }
            }
            else if (columnName == "StartTime" || columnName == "EndTime")
            {
                if (validValue == string.Empty)
                    cell.ErrorText = string.Empty;
                else
                {
                    DateTime? dt = DateTimeHelper.ParseGregorian(validValue, PaddingMethod.First);
                    if (!dt.HasValue)
                        cell.ErrorText = "您必須要輸入合法的日期格式。";
                    else
                        cell.ErrorText = string.Empty;
                }
            }
        }

        private void dataview_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (dataview.Rows[e.RowIndex].IsNewRow) return;

            DataGridViewCell cell = GetCell(e.RowIndex, e.ColumnIndex);

            if (cell.ErrorText == string.Empty)//表示驗證沒有通過。
            {
                string columnName = GetColumnName(cell);

                if (columnName == "StartTime" || columnName == "EndTime")
                {
                    if ((cell.Value + string.Empty) != string.Empty)
                    {
                        PaddingMethod method = (columnName == "StartTime" ? PaddingMethod.First : PaddingMethod.Last);

                        DateTime? dt = DateTimeHelper.ParseGregorian(cell.Value + string.Empty, method);
                        cell.Value = dt.Value.ToString(Consts.TimeFormat);
                    }
                }
            }

            ShowDirtyStatus();
        }

        private void ShowDirtyStatus()
        {
            if (IsDirty())
                peTemplateName1.Text = CurrentTemplate.TemplateName + DirtyMarkString;
            else
                peTemplateName1.Text = CurrentTemplate.TemplateName;
        }

        private void dataview_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewRow row = dataview.Rows[e.RowIndex];

            if (row.IsNewRow) return;

            DataGridViewCell startTime = row.Cells["StartTime"];
            DataGridViewCell endTime = row.Cells["EndTime"];

            if (startTime.ErrorText != string.Empty || endTime.ErrorText != string.Empty)
                return;

            if (startTime.Value != null && endTime.Value != null)
            {
                DateTime? objStart = DateTimeHelper.ParseGregorian(startTime.Value + string.Empty, PaddingMethod.First);
                DateTime? objEnd = DateTimeHelper.ParseGregorian(endTime.Value + string.Empty, PaddingMethod.Last);
                if (objStart.Value > objEnd.Value)
                    row.ErrorText = "「開始時間」必須在「結束時間」之前。";
                else
                    row.ErrorText = string.Empty;
            }

            string exam = row.Cells["ExamID"].Value + string.Empty;
            if (exam == string.Empty)
                row.Cells["ExamID"].ErrorText = "必須選擇一個評量。";
            else
                row.Cells["ExamID"].ErrorText = string.Empty;

            ValidateGrid();
        }

        private void dataview_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            _has_deleted = true;
        }

        private DataGridViewCell GetCell(int row, int column)
        {
            return dataview.Rows[row].Cells[column];
        }

        private string GetColumnName(DataGridViewCell cell)
        {
            return dataview.Columns[cell.ColumnIndex].Name;
        }

        private void txtStartTime_Validating(object sender, CancelEventArgs e)
        {
            ValidTextTime(txtStartTime, PaddingMethod.First);
        }

        private void txtEndTime_Validating(object sender, CancelEventArgs e)
        {
            ValidTextTime(txtEndTime, PaddingMethod.Last);
        }

        private void ValidTextTime(TextBoxX textbox, PaddingMethod method)
        {
            if (textbox.Text == string.Empty)
                _errors.SetError(textbox, "");
            else
            {
                DateTime? objStart = DateTimeHelper.ParseGregorian(textbox.Text, method);
                if (!objStart.HasValue)
                    _errors.SetError(textbox, "您必須輸入合法的日期格式。");
                else
                    _errors.SetError(textbox, "");
            }
        }

        private void txtStartTime_Validated(object sender, EventArgs e)
        {
            if (txtStartTime.Text != string.Empty)
                FormatDateTime(txtStartTime, PaddingMethod.First);

            CurrentTemplate.StartTime = txtStartTime.Text;

            ShowDirtyStatus();
        }

        private void txtEndTime_Validated(object sender, EventArgs e)
        {
            if (txtEndTime.Text != string.Empty)
                FormatDateTime(txtEndTime, PaddingMethod.Last);

            CurrentTemplate.EndTime = txtEndTime.Text;

            ShowDirtyStatus();
        }

        private void cboScoreSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentTemplate == null) return;

            if (cboScoreSource.SelectedIndex == 0)
            {
                CurrentTemplate.AllowUpload = "是";
                txtStartTime.Enabled = true;
                txtEndTime.Enabled = true;
            }
            else
            {
                CurrentTemplate.AllowUpload = "否";
                txtStartTime.Enabled = false;
                txtEndTime.Enabled = false;
            }

            ShowDirtyStatus();
        }

        private void FormatDateTime(TextBoxX textbox, PaddingMethod method)
        {
            if (_errors.GetError(textbox) == string.Empty)
            {
                DateTime? dt = DateTimeHelper.ParseGregorian(textbox.Text, method);
                textbox.Text = dt.Value.ToString(Consts.TimeFormat);
            }
        }

        private void ValidateGrid()
        {
            List<string> examDuplicate = new List<string>();
            float weight = 0;
            foreach (DataGridViewRow each in dataview.Rows)
            {
                if (each.IsNewRow) continue;

                string examValue = each.Cells["ExamID"].Value + string.Empty;
                if (examValue != string.Empty)
                {
                    if (examDuplicate.Contains(examValue))
                        each.ErrorText = "評量名稱重複。";
                    else
                        examDuplicate.Add(examValue);
                }

                string weightValue = each.Cells["Weight"].Value + string.Empty;
                bool weightNoError = each.Cells["Weight"].ErrorText == string.Empty;
                if (weightValue != string.Empty && weightNoError)
                    weight += float.Parse(weightValue);
            }

            string errorMsg = "評量比重加總必須等於 100。";
            foreach (DataGridViewRow each in dataview.Rows)
            {
                if (each.ErrorText == errorMsg)
                    each.ErrorText = string.Empty;
            }

            if (weight != 100)
                dataview.Rows[dataview.Rows.Count - 2].ErrorText = errorMsg;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CurrentTemplate == null) return;

            if (SaveTemplate())
            {
                ReloadTempalte(CurrentTemplate);
                SelectTemplate(CurrentTemplate);
            }
        }

        private void CopyTemplate(Template source, Template target)
        {
            try
            {
                string startTime = DateToSaveFormat(source.StartTime);
                string endTime = DateToSaveFormat(source.EndTime);

                EditTemplate.UpdateTemplate(target.Identity, target.TemplateName, source.AllowUpload, startTime, endTime);

                bool executeRequired = false;
                DSXmlHelper reqInclude = new DSXmlHelper("Request");
                foreach (Exam exam in source.Exams)
                {
                    reqInclude.AddElement("IncludeExam");
                    reqInclude.AddElement("IncludeExam", "RefExamTemplateID", target.Identity);
                    reqInclude.AddElement("IncludeExam", "RefExamID", exam.ExamID);
                    reqInclude.AddElement("IncludeExam", "UseScore", exam.UseScore);
                    reqInclude.AddElement("IncludeExam", "UseText", exam.UseText);
                    reqInclude.AddElement("IncludeExam", "Weight", exam.Weight);
                    reqInclude.AddElement("IncludeExam", "OpenTeacherAccess", exam.OpenTeacherAccess);
                    reqInclude.AddElement("IncludeExam", "StartTime", DateToSaveFormat(exam.StartTime));
                    reqInclude.AddElement("IncludeExam", "EndTime", DateToSaveFormat(exam.EndTime));
                    reqInclude.AddElement("IncludeExam", "InputRequired", exam.InputRequired);
                    executeRequired = true;
                }
                if (executeRequired)
                    EditTemplate.InsertIncludeExam(reqInclude.BaseElement);
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show(ex.Message);
            }
        }

        private bool SaveTemplate()
        {
            if (HasErrors())
            {
                MsgBox.Show("請修正資料後再儲存。", Application.ProductName);
                return false;
            }

            try
            {
                string startTime = DateToSaveFormat(CurrentTemplate.StartTime);
                string endTime = DateToSaveFormat(CurrentTemplate.EndTime);

                if (cboScoreSource.SelectedIndex == 0)
                {
                    //if (string.IsNullOrEmpty(startTime) || string.IsNullOrEmpty(endTime))
                    //{
                    //    MsgBox.Show("由教師提供課程成績必須指定上傳時間。", Application.ProductName);
                    //    return false;
                    //}
                    //由教師提供
                    EditTemplate.UpdateTemplate(CurrentTemplate.Identity, CurrentTemplate.TemplateName, CurrentTemplate.AllowUpload, startTime, endTime);
                }
                else //由學校計算
                {
                    foreach (DataGridViewRow each in dataview.Rows)
                    {
                        if (each.IsNewRow) continue;

                        if ((bool)each.Cells["InputRequired"].FormattedValue)
                        {
                            MsgBox.Show("成績由「學校計算」時，所有「評量」必需設定成「強制繳交成績」。");
                            return false;
                        }
                    }

                    EditTemplate.UpdateTemplate(CurrentTemplate.Identity, CurrentTemplate.TemplateName, CurrentTemplate.AllowUpload, string.Empty, string.Empty);
                }

                DSXmlHelper request = new DSXmlHelper("Request");
                request.AddElement("IncludeExam");
                request.AddElement("IncludeExam", "RefExamTemplateID", CurrentTemplate.Identity);
                EditTemplate.DeleteIncludeExam(request.BaseElement);

                bool executeRequired = false;
                DSXmlHelper reqInclude = new DSXmlHelper("Request");
                foreach (DataGridViewRow each in dataview.Rows)
                {
                    if (each.IsNewRow) continue;

                    reqInclude.AddElement("IncludeExam");
                    reqInclude.AddElement("IncludeExam", "RefExamTemplateID", CurrentTemplate.Identity);
                    reqInclude.AddElement("IncludeExam", "RefExamID", each.Cells["ExamID"].Value + string.Empty);
                    reqInclude.AddElement("IncludeExam", "UseScore", GetYesNoString(each.Cells["UseScore"].FormattedValue, "否"));
                    reqInclude.AddElement("IncludeExam", "UseText", GetYesNoString(each.Cells["UseText"].FormattedValue, "否"));
                    reqInclude.AddElement("IncludeExam", "Weight", each.Cells["Weight"].Value + string.Empty);
                    reqInclude.AddElement("IncludeExam", "OpenTeacherAccess", GetYesNoString(each.Cells["OpenTeacherAccess"].FormattedValue, "否"));
                    reqInclude.AddElement("IncludeExam", "StartTime", DateToSaveFormat(each.Cells["StartTime"].Value + string.Empty));
                    reqInclude.AddElement("IncludeExam", "EndTime", DateToSaveFormat(each.Cells["EndTime"].Value + string.Empty));

                    if (each.Cells["InputRequired"].FormattedValue == null)
                        reqInclude.AddElement("IncludeExam", "InputRequired", "是");
                    else if ((bool)each.Cells["InputRequired"].FormattedValue)
                        reqInclude.AddElement("IncludeExam", "InputRequired", "否");
                    else
                        reqInclude.AddElement("IncludeExam", "InputRequired", "是");

                    executeRequired = true;
                }
                if (executeRequired)
                    EditTemplate.InsertIncludeExam(reqInclude.BaseElement);

                ResetDirty();
                return true;
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show(ex.Message);
                return false;
            }
        }

        private string GetYesNoString(object input, string defaultValue)
        {
            if (input == null)
                return defaultValue;

            if ((bool)input)
                return "是";
            else
                return "否";
        }

        private bool HasErrors()
        {
            bool hasError = false;

            foreach (DataGridViewRow each in dataview.Rows)
            {
                hasError |= (each.ErrorText != string.Empty);
                foreach (DataGridViewCell cell in each.Cells)
                    hasError |= (cell.ErrorText != string.Empty);
            }

            return hasError || _errors.HasError;
        }

        private void ReloadTempalte(Template item)
        {
            try
            {
                XmlElement rawTemplate = QueryTemplate.GetTempalteInfo(item.Identity);
                XmlElement rawIncludeExams = QueryTemplate.GetIncludeExamList(item.Identity);

                item.TemplateName = rawTemplate.SelectSingleNode("ExamTemplate/TemplateName").InnerText;
                item.AllowUpload = rawTemplate.SelectSingleNode("ExamTemplate/AllowUpload").InnerText;
                item.StartTime = DateToDisplayFormat(rawTemplate.SelectSingleNode("ExamTemplate/StartTime").InnerText);
                item.EndTime = DateToDisplayFormat(rawTemplate.SelectSingleNode("ExamTemplate/EndTime").InnerText);

                item.Exams.Clear();
                foreach (XmlElement each in rawIncludeExams.SelectNodes("IncludeExam"))
                {
                    Exam e = CreateExamObject(each);
                    item.Exams.Add(e);
                }
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show(ex.Message);
                DisableFunctions();
            }
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

                    CurrentTemplate.Click -= new EventHandler(Template_Click);
                    CurrentTemplate.DoubleClick -= new EventHandler(Template_DoubleClick);

                    _templates.Remove(CurrentTemplate.Identity);
                    ipTemplateList.Items.Remove(CurrentTemplate);
                    ipTemplateList.RecalcLayout();

                    SelectTemplate(null);
                    CurrentTemplate = null;

                    ResetDirty();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
                CurrentUser.ReportError(ex);
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CanContinue()) return;

                TemplateNameEditor editor = new TemplateNameEditor(this, new List<Template>(_templates.Values));
                DialogResult dr = editor.ShowDialog();
                string newid = string.Empty;

                if (dr == DialogResult.OK)
                {
                    newid = EditTemplate.Insert(editor.TemplateName);
                    CourseEntity.Instance.InvokeForeignTableChanged();

                   
                    Template item = new Template(newid, editor.TemplateName);
                    item.OptionGroup = "TemplateItems";
                    item.Click += new EventHandler(Template_Click);
                    item.DoubleClick += new EventHandler(Template_DoubleClick);

                    if (editor.ExistTemplate != null)
                        CopyTemplate(editor.ExistTemplate, item);

                    AddTemplateToList(item);
                    _templates.Add(item.Identity, item);
                    CurrentTemplate = item;
                    ReloadTempalte(item);
                    SelectTemplate(item);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
                CurrentUser.ReportError(ex);
            }
        }

        internal bool ContainsTemplateName(string p)
        {
            foreach (Template each in _templates.Values)
            {
                if (each.TemplateName == p)
                    return true;
            }

            return false;
        }

        private void dataview_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //新增ROW時，開放繳交預設為 "是"
            if (string.IsNullOrEmpty(dataview.Rows[e.RowIndex].Cells[ExamID.Name].FormattedValue.ToString()))
            {
                dataview.Rows[e.RowIndex].Cells[OpenTeacherAccess.Name].Value = "是";
                dataview.Rows[e.RowIndex].Cells[OpenTeacherAccess.Name].Tag = "是";
            }
        }
    }
}