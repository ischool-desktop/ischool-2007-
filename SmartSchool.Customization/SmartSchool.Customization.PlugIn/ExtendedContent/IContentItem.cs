using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SmartSchool.Customization.PlugIn.ExtendedContent
{
    /// <summary>
    /// 延伸資料的管理畫面
    /// </summary>
    public interface IContentItem:ICloneable
    {
        /// <summary>
        /// 儲存按鈕Visible
        /// </summary>
        bool SaveButtonVisible{ get;}
        /// <summary>
        /// 取消按鈕Visible
        /// </summary>
        bool CancelButtonVisible { get;}
        /// <summary>
        /// 標題
        /// </summary>
        string Title { get;}
        /// <summary>
        /// 管理畫面的控制項(Panel)
        /// </summary>
        Control DisplayControl { get;}
        /// <summary>
        /// 讀取資料
        /// </summary>
        /// <param name="id">主資料的系統編號</param>
        void LoadContent(string id);
        /// <summary>
        /// 儲存
        /// </summary>
        void Save();
        /// <summary>
        /// 取消變更
        /// </summary>
        void Undo();
        /// <summary>
        /// SaveButtonVisible屬性改變時
        /// </summary>
        event EventHandler SaveButtonVisibleChanged;
        /// <summary>
        /// CancelButtonVisible屬性改變時
        /// </summary>
        event EventHandler CancelButtonVisibleChanged;
    }
}
