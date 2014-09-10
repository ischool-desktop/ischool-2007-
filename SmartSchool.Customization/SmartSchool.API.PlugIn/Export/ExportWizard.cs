using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.API.PlugIn.Import;
using System.Windows.Forms;
using SmartSchool.API.PlugIn.Collections;

namespace SmartSchool.API.PlugIn.Export
{
    public interface ExportWizard
    {
        /// <summary>
        /// 控制面版關閉
        /// </summary>
        event EventHandler ControlPanelClose;

        /// <summary>
        /// 控制面版開啟
        /// </summary>
        event EventHandler ControlPanelOpen;

        /// <summary>
        /// 點擊Help按鈕
        /// </summary>
        event EventHandler HelpButtonClick;
        /// <summary>
        /// 資料匯出
        /// </summary>
        event EventHandler<ExportPackageEventArgs> ExportPackage;

        /// <summary>
        /// 精靈視窗的高度
        /// </summary>
        int Height
        {
            get;
            set;
        }

        /// <summary>
        /// 顯示Help按鈕
        /// </summary>
        bool HelpButtonVisible
        {
            get;
            set;
        }

        /// <summary>
        /// 分割封包的期望值
        /// </summary>
        int PackageLimit
        {
            get;
            set;
        }

        /// <summary>
        /// 精靈視窗的寬度
        /// </summary>
        int Width
        {
            get;
            set;
        }

        /// <summary>
        /// 始用者選取要匯出的欄位
        /// </summary>
        FieldsCollection SelectedFields
        {
            get;
        }

        /// <summary>
        /// 可選擇匯出的欄位
        /// </summary>
        FieldsCollection ExportableFields
        {
            get;
        }

        /// <summary>
        /// 設定項目
        /// </summary>
        OptionCollection Options
        {
            get;
        }
    }
}
