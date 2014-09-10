using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SmartSchool.API.PlugIn.Collections;

namespace SmartSchool.API.PlugIn.Import
{
    /// <summary>
    /// 匯入流程
    /// </summary>
    public interface  ImportWizard
    {

        /// <summary>
        /// 開啟匯入檔案
        /// </summary>
        event EventHandler<LoadSourceEventArgs> LoadSource;

        /// <summary>
        /// 點擊Help按鈕
        /// </summary>
        event EventHandler HelpButtonClick;

        /// <summary>
        /// 開始匯入
        /// </summary>
        event System.EventHandler ImportStart;

        /// <summary>
        /// 開始驗證
        /// </summary>
        event System.EventHandler<SmartSchool.API.PlugIn.Import.ValidateStartEventArgs> ValidateStart;

        /// <summary>
        /// 進行資料匯入
        /// </summary>
        event System.EventHandler<SmartSchool.API.PlugIn.Import.ImportPackageEventArgs> ImportPackage;

        /// <summary>
        /// 結束匯入
        /// </summary>
        event System.EventHandler ImportComplete;

        /// <summary>
        /// 驗證結束
        /// </summary>
        event System.EventHandler ValidateComplete;

        /// <summary>
        /// 檢驗資料欄位
        /// </summary>
        event System.EventHandler<SmartSchool.API.PlugIn.Import.ValidateRowEventArgs> ValidateRow;

        /// <summary>
        /// 控制面版開啟
        /// </summary>
        event EventHandler ControlPanelOpen;

        /// <summary>
        /// 控制面版關閉
        /// </summary>
        event EventHandler ControlPanelClose;

        /// <summary>
        /// 必需的欄位(通常為匯入資料的索引欄位)
        /// </summary>
        FieldsCollection RequiredFields
        {
            get;
        }

        /// <summary>
        /// 允許匯入的欄位
        /// </summary>
        FieldsCollection ImportableFields
        {
            get;
        }

        /// <summary>
        /// 始用者選取要匯入的欄位
        /// </summary>
        FieldsCollection SelectedFields
        {
            get;
        }

        /// <summary>
        /// 精靈視窗的高度
        /// </summary>
        int Height
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
        /// 分割封包的期望值
        /// </summary>
        /// <remarks>
        /// 系統將會自動依此期望值做分割資料進行匯入，
        /// 分割時會將屬於同一人(班、課程)之資料分至同一Package，
        /// 所以若期望值為300而匯入資料中有500比資料屬於同一人時，
        /// 則將有一封包會超過500筆資料。
        /// </remarks>
        int PackageLimit
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
        /// 設定項目
        /// </summary>
        OptionCollection Options
        {
            get;
        }

        /// <summary>
        /// 新增警告
        /// </summary>
        /// <param name="rowData">警告的資料行</param>
        /// <param name="message">警告內容</param>
        void AddWarning(SmartSchool.API.PlugIn.RowData rowData, string message);

        /// <summary>
        /// 新增錯誤
        /// </summary>
        /// <param name="rowData">錯誤的資料行</param>
        /// <param name="message">錯誤內容</param>
        void AddError(SmartSchool.API.PlugIn.RowData rowData, string message);
    }

    public class LoadSourceEventArgs : EventArgs
    {
        private List<string> _Fields = new List<string>();
        /// <summary>
        /// 資料來源所包含的所有欄位名稱
        /// </summary>
        public List<string> Fields { get { return _Fields; } }
    }
}
