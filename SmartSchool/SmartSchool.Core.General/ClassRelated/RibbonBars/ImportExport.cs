using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.ClassRelated.RibbonBars.Export;
using SmartSchool.ClassRelated.RibbonBars.Import;
using SmartSchool.Security;

namespace SmartSchool.ClassRelated.RibbonBars
{
    public partial class ImportExport : RibbonBarBase
    {
        FeatureAccessControl exportCtrl;
        FeatureAccessControl importCtrl;
        ButtonItemPlugInManager buttonManager;

        public ImportExport()
        {
            InitializeComponent();

            #region 設定為 "班級/匯出匯入" 的外掛處理者
            buttonManager = new ButtonItemPlugInManager(itemContainer1);
            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<DevComponents.DotNetBar.ButtonItem>.Instance.Add("班級/匯出匯入", buttonManager);
            #endregion

            //權限判斷 - 匯出班級
            exportCtrl = new FeatureAccessControl("Button0420");
            //權限判斷 - 匯入班級
            importCtrl = new FeatureAccessControl("Button0430");

            exportCtrl.Inspect(btnExport);
            importCtrl.Inspect(btnImport);
        }

        private void btnExportClass_Click(object sender, EventArgs e)
        {
            ExportClass form = new ExportClass();
            form.ShowDialog();
        }

        private void btnImportClass_Click(object sender, EventArgs e)
        {
            ClassImportWizard wizard = new ClassImportWizard();
            wizard.ShowDialog();
        }

    }
}
