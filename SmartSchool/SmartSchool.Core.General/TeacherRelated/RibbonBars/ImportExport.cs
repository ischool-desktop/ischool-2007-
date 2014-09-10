using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.TeacherRelated.RibbonBars.Import;
using SmartSchool.Common;
using SmartSchool.Security;

namespace SmartSchool.TeacherRelated.RibbonBars
{
    public partial class ImportExport : SmartSchool.TeacherRelated.RibbonBars.RibbonBarBase
    {
        FeatureAccessControl exportCtrl;
        FeatureAccessControl importCtrl;

        public ImportExport()
        {
            InitializeComponent();

            //�v���P�_ - �ץX�Юv
            exportCtrl = new FeatureAccessControl("Button0490");

            //�v���P�_ - �פJ�Юv
            importCtrl = new FeatureAccessControl("Button0500");

            exportCtrl.Inspect(btnExport);
            importCtrl.Inspect(btnImport);
        }

        private void buttonItem109_Click(object sender, EventArgs e)
        {
            ExportTeacher form = new ExportTeacher();
            form.ShowDialog();
        }

        private void buttonItem102_Click(object sender, EventArgs e)
        {
            TeacherImportWizard wizard = new TeacherImportWizard();
            wizard.ShowDialog();
        }
    }
}

