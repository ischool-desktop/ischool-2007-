using SmartSchool.Common;
namespace SmartSchool.CourseRelated
{
    partial class CourseEntity
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該公開 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CourseEntity));
            this.gif_loadding = new System.Windows.Forms.PictureBox();
            this.TreeClass = new SmartSchool.Common.DragDropTreeView();
            this.eppCourseNavigation = new DevComponents.DotNetBar.ExpandablePanel();
            this.contextMenuBar1 = new DevComponents.DotNetBar.ContextMenuBar();
            this.ctxDefault = new DevComponents.DotNetBar.ButtonItem();
            this.ctxiRefresh = new DevComponents.DotNetBar.ButtonItem();
            this.TreeTeacher = new SmartSchool.Common.DragDropTreeView();
            this.TreeSubject = new SmartSchool.Common.DragDropTreeView();
            this.epPrespective = new DevComponents.DotNetBar.ExpandablePanel();
            this.cboSemesters = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.chkClass = new System.Windows.Forms.RadioButton();
            this.chkSubject = new System.Windows.Forms.RadioButton();
            this.chkTeacher = new System.Windows.Forms.RadioButton();
            this.NavigationBar = new DevComponents.DotNetBar.NavigationPane();
            this.NavigationPaneControl = new DevComponents.DotNetBar.NavigationPanePanel();
            this.btnSearchAll = new DevComponents.DotNetBar.ButtonX();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.LoadCourseWorker = new System.ComponentModel.BackgroundWorker();
            this.ContentPane = new SmartSchool.CourseRelated.CourseContentPane();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.gif_loadding)).BeginInit();
            this.eppCourseNavigation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar1)).BeginInit();
            this.epPrespective.SuspendLayout();
            this.NavigationBar.SuspendLayout();
            this.NavigationPaneControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // gif_loadding
            // 
            this.gif_loadding.BackColor = System.Drawing.Color.White;
            this.gif_loadding.Image = global::SmartSchool.Properties.Resources.loading5;
            this.gif_loadding.Location = new System.Drawing.Point(71, 104);
            this.gif_loadding.Margin = new System.Windows.Forms.Padding(4);
            this.gif_loadding.Name = "gif_loadding";
            this.gif_loadding.Size = new System.Drawing.Size(32, 32);
            this.gif_loadding.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.gif_loadding.TabIndex = 3;
            this.gif_loadding.TabStop = false;
            // 
            // TreeClass
            // 
            this.TreeClass.AllowDrop = true;
            this.TreeClass.BackColor = System.Drawing.Color.White;
            this.contextMenuBar1.SetContextMenuEx(this.TreeClass, this.ctxDefault);
            this.TreeClass.Cursor = System.Windows.Forms.Cursors.Default;
            this.TreeClass.ForeColor = System.Drawing.Color.Black;
            this.TreeClass.FullRowSelect = true;
            this.TreeClass.HideSelection = false;
            this.TreeClass.HotTracking = true;
            this.TreeClass.ItemHeight = 20;
            this.TreeClass.Location = new System.Drawing.Point(0, 36);
            this.TreeClass.Margin = new System.Windows.Forms.Padding(4);
            this.TreeClass.Name = "TreeClass";
            this.TreeClass.Size = new System.Drawing.Size(148, 80);
            this.TreeClass.TabIndex = 1;
            this.TreeClass.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
            this.TreeClass.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseClick);
            // 
            // eppCourseNavigation
            // 
            this.eppCourseNavigation.CanvasColor = System.Drawing.SystemColors.Control;
            this.eppCourseNavigation.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.eppCourseNavigation.Controls.Add(this.contextMenuBar1);
            this.eppCourseNavigation.Controls.Add(this.gif_loadding);
            this.eppCourseNavigation.Controls.Add(this.TreeTeacher);
            this.eppCourseNavigation.Controls.Add(this.TreeSubject);
            this.eppCourseNavigation.Controls.Add(this.TreeClass);
            this.eppCourseNavigation.Controls.Add(this.epPrespective);
            this.eppCourseNavigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eppCourseNavigation.ExpandButtonVisible = false;
            this.eppCourseNavigation.Location = new System.Drawing.Point(0, 25);
            this.eppCourseNavigation.Margin = new System.Windows.Forms.Padding(4);
            this.eppCourseNavigation.Name = "eppCourseNavigation";
            this.eppCourseNavigation.Size = new System.Drawing.Size(171, 489);
            this.eppCourseNavigation.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.eppCourseNavigation.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.eppCourseNavigation.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.eppCourseNavigation.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.eppCourseNavigation.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.eppCourseNavigation.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.eppCourseNavigation.Style.GradientAngle = 90;
            this.eppCourseNavigation.TabIndex = 2;
            this.eppCourseNavigation.TitleHeight = 25;
            this.eppCourseNavigation.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.eppCourseNavigation.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.eppCourseNavigation.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.eppCourseNavigation.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.eppCourseNavigation.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.eppCourseNavigation.TitleStyle.CornerDiameter = 2;
            this.eppCourseNavigation.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.eppCourseNavigation.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.eppCourseNavigation.TitleStyle.GradientAngle = 90;
            this.eppCourseNavigation.TitleText = "課程分類";
            // 
            // contextMenuBar1
            // 
            this.contextMenuBar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.contextMenuBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ctxDefault});
            this.contextMenuBar1.Location = new System.Drawing.Point(11, 123);
            this.contextMenuBar1.Name = "contextMenuBar1";
            this.contextMenuBar1.Size = new System.Drawing.Size(137, 25);
            this.contextMenuBar1.Stretch = true;
            this.contextMenuBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.contextMenuBar1.TabIndex = 7;
            this.contextMenuBar1.TabStop = false;
            this.contextMenuBar1.Text = "contextMenuBar1";
            // 
            // ctxDefault
            // 
            this.ctxDefault.AutoExpandOnClick = true;
            this.ctxDefault.ImagePaddingHorizontal = 8;
            this.ctxDefault.Name = "ctxDefault";
            this.ctxDefault.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ctxiRefresh});
            this.ctxDefault.Text = "DefaultMenu";
            // 
            // ctxiRefresh
            // 
            this.ctxiRefresh.ImagePaddingHorizontal = 8;
            this.ctxiRefresh.Name = "ctxiRefresh";
            this.ctxiRefresh.Text = "重新整理";
            this.ctxiRefresh.Click += new System.EventHandler(this.ctxiRefresh_Click);
            // 
            // TreeTeacher
            // 
            this.TreeTeacher.AllowDrop = true;
            this.TreeTeacher.BackColor = System.Drawing.Color.White;
            this.contextMenuBar1.SetContextMenuEx(this.TreeTeacher, this.ctxDefault);
            this.TreeTeacher.Cursor = System.Windows.Forms.Cursors.Default;
            this.TreeTeacher.ForeColor = System.Drawing.Color.Black;
            this.TreeTeacher.FullRowSelect = true;
            this.TreeTeacher.HideSelection = false;
            this.TreeTeacher.HotTracking = true;
            this.TreeTeacher.ItemHeight = 20;
            this.TreeTeacher.Location = new System.Drawing.Point(0, 242);
            this.TreeTeacher.Margin = new System.Windows.Forms.Padding(4);
            this.TreeTeacher.Name = "TreeTeacher";
            this.TreeTeacher.Size = new System.Drawing.Size(148, 80);
            this.TreeTeacher.TabIndex = 6;
            this.TreeTeacher.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
            this.TreeTeacher.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseClick);
            // 
            // TreeSubject
            // 
            this.TreeSubject.AllowDrop = true;
            this.TreeSubject.BackColor = System.Drawing.Color.White;
            this.contextMenuBar1.SetContextMenuEx(this.TreeSubject, this.ctxDefault);
            this.TreeSubject.Cursor = System.Windows.Forms.Cursors.Default;
            this.TreeSubject.ForeColor = System.Drawing.Color.Black;
            this.TreeSubject.FullRowSelect = true;
            this.TreeSubject.HideSelection = false;
            this.TreeSubject.HotTracking = true;
            this.TreeSubject.ItemHeight = 20;
            this.TreeSubject.Location = new System.Drawing.Point(0, 139);
            this.TreeSubject.Margin = new System.Windows.Forms.Padding(4);
            this.TreeSubject.Name = "TreeSubject";
            this.TreeSubject.Size = new System.Drawing.Size(148, 80);
            this.TreeSubject.TabIndex = 5;
            this.TreeSubject.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
            this.TreeSubject.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseClick);
            // 
            // epPrespective
            // 
            this.epPrespective.AutoSize = true;
            this.epPrespective.CanvasColor = System.Drawing.SystemColors.Control;
            this.epPrespective.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.epPrespective.Controls.Add(this.cboSemesters);
            this.epPrespective.Controls.Add(this.chkClass);
            this.epPrespective.Controls.Add(this.chkSubject);
            this.epPrespective.Controls.Add(this.chkTeacher);
            this.epPrespective.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.epPrespective.ExpandOnTitleClick = true;
            this.epPrespective.Location = new System.Drawing.Point(0, 346);
            this.epPrespective.Name = "epPrespective";
            this.epPrespective.Size = new System.Drawing.Size(171, 143);
            this.epPrespective.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.epPrespective.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.epPrespective.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.epPrespective.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.epPrespective.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.epPrespective.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.epPrespective.Style.GradientAngle = 90;
            this.epPrespective.TabIndex = 4;
            this.epPrespective.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.epPrespective.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.epPrespective.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.epPrespective.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.epPrespective.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.epPrespective.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.epPrespective.TitleStyle.GradientAngle = 90;
            this.epPrespective.TitleText = "檢視模式";
            this.epPrespective.ExpandedChanged += new DevComponents.DotNetBar.ExpandChangeEventHandler(this.epPrespective_ExpandedChanged);
            // 
            // cboSemesters
            // 
            this.cboSemesters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSemesters.DisplayMember = "Text";
            this.cboSemesters.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSemesters.FormattingEnabled = true;
            this.cboSemesters.Location = new System.Drawing.Point(11, 29);
            this.cboSemesters.Name = "cboSemesters";
            this.cboSemesters.Size = new System.Drawing.Size(151, 26);
            this.cboSemesters.TabIndex = 4;
            this.cboSemesters.SelectedIndexChanged += new System.EventHandler(this.cboSemesters_SelectedIndexChanged);
            // 
            // chkClass
            // 
            this.chkClass.Location = new System.Drawing.Point(11, 117);
            this.chkClass.Name = "chkClass";
            this.chkClass.Size = new System.Drawing.Size(160, 23);
            this.chkClass.TabIndex = 3;
            this.chkClass.Tag = "Class";
            this.chkClass.Text = "依班級檢視課程";
            this.chkClass.CheckedChanged += new System.EventHandler(this.NavigateCheckBox_CheckedChanged);
            // 
            // chkSubject
            // 
            this.chkSubject.Location = new System.Drawing.Point(11, 88);
            this.chkSubject.Name = "chkSubject";
            this.chkSubject.Size = new System.Drawing.Size(160, 23);
            this.chkSubject.TabIndex = 2;
            this.chkSubject.Tag = "Subject";
            this.chkSubject.Text = "依科目檢視課程";
            this.chkSubject.CheckedChanged += new System.EventHandler(this.NavigateCheckBox_CheckedChanged);
            // 
            // chkTeacher
            // 
            this.chkTeacher.Location = new System.Drawing.Point(11, 59);
            this.chkTeacher.Name = "chkTeacher";
            this.chkTeacher.Size = new System.Drawing.Size(160, 23);
            this.chkTeacher.TabIndex = 1;
            this.chkTeacher.Tag = "Teacher";
            this.chkTeacher.Text = "依教師檢視課程";
            this.chkTeacher.CheckedChanged += new System.EventHandler(this.NavigateCheckBox_CheckedChanged);
            // 
            // NavigationBar
            // 
            this.NavigationBar.BackColor = System.Drawing.Color.Transparent;
            this.NavigationBar.Controls.Add(this.NavigationPaneControl);
            this.NavigationBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.NavigationBar.ItemPaddingBottom = 2;
            this.NavigationBar.ItemPaddingTop = 2;
            this.NavigationBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1});
            this.NavigationBar.Location = new System.Drawing.Point(0, 0);
            this.NavigationBar.Margin = new System.Windows.Forms.Padding(4);
            this.NavigationBar.Name = "NavigationBar";
            this.NavigationBar.Padding = new System.Windows.Forms.Padding(1);
            this.NavigationBar.Size = new System.Drawing.Size(173, 573);
            this.NavigationBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.NavigationBar.TabIndex = 2;
            // 
            // 
            // 
            this.NavigationBar.TitlePanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.NavigationBar.TitlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.NavigationBar.TitlePanel.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NavigationBar.TitlePanel.Location = new System.Drawing.Point(1, 1);
            this.NavigationBar.TitlePanel.Margin = new System.Windows.Forms.Padding(4);
            this.NavigationBar.TitlePanel.Name = "panelTitle";
            this.NavigationBar.TitlePanel.Size = new System.Drawing.Size(171, 25);
            this.NavigationBar.TitlePanel.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.NavigationBar.TitlePanel.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.NavigationBar.TitlePanel.Style.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.NavigationBar.TitlePanel.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.NavigationBar.TitlePanel.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.NavigationBar.TitlePanel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.NavigationBar.TitlePanel.Style.GradientAngle = 90;
            this.NavigationBar.TitlePanel.Style.MarginLeft = 4;
            this.NavigationBar.TitlePanel.TabIndex = 0;
            this.NavigationBar.TitlePanel.Text = "課程";
            // 
            // NavigationPaneControl
            // 
            this.NavigationPaneControl.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.NavigationPaneControl.Controls.Add(this.eppCourseNavigation);
            this.NavigationPaneControl.Controls.Add(this.btnSearchAll);
            this.NavigationPaneControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NavigationPaneControl.Location = new System.Drawing.Point(1, 26);
            this.NavigationPaneControl.Margin = new System.Windows.Forms.Padding(4);
            this.NavigationPaneControl.Name = "NavigationPaneControl";
            this.NavigationPaneControl.ParentItem = this.buttonItem1;
            this.NavigationPaneControl.Size = new System.Drawing.Size(171, 514);
            this.NavigationPaneControl.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.NavigationPaneControl.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.NavigationPaneControl.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.NavigationPaneControl.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.NavigationPaneControl.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.NavigationPaneControl.Style.GradientAngle = 90;
            this.NavigationPaneControl.TabIndex = 2;
            this.NavigationPaneControl.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnSearchAll
            // 
            this.btnSearchAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearchAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearchAll.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSearchAll.Location = new System.Drawing.Point(0, 0);
            this.btnSearchAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearchAll.Name = "btnSearchAll";
            this.btnSearchAll.Size = new System.Drawing.Size(171, 25);
            this.btnSearchAll.SubItemsExpandWidth = 17;
            this.btnSearchAll.TabIndex = 1;
            this.btnSearchAll.Text = "搜尋所有課程";
            this.btnSearchAll.Click += new System.EventHandler(this.btnSearchAll_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.Checked = true;
            this.buttonItem1.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem1.Image")));
            this.buttonItem1.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.OptionGroup = "navBar";
            this.buttonItem1.Text = "課程";
            // 
            // LoadCourseWorker
            // 
            this.LoadCourseWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.LoadCourseWorker_DoWork);
            this.LoadCourseWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.LoadCourseWorker_RunWorkerCompleted);
            // 
            // ContentPane
            // 
            this.ContentPane.BackColor = System.Drawing.Color.Transparent;
            this.ContentPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentPane.Location = new System.Drawing.Point(173, 0);
            this.ContentPane.Name = "ContentPane";
            this.ContentPane.Size = new System.Drawing.Size(819, 573);
            this.ContentPane.TabIndex = 3;
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.DisplayMember = "Text";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.Location = new System.Drawing.Point(11, 121);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(160, 23);
            this.comboBoxEx1.TabIndex = 4;
            // 
            // CourseEntity
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(992, 573);
            this.Controls.Add(this.ContentPane);
            this.Controls.Add(this.NavigationBar);
            this.Name = "CourseEntity";
            this.Text = "Course";
            ((System.ComponentModel.ISupportInitialize)(this.gif_loadding)).EndInit();
            this.eppCourseNavigation.ResumeLayout(false);
            this.eppCourseNavigation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar1)).EndInit();
            this.epPrespective.ResumeLayout(false);
            this.NavigationBar.ResumeLayout(false);
            this.NavigationPaneControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox gif_loadding;
        private DevComponents.DotNetBar.ExpandablePanel eppCourseNavigation;
        private DevComponents.DotNetBar.NavigationPane NavigationBar;
        private DevComponents.DotNetBar.NavigationPanePanel NavigationPaneControl;
        private DevComponents.DotNetBar.ButtonX btnSearchAll;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ExpandablePanel epPrespective;
        private System.Windows.Forms.RadioButton chkClass;
        private System.Windows.Forms.RadioButton chkSubject;
        private System.Windows.Forms.RadioButton chkTeacher;
        private System.ComponentModel.BackgroundWorker LoadCourseWorker;
        private DragDropTreeView TreeTeacher;
        private DragDropTreeView TreeSubject;
        private DragDropTreeView TreeClass;
        private CourseContentPane ContentPane;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSemesters;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx1;
        private DevComponents.DotNetBar.ContextMenuBar contextMenuBar1;
        private DevComponents.DotNetBar.ButtonItem ctxDefault;
        private DevComponents.DotNetBar.ButtonItem ctxiRefresh;
    }
}