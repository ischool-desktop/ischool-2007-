using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using SmartSchool.StudentRelated;
using SmartSchool.StudentRelated.Placing.Rule;
using SmartSchool.StudentRelated.Placing.DataSource;
using System.Xml;
using SmartSchool.StudentRelated.Placing.Control;
using SmartSchool.StudentRelated.Placing.Score;
using SmartSchool.StudentRelated.Placing.DataSource.Formater;
using SmartSchool.StudentRelated.Placing.Export;
using SmartSchool.StudentRelated.Placing.Rule.ScoreDependance;
using DevComponents.DotNetBar;

namespace SmartSchool.StudentRelated.Placing
{
    public partial class PlaceForm : BaseForm
    {
        private const string �Ǵ���ئ��Z = "�Ǵ���ئ��Z";
        private const string �Ǵ��������Z = "�Ǵ��������Z";
        private const string �Ǧ~��ئ��Z = "�Ǧ~��ئ��Z";
        private const string �Ǧ~�������Z = "�Ǧ~�������Z";
        private const string ���~�������Z = "���~�������Z";
        List<string> _studentidList;

        private IDataFormater _dataFormater;
        private IScoreDependance _scoreDependance;
        private IPlacingRule _placingRule;
        private XmlElement _source;

        public PlaceForm()
        {
            InitializeComponent();

            _studentidList = new List<string>();
            foreach (BriefStudentData student in SmartSchool.StudentRelated.Student.Instance.SelectionStudents)
            {
                _studentidList.Add(student.ID);
            }

            cboType.Items.Add(�Ǵ���ئ��Z);
            cboType.Items.Add(�Ǵ��������Z);
            cboType.Items.Add(�Ǧ~��ئ��Z);
            cboType.Items.Add(�Ǧ~�������Z);
            //cboType.Items.Add(���~�������Z);
            cboType.SelectedIndex = 0;

            rbDupicate.Checked = true;
            chkOrigin.Checked = true;
            chkSubject.Checked = true;

            int sy = SmartSchool.CurrentUser.Instance.SchoolYear;
            for (int i = sy; i > sy - 4; i--)
            {
                cboSchoolYear.Items.Add(i);
            }
            cboSchoolYear.Text = sy.ToString();

            cboSemester.Items.Add(1);
            cboSemester.Items.Add(2);
            cboSemester.Text = SmartSchool.CurrentUser.Instance.Semester.ToString();

        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboType.Text == �Ǵ���ئ��Z)
            {
                cboSemester.Enabled = true;
                gpSource.Enabled = true;
                gpScoreType.Enabled = true;
            }
            else if (cboType.Text == �Ǵ��������Z)
            {
                cboSemester.Enabled = true;
                gpSource.Enabled = false;
                gpScoreType.Enabled = false;
            }
            else if (cboType.Text == �Ǧ~��ئ��Z)
            {
                cboSemester.Enabled = false;
                gpSource.Enabled = true;
                gpScoreType.Enabled = true;
            }
            else if (cboType.Text == �Ǧ~�������Z)
            {
                cboSemester.Enabled = false;
                gpSource.Enabled = false;
                gpScoreType.Enabled = false;
            }
            else
            {
                cboSemester.Enabled = false;
                gpSource.Enabled = false;
            }

            LoadData();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private long t1;
        private void LoadData()
        {
            if (!string.IsNullOrEmpty(errorProvider1.GetError(cboSemester)) || !string.IsNullOrEmpty(errorProvider1.GetError(cboSchoolYear)))
                return;

            t1 = Environment.TickCount;

            pictureBox1.Visible = true;
            dataGridViewX1.Rows.Clear();
            IDataProvider dp;
            if (cboType.Text == �Ǵ���ئ��Z)
            {
                if (string.IsNullOrEmpty(cboSchoolYear.Text) || string.IsNullOrEmpty(cboSemester.Text)) return;
                dp = new SemesterDataProvider(cboSchoolYear.Text, cboSemester.Text, _studentidList);
            }
            else if (cboType.Text == �Ǧ~��ئ��Z)
            {
                if (string.IsNullOrEmpty(cboSchoolYear.Text)) return;
                dp = new SchoolYearDataProvider(cboSchoolYear.Text, _studentidList);
            }
            else if (cboType.Text == �Ǵ��������Z)
            {
                if (string.IsNullOrEmpty(cboSchoolYear.Text) || string.IsNullOrEmpty(cboSemester.Text)) return;
                dp = new SemesterEntryDataProvider(cboSchoolYear.Text, cboSemester.Text, _studentidList);
            }
            else if (cboType.Text == �Ǧ~�������Z)
            {
                if (string.IsNullOrEmpty(cboSchoolYear.Text)) return;
                dp = new SchoolYearEntryDataProvider(cboSchoolYear.Text, _studentidList);
            }
            else
            {
                if (string.IsNullOrEmpty(cboSchoolYear.Text) || string.IsNullOrEmpty(cboSemester.Text)) return;
                dp = new SemesterDataProvider(cboSchoolYear.Text, cboSemester.Text, _studentidList);
            }
                       
            dp.DataLoaded += new EventHandler<DataLoadEventHandler>(dp_DataLoaded);
            dp.GetData();            
        }

        void dp_DataLoaded(object sender, DataLoadEventHandler e)
        {
            long t2 = Environment.TickCount;

            _source = e.Result;
            ISubjectStatistic subject;
            if (cboType.Text == �Ǵ���ئ��Z)
                subject = new SemesterSubjectStatistic(_source);
            else if (cboType.Text == �Ǧ~��ئ��Z)
                subject = new SchoolYearSubjectStatistic(_source);
            else if (cboType.Text == �Ǵ��������Z)
                subject = new SemesterEntryStatistic(_source);
            else if (cboType.Text == �Ǧ~�������Z)
                subject = new SchoolYearEntryStatistic(_source);
            else
                subject = new SemesterSubjectStatistic(_source);


            SubjectInfoCollection collection = subject.Statistic();
            foreach (SubjectInfo info in collection)
            {
                int rowIndex = dataGridViewX1.Rows.Add();
                DataGridViewRow row = dataGridViewX1.Rows[rowIndex];
                row.Cells[colSelected.Name].Value = false;
                row.Cells[colSubject.Name].Value = info.SubjectName;
                //row.Cells[colCount.Name].Value = info.StudentCount;
                row.Cells[colWeight.Name].Value = 1;
            }
            pictureBox1.Visible = false;
            long t3 = Environment.TickCount;
            // lblMessage.Text = "RT:" + (t2 - t1) + "  ST:" + (t3 - t2);
        }

        private void cboSchoolYear_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(cboSchoolYear, string.Empty);
            string value = cboSchoolYear.Text;
            int y;
            if (!int.TryParse(value, out y))
                errorProvider1.SetError(cboSchoolYear, "�������Ʀr");
            lblMessage.Text = DogmaticBill.GetRomanNumber(cboSchoolYear.Text);
            LoadData();
        }

        private void cboSemester_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(cboSemester, string.Empty);
            if (cboType.Text.IndexOf("�Ǵ�") == -1) return;
            if (cboSemester.Text != "1" && cboSemester.Text != "2")
                errorProvider1.SetError(cboSemester, "������1��2");
            LoadData();
        }

        private void rbDupicate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDupicate.Checked)
                _placingRule = new DuplicatePlacingRule();
        }

        private void rbUndupicate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUndupicate.Checked)
                _placingRule = new UnduplicatePlacingRule();
        }

        private IDataFormater GetDataFormater()
        {
            IDataFormater formater = null;
            
            #region �¼g�k
            //if (cboType.Text == �Ǵ���ئ��Z && rbSourceYS.Checked)
            //{
            //    formater = new SemesterScoreOriginDataFormater();
            //}
            //else if (cboType.Text == �Ǵ���ئ��Z && rbSourceBK.Checked)
            //{
            //    formater = new SemesterScoreResitDataFormater();
            //}
            //else if (cboType.Text == �Ǵ���ئ��Z && rbSourceCX.Checked)
            //{
            //    formater = new SemesterScoreRepeatDataFormater();
            //}
            //else if (cboType.Text == �Ǵ���ئ��Z && rbSourceZY.Checked)
            //{
            //    formater = new SemesterScoreBestDataFormater();
            //}
            //else if (cboType.Text == �Ǵ���ئ��Z && rbSourceTZ.Checked)
            //{
            //    formater = new SemesterScoreModifyDataFormater();
            //}
            #endregion

            if (cboType.Text == �Ǵ���ئ��Z)
            {
                ChoiceScoreTypeArg arg = new ChoiceScoreTypeArg();
                if (chkOrigin.Checked)
                    arg.Join(ChoiceScoreType.��l���Z);
                if (chkResit.Checked)
                    arg.Join(ChoiceScoreType.�ɦҦ��Z);
                if (chkRepeat.Checked)
                    arg.Join(ChoiceScoreType.���צ��Z);
                formater = new SemesterScoreChoiceDataFormater(arg);
            }
            else if (cboType.Text == �Ǧ~��ئ��Z)
            {
                formater = new SchoolYearScoreDataFormater();
            }
            else if (cboType.Text == �Ǵ��������Z)
            {
                formater = new SemesterEntryScoreDataFormater();
            }
            else if (cboType.Text == �Ǧ~�������Z)
            {
                formater = new SchoolYearEntryScoreDataFormater();
            }
            else
            {
                formater = new SemesterScoreOriginDataFormater();
            }
            return formater;
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            if (!IsValid()) return;
            _dataFormater = GetDataFormater();
            StudentSemesterScoreRecordCollection collection = _dataFormater.Format(_source);
            ExcelExporter exporter = new ExcelExporter();
            IList<SubjectWeight> subjects = GetSelectedSubject();
            if (chkSubject.Checked)
            {
                foreach (SubjectWeight sw in subjects)
                {
                    string subjectName = sw.SubjectName;
                    IScoreDependance sd = new SubjectScoreDependance(subjectName);
                    IList<PlacingInfo> placing = _placingRule.GetPlacingInfo(collection, sd);
                    ExcelSheetInfo si = new ExcelSheetInfo("�i" +subjectName + "�j��رƦW", placing, SheetType.Nomal);
                    exporter.Add(si);
                }
            }

            if (chkTotal.Checked)
            {
                IScoreDependance sd = new WeightTotalScoreDependance(subjects);
                IList<PlacingInfo> placing = _placingRule.GetPlacingInfo(collection, sd);
                ExcelSheetInfo si = new ExcelSheetInfo("�[�v�`���ƦW", placing, SheetType.Nomal);
                exporter.Add(si);
            }

            if (chkAvg1.Checked)
            {
                IScoreDependance sd = new WeightAvgScoreDependance(subjects);
                IList<PlacingInfo> placing = _placingRule.GetPlacingInfo(collection, sd);
                ExcelSheetInfo si = new ExcelSheetInfo("�[�v�����ƦW(�L���Z���p��)", placing, SheetType.Nomal);
                exporter.Add(si);
            }

            if (chkAvg2.Checked)
            {
                IScoreDependance sd = new WeightAvgWhateverScoreDependance(subjects);
                IList<PlacingInfo> placing = _placingRule.GetPlacingInfo(collection, sd);
                ExcelSheetInfo si = new ExcelSheetInfo("�[�v�����ƦW(�L���Z�C�J�p��)", placing, SheetType.Nomal);
                exporter.Add(si);
            }

            exporter.Export();
            exporter.Save();
        }

        private IList<SubjectWeight> GetSelectedSubject()
        {
            List<SubjectWeight> list = new List<SubjectWeight>();
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                bool selected;
                string value = row.Cells[colSelected.Name].Value == null ? "false" : row.Cells[colSelected.Name].Value.ToString();
                if (!bool.TryParse(value, out selected))
                    selected = false;
                if (!selected) continue;
                string subjectName = row.Cells[colSubject.Name].Value.ToString();
                string w = row.Cells[colWeight.Name].Value == null ? "1" : row.Cells[colWeight.Name].Value.ToString();
                decimal weight;
                if (!decimal.TryParse(w, out weight))
                    weight = 1;
                SubjectWeight sw = new SubjectWeight(subjectName, weight);
                list.Add(sw);
            }
            return list;
        }
        private bool IsValid()
        {
            if (GetSelectedSubject().Count == 0)
            {
                MsgBox.Show("������ܬ��", "���ҥ���", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (!chkAvg1.Checked && !chkAvg2.Checked && !chkSubject.Checked && !chkTotal.Checked)
            {
                MsgBox.Show("������ܱƦW���Z�ﶵ", "���ҥ���", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void dataGridViewX1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != colWeight.Index) return;
            if (e.RowIndex == -1) return;
            DataGridViewCell cell = dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string value = cell.Value == null ? "1" : cell.Value.ToString();
            decimal w;
            if (!decimal.TryParse(value, out w))
                w = 1;
            cell.Value = w.ToString();
        }

        private bool _selected = false;
        private void dataGridViewX1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn column = dataGridViewX1.Columns[e.ColumnIndex];
            if (column != colSelected) return;
            _selected = !_selected;
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                row.Cells[colSelected.Name].Value = _selected;
            }
        }
    }
}