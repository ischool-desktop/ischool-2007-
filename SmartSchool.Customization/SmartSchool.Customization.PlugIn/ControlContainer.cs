using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SmartSchool.Customization.PlugIn
{
    /// <summary>
    /// 控制項容器
    /// </summary>
    public class ControlContainer
    {
        private Control _Control = null;
        private string _Path = "";
        /// <summary>
        /// 建構子
        /// </summary>
        public ControlContainer()
        { }
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="control">控制項</param>
        public ControlContainer(Control control)
        { 
            _Control=control;
        }
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="control">控制項</param>
        /// <param name="path">路徑</param>
        public ControlContainer(Control control, string path)
        {
            _Control = control;
            _Path = path;
        }

        /// <summary>
        /// 控制項
        /// </summary>
        public Control Control
        {
            get { return _Control; }
            set { _Control = value; }
        }
        /// <summary>
        /// 路徑
        /// </summary>
        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }
    }
}
