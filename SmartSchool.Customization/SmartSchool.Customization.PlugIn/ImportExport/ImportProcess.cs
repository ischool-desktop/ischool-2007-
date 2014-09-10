using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace SmartSchool.Customization.PlugIn.ImportExport
{
    /// <summary>
    /// 匯入資料作業
    /// </summary>
    public class ImportProcess//:ICloneable
    {
        private string _Title;
        private Image _Image;
        private string _Group;
        private int _PackageLimit = 500;
        private HasEventList<string> _ImportableFields;
        private HasEventList<string> _RequiredFields;
        private Control _Configuration = null;


        /// <summary>
        /// 開始驗證
        /// </summary>
        public event EventHandler<BeginValidateEventArgs> BeginValidate;
        /// <summary>
        /// 檢驗資料欄位
        /// </summary>
        public event EventHandler<RowDataValidatedEventArgs> RowDataValidated;
        /// <summary>
        /// 驗證結束
        /// </summary>
        public event EventHandler EndValidate;
        /// <summary>
        /// 開始匯入
        /// </summary>
        public event EventHandler BeginImport;
        /// <summary>
        /// 進行資料匯入
        /// </summary>
        public event EventHandler<DataImportEventArgs> DataImport;
        /// <summary>
        /// 結束匯入
        /// </summary>
        public event EventHandler EndImport;
        /// <summary>
        /// ImportableFields被變更時
        /// </summary>
        public event EventHandler ImportableFieldsChanged;
        /// <summary>
        /// RequiredFields被變更時
        /// </summary>
        public event EventHandler RequiredFieldsChanged;
        /// <summary>
        /// 起始一個匯入作業
        /// </summary>
        public event EventHandler Initialize;

        /// <summary>
        /// 建構子
        /// </summary>
        public ImportProcess()
        {
            _ImportableFields = new HasEventList<string>();
            _ImportableFields.ItemChanged += new EventHandler(_ImportableFields_ItemChanged);
            _RequiredFields = new HasEventList<string>();
            _RequiredFields.ItemChanged += new EventHandler(_RequiredFields_ItemChanged);
            _Image = null;
            _Title = "";
            _Group = "";
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
        public string Group { get{return _Group;}set{_Group=value;}}
        /// <summary>
        /// 分割封包的期望值
        /// </summary>
        public int PackageLimit { get { return _PackageLimit; } set { _PackageLimit = value; } }
        /// <summary>
        /// 允許匯入的欄位
        /// </summary>
        public Collection<string> ImportableFields { get { return _ImportableFields; } }
        /// <summary>
        /// 必需的欄位
        /// </summary>
        public Collection<string> RequiredFields { get { return _RequiredFields; } }
        /// <summary>
        /// 進階設定表單
        /// </summary>
        public Control Configuration { get { return _Configuration; } set { _Configuration = value; } }
        /// <summary>
        /// 開始驗證
        /// </summary>
        public void StartValidate(IEnumerable<string> list)
        {
            BeginValidateEventArgs args = new BeginValidateEventArgs();
                List<string> l = new List<string>();
                foreach ( string var in list )
                {
                    l.Add(var);
                }
                args.List = l.ToArray();
            OnBeginValidate(args);
            if ( BeginValidate != null )
            {
                BeginValidate.Invoke(this, args);
            }
        }
        /// <summary>
        /// 開始驗證資料
        /// </summary>
        /// <param name="args"></param>
        protected virtual  void OnBeginValidate(BeginValidateEventArgs args)
        {
            
        }
        /// <summary>
        /// 驗證資料
        /// </summary>
        public RowDataValidatedEventArgs ValidateRow(RowData data,IEnumerable<string> selectedFields)
        {
            RowDataValidatedEventArgs args = new RowDataValidatedEventArgs();
            args.Data = data;
            foreach ( string var in selectedFields )
            {
                args.SelectFields.Add(var);
            }
            OnValidateRow(args);
            if ( RowDataValidated != null )
                RowDataValidated.Invoke(this, args);
            return args;
        }
        /// <summary>
        /// 驗證資料
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnValidateRow(RowDataValidatedEventArgs args)
        {
            
        }
        /// <summary>
        /// 結束驗證
        /// </summary>
        public void FinishValidate()
        {
            OnEndValidate();
            if ( EndValidate != null )
                EndValidate.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 結束驗證
        /// </summary>
        protected virtual void OnEndValidate()
        {
            
        }
        /// <summary>
        /// 開始匯入
        /// </summary>
        public void StartImport()
        {
            OnBeginImport();
            if ( BeginImport != null )
                BeginImport.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 開始匯入
        /// </summary>
        protected virtual void OnBeginImport()
        {
        }
        /// <summary>
        /// 匯入資料
        /// </summary>
        public void ImportPackage(IEnumerable<RowData> items,IEnumerable<string> fields)
        {
                DataImportEventArgs args = new DataImportEventArgs();
                foreach ( RowData var in items )
                {
                    args.Items.Add(var);
                }
                foreach ( string var in fields )
                {
                    args.ImportFields.Add(var);
               }
               OnDataImport(args);
            if ( DataImport != null )
            {
               DataImport.Invoke(this, args);
            }
        }
        /// <summary>
        /// 匯入資料
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnDataImport(DataImportEventArgs args)
        {
            
        }
        /// <summary>
        /// 結束匯入
        /// </summary>
        public void FinishImport()
        {
            OnEndImport();
            if ( EndImport != null )
                EndImport.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 結束匯入
        /// </summary>
        protected virtual void OnEndImport()
        {

        }
        ///// <summary>
        ///// 建立新的執行個體
        ///// </summary>
        //public object Clone()
        //{
        //    if ( Initiation != null )
        //        Initiation.Invoke(this, new EventArgs());
        //    ImportProcess p = new ImportProcess();
        //    p.Title = this.Title;
        //    foreach ( string var in this.RequiredFields )
        //    {
        //        p.RequiredFields.Add(var);
        //    }
        //    p.PackageLimit = this.PackageLimit;
        //    foreach ( string var in this.ImportableFields )
        //    {
        //        p.ImportableFields.Add(var);
        //    }
        //    p.Image = this.Image;
        //    p.Group = this.Group;
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

        private bool _Syncing=false;
        private void _ImportableFields_ItemChanged(object sender, EventArgs e)
        {
            #region 從_RequiredFields刪掉已不存在的欄位
            if ( !_Syncing )
            {
                _Syncing = true;
                List<string> removelist = new List<string>();
                foreach ( string var in _RequiredFields )
                {
                    if ( !_ImportableFields.Contains(var) )
                        removelist.Add(var);
                }
                foreach ( string var in removelist )
                {
                    _RequiredFields.Remove(var);
                }
                _Syncing = false;
            }
            #endregion

            if ( ImportableFieldsChanged != null )
                ImportableFieldsChanged.Invoke(this, new EventArgs());
        }
        private void _RequiredFields_ItemChanged(object sender, EventArgs e)
        {
            #region 從_RequiredFields加入_ImportableFields不存在的欄位
            if ( !_Syncing )
            {
                _Syncing = true;
                List<string> insertlist = new List<string>();
                foreach ( string var in _RequiredFields )
                {
                    if ( !_ImportableFields.Contains(var) )
                        insertlist.Add(var);
                }
                foreach ( string var in insertlist )
                {
                    _ImportableFields.Add(var);
                }
                _Syncing = false;
            }
            #endregion

            if ( RequiredFieldsChanged != null )
                RequiredFieldsChanged.Invoke(this, new EventArgs());
        }

    }
}
