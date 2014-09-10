using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.Feature;
using IntelliSchool.DSA30.Util;
using System.Xml;
using DevComponents.DotNetBar;
using SmartSchool.ApplicationLog;
using SmartSchool.Feature.Course;
using SmartSchool.AccessControl;
using SmartSchool.StudentRelated;

namespace SmartSchool.CourseRelated.DetailPaneItem.OtherEntity
{
    [FeatureCode("Content0110")]
    internal partial class CourseScorePalmerwormItem : PalmerwormItem
    {
        private List<int> _schoolYearList;
        private DSResponse _examList;
        private ErrorProvider _errorProvider;
        private bool _initialized;
        private bool _loading;
        private ConditionArg _currentArg;
        private DataGetter _data_getter;
        private ScoreType _process_score_type;

        public CourseScorePalmerwormItem()
        {
            InitializeComponent();
            Title = "課程相關成績";
            _errorProvider = new ErrorProvider();
            _initialized = false;
            _loading = false;
        }

        #region Initialize Jobs
        protected override object OnBackgroundWorkerWorking()
        {
            _schoolYearList = CourseEntity.Instance.Semesters.GroupSchoolYear();
            _examList = SmartSchool.Feature.Course.QueryCourse.GetExamList();
            return null;
        }

        protected override void OnBackgroundWorkerCompleted(object result)
        {
            //將畫面上的條件選項重置成預設狀態。
            ResetConditionOptions();

            BindData();
        }

        private void ResetConditionOptions()
        {
            cboSchoolYear.SelectedItem = null;
            cboSchoolYear.Items.Clear();
            cboSemester.SelectedItem = null;
            cboSemester.Items.Clear();
            cboExam.SelectedItem = null;
            cboExam.Items.Clear();

            //填入學年度、學期條件選項。
            IninialSemesterConditions();

            //填入試別條件選項。
            InitialExamConditions();

            if (cboExam.Items.Count > 0)
                cboExam.SelectedIndex = 0;

            cboSchoolYear.Text = CurrentUser.Instance.SchoolYear.ToString();
            cboSemester.Text = CurrentUser.Instance.Semester.ToString();
            _initialized = true;
        }

        private void InitialExamConditions()
        {
            //利用 ExamId 的特殊資料，代表課程成績。
            KeyValuePair<string, string> courseItem = new KeyValuePair<string, string>("CourseScore", "(課程成績)");
            cboExam.Items.Add(courseItem);

            foreach (XmlElement element in _examList.GetContent().GetElements("Exam"))
            {
                string id = element.GetAttribute("ID");
                string name = element.SelectSingleNode("ExamName").InnerText;
                KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(id, name);
                cboExam.Items.Add(kvp);
            }
            cboExam.DisplayMember = "Value";
            cboExam.ValueMember = "Key";
        }

        private void IninialSemesterConditions()
        {
            foreach (int schoolYear in _schoolYearList)
                cboSchoolYear.Items.Add(schoolYear);

            cboSemester.Items.Add("1");
            cboSemester.Items.Add("2");
        }
        #endregion

        # region BindData Code
        private void BindData()
        {
            if (!IsValid()) return;
            if (!_initialized) return;
            if (_loading) return;
            if (cboExam.SelectedItem == null) return;

            dgvScore.EndEdit();
            dgvScore.Rows.Clear();
            dgvScore.Enabled = false;
            picWaiting.Show();

            ConditionArg arg = new ConditionArg();
            arg.Examid = ((KeyValuePair<string, string>)cboExam.SelectedItem).Key;
            arg.SchoolYear = cboSchoolYear.Text;
            arg.Semester = cboSemester.Text;
            _currentArg = arg;
            _loading = true;

            if (arg.Examid == "CourseScore")
                _process_score_type = ScoreType.Course;
            else
                _process_score_type = ScoreType.Exam;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync(arg);
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _data_getter = new DataGetter(RunningID, _process_score_type);
            e.Result = _data_getter.Evaluate(e.Argument as ConditionArg);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // 如果載入完成後檢查學年度學期考試三種參數已經和當時載入時所設定的不同的話, 重新取得資料
            if (_currentArg.SchoolYear != cboSchoolYear.Text
                || _currentArg.Semester != cboSemester.Text
                || _currentArg.Examid != ((KeyValuePair<string, string>)cboExam.SelectedItem).Key)
            {
                _loading = false;
                BindData();
                return;
            }
            // 如果有錯的話
            if (e.Error != null)
            {
                MsgBox.Show(e.Error.Message);
                return;
            }

            dgvScore.Enabled = true;
            _valueManager.ResetValues();
            dgvScore.EndEdit();
            dgvScore.Rows.Clear();
            if ( dgvScore.Columns.Count > 0 )
            {
                RowInfoCollection collection = e.Result as RowInfoCollection;
                foreach ( RowInfo info in collection.Items )
                {
                    int rowIndex = dgvScore.Rows.Add();
                    DataGridViewRow row = dgvScore.Rows[rowIndex];
                    row.Cells[colCourse.Name].Value = info.CourseName;
                    row.Cells[colCourse.Name].Tag = info.CourseID;
                    row.Cells[colScore.Name].Value = info.Score == "-1" ? "缺" : info.Score;
                    row.Cells[colScore.Name].Tag = info.Score;
                    row.Tag = info;
                    _valueManager.AddValue(info.CourseID, info.Score == "-1" ? "缺" : info.Score);
                }
            }
            dgvScore.SuspendLayout();
            picWaiting.Hide();
            _loading = false;
        }

        #region Inner Classes
        private class DataGetter
        {
            private string _running_id;
            private DSXmlHelper _currentResponse;
            private DSResponse rsp_temp;
            private ScoreType _score_type;

            public DataGetter(string runningId, ScoreType scoreType)
            {
                _running_id = runningId;
                _score_type = scoreType;
            }

            private string StudentID
            {
                get { return _running_id; }
            }

            public DSXmlHelper CurrentResponse
            {
                get { return _currentResponse; }
                private set { _currentResponse = value; }
            }

            public RowInfoCollection Evaluate(ConditionArg arg)
            {
                //先取得該學生於指定學年度學期修習之所有課程清單
                DSXmlHelper xml_temp = GetMajorCourse(arg);

                DSXmlHelper h2 = new DSXmlHelper("MappingRequest");
                h2.AddElement("Field");
                h2.AddElement("Field", "All");
                h2.AddElement("Condition");
                Dictionary<string, string> _courseList = new Dictionary<string, string>();
                Dictionary<string, string> _attendidList = new Dictionary<string, string>();

                foreach (XmlElement element in xml_temp.GetElements("Course"))
                {
                    string courseName = element.SelectSingleNode("CourseName").InnerText;
                    string courseID = element.GetAttribute("ID");
                    string attendID = element.SelectSingleNode("AttendID").InnerText;

                    _courseList.Add(courseID, courseName);
                    _attendidList.Add(courseID, attendID);
                    h2.AddElement("Condition", "RefCourseID", courseID);
                }

                if (_courseList.Count == 0)
                    return new RowInfoCollection();

                // 取得目前列出課程所有的考試對照表
                try
                {
                    DSResponse dsrsp = SmartSchool.Feature.Course.QueryCourse.GetExamMapping(new DSRequest(h2));
                    h2 = dsrsp.GetContent();
                }
                catch (Exception ex)
                {
                    throw new Exception("查詢課程所有的考試對照表失敗:" + ex.Message, ex);
                }

                // 依據cboExam 所選的考試項目過濾掉沒有參與考試的課程, 然後顯示在畫面上                    
                RowInfoCollection collection = new RowInfoCollection();
                foreach (string courseid in _courseList.Keys)
                {
                    string courseName = _courseList[courseid];

                    if (_score_type != ScoreType.Course) //如果不是課程成績，才需要判斷是否有使用特定評量。
                    {
                        bool found = false;
                        foreach (XmlElement element in h2.GetElements("Mapping"))
                        {
                            if (element.GetAttribute("CourseID") != courseid || element.GetAttribute("ExamID") != arg.Examid) continue;
                            found = true;
                        }
                        if (!found) continue;
                    }

                    RowInfo info = new RowInfo();
                    info.CourseID = courseid;
                    info.CourseName = courseName;
                    info.AttendID = _attendidList[courseid];
                    collection.Items.Add(info);
                }

                if (_score_type == ScoreType.Course) //試別欄位的特殊資料，代表要顯示課程成績。
                    GetSCAttendScore(arg, collection);
                else
                    GetSCETakeScore(arg, collection);

                return collection;
            }

            private void GetSCETakeScore(ConditionArg arg, RowInfoCollection rows)
            {
                //取得該學生於指定學年度學期之某次考試的所有成績清單
                DSXmlHelper xmlTemp = new DSXmlHelper("Request");
                xmlTemp.AddElement("Field");
                xmlTemp.AddElement("Field", "All");
                xmlTemp.AddElement("Condition");
                xmlTemp.AddElement("Condition", "RefExamID", arg.Examid);
                xmlTemp.AddElement("Condition", "SchoolYear", arg.SchoolYear);
                xmlTemp.AddElement("Condition", "Semester", arg.Semester);
                xmlTemp.AddElement("Condition", "RefStudentID", StudentID);

                try
                {
                    rsp_temp = SmartSchool.Feature.QueryStudent.GetStudentExamScore(new DSRequest(xmlTemp));
                }
                catch (Exception ex)
                {
                    throw new Exception("查詢學生成績失敗:" + ex.Message, ex);
                }
                xmlTemp = rsp_temp.GetContent();
                CurrentResponse = xmlTemp;

                // 依照查詢回來的成績清單填回所對應的課程
                foreach (XmlElement element in xmlTemp.GetElements("Score"))
                {
                    string courseName = element.SelectSingleNode("CourseName").InnerText;
                    string courseID = element.SelectSingleNode("RefCourseID").InnerText;
                    string attendid = element.SelectSingleNode("RefAttendID").InnerText;
                    string sce_id = element.GetAttribute("ID");
                    string score = element.SelectSingleNode("Score").InnerText;

                    foreach (RowInfo info in rows.Items)
                    {
                        if (info.CourseID != courseID) continue;

                        info.SecID = sce_id;
                        info.Score = score;
                    }
                }
            }

            private void GetSCAttendScore(ConditionArg arg, RowInfoCollection rows)
            {
                //取得該學生於指定學年度學期之某次考試的所有成績清單
                DSXmlHelper xmlTemp = new DSXmlHelper("Request");
                xmlTemp.AddElement(".", "Field", "<ID/><RefStudentID/><RefCourseID/><Score/>", true);
                xmlTemp.AddElement("Condition");
                xmlTemp.AddElement("Condition", "StudentID", StudentID);

                try
                {
                    rsp_temp = QueryCourse.GetSCAttend(new DSRequest(xmlTemp));
                }
                catch (Exception ex)
                {
                    throw new Exception("查詢學生成績失敗:" + ex.Message, ex);
                }
                xmlTemp = rsp_temp.GetContent();
                CurrentResponse = xmlTemp;

                // 依照查詢回來的成績清單填回所對應的課程
                foreach (XmlElement element in xmlTemp.GetElements("Student"))
                {
                    DSXmlHelper helper = new DSXmlHelper(element);
                    string courseID = helper.GetText("RefCourseID");
                    string attendid = helper.GetText("@ID");
                    string score = helper.GetText("Score");

                    foreach (RowInfo info in rows.Items)
                    {
                        if (info.CourseID != courseID) continue;

                        info.SecID = "-1";
                        info.Score = score;
                    }
                }
            }

            private DSXmlHelper GetMajorCourse(ConditionArg arg)
            {
                DSXmlHelper xmlTemp = new DSXmlHelper("Request");
                xmlTemp.AddElement("Field");
                xmlTemp.AddElement("Field", "All");
                xmlTemp.AddElement("Condition");
                xmlTemp.AddElement("Condition", "SchoolYear", arg.SchoolYear);
                xmlTemp.AddElement("Condition", "Semester", arg.Semester);
                xmlTemp.AddElement("Condition", "RefStudentID", StudentID);
                try
                {
                    rsp_temp = SmartSchool.Feature.QueryStudent.GetStudentMajorCourse(new DSRequest(xmlTemp));
                    xmlTemp = rsp_temp.GetContent();
                }
                catch (Exception ex)
                {
                    throw new Exception("查詢學生於指定學年度學期修習之所有課程清單失敗:" + ex.Message, ex);
                }
                return xmlTemp;
            }
        }
        #endregion

        #endregion

        #region User Interactions

        private bool IsValid()
        {
            if (!_initialized) return true;

            _errorProvider.SetError(cboSchoolYear, null);
            _errorProvider.SetError(cboSemester, null);
            bool valid = true;
            int a;
            if (!int.TryParse(cboSchoolYear.Text, out a))
            {
                _errorProvider.SetError(cboSchoolYear, "必須為數字");
                valid = false;
            }
            _errorProvider.SetError(cboSemester, null);
            if (cboSemester.Text != "1" && cboSemester.Text != "2")
            {
                _errorProvider.SetError(cboSemester, "必須為數字 1 或 2 ");
                valid = false;
            }
            return valid;
        }

        private void cboSchoolYear_Validated(object sender, EventArgs e)
        {

            if (!IsValid()) return;
            if ("" + cboSchoolYear.Tag != cboSchoolYear.Text)
            {
                cboSchoolYear.Tag = cboSchoolYear.Text;
                BindData();
            }
        }

        private void cboSemester_Validated(object sender, EventArgs e)
        {
            if (!IsValid()) return;
            if ("" + cboSemester.Tag != cboSemester.Text)
            {
                cboSemester.Tag = cboSemester.Text;
                BindData();
            }
        }

        private void cboExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsValid()) return;
            if ("" + cboExam.Tag != cboExam.Text)
            {
                cboExam.Tag = cboExam.Text;
            }
            BindData();
        }

        private void dgvScore_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvScore.Rows[e.RowIndex];
            DataGridViewCell cell = row.Cells[e.ColumnIndex];
            cell.Style.BackColor = Color.White;
            cell.Style.ForeColor = Color.Black;
            cell.ErrorText = string.Empty;
            cell.ToolTipText = string.Empty;
            RowInfo info = row.Tag as RowInfo;

            string id = info.CourseID;
            string oldValue = info.Score == "-1" ? "缺" : info.Score;
            string value = cell.Value == null ? null : cell.Value.ToString();

            decimal score;
            if (_process_score_type == ScoreType.Exam)
            {
                if (!string.IsNullOrEmpty(value) && !decimal.TryParse(value, out score) && value != "缺")
                {
                    cell.ErrorText = "分數必須為數字或『缺』。";
                    return;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(value) && !decimal.TryParse(value, out score))
                {
                    cell.ErrorText = "分數必須為數字。";
                    return;
                }
            }

            OnValueChanged(id, value);
            if (value != oldValue)
            {
                cell.Style.BackColor = Color.Yellow;
                cell.Style.ForeColor = Color.Blue;
                cell.ToolTipText = "已由『" + oldValue + "』變成『" + value + "』。";
            }
        }

        public override void Save()
        {
            if (!isAllValid())
            {
                MsgBox.Show("資料輸入不正確，請修正後再行儲存：" + Title);
                return;
            }

            if (_process_score_type == ScoreType.Course)
                SaveCourseScore();
            else
                SaveExamScore();

            SaveButtonVisible = false;
            LoadContent(RunningID);
        }

        private void SaveCourseScore()
        {
            bool updateRequired = false;
            DSXmlHelper updateHelper = new DSXmlHelper("Request");

            StringBuilder updateDesc = new StringBuilder("");
            updateDesc.Append("學生：").Append(Student.Instance.Items[RunningID].Name);
            updateDesc.Append(", 學年度：").Append(cboSchoolYear.Text);
            updateDesc.Append(", 學期：").AppendLine(cboSemester.Text);

            foreach (DataGridViewRow row in dgvScore.Rows)
            {
                RowInfo info = row.Tag as RowInfo;
                string newScore = row.Cells[colScore.Name].Value + string.Empty;

                if (info.Score == newScore) continue;

                updateDesc.AppendLine("課程成績『" + info.CourseName + "』由『" + info.Score + "』修改為『" + newScore + "』。");

                updateHelper.AddElement("Attend");
                updateHelper.AddElement("Attend", "ID", info.AttendID);
                updateHelper.AddElement("Attend", "Score", newScore);

                updateRequired = true;
            }

            if (updateRequired)
            {
                try
                {
                    EditCourse.UpdateAttend(updateHelper);
                    CurrentUser.Instance.AppLog.Write(EntityType.Student, EntityAction.Update, RunningID, updateDesc.ToString(), "學生課程成績", updateHelper.GetRawXml());
                }
                catch (Exception ex)
                {
                    CurrentUser.ReportError(ex);
                    MsgBox.Show(ex.Message);
                }
            }
        }

        private void SaveExamScore()
        {
            DSXmlHelper updateHelper = new DSXmlHelper("Request");
            DSXmlHelper insertHelper = new DSXmlHelper("Request");
            DSXmlHelper deleteHelper = new DSXmlHelper("Request");

            updateHelper.AddElement("ScoreSheetList");
            insertHelper.AddElement("ScoreSheetList");
            deleteHelper.AddElement("ScoreSheet");

            StringBuilder updateDesc = new StringBuilder("");
            updateDesc.Append("學生：").Append(Student.Instance.Items[RunningID].Name);
            updateDesc.Append(", 學年度：").Append(cboSchoolYear.Text);
            updateDesc.Append(", 學期：").Append(cboSemester.Text);
            updateDesc.Append(", 考試：").Append(cboExam.Text).Append("\n");

            StringBuilder insertDesc = new StringBuilder(updateDesc.ToString());
            StringBuilder deleteDesc = new StringBuilder(updateDesc.ToString());

            bool hasInsert = false, hasUpdate = false, hasDelete = false;

            foreach (DataGridViewRow row in dgvScore.Rows)
            {
                RowInfo info = row.Tag as RowInfo;
                DataGridViewCell cell = row.Cells[colScore.Name];

                /** 
                 * 修改
                 * 1.分數被變更
                 * 2.Sce_ID 不為空值
                 * 3.變更後分數不為空值
                 */
                if (cell.Style.BackColor == Color.Yellow && cell.Value != null && !string.IsNullOrEmpty(info.SecID))
                {
                    updateHelper.AddElement("ScoreSheetList", "ScoreSheet");
                    updateHelper.AddElement("ScoreSheetList/ScoreSheet", "ID", info.SecID);
                    updateHelper.AddElement("ScoreSheetList/ScoreSheet", "Score", cell.Value == null ? "" : cell.Value.ToString());

                    updateDesc.Append("課程『" + info.CourseName + "』成績由『" + info.Score + "』修改為『" + cell.Value.ToString() + "』。\n");
                    hasUpdate = true;
                }
                /** 
                * 新增
                * 1.分數被變更
                * 2.Sce_ID 為空值
                * 3.變更後分數不為空值
                */
                else if (cell.Style.BackColor == Color.Yellow && cell.Value != null && string.IsNullOrEmpty(info.SecID))
                {
                    insertHelper.AddElement("ScoreSheetList", "ScoreSheet");
                    insertHelper.AddElement("ScoreSheetList/ScoreSheet", "AttendID", info.AttendID);
                    insertHelper.AddElement("ScoreSheetList/ScoreSheet", "ExamID", ((KeyValuePair<string, string>)cboExam.SelectedItem).Key);
                    insertHelper.AddElement("ScoreSheetList/ScoreSheet", "Score", cell.Value == null ? "" : cell.Value.ToString());

                    insertDesc.Append("課程『" + info.CourseName + "』新增成績『" + cell.Value.ToString() + "』。\n");
                    hasInsert = true;
                }
                /** 
                * 刪除
                * 1.分數原本不為空值
                * 2.Sce_ID 不為空值
                * 3.變更後分數為空值
                */
                else if (cell.Tag != null && !string.IsNullOrEmpty(info.SecID) && cell.Value == null)
                {
                    deleteHelper.AddElement("ScoreSheet", "ID", info.SecID);

                    deleteDesc.Append("將課程『" + info.CourseName + "』原成績『" + info.Score + "』刪除。\n");
                    hasDelete = true;
                }
            }
            try
            {
                if (hasInsert)
                {
                    SmartSchool.Feature.Course.EditCourse.InsertSCEScore(new DSRequest(insertHelper));
                    CurrentUser.Instance.AppLog.Write(EntityType.Student, EntityAction.Insert, RunningID, insertDesc.ToString(), Title, insertHelper.GetRawXml());
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show("新增成績錯誤:" + ex.Message);
            }

            try
            {
                if (hasUpdate)
                {
                    SmartSchool.Feature.Course.EditCourse.UpdateSCEScore(new DSRequest(updateHelper));
                    CurrentUser.Instance.AppLog.Write(EntityType.Student, EntityAction.Update, RunningID, updateDesc.ToString(), Title, updateHelper.GetRawXml());
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show("修改成績錯誤:" + ex.Message);
            }

            try
            {
                if (hasDelete)
                {
                    SmartSchool.Feature.Course.EditCourse.DeleteSCEScore(new DSRequest(deleteHelper));
                    CurrentUser.Instance.AppLog.Write(EntityType.Student, EntityAction.Delete, RunningID, deleteDesc.ToString(), Title, deleteHelper.GetRawXml());
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show("刪除成績錯誤:" + ex.Message);
            }
        }

        private bool isAllValid()
        {
            if (!IsValid()) return false;
            foreach (DataGridViewRow row in dgvScore.Rows)
            {
                DataGridViewCell cell = row.Cells[colScore.Name];
                //if (cell.Style.BackColor == Color.Red)
                if (!string.IsNullOrEmpty(cell.ErrorText))
                    return false;
            }
            return true;
        }

        private void panel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
                XmlBox.ShowXml(_data_getter.CurrentResponse.BaseElement);
        }

        private void dgvScore_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvScore.Rows[e.RowIndex];
            DataGridViewCell cell = row.Cells[e.ColumnIndex];
            cell.Style.BackColor = Color.White;
            cell.Style.ForeColor = Color.Black;
            cell.ToolTipText = "";
            RowInfo info = row.Tag as RowInfo;

            string id = info.CourseID;
            string oldValue = info.Score;
            string value = cell.Value == null ? "" : cell.Value.ToString();

            decimal score;
            if (!string.IsNullOrEmpty(value) && !decimal.TryParse(value, out score))
            {
                cell.Style.BackColor = Color.Red;
                cell.Style.ForeColor = Color.White;
                cell.ToolTipText = "分數必須為數字";
                return;
            }

            OnValueChanged(id, value);
            if (value != oldValue)
            {
                cell.Style.BackColor = Color.Yellow;
                cell.Style.ForeColor = Color.Blue;
                cell.ToolTipText = "已由『" + oldValue + "』變成『" + value + "』";
            }
        }

        #endregion
        public override object Clone()
        {
            return new CourseScorePalmerwormItem();
        }
    }

    #region Internal Classes

    internal enum ScoreType
    {
        Exam, Course
    }

    internal class RowInfo
    {
        private string _secID;

        public RowInfo()
        {
        }

        public string SecID
        {
            get { return _secID; }
            set { _secID = value; }
        }
        private string _courseID;

        public string CourseID
        {
            get { return _courseID; }
            set { _courseID = value; }
        }
        private string _courseName;

        public string CourseName
        {
            get { return _courseName; }
            set { _courseName = value; }
        }
        private string _score;

        public string Score
        {
            get { return _score; }
            set { _score = value; }
        }

        private string _attendID;

        public string AttendID
        {
            get { return _attendID; }
            set { _attendID = value; }
        }
    }

    internal class RowInfoCollection
    {
        private List<RowInfo> _items;
        public RowInfoCollection()
        {
            _items = new List<RowInfo>();
        }
        internal List<RowInfo> Items
        {
            get { return _items; }
            set { _items = value; }
        }
    }

    internal class ConditionArg
    {
        private string _semester;

        public string Semester
        {
            get { return _semester; }
            set { _semester = value; }
        }
        private string _schoolYear;

        public string SchoolYear
        {
            get { return _schoolYear; }
            set { _schoolYear = value; }
        }
        private string _examid;

        public string Examid
        {
            get { return _examid; }
            set { _examid = value; }
        }
    }
    #endregion
}
