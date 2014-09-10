using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using DevComponents.DotNetBar.Controls;
using SmartSchool.Feature.Course;
using System.Xml;
using IntelliSchool.DSA30.Util;
using SmartSchool.CourseRelated.Forms;
using SmartSchool.Properties;
using SmartSchool.CourseRelated.DetailPaneItem;
using SmartSchool.CourseRelated.Navigation.Nodes;
using SmartSchool.CourseRelated.Navigation.Nodes;
using DevComponents.DotNetBar.Rendering;
using System.Threading;

namespace SmartSchool.CourseRelated
{
    public partial class CourseEntity : BaseForm, IEntity
    {
        private static CourseEntity _Instance;

        #region Singleton
        public static CourseEntity Instance
        {
            get
            {
                if (_Instance != null)
                    return _Instance;
                else
                {
                    throw new Exception("請先呼叫 CreateInstance()");
                }
            }
        }

        internal static void CreateInstance()
        {
            _Instance = new CourseEntity();
        }

        internal void SetupSynchronization()
        {

        }
        #endregion

        /// <summary>
        /// Perspective 看法,觀點。
        /// </summary>
        private enum Perspective
        {
            Teacher,
            Subject,
            Class
        }

        private const string PERSPECTIVE = "CourseEntity_Perspective";
        private const string PERSPECTIVEOPTIONSEXPANDED = "CourseEntity_PrespectiveOptionsExpanded";

        private CourseDataSource _course_source;

        private Perspective _current_perspective;
        private SemesterInfo _current_semester;
        private Dictionary<Perspective, INavigatePerspective> _perspectives;
        private CoursePreference _preference;
        private SemesterManager _semester_manager;
        private TemporalHandler _temporal_handler;
        private bool _initial_required, _block_source_refresh;
        private CourseCollection _Selection;

        private ManualResetEvent _loading;

        public CourseEntity()
        {
            Font = FontStyles.General;
            InitializeComponent();
            _Selection = new CourseCollection();
            //設計模式不執行下列程式碼。
            if (Site != null && Site.DesignMode)
                return;

            _initial_required = true;
            _block_source_refresh = false;

            TreeClass.Dock = DockStyle.Fill;
            TreeSubject.Dock = DockStyle.Fill;
            TreeTeacher.Dock = DockStyle.Fill;

            //CourseInserted += new EventHandler(CourseEntity_AfterCourseInsert);
            SmartSchool.Broadcaster.Events.Items["課程/新增"].AddAfterInvokeHandler(delegate
            {
                if ( _course_source != null )
                    _course_source.Sync();

                RefreshAllPerspective();
            });
            CourseDeleted += new EventHandler<CourseDeleteEventArgs>(CourseEntity_AfterCourseDelete);
            CourseChanged += new EventHandler<CourseChangeEventArgs>(CourseEntity_AfterCourseChange);

            InitialPreference();

            _loading = new ManualResetEvent(true);

#if CourseDevelope
            InitialNavigation();
#endif
            #region 如果系統的Renderer是Office2007Renderer，同化_ClassTeacherView,_CategoryView的顏色
            if (GlobalManager.Renderer is Office2007Renderer)
            {
                ((Office2007Renderer)GlobalManager.Renderer).ColorTableChanged += new EventHandler(ScoreCalcRuleEditor_ColorTableChanged);
            }
            #endregion
        }
        void ScoreCalcRuleEditor_ColorTableChanged(object sender, EventArgs e)
        {
            SetForeColor(this);
        }

        private void SetForeColor(Control parent)
        {
            foreach (Control var in parent.Controls)
            {
                if (var is RadioButton)
                    var.ForeColor = ((Office2007Renderer)GlobalManager.Renderer).ColorTable.CheckBoxItem.Default.Text;
                SetForeColor(var);
            }
        }

        private void CourseEntity_AfterCourseChange(object sender, CourseChangeEventArgs e)
        {
            if (_course_source != null)
                _course_source.Sync();

            RefreshAllPerspective();
        }

        private void CourseEntity_AfterCourseDelete(object sender, CourseDeleteEventArgs e)
        {
            if (_course_source != null)
                _course_source.Sync();

            RefreshAllPerspective();
        }

        //private void CourseEntity_AfterCourseInsert(object sender, EventArgs e)
        //{
        //    if (_course_source != null)
        //        _course_source.Sync();

        //    RefreshAllPerspective();
        //}

        private void RefreshAllPerspective()
        {
            if (_perspectives != null)
            {
                try
                {
                    (_perspectives[Perspective.Teacher] as NavigateBase).RefreshTreeView();
                    (_perspectives[Perspective.Class] as NavigateBase).RefreshTreeView();
                    (_perspectives[Perspective.Subject] as NavigateBase).RefreshTreeView();
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message);
                }
            }
        }

        private void Temporal_Handler_ContentChanged(object sender, EventArgs e)
        {
            if (TemporalChanged != null)
                TemporalChanged(this, EventArgs.Empty);
        }

        private void LoadCourseWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            double t1 = Environment.TickCount;

            _loading.Reset();

            //從個人設定中讀取導覽方式。
            SetPerspectiveByPreference();

            //載入所有的學期。
            if (_semester_manager == null)
                _semester_manager = new SemesterManager();

            _semester_manager.SemesterListChange += new EventHandler(SemesterListChange);

            FillSemesterList(); //將資料填入 ComboBox 中。

            CurrentUser user = CurrentUser.Instance;

            //初始化課程資料管理物件。
            _course_source = new CourseDataSource(user.SchoolYear, user.Semester);
            _current_semester = FindSemesterComboBoxItem(user.SchoolYear, user.Semester);
            _course_source.Sync(); //同步課程資料到用戶端。

            //建立所有會用到的導覽方式。
            //TreeView 與資料的互動程式碼都在 INavigatePerspective 的實作裡。
            _perspectives = new Dictionary<Perspective, INavigatePerspective>();
            _perspectives.Add(Perspective.Teacher, new NavigateByTeacher(_course_source));
            _perspectives.Add(Perspective.Subject, new NavigateBySubject(_course_source));
            _perspectives.Add(Perspective.Class, new NavigateByClass(_course_source));

            _temporal_handler = new TemporalHandler();
            _temporal_handler.ContentChanged += new EventHandler(Temporal_Handler_ContentChanged);
            foreach (INavigatePerspective each in _perspectives.Values)
                each.SetTemporalHandler(_temporal_handler);

            Console.WriteLine("Working Time：" + (Environment.TickCount - t1));

            _loading.Set();
        }

        private void LoadCourseWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            double t1 = Environment.TickCount;

            _perspectives[Perspective.Class].BindView(TreeClass);
            _perspectives[Perspective.Subject].BindView(TreeSubject);
            _perspectives[Perspective.Teacher].BindView(TreeTeacher);

            if (_current_perspective == Perspective.Class)
                chkClass.Checked = true;
            else if (_current_perspective == Perspective.Subject)
                chkSubject.Checked = true;
            else if (_current_perspective == Perspective.Teacher)
                chkTeacher.Checked = true;

            _block_source_refresh = true;
            SelectComboBoxItemByCurrent();
            _block_source_refresh = false;

            epPrespective.Enabled = true;

            //讀取 Perspective Options 是否展開的設定值。
            epPrespective.Expanded = Preference.GetBoolean(PERSPECTIVEOPTIONSEXPANDED, true);

            HideLoadding();

            Console.Write("Complete Time：" + (Environment.TickCount - t1));
        }

        /// <summary>
        /// 尋找 ComboBox 中的 SemesterInfo 物件，如果找不到則回傳 Null。
        /// </summary>
        private SemesterInfo FindSemesterComboBoxItem(int schoolyear, int semester)
        {
            foreach (object each in cboSemesters.Items)
            {
                SemesterInfo sems = each as SemesterInfo;
                if (sems != null)
                {
                    if (sems.SchoolYear == schoolyear && sems.Semester == semester)
                        return sems;
                }
            }

            return null;
        }

        private void SelectComboBoxItemByCurrent()
        {
            CurrentUser user = CurrentUser.Instance;
            SemesterInfo sems = FindSemesterComboBoxItem(user.SchoolYear, user.Semester);
            _current_semester = sems; //設定目前的選擇。

            //這時會引發 ComboBox 的 SelectedIndexChanged 事件，會進行課程資料的載入。
            cboSemesters.SelectedItem = sems;
        }

        private void SemesterListChange(object sender, EventArgs e)
        {
            FillSemesterList();
        }

        private delegate void NoneParametereInvoke();

        private void FillSemesterList()
        {
            if (cboSemesters.InvokeRequired)
                cboSemesters.Invoke(new NoneParametereInvoke(FillSemesterList));
            else
            {
                cboSemesters.DisplayMember = "DisplayName";
                cboSemesters.SelectedItem = null;
                cboSemesters.Items.Clear();
                cboSemesters.Items.AddRange(_semester_manager.Semesters.ToArray());
            }
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DisplayNodeContent(e.Node);
        }

        private void TreeView_NodeMouseClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            DisplayNodeContent(e.Node);
        }

        private void NavigateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton check = sender as RadioButton;

            if (check.Checked)
            {
                object new_perspective = Enum.Parse(typeof(Perspective), check.Tag.ToString());
                ChangeNavigateView((Perspective)new_perspective);
            }
        }

        private void cboSemesters_SelectedIndexChanged(object sender, EventArgs e)
        {
            SemesterInfo sems = cboSemesters.SelectedItem as SemesterInfo;
            _current_semester = sems;

            SyncNavigationTitle();

            //_block_source_refresh 是 false 才執行。
            if (sems != null && !_block_source_refresh)
            {
                _course_source.Sync(sems.SchoolYear, sems.Semester);
                RefreshAllPerspective();
            }
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            ContentPane.FillListPane(new ServiceSearchSource(_course_source));
            ContentPane.FocusOnSearch();
        }

        private void epPrespective_ExpandedChanged(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            Preference.SetBoolean(PERSPECTIVEOPTIONSEXPANDED, epPrespective.Expanded);
        }

        private void ctxiRefresh_Click(object sender, EventArgs e)
        {
            _course_source.Sync();
        }

        private void SyncNavigationTitle()
        {
            SemesterInfo sems = _current_semester;
            string title;

            if (_current_perspective == Perspective.Subject)
                title = chkSubject.Text;
            else if (_current_perspective == Perspective.Class)
                title = chkClass.Text;
            else if (_current_perspective == Perspective.Teacher)
                title = chkTeacher.Text;
            else
                title = "不支援此種檢視";

            if (sems.SchoolYear == -1 || sems.Semester == -1)
                eppCourseNavigation.TitleText = title + " [未定]";
            else
                eppCourseNavigation.TitleText = string.Format("{0} {1}", title, sems.ShortName);
        }

        /// <summary>
        /// 顯示 TreeNode 裡面所包含的課程資料到 ListPane 上。
        /// </summary>
        public void DisplayNodeContent(TreeNode e)
        {
            ICourseInfoContainer node = e as ICourseInfoContainer;

            if (node != null)
            {
                if (Control.ModifierKeys == Keys.Control)
                {
                    ContentPane.FillListPane(new RecursiveNodeContainer(node as ICourseInfoContainer));
                    ContentPane.SelectAll();
                }
                else
                    ContentPane.FillListPane(node);
            }
            else
                ContentPane.ClearListPane();
        }

        private void SetPerspectiveByPreference()
        {
            string value = Preference.GetString(PERSPECTIVE, "Subject");
            Perspective user_previous = (Perspective)Enum.Parse(typeof(Perspective), value);
            _current_perspective = user_previous;
        }

        /// <summary>
        /// 記錄課程相關的個人化設定。
        /// </summary>
        internal CoursePreference Preference
        {
            get { return _preference; }
        }

        private void InitialPreference()
        {
            XmlElement xmlpreference = CurrentUser.Instance.Preference["CourseEntity"];
            if (xmlpreference == null)
            {
                CurrentUser.Instance.Preference["CourseEntity"] = new XmlDocument().CreateElement("CourseEntity");
                xmlpreference = CurrentUser.Instance.Preference["CourseEntity"];
            }
            _preference = new CoursePreference(xmlpreference);
        }

        /// <summary>
        /// 改變畫面上的導覽方式。
        /// </summary>
        private void ChangeNavigateView(Perspective perspective)
        {
            _current_perspective = perspective;
            SyncNavigateView();
            SyncNavigationTitle();

            //儲存目前設定。
            Preference.SetString(PERSPECTIVE, _current_perspective.ToString());
        }

        /// <summary>
        /// 同步畫面上的導覽方式。
        /// </summary>
        private void SyncNavigateView()
        {
            //先將要顯示的先顯示。
            _perspectives[_current_perspective].Show();

            //再將要穩藏的穩藏，避免畫面閃動。
            foreach (INavigatePerspective each in _perspectives.Values)
            {
                if (_perspectives[_current_perspective] != each)
                    each.Hide();
            }

            //更新 ListPane 的資料。
            DisplayNodeContent(_perspectives[_current_perspective].CurrentView.SelectedNode);
        }

        private void InitialNavigation()
        {
            if (_initial_required)
            {
                _initial_required = false;

                //初始化各項設定。
                InitialPreference();

                //載入完成前，不可以讓使用者選。
                epPrespective.Enabled = false;

                //載入課程相關資料
                ShowLoadding();
                LoadCourseWorker.RunWorkerAsync();
            }
        }

        private void ShowLoadding()
        {
            gif_loadding.Show();
        }

        private void HideLoadding()
        {
            gif_loadding.Hide();
        }

        internal SemesterManager Semesters
        {
            get
            {
                if (_semester_manager == null)
                    _semester_manager = new SemesterManager();

                return _semester_manager;
            }
        }

        internal TemporalHandler Temporal
        {
            get { return _temporal_handler; }
        }

        internal INavigatePerspective CurrentPerspective
        {
            get
            {
                return _perspectives[_current_perspective];
            }
        }

        internal void DisplayDetail(int courseId)
        {
            try
            {
                DSResponse rsp = QueryCourse.GetCourseDetail(courseId);

                CourseInformation course = new CourseInformation(rsp.GetContent().GetElement("Course"));
                CourseDetailForm.PopupDetail(course);
            }
            catch (Exception ex)
            {
                MsgBox.Show("顯示課程資料錯誤：" + ex.Message);
            }
        }

        #region IEntity 成員

        public string Title
        {
            get { return "課程"; }
        }

        public DevComponents.DotNetBar.NavigationPanePanel NavPanPanel
        {
            get { return NavigationPaneControl; }
        }

        /// <summary>
        /// IEntity 成員。
        /// </summary>
        public Panel ContentPanel
        {
            get
            {
                return ContentPane.ContentPanel;
            }
        }

        internal CourseContentPane CourseContentPane
        {
            get { return ContentPane; }
        }

        public Image Picture
        {
            get { return Resources.Navigation_Course_New; }
        }

        public void Actived()
        {
#if !CourseDevelope
            InitialNavigation();
            ContentPane.Activate();
#endif
        }

        #endregion

        #region Entity Events
        //public event EventHandler CourseInserted;

        public event EventHandler<CourseDeleteEventArgs> CourseDeleted;

        public event EventHandler<CourseChangeEventArgs> CourseChanged;

        public event EventHandler SelectionChanged;

        public event EventHandler TemporalChanged;

        public event EventHandler ForeignTableChanged;

        //public void InvokeAfterCourseInsert()
        //{
        //    if (CourseInserted != null)
        //        CourseInserted(this, EventArgs.Empty);
        //}

        public void InvokeAfterCourseDelete(int courseId)
        {
            if (_course_source != null)
                _course_source.ReloadCourseInformation(courseId);

            if ( CourseDeleted != null )
                CourseDeleted(this, new CourseDeleteEventArgs(courseId));
        }

        public void InvokeAfterCourseChange(params int[] courseId)
        {
            if ( courseId.Length == 0 ) return;
            List<int> courses = new List<int>();
            courses.AddRange(courseId);

            if (_course_source != null)
                _course_source.ReloadCourseInformation(courseId);

            if (CourseChanged != null)
                CourseChanged(this, new CourseChangeEventArgs(courses));
        }

        public void InvokeSelectionChanged()
        {
            _Selection = ContentPane.Selection;
            if (SelectionChanged != null)
                SelectionChanged(this, EventArgs.Empty);
        }

        public void InvokeForeignTableChanged()
        {
            if (ForeignTableChanged != null)
                ForeignTableChanged(this, EventArgs.Empty);
        }

        public CourseCollection SelectionCourse
        {
            get { return _Selection; }
        }

        /// <summary>
        /// 待處理的課程。
        /// </summary>
        public CourseCollection TemporalCourse
        {
            get
            {
                if (Temporal == null)
                    return new CourseCollection();
                return Temporal.Courses;
            }
        }

        internal CourseDictionary Items
        {
            get
            {
                if ( _course_source == null )
                {
                    _course_source = new CourseDataSource(CurrentUser.Instance.SchoolYear, CurrentUser.Instance.Semester);
                    _course_source.EnsureCourse(CurrentUser.Instance.SchoolYear, CurrentUser.Instance.Semester);
                }
                return _course_source.CourseDictionary;
            }
        }

        /// <summary>
        /// 保障擁有學年度學期的課程資料
        /// </summary>
        internal void EnsureCourse(int schoolyear, int semester)
        {
            if (_course_source == null)
                _course_source = new CourseDataSource(CurrentUser.Instance.SchoolYear, CurrentUser.Instance.Semester);
            _course_source.EnsureCourse(schoolyear, semester);
        }
        #endregion

        public CourseInformation FindByCourseID(int courseId)
        {
            //DSResponse rsp = QueryCourse.GetCourseById(courseId.ToString());
            //CourseInformation courseInfo = new CourseInformation(rsp.GetContent().GetElement("Course"));
            //return courseInfo;
            return this.Items[""+courseId];
        }

        public int FindSCAttendByCourseID(int courseId)
        {
            int attendCount = 0;

            DSResponse rsp = QueryCourse.GetSCAttend(courseId.ToString());
            foreach (XmlElement var in rsp.GetContent().GetElements("Student"))
            {
                attendCount++;
            }

            return attendCount;

        }

        //暫時的解。檢查課程是否重複。2008/3/14
        public bool ValidateCourse(string course_name, string schoolyear, string semester)
        {
            _loading.WaitOne();

            if (string.IsNullOrEmpty(course_name)) return false;
            foreach (CourseInformation info in _course_source.CourseList)
            {
                bool name_check = (info.CourseName == course_name);
                bool schoolyear_check = (info.SchoolYear.ToString() == schoolyear);
                bool semester_check = (info.Semester.ToString() == semester);

                if (name_check && schoolyear_check && semester_check)
                    return false;
            }
            return true;
        }
    }
}