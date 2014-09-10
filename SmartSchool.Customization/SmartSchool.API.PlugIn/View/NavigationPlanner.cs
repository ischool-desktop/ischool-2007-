using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SmartSchool.API.PlugIn.View
{
    /// <summary>
    /// 將大量資料以結構化方式顯示並供選取子集合
    /// </summary>
    public abstract class NavigationPlanner
    {
        private string _Text = "";
        private string _Description = "";
        private bool _NeedReflash = false;
        private Collection<string> _SelectedSource = new Collection<string>();
        /// <summary>
        /// 名稱
        /// </summary>
        public virtual string Text { get { return _Text; } protected set { _Text = value; } }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get { return _Description; } protected set { _Description = value; } }
        /// <summary>
        /// 資料需要被重新整理
        /// </summary>
        public bool NeedReflash { get { return _NeedReflash; } }
        /// <summary>
        /// 顯示的控制項
        /// </summary>
        public abstract Control DisplayControl { get; }
        /// <summary>
        /// 傳入經過篩選符合條件的資料進行結構化顯示
        /// </summary>
        /// <param name="source">經過篩選符合條件的資料集</param>
        public void Perform(List<string> source)
        {
            Layout(source);
            _NeedReflash = false;
        }
        /// <summary>
        /// 要求重新整理資料
        /// </summary>
        protected void AskReflash()
        {
            _NeedReflash = true;
        }
        /// <summary>
        /// 將資料集合顯示在控制項中
        /// </summary>
        /// <param name="source">資料集合</param>
        protected abstract void Layout(List<string> source);
        /// <summary>
        /// 被選取的子集合
        /// </summary>
        public Collection<string> SelectedSource { get { return _SelectedSource; } }
    }
}
