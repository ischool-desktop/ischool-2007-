//using System;
//using System.Collections.Generic;
//using System.Text;
//using DevComponents.DotNetBar;
//using System.Windows.Forms;
//using SmartSchool.Common;
//using System.Drawing;

//namespace SmartSchool.CourseRelated
//{
//    class Course : IEntity
//    {
//        private Course() { }
//        static private Course _Instance;
//        static public Course Instance
//        {
//            get
//            {
//                if ( _Instance == null )
//                    _Instance = new Course();
//                return _Instance;
//            }
//        }

//        #region IEntity 成員
//        private bool _Actived = false;
//        private bool _Initialized = false;
//        private NavigationPanePanel NavPanel;
//        private DevComponents.DotNetBar.ButtonX btnSearchAll;
//        private DevComponents.DotNetBar.ExpandablePanel eppInSchoolStudent;
//        private DragDropTreeView _TreeViewStudent;
//        private System.Windows.Forms.PictureBox pictureBox1;
//        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
//        private DevComponents.DotNetBar.ItemContainer itemContainer1;
//        private DevComponents.DotNetBar.ContextMenuBar contextMenuBar1;
//        private DevComponents.DotNetBar.ButtonItem buttonItem1;
//        private ContextMenuStrip listPaneMenuStrip;
//        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
//        private DevComponents.DotNetBar.ButtonItem toolStripMenuItemAddToTemp;
//        private DevComponents.DotNetBar.ButtonItem toolStripMenuItemRemoveFromTemp;
//        private TreeNode _SelectionNode;

//        private ItemSelector<ICourseDivider> _EppViewMode;

//        public string Title
//        {
//            get { return "課程"; }
//        }

//        public DevComponents.DotNetBar.NavigationPanePanel NavPanPanel
//        {
//            get
//            {
//                if ( !_Initialized )
//                {
//                    Init();
//                } return NavPanel;
//            }
//        }

//        public System.Windows.Forms.Panel ContentPanel
//        {
//            get
//            {
//                if ( !_Initialized )
//                {
//                    Init();
//                }
//                return new Panel();
//            }
//        }

//        public System.Drawing.Image Picture
//        {
//            get { return Properties.Resources.Navigation_Course_New; }
//        }

//        public void Actived()
//        {
//            if ( !_Actived )
//            {
//                //contentInfo.LoadPreference();
//                _Actived = true;
//            }
//        }
//        private void Init()
//        {
//            setTreeViewWait();
//            Application.Idle += new EventHandler(Application_Idle);


//            #region btnSearchAll
//            btnSearchAll = new ButtonX();
//            this.btnSearchAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
//            this.btnSearchAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
//            this.btnSearchAll.Dock = System.Windows.Forms.DockStyle.Top;
//            this.btnSearchAll.Location = new System.Drawing.Point(0, 0);
//            this.btnSearchAll.Name = "btnSearchAll";
//            this.btnSearchAll.Size = new System.Drawing.Size(139, 23);
//            this.btnSearchAll.SubItemsExpandWidth = 17;
//            this.btnSearchAll.TabIndex = 0;
//            this.btnSearchAll.Text = "搜尋所有課程";
//            this.btnSearchAll.Click += new EventHandler(btnSearchAll_Click);
//            #endregion

//            #region toolStripMenuItemAddToTemp
//            this.toolStripMenuItemAddToTemp = new ButtonItem();
//            this.toolStripMenuItemAddToTemp.Name = "toolStripMenuItemAddToTemp";
//            this.toolStripMenuItemAddToTemp.Size = new System.Drawing.Size(93, 22);
//            this.toolStripMenuItemAddToTemp.Text = "加入至待處理";
//            this.toolStripMenuItemAddToTemp.Image = new Bitmap(20, 20);
//            this.toolStripMenuItemAddToTemp.ImagePosition = eImagePosition.Left;
//            this.toolStripMenuItemAddToTemp.ButtonStyle = eButtonStyle.ImageAndText;
//            this.toolStripMenuItemAddToTemp.Click += new EventHandler(toolStripMenuItemAddToTemp_Click);
//            #endregion

//            #region toolStripMenuItemRemoveFromTemp
//            this.toolStripMenuItemRemoveFromTemp = new ButtonItem();
//            this.toolStripMenuItemRemoveFromTemp.Name = "toolStripMenuItemRemoveFromTemp";
//            this.toolStripMenuItemRemoveFromTemp.Size = new System.Drawing.Size(93, 22);
//            this.toolStripMenuItemRemoveFromTemp.Text = "移出待處理";
//            this.toolStripMenuItemAddToTemp.Image = new Bitmap(20, 20);
//            this.toolStripMenuItemAddToTemp.ImagePosition = eImagePosition.Left;
//            this.toolStripMenuItemAddToTemp.ButtonStyle = eButtonStyle.ImageAndText;
//            this.toolStripMenuItemRemoveFromTemp.Click += new EventHandler(toolStripMenuItemRemoveFromTemp_Click);
//            #endregion

//            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
//            this.itemContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
//            this.itemContainer1.MinimumSize = new System.Drawing.Size(0, 0);
//            this.itemContainer1.Name = "itemContainer1";
//            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
//                this.toolStripMenuItemAddToTemp,
//            this.toolStripMenuItemRemoveFromTemp
//            });

//            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
//            this.buttonItem1.AutoExpandOnClick = true;
//            this.buttonItem1.ImagePaddingHorizontal = 8;
//            this.buttonItem1.Name = "buttonItem1";
//            this.buttonItem1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
//            this.itemContainer1});
//            this.buttonItem1.Tag = itemContainer1;
//            this.buttonItem1.Text = "buttonItem1";

//            this.contextMenuBar1 = new DevComponents.DotNetBar.ContextMenuBar();
//            this.contextMenuBar1.CloseSingleTab = true;
//            this.contextMenuBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
//            this.buttonItem1});
//            this.contextMenuBar1.Location = new System.Drawing.Point(89, 496);
//            this.contextMenuBar1.Name = "contextMenuBar1";
//            this.contextMenuBar1.Size = new System.Drawing.Size(141, 51);
//            this.contextMenuBar1.Stretch = true;
//            this.contextMenuBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
//            this.contextMenuBar1.TabIndex = 7;
//            this.contextMenuBar1.TabStop = false;
//            this.contextMenuBar1.Text = "contextMenuBar1";
//            this.contextMenuBar1.PopupOpen += new DotNetBarManager.PopupOpenEventHandler(contextMenuBar1_PopupOpen);

//            #region toolStripMenuItem1
//            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
//            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
//            this.toolStripMenuItem1.Size = new System.Drawing.Size(93, 22);
//            this.toolStripMenuItem1.Text = "重新整理";
//            this.toolStripMenuItem1.Click += new EventHandler(toolStripMenuItem1_Click);
//            #endregion

//            #region contextMenuStrip1
//            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
//            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
//            this.toolStripMenuItem1});
//            this.contextMenuStrip1.Name = "contextMenuStrip1";
//            this.contextMenuStrip1.ShowImageMargin = false;
//            this.contextMenuStrip1.Size = new System.Drawing.Size(94, 26);
//            #endregion

//            #region listPaneMenuStrip
//            this.listPaneMenuStrip = new System.Windows.Forms.ContextMenuStrip();
//            this.listPaneMenuStrip.Name = "listPaneMenuStrip";
//            this.listPaneMenuStrip.ShowImageMargin = false;
//            this.listPaneMenuStrip.Size = new System.Drawing.Size(94, 26);
//            this.listPaneMenuStrip.Opening += new CancelEventHandler(listPaneMenuStrip_Opening);
//            #endregion

//            #region _TreeViewStudent
//            this._TreeViewStudent = new DragDropTreeView();
//            this._TreeViewStudent.BackColor = System.Drawing.Color.White;
//            this._TreeViewStudent.ContextMenuStrip = this.contextMenuStrip1;
//            this._TreeViewStudent.Cursor = System.Windows.Forms.Cursors.Default;
//            this._TreeViewStudent.Dock = System.Windows.Forms.DockStyle.Fill;
//            this._TreeViewStudent.ForeColor = System.Drawing.Color.Black;
//            this._TreeViewStudent.HotTracking = true;
//            this._TreeViewStudent.ItemHeight = 20;
//            this._TreeViewStudent.Location = new System.Drawing.Point(0, 23);
//            this._TreeViewStudent.Name = "treeViewStudent";
//            this._TreeViewStudent.Size = new System.Drawing.Size(139, 410);
//            this._TreeViewStudent.TabIndex = 1;
//            this._TreeViewStudent.NodeMouseClick += new TreeNodeMouseClickEventHandler(_TreeViewStudent_NodeMouseClick);
//            #endregion

//            #region pictureBox1
//            this.pictureBox1 = new System.Windows.Forms.PictureBox();
//            this.pictureBox1.BackColor = System.Drawing.Color.White;
//            this.pictureBox1.Image = global::SmartSchool.Properties.Resources.loading5;
//            this.pictureBox1.Location = new System.Drawing.Point(52, 56);
//            this.pictureBox1.Name = "pictureBox1";
//            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
//            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
//            this.pictureBox1.TabIndex = 3;
//            this.pictureBox1.TabStop = false;
//            #endregion

//            #region eppInSchoolStudent
//            this.eppInSchoolStudent = new DevComponents.DotNetBar.ExpandablePanel();
//            this.eppInSchoolStudent.CanvasColor = System.Drawing.SystemColors.Control;
//            this.eppInSchoolStudent.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
//            this.eppInSchoolStudent.Controls.Add(this.pictureBox1);
//            this.eppInSchoolStudent.Controls.Add(this._TreeViewStudent);
//            this.eppInSchoolStudent.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.eppInSchoolStudent.Location = new System.Drawing.Point(0, 23);
//            this.eppInSchoolStudent.Name = "eppInSchoolStudent";
//            this.eppInSchoolStudent.Size = new System.Drawing.Size(139, 433);
//            this.eppInSchoolStudent.Style.Alignment = System.Drawing.StringAlignment.Center;
//            this.eppInSchoolStudent.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
//            this.eppInSchoolStudent.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
//            this.eppInSchoolStudent.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
//            this.eppInSchoolStudent.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
//            this.eppInSchoolStudent.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
//            this.eppInSchoolStudent.Style.GradientAngle = 90;
//            this.eppInSchoolStudent.TabIndex = 1;
//            this.eppInSchoolStudent.TitleHeight = 23;
//            this.eppInSchoolStudent.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
//            this.eppInSchoolStudent.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
//            this.eppInSchoolStudent.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
//            this.eppInSchoolStudent.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
//            this.eppInSchoolStudent.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
//            this.eppInSchoolStudent.TitleStyle.CornerDiameter = 2;
//            this.eppInSchoolStudent.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
//            this.eppInSchoolStudent.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
//            this.eppInSchoolStudent.TitleStyle.GradientAngle = 90;
//            this.eppInSchoolStudent.TitleText = "檢視課程";
//            this.eppInSchoolStudent.ExpandedChanged += new DevComponents.DotNetBar.ExpandChangeEventHandler(this.eppInSchoolStudent_ExpandedChanged);
//            this.eppInSchoolStudent.SizeChanged += new System.EventHandler(this.eppInSchoolStudent_SizeChanged);
//            #endregion

//            _TempStudentSourceProvider = new TempStudentSourceProvider();
//            _TempStudentSourceProvider.SourceChanged += new EventHandler(TempStudentSourceProvider_SourceChanged);

//            //_ClassDivider = new ClassDivider();
//            //_ClassDivider.TempProvider = _TempStudentSourceProvider;
//            //_ClassDivider.TargetTreeView = _TreeViewStudent;

//            //_CategoryDivider = new CategoryDivider();
//            //_CategoryDivider.TempProvider = _TempStudentSourceProvider;
//            //_CategoryDivider.TargetTreeView = _TreeViewStudent;

//            //_AttendanceDivider = new AttendanceDivider();
//            //_AttendanceDivider.TempProvider = _TempStudentSourceProvider;
//            //_AttendanceDivider.TargetTreeView = _TreeViewStudent;

//            //_DisciplineDivider = new DisciplineDivider();
//            //_DisciplineDivider.TempProvider = _TempStudentSourceProvider;
//            //_DisciplineDivider.TargetTreeView = _TreeViewStudent;
//            #region _EppViewMode
//            this._EppViewMode = new ItemSelector<IStudentDivider>();
//            this._EppViewMode.CanvasColor = System.Drawing.SystemColors.Control;
//            this._EppViewMode.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
//            this._EppViewMode.Dock = System.Windows.Forms.DockStyle.Bottom;
//            this._EppViewMode.Location = new System.Drawing.Point(0, 23);
//            this._EppViewMode.Name = "_EppViewMode";
//            this._EppViewMode.Size = new System.Drawing.Size(139, 85);
//            this._EppViewMode.CollapseDirection = eCollapseDirection.TopToBottom;
//            this._EppViewMode.Style.Alignment = System.Drawing.StringAlignment.Center;
//            this._EppViewMode.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
//            this._EppViewMode.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
//            this._EppViewMode.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
//            this._EppViewMode.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
//            this._EppViewMode.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
//            this._EppViewMode.Style.GradientAngle = 90;
//            this._EppViewMode.TabIndex = 1;
//            this._EppViewMode.TitleHeight = 23;
//            this._EppViewMode.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
//            this._EppViewMode.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
//            this._EppViewMode.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
//            this._EppViewMode.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
//            this._EppViewMode.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
//            this._EppViewMode.TitleStyle.CornerDiameter = 2;
//            this._EppViewMode.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
//            this._EppViewMode.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
//            this._EppViewMode.TitleStyle.GradientAngle = 90;
//            this._EppViewMode.TitleText = "檢視模式";
//            this._EppViewMode.SelectionChanged += new EventHandler(_EppViewMode_SelectionChanged);
//            this._EppViewMode.Items.Add(_ClassDivider);
//            this._EppViewMode.Items.Add(_CategoryDivider);
//            this._EppViewMode.Items.Add(_DisciplineDivider);
//            this._EppViewMode.Items.Add(_AttendanceDivider);

//            #endregion

//            #region NavPanel
//            NavPanel = new NavigationPanePanel();
//            NavPanel.ColorScheme.ItemDesignTimeBorder = System.Drawing.Color.Black;
//            NavPanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
//            NavPanel.Controls.Add(this.eppInSchoolStudent);
//            NavPanel.Controls.Add(this._EppViewMode);
//            NavPanel.Controls.Add(this.btnSearchAll);
//            NavPanel.Dock = System.Windows.Forms.DockStyle.Fill;
//            NavPanel.Location = new System.Drawing.Point(1, 25);
//            NavPanel.Name = "NavPanel";
//            NavPanel.Size = new System.Drawing.Size(139, 456);
//            NavPanel.Style.Alignment = System.Drawing.StringAlignment.Center;
//            NavPanel.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
//            NavPanel.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
//            NavPanel.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
//            NavPanel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
//            NavPanel.Style.GradientAngle = 90;
//            NavPanel.TabIndex = 2;
//            NavPanel.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ));
//            #endregion

//            #region contentInfo
//            //contentInfo = new ContentInfo();
//            //contentInfo.ListPaneMenuStrip = listPaneMenuStrip;
//            //contentInfo.Dock = DockStyle.Fill;
//            //contentInfo.SourceProvider = _SearchAllStudentSourceProvider;
//            //SmartSchool.Customization.PlugIn.ExtendedColumn.ExtendStudentColumn.SetManager(contentInfo);
//            #endregion

//            _Initialized = true;
//            //SmartSchool.Customization.PlugIn.ContextMenu.CourseMenuButton.SetManager(this);
//        }

//        //同步選取資料及顯示資料
//        private void Application_Idle(object sender, EventArgs e)
//        {
//            ////要求重新整理且資料準備OK
//            //#region 重新整理資料
//            //if ( _ReFlashTree && _StudentList != null && _EppViewMode.SelectedItem != null )
//            //{
//            //    _SearchAllStudentSourceProvider.Source = _SearchAllStudentSourceProvider.Source;

//            //    _LoadStudent.WaitOne();

//            //    _TreeViewStudent.SuspendLayout();
//            //    _TreeViewStudent.Nodes.Clear();

//            //    //檢查待處理學生資料是否更新
//            //    List<BriefStudentData> newTempSource = new List<BriefStudentData>();
//            //    foreach ( BriefStudentData var in _TempStudentSourceProvider.Source )
//            //    {
//            //        if ( _StudentList.ContainsKey(var.ID) )
//            //            newTempSource.Add(_StudentList[var.ID]);
//            //    }
//            //    _TempStudentSourceProvider.Source = newTempSource;
//            //    //重新分割節點
//            //    _EppViewMode.SelectedItem.Divide(_StudentList);

//            //    #region 保留原始方法
//            //    //#region 建立Source
//            //    //List<BriefStudentData> _NormalStudentSource = new List<BriefStudentData>();
//            //    //List<BriefStudentData> _NonGradeStudentSource = new List<BriefStudentData>();
//            //    //Dictionary<string, List<BriefStudentData>> _GradeStudentSource = new Dictionary<string, List<BriefStudentData>>();
//            //    //Dictionary<string, List<BriefStudentData>> _ClassStudentSource = new Dictionary<string, List<BriefStudentData>>();
//            //    //Dictionary<string, List<BriefStudentData>> _NonClassStudentSource = new Dictionary<string, List<BriefStudentData>>();
//            //    //List<BriefStudentData> _OnLeaveStudentSource = new List<BriefStudentData>();
//            //    //List<BriefStudentData> _ExtendingStudentSource = new List<BriefStudentData>();
//            //    //List<BriefStudentData> _DeletedStudentSource = new List<BriefStudentData>();
//            //    //#endregion
//            //    //#region 建立SourceProvider
//            //    //_NormalStudentSourceProvider = (_NormalStudentSourceProvider != null ? _NormalStudentSourceProvider : new NormalStudentSourceProvider());
//            //    //_NonGradeStudentSourceProvider = (_NonGradeStudentSourceProvider != null ? _NonGradeStudentSourceProvider : new NonGradeStudentSourceProvider());
//            //    //_TempStudentSourceProvider = (_TempStudentSourceProvider != null ? _TempStudentSourceProvider : new TempStudentSourceProvider());
//            //    //_OnLeaveStudentSourceProvider = (_OnLeaveStudentSourceProvider != null ? _OnLeaveStudentSourceProvider : new OnLeaveStudentSourceProvider());
//            //    //_ExtendingStudentSourceProvider = (_ExtendingStudentSourceProvider != null ? _ExtendingStudentSourceProvider : new ExtendingStudentSourceProvider());
//            //    //_DeletedStudentSourceProvider = (_DeletedStudentSourceProvider != null ? _DeletedStudentSourceProvider : new DeletedStudentSourceProvider());
//            //    //_GraduatedStudentSourceProvider = (_GraduatedStudentSourceProvider != null ? _GraduatedStudentSourceProvider : new GraduatedStudentSourceProvider());

//            //    //Dictionary<string, GradeStudentSourceProvider> _OldGradeStudentSourceProvider = (_GradeStudentSourceProvider != null ? _GradeStudentSourceProvider : new Dictionary<string, GradeStudentSourceProvider>());
//            //    //Dictionary<string, ClassStudentSourceProvider> _OldClassStudentSourceProvider = (_ClassStudentSourceProvider != null ? _ClassStudentSourceProvider : new Dictionary<string, ClassStudentSourceProvider>());
//            //    //Dictionary<string, NonClassStudentSourceProvider> _OldGradeNonClassStudentSourceProvider = (_GradeNonClassStudentSourceProvider != null ? _GradeNonClassStudentSourceProvider : new Dictionary<string, NonClassStudentSourceProvider>());

//            //    //_GradeStudentSourceProvider = new Dictionary<string, GradeStudentSourceProvider>();
//            //    //_ClassStudentSourceProvider = new Dictionary<string, ClassStudentSourceProvider>();
//            //    //_GradeNonClassStudentSourceProvider = new Dictionary<string, NonClassStudentSourceProvider>();

//            //    //#region 巡迴班及建立年級班級節點
//            //    //foreach (XmlElement var in _ClassResponse.GetContent().GetElements("Class"))
//            //    //{
//            //    //    //年級
//            //    //    string gradeYear = var.SelectSingleNode("GradeYear").InnerText;
//            //    //    if (gradeYear != "" && !_GradeStudentSourceProvider.ContainsKey(gradeYear))
//            //    //    {
//            //    //        GradeStudentSourceProvider newGradeNode;
//            //    //        if (!_OldGradeStudentSourceProvider.ContainsKey(gradeYear))
//            //    //        {
//            //    //            newGradeNode = new GradeStudentSourceProvider();
//            //    //            newGradeNode.Grade = gradeYear;
//            //    //        }
//            //    //        else
//            //    //        {
//            //    //            newGradeNode = _OldGradeStudentSourceProvider[gradeYear];
//            //    //            _OldGradeStudentSourceProvider.Remove(gradeYear);
//            //    //        }
//            //    //        _GradeStudentSourceProvider.Add(gradeYear, newGradeNode);
//            //    //    }
//            //    //    //班級
//            //    //    string className = var.SelectSingleNode("ClassName").InnerText;
//            //    //    string classID = var.SelectSingleNode("@ID").InnerText;
//            //    //    ClassStudentSourceProvider classStudentSourceProvider;
//            //    //    if (!_OldClassStudentSourceProvider.ContainsKey(classID))
//            //    //    {
//            //    //        classStudentSourceProvider = new ClassStudentSourceProvider();
//            //    //        classStudentSourceProvider.ClassName = className;
//            //    //        classStudentSourceProvider.Grade = gradeYear;
//            //    //        classStudentSourceProvider.ClassID = classID;
//            //    //    }
//            //    //    else
//            //    //    {
//            //    //        classStudentSourceProvider = _OldClassStudentSourceProvider[classID];
//            //    //        _OldClassStudentSourceProvider.Remove(classID);
//            //    //    }
//            //    //    _ClassStudentSourceProvider.Add(classID, classStudentSourceProvider);
//            //    //}
//            //    //#endregion
//            //    //#endregion
//            //    //#region 將資料填入Source
//            //    //foreach (BriefStudentData var in _StudentList.Values)
//            //    //{
//            //    //    #region 在校學生
//            //    //    if (var.Status == "一般" || var.Status == "延修")
//            //    //    {
//            //    //        //加入至在校學生
//            //    //        _NormalStudentSource.Add(var);
//            //    //        //如果是未分年級
//            //    //        if (var.GradeYear == "")
//            //    //        {
//            //    //            //填入未分年級
//            //    //            _NonGradeStudentSource.Add(var);
//            //    //        }
//            //    //        else
//            //    //        {
//            //    //            //有年級
//            //    //            if (!_GradeStudentSource.ContainsKey(var.GradeYear))
//            //    //            {
//            //    //                //如果年級不存在新增該年級
//            //    //                _GradeStudentSource.Add(var.GradeYear, new List<BriefStudentData>());
//            //    //            }
//            //    //            _GradeStudentSource[var.GradeYear].Add(var);
//            //    //        }
//            //    //        //如果是未分班
//            //    //        if (var.RefClassID == "")
//            //    //        {
//            //    //            if (!_NonClassStudentSource.ContainsKey(var.GradeYear))
//            //    //            {
//            //    //                //如果未分班的年級不存在則新增
//            //    //                _NonClassStudentSource.Add(var.GradeYear, new List<BriefStudentData>());
//            //    //            }
//            //    //            _NonClassStudentSource[var.GradeYear].Add(var);
//            //    //        }
//            //    //        else
//            //    //        {
//            //    //            //有班級
//            //    //            if (!_ClassStudentSource.ContainsKey(var.RefClassID))
//            //    //            {
//            //    //                _ClassStudentSource.Add(var.RefClassID, new List<BriefStudentData>());
//            //    //            }
//            //    //            _ClassStudentSource[var.RefClassID].Add(var);
//            //    //        }
//            //    //    }
//            //    //    #endregion
//            //    //    //休學學生
//            //    //    if (var.Status == "休學")
//            //    //    {
//            //    //        _OnLeaveStudentSource.Add(var);
//            //    //    }
//            //    //    //延修學生
//            //    //    if (var.Status == "延修")
//            //    //    {
//            //    //        _ExtendingStudentSource.Add(var);
//            //    //    }
//            //    //    //刪除學生
//            //    //    if (var.Status == "刪除")
//            //    //    {
//            //    //        _DeletedStudentSource.Add(var);
//            //    //    }
//            //    //}
//            //    //#endregion
//            //    //#region 將Source指派給SourceProvider
//            //    ////在校學生
//            //    //_NormalStudentSourceProvider.Source = _NormalStudentSource;
//            //    ////休學學生
//            //    //_OnLeaveStudentSourceProvider.Source = _OnLeaveStudentSource;
//            //    ////延休學生
//            //    //_ExtendingStudentSourceProvider.Source = _ExtendingStudentSource;
//            //    ////刪除學生
//            //    //_DeletedStudentSourceProvider.Source = _DeletedStudentSource;
//            //    //#region 每個年級
//            //    //foreach (string grade in _GradeStudentSource.Keys)
//            //    //{
//            //    //    #region 假如是沒有班級的年級則要再增加此年級的SourceProvider
//            //    //    if (!_GradeStudentSourceProvider.ContainsKey(grade))
//            //    //    {
//            //    //        GradeStudentSourceProvider newGradeStudentSourceProvider;
//            //    //        if (!_OldGradeStudentSourceProvider.ContainsKey(grade))
//            //    //        {
//            //    //            //如果SourceProvider真的不存在
//            //    //            newGradeStudentSourceProvider = new GradeStudentSourceProvider();
//            //    //            newGradeStudentSourceProvider.Grade = grade;
//            //    //            _OldGradeStudentSourceProvider.Remove(grade);
//            //    //        }
//            //    //        else
//            //    //        {
//            //    //            //抓以前的來用
//            //    //            newGradeStudentSourceProvider = _OldGradeStudentSourceProvider[grade];
//            //    //        }
//            //    //        _GradeStudentSourceProvider.Add(grade, newGradeStudentSourceProvider);
//            //    //    }
//            //    //    #endregion
//            //    //    _GradeStudentSourceProvider[grade].Source = _GradeStudentSource[grade];
//            //    //}
//            //    //foreach (string grade in _GradeStudentSourceProvider.Keys)
//            //    //{
//            //    //    #region 尋找已存在但沒有資料的節點，將資料清空
//            //    //    if (!_GradeStudentSource.ContainsKey(grade))
//            //    //        _GradeStudentSourceProvider[grade].Source = new List<BriefStudentData>();
//            //    //    #endregion
//            //    //}
//            //    //#endregion
//            //    //#region 每個班級
//            //    //foreach (string classid in _ClassStudentSource.Keys)
//            //    //{
//            //    //    _ClassStudentSourceProvider[classid].Source = _ClassStudentSource[classid];
//            //    //}
//            //    //foreach (string classid in _ClassStudentSourceProvider.Keys)
//            //    //{
//            //    //    #region 尋找已存在但沒有資料的節點，將資料清空
//            //    //    if (!_ClassStudentSource.ContainsKey(classid))
//            //    //        _ClassStudentSourceProvider[classid].Source = new List<BriefStudentData>();
//            //    //    #endregion
//            //    //}
//            //    //#endregion
//            //    //#region 沒有班級學生
//            //    //foreach (string grade in _NonClassStudentSource.Keys)
//            //    //{
//            //    //    if (!_GradeNonClassStudentSourceProvider.ContainsKey(grade))
//            //    //    {
//            //    //        NonClassStudentSourceProvider nonClassStudentSourceProvider;
//            //    //        if (!_OldGradeNonClassStudentSourceProvider.ContainsKey(grade))
//            //    //        {
//            //    //            nonClassStudentSourceProvider = new NonClassStudentSourceProvider();
//            //    //            nonClassStudentSourceProvider.Grade = grade;
//            //    //        }
//            //    //        else
//            //    //        {
//            //    //            nonClassStudentSourceProvider = _OldGradeNonClassStudentSourceProvider[grade];
//            //    //            _OldGradeNonClassStudentSourceProvider.Remove(grade);
//            //    //        }
//            //    //        _GradeNonClassStudentSourceProvider.Add(grade, nonClassStudentSourceProvider);
//            //    //    }
//            //    //    _GradeNonClassStudentSourceProvider[grade].Source = new List<BriefStudentData>(_NonClassStudentSource[grade].ToArray());
//            //    //}
//            //    //_NonGradeStudentSourceProvider.Source = _NonGradeStudentSource;
//            //    //_NonGradeStudentSourceProvider.Nodes.Clear();
//            //    //#endregion
//            //    //#endregion
//            //    //#region 將SourceProvider放入TreeView
//            //    //#region 在校學生
//            //    ////在校學生節點不存在則新增
//            //    //if (!treeViewStudent.Nodes.Contains(_NormalStudentSourceProvider))
//            //    //    treeViewStudent.Nodes.Add(_NormalStudentSourceProvider);
//            //    ////各年級
//            //    //foreach (GradeStudentSourceProvider gradeSourceProvider in _GradeStudentSourceProvider.Values)
//            //    //{
//            //    //    //該年級結點不存在則新增
//            //    //    if (!_NormalStudentSourceProvider.Nodes.Contains(gradeSourceProvider))
//            //    //        _NormalStudentSourceProvider.Nodes.Add(gradeSourceProvider);
//            //    //}
//            //    ////各班級
//            //    //foreach (ClassStudentSourceProvider classSourceProvider in _ClassStudentSourceProvider.Values)
//            //    //{
//            //    //    //如果是未分年級
//            //    //    if (classSourceProvider.Grade != "")
//            //    //    {
//            //    //        //如果班級結點不存在則新增
//            //    //        if (!_GradeStudentSourceProvider[classSourceProvider.Grade].Nodes.Contains(classSourceProvider))
//            //    //            _GradeStudentSourceProvider[classSourceProvider.Grade].Nodes.Add(classSourceProvider);
//            //    //    }
//            //    //    else
//            //    //    {
//            //    //        //如果班級結點不存在則新增
//            //    //        if (!_NonGradeStudentSourceProvider.Nodes.Contains(classSourceProvider))
//            //    //            _NonGradeStudentSourceProvider.Nodes.Add(classSourceProvider);
//            //    //    }
//            //    //}
//            //    ////如果未分年級節點不存在則新增
//            //    //if (!_NormalStudentSourceProvider.Nodes.Contains(_NonGradeStudentSourceProvider))
//            //    //    _NormalStudentSourceProvider.Nodes.Add(_NonGradeStudentSourceProvider);
//            //    ////各年級未分班
//            //    //foreach (NonClassStudentSourceProvider var in _GradeNonClassStudentSourceProvider.Values)
//            //    //{
//            //    //    //未分年級未分班
//            //    //    if (var.Grade == "")
//            //    //    {
//            //    //        //如果班級節點不存在則新增
//            //    //        if (!_NonGradeStudentSourceProvider.Nodes.Contains(var))
//            //    //            _NonGradeStudentSourceProvider.Nodes.Add(var);
//            //    //    }
//            //    //    else
//            //    //    {
//            //    //        //如果班級節點不存在則新增
//            //    //        if (!_GradeStudentSourceProvider[var.Grade].Nodes.Contains(var))
//            //    //            _GradeStudentSourceProvider[var.Grade].Nodes.Add(var);
//            //    //    }
//            //    //}
//            //    ////刪掉已被移除的班級節點
//            //    //foreach (TreeNode deleteNode in _OldClassStudentSourceProvider.Values)
//            //    //{
//            //    //    if (deleteNode.Parent != null && deleteNode.Parent.Nodes.Contains(deleteNode))
//            //    //        deleteNode.Parent.Nodes.Remove(deleteNode);
//            //    //}
//            //    ////刪掉已被移除的未分班結點
//            //    //foreach (TreeNode deleteNode in _OldGradeNonClassStudentSourceProvider.Values)
//            //    //{
//            //    //    if (deleteNode.Parent != null && deleteNode.Parent.Nodes.Contains(deleteNode))
//            //    //        deleteNode.Parent.Nodes.Remove(deleteNode);
//            //    //}
//            //    ////刪掉已被移除的年級結點
//            //    //foreach (TreeNode deleteNode in _OldGradeStudentSourceProvider.Values)
//            //    //{
//            //    //    if (deleteNode.Parent != null && deleteNode.Parent.Nodes.Contains(deleteNode))
//            //    //        deleteNode.Parent.Nodes.Remove(deleteNode);
//            //    //}
//            //    ////展開在校學生
//            //    //_NormalStudentSourceProvider.Expand();
//            //    //#endregion
//            //    ////休學學生
//            //    //if (!treeViewStudent.Nodes.Contains(_OnLeaveStudentSourceProvider))
//            //    //    treeViewStudent.Nodes.Add(_OnLeaveStudentSourceProvider);
//            //    ////延修學生
//            //    //if (!treeViewStudent.Nodes.Contains(_ExtendingStudentSourceProvider))
//            //    //    treeViewStudent.Nodes.Add(_ExtendingStudentSourceProvider);
//            //    ////畢業及結業
//            //    //if (!treeViewStudent.Nodes.Contains(_GraduatedStudentSourceProvider))
//            //    //    treeViewStudent.Nodes.Add(_GraduatedStudentSourceProvider);
//            //    ////待處理學生
//            //    //if (!treeViewStudent.Nodes.Contains(_TempStudentSourceProvider))
//            //    //    treeViewStudent.Nodes.Add(_TempStudentSourceProvider);
//            //    ////刪除學生
//            //    //if (!treeViewStudent.Nodes.Contains(_DeletedStudentSourceProvider))
//            //    //    treeViewStudent.Nodes.Add(_DeletedStudentSourceProvider);
//            //    //#endregion
//            //    #endregion

//            //    _ReFlashTree = false;
//            //    _TreeViewStudent.ResumeLayout();
//            //    resetTreeViewWait();
//            //}
//            //#endregion
//            ////檢查選取資料是否變更
//            //#region 檢查選取資料是否變更
//            //List<BriefStudentData> newList = contentInfo.SelectedList;
//            //if ( newList.Count != _SelectionList.Count )
//            //{
//            //    _SelectionList = newList;
//            //    _SelectionList.TrimExcess();
//            //    enumList = _SelectionList.GetEnumerator();
//            //    //if (SelectionChanged != null)
//            //    //    SelectionChanged.Invoke(this, new EventArgs());
//            //    SmartSchool.Broadcaster.Events.Items["學生/選取變更"].Invoke();
//            //}
//            //else
//            //{
//            //    enumList.Reset();
//            //    foreach ( BriefStudentData var in newList )
//            //    {
//            //        enumList.MoveNext();
//            //        if ( enumList.Current != var )
//            //        {
//            //            _SelectionList = newList;
//            //            _SelectionList.TrimExcess();
//            //            enumList = _SelectionList.GetEnumerator();
//            //            //if (SelectionChanged != null)
//            //            //    SelectionChanged.Invoke(this, new EventArgs());
//            //            SmartSchool.Broadcaster.Events.Items["學生/選取變更"].Invoke();
//            //            break;
//            //        }
//            //    }
//            //}
//            //#endregion
//            ////變更清單右鍵選項
//            //#region 變更清單右鍵選項
//            //if ( contentInfo.SourceProvider == _TempStudentSourceProvider )
//            //{
//            //    toolStripMenuItemAddToTemp.Visible = false;
//            //    toolStripMenuItemRemoveFromTemp.Visible = true;
//            //}
//            //else
//            //{
//            //    toolStripMenuItemAddToTemp.Visible = true;
//            //    toolStripMenuItemRemoveFromTemp.Visible = false;
//            //}
//            ////toolStripMenuItemAddToTemp.Enabled = toolStripMenuItemRemoveFromTemp.Enabled = (contentInfo.SelectedList.Count > 0);
//            //foreach ( BaseItem btn in itemContainer1.SubItems )
//            //{
//            //    btn.Enabled = ( contentInfo.SelectedList.Count > 0 );
//            //}
//            //#endregion
//            ////檢查選取的SourceProvider
//            //#region 檢查選取的SourceProvider
//            //if ( _SelectionNode != _TreeViewStudent.SelectedNode )
//            //{
//            //    _SelectionNode = _TreeViewStudent.SelectedNode;
//            //    if ( _TreeViewStudent.SelectedNode != null && _TreeViewStudent.SelectedNode is ISourceProvider<CourseRec, ISearchCourse> )
//            //        contentInfo.SourceProvider = (ISourceProvider<CourseRec, ISearchCourse>)_TreeViewStudent.SelectedNode;
//            //    else
//            //        contentInfo.SourceProvider = _SearchAllStudentSourceProvider;
//            //}
//            //#endregion
//        }
//        /// <summary>
//        /// 設定顯示執行中圖示
//        /// </summary>
//        private void setTreeViewWait()
//        {
//            _treeViewWait = true;
//            if ( pictureBox1 != null )
//                pictureBox1.Visible = ( _treeViewWait ) & eppInSchoolStudent.Expanded;
//        }

//        /// <summary>
//        /// 設定隱藏執行中圖示
//        /// </summary>
//        private void resetTreeViewWait()
//        {
//            _treeViewWait = false;
//            if ( pictureBox1 != null )
//                pictureBox1.Visible = ( _treeViewWait ) & eppInSchoolStudent.Expanded;
//        }

//        private void btnSearchAll_Click(object sender, EventArgs e)
//        {
//            contentInfo.SourceProvider = _SearchAllStudentSourceProvider;
//        }
//        private void toolStripMenuItem1_Click(object sender, EventArgs e)
//        {
//            //ClassRelated.Class.Instance.Reflash();
//            Reflash();
//        }
//        #endregion
//    }
//}
