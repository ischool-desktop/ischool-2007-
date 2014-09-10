using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartSchool.StudentRelated.SmallCard
{
    internal class StudentCardCollection
    {
        private List <UCStudentCard> _ShowList;

        private List<UCStudentCard> _HideList;

        private Control _Container;

        private int _MarginLeft;

        private int _ChangingMarginLeft;

        private Padding _ChangingPadding;

        private BackgroundWorker BkwMarginLeft;

        public StudentCardCollection()
        {
            BkwMarginLeft = new BackgroundWorker();
            BkwMarginLeft.WorkerSupportsCancellation = true;
            BkwMarginLeft.WorkerReportsProgress = true;
            BkwMarginLeft.ProgressChanged += new ProgressChangedEventHandler(BkwMarginLeft_ProgressChanged);
            BkwMarginLeft.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BkwMarginLeft_RunWorkerCompleted);
            BkwMarginLeft.DoWork += new DoWorkEventHandler(BkwMarginLeft_DoWork);
            _ShowList = new List<UCStudentCard>();
            _HideList = new List<UCStudentCard>();
        }

        void BkwMarginLeft_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (UCStudentCard card in _ShowList)
            {
                if (BkwMarginLeft.CancellationPending)
                    return;
                BkwMarginLeft.ReportProgress(0, card);
            }
            foreach (UCStudentCard card in _HideList)
            {
                if (BkwMarginLeft.CancellationPending)
                    return;
                BkwMarginLeft.ReportProgress(0, card);
            }
        }

        void BkwMarginLeft_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_ChangingMarginLeft != _MarginLeft)
            {
                startChange();
            }
            else 
            {
                _Container.ResumeLayout();
            }
        }

        void BkwMarginLeft_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Control card = ((Control)e.UserState);
            card.Margin = _ChangingPadding;
            //card.Visible = true;
        }

        public Control Container
        {
            get { return _Container; }
            set { _Container = value; }
        }
        public List<UCStudentCard> ShowList
        {
            get
            {
                return _ShowList;
            }
        }
        public List<UCStudentCard> HideList
        {
            get
            {
                return _HideList;
            }
        }
        public int MarginLeft
        {
            get
            {
                return _MarginLeft;
            }
            set
            {
                if (_MarginLeft != value)
                {
                    _MarginLeft = value;
                    if (BkwMarginLeft.IsBusy)
                    {
                        BkwMarginLeft.CancelAsync();
                    }
                    else
                    {
                        _Container.SuspendLayout();
                        startChange();
                    }
                }
            }
        }

        private void startChange()
        {
            _ChangingMarginLeft = _MarginLeft;
            _ChangingPadding = new System.Windows.Forms.Padding(_MarginLeft, 5, 0, 5);
            BkwMarginLeft.RunWorkerAsync();
        }
    }
}