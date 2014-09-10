using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Rendering;
using SmartSchool.API.PlugIn.View;
using SmartSchool.API.PlugIn;
using System.Xml;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.PlugIn.ExtendedColumn;

namespace SmartSchool
{
    public partial class GeneralEntityView : UserControl, IEntity, Customization.PlugIn.IManager<ButtonAdapter>,IManager<IColumnItem> ,IPreference
    {
        private Collection<NavigationPlanner> _Planners = new Collection<NavigationPlanner>();
        private Collection<string> _FiltratedList = new Collection<string>();
        private List<string> _DisplaySource = new List<string>();
        private List<string> _SelectedSource = new List<string>();
        private List<string> _TemporaSource = new List<string>();
        public event EventHandler SelectedSourceChanged;
        public event EventHandler TemporaSourceChanged;
        //被選取的View
        private NavigationPlanner _SelectedPlanner = null;
        private bool _Reflash = false;
        private bool _JustReflashDIsplay = false;
        private bool _ShowLoading = false;
        private bool _CheckSelection = false;

        public GeneralEntityView()
        {
            InitializeComponent();
            #region 整理畫面細節
            expandableSplitter3.GripLightColor = expandableSplitter3.GripDarkColor = System.Drawing.Color.Transparent;
            if ( GlobalManager.Renderer is Office2007Renderer )
            {
                ( GlobalManager.Renderer as Office2007Renderer ).ColorTableChanged += delegate
                {
                    this.dgvDisplayList.AlternatingRowsDefaultCellStyle.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.MouseOver.TopBackground.End;
                    SetForeColor(this.eppViewSelector);
                };
                this.dgvDisplayList.AlternatingRowsDefaultCellStyle.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.MouseOver.TopBackground.End;
            }
            #endregion
            #region 處理顯視模式的選取
            List<RadioButton> _Buttons = new List<RadioButton>();
            _Planners.ItemsChanged += delegate
            {
                this.eppViewSelector.SuspendLayout();
                _SelectedPlanner = null;
                int itemcount = _Buttons.Count < _Planners.Count ? _Buttons.Count : _Planners.Count;
                for ( int i = 0 ; i < itemcount ; i++ )
                {
                    if ( _Buttons[i].Checked )
                        _SelectedPlanner = (NavigationPlanner)_Buttons[i].Tag;
                }
                if ( !_Planners.Contains(_SelectedPlanner) )
                    _SelectedPlanner = null;
                int Y = this.eppViewSelector.TitleHeight + 6;
                int speace =0;
                #region 補按鈕到足夠數量(並設定按紐被選起時委派處理)
                for ( int i = _Buttons.Count ; i < _Planners.Count ; i++ )
                {
                    RadioButton newButton = new RadioButton();
                    newButton.Font = this.Font;
                    newButton.TabIndex = 0;
                    newButton.TabStop = true;
                    newButton.AutoSize = true;
                    newButton.CheckedChanged += delegate(object bs, EventArgs be)
                    {
                        RadioButton r = (RadioButton)bs;
                        if ( r.Checked )
                        {
                            this.panel2.Controls.Clear();
                            _SelectedPlanner = (NavigationPlanner)r.Tag;
                            this.panel2.Controls.Add(_SelectedPlanner.DisplayControl);
                            _SelectedPlanner.DisplayControl.Dock = DockStyle.Fill;
                            pictureBox1.BackColor = _SelectedPlanner.DisplayControl.BackColor;
                            Reload();
                        }
                    };
                    this.eppViewSelector.Controls.Add(newButton);
                    _Buttons.Add(newButton);
                }
                #endregion
                #region 隱藏多餘的按鈕
                for ( int i = _Buttons.Count - 1 ; i >= _Planners.Count ; i-- )
                {
                    _Buttons[i].Visible = false;
                }
                #endregion
                #region 設定按紐顯示並看哪個該被選起來
                for ( int i = 0 ; i < _Planners.Count ; i++ )
                {
                    if ( _Planners[i].Text == Preference.GetAttribute("Panner") )//選Preference記錄的Planner
                    {
                        for ( int j = 0 ; j < i ; j++ )
                        {
                            if ( _Buttons[i].Checked )
                                _Buttons[i].Checked = false;
                        }
                        _SelectedPlanner = _Planners[i];
                    }
                    RadioButton newButton = _Buttons[i];
                    newButton.Tag = _Planners[i];
                    newButton.Text = _Planners[i].Text;
                    if ( _SelectedPlanner == null && i == 0 )
                        newButton.Checked = true;
                    else
                    {
                        if ( _Planners[i].Equals(_SelectedPlanner) )
                            newButton.Checked = true;
                        else
                            newButton.Checked = false;
                    }
                    newButton.Location = new Point(12, Y);
                    Y += newButton.Height + speace;
                    newButton.Visible = true;
                }
                #endregion
                Y += 6;
                this.eppViewSelector.Size = new Size(this.eppViewSelector.Width, Y);
                SetForeColor(this.eppViewSelector);
                this.eppViewSelector.Visible = _Planners.Count > 1;
                this.eppViewSelector.ResumeLayout();
            };
            #endregion
            #region 當View縮起時一併拉起viewSelector
            eppView.ExpandedChanged += delegate
            {
                navigationPanePanel1.SuspendLayout();
                pictureBox1.Visible = ( _ShowLoading ) & eppView.Expanded;
                this.eppViewSelector.Dock = ( eppView.Expanded ? DockStyle.Bottom : DockStyle.Top );
                eppView.Dock = ( eppView.Expanded ? DockStyle.Fill : DockStyle.Top );
                navigationPanePanel1.Controls.SetChildIndex(eppViewSelector, ( eppView.Expanded ? 2 : 0 ));
                navigationPanePanel1.ResumeLayout();
            }; 
            #endregion
            eppView.SizeChanged += delegate
            {
                int x = ( eppView.Width - pictureBox1.Width ) / 2;
                int y = ( eppView.Height - pictureBox1.Height ) / 3;
                if ( x < 0 ) x = 0;
                if ( y < 0 ) y = 0;
                pictureBox1.Location = new Point(x, y);
            };
            PreferenceUpdater.Instance.Items.Add(this);
            _ContexMenuManager = new ButtonAdapterPlugInManager(itemContainer1);
            _ContexMenuManager.TemplateButton = buttonItem4;
            _FiltratedList.ItemsChanged += delegate { _Reflash = true; };
            Planners.ItemAdded += delegate(object sender, ItemEventArgs<NavigationPlanner> e){e.Item.SelectedSource.ItemsChanged += new EventHandler(PlannerSourceChanged); };
            Planners.ItemRemoved +=delegate(object sender, ItemEventArgs<NavigationPlanner> e){e.Item.SelectedSource.ItemsChanged -= new EventHandler(PlannerSourceChanged); };
            Application.Idle += new EventHandler(Application_Idle);
            this.TemporaSourceChanged += delegate
            {
                this.btnTempory.Text = "待處理" + this.Title + "(" + TemporaSource.Count + ")";
                if ( btnTempory.Checked )
                {
                    _JustReflashDIsplay = true;
                    //_DisplaySource.Clear();
                }
            };
            this.SelectedSourceChanged += delegate
            {
                #region 變更清單右鍵選項
                addToTemp.Visible = true;
                removeFormTemp.Visible = true;

                foreach ( DevComponents.DotNetBar.BaseItem btn in itemContainer1.SubItems )
                {
                    btn.Enabled = ( _SelectedSource.Count > 0 );
                }

                bool canAdd = false;
                bool canRemove = false;
                foreach ( string id in _SelectedSource )
                {
                    if ( _TemporaSource.Contains(id) )
                        canRemove = true;
                    else
                        canAdd = true;
                }
                addToTemp.Enabled = canAdd;
                removeFormTemp.Enabled = canRemove;
                #endregion
            };
        }
        protected List<string> SelectedSource { get { return _SelectedSource; } }
        protected List<string> DisplaySource { get { return _DisplaySource; } }
        protected List<string> TemporaSource { get { return _TemporaSource; } }
        protected virtual void FillSource(bool selectAll)
        {
            MotherForm.Instance.SetWaitCursor();
            int scrollIndex = dgvDisplayList.FirstDisplayedScrollingRowIndex;
            //清空並重新填入學生
            DataGridViewRow[] newRows = new DataGridViewRow[DisplaySource.Count];
            for ( int i = 0 ; i < DisplaySource.Count ; i++ )
            {
                newRows[i] = new DataGridViewRow();
                newRows[i].CreateCells(dgvDisplayList);
            }
            dgvDisplayList.Rows.Clear();
            dgvDisplayList.Rows.AddRange(newRows);
            foreach ( IColumnItem plugInColumn in _ColumnItemCollection.Values )
            {
                plugInColumn.FillExtendedValues(DisplaySource);
            }
            int index = 0;
            foreach ( string id in DisplaySource)
            {
                DataGridViewRow row = newRows[index++];
                row.Tag = id;
                object[] list = new object[dgvDisplayList.Columns.Count];
                object[] values = GetDisplaySource(id);
                int sourceIndex = 0;
                for ( int i = 0 ; i < dgvDisplayList.Columns.Count ; i++ )
                {
                    if ( _ColumnItemCollection.ContainsKey(dgvDisplayList.Columns[i]) )
                        list[i] = _ColumnItemCollection[dgvDisplayList.Columns[i]].ExtendedValues[id];
                    else
                        list[i] = values[sourceIndex++];
                }
                row.SetValues(list);
                row.Selected = selectAll ? true : _SelectedSource.Contains(id);
            }
            if ( DisplaySource.Count > scrollIndex && scrollIndex >= 0 )
                dgvDisplayList.FirstDisplayedScrollingRowIndex = scrollIndex;
            dgvDisplayList_Sorted(null, null);
            MotherForm.Instance.ResetWaitCursor();
        }
        protected virtual object[] GetDisplaySource(string id)
        {
            return new object[0];
        }

        /// <summary>
        ///重新整理資料
        /// </summary>
        protected virtual void Reload()
        {
            _Reflash = true;
        }
        protected virtual void RloadDisplayData()
        {
            _JustReflashDIsplay = true;
        }
        /// <summary>
        /// 顯示讀取中的圈圈
        /// </summary>
        protected bool ShowLoading { get { return _ShowLoading; } set { _ShowLoading = value; } }
        /// <summary>
        /// 新增一個資料列
        /// </summary>
        /// <param name="columnName">資料列標題</param>
        /// <returns>新增的資料列</returns>
        protected DataGridViewColumn AddNewColumn(string columnName)
        {
            DataGridViewTextBoxColumn newColumn = new DataGridViewTextBoxColumn();
            newColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            newColumn.HeaderText = columnName;
            newColumn.ReadOnly = true;
            dgvDisplayList.Columns.Add(newColumn);
            #region 讀取設定檔
            XmlElement PreferenceData = Preference;
            if ( PreferenceData != null )
            {
                #region 設定欄位顯示隱藏(只有當狀態是收合時設定)
                if ( splitterListDetial.Expanded )
                {
                    if ( PreferenceData.HasAttribute("col" + columnName + "Visible") )
                        newColumn.Visible = !( PreferenceData.Attributes["col" + columnName + "Visible"].InnerText == "False" );
                }
                else
                {
                    if ( PreferenceData.HasAttribute("col" + columnName + "ExtVisible") )
                        newColumn.Visible = !( PreferenceData.Attributes["col" + columnName + "ExtVisible"].InnerText == "False" );
                }
                #endregion
                #region 設定清單顯示順序
                if ( PreferenceData.HasAttribute("col" + columnName + "Index") )
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["col" + columnName + "Index"].Value, out displayIndex) && displayIndex > 0 )
                        newColumn.DisplayIndex = displayIndex >= dgvDisplayList.Columns.Count ? dgvDisplayList.Columns.Count - 1 : displayIndex;
                }
                #endregion
            }
            #endregion
            return newColumn;
        }
        /// <summary>
        /// 新增一個資料列
        /// </summary>
        /// <param name="columnName">資料列標題</param>
        /// <param name="minWidth">最小寬度</param>
        /// <returns>新增的資料列</returns>
        protected DataGridViewColumn AddNewColumn(string columnName, int minWidth)
        {
            DataGridViewTextBoxColumn newColumn = new DataGridViewTextBoxColumn();
            newColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            newColumn.HeaderText = columnName;
            newColumn.ReadOnly = true;
            newColumn.MinimumWidth = minWidth;
            newColumn.FillWeight = minWidth;
            dgvDisplayList.Columns.Add(newColumn);
            #region 讀取設定檔
            XmlElement PreferenceData = Preference;
            if ( PreferenceData != null )
            {
                #region 設定欄位顯示隱藏(只有當狀態是收合時設定)
                if ( splitterListDetial.Expanded )
                {
                    if ( PreferenceData.HasAttribute("col" + columnName + "Visible") )
                        newColumn.Visible = !( PreferenceData.Attributes["col" + columnName + "Visible"].InnerText == "False" );
                }
                else
                {
                    if ( PreferenceData.HasAttribute("col" + columnName + "ExtVisible") )
                        newColumn.Visible = !( PreferenceData.Attributes["col" + columnName + "ExtVisible"].InnerText == "False" );
                }
                #endregion
                #region 設定清單顯示順序
                if ( PreferenceData.HasAttribute("col" + columnName + "Index") )
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["col" + columnName + "Index"].Value, out displayIndex) && displayIndex > 0 )
                        newColumn.DisplayIndex = displayIndex >= dgvDisplayList.Columns.Count ? dgvDisplayList.Columns.Count - 1 : displayIndex;
                }
                #endregion
            }
            #endregion
            return newColumn;
        }
        /// <summary>
        /// 新增一個資料列
        /// </summary>
        /// <param name="columnName">資料列標題</param>
        /// <param name="minWidth">最小寬度</param>
        /// <param name="fillWeigth">自動欄寬比重</param>
        /// <returns>新增的資料列</returns>
        protected DataGridViewColumn AddNewColumn(string columnName, int minWidth,float fillWeigth)
        {
            DataGridViewTextBoxColumn newColumn = new DataGridViewTextBoxColumn();
            newColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            newColumn.HeaderText = columnName;
            newColumn.ReadOnly = true;
            newColumn.MinimumWidth = minWidth;
            newColumn.FillWeight = fillWeigth;
            dgvDisplayList.Columns.Add(newColumn);
            #region 讀取設定檔
            XmlElement PreferenceData = Preference;
            if ( PreferenceData != null )
            {
                #region 設定欄位顯示隱藏(只有當狀態是收合時設定)
                if ( splitterListDetial.Expanded )
                {
                    if ( PreferenceData.HasAttribute("col" + columnName + "Visible") )
                        newColumn.Visible = !( PreferenceData.Attributes["col" + columnName + "Visible"].InnerText == "False" );
                }
                else
                {
                    if ( PreferenceData.HasAttribute("col" + columnName + "ExtVisible") )
                        newColumn.Visible = !( PreferenceData.Attributes["col" + columnName + "ExtVisible"].InnerText == "False" );
                }
                #endregion
                #region 設定清單顯示順序
                if ( PreferenceData.HasAttribute("col" + columnName + "Index") )
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["col" + columnName + "Index"].Value, out displayIndex) && displayIndex > 0 )
                        newColumn.DisplayIndex = displayIndex >= dgvDisplayList.Columns.Count ? dgvDisplayList.Columns.Count - 1 : displayIndex;
                }
                #endregion
            }
            #endregion
            return newColumn;
        }

        /// <summary>
        /// 新增一個資料列
        /// </summary>
        /// <param name="columnName">資料列標題</param>
        /// <param name="minWidth">最小寬度</param>
        /// <param name="fillWeigth">自動欄寬比重</param>
        /// <param name="denyColumnVisibleManager">這個資料列不讓使用者選擇顯示或隱藏</param>
        /// <returns>新增的資料列</returns>
        protected DataGridViewColumn AddNewColumn(string columnName, int minWidth, float fillWeigth, bool denyColumnVisibleManager)
        {
            DataGridViewTextBoxColumn newColumn = new DataGridViewTextBoxColumn();
            newColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            newColumn.HeaderText = columnName;
            newColumn.ReadOnly = true;
            newColumn.MinimumWidth = minWidth;
            newColumn.FillWeight = fillWeigth;
            dgvDisplayList.Columns.Add(newColumn);
            if ( denyColumnVisibleManager )
                _DenyColumns.Add(newColumn);
            else
            {
                #region 讀取設定檔
                XmlElement PreferenceData = Preference;
                if ( PreferenceData != null )
                {
                    #region 設定欄位顯示隱藏(只有當狀態是收合時設定)
                    if ( splitterListDetial.Expanded )
                    {
                        if ( PreferenceData.HasAttribute("col" + columnName + "Visible") )
                            newColumn.Visible = !( PreferenceData.Attributes["col" + columnName + "Visible"].InnerText == "False" );
                    }
                    else
                    {
                        if ( PreferenceData.HasAttribute("col" + columnName + "ExtVisible") )
                            newColumn.Visible = !( PreferenceData.Attributes["col" + columnName + "ExtVisible"].InnerText == "False" );
                    }
                    #endregion
                    #region 設定清單顯示順序
                    if ( PreferenceData.HasAttribute("col" + columnName + "Index") )
                    {
                        int displayIndex;
                        if ( int.TryParse(PreferenceData.Attributes["col" + columnName + "Index"].Value, out displayIndex) && displayIndex > 0 )
                            newColumn.DisplayIndex = displayIndex >= dgvDisplayList.Columns.Count ? dgvDisplayList.Columns.Count - 1 : displayIndex;
                    }
                    #endregion
                }
                #endregion
            }
            return newColumn;
        }
        /// <summary>
        /// 新增一個資料列
        /// </summary>
        /// <param name="column">要新增的資料列</param>
        /// <returns>新增的資料列</returns>
        protected DataGridViewColumn AddNewColumn(DataGridViewColumn column)
        {
            dgvDisplayList.Columns.Add(column);
            return column;
        }
        
        /// <summary>
        /// 新增一個資料列
        /// </summary>
        /// <param name="column">要新增的資料列</param>
        /// <param name="denyColumnVisibleManager">這個資料列不讓使用者選擇顯示或隱藏</param>
        /// <returns>新增的資料列</returns>
        protected DataGridViewColumn AddNewColumn(DataGridViewColumn column, bool denyColumnVisibleManager)
        {
            dgvDisplayList.Columns.Add(column);
            if ( denyColumnVisibleManager )
                _DenyColumns.Add(column);
            return column;
        }
        #region IManager<IColumnItem> 成員
        private Dictionary<DataGridViewColumn, IColumnItem> _ColumnItemCollection = new Dictionary<DataGridViewColumn, IColumnItem>();

        void IManager<IColumnItem>.Add(IColumnItem instance)
        {
            instance.VariableChanged += new EventHandler(instance_VariableChanged);
            DataGridViewColumn newColumn = new DataGridViewColumn(new DataGridViewTextBoxCell());
            newColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            newColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            newColumn.HeaderText = instance.ColumnHeader;
            _ColumnItemCollection.Add(newColumn, instance);
            newColumn.Visible = true;
            this.dgvDisplayList.Columns.Add(newColumn);
            newColumn.MinimumWidth = newColumn.Width - 11;
            newColumn.FillWeight = newColumn.MinimumWidth;
            newColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            #region 讀取設定檔
            XmlElement PreferenceData = Preference;
            if ( PreferenceData != null )
            {
                #region 設定欄位顯示隱藏(只有當狀態是收合時設定)
                if ( splitterListDetial.Expanded )
                {
                    if ( PreferenceData.HasAttribute("col" + instance.ColumnHeader + "Visible") )
                        newColumn.Visible = !( PreferenceData.Attributes["col" + instance.ColumnHeader + "Visible"].InnerText == "False" );
                }
                else
                {
                    if ( PreferenceData.HasAttribute("col" + instance.ColumnHeader + "ExtVisible") )
                        newColumn.Visible = !( PreferenceData.Attributes["col" + instance.ColumnHeader + "ExtVisible"].InnerText == "False" );
                }
                #endregion
                #region 設定清單顯示順序
                if ( PreferenceData.HasAttribute("col" + instance.ColumnHeader + "Index") )
                {
                    int displayIndex;
                    if ( int.TryParse(PreferenceData.Attributes["col" + instance.ColumnHeader + "Index"].Value, out displayIndex) && displayIndex > 0 )
                        newColumn.DisplayIndex = displayIndex >= dgvDisplayList.Columns.Count ? dgvDisplayList.Columns.Count - 1 : displayIndex;
                }
                #endregion
            }
            #endregion
        }

        void IManager<IColumnItem>.Remove(IColumnItem instance)
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

        private void instance_VariableChanged(object sender, EventArgs e)
        {
            IColumnItem columnItem = ( (IColumnItem)sender );
            columnItem.FillExtendedValues(DisplaySource);

            int index = 0;
            foreach ( DataGridViewColumn col in _ColumnItemCollection.Keys )
            {
                if ( _ColumnItemCollection[col] == columnItem )
                {
                    index = col.Index;
                    break;
                }
            }

            foreach ( DataGridViewRow row in dgvDisplayList.Rows )
            {
                string id = "" + row.Tag;
                row.Cells[index].Value = columnItem.ExtendedValues[id];
            }
        }

        #endregion

        /// <summary>
        /// 顯示模式集合
        /// </summary>
        protected Collection<NavigationPlanner> Planners { get { return _Planners; } }
        /// <summary>
        /// 經過篩選後的資料集合
        /// </summary>
        protected Collection<string> FiltratedList { get { return _FiltratedList; } }
        #region IEntity 成員

        public virtual string Title
        {
            get { return buttonItem1.Text; }
        }

        public virtual DevComponents.DotNetBar.NavigationPanePanel NavPanPanel
        {
            get { return navigationPanePanel1; }
        }

        public virtual Panel ContentPanel
        {
            get { return panelContent; }
        }

        public virtual Image Picture
        {
            get { return buttonItem1.Image; }
        }

        public virtual void Actived()
        {
            this.btnTempory.Text = "待處理" + this.Title + "(" + TemporaSource.Count + ")";
            #region 讀取設定檔
            XmlElement PreferenceData = Preference;
            if ( PreferenceData != null )
            {
                //設定清單顯示寬度
                if ( PreferenceData.HasAttribute("PanelListWidth") )
                {
                    int width;
                    if ( int.TryParse(PreferenceData.Attributes["PanelListWidth"].Value, out width) )
                        splitterListDetial.SplitPosition = width;
                }
                //設定展開
                if ( PreferenceData.HasAttribute("ListExpanded") && PreferenceData.Attributes["ListExpanded"].Value == "True" )
                {
                    buttonExpand.Text = "<<";
                    buttonExpand.Tooltip = "還原";
                    splitterListDetial.Expanded = false;
                    for ( int i = 1 ; i < dgvDisplayList.Columns.Count ; i++ )
                    {
                        dgvDisplayList.Columns[i].Visible = true;
                    }
                }
                #region 設定欄位顯示隱藏(只有當狀態是收合時設定)
                if ( splitterListDetial.Expanded )
                {
                    //讀取外掛欄的顯式隱藏
                    foreach ( DataGridViewColumn var in dgvDisplayList.Columns )
                    {
                        if ( PreferenceData.HasAttribute("col" + var.HeaderText + "Visible") )
                            var.Visible = !( PreferenceData.Attributes["col" + var.HeaderText + "Visible"].InnerText == "False" );
                    }
                }
                else
                {
                    //讀取外掛欄的顯式隱藏
                    foreach ( DataGridViewColumn var in dgvDisplayList.Columns )
                    {
                        if ( PreferenceData.HasAttribute("col" + var.HeaderText + "ExtVisible") )
                            var.Visible = !( PreferenceData.Attributes["col" + var.HeaderText + "ExtVisible"].InnerText == "False" );
                    }
                }
                #endregion
                #region 設定清單顯示順序
                //讀取外掛欄的顯式順序
                foreach ( DataGridViewColumn var in dgvDisplayList.Columns )
                {
                    if ( PreferenceData.HasAttribute("col" + var.HeaderText + "Index") )
                    {
                        int displayIndex;
                        if ( int.TryParse(PreferenceData.Attributes["col" + var.HeaderText + "Index"].Value, out displayIndex) && displayIndex >= 0 )
                            var.DisplayIndex = displayIndex >= dgvDisplayList.Columns.Count ? dgvDisplayList.Columns.Count - 1 : displayIndex;
                    }
                }
                #endregion
            }
            #endregion
        }

        #endregion

        //public bool CheckListChanged(IEnumerable<string> list1, IEnumerable<string> list2)
        //{
        //    List<string> lista = new List<string>(list1);
        //    List<string> listb = new List<string>(list2);
        //    if ( lista.Count != listb.Count )
        //        return true;
        //    else
        //    {
        //        lista.Sort();
        //        listb.Sort();
        //        for ( int i = 0 ; i < listb.Count ; i++ )
        //        {
        //            if ( lista[i] != listb[i] )
        //                return true;
        //        }
        //    }
        //    return false;
        //}

        private void buttonExpand_Click(object sender, EventArgs e)
        {
            if ( splitterListDetial.Expanded )
            {
                //寫入紀錄(目前顯示欄位)
                buttonExpand.Text = "<<";
                buttonExpand.Tooltip = "還原";
                UpdatePreference();
                splitterListDetial.Expanded = false;
                //for ( int i = 1 ; i < dgvDisplayList.Columns.Count ; i++ )
                //{
                //    dgvDisplayList.Columns[i].Visible = true;
                //}
                //#region 讀取設定檔
                //XmlElement PreferenceData = Preference;
                //if ( PreferenceData != null )
                //{
                //    //讀取外掛資料行的設定
                //    foreach ( DataGridViewColumn var in dgvDisplayList.Columns )
                //    {
                //        if ( PreferenceData.HasAttribute("col" + var.HeaderText + "ExtVisible") )
                //            var.Visible = ( PreferenceData.Attributes["col" + var.HeaderText + "ExtVisible"].InnerText == "True" );
                //    }
                //}
                //#endregion
            }
            else
            {
                buttonExpand.Text = ">>";
                buttonExpand.Tooltip = "最大化";
                UpdatePreference();
                splitterListDetial.Expanded = true;
                //for ( int i = 1 ; i < dgvDisplayList.Columns.Count ; i++ )
                //{
                //    dgvDisplayList.Columns[i].Visible = false;
                //}
                ////colName.Visible = true;
                //#region 讀取設定檔
                //XmlElement PreferenceData = Preference;
                //if ( PreferenceData != null )
                //{
                //    //讀取外掛資料行的設定
                //    foreach ( DataGridViewColumn var in dgvDisplayList.Columns )
                //    {
                //        if ( PreferenceData.HasAttribute("col" + var.HeaderText + "Visible") )
                //            var.Visible = ( PreferenceData.Attributes["col" + var.HeaderText + "Visible"].InnerText == "True" );
                //    }
                //}
                //#endregion
            }
        }
        /// <summary>
        /// 設定焦點
        /// </summary>
        private void SetFocus(object sender, EventArgs e)
        {
            Control c = ( (Control)sender );
            if ( c.TopLevelControl.ContainsFocus && !txtSearch.Focused && !c.ContainsFocus )
            {
                List<DataGridViewRow> selectedRow = new List<DataGridViewRow>();
                foreach ( DataGridViewRow var in dgvDisplayList.Rows )
                {
                    if ( var.Selected )
                        selectedRow.Add(var);
                }

                c.Focus();

                foreach ( DataGridViewRow row in dgvDisplayList.Rows )
                {
                    row.Selected = selectedRow.Contains(row);
                }
            }
        }
        /// <summary>
        /// 設定縮放
        /// </summary>
        private void splitterListDetial_ExpandedChanged(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            panelList.ResumeLayout();
            panel3.ResumeLayout();
            panelContent.ResumeLayout(); 
            if (! splitterListDetial.Expanded )
            {
                for ( int i = 1 ; i < dgvDisplayList.Columns.Count ; i++ )
                {
                    dgvDisplayList.Columns[i].Visible = true;
                }
                #region 讀取設定檔
                XmlElement PreferenceData = Preference;
                if ( PreferenceData != null )
                {
                    //讀取外掛資料行的設定
                    foreach ( DataGridViewColumn var in dgvDisplayList.Columns )
                    {
                        if ( PreferenceData.HasAttribute("col" + var.HeaderText + "ExtVisible") )
                            var.Visible = ( PreferenceData.Attributes["col" + var.HeaderText + "ExtVisible"].InnerText == "True" );
                    }
                }
                #endregion
            }
            else
            {
                for ( int i = 1 ; i < dgvDisplayList.Columns.Count ; i++ )
                {
                    dgvDisplayList.Columns[i].Visible = false;
                }
                //colName.Visible = true;
                #region 讀取設定檔
                XmlElement PreferenceData = Preference;
                if ( PreferenceData != null )
                {
                    //讀取外掛資料行的設定
                    foreach ( DataGridViewColumn var in dgvDisplayList.Columns )
                    {
                        if ( PreferenceData.HasAttribute("col" + var.HeaderText + "Visible") )
                            var.Visible = ( PreferenceData.Attributes["col" + var.HeaderText + "Visible"].InnerText == "True" );
                    }
                }
                #endregion
            }
        }
        /// <summary>
        /// 設定設定縮放
        /// </summary>
        private void splitterListDetial_ExpandedChanging(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            panelContent.SuspendLayout();
            panelList.SuspendLayout();
            panel3.SuspendLayout();
            if ( splitterListDetial.Expanded )
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
        //把控制項中的RadioButton都改成漂亮顯示
        private void SetForeColor(Control parent)
        {
            foreach ( Control var in parent.Controls )
            {
                if ( var is RadioButton )
                    var.ForeColor = ( (Office2007Renderer)GlobalManager.Renderer ).ColorTable.CheckBoxItem.Default.Text;
                SetForeColor(var);
            }
        }
        protected XmlElement Preference
        {
            get
            {
                XmlElement PreferenceElement = CurrentUser.Instance.Preference["GeneralEntity_" + this.buttonItem1.Text];
                if ( PreferenceElement == null )
                {
                    PreferenceElement = new XmlDocument().CreateElement("GeneralEntity_" + this.buttonItem1.Text);
                }
                return PreferenceElement;
            }
            set
            {
                CurrentUser.Instance.Preference["GeneralEntity_" + this.buttonItem1.Text] = value;
            }
        }
        public void UpdatePreference()
        {
            #region 紀錄欄位顯示
            #region 取得PreferenceElement
            XmlElement PreferenceElement = Preference;
            #endregion
            //紀錄展開
            PreferenceElement.SetAttribute("ListExpanded", splitterListDetial.Expanded ? "False" : "True");
            #region 紀錄欄位顯示隱藏(只有當狀態是收合時記錄)
            if ( splitterListDetial.Expanded )
            {
                //紀錄資料行顯示隱藏
                foreach ( DataGridViewColumn var in dgvDisplayList.Columns )
                {
                    PreferenceElement.SetAttribute("col" + var.HeaderText + "Visible", var.Visible ? "True" : "False");
                }
            }
            else
                foreach ( DataGridViewColumn var in dgvDisplayList.Columns )
                {
                    PreferenceElement.SetAttribute("col" + var.HeaderText + "ExtVisible", var.Visible ? "True" : "False");
                }
            #endregion
            //紀錄清單顯示寬度(只有當狀態是收合時記錄)
            if ( splitterListDetial.Expanded )
            {
                PreferenceElement.SetAttribute("PanelListWidth", panelList.Width.ToString());
            }
            #region 紀錄清單顯示順序
            //紀錄資料行顯示隱藏
            foreach ( DataGridViewColumn var in dgvDisplayList.Columns )
            {
                if ( var.DisplayIndex >= 0 )
                    PreferenceElement.SetAttribute("col" + var.HeaderText + "Index", var.DisplayIndex.ToString());
            }
            #endregion
            //記錄檢視模式
            if ( _SelectedPlanner != null )
                PreferenceElement.SetAttribute("Panner", _SelectedPlanner.Text);
            Preference = PreferenceElement;
            #endregion
        }
        private void PlannerSourceChanged(object sender, EventArgs e)
        {
            //if ( ( (NavigationPlanner)sender ) == _SelectedPlanner )
            {
                if ( panel2.ContainsFocus )
                {
                    btnTempory.Checked = false;
                }
                _JustReflashDIsplay = true;
            }
        }
        private void Application_Idle(object sender, EventArgs e)
        {
            pictureBox1.Visible = ( _ShowLoading ) & eppView.Expanded;
            #region 重新整理資料
            if ( _Reflash && _SelectedPlanner != null )
            {
                _SelectedPlanner.Perform(new List<string>(_FiltratedList));
            }
            #endregion
            #region 檢查Display的資料變更
            if (  _JustReflashDIsplay )
            {
                List<string> source;
                if ( _SelectedPlanner == null && !btnTempory.Checked )
                    source = new List<string>();
                else if ( btnTempory.Checked )
                    source = new List<string>(TemporaSource);
                else source = new List<string>(_SelectedPlanner.SelectedSource);
                _DisplaySource = source;
                FillSource(btnTempory.Checked?false: ( Control.ModifierKeys & Keys.Control ) == Keys.Control);
            }
            #endregion
            #region 檢查選取資料變更
            if ( _CheckSelection )
            {
                List<string> _SelectedList = new List<string>();
                foreach ( DataGridViewRow var in dgvDisplayList.SelectedRows )
                {
                    _SelectedList.Insert(0, "" + var.Tag);
                }
                if ( _SelectedList.Count == _SelectedSource.Count )
                {
                    _SelectedList.Sort();
                    for ( int i = 0 ; i < _SelectedSource.Count ; i++ )
                    {
                        if ( _SelectedSource[i] != _SelectedList[i] )
                        {
                            _SelectedSource = _SelectedList;
                            if ( SelectedSourceChanged != null )
                                SelectedSourceChanged(this, new EventArgs());
                            break;
                        }
                    }
                }
                else
                {
                    _SelectedSource = _SelectedList;
                    if ( SelectedSourceChanged != null )
                        SelectedSourceChanged(this, new EventArgs());
                }
            } 
            #endregion
            _JustReflashDIsplay = false;
            _Reflash = false;
            _CheckSelection = false;
        }

        #region IManager<ButtonAdapter> 成員
        private ButtonAdapterPlugInManager _ContexMenuManager;
        void Customization.PlugIn.IManager<ButtonAdapter>.Add(ButtonAdapter button)
        {
            _ContexMenuManager.Add(button);
        }

        void Customization.PlugIn.IManager<ButtonAdapter>.Remove(ButtonAdapter instance)
        {
            _ContexMenuManager.Remove(instance);
        }
        #endregion

        private void buttonItem2_PopupOpen(object sender, DevComponents.DotNetBar.PopupOpenEventArgs e)
        {
            e.Cancel = dgvDisplayList.PointToClient(Control.MousePosition).Y < dgvDisplayList.ColumnHeadersHeight + 4;
        }
        private Dictionary<DataGridViewColumn, DevComponents.DotNetBar.ButtonItem> _columnManagerItems = new Dictionary<DataGridViewColumn, DevComponents.DotNetBar.ButtonItem>();
        private List<DataGridViewColumn> _DenyColumns = new List<DataGridViewColumn>();
        private void buttonItem3_PopupOpen(object sender, DevComponents.DotNetBar.PopupOpenEventArgs e)
        {
            e.Cancel = dgvDisplayList.PointToClient(Control.MousePosition).Y >= dgvDisplayList.ColumnHeadersHeight+4;
            if ( !e.Cancel )
            {
                buttonItem3.SubItems.Clear();
                foreach ( DataGridViewColumn column in dgvDisplayList.Columns )
                {
                    if ( !_DenyColumns.Contains(column) )
                    {
                        DevComponents.DotNetBar.ButtonItem item;
                        if ( _columnManagerItems.ContainsKey(column) )
                            item = _columnManagerItems[column];
                        else
                        {
                            item = new DevComponents.DotNetBar.ButtonItem();
                            item.AutoCheckOnClick = true;
                            item.AutoCollapseOnClick = false;
                            item.ClickAutoRepeat = true;
                            item.CheckedChanged += delegate(object s, EventArgs es)
                            {
                                DevComponents.DotNetBar.ButtonItem it = (DevComponents.DotNetBar.ButtonItem)s;
                                foreach ( DataGridViewColumn col in _columnManagerItems .Keys)
                                {
                                    if ( _columnManagerItems[col] == it )
                                    {
                                        if ( col.Visible != it.Checked )
                                        {
                                            col.Visible = it.Checked;
                                        }
                                        break;
                                    }
                                }
                            };
                            _columnManagerItems.Add(column, item);
                        }
                        item.Text = column.HeaderText;
                        item.Checked = column.Visible;
                        buttonItem3.SubItems.Add(item);
                    }
                }
            }
        }

        private void addToTemp_Click(object sender, EventArgs e)
        {
            List<string> addList = new List<string>();
            foreach ( string id in _SelectedSource )
            {
                if ( !_TemporaSource.Contains(id) )
                    addList.Add(id);
            }
            if ( addList.Count > 0 )
            {
                _TemporaSource.AddRange(addList);
                if ( TemporaSourceChanged != null )
                    TemporaSourceChanged(this, new EventArgs());
            }
        }

        private void memoveFormTemp_Click(object sender, EventArgs e)
        {
            bool hasChanged = false;
            foreach ( string id in _SelectedSource )
            {
                if ( _TemporaSource.Contains(id) )
                {
                    _TemporaSource.Remove(id);
                    hasChanged = true;
                }
            }
            if ( hasChanged )
            {
                if ( TemporaSourceChanged != null )
                    TemporaSourceChanged(this, new EventArgs());
            }
        }
        public void SetTemporaSource(List<string> list)
        {
            _TemporaSource =new List<string>( list);
            if ( TemporaSourceChanged != null )
                TemporaSourceChanged(this, new EventArgs());
        }

        private void btnTempory_Click(object sender, EventArgs e)
        {
            if ( btnTempory.Checked )
            {
                _DisplaySource.Clear();
                _SelectedSource.Clear();
                _SelectedSource.AddRange(_TemporaSource);
                if ( this.SelectedSourceChanged != null )
                    SelectedSourceChanged(this, new EventArgs());
                _JustReflashDIsplay = true;
            }
            else
            {
                btnTempory.Checked = true;
            }
        }

        private void btnTempory_CheckedChanged(object sender, EventArgs e)
        {
            if ( TemporaSource.Count == 0 && btnTempory.Checked )
            {
                btnTempory.Checked = false;
                return;
            }
            else
            {
                btnTempory.ColorTable= (btnTempory.Checked?  DevComponents.DotNetBar.eButtonColor.Office2007WithBackground: DevComponents.DotNetBar.eButtonColor.OrangeWithBackground);
                if ( btnTempory.Checked )
                {
                    _DisplaySource.Clear();
                    _SelectedSource.Clear();
                    _SelectedSource.AddRange(_TemporaSource);
                    if ( this.SelectedSourceChanged != null )
                        SelectedSourceChanged(this, new EventArgs());
                    _JustReflashDIsplay = true;
                }
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {

            if ( e.KeyCode == Keys.Enter )
            {
                btnSearch.PerformClick();
            }
        }


        //private void btnFilter_MouseEnter(object sender, EventArgs e)
        //{
        //    if ( !btnFilter.Expanded )
        //        btnFilter.Expanded = true;
        //}

        protected void ViewTemp()
        {
            btnTempory.Checked = true;
            if ( TemporaSource.Count > 0 )
            {
                _JustReflashDIsplay = true;
                SelectedSource.Clear();
                SelectedSource.AddRange(TemporaSource);
                if ( this.SelectedSourceChanged != null )
                    SelectedSourceChanged(this, new EventArgs());
            }
        }

        private void dgvDisplayList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private Dictionary<DataGridViewRow, int> _RowIndex = new Dictionary<DataGridViewRow, int>();
        private void dgvDisplayList_Sorted(object sender, EventArgs e)
        {
            //把每個row的順序記錄起來，當下次排序時，碰到同樣大小的值，會依照排序前row的先後排列。
            _RowIndex.Clear();
            foreach ( DataGridViewRow row in this.dgvDisplayList.Rows )
            {
                _RowIndex.Add(row, row.Index);
            }
            //把排序重處理新注冊過，讓它可以在繼承的控制項自訂排序後還再補上同順序時會依原來的順序的功能
            dgvDisplayList.SortCompare -= new DataGridViewSortCompareEventHandler(dgvDisplayList_SortCompare);
            dgvDisplayList.SortCompare+=new DataGridViewSortCompareEventHandler(dgvDisplayList_SortCompare);
        }

        private void dgvDisplayList_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if ( !e.Handled )
            {
                e.SortResult = ( "" + e.CellValue1 ).CompareTo("" + e.CellValue2);
                e.Handled = true;
            }
            if ( e.SortResult == 0 )
            {
                e.SortResult = ( _RowIndex[dgvDisplayList.Rows[e.RowIndex1]] ).CompareTo(_RowIndex[dgvDisplayList.Rows[e.RowIndex2]]);
            }
        }

        private void dgvDisplayList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            _CheckSelection = true;
        }

        private void dgvDisplayList_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            _CheckSelection = true;
        }

        private void dgvDisplayList_SelectionChanged(object sender, EventArgs e)
        {
            _CheckSelection = true;
        }

        private void buttonItem3_PopupClose(object sender, EventArgs e)
        {
            UpdatePreference();
        }
    }
}
