using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using DevComponents.DotNetBar;
using SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.Connector;
using SmartSchool.StudentRelated;
using System.Xml;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler.Formater;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler;
using SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler;
using SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.Output;
using System.Diagnostics;
using DevComponents.DotNetBar.Rendering;

namespace SmartSchool.StudentRelated.RibbonBars.Import
{
    public partial class ExportWizard : BaseForm
    {
        //private ButtonX advButton;
        //private DevComponents.DotNetBar.ControlContainerItem advContainer;
        //private LinkLabel helpButton;

        //public event EventHandler HelpButtonClick;

        public ExportWizard()
        {
            InitializeComponent();

            //#region �[�J�i����HELP���s
            //advContainer = new ControlContainerItem();
            //advContainer.AllowItemResize = false;
            //advContainer.GlobalItem = false;
            //advContainer.MenuVisibility = eMenuVisibility.VisibleAlways;

            //ItemContainer itemContainer2 = new ItemContainer();
            //itemContainer2.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            //itemContainer2.MinimumSize = new System.Drawing.Size(0, 0);
            //itemContainer2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            //advContainer});

            //advButton = new ButtonX();
            //advButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            //advButton.Text = "    �i��";
            //advButton.Top = this.wizard1.Controls[1].Controls[0].Top;
            //advButton.Left = 5;
            //advButton.Size = this.wizard1.Controls[1].Controls[0].Size;
            //advButton.Visible = true;
            //advButton.SubItems.Add(itemContainer2);
            //advButton.PopupSide = ePopupSide.Top;
            //advButton.SplitButton = true;
            //advButton.Enabled = false;
            //advButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            //advButton.AutoExpandOnClick = true;
            //advButton.SubItemsExpandWidth = 16;
            //advButton.FadeEffect = false;
            //advButton.FocusCuesEnabled = false;
            //this.wizard1.Controls[1].Controls.Add(advButton);

            //helpButton = new LinkLabel();
            //helpButton.AutoSize = true;
            //helpButton.BackColor = System.Drawing.Color.Transparent;
            //helpButton.Location = new System.Drawing.Point(81, 10);
            //helpButton.Size = new System.Drawing.Size(69, 17);
            //helpButton.TabStop = true;
            //helpButton.Text = "Help";
            ////helpButton.Top = this.wizard1.Controls[1].Controls[0].Top + this.wizard1.Controls[1].Controls[0].Height - helpButton.Height;
            ////helpButton.Left = 150;
            //helpButton.Visible = false;
            //helpButton.Click += delegate { if (HelpButtonClick != null)HelpButtonClick(this, new EventArgs()); };
            //helpButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            //this.wizard1.Controls[1].Controls.Add(helpButton);
            //#endregion

            #region �]�wWizard�|���Style�]
            this.wizard1.HeaderStyle.ApplyStyle((GlobalManager.Renderer as Office2007Renderer).ColorTable.GetClass(ElementStyleClassKeys.RibbonFileMenuBottomContainerKey));
            this.wizard1.FooterStyle.BackColorGradientAngle = -90;
            this.wizard1.FooterStyle.BackColorGradientType = eGradientType.Linear;
            this.wizard1.FooterStyle.BackColor = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.Default.TopBackground.Start;
            this.wizard1.FooterStyle.BackColor2 = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.Default.TopBackground.End;
            this.wizard1.BackColor = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.Default.TopBackground.Start;
            this.wizard1.BackgroundImage = null;
            for (int i = 0; i < 5; i++)
            {
                (this.wizard1.Controls[1].Controls[i] as ButtonX).ColorTable = eButtonColor.OrangeWithBackground;
            }
            (this.wizard1.Controls[0].Controls[1] as System.Windows.Forms.Label).ForeColor = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.MouseOver.TitleText;
            (this.wizard1.Controls[0].Controls[2] as System.Windows.Forms.Label).ForeColor = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.Default.TitleText;
            #endregion
        }

        private void wizard1_CancelButtonClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }

        private void wizard1_FinishButtonClick(object sender, CancelEventArgs e)
        {
            if (GetSelectedFields().Count == 0)
            {
                MsgBox.Show("�����ܤֿ�ܤ@���ץX���!", "���ť�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            saveFileDialog1.Filter = "Excel (*.xls)|*.xls|�Ҧ��ɮ� (*.*)|*.*";
            saveFileDialog1.FileName = "�ץX�ǥͰ򥻸��";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ExportStudentConnector ec = new ExportStudentConnector();
                foreach (BriefStudentData student in SmartSchool.StudentRelated.Student.Instance.SelectionStudents)
                {
                    ec.AddCondition(student.ID);
                }
                ec.SetSelectedFields(GetSelectedFields());
                ExportTable table = ec.Export();

                ExportOutput output = new ExportOutput();
                output.SetSource(table);
                try
                {
                    output.Save(saveFileDialog1.FileName);
                }
                catch (Exception)
                {
                    MsgBox.Show("�ɮ��x�s����, �ɮץثe�i��w�g�}�ҡC", "�x�s����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (MsgBox.Show("�ɮצs�ɧ����A�O�_�}�Ҹ��ɮ�", "�O�_�}��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(saveFileDialog1.FileName);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("�}���ɮ׵o�ͥ���:" + ex.Message, "���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                this.Close();
            }
        }

        private void ExportWizard_Load(object sender, EventArgs e)
        {
            XmlElement element = SmartSchool.Feature.Student.StudentBulkProcess.GetExportDescription();
            BaseFieldFormater formater = new BaseFieldFormater();
            FieldCollection collection = formater.Format(element);

            foreach (Field field in collection)
            {
                ListViewItem item = listView.Items.Add(field.DisplayText);
                item.Tag = field;
                item.Checked = true;
            }
        }

        private FieldCollection GetSelectedFields()
        {
            FieldCollection collection = new FieldCollection();
            foreach (ListViewItem item in listView.CheckedItems)
            {
                Field field = item.Tag as Field;
                collection.Add(field);
            }
            return collection;
        }

        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView.Items)
            {
                item.Checked = chkSelect.Checked;
            }
        }
    }
}