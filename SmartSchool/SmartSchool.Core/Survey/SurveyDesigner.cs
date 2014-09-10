using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using System.Xml;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature.Survey;

namespace SmartSchool.Survey
{
    public partial class SurveyDesigner : BaseForm
    {
        private SurveyData _survey_data;

        public SurveyDesigner(SurveyData surveyData)
        {
            InitializeComponent();
            _survey_data = surveyData;

            lblSurveyeeType.Text = SurveyData.GetSurveyeeTypeName(_survey_data.SurveyeeType);
            lblSchoolYear.Text = surveyData.SchoolYear;
            lblSemester.Text = surveyData.Semester;
            txtName.Text = surveyData.Name;
            txtComment.Text = surveyData.Comment;

            DisplayQuestions(surveyData.Questions);
        }

        private void DisplayQuestions(XmlElement xmlElement)
        {
            if (xmlElement == null)
                return;

            foreach (XmlElement each in new DSXmlHelper(xmlElement).GetElements("Question"))
            {
                DSXmlHelper question = new DSXmlHelper(each);

                string name = question.GetText("@Name");
                string datatype = question.GetText("@DataType");
                string valuelist = question.GetText("@ValueList");

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgQuestions, name, datatype, valuelist);
                dgQuestions.Rows.Add(row);
            }
        }

        private EnhancedErrorProvider _errors = new EnhancedErrorProvider();

        private void btnSave_Click(object sender, EventArgs e)
        {
            _errors.Clear();

            if (string.IsNullOrEmpty(txtName.Text))
            {
                _errors.SetError(txtName, "請輸入問卷名稱。");
                return;
            }

            _survey_data.Name = txtName.Text;
            _survey_data.Comment = txtComment.Text;
            _survey_data.Questions = GenerateQuestions();

            DSXmlHelper request = new DSXmlHelper("Request");
            request.AddElement("Survey");
            request.AddElement("Survey", "Name", _survey_data.Name);
            request.AddElement("Survey", "Comment", _survey_data.Comment);
            request.AddElement("Survey", "Content", _survey_data.Questions.OuterXml, true);

            if (string.IsNullOrEmpty(_survey_data.Identity))
            {
                request.AddElement("Survey", "SurveyeeType", _survey_data.SurveyeeType.ToString());
                request.AddElement("Survey", "SchoolYear", _survey_data.SchoolYear);
                request.AddElement("Survey", "Semester", _survey_data.Semester);

                _survey_data.Identity = AddSurvey.InsertSurvey(request);
            }
            else
            {
                request.AddElement("Survey", "Condition");
                request.AddElement("Survey/Condition", "ID", _survey_data.Identity);

                EditSurvey.UpdateSurvey(request);
            }

            Close();
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

        private void dgQuestions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgQuestions.BeginEdit(true);
        }
    }
}