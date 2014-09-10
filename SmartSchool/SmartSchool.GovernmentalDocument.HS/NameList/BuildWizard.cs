using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.Common;
using System.Xml;
using SmartSchool.Feature.UpdateRecordBatch;
//using SmartPlugIn.StudentRecord.NameList.NameListBuilder;
using IntelliSchool.DSA.ClientFramework.ControlCommunication;

namespace SmartSchool.GovernmentalDocument.NameList
{
    public partial class BuildWizard : BaseForm
    {
        INameListProvider _NameListProvider;
        BackgroundWorker _BackgroundWorker = new BackgroundWorker();

        public BuildWizard()
        {
            InitializeComponent();
            comboBoxEx1.Items.AddRange(new object[] { CurrentUser.Instance.SchoolYear.ToString(), (CurrentUser.Instance.SchoolYear - 1).ToString() });
            comboBoxEx2.Items.AddRange(new object[] { "1","2"});
            comboBoxEx1.Text = CurrentUser.Instance.SchoolYear.ToString();
            comboBoxEx2.Text = CurrentUser.Instance.Semester.ToString();
            AddProvider(new EnrollmentListProvider());
            AddProvider(new ExtendingStudentListProvider());
            AddProvider(new GraduatingStudentListProvider());
            AddProvider(new ExtendingStudentGraduateListProvider());
            AddProvider(new StudentUpdateRecordListProvider());
            AddProvider(new ExtendingStudentUpdateRecordListProvider());
            AddProvider(new TransferringStudentUpdateRecordListProvider());
            lvSorter1.ColumnSorter.Add(columnHeader3,SortType.Int);
            lvSorter1.ColumnSorter.Add(columnHeader6,SortType.Int);
            lvSorter1.ColumnSorter.Add(columnHeader5, SortType.DateTime);
            lvSorter1.ColumnSorter.Add(columnHeader1, SortType.Int);
            lvSorter1.ColumnSorter.Add(columnHeader2, SortType.String);
            lvSorter1.ColumnSorter.Add(columnHeader4, SortType.String);
            lvSorter1.ColumnSorter.Add(columnHeader7, SortType.String);
            lvViewManager1.SetDynamicGrouping(columnHeader1);
            lvViewManager1.SetDynamicGrouping(columnHeader2);
            lvViewManager1.SetDynamicGrouping(columnHeader5);
            lvViewManager1.SetDynamicGrouping(columnHeader6);

        }

        private void AddProvider(INameListProvider nameListProvider)
        {
            System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem(nameListProvider.Title, 0);
            listViewItem.Tag=nameListProvider;
            listView1.Items.Add(listViewItem);
        }

        #region 下一步
        private void BuildWizard_Load(object sender, EventArgs e)
        {
            int xm, ym;
            xm = (this.Width - panelEx1.Width - 20) / 2;
            ym = (this.Height - panelEx1.Height - 40) / 2;
            this.Top += ym;
            this.Left += xm;
            this.SuspendLayout();
            panelEx1.SuspendLayout();
            panelEx2.SuspendLayout();
            panelEx3.SuspendLayout();
            this.Height = panelEx1.Height + 40;
            this.Width = panelEx1.Width + 20;
            panelEx1.Visible = true;
            panelEx2.Visible = false;
            panelEx3.Visible = false;
            panelEx1.Dock = DockStyle.Fill;
            panelEx2.Dock = DockStyle.None;
            panelEx3.Dock = DockStyle.None;
            panelEx1.ResumeLayout();
            panelEx2.ResumeLayout();
            panelEx3.ResumeLayout();
            this.ResumeLayout();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            int xm, ym;
            xm = (this.Width - panelEx2.Width - 20) / 2;
            ym = (this.Height - panelEx2.Height - 40) / 2;
            this.Top += ym;
            this.Left += xm;
            this.SuspendLayout();
            panelEx1.SuspendLayout();
            panelEx2.SuspendLayout();
            panelEx3.SuspendLayout();
            this.Height = panelEx2.Height + 40;
            this.Width = panelEx2.Width + 20;
            panelEx1.Visible = false;
            panelEx2.Visible = true;
            panelEx3.Visible = false;
            panelEx1.Dock = DockStyle.None;
            panelEx2.Dock = DockStyle.Fill;
            panelEx3.Dock = DockStyle.None;
            this.Visible = true;
            panelEx1.ResumeLayout();
            panelEx2.ResumeLayout();
            panelEx3.ResumeLayout();
            this.ResumeLayout();
            _NameListProvider = (INameListProvider)listView1.SelectedItems[0].Tag;
            listView2.Items.Clear();

            _BackgroundWorker.DoWork += new DoWorkEventHandler(_BackgroundWorker_DoWork);
            _BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BackgroundWorker_RunWorkerCompleted);
            _BackgroundWorker.WorkerSupportsCancellation = true;
            _BackgroundWorker.RunWorkerAsync();
            
            comboBoxEx1_TextChanged(null, null);
        }

        void _BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox1.Visible = false;
                listView2.SuspendLayout();
                foreach (XmlElement var in (List<XmlElement>)e.Result)
                {
                    ListViewItem item = new ListViewItem(new string[] { 
                        var.SelectSingleNode("UpdateCode").InnerText,
                        var.SelectSingleNode("UpdateDate").InnerText,
                        var.SelectSingleNode("GradeYear").InnerText,
                        var.SelectSingleNode("Department").InnerText,
                        var.SelectSingleNode("StudentNumber").InnerText,
                        var.SelectSingleNode("Name").InnerText,
                        var.SelectSingleNode("UpdateDescription").InnerText
                    });
                    item.Tag = var;
                    listView2.Items.Add(item);
                }
                listView2.ResumeLayout();
                dateTimeTextBox1_TextChanged(null, null);
            }
        }

        void _BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = _NameListProvider.GetExpectantList();
            if (_BackgroundWorker.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            int xm, ym;
            xm = (this.Width - panelEx3.Width - 20) / 2;
            ym = (this.Height - panelEx3.Height - 40) / 2;
            this.Top += ym;
            this.Left += xm;
            this.SuspendLayout();
            panelEx1.SuspendLayout();
            panelEx2.SuspendLayout();
            panelEx3.SuspendLayout();
            this.Height = panelEx3.Height + 40;
            this.Width = panelEx3.Width + 20;
            panelEx1.Visible = false;
            panelEx2.Visible = false;
            panelEx3.Visible = true;
            panelEx1.Dock = DockStyle.None;
            panelEx2.Dock = DockStyle.None;
            panelEx3.Dock = DockStyle.Fill;
            this.Visible = true;
            panelEx1.ResumeLayout();
            panelEx2.ResumeLayout();
            panelEx3.ResumeLayout();
            this.ResumeLayout();
        } 
        #endregion

        private void Exit(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxEx1_TextChanged(object sender, EventArgs e)
        {
            if (_NameListProvider != null)
                txtNameListName.Text = comboBoxEx1.Text + "_" + comboBoxEx2.Text + "_" + _NameListProvider.Title;
            int i = 0;
            dateTimeTextBox1.Text = dateTimeTextBox2.Text = "";
            if (int.TryParse(comboBoxEx1.Text, out i))
            {
                if (comboBoxEx2.Text == "1")
                {
                    dateTimeTextBox1.Text = "" + (i + 1911) + "/8/1";
                    dateTimeTextBox2.Text = "" + (i + 1911 + 1) + "/1/31";
                }
                if (comboBoxEx2.Text == "2")
                {
                    dateTimeTextBox1.Text = "" + (i + 1911 + 1) + "/2/1";
                    dateTimeTextBox2.Text = "" + (i + 1911 + 1) + "/7/31";
                }
                buttonX3.Enabled = true;
            }
            else
                buttonX3.Enabled = false;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonX1.Enabled = (listView1.SelectedItems.Count == 1);
        }

        private void dateTimeTextBox1_TextChanged(object sender, EventArgs e)
        {
            DateTime begin = DateTime.Now;
            DateTime end = DateTime.Now;
            DateTime updateDate = DateTime.Now;
            if (DateTime.TryParse(dateTimeTextBox1.Text, out begin) && DateTime.TryParse(dateTimeTextBox2.Text, out end))
            {   
                foreach (ListViewItem var in listView2.Items)
                {
                    XmlElement element = (XmlElement)var.Tag;                    
                    
                    if (DateTime.TryParse(element.SelectSingleNode("UpdateDate").InnerText, out updateDate) && updateDate >= begin && updateDate <= end)
                    {
                        var.ForeColor = Color.Blue;
                        var.Checked = true;
                    }
                    else
                    {
                        var.ForeColor = listView2.ForeColor;
                        var.Checked = false;
                    }
                }
            }
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            List<XmlElement> list = new List<XmlElement>();
            foreach (ListViewItem var in listView2.Items)
            {
                if (var.Checked)
                    list.Add((XmlElement)var.Tag);
            }
            UpdateRecordBatch.InsertUpdateRecordBatch(txtNameListName.Text, comboBoxEx1.Text, comboBoxEx2.Text, _NameListProvider.CreateNameList(comboBoxEx1.Text, comboBoxEx2.Text, list));
            //_NameListProvider.CreateNameList(comboBoxEx1.Text, comboBoxEx2.Text, list).OwnerDocument.Save("D:\\1234.xml");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BuildWizard_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_BackgroundWorker.IsBusy)
            {
                _BackgroundWorker.CancelAsync();
            }
            if (this.DialogResult != DialogResult.OK)
                this.DialogResult = DialogResult.Cancel;
        }
    }
}