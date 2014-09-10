using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.API.PlugIn
{
    public class VirtualButton:VirtualItem
    {
        private System.Drawing.Image _Image;

        private string _Path;

        /// <summary>
        /// 發生於Image屬性變更時。
        /// </summary>
        public event EventHandler ImageChanged;

        /// <summary>
        /// 發生於Path屬性變更時。
        /// </summary>
        public event EventHandler PathChanged;

        /// <summary>
        /// 取得或設定Item上顯示的影像。
        /// </summary>
        public virtual System.Drawing.Image Image
        {
            get { return _Image; }
            set { _Image = value; if ( ImageChanged != null )ImageChanged(this, new EventArgs()); }
        }

        /// <summary>
        /// 取得或設定Item的路徑。
        /// </summary>
        public virtual string Path
        {
            get { return _Path; }
            set { _Path = value; if ( PathChanged != null )PathChanged(this, new EventArgs()); }
        }
    }
}
