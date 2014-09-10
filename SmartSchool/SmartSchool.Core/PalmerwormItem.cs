using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using IntelliSchool.DSA30.Util;

namespace SmartSchool
{
    public partial class PalmerwormItem : UserControl, SmartSchool.Customization.PlugIn.ExtendedContent.IContentItem
    {
        protected bool _SaveButtonVisible;
        private string _Title;
        protected DataValueManager _valueManager;
        protected BackgroundWorker _bgWorker;
        private string _runningid;
        private bool _show_waiting = true;

        private string _waitingid;

        public PalmerwormItem()
        {
            Font = SmartSchool.Common.FontStyles.General;
            AutoScaleMode = AutoScaleMode.None;

            InitializeComponent();
            _valueManager = new DataValueManager();
            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
            _SaveButtonVisible = false;
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ( this.Disposing ) return;
            if (_runningid != _waitingid)
            {
                _runningid = _waitingid;
                _bgWorker.RunWorkerAsync();

            }
            else
            {
                this.Show();
                OnBackgroundWorkerCompleted(e.Result);
                this.picWaiting.Hide();
            }
        }

        protected bool ShowWaitingIcon
        {
            get { return _show_waiting; }
            set { _show_waiting = value; }
        }

        protected virtual void OnBackgroundWorkerCompleted(object result)
        {
            //throw new Exception("必須覆寫OnBackgroundWorkerCompleted方法");
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = OnBackgroundWorkerWorking();
        }

        protected virtual object OnBackgroundWorkerWorking()
        {
            throw new Exception("必須覆寫OnBackgroundWorkerWorking方法.");
        }

        #region IContentItem 成員

        public bool SaveButtonVisible
        {
            get
            {
                return _valueManager.IsDirty;
            }
            set
            {
                if (!value)
                {
                    _valueManager.MakeDirtyToClean();
                    RaiseEvent();
                }
            }
        }

        protected void OnValueChanged(string name, string value)
        {
            _valueManager.SetValue(name, value);
            RaiseEvent();
        }

        protected void RaiseEvent()
        {
            if (_valueManager.IsDirty != _SaveButtonVisible )
            {
                _SaveButtonVisible = _valueManager.IsDirty;
                if ( this.SaveButtonVisibleChanged != null )
                {
                    SaveButtonVisibleChanged.Invoke(this, new EventArgs());
                }
                if ( this.CancelButtonVisibleChanged!= null )
                {
                    CancelButtonVisibleChanged.Invoke(this, new EventArgs());
                }
            }
        }

        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }

        public string RunningID
        {
            get { return _runningid; }
            set { _runningid = value; }
        }

        public Control DisplayControl
        {
            get { return this; }
        }

        public virtual void LoadContent(string id)
        {
            if ( this.Disposing ) return;
            _valueManager.ResetValues();
            RaiseEvent();
            _waitingid = id;

            if (!_bgWorker.IsBusy)
            {
                _runningid = _waitingid;

                //if (!(this is TagBar)) //如果是 TagBar 就不要穩藏， TagBar 自行控制顯示穩藏。
                //    this.Hide();

                if (ShowWaitingIcon)
                    this.picWaiting.Show();

                if (this.Parent != null && !this.Parent.Controls.Contains(this.picWaiting))
                {
                    this.Parent.Controls.Add(this.picWaiting);
                    this.Parent.Controls.SetChildIndex(picWaiting, 0);
                    int x = (this.Parent.Width - picWaiting.Width) / 2;
                    int y = (this.Parent.Height - picWaiting.Height) / 2;

                    this.picWaiting.Location = new Point(x, y);
                }
                _bgWorker.RunWorkerAsync();
            }
        }

        public virtual void Save()
        {
        }

        public virtual void Undo()
        {
            if (!_bgWorker.IsBusy)
                LoadContent(_waitingid);
        }

        public event EventHandler SaveButtonVisibleChanged;

        public bool CancelButtonVisible
        {
            get { return _SaveButtonVisible; }
        }

        public event EventHandler CancelButtonVisibleChanged;

        #endregion

        #region ICloneable 成員

        public virtual object Clone()
        {
            throw new Exception("必須實做Clone");
        }

        #endregion
    }
}
