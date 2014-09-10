using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using System.Xml;
using SmartSchool.TagManage;
using StudentItem = SmartSchool.TagManage.EntityItem;
using SmartSchool.Feature.Tag;
using SmartSchool.ApplicationLog;

namespace SmartSchool.StudentRelated
{
    public partial class StudentTagForm : BaseForm
    {
        private TagManager _stu_tag_man;
        private StudentTagManager _stu_tag_sel;
        private Dictionary<int, StudentTagRow> _state_rows;

        //暫解，記錄顏色排序的箭頭
        private SortOrder _currentSortOrder = SortOrder.None;

        public StudentTagForm()
        {
            InitializeComponent();

            sdgTagList.RowCheckStateChanged += new RowCheckedEventHandler(SDGTagList_RowCheckStateChanged);
        }

        private void TagManager_Load(object sender, EventArgs e)
        {
            _stu_tag_man = Student.Instance.TagManager;
            _stu_tag_man.TagRefresh += new EventHandler(TagManager_TagRefresh);
            _stu_tag_sel = Student.Instance.SelectionTagManager;
            _state_rows = new Dictionary<int, StudentTagRow>();

            CreateStateRows();
            SynchronizeRowState(false);
            RefreshGroupCombobox();
        }

        private void TagManager_TagRefresh(object sender, EventArgs e)
        {
            RefreshAllInformation();
        }

        private void RefreshAllInformation()
        {
            Dictionary<int, CheckState> origin_states = new Dictionary<int, CheckState>();
            foreach (StudentTagRow each in _state_rows.Values)
                origin_states.Add(each.TagIdentity, each.CurrentState);

            _state_rows = new Dictionary<int, StudentTagRow>();
            CreateStateRows();
            SynchronizeRowState(true);

            foreach (StudentTagRow each in _state_rows.Values)
            {
                if (origin_states.ContainsKey(each.TagIdentity))
                    each.CurrentState = origin_states[each.TagIdentity];
            }

            RefreshGroupCombobox();
        }

        private void CreateStateRows()
        {
            InitialImageList();
            foreach (TagInfo each in _stu_tag_man.Tags.Values)
            {
                StudentTagRow row = new StudentTagRow(each);

                row.CreateCells(sdgTagList, StateDataGridView.UncheckedImage,
                    imgTagColor.Images[each.Color.ToArgb().ToString()],
                    each.Name);

                _state_rows.Add(row.TagIdentity, row);
            }
        }

        private void SynchronizeRowState(bool forceRefreshAll)
        {
            _stu_tag_sel.RefreshSelectionReference();
            if (!_stu_tag_sel.IsSynchronized || forceRefreshAll)
                _stu_tag_sel.SynchorizeSelection();

            foreach (StudentItem each in _stu_tag_sel.EntityItems.Values)
            {
                foreach (int tag in each.Tags)
                {
                    if (_state_rows.ContainsKey(tag))
                        _state_rows[tag].AddStateCount();
                }
            }

            foreach (StudentTagRow each in _state_rows.Values)
            {
                each.CalcuateState(_stu_tag_sel.EntityItems.Count);
            }
        }

        private void SDGTagList_RowCheckStateChanged(object sender, RowCheckedEventArgs e)
        {
            StudentTagRow row = e.Row as StudentTagRow;

            if (row != null)
            {
                if (e.NewState == CheckState.Indeterminate)
                {
                    if (row.OriginState != CheckState.Indeterminate)
                    {
                        if (row.CurrentState == CheckState.Checked)
                            e.NewState = CheckState.Unchecked;
                        else
                            e.NewState = CheckState.Checked;
                    }
                }
            }
        }

        private void RefreshGroupCombobox()
        {
            PrefixCollection groups = _stu_tag_man.Prefixes;
            string originName = cboGroup.Text;

            cboGroup.SelectedItem = null;
            cboGroup.Items.Clear();
            cboGroup.DisplayMember = "DisplayText";

            cboGroup.Items.Add(new AllPrefix());
            foreach (Prefix each in groups.Values)
                cboGroup.Items.Add(each);

            int selIndex = cboGroup.FindString(originName);
            if (selIndex == -1)
                cboGroup.SelectedIndex = (cboGroup.Items.Count > 0 ? 0 : -1);
            else
                cboGroup.SelectedIndex = selIndex;
        }

        private class AllPrefix : Prefix
        {
            public AllPrefix()
                : base(string.Empty)
            {
            }

            public override string DisplayText
            {
                get
                {
                    return "<顯示所有類別>";
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            StudentTagEditor.InsertTag();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            StudentTagRow row = null;
            if (sdgTagList.SelectedRows.Count > 0)
                row = sdgTagList.SelectedRows[0] as StudentTagRow;

            StudentTagEditor.ModifyTag(row.Tag);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            StudentTagRow row = null;
            if (sdgTagList.SelectedRows.Count > 0)
                row = sdgTagList.SelectedRows[0] as StudentTagRow;

            if (row != null)
            {
                TagInfo tag = row.Tag;

                string message;
                XmlElement references = QueryTag.GetUseStudentList(tag.Identity);
                int refCount = references.SelectNodes("Tag").Count;

                if (refCount > 0)
                    message = "已有 " + refCount + " 位學生使用「" + tag.FullName + "」類別，確定要刪除？";
                else
                    message = "確定要刪除「" + tag.FullName + "」類別？";

                if (MsgBox.Show(message, Application.ProductName, MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        _stu_tag_man.Delete(tag);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<int, EditStudentTag.RemoveInfo> removes = new Dictionary<int, EditStudentTag.RemoveInfo>();
                List<int> students = new List<int>();
                EntityItemCollection entites = _stu_tag_sel.EntityItems;

                //log
                Dictionary<int, List<int>> tagAddedList = new Dictionary<int, List<int>>();
                Dictionary<int, List<int>> tagRemovedList = new Dictionary<int, List<int>>();

                foreach (StudentItem each in entites.Values)
                {
                    removes.Add(each.Identity, new EditStudentTag.RemoveInfo(each.Identity));
                    students.Add(each.Identity);
                }

                List<int> tags = new List<int>();
                foreach (StudentTagRow row in _state_rows.Values)
                {
                    if (row.OriginState != row.CurrentState)
                    {
                        if (row.CurrentState == CheckState.Checked)
                        {
                            tagAddedList.Add(row.TagIdentity, new List<int>(students)); //log
                            tags.Add(row.TagIdentity);
                        }

                        //log
                        if (row.CurrentState == CheckState.Unchecked)
                            tagRemovedList.Add(row.TagIdentity, new List<int>());

                        foreach (EditStudentTag.RemoveInfo each in removes.Values)
                        {
                            StudentItem entity = entites[each.StudentID];
                            if (entity.Tags.Contains(row.TagIdentity))
                                each.Tags.Add(row.TagIdentity);
                        }
                    }
                }

                #region Log
                foreach (int student_id in removes.Keys)
                {
                    foreach (int tag_id in removes[student_id].Tags)
                    {
                        if (tagAddedList.ContainsKey(tag_id))
                            tagAddedList[tag_id].Remove(student_id);
                        else
                            tagRemovedList[tag_id].Add(student_id);
                    }
                }
                #endregion

                /* 先將學生原先的 Tag 全部移除，再重新新增 Tag。 */
                EditStudentTag.Remove(new List<EditStudentTag.RemoveInfo>(removes.Values).ToArray());
                EditStudentTag.Add(students, tags);

                #region 寫入log

                StringBuilder builder = new StringBuilder("");

                if (tagAddedList.Count > 0)
                {
                    foreach (int tag_id in tagAddedList.Keys)
                    {
                        builder.AppendLine("新增類別「" + _state_rows[tag_id].Tag.FullName + "」：");
                        int count = 0;
                        foreach (int student_id in tagAddedList[tag_id])
                        {
                            if (count % 5 == 0 && count > 0)
                                builder.AppendLine("");
                            BriefStudentData data = StudentRelated.Student.Instance.Items[student_id + ""];
                            builder.Append("　" + data.Name + "(" + data.StudentNumber + ")");
                            count++;
                        }
                        builder.AppendLine("");
                        builder.AppendLine("");
                    }
                }

                if (tagRemovedList.Count > 0)
                {
                    foreach (int tag_id in tagRemovedList.Keys)
                    {
                        builder.AppendLine("移除類別「" + _state_rows[tag_id].Tag.FullName + "」：");
                        int count = 0;
                        foreach (int student_id in tagRemovedList[tag_id])
                        {
                            if (count % 5 == 0 && count > 0)
                                builder.AppendLine("");
                            BriefStudentData data = StudentRelated.Student.Instance.Items[student_id + ""];
                            builder.Append("　" + data.Name + "(" + data.StudentNumber + ")");
                            count++;
                        }
                        builder.AppendLine("");
                        builder.AppendLine("");
                    }
                }

                CurrentUser.Instance.AppLog.Write(EntityType.Student, "變更學生類別", "", builder.ToString(), "類別", "");

                #endregion

                List<string> studentIDList = new List<string>();
                foreach (int id in students)
                {
                    studentIDList.Add("" + id);
                }

                if (studentIDList.Count > 0)
                    //Student.Instance.InvokBriefDataChanged(studentIDList.ToArray());
                    SmartSchool.Broadcaster.Events.Items["學生/資料變更"].Invoke(studentIDList.ToArray());

                _stu_tag_sel.SynchorizeSelection();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
                DialogResult = DialogResult.None;
            }
        }

        private void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            Prefix current = cboGroup.SelectedItem as Prefix;

            if (current == null) return;

            StudentTagRow originRow = null;
            if (sdgTagList.SelectedRows.Count > 0)
                originRow = sdgTagList.SelectedRows[0] as StudentTagRow;

            sdgTagList.Rows.Clear();

            if (current is AllPrefix) //如果是顯示所有分類的話。
            {
                foreach (StudentTagRow each in _state_rows.Values)
                {
                    sdgTagList.Rows.Add(each);
                    each.SynchronizeState();
                    each.DisplayFullName();
                }
            }
            else
            {
                foreach (TagInfo each in current.Tags.Values)
                {
                    if (_state_rows.ContainsKey(each.Identity))
                    {
                        StudentTagRow row = _state_rows[each.Identity];
                        sdgTagList.Rows.Add(row);
                        row.SynchronizeState();
                        row.DisplayShortName();
                    }
                }
            }

            if (originRow != null)
            {
                foreach (StudentTagRow row in sdgTagList.Rows)
                {
                    if (row.TagIdentity == originRow.TagIdentity)
                        row.Selected = true;
                }
            }
        }

        private void InitialImageList()
        {
            Dictionary<Color, TagColor> _tagColors = _stu_tag_man.Tags.GetTagColors();
            imgTagColor.Images.Clear();

            foreach (TagColor each in _tagColors.Values)
                imgTagColor.Images.Add(each.Color.ToArgb().ToString(), each.Image);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                _stu_tag_man.Refresh();
            }
            catch (Exception ex)
            {
                MsgBox.Show("重新整理資料錯誤。(" + ex.Message + ")");
            }
        }

        #region StudentTagRow Class

        private class StudentTagRow : StateDataGridViewRow, ITagCheckState
        {
            private TagInfo _tag;
            private int _state_count;

            public StudentTagRow(TagInfo tag)
            {
                _tag = tag;
                _state_count = 0;
                _origin_state = CheckState.Unchecked;
            }

            public new TagInfo Tag
            {
                get { return _tag; }
            }

            public void SynchronizeState()
            {
                SyncStateColumn();
            }

            public void DisplayShortName()
            {
                Cells[2].Value = Tag.Name;
            }

            public void DisplayFullName()
            {
                Cells[2].Value = Tag.FullName;
            }

            #region ITagCheckState 成員

            public int TagIdentity
            {
                get { return _tag.Identity; }
            }

            public TagInfo StateOwner
            {
                get { return _tag; }
            }

            private CheckState _origin_state;
            public CheckState OriginState
            {
                get { return _origin_state; }
            }

            public void AddStateCount()
            {
                _state_count++;
            }

            public void CalcuateState(int totalCount)
            {
                if (_state_count == 0)
                    _origin_state = CheckState.Unchecked;

                else if (_state_count == totalCount)
                    _origin_state = CheckState.Checked;

                else if (_state_count != totalCount)
                    _origin_state = CheckState.Indeterminate;

                CurrentState = _origin_state;

                _state_count = 0;
            }

            #endregion
        }
        #endregion

        private void sdgTagList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (sdgTagList.Columns[e.ColumnIndex] == colColor)
            {
                colColor.SortMode = DataGridViewColumnSortMode.Automatic;

                ColorComparer colComparer = new ColorComparer(_currentSortOrder);
                sdgTagList.Sort(colComparer);
                colColor.HeaderCell.SortGlyphDirection = _currentSortOrder = colComparer.GetSortOrder();
            }
        }

        class ColorComparer : System.Collections.IComparer
        {
            private int _value;
            private SortOrder _order = SortOrder.None;

            public ColorComparer(SortOrder orderValue)
            {
                switch (orderValue)
                {
                    case SortOrder.Ascending:
                        _value = 1;
                        _order = SortOrder.Descending;
                        break;
                    case SortOrder.Descending:
                        _value = -1;
                        _order = SortOrder.Ascending;
                        break;
                    case SortOrder.None:
                        _value = -1;
                        _order = SortOrder.Ascending;
                        break;
                }
            }

            public SortOrder GetSortOrder()
            {
                return _order;
            }

            public int Compare(object x, object y)
            {
                StudentTagRow sx = (x as StudentTagRow);
                StudentTagRow sy = (y as StudentTagRow);

                if (sx.Tag.Color.ToArgb().CompareTo(sy.Tag.Color.ToArgb()) == 0)
                    return sx.Tag.FullName.CompareTo(sy.Tag.FullName) * _value * -1;

                return sx.Tag.Color.ToArgb().CompareTo(sy.Tag.Color.ToArgb()) * _value;
            }
        }
    }
}