using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.StudentRelated;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.StudentRelated.RibbonBars.AttendanceEditor;
using DevComponents.DotNetBar;
using SmartSchool.ApplicationLog;

namespace SmartSchool.StudentRelated.RibbonBars.Discipline
{
    public partial class DemeritEditor : BaseForm
    {
        public event EventHandler DataSaved;
        private List<BriefStudentData> _students;
        private bool _meritflag;
        private ISemester _semesterProvider;
        private ErrorProvider _errorProvider;

        public DemeritEditor(List<BriefStudentData> students, bool meritflag)
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
                lbl1.Text = "大功";
                lbl2.Text = "小功";
                lbl3.Text = "嘉獎";
                Text = "獎勵管理";
            }
            else
            {
                lbl1.Text = "大過";
                lbl2.Text = "小過";
                lbl3.Text = "警告";
                Text = "懲戒管理";
            }
            dateText.SetDate(DateTime.Today.ToShortDateString());
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
                _errorProvider.SetError(cboSchoolYear, "學年度必須為整數數字");
        }

        private void cboSemester_Validated(object sender, EventArgs e)
        {
            _errorProvider.SetError(cboSemester, null);
            if (cboSemester.Text != "1" && cboSemester.Text != "2")
                _errorProvider.SetError(cboSemester, "學期必須填入1或2");
        }

        private void dateText_Validated(object sender, EventArgs e)
        {
            _errorProvider.SetError(dateText, null);
            if (!dateText.IsValid)
                _errorProvider.SetError(dateText, "日期格式不正確");
        }

        private void txt1_Validated(object sender, EventArgs e)
        {
            _errorProvider.SetError(txt1, null);
            if (string.IsNullOrEmpty(txt1.Text))
                return;
            int i;
            if (!int.TryParse(txt1.Text, out i))
                _errorProvider.SetError(txt1, "必須為整數數字");
        }

        private void txt2_Validated(object sender, EventArgs e)
        {
            _errorProvider.SetError(txt2, null);
            if (string.IsNullOrEmpty(txt2.Text))
                return;
            int i;
            if (!int.TryParse(txt2.Text, out i))
                _errorProvider.SetError(txt2, "必須為整數數字");
        }

        private void txt3_Validated(object sender, EventArgs e)
        {
            _errorProvider.SetError(txt3, null);
            if (string.IsNullOrEmpty(txt3.Text))
                return;
            int i;
            if (!int.TryParse(txt3.Text, out i))
                _errorProvider.SetError(txt3, "必須為整數數字");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool valid = true;
            foreach (Control control in this.Controls)
                if (!string.IsNullOrEmpty(_errorProvider.GetError(control)))
                    valid = false;

            if (!valid)
            {
                MsgBox.Show("資料驗證錯誤，請先修正後再行儲存");
                return;
            }

            //檢查使用者是否忘記輸入功過次數。
            if (!chkAsshole.Checked)
            {
                int sum = int.Parse(GetTextValue(txt1.Text)) + int.Parse(GetTextValue(txt2.Text)) + int.Parse(GetTextValue(txt3.Text));
                if (sum <= 0)
                {
                    MsgBox.Show("請別忘了輸入功過次數。");
                    return;
                }
            }

            //檢查學生中是否有未分班學生，用年級檢查。
            //foreach (BriefStudentData each_stu in _students)
            //{
            //    if (string.IsNullOrEmpty(each_stu.GradeYear))
            //    {
            //        MsgBox.Show("新增失敗，部分學生未屬於任何班級，請先確認學生編班狀況。");
            //        return;
            //    }
            //}

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
                //helper.AddElement("Discipline", "GradeYear", student.GradeYear);
                helper.AddElement("Discipline", "OccurDate", dateText.DateString);
                helper.AddElement("Discipline", "Reason", txtReason.Text);
                helper.AddElement("Discipline", "MeritFlag", chkAsshole.Checked ? "2" : "0");
                helper.AddElement("Discipline", "Type", "1");
                helper.AddElement("Discipline", "Detail", h.GetRawXml(), true);
            }

            try
            {
                SmartSchool.Feature.Student.EditDiscipline.Insert(new DSRequest(helper));

                //新增獎懲紀錄 log
                foreach (BriefStudentData student in _students)
                {
                    StringBuilder desc = new StringBuilder("");
                    desc.AppendLine("學生姓名：" + SmartSchool.StudentRelated.Student.Instance.Items[student.ID].Name + " ");
                    //desc.AppendLine(CurrentUser.Instance.SchoolYear + " 學年度 第 " + CurrentUser.Instance.Semester + " 學期 ");
                    desc.AppendLine("日期：" + dateText.Text + " ");
                    if (chkAsshole.Checked)
                    {
                        desc.AppendLine(" 被打入留校察看名單之中!! ");
                    }
                    else
                    {
                        if (int.Parse(GetTextValue(txt1.Text)) > 0)
                            desc.AppendLine(lbl1.Text + "：" + GetTextValue(txt1.Text) + " ");
                        if (int.Parse(GetTextValue(txt2.Text)) > 0)
                            desc.AppendLine(lbl2.Text + "：" + GetTextValue(txt2.Text) + " ");
                        if (int.Parse(GetTextValue(txt3.Text)) > 0)
                            desc.AppendLine(lbl3.Text + "：" + GetTextValue(txt3.Text) + " ");
                    }
                    desc.AppendLine(Text.Substring(0, 2) + "事由：" + txtReason.Text);
                    CurrentUser.Instance.AppLog.Write(EntityType.Student, "新增獎懲紀錄", student.ID, desc.ToString(), Text, helper.GetRawXml());
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show("資料新增失敗：" + ex.Message);
                return;
            }

            if (DataSaved != null)
                DataSaved(this, EventArgs.Empty);
            MsgBox.Show("儲存完成");
            this.Close();
        }

        private string GetTextValue(string text)
        {
            if (string.IsNullOrEmpty(text) || chkAsshole.Checked)
                return "0";
            return text;
        }

        private void cboReasonRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)cboReasonRef.SelectedItem;
            txtReason.Text = kvp.Value;
        }

        private void DemeritEditor_Load(object sender, EventArgs e)
        {
            DSResponse dsrsp = SmartSchool.Feature.Basic.Config.GetDisciplineReasonList();
            cboReasonRef.SelectedItem = null;
            cboReasonRef.Items.Clear();
            DSXmlHelper helper = dsrsp.GetContent();
            KeyValuePair<string, string> fkvp = new KeyValuePair<string, string>("", "");
            cboReasonRef.Items.Add(fkvp);

            foreach (XmlElement element in helper.GetElements("Reason"))
            {
                if (element.GetAttribute("Type") == "懲誡" && _meritflag) continue;
                if (element.GetAttribute("Type") == "獎勵" && !_meritflag) continue;
                if (element.GetAttribute("Type") == "消過") continue;

                string k = element.GetAttribute("Code") + "-" + element.GetAttribute("Description");
                string v = element.GetAttribute("Description");
                KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(k, v);
                cboReasonRef.Items.Add(kvp);
            }
            cboReasonRef.DisplayMember = "Key";
            cboReasonRef.ValueMember = "Value";
            cboReasonRef.SelectedIndex = 0;
        }

        private void chkAsshole_CheckedChanged(object sender, EventArgs e)
        {   
            txt1.Enabled = !chkAsshole.Checked;
            txt2.Enabled = !chkAsshole.Checked;
            txt3.Enabled = !chkAsshole.Checked;
        }
    }
}