using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.Customization.PlugIn.ImportExport;
using SmartSchool.Customization.Data;
using System.Threading;
using Aspose.Cells;
using System.IO;
using DevComponents.DotNetBar.Rendering;
using SmartSchool.Common;

namespace SmartSchool.StudentRelated.RibbonBars.Import
{
    public partial class ExportStudent : SmartSchool.Common.BaseForm
    {
        private ExportProcess _Process;

        private AccessHelper _AccessHelper;

        private IntelliSchool.DSA.ClientFramework.ControlCommunication.ListViewCheckAllManager _CheckAllManager = new IntelliSchool.DSA.ClientFramework.ControlCommunication.ListViewCheckAllManager();

        public ExportStudent(ExportProcess process)
        {
            InitializeComponent();

            process.StartupProcess();

            ButtonX advButton = new ButtonX();
            advButton.ShowSubItems=false;
            advButton.Text = "進階>>";
            advButton.Top = this.wizard1.Controls[1].Controls[0].Top;
            advButton.Left = 5;
            advButton.Size = this.wizard1.Controls[1].Controls[0].Size;
            advButton.Visible = true;
            DevComponents.DotNetBar.ControlContainerItem container = new ControlContainerItem();
            advButton.SubItems.Add(container);
            advButton.PopupSide = ePopupSide.Top;
            advButton.SplitButton = false;
            container.Control = process.Configuration;
            advButton.Click += new EventHandler(advButton_Click);
            advButton.Enabled = ( process.Configuration != null );
            this.wizard1.Controls[1].Controls.Add(advButton);

            #region 設定Wizard會跟著Style跑
            //this.wizard1.FooterStyle.ApplyStyle(( GlobalManager.Renderer as Office2007Renderer ).ColorTable.GetClass(ElementStyleClassKeys.RibbonFileMenuBottomContainerKey));
            this.wizard1.HeaderStyle.ApplyStyle(( GlobalManager.Renderer as Office2007Renderer ).ColorTable.GetClass(ElementStyleClassKeys.RibbonFileMenuBottomContainerKey));
            this.wizard1.FooterStyle.BackColorGradientAngle = -90;
            this.wizard1.FooterStyle.BackColorGradientType = eGradientType.Linear;
            this.wizard1.FooterStyle.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.Default.TopBackground.Start;
            this.wizard1.FooterStyle.BackColor2 = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.Default.TopBackground.End;
            this.wizard1.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.Default.TopBackground.Start;
            this.wizard1.BackgroundImage = null;
            for ( int i = 0 ; i < 6 ; i++ )
            {
                ( this.wizard1.Controls[1].Controls[i] as ButtonX ).ColorTable = eButtonColor.OrangeWithBackground;
            }
            ( this.wizard1.Controls[0].Controls[1] as System.Windows.Forms.Label ).ForeColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.MouseOver.TitleText;
            ( this.wizard1.Controls[0].Controls[2] as System.Windows.Forms.Label ).ForeColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.Default.TitleText; 
            #endregion


            this.checkBox1.ForeColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.CheckBoxItem.Default.Text;
            listViewEx1.ForeColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.CheckBoxItem.Default.Text;
            //listViewEx1.BackColor = this.wizard1.BackColor;

            _Process = process;
            this.Text = process.Title;

            wizardPage1.PageTitle = process.Title;
            _AccessHelper = new AccessHelper();
            _CheckAllManager.TargetComboBox = this.checkBox1;
            _CheckAllManager.TargetListView = this.listViewEx1;
        }

        void advButton_Click(object sender, EventArgs e)
        {
            ButtonX button=( (ButtonX)sender );
            button.Expanded ^= true;
            button.Text = button.Expanded ? "進階<<" : "進階>>";
        }

        private void wizard1_CancelButtonClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }

        private void ExportStudent_Shown(object sender, EventArgs e)
        {
            //System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("匯出資料欄位", System.Windows.Forms.HorizontalAlignment.Center);
            //this.listViewEx1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            //listViewGroup4});
            List<ListViewItem> items = new List<ListViewItem>();
            foreach ( string var in _Process.ExportableFields )
            {
                ListViewItem item = new ListViewItem(var);
                item.Checked = true;
                items.Add(item);
            }
            listViewEx1.Items.AddRange(items.ToArray());
            _Process.ExportableFieldsChanged += new EventHandler(_Process_ExportableFieldsChanged);
            if ( _Process.Image != null )
            {
                Bitmap b = new Bitmap(48, 48);
                using ( Graphics g = Graphics.FromImage(b) )
                    g.DrawImage(_Process.Image, 0, 0, 48, 48);
                this.wizardPage1.PageHeaderImage = b;
            }
        }

        void _Process_ExportableFieldsChanged(object sender, EventArgs e)
        {
            List<string> unCheckedList = new List<string>();
            foreach ( ListViewItem item in listViewEx1.Items )
            {
                if ( !item.Checked )
                    unCheckedList.Add(item.Text);
            }
            List<string> newFields = new List<string>(new string[] { "學生系統編號", "學號", "班級", "座號", "科別", "姓名" });
            newFields.AddRange(_Process.ExportableFields);
            List<ListViewItem> items = new List<ListViewItem>();
            foreach ( string var in newFields )
            {
                ListViewItem item = new ListViewItem(var);
                item.Checked = !unCheckedList.Contains(var);
                items.Add(item);
            }
            listViewEx1.Items.AddRange(items.ToArray());
        }

        private void wizard1_FinishButtonClick(object sender, CancelEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "匯出成績";
            saveFileDialog1.FileName = ""+_Process.Title+".xls";
            saveFileDialog1.Filter = "Excel (*.xls)|*.xls|所有檔案 (*.*)|*.*";
            if ( saveFileDialog1.ShowDialog() == DialogResult.OK )
            {
                List<string> idlist = new List<string>();
                #region 取得選取學生編號
                List<SmartSchool.Customization.Data.StudentRecord> selectedStudents = _AccessHelper.StudentHelper.GetSelectedStudent();
                foreach ( SmartSchool.Customization.Data.StudentRecord stu in selectedStudents )
                {
                    if ( !idlist.Contains(stu.StudentID) )
                    {
                        idlist.Add(stu.StudentID);
                    }
                } 
                #endregion

                List<string> studentFieldList = new List<string>();
                List<string> exportFieldList = new List<string>();
                #region 取得選取欄位
                foreach ( ListViewItem item in listViewEx1.Items )
                {
                    if ( item.Checked )
                    {
                        if ( _Process.ExportableFields.Contains(item.Text) )
                            exportFieldList.Add(item.Text.Trim());
                        else
                            studentFieldList.Add(item.Text.Trim());
                    }
                } 
                #endregion

                List<List<string>> splitList = new List<List<string>>();
                //把全部學生以_Process.PackageLimit人分一包
                #region 把全部學生以_Process.PackageLimit人分一包
                int count = 0;
                List<string> package = new List<string>();
                foreach ( string id in idlist )
                {
                    if ( count == 0 )
                    {
                        count = ( splitList.Count + 1 ) * 50;
                        count = count > _Process.PackageLimit ? _Process.PackageLimit : count;
                        package = new List<string>(_Process.PackageLimit);
                        splitList.Add(package);
                    }
                    package.Add(id);
                    count--;
                }
                #endregion
                //兩條獨立讀取
                Dictionary<List<string>, ManualResetEvent> Loader1 = new Dictionary<List<string>, ManualResetEvent>();
                Dictionary<List<string>, ManualResetEvent> Loader2 = new Dictionary<List<string>, ManualResetEvent>();
                //已讀取資料
                Dictionary<ManualResetEvent, List<RowData>> Filler = new Dictionary<ManualResetEvent, List<RowData>>();
                int i = 0;
                foreach ( List<string> p in splitList )
                {
                    ManualResetEvent handleEvent = new ManualResetEvent(false);
                    if ( ( i & 1 ) == 0 )
                        Loader1.Add(p, handleEvent);
                    else
                        Loader2.Add(p, handleEvent);
                    Filler.Add(handleEvent, new List<RowData>());
                    i++;
                }

                //在背景執行取得資料
                BackgroundWorker bkwDataLoader = new BackgroundWorker();
                bkwDataLoader.DoWork += new DoWorkEventHandler(bkwDataLoader_DoWork);
                bkwDataLoader.RunWorkerAsync(new object[] { Loader1, Filler, exportFieldList });
                bkwDataLoader = new BackgroundWorker();
                bkwDataLoader.DoWork += new DoWorkEventHandler(bkwDataLoader_DoWork);
                bkwDataLoader.RunWorkerAsync(new object[] { Loader2, Filler, exportFieldList });
                //在背景計算不及格名單
                BackgroundWorker bkwNotPassComputer = new BackgroundWorker();
                bkwNotPassComputer.WorkerReportsProgress = true;
                bkwNotPassComputer.DoWork += new DoWorkEventHandler(bkwNotPassComputer_DoWork);
                bkwNotPassComputer.ProgressChanged += new ProgressChangedEventHandler(bkwNotPassComputer_ProgressChanged);
                bkwNotPassComputer.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkwNotPassComputer_RunWorkerCompleted);
                bkwNotPassComputer.RunWorkerAsync(new object[] { saveFileDialog1.FileName, studentFieldList, exportFieldList, Filler });
                this.Close();
            }
        }

        void bkwNotPassComputer_DoWork(object sender, DoWorkEventArgs e)
        {
            string fileName = (string)( (object[])e.Argument )[0];
            List<string> studentFieldList = (List<string>)( (object[])e.Argument )[1];
            List<string> exportFieldList = (List<string>)( (object[])e.Argument )[2];
            Dictionary<ManualResetEvent, List<RowData>> Filler = (Dictionary<ManualResetEvent, List<RowData>>)( (object[])e.Argument )[3];
            double totleProgress = 0.0;
            double packageProgress = 100.0 / Filler.Count;
            Workbook report = new Workbook();
            report.Worksheets[0].Name = _Process.Title;
            ( (BackgroundWorker)sender ).ReportProgress(1, _Process.Title + " 資料整理中...");
            int RowIndex = 0;
            int i = 0;
            //填表頭
            for ( ; i < studentFieldList.Count ; i++ )
            {
                report.Worksheets[0].Cells[0, i].PutValue(studentFieldList[i]);
            }
            for ( int j = 0 ; j < exportFieldList.Count ; j++ )
            {
                report.Worksheets[0].Cells[0, i + j].PutValue(exportFieldList[j]);
            }
            RowIndex=1;
            foreach ( ManualResetEvent eve in Filler.Keys )
            {
                eve.WaitOne();
                if ( RowIndex <= 65535 )
                {
                    double miniProgress = Filler[eve].Count == 0 ? 1 : packageProgress / Filler[eve].Count;
                    double miniTotle = 0;
                    foreach ( RowData row in Filler[eve] )
                    {
                        List<SmartSchool.Customization.Data.StudentRecord> students = _AccessHelper.StudentHelper.GetStudents(row.ID);
                        if ( students.Count == 1 )
                        {
                            if ( RowIndex <= 65535 )
                            {
                                SmartSchool.Customization.Data.StudentRecord student = students[0];
                                i = 0;
                                for ( ; i < studentFieldList.Count ; i++ )
                                {
                                    switch ( studentFieldList[i] )
                                    {
                                        case "學生系統編號": report.Worksheets[0].Cells[RowIndex, i].PutValue(student.StudentID); break;
                                        case "學號": report.Worksheets[0].Cells[RowIndex, i].PutValue(student.StudentNumber); break;
                                        case "班級": report.Worksheets[0].Cells[RowIndex, i].PutValue(student.RefClass == null ? "" : student.RefClass.ClassName); break;
                                        case "座號": report.Worksheets[0].Cells[RowIndex, i].PutValue(student.SeatNo); break;
                                        case "科別": report.Worksheets[0].Cells[RowIndex, i].PutValue(student.Department); break;
                                        case "姓名": report.Worksheets[0].Cells[RowIndex, i].PutValue(student.StudentName); break;
                                        default:
                                            break;
                                    }
                                }
                                for ( int j = 0 ; j < exportFieldList.Count ; j++ )
                                {
                                    report.Worksheets[0].Cells[RowIndex, i + j].PutValue(row.ContainsKey(exportFieldList[j]) ? row[exportFieldList[j]] : "");
                                } 
                            }
                            RowIndex++;
                        }
                        miniTotle += miniProgress;
                        ( (BackgroundWorker)sender ).ReportProgress((int)(totleProgress + miniTotle), _Process.Title + " 處理中...");
                    }
                }
                totleProgress += packageProgress;
                ( (BackgroundWorker)sender ).ReportProgress((int)(totleProgress), _Process.Title + " 處理中...");
            }
            for ( int k = 0 ;k < studentFieldList.Count + exportFieldList.Count ; k++ )
            {
                report.Worksheets[0].AutoFitColumn(k, 0, 150);
            }
            report.Worksheets[0].FreezePanes(1, 0, 1, studentFieldList.Count + exportFieldList.Count);
            e.Result = new object[] { report, fileName, RowIndex > 65535 };
        }

        void bkwNotPassComputer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage("" + e.UserState, e.ProgressPercentage);
        }

        void bkwNotPassComputer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage(_Process.Title + " 檔案儲存中。", 100);
            if ( e.Error == null )
            {
                Workbook report = (Workbook)( (object[])e.Result )[0];
                bool overLimit = (bool)( (object[])e.Result )[2];
                //儲存 Excel
                #region 儲存 Excel
                string path = (string)( (object[])e.Result )[1];

                if ( File.Exists(path) )
                {
                    bool needCount = true;
                    try
                    {
                        File.Delete(path);
                        needCount = false;
                    }
                    catch { }
                    int i = 1;
                    while ( needCount )
                    {
                        string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ( i++ ) + Path.GetExtension(path);
                        if ( !File.Exists(newPath) )
                        {
                            path = newPath;
                            break;
                        }
                        else
                        {
                            try
                            {
                                File.Delete(newPath);
                                path = newPath;
                                break;
                            }
                            catch { }
                        }
                    }
                }
                try
                {
                    File.Create(path).Close();
                }
                catch
                {
                    SaveFileDialog sd = new SaveFileDialog();
                    sd.Title = "另存新檔";
                    sd.FileName = Path.GetFileNameWithoutExtension(path) + ".xls";
                    sd.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                    if ( sd.ShowDialog() == DialogResult.OK )
                    {
                        try
                        {
                            File.Create(sd.FileName);
                            path = sd.FileName;
                        }
                        catch
                        {
                            MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                report.Save(path, FileFormatType.Excel2003);
                #endregion
                SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage(_Process.Title + "完成。");
                if ( overLimit )
                    MsgBox.Show("匯出資料已經超過Excel的極限(65536筆)。\n超出的資料無法被匯出。\n\n請減少選取學生人數。");
                System.Diagnostics.Process.Start(path);
            }
            else
                SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage(_Process.Title + "發生未預期錯誤。");
        }

        void bkwDataLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            Dictionary<List<string>, ManualResetEvent> handle = (Dictionary<List<string>, ManualResetEvent>)( (object[])e.Argument )[0];
            Dictionary<ManualResetEvent, List<RowData>> Filler = (Dictionary<ManualResetEvent, List<RowData>>)( (object[])e.Argument )[1];
            List<string> exportFieldList = (List<string>)( (object[])e.Argument )[2];
            foreach ( List<string> splitList in handle.Keys )
            {
                try
                {
                    Filler[handle[splitList]].AddRange(_Process.GetExportData(splitList, exportFieldList));
                }
                catch(Exception ex)
                {

                    CurrentUser user = CurrentUser.Instance;
                    SmartSchool.ExceptionHandler.BugReporter.ReportException("SmartSchool", user.SystemVersion, ex, false);
                }
                finally
                {
                    handle[splitList].Set();
                }
            }
        }
    }
}