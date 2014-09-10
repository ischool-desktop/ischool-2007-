using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;
using System.Windows.Forms;
using System.ComponentModel;
using DevComponents.DotNetBar;
using System.Drawing;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.TeacherRelated.SourceProvider;
using SmartSchool.Properties;
using SmartSchool.TeacherRelated.Search;
using SmartSchool.Feature.Teacher;
using DevComponents.DotNetBar.Rendering;
using SmartSchool.TeacherRelated.Divider;
using SmartSchool.ClassRelated;
using System.Threading;

namespace SmartSchool.TeacherRelated
{
    public class Teacher : IEntity
    {
        private static Teacher _Instance;
        public static Teacher Instance
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
            _Instance = new Teacher();
        }

        internal void SetupSynchronization()
        {
            //同步更新資料
            SelectionChanged += new EventHandler(Instance_SelectionChanged);
            TeacherDataChanged += new EventHandler<TeacherDataChangedEventArgs>(Instance_TeacherDataChanged);
            TeacherDeleted += new EventHandler<TeacherDeletedEventArgs>(Instance_TeacherDeleted);
            TeacherInserted += new EventHandler(Instance_TeacherInserted);
            Class.Instance.ClassUpdated += new EventHandler<UpdateClassEventArgs>(Instance_ClassUpdated);
            Class.Instance.ClassDeleted += new EventHandler<DeleteClassEventArgs>(Instance_ClassDeleted);
        }

        private Teacher()
        {
            //取得班級跟學生資料
            getBriefData();
        }

        private bool _Initialized = false;
        private List<BriefTeacherData> _SelectionList = new List<BriefTeacherData>();
        private bool _treeViewWait = false;
        //進入Idle時是否重新整理資料
        private bool _ReFlashTree = false;
        //暫存學生清單
        private Dictionary<string, BriefTeacherData> _TeacherList;
        //同步取得班級和學生資料的backGroundWorker
        private BackgroundWorker _BkwBriefDataLoader;

        private ManualResetEvent _LoadTeacher = new ManualResetEvent(true);

        private NavigationPanePanel _NavPanel;
        private DevComponents.DotNetBar.ButtonX _BtnSearchAll;
        private DevComponents.DotNetBar.ExpandablePanel _EppTeacher;
        private DragDropTreeView _TreeViewTeacher;
        private System.Windows.Forms.PictureBox _WaitingPicture;
        private System.Windows.Forms.ContextMenuStrip _ContextMenuReflash;
        private System.Windows.Forms.ToolStripMenuItem _ToolStripReflashItem;
        private ContextMenuStrip listPaneMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAddToTemp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRemoveFromTemp;
        //private System.Windows.Forms.RadioButton _ClassTeacherView;
        //private System.Windows.Forms.RadioButton _CategoryView;
        private ItemSelector<ITeacherDivider> _EppViewMode;
        private bool _IsTreeViewDividing = false;
        //private NormalStatusTeacherSourceProvider _NormalStatusTeacherSourceProvider;
        //private SupervisedBySourceProvider _SupervisedBySourceProvider;
        //private Dictionary<string, SupervisedByGradeSourceProvider> _SupervisedByGradeSourceProviders = new Dictionary<string, SupervisedByGradeSourceProvider>();
        private AllTeacherSourceProvider _AllTeacherSourceProvider = new AllTeacherSourceProvider();
        //private NonCategorySourceProvider _NonCategorySourceProvider = new NonCategorySourceProvider();
        //private Dictionary<string, TeacherCategorySourceProvider> _TeacherCategorySourceProviders = new Dictionary<string, TeacherCategorySourceProvider>();
        //private AllCategorySourceProvider _AllCategorySourceProvider;
        private TempTeacherSourceProvider _TempTeacherSourceProvider;
        //private DeleteStatusTeacherSourceProvider _DeleteStatusTeacherSourceProvider;

        //private TreeNode _ClassTeacherSelectedNode;
        //private TreeNode _CategorySelectedNode;

        //加速選取學生比對判斷選取學生變更用
        private IEnumerator<BriefTeacherData> enumList = new List<BriefTeacherData>(0).GetEnumerator();

        private ContentInfo contentInfo;

        private ClassDivider _ClassDivider = new ClassDivider();
        private CategoryDivider _CategoryDivider = new CategoryDivider();

        private void Init()
        {
            setTreeViewWait();
            Application.Idle += new EventHandler(Application_Idle);

            #region toolStripMenuItemAddToTemp
            this.toolStripMenuItemAddToTemp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAddToTemp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItemAddToTemp.Name = "toolStripMenuItemAddToTemp";
            this.toolStripMenuItemAddToTemp.Size = new System.Drawing.Size(93, 22);
            this.toolStripMenuItemAddToTemp.Text = "加入至待處理";
            this.toolStripMenuItemAddToTemp.Click += new EventHandler(toolStripMenuItemAddToTemp_Click);
            #endregion

            #region toolStripMenuItemRemoveFromTemp
            this.toolStripMenuItemRemoveFromTemp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRemoveFromTemp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItemRemoveFromTemp.Name = "toolStripMenuItemRemoveFromTemp";
            this.toolStripMenuItemRemoveFromTemp.Size = new System.Drawing.Size(93, 22);
            this.toolStripMenuItemRemoveFromTemp.Text = "移出待處理";
            this.toolStripMenuItemRemoveFromTemp.Click += new EventHandler(toolStripMenuItemRemoveFromTemp_Click);
            #endregion

            #region listPaneMenuStrip
            this.listPaneMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.listPaneMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAddToTemp,
            this.toolStripMenuItemRemoveFromTemp});
            this.listPaneMenuStrip.Name = "listPaneMenuStrip";
            this.listPaneMenuStrip.ShowImageMargin = false;
            this.listPaneMenuStrip.Size = new System.Drawing.Size(94, 26);
            #endregion

            #region btnSearchAll
            _BtnSearchAll = new ButtonX();
            this._BtnSearchAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this._BtnSearchAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this._BtnSearchAll.Dock = System.Windows.Forms.DockStyle.Top;
            this._BtnSearchAll.Location = new System.Drawing.Point(0, 0);
            this._BtnSearchAll.Name = "btnSearchAll";
            this._BtnSearchAll.Size = new System.Drawing.Size(139, 23);
            this._BtnSearchAll.SubItemsExpandWidth = 17;
            this._BtnSearchAll.TabIndex = 0;
            this._BtnSearchAll.Text = "搜尋所有教師";
            this._BtnSearchAll.Click += new EventHandler(btnSearchAll_Click);
            #endregion

            #region toolStripMenuItem1
            this._ToolStripReflashItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ToolStripReflashItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._ToolStripReflashItem.Name = "toolStripMenuItem1";
            this._ToolStripReflashItem.Size = new System.Drawing.Size(93, 22);
            this._ToolStripReflashItem.Text = "重新整理";
            this._ToolStripReflashItem.Click += new EventHandler(toolStripMenuItem1_Click);
            #endregion

            #region contextMenuStrip1
            this._ContextMenuReflash = new System.Windows.Forms.ContextMenuStrip();
            this._ContextMenuReflash.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ToolStripReflashItem});
            this._ContextMenuReflash.Name = "contextMenuStrip1";
            this._ContextMenuReflash.ShowImageMargin = false;
            this._ContextMenuReflash.Size = new System.Drawing.Size(94, 26);
            #endregion

            //#region _ClassTeacherView
            //this._ClassTeacherView = new RadioButton();
            //this._ClassTeacherView.AutoSize = true;
            //this._ClassTeacherView.Location = new System.Drawing.Point(12, 28);
            //this._ClassTeacherView.Name = "_ClassTeacherView";
            //this._ClassTeacherView.Size = new System.Drawing.Size(91, 21);
            //this._ClassTeacherView.TabIndex = 0;
            //this._ClassTeacherView.TabStop = true;
            //this._ClassTeacherView.Text = "檢視班導師";
            //this._ClassTeacherView.UseVisualStyleBackColor = true;
            //this._ClassTeacherView.Checked = true;
            //this._ClassTeacherView.FlatStyle = FlatStyle.Flat;
            //#endregion

            //#region _CategoryView
            //this._CategoryView = new RadioButton();
            //this._CategoryView.AutoSize = true;
            //this._CategoryView.Location = new System.Drawing.Point(12, 55);
            //this._CategoryView.Name = "_CategoryView";
            //this._CategoryView.Size = new System.Drawing.Size(117, 21);
            //this._CategoryView.TabIndex = 0;
            //this._CategoryView.TabStop = true;
            //this._CategoryView.Text = "檢視各分類教師";
            //this._CategoryView.UseVisualStyleBackColor = true;
            //this._CategoryView.FlatStyle = FlatStyle.Flat;
            //#endregion

            #region _TreeViewTeacher
            this._TreeViewTeacher = new DragDropTreeView();
            this._TreeViewTeacher.BackColor = System.Drawing.Color.White;
            this._TreeViewTeacher.ContextMenuStrip = this._ContextMenuReflash;
            this._TreeViewTeacher.Cursor = System.Windows.Forms.Cursors.Default;
            this._TreeViewTeacher.Dock = System.Windows.Forms.DockStyle.Fill;
            this._TreeViewTeacher.ForeColor = System.Drawing.Color.Black;
            this._TreeViewTeacher.BorderStyle = BorderStyle.Fixed3D;
            this._TreeViewTeacher.HotTracking = true;
            this._TreeViewTeacher.ItemHeight = 20;
            this._TreeViewTeacher.Location = new System.Drawing.Point(0, 23);
            this._TreeViewTeacher.Name = "treeViewStudent";
            this._TreeViewTeacher.Size = new System.Drawing.Size(139, 410);
            this._TreeViewTeacher.TabIndex = 1;
            this._TreeViewTeacher.AfterSelect += new TreeViewEventHandler(treeViewStudent_AfterSelect);
            this._TreeViewTeacher.NodeMouseClick += new TreeNodeMouseClickEventHandler(_TreeViewTeacher_NodeMouseClick);
            #endregion

            #region pictureBox1
            this._WaitingPicture = new System.Windows.Forms.PictureBox();
            this._WaitingPicture.BackColor = System.Drawing.Color.White;
            this._WaitingPicture.Image = global::SmartSchool.Properties.Resources.loading5;
            this._WaitingPicture.Location = new System.Drawing.Point(52, 56);
            this._WaitingPicture.Name = "pictureBox1";
            this._WaitingPicture.Size = new System.Drawing.Size(32, 32);
            this._WaitingPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this._WaitingPicture.TabIndex = 3;
            this._WaitingPicture.TabStop = false;
            #endregion

            #region _EppTeacher
            this._EppTeacher = new DevComponents.DotNetBar.ExpandablePanel();
            this._EppTeacher.CanvasColor = System.Drawing.SystemColors.Control;
            this._EppTeacher.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this._EppTeacher.Controls.Add(this._WaitingPicture);
            this._EppTeacher.Controls.Add(this._TreeViewTeacher);
            this._EppTeacher.Dock = System.Windows.Forms.DockStyle.Fill;
            this._EppTeacher.Name = "_EppTeacher";
            this._EppTeacher.Size = new System.Drawing.Size(139, 433);
            this._EppTeacher.Style.Alignment = System.Drawing.StringAlignment.Center;
            this._EppTeacher.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this._EppTeacher.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this._EppTeacher.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this._EppTeacher.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this._EppTeacher.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this._EppTeacher.Style.GradientAngle = 90;
            this._EppTeacher.TabIndex = 1;
            this._EppTeacher.TitleHeight = 23;
            this._EppTeacher.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this._EppTeacher.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this._EppTeacher.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this._EppTeacher.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this._EppTeacher.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this._EppTeacher.TitleStyle.CornerDiameter = 2;
            this._EppTeacher.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this._EppTeacher.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this._EppTeacher.TitleStyle.GradientAngle = 90;
            this._EppTeacher.TitleText = "教師類別";            
            this._EppTeacher.ExpandedChanged += new DevComponents.DotNetBar.ExpandChangeEventHandler(this.eppInSchoolStudent_ExpandedChanged);
            this._EppTeacher.SizeChanged += new System.EventHandler(this.eppInSchoolStudent_SizeChanged);
            #endregion

            #region TempSourceProvider
            _TempTeacherSourceProvider = new TempTeacherSourceProvider();
            _TempTeacherSourceProvider.SourceChanged += new EventHandler(TempTeacherSourceProvider_SourceChanged); 
            #endregion

            #region Dividers
            _ClassDivider = new ClassDivider();
            _ClassDivider.TempProvider = _TempTeacherSourceProvider;
            _ClassDivider.TargetTreeView = _TreeViewTeacher;

            _CategoryDivider = new CategoryDivider();
            _CategoryDivider.TempProvider = _TempTeacherSourceProvider;
            _CategoryDivider.TargetTreeView = _TreeViewTeacher; 
            #endregion

            #region _EppViewMode
            this._EppViewMode = new ItemSelector<ITeacherDivider>();
            this._EppViewMode.CanvasColor = System.Drawing.SystemColors.Control;
            this._EppViewMode.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this._EppViewMode.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._EppViewMode.Location = new System.Drawing.Point(0, 23);
            this._EppViewMode.Name = "_EppViewMode";
            this._EppViewMode.Size = new System.Drawing.Size(139, 85);
            this._EppViewMode.CollapseDirection = eCollapseDirection.TopToBottom;
            this._EppViewMode.Style.Alignment = System.Drawing.StringAlignment.Center;
            this._EppViewMode.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this._EppViewMode.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this._EppViewMode.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this._EppViewMode.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this._EppViewMode.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this._EppViewMode.Style.GradientAngle = 90;
            this._EppViewMode.TabIndex = 1;
            this._EppViewMode.TitleHeight = 23;
            this._EppViewMode.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this._EppViewMode.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this._EppViewMode.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this._EppViewMode.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this._EppViewMode.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this._EppViewMode.TitleStyle.CornerDiameter = 2;
            this._EppViewMode.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this._EppViewMode.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this._EppViewMode.TitleStyle.GradientAngle = 90;
            this._EppViewMode.TitleText = "檢視模式";
            this._EppViewMode.SelectionChanged += new EventHandler(_EppViewMode_SelectionChanged);
            this._EppViewMode.Items.Add(_CategoryDivider);        
            this._EppViewMode.Items.Add(_ClassDivider);

            #endregion

            #region NavPanel
            _NavPanel = new NavigationPanePanel();
            _NavPanel.ColorScheme.ItemDesignTimeBorder = System.Drawing.Color.Black;
            _NavPanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            _NavPanel.Controls.Add(this._EppTeacher);
            _NavPanel.Controls.Add(this._EppViewMode);
            _NavPanel.Controls.Add(this._BtnSearchAll);
            _NavPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            _NavPanel.Location = new System.Drawing.Point(1, 25);
            _NavPanel.Name = "NavPanel";
            _NavPanel.Size = new System.Drawing.Size(139, 456);
            _NavPanel.Style.Alignment = System.Drawing.StringAlignment.Center;
            _NavPanel.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            _NavPanel.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            _NavPanel.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            _NavPanel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            _NavPanel.Style.GradientAngle = 90;
            _NavPanel.TabIndex = 2;
            _NavPanel.Font = new System.Drawing.Font(FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            #endregion

            #region contentInfo
            contentInfo = new ContentInfo();
            contentInfo.ListPaneMenuStrip = listPaneMenuStrip;
            contentInfo.Dock = DockStyle.Fill;
            SmartSchool.Customization.PlugIn.ExtendedColumn.ExtendTeacherColumn.SetManager(contentInfo);
            #endregion

            _Initialized = true;           
        }

        void _EppViewMode_SelectionChanged(object sender, EventArgs e)
        {
            _ReFlashTree = true;
        }

        void _TreeViewTeacher_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                _TreeViewTeacher.SelectedNode = e.Node;
                contentInfo.SourceProvider = (ISourceProvider<BriefTeacherData, ISearchTeacher>)e.Node;
                contentInfo.SelectAllSource();
            }
        }

        void Instance_ClassUpdated(object sender, UpdateClassEventArgs e)
        {
            _ReFlashTree = true;
        }

        private void Instance_TeacherInserted(object sender, EventArgs e)
        {
            getBriefData();
        }

        private void Instance_TeacherDeleted(object sender, TeacherDeletedEventArgs e)
        {
            _TeacherList.Remove(e.ID);
            _ReFlashTree = true;
        }

        private void Instance_SelectionChanged(object sender, EventArgs e)
        {
            MotherForm.Instance.SetBarMessage("已選取" + this.SelectionTeachers.Count + "名教師");
        }

        private void Instance_TeacherDataChanged(object sender, TeacherDataChangedEventArgs e)
        {
            foreach (TeacherDataChangedContent var in e.Items)
            {
                _TeacherList[var.OldData.ID] = var.NewData;
            }
            _ReFlashTree = true;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //ClassRelated.Class.Instance.Reflash();
            Reflash(); 
        }

        public void Reflash()
        {
            if (_TreeViewTeacher == null) return;

            _ReFlashTree = true;
            setTreeViewWait();
            contentInfo.SourceProvider = null;

            //_TempTeacherSourceProvider = new TempTeacherSourceProvider();
            //_TempTeacherSourceProvider.SourceChanged += new EventHandler(TempTeacherSourceProvider_SourceChanged);

            getBriefData();
        }

        private void Instance_ClassDeleted(object sender, SmartSchool.ClassRelated.DeleteClassEventArgs e)
        {
            _ReFlashTree = true;
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            _AllTeacherSourceProvider.Source = new List<BriefTeacherData>(_TeacherList.Values);
            contentInfo.SourceProvider = _AllTeacherSourceProvider;
        }

        private void treeViewStudent_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!_IsTreeViewDividing)
            {
                if (_TreeViewTeacher.SelectedNode != null)
                    contentInfo.SourceProvider = (ISourceProvider<BriefTeacherData, ISearchTeacher>)_TreeViewTeacher.SelectedNode;
                else
                    btnSearchAll_Click(null, null);
            }
        }

        //同步選取資料及顯示資料
        private void Application_Idle(object sender, EventArgs e)
        {
            //要求重新整理且資料準備OK
            #region 重新整理資料
            if (_ReFlashTree && _TeacherList != null && _EppViewMode.SelectedItem!=null)
            {
                _TreeViewTeacher.SuspendLayout();

                _TreeViewTeacher.Nodes.Clear();

                //檢查待處理教師資料是否更新
                List<BriefTeacherData> newTempSource = new List<BriefTeacherData>();
                foreach (BriefTeacherData var in _TempTeacherSourceProvider.Source)
                {
                    if (_TeacherList.ContainsKey(var.ID))
                        newTempSource.Add(_TeacherList[var.ID]);
                }
                _TempTeacherSourceProvider.Source = newTempSource;
                _AllTeacherSourceProvider.Source = new List<BriefTeacherData>(_TeacherList.Values);

                //紀錄原本選取的點
                TreeNode selectNode = null;
                if (contentInfo.SourceProvider != null && !(contentInfo.SourceProvider is AllTeacherSourceProvider))
                {
                    selectNode = (TreeNode)contentInfo.SourceProvider;
                }

                #region 重新整理樹狀結構
                _IsTreeViewDividing = true;
                _EppViewMode.SelectedItem.Divide(_TeacherList);
                _IsTreeViewDividing = false;
                #endregion

                //還原選取的點
                if (selectNode != null)
                {
                    if (selectNode.TreeView == null)
                    {
                        _TreeViewTeacher.SelectedNode = null;
                        btnSearchAll_Click(null, null);
                    }
                    else
                        _TreeViewTeacher.SelectedNode = selectNode;
                }

                _ReFlashTree = false;
                if (contentInfo.SourceProvider == null)
                {
                    _AllTeacherSourceProvider.Source = new List<BriefTeacherData>(_TeacherList.Values);
                    contentInfo.SourceProvider = _AllTeacherSourceProvider;
                }
                _TreeViewTeacher.ResumeLayout();
                resetTreeViewWait();
            }
            #endregion
            //檢查選取資料是否變更
            #region 檢查選取資料是否變更

            List<BriefTeacherData> newList = contentInfo.SelectedList;
            if (newList.Count != _SelectionList.Count)
            {
                _SelectionList = newList;
                _SelectionList.TrimExcess();
                enumList = _SelectionList.GetEnumerator();
                if (SelectionChanged != null)
                    SelectionChanged.Invoke(this, new EventArgs());
            }
            else
            {
                enumList.Reset();
                foreach (BriefTeacherData var in newList)
                {
                    enumList.MoveNext();
                    if (enumList.Current != var)
                    {
                        _SelectionList = newList;
                        _SelectionList.TrimExcess();
                        enumList = _SelectionList.GetEnumerator();
                        if (SelectionChanged != null)
                            SelectionChanged.Invoke(this, new EventArgs());
                        break;
                    }
                }
            }
            //if (contentInfo.SelectedList.Count != _SelectionList.Count)
            //{
            //    _SelectionList = contentInfo.SelectedList;
            //    if (SelectionChanged != null)
            //        SelectionChanged.Invoke(this, new EventArgs());
            //}
            //else
            //{
            //    foreach (BriefTeacherData var in contentInfo.SelectedList)
            //    {
            //        if (!_SelectionList.Contains(var))
            //        {
            //            _SelectionList = contentInfo.SelectedList;
            //            if (SelectionChanged != null)
            //                SelectionChanged.Invoke(this, new EventArgs());
            //            break;
            //        }
            //    }
            //}
            #endregion
            //變更清單右鍵選項
            #region 變更清單右鍵選項
            if (contentInfo.SourceProvider == _TempTeacherSourceProvider)
            {
                toolStripMenuItemAddToTemp.Visible = false;
                toolStripMenuItemRemoveFromTemp.Visible = true;
            }
            else
            {
                toolStripMenuItemAddToTemp.Visible = true;
                toolStripMenuItemRemoveFromTemp.Visible = false;
            }
            toolStripMenuItemAddToTemp.Enabled = toolStripMenuItemRemoveFromTemp.Enabled = (contentInfo.SelectedList.Count > 0);
            #endregion            
        }

        internal TeacherCollection Items
        {
            get
            {
                _LoadTeacher.WaitOne();
                return new TeacherCollection(_TeacherList);
            }
        }

        // 取得教師資料
        private void getBriefData()
        {
            _LoadTeacher.WaitOne();
            if (_BkwBriefDataLoader == null)
            {
                _BkwBriefDataLoader = new BackgroundWorker();
                _BkwBriefDataLoader.DoWork += new DoWorkEventHandler(_bkwBriefDataLoader_DoWork);
                _BkwBriefDataLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bkwBriefDataLoader_RunWorkerCompleted);
            }
            _BkwBriefDataLoader.RunWorkerAsync();
        }
        private void _bkwBriefDataLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _ReFlashTree = true;
        }
        private void _bkwBriefDataLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _LoadTeacher.Reset();
            _TeacherList = new Dictionary<string, BriefTeacherData>();
                DSResponse dsrsp = SmartSchool.Feature.Teacher.QueryTeacher.GetTeacherDetailTest();
                foreach (XmlElement var in dsrsp.GetContent().GetElements("Teacher"))
                {
                    BriefTeacherData data;
                    string id = var.SelectSingleNode("@ID").InnerText;
                    if (_TeacherList.ContainsKey(id))
                    {
                        data = _TeacherList[id];
                    }
                    else
                    {
                        data = new BriefTeacherData(var);
                        _TeacherList.Add(data.ID, data);
                    }
                }
            }
            catch { }
            finally
            {
                _LoadTeacher.Set();
            }
        }


        /// <summary>
        /// 設定轉圈圈位置置中
        /// </summary>
        private void eppInSchoolStudent_SizeChanged(object sender, EventArgs e)
        {
            int x = (_EppTeacher.Width - _WaitingPicture.Width) / 2;
            int y = (_EppTeacher.Height - _WaitingPicture.Height) / 3;
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            _WaitingPicture.Location = new Point(x, y);
        }

        /// <summary>
        /// 當在校學生列縮起時同時隱藏圈圈圖
        /// </summary>
        private void eppInSchoolStudent_ExpandedChanged(object sender, ExpandedChangeEventArgs e)
        {
            _NavPanel.SuspendLayout();
            _WaitingPicture.Visible = (_treeViewWait) & _EppTeacher.Expanded;
            _EppViewMode.Dock = (_EppTeacher.Expanded ? DockStyle.Bottom : DockStyle.Top);
            _EppTeacher.Dock = (_EppTeacher.Expanded ? DockStyle.Fill : DockStyle.Top);
            _NavPanel.Controls.SetChildIndex(_EppViewMode, (_EppTeacher.Expanded ? 2 : 0));
            _NavPanel.ResumeLayout();
        }

        /// <summary>
        /// 設定顯示執行中圖示
        /// </summary>
        private void setTreeViewWait()
        {
            _treeViewWait = true;
            if (_WaitingPicture != null)
                _WaitingPicture.Visible = (_treeViewWait) & _EppTeacher.Expanded;
        }

        /// <summary>
        /// 設定隱藏執行中圖示
        /// </summary>
        private void resetTreeViewWait()
        {
            _treeViewWait = false;
            if (_WaitingPicture != null)
                _WaitingPicture.Visible = (_treeViewWait) & _EppTeacher.Expanded;
        }

        #region 教師相關事件
        public event EventHandler TemporalChanged;
        private void TempTeacherSourceProvider_SourceChanged(object sender, EventArgs e)
        {
            if (TemporalChanged != null)
                TemporalChanged(this, EventArgs.Empty);
        }

        public event EventHandler SelectionChanged;
        public List<BriefTeacherData> SelectionTeachers
        {
            get
            {
                if (contentInfo != null)
                {
                    if (contentInfo.SelectedList.Count != _SelectionList.Count)
                    {
                        _SelectionList = contentInfo.SelectedList;
                        if (SelectionChanged != null)
                            SelectionChanged.Invoke(this, new EventArgs());
                    }
                    else
                    {
                        foreach (BriefTeacherData var in contentInfo.SelectedList)
                        {
                            if (!_SelectionList.Contains(var))
                            {
                                _SelectionList = contentInfo.SelectedList;
                                if (SelectionChanged != null)
                                    SelectionChanged.Invoke(this, new EventArgs());
                                break;
                            }
                        }
                    }
                }
                return _SelectionList;
            }
        }

        public event EventHandler TeacherInserted;
        public void InvokTeacherInserted(EventArgs e)
        {
            if (TeacherInserted != null)
            {
                TeacherInserted.Invoke(this, e);
            }
        }

        public event EventHandler<TeacherDeletedEventArgs> TeacherDeleted;
        public void InvokTeacherDeleted(TeacherDeletedEventArgs e)
        {
            if (TeacherDeleted != null)
                TeacherDeleted.Invoke(this, e);
        }

        public event EventHandler<TeacherDataChangedEventArgs> TeacherDataChanged;
        public void InvokTeacherDataChanged(params string[] teacherIDList)
        {
            if ( teacherIDList.Length == 0 ) return;
            TeacherDataChangedEventArgs e = new TeacherDataChangedEventArgs();
            Dictionary<string, BriefTeacherData> _ChangedData = new Dictionary<string, BriefTeacherData>();
            //取得SERVER上最新資料
            //XmlElement[] elements = QueryTeacher.GetTeacherListWithSupervisedByClassInfo(teacherIDList).GetContent().GetElements("Teacher");
            XmlElement[] elements = QueryTeacher.GetTeacherDetailTest().GetContent().GetElements("Teacher");
            foreach (XmlElement ele in elements)
            {
                BriefTeacherData newData;
                string id = ele.SelectSingleNode("@ID").InnerText;
                if (_ChangedData.ContainsKey(id))
                {
                    newData = _TeacherList[id];
                }
                else
                {
                    newData = new BriefTeacherData(ele);
                    _ChangedData.Add(id, newData);
                }

                //if (ele.SelectSingleNode("SupervisedByClassID").InnerText != "")
                //{
                //    newData.SupervisedByClassInfo.Add(new SupervisedByClassInfo(ele));
                //}
            }
            foreach (BriefTeacherData newData in _ChangedData.Values)
            {
                if (_TeacherList.ContainsKey(newData.ID))
                {
                    e.Items.Add(new TeacherDataChangedContent(_TeacherList[newData.ID], newData));
                }
            }
            if (TeacherDataChanged != null)
                TeacherDataChanged.Invoke(this, e);
        }
        #endregion

        #region IEntity 成員

        public string Title
        {
            get { return "教師"; }
        }

        public NavigationPanePanel NavPanPanel
        {
            get
            {
                if (!_Initialized)
                {
                    Init();
                }
                return _NavPanel;
            }
        }

        public Panel ContentPanel
        {
            get
            {
                if (!_Initialized)
                {
                    Init();
                }
                return contentInfo.panelContent;
            }
        }

        public Image Picture
        {
            get
            {
                return Resources.Navigation_Teacher_New;
            }
        }

        public void Actived()
        {
            contentInfo.LoadPreference();
        }

        #endregion

        private void toolStripMenuItemRemoveFromTemp_Click(object sender, EventArgs e)
        {
            foreach (BriefTeacherData var in contentInfo.SelectedList)
            {
                if (_TempTeacherSourceProvider.Source.Contains(var))
                    _TempTeacherSourceProvider.Source.Remove(var);
            }
            _TempTeacherSourceProvider.Source = _TempTeacherSourceProvider.Source;
        }

        private void toolStripMenuItemAddToTemp_Click(object sender, EventArgs e)
        {
            List<BriefTeacherData> insertList = new List<BriefTeacherData>();
            foreach (BriefTeacherData var in contentInfo.SelectedList)
            {
                if (!_TempTeacherSourceProvider.Source.Contains(var))
                    insertList.Add(var);
            }
            _TempTeacherSourceProvider.Source.AddRange(insertList);
            _TempTeacherSourceProvider.Source = _TempTeacherSourceProvider.Source;
        }

        public List<BriefTeacherData> TemporaTeacher
        {
            get { return _TempTeacherSourceProvider==null?new List<BriefTeacherData>():_TempTeacherSourceProvider.Source; }
        }
    }

    public class TeacherDataChangedEventArgs : EventArgs
    {
        private List<TeacherDataChangedContent> _Items = new List<TeacherDataChangedContent>();
        public List<TeacherDataChangedContent> Items { get { return _Items; } }
    }

    public class TeacherDataChangedContent
    {
        private readonly BriefTeacherData _OldData;
        private readonly BriefTeacherData _NewData;
        public BriefTeacherData OldData { get { return _OldData; } }
        public BriefTeacherData NewData { get { return _NewData; } }
        public TeacherDataChangedContent(BriefTeacherData oldData, BriefTeacherData newData)
        {
            _OldData = oldData;
            _NewData = newData;
        }
    }

    public class TeacherDeletedEventArgs : EventArgs
    {
        string _id;
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
