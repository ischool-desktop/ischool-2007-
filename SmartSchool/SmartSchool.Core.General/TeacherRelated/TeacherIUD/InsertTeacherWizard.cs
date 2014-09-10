using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Feature.Basic;
using DevComponents.DotNetBar;
using SmartSchool.Common;
using SmartSchool.Feature;
using SmartSchool.Feature.Teacher;
using DevComponents.DotNetBar.Rendering;

namespace SmartSchool.TeacherRelated.TeacherIUD
{
    internal partial class InsertTeacherWizard : BaseForm
    {
        string _NewTeacherID;

        public InsertTeacherWizard()
        {
            InitializeComponent();
            #region 設定Wizard會跟著Style跑
            //this.wizard1.FooterStyle.ApplyStyle(( GlobalManager.Renderer as Office2007Renderer ).ColorTable.GetClass(ElementStyleClassKeys.RibbonFileMenuBottomContainerKey));
            this.wizard1.HeaderStyle.ApplyStyle(( GlobalManager.Renderer as Office2007Renderer ).ColorTable.GetClass(ElementStyleClassKeys.RibbonFileMenuBottomContainerKey));
            this.wizard1.FooterStyle.BackColorGradientAngle = -90;
            this.wizard1.FooterStyle.BackColorGradientType = eGradientType.Linear;
            this.wizard1.FooterStyle.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.Default.TopBackground.Start;
            this.wizard1.FooterStyle.BackColor2 = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.Default.TopBackground.End;
            this.wizard1.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.Default.TopBackground.Start;
            this.wizard1.BackgroundImage = null;
            for ( int i = 0 ; i < 5 ; i++ )
            {
                ( this.wizard1.Controls[1].Controls[i] as ButtonX ).ColorTable = eButtonColor.OrangeWithBackground;
            }
            ( this.wizard1.Controls[0].Controls[1] as System.Windows.Forms.Label ).ForeColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.MouseOver.TitleText;
            ( this.wizard1.Controls[0].Controls[2] as System.Windows.Forms.Label ).ForeColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.Default.TitleText;
            #endregion
        }

        public string NewTeacherID
        {
            get { return _NewTeacherID; }
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
                bool nameOk = false;
                int nick = 0;
                string insertInck = string.Empty;
                while (!nameOk)
                {
                    if (QueryTeacher.NameExists(string.Empty, textBox1.Text, insertInck))
                    {
                        if (nick <= 0)
                            insertInck = "New";
                        else
                            insertInck = "New " + nick;

                        nick++;
                    }
                    else
                        nameOk = true;
                }

                _NewTeacherID = AddTeacher.InsertTeacher(textBox1.Text, insertInck);
                CurrentUser.Instance.AppLog.Write(SmartSchool.ApplicationLog.EntityType.Teacher, "新增教師", _NewTeacherID, "新教師姓名：" + textBox1.Text, "教師", _NewTeacherID);

                Teacher.Instance.InvokTeacherInserted(new EventArgs());
            }
        }
    }
}