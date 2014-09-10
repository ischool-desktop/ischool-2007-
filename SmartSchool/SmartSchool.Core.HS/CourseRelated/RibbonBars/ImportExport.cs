using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.CourseRelated.RibbonBars.Export;
using SmartSchool.CourseRelated.RibbonBars.Import;
using SmartSchool.Common;
using SmartSchool.Security;

namespace SmartSchool.CourseRelated.RibbonBars
{
    public partial class ImportExport : SmartSchool.CourseRelated.RibbonBars.RibbonBarBase
    {
        FeatureAccessControl exportCtrl;
        FeatureAccessControl importCtrl;

        public ImportExport()
        {
            InitializeComponent();

            //�v���P�_ - �ץX�ҵ{
            exportCtrl = new FeatureAccessControl("Button0600");
            //�v���P�_ - �פJ�ҵ{
            importCtrl = new FeatureAccessControl("Button0610");

            exportCtrl.Inspect(btnExport);
            importCtrl.Inspect(btnImport);
        }

        private void buttonItem109_Click(object sender, EventArgs e)
        {
            ExportForm form = new ExportForm();
            form.ShowDialog();
        }

        private void buttonItem102_Click(object sender, EventArgs e)
        {
            CourseImportWizard form = new CourseImportWizard();
            form.ShowDialog();
        }
    }
}

