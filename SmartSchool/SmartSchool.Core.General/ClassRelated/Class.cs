using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using DevComponents.DotNetBar;
using SmartSchool.StudentRelated;
using SmartSchool.Feature.Class;
using IntelliSchool.DSA30.Util;
using System.Xml;
using System.Text.RegularExpressions;
using SmartSchool.ClassRelated.SourceProvider;
using SmartSchool.ClassRelated.Divider;
using SmartSchool.Properties;
using System.Threading;
using SmartSchool.TeacherRelated;
using SmartSchool.ClassRelated.Search;

namespace SmartSchool.ClassRelated
{
    public class Class : IEntity
    {
        private static Class _Instance;
        public static Class Instance
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
            _Instance = new Class();
        }

        internal void SetupSynchronization()
        {
            //Instance.SelectionChanged+=new EventHandler(Instance_SelectionChanged);
            SmartSchool.Broadcaster.Events.Items["班級/選取變更"].Handler += delegate
            {
                MotherForm.Instance.SetBarMessage("已選取" + this.SelectionClasses.Count + "個班級");
            };
            ClassDeleted += new EventHandler<DeleteClassEventArgs>(Class_ClassDeleted);
            Student.Instance.StudentDeleted += new EventHandler<StudentDeletedEventArgs>(Instance_StudentDeleted);
            //Student.Instance.BriefDataChanged += new EventHandler<BriefDataChangedEventArgs>(Instance_BriefDataChanged);
            SmartSchool.Broadcaster.Events.Items["學生/資料變更"].Handler += delegate { _ReFlashTree = true; };
            Teacher.Instance.TeacherDeleted += new EventHandler<TeacherDeletedEventArgs>(Instance_TeacherDeleted);
            Teacher.Instance.TeacherDataChanged += new EventHandler<TeacherDataChangedEventArgs>(Instance_TeacherDataChanged);
        }

        void Class_ClassDeleted(object sender, DeleteClassEventArgs e)
        {
            _ReFlashTree = true;
        }

        void Instance_TeacherDataChanged(object sender, TeacherDataChangedEventArgs e)
        {
            _ReFlashTree = true;
        }

        void Instance_TeacherDeleted(object sender, TeacherDeletedEventArgs e)
        {
            List<string> classIDList=new List<string>();
            foreach (ClassInfo info in _ClassList.Values)
            {
                if (info.TeacherID == e.ID)
                {
                    classIDList.Add(info.ClassID);
                }
            }
            DSResponse dsrsp = SmartSchool.Feature.Class.QueryClass.GetClass(classIDList.ToArray());
            _loadingWait.WaitOne();
            _loadingWait.Reset();
            foreach (XmlElement element in dsrsp.GetContent().GetElements("Class"))
            {
                ClassInfo info = new ClassInfo(element);
                _ClassList[info.ClassID] = info;
            }
            _loadingWait.Set();
            _ReFlashTree = true;            
        }

        void Instance_StudentDeleted(object sender, StudentDeletedEventArgs e)
        {
            _ReFlashTree = true;
        }

        //void Instance_BriefDataChanged(object sender, BriefDataChangedEventArgs e)
        //{
        //    _ReFlashTree = true;
        //}

        private Class()
        {
            //取得班級跟學生資料
            getBriefData();
        }

        internal List<ClassInfo> GetSupervise(string teacherid)
        {
            List<ClassInfo> list = new List<ClassInfo>();
            foreach (ClassInfo var in _ClassList.Values)
            {
                if (var.TeacherID == teacherid)
                    list.Add(var);
            }
            return list;
        }

        internal void SortItems()
        {
            _loadingWait.WaitOne();
            List<ClassInfo> studentList = new List<ClassInfo>();
            studentList.AddRange(_ClassList.Values);
            studentList.Sort();
            _loadingWait.Reset();
            _ClassList.Clear();
            foreach ( ClassInfo var in studentList )
            {
                _ClassList.Add(var.ClassID, var);
            }
            _loadingWait.Set();
        }

        private bool _Initialized = false;
        private List<ClassInfo> _SelectionList = new List<ClassInfo>();
        private bool _treeViewWait = false;
        //進入Idle時是否重新整理資料
        private bool _ReFlashTree = false;
        //暫存班級清單
        private Dictionary<string, ClassInfo> _ClassList;
        //同步取得班級和學生資料的backGroundWorker

        private ManualResetEvent _loadingWait=new ManualResetEvent(true);
        private BackgroundWorker _BkwBriefDataLoader;
        private NavigationPanePanel _NavPanel;
        private DevComponents.DotNetBar.ButtonX _BtnSearchAll;
        private DevComponents.DotNetBar.ExpandablePanel _EppTeacher;
        private DragDropTreeView _TreeViewClass;
        private System.Windows.Forms.PictureBox _WaitingPicture;
        private System.Windows.Forms.ContextMenuStrip _ContextMenuReflash;
        private System.Windows.Forms.ToolStripMenuItem _ToolStripReflashItem;
        private ContextMenuStrip listPaneMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAddToTemp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRemoveFromTemp;
        private bool _IsTreeViewDividing = false;
        //加速選取學生比對判斷選取學生變更用
        //private IEnumerator<ClassInfo> enumList = new List<ClassInfo>(0).GetEnumerator();


        private Dictionary<string, PopupClassForm> _forms;
        public void PopupClassForm(string classid, string caption)
        {
            if (_forms == null)
                _forms = new Dictionary<string, PopupClassForm>();
            PopupClassForm form;
            if (_forms.ContainsKey(classid))
                form = _forms[classid];
            else
            {
                form = new PopupClassForm(classid);
                //form.Size = new Size(570, 600);
                form.Text = caption;
                //form.ShowIcon = false;
                //form.StartPosition = FormStartPosition.CenterParent;
                form.ClassFormClosing += new EventHandler<ClassFormClosingEventArgs>(form_ClassFormClosing);
                _forms.Add(classid, form);
            }
            form.Show();
            form.Focus();
        }
        void form_ClassFormClosing(object sender, ClassFormClosingEventArgs e)
        {
            if (_forms.ContainsKey(e.ClassID))
                _forms.Remove(e.ClassID);
        }


        private AllClassSourceProvider _AllClassSourceProvider = new AllClassSourceProvider();
        private TempClassSourceProvider _TempClassSourceProvider;

        private ContentInfo contentInfo;

        private GradeYearDivider _GradeYearDivider;

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
            this._BtnSearchAll.Text = "搜尋所有班級";
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

            #region _TreeViewTeacher
            this._TreeViewClass = new DragDropTreeView();
            this._TreeViewClass.BackColor = System.Drawing.Color.White;
            this._TreeViewClass.ContextMenuStrip = this._ContextMenuReflash;
            this._TreeViewClass.Cursor = System.Windows.Forms.Cursors.Default;
            this._TreeViewClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this._TreeViewClass.ForeColor = System.Drawing.Color.Black;
            this._TreeViewClass.BorderStyle = BorderStyle.Fixed3D;
            this._TreeViewClass.HotTracking = true;
            this._TreeViewClass.ItemHeight = 20;
            this._TreeViewClass.Location = new System.Drawing.Point(0, 23);
            this._TreeViewClass.Name = "treeViewStudent";
            this._TreeViewClass.Size = new System.Drawing.Size(139, 410);
            this._TreeViewClass.TabIndex = 1;
            this._TreeViewClass.AfterSelect += new TreeViewEventHandler(treeViewStudent_AfterSelect);
            this._TreeViewClass.NodeMouseClick += new TreeNodeMouseClickEventHandler(_TreeViewClass_NodeMouseClick);
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
            this._EppTeacher.Controls.Add(this._TreeViewClass);
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
            this._EppTeacher.TitleText = "班級類別";
            this._EppTeacher.SizeChanged += new System.EventHandler(this.eppInSchoolStudent_SizeChanged);
            this._EppTeacher.ExpandedChanged += new ExpandChangeEventHandler(_EppTeacher_ExpandedChanged);
            #endregion

            #region NavPanel
            _NavPanel = new NavigationPanePanel();
            _NavPanel.ColorScheme.ItemDesignTimeBorder = System.Drawing.Color.Black;
            _NavPanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            _NavPanel.Controls.Add(this._EppTeacher);
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
            SmartSchool.Customization.PlugIn.ExtendedColumn.ExtendClassColumn.SetManager(contentInfo);
            #endregion

            _TempClassSourceProvider = new TempClassSourceProvider();
            _GradeYearDivider = new GradeYearDivider();
            _GradeYearDivider.TempProvider = _TempClassSourceProvider;
            _GradeYearDivider.TargetTreeView = _TreeViewClass;

            _Initialized = true;
        }

        void _TreeViewClass_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                _TreeViewClass.SelectedNode = e.Node;
                contentInfo.SourceProvider = (ISourceProvider<ClassInfo, ISearchClass>)e.Node;
                contentInfo.SelectAllSource();
            }
        }

        private void _EppTeacher_ExpandedChanged(object sender, ExpandedChangeEventArgs e)
        {
            _WaitingPicture.Visible = _treeViewWait & _EppTeacher.Expanded;
        }

        //private void Instance_TeacherDeleted(object sender, TeacherDeletedEventArgs e)
        //{
        //    _TeacherList.Remove(e.ID);
        //    _ReFlashTree = true;
        //}

        //private void Instance_SelectionChanged(object sender, EventArgs e)
        //{
        //    MotherForm.Instance.SetBarMessage("已選取" + this.SelectionClasses.Count + "個班級");
        //}

        //private void Instance_TeacherDataChanged(object sender, TeacherDataChangedEventArgs e)
        //{
        //    foreach (TeacherDataChangedContent var in e.Items)
        //    {
        //        _TeacherList[var.OldData.ID] = var.NewData;
        //    }
        //    _ReFlashTree = true;
        //}

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //StudentRelated.Student.Instance.Reflash();
            Reflash();
        }

        public void Reflash()
        {
            _ReFlashTree = true;
            setTreeViewWait();
            //_TempClassSourceProvider = new TempClassSourceProvider();
            //_TempClassSourceProvider.SourceChanged += new EventHandler(TempClassSourceProvider_SourceChanged);

            contentInfo.SourceProvider = null;
            getBriefData();
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            _loadingWait.WaitOne();
            this._AllClassSourceProvider.Source = new List<ClassInfo>(_ClassList.Values);
            contentInfo.SourceProvider = _AllClassSourceProvider;
        }

        private void treeViewStudent_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!_IsTreeViewDividing)
            {
                if (_TreeViewClass.SelectedNode != null)
                    contentInfo.SourceProvider = (ISourceProvider<ClassInfo, Search.ISearchClass>)_TreeViewClass.SelectedNode;
                else
                    btnSearchAll_Click(null, null);
            }
        }

        //同步選取資料及顯示資料
        private void Application_Idle(object sender, EventArgs e)
        {
            //要求重新整理且資料準備OK
            #region 重新整理資料
            _loadingWait.WaitOne();
            if (_ReFlashTree && _ClassList != null)
            {
                _TreeViewClass.SuspendLayout();

                _TreeViewClass.Nodes.Clear();

                //檢查待處理班級資料是否更新
                List<ClassInfo> newTempSource = new List<ClassInfo>();
                foreach (ClassInfo var in _TempClassSourceProvider.Source)
                {
                    if (_ClassList.ContainsKey(var.ClassID))
                        newTempSource.Add(_ClassList[var.ClassID]);
                }
                _TempClassSourceProvider.Source = newTempSource;
                this._AllClassSourceProvider.Source = new List<ClassInfo>(_ClassList.Values);
                //紀錄原本選取的點
                TreeNode selectNode = null;
                if (contentInfo.SourceProvider != null && !(contentInfo.SourceProvider is AllClassSourceProvider))
                {
                    selectNode = (TreeNode)contentInfo.SourceProvider;
                }

                _IsTreeViewDividing = true;
                _GradeYearDivider.Divide(_ClassList);
                _IsTreeViewDividing = false;

                //還原選取的點
                if (selectNode != null)
                {
                    if (selectNode.TreeView == null)
                    {
                        _TreeViewClass.SelectedNode = null;
                        btnSearchAll_Click(null, null);
                    }
                    else
                        _TreeViewClass.SelectedNode = selectNode;
                }

                _ReFlashTree = false;
                if (contentInfo.SourceProvider == null)
                {
                    _AllClassSourceProvider.Source = new List<ClassInfo>(_ClassList.Values);
                    contentInfo.SourceProvider = _AllClassSourceProvider;
                }
                _TreeViewClass.ResumeLayout();
                resetTreeViewWait();
            }
            #endregion
            //檢查選取資料是否變更
            #region 檢查選取資料是否變更
            List<ClassInfo> newList = contentInfo.SelectedList;
            newList.Sort();
            if (newList.Count != _SelectionList.Count)
            {
                _SelectionList = newList;
                //_SelectionList.TrimExcess();
                //enumList = _SelectionList.GetEnumerator();
                //if (SelectionChanged != null)
                //    SelectionChanged.Invoke(this, new EventArgs());
                SmartSchool.Broadcaster.Events.Items["班級/選取變更"].Invoke();
            }
            else
            {
                //enumList.Reset();
                //int i = 0;
                for ( int i = 0 ; i < newList.Count ; i++ )
                {
                    //enumList.MoveNext();
                    //if (enumList.Current != var)
                    if ( !newList[i].Equals(_SelectionList[i]) )
                    {
                        _SelectionList = newList;
                        //_SelectionList.TrimExcess();
                        //enumList = _SelectionList.GetEnumerator();
                        //if (SelectionChanged != null)
                        //    SelectionChanged.Invoke(this, new EventArgs());
                        SmartSchool.Broadcaster.Events.Items["班級/選取變更"].Invoke();
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
            //    foreach (ClassInfo var in contentInfo.SelectedList)
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
            if (contentInfo.SourceProvider == _TempClassSourceProvider)
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

        // 取得班級資料
        private void getBriefData()
        {
            _loadingWait.WaitOne();

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
            //if (!_Initialized)
            //    Init();
            SortItems();
            _ReFlashTree = true;
        }
        private void _bkwBriefDataLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            //取得所有班級資料
            try
            {
                _loadingWait.Reset();
            _ClassList = new Dictionary<string, ClassInfo>();
                e.Result = SmartSchool.Feature.Class.QueryClass.GetClass();
                DSResponse dsrsp = (DSResponse)e.Result;
                foreach (XmlElement element in dsrsp.GetContent().GetElements("Class"))
                {
                    ClassInfo info = new ClassInfo(element);
                    _ClassList.Add(info.ClassID, info);
                }
            }
            catch { }
            finally
            {
                _loadingWait.Set();
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

        #region 班級相關事件
        //public event EventHandler SelectionChanged;
        public List<ClassInfo> SelectionClasses
        {
            get
            {
                if (contentInfo != null)
                {
                    if (contentInfo.SelectedList.Count != _SelectionList.Count)
                    {
                        _SelectionList = contentInfo.SelectedList;
                        //if (SelectionChanged != null)
                        //    SelectionChanged.Invoke(this, new EventArgs());
                        SmartSchool.Broadcaster.Events.Items["班級/選取變更"].Invoke();
                    }
                    else
                    {
                        foreach (ClassInfo var in contentInfo.SelectedList)
                        {
                            if (!_SelectionList.Contains(var))
                            {
                                _SelectionList = contentInfo.SelectedList;
                                //if (SelectionChanged != null)
                                //    SelectionChanged.Invoke(this, new EventArgs());
                                SmartSchool.Broadcaster.Events.Items["班級/選取變更"].Invoke();
                                break;
                            }
                        }
                    }
                }
                return _SelectionList;
            }
        }

        public event EventHandler<DeleteClassEventArgs> ClassDeleted;
        public void InvokClassDeleted(DeleteClassEventArgs args)
        {
            _loadingWait.WaitOne();
            _loadingWait.Reset();
            foreach (string var in args.DeleteClassIDArray)
            {
                _ClassList.Remove(var);
            }
            _loadingWait.Set();
            if (ClassDeleted != null)
                ClassDeleted.Invoke(this, args);
        }

        public event EventHandler<InsertClassEventArgs> ClassInserted;
        public void InvokClassInserted(InsertClassEventArgs args)
        {
            DSResponse dsrsp = SmartSchool.Feature.Class.QueryClass.GetClass(args.InsertClassID);
            _loadingWait.WaitOne();
            _loadingWait.Reset();
            foreach (XmlElement element in dsrsp.GetContent().GetElements("Class"))
            {
                ClassInfo info = new ClassInfo(element);
                _ClassList.Add(info.ClassID, info);
            }
            _loadingWait.Set();
            SortItems();
            _ReFlashTree = true;
            if (ClassInserted != null)
                ClassInserted.Invoke(this, args);
        }

        public event EventHandler<UpdateClassEventArgs> ClassUpdated;
        public void InvokClassUpdated(params string[] classIDList)
        {
            if ( classIDList.Length == 0 ) return;
            UpdateClassEventArgs args = new UpdateClassEventArgs();
            DSResponse dsrsp = SmartSchool.Feature.Class.QueryClass.GetClass(classIDList);
            _loadingWait.WaitOne();
            _loadingWait.Reset();
            foreach (XmlElement element in dsrsp.GetContent().GetElements("Class"))
            {
                ClassInfo info = new ClassInfo(element);
                args.Items.Add(new UpdateClassContent(_ClassList[info.ClassID], info));
                _ClassList[info.ClassID] = info;
            }
            _loadingWait.Set();
            SortItems();
            _ReFlashTree = true;
            if (ClassUpdated != null)
            {
                ClassUpdated.Invoke(this, args);
            }
        }
        //public event EventHandler TeacherInserted;
        //public void InvokTeacherInserted(object sender, EventArgs e)
        //{
        //    if (TeacherInserted != null)
        //    {
        //        TeacherInserted.Invoke(sender, e);
        //    }
        //}

        //public event EventHandler<TeacherDeletedEventArgs> TeacherDeleted;
        //public void InvokTeacherDeleted(object sender, TeacherDeletedEventArgs e)
        //{
        //    if (TeacherDeleted != null)
        //        TeacherDeleted.Invoke(sender, e);
        //}

        //public event EventHandler<TeacherDataChangedEventArgs> TeacherDataChanged;
        //public void InvokTeacherDataChanged(object sender, params string[] teacherIDList)
        //{
        //    TeacherDataChangedEventArgs e = new TeacherDataChangedEventArgs();
        //    Dictionary<string, DetailClassInfo> _ChangedData = new Dictionary<string, DetailClassInfo>();
        //    //取得SERVER上最新資料
        //    XmlElement[] elements = QueryTeacher.GetTeacherListWithSupervisedByClassInfo(teacherIDList).GetContent().GetElements("Teacher");
        //    foreach (XmlElement ele in elements)
        //    {
        //        DetailClassInfo newData;
        //        string id = ele.SelectSingleNode("@ID").InnerText;
        //        if (_ChangedData.ContainsKey(id))
        //        {
        //            newData = _TeacherList[id];
        //        }
        //        else
        //        {
        //            newData = new DetailClassInfo(ele);
        //            _ChangedData.Add(id, newData);
        //        }

        //        if (ele.SelectSingleNode("SupervisedByClassID").InnerText != "")
        //        {
        //            newData.SupervisedByClassInfo.Add(new SupervisedByClassInfo(ele));
        //        }
        //    }
        //    foreach (DetailClassInfo newData in _ChangedData.Values)
        //    {
        //        if (_TeacherList.ContainsKey(newData.ID))
        //        {
        //            e.Items.Add(new TeacherDataChangedContent(_TeacherList[newData.ID], newData));
        //        }
        //    }
        //    if (TeacherDataChanged != null)
        //        TeacherDataChanged.Invoke(sender, e);
        //}
        #endregion

        #region IEntity 成員

        public string Title
        {
            get { return "班級"; }
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
                return Resources.Navigation_Class_New;
            }
        }

        public void Actived()
        {
            contentInfo.LoadPreference();
        }

        #endregion

        private void toolStripMenuItemRemoveFromTemp_Click(object sender, EventArgs e)
        {
            foreach (ClassInfo var in contentInfo.SelectedList)
            {
                if (_TempClassSourceProvider.Source.Contains(var))
                    _TempClassSourceProvider.Source.Remove(var);
            }
            _TempClassSourceProvider.Source = _TempClassSourceProvider.Source;
        }

        private void toolStripMenuItemAddToTemp_Click(object sender, EventArgs e)
        {
            List<ClassInfo> insertList = new List<ClassInfo>();
            foreach (ClassInfo var in contentInfo.SelectedList)
            {
                if (!_TempClassSourceProvider.Source.Contains(var))
                    insertList.Add(var);
            }
            _TempClassSourceProvider.Source.AddRange(insertList);
            _TempClassSourceProvider.Source = _TempClassSourceProvider.Source;
        }

        internal string ParseClassName(string namingRule, int gradeYear)
        {
            gradeYear--;
            if ( !ValidateNamingRule(namingRule) )
                return namingRule;
            string classlist_firstname = "", classlist_lastname = "";
            if ( namingRule.Length == 0 ) return "{" + ( gradeYear + 1 ) + "}";

            string tmp_convert = namingRule;

            // 找出"{"之前文字 並放入 classlist_firstname , 並除去"{"
            if ( tmp_convert.IndexOf('{') > 0 )
            {
                classlist_firstname = tmp_convert.Substring(0, tmp_convert.IndexOf('{'));
                tmp_convert = tmp_convert.Substring(tmp_convert.IndexOf('{') + 1, tmp_convert.Length - ( tmp_convert.IndexOf('{') + 1 ));
            }
            else tmp_convert = tmp_convert.TrimStart('{');

            // 找出 } 之後文字 classlist_lastname , 並除去"}"
            if ( tmp_convert.IndexOf('}') > 0 && tmp_convert.IndexOf('}') < tmp_convert.Length - 1 )
            {
                classlist_lastname = tmp_convert.Substring(tmp_convert.IndexOf('}') + 1, tmp_convert.Length - ( tmp_convert.IndexOf('}') + 1 ));
                tmp_convert = tmp_convert.Substring(0, tmp_convert.IndexOf('}'));
            }
            else tmp_convert = tmp_convert.TrimEnd('}');

            // , 存入 array
            string[] listArray = new string[tmp_convert.Split(',').Length];
            listArray = tmp_convert.Split(',');

            // 檢查是否在清單範圍
            if ( gradeYear >= 0 && gradeYear < listArray.Length )
            {
                tmp_convert = classlist_firstname + listArray[gradeYear] + classlist_lastname;
            }
            else
            {
                tmp_convert = classlist_firstname + "{" + ( gradeYear + 1 ) + "}" + classlist_lastname;
            }
            return tmp_convert;
        }

        internal bool ValidateNamingRule(string namingRule)
        {
            return namingRule.IndexOf('{') < namingRule.IndexOf('}');
        }

        internal bool ValidClassName(string className)
        {
            _loadingWait.WaitOne();
            if (string.IsNullOrEmpty(className)) return false;
            foreach (ClassInfo var in _ClassList.Values)
            {
                if (var.ClassName == className)
                    return false;
            }
            return true;
        }

        internal bool ValidClassName(string classid, string className)
        {
            _loadingWait.WaitOne();
            if (string.IsNullOrEmpty(className)) return false;
            foreach (ClassInfo var in _ClassList.Values)
            {
                if (var.ClassID != classid && var.ClassName == className)
                    return false;
            }
            return true;
        }

        public ClassInfoCollection Items
        {
            get
            {
                _loadingWait.WaitOne();
                return new ClassInfoCollection(_ClassList); 
            }
        }
    }
    public class DeleteClassEventArgs : EventArgs
    {
        private List<string> _deleteClassIDArray;
        
        public DeleteClassEventArgs()
        {
            _deleteClassIDArray = new List<string>();
        }

        public List<string> DeleteClassIDArray
        {
            get { return _deleteClassIDArray; }
            set { _deleteClassIDArray = value; }
        }
    }

    public class InsertClassEventArgs : EventArgs
    {
        private string _insertClassID;
        public string InsertClassID
        {
            get { return _insertClassID; }
            set { _insertClassID = value; }
        }
    }
    public class UpdateClassEventArgs : EventArgs
    {
        List<UpdateClassContent> _Items;
        public UpdateClassEventArgs()
        {
            _Items = new List<UpdateClassContent>();
        }
        public List<UpdateClassContent> Items
        {
            get { return _Items; }
        }
    }

    public class UpdateClassContent
    {
        private ClassInfo _OldClassInfo;
        private ClassInfo _NewClassInfo;
        internal UpdateClassContent(ClassInfo oldClassInfo, ClassInfo newClassInfo)
        {
            _OldClassInfo = oldClassInfo;
            _NewClassInfo = newClassInfo;
        }
        public ClassInfo OldClassInfo
        {
            get { return _OldClassInfo; }
        }
        public ClassInfo NewClassInfo
        {
            get { return _NewClassInfo; }
        }
    }
    //public partial class Class : BaseForm, IEntity, IPreference
    //{
    //    private SearchInfo _searchInfo;
    //    private static Class _Instance;
    //    public event EventHandler<DeleteClassEventArgs> ClassDeleted;
    //    public event EventHandler<InsertClassEventArgs> ClassInserted;

    //    public static Class Instance
    //    {
    //        get
    //        {
    //            if (_Instance == null)
    //                _Instance = new Class();
    //            return _Instance;
    //        }
    //    }
    //    private Class()
    //    {
    //        InitializeComponent();
    //        Initialize();
    //        PreferenceUpdater.Instance.Items.Add(this);
    //    }

    //    private void Initialize()
    //    {
    //        _searchInfo = new SearchInfo();
    //        ModifySearchRange(null, null);

    //        LoadtreeViewClass(null);
    //        treeViewClass.NodeMouseClick += new TreeNodeMouseClickEventHandler(treeViewClass_NodeMouseClick);

    //        XmlElement xmlpreference = CurrentUser.Instance.Preference["ClassProvider"];
    //        if (xmlpreference == null)
    //        {
    //            CurrentUser.Instance.Preference["ClassProvider"] = new XmlDocument().CreateElement("ClassProvider");
    //            xmlpreference = CurrentUser.Instance.Preference["ClassProvider"];
    //        }
    //        _preference = new CoursePreference(xmlpreference);
    //        bool expended = _preference.GetBoolean("ClassDetailExpended", true);
    //        if (expended)
    //        {
    //            buttonExpand.Text = ">>";
    //            buttonExpand.Tooltip = "最大化";
    //            splitterListDetial.Expanded = true;
    //            panelList.Dock = DockStyle.Left;
    //            panelDetial.Visible = true;
    //        }
    //        else
    //        {
    //            buttonExpand.Text = "<<";
    //            buttonExpand.Tooltip = "還原";
    //            splitterListDetial.Expanded = false;
    //            panelList.Dock = DockStyle.Fill;
    //            panelDetial.Visible = false;
    //        }
    //        colClassName.Visible = _preference.GetBoolean("colClassNameVisible", true);
    //        colTeacher.Visible = _preference.GetBoolean("colTeacherVisible", true);
    //        colDepartment.Visible = _preference.GetBoolean("colDepartmentVisible", false);
    //        colGradeYear.Visible = _preference.GetBoolean("colGradeYearVisible", false);
    //        colStudentCount.Visible = _preference.GetBoolean("colStudentCountVisible", true);
    //        LoadAllClassList();
    //    }

    //    void treeViewClass_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    //    {
    //        // 這邊還要做一些事情，讓他可以清除待處理中的資料
    //        if (e.Button == MouseButtons.Right && e.Node.Text.StartsWith("待處理")) return;

    //        FlushTreeNodeColor(treeViewClass.Nodes[0]);
    //        e.Node.BackColor = Color.CornflowerBlue;
    //        e.Node.ForeColor = Color.White;
    //        SelectedGradeYear(e.Node, null);
    //    }

    //    private void FlushTreeNodeColor(TreeNode node)
    //    {
    //        node.BackColor = treeViewClass.BackColor;
    //        node.ForeColor = treeViewClass.ForeColor;

    //        foreach (TreeNode cn in node.Nodes)
    //            FlushTreeNodeColor(cn);
    //    }

    //    private void SelectedGradeYear(TreeNode selectedNode, ClassBriefData data)
    //    {
    //        treeViewClass.SelectedNode = selectedNode;
    //        ChangeSelectedNodeStyle(selectedNode);
    //        if (selectedNode.Text.StartsWith("所有班級"))
    //        {
    //            _searchInfo.SearchMode = SearchMode.AllClass;
    //            txtSearch.WatermarkText = "搜尋所有班級";

    //            dgvClass.Rows.Clear();
    //            return;
    //        }
    //        dgvClass.Rows.Clear();

    //        //DSXmlHelper helper = null;
    //        Dictionary<string, ClassInfo> classInfoList = new Dictionary<string, ClassInfo>();

    //        if (selectedNode.Tag != null)
    //        {
    //            _searchInfo.GradeYear = selectedNode.Tag.ToString();
    //            if (selectedNode.Tag.ToString() == "")
    //            {
    //                txtSearch.WatermarkText = "在未分類班級中搜尋";
    //                _searchInfo.SearchMode = SearchMode.Undecision;
    //            }
    //            else
    //            {
    //                string className = "";
    //                foreach (char c in selectedNode.Text)
    //                {
    //                    if (c == '(')
    //                        break;
    //                    else
    //                        className = className + c;
    //                }
    //                txtSearch.WatermarkText = "在 " + className + " 中搜尋";
    //                _searchInfo.SearchMode = SearchMode.Normal;
    //            }
    //            string gradeYear = selectedNode.Tag.ToString();
    //            classInfoList = SmartSchool.Feature.Basic.Class.GetDetailClassListByGradeYear(gradeYear, "1");
    //            //helper = dsrsp.GetContent();
    //        }
    //        else if (selectedNode.Name == "待處理班級")
    //        {
    //            _searchInfo.SearchMode = SearchMode.Waiting;
    //            txtSearch.WatermarkText = "在 待處理班級 中搜尋";
    //            classInfoList = _waitingClassInfoList;
    //        }

    //        //else if (selectedNode.Text.StartsWith("刪除"))
    //        //{
    //        //    _searchInfo.SearchMode = SearchMode.DeletedClass;
    //        //    txtSearch.WatermarkText = "在刪除班級中搜尋";
    //        //    DSResponse dsrsp = SmartSchool.Feature.Basic.Class.GetDeletedClassList();
    //        //    helper = dsrsp.GetContent();
    //        //}

    //        //foreach (XmlElement element in helper.GetElements("Class"))
    //        //{
    //        //    int rowIndex = dgvClass.Rows.Add();
    //        //    DataGridViewRow row = dgvClass.Rows[rowIndex];
    //        //    row.Cells["colID"].Value = element.GetAttribute("ID");
    //        //    row.Cells["colClassName"].Value = element.GetAttribute("ClassName");
    //        //    row.Cells["colStudentCount"].Value = element.GetAttribute("StudentCount");
    //        //    row.Cells["colTeacher"].Value = element.GetAttribute("TeacherName");

    //        //    if (rowIndex == 0)
    //        //        row.Selected = false;

    //        //    if (data == null) continue;
    //        //    if (element.GetAttribute("ID") == data.ClassID)
    //        //        row.Selected = true;
    //        //}

    //        foreach (ClassInfo info in classInfoList.Values)
    //        {
    //            int rowIndex = dgvClass.Rows.Add();
    //            DataGridViewRow row = dgvClass.Rows[rowIndex];
    //            row.Cells["colID"].Value = info.ClassID;
    //            row.Cells["colClassName"].Value = info.ClassName;
    //            row.Cells["colStudentCount"].Value = info.StudentCount;
    //            row.Cells["colTeacher"].Value = info.Teacher;
    //            row.Cells["colDepartment"].Value = info.Department;
    //            row.Cells["colGradeYear"].Value = info.GradeYear;
    //            row.Tag = info;

    //            if (rowIndex == 0)
    //                row.Selected = false;

    //            if (data == null) continue;
    //            if (info.ClassID == data.ClassID)
    //                row.Selected = true;
    //        }
    //    }


    //    #region 取出左方listView的值
    //    /// <summary>
    //    /// 取出左方listView的值
    //    /// </summary>
    //    private void LoadtreeViewClass(ClassBriefData data)
    //    {
    //        pictureBox1.Visible = true;
    //        BackgroundWorker bgw = new BackgroundWorker();
    //        bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
    //        bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);

    //        if (data == null)
    //            bgw.RunWorkerAsync();
    //        else
    //            bgw.RunWorkerAsync(data);
    //    }

    //    void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    //    {
    //        ResultPackage pack = e.Result as ResultPackage;
    //        DSResponse dsrsp = pack.DSResponse;
    //        DSXmlHelper helper = dsrsp.GetContent();
    //        treeViewClass.Nodes.Clear();
    //        TreeNode root = treeViewClass.Nodes.Add(int.MaxValue.ToString(), "所有班級");
    //        root.Tag = int.MaxValue;

    //        //int deleteCount = 0;
    //        int aliveCount = 0;
    //        foreach (XmlElement element in helper.GetElements("GradeYear"))
    //        {
    //            //if (element.SelectSingleNode("Status").InnerText == "1")
    //            //{
    //                string gradeYear = element.SelectSingleNode("GradeYear").InnerText;
    //                string classCount = element.SelectSingleNode("ClassCount").InnerText;
    //                int c;
    //                if (int.TryParse(classCount, out c))
    //                    aliveCount += c;

    //                string display = "";
    //                if (string.IsNullOrEmpty(gradeYear))
    //                    display = "未分年級(" + classCount + ")";
    //                else
    //                    display = gradeYear + " 年級(" + classCount + ")";
    //                TreeNode node = root.Nodes.Add(gradeYear, display);
    //                node.Tag = gradeYear;
    //            //}
    //            //else
    //            //{
    //            //    int c;
    //            //    if (int.TryParse(element.SelectSingleNode("ClassCount").InnerText, out c))
    //            //        deleteCount += c;
    //            //}
    //        }
    //        root.Text = "所有班級(" + aliveCount + ")";
    //        // 處理刪除班級            
    //        //TreeNode deleteNode =  treeViewClass.Nodes.Add(int.MinValue.ToString(), "刪除班級(" + deleteCount + ")");
    //        //deleteNode.Tag = int.MinValue.ToString();

    //        TreeNode wNode = treeViewClass.Nodes.Add("待處理班級", "待處理班級("+_waitingClassInfoList.Count+")");


    //        pictureBox1.Visible = false;
    //        treeViewClass.ExpandAll();

    //        if (pack.Data != null)
    //        {
    //            //TreeNode[] nodes = treeViewClass.Nodes.Find(pack.Data.GradeYear, true);
    //            TreeNode node = FindNodeByTag(treeViewClass.Nodes[0], pack.Data.GradeYear);
    //            if (node != null)
    //                SelectedGradeYear(node, pack.Data);
    //            if (pack.Data.GradeYear == null)
    //                treeViewClass.SelectedNode = treeViewClass.Nodes[1];
    //        }
    //        //ReCountWaitingClassNode();
    //    }

    //    private Dictionary<string, ClassInfo> _waitingClassInfoList = new Dictionary<string, ClassInfo>();
    //    private void AddWaitingClass(ClassInfo classInfo)
    //    {
    //        if (!_waitingClassInfoList.ContainsKey(classInfo.ClassID))
    //            _waitingClassInfoList.Add(classInfo.ClassID, classInfo);
    //        //ReCountWaitingClassNode();
    //    }

    //    private void RemoveWaitingClass(ClassInfo classInfo)
    //    {
    //        if (_waitingClassInfoList == null) return;
    //        if (_waitingClassInfoList.ContainsKey(classInfo.ClassID))
    //            _waitingClassInfoList.Remove(classInfo.ClassID);
    //        //ReCountWaitingClassNode();
    //    }

    //    private void ReCountWaitingClassNode()
    //    {
    //        TreeNode wNode;
    //        TreeNode[] findNodes = treeViewClass.Nodes.Find("待處理班級", false);
    //        //if (findNodes.Length < 1 && _waitingClassInfoList.Keys.Count > 0)
    //        //    wNode = treeViewClass.Nodes.Add("待處理班級", "待處理班級");
    //        //else if (findNodes.Length > 0 && _waitingClassInfoList.Keys.Count > 0)
    //        //{
    //        wNode = findNodes[0];
    //        wNode.Text = "待處理班級 (" + _waitingClassInfoList.Count + ")";
    //        //}
    //        //else if (findNodes.Length > 0 && _waitingClassInfoList.Keys.Count == 0)
    //        //{
    //        //    wNode = findNodes[0];
    //        //    treeViewClass.Nodes.Remove(wNode);
    //        //}
    //    }

    //    private TreeNode FindNodeByTag(TreeNode node, string tagValue)
    //    {
    //        if (node.Tag == null && tagValue == null)
    //            return node;
    //        if (node.Tag != null && node.Tag.ToString() == tagValue)
    //            return node;
    //        TreeNode n;
    //        foreach (TreeNode cn in node.Nodes)
    //        {
    //            n = FindNodeByTag(cn, tagValue);
    //            if (n != null)
    //                return n;
    //        }
    //        return null;
    //    }

    //    void bgw_DoWork(object sender, DoWorkEventArgs e)
    //    {
    //        ResultPackage pack = new ResultPackage();
    //        pack.DSResponse = QueryClass.GetGradeYearList();
    //        pack.Data = e.Argument as ClassBriefData;
    //        e.Result = pack;
    //    }

    //    #endregion

    //    #region IEntity 成員

    //    string IEntity.Title
    //    {
    //        get { return "班級"; }
    //    }

    //    DevComponents.DotNetBar.NavigationPanePanel IEntity.NavPanPanel
    //    {
    //        get { return navigationPanePanel1; }
    //    }

    //    System.Windows.Forms.Panel IEntity.ContentPanel
    //    {
    //        get { return panelContent; }
    //    }

    //    System.Drawing.Image IEntity.Picture
    //    {
    //        get { return Properties.Resources.Navigation_Class; }
    //    }

    //    void IEntity.Actived()
    //    {

    //    }

    //    #endregion

    //    private void btnSearchAll_Click(object sender, EventArgs e)
    //    {
    //        _searchInfo.SearchMode = SearchMode.AllClass;

    //        txtSearch.WatermarkText = "搜尋所有班級";
    //        txtSearch.Text = "";
    //        txtSearch.Focus();
    //        toolTip1.SetToolTip(txtSearch, "輸入搜尋條件在所有班級中搜尋");
    //        toolTip1.Show("輸入搜尋條件在所有班級中搜尋", txtSearch.TopLevelControl, 20 - (txtSearch.PointToClient(new Point(0, 0)).X - txtSearch.TopLevelControl.PointToClient(new Point(0, 0)).X), 10 - (txtSearch.PointToClient(new Point(0, 0)).Y - txtSearch.TopLevelControl.PointToClient(new Point(0, 0)).Y), 2000);

    //        if (treeViewClass.SelectedNode != null)
    //        {
    //            treeViewClass.SelectedNode.BackColor = treeViewClass.BackColor;
    //            treeViewClass.SelectedNode.ForeColor = treeViewClass.ForeColor;
    //            treeViewClass.SelectedNode = null;
    //        }
    //    }

    //    private void treeViewClass_BeforeSelect(object sender, TreeViewCancelEventArgs e)
    //    {
    //        ChangeSelectedNodeStyle(e.Node);
    //    }

    //    private void ChangeSelectedNodeStyle(TreeNode e)
    //    {
    //        if (treeViewClass.SelectedNode != null)
    //        {
    //            treeViewClass.SelectedNode.BackColor = treeViewClass.BackColor;
    //            treeViewClass.SelectedNode.ForeColor = treeViewClass.ForeColor;
    //        }
    //        e.BackColor = Color.CornflowerBlue;
    //        e.ForeColor = Color.White;
    //    }

    //    private void btnSearch_Click(object sender, EventArgs e)
    //    {
    //        BackgroundWorker sbgw = new BackgroundWorker();
    //        sbgw.DoWork += new DoWorkEventHandler(sbgw_DoWork);
    //        sbgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(sbgw_RunWorkerCompleted);
    //        sbgw.RunWorkerAsync();
    //    }

    //    void sbgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    //    {
    //        Dictionary<string, ClassInfo> classInfoList = (Dictionary<string, ClassInfo>)e.Result;

    //        dgvClass.Rows.Clear();
    //        foreach (ClassInfo info in classInfoList.Values)
    //        {
    //            int rowIndex = dgvClass.Rows.Add();
    //            DataGridViewRow row = dgvClass.Rows[rowIndex];
    //            row.Cells["colID"].Value = info.ClassID;
    //            row.Cells["colClassName"].Value = info.ClassName;
    //            row.Cells["colStudentCount"].Value = info.StudentCount;
    //            row.Cells["colTeacher"].Value = info.Teacher;
    //            row.Cells["colGradeYear"].Value = info.GradeYear;
    //            row.Cells["colDepartment"].Value = info.Department;
    //            row.Tag = info;

    //            if (rowIndex == 0)
    //                row.Selected = false;
    //        }
    //    }

    //    void sbgw_DoWork(object sender, DoWorkEventArgs e)
    //    {
    //        ISearchRequest searchRequest = SearchRequetFactory.CreateInstance(_searchInfo.SearchMode);
    //        DSRequest request = searchRequest.GetRequest(_searchInfo);
    //        e.Result = SearchResponseFactory.CreateInstance(_searchInfo.SearchMode, _waitingClassInfoList).GetResponse(request);
    //    }

    //    private void txtSearch_TextChanged(object sender, EventArgs e)
    //    {
    //        _searchInfo.PreparedSearchText = txtSearch.Text;
    //    }

    //    private void ModifySearchRange(object sender, CheckBoxChangeEventArgs e)
    //    {
    //        _searchInfo.SearchRangeCollection.Clear();

    //        if (chkSearchInClassName.Checked)
    //            _searchInfo.SearchRangeCollection.Add(SearchRange.ClassName);

    //        if (chkSearchInTeacher.Checked)
    //            _searchInfo.SearchRangeCollection.Add(SearchRange.TeacherName);
    //    }

    //    private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
    //    {
    //        if (e.KeyChar == 13)
    //            btnSearch_Click(null, null);
    //    }

    //    private ClassInfoPanel _classInfo;
    //    private void dgvClass_SelectionChanged(object sender, EventArgs e)
    //    {
    //        if (dgvClass.SelectedRows.Count == 0)
    //        {
    //            ClassUDMediator.OnSelectedClassChange(false);
    //            return;
    //        }

    //        if (dgvClass.SelectedRows[0].Cells["colID"].Value == null)
    //        {
    //            ClassUDMediator.OnSelectedClassChange(false);
    //            return;
    //        }
    //        ClassUDMediator.OnSelectedClassChange(true);
    //        string classid = dgvClass.SelectedRows[0].Cells["colID"].Value.ToString();
    //        if (_classInfo == null)
    //        {
    //            _classInfo = new ClassInfoPanel();
    //            _classInfo.Dock = DockStyle.Fill;
    //            _classInfo.TabIndex = 5;
    //            this.panelDetial.Controls.Add(_classInfo);
    //            ClassUDMediator.Palmerworm = _classInfo;
    //        }
    //        _classInfo.Initialize(classid);
    //    }

    //    public string[] SelectedClassIDCollection
    //    {
    //        get
    //        {
    //            List<string> list = new List<string>();
    //            foreach (DataGridViewRow row in dgvClass.SelectedRows)
    //            {
    //                string id = row.Cells["colID"].Value.ToString();
    //                list.Add(id);
    //            }
    //            return list.ToArray();
    //        }
    //    }

    //    public string GradeYear
    //    {
    //        get
    //        {
    //            if (treeViewClass.SelectedNode == null)
    //                return null;
    //            if (treeViewClass.SelectedNode.Tag != null)
    //                return treeViewClass.SelectedNode.Tag.ToString();
    //            else
    //                return null;
    //        }
    //    }

    //    public event EventHandler<ClassBriefData> BriefDataChanged;
    //    public void InvokBriefDataChanged(object sender, ClassBriefData classBriefData)
    //    {
    //        ReloadSomething(classBriefData);
    //        if (BriefDataChanged != null)
    //            BriefDataChanged.Invoke(sender, classBriefData);
    //    }

    //    private void ReloadSomething(ClassBriefData data)
    //    {
    //        LoadtreeViewClass(data);
    //    }

    //    public void OnClassDeleted(string[] classid)
    //    {
    //        if (ClassDeleted != null)
    //        {
    //            DeleteClassEventArgs arg = new DeleteClassEventArgs();
    //            arg.DeleteClassIDArray = classid;
    //            ClassDeleted(this, arg);
    //        }
    //    }

    //    public void OnClassInsert(string classid)
    //    {
    //        if (ClassInserted != null)
    //        {
    //            ClassBriefData data = new ClassBriefData();
    //            data.ClassID = classid;
    //            data.GradeYear = "";
    //            LoadtreeViewClass(data);
    //            Class.Instance.LoadAllClassList();
    //            InsertClassEventArgs arg = new InsertClassEventArgs();
    //            arg.InsertClassID = classid;
    //            ClassInserted(this, arg);
    //        }
    //    }

    //    private void dgvClass_MouseClick(object sender, MouseEventArgs e)
    //    {
    //        if (e.Button != MouseButtons.Right) return;
    //        if (dgvClass.SelectedRows.Count == 0) return;
    //        if (treeViewClass.SelectedNode.Name == "待處理班級")
    //        {
    //            btnRemoveWaiting.Visible = true;
    //            btnAddWaiting.Visible = false;
    //        }
    //        else
    //        {
    //            btnRemoveWaiting.Visible = false;
    //            btnAddWaiting.Visible = true;
    //        }
    //    }

    //    private void dgvClass_MouseDoubleClick(object sender, MouseEventArgs e)
    //    {
    //        if (dgvClass.SelectedRows.Count == 0)
    //        {
    //            ClassUDMediator.OnSelectedClassChange(false);
    //            return;
    //        }

    //        if (dgvClass.SelectedRows[0].Cells["colID"].Value == null)
    //        {
    //            ClassUDMediator.OnSelectedClassChange(false);
    //            return;
    //        }
    //        ClassUDMediator.OnSelectedClassChange(true);
    //        string classid = dgvClass.SelectedRows[0].Cells["colID"].Value.ToString();
    //        string className = "班級資訊【" + dgvClass.SelectedRows[0].Cells["colClassName"].Value.ToString() + "】";
    //        PopupClassForm(classid, className);
    //    }

    //    void btnAddWaiting_Click(object sender, EventArgs e)
    //    {
    //        foreach (DataGridViewRow row in dgvClass.SelectedRows)
    //        {
    //            ClassInfo info = row.Tag as ClassInfo;
    //            AddWaitingClass(info);
    //        }
    //        ReCountWaitingClassNode();
    //    }

    //    void btnRemoveWaiting_Click(object sender, EventArgs e)
    //    {
    //        foreach (DataGridViewRow row in dgvClass.SelectedRows)
    //        {
    //            dgvClass.Rows.Remove(row);
    //            ClassInfo info = row.Tag as ClassInfo;
    //            RemoveWaitingClass(info);
    //        }
    //        ReCountWaitingClassNode();
    //    }

    //    private CoursePreference _preference;
    //    private void buttonExpand_Click(object sender, EventArgs e)
    //    {
    //        panelDetial.Visible = false;

    //        if (splitterListDetial.Expanded)
    //        {
    //            buttonExpand.Text = "<<";
    //            buttonExpand.Tooltip = "還原";
    //            splitterListDetial.Expanded = false;

    //            for (int i = 1; i < dgvClass.Columns.Count; i++)
    //            {
    //                dgvClass.Columns[i].Visible = true;
    //            }
    //        }
    //        else
    //        {              
    //            buttonExpand.Text = ">>";
    //            buttonExpand.Tooltip = "最大化";
    //            splitterListDetial.Expanded = true;                

    //            //for (int i = 1; i < dgvClass.Columns.Count; i++)
    //            //{
    //            //    dgvClass.Columns[i].Visible = false;
    //            //}

    //            #region 讀取設定檔


    //            #region 設定欄位顯示隱藏(只有當狀態是收合時設定)
    //            colClassName.Visible = true;
    //            colTeacher.Visible = true;
    //            colDepartment.Visible = false;
    //            colGradeYear.Visible = false;
    //            colStudentCount.Visible = true;

    //            panelDetial.Visible = true;
    //            //colClassName.Visible = _preference.GetBoolean("colClassNameVisible", true);
    //            //colTeacher.Visible = _preference.GetBoolean("colTeacherVisible", true);
    //            //colDepartment.Visible = _preference.GetBoolean("colDepartmentVisible", false);
    //            //colGradeYear.Visible = _preference.GetBoolean("colGradeYearVisible", false);
    //            //colStudentCount.Visible = _preference.GetBoolean("colStudentCountVisible", true);
    //            #endregion

    //            #endregion
    //        }
    //        //寫入紀錄(目前顯示欄位)
    //        ((IPreference)this).UpdatePreference();
    //    }

    //    #region IPreference 成員

    //    public void UpdatePreference()
    //    {
    //        _preference.SetBoolean("ClassDetailExpended", panelDetial.Visible);
    //        _preference.SetBoolean("colClassNameVisible", colClassName.Visible);
    //        _preference.SetBoolean("colTeacherVisible", colTeacher.Visible);
    //        _preference.SetBoolean("colDepartmentVisible", colDepartment.Visible);
    //        _preference.SetBoolean("colGradeYearVisible", colGradeYear.Visible);
    //        _preference.SetBoolean("colStudentCountVisible", colStudentCount.Visible);
    //    }

    //    #endregion

    //    private Dictionary<string, PopupClassForm> _forms;
    //    public void PopupClassForm(string classid, string caption)
    //    {
    //        if (_forms == null)
    //            _forms = new Dictionary<string, PopupClassForm>();
    //        PopupClassForm form;
    //        if (_forms.ContainsKey(classid))
    //            form = _forms[classid];
    //        else
    //        {
    //            form = new PopupClassForm(classid);
    //            form.Size = new Size(570, 600);
    //            form.Text = caption;
    //            form.ShowIcon = false;
    //            form.StartPosition = FormStartPosition.CenterParent;
    //            form.ClassFormClosing += new EventHandler<ClassFormClosingEventArgs>(form_ClassFormClosing);
    //            _forms.Add(classid, form);
    //        }
    //        form.Show();
    //        form.Focus();
    //    }

    //    void form_ClassFormClosing(object sender, ClassFormClosingEventArgs e)
    //    {
    //        if (_forms.ContainsKey(e.ClassID))
    //            _forms.Remove(e.ClassID);
    //    }

    //    public void OnClassDeleted(string classid)
    //    {
    //        if (_forms.ContainsKey(classid))
    //            _forms[classid].Close();
    //    }

    //    public void ReloadPopupForm(string classid)
    //    {
    //        if (_forms.ContainsKey(classid))
    //            _forms[classid].Reload();
    //    }

    //    private void splitterListDetial_ExpandedChanging(object sender, ExpandedChangeEventArgs e)
    //    {
    //        panelContent.SuspendLayout();
    //        panelList.SuspendLayout();
    //        panelDetial.SuspendLayout();
    //        if (splitterListDetial.Expanded)
    //        {
    //            splitterListDetial.Dock = DockStyle.Right;
    //            panelList.Dock = DockStyle.Fill;
    //            splitterListDetial.Enabled = false;
    //        }
    //        else
    //        {
    //            panelList.Dock = DockStyle.Left;
    //            splitterListDetial.Dock = DockStyle.Left;
    //            splitterListDetial.Enabled = true;
    //        }
    //    }

    //    private void splitterListDetial_ExpandedChanged(object sender, ExpandedChangeEventArgs e)
    //    {
    //        panelList.ResumeLayout();
    //        panelDetial.ResumeLayout();
    //        panelContent.ResumeLayout();
    //    }

    //    public Dictionary<string, ClassInfo> GetWaitingClassList
    //    {
    //        get { return _waitingClassInfoList; }
    //    }

    //    public ClassSelector GetSelectionControl()
    //    {
    //        DSResponse dsrsp = Feature.Class.QueryClass.GetAllClass();
    //        DSXmlHelper rsphelper = dsrsp.GetContent();
    //        List<DetailClassInfo> list = new List<DetailClassInfo>();            
    //        foreach (XmlElement element in rsphelper.GetElements("Class"))
    //        {
    //            DetailClassInfo info = new DetailClassInfo(element);
    //            list.Add(info);
    //        }
    //        ClassSelector cs = new ClassSelector();
    //        cs.SetSource(list);
    //        return cs;
    //    }

    //    #region 處理取得所有班級名單
    //    private Dictionary<string, string> _allClassList;

    //    public Dictionary<string, string> AllClassList
    //    {
    //        get { return _allClassList; }           
    //    }

    //    private bool _allClassListLoadFinished = true;

    //    public void LoadAllClassList()
    //    {
    //        _allClassListLoadFinished = false;
    //        _allClassList = new Dictionary<string, string>();
    //        BackgroundWorker worker = new BackgroundWorker();
    //        worker.DoWork += new DoWorkEventHandler(worker_DoWork);
    //        worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
    //        worker.RunWorkerAsync();
    //    }

    //    void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    //    {
    //        DSResponse dsrsp = e.Result as DSResponse;
    //        DSXmlHelper helper = dsrsp.GetContent();

    //        foreach (XmlElement element in helper.GetElements("Class"))
    //            _allClassList.Add(element.GetAttribute("ID"), element.SelectSingleNode("ClassName").InnerText);
    //        _allClassListLoadFinished = true;            
    //    }

    //    void worker_DoWork(object sender, DoWorkEventArgs e)
    //    {
    //        e.Result = QueryClass.GetClassList();
    //    }

    //    public bool ValidClassName(string className)
    //    {
    //        if (string.IsNullOrEmpty(className)) return false;

    //        while (_allClassListLoadFinished)
    //        {
    //            return !_allClassList.ContainsValue(className);
    //        }
    //        return true;
    //    }

    //    public bool ValidClassName(string classid, string className)
    //    {
    //        if (string.IsNullOrEmpty(className)) return false;

    //        while (_allClassListLoadFinished)
    //        {
    //            foreach (string key in _allClassList.Keys)
    //            {
    //                string name = _allClassList[key];
    //                if (name == className && classid != key)
    //                    return false;
    //            }
    //            return true;
    //        }
    //        return true;
    //    }
    //    #endregion
    //}

    //#region others
    //public enum SearchMode
    //{
    //    AllClass, DeletedClass, Undecision, Normal, Waiting
    //}

    //public enum SearchRange
    //{
    //    TeacherName, ClassName
    //}

    //public class SearchInfo
    //{
    //    private SearchMode _searchMode;
    //    public SearchMode SearchMode
    //    {
    //        get { return _searchMode; }
    //        set { _searchMode = value; }
    //    }
    //    private string _gradeYear;
    //    public string GradeYear
    //    {
    //        get { return _gradeYear; }
    //        set { _gradeYear = value; }
    //    }
    //    public SearchInfo()
    //    {
    //        _searchMode = SearchMode.AllClass;
    //        _gradeYear = "";
    //        _searchRangeCollection = new List<SearchRange>();
    //        _searchText = "";
    //    }
    //    private List<SearchRange> _searchRangeCollection;
    //    public List<SearchRange> SearchRangeCollection
    //    {
    //        get { return _searchRangeCollection; }
    //        set { _searchRangeCollection = value; }
    //    }
    //    private string _searchText;
    //    public string PreparedSearchText
    //    {
    //        get { return "*" + _searchText + "*"; }
    //        set { _searchText = value; }
    //    }

    //    public string SearchText
    //    {
    //        get { return _searchText; }
    //        set { _searchText = value; }
    //    }
    //}

    //public interface ISearchRequest
    //{
    //    DSRequest GetRequest(SearchInfo searchInfo);
    //}

    //public class AllClassSearchRequest : ISearchRequest
    //{
    //    #region ISearchRequest 成員

    //    public DSRequest GetRequest(SearchInfo searchInfo)
    //    {
    //        DSXmlHelper helper = new DSXmlHelper("GetClassListRequest");
    //        helper.AddElement("Field");
    //        helper.AddElement("Field", "All");
    //        helper.AddElement("Condition");
    //        //helper.AddElement("Condition", "Status", "1");
    //        if (searchInfo.SearchRangeCollection.Count > 0)
    //        {
    //            helper.AddElement("Condition", "Or");
    //            if (searchInfo.SearchRangeCollection.Contains(SearchRange.TeacherName))
    //                helper.AddElement("Condition/Or", "TeacherName", searchInfo.PreparedSearchText);
    //            if (searchInfo.SearchRangeCollection.Contains(SearchRange.ClassName))
    //                helper.AddElement("Condition/Or", "ClassName", searchInfo.PreparedSearchText);
    //        }
    //        return new DSRequest(helper);
    //    }

    //    #endregion
    //}

    //public class OneClassSearchRequest : ISearchRequest
    //{
    //    #region ISearchRequest 成員

    //    public DSRequest GetRequest(SearchInfo searchInfo)
    //    {
    //        DSXmlHelper helper = new DSXmlHelper("GetClassListRequest");
    //        helper.AddElement("Field");
    //        helper.AddElement("Field", "All");
    //        helper.AddElement("Condition");
    //        //helper.AddElement("Condition", "Status", "1");
    //        helper.AddElement("Condition", "GradeYear", searchInfo.GradeYear);
    //        if (searchInfo.SearchRangeCollection.Count > 0)
    //        {
    //            helper.AddElement("Condition", "Or");
    //            if (searchInfo.SearchRangeCollection.Contains(SearchRange.TeacherName))
    //                helper.AddElement("Condition/Or", "TeacherName", searchInfo.PreparedSearchText);
    //            if (searchInfo.SearchRangeCollection.Contains(SearchRange.ClassName))
    //                helper.AddElement("Condition/Or", "ClassName", searchInfo.PreparedSearchText);
    //        }
    //        return new DSRequest(helper);
    //    }

    //    #endregion
    //}

    //public class DeletedClassSearchRequest : ISearchRequest
    //{
    //    #region ISearchRequest 成員

    //    public DSRequest GetRequest(SearchInfo searchInfo)
    //    {
    //        DSXmlHelper helper = new DSXmlHelper("GetClassListRequest");
    //        helper.AddElement("Field");
    //        helper.AddElement("Field", "All");
    //        helper.AddElement("Condition");
    //        //helper.AddElement("Condition", "Status", "255");
    //        if (searchInfo.SearchRangeCollection.Count > 0)
    //        {
    //            helper.AddElement("Condition", "Or");
    //            if (searchInfo.SearchRangeCollection.Contains(SearchRange.TeacherName))
    //                helper.AddElement("Condition/Or", "TeacherName", searchInfo.PreparedSearchText);
    //            if (searchInfo.SearchRangeCollection.Contains(SearchRange.ClassName))
    //                helper.AddElement("Condition/Or", "ClassName", searchInfo.PreparedSearchText);
    //        }
    //        return new DSRequest(helper);
    //    }

    //    #endregion
    //}

    //public class UndecidedClassSearchRequest : ISearchRequest
    //{
    //    #region ISearchRequest 成員

    //    public DSRequest GetRequest(SearchInfo searchInfo)
    //    {
    //        DSXmlHelper helper = new DSXmlHelper("GetClassListRequest");
    //        helper.AddElement("Field");
    //        helper.AddElement("Field", "All");
    //        helper.AddElement("Condition");
    //        //helper.AddElement("Condition", "Status", "1");
    //        helper.AddElement("Condition", "GradeYear", "");
    //        if (searchInfo.SearchRangeCollection.Count > 0)
    //        {
    //            helper.AddElement("Condition", "Or");
    //            if (searchInfo.SearchRangeCollection.Contains(SearchRange.TeacherName))
    //                helper.AddElement("Condition/Or", "TeacherName", searchInfo.PreparedSearchText);
    //            if (searchInfo.SearchRangeCollection.Contains(SearchRange.ClassName))
    //                helper.AddElement("Condition/Or", "ClassName", searchInfo.PreparedSearchText);
    //        }
    //        return new DSRequest(helper);
    //    }

    //    #endregion
    //}

    //public class WaitingClassSearchRequest : ISearchRequest
    //{

    //    #region ISearchRequest 成員

    //    public DSRequest GetRequest(SearchInfo searchInfo)
    //    {
    //        DSXmlHelper helper = new DSXmlHelper("Root");
    //        //helper.AddElement("SearchText",searchInfo.SearchText);
    //        if (searchInfo.SearchRangeCollection.Count > 0)
    //        {
    //            if (searchInfo.SearchRangeCollection.Contains(SearchRange.TeacherName))
    //                helper.AddElement(".", "TeacherName", searchInfo.SearchText);
    //            if (searchInfo.SearchRangeCollection.Contains(SearchRange.ClassName))
    //                helper.AddElement(".", "ClassName", searchInfo.SearchText);
    //        }
    //        return new DSRequest(helper);
    //    }

    //    #endregion
    //}

    //public class SearchRequetFactory
    //{
    //    public static ISearchRequest CreateInstance(SearchMode mode)
    //    {
    //        switch (mode)
    //        {
    //            case SearchMode.AllClass:
    //                return new AllClassSearchRequest();
    //            case SearchMode.DeletedClass:
    //                return new DeletedClassSearchRequest();
    //            case SearchMode.Normal:
    //                return new OneClassSearchRequest();
    //            case SearchMode.Undecision:
    //                return new UndecidedClassSearchRequest();
    //            case SearchMode.Waiting:
    //                return new WaitingClassSearchRequest();
    //            default:
    //                return new AllClassSearchRequest();
    //        }
    //    }
    //}

    //public interface ISearchResponse
    //{
    //    object Argument { get;set;}
    //    Dictionary<string, ClassInfo> GetResponse(DSRequest request);
    //}

    //public class DSASearchResponse : ISearchResponse
    //{
    //    #region ISearchResponse 成員
    //    public object Argument { get { return null; } set { } }
    //    public Dictionary<string, ClassInfo> GetResponse(DSRequest request)
    //    {
    //        return QueryClass.SearchClass(request);
    //    }

    //    #endregion
    //}

    //public class WaitingSearchResponse : ISearchResponse
    //{
    //    #region ISearchResponse 成員
    //    private Dictionary<string, ClassInfo> _source;
    //    public object Argument
    //    {
    //        get { return _source; }
    //        set
    //        {
    //            try
    //            {
    //                _source = (Dictionary<string, ClassInfo>)value;
    //            }
    //            catch (Exception)
    //            {
    //                throw new Exception("WaitingSearchResponse 的 Argument 必須是 Dictionary<string,ClassInfo> 型別");
    //            }
    //        }
    //    }
    //    public Dictionary<string, ClassInfo> GetResponse(DSRequest request)
    //    {
    //        if (_source == null)
    //            throw new Exception("WaitingSearchResponse 必須先設定Argument 屬性");

    //        Dictionary<string, ClassInfo> result = new Dictionary<string, ClassInfo>();
    //        DSXmlHelper helper = request.GetContent();
    //        string tText = helper.GetText("TeacherName").Replace(@"\", @"\\");
    //        string cText = helper.GetText("ClassName").Replace(@"\", @"\\");

    //        Regex treg = new Regex(tText);
    //        Regex creg = new Regex(cText);
    //        foreach (ClassInfo info in _source.Values)
    //        {
    //            if (!string.IsNullOrEmpty(tText))
    //            {
    //                Match m = treg.Match(info.Teacher);
    //                if (m.Success && !result.ContainsKey(info.ClassID))
    //                    result.Add(info.ClassID, info);
    //            }
    //            if (!string.IsNullOrEmpty(cText))
    //            {
    //                Match m = creg.Match(info.ClassName);
    //                if (m.Success && !result.ContainsKey(info.ClassID))
    //                    result.Add(info.ClassID, info);
    //            }
    //        }
    //        return result;
    //    }

    //    #endregion
    //}

    //public class SearchResponseFactory
    //{
    //    public static ISearchResponse CreateInstance(SearchMode mode, object Argument)
    //    {
    //        ISearchResponse searcher;
    //        if (mode == SearchMode.Waiting)
    //            searcher = new WaitingSearchResponse();
    //        else
    //            searcher = new DSASearchResponse();
    //        searcher.Argument = Argument;
    //        return searcher;
    //    }
    //    public static ISearchResponse CreateInstance(SearchMode mode)
    //    {
    //        return CreateInstance(mode);
    //    }
    //}

    //public class ClassBriefData : EventArgs
    //{
    //    private string _classID;

    //    public string ClassID
    //    {
    //        get { return _classID; }
    //        set { _classID = value; }
    //    }

    //    private string _className;

    //    public string ClassName
    //    {
    //        get { return _className; }
    //        set { _className = value; }
    //    }

    //    private string _gradeYear;

    //    public string GradeYear
    //    {
    //        get { return _gradeYear; }
    //        set { _gradeYear = value; }
    //    }

    //    private string _teacherName;

    //    public string TeacherName
    //    {
    //        get { return _teacherName; }
    //        set { _teacherName = value; }
    //    }

    //    private string _displayOrder;

    //    public string DisplayOrder
    //    {
    //        get { return _displayOrder; }
    //        set { _displayOrder = value; }
    //    }

    //    private int _studentCount;

    //    public int StudentCount
    //    {
    //        get { return _studentCount; }
    //        set { _studentCount = value; }
    //    }
    //}

    //public class ResultPackage
    //{
    //    private DSResponse _dsresponse;

    //    public DSResponse DSResponse
    //    {
    //        get { return _dsresponse; }
    //        set { _dsresponse = value; }
    //    }
    //    private ClassBriefData _data;

    //    public ClassBriefData Data
    //    {
    //        get { return _data; }
    //        set { _data = value; }
    //    }
    //}
    //#endregion

}