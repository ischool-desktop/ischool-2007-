using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.CourseRelated;
using IntelliSchool.DSA30.Util;
using SmartSchool;
using System.Threading;

namespace SmartSchool.CourseRelated.RibbonBars.ScoresCalc.Forms
{
    public partial class CalculateWizard : BaseForm, IProgressUI
    {
        private BackgroundWorker _calc_worker, _export_worker;
        private CourseDataLoader _raw_data;

        public CalculateWizard()
        {
            InitializeComponent();
            InitializeBackgroundWorker();

            lblTitle.Font = new Font(FontStyles.GeneralFontFamily, 20);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _raw_data = new CourseDataLoader();
            _raw_data.LoadCalculationData(this);

            CourseScoreCalculate calculate = new CourseScoreCalculate(_raw_data.Courses);
            calculate.Calculate();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (_raw_data == null) return;

            if (e.UserState != null)
                lblCourseCount.Text = string.Format("<font color=\"#0A11FF\">建立課程總覽資料，請稍後...(進度：{0}/4)\n", _raw_data.CurrentStep) + e.UserState.ToString() + "</font>";
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Cancellation) return;

            if (e.Error == null)
            {
                btnExport.Enabled = true;
                btnCalcuate.Enabled = true;

                CourseSummaryCalculate summary = new CourseSummaryCalculate(_raw_data.Courses);
                summary.Calculate();
                lblCourseCount.Text = summary.Message();
            }
            else
                MsgBox.Show("取得課程成績相關資料錯誤。", Application.ProductName);
        }

        private void ExportWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            LackScoreExporter exporter = new LackScoreExporter(_raw_data.Courses, new BarMessageProgress(this));
            exporter.Export();
        }

        private void ExportWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                MsgBox.Show(e.Error.Message, Application.ProductName);

            btnExport.Enabled = true;
        }

        private void CalculateWizard_Load(object sender, EventArgs e)
        {
            lblCourseCount.Text = "建立課程總覽資料，請稍後...";
            btnExport.Enabled = false;
            btnCalcuate.Enabled = false;
            _calc_worker.RunWorkerAsync();
        }

        private void btnCalcuate_Click(object sender, EventArgs e)
        {
            CalculateResult result = new CalculateResult(_raw_data.Courses);
            result.ShowDialog();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            _export_worker.RunWorkerAsync();
            btnExport.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Cancel();
        }

        #region IProgressUI 成員

        public void ReportProgress(string message, int progress)
        {
            _calc_worker.ReportProgress(progress, message);
        }

        public void Cancel()
        {
            _calc_worker.CancelAsync();
        }

        public bool Cancellation
        {
            get { return _calc_worker.CancellationPending; }
        }

        #endregion

        private void InitializeBackgroundWorker()
        {
            _calc_worker = new BackgroundWorker();
            _calc_worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            _calc_worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_RunWorkerCompleted);
            _calc_worker.ProgressChanged += new ProgressChangedEventHandler(Worker_ProgressChanged);
            _calc_worker.WorkerReportsProgress = true;
            _calc_worker.WorkerSupportsCancellation = true;

            _export_worker = new BackgroundWorker();
            _export_worker.DoWork += new DoWorkEventHandler(ExportWorker_DoWork);
            _export_worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ExportWorker_RunWorkerCompleted);
        }

        class BarMessageProgress : IProgressUI
        {
            private Form _frm;

            public BarMessageProgress(Form frm)
            {
                _frm = frm;
            }

            #region IProgressUI 成員

            private delegate void RaiseProgress(string message, int progress);

            public void ReportProgress(string message, int progress)
            {
                if (!string.IsNullOrEmpty(message))
                    MotherForm.Instance.SetBarMessage(message);
                else
                {
                    if (_frm.InvokeRequired)
                        _frm.Invoke(new RaiseProgress(ReportProgress), message, progress);
                    else
                    {
                        MotherForm.Instance.SetBarMessage(message, progress);
                        Application.DoEvents();
                    }
                }
            }

            public void Cancel()
            {
            }

            public bool Cancellation
            {
                get { return false; }
            }

            #endregion
        }
    }
}