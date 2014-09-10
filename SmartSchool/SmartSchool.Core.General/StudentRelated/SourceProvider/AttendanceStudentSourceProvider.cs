using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;
using System.Xml;
using SmartSchool.API.StudentExtension;
using SmartSchool.StudentRelated.Divider;

namespace SmartSchool.StudentRelated.SourceProvider
{
    class SemesterAttendanceStudentSourceProvider : NormalizeStudentSourceProvider, IComparable<SemesterAttendanceStudentSourceProvider>, IComparer<SemesterAttendanceStudentSourceProvider>
    {
        private int _SchoolYear = CurrentUser.Instance.SchoolYear;
        private int _Semester = CurrentUser.Instance.Semester;
        private BackgroundWorker _BkwLoader = new BackgroundWorker();
        private TreeNode _LoadingNode = new TreeNode("讀取中...");
        private Dictionary<string, TreeNode> _PathNodes = new Dictionary<string, TreeNode>();
        private Dictionary<string, List<Attendance>> _StudentAttendance = null;
        private List<TreeNode> _RelatedNodes = null;
        private IAttendanceItemShown _Setup;

        public int SchoolYear
        {
            get { return _SchoolYear; }
            set
            {
                _SchoolYear = value;
                Text = "" + _SchoolYear + "學年第" + _Semester + "學期";
            }
        }
        public int Semester
        {
            get { return _Semester; }
            set
            {
                _Semester = value;
                Text = "" + _SchoolYear + "學年第" + _Semester + "學期";
            }
        }

        public SemesterAttendanceStudentSourceProvider(int schoolYear, int semester, List<TreeNode> relatedNodeCollection, IAttendanceItemShown setup)
        {
            SchoolYear = schoolYear;
            Semester = semester;
            _BkwLoader.DoWork += new DoWorkEventHandler(_BkwLoader_DoWork);
            _BkwLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BkwLoader_RunWorkerCompleted);
            _RelatedNodes = relatedNodeCollection;
            _Setup = setup;
            this.Expand();
        }

        void _BkwLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            int schoolyear = Convert.ToInt32(( (object[])e.Argument )[0]);
            int semester = Convert.ToInt32(( (object[])e.Argument )[1]);
            Dictionary<string, List<Attendance>> marks = new Dictionary<string, List<Attendance>>();
            #region 把有案底的都抓出來檢驗
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            if ( schoolyear > 0 )
                helper.AddElement("Condition", "SchoolYear", schoolyear.ToString());
            if ( semester > 0 )
                helper.AddElement("Condition", "Semester", semester.ToString());
            helper.AddElement("Order");
            helper.AddElement("Order", "OccurDate", "desc");

            foreach ( XmlElement var in SmartSchool.Feature.Student.QueryAttendance.GetAttendance(new DSRequest(helper)).GetContent().GetElements("Attendance") )
            {
                int schoolyear2 = 0;
                int.TryParse(var.SelectSingleNode("SchoolYear").InnerText, out schoolyear2);
                int semester2 = 0;
                int.TryParse(var.SelectSingleNode("Semester").InnerText, out semester2);
                DateTime occurdate;
                DateTime.TryParse(var.SelectSingleNode("OccurDate").InnerText, out occurdate);
                string studentID = var.SelectSingleNode("RefStudentID").InnerText;

                foreach ( XmlElement element in var.SelectNodes("Detail/Attendance/Period") )
                {
                    string period = element.InnerText;
                    string periodtype = element.GetAttribute("AttendanceType");
                    string attendance = element.GetAttribute("AbsenceType");

                    SmartSchool.API.StudentExtension.Attendance attendanceInfo = new SmartSchool.API.StudentExtension.Attendance(schoolyear2, semester2, occurdate, period, periodtype, attendance,var);

                    if ( !marks.ContainsKey(studentID) )
                        marks.Add(studentID, new List<Attendance>());
                    marks[studentID].Add(attendanceInfo);
                }
            }
            #endregion
            //取得學生相關資料
            StudentRelated.Student.Instance.Items.EnsureStudent(marks.Keys);
            e.Result = marks;
        }

        void _BkwLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ( e.Result == null ) return;
            Student.Instance.AttendanceChanged += new EventHandler<StudentAttendanceChangedEventArgs>(Instance_AttendanceChanged);
            //每個學生編號在此學期的缺曠時間
            _StudentAttendance = (Dictionary<string, List<Attendance>>)e.Result;
            FillNodes();
        }

        void Instance_AttendanceChanged(object sender, StudentAttendanceChangedEventArgs e)
        {
            Dictionary<string, List<Attendance>> marks = new Dictionary<string, List<Attendance>>();
            #region 取得缺曠資料
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            foreach ( BriefStudentData student in e.Students )
            {
                helper.AddElement("Condition", "RefStudentID", student.ID);
                if ( _StudentAttendance.ContainsKey(student.ID) )
                    _StudentAttendance.Remove(student.ID);
            }
            if ( _SchoolYear > 0 )
                helper.AddElement("Condition", "SchoolYear", _SchoolYear.ToString());
            if ( _Semester > 0 )
                helper.AddElement("Condition", "Semester", _Semester.ToString());
            helper.AddElement("Order");
            helper.AddElement("Order", "OccurDate", "desc");

            foreach ( XmlElement var in SmartSchool.Feature.Student.QueryAttendance.GetAttendance(new DSRequest(helper)).GetContent().GetElements("Attendance") )
            {
                int schoolyear = 0;
                int.TryParse(var.SelectSingleNode("SchoolYear").InnerText, out schoolyear);
                int semester2 = 0;
                int.TryParse(var.SelectSingleNode("Semester").InnerText, out semester2);
                DateTime occurdate;
                DateTime.TryParse(var.SelectSingleNode("OccurDate").InnerText, out occurdate);
                string studentID = var.SelectSingleNode("RefStudentID").InnerText;

                foreach ( XmlElement element in var.SelectNodes("Detail/Attendance/Period") )
                {
                    string period = element.InnerText;
                    string periodtype = element.GetAttribute("AttendanceType");
                    string attendance = element.GetAttribute("AbsenceType");

                    SmartSchool.API.StudentExtension.Attendance attendanceInfo = new SmartSchool.API.StudentExtension.Attendance(schoolyear, semester2, occurdate, period, periodtype, attendance, var);

                    if ( !marks.ContainsKey(studentID) )
                        marks.Add(studentID, new List<Attendance>());
                    marks[studentID].Add(attendanceInfo);
                }
            }
            #endregion
            foreach ( string id in marks.Keys )
            {
                if ( _StudentAttendance.ContainsKey(id) )
                    _StudentAttendance[id] = marks[id];
                else
                    _StudentAttendance.Add(id, marks[id]);
            }
            FillNodes();
        }

        private void FillNodes()
        {
            MotherForm.Instance.SetWaitCursor();
            TreeNode selectNode = this.TreeView != null ? this.TreeView.SelectedNode : null;
            if ( this.TreeView != null )
                this.TreeView.SuspendLayout();
            //移除讀取中節點
            this.Nodes.Clear();
            #region 填入子節點
            List<BriefStudentData> students = new List<BriefStudentData>();
            Dictionary<string, List<BriefStudentData>> monthList = new Dictionary<string, List<BriefStudentData>>();
            Dictionary<string, List<string>> monthDayList = new Dictionary<string, List<string>>();
            Dictionary<string, List<BriefStudentData>> dayList = new Dictionary<string, List<BriefStudentData>>();
            Dictionary<string, int> pathYear = new Dictionary<string, int>();
            Dictionary<string, int> pathMonth = new Dictionary<string, int>();
            Dictionary<string, int> pathDay = new Dictionary<string, int>();

            foreach ( string var in _StudentAttendance.Keys )
            {
                //不在篩選清單中
                if ( !_List.Contains(var) )
                    continue;
                BriefStudentData student = Student.Instance.Items[var];
                bool countInStudent = false;
                //用時間整理
                foreach ( Attendance date in _StudentAttendance[var] )
                {
                    if ( _Setup.Shown(student, date) )
                    {
                        countInStudent = true;
                        //整理成月份
                        string monthPath = "" + date.OccurDate.Year + "_" + date.OccurDate.Month + "";
                        if ( !monthList.ContainsKey(monthPath) )
                        {
                            monthList.Add(monthPath, new List<BriefStudentData>());
                            //年
                            pathYear.Add(monthPath, date.OccurDate.Year);
                            //月
                            pathMonth.Add(monthPath, date.OccurDate.Month);
                        }
                        if ( !monthList[monthPath].Contains(Student.Instance.Items[var]) )
                            monthList[monthPath].Add(Student.Instance.Items[var]);
                        //整理成日期
                        string dayPath = "" + date.OccurDate.Year + "_" + date.OccurDate.Month + "_" + date.OccurDate.Day;
                        //本月份的日期LIST
                        if ( !monthDayList.ContainsKey(monthPath) )
                            monthDayList.Add(monthPath, new List<string>());
                        if ( !monthDayList[monthPath].Contains(dayPath) )
                            monthDayList[monthPath].Add(dayPath);
                        //各日期的LIST
                        if ( !dayList.ContainsKey(dayPath) )
                        {
                            dayList.Add(dayPath, new List<BriefStudentData>());
                            //年
                            pathYear.Add(dayPath, date.OccurDate.Year);
                            //月
                            pathMonth.Add(dayPath, date.OccurDate.Month);
                            //日
                            pathDay.Add(dayPath, date.OccurDate.Day);
                        }
                        if ( !dayList[dayPath].Contains(Student.Instance.Items[var]) )
                            dayList[dayPath].Add(Student.Instance.Items[var]);
                    }
                }
                if ( countInStudent )
                    students.Add(Student.Instance.Items[var]);
            }
            students.Sort();
            Source = students;
            #region 整理每個月分的資料
            List<MonthAttendanceStudentSourceProvider> monthProviders = new List<MonthAttendanceStudentSourceProvider>();
            foreach ( string monthpath in monthList.Keys )
            {
                MonthAttendanceStudentSourceProvider monthNode;
                //沒這月的節點就新增一個
                if ( !_PathNodes.ContainsKey(monthpath) )
                {
                    monthNode = new MonthAttendanceStudentSourceProvider();
                    monthNode.Year = pathYear[monthpath];//int.Parse(monthpath.Split("_".ToCharArray())[0]);
                    monthNode.Month = pathMonth[monthpath];// int.Parse(monthpath.Split("_".ToCharArray())[1]);
                    monthNode.DisplaySource = true;
                    //登紀關聯
                    _PathNodes.Add(monthpath, monthNode);
                    _RelatedNodes.Add(monthNode);
                }
                else
                    monthNode = (MonthAttendanceStudentSourceProvider)_PathNodes[monthpath];
                monthList[monthpath].Sort();
                monthNode.Source = monthList[monthpath];
                monthProviders.Add(monthNode);
                monthNode.Collapse();
                //這個月內的單日紀錄
                monthNode.Nodes.Clear();
                List<DateAttendanceStudentSourceProvider> dayProviders = new List<DateAttendanceStudentSourceProvider>();
                foreach ( string dayPath in monthDayList[monthpath] )
                {
                    DateAttendanceStudentSourceProvider dayNode;
                    if ( !_PathNodes.ContainsKey(dayPath) )
                    {
                        dayNode = new DateAttendanceStudentSourceProvider();
                        dayNode.Month = pathMonth[dayPath]; //int.Parse(dayPath.Split("_".ToCharArray())[1]);
                        dayNode.Day = pathDay[dayPath];//int.Parse(dayPath.Split("_".ToCharArray())[2]);
                        dayNode.DisplaySource = true;
                        //登記關聯
                        _PathNodes.Add(dayPath, dayNode);
                        _RelatedNodes.Add(dayNode);
                    }
                    else
                        dayNode = (DateAttendanceStudentSourceProvider)_PathNodes[dayPath];
                    dayList[dayPath].Sort();
                    dayNode.Source = dayList[dayPath];
                    dayProviders.Add(dayNode);
                }
                dayProviders.Sort();
                monthNode.Nodes.AddRange(dayProviders.ToArray());
            }
            monthProviders.Sort();
            this.Nodes.AddRange(monthProviders.ToArray());
            #endregion
            #endregion
            if ( this.TreeView != null )
            {
                this.TreeView.ResumeLayout();
                this.TreeView.SelectedNode = selectNode;
            }
            MotherForm.Instance.ResetWaitCursor();
        }
        List<string> _List = new List<string>();
        public void Reflash(List<string> list)
        {
            _List = list;
            if ( _BkwLoader.IsBusy )
                return;
            if ( _StudentAttendance == null )
            {
                this.Nodes.Clear();
                this.Nodes.Add(_LoadingNode);
                ( (TreeNode)this ).Text = "" + _SchoolYear + "學年第" + _Semester + "學期(???)";
                _BkwLoader.RunWorkerAsync(new object[] { _SchoolYear, _Semester });
            }
            else
                FillNodes();
        }

        #region IComparer 成員

        public int Compare(SemesterAttendanceStudentSourceProvider xnode, SemesterAttendanceStudentSourceProvider ynode)
        {
            if ( xnode.SchoolYear > ynode.SchoolYear ) return -1;
            if ( xnode.SchoolYear < ynode.SchoolYear ) return 1;
            if ( xnode.Semester > ynode.Semester ) return -1;
            else if ( xnode.Semester < ynode.Semester ) return 1;
            else return 0;
        }

        #endregion

        #region IComparable<SemesterAttendanceStudentSourceProvider> 成員

        public int CompareTo(SemesterAttendanceStudentSourceProvider other)
        {
            if ( this.SchoolYear > other.SchoolYear ) return -1;
            if ( this.SchoolYear < other.SchoolYear ) return 1;
            if ( this.Semester > other.Semester ) return -1;
            else if ( this.Semester < other.Semester ) return 1;
            else return 0;
        }

        #endregion
    }
    class MonthAttendanceStudentSourceProvider : NormalizeStudentSourceProvider, IComparable<MonthAttendanceStudentSourceProvider>, IComparer<MonthAttendanceStudentSourceProvider>
    {
        private int _Year = DateTime.Now.Year;
        private int _Month = DateTime.Now.Month;

        public int Year
        {
            get { return _Year; }
            set
            {
                _Year = value;
                Text = "" + _Year + "年" + _Month + "月";
            }
        }
        public int Month
        {
            get { return _Month; }
            set
            {
                _Month = value;
                Text = "" + _Year + "年" + _Month + "月";
            }
        }

        #region IComparer<MonthAttendanceStudentSourceProvider> 成員

        public int Compare(MonthAttendanceStudentSourceProvider xm, MonthAttendanceStudentSourceProvider ym)
        {
            if ( xm.Year > ym.Year ) return -1;
            if ( xm.Year < ym.Year ) return 1;
            if ( xm.Month > ym.Month ) return -1;
            else if ( xm.Month < ym.Month ) return 1;
            else return 0;
        }

        #endregion

        #region IComparable<MonthAttendanceStudentSourceProvider> 成員

        public int CompareTo(MonthAttendanceStudentSourceProvider other)
        {
            if ( this.Year > other.Year ) return -1;
            if ( this.Year < other.Year ) return 1;
            if ( this.Month > other.Month ) return -1;
            else if ( this.Month < other.Month ) return 1;
            else return 0;
        }

        #endregion
    }
    class DateAttendanceStudentSourceProvider : NormalizeStudentSourceProvider, IComparable<DateAttendanceStudentSourceProvider>, IComparer<DateAttendanceStudentSourceProvider>
    {
        private int _Day = DateTime.Now.Day;
        private int _Month = DateTime.Now.Month;

        public int Day
        {
            get { return _Day; }
            set
            {
                _Day = value;
                Text = "" + _Month + "/" + _Day;
            }
        }
        public int Month
        {
            get { return _Month; }
            set
            {
                _Month = value;
                Text = "" + _Month + "/" + _Day;
            }
        }

        #region IComparer<MonthAttendanceStudentSourceProvider> 成員

        public int Compare(DateAttendanceStudentSourceProvider xm, DateAttendanceStudentSourceProvider ym)
        {
            if ( xm.Month > ym.Month ) return -1;
            if ( xm.Month < ym.Month ) return 1;
            if ( xm.Day > ym.Day ) return -1;
            else if ( xm.Day < ym.Day ) return 1;
            else return 0;
        }

        #endregion

        #region IComparable<MonthAttendanceStudentSourceProvider> 成員

        public int CompareTo(DateAttendanceStudentSourceProvider other)
        {
            if ( this.Month > other.Month ) return -1;
            if ( this.Month < other.Month ) return 1;
            if ( this.Day > other.Day ) return -1;
            else if ( this.Day < other.Day ) return 1;
            else return 0;
        }

        #endregion
    }
    
}
