using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using System.Xml;
using DevComponents.DotNetBar.Rendering;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.PlugIn.ExtendedColumn;

namespace SmartSchool.ClassRelated
{
    public partial class ContentInfo : UserControl, IPreference, IManager<IColumnItem>
    {
        private DataGridViewRow _MouseDownRow = null;

        private string _ImmediatelySearchText;

        private bool _inSearch = false;

        private bool _SelectAllSource = false;

        private DataGridViewSelectedRowCollection _keepingSelection;

        private ClassInfoPanel _PalmerwormView1 = null;

        private ISourceProvider<ClassInfo, Search.ISearchClass> _SourceProvider;

        private void _SourceProvider_SourceChanged(object sender, EventArgs e)
        {
            ISourceProvider<ClassInfo, Search.ISearchClass> sourceProvider = (ISourceProvider<ClassInfo, Search.ISearchClass>)sender;
            if (_inSearch)
            {
                Search.ISearchClass _searchengine = sourceProvider.SearchProvider;
                FillSource(_searchengine.Search(txtSearch.Text), false);
                return;
            }
            else if (_SelectAllSource)
            {
                FillSource(sourceProvider.Source, SelectedList.Count == dgvStudent.Rows.Count);
            }
            else if (sourceProvider.DisplaySource)
            {
                FillSource(sourceProvider.Source, false);
            }
            else
                dgvStudent.Rows.Clear();
        }

        private void FillSource(List<ClassInfo> source, bool selectAll)
        {
            MotherForm.Instance.SetWaitCursor();
            List<string> ids = new List<string>();

            foreach ( ClassInfo var in source )
            {
                ids.Add(var.ClassID);
            }
            foreach ( IColumnItem plugInColumn in _ColumnItemCollection.Values )
            {
                plugInColumn.FillExtendedValues(ids);
            }
            //記錄目前學生選取狀態
            List<string> _SelectedKey = new List<string>();
            if (!selectAll)
            {
                foreach (ClassInfo var in SelectedList)
                {
                    _SelectedKey.Add(var.ClassID);
                }
            }
            //清空並重新填入學生
            dgvStudent.Rows.Clear();
            foreach (ClassInfo var in source)
            {
                DataGridViewRow row = new DataGridViewRow();
                List<object> values = new List<object>();
                values.Add(var.ClassID);
                values.Add(var.ClassName);
                values.Add(var.StudentCount);
                values.Add(var.TeacherUniqName);
                values.Add(var.GradeYear);
                values.Add(var.Department);
                foreach ( IColumnItem plugInColumn in _ColumnItemCollection.Values )
                {
                    values.Add(plugInColumn.ExtendedValues[var.ClassID]);
                }
                #region 建立學生資訊
                row.CreateCells(dgvStudent,values.ToArray());
                row.Tag = var;
                #endregion
                //新增進去
                dgvStudent.Rows.Add(row);
                if (selectAll) row.Selected = true;
            }
            if (!selectAll)
            {
                //將原本選取的學生選回來
                foreach (DataGridViewRow row in dgvStudent.Rows)
                {
                    row.Selected = _SelectedKey.Contains(((ClassInfo)row.Tag).ClassID);
                }
            }
            MotherForm.Instance.ResetWaitCursor();
        }

        private void SearchClick(object sender, EventArgs e)
        {
            if (_SourceProvider != null)
            {
                Search.ISearchClass _searchengine = _SourceProvider.SearchProvider;
                MotherForm.Instance.SetWaitCursor();
                List<ClassInfo> source = _searchengine.Search(txtSearch.Text);
                //設定搜尋狀態
                _inSearch = true;
                //填入搜尋結果
                FillSource(source, false);
                txtSearch.SelectAll();
                MotherForm.Instance.ResetWaitCursor();
            }
        }

        private void Application_Idle(object sender, EventArgs e)
        {

            #region 及時搜尋
            if (txtSearch.ContainsFocus && _SourceProvider != null && _SourceProvider.ImmediatelySearch && _ImmediatelySearchText != this.txtSearch.Text)
            {
                _ImmediatelySearchText = txtSearch.Text;
                Search.ISearchClass _searchengine = _SourceProvider.SearchProvider;
                if (_ImmediatelySearchText != "")
                {
                    List<ClassInfo> source = _searchengine.Search(txtSearch.Text);
                    //設定搜尋狀態
                    _inSearch = true;
                    //填入搜尋結果
                    FillSource(source, false);
                }
                else
                {
                    _inSearch = false;
                    if (_SourceProvider.DisplaySource)
                    {
                        FillSource(_SourceProvider.Source, false);
                    }
                    else
                        dgvStudent.Rows.Clear();
                }
            }
            #endregion
            #region 處理毛毛蟲
            if (_PalmerwormView1 == null)
            {
                //如果毛毛蟲未建立則建立
                this._PalmerwormView1 = new ClassInfoPanel();
                this._PalmerwormView1.Dock = System.Windows.Forms.DockStyle.Fill;
                this._PalmerwormView1.TabIndex = 5;
                this._PalmerwormView1.Size = this.panelDetial.Size;
                this._PalmerwormView1.Visible = false;
                this._PalmerwormView1.Manager = ClassRelated.RibbonBars.Manage.Instance;
                this.panelDetial.Controls.Add(this._PalmerwormView1);
            }
            //清單模式不用處理
            if (splitterListDetial.Expanded)
            {
                if (this.dgvStudent.SelectedRows.Count > 0)
                {
                    ClassInfo classinfo = null;
                    bool IdChanged = true;
                    foreach (DataGridViewRow var in dgvStudent.SelectedRows)
                    {
                        classinfo = (ClassInfo)var.Tag;
                        if (_PalmerwormView1.ClassInfo != null && classinfo.ClassID == _PalmerwormView1.ClassInfo.ClassID)
                        {
                            if (classinfo == _PalmerwormView1.ClassInfo)
                            {
                                IdChanged = false;
                                break;
                            }
                            else
                            {//ID相同但被改變
                                IdChanged = true;
                                break;
                            }
                        }
                    }
                    if (IdChanged)
                        _PalmerwormView1.ClassInfo = classinfo;
                    _PalmerwormView1.Visible = true;
                }
                else
                {
                    //if (_PalmerwormView1 != null)
                    //    _PalmerwormView1.Visible = false;
                    _PalmerwormView1.ClassInfo = null;
                    _PalmerwormView1.Visible = false;
                }
            }
            #endregion
        }

        private void dgvStudent_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Control.ModifierKeys != Keys.Alt && Control.ModifierKeys != Keys.Control && Control.ModifierKeys != Keys.Shift && e.Button == MouseButtons.Left)
            {
                if (e.RowIndex >= 0 && dgvStudent.Rows[e.RowIndex].Selected)
                {
                    _keepingSelection = dgvStudent.SelectedRows;
                    _MouseDownRow = dgvStudent.Rows[e.RowIndex];
                    dragDropTimer.Start();
                }
                if (e.RowIndex >= 0 && !dgvStudent.Rows[e.RowIndex].Selected)
                {
                    _keepingSelection = null;
                }
            }
        }


        private void dragDropTimer_Tick(object sender, EventArgs e)
        {
            _MouseDownRow = null;
            dgvStudent.Cursor = System.Windows.Forms.Cursors.No;
            dragDropTimer.Stop();
            DoDragDrop(SelectedList, DragDropEffects.All);
            dgvStudent.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void dgvStudent_SelectionChanged(object sender, EventArgs e)
        {
            if (_keepingSelection != null)
            {
                foreach (DataGridViewRow var in _keepingSelection)
                {
                    var.Selected = true;
                }
                _keepingSelection = null;
            }
        }

        /// <summary>
        /// 設定焦點
        /// </summary>
        private void SetFocus(object sender, EventArgs e)
        {
            Control c = ((Control)sender);
            if (c.TopLevelControl.ContainsFocus && !txtSearch.Focused && !c.ContainsFocus)
            {
                List<string> _SelectedKey = new List<string>();
                foreach (ClassInfo var in SelectedList)
                {
                    _SelectedKey.Add(var.ClassID);
                }

                c.Focus();

                foreach (DataGridViewRow row in dgvStudent.Rows)
                {
                    row.Selected = _SelectedKey.Contains(((ClassInfo)row.Tag).ClassID);
                }
            }
        }

        private void buttonExpand_Click(object sender, EventArgs e)
        {
            if (splitterListDetial.Expanded)
            {
                //寫入紀錄(目前顯示欄位)
                buttonExpand.Text = "<<";
                buttonExpand.Tooltip = "還原";
                ((IPreference)this).UpdatePreference();
                splitterListDetial.Expanded = false;
                for (int i = 1; i < dgvStudent.Columns.Count; i++)
                {
                    dgvStudent.Columns[i].Visible = true;
                }
            }
            else
            {
                buttonExpand.Text = ">>";
                buttonExpand.Tooltip = "最大化";
                splitterListDetial.Expanded = true;
                for (int i = 1; i < dgvStudent.Columns.Count; i++)
                {
                    dgvStudent.Columns[i].Visible = false;
                }
                colClassName.Visible = true;
                #region 讀取設定檔
                XmlElement PreferenceData = CurrentUser.Instance.Preference["ClassProvider"];
                if (PreferenceData != null)
                {
                    #region 設定欄位顯示隱藏(只有當狀態是收合時設定)
                    //if (PreferenceData.HasAttribute("colClassNameVisible"))
                    //    colClassName.Visible = (PreferenceData.Attributes["colClassNameVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colDepartmentVisible"))
                        this.colDepartment.Visible = (PreferenceData.Attributes["colDepartmentVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colGradeYearVisible"))
                        this.colGradeYear.Visible = (PreferenceData.Attributes["colGradeYearVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colStudentCountVisible"))
                        this.colStudentCount.Visible = (PreferenceData.Attributes["colStudentCountVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colTeacherVisible"))
                        this.colTeacher.Visible = (PreferenceData.Attributes["colTeacherVisible"].InnerText == "True");
                    //讀取外掛資料行的設定
                    foreach ( DataGridViewColumn var in _ColumnItemCollection.Keys )
                    {
                        if ( PreferenceData.HasAttribute("col" + var.HeaderText + "Visible") )
                            var.Visible = ( PreferenceData.Attributes["col" + var.HeaderText + "Visible"].InnerText == "True" );
                    }
                    #endregion
                }
                #endregion
            }
        }
        /// <summary>
        /// 設定縮放
        /// </summary>
        private void splitterListDetial_ExpandedChanged(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            panelList.ResumeLayout();
            panelDetial.ResumeLayout();
            panelContent.ResumeLayout();
        }

        /// <summary>
        /// 設定設定縮放
        /// </summary>
        private void splitterListDetial_ExpandedChanging(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            panelContent.SuspendLayout();
            panelList.SuspendLayout();
            panelDetial.SuspendLayout();
            if (splitterListDetial.Expanded)
            {
                splitterListDetial.Dock = DockStyle.Right;
                panelList.Dock = DockStyle.Fill;
                splitterListDetial.Enabled = false;
                panelContent.Controls.SetChildIndex(panelList, 0);
            }
            else
            {
                panelList.Dock = DockStyle.Left;
                splitterListDetial.Dock = DockStyle.Left;
                splitterListDetial.Enabled = true;
                panelContent.Controls.SetChildIndex(panelList, 3);
            }
        }

        internal ISourceProvider<ClassInfo, Search.ISearchClass> SourceProvider
        {
            get
            {
                return _SourceProvider;
            }
            set
            {
                //設定_SourceProvider為value
                if (_SourceProvider != null)
                {
                    _SourceProvider.SourceChanged -= new EventHandler(_SourceProvider_SourceChanged);
                }
                _SourceProvider = value;
                //dgvStudent.Rows.Clear();
                if (_SourceProvider != null)
                {
                    _SourceProvider.SourceChanged += new EventHandler(_SourceProvider_SourceChanged);
                    //重設搜尋欄
                    _inSearch = false;
                    txtSearch.WatermarkText = _SourceProvider.SearchWatermark + "(&F)";
                    _ImmediatelySearchText = txtSearch.Text = "";
                    //如果為直接顯示資料則填入
                    if (_SourceProvider.DisplaySource)
                    {
                        FillSource(_SourceProvider.Source, false);
                    }
                    else
                        dgvStudent.Rows.Clear();
                }
                else
                {
                    _inSearch = false;
                    txtSearch.WatermarkText = "";
                    _ImmediatelySearchText = txtSearch.Text = "";
                    dgvStudent.Rows.Clear();
                }
            }
        }

        public ContentInfo()
        {
            InitializeComponent();
            //設定畫面分隔線樣式
            expandableSplitter3.GripLightColor = expandableSplitter3.GripDarkColor = System.Drawing.Color.Transparent;
            #region 設定DataGridView不允許使用者隱藏／顯示欄位(編號 姓名)
            dcmStudent.DenyColumn.Add(colID);
            //dcmStudent.DenyColumn.Add(colStatus);
            dcmStudent.DenyColumn.Add(colClassName);
            #endregion
            Application.Idle += new EventHandler(Application_Idle);
            PreferenceUpdater.Instance.Items.Add(this);


            if ( GlobalManager.Renderer is Office2007Renderer )
            {
                ( GlobalManager.Renderer as Office2007Renderer ).ColorTableChanged += new EventHandler(ContentInfo_ColorTableChanged);
                this.dgvStudent.AlternatingRowsDefaultCellStyle.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.MouseOver.TopBackground.End;
            }

            TeacherRelated.Teacher.Instance.TeacherDataChanged += delegate
            {
                foreach ( DataGridViewRow var in dgvStudent.SelectedRows )
                {
                    ClassInfo c = (ClassInfo)var.Tag;
                    var.Cells[3].Value = c.TeacherUniqName;
                }
            };
            SmartSchool.Broadcaster.Events.Items["學生/資料變更"].Handler += delegate
            {
                foreach ( DataGridViewRow var in dgvStudent.SelectedRows )
                {
                    ClassInfo c = (ClassInfo)var.Tag;
                    var.Cells[2].Value = c.StudentCount;
                }
            };
        }

        void ContentInfo_ColorTableChanged(object sender, EventArgs e)
        {
            this.dgvStudent.AlternatingRowsDefaultCellStyle.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.MouseOver.TopBackground.End;
        }

        public void LoadPreference()
        {
            #region 讀取設定檔
            XmlElement PreferenceData = CurrentUser.Instance.Preference["ClassProvider"];
            if (PreferenceData != null)
            {
                //設定清單顯示寬度
                if (PreferenceData.HasAttribute("PanelListWidth"))
                {
                    int width;
                    if (int.TryParse(PreferenceData.Attributes["PanelListWidth"].Value, out width))
                        splitterListDetial.SplitPosition = width;
                }
                //設定展開
                if (PreferenceData.HasAttribute("ListExpanded") && PreferenceData.Attributes["ListExpanded"].Value == "True")
                {
                    buttonExpand.Text = "<<";
                    buttonExpand.Tooltip = "還原";
                    splitterListDetial.Expanded = false;
                    for (int i = 1; i < dgvStudent.Columns.Count; i++)
                    {
                        dgvStudent.Columns[i].Visible = true;
                    }
                }
                #region 設定欄位顯示隱藏(只有當狀態是收合時設定)
                if (splitterListDetial.Expanded)
                {
                    //if (PreferenceData.HasAttribute("colClassNameVisible"))
                    //    colClassName.Visible = (PreferenceData.Attributes["colClassNameVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colDepartmentVisible"))
                        this.colDepartment.Visible = (PreferenceData.Attributes["colDepartmentVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colGradeYearVisible"))
                        this.colGradeYear.Visible = (PreferenceData.Attributes["colGradeYearVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colStudentCountVisible"))
                        this.colStudentCount.Visible = (PreferenceData.Attributes["colStudentCountVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colTeacherVisible"))
                        this.colTeacher.Visible = (PreferenceData.Attributes["colTeacherVisible"].InnerText == "True");
                    //讀取外掛欄的顯式隱藏
                    foreach ( DataGridViewColumn var in _ColumnItemCollection.Keys )
                    {
                        if ( PreferenceData.HasAttribute("col" + var.HeaderText + "Visible") )
                            var.Visible = !( PreferenceData.Attributes["col" + var.HeaderText + "Visible"].InnerText == "False" );
                    }
                }
                #endregion
                #region 設定清單顯示順序
                if (PreferenceData.HasAttribute("colClassNameIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colClassNameIndex"].Value, out displayIndex) && displayIndex >=0)
                        colClassName.DisplayIndex = displayIndex >= dgvStudent.Columns.Count ? dgvStudent.Columns.Count - 1 : displayIndex;
                }
                if (PreferenceData.HasAttribute("colDepartmentIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colDepartmentIndex"].Value, out displayIndex) && displayIndex >= 0 )
                        this.colDepartment.DisplayIndex = displayIndex >= dgvStudent.Columns.Count ? dgvStudent.Columns.Count - 1 : displayIndex;
                }
                if (PreferenceData.HasAttribute("colGradeYearIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colGradeYearIndex"].Value, out displayIndex) && displayIndex >= 0 )
                        this.colGradeYear.DisplayIndex = displayIndex >= dgvStudent.Columns.Count ? dgvStudent.Columns.Count - 1 : displayIndex;
                }
                if (PreferenceData.HasAttribute("colStudentCountIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colStudentCountIndex"].Value, out displayIndex) && displayIndex >= 0 )
                        this.colStudentCount.DisplayIndex =displayIndex>= dgvStudent.Columns.Count ? dgvStudent.Columns.Count - 1 : displayIndex;
                }
                if (PreferenceData.HasAttribute("colTeacherIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colTeacherIndex"].Value, out displayIndex) && displayIndex >= 0 )
                        this.colTeacher.DisplayIndex = displayIndex >= dgvStudent.Columns.Count ? dgvStudent.Columns.Count - 1 : displayIndex;
                }
                //讀取外掛欄的顯式順序
                foreach ( DataGridViewColumn var in _ColumnItemCollection.Keys )
                {
                    if ( PreferenceData.HasAttribute("col" + var.HeaderText + "Index") )
                    {
                        int displayIndex;
                        if ( int.TryParse(PreferenceData.Attributes["col" + var.HeaderText + "Index"].Value, out displayIndex) && displayIndex >= 0 )
                            var.DisplayIndex = displayIndex >= dgvStudent.Columns.Count ? dgvStudent.Columns.Count - 1 : displayIndex;
                    }
                }
                #endregion
            }
            #endregion

        }

        public List<ClassInfo> SelectedList
        {
            get
            {
                //即時傳回選取資料
                List<ClassInfo> _SelectedList = new List<ClassInfo>();
                foreach (DataGridViewRow var in dgvStudent.SelectedRows)
                {
                    _SelectedList.Insert(0, (ClassInfo)var.Tag);
                }
                return _SelectedList;
            }
        }

        public ContextMenuStrip ListPaneMenuStrip
        {
            get { return dgvStudent.ContextMenuStrip; }
            set { dgvStudent.ContextMenuStrip = value; }
        }

        #region IPreference 成員

        void IPreference.UpdatePreference()
        {
            #region 紀錄欄位顯示
            #region 取得PreferenceElement
            XmlElement PreferenceElement = CurrentUser.Instance.Preference["ClassProvider"];
            if ( PreferenceElement == null )
            {
                PreferenceElement = new XmlDocument().CreateElement("ClassProvider");
            }
            #endregion
            //紀錄展開
            PreferenceElement.SetAttribute("ListExpanded", splitterListDetial.Expanded ? "False" : "True");
            #region 紀錄欄位顯示隱藏(只有當狀態是收合時記錄)
            if ( splitterListDetial.Expanded )
            {
                //PreferenceElement.SetAttribute("colClassNameVisible", colClassName.Visible ? "True" : "False");
                PreferenceElement.SetAttribute("colDepartmentVisible", this.colDepartment.Visible ? "True" : "False");
                PreferenceElement.SetAttribute("colGradeYearVisible", this.colGradeYear.Visible ? "True" : "False");
                PreferenceElement.SetAttribute("colTeacherVisible", this.colTeacher.Visible ? "True" : "False");
                PreferenceElement.SetAttribute("colStudentCountVisible", this.colStudentCount.Visible ? "True" : "False");
                //紀錄外掛資料行顯示隱藏
                foreach ( DataGridViewColumn var in _ColumnItemCollection.Keys )
                {
                    PreferenceElement.SetAttribute("col" + var.HeaderText + "Visible", var.Visible ? "True" : "False");
                }
            }
            #endregion
            //紀錄清單顯示寬度(只有當狀態是收合時記錄)
            if ( splitterListDetial.Expanded )
            {
                PreferenceElement.SetAttribute("PanelListWidth", panelList.Width.ToString());
            }
            #region 紀錄清單顯示順序
            if ( colClassName.DisplayIndex >= 0 )
                PreferenceElement.SetAttribute("colClassNameIndex", colClassName.DisplayIndex.ToString());
            if ( colDepartment.DisplayIndex >= 0 )
                PreferenceElement.SetAttribute("colDepartmentIndex", this.colDepartment.DisplayIndex.ToString());
            if ( colGradeYear.DisplayIndex >= 0 )
                PreferenceElement.SetAttribute("colGradeYearIndex", this.colGradeYear.DisplayIndex.ToString());
            if ( colStudentCount.DisplayIndex >= 0 )
                PreferenceElement.SetAttribute("colStudentCountIndex", this.colStudentCount.DisplayIndex.ToString());
            if ( colTeacher.DisplayIndex >= 0 )
                PreferenceElement.SetAttribute("colTeacherIndex", this.colTeacher.DisplayIndex.ToString());
            //紀錄外掛資料行顯示隱藏
            foreach ( DataGridViewColumn var in _ColumnItemCollection.Keys )
            {
                if ( var.DisplayIndex >= 0 )
                    PreferenceElement.SetAttribute("col" + var.HeaderText + "Index", var.DisplayIndex.ToString());
            }
            #endregion

            CurrentUser.Instance.Preference["ClassProvider"] = PreferenceElement;
            #endregion
        }

        #endregion

        private void dgvStudent_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                MotherForm.Instance.SetWaitCursor();
                ClassRelated.Class.Instance.PopupClassForm(dgvStudent.Rows[e.RowIndex].Cells[0].Value.ToString(), dgvStudent.Rows[e.RowIndex].Cells[1].Value.ToString());
                MotherForm.Instance.ResetWaitCursor();
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchClick(null, null);
            }
        }

        private void dgvStudent_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            _keepingSelection = null;
            if (e.Button == MouseButtons.Left && _MouseDownRow != null)
            {
                foreach (DataGridViewRow row in dgvStudent.SelectedRows)
                {
                    row.Selected = (row == _MouseDownRow);
                }
            }
            _MouseDownRow = null;
            dragDropTimer.Stop();
        }

        private void dgvStudent_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            _keepingSelection = null;
            if (_MouseDownRow != null)
            {
                foreach (DataGridViewRow row in dgvStudent.SelectedRows)
                {
                    row.Selected = (row == _MouseDownRow);
                }
            }
            _MouseDownRow = null;
            dragDropTimer.Stop();
        }

        private void dgvStudent_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            //座號
            if (e.Column == colStudentCount)
            {
                e.SortResult = (int.Parse(e.CellValue1.ToString() == "" ? "0" : e.CellValue1.ToString())).CompareTo(int.Parse(e.CellValue2.ToString() == "" ? "0" : e.CellValue2.ToString()));
                e.Handled = true;
            }
        }
        internal void SelectAllSource()
        {
            _SelectAllSource = true;
            FillSource(_SourceProvider.Source, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtSearch.Focus();
        }

        #region IManager<IColumnItem> 成員

        private Dictionary<DataGridViewColumn, IColumnItem> _ColumnItemCollection = new Dictionary<DataGridViewColumn, IColumnItem>();

        private void instance_VariableChanged(object sender, EventArgs e)
        {
            List<string> idList = new List<string>();
            foreach ( DataGridViewRow row in dgvStudent.Rows )
            {
                string id = ( row.Tag as ClassInfo ).ClassID ;
                if ( !idList.Contains(id) )
                    idList.Add(id);
            }
            IColumnItem columnItem = ( (IColumnItem)sender );
            columnItem.FillExtendedValues(idList);

            int index = 0;
            foreach ( DataGridViewColumn col in _ColumnItemCollection.Keys )
            {
                if ( _ColumnItemCollection[col] == columnItem )
                {
                    index = col.Index;
                    break;
                }
            }

            foreach ( DataGridViewRow row in dgvStudent.Rows )
            {
                string id = ( row.Tag as ClassInfo ).ClassID;
                row.Cells[index].Value = columnItem.ExtendedValues[id];
            }
        }

        public void Add(IColumnItem instance)
        {
            instance.VariableChanged += new EventHandler(instance_VariableChanged);
            DataGridViewColumn newColumn = new DataGridViewColumn(new DataGridViewTextBoxCell());
            newColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            newColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            newColumn.HeaderText = instance.ColumnHeader;
            _ColumnItemCollection.Add(newColumn, instance);
            newColumn.Visible = true;
            this.dgvStudent.Columns.Add(newColumn);
            newColumn.MinimumWidth = newColumn.Width - 11;
            newColumn.FillWeight = colClassName .FillWeight * newColumn.MinimumWidth / colClassName.MinimumWidth;
            newColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            #region 讀取設定檔
            XmlElement PreferenceData = CurrentUser.Instance.Preference["ClassProvider"];
            if ( PreferenceData != null )
            {
                #region 設定欄位顯示隱藏(只有當狀態是收合時設定)
                if ( splitterListDetial.Expanded )
                {
                    if ( PreferenceData.HasAttribute("col" + instance.ColumnHeader + "Visible") )
                        newColumn.Visible = !( PreferenceData.Attributes["col" + instance.ColumnHeader + "Visible"].InnerText == "False" );
                }
                #endregion
                #region 設定清單顯示順序
                if ( PreferenceData.HasAttribute("col" + instance.ColumnHeader + "Index") )
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["col" + instance.ColumnHeader + "Index"].Value, out displayIndex) )
                        newColumn.DisplayIndex = displayIndex >= dgvStudent.Columns.Count ? dgvStudent.Columns.Count - 1 : displayIndex;
                }
                #endregion
            }
            #endregion
        }

        public void Remove(IColumnItem instance)
        {
            foreach ( DataGridViewColumn var in _ColumnItemCollection.Keys )
            {
                if ( _ColumnItemCollection[var] == instance )
                {
                    _ColumnItemCollection[var].VariableChanged -= new EventHandler(instance_VariableChanged);
                    _ColumnItemCollection.Remove(var);
                    break;
                }
            }
        }

        #endregion
    }
}
