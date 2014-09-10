using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace SmartSchool.Customization.PlugIn.ImportExport
{
    /// <summary>
    /// 匯出資料作業
    /// </summary>
    public class ExportProcess //: ICloneable
    {
        private string _Title = "";
        private Image _Image= null;
        private string _Group = "";
        private int _PackageLimit = 200;
        private HasEventList<string> _ExportableFields = new HasEventList<string>();
        private Control _Configuration = null;
        /// <summary>
        /// 資料匯出
        /// </summary>
        public event EventHandler<ExportPackageEventArgs> ExportPackage;
        /// <summary>
        /// 可匯出欄未變更
        /// </summary>
        public event EventHandler ExportableFieldsChanged;
        /// <summary>
        /// 起始一個匯出作業
        /// </summary>
        public event EventHandler Initialize;

        /// <summary>
        /// 建構子
        /// </summary>
        public ExportProcess()
        {
            _ExportableFields.ItemChanged += new EventHandler(_ExportableFields_ItemChanged);
        }
        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get { return _Title; } set { _Title = value; } }
        /// <summary>
        /// 圖示
        /// </summary>
        public Image Image { get { return _Image; } set { _Image = value; } }
        /// <summary>
        /// 群組
        /// </summary>
        public string Group { get { return _Group; } set { _Group = value; } }
        /// <summary>
        /// 分割封包的期望值
        /// </summary>
        public int PackageLimit { get { return _PackageLimit; } set { _PackageLimit = value; } }
        /// <summary>
        /// 允許匯入的欄位
        /// </summary>
        public Collection<string> ExportableFields { get { return _ExportableFields; } }
        /// <summary>
        /// 進階設定表單
        /// </summary>
        public Control Configuration { get { return _Configuration; } set { _Configuration = value; } }
        /// <summary>
        /// 填入這個Package的RowData
        /// </summary>
        public List<RowData> GetExportData(IEnumerable<string> idInPackage, IEnumerable<string> selectedFields)
        {
            if ( ExportPackage != null )
            {
                ExportPackageEventArgs args = new ExportPackageEventArgs();
                foreach ( string var in idInPackage )
                {
                    args.List.Add(var);
                }
                foreach ( string var in selectedFields )
                {
                    if ( _ExportableFields.Contains(var) )
                        args.ExportFields.Add(var);
                }
                ExportPackage.Invoke(this, args);
                return args.Items;
            }
            else
                return new List<RowData>();
        }
        ///// <summary>
        ///// 建立新的執行個體
        ///// </summary>
        //public object Clone()
        //{
        //    if ( Initiation != null )
        //        Initiation.Invoke(this, new EventArgs());
        //    ExportProcess p = new ExportProcess();
        //    p.Title = this.Title;
        //    p.PackageLimit = this.PackageLimit;
        //    p.Image = this.Image;
        //    p.Group = this.Group;
        //    foreach ( string var in this.ExportableFields )
        //    {
        //        p.ExportableFields.Add(var);
        //    }
        //    p.Configuration = this.Configuration;
        //    return p;
        //}
        /// <summary>
        /// 
        /// </summary>
        public void StartupProcess()
        {
            if ( Initialize != null )
                Initialize.Invoke(this, new EventArgs());
        }

        private void _ExportableFields_ItemChanged(object sender, EventArgs e)
        {
            if ( ExportableFieldsChanged != null )
                ExportableFieldsChanged.Invoke(this, new EventArgs());
        }
    }
}
