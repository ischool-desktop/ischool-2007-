using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.Common.DateTimeProcess;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.Feature.Survey;
using SmartSchool.Customization.Data;

namespace SmartSchool.Survey
{
    public partial class SurveyWizard : BaseForm
    {
        private EnhancedErrorProvider _errors = new EnhancedErrorProvider();

        public SurveyWizard()
        {
            InitializeComponent();
            _survey_data = new SurveyData();

            lblSchoolYear.Text = SmartSchool.Customization.Data.SystemInformation.SchoolYear.ToString();
            lblSemester.Text = SmartSchool.Customization.Data.SystemInformation.Semester.ToString();
        }

        private SurveyData _survey_data;
        public SurveyData Data
        {
            get { return _survey_data; }
        }

        private void wizardPage1_NextButtonClick(object sender, CancelEventArgs e)
        {
            _errors.Clear();

            if (IsValidDateData())
                _errors.SetError(txtExpireation, "日期格式錯誤。");

            if (string.IsNullOrEmpty(txtName.Text))
                _errors.SetError(txtName, "請輸入問卷名稱。");

            if (_errors.HasError)
            {
                e.Cancel = true;
                return;
            }

            Data.SchoolYear = lblSchoolYear.Text;
            Data.Semester = lblSemester.Text;
            Data.Name = txtName.Text;
            Data.Expireation = txtExpireation.Text;
            Data.Comment = txtComment.Text;
        }

        private void wizardPage2_FinishButtonClick(object sender, CancelEventArgs e)
        {
            try
            {
                Data.Questions = GenerateQuestions();

                Data.SurveyeeType = chkClassStudent.Checked ? SurveyeeType.ClassStudent : SurveyeeType.Class;

                DSXmlHelper reqSurvey = new DSXmlHelper("Request");
                reqSurvey.AddElement("Survey");
                reqSurvey.AddElement("Survey", "Name", Data.Name);
                reqSurvey.AddElement("Survey", "Comment", Data.Comment);
                reqSurvey.AddElement("Survey", "Content", Data.Questions.OuterXml, true);
                reqSurvey.AddElement("Survey", "SurveyeeType", Data.SurveyeeType.ToString());
                reqSurvey.AddElement("Survey", "SchoolYear", Data.SchoolYear);
                reqSurvey.AddElement("Survey", "Semester", Data.Semester);

                Data.Identity = AddSurvey.InsertSurvey(reqSurvey);

                AccessHelper ds = new AccessHelper();
                List<ClassRecord> classes = ds.ClassHelper.GetSelectedClass();

                List<DSXmlHelper> reqSurveyRequests = new List<DSXmlHelper>();
                DSXmlHelper reqSurveyRequest = null;
                int packagesize = 50, currentsize = 0;

                if (chkClassStudent.Checked)
                {
                    foreach (ClassRecord each in classes)
                    {
                        foreach (StudentRecord eachStu in each.Students)
                        {
                            if (currentsize % packagesize == 0)
                            {
                                reqSurveyRequest = new DSXmlHelper("Request");
                                reqSurveyRequests.Add(reqSurveyRequest);
                            }

                            DSXmlHelper recrsp = new DSXmlHelper(reqSurveyRequest.AddElement("SurveyResponse"));
                            recrsp.AddElement(".", "RefSurveyID", Data.Identity);
                            recrsp.AddElement(".", "RefSurveyeeID", eachStu.StudentID);
                            recrsp.AddElement(".", "RefCollectorID", each.ClassID);

                            currentsize++;
                        }
                    }
                }
                else
                {
                    foreach (ClassRecord each in classes)
                    {
                        if (currentsize % packagesize == 0)
                        {
                            reqSurveyRequest = new DSXmlHelper("Request");
                            reqSurveyRequests.Add(reqSurveyRequest);
                        }

                        DSXmlHelper recrsp = new DSXmlHelper(reqSurveyRequest.AddElement("SurveyResponse"));
                        recrsp.AddElement(".", "RefSurveyID", Data.Identity);
                        recrsp.AddElement(".", "RefSurveyeeID", each.ClassID);
                        recrsp.AddElement(".", "RefCollectorID", each.ClassID);

                        currentsize++;
                    }
                }

                foreach (DSXmlHelper each in reqSurveyRequests)
                    EditSurvey.InsertSurveyResponse(each);

                Close();
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                MsgBox.Show(ex.Message);
            }

        }

        private XmlElement GenerateQuestions()
        {
            DSXmlHelper questions = new DSXmlHelper("Questions");

            foreach (DataGridViewRow each in dgQuestions.Rows)
            {
                if (each.IsNewRow) continue;

                XmlElement question = questions.AddElement("Question");

                question.SetAttribute("Name", each.Cells["chQuestion"].Value + "");
                question.SetAttribute("DataType", each.Cells["chData"].Value + "");
                question.SetAttribute("ValueList", each.Cells["chValueList"].Value + "");
            }

            return questions.BaseElement;
        }

        private bool IsValidDateData()
        {
            bool notEmpty = !string.IsNullOrEmpty(txtExpireation.Text);
            bool isValidData = !DateTimeHelper.IsValidDate(txtExpireation.Text);

            return notEmpty && isValidData;
        }

        private void dgQuestions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgQuestions.BeginEdit(false);
        }
    }
}