using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.Customization.PlugIn.ImportExport;
using Aspose.Cells;
using System.Diagnostics;
using System.IO;
using SmartSchool.StudentRelated;
using System.Threading;
using DevComponents.DotNetBar.Rendering;
using SmartSchool.Common;

namespace SmartSchool.StudentRelated.RibbonBars.Import
{
    public partial class ImportStudent : SmartSchool.Common.BaseForm
    {
        private ImportProcess _Process;

        private IntelliSchool.DSA.ClientFramework.ControlCommunication.ListViewCheckAllManager _CheckAllManager = new IntelliSchool.DSA.ClientFramework.ControlCommunication.ListViewCheckAllManager();

        private List<string> _StudentFields = new List<string>(new string[] { "學生系統編號", "學號", "班級", "座號", "科別", "姓名" });

        private Dictionary<string, int> _ImportFields = new Dictionary<string, int>();

        private Workbook _WorkBook;

        private Workbook _ErrorWB = null;

        public ImportStudent(ImportProcess process)
        {
            InitializeComponent();
            #region 設定Wizard會跟著Style跑
            //this.wizard1.FooterStyle.ApplyStyle(( GlobalManager.Renderer as Office2007Renderer ).ColorTable.GetClass(ElementStyleClassKeys.RibbonFileMenuBottomContainerKey));
            this.wizard1.HeaderStyle.ApplyStyle(( GlobalManager.Renderer as Office2007Renderer ).ColorTable.GetClass(ElementStyleClassKeys.RibbonFileMenuBottomContainerKey));
            this.wizard1.FooterStyle.BackColorGradientAngle = -90;
            this.wizard1.FooterStyle.BackColorGradientType = eGradientType.Linear;
            this.wizard1.FooterStyle.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.Default.TopBackground.Start;
            this.wizard1.FooterStyle.BackColor2 = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.Default.TopBackground.End;
            this.wizard1.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.Default.TopBackground.Start;
            this.wizard1.BackgroundImage = null;
            for ( int i = 0 ; i < 5 ; i++ )
            {
                ( this.wizard1.Controls[1].Controls[i] as ButtonX ).ColorTable = eButtonColor.OrangeWithBackground;
            }
            ( this.wizard1.Controls[0].Controls[1] as System.Windows.Forms.Label ).ForeColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.MouseOver.TitleText;
            ( this.wizard1.Controls[0].Controls[2] as System.Windows.Forms.Label ).ForeColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.Default.TitleText;
            _CheckAllManager.TargetComboBox = this.checkBox1;
            _CheckAllManager.TargetListView = this.listView1;
            #endregion

            _Process = process;
            this.Text = process.Title;

            foreach ( WizardPage page in wizard1.WizardPages )
            {
                page.PageTitle = _Process.Title;
                if ( _Process.Image != null )
                {
                    Bitmap b = new Bitmap(48, 48);
                    using ( Graphics g = Graphics.FromImage(b) )
                        g.DrawImage(_Process.Image, 0, 0, 48, 48);
                    page.PageHeaderImage = b;
                }
            }
            lblReqFields.Text = "<font color=\"#7A4E2B\"><b>學生系統編號 或 學號</b></font>";
            foreach ( string req in _Process.RequiredFields )
            {
                lblReqFields.Text += " 、 " + "<font color=\"#7A4E2B\"><b>" + req + "</b></font>";
            }
            if ( _Process.ImportableFields.Count == _Process.RequiredFields.Count )
                wizard1.WizardPages.RemoveAt(1);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if ( SelectSourceFileDialog.ShowDialog() == DialogResult.OK )
            {
                if ( txtFile.Text == SelectSourceFileDialog.FileName )
                    txtFile_TextChanged(null, null);
                else
                    txtFile.Text = SelectSourceFileDialog.FileName;
            }
        }

        private void txtFile_TextChanged(object sender, EventArgs e)
        {
            this.wizardPage1.NextButtonEnabled = eWizardButtonState.False;
            this.UseWaitCursor = true;
            linkLabel2.Visible = false;
            errorFile.Clear();
            Application.DoEvents();
            _ImportFields.Clear();
            _WorkBook = new Workbook();
            try
            {
                _WorkBook.Open(txtFile.Text);
            }
            catch
            {
                errorFile.SetIconAlignment(txtFile, ErrorIconAlignment.MiddleLeft);
                errorFile.SetError(txtFile, "檔案不存在、格式錯誤或檔案開啟中無法讀取。");
                this.UseWaitCursor = false;
                return;
            }
            linkLabel2.Visible = true;
            this.UseWaitCursor = false;
            //讀取檔案內所有的欄位
            for ( int i = 0 ; ( "" + _WorkBook.Worksheets[0].Cells[0, i].StringValue ).Trim() != "" ; i++ )
            {
                _ImportFields.Add(( "" + _WorkBook.Worksheets[0].Cells[0, i].StringValue ).Trim(), i);
            }
            string messingFields = "";
            foreach ( string key in _Process.RequiredFields )
            {
                if ( !_ImportFields.ContainsKey(key) )
                {
                    messingFields += ( messingFields == "" ? "" : "," ) + key;
                }
            }
            if ( !_ImportFields.ContainsKey("學生系統編號") && !_ImportFields.ContainsKey("學號") )
            {
                messingFields += ( messingFields == "" ? "" : "," ) + "學生系統編號或學號";
            }
            if ( messingFields != "" )
            {
                errorFile.SetIconAlignment(txtFile, ErrorIconAlignment.MiddleLeft);
                errorFile.SetError(txtFile, "缺少必要欄位:\n" + messingFields);
                return;
            }
            listView1.SuspendLayout();
            listView1.Items.Clear();
            List<ListViewItem> items = new List<ListViewItem>();
            foreach ( string field in _Process.ImportableFields )
            {
                if ( !_Process.RequiredFields.Contains(field) && _ImportFields.ContainsKey(field) )
                {
                    ListViewItem item = new ListViewItem(field);
                    item.Checked = true;
                    items.Add(item);
                }
            }
            listView1.Items.AddRange(items.ToArray());
            listView1.ResumeLayout();
            this.wizardPage1.NextButtonEnabled = eWizardButtonState.True;
        }

        private BackgroundWorker _BKWValidate;

        private void wizardPage3_AfterPageDisplayed(object sender, WizardPageChangeEventArgs e)
        {
            this.progressBarX1.Value = 0;
            lblWarningCount.Text = lblErrCount.Text = "0";
            this.wizardPage3.FinishButtonEnabled = eWizardButtonState.False;
            linkLabel1.Visible = false;
            labelX2.Text = "資料驗證中";
            linkLabel3.Tag = null;
            linkLabel3.Visible = false;
            Application.DoEvents();

            _BKWValidate = new BackgroundWorker();
            _BKWValidate.WorkerReportsProgress = true;
            _BKWValidate.WorkerSupportsCancellation = true;
            _BKWValidate.DoWork += new DoWorkEventHandler(_BKWValidate_DoWork);
            _BKWValidate.ProgressChanged += new ProgressChangedEventHandler(_BKWValidate_ProgressChanged);
            _BKWValidate.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BKWValidate_RunWorkerCompleted);

            List<string> fields = new List<string>();
            string fileName = txtFile.Text;
            fields.AddRange(_Process.RequiredFields);
            foreach ( ListViewItem item in listView1.Items )
            {
                if ( item.Checked )
                    fields.Add(item.Text);
            }
            Workbook wb = new Workbook();
            wb.Copy(_WorkBook);
            _BKWValidate.RunWorkerAsync(new object[] { fields, _ImportFields, wb });
        }

        private void wizardPage3_BackButtonClick(object sender, CancelEventArgs e)
        {
            if ( _BKWValidate != null && _BKWValidate.IsBusy )
                _BKWValidate.CancelAsync();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if ( _BKWValidate != null && _BKWValidate.IsBusy )
                _BKWValidate.CancelAsync();
            linkLabel3.Tag = "!";
        }

        private void ImportStudent_FormClosed(object sender, FormClosedEventArgs e)
        {
            if ( _BKWValidate != null && _BKWValidate.IsBusy )
                _BKWValidate.CancelAsync();
        }

        private void _BKWValidate_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bkw = (BackgroundWorker)sender;

            List<string> selectedFields = (List<string>)( (object[])e.Argument )[0];
            Dictionary<string, int> importFields = (Dictionary<string, int>)( (object[])e.Argument )[1];
            Workbook wb = (Workbook)( (object[])e.Argument )[2];
            wb.Worksheets[0].Comments.Clear();
            wb.Worksheets[0].Hyperlinks.Clear();
            int errorSheetIndex = wb.Worksheets.Add();
            {
                int errc = 0;
                #region 命名
                for ( ; ; errc++ )
                {
                    bool pass = true;
                    string n = "錯誤&警告說明" + ( errc == 0 ? "" : "(" + errc + ")" );
                    foreach ( Aspose.Cells.Worksheet var in wb.Worksheets )
                    {
                        if ( n == var.Name )
                        {
                            pass = false;
                            break;
                        }
                    }
                    if ( pass ) break;
                }
                #endregion
                wb.Worksheets[errorSheetIndex].Name = "錯誤&警告說明" + ( errc == 0 ? "" : "(" + errc + ")" );
            }
            string errorSheetName = wb.Worksheets[errorSheetIndex].Name;
            Worksheet errorSheet = wb.Worksheets[errorSheetIndex];
            errorSheet.Cells[0, 0].PutValue("行號");
            errorSheet.Cells[0, 1].PutValue("種類");
            errorSheet.Cells[0, 2].PutValue("說明");
            int errorSheetRowIndex = 1;

            Style errorStyle = wb.Styles[wb.Styles.Add()];
            Style warningStyle = wb.Styles[wb.Styles.Add()];
            Style passStyle = wb.Styles[wb.Styles.Add()];

            Style errorStyle2 = wb.Styles[wb.Styles.Add()];
            Style warningStyle2 = wb.Styles[wb.Styles.Add()];

            //errorStyle.Pattern = BackgroundType.Solid;
            //errorStyle.ForegroundColor = Color.Red;
            errorStyle.Font.Color = Color.Red;
            errorStyle.Font.Underline = FontUnderlineType.Single;

            //warningStyle.Pattern = BackgroundType.Solid;
            //warningStyle.ForegroundColor = Color.Yellow;
            warningStyle.Font.Color = wb.GetMatchingColor(Color.Goldenrod);
            warningStyle.Font.Underline = FontUnderlineType.Single;

            //passStyle.Pattern = BackgroundType.Solid;
            //passStyle.ForegroundColor = Color.Green;
            passStyle.Font.Color = Color.Green;

            warningStyle2.Font.Color = wb.GetMatchingColor(Color.Goldenrod);
            errorStyle2.Font.Color = Color.Red;


            int errorCount = 0;
            int warningCount = 0;
            Dictionary<RowData, int> rowDataIndex = new Dictionary<RowData, int>();

            Dictionary<int, string> fieldIndex = new Dictionary<int, string>();
            foreach ( string field in selectedFields )
            {
                fieldIndex.Add(importFields[field], field);
            }

            double progress = 0.0;

            #region 產生RowData資料
            if ( importFields.ContainsKey("學生系統編號") )
            {
                #region 用編號驗證資料
                for ( int i = 1 ; i <= wb.Worksheets[0].Cells.MaxDataRow ; i++ )
                {
                    // , "學號", "班級", "座號", "科別", "姓名"
                    string id = ( "" + wb.Worksheets[0].Cells[i, importFields["學生系統編號"]].StringValue ).Trim();
                    if ( id != "" )
                    {
                        string rowError = "";
                        #region 驗明正身
                        BriefStudentData stu = SmartSchool.StudentRelated.Student.Instance.Items[id];
                        if ( stu != null )
                        {
                            if ( importFields.ContainsKey("學號") && (""+wb.Worksheets[0].Cells[i, importFields["學號"]].StringValue).Trim() != stu.StudentNumber.Trim() )
                            {
                                //rowError = "學號與系統內學生資料不同!!";
                                rowError += ( rowError == "" ? "" : "、\n" ) + "系統內學生學號為\"" + stu.StudentNumber.Trim() + "\"";
                            }
                            if ( importFields.ContainsKey("班級") &&  (""+wb.Worksheets[0].Cells[i, importFields["班級"]].StringValue).Trim() != stu.ClassName.Trim() )
                            {
                                //rowError = "班級與系統內學生資料不同!!";
                                rowError += ( rowError == "" ? "" : "、\n" ) + "系統內學生班級為\"" + stu.ClassName.Trim() + "\"";
                            }
                            if ( importFields.ContainsKey("科別") &&  (""+wb.Worksheets[0].Cells[i, importFields["科別"]].StringValue).Trim() != stu.Department.Trim() )
                            {
                                //rowError = "科別與系統內學生資料不同!!";
                                rowError += ( rowError == "" ? "" : "、\n" ) + "系統內學生科別為\"" + stu.Department.Trim() + "\"";
                            }
                            if ( importFields.ContainsKey("座號") &&  (""+wb.Worksheets[0].Cells[i, importFields["座號"]].StringValue).Trim() != stu.SeatNo.Trim() )
                            {
                                //rowError = "座號與系統內學生資料不同!!";
                                rowError += ( rowError == "" ? "" : "、\n" ) + "系統內學生座號為\"" + stu.SeatNo.Trim() + "\"";
                            }
                            if ( importFields.ContainsKey("姓名") && ( "" + wb.Worksheets[0].Cells[i, importFields["姓名"]].StringValue ).Trim() != stu.Name.Trim() )
                            {
                                //rowError = "姓名與系統內學生資料不同!!";
                                rowError += ( rowError == "" ? "" : "、\n" ) + "系統內學生姓名為\"" + stu.Name.Trim() + "\"";
                            }
                        }
                        else
                        {
                            rowError = "學生不存在!!";
                        }
                        #endregion
                        if ( rowError == "" )
                        {
                            if ( !stu.IsNormal )
                            {
                                #region 警告非在校生
                                errorSheet.Cells[errorSheetRowIndex, 0].PutValue(i + 1);
                                errorSheet.Cells[errorSheetRowIndex, 1].PutValue("警告");
                                errorSheet.Cells[errorSheetRowIndex, 2].PutValue("學生不是在校生。");
                                errorSheet.Cells[errorSheetRowIndex, 0].Style = warningStyle;
                                errorSheet.Cells[errorSheetRowIndex, 1].Style = warningStyle2;
                                errorSheet.Cells[errorSheetRowIndex, 2].Style = warningStyle2;
                                errorSheet.Hyperlinks.Add(errorSheetRowIndex, 0, 1, 1, "'" + wb.Worksheets[0].Name + "'!" + wb.Worksheets[0].Cells[i, 0].Name);
                                errorSheet.AutoFitRow(errorSheetRowIndex);
                                errorSheetRowIndex++;
                                warningCount++;
                                #endregion
                            }
                            RowData rowdata = new RowData();
                            rowdata.ID = id;
                            foreach ( int index in fieldIndex.Keys )
                            {
                                if ( wb.Worksheets[0].Cells[i, index].Type == CellValueType.IsDateTime )
                                {
                                    rowdata.Add(fieldIndex[index], wb.Worksheets[0].Cells[i, index].DateTimeValue.ToString());
                                }
                                else
                                    rowdata.Add(fieldIndex[index], ( "" + wb.Worksheets[0].Cells[i, index].StringValue ).Trim());
                            }
                            //foreach ( string field in selectedFields )
                            //{
                            //    rowdata.Add(field, wb.Worksheets[0].Cells[i, importFields[field]].StringValue.Trim());
                            //}
                            rowDataIndex.Add(rowdata, i);
                        }
                        else
                        {
                            errorCount++;
                            errorSheet.Cells[errorSheetRowIndex, 0].PutValue(i + 1);
                            errorSheet.Cells[errorSheetRowIndex, 1].PutValue("錯誤");
                            errorSheet.Cells[errorSheetRowIndex, 2].PutValue(rowError);
                            errorSheet.Cells[errorSheetRowIndex, 0].Style = errorStyle;
                            errorSheet.Cells[errorSheetRowIndex, 1].Style = errorStyle2;
                            errorSheet.Cells[errorSheetRowIndex, 2].Style = errorStyle2;
                            errorSheet.Hyperlinks.Add(errorSheetRowIndex, 0, 1, 1, "'" + wb.Worksheets[0].Name + "'!" + wb.Worksheets[0].Cells[i, 0].Name);
                            wb.Worksheets[0].Hyperlinks.Add(i, 0, 1, 1, "'" + errorSheetName + "'!" + errorSheet.Cells[errorSheetRowIndex, 0].Name);
                            errorSheet.AutoFitRow(errorSheetRowIndex);
                            errorSheetRowIndex++;
                            wb.Worksheets[0].Cells[i, 0].Style = errorStyle;
                        }
                    }
                    else
                    {
                        bool isNullRow = true;
                        for ( byte c = 0 ; c < wb.Worksheets[0].Cells.MaxDataColumn ; c++ )
                        {
                            if ((""+ wb.Worksheets[0].Cells[i, c].StringValue).Trim() != "" )
                                isNullRow = false;
                        }
                        if ( !isNullRow )
                        {
                            errorCount++;
                            errorSheet.Cells[errorSheetRowIndex, 0].PutValue(i + 1);
                            errorSheet.Cells[errorSheetRowIndex, 1].PutValue("錯誤");
                            errorSheet.Cells[errorSheetRowIndex, 2].PutValue("驗證欄位(學生系統編號)不得空白");
                            errorSheet.Cells[errorSheetRowIndex, 0].Style = errorStyle;
                            errorSheet.Cells[errorSheetRowIndex, 1].Style = errorStyle2;
                            errorSheet.Cells[errorSheetRowIndex, 2].Style = errorStyle2;
                            errorSheet.Hyperlinks.Add(errorSheetRowIndex, 0, 1, 1, "'" + wb.Worksheets[0].Name + "'!" + wb.Worksheets[0].Cells[i, 0].Name);
                            wb.Worksheets[0].Hyperlinks.Add(i, 0, 1, 1, "'" + errorSheetName + "'!" + errorSheet.Cells[errorSheetRowIndex, 0].Name);
                            errorSheet.AutoFitRow(errorSheetRowIndex);
                            errorSheetRowIndex++;
                            wb.Worksheets[0].Cells[i, 0].Style = errorStyle;
                        }
                    }
                    if ( bkw.CancellationPending )
                    {
                        e.Cancel = true;
                        _ErrorWB = wb;
                        return;
                    }
                    progress = ( (double)i ) * 100.0 / 100000;
                    bkw.ReportProgress((int)progress, new int[] { errorCount, warningCount });
                }
                #endregion
            }
            else if ( importFields.ContainsKey("學號") )
            {
                #region 用學號驗證資料
                Dictionary<string, List<BriefStudentData>> studentNumberStudents = new Dictionary<string, List<BriefStudentData>>();
                #region 整理學號對應學生清單(如索引欄不試系統編號時用)
                foreach ( SmartSchool.StudentRelated.BriefStudentData stu in SmartSchool.StudentRelated.Student.Instance.Items )
                {
                    if ( stu.IsNormal )
                    {
                        if ( !studentNumberStudents.ContainsKey(stu.StudentNumber) )
                            studentNumberStudents.Add(stu.StudentNumber, new List<BriefStudentData>(new BriefStudentData[] { stu }));
                        else
                            studentNumberStudents[stu.StudentNumber].Add(stu);
                    }
                }
                #endregion
                for ( int i = 1 ; i <= wb.Worksheets[0].Cells.MaxDataRow ; i++ )
                {
                    string num = (""+wb.Worksheets[0].Cells[i, importFields["學號"]].StringValue).Trim();
                    //wb.Worksheets[0].Cells.
                    if ( num != "" )
                    {
                        string rowError = "";
                        #region 驗明正身
                        BriefStudentData stu = null;
                        if ( studentNumberStudents.ContainsKey(num) )
                        {
                            if ( studentNumberStudents[num].Count > 1 )
                            {
                                #region 必需要其他欄位做索引
                                bool err = true;
                                foreach ( string validateKey in new string[] { "班級", "科別", "座號", "姓名" } )
                                {
                                    if ( importFields.ContainsKey(validateKey) )
                                    {
                                        err = false;
                                        foreach ( BriefStudentData var in studentNumberStudents[num] )
                                        {
                                            bool pass = true;
                                            if ( importFields.ContainsKey("班級") &&(""+ wb.Worksheets[0].Cells[i, importFields["班級"]].StringValue).Trim() != var.ClassName.Trim() )
                                            {
                                                if ( studentNumberStudents[num].Count == 1 )
                                                    rowError += ( rowError == "" ? "" : "、\n" ) + "系統內學生班級為\"" + var.ClassName.Trim() + "\"";
                                                pass &= false;
                                            }
                                            if ( importFields.ContainsKey("科別") && ( "" + wb.Worksheets[0].Cells[i, importFields["科別"]].StringValue ).Trim() != var.Department.Trim() )
                                            {
                                                if ( studentNumberStudents[num].Count == 1 )
                                                    rowError += ( rowError == "" ? "" : "、\n" ) + "系統內學生科別為\"" + var.Department.Trim() + "\"";
                                                pass &= false;
                                            }
                                            if ( importFields.ContainsKey("座號") && (""+wb.Worksheets[0].Cells[i, importFields["座號"]].StringValue).Trim() != var.SeatNo.Trim() )
                                            {
                                                if ( studentNumberStudents[num].Count == 1 )
                                                    rowError += ( rowError == "" ? "" : "、\n" ) + "系統內學生座號為\"" + var.SeatNo.Trim() + "\"";
                                                pass &= false;
                                            }
                                            if ( importFields.ContainsKey("姓名") && ( "" + wb.Worksheets[0].Cells[i, importFields["姓名"]].StringValue ).Trim() != var.Name.Trim() )
                                            {
                                                if ( studentNumberStudents[num].Count == 1 )
                                                    rowError += ( rowError == "" ? "" : "、\n" ) + "系統內學生姓名為\"" + var.Name.Trim() + "\"";
                                                pass &= false;
                                            }
                                            if ( pass )
                                            {
                                                stu = var;
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                                if ( err )
                                {
                                    rowError = "系統內發現多名相同學號學生且皆為在校學生，\n需要其他學生欄位進行識別。";
                                }
                                #endregion
                            }
                            if ( studentNumberStudents[num].Count == 1 )
                            {
                                BriefStudentData var = studentNumberStudents[num][0];
                                bool pass = true;
                                if ( importFields.ContainsKey("班級") && ( "" + wb.Worksheets[0].Cells[i, importFields["班級"]].StringValue ).Trim() != var.ClassName.Trim() )
                                {
                                    if ( studentNumberStudents[num].Count == 1 )
                                        rowError += ( rowError == "" ? "" : "、\n" ) + "系統內學生班級為\"" + var.ClassName.Trim() + "\"";
                                    pass &= false;
                                }
                                if ( importFields.ContainsKey("科別") && (""+wb.Worksheets[0].Cells[i, importFields["科別"]].StringValue).Trim() != var.Department.Trim() )
                                {
                                    if ( studentNumberStudents[num].Count == 1 )
                                        rowError += ( rowError == "" ? "" : "、\n" ) + "系統內學生科別為\"" + var.Department.Trim() + "\"";
                                    pass &= false;
                                }
                                if ( importFields.ContainsKey("座號") && ( "" + wb.Worksheets[0].Cells[i, importFields["座號"]].StringValue ).Trim() != var.SeatNo.Trim() )
                                {
                                    if ( studentNumberStudents[num].Count == 1 )
                                        rowError += ( rowError == "" ? "" : "、\n" ) + "系統內學生座號為\"" + var.SeatNo.Trim() + "\"";
                                    pass &= false;
                                }
                                if ( importFields.ContainsKey("姓名") && (""+wb.Worksheets[0].Cells[i, importFields["姓名"]].StringValue).Trim() != var.Name.Trim() )
                                {
                                    if ( studentNumberStudents[num].Count == 1 )
                                        rowError += ( rowError == "" ? "" : "、\n" ) + "系統內學生姓名為\"" + var.Name.Trim() + "\"";
                                    pass &= false;
                                }
                                if ( pass )
                                {
                                    stu = var;
                                }
                            }
                        }
                        else
                        {
                            rowError = "學生不存在或不是在校生!!";
                        }
                        if ( rowError == "" && stu == null )
                            rowError = "學生資料有誤!!";
                        #endregion
                        if ( rowError == "" )
                        {
                            RowData rowdata = new RowData();
                            rowdata.ID = stu.ID;
                            foreach ( int index in fieldIndex.Keys )
                            {
                                //rowdata.Add(fieldIndex[index], ( "" + wb.Worksheets[0].Cells[i, index].StringValue ).Trim());
                                if ( wb.Worksheets[0].Cells[i, index].Type == CellValueType.IsDateTime )
                                {
                                    rowdata.Add(fieldIndex[index], wb.Worksheets[0].Cells[i, index].DateTimeValue.ToString());
                                }
                                else
                                    rowdata.Add(fieldIndex[index], ( "" + wb.Worksheets[0].Cells[i, index].StringValue ).Trim());
                            }
                            rowDataIndex.Add(rowdata, i);
                        }
                        else
                        {
                            errorCount++;
                            errorSheet.Cells[errorSheetRowIndex, 0].PutValue(i + 1);
                            errorSheet.Cells[errorSheetRowIndex, 1].PutValue("錯誤");
                            errorSheet.Cells[errorSheetRowIndex, 2].PutValue(rowError);
                            errorSheet.Cells[errorSheetRowIndex, 0].Style = errorStyle;
                            errorSheet.Cells[errorSheetRowIndex, 1].Style = errorStyle2;
                            errorSheet.Cells[errorSheetRowIndex, 2].Style = errorStyle2;
                            errorSheet.Hyperlinks.Add(errorSheetRowIndex, 0, 1, 1, "'" + wb.Worksheets[0].Name + "'!" + wb.Worksheets[0].Cells[i, 0].Name);
                            wb.Worksheets[0].Hyperlinks.Add(i, 0, 1, 1, "'" + errorSheetName + "'!" + errorSheet.Cells[errorSheetRowIndex, 0].Name);
                            errorSheet.AutoFitRow(errorSheetRowIndex);
                            errorSheetRowIndex++;
                            wb.Worksheets[0].Cells[i, 0].Style = errorStyle;
                        }
                    }
                    else
                    {
                        bool isNullRow = true;
                        for ( byte c = 0 ; c < wb.Worksheets[0].Cells.MaxDataColumn ; c++ )
                        {
                            if ( ( "" + wb.Worksheets[0].Cells[i, c].StringValue ).Trim() != "" )
                                isNullRow = false;
                        }
                        if ( !isNullRow )
                        {
                            errorCount++;
                            errorSheet.Cells[errorSheetRowIndex, 0].PutValue(i + 1);
                            errorSheet.Cells[errorSheetRowIndex, 1].PutValue("錯誤");
                            errorSheet.Cells[errorSheetRowIndex, 2].PutValue("驗證欄位(學號)不得空白");
                            errorSheet.Cells[errorSheetRowIndex, 0].Style = errorStyle;
                            errorSheet.Cells[errorSheetRowIndex, 1].Style = errorStyle2;
                            errorSheet.Cells[errorSheetRowIndex, 2].Style = errorStyle2;
                            errorSheet.Hyperlinks.Add(errorSheetRowIndex, 0, 1, 1, "'" + wb.Worksheets[0].Name + "'!" + wb.Worksheets[0].Cells[i, 0].Name);
                            wb.Worksheets[0].Hyperlinks.Add(i, 0, 1, 1, "'" + errorSheetName + "'!" + errorSheet.Cells[errorSheetRowIndex, 0].Name);
                            errorSheet.AutoFitRow(errorSheetRowIndex);
                            errorSheetRowIndex++;
                            wb.Worksheets[0].Cells[i, 0].Style = errorStyle;
                        }
                    }

                    if ( bkw.CancellationPending )
                    {
                        e.Cancel = true;
                        _ErrorWB = wb;
                        return;
                    }
                    progress = ( (double)i ) * 100.0 / 100000;
                    bkw.ReportProgress((int)progress, new int[] { errorCount, warningCount });
                }
                #endregion
            }
            #endregion

            #region 驗證資料
            List<string> list = new List<string>();
            foreach ( RowData row in rowDataIndex.Keys )
            {
                if ( !list.Contains(row.ID) )
                    list.Add(row.ID);
            }
            _Process.StartValidate(list);
            double totleCount = (double)rowDataIndex.Count;
            double count = 0.0;
            foreach ( RowData row in rowDataIndex.Keys )
            {
                #region 驗證
                string rowError = "";
                Dictionary<string, string> errorFields, warningFields;
                RowDataValidatedEventArgs args = _Process.ValidateRow(row, selectedFields);
                errorFields = args.ErrorFields;
                warningFields = args.WarningFields;
                rowError += args.ErrorMessage;
                if ( rowError == "" && errorFields.Count == 0 && warningFields.Count == 0 )
                {
                    wb.Worksheets[0].Cells[rowDataIndex[row], 0].Style = passStyle;
                }
                else
                {
                    string message = "";
                    bool hasError = false;
                    if ( rowError != "" || errorFields.Count != 0 )
                    {
                        errorCount++;
                        hasError = true;
                        message = rowError;
                        message += ( message == "" ? "" : "\n" ) + "";
                        foreach ( string key in errorFields.Keys )
                        {
                            message += ( message == "" ? "" : "\n" ) + "  " + key + "：" + errorFields[key];
                        }
                        errorSheet.Cells[errorSheetRowIndex, 0].PutValue(rowDataIndex[row] + 1);
                        errorSheet.Cells[errorSheetRowIndex, 1].PutValue(hasError ? "錯誤" : "警告");
                        errorSheet.Cells[errorSheetRowIndex, 2].PutValue(message);
                        errorSheet.Cells[errorSheetRowIndex, 0].Style = errorStyle;
                        errorSheet.Cells[errorSheetRowIndex, 1].Style = errorStyle2;
                        errorSheet.Cells[errorSheetRowIndex, 2].Style = errorStyle2;
                        errorSheet.Hyperlinks.Add(errorSheetRowIndex, 0, 1, 1, "'" + wb.Worksheets[0].Name + "'!" + wb.Worksheets[0].Cells[rowDataIndex[row], 0].Name);
                        wb.Worksheets[0].Hyperlinks.Add(rowDataIndex[row], 0, 1, 1, "'" + errorSheetName + "'!" + errorSheet.Cells[errorSheetRowIndex, 0].Name);
                        errorSheet.AutoFitRow(errorSheetRowIndex);
                        errorSheetRowIndex++;
                    }
                    message = "";
                    if ( warningFields.Count != 0 )
                    {
                        if ( !hasError )
                            warningCount++;
                        message += ( message == "" ? "" : "\n" ) + "";
                        foreach ( string key in warningFields.Keys )
                        {
                            message += ( message == "" ? "" : "\n" ) + "  " + key + "：" + warningFields[key];
                        }
                        errorSheet.Cells[errorSheetRowIndex, 0].PutValue(rowDataIndex[row] + 1);
                        errorSheet.Cells[errorSheetRowIndex, 1].PutValue("警告");
                        errorSheet.Cells[errorSheetRowIndex, 2].PutValue(message);
                        errorSheet.Cells[errorSheetRowIndex, 0].Style = warningStyle;
                        errorSheet.Cells[errorSheetRowIndex, 1].Style = warningStyle2;
                        errorSheet.Cells[errorSheetRowIndex, 2].Style = warningStyle2;
                        errorSheet.Hyperlinks.Add(errorSheetRowIndex, 0, 1, 1, "'" + wb.Worksheets[0].Name + "'!" + wb.Worksheets[0].Cells[rowDataIndex[row], 0].Name);
                        if ( !hasError )
                            wb.Worksheets[0].Hyperlinks.Add(rowDataIndex[row], 0, 1, 1, "'" + errorSheetName + "'!" + errorSheet.Cells[errorSheetRowIndex, 0].Name);
                        errorSheet.AutoFitRow(errorSheetRowIndex);
                        errorSheetRowIndex++;
                    }
                    wb.Worksheets[0].Cells[rowDataIndex[row], 0].Style = hasError ? errorStyle : warningStyle;
                }
                #endregion
                if ( bkw.CancellationPending )
                {
                    e.Cancel = true;
                    _ErrorWB = wb;
                    return;
                }
                count++;
                bkw.ReportProgress((int)( progress + count * ( 100.0 - progress ) / totleCount ), new int[] { errorCount, warningCount });
            }


            _Process.FinishValidate();
            #endregion

            List<RowData> rows = new List<RowData>();
            rows.AddRange(rowDataIndex.Keys);

            bkw.ReportProgress(100, new int[] { errorCount, warningCount });

            errorSheet.AutoFitColumn(0);
            errorSheet.AutoFitColumn(1, 1, 500);
            errorSheet.AutoFitColumn(2, 1, 500);

            e.Result = new object[] { wb, errorCount == 0, rows, selectedFields };
        }

        private void _BKWValidate_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker bkw = (BackgroundWorker)sender;
            if ( bkw.CancellationPending )
                return;
            progressBarX1.Value = e.ProgressPercentage;
            lblErrCount.Text = "" + (int)( (int[])e.UserState )[0];
            lblWarningCount.Text = "" + (int)( (int[])e.UserState )[1];
            if ( linkLabel3.Visible == false && (int)( (int[])e.UserState )[0] > 0 || (int)( (int[])e.UserState )[1] > 0 )
                linkLabel3.Visible = true;
        }

        private void _BKWValidate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker bkw = (BackgroundWorker)sender;

            linkLabel3.Visible = false;
            if ( !bkw.CancellationPending && !e.Cancelled )
            {
                linkLabel1.Visible = true;
                linkLabel1.Tag = ( (object[])e.Result )[0];
                wizardPage3.FinishButtonEnabled = ( "" + linkLabel3.Tag != "!" && (bool)( (object[])e.Result )[1] ) ? eWizardButtonState.True : eWizardButtonState.False;
                this.Tag = ( (object[])e.Result )[2];
                wizard1.Tag = ( (object[])e.Result )[3];
                if ( wizardPage3.FinishButtonEnabled == eWizardButtonState.True )
                    labelX2.Text = "資料驗證完成";
                else
                    labelX2.Text = "資料驗證失敗";
            }
            else
            {
                if ( _ErrorWB != null )
                {
                    linkLabel1.Visible = true;
                    linkLabel1.Tag = _ErrorWB;
                    wizardPage3.FinishButtonEnabled = eWizardButtonState.False;
                    labelX2.Text = "資料驗證失敗";
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Workbook wb = new Workbook();
            wb.Copy((Workbook)( (Control)sender ).Tag);
            Completed(_Process.Title + "_驗證", wb);
        }

        private void Completed(string inputReportName, Workbook inputDoc)
        {
            string reportName = inputReportName;

            string path = Path.Combine(Application.StartupPath, "Reports");
            if ( !Directory.Exists(path) )
                Directory.CreateDirectory(path);
            path = Path.Combine(path, reportName + ".xls");

            Workbook doc = inputDoc;

            if ( File.Exists(path) )
            {
                int i = 1;
                while ( true )
                {
                    string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ( i++ ) + Path.GetExtension(path);
                    if ( !File.Exists(newPath) )
                    {
                        path = newPath;
                        break;
                    }
                }
            }

            try
            {
                doc.Save(path);
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Title = "另存新檔";
                sd.FileName = reportName + ".xls";
                sd.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                if ( sd.ShowDialog() == DialogResult.OK )
                {
                    try
                    {
                        doc.Save(sd.FileName);

                    }
                    catch
                    {
                        MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void wizardPage3_FinishButtonClick(object sender, CancelEventArgs e)
        {
            Dictionary<string, List<RowData>> ID_Rows = new Dictionary<string, List<RowData>>();
            #region 將資料依ID群組
            List<RowData> rows = (List<RowData>)this.Tag;
            foreach ( RowData row in rows )
            {
                if ( !ID_Rows.ContainsKey(row.ID) )
                    ID_Rows.Add(row.ID, new List<RowData>());
                ID_Rows[row.ID].Add(row);
            }
            #endregion
            List<List<RowData>> packages = new List<List<RowData>>();
            #region 將資料分割成數個Package
            {
                List<RowData> package = null;
                int packageCount = 0;
                foreach ( string id in ID_Rows.Keys )
                {
                    if ( packageCount <= 0 )
                    {
                        package = new List<RowData>();
                        packages.Add(package);
                        packageCount = _Process.PackageLimit;
                    }
                    package.AddRange(ID_Rows[id]);
                    packageCount -= ID_Rows[id].Count;
                }
            }
            #endregion
            BackgroundWorker bkwImport = new BackgroundWorker();
            bkwImport.WorkerReportsProgress = true;
            bkwImport.DoWork += new DoWorkEventHandler(bkwImport_DoWork);
            bkwImport.ProgressChanged += new ProgressChangedEventHandler(bkwImport_ProgressChanged);
            bkwImport.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkwImport_RunWorkerCompleted);
            bkwImport.RunWorkerAsync(new object[] { packages, wizard1.Tag });
            this.Close();
        }

        void bkwImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ( e.Error != null )
                throw e.Error;
            else
            {
                _Process.FinishImport();
                SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage(_Process.Title + " 完成", 100);
            }
        }

        void bkwImport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage(_Process.Title + "...", e.ProgressPercentage);
        }

        void bkwImport_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bkw = (BackgroundWorker)sender;
            List<List<RowData>> packages = (List<List<RowData>>)( (object[])e.Argument )[0];
            List<string> importFields = (List<string>)( (object[])e.Argument )[1];
            _Process.StartImport();
            double totle = 0.0;
            double packageProgress = 100.0 / packages.Count;
            bkw.ReportProgress(1);
            foreach ( List<RowData> package in packages )
            {
                _Process.ImportPackage(package, importFields);
                totle += packageProgress;
                bkw.ReportProgress((int)totle);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtFile_TextChanged(null, null);
        }
    }
}