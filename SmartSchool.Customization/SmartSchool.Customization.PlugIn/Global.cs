using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.PlugIn
{
    /// <summary>
    /// 全域功能
    /// </summary>
    public static class Global
    {
        /// <summary>
        /// 設定StatusBar上顯示訊息時
        /// </summary>
        public static event EventHandler<SetStatusBarMessageEventArgs> OnSetStatusBarMessage;
        /// <summary>
        /// 在StatusBar上顯示訊息
        /// </summary>
        /// <param name="message">訊息</param>
        public static void SetStatusBarMessage(string message)
        {
            if ( OnSetStatusBarMessage != null )
                OnSetStatusBarMessage.Invoke(null, new SetStatusBarMessageEventArgs(message));
        }
        /// <summary>
        /// 在StatusBar上顯示訊息及進度
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="progress">進度(0-100)</param>
        public static void SetStatusBarMessage(string message, int progress)
        {
            if ( OnSetStatusBarMessage != null )
                OnSetStatusBarMessage.Invoke(null, new SetStatusBarMessageEventArgs(message, progress));
        }
        /// <summary>
        /// 傳遞要顯示的訊息及進度
        /// </summary>
        public class SetStatusBarMessageEventArgs : EventArgs
        {
            private string _Message;
            private int _Progress;
            private bool _HasProgress;
            /// <summary>
            /// 訊息
            /// </summary>
            public string Message { get { return _Message; } }
            /// <summary>
            /// 進度
            /// </summary>
            public int Progress { get { return _Progress; } }
            /// <summary>
            /// 有進度
            /// </summary>
            public bool HasProgress { get { return _HasProgress; } }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="message"></param>
            /// <param name="progress"></param>
            public SetStatusBarMessageEventArgs(string message, int progress)
            {
                _Progress = progress;
                _Message = message;
                _HasProgress = true;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="message"></param>
            public SetStatusBarMessageEventArgs(string message)
            {
                _Message = message;
                _HasProgress = false;
            }
        }
    }
}
