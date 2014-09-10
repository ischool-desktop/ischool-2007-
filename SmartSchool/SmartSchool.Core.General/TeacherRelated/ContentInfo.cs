using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.TeacherRelated.Search;
using System.Xml;
using DevComponents.DotNetBar.Rendering;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.PlugIn.ExtendedColumn;

namespace SmartSchool.TeacherRelated
{
    public partial class ContentInfo : UserControl, IPreference, IManager<IColumnItem>
    {
        private bool _inSearch = false;

        private string _ImmediatelySearchText;

        private bool _SelectAllSource = false;

        private DataGridViewRow _MouseDownRow = null;

        private PalmerwormTeacher _PalmerwormView1;

        private DataGridViewSelectedRowCollection _keepingSelection;

        private ISourceProvider<BriefTeacherData, Search.ISearchTeacher> _SourceProvider;

        private void _SourceProvider_SourceChanged(object sender, EventArgs e)
        {
            ISourceProvider<BriefTeacherData, ISearchTeacher> sourceProvider = (ISourceProvider<BriefTeacherData, ISearchTeacher>)sender;
            if (_inSearch)
            {
                ISearchTeacher _searchengine = sourceProvider.SearchProvider;
                FillSource(_searchengine.Search(txtSearch.Text), false);
                return;
            }
            else if (_SelectAllSource)
            {
                FillSource(sourceProvider.Source, SelectedList.Count == dgvTeacher.Rows.Count);
            }
            else if (sourceProvider.DisplaySource)
            {
                FillSource(sourceProvider.Source, false);
            }
            else
                dgvTeacher.Rows.Clear();
        }

        private void FillSource(List<BriefTeacherData> source, bool selectAll)
        {
            MotherForm.Instance.SetWaitCursor();
            List<string> ids = new List<string>();

            foreach ( BriefTeacherData var in source )
            {
                ids.Add(var.ID);
            }
            foreach ( IColumnItem plugInColumn in _ColumnItemCollection.Values )
            {
                plugInColumn.FillExtendedValues(ids);
            }
            ////記錄目前學生選取狀態
            List<string> _SelectedKey = new List<string>();
            if (!selectAll)
            {
                foreach (BriefTeacherData var in SelectedList)
                {
                    _SelectedKey.Add(var.ID);
                }
            }
            //清空並重新填入學生
            dgvTeacher.Rows.Clear();
            if (source != null)
            {
                foreach (BriefTeacherData var in source)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    Image pic = null;
                    string statusD = "";
                    #region 建立學生資訊
                    #region 判斷在學狀態並對應成圖片
                    switch (var.Status)
                    {
                        case "一般":
                            pic = global::SmartSchool.Properties.Resources.一般;
                            pic.Tag = 0;
                            statusD = "一般";
                            break;
                        case "刪除":
                            pic = global::SmartSchool.Properties.Resources.刪除;
                            pic.Tag = 4;
                            statusD = "已刪除";
                            break;
                        default:
                            pic = null;
                            statusD = "我也不知道";
                            break;
                    }
                    #endregion
                    List<object> values = new List<object>();
                    values.Add(var.ID);
                    values.Add(pic);
                    values.Add(var.UniqName);
                    values.Add(var.Gender);
                    values.Add(var.IDNumber);
                    values.Add(var.SupervisedByClass);
                    values.Add(var.ContactPhone);
                    foreach ( IColumnItem plugInColumn in _ColumnItemCollection.Values )
                    {
                        values.Add(plugInColumn.ExtendedValues[var.ID]);
                    }
                    row.CreateCells(dgvTeacher, values.ToArray());
                    row.Tag = var;
                    row.Cells[1].ToolTipText = statusD;
                    #endregion
                    //新增進去
                    dgvTeacher.Rows.Add(row);
                    if (selectAll)
                        row.Selected = true;
                }
                if (!selectAll)
                {
                    //將原本選取的學生選回來
                    foreach (DataGridViewRow row in dgvTeacher.Rows)
                    {
                        row.Selected = _SelectedKey.Contains(((BriefTeacherData)row.Tag).ID);
                    }
                }
                MotherForm.Instance.ResetWaitCursor();
            }
        }

        private void SearchClick(object sender, EventArgs e)
        {
            if (_SourceProvider != null)
            {
                ISearchTeacher _searchengine = _SourceProvider.SearchProvider;
                MotherForm.Instance.SetWaitCursor();
                List<BriefTeacherData> source = _searchengine.Search(txtSearch.Text);
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
                ISearchTeacher _searchengine = _SourceProvider.SearchProvider;
                if (_ImmediatelySearchText != "")
                {
                    List<BriefTeacherData> source = _searchengine.Search(txtSearch.Text);
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
                        dgvTeacher.Rows.Clear();
                }
            }
            #endregion
            #region 處理毛毛蟲
            if (_PalmerwormView1 == null)
            {
                //如果毛毛蟲未建立則建立
                this._PalmerwormView1 = new PalmerwormTeacher();
                this._PalmerwormView1.Dock = System.Windows.Forms.DockStyle.Fill;
                this._PalmerwormView1.TabIndex = 5;
                this._PalmerwormView1.Size = this.panelDetial.Size;
                _PalmerwormView1.Visible = false;
                _PalmerwormView1.Manager = RibbonBars.Manage.Instance;
                this.panelDetial.Controls.Add(this._PalmerwormView1);
            }
            //清單模式不用處理
            if (splitterListDetial.Expanded)
            {
                if (this.dgvTeacher.SelectedRows.Count > 0)
                {
                    BriefTeacherData teacher = null;
                    bool IdChanged = true;
                    foreach (DataGridViewRow var in dgvTeacher.SelectedRows)
                    {
                        teacher = ((BriefTeacherData)var.Tag);
                        if (_PalmerwormView1.TeacherInfo != null && teacher.ID == _PalmerwormView1.TeacherInfo.ID)
                        {
                            if (teacher == _PalmerwormView1.TeacherInfo)
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
                        _PalmerwormView1.TeacherInfo = teacher;
                    _PalmerwormView1.Visible = true;
                }
                else
                {
                    _PalmerwormView1.TeacherInfo = null;
                    _PalmerwormView1.Visible = false;
                }
            }
            #endregion
        }

        private void dgvStudent_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Control.ModifierKeys != Keys.Alt && Control.ModifierKeys != Keys.Control && Control.ModifierKeys != Keys.Shift)
            {
                if (e.RowIndex >= 0 && dgvTeacher.Rows[e.RowIndex].Selected)
                {
                    _keepingSelection = dgvTeacher.SelectedRows;
                    _MouseDownRow = dgvTeacher.Rows[e.RowIndex];
                    dragDropTimer.Start();
                }
                if (e.RowIndex >= 0 && !dgvTeacher.Rows[e.RowIndex].Selected)
                {
                    _keepingSelection = null;
                }
            }
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

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchClick(null, null);
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
                foreach (BriefTeacherData var in SelectedList)
                {
                    _SelectedKey.Add(var.ID);
                }

                c.Focus();

                foreach (DataGridViewRow row in dgvTeacher.Rows)
                {
                    row.Selected = _SelectedKey.Contains(((BriefTeacherData)row.Tag).ID);
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
                for (int i = 1; i < dgvTeacher.Columns.Count; i++)
                {
                    dgvTeacher.Columns[i].Visible = true;
                }
            }
            else
            {
                buttonExpand.Text = ">>";
                buttonExpand.Tooltip = "最大化";
                splitterListDetial.Expanded = true;
                for (int i = 1; i < dgvTeacher.Columns.Count; i++)
                {
                    dgvTeacher.Columns[i].Visible = false;
                }
                colName.Visible = true;
                #region 讀取設定檔
                XmlElement PreferenceData = CurrentUser.Instance.Preference["TeacherProvider"];
                if (PreferenceData != null)
                {
                    #region 設定欄位顯示隱藏(只有當狀態是收合時設定)
                    if (PreferenceData.HasAttribute("colClassNameVisible"))
                        colClassName.Visible = (PreferenceData.Attributes["colClassNameVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colStatusVisible"))
                        colStatus.Visible = (PreferenceData.Attributes["colStatusVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colNameVisible"))
                        colName.Visible = (PreferenceData.Attributes["colNameVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colIDNumberVisible"))
                        this.colIDNumber.Visible = (PreferenceData.Attributes["colIDNumberVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colGenderVisible"))
                        colGender.Visible = (PreferenceData.Attributes["colGenderVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colContactPhoneVisible"))
                        this.colContactPhone.Visible = ( PreferenceData.Attributes["colContactPhoneVisible"].InnerText == "True" );
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

        internal ISourceProvider<BriefTeacherData, ISearchTeacher> SourceProvider
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
                dgvTeacher.Rows.Clear();
                if (_SourceProvider != null)
                {
                    txtSearch.Enabled = buttonX1.Enabled = true;
                    _SourceProvider.SourceChanged += new EventHandler(_SourceProvider_SourceChanged);
                    //重設搜尋欄
                    _inSearch = false;
                    txtSearch.WatermarkText = _SourceProvider.SearchWatermark + ("(&F)");
                    _ImmediatelySearchText = txtSearch.Text = "";
                    //如果為直接顯示資料則填入
                    if (_SourceProvider.DisplaySource)
                    {
                        FillSource(_SourceProvider.Source, false);
                    }
                    else
                        dgvTeacher.Rows.Clear();
                }
                else
                {
                    txtSearch.Enabled = buttonX1.Enabled = false;
                    //重設搜尋欄
                    _inSearch = false;
                    txtSearch.WatermarkText = "";
                    _ImmediatelySearchText = txtSearch.Text = "";
                    dgvTeacher.Rows.Clear();
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
            dcmStudent.DenyColumn.Add(colName);
            #endregion
            Application.Idle += new EventHandler(Application_Idle);
            PreferenceUpdater.Instance.Items.Add(this);


            if ( GlobalManager.Renderer is Office2007Renderer )
            {
                ( GlobalManager.Renderer as Office2007Renderer ).ColorTableChanged += new EventHandler(ContentInfo_ColorTableChanged);
                this.dgvTeacher.AlternatingRowsDefaultCellStyle.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.MouseOver.TopBackground.End;
            }

            ClassRelated.Class.Instance.ClassUpdated += delegate
            {
                foreach ( DataGridViewRow row in dgvTeacher.Rows )
                {
                    row.Cells[5].Value = ( (BriefTeacherData)row.Tag ).SupervisedByClass;
                }
            };
        }

        void ContentInfo_ColorTableChanged(object sender, EventArgs e)
        {
            this.dgvTeacher.AlternatingRowsDefaultCellStyle.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.MouseOver.TopBackground.End;
        }

        public List<BriefTeacherData> SelectedList
        {
            get
            {
                //即時傳回選取資料
                List<BriefTeacherData> _SelectedList = new List<BriefTeacherData>();
                foreach (DataGridViewRow var in dgvTeacher.SelectedRows)
                {
                    _SelectedList.Insert(0, (BriefTeacherData)var.Tag);
                }
                return _SelectedList;
            }
        }

        public void LoadPreference()
        {
            #region 讀取設定檔
            XmlElement PreferenceData = CurrentUser.Instance.Preference["TeacherProvider"];
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
                    for (int i = 1; i < dgvTeacher.Columns.Count; i++)
                    {
                        dgvTeacher.Columns[i].Visible = true;
                    }
                }
                #region 設定欄位顯示隱藏(只有當狀態是收合時設定)
                if (splitterListDetial.Expanded)
                {
                    if (PreferenceData.HasAttribute("colClassNameVisible"))
                        colClassName.Visible = (PreferenceData.Attributes["colClassNameVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colStatusVisible"))
                        colStatus.Visible = (PreferenceData.Attributes["colStatusVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colNameVisible"))
                        colName.Visible = (PreferenceData.Attributes["colNameVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colIDNumberVisible"))
                        colIDNumber.Visible = (PreferenceData.Attributes["colIDNumberVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colGenderVisible"))
                        colGender.Visible = (PreferenceData.Attributes["colGenderVisible"].InnerText == "True");
                    if (PreferenceData.HasAttribute("colContactPhoneVisible"))
                        colContactPhone.Visible = ( PreferenceData.Attributes["colContactPhoneVisible"].InnerText == "True" );
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
                        colStatus.DisplayIndex = displayIndex >= dgvTeacher.Columns.Count ? dgvTeacher.Columns.Count - 1 : displayIndex;
                }
                if (PreferenceData.HasAttribute("colClassNameIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colClassNameIndex"].Value, out displayIndex) && displayIndex >= 0 )
                        colClassName.DisplayIndex = displayIndex >= dgvTeacher.Columns.Count ? dgvTeacher.Columns.Count - 1 : displayIndex;
                }
                if (PreferenceData.HasAttribute("colNameIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colNameIndex"].Value, out displayIndex) && displayIndex >= 0 )
                        colName.DisplayIndex = displayIndex >= dgvTeacher.Columns.Count ? dgvTeacher.Columns.Count - 1 : displayIndex;
                }
                if (PreferenceData.HasAttribute("colIDNumberIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colIDNumberIndex"].Value, out displayIndex) && displayIndex >= 0 )
                        this.colIDNumber.DisplayIndex = displayIndex >= dgvTeacher.Columns.Count ? dgvTeacher.Columns.Count - 1 : displayIndex;
                }
                if (PreferenceData.HasAttribute("colGenderIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colGenderIndex"].Value, out displayIndex) && displayIndex >= 0 )
                        colGender.DisplayIndex = displayIndex >= dgvTeacher.Columns.Count ? dgvTeacher.Columns.Count - 1 : displayIndex;
                }
                if (PreferenceData.HasAttribute("colContactPhoneIndex"))
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["colContactPhoneIndex"].Value, out displayIndex) && displayIndex >= 0 )
                        this.colContactPhone.DisplayIndex = displayIndex >= dgvTeacher.Columns.Count ? dgvTeacher.Columns.Count - 1 : displayIndex;
                }
                //讀取外掛欄的顯式順序
                foreach ( DataGridViewColumn var in _ColumnItemCollection.Keys )
                {
                    if ( PreferenceData.HasAttribute("col" + var.HeaderText + "Index") )
                    {
                        int displayIndex;
                        if ( int.TryParse(PreferenceData.Attributes["col" + var.HeaderText + "Index"].Value, out displayIndex) && displayIndex >= 0 )
                            var.DisplayIndex = displayIndex >= dgvTeacher.Columns.Count ? dgvTeacher.Columns.Count - 1 : displayIndex;
                    }
                }
                #endregion
            }
            #endregion

        }

        public ContextMenuStrip ListPaneMenuStrip
        {
            get { return this.dgvTeacher.ContextMenuStrip; }
            set { dgvTeacher.ContextMenuStrip = value; }
        }

        #region IPreference 成員

        void IPreference.UpdatePreference()
        {
            #region 紀錄欄位顯示
            #region 取得PreferenceElement
            XmlElement PreferenceElement = CurrentUser.Instance.Preference["TeacherProvider"];
            if ( PreferenceElement == null )
            {
                PreferenceElement = new XmlDocument().CreateElement("TeacherProvider");
            }
            #endregion
            //紀錄展開
            PreferenceElement.SetAttribute("ListExpanded", splitterListDetial.Expanded ? "False" : "True");
            #region 紀錄欄位顯示隱藏(只有當狀態是收合時記錄)
            if ( splitterListDetial.Expanded )
            {
                PreferenceElement.SetAttribute("colStatusVisible", colStatus.Visible ? "True" : "False");
                PreferenceElement.SetAttribute("colClassNameVisible", colClassName.Visible ? "True" : "False");
                PreferenceElement.SetAttribute("colIDNumberVisible", colIDNumber.Visible ? "True" : "False");
                PreferenceElement.SetAttribute("colGenderVisible", colGender.Visible ? "True" : "False");
                PreferenceElement.SetAttribute("colNameVisible", colName.Visible ? "True" : "False");
                PreferenceElement.SetAttribute("colContactPhoneVisible", colContactPhone.Visible ? "True" : "False");
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
            if ( colName.DisplayIndex >= 0 )
                PreferenceElement.SetAttribute("colNameIndex", colName.DisplayIndex.ToString());
            if ( colIDNumber.DisplayIndex >= 0 )
                PreferenceElement.SetAttribute("colIDNumberIndex", this.colIDNumber.DisplayIndex.ToString());
            if ( colGender.DisplayIndex >= 0 )
                PreferenceElement.SetAttribute("colGenderIndex", colGender.DisplayIndex.ToString());
            if ( colContactPhone.DisplayIndex >= 0 )
                PreferenceElement.SetAttribute("colContactPhoneIndex", this.colContactPhone.DisplayIndex.ToString());
            //紀錄外掛資料行顯示隱藏
            foreach ( DataGridViewColumn var in _ColumnItemCollection.Keys )
            {
                if ( var.DisplayIndex >= 0 )
                    PreferenceElement.SetAttribute("col" + var.HeaderText + "Index", var.DisplayIndex.ToString());
            }
            #endregion

            CurrentUser.Instance.Preference["TeacherProvider"] = PreferenceElement;
            #endregion
        }

        #endregion

        private void dragDropTimer_Tick(object sender, EventArgs e)
        {
            _MouseDownRow = null;
            dgvTeacher.Cursor = System.Windows.Forms.Cursors.No;
            dragDropTimer.Stop();
            DoDragDrop(SelectedList, DragDropEffects.All);
            dgvTeacher.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void dgvStudent_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            _keepingSelection = null;
            if (_MouseDownRow != null)
            {
                foreach (DataGridViewRow row in dgvTeacher.SelectedRows)
                {
                    row.Selected = (row == _MouseDownRow);
                }
            }
            _MouseDownRow = null;
            dragDropTimer.Stop();
        }

        private void dgvStudent_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            _keepingSelection = null;
            if (e.Button == MouseButtons.Left && _MouseDownRow != null)
            {
                foreach (DataGridViewRow row in dgvTeacher.SelectedRows)
                {
                    row.Selected = (row == _MouseDownRow);
                }
            }
            _MouseDownRow = null;
            dragDropTimer.Stop();
        }

        private void dgvTeacher_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                MotherForm.Instance.SetWaitCursor();
                PopupPalmerwormTeacher.ShowPopupPalmerwormTeacher(dgvTeacher.Rows[e.RowIndex].Cells[0].Value.ToString());
                MotherForm.Instance.ResetWaitCursor();
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
            foreach ( DataGridViewRow row in this.dgvTeacher.Rows )
            {
                string id = ( row.Tag as BriefTeacherData ).ID;
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

            foreach ( DataGridViewRow row in dgvTeacher.Rows )
            {
                string id = ( row.Tag as BriefTeacherData ).ID;
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
            this.dgvTeacher.Columns.Add(newColumn);
            newColumn.MinimumWidth = newColumn.Width - 11;
            newColumn.FillWeight = colName.FillWeight * newColumn.MinimumWidth / colName.MinimumWidth;
            newColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            #region 讀取設定檔
            XmlElement PreferenceData = CurrentUser.Instance.Preference["TeacherProvider"];
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
                        newColumn.DisplayIndex = displayIndex >= dgvTeacher.Columns.Count ? dgvTeacher.Columns.Count - 1 : displayIndex;
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
