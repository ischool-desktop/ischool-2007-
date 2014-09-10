using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using System.Xml;
using SmartSchool.TagManage;
using System.Drawing.Drawing2D;
using DevComponents.DotNetBar.Rendering;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.PlugIn.ExtendedColumn;

namespace SmartSchool.StudentRelated
{
    public partial class ContentInfo : UserControl, IPreference,IManager<IColumnItem>
    {
        private const int _SearchLimint = 300;

        private DataGridViewRow _MouseDownRow = null;

        private bool _inSearch = false;

        private string _ImmediatelySearchText;

        private bool _SelectAllSource = false;

        private bool _inSearchLimint = false;

        private DataGridViewSelectedRowCollection _keepingSelection;

        private SmartSchool.StudentRelated.PalmerwormStudent _PalmerwormView1 = null;

        private ISourceProvider<BriefStudentData, SmartSchool.StudentRelated.Search.ISearchStudent> _SourceProvider;

        private Dictionary<DataGridViewRow, int> _RowIndex = new Dictionary<DataGridViewRow, int>();

        private void _SourceProvider_SourceChanged(object sender, EventArgs e)
        {
            ISourceProvider<BriefStudentData, SmartSchool.StudentRelated.Search.ISearchStudent> sourceProvider = (ISourceProvider<BriefStudentData, SmartSchool.StudentRelated.Search.ISearchStudent>)sender;
            if (_inSearch)
            {
                SmartSchool.StudentRelated.Search.ISearchStudent _searchengine = sourceProvider.SearchProvider;
                _searchengine.SearchInName = chkSearchInName.Checked;
                _searchengine.SearchInSSN = chkSearchInSSN.Checked;
                _searchengine.SearchInStudentID = chkSearchInStudentId.Checked;
                if (_inSearchLimint)
                    FillSource(_searchengine.Search(txtSearch.Text, _SearchLimint + 1, 1), false);
                else
                    FillSource(_searchengine.Search(txtSearch.Text, int.MaxValue, 1), false);
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
                this.dgvStudent.Rows.Clear();
        }

        private void FillSource(List<BriefStudentData> source, bool selectAll)
        {
            MotherForm.Instance.SetWaitCursor();
            List<string> ids = new List<string>();
            int scrollIndex = dgvStudent.FirstDisplayedScrollingRowIndex;
            foreach ( BriefStudentData stu in source )
            {
                ids.Add(stu.ID);
            }
            foreach ( IColumnItem plugInColumn in _ColumnItemCollection.Values )
            {
                plugInColumn.FillExtendedValues(ids);
            }
            //記錄目前學生選取狀態
            List<string> _SelectedKey = new List<string>();
            if (!selectAll)
            {
                foreach (BriefStudentData var in SelectedList)
                {
                    _SelectedKey.Add(var.ID);
                }
            }
            //清空並重新填入學生
            dgvStudent.Rows.Clear();
            foreach (BriefStudentData var in source)
            {
                DataGridViewRow row = new DataGridViewRow();
                Image pic = null;
                string statusD = "";
                #region 建立學生資訊
                #region 判斷在學狀態並對應成圖片
                if (var.IsExtending)
                {
                    pic = global::SmartSchool.Properties.Resources.輟學;
                    pic.Tag = 2;
                    statusD = "延修";
                }
                else if (var.IsDiscontinued)
                {
                    pic = global::SmartSchool.Properties.Resources.畢業;
                    pic.Tag = 3;
                    statusD = "輟學";
                }
                else if (var.IsOnLeave)
                {
                    pic = global::SmartSchool.Properties.Resources.休學;
                    pic.Tag = 2;
                    statusD = "休學";
                }
                else if (var.IsGraduated)
                {
                    pic = global::SmartSchool.Properties.Resources.離校;
                    pic.Tag = 1;
                    statusD = "畢業或離校";
                }
                else if (var.IsDeleted)
                {
                    pic = global::SmartSchool.Properties.Resources.刪除;
                    pic.Tag = 4;
                    statusD = "已刪除";
                }
                else if (var.IsNormal)
                {
                    pic = global::SmartSchool.Properties.Resources.一般;
                    pic.Tag = 0;
                    statusD = "在校";
                }
                else
                {
                    pic = null;
                    statusD = "我也不知道";
                }
                #endregion
                List<object> values = new List<object>();
                values.Add(var.ID);
                values.Add(pic);
                values.Add(var.SeatNo);
                values.Add(var.Name);
                values.Add(var.StudentNumber);
                values.Add(var.Gender);
                values.Add(var.IDNumber);
                values.Add(var.ClassName);
                values.Add(var.PermanentPhone);
                values.Add(var.ContactPhone);
                values.Add(var.Tags);
                foreach ( IColumnItem plugInColumn in _ColumnItemCollection.Values )
                {
                    values.Add(plugInColumn.ExtendedValues[var.ID]);
                }
                row.CreateCells(dgvStudent, values.ToArray());
                row.Tag = var;
                row.Cells[1].ToolTipText = statusD;
                foreach (TagInfo tag in var.Tags)
                {
                    row.Cells[10].ToolTipText += (row.Cells[10].ToolTipText == "" ? "" : "\n") + tag.FullName;
                }
                #endregion
                //新增進去
                dgvStudent.Rows.Add(row);
                if (selectAll)
                    row.Selected = true;
            }
            if (!selectAll)
            {
                //將原本選取的學生選回來
                foreach (DataGridViewRow row in dgvStudent.Rows)
                {
                    row.Selected = _SelectedKey.Contains(((BriefStudentData)row.Tag).ID);
                }
            }
            dgvStudent_Sorted(null, null);
            if ( dgvStudent.Rows.Count > scrollIndex &&scrollIndex>=0)
                dgvStudent.FirstDisplayedScrollingRowIndex = scrollIndex;
            Instance_TemporalChanged(null, null);
            MotherForm.Instance.ResetWaitCursor();
        }

        private void SearchClick(object sender, EventArgs e)
        {
            if (_SourceProvider != null)
            {
                SmartSchool.StudentRelated.Search.ISearchStudent _searchengine = _SourceProvider.SearchProvider;
                MotherForm.Instance.SetWaitCursor();
                _searchengine.SearchInName = chkSearchInName.Checked;
                _searchengine.SearchInSSN = chkSearchInSSN.Checked;
                _searchengine.SearchInStudentID = chkSearchInStudentId.Checked;
                List<BriefStudentData> source = _searchengine.Search(txtSearch.Text, _SearchLimint + 1, 1);
                if (source.Count == _SearchLimint + 1 &&
                    MsgBox.Show("目前找到符合搜尋條件的學生已超過" + _SearchLimint + "人，\n\n若繼續搜尋，搜尋的時間將無法估計，\n且搜尋動作不能中途停止。\n\n建議您停止搜尋，改用更精確的搜尋條件或縮小搜尋範圍後，\n再進行搜尋。\n\n是否停止搜尋？", "搜尋結果數量過多", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    source = _searchengine.Search(txtSearch.Text, int.MaxValue, 1);
                    _inSearchLimint = false;
                }
                else
                    _inSearchLimint = true;
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
            if (txtSearch.ContainsFocus&&_SourceProvider != null && _SourceProvider.ImmediatelySearch && _ImmediatelySearchText != this.txtSearch.Text)
            {
                _ImmediatelySearchText = txtSearch.Text;
                SmartSchool.StudentRelated.Search.ISearchStudent _searchengine = _SourceProvider.SearchProvider;
                if (_ImmediatelySearchText != "")
                {
                    _searchengine.SearchInName = chkSearchInName.Checked;
                    _searchengine.SearchInSSN = chkSearchInSSN.Checked;
                    _searchengine.SearchInStudentID = chkSearchInStudentId.Checked;
                    List<BriefStudentData> source = _searchengine.Search(txtSearch.Text, 100, 1);
                    _inSearchLimint = false;
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
            #region 處理毛毛蟲
            if (_PalmerwormView1 == null)
            {
                //如果毛毛蟲未建立則建立
                this._PalmerwormView1 = new SmartSchool.StudentRelated.PalmerwormStudent();
                this._PalmerwormView1.Dock = System.Windows.Forms.DockStyle.Fill;
                this._PalmerwormView1.TabIndex = 5;
                this._PalmerwormView1.Size = this.panelDetial.Size;
                _PalmerwormView1.Visible = false;
                _PalmerwormView1.Manager = SmartSchool.StudentRelated.Process.StudentIUD.StudentIDUProcess.Instance;
                this.panelDetial.Controls.Add(this._PalmerwormView1);
            }
            //清單模式不用處理
            if (splitterListDetial.Expanded)
            {
                if (this.dgvStudent.SelectedRows.Count > 0)
                {
                    BriefStudentData student = null;
                    bool IdChanged = true;
                    foreach (DataGridViewRow var in dgvStudent.SelectedRows)
                    {
                        student = (BriefStudentData)var.Tag;
                        if (_PalmerwormView1.StudentInfo != null && student.ID == _PalmerwormView1.StudentInfo.ID)
                        {
                            if (student == _PalmerwormView1.StudentInfo)
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
                    //if (IdChanged)
                        _PalmerwormView1.StudentInfo = student;
                    _PalmerwormView1.Visible = true;
                }
                else
                {
                    //if (_PalmerwormView1 != null)
                    //    _PalmerwormView1.Visible = false;

                    _PalmerwormView1.StudentInfo = null;
                    _PalmerwormView1.Visible = false;
                }
            } 
            #endregion
        }

        private void dgvStudent_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Control.ModifierKeys != Keys.Alt && Control.ModifierKeys != Keys.Control && Control.ModifierKeys != Keys.Shift)
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
                foreach (BriefStudentData var in SelectedList)
                {
                    _SelectedKey.Add(var.ID);
                }

                c.Focus();

                foreach (DataGridViewRow row in dgvStudent.Rows)
                {
                    row.Selected = _SelectedKey.Contains(((BriefStudentData)row.Tag).ID);
                }
            }
        }

        private void buttonExpand_Click(object sender, EventArgs e)
        {
            if ( splitterListDetial.Expanded )
            {
                //寫入紀錄(目前顯示欄位)
                buttonExpand.Text = "<<";
                buttonExpand.Tooltip = "還原";
                ( (IPreference)this ).UpdatePreference();
                splitterListDetial.Expanded = false;
                for ( int i = 1 ; i < dgvStudent.Columns.Count ; i++ )
                {
                    dgvStudent.Columns[i].Visible = true;
                }
            }
            else
            {
                buttonExpand.Text = ">>";
                buttonExpand.Tooltip = "最大化";
                splitterListDetial.Expanded = true;
                for ( int i = 1 ; i < dgvStudent.Columns.Count ; i++ )
                {
                    dgvStudent.Columns[i].Visible = false;
                }
                colName.Visible = true;
                #region 讀取設定檔
                XmlElement PreferenceData = CurrentUser.Instance.Preference["StudentProvider"];
                if ( PreferenceData != null )
                {
                    #region 設定欄位顯示隱藏(只有當狀態是收合時設定)
                    if ( PreferenceData.HasAttribute("colClassNameVisible") )
                        colClassName.Visible = ( PreferenceData.Attributes["colClassNameVisible"].InnerText == "True" );
                    if ( PreferenceData.HasAttribute("colStatusVisible") )
                        colStatus.Visible = ( PreferenceData.Attributes["colStatusVisible"].InnerText == "True" );
                    if ( PreferenceData.HasAttribute("colSeatNoVisible") )
                        colSeatNo.Visible = ( PreferenceData.Attributes["colSeatNoVisible"].InnerText == "True" );
                    if ( PreferenceData.HasAttribute("colStudentNumberVisible") )
                        this.colStudentNumber.Visible = ( PreferenceData.Attributes["colStudentNumberVisible"].InnerText == "True" );
                    //if (PreferenceData.HasAttribute("colNameVisible"))
                    //    colName.Visible = (PreferenceData.Attributes["colNameVisible"].InnerText == "True");
                    if ( PreferenceData.HasAttribute("colIDNumberVisible") )
                        this.colIDNumber.Visible = ( PreferenceData.Attributes["colIDNumberVisible"].InnerText == "True" );
                    if ( PreferenceData.HasAttribute("colGenderVisible") )
                        colGender.Visible = ( PreferenceData.Attributes["colGenderVisible"].InnerText == "True" );
                    if ( PreferenceData.HasAttribute("colPermanentPhoneVisible") )
                        this.colPermanentPhone.Visible = ( PreferenceData.Attributes["colPermanentPhoneVisible"].InnerText == "True" );
                    if ( PreferenceData.HasAttribute("colContactPhoneVisible") )
                        this.colContactPhone.Visible = ( PreferenceData.Attributes["colContactPhoneVisible"].InnerText == "True" );
                    if ( PreferenceData.HasAttribute("colTagVisible") )
                        this.colTag.Visible = ( PreferenceData.Attributes["colTagVisible"].InnerText == "True" );
                    #endregion
                    //讀取外掛資料行的設定
                    foreach ( DataGridViewColumn var in _ColumnItemCollection.Keys )
                    {
                        if ( PreferenceData.HasAttribute("col" + var.HeaderText + "Visible") )
                            var.Visible = ( PreferenceData.Attributes["col" + var.HeaderText + "Visible"].InnerText == "True" );
                    }
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

        internal ISourceProvider<BriefStudentData, SmartSchool.StudentRelated.Search.ISearchStudent> SourceProvider
        {
            get
            {
                return _SourceProvider;
            }
            set
            {
                if (_SourceProvider == value)
                    return;
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
                    _SelectAllSource = false;
                    txtSearch.WatermarkText = _SourceProvider.SearchWatermark + ("(&F)");
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
                    _SelectAllSource = false;
                    txtSearch.WatermarkText = "";
                    _ImmediatelySearchText=txtSearch.Text = "";
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
            dcmStudent.DenyColumn.Add(colName);
            #endregion
            Application.Idle += new EventHandler(Application_Idle);
            PreferenceUpdater.Instance.Items.Add(this);

            if ( GlobalManager.Renderer is Office2007Renderer )
            {
                ( GlobalManager.Renderer as Office2007Renderer ).ColorTableChanged += new EventHandler(ContentInfo_ColorTableChanged);
                this.dgvStudent.AlternatingRowsDefaultCellStyle.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.MouseOver.TopBackground.End;   
            }

            Student.Instance.TemporalChanged += new EventHandler(Instance_TemporalChanged);
        }

        void Instance_TemporalChanged(object sender, EventArgs e)
        {
            foreach ( DataGridViewRow row in dgvStudent.Rows )
            {
                bool intemp = Student.Instance.TemporaStudent.Contains(row.Tag as BriefStudentData);
                DataGridViewCell cell = row.Cells[dgvStudent.Columns.IndexOf(colName)];
                cell.Style.Font = new Font(dgvStudent.Font, intemp ? (FontStyle.Italic|FontStyle.Underline) : FontStyle.Regular);
            }
        }

        void ContentInfo_ColorTableChanged(object sender, EventArgs e)
        {
            this.dgvStudent.AlternatingRowsDefaultCellStyle.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.MouseOver.TopBackground.End;   
        }

        public void LoadPreference()
        {
            #region 讀取設定檔
            XmlElement PreferenceData = CurrentUser.Instance.Preference["StudentProvider"];
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
                    if (PreferenceData.HasAttribute("colClassNameVisible"))
                        colClassName.Visible = (PreferenceData.Attributes["colClassNameVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colStatusVisible"))
                        colStatus.Visible = (PreferenceData.Attributes["colStatusVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colSeatNoVisible"))
                        colSeatNo.Visible = (PreferenceData.Attributes["colSeatNoVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colStudentNumberVisible"))
                        colStudentNumber.Visible = (PreferenceData.Attributes["colStudentNumberVisible"].InnerText == "True");
                    //if (PreferenceData.HasAttribute("colNameVisible"))
                    //    colName.Visible = (PreferenceData.Attributes["colNameVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colIDNumberVisible"))
                        colIDNumber.Visible = (PreferenceData.Attributes["colIDNumberVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colGenderVisible"))
                        colGender.Visible = (PreferenceData.Attributes["colGenderVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colPermanentPhoneVisible"))
                        colPermanentPhone.Visible = (PreferenceData.Attributes["colPermanentPhoneVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colContactPhoneVisible"))
                        colContactPhone.Visible = (PreferenceData.Attributes["colContactPhoneVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colTagVisible"))
                        colTag.Visible = ( PreferenceData.Attributes["colTagVisible"].InnerText == "True" );
                    //讀取外掛欄的顯式隱藏
                    foreach ( DataGridViewColumn var in _ColumnItemCollection.Keys )
                    {
                        if ( PreferenceData.HasAttribute("col" + var.HeaderText + "Visible") )
                            var.Visible = !( PreferenceData.Attributes["col" + var.HeaderText + "Visible"].InnerText == "False" );
                    }
                }
                #endregion
                #region 設定清單顯示順序
                if (PreferenceData.HasAttribute("colStatusIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colStatusIndex"].Value, out displayIndex) && displayIndex >=0)
                        colStatus.DisplayIndex = displayIndex >= dgvStudent.Columns.Count ? dgvStudent.Columns.Count-1 : displayIndex;
                }
                if (PreferenceData.HasAttribute("colClassNameIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colClassNameIndex"].Value, out displayIndex) && displayIndex >= 0 )
                        colClassName.DisplayIndex = displayIndex >= dgvStudent.Columns.Count ? dgvStudent.Columns.Count-1 : displayIndex;
                }
                if (PreferenceData.HasAttribute("colSeatNoIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colSeatNoIndex"].Value, out displayIndex) && displayIndex >= 0 )
                        colSeatNo.DisplayIndex = displayIndex >= dgvStudent.Columns.Count ? dgvStudent.Columns.Count-1 : displayIndex;
                }
                if (PreferenceData.HasAttribute("colStudentNumberIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colStudentNumberIndex"].Value, out displayIndex) && displayIndex >= 0 )
                        colStudentNumber.DisplayIndex = displayIndex >= dgvStudent.Columns.Count ? dgvStudent.Columns.Count-1 : displayIndex;
                }
                if (PreferenceData.HasAttribute("colNameIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colNameIndex"].Value, out displayIndex) && displayIndex >= 0 )
                        colName.DisplayIndex = displayIndex >= dgvStudent.Columns.Count ? dgvStudent.Columns.Count-1 : displayIndex;
                }
                if (PreferenceData.HasAttribute("colIDNumberIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colIDNumberIndex"].Value, out displayIndex) && displayIndex >=0 )
                        this.colIDNumber.DisplayIndex = displayIndex >= dgvStudent.Columns.Count ? dgvStudent.Columns.Count-1 : displayIndex;
                }
                if (PreferenceData.HasAttribute("colGenderIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colGenderIndex"].Value, out displayIndex) && displayIndex >= 0 )
                        colGender.DisplayIndex = displayIndex >= dgvStudent.Columns.Count ? dgvStudent.Columns.Count-1 : displayIndex;
                }
                if (PreferenceData.HasAttribute("colPermanentPhoneIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colPermanentPhoneIndex"].Value, out displayIndex) && displayIndex >= 0 )
                        this.colPermanentPhone.DisplayIndex = displayIndex >= dgvStudent.Columns.Count ? dgvStudent.Columns.Count-1 : displayIndex;
                }
                if (PreferenceData.HasAttribute("colContactPhoneIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colContactPhoneIndex"].Value, out displayIndex) && displayIndex >= 0 )
                        this.colContactPhone.DisplayIndex = displayIndex >= dgvStudent.Columns.Count ? dgvStudent.Columns.Count-1 : displayIndex;
                }
                if (PreferenceData.HasAttribute("colTagIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colTagIndex"].Value, out displayIndex) && displayIndex >= 0 )
                        this.colTag.DisplayIndex = displayIndex >= dgvStudent.Columns.Count ? dgvStudent.Columns.Count-1 : displayIndex;
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

        public List<BriefStudentData> SelectedList
        {
            get
            {
                //即時傳回選取資料
                List<BriefStudentData> _SelectedList = new List<BriefStudentData>();
                foreach (DataGridViewRow var in dgvStudent.SelectedRows)
                {
                    _SelectedList.Insert(0, (BriefStudentData)var.Tag);
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
            XmlElement PreferenceElement = CurrentUser.Instance.Preference["StudentProvider"];
            if ( PreferenceElement == null )
            {
                PreferenceElement = new XmlDocument().CreateElement("StudentProvider");
            }
            #endregion
            //紀錄展開
            PreferenceElement.SetAttribute("ListExpanded", splitterListDetial.Expanded ? "False" : "True");
            #region 紀錄欄位顯示隱藏(只有當狀態是收合時記錄)
            if ( splitterListDetial.Expanded )
            {
                PreferenceElement.SetAttribute("colStatusVisible", colStatus.Visible ? "True" : "False");
                PreferenceElement.SetAttribute("colClassNameVisible", colClassName.Visible ? "True" : "False");
                PreferenceElement.SetAttribute("colSeatNoVisible", colSeatNo.Visible ? "True" : "False");
                PreferenceElement.SetAttribute("colStudentNumberVisible", colStudentNumber.Visible ? "True" : "False");
                PreferenceElement.SetAttribute("colIDNumberVisible", colIDNumber.Visible ? "True" : "False");
                PreferenceElement.SetAttribute("colGenderVisible", colGender.Visible ? "True" : "False");
                //PreferenceElement.SetAttribute("colNameVisible", colName.Visible ? "True" : "False");
                PreferenceElement.SetAttribute("colPermanentPhoneVisible", colPermanentPhone.Visible ? "True" : "False");
                PreferenceElement.SetAttribute("colContactPhoneVisible", colContactPhone.Visible ? "True" : "False");
                PreferenceElement.SetAttribute("colTagVisible", colTag.Visible ? "True" : "False");
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
            if ( colStatus.DisplayIndex >= 0 )
                PreferenceElement.SetAttribute("colStatusIndex", colStatus.DisplayIndex.ToString());
            if ( colClassName.DisplayIndex >= 0 )
                PreferenceElement.SetAttribute("colClassNameIndex", colClassName.DisplayIndex.ToString());
            if ( colSeatNo.DisplayIndex >= 0 )
                PreferenceElement.SetAttribute("colSeatNoIndex", colSeatNo.DisplayIndex.ToString());
            if ( colStudentNumber.DisplayIndex >= 0 )
                PreferenceElement.SetAttribute("colStudentNumberIndex", this.colStudentNumber.DisplayIndex.ToString());
            if ( colName.DisplayIndex >= 0 )
                PreferenceElement.SetAttribute("colNameIndex", colName.DisplayIndex.ToString());
            if ( colIDNumber.DisplayIndex >= 0 )
                PreferenceElement.SetAttribute("colIDNumberIndex", this.colIDNumber.DisplayIndex.ToString());
            if ( colPermanentPhone.DisplayIndex >= 0 )
                PreferenceElement.SetAttribute("colPermanentPhoneIndex", this.colPermanentPhone.DisplayIndex.ToString());
            if ( colGender.DisplayIndex >= 0 )
                PreferenceElement.SetAttribute("colGenderIndex", colGender.DisplayIndex.ToString());
            if ( colContactPhone.DisplayIndex >= 0 )
                PreferenceElement.SetAttribute("colContactPhoneIndex", this.colContactPhone.DisplayIndex.ToString());
            if ( colTag.DisplayIndex >= 0 )
                PreferenceElement.SetAttribute("colTagIndex", this.colTag.DisplayIndex.ToString());
            //紀錄外掛資料行顯示隱藏
            foreach ( DataGridViewColumn var in _ColumnItemCollection.Keys )
            {
                if ( var.DisplayIndex >= 0 )
                    PreferenceElement.SetAttribute("col" + var.HeaderText + "Index", var.DisplayIndex.ToString());
            }
            #endregion

            CurrentUser.Instance.Preference["StudentProvider"] = PreferenceElement;
            #endregion
        }

        #endregion

        private void dgvStudent_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                MotherForm.Instance.SetWaitCursor();
                PopupPalmerwormStudent.ShowPopupPalmerwormStudent(dgvStudent.Rows[e.RowIndex].Cells[0].Value.ToString());
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
            //在學狀態
            if (e.Column == colStatus)
            {
                e.SortResult = ((int)((Image)e.CellValue1).Tag).CompareTo((int)((Image)e.CellValue2).Tag);
            }
            //座號
            else if ( e.Column == colSeatNo )
            {
                e.SortResult = ( int.Parse(e.CellValue1.ToString() == "" ? "0" : e.CellValue1.ToString()) ).CompareTo(int.Parse(e.CellValue2.ToString() == "" ? "0" : e.CellValue2.ToString()));
            }
            //班級
            else if ( e.Column == colClassName )
            {
                BriefStudentData s1 = (BriefStudentData)dgvStudent.Rows[e.RowIndex1].Tag;
                BriefStudentData s2 = (BriefStudentData)dgvStudent.Rows[e.RowIndex2].Tag;
                if ( s1.RefClassID == "" ) e.SortResult = -1;
                else if ( s2.RefClassID == "" ) e.SortResult = 1;
                else e.SortResult = ClassRelated.Class.Instance.Items[s1.RefClassID].CompareTo(ClassRelated.Class.Instance.Items[s2.RefClassID]);
            }
            else
            {
                e.SortResult = ( "" + e.CellValue1 ).CompareTo("" + e.CellValue2);
            }
            if ( e.SortResult == 0 )
            {
                e.SortResult = ( _RowIndex[dgvStudent.Rows[e.RowIndex1]] ).CompareTo(_RowIndex[dgvStudent.Rows[e.RowIndex2]]);
            }
            e.Handled = true;
            return;
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

        private void dgvStudent_Sorted(object sender, EventArgs e)
        {
            _RowIndex.Clear();
            foreach ( DataGridViewRow row in this.dgvStudent.Rows )
            {
                _RowIndex.Add(row, row.Index);
            }
        }

        #region IManager<IColumnItem> 成員

        private Dictionary<DataGridViewColumn, IColumnItem> _ColumnItemCollection = new Dictionary<DataGridViewColumn, IColumnItem>();

        private void instance_VariableChanged(object sender, EventArgs e)
        {
            List<string> idList = new List<string>();
            foreach ( DataGridViewRow row in dgvStudent.Rows )
            {
                string id = ( row.Tag as BriefStudentData ).ID;
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
                string id = ( row.Tag as BriefStudentData ).ID;
                row.Cells[index].Value = columnItem.ExtendedValues[id];
            }
        }

        public void Add(IColumnItem instance)
        {
            instance.VariableChanged += new EventHandler(instance_VariableChanged);
            DataGridViewColumn newColumn=new DataGridViewColumn(new DataGridViewTextBoxCell());
            newColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            newColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            newColumn.HeaderText = instance.ColumnHeader;
            _ColumnItemCollection.Add(newColumn,instance);
            newColumn.Visible = true;
            this.dgvStudent.Columns.Add(newColumn);
            newColumn.MinimumWidth = newColumn.Width-11;
            newColumn.FillWeight = colName.FillWeight * newColumn.MinimumWidth / colName.MinimumWidth;
            newColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            #region 讀取設定檔
            XmlElement PreferenceData = CurrentUser.Instance.Preference["StudentProvider"];
            if ( PreferenceData != null )
            {
                #region 設定欄位顯示隱藏(只有當狀態是收合時設定)
                if ( splitterListDetial.Expanded )
                {
                    if ( PreferenceData.HasAttribute("col" + instance .ColumnHeader+ "Visible") )
                        newColumn.Visible = !( PreferenceData.Attributes["col" + instance.ColumnHeader + "Visible"].InnerText == "False" );
                }
                #endregion
                #region 設定清單顯示順序
                if ( PreferenceData.HasAttribute("col" + instance.ColumnHeader + "Index") )
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["col" + instance.ColumnHeader + "Index"].Value, out displayIndex) && displayIndex >0)
                        newColumn.DisplayIndex = displayIndex >= dgvStudent.Columns.Count ? dgvStudent.Columns.Count-1 : displayIndex;
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
