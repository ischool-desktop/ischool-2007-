using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.Customization.Data;
using System.Threading;
using SmartSchool;
using Aspose.Cells;
using System.IO;
using SmartSchool.Customization.Data.StudentExtension;
using SmartSchool.Common;

namespace SmartSchool.StudentRelated.RibbonBars.Import
{
    public partial class ExportSemesterScore : SmartSchool.Common.BaseForm
    {
        private AccessHelper _Seed;

        public ExportSemesterScore()
        {
            InitializeComponent();
            _Seed = new AccessHelper();
        }

        private void wizard1_CancelButtonClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }

        #region 科目不及格名單
        private const int _PackageSize = 300;
        private void wizard1_FinishButtonClick(object sender, CancelEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "匯出成績";
            saveFileDialog1.FileName = "學期科目成績.xls";
            saveFileDialog1.Filter = "Excel (*.xls)|*.xls|所有檔案 (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                List<SmartSchool.Customization.Data.StudentRecord> selectedStudents = _Seed.StudentHelper.GetSelectedStudent();
                List<List<SmartSchool.Customization.Data.StudentRecord>> splitList = new List<List<SmartSchool.Customization.Data.StudentRecord>>();
                Dictionary<List<SmartSchool.Customization.Data.StudentRecord>, ManualResetEvent> handle = new Dictionary<List<SmartSchool.Customization.Data.StudentRecord>, ManualResetEvent>();
                Dictionary<List<SmartSchool.Customization.Data.StudentRecord>, ManualResetEvent> handle1 = new Dictionary<List<SmartSchool.Customization.Data.StudentRecord>, ManualResetEvent>();
                Dictionary<List<SmartSchool.Customization.Data.StudentRecord>, ManualResetEvent> handle2 = new Dictionary<List<SmartSchool.Customization.Data.StudentRecord>, ManualResetEvent>();

                //把全部在校生以_PackageSize人分一包
                #region 把全部在校生以_PackageSize人分一包
                int count = 0;
                List<SmartSchool.Customization.Data.StudentRecord> package = new List<SmartSchool.Customization.Data.StudentRecord>();
                foreach (SmartSchool.Customization.Data.StudentRecord student in selectedStudents)
                {
                    if (count == 0)
                    {
                        count = (splitList.Count + 1) * 50;
                        count = count > _PackageSize ? _PackageSize : count;
                        package = new List<SmartSchool.Customization.Data.StudentRecord>(_PackageSize);
                        splitList.Add(package);
                    }
                    package.Add(student);
                    count--;
                }
                #endregion
                //每一包一個ManualResetEvent一個DSResponse
                #region 每一包一個ManualResetEvent(預設為不可通過)一個DSResponse
                int i = 0;
                foreach (List<SmartSchool.Customization.Data.StudentRecord> p in splitList)
                {
                    ManualResetEvent handleEvent = new ManualResetEvent(false);
                    handle.Add(p, handleEvent);
                    if ((i & 1) == 0)
                        handle1.Add(p, handleEvent);
                    else
                        handle2.Add(p, handleEvent);
                    i++;
                }
                #endregion
                //在背景執行取得資料
                BackgroundWorker bkwDataLoader = new BackgroundWorker();
                bkwDataLoader.DoWork += new DoWorkEventHandler(bkwDataLoader_DoWork);
                bkwDataLoader.RunWorkerAsync(new object[] { handle1 });
                bkwDataLoader = new BackgroundWorker();
                bkwDataLoader.DoWork += new DoWorkEventHandler(bkwDataLoader_DoWork);
                bkwDataLoader.RunWorkerAsync(new object[] { handle2 });
                //在背景計算不及格名單
                BackgroundWorker bkwNotPassComputer = new BackgroundWorker();
                bkwNotPassComputer.WorkerReportsProgress = true;
                bkwNotPassComputer.DoWork += new DoWorkEventHandler(bkwNotPassComputer_DoWork);
                bkwNotPassComputer.ProgressChanged += new ProgressChangedEventHandler(bkwNotPassComputer_ProgressChanged);
                bkwNotPassComputer.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkwNotPassComputer_RunWorkerCompleted);
                List<string> fieldList = new List<string>();
                foreach (ListViewItem item in listViewEx1.Items)
                {
                    if (item.Checked)
                    {
                        fieldList.Add(item.Text.Trim());
                    }
                }
                bkwNotPassComputer.RunWorkerAsync(new object[] { handle, saveFileDialog1.FileName, fieldList.ToArray() });
                this.Close();
            }
        }

        void bkwNotPassComputer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MotherForm.Instance.SetBarMessage("匯出學期科目成績檔案儲存中。",100);
            if (e.Error == null)
            {
                Workbook report = (Workbook)((object[])e.Result)[0];
                bool overLimit = (bool)((object[])e.Result)[2];
                //儲存 Excel
                #region 儲存 Excel
                string path = (string)((object[])e.Result)[1];

                if (File.Exists(path))
                {
                    bool needCount = true;
                    try
                    {
                        File.Delete(path);
                        needCount = false;
                    }
                    catch { }
                    int i = 1;
                    while (needCount)
                    {
                        string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + (i++) + Path.GetExtension(path);
                        if (!File.Exists(newPath))
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
                    if (sd.ShowDialog() == DialogResult.OK)
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
                MotherForm.Instance.SetBarMessage("匯出學期科目成績完成。");
                if(overLimit)
                    MsgBox.Show("匯出資料已經超過Excel的極限(65536筆)。\n超出的資料無法被匯出。\n\n請減少選取學生人數。");
                System.Diagnostics.Process.Start(path);
            }
            else
                MotherForm.Instance.SetBarMessage("匯出學期科目成績發生未預期錯誤。");
        }

        void bkwNotPassComputer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            MotherForm.Instance.SetBarMessage("" + e.UserState, e.ProgressPercentage);
        }

        void bkwNotPassComputer_DoWork(object sender, DoWorkEventArgs e)
        {
            Dictionary<List<SmartSchool.Customization.Data.StudentRecord>, ManualResetEvent> handle = (Dictionary<List<SmartSchool.Customization.Data.StudentRecord>, ManualResetEvent>)((object[])e.Argument)[0];
            string fileName = "" + ((object[])e.Argument)[1];
            string[] fildList = (string[])((object[])e.Argument)[2];
            double totleProgress = 0.0;
            double currentProgress = 100.0 / handle.Count;
            Workbook report = new Workbook();
            report.Worksheets[0].Name = "學生成績資料";

            for (int i = 0; i < fildList.Length; i++)
            {
                report.Worksheets[0].Cells[0, i].PutValue(fildList[i]);
            }
            ((BackgroundWorker)sender).ReportProgress(1, "學期科目成績整理中...");
            int RowIndex = 1;
            foreach (List<SmartSchool.Customization.Data.StudentRecord> splitList in handle.Keys)
            {
                //等待這包的成績資料載下來
                handle[splitList].WaitOne();
                double packageProgress = 0.0;
                double miniProgress = currentProgress / splitList.Count;
                foreach (SmartSchool.Customization.Data.StudentRecord student in splitList)
                {
                    foreach (SemesterSubjectScoreInfo subjectScore in student.SemesterSubjectScoreList)
                    {
                        //EXCEL極限65536列，超過不列出
                        if (RowIndex <= 65535)
                        {
                            #region 填入欄位資料
                            for (int i = 0; i < fildList.Length; i++)
                            {
                                switch (fildList[i])
                                {
                                    case "學生系統編號": report.Worksheets[0].Cells[RowIndex, i].PutValue(student.StudentID); break;
                                    case "學號": report.Worksheets[0].Cells[RowIndex, i].PutValue(student.StudentNumber); break;
                                    case "班級": report.Worksheets[0].Cells[RowIndex, i].PutValue(student.RefClass == null ? "" : student.RefClass.ClassName); break;
                                    case "座號": report.Worksheets[0].Cells[RowIndex, i].PutValue(student.SeatNo); break;
                                    case "科別": report.Worksheets[0].Cells[RowIndex, i].PutValue(student.RefClass == null ? "" : student.RefClass.Department); break;
                                    case "姓名": report.Worksheets[0].Cells[RowIndex, i].PutValue(student.StudentName); break;
                                    case "科目": report.Worksheets[0].Cells[RowIndex, i].PutValue(subjectScore.Subject); break;
                                    case "科目級別": report.Worksheets[0].Cells[RowIndex, i].PutValue(subjectScore.Level); break;
                                    case "學年度": report.Worksheets[0].Cells[RowIndex, i].PutValue(subjectScore.SchoolYear); break;
                                    case "學期": report.Worksheets[0].Cells[RowIndex, i].PutValue(subjectScore.Semester); break;
                                    case "學分數": report.Worksheets[0].Cells[RowIndex, i].PutValue(subjectScore.Credit); break;
                                    case "分項類別": report.Worksheets[0].Cells[RowIndex, i].PutValue(subjectScore.Detail.GetAttribute("開課分項類別")); break;
                                    case "成績年級": report.Worksheets[0].Cells[RowIndex, i].PutValue(subjectScore.GradeYear); break;
                                    case "必選修": report.Worksheets[0].Cells[RowIndex, i].PutValue(subjectScore.Require ? "必修" : "選修"); break;
                                    case "校部訂": report.Worksheets[0].Cells[RowIndex, i].PutValue(subjectScore.Detail.GetAttribute("修課校部訂")); break;
                                    case "科目成績": report.Worksheets[0].Cells[RowIndex, i].PutValue(""+subjectScore.Score); break;
                                    case "原始成績": report.Worksheets[0].Cells[RowIndex, i].PutValue(subjectScore.Detail.GetAttribute("原始成績")); break;
                                    case "補考成績": report.Worksheets[0].Cells[RowIndex, i].PutValue(subjectScore.Detail.GetAttribute("補考成績")); break;
                                    case "重修成績": report.Worksheets[0].Cells[RowIndex, i].PutValue(subjectScore.Detail.GetAttribute("重修成績")); break;
                                    case "擇優採計成績": report.Worksheets[0].Cells[RowIndex, i].PutValue(subjectScore.Detail.GetAttribute("擇優採計成績")); break;
                                    case "學年調整成績": report.Worksheets[0].Cells[RowIndex, i].PutValue(subjectScore.Detail.GetAttribute("學年調整成績")); break;
                                    case "取得學分": report.Worksheets[0].Cells[RowIndex, i].PutValue(subjectScore.Pass ? "是" : "否"); break;
                                    case "不計學分": report.Worksheets[0].Cells[RowIndex, i].PutValue(subjectScore.Detail.GetAttribute("不計學分") == "是" ? "" : ""); break;
                                    case "不需評分": report.Worksheets[0].Cells[RowIndex, i].PutValue(subjectScore.Detail.GetAttribute("不需評分") == "是" ? "" : ""); break;
                                }
                            }
                            #endregion
                        }
                        RowIndex++;
                    }
                    packageProgress += miniProgress;
                    ((BackgroundWorker)sender).ReportProgress((int)(totleProgress + packageProgress), "學期科目成績匯出中...");
                }

                totleProgress += currentProgress;
                ((BackgroundWorker)sender).ReportProgress((int)totleProgress, "學期科目成績匯出中...");
            }
            for (int i = 0; i < fildList.Length; i++)
			{
                report.Worksheets[0].AutoFitColumn(i, 0, 150);
			}
            report.Worksheets[0].FreezePanes(1, 0, 1, fildList.Length);
            e.Result = new object[] {report,fileName,RowIndex>65535 };
        }

        void bkwDataLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            Dictionary<List<SmartSchool.Customization.Data.StudentRecord>, ManualResetEvent> handle = (Dictionary<List<SmartSchool.Customization.Data.StudentRecord>, ManualResetEvent>)((object[])e.Argument)[0];
            foreach (List<SmartSchool.Customization.Data.StudentRecord> splitList in handle.Keys)
            {
                try
                {
                    _Seed.StudentHelper.FillSemesterSubjectScore(true, splitList);
                }
                catch
                { }
                finally
                {
                    handle[splitList].Set();
                }
            }
        }
        #endregion

        private void ExportSemesterScore_Shown(object sender, EventArgs e)
        {
            this.listViewEx1.SuspendLayout();
            this.listViewEx1.ShowGroups = false;
            this.listViewEx1.ShowGroups = true;
            this.listViewEx1.ResumeLayout();
        }

    }
}