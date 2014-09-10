using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SysInfo = SmartSchool.Customization.Data.SystemInformation;
using SmartSchool.Feature.Survey;
using IntelliSchool.DSA30.Util;
using System.Xml;

namespace SmartSchool.Survey
{
    public partial class SurveyManager : BaseForm
    {
        public SurveyManager()
        {
            InitializeComponent();
            InitializeForm();
        }

        protected virtual void InitializeForm()
        {
            InitializeSemester();
            LoadSurveyList();
        }

        protected virtual void AddSurvey()
        {
            List<SurveyeeTypeItem> items = new List<SurveyeeTypeItem>();
            items.Add(new SurveyeeTypeItem(SurveyeeType.Class, "班級", "將問卷傳送給班導師，由班導師回答「針對班級」的問題。"));
            items.Add(new SurveyeeTypeItem(SurveyeeType.ClassStudent, "班級學生", "將問卷傳送給班導師，由班導師回答「針對班上每位學生」的問題。"));

            SelectSurveyeeType stype = new SelectSurveyeeType(items.ToArray());

            if (stype.ShowDialog() == DialogResult.OK)
            {
                SurveyData survey = new SurveyData();
                survey.SurveyeeType = stype.SelectedValue;
                survey.SchoolYear = cboSchoolYear.Text;
                survey.Semester = cboSemester.Text;

                new SurveyDesigner(survey).ShowDialog();

                LoadSurveyList();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddSurvey();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentSurvey == null)
                    return;

                string msg = string.Format("您確定要刪除「{0}」問卷？", CurrentSurvey.Name);
                if (MsgBox.Show(msg, Application.ProductName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    RemoveSurvey.DeleteSurvey(CurrentSurvey.Identity);
                    LoadSurveyList();
                }

            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show(ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
        }

        private void btnEditSurveyee_Click(object sender, EventArgs e)
        {

        }

        private void btnEditQuestion_Click(object sender, EventArgs e)
        {
            if (CurrentSurvey == null) return;

            try
            {
                SurveyData survey = SurveyData.LoadData(CurrentSurvey.Identity);

                new SurveyDesigner(survey).ShowDialog();
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show(ex.Message);
            }
        }

        private void btnSetExpireation_Click(object sender, EventArgs e)
        {
            if (CurrentSurvey == null) return;

            try
            {
                SurveyData sd = SurveyData.LoadData(CurrentSurvey.Identity);

                SetExpireation exp = new SetExpireation(sd.Expireation);
                if (exp.ShowDialog() == DialogResult.OK)
                {
                    DSXmlHelper req = new DSXmlHelper("Request");
                    req.AddElement("Survey");
                    req.AddElement("Survey", "Expireation", exp.Expireation);
                    req.AddElement("Survey", "Condition");
                    req.AddElement("Survey/Condition", "ID", CurrentSurvey.Identity);

                    EditSurvey.UpdateSurvey(req);

                    LoadSurveyList();
                }
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show(ex.Message);
            }
        }

        #region (Process) InitializeSemester
        private void InitializeSemester()
        {
            try
            {
                for (int i = -2; i <= 2; i++) //只顯示前後兩個學年的選項，其他的用手動輸入。
                    cboSchoolYear.Items.Add(SysInfo.SchoolYear + i);

                cboSchoolYear.Text = SysInfo.SchoolYear.ToString();
                cboSemester.Text = SysInfo.Semester.ToString();
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(new Exception("填入學年度學期選項清單時發生錯誤。", ex));
            }
        }
        #endregion

        protected void LoadSurveyList()
        {
            OnSemesterChanged();
        }

        protected virtual void OnSemesterChanged()
        {
            try
            {
                DSXmlHelper rsp = QuerySurvey.GetDetailList(GetTargetType(),
            CurrentSchoolYear,
            CurrentSemester,
            "ID,Name,Expireation");

                dgSurveyList.Rows.Clear();
                foreach (XmlElement each in rsp.GetElements("Survey"))
                {
                    DSXmlHelper hlpeach = new DSXmlHelper(each);
                    string id = hlpeach.GetText("@ID");
                    string name = hlpeach.GetText("Name");
                    string expireation = SurveyData.ToDisplayDate(hlpeach.GetText("Expireation"));
                    string published = (expireation == "" ? "否" : "是");

                    SurveyData survey = new SurveyData();
                    survey.Identity = id;
                    survey.Name = name;
                    survey.Expireation = expireation;

                    DataGridViewRow row = new DataGridViewRow();
                    row.Tag = survey;
                    row.CreateCells(dgSurveyList, name, expireation, published);

                    dgSurveyList.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show(ex.Message);
            }
        }

        protected virtual SurveyeeType[] GetTargetType()
        {
            return new SurveyeeType[] { SurveyeeType.Class, SurveyeeType.ClassStudent };
        }

        public string CurrentSchoolYear
        {
            get { return GetControlIntegerValue(cboSchoolYear, 0).ToString(); }
        }

        public string CurrentSemester
        {
            get { return GetControlIntegerValue(cboSemester, 0).ToString(); }
        }

        public DataGridViewRow CurrentRow
        {
            get
            {
                if (dgSurveyList.SelectedRows.Count > 0)
                    return dgSurveyList.SelectedRows[0];
                else
                    return null;
            }
        }

        public SurveyData CurrentSurvey
        {
            get
            {
                if (CurrentRow == null)
                    return null;

                return CurrentRow.Tag as SurveyData;
            }
        }

        #region Semester Controls Event
        private void cboSchoolYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSemesterChanged();
        }

        private void cboSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSemesterChanged();
        }

        private void cboSchoolYear_TextChanged(object sender, EventArgs e)
        {
            OnSemesterChanged();
        }

        private void cboSemester_TextChanged(object sender, EventArgs e)
        {
            OnSemesterChanged();
        }
        #endregion

        #region Comment Methods
        private int GetControlIntegerValue(Control ctl, int defaultValue)
        {
            int value;

            if (int.TryParse(ctl.Text, out value))
                return value;
            else
                return defaultValue;
        }
        #endregion
    }
}