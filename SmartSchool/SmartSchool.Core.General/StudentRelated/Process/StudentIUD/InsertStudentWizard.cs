using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Feature.Basic;
using DevComponents.DotNetBar;
using SmartSchool.StudentRelated.Palmerworm;
using SmartSchool.Common;
using SmartSchool.Feature;
using SmartSchool.ExceptionHandler;
using SmartSchool.ApplicationLog;

namespace SmartSchool.StudentRelated.Process.StudentIUD
{
    internal partial class InsertStudentWizard : Form
    {
        string _NewStudentID;

        public InsertStudentWizard()
        {
            InitializeComponent();
        }

        public string NewStudentID
        {
            get { return _NewStudentID; }
        }

        private void wizard1_FinishButtonClick(object sender, CancelEventArgs e)
        {
            Application.DoEvents();
            if (checkBox1.Checked)
            {
                //Form f = new BaseForm();
                //PalmerwormStudent pv = new PalmerwormStudent();
                //pv.Dock = DockStyle.Fill;
                //f.Size = new Size(800, 600);
                //f.Controls.Add(pv);
                //pv.ID = newStudentID;
                //f.StartPosition = FormStartPosition.CenterParent;
                //f.ShowDialog();
                this.DialogResult = DialogResult.Yes;
                this.Hide();
            }
            else
                this.Close();
        }

        private void CheckCanInsert(object sender, EventArgs e)
        {
            wizardPage1.NextButtonEnabled = (textBox1.Text != "") ? DevComponents.DotNetBar.eWizardButtonState.True : DevComponents.DotNetBar.eWizardButtonState.False;
        }

        private void wizard1_WizardPageChanged(object sender, DevComponents.DotNetBar.WizardPageChangeEventArgs e)
        {
            if (e.OldPage == wizardPage1 && e.PageChangeSource == DevComponents.DotNetBar.eWizardPageChangeSource.NextButton)
            {
                _NewStudentID = AddStudent.InsertStudent(textBox1.Text);

                CurrentUser user = CurrentUser.Instance;
                try
                {
                    InsertStudentLog log = new InsertStudentLog(NewStudentID);
                    log.StudentName = textBox1.Text;
                    user.AppLog.Write(log);
                }
                catch (Exception ex)
                {
                    BugReporter.ReportException(user.SystemName, user.SystemVersion, ex, false);
                }

                Student.Instance.InvokStudentInserted(new EventArgs());
            }
        }
    }
}