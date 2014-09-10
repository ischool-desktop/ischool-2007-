using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IntelliSchool.DSA30.Util;
using SmartSchool;
using System.Xml;
using SmartSchool.Customization.Data;
using SmartSchool.Common;

namespace DataManager
{
    public partial class UpdateRecord : BaseForm
    {
        private DateTime _startDate;
        private DateTime _endDate;
        private UpdateTypeForm _typeForm;
        private BackgroundWorker _loader;
        private AccessHelper _accessHelper = new AccessHelper();

        //Cache學生集合
        private Dictionary<string, SmartSchool.StudentRelated.BriefStudentData> students = new Dictionary<string, SmartSchool.StudentRelated.BriefStudentData>();

        public UpdateRecord()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            _loader = new BackgroundWorker();
            _loader.DoWork += new DoWorkEventHandler(_loader_DoWork);
            _loader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_loader_RunWorkerCompleted);

            txtEndDate.Text = txtStartDate.Text = DateTime.Today.ToString("yyyy/MM/dd");

            _typeForm = new UpdateTypeForm();

            foreach (SmartSchool.StudentRelated.BriefStudentData var in SmartSchool.StudentRelated.Student.Instance.Items)
            {
                students.Add(var.ID, var);
            }
        }

        private void _loader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result is DSXmlHelper && e.Result != null)
                    FillDataGridView(e.Result as DSXmlHelper);
            }
        }

        private void _loader_DoWork(object sender, DoWorkEventArgs e)
        {

            //<GetDetailListRequest>
            //    <Field>
            //        <ID/>
            //        <RefStudentID/>
            //        <SchoolYear/>
            //        <Semester/>
            //        <Name/>
            //        <StudentNumber/>
            //        <UpdateDate/>
            //        <UpdateCode/>
            //        <UpdateDescription/>
            //        <ADNumber/>
            //    </Field>
            //    <Condition>
            //        <UpdateCode>301</UpdateCode>
            //        <StartDate>2006/08/01</StartDate>
            //        <EndDate>2006/08/01</EndDate>
            //    </Condition>
            //</GetDetailListRequest>

            DSXmlHelper helper = new DSXmlHelper("GetDetailListRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "ID");
            helper.AddElement("Field", "RefStudentID");
            helper.AddElement("Field", "SchoolYear");
            helper.AddElement("Field", "Semester");
            helper.AddElement("Field", "Name");
            helper.AddElement("Field", "StudentNumber");
            helper.AddElement("Field", "UpdateDate");
            helper.AddElement("Field", "UpdateCode");
            helper.AddElement("Field", "UpdateDescription");
            helper.AddElement("Field", "ADNumber");
            helper.AddElement("Condition");
            helper.AddElement("Condition", "StartDate", _startDate.ToString("yyyy/MM/dd"));
            helper.AddElement("Condition", "EndDate", _endDate.ToString("yyyy/MM/dd"));
            foreach (string code in _typeForm.CodeList)
                helper.AddElement("Condition", "UpdateCode", code);
            helper.AddElement("Order");
            helper.AddElement("Order", "UpdateCode");
            helper.AddElement("Order", "RefStudentID");

            try
            {
                DSResponse dsrsp = SmartSchool.Feature.QueryStudent.GetUpdateRecord(new DSRequest(helper));
                e.Result = dsrsp.GetContent();
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show("取得異動資料失敗。");
            }
        }

        private void FillDataGridView(DSXmlHelper helper)
        {
            dataGridViewX1.SuspendLayout();

            foreach (XmlElement ur in helper.GetElements("UpdateRecord"))
            {
                DSXmlHelper urHelper = new DSXmlHelper(ur);
                SmartSchool.StudentRelated.BriefStudentData student = null;
                SmartSchool.ClassRelated.ClassInfo classInfo = null;
                try
                {
                    student = students[urHelper.GetText("@RefStudentID")];
                    if (!string.IsNullOrEmpty(student.RefClassID))
                        classInfo = SmartSchool.ClassRelated.Class.Instance.Items[student.RefClassID];
                }
                catch (Exception ex) { }

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewX1,
                    urHelper.GetText("@ID"),
                    urHelper.GetText("UpdateDate"),
                    (student != null) ? ((classInfo != null) ? classInfo.ClassName : "") : "",
                    (student != null) ? student.SeatNo : "",
                    urHelper.GetText("StudentNumber"),
                    urHelper.GetText("Name"),
                    (student != null) ? student.Gender : "",
                    urHelper.GetText("UpdateCode"),
                    urHelper.GetText("UpdateDescription"),
                    "",//urHelper.GetText("SchoolYear"),
                    "",//urHelper.GetText("Semester"),
                    urHelper.GetText("ADNumber")
                );
                dataGridViewX1.Rows.Add(row);
            }

            dataGridViewX1.ResumeLayout();
        }

        private bool IsDateTime(string date)
        {
            DateTime try_value;
            if (DateTime.TryParse(date, out try_value))
                return true;
            return false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            bool valid = true;
            errorProvider1.Clear();

            if (!IsDateTime(txtStartDate.Text))
            {
                errorProvider1.SetError(txtStartDate, "日期格式錯誤");
                valid = false;
            }
            else
                _startDate = DateTime.Parse(txtStartDate.Text);

            if (!IsDateTime(txtEndDate.Text))
            {
                errorProvider1.SetError(txtEndDate, "日期格式錯誤");
                valid = false;
            }
            else
                _endDate = DateTime.Parse(txtEndDate.Text);

            if (valid && !_loader.IsBusy)
            {
                dataGridViewX1.Rows.Clear();
                _loader.RunWorkerAsync();
            }
        }

        private void btnTypeFilter_Click(object sender, EventArgs e)
        {
            _typeForm.ShowDialog();
        }

        private void UpdateRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            XmlElement pref = new XmlDocument().CreateElement("異動資料檢視_異動代碼");
            foreach (string code in _typeForm.CodeList)
            {
                XmlElement codeElement = pref.OwnerDocument.CreateElement("Code");
                codeElement.InnerText = code;
                pref.AppendChild(codeElement);
            }
            SmartSchool.Customization.Data.SystemInformation.Preference["異動資料檢視_異動代碼"] = pref;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            DataGridViewExport export = new DataGridViewExport(dataGridViewX1);
            export.Save(saveFileDialog1.FileName);

            if (new CompleteForm().ShowDialog() == DialogResult.Yes)
                System.Diagnostics.Process.Start(saveFileDialog1.FileName);
        }
    }
}