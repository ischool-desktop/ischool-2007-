using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using System.Xml;
using System.ComponentModel;
using System.Threading;

namespace SmartSchool.Payment.GT
{
    internal delegate void LoaderCallback(ExecuteResult result);

    internal delegate object LoaderExecutor(Arguments args);

    internal class Arguments : Dictionary<string, object>
    {
    }

    #region Class ExecuteResult
    internal class ExecuteResult
    {
        /// <summary>
        /// 使用此建構式，代表執行結果不正確。
        /// </summary>
        public ExecuteResult(Exception error)
        {
            _success = false;
            _error = error;
        }

        /// <summary>
        /// 使用此建構式，代表執行結果正確。
        /// </summary>
        /// <param name="result"></param>
        public ExecuteResult(object result)
        {
            _success = true;
            _result = result;
        }

        private bool _success;
        public bool Success
        {
            get { return _success; }
        }

        private object _result;
        public object Result
        {
            get { return _result; }
        }

        private Exception _error;
        public Exception Error
        {
            get { return _error; }
        }
    }
    #endregion

    #region Class LoaderState
    internal class LoaderState
    {
        private LoaderCallback _callback;
        public LoaderCallback Callback
        {
            get { return _callback; }
            set { _callback = value; }
        }

        private LoaderExecutor _executor;
        public LoaderExecutor Executor
        {
            get { return _executor; }
            set { _executor = value; }
        }

        private Arguments _args;
        public Arguments Args
        {
            get { return _args; }
            set { _args = value; }
        }

        private object _result;
        public object Result
        {
            get { return _result; }
            set { _result = value; }
        }

        private Exception _error;
        public Exception Error
        {
            get { return _error; }
            set { _error = value; }
        }
    }
    #endregion

    internal class AsyncDataLoader
    {
        public void Execute(LoaderExecutor executor, LoaderCallback callback, Arguments args)
        {
            LoaderState state = new LoaderState();
            state.Executor = executor; //真正執行取得資料的 Method。
            state.Callback = callback; //當完成取得資料時，呼叫的 Method。
            state.Args = args; //執行取得資料時所需要的參數

            if (BGWorker == null) CreateNewWorker();

            if (BGWorker.IsBusy)
            {
                BGWorker.CancelAsync(); //取消前一個 Worker 的工作。
                CreateNewWorker();    //建立新 Worker。
            }

            BGWorker.RunWorkerAsync(state);
        }

        public void Wait()
        {
            WaitHandler.WaitOne();
        }

        #region Properties
        private BackgroundWorker _worker;
        private BackgroundWorker BGWorker
        {
            get { return _worker; }
            set { _worker = value; }
        }

        private ManualResetEvent _wait_handler = new ManualResetEvent(true);
        /// <summary>
        /// 此 Handler 只同步第二執行緒的作業。(只同步 LoaderExecutor 中的作業)
        /// </summary>
        private ManualResetEvent WaitHandler
        {
            get { return _wait_handler; }
        }

        #endregion

        private void CreateNewWorker()
        {
            BGWorker = new BackgroundWorker();
            BGWorker.WorkerSupportsCancellation = true;
            BGWorker.DoWork += new DoWorkEventHandler(BGWorker_DoWork);
            BGWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGWorker_RunWorkerCompleted);
        }

        private void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            WaitHandler.Reset();

            LoaderState pll = e.Argument as LoaderState;
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {
                pll.Result = pll.Executor(pll.Args);
            }
            catch (Exception ex)
            {
                pll.Error = ex;
            }

            if (worker.CancellationPending)
                e.Cancel = true;

            e.Result = pll;

            WaitHandler.Set();
        }

        private void BGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                return;

            LoaderState pll = e.Result as LoaderState;

            ExecuteResult eresult;
            if (pll.Error == null) //沒有錯誤時。
                eresult = new ExecuteResult(pll.Result);
            else //有錯誤時。
                eresult = new ExecuteResult(pll.Error);

            pll.Callback(eresult);
        }
    }
}
