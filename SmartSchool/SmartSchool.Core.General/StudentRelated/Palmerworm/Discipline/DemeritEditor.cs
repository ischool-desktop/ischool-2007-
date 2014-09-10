using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.StudentRelated.RibbonBars.AttendanceEditor;
using System.Xml;
using DevComponents.DotNetBar;
using IntelliSchool.DSA30.Util;
using SmartSchool.ApplicationLog;

namespace SmartSchool.StudentRelated.Palmerworm
{
    public partial class DemeritEditor : BaseForm
    {
        public event EventHandler DataSaved;

        private List<BriefStudentData> _students;
        private bool _meritflag;
        private ISemester _semesterProvider;
        private ErrorProvider _errorProvider;
        private Discipline _discipline;        

        //log 需要用到的
        private Dictionary<string, string> beforeData = new Dictionary<string, string>();
        private Dictionary<string, string> afterData = new Dictionary<string, string>();

        public DemeritEditor(List<BriefStudentData> students, bool meritflag)
        {
            Initialize(students, meritflag, null);
        }

        public DemeritEditor(List<BriefStudentData> students, bool meritflag, Discipline discipline)
        {
            Initialize(students, meritflag, discipline);
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

        private void chkAsshole_CheckedChanged(object sender, EventArgs e)
        {
            txt1.Enabled = !chkAsshole.Checked;
            txt2.Enabled = !chkAsshole.Checked;
            txt3.Enabled = !chkAsshole.Checked;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboReasonRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)cboReasonRef.SelectedItem;
            txtReason.Text = kvp.Value;
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

            if (_discipline == null)
                Insert();
            else
                Modify();

            if (DataSaved != null)
                DataSaved(this, EventArgs.Empty);
            List<string> idList = new List<string>();
            foreach ( BriefStudentData var in _students )
            {
                if ( !idList.Contains(var.ID) )
                    idList.Add(var.ID);
            }
            Student.Instance.InvokDisciplineChanged(idList.ToArray());
            MsgBox.Show("儲存完成");
            this.Close();
        }

        private string GetTextValue(string text)
        {
            if (string.IsNullOrEmpty(text) || chkAsshole.Checked)
                return "0";
            return text;
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

            if (_discipline != null)
            {
                chkAsshole.Checked = _discipline.IsAsshole;
                txtReason.Text = _discipline.Reason;
                txt1.Text = _discipline.A;
                txt2.Text = _discipline.B;
                txt3.Text = _discipline.C;
                dateText.Text = _discipline.OccurDate;
                cboSchoolYear.Text = _discipline.SchoolYear;
                cboSemester.Text = _discipline.Semester;
            }
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
            _discipline = discipline;

            //log 紀錄之前的獎懲狀態
            if (_discipline != null)
            {
                beforeData.Add("學年度", _discipline.SchoolYear);
                beforeData.Add("學期", _discipline.Semester);
                beforeData.Add(lbl1.Text, _discipline.A);
                beforeData.Add(lbl2.Text, _discipline.B);
                beforeData.Add(lbl3.Text, _discipline.C);
                beforeData.Add("日期", _discipline.OccurDate);
                beforeData.Add("事由", _discipline.Reason);
                beforeData.Add("是否留校察看", _discipline.IsAsshole.ToString());
            }
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
                //helper.AddElement("Discipline/Field", "GradeYear", (student.GradeYear == "未分年級") ? "" : student.GradeYear);
                helper.AddElement("Discipline/Field", "OccurDate", dateText.DateString);
                helper.AddElement("Discipline/Field", "Reason", txtReason.Text);
                helper.AddElement("Discipline/Field", "MeritFlag", chkAsshole.Checked ? "2" : "0");
                helper.AddElement("Discipline/Field", "Type", "1");
                helper.AddElement("Discipline/Field", "Detail", h.GetRawXml(), true);
                helper.AddElement("Discipline", "Condition");
                helper.AddElement("Discipline/Condition", "ID", _discipline.Id);

                //log 紀錄修改後的獎懲狀態
                if (_discipline != null)
                {
                    afterData.Clear();
                    afterData.Add("學年度", cboSchoolYear.Text);
                    afterData.Add("學期", cboSemester.Text);
                    afterData.Add(lbl1.Text, GetTextValue(txt1.Text));
                    afterData.Add(lbl2.Text, GetTextValue(txt2.Text));
                    afterData.Add(lbl3.Text, GetTextValue(txt3.Text));
                    afterData.Add("日期", dateText.DateString);
                    afterData.Add("事由", txtReason.Text);
                    afterData.Add("是否留校察看", chkAsshole.Checked.ToString());
                }
            }

            try
            {
                SmartSchool.Feature.Student.EditDiscipline.Update(new DSRequest(helper));

                //修改獎懲紀錄 log
                foreach (BriefStudentData student in _students)
                {
                    bool dirty = false;
                    StringBuilder desc = new StringBuilder("");
                    desc.AppendLine("學生姓名：" + Student.Instance.Items[student.ID].Name + " ");
                    //desc.AppendLine(CurrentUser.Instance.SchoolYear + " 學年度 第 " + CurrentUser.Instance.Semester + " 學期 ");
                    desc.AppendLine("日期：" + dateText.Text + " ");

                    foreach (string key in beforeData.Keys)
                    {
                        if (beforeData[key] != afterData[key])
                        {
                            dirty = true;
                            desc.AppendLine("「" + key + "」由「" + beforeData[key] + "」變更為「" + afterData[key] + "」 ");
                        }
                    }

                    if (dirty)
                        CurrentUser.Instance.AppLog.Write(EntityType.Student, "修改獎懲紀錄", student.ID, desc.ToString(), Text, helper.GetRawXml());
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show("資料修改失敗：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //helper.AddElement("Discipline", "GradeYear", (student.GradeYear == "未分年級") ? "" : student.GradeYear);
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
                    desc.AppendLine("學生姓名：" + Student.Instance.Items[student.ID].Name + " ");
                    //desc.AppendLine(CurrentUser.Instance.SchoolYear + " 學年度 第 " + CurrentUser.Instance.Semester + " 學期 ");
                    desc.AppendLine("日期：" + dateText.Text + " ");
                    if (!chkAsshole.Checked)
                    {
                        if (int.Parse(GetTextValue(txt1.Text)) > 0)
                            desc.AppendLine(lbl1.Text + "：" + GetTextValue(txt1.Text) + " ");
                        if (int.Parse(GetTextValue(txt2.Text)) > 0)
                            desc.AppendLine(lbl2.Text + "：" + GetTextValue(txt2.Text) + " ");
                        if (int.Parse(GetTextValue(txt3.Text)) > 0)
                            desc.AppendLine(lbl3.Text + "：" + GetTextValue(txt3.Text) + " ");
                    }
                    else
                    {
                        desc.AppendLine(" 將學生打入留校察看名單 ");
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
        }
    }
}