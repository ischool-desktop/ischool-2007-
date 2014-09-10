using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.StudentRelated.RibbonBars.Import;
using SmartSchool.StudentRelated.RibbonBars.Import;
using DevComponents.DotNetBar;
using SmartSchool.Common;
using SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.Connector;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler;
using SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler;
using SmartSchool.StudentRelated;
using System.Xml;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler.Formater;
using SmartSchool.Customization.Data;
using Aspose.Cells;
using System.Threading;
using System.IO;
using SmartSchool;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.PlugIn.ImportExport;
using SmartSchool.AccessControl;
using SmartSchool.StudentRelated.RibbonBars;
using SmartSchool.Security;

namespace SmartSchool.StudentRelated.RibbonBars.Import
{
    public partial class ImportExport : RibbonBarBase, IManager<ImportProcess>, IManager<ExportProcess>
    {
        FeatureAccessControl exportStudentCtrl;
        FeatureAccessControl importStudentCtrl;
        FeatureAccessControl importPhotoCtrl;

        private DevComponents.DotNetBar.GalleryGroup _StudentRecord;
        private DevComponents.DotNetBar.GalleryGroup _SemesterSubjectScore;
        private DevComponents.DotNetBar.GalleryGroup _Photo;
        private Dictionary<string, GalleryGroup> _Group = new Dictionary<string, GalleryGroup>();
        private ButtonAdapterPlugInManager _ImportButtonManager;
        private ButtonAdapterPlugInManager _ExportButtonManager;
        private Dictionary<SmartSchool.API.PlugIn.Import.Importer, ButtonAdapter> _ImportAdapter = new Dictionary<SmartSchool.API.PlugIn.Import.Importer, ButtonAdapter>();
        private Dictionary<SmartSchool.API.PlugIn.Export.Exporter, ButtonAdapter> _ExportAdapter = new Dictionary<SmartSchool.API.PlugIn.Export.Exporter, ButtonAdapter>();

        public ImportExport()
        {
            InitializeComponent();
            superTooltip1.DefaultFont = FontStyles.General;

            #region 設定群組
            _StudentRecord = new GalleryGroup();
            _SemesterSubjectScore = new GalleryGroup();
            _Photo = new GalleryGroup();
            // 
            // _StudentRecord
            // 
            _StudentRecord.Name = "學籍基本資料";
            _StudentRecord.Text = "<b>學籍基本資料</b>";
            // 
            // _SemesterSubjectScore
            // 
            _SemesterSubjectScore.DisplayOrder = 1;
            _SemesterSubjectScore.Name = "學期科目成績";
            _SemesterSubjectScore.Text = "<b>學期科目成績</b>";
            // 
            // _Photo
            // 
            _Photo.DisplayOrder = int.MaxValue;
            _Photo.Name = "照片";
            _Photo.Text = "<b>照片</b>";

            this.galleryContainer1.GalleryGroups.AddRange(new DevComponents.DotNetBar.GalleryGroup[] {
            _StudentRecord,
            _SemesterSubjectScore,_Photo});

            _Group.Add("學籍基本資料", _StudentRecord);

            _Group.Add("學期科目成績", _SemesterSubjectScore);

            _Group.Add("照片", _Photo);

            // 2008/4/9
            //this.galleryContainer1.SetGalleryGroup(this.btnExport, _StudentRecord);
            //this.galleryContainer1.SetGalleryGroup(this.btnImport, _StudentRecord); ;
            //this.galleryContainer1.SetGalleryGroup(this.buttonItem1, _Photo);

            #endregion

            SmartSchool.Customization.PlugIn.ImportExport.ImportStudent.SetManager(this);
            SmartSchool.Customization.PlugIn.ImportExport.ExportStudent.SetManager(this);

            #region 權限
            //權限判斷 - 匯出學籍資料	Button0130
            exportStudentCtrl = new FeatureAccessControl("Button0130");
            //權限判斷 - 匯入學籍資料	Button0210
            importStudentCtrl = new FeatureAccessControl("Button0210");
            //權限判斷 - 匯入照片	Button0290
            importPhotoCtrl = new FeatureAccessControl("Button0290");
            #endregion

            exportStudentCtrl.Inspect(btnExport);
            importStudentCtrl.Inspect(btnImport);
            importPhotoCtrl.Inspect(btnImportPhoto);

            _ImportButtonManager = new ButtonAdapterPlugInManager(btnImportList);
            _ExportButtonManager = new ButtonAdapterPlugInManager(btnExportList);
            foreach ( SmartSchool.API.PlugIn.Import.Importer var in API.PlugIn.PlugInManager.Student.Importers )
            {
                AddImport(var);
            }
            API.PlugIn.PlugInManager.Student.Importers.ItemAdded += delegate(object sender, SmartSchool.API.PlugIn.ItemEventArgs<SmartSchool.API.PlugIn.Import.Importer> ea)
            {
                AddImport(ea.Item);
            };
            API.PlugIn.PlugInManager.Student.Importers.ItemRemoved += delegate(object sender, SmartSchool.API.PlugIn.ItemEventArgs<SmartSchool.API.PlugIn.Import.Importer> ea)
            {
                if ( _ImportAdapter.ContainsKey(ea.Item) )
                {
                    _ImportAdapter.Remove(ea.Item);
                    _ImportButtonManager.Remove(_ImportAdapter[ea.Item]);
                }
            };
            foreach ( SmartSchool.API.PlugIn.Export.Exporter var in API.PlugIn.PlugInManager.Student.Exporters )
            {
                AddExport(var);
            }
            API.PlugIn.PlugInManager.Student.Exporters.ItemAdded += delegate(object sender, SmartSchool.API.PlugIn.ItemEventArgs<SmartSchool.API.PlugIn.Export.Exporter> ea)
            {
                AddExport(ea.Item);
            };
            API.PlugIn.PlugInManager.Student.Exporters.ItemRemoved += delegate(object sender, SmartSchool.API.PlugIn.ItemEventArgs<SmartSchool.API.PlugIn.Export.Exporter> ea)
            {
                if ( _ExportAdapter.ContainsKey(ea.Item) )
                {
                    _ExportAdapter.Remove(ea.Item);
                    _ExportButtonManager.Remove(_ExportAdapter[ea.Item]);
                }
            };
        }
        
        private void AddImport(SmartSchool.API.PlugIn.Import.Importer importer)
        {
            
            ButtonAdapter btn = new ButtonAdapter();
            btn.Image = importer.Image;
            btn.Path = importer.Path;
            btn.Text = importer.Text;
            btn.OnClick += delegate(object sender, EventArgs e)
            {
                Import.ImportStudentV2 wizard = new ImportStudentV2(importer.Text, importer.Image);
                importer.InitializeImport(wizard);
                wizard.ShowDialog();
            };

            if (!Attribute.IsDefined(importer.GetType(), typeof(FeatureCodeAttribute)) || CurrentUser.Acl[importer.GetType()].Executable)
            {
                _ImportAdapter.Add(importer, btn);
                _ImportButtonManager.Add(btn);
            }
        }

        private void AddExport(SmartSchool.API.PlugIn.Export.Exporter exporter)
        {
            ButtonAdapter btn = new ButtonAdapter();
            btn.Image = exporter.Image;
            btn.Path = exporter.Path;
            btn.Text = exporter.Text;
            btn.OnClick += delegate(object sender, EventArgs e)
            {
                //ButtonAdapter btn = (ButtonAdapter)sender;
                //foreach ( SmartSchool.API.PlugIn.Import.Importer var in _ExportAdapter.Keys )
                //{
                //    if ( _ExportAdapter[var] == btn )
                //    {
                //MessageBox.Show(exporter.Text);
                ExportStudentV2 wizard=new ExportStudentV2(exporter.Text,exporter.Image);
                exporter.InitializeExport(wizard);
                wizard.ShowDialog();
                //        break;
                //    }
                //}
            };

            if (!Attribute.IsDefined(exporter.GetType(), typeof(FeatureCodeAttribute)) || CurrentUser.Acl[exporter.GetType()].Executable)
            {
                _ExportAdapter.Add(exporter, btn);
                _ExportButtonManager.Add(btn);
            }
        }

        public override string ProcessTabName
        {
            get
            {
                return "學生";
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            StudentImportWizard wizard = new StudentImportWizard();
            wizard.ShowDialog();
        }

        private void MainRibbonBar_ItemClick(object sender, EventArgs e)
        {
        }

        private void buttonItem88_Click(object sender, EventArgs e)
        {
            //MsgBox.Show("開發中功能。"); 
            ExportWizard export = new ExportWizard();
            export.ShowDialog();
        }

        #region IManager<ImportProcess> 成員
        public void Add(ImportProcess instance)
        {
            ButtonItem importItem = new ButtonItem();
            //importItem.FixedSize = new System.Drawing.Size(120, 35);
            importItem.ImagePaddingHorizontal = 0;
            importItem.ImagePaddingVertical = 0;
            if (instance.Image != null)
            {
                importItem.Image = instance.Image;
                importItem.ImageFixedSize = new System.Drawing.Size(16, 16);
                importItem.ImagePosition = DevComponents.DotNetBar.eImagePosition.Left;
                importItem.RibbonWordWrap = false;
                importItem.ButtonStyle = eButtonStyle.ImageAndText;
                //string s = instance.Title;
                //for ( int i = 1 ; i * 5 + ( i - 1 ) * 5 < s.Length ; i++ )
                //{
                //    s = s.Insert(i * 5+ ( i - 1 ) * 5, "<br/>");
                //}
                //importItem.Text = s  ;
                importItem.Text = instance.Title;
            }
            else
            {
                //string s = instance.Title;
                //for ( int i = 1 ; i * 8 + ( i - 1 ) * 5 < s.Length ; i++ )
                //{
                //    s = s.Insert(i * 8 + ( i - 1 ) * 5, "<br/>");
                //}
                //importItem.Text =s;
                importItem.Text = instance.Title;
            }
            //importItem.ColorTable = eButtonColor.OrangeWithBackground;
            importItem.ShowSubItems = false;
            importItem.Tooltip = instance.Title;
            importItem.Click += new EventHandler(importItem_Click);
            importItem.Tag = instance;
            // 2008/4/9
            //this.galleryContainer1.SubItems.Add(importItem, galleryContainer1.SubItems.IndexOf(buttonItem1));
            this.btnImportList.SubItems.Add(importItem, btnImportList.SubItems.Count - 3);
            //this.btnImportList.SubItems.Add(importItem);

            // 2008/4/9
            //if ( !_Group.ContainsKey(instance.Group) )
            //{
            //    GalleryGroup newGroup = new GalleryGroup();
            //    newGroup.Name = "instance.Group";
            //    newGroup.Text = "<b>" + instance.Group + "</b>";
            //    newGroup.DisplayOrder = _Group.Count;
            //    this.galleryContainer1.GalleryGroups.Add(newGroup);
            //    _Group.Add(instance.Group, newGroup);
            //}
            //this.galleryContainer1.SetGalleryGroup(importItem, _Group[instance.Group]);

            //權限判斷
            if (Attribute.IsDefined(instance.GetType(), typeof(FeatureCodeAttribute)))
            {
                if (!CurrentUser.Acl[instance.GetType()].Executable)
                    importItem.Enabled = false;
            }
        }

        void importItem_Click(object sender, EventArgs e)
        {
            new ImportStudent((ImportProcess)((ButtonItem)sender).Tag).ShowDialog();
        }

        public void Remove(ImportProcess instance)
        {

        }

        #endregion

        #region IManager<ExportProcess> 成員

        public void Add(ExportProcess instance)
        {
            ButtonItem exportItem = new ButtonItem();
            //exportItem.FixedSize = new System.Drawing.Size(120, 35);
            exportItem.ImagePaddingHorizontal = 0;
            exportItem.ImagePaddingVertical = 0;
            if (instance.Image != null)
            {
                exportItem.Image = instance.Image;
                exportItem.ImageFixedSize = new System.Drawing.Size(16, 16);
                exportItem.ImagePosition = DevComponents.DotNetBar.eImagePosition.Left;
                exportItem.RibbonWordWrap = false;
                exportItem.ButtonStyle = eButtonStyle.ImageAndText;
                //string s = instance.Title;
                //for ( int i = 1 ; i * 5 + ( i - 1 ) * 5 < s.Length ; i++ )
                //{
                //    s = s.Insert(i * 5 + ( i - 1 ) * 5, "<br/>");
                //}
                //exportItem.Text =s;
                exportItem.Text = instance.Title;
            }
            else
            {
                //string s = instance.Title;
                //for ( int i = 1 ; i * 8 + ( i - 1 ) * 5 < s.Length ; i++ )
                //{
                //    s = s.Insert(i * 8 + ( i - 1 ) * 5, "<br/>");
                //}
                //exportItem.Text = s ;
                exportItem.Text = instance.Title;
            }
            //exportItem.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(0, 0, 0,23);
            //exportItem.ColorTable = eButtonColor.OrangeWithBackground;
            exportItem.ShowSubItems = false;
            exportItem.Tooltip = instance.Title;
            exportItem.Click += new EventHandler(exportItem_Click);
            exportItem.Tag = instance;
            // 2008/4/9
            //this.galleryContainer1.SubItems.Add(exportItem,galleryContainer1.SubItems.IndexOf(btnImport));
            this.btnExportList.SubItems.Add(exportItem, btnExportList.SubItems.Count);
            //this.btnExportList.SubItems.Add(exportItem);

            // 2008/4/9
            //if ( !_Group.ContainsKey(instance.Group) )
            //{
            //    GalleryGroup newGroup = new GalleryGroup();
            //    newGroup.Name = "instance.Group";
            //    newGroup.Text = "<b>" + instance.Group + "</b>";
            //    newGroup.DisplayOrder = _Group.Count;
            //    this.galleryContainer1.GalleryGroups.Add(newGroup);
            //    _Group.Add(instance.Group, newGroup);
            //}
            //this.galleryContainer1.SetGalleryGroup(exportItem, _Group[instance.Group]);

            //權限判斷
            if (Attribute.IsDefined(instance.GetType(), typeof(FeatureCodeAttribute)))
            {
                if (!CurrentUser.Acl[instance.GetType()].Executable)
                    exportItem.Enabled = false;
            }
        }

        void exportItem_Click(object sender, EventArgs e)
        {
            new ExportStudent(((ExportProcess)((ButtonItem)sender).Tag)).ShowDialog();
        }

        public void Remove(ExportProcess instance)
        {
        }

        #endregion

        
        private void buttonItem1_Click_1(object sender, EventArgs e)
        {
            new SmartSchool.StudentRelated.RibbonBars.Import.BatchUploadPhotoForm().ShowDialog();
        }

        private void btnExportList_PopupOpen(object sender, PopupOpenEventArgs e)
        {
            List<ButtonItem> list = new List<ButtonItem>();
            foreach (ButtonItem item in btnExportList.SubItems)
                list.Add(item);
            list.Sort(new Comparison<ButtonItem>(SortItem));
            btnExportList.SubItems.Clear();
            foreach (ButtonItem item in list)
                btnExportList.SubItems.Add(item);
        }

        private void btnImportList_PopupOpen(object sender, PopupOpenEventArgs e)
        {
            List<ButtonItem> list = new List<ButtonItem>();
            foreach (ButtonItem item in btnImportList.SubItems)
                list.Add(item);
            list.Sort(new Comparison<ButtonItem>(SortItem));
            btnImportList.SubItems.Clear();
            foreach (ButtonItem item in list)
                btnImportList.SubItems.Add(item);
        }

        private int SortItem(ButtonItem x, ButtonItem y)
        {
            int xi = GetItemIndex(x.Text);
            int yi = GetItemIndex(y.Text);

            if (xi > 0 || yi > 0)
                return xi.CompareTo(yi);
            else
                return y.Text.CompareTo(x.Text);
        }

        private int GetItemIndex(string x)
        {
            #region 排序清單
            List<string> list = new List<string>(new string[] {
                "匯出學籍資料",
                "匯出學期科目成績",
                "匯出學期分項成績",
                "匯出學年科目成績",
                "匯出學年分項成績",
                "匯出缺曠紀錄",
                "匯出獎懲紀錄",
                "匯出異動紀錄",
                "匯出新生異動",
                "匯出轉入異動",
                "匯出學籍異動",
                "匯出畢業異動",
                "匯出畢業成績",
                "匯出自訂欄位",
                "匯出學生類別",
                "匯出離校資訊",

                "匯入學籍資料",
                "匯入學期科目成績",
                "匯入學期分項成績",
                "匯入學年科目成績",
                "匯入學年分項成績",
                "匯入缺曠紀錄",
                "匯入獎懲紀錄",
                "匯入異動紀錄",
                "匯入新生異動",
                "匯入轉入異動",
                "匯入學籍異動",
                "匯入畢業異動",
                "匯入照片",
                "匯入自訂欄位",
                "匯入學生類別"
            });
            #endregion

            int xi = list.IndexOf(x);
            if ( xi < 0 ) xi = list.Count;
            return xi;
        }
    }
}