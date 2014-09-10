using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace SmartSchool.Customization.PlugIn
{
    public class ButtonAdapter
    {
        private Image _Image = null;
        /// <summary>
        /// 照片
        /// </summary>
        public Image Image
        {
            get
            {
                return _Image;
            }
            set
            {
                _Image =value;
            }
        }

        private string _Text;

        /// <summary>
        /// 文字
        /// </summary>
        public string Text { get { return _Text; } set { _Text = value; } }

        private string _Path;
        /// <summary>
        /// 路徑
        /// </summary>
        public string Path { get { return _Path; } set { _Path = value; } }

        /// <summary>
        /// 被按下時的事件
        /// </summary>
        public event EventHandler OnClick;

        /// <summary>
        /// 設定StatusBar上顯示訊息時
        /// </summary>
        public event EventHandler<SetBarMessageEventArgs> OnSetBarMessage;
        
        /// <summary>
        ///  
        /// </summary>
        public void Click()
        {
            if (OnClick != null)
                OnClick.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// 在StatusBar上顯示訊息
        /// </summary>
        /// <param name="message"></param>
        public void SetBarMessage(string message)
        {
            if (OnSetBarMessage != null)
                OnSetBarMessage.Invoke(this, new SetBarMessageEventArgs(message));
        }

        /// <summary>
        /// 在StatusBar上顯示訊息及進度
        /// </summary>
        /// <param name="progress">進度(0-100)</param>
        public void SetBarMessage(string message, int progress)
        {
            if (OnSetBarMessage != null)
                OnSetBarMessage.Invoke(this, new SetBarMessageEventArgs(message,progress));
        }

        /// <summary>
        /// 傳遞要顯示的訊息及進度
        /// </summary>
        public class SetBarMessageEventArgs : EventArgs
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
            public SetBarMessageEventArgs(string message, int progress)
            {
                _Progress = progress;
                _Message = message;
                _HasProgress = true;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="message"></param>
            public SetBarMessageEventArgs(string message)
            {
                _Message = message;
                _HasProgress = false;
            }
        }
    }
}
