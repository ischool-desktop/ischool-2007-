using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature;
using System.Xml;
//using SmartPlugIn.StudentRecord.NameList.��ameListBuilder;
//using SmartSchool.StudentRelated;
//using RB = SmartSchool.ReportBuilder.IReportBuilder;
//using RM = SmartSchool.ReportBuilder.ReportBuilderManager;
//using SmartSchool.ReportBuilder;
using System.IO;
using SmartSchool.Feature.UpdateRecordBatch;

namespace SmartSchool.GovernmentalDocument.NameList
{
    public partial class ListForm : BaseForm
    {
        public ListForm()
        {
            InitializeComponent();
            Initialize();
        }

        /// <summary>
        /// ��l��
        /// </summary>
        private void Initialize()
        {
            buttonX1.Visible = false;
            buttonX2.Visible = false;
            lblADName1.Text = "";
            lblADName2.Text = "";
            lblADName3.Text = "";
            lblADInfo.Text = "";
            lblADCounter.Text = "";
            lblADName3.Text = "";
            // �٤���Ϊ���� disabled
            btnDelete.Enabled = false;
            btnAD.Enabled = false;
            lblListContent.Text = "";
            lblAD.Text = "";
            listView.Clear();
            // ���J�{���W�U�����Ǧ~�ײM��
            DSResponse dsrsp = QueryStudent.GetSchoolYearList();
            DSXmlHelper helper = dsrsp.GetContent();
            cboSchoolYear.SelectedItem = null;
            cboSchoolYear.Items.Clear();
            foreach (XmlNode node in helper.GetElements("SchoolYear"))
            {
                string year = node.InnerText;
                string schoolYear = year + " �Ǧ~��";
                KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(year, schoolYear);
                cboSchoolYear.Items.Add(kvp);
            }
            cboSchoolYear.DisplayMember = "Value";
            cboSchoolYear.ValueMember = "Key";

            // �w�אּ��Ǧ~��
            int selectedIndex = 0;
            if (cboSchoolYear.Items.Count > 0)
            {
                for (int i = 0; i < cboSchoolYear.Items.Count; i++)
                {
                    KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)cboSchoolYear.Items[i];
                    if (kvp.Key == CurrentUser.Instance.SchoolYear.ToString())
                    {
                        selectedIndex = i;
                        break;
                    }
                }
                cboSchoolYear.SelectedIndex = selectedIndex;
            }
        }

        private void cboSchoolYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBatchList();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            BuildWizard b = new BuildWizard();
            DialogResult result = b.ShowDialog();
            if (result == DialogResult.OK)
            {
                Initialize();
                //LoadBatchList();
            }
        }

        private ISummaryProvider provider;
        private XmlElement source;
        private void lstList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstList.SelectedItems.Count > 0)
            {
                btnDelete.Enabled = true;
                btnAD.Enabled = true;

                ListViewItem item = lstList.SelectedItems[0];
                string id = item.Tag.ToString();
                DSResponse dsrsp = QueryStudent.GetUpdateRecordBatch(id);

                listView.SuspendLayout();
                if (dsrsp.HasContent)
                {
                    DSXmlHelper helper = dsrsp.GetContent();
                    provider = new SummaryProvider(helper);

                    lblADCounter.Text = "�i" + provider.Title + "�j �U��H�Ʋέp";
                    lblADInfo.Text = "�i" + provider.Title + "�j �֭�帹";
                    buttonX1.Visible = true;
                    buttonX2.Visible = true;

                    XmlNode contentNode = helper.GetElement("UpdateRecordBatch/Content");
                    if (contentNode == null)
                        return;
                    source = (XmlElement)contentNode.FirstChild;

                    //�B�z�ݪO��T
                    StringBuilder builder = new StringBuilder("");
                    foreach (Department dept in provider.GetDepartments())
                    {
                        builder = builder.Append("��").Append(dept.Name).Append("�@")
                            .Append("�k�� <font color='blue'>").Append(dept.Male).Append("</font> �H�@")
                            .Append("�k�� <font color='blue'>").Append(dept.Female).Append("</font> �H�@")
                            .Append("(�X�p <font color='blue'>").Append(dept.Total).Append("</font> �H)");

                        if (dept.Unknow > 0)
                        {
                            builder = builder.Append("<font color='red'>").Append(dept.Unknow)
                            .Append("</font>�H����ʧO");
                        }
                        builder = builder.Append("<br/>");
                    }
                    lblListContent.Text = builder.ToString();
                    lblADName1.Text = provider.Title;
                    lblADName2.Text = provider.Title;
                    lblADName3.Text = provider.Title;

                    // �B�z�֭����P�帹
                    string adString = "";
                    if (!string.IsNullOrEmpty(provider.ADNumber))
                    {
                        adString += "�֭�帹�@<font color='red'>" + provider.ADNumber + "</font>�@";
                        adString += "�֭����@<font color='red'>" + provider.ADDate + "</font>";
                    }
                    else
                        adString = "<font color='red'>���n��</font>";
                    lblAD.Text = adString;

                    //�B�zListView�e�{���
                    listView.Clear();

                    foreach (IEntryFormat format in provider.GetEntities())
                    {
                        // �Y�L�s�իh���[�W�s��                       
                        if (listView.Groups[format.Group] == null)
                            listView.Groups.Add(format.Group, format.Group);

                        // �Y�S����W�h���[�W��W
                        if (listView.Columns.Count == 0)
                        {
                            foreach (string column in format.DisplayColumns.Keys)
                                listView.Columns.Add(column, format.DisplayColumns[column].Width);
                        }

                        // ��쳣���F�h�̾�����J���ݩʭ�
                        ListViewItem rowItem = null;
                        for (int i = 0; i < listView.Columns.Count; i++)
                        {
                            string columnName = listView.Columns[i].Text;
                            string value = format.DisplayColumns[columnName].Value;
                            if (i == 0)
                                rowItem = new ListViewItem(value);
                            else
                                rowItem.SubItems.Add(value);
                        }
                        rowItem.Tag = format;
                        rowItem.Group = listView.Groups[format.Group];
                        listView.Items.Add(rowItem);
                    }
                }
                listView.ResumeLayout();
            }
            else
            {
                btnDelete.Enabled = false;
                btnAD.Enabled = false;
            }

            this.itemPanel1.RecalcLayout();
            //this.itemPanel1.Refresh();
        }

        private UpdateRecordADN _adnForm;
        private void btnAD_Click(object sender, EventArgs e)
        {
            if (_adnForm == null)
            {
                _adnForm = new UpdateRecordADN();
                _adnForm.DataSaved += new EventHandler(_adnForm_DataSaved);
            }
            ListViewItem item = lstList.FocusedItem;
            //string id = item.Tag.ToString();
            //_adnForm.Initialize(id);
            _adnForm.Initialize(provider);
            _adnForm.ShowDialog();
        }

        void _adnForm_DataSaved(object sender, EventArgs e)
        {
            LoadBatchList();
        }

        private void LoadBatchList()
        {
            lstList.SuspendLayout();
            lstList.Items.Clear();
            KeyValuePair<string, string> kvp;//= (KeyValuePair<string, string>)cboSchoolYear.SelectedItem;
            string schoolYear;
            if (cboSchoolYear.SelectedItem == null)
            {
                return;
            }
            else
            {
                kvp = (KeyValuePair<string, string>)cboSchoolYear.SelectedItem;
                schoolYear = kvp.Key;
            }
            DSResponse dsrsp = QueryStudent.GetUpdateRecordBatchBySchoolYear(schoolYear);
            DSXmlHelper helper = dsrsp.GetContent();

            foreach (XmlNode node in helper.GetElements("UpdateRecordBatch"))
            {
                string name = node.SelectSingleNode("Name").InnerText;
                string adn = node.SelectSingleNode("ADNumber").InnerText;
                int imageIndex = !string.IsNullOrEmpty(adn) ? 1 : 0;
                string id = node.Attributes["ID"].Value;

                ListViewItem item = new ListViewItem(name, imageIndex);
                item.Tag = id;
                lstList.Items.Add(item);
            }

            lstList.ResumeLayout();
        }

        string path;
        List<IReportBuilder> registedBuilder = new List<IReportBuilder>();

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (provider == null)
                return;
            if (ReportBuilderManager.Items[provider.Type].Count == 0)
            {
                MsgBox.Show("�q�д���");
                return;
            }
            progressBarX1.Value = 0;
            pnlReport.Visible = true;

            IReportBuilder builder = ReportBuilderManager.Items[provider.Type][0];
            if (!registedBuilder.Contains(builder))
            {
                builder.Completed += new RunWorkerCompletedEventHandler(builder_Completed);
                builder.ProgressChanged += new ProgressChangedEventHandler(builder_ProgressChanged);
                registedBuilder.Add(builder);
            }

            if (builder.Status == Status.Busy) return;

            path = Path.Combine(Application.StartupPath, "Reports");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, provider.Type + ".xlt");

            if (File.Exists(path))
            {
                int i = 1;
                while (true)
                {
                    string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + (i++) + Path.GetExtension(path);
                    if (!File.Exists(newPath))
                    {
                        path = newPath;
                        break;
                    }
                }
            }
            try
            {
                File.Create(path).Close();
            }
            catch
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Title = "�t�s�s��";
                sd.FileName = Path.GetFileNameWithoutExtension(path) + ".xls";
                sd.Filter = "Excel�ɮ� (*.xls)|*.xls|�Ҧ��ɮ� (*.*)|*.*";
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.Create(sd.FileName);
                        path = sd.FileName;
                    }
                    catch
                    {
                        MsgBox.Show("���w���|�L�k�s���C", "�إ��ɮץ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            builder.BuildReport(source, path);
        }

        void builder_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarX1.Value = e.ProgressPercentage;
        }

        void builder_Completed(object sender, RunWorkerCompletedEventArgs e)
        {            
            progressBarX1.Value = 0;
            pnlReport.Visible = false;
            try
            {
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception)
            {

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstList.FocusedItem == null)
                return;
            string id = lstList.FocusedItem.Tag.ToString();
            if (MsgBox.Show("�z�T�w�R���ӦW�U�Ψ䤺�e?", "�T�w", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                UpdateRecordBatch.DeleteBatch(id);
                Initialize();
                LoadBatchList();
            }
        }

        private void ListForm_Load(object sender, EventArgs e)
        {

        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            //if (listView.SelectedItems.Count != 1) return;
            //ListViewItem item = listView.SelectedItems[0];
            //if (item.Tag == null) return;
            //IEntryFormat format = item.Tag as IEntryFormat;
            //format.
            //SmartSchool.StudentRelated.Student.Instance.ShowDetail(studentid);
        }

        //�����e,�ϵ��U�ƥ�
        private void ListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (IReportBuilder builder in registedBuilder)
            {
                builder.Completed -= new RunWorkerCompletedEventHandler(builder_Completed);
                builder.ProgressChanged -= new ProgressChangedEventHandler(builder_ProgressChanged);
            }
        }
    }
}