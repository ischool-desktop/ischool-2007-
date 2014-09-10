using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;
using Aspose.Cells;
using SmartSchool.Common;

namespace SmartSchool.StudentRelated.RibbonBars.Import
{
    public partial class ImportSemesterScore : SmartSchool.Common.BaseForm
    {
        private Workbook workBook;

        public ImportSemesterScore()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog()== DialogResult.OK)
            {
                this.txtSourceFile.Text = openFileDialog1.FileName; 

            }
        }

        private void txtSourceFile_TextChanged(object sender, EventArgs e)
        {
            wizardPage1.NextButtonEnabled = eWizardButtonState.False;
            if (File.Exists(this.txtSourceFile.Text))
            {
                wizardPage1.NextButtonEnabled = eWizardButtonState.True;
            }
        }

        private void wizardPage1_NextButtonClick(object sender, CancelEventArgs e)
        {
            try
            {
                workBook = new Workbook();
                workBook.Open(txtSourceFile.Text);
            }
            catch (Exception ex)
            {
                MsgBox.Show("檔案無法開啟。\n" + ex.Message);
                e.Cancel = true;
            }
        }
    }
}