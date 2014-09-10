using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using SmartSchool.CourseRelated.Forms;
using DevComponents.DotNetBar.Rendering;

namespace SmartSchool.CourseRelated
{
    internal partial class CourseContentPane : UserControl, IPreference
    {
        private enum ListMode
        {
            Simple, Complete
        }

        //private const string Column_Simple = "CourseEntity_Column_Simple_Visible";
        //private const string Column_Complete = "CourseEntity_Column_Complete_Visible";
        private const string List_Mode = "CourseEntity_ListMode";

        private ISearchSource _search_source;
        private CourseCollection _selection_list;
        private bool _selection_changeing;
        private ListMode _current_list_mode;
        private bool _init_required;
        //private ColumnPreferences _simple, _complete;

        public CourseContentPane()
        {
            _init_required = true;

            InitializeComponent();

            if (Site != null && Site.DesignMode) return;

            _selection_changeing = false;
            _selection_list = new CourseCollection();

            dcmCourse.DenyColumn.Add(colCourseName);
            dcmCourse.DenyColumn.Add(HideBug);

            //_simple = new ColumnPreferences();
            //_complete = new ColumnPreferences();

            Application.Idle += new EventHandler(Application_Idle);
            PreferenceUpdater.Instance.Items.Add(this);

            if ( GlobalManager.Renderer is Office2007Renderer )
            {
                ( GlobalManager.Renderer as Office2007Renderer ).ColorTableChanged += new EventHandler(ContentInfo_ColorTableChanged);
                this._grid_view.AlternatingRowsDefaultCellStyle.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.MouseOver.TopBackground.End;
            }
        }

        void ContentInfo_ColorTableChanged(object sender, EventArgs e)
        {
            this._grid_view.AlternatingRowsDefaultCellStyle.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.MouseOver.TopBackground.End;
        }

        void Application_Idle(object sender, EventArgs e)
        {
            if (DetailPane.Manager == null)
                DetailPane.Manager = SmartSchool.CourseRelated.RibbonBars.Manage.Instance;
        }

        private void LoadPreference()
        {
            CoursePreference pref = CourseEntity.Instance.Preference;

            //_simple = pref.GetObject(Column_Simple) as ColumnPreferences;
            //_complete = pref.GetObject(Column_Complete) as ColumnPreferences;

            string list_mode = pref.GetString(List_Mode, Enum.GetName(typeof(ListMode), ListMode.Simple));
            CurrentListMode = (ListMode)Enum.Parse(typeof(ListMode), list_mode);
        }

        private void SavePreference()
        {
            CoursePreference pref = CourseEntity.Instance.Preference;

            //如果是 Null，代表 CourseEntity 沒有使用，當然就沒有需要儲存的 Preference。
            if (pref == null) return;

            pref.SetString(List_Mode, Enum.GetName(typeof(ListMode), CurrentListMode));
            //pref.SetObject(Column_Simple, _simple);
            //pref.SetObject(Column_Complete, _complete);
        }

        private ListMode CurrentListMode
        {
            get
            {
                return _current_list_mode;
            }
            set
            {
                _current_list_mode = value;
                SyncModeView();

                //if (_current_list_mode == ListMode.Simple)
                //    RecoveryColumnPreference(_simple);
                //else
                //    RecoveryColumnPreference(_complete);
            }
        }

        private void SyncModeView()
        {
            if (CurrentListMode == ListMode.Complete)
            {
                ListPaneControl.Dock = DockStyle.Fill;
                btnExpand.Text = "<<";

                foreach (DataGridViewColumn each in _grid_view.Columns)
                    each.Visible = true;
            }
            else if (CurrentListMode == ListMode.Simple)
            {
                ListPaneControl.Dock = DockStyle.Left;
                btnExpand.Text = ">>";
                DisplayFirstSelect();

                foreach (DataGridViewColumn each in _grid_view.Columns)
                    each.Visible = true;
                colTeacher.Visible = false;
                colSubject.Visible = false;
                colClass.Visible = false;
                colExamTemplate.Visible = false;
            }
            else
                throw new Exception("沒有此種檢視模式。");

            HideBug.Visible = false;

            splitterListDetial.Visible = (CurrentListMode == ListMode.Simple);
            DetailPaneContainer.Visible = (CurrentListMode == ListMode.Simple);
        }

        //private void RecoveryColumnPreference(ColumnPreferences cp)
        //{
        //    foreach (ColumnPreference each in cp)
        //    {
        //        if (_grid_view.Columns.Contains(each.Name))
        //        {
        //            DataGridViewColumn column = _grid_view.Columns[each.Name];

        //            column.DisplayIndex = each.DisplayIndex;
        //            column.Width = each.Width;
        //            column.Visible = each.Visible;
        //        }
        //    }
        //}

        private void GridView_SelectionChanged(object sender, EventArgs e)
        {
            //選擇正在改變中，不進行處理。
            if (_selection_changeing) return;

            SyncSelectionState();
        }

        private void _grid_view_DoubleClick(object sender, EventArgs e)
        {
            if (Selection.Count > 0)
            {
                CourseDetailForm.PopupDetail(Selection[0]);
            }
        }

        private void ctxDefault_PopupShowing(object sender, EventArgs e)
        {

        }

        private void ctxDefault_PopupOpen(object sender, DevComponents.DotNetBar.PopupOpenEventArgs e)
        {
            Point p = _grid_view.PointToClient(Control.MousePosition);
            DataGridView.HitTestInfo hit = _grid_view.HitTest(p.X, p.Y);

            if (hit.Type == DataGridViewHitTestType.ColumnHeader)
                e.Cancel = true;

            //看目前是否選在待處理節點。
            TreeNode node = CourseEntity.Instance.CurrentPerspective.CurrentView.SelectedNode as TemporalTreeNode;

            bool atPrepar = (node != null);

            ctxiAddPrepare.Visible = !atPrepar;
            ctxiRemovePrepare.Visible = atPrepar;
        }

        private void ctxiAddPrepare_Click(object sender, EventArgs e)
        {
            foreach (CourseInformation each in _selection_list)
                CourseEntity.Instance.Temporal.AddCourse(each);

        }

        private void ctxiRemovePrepare_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> willRemove = new List<DataGridViewRow>();

            foreach (CourseInformation each in _selection_list)
            {
                CourseEntity.Instance.Temporal.RemoveCourse(each);
                foreach (DataGridViewRow row in _grid_view.Rows)
                {
                    if (row.Tag == each)
                        willRemove.Add(row);
                }
            }

            foreach (DataGridViewRow each in willRemove)
                _grid_view.Rows.Remove(each);

            _selection_changeing = true;
            DeselectionAll();
            _selection_changeing = false;
            DetailPane.DisplayDetail(null);
        }

        private void SyncSelectionState()
        {
            _selection_list.Clear();
            foreach (DataGridViewRow row in _grid_view.SelectedRows)
            {
                CourseInformation course = row.Tag as CourseInformation;

                if (course != null)
                    _selection_list.Add(course);
            }

            DisplayFirstSelect();

            MotherForm.Instance.SetBarMessage("選擇了" + _selection_list.Count + "筆課程");

            //發出選擇改變事件。
            CourseEntity.Instance.InvokeSelectionChanged();
        }

        /// <summary>
        /// 改天在想...如何選擇真正的第一筆。
        /// </summary>
        private void DisplayFirstSelect()
        {
            if (_current_list_mode == ListMode.Complete) return;

            //顯示第一個課程的詳細資料。
            if (_selection_list.Count > 0)
                DetailPane.DisplayDetail(_selection_list[_selection_list.Count - 1]);
            else
                DetailPane.DisplayDetail(null);
        }

        private void TextSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                SearchAndDisplay();
        }

        private void DoSearch_Click(object sender, EventArgs e)
        {
            SearchAndDisplay();
        }

        private void btnExpand_Click(object sender, EventArgs e)
        {
            if (CurrentListMode == ListMode.Simple)
                CurrentListMode = ListMode.Complete;
            else
                CurrentListMode = ListMode.Simple;
        }

        public void FillListPane(ICourseInfoContainer courses)
        {
            //先將目前選擇的記錄到集合中。
            List<CourseInformation> selected = new List<CourseInformation>(_selection_list);
            _selection_list.Clear(); //清除目前選擇。

            //先讓 DetailPane 不顯示任何東西。
            DetailPane.DisplayDetail(null);

            //設定為 True ，讓 DataGridView 的 SelectionChange  事件暫時不工作。
            _selection_changeing = true;

            //將資料新增到 GridView中。
            AddItemsToGrid(courses);

            //設為搜尋的來源。
            SetSearchSource(courses);

            DeselectionAll();

            //讓 DataGridView 的 SelectionChanged 事件開始工作。
            _selection_changeing = false;

            //復原選擇。
            RecoverySelection(selected);
        }

        public void SelectAll()
        {
            _selection_changeing = true;
            foreach (DataGridViewRow each in _grid_view.Rows)
                each.Selected = true;
            _selection_changeing = false;
            SyncSelectionState();
        }

        private void DeselectionAll()
        {
            //把所有選擇的反選擇。
            foreach (DataGridViewRow row in _grid_view.SelectedRows)
                row.Selected = false;
        }

        private void RecoverySelection(List<CourseInformation> selected)
        {
            //將原本就是選擇的反白。
            foreach (DataGridViewRow row in _grid_view.Rows)
            {
                CourseInformation course = row.Tag as CourseInformation;

                foreach (CourseInformation each in selected)
                {
                    if (each.Identity == course.Identity)
                        row.Selected = true;
                }
            }

            //如果有選擇超過一筆，就把第一筆顯示在畫面上。
            DataGridViewSelectedRowCollection rows = _grid_view.SelectedRows;
            if (rows.Count > 0)
                _grid_view.FirstDisplayedScrollingRowIndex = rows[rows.Count - 1].Index;

            SyncSelectionState();
        }

        private void AddItemsToGrid(ICourseInfoContainer courses)
        {
            ClearListPane(); //先清除所有項目
            foreach (CourseInformation course in courses.GetCourses())
                AddGridItem(course);
        }

        internal void ClearListPane()
        {
            _grid_view.Rows.Clear();
        }

        private void SetSearchSource(ICourseInfoContainer courses)
        {
            //如果支援 ISearchSource 介面，就設為 Search 來源
            ISearchSource search_source = courses as ISearchSource;
            _search_source = search_source;
            _txt_search.Text = "";

            //設定 Search 的 Watermark。
            if (_search_source != null)
                _txt_search.WatermarkText = _search_source.Title;
            else
                _txt_search.WatermarkText = "";
        }

        public void FocusOnSearch()
        {
            _txt_search.Focus();
        }

        private void AddGridItem(CourseInformation course)
        {
            string credit;
            if (course.Credit == -1)
                credit = string.Empty;
            else
                credit = course.Credit.ToString();

            string teachers = string.Empty;
            if (course.Teachers.Count > 0)
            {
                foreach (CourseInformation.Teacher each in course.Teachers)
                    teachers += each.UniqName + ",";
                teachers = teachers.Substring(0, teachers.Length - 1);
            }

            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(_grid_view, "", course.CourseName, credit, course.Subject, course.ClassName, teachers, course.ExamTemplateName);
            row.Tag = course;

            _grid_view.Rows.Add(row);
        }

        private void SearchAndDisplay()
        {
            if (_search_source != null)
            {
                string pattern = _txt_search.Text.Replace("*", ".*").Replace(@"\", @"\\");

                Regex rx = new Regex(pattern, RegexOptions.Singleline);

                List<CourseInformation> result = new List<CourseInformation>();
                foreach (CourseInformation each in _search_source.GetCourses())
                {
                    if (MatchSearch(rx, each))
                    {
                        if (!result.Contains(each)) //一個課程可能會出現於兩個地方，例如以教師檢視時。
                            result.Add(each);
                    }
                }

                ClearListPane();
                foreach (CourseInformation each in result)
                    AddGridItem(each);
            }
        }

        private bool MatchSearch(Regex rx, CourseInformation each)
        {
            Match m;
            if (chkSearchCourseName.Checked)
            {
                m = rx.Match(each.CourseName);
                if (m.Success)
                    return true;
            }

            if (chkSearchClass.Checked)
            {
                m = rx.Match(each.ClassName);
                if (m.Success) return true;
            }

            if (chkSearchTeacher.Checked)
            {
                foreach (CourseInformation.Teacher t in each.Teachers)
                {
                    m = rx.Match(t.TeacherName);
                    if (m.Success) return true;
                }
            }

            return false;
        }

        public Panel ContentPanel
        {
            get { return ContentPaneControl; }
        }

        public CourseCollection Selection
        {
            get { return _selection_list; }
        }

        public void Activate()
        {
            if (_init_required)
            {
                LoadPreference();
                _init_required = false;
            }
        }

        #region IPreference 成員

        public void UpdatePreference()
        {
            SavePreference();
        }

        #endregion

    }
}
