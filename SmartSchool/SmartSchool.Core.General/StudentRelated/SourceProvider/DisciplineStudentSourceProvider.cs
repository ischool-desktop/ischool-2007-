using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.API.StudentExtension;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.StudentRelated.Divider;

namespace SmartSchool.StudentRelated.SourceProvider
{
    class SemesterDisciplineStudentSourceProvider : NormalizeStudentSourceProvider, IComparable<SemesterDisciplineStudentSourceProvider>, IComparer<SemesterDisciplineStudentSourceProvider>
    {
        private int _SchoolYear = CurrentUser.Instance.SchoolYear;
        private int _Semester = CurrentUser.Instance.Semester;
        private BackgroundWorker _BkwLoader = new BackgroundWorker();
        private TreeNode _LoadingNode = new TreeNode("讀取中...");
        private Dictionary<string, TreeNode> _PathNodes = new Dictionary<string, TreeNode>();
        private Dictionary<string, List<Reward>> _StudentDiscipline = null;
        private List<TreeNode> _RelatedNodes = null;
        private ISetupDisciplineView _Setup;

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

        public SemesterDisciplineStudentSourceProvider(int schoolYear, int semester, List<TreeNode> relatedNodeCollection, ISetupDisciplineView setup)
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
            Dictionary<string, List<Reward>> marks = new Dictionary<string, List<Reward>>();
            #region 把有案底的都抓出來檢驗
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            helper.AddElement("Condition", "SchoolYear", schoolyear.ToString());
            helper.AddElement("Condition", "Semester", semester.ToString());
            foreach ( XmlElement var in SmartSchool.Feature.Student.QueryDiscipline.GetDiscipline(new DSRequest(helper)).GetContent().GetElements("Discipline") )
            {
                int schoolyear2 = 0;
                int.TryParse(var.SelectSingleNode("SchoolYear").InnerText, out schoolyear2);
                int semester2 = 0;
                int.TryParse(var.SelectSingleNode("Semester").InnerText, out semester2);
                DateTime occurdate;
                DateTime.TryParse(var.SelectSingleNode("OccurDate").InnerText, out occurdate);
                string studentID = var.SelectSingleNode("RefStudentID").InnerText;

                string occurplace = "";
                string occurreason = var.SelectSingleNode("Reason").InnerText;

                int awardA = 0;
                int awardB = 0;
                int awardC = 0;
                int faultA = 0;
                int faultB = 0;
                int faultC = 0;
                bool cleared = false;
                DateTime cleardate = DateTime.Today;
                string clearreason = "";
                bool ultimateAdmonition = false;

                DSXmlHelper helper2 = new DSXmlHelper(var);
                switch ( var.SelectSingleNode("MeritFlag").InnerText )
                {
                    case "1":
                        int.TryParse(helper2.GetText("Detail/Discipline/Merit/@A"), out awardA);
                        int.TryParse(helper2.GetText("Detail/Discipline/Merit/@B"), out awardB);
                        int.TryParse(helper2.GetText("Detail/Discipline/Merit/@C"), out awardC);
                        break;
                    case "0":
                        int.TryParse(helper2.GetText("Detail/Discipline/Demerit/@A"), out faultA);
                        int.TryParse(helper2.GetText("Detail/Discipline/Demerit/@B"), out faultB);
                        int.TryParse(helper2.GetText("Detail/Discipline/Demerit/@C"), out faultC);
                        cleared = ( helper2.GetText("Detail/Discipline/Demerit/@Cleared") == "是" ) ? true : false;
                        DateTime.TryParse(helper2.GetText("Detail/Discipline/Demerit/@ClearDate"), out cleardate);
                        clearreason = helper2.GetText("Detail/Discipline/Demerit/@ClearReason");
                        break;
                    case "2":
                        ultimateAdmonition = true;
                        break;
                    default:
                        break;
                }

                Reward rewardInfo = new Reward(schoolyear2, semester2, occurdate, occurplace, occurreason, new int[] { awardA, awardB, awardC }, new int[] { faultA, faultB, faultC }, cleared, cleardate, clearreason, ultimateAdmonition, var);

                if ( !marks.ContainsKey(studentID) )
                    marks.Add(studentID, new List<Reward>());
                marks[studentID].Add(rewardInfo);
            }
            #endregion
            //取得學生相關資料
            StudentRelated.Student.Instance.Items.EnsureStudent(marks.Keys);
            e.Result = marks;
        }

        void _BkwLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ( e.Result == null ) return;
            Student.Instance.DisciplineChanged += new EventHandler<StudentDisciplineChangedEventArgs>(Instance_DisciplineChanged);
            //每個學生編號在此學期的缺曠時間
            _StudentDiscipline = (Dictionary<string, List<Reward>>)e.Result;
            FillNodes();
        }

        void Instance_DisciplineChanged(object sender, StudentDisciplineChangedEventArgs e)
        {
            Dictionary<string, List<Reward>> marks = new Dictionary<string, List<Reward>>();
            #region 查變動學生的案底
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            foreach ( BriefStudentData student in e.Students)
            {
                helper.AddElement("Condition", "RefStudentID", student.ID);
                if ( _StudentDiscipline.ContainsKey(student.ID) )
                    _StudentDiscipline.Remove(student.ID);
            }
            helper.AddElement("Condition", "SchoolYear", _SchoolYear.ToString());
            helper.AddElement("Condition", "Semester", _Semester.ToString());
            foreach ( XmlElement var in SmartSchool.Feature.Student.QueryDiscipline.GetDiscipline(new DSRequest(helper)).GetContent().GetElements("Discipline") )
            {
                int schoolyear2 = 0;
                int.TryParse(var.SelectSingleNode("SchoolYear").InnerText, out schoolyear2);
                int semester2 = 0;
                int.TryParse(var.SelectSingleNode("Semester").InnerText, out semester2);
                DateTime occurdate;
                DateTime.TryParse(var.SelectSingleNode("OccurDate").InnerText, out occurdate);
                string studentID = var.SelectSingleNode("RefStudentID").InnerText;

                string occurplace = "";
                string occurreason = var.SelectSingleNode("Reason").InnerText;

                int awardA = 0;
                int awardB = 0;
                int awardC = 0;
                int faultA = 0;
                int faultB = 0;
                int faultC = 0;
                bool cleared = false;
                DateTime cleardate = DateTime.Today;
                string clearreason = "";
                bool ultimateAdmonition = false;

                DSXmlHelper helper2 = new DSXmlHelper(var);
                switch ( var.SelectSingleNode("MeritFlag").InnerText )
                {
                    case "1":
                        int.TryParse(helper2.GetText("Detail/Discipline/Merit/@A"), out awardA);
                        int.TryParse(helper2.GetText("Detail/Discipline/Merit/@B"), out awardB);
                        int.TryParse(helper2.GetText("Detail/Discipline/Merit/@C"), out awardC);
                        break;
                    case "0":
                        int.TryParse(helper2.GetText("Detail/Discipline/Demerit/@A"), out faultA);
                        int.TryParse(helper2.GetText("Detail/Discipline/Demerit/@B"), out faultB);
                        int.TryParse(helper2.GetText("Detail/Discipline/Demerit/@C"), out faultC);
                        cleared = ( helper2.GetText("Detail/Discipline/Demerit/@Cleared") == "是" ) ? true : false;
                        DateTime.TryParse(helper2.GetText("Detail/Discipline/Demerit/@ClearDate"), out cleardate);
                        clearreason = helper2.GetText("Detail/Discipline/Demerit/@ClearReason");
                        break;
                    case "2":
                        ultimateAdmonition = true;
                        break;
                    default:
                        break;
                }

                Reward rewardInfo = new Reward(schoolyear2, semester2, occurdate, occurplace, occurreason, new int[] { awardA, awardB, awardC }, new int[] { faultA, faultB, faultC }, cleared, cleardate, clearreason, ultimateAdmonition, var);

                if ( !marks.ContainsKey(studentID) )
                    marks.Add(studentID, new List<Reward>());
                marks[studentID].Add(rewardInfo);
            }
            #endregion
            foreach ( string id in marks.Keys )
            {
                if ( _StudentDiscipline.ContainsKey(id) )
                    _StudentDiscipline[id] = marks[id];
                else
                    _StudentDiscipline.Add(id, marks[id]);
            }
            FillNodes();
        }

        private void FillNodes()
        {
            TreeNode selectNode = this.TreeView != null ? this.TreeView.SelectedNode : null;
            //移除讀取中節點
            this.Nodes.Clear();
            #region 填入子節點
            List<BriefStudentData> students = new List<BriefStudentData>();
            Dictionary<string, List<BriefStudentData>> monthList = new Dictionary<string, List<BriefStudentData>>();
            foreach ( string var in _StudentDiscipline.Keys )
            {
                //#region 學生狀態篩選
                ////不看一般生
                //if ( !_Setup.檢視一般生 && Student.Instance.Items[var].Status == "一般" )
                //    continue;
                ////不看延修生
                //if ( !_Setup.檢視延修生 && Student.Instance.Items[var].Status == "延修" )
                //    continue;
                ////不看休學學生
                //if ( !_Setup.檢視休學學生 && Student.Instance.Items[var].Status == "休學" )
                //    continue;
                ////不看畢業及離校學生
                //if ( !_Setup.檢視畢業及離校學生 && Student.Instance.Items[var].Status == "畢業或離校" )
                //    continue;
                ////不看已刪除學生
                //if ( !_Setup.檢視已刪除學生 && Student.Instance.Items[var].Status == "刪除" )
                //    continue;
                //#endregion
                if ( !_List.Contains(var) )//不是篩選過的學生
                    continue;
                bool countInStudent = false;
                //用時間整理
                foreach ( Reward date in _StudentDiscipline[var] )
                {
                    #region 檢驗這筆獎懲紀錄
                    int a1 = date.AwardA;
                    int a2 = date.AwardB;
                    int a3 = date.AwardC;
                    int b1 = date.FaultA;
                    int b2 = date.FaultB;
                    int b3 = date.FaultC;
                    bool u = date.UltimateAdmonition;
                    bool c = date.Cleared;
                    if ( !_Setup.檢視大功 ) a1 = 0;
                    if ( !_Setup.檢視小功 ) a2 = 0;
                    if ( !_Setup.檢視嘉獎 ) a3 = 0;
                    if ( !_Setup.檢視大過 ) b1 = 0;
                    if ( !_Setup.檢視小過 ) b2 = 0;
                    if ( !_Setup.檢視警告 ) b3 = 0;
                    if ( !_Setup.檢視留校察看 ) u = false;
                    if ( !_Setup.檢視已銷過紀錄 && c ) continue;
                    #endregion
                    if ( a1 + a2 + a3 + b1 + b2 + b3 > 0 || u )
                    {
                        countInStudent = true;
                        //整理成月份
                        string monthPath = "" + date.OccurDate.Year + "_" + date.OccurDate.Month + "";
                        if ( !monthList.ContainsKey(monthPath) )
                            monthList.Add(monthPath, new List<BriefStudentData>());
                        if ( !monthList[monthPath].Contains(Student.Instance.Items[var]) )
                            monthList[monthPath].Add(Student.Instance.Items[var]);
                    }
                }
                if ( countInStudent )
                    students.Add(Student.Instance.Items[var]);
            }
            students.Sort();
            Source = students;
            #region 整理每個月分的資料
            List<MonthDisciplineStudentSourceProvider> monthProviders = new List<MonthDisciplineStudentSourceProvider>();
            foreach ( string monthpath in monthList.Keys )
            {
                MonthDisciplineStudentSourceProvider monthNode;
                //沒這月的節點就新增一個
                if ( !_PathNodes.ContainsKey(monthpath) )
                {
                    monthNode = new MonthDisciplineStudentSourceProvider();
                    monthNode.Year = int.Parse(monthpath.Split("_".ToCharArray())[0]);
                    monthNode.Month = int.Parse(monthpath.Split("_".ToCharArray())[1]);
                    monthNode.DisplaySource = true;
                    //登紀關聯
                    _PathNodes.Add(monthpath, monthNode);
                    _RelatedNodes.Add(monthNode);
                }
                else
                    monthNode = (MonthDisciplineStudentSourceProvider)_PathNodes[monthpath];
                monthList[monthpath].Sort();
                monthNode.Source = monthList[monthpath];
                monthProviders.Add(monthNode);
            }
            monthProviders.Sort();
            this.Nodes.AddRange(monthProviders.ToArray());
            #endregion
            #endregion
            if ( this.TreeView != null )
                this.TreeView.SelectedNode = selectNode;
        }
        private List<string> _List = new List<string>();
        public void Reflash(List<string> list)
        {
            _List = list;
            if ( _BkwLoader.IsBusy )
                return;
            if ( _StudentDiscipline == null  )
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

        public int Compare(SemesterDisciplineStudentSourceProvider xnode, SemesterDisciplineStudentSourceProvider ynode)
        {
            if ( xnode.SchoolYear > ynode.SchoolYear ) return -1;
            if ( xnode.SchoolYear < ynode.SchoolYear ) return 1;
            if ( xnode.Semester > ynode.Semester ) return -1;
            else if ( xnode.Semester < ynode.Semester ) return 1;
            else return 1;
        }

        #endregion

        #region IComparable<SemesterDisciplineStudentSourceProvider> 成員

        public int CompareTo(SemesterDisciplineStudentSourceProvider other)
        {
            if ( this.SchoolYear > other.SchoolYear ) return -1;
            if ( this.SchoolYear < other.SchoolYear ) return 1;
            if ( this.Semester > other.Semester ) return -1;
            else if ( this.Semester < other.Semester ) return 1;
            else return 0;
        }

        #endregion
    }
    class MonthDisciplineStudentSourceProvider : NormalizeStudentSourceProvider, IComparable<MonthDisciplineStudentSourceProvider>, IComparer<MonthDisciplineStudentSourceProvider>
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

        #region IComparer<MonthDisciplineStudentSourceProvider> 成員

        public int Compare(MonthDisciplineStudentSourceProvider xm, MonthDisciplineStudentSourceProvider ym)
        {
            if ( xm.Year > ym.Year ) return -1;
            if ( xm.Year < ym.Year ) return 1;
            if ( xm.Month > ym.Month ) return -1;
            else if ( xm.Month < ym.Month ) return 1;
            else return 0;
        }

        #endregion

        #region IComparable<MonthDisciplineStudentSourceProvider> 成員

        public int CompareTo(MonthDisciplineStudentSourceProvider other)
        {
            if ( this.Year > other.Year ) return -1;
            if ( this.Year < other.Year ) return 1;
            if ( this.Month > other.Month ) return -1;
            else if ( this.Month < other.Month ) return 1;
            else return 0;
        }

        #endregion
    }

}
