using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.StudentRelated;
using SmartSchool.StudentRelated.RibbonBars.AttendanceEditor;
using DevComponents.DotNetBar;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.ApplicationLog;

namespace SmartSchool.StudentRelated.Palmerworm
{
    public partial class InsertEditor : BaseForm
    {
        public event EventHandler DataSaved;

        private List<BriefStudentData> _students;
        private bool _meritflag;
        private ISemester _semesterProvider;
        private ErrorProvider _errorProvider;
        private Discipline _discipline;

        //log �ݭn�Ψ쪺
        private Dictionary<string, string> beforeData = new Dictionary<string, string>();
        private Dictionary<string, string> afterData = new Dictionary<string, string>();

        public InsertEditor(List<BriefStudentData> students, bool meritflag)
        {
            Initialize(students, meritflag, null);
        }

        public InsertEditor(List<BriefStudentData> students, bool meritflag, Discipline discipline)
        {
            Initialize(students, meritflag, discipline);
        }

        private void Initialize(List<BriefStudentData> students, bool meritflag, Discipline discipline)
        {
            InitializeComponent();
            _errorProvider = new ErrorProvider();
            _meritflag = meritflag;
            _students = students;
            _semesterProvider = SemesterProvider.GetInstance();
            _semesterProvider.SetDate(DateTime.Today);
            int schoolYear = _semesterProvider.SchoolYear;
            int semester = _semesterProvider.Semester;

            for (int i = schoolYear; i > schoolYear - 3; i--)
            {
                cboSchoolYear.Items.Add(i.ToString());
            }
            cboSchoolYear.Text = schoolYear.ToString();

            cboSemester.Items.Add("1");
            cboSemester.Items.Add("2");
            cboSemester.Text = semester.ToString();

            if (_meritflag)
            {
                lbl1.Text = "�j�\";
                lbl2.Text = "�p�\";
                lbl3.Text = "�ż�";
                Text = "���y�޲z";
            }
            else
            {
                lbl1.Text = "�j�L";
                lbl2.Text = "�p�L";
                lbl3.Text = "ĵ�i";
                Text = "�g�ٺ޲z";
            }
            dateText.SetDate(DateTime.Today.ToShortDateString());
            _discipline = discipline;

            //log �������e�����g���A
            if (_discipline != null)
            {
                beforeData.Add("�Ǧ~��", _discipline.SchoolYear);
                beforeData.Add("�Ǵ�", _discipline.Semester);
                beforeData.Add(lbl1.Text, _discipline.A);
                beforeData.Add(lbl2.Text, _discipline.B);
                beforeData.Add(lbl3.Text, _discipline.C);
                beforeData.Add("���", _discipline.OccurDate);
                beforeData.Add("�ƥ�", _discipline.Reason);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboSchoolYear_Validated(object sender, EventArgs e)
        {
            _errorProvider.SetError(cboSchoolYear, null);
            int i;
            if (!int.TryParse(cboSchoolYear.Text, out i))
                _errorProvider.SetError(cboSchoolYear, "�Ǧ~�ץ�������ƼƦr");
        }

        private void cboSemester_Validated(object sender, EventArgs e)
        {
            _errorProvider.SetError(cboSemester, null);
            if (cboSemester.Text != "1" && cboSemester.Text != "2")
                _errorProvider.SetError(cboSemester, "�Ǵ�������J1��2");
        }

        private void dateText_Validated(object sender, EventArgs e)
        {
            _errorProvider.SetError(dateText, null);
            if (!dateText.IsValid)
                _errorProvider.SetError(dateText, "����榡�����T");
        }

        private void txt1_Validated(object sender, EventArgs e)
        {
            _errorProvider.SetError(txt1, null);
            if (string.IsNullOrEmpty(txt1.Text))
                return;
            int i;
            if (!int.TryParse(txt1.Text, out i))
                _errorProvider.SetError(txt1, "��������ƼƦr");
        }

        private void txt2_Validated(object sender, EventArgs e)
        {
            _errorProvider.SetError(txt2, null);
            if (string.IsNullOrEmpty(txt2.Text))
                return;
            int i;
            if (!int.TryParse(txt2.Text, out i))
                _errorProvider.SetError(txt2, "��������ƼƦr");
        }

        private void txt3_Validated(object sender, EventArgs e)
        {
            _errorProvider.SetError(txt3, null);
            if (string.IsNullOrEmpty(txt3.Text))
                return;
            int i;
            if (!int.TryParse(txt3.Text, out i))
                _errorProvider.SetError(txt3, "��������ƼƦr");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool valid = true;
            foreach (Control control in this.Controls)
                if (!string.IsNullOrEmpty(_errorProvider.GetError(control)))
                    valid = false;

            if (!valid)
            {
                MsgBox.Show("������ҿ��~�A�Х��ץ���A���x�s");
                return;
            }

            //�ˬd�ϥΪ̬O�_�ѰO��J�\�L���ơC
            int sum = int.Parse(GetTextValue(txt1.Text)) + int.Parse(GetTextValue(txt2.Text)) + int.Parse(GetTextValue(txt3.Text));
            if (sum <= 0)
            {
                MsgBox.Show("�ЧO�ѤF��J�\�L���ơC");
                return;
            }

            //�ˬd�ǥͤ��O�_�������Z�ǥ͡A�Φ~���ˬd�C
            //foreach (BriefStudentData each_stu in _students)
            //{
            //    if (string.IsNullOrEmpty(each_stu.GradeYear))
            //    {
            //        MsgBox.Show("�s�W���ѡA�����ǥͥ��ݩ����Z�šA�Х��T�{�ǥͽs�Z���p�C");
            //        return;
            //    }
            //}

            if (_discipline == null)
                Insert();
            else
                Modify();


            List<string> idList = new List<string>();
            foreach ( BriefStudentData var in _students )
            {
                if ( !idList.Contains(var.ID) )
                    idList.Add(var.ID);
            }
            Student.Instance.InvokDisciplineChanged(idList.ToArray()); ;
            if (DataSaved != null)
                DataSaved(this, EventArgs.Empty);
            MsgBox.Show("�x�s����");
            this.Close();
        }

        private void Modify()
        {
            DSXmlHelper h = new DSXmlHelper("Discipline");
            if (_meritflag)
            {
                XmlElement element = h.AddElement("Merit");
                element.SetAttribute("A", GetTextValue(txt1.Text));
                element.SetAttribute("B", GetTextValue(txt2.Text));
                element.SetAttribute("C", GetTextValue(txt3.Text));
            }
            else
            {
                XmlElement element = h.AddElement("Demerit");
                element.SetAttribute("A", GetTextValue(txt1.Text));
                element.SetAttribute("B", GetTextValue(txt2.Text));
                element.SetAttribute("C", GetTextValue(txt3.Text));
                element.SetAttribute("Cleared", "");
                element.SetAttribute("ClearDate", "");
                element.SetAttribute("ClearReason", "");
            }

            DSXmlHelper helper = new DSXmlHelper("UpdateRequest");
            foreach (BriefStudentData student in _students)
            {
                helper.AddElement("Discipline");
                helper.AddElement("Discipline", "Field");
                helper.AddElement("Discipline/Field", "RefStudentID", student.ID);
                helper.AddElement("Discipline/Field", "SchoolYear", cboSchoolYear.Text);
                helper.AddElement("Discipline/Field", "Semester", cboSemester.Text);
                //helper.AddElement("Discipline/Field", "GradeYear", (student.GradeYear == "�����~��") ? "" : student.GradeYear);
                helper.AddElement("Discipline/Field", "OccurDate", dateText.DateString);
                helper.AddElement("Discipline/Field", "Reason", txtReason.Text);
                helper.AddElement("Discipline/Field", "MeritFlag", Convert.ToInt32(_meritflag).ToString());
                helper.AddElement("Discipline/Field", "Type", "1");
                helper.AddElement("Discipline/Field", "Detail", h.GetRawXml(), true);
                helper.AddElement("Discipline", "Condition");
                helper.AddElement("Discipline/Condition", "ID", _discipline.Id);

                //log �����ק�᪺���g���A
                if (_discipline != null)
                {
                    afterData.Add("�Ǧ~��", cboSchoolYear.Text);
                    afterData.Add("�Ǵ�", cboSemester.Text);
                    afterData.Add(lbl1.Text, GetTextValue(txt1.Text));
                    afterData.Add(lbl2.Text, GetTextValue(txt2.Text));
                    afterData.Add(lbl3.Text, GetTextValue(txt3.Text));
                    afterData.Add("���", dateText.DateString);
                    afterData.Add("�ƥ�", txtReason.Text);
                }
            }

            try
            {
                SmartSchool.Feature.Student.EditDiscipline.Update(new DSRequest(helper));

                //�ק���g���� log
                foreach (BriefStudentData student in _students)
                {
                    bool dirty = false;
                    StringBuilder desc = new StringBuilder("");
                    desc.AppendLine("�ǥͩm�W�G" + Student.Instance.Items[student.ID].Name + " ");
                    //desc.AppendLine(CurrentUser.Instance.SchoolYear + " �Ǧ~�� �� " + CurrentUser.Instance.Semester + " �Ǵ� ");
                    desc.AppendLine("����G" + dateText.Text + " ");

                    foreach (string key in beforeData.Keys)
                    {
                        if (beforeData[key] != afterData[key])
                        {
                            dirty = true;
                            desc.AppendLine("�u" + key + "�v�ѡu" + beforeData[key] + "�v�ܧ󬰡u" + afterData[key] + "�v ");
                        }
                    }

                    if (dirty)
                        CurrentUser.Instance.AppLog.Write(EntityType.Student, "�ק���g����", student.ID, desc.ToString(), Text, helper.GetRawXml());
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show("��ƭק異�ѡG" + ex.Message, "���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void Insert()
        {
            DSXmlHelper h = new DSXmlHelper("Discipline");
            if (_meritflag)
            {
                XmlElement element = h.AddElement("Merit");
                element.SetAttribute("A", GetTextValue(txt1.Text));
                element.SetAttribute("B", GetTextValue(txt2.Text));
                element.SetAttribute("C", GetTextValue(txt3.Text));
            }
            else
            {
                XmlElement element = h.AddElement("Demerit");
                element.SetAttribute("A", GetTextValue(txt1.Text));
                element.SetAttribute("B", GetTextValue(txt2.Text));
                element.SetAttribute("C", GetTextValue(txt3.Text));
                element.SetAttribute("Cleared", "");
                element.SetAttribute("ClearDate", "");
                element.SetAttribute("ClearReason", "");
            }

            DSXmlHelper helper = new DSXmlHelper("InsertRequest");
            foreach (BriefStudentData student in _students)
            {
                helper.AddElement("Discipline");
                helper.AddElement("Discipline", "RefStudentID", student.ID);
                helper.AddElement("Discipline", "SchoolYear", cboSchoolYear.Text);
                helper.AddElement("Discipline", "Semester", cboSemester.Text);
                //helper.AddElement("Discipline", "GradeYear", (student.GradeYear == "�����~��") ? "" : student.GradeYear);
                helper.AddElement("Discipline", "OccurDate", dateText.DateString);
                helper.AddElement("Discipline", "Reason", txtReason.Text);
                helper.AddElement("Discipline", "MeritFlag", Convert.ToInt32(_meritflag).ToString());
                helper.AddElement("Discipline", "Type", "1");
                helper.AddElement("Discipline", "Detail", h.GetRawXml(), true);
            }

            try
            {
                SmartSchool.Feature.Student.EditDiscipline.Insert(new DSRequest(helper));

                //�s�W���g���� log
                foreach (BriefStudentData student in _students)
                {
                    StringBuilder desc = new StringBuilder("");
                    desc.AppendLine("�ǥͩm�W�G" + Student.Instance.Items[student.ID].Name + " ");
                    //desc.AppendLine(CurrentUser.Instance.SchoolYear + " �Ǧ~�� �� " + CurrentUser.Instance.Semester + " �Ǵ� ");
                    desc.AppendLine("����G" + dateText.Text + " ");
                    if (int.Parse(GetTextValue(txt1.Text)) > 0)
                        desc.AppendLine(lbl1.Text + "�G" + GetTextValue(txt1.Text) + " ");
                    if (int.Parse(GetTextValue(txt2.Text)) > 0)
                        desc.AppendLine(lbl2.Text + "�G" + GetTextValue(txt2.Text) + " ");
                    if (int.Parse(GetTextValue(txt3.Text)) > 0)
                        desc.AppendLine(lbl3.Text + "�G" + GetTextValue(txt3.Text) + " ");
                    desc.AppendLine(Text.Substring(0, 2) + "�ƥѡG" + txtReason.Text);
                    CurrentUser.Instance.AppLog.Write(EntityType.Student, "�s�W���g����", student.ID, desc.ToString(), Text, helper.GetRawXml());
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show("��Ʒs�W���ѡG" + ex.Message);
                return;
            }
        }

        private string GetTextValue(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "0";
            return text;
        }

        private void InsertEditor_Load(object sender, EventArgs e)
        {
            DSResponse dsrsp = SmartSchool.Feature.Basic.Config.GetDisciplineReasonList();
            cboReasonRef.SelectedItem = null;
            cboReasonRef.Items.Clear();
            DSXmlHelper helper = dsrsp.GetContent();
            KeyValuePair<string, string> fkvp = new KeyValuePair<string, string>("", "");
            cboReasonRef.Items.Add(fkvp);

            foreach (XmlElement element in helper.GetElements("Reason"))
            {
                if (element.GetAttribute("Type") == "�g�|" && _meritflag) continue;
                if (element.GetAttribute("Type") == "�g��" && _meritflag) continue;
                if (element.GetAttribute("Type") == "���y" && !_meritflag) continue;
                if (element.GetAttribute("Type") == "���L") continue;
                if (element.GetAttribute("Type") == "�P�L") continue;

                string k = element.GetAttribute("Code") + "-" + element.GetAttribute("Description");
                string v = element.GetAttribute("Description");
                KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(k, v);
                cboReasonRef.Items.Add(kvp);
            }
            cboReasonRef.DisplayMember = "Key";
            cboReasonRef.ValueMember = "Value";
            cboReasonRef.SelectedIndex = 0;

            if (_discipline == null) return;
            cboSchoolYear.Text = _discipline.SchoolYear;
            cboSemester.Text = _discipline.Semester;
            dateText.SetDate(_discipline.OccurDate);
            txtReason.Text = _discipline.Reason;
            txt1.Text = _discipline.A;
            txt2.Text = _discipline.B;
            txt3.Text = _discipline.C;
        }

        private void cboReasonRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)cboReasonRef.SelectedItem;
            txtReason.Text = kvp.Value;
        }
    }

    public class Discipline
    {
        private string _occurDate;

        public string OccurDate
        {
            get { return _occurDate; }
            set { _occurDate = value; }
        }
        private string _a, _b, _c;

        public string C
        {
            get { return _c; }
            set { _c = value; }
        }

        public string B
        {
            get { return _b; }
            set { _b = value; }
        }

        public string A
        {
            get { return _a; }
            set { _a = value; }
        }
        private string _reason;

        public string Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }

        private string _schoolYear;

        public string SchoolYear
        {
            get { return _schoolYear; }
            set { _schoolYear = value; }
        }
        private string _semester;

        public string Semester
        {
            get { return _semester; }
            set { _semester = value; }
        }
        private string _gradeYear;

        public string GradeYear
        {
            get { return _gradeYear; }
            set { _gradeYear = value; }
        }
        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Discipline()
        { }

        public override string ToString()
        {
            return _id;
        }

        private bool _cleared;

        public bool Cleared
        {
            get { return _cleared; }
            set { _cleared = value; }
        }
        private string _clearDate;

        public string ClearDate
        {
            get { return _clearDate; }
            set { _clearDate = value; }
        }
        private string _clearReason;

        public string ClearReason
        {
            get { return _clearReason; }
            set { _clearReason = value; }
        }

        private bool _isAsshole;

        public bool IsAsshole
        {
            get { return _isAsshole; }
            set { _isAsshole = value; }
        }
    }
}