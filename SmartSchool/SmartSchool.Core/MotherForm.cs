using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.Common;
using DevComponents.DotNetBar.Rendering;
//using SmartSchool.StudentRelated.Process.StudentIUD;
//using SmartSchool.StudentRelated;
using System.Xml;
//using SmartSchool.ClassRelated;
//using SmartSchool.TeacherRelated;
//using SmartSchool.StudentRelated.RibbonBars;
using System.Diagnostics;
//using SmartSchool.CouseRelated;
using SmartSchool.ApplicationLog.Forms;
using SmartSchool.UserInfomation;
using System.Threading;
using SmartSchool.Security;
using SmartSchool.AccessControl;
using System.Reflection;
using SmartSchool.Feedback;

namespace SmartSchool
{

    public partial class MotherForm : Office2007RibbonForm, IPreference
    {
        private eOffice2007ColorScheme _ColorSchema;

        private Dictionary<string, RibbonTabItem> _ProcessTabDictionary = new Dictionary<string, RibbonTabItem>();
        private Dictionary<string, List<IProcess>> _ProcessListDictionary = new Dictionary<string, List<IProcess>>();
        private Dictionary<string, bool> _ProcessLoadedDictionary = new Dictionary<string, bool>();
        private Dictionary<string, List<IProcess>> _ProcessAllListDictionary = new Dictionary<string, List<IProcess>>();
        private Dictionary<string, bool> _ProcessShowSecTab = new Dictionary<string, bool>();

        private Image _NormalImage = Properties.Resources.Icon;// ((System.Drawing.Image)(new System.ComponentModel.ComponentResourceManager(typeof(MotherForm)).GetObject("office2007StartButton2.Image")));
        private Image _LoadingImage = Properties.Resources.寬箭頭;
        private Image _ErrorImage = Properties.Resources.紅箭頭;
        private Image _ShadowImage = Properties.Resources.寬箭頭陰影;

        static private int _MinimalLoadingTime = 0;
        static private bool _Closed = false;
        static private Thread _MainThread = null;
        static private bool _SayByeBye = false;
        static private bool _JestLeave = false;
        static private bool _MessageBoxShown = false;
        static private int _NetWorkingCount = 0;
        static private bool _Successed = true;
        static private Bitmap b;
        static private Graphics g, gshadow;
        static private float _Speed = 15;
        static private ulong _Bytes = 0;
        static private ulong _CostTimes = 0;
        static private string _ExceptionMessage = "";

        private int WaitCursorCount;

        static private AutoResetEvent _InitWait = new AutoResetEvent(true);
        static internal MotherForm _Instance;
        static public MotherForm Instance
        {
            get
            {
                _InitWait.WaitOne();
                if (_Instance == null)
                {

                    _Instance = new MotherForm();
                    _InitWait.Set();
                    //EntityFactory _EntityFactory = new EntityFactory();

                    //加入Entity
                    //foreach (IEntity var in _EntityFactory.BuildEntities())
                    //{
                    //    _Instance.AddEntity(var);
                    //}

                    /*加入Process*/

                    ////學生相關 Ribbon
                    //_Instance.AddProcess(StudentIDUProcess.Instance);

                    ////班級相關 Ribbon
                    //_Instance.AddProcess(SmartSchool.ClassRelated.RibbonBars.Manage.Instance);

                    ////教師相關 Ribbon
                    //_Instance.AddProcess(SmartSchool.TeacherRelated.RibbonBars.Manage.Instance);

                    ////課程相關 Ribbon
                    //_Instance.AddProcess(SmartSchool.CouseRelated.RibbonBars.Manage.Instance);

                    ////設定Customization資料介面使用的的InformationProvider
                    //Customization.Data.AccessHelper.SetStudentProvider(new API.Provider.StudentProvider());
                    //Customization.Data.AccessHelper.SetClassProvider(new API.Provider.ClassProvider());
                    //Customization.Data.AccessHelper.SetTeacherProvider(new API.Provider.TeacherProvider());
                    //Customization.Data.AccessHelper.SetCourseProvider(new API.Provider.CourseProvider());
                    //Customization.Data.SystemInformation.SetProvider(new API.Provider.SystemProvider());
                    //Customization.PlugIn.ExtendedContent.ExtendStudentContent.SetManager(StudentRelated.PalmerwormFactory.Instence);
                }
                else
                {
                    _InitWait.Set();
                }
                return _Instance;
            }
        }

        private MotherForm()
        {
            InitializeComponent();
            _MainThread = Thread.CurrentThread;
            timer1_Tick(null, null);
            this.navigationPane1.TitlePanel.Text = "";
            PreferenceUpdater.Instance.Items.Add(this);
            this.Font = FontStyles.General;

            b = new Bitmap(30, 30);
            g = Graphics.FromImage(b);
            g.TranslateTransform(14.2F, 14.5F);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gshadow = Graphics.FromImage(b);
            gshadow.TranslateTransform(15.7F, 15.5F);
            timer1.Start();

            Customization.PlugIn.Global.OnSetStatusBarMessage += new EventHandler<SmartSchool.Customization.PlugIn.Global.SetStatusBarMessageEventArgs>(Global_OnSetStatusBarMessage);

            #region 權限判斷
            
            //角色權限管理
            btnRoleManagement.Enabled = CurrentUser.Acl[typeof(RoleManager)].Executable || CurrentUser.Instance.IsSysAdmin;

            //使用者管理
            btnUserManagement.Enabled = CurrentUser.Acl[typeof(UserManager)].Executable || CurrentUser.Instance.IsSysAdmin;

            //變更密碼
            btnChangePassword.Enabled = CurrentUser.Acl[typeof(UserInfoManager)].Executable;
            
            //編輯學校資訊
            btnSchoolInfo.Enabled = CurrentUser.Acl[typeof(SchoolInfoEditor)].Executable;
            
            Type feature_type = typeof(FeatureCodeAttribute);
            ConstructorInfo ctor_type;

            //查詢個人日誌
            ctor_type = typeof(LogBrowserForm).GetConstructor(new Type[] { typeof(string) });
            btnQueryLog.Enabled = CurrentUser.Acl[(Attribute.GetCustomAttribute(ctor_type, feature_type) as FeatureCodeAttribute).FeatureCode].Executable;

            //查詢所有日誌
            ctor_type = typeof(LogBrowserForm).GetConstructor(Type.EmptyTypes);
            btnAllLog.Enabled = CurrentUser.Acl[(Attribute.GetCustomAttribute(ctor_type, feature_type) as FeatureCodeAttribute).FeatureCode].Executable;

            #endregion 
        }

        private void Global_OnSetStatusBarMessage(object sender, SmartSchool.Customization.PlugIn.Global.SetStatusBarMessageEventArgs e)
        {
            if (e.HasProgress)
                this.SetBarMessage(e.Message, e.Progress);
            else
                this.SetBarMessage(e.Message);
        }

        private void MotherForm_Load(object sender, EventArgs e)
        {
            //避免顯示錯誤情形
            navigationPane1.RecalcLayout();
            navigationPane1.NavigationBarHeight = 240;
            //自動選取
            foreach (BaseItem var in ribbonControl1.Items)
            {
                //找RibbonTabItem
                if (var is RibbonTabItem)
                {
                    if (var.Tag is EntityItem)
                    {
                        //如果是Entity的Tab
                        ((EntityItem)var.Tag).NavButton.Checked = true;
                        break;
                    }
                    else
                    {
                        //如果不是Entity的Tab
                        ribbonControl1.SelectedRibbonTabItem = (RibbonTabItem)var;
                        break;
                    }
                }
            }

            #region 讀取Preference資料
            this.SuspendLayout();
            XmlElement PreferenceData = CurrentUser.Instance.Preference["MotherForm"];
            if (PreferenceData != null)
            {
                //顯示項目高度
                if (PreferenceData.HasAttribute("NavigationBarHeight"))
                {
                    int height = 240;
                    if (int.TryParse(PreferenceData.Attributes["NavigationBarHeight"].Value, out height))
                        this.navigationPane1.NavigationBarHeight = height;
                }
                //是否最大化
                if ( PreferenceData.HasAttribute("Maximized") && PreferenceData.GetAttribute("Maximized") == "True" )
                {
                    this.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    //如果沒有最大化則嘗試讀取高跟寬
                    if ( PreferenceData.HasAttribute("Height") )
                    {
                        int height;
                        if ( int.TryParse(PreferenceData.GetAttribute("Height"), out height) )
                            this.Height = height;
                    }
                    if ( PreferenceData.HasAttribute("Width") )
                    {
                        int width;
                        if ( int.TryParse(PreferenceData.GetAttribute("Width"), out width) )
                            this.Width = width;
                    }
                }
                //設定navigationPane1的展開設定
                if (PreferenceData.HasAttribute("NavPanExpanded") && PreferenceData.Attributes["NavPanExpanded"].InnerText == "False")
                {
                    navigationPane1.Expanded = false;
                }
                //設定ribbonControl1的展開設定
                if (PreferenceData.HasAttribute("RibbonControlExpanded") && PreferenceData.Attributes["RibbonControlExpanded"].InnerText == "False")
                {
                    ribbonControl1.Expanded = false;
                }
                //設定ColorTable
                if (PreferenceData.HasAttribute("ColorTable"))
                {
                    switch (PreferenceData.Attributes["ColorTable"].InnerText)
                    {
                        default:
                        case "Blue":
                            buttonStyleOffice2007Blue.Checked = true;
                            StyleChange(buttonStyleOffice2007Blue, null);
                            break;
                        case "Black":
                            buttonStyleOffice2007Black.Checked = true;
                            StyleChange(buttonStyleOffice2007Black, null);
                            break;
                        case "Silver":
                            buttonStyleOffice2007Silver.Checked = true;
                            StyleChange(buttonStyleOffice2007Silver, null);
                            break;
                    }
                }
            }
            this.ResumeLayout();
            #endregion

            //在 Title 上顯示相關資訊
            CurrentUser user = CurrentUser.Instance;
            string titleInfo = "ischool 【學校名稱:{0}，學年度：{1}，學期：{2}，系統版本：{3}】【主機：{4}】";

            Text = string.Format(titleInfo, user.SchoolChineseName, user.SchoolYear, user.Semester, user.SystemVersion, user.AccessPoint);

            //登入時，跳出最新消息
            new NewsNotice();
        }

        //重新登入（重新啟動系統）
        private void buttonItem1_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                SkillSchool skill = new SkillSchool();
                skill.ShowDialog();
            }

            Application.Restart();
        }

        //查詢個人日誌
        private void btnQueryLog_Click(object sender, EventArgs e)
        {
            LogBrowserForm browser = new LogBrowserForm(CurrentUser.Instance.UserName.ToUpper());
            browser.ShowDialog();
        }

        private void btnAllLog_Click(object sender, EventArgs e)
        {
            LogBrowserForm browser = new LogBrowserForm();
            browser.ShowDialog();
        }

        #region Entity相關
        //public Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight)
        //{
        //    Bitmap result = new Bitmap(nWidth, nHeight);
        //    using (Graphics g = Graphics.FromImage((Image)result))
        //        g.DrawImage(b, 0, 0, nWidth, nHeight);
        //    return result;
        //}

        public void AddEntity(IEntity newEntity)
        {
            //讀取設定檔
            XmlElement PreferenceData = CurrentUser.Instance.Preference["MotherForm"];
            if ( PreferenceData == null )
                PreferenceData = new XmlDocument().CreateElement("MotherForm");
            //加入新ProcessTab
            #region 加入新ProcessTab
            DevComponents.DotNetBar.RibbonTabItem newRibbonTab = new RibbonTabItem();
            DevComponents.DotNetBar.RibbonPanel newRibbonPanel = new RibbonPanel();

            if ( !_ProcessTabDictionary.ContainsKey(newEntity.Title) )
            {
                ribbonControl1.Controls.Add(newRibbonPanel);

                newRibbonTab.Text = newEntity.Title;
                newRibbonTab.Panel = newRibbonPanel;
                newRibbonPanel.Dock = DockStyle.Fill;
                newRibbonPanel.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
                //newRibbonTab.Group = ribbonTabItemGroup1;
                ribbonControl1.Items.Insert(ribbonControl1.Items.Count - 2, newRibbonTab);
                newRibbonTab.CheckedChanged -= new EventHandler(newRibbonTab_CheckedChanged);

                _ProcessTabDictionary.Add(newEntity.Title, newRibbonTab);
                _ProcessListDictionary.Add(newEntity.Title, new List<IProcess>());
                _ProcessLoadedDictionary.Add(newEntity.Title, false);
            }
            else
            {
                newRibbonTab = _ProcessTabDictionary[newEntity.Title];
                newRibbonPanel = newRibbonTab.Panel;
            }
            //建立每個Tab所有的Process清單
            if ( !_ProcessAllListDictionary.ContainsKey(newEntity.Title) )
                _ProcessAllListDictionary.Add(newEntity.Title, new List<IProcess>());
            //設定每個Tab要不要顯示副頁籤
            if ( !_ProcessShowSecTab.ContainsKey(newEntity.Title) )
                _ProcessShowSecTab.Add(newEntity.Title, PreferenceData.GetAttribute("ShowSecTab_" + newEntity.Title) == "True");
            #endregion
            //加入新ProcessTab的副籤
            #region 加入新ProcessTab的副籤
            DevComponents.DotNetBar.RibbonTabItem newRibbonTabSec = new RibbonTabItem();
            DevComponents.DotNetBar.RibbonPanel newRibbonPanelSec = new RibbonPanel();

            if ( !_ProcessTabDictionary.ContainsKey(newEntity.Title+"(延伸)") )
            {
                ribbonControl1.Controls.Add(newRibbonPanelSec);

                newRibbonTabSec.Text = newEntity.Title + "(延伸)";
                newRibbonTabSec.Panel = newRibbonPanelSec;
                newRibbonPanelSec.Dock = DockStyle.Fill;
                newRibbonPanelSec.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
                //副籤預設不顯示
                newRibbonTabSec.Visible = false;
                //newRibbonTabSec.Group = ribbonTabItemGroup1;
                ribbonControl1.Items.Insert(ribbonControl1.Items.IndexOf(newRibbonTab)+1, newRibbonTabSec);
                newRibbonTabSec.CheckedChanged -= new EventHandler(newRibbonTab_CheckedChanged);

                _ProcessTabDictionary.Add(newEntity.Title + "(延伸)", newRibbonTabSec);
                _ProcessListDictionary.Add(newEntity.Title + "(延伸)", new List<IProcess>());
                _ProcessLoadedDictionary.Add(newEntity.Title + "(延伸)", false);
            }
            else
            {
                newRibbonTabSec = _ProcessTabDictionary[newEntity.Title + "(延伸)"];
                newRibbonPanelSec = newRibbonTabSec.Panel;
            } 
            #endregion
            //加入新NavigationButton
            ButtonItem newButton = new ButtonItem();
            newButton.Text = newButton.Tooltip = newEntity.Title;
            newButton.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            newButton.OptionGroup = "navBar";
            navigationPane1.Items.Add(newButton);
            //Resize圖片成24*24大小
            Bitmap b = new Bitmap(24, 24);
            using (Graphics g = Graphics.FromImage(b))
                g.DrawImage(newEntity.Picture, 0, 0, 24, 24);
            newButton.Image = b;// newEntity.Picture;
            //建立集合
            EntityItem item = new EntityItem();
            item.NavButton = newButton;
            item.RibbonTabItem = newRibbonTab;
            item.Entity = newEntity;
            newButton.Tag = item;
            newRibbonTab.Tag = item;
            newRibbonTabSec.Tag = item;
            newButton.CheckedChanged += new EventHandler(newButton_CheckedChanged);
            newRibbonTab.CheckedChanged += new EventHandler(newTab_CheckedChanged);
            newRibbonTabSec.CheckedChanged += new EventHandler(newTab_CheckedChanged);

            newRibbonTab.CheckedChanged += new EventHandler(newRibbonTab_CheckedChanged);
            newRibbonTabSec.CheckedChanged += new EventHandler(newRibbonTab_CheckedChanged);

        }

        void newTab_CheckedChanged(object sender, EventArgs e)
        {
            EntityItem item = (EntityItem)((RibbonTabItem)sender).Tag;
            //被選取的Tab用粗體字
            item.RibbonTabItem.FontBold = item.RibbonTabItem.Checked | _ProcessTabDictionary[item.Entity.Title + "(延伸)"].Checked;
            if (item.RibbonTabItem.Checked)
            {
                //同步選取NavButton
                if (item.NavButton.Checked != true)
                {
                    item.NavButton.Checked = true;
                }
            }
            else
            {
                //同步取消選取NavButton
                //if (item.NavButton.Checked != false)
                //    item.NavButton.Checked = false;
                //如果Tab被取消選取則將navigationPane1的標題清空
                //this.navigationPane1.TitlePanel.Text = "";
            }
        }
        List<EntityItem> _ActivedItems = new List<EntityItem>();
        void newButton_CheckedChanged(object sender, EventArgs e)
        {
            ButtonItem button = (ButtonItem)sender;
            //DotNetBar很機車喔，如果他自己把按鈕縮起來，在選單上看到的會是另一個object喔，那個object的Tag才是原本的那個按鈕喔，很機車吧。
            EntityItem item = button.Tag is EntityItem ? (EntityItem)button.Tag : (EntityItem)((PopupItem)button.Tag).Tag;
            //navigationPane1.TitlePanel.Text = button.Text;

            //如果NavPanPanel未載入則載入
            if (item.NavPanPanel == null)
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                item.NavPanPanel = item.Entity.NavPanPanel;
                item.NavPanPanel.ParentItem = item.NavButton;
                this.navigationPane1.Controls.Add(item.NavPanPanel);
                button.Checked = false;
                button.Checked = true;
                ribbonControl1.Office2007ColorTable = ribbonControl1.Office2007ColorTable;
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }

            //如果ContentPanel未載入則載入
            if (item.ContentPanel == null)
            {
                item.ContentPanel = item.Entity.ContentPanel;
                item.ContentPanel.Dock = DockStyle.Fill;
                item.ContentPanel.Size = this.panelContent.Size;
                this.panelContent.Controls.Add(item.ContentPanel);
                ribbonControl1.Office2007ColorTable = ribbonControl1.Office2007ColorTable;
            }
            //同步選取ProcessTab
            if (item.RibbonTabItem.Checked != button.Checked)
                item.RibbonTabItem.Checked = button.Checked;
            //同步選取ContentPanel
            if (item.ContentPanel.Visible != button.Checked)
                item.ContentPanel.Visible = button.Checked;
            //第一次選取這個item呼叫Actived()
            if (!_ActivedItems.Contains(item))
            {
                _ActivedItems.Add(item);
                item.Entity.Actived();
            }
            //如果navigationPane1是縮到左邊狀態則彈出NavPanPanel
            if (!navigationPane1.Expanded && button.Checked)
            {
                navigationPane1.PopupSelectedPaneVisible = true;
            }
        }

        #endregion

        #region Process相關
        public void AddProcess(IProcess process, int level)
        {
            process.Level = level;
            AddProcess(process);
        }

        public void AddProcess(IProcess process)
        {
            //讀取設定檔
            XmlElement PreferenceData = CurrentUser.Instance.Preference["MotherForm"];
            if ( PreferenceData == null )
                PreferenceData = new XmlDocument().CreateElement("MotherForm");
            //把裡面所有的BaseItem.GlobalItem改成false
            for (int i = 0; i < process.ProcessRibbon.Items.Count; i++)
            {
                SetGlobalItem(process.ProcessRibbon.Items[i]);
            }
            process.ProcessRibbon.Location = new Point((int)(1000 * process.Level), 1);
            //建立每個Tab所有的Process清單
            if ( !_ProcessAllListDictionary.ContainsKey(process.ProcessTabName) )
                _ProcessAllListDictionary.Add(process.ProcessTabName, new List<IProcess>());
            _ProcessAllListDictionary[process.ProcessTabName].Add(process);
            //設定每個Tab要不要顯示副頁籤
            if ( !_ProcessShowSecTab.ContainsKey(process.ProcessTabName) )
                _ProcessShowSecTab.Add(process.ProcessTabName, PreferenceData.GetAttribute("ShowSecTab_" + process.ProcessTabName) == "True");
            //判斷是否需要新增新的ProcessTab
            if ( !_ProcessListDictionary.ContainsKey(process.ProcessTabName) )
            {
                //如果Tab不存在則新增新ProcessTab
                #region 新增主籤
                RibbonPanel processPanel = new RibbonPanel();
                RibbonTabItem newRibbonTab = new RibbonTabItem();
                ribbonControl1.Controls.Add(processPanel);
                newRibbonTab.Text = process.ProcessTabName;
                newRibbonTab.Panel = processPanel;
                processPanel.Dock = DockStyle.Fill;
                processPanel.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
                ribbonControl1.Items.Insert(ribbonControl1.Items.Count - 2, newRibbonTab);

                _ProcessTabDictionary.Add(process.ProcessTabName, newRibbonTab);
                _ProcessListDictionary.Add(process.ProcessTabName, new List<IProcess>());
                _ProcessLoadedDictionary.Add(process.ProcessTabName, false);

                newRibbonTab.CheckedChanged += new EventHandler(newRibbonTab_CheckedChanged); 
                #endregion

                #region 新增副籤
                RibbonPanel processPanelSec = new RibbonPanel();
                RibbonTabItem newRibbonTabSec = new RibbonTabItem();
                ribbonControl1.Controls.Add(processPanelSec);
                newRibbonTabSec.Text = process.ProcessTabName + "(延伸)";
                newRibbonTabSec.Panel = processPanelSec;
                processPanelSec.Dock = DockStyle.Fill;
                processPanelSec.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
                ribbonControl1.Items.Insert(ribbonControl1.Items.IndexOf(newRibbonTab)+1, newRibbonTabSec);
                newRibbonTabSec.Visible = false;
                _ProcessTabDictionary.Add(process.ProcessTabName + "(延伸)", newRibbonTabSec);
                _ProcessListDictionary.Add(process.ProcessTabName + "(延伸)", new List<IProcess>());
                _ProcessLoadedDictionary.Add(process.ProcessTabName + "(延伸)", false);

                newRibbonTab.CheckedChanged += new EventHandler(newRibbonTab_CheckedChanged); 
                #endregion
            }
            bool inSecTab = PreferenceData.GetAttribute("ShowFunction_" + fixName(process.ProcessTabName) + "_" + fixName(process.ProcessRibbon.Text)) == "False";
            if ( !inSecTab )
            {
                //加入Process
                _ProcessListDictionary[process.ProcessTabName].Add(process);
                //如果該Tab已經被選取則直接觸發選取事件已達更新的目地
                if ( _ProcessTabDictionary[process.ProcessTabName].Checked )
                    newRibbonTab_CheckedChanged(_ProcessTabDictionary[process.ProcessTabName], new EventArgs());
            }
            else
            {
                //加入Process
                _ProcessListDictionary[process.ProcessTabName + "(延伸)"].Add(process);
                //如果該Tab已經被選取則直接觸發選取事件已達更新的目地
                if ( _ProcessTabDictionary[process.ProcessTabName + "(延伸)"].Checked )
                    newRibbonTab_CheckedChanged(_ProcessTabDictionary[process.ProcessTabName + "(延伸)"], new EventArgs());
            }
        }

        private string fixName(string p)
        {
            return p.Replace("/", "_").Replace("(", "_").Replace(")", "_").Replace("[", "_").Replace("]", "_").Replace("^", "_").Replace("!", "_").Replace("?", "_");
        }

        private void SetGlobalItem(BaseItem item)
        {
            item.GlobalItem = false;
            for (int i = 0; i < item.SubItems.Count; i++)
            {

                SetGlobalItem(item.SubItems[i]);
            }
        }
        private bool btnShowHidden_Checked_UIChanging = false;
        void newRibbonTab_CheckedChanged(object sender, EventArgs e)
        {
            RibbonTabItem tabItem = (RibbonTabItem)sender;
            //顯示副籤
            if ( _ProcessTabDictionary.ContainsKey(tabItem.Text + "(延伸)") )
            {
                btnShowHidden.Tag = tabItem.Text;
                btnShowHidden.Text = "顯示\"" + tabItem.Text + "(延伸)\"";
                btnShowHidden.Enabled = _ProcessListDictionary[tabItem.Text + "(延伸)"].Count > 0;
                lblShowSecTab.Text = "未勾選的功能將被搬移至\"" + tabItem.Text + "(延伸)\"之中";
                btnShowHidden_Checked_UIChanging = true;
                //if ( !btnShowHidden.Enabled )
                //    btnShowHidden.Checked = false;
                btnShowHidden.Checked = btnShowHidden.Enabled & _ProcessShowSecTab[tabItem.Text];
                btnShowHidden_Checked_UIChanging = false;
                _ProcessTabDictionary[tabItem.Text + "(延伸)"].Visible = btnShowHidden.Checked & tabItem.Checked & _ProcessListDictionary[tabItem.Text + "(延伸)"].Count > 0;
                buttonItem2.SubItems.Clear();
                buttonItem2.SubItems.Add(lblProcess);
                lblProcess.Text = tabItem.Text + " 所有功能";
                foreach ( IProcess process in _ProcessAllListDictionary[tabItem.Text] )
                {
                    ButtonItem btnShowProcess=new ButtonItem();
                    btnShowProcess.AutoCollapseOnClick = false;
                    btnShowProcess.AutoCheckOnClick = true;
                    btnShowProcess.ImagePaddingHorizontal = 8;
                    btnShowProcess.Text = process.ProcessRibbon.Text;
                    btnShowProcess.Tag = process;
                    btnShowProcess.Checked = _ProcessListDictionary[tabItem.Text].Contains(process);
                    btnShowProcess.CheckedChanged += new EventHandler(btnShowProcess_CheckedChanged);
                    buttonItem2.SubItems.Add(btnShowProcess);
                    //btnShowProcess.CheckedChanged += new System.EventHandler(this.chkShowHidden_CheckedChanged);
                }
                buttonItem2.SubItems.Add(lblShowSecTab);
                buttonItem2.SubItems.Add(btnShowHidden);
            }
            else
                _ProcessTabDictionary[tabItem.Text].Visible = _ProcessTabDictionary[tabItem.Text].Checked;
            //判斷ProcessItem是否已被載入，若未被載入則載入
            if (!_ProcessLoadedDictionary[tabItem.Text])
            {
                _ProcessListDictionary[tabItem.Text].Sort(SortByLevel);
                tabItem.Panel.Controls.Clear();
                foreach (IProcess var in _ProcessListDictionary[tabItem.Text])
                {
                    var.ProcessRibbon.Location = new Point((int)( 1000 * var.Level ), 1);
                    tabItem.Panel.Controls.Add(var.ProcessRibbon);
                }
                _ProcessLoadedDictionary[tabItem.Text] = true;
            }
            ribbonControl1.RecalcLayout();
        }

        void btnShowProcess_CheckedChanged(object sender, EventArgs e)
        {
            string tabKey = "" + btnShowHidden.Tag;
            ButtonItem btnShowProcess = (ButtonItem)sender;
            IProcess process=(IProcess )btnShowProcess.Tag;
            if ( btnShowProcess.Checked )
            {
                if ( !_ProcessListDictionary[tabKey].Contains(process) && _ProcessListDictionary[tabKey + "(延伸)"].Contains(process) )
                {
                    _ProcessListDictionary[tabKey + "(延伸)"].Remove(process);
                    _ProcessListDictionary[tabKey].Add(process);
                    _ProcessLoadedDictionary[tabKey] = _ProcessLoadedDictionary[tabKey + "(延伸)"] = false;
                }
            }
            else
            {
                if ( _ProcessListDictionary[tabKey].Contains(process) &&! _ProcessListDictionary[tabKey + "(延伸)"].Contains(process) )
                {
                    _ProcessListDictionary[tabKey].Remove(process);
                    _ProcessListDictionary[tabKey + "(延伸)"].Add(process);
                    _ProcessLoadedDictionary[tabKey] = _ProcessLoadedDictionary[tabKey + "(延伸)"] = false;
                }
            }
            if ( _ProcessTabDictionary[tabKey + "(延伸)"].Checked && ( btnShowHidden.Checked == false || _ProcessListDictionary[tabKey + "(延伸)"].Count == 0 ) )
                _ProcessTabDictionary[tabKey].Checked = true;
            _ProcessTabDictionary[tabKey + "(延伸)"].Visible = btnShowHidden.Checked & _ProcessListDictionary[tabKey + "(延伸)"].Count > 0;
            
            btnShowHidden_Checked_UIChanging = true;
            btnShowHidden.Enabled = _ProcessListDictionary[tabKey + "(延伸)"].Count > 0;
            btnShowHidden.Checked = btnShowHidden.Enabled & _ProcessShowSecTab[tabKey];
            btnShowHidden_Checked_UIChanging = false;
        }

        private void chkShowHidden_CheckedChanged(object sender, EventArgs e)
        {
            if ( btnShowHidden_Checked_UIChanging )
                return;
            string tabKey = "" + btnShowHidden.Tag;
            _ProcessShowSecTab[tabKey] = btnShowHidden.Checked;
            if ( _ProcessTabDictionary[tabKey + "(延伸)"].Checked &&( btnShowHidden.Checked==false || _ProcessListDictionary[tabKey + "(延伸)"].Count == 0) )
                _ProcessTabDictionary[tabKey].Checked = true;
            _ProcessTabDictionary[tabKey + "(延伸)"].Visible = btnShowHidden.Checked & _ProcessListDictionary[tabKey + "(延伸)"].Count > 0;
        }


        private void buttonItem2_ExpandChange(object sender, EventArgs e)
        {
            string tabKey = "" + btnShowHidden.Tag;
            if ( buttonItem2.Expanded == false )
            {
                if ( _ProcessTabDictionary[tabKey].Checked )
                    newRibbonTab_CheckedChanged(_ProcessTabDictionary[tabKey], new EventArgs());
                if ( _ProcessTabDictionary[tabKey + "(延伸)"].Checked )
                    newRibbonTab_CheckedChanged(_ProcessTabDictionary[tabKey + "(延伸)"], new EventArgs());
                this.UpdatePreference();
            }
        }

        #endregion
        private static int SortByLevel(IProcess p1, IProcess p2)
        {
            return p1.Level.CompareTo(p2.Level);
        }
        #region 變換色彩用
        public void SetStyle()
        {
            ribbonControl1.Office2007ColorTable = ribbonControl1.Office2007ColorTable;
        }

        private void StyleReset(object sender, EventArgs e)
        {
            if (ribbonControl1.Office2007ColorTable != _ColorSchema)
            {
                ribbonControl1.Office2007ColorTable = _ColorSchema;
                this.Invalidate();
            }
        }

        private void StyleChange(object sender, EventArgs e)
        {
            ButtonItem item = sender as ButtonItem;
            if (item == buttonStyleOffice2007Blue)
            {
                // This is all that is needed to change the color table for all controls on the form
                _ColorSchema = eOffice2007ColorScheme.Blue;
            }
            else if (item == buttonStyleOffice2007Black)
            {
                // This is all that is needed to change the color table for all controls on the form
                _ColorSchema = eOffice2007ColorScheme.Black;
            }
            else if (item == buttonStyleOffice2007Silver)
            {
                // This is all that is needed to change the color table for all controls on the form
                _ColorSchema = eOffice2007ColorScheme.Silver;
            }
            ribbonControl1.Office2007ColorTable = _ColorSchema;
            this.Invalidate();
        }

        private void StylePreview(object sender, EventArgs e)
        {
            ButtonItem item = sender as ButtonItem;
            if (item == buttonStyleOffice2007Blue)
            {
                if (ribbonControl1.Office2007ColorTable == eOffice2007ColorScheme.Blue) return;
                // This is all that is needed to change the color table for all controls on the form
                ribbonControl1.Office2007ColorTable = eOffice2007ColorScheme.Blue;
            }
            else if (item == buttonStyleOffice2007Black)
            {
                if (ribbonControl1.Office2007ColorTable == eOffice2007ColorScheme.Black) return;
                // This is all that is needed to change the color table for all controls on the form
                ribbonControl1.Office2007ColorTable = eOffice2007ColorScheme.Black;
            }
            else if (item == buttonStyleOffice2007Silver)
            {
                if (ribbonControl1.Office2007ColorTable == eOffice2007ColorScheme.Silver) return;
                // This is all that is needed to change the color table for all controls on the form
                ribbonControl1.Office2007ColorTable = eOffice2007ColorScheme.Silver;
            }

            this.Invalidate();
        }

        #endregion

        #region 設定stateBar的內容

        /// <summary>
        /// 防止多緒執行時發生錯誤。
        /// </summary>
        /// <param name="message"></param>
        private delegate void SetText(string message);

        public void SetBarMessage(string labelMessage)
        {
            if (InvokeRequired)
            {
                SetText invoke = new SetText(SetBarMessage);
                Invoke(invoke, labelMessage);
            }
            else
                SetBarMessage(labelMessage, -1);
        }

        public void SetBarMessage(string labelMessage, int progress)
        {
            if (progress > 100 || progress < 0)
            {
                progressBarMessage.Visible = false;
            }
            else
            {
                progressBarMessage.Value = progress;
                progressBarMessage.Visible = true;
            }
            lblBarMessage.Text = labelMessage;
        }
        #endregion

        #region 設定滑鼠指標
        public void SetWaitCursor()
        {
            Application.UseWaitCursor = true;
            //this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            WaitCursorCount++;
        }

        public void ResetWaitCursor()
        {
            if (--WaitCursorCount == 0)
            {
                Application.UseWaitCursor = false;
                //this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }
        #endregion

        #region UI與Preference相關

        private void navigationPane1_ExpandedChanged(object sender, ExpandedChangeEventArgs e)
        {
            expandableSplitter1.Enabled = navigationPane1.Expanded;
            navigationPane1.Invalidate();
            UpdatePreference();
        }

        #region IPreference 成員
        //寫入Preference資料
        public void UpdatePreference()
        {
            #region 取得PreferenceElement
            XmlElement PreferenceElement = CurrentUser.Instance.Preference["MotherForm"];
            if (PreferenceElement == null)
            {
                PreferenceElement = new XmlDocument().CreateElement("MotherForm");
            }
            #endregion
            //紀錄navigationPane1的展開設定
            PreferenceElement.SetAttribute("NavPanExpanded", navigationPane1.Expanded ? "True" : "False");
            //紀錄ribbonControl1的展開設定
            PreferenceElement.SetAttribute("RibbonControlExpanded", ribbonControl1.Expanded ? "True" : "False");
            //紀錄ColorTable
            if (buttonStyleOffice2007Blue.Checked)
                PreferenceElement.SetAttribute("ColorTable", "Blue");
            if (buttonStyleOffice2007Black.Checked)
                PreferenceElement.SetAttribute("ColorTable", "Black");
            if (buttonStyleOffice2007Silver.Checked)
                PreferenceElement.SetAttribute("ColorTable", "Silver");
            //紀錄顯示項目高度
            PreferenceElement.SetAttribute("NavigationBarHeight", navigationPane1.NavigationBarHeight.ToString());
            //紀錄是否最大化設定
            PreferenceElement.SetAttribute("Maximized", this.WindowState == FormWindowState.Maximized ? "True" : "False");
            //紀錄畫面高
            PreferenceElement.SetAttribute("Height", this.Height.ToString());
            //紀錄畫面寬
            PreferenceElement.SetAttribute("Width", this.Width.ToString());
            //紀錄自訂功能狀況
            foreach ( string tabKey in _ProcessShowSecTab.Keys )
            {
                PreferenceElement.SetAttribute("ShowSecTab_" + tabKey, _ProcessShowSecTab[tabKey].ToString());
                foreach ( IProcess process in _ProcessAllListDictionary[tabKey] )
                {
                    PreferenceElement.SetAttribute("ShowFunction_" + fixName(tabKey) + "_" + fixName(process.ProcessRibbon.Text), _ProcessListDictionary[tabKey].Contains(process).ToString());
                }
            }
            CurrentUser.Instance.Preference["MotherForm"] = PreferenceElement;
        }

        #endregion

        private void navigationPane1_LocalizeString(object sender, LocalizeEventArgs e)
        {
            switch (e.Key)
            {
                case "navbar_showmorebuttons":
                    e.Handled = true;
                    e.LocalizedValue = "顯示較多按鈕";
                    break;
                case "navbar_showfewerbuttons":
                    e.Handled = true;
                    e.LocalizedValue = "顯示較少按鈕";
                    break;
                default:
                    break;
            }
        }
        #endregion

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            UserInfoManager uimForm = new UserInfoManager();
            uimForm.StartPosition = FormStartPosition.CenterScreen;
            uimForm.ShowDialog();
        }

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            UserManager umForm = new UserManager();
            umForm.ShowDialog();
        }

        private void btnRoleManagement_Click(object sender, EventArgs e)
        {
            RoleManager rmForm = new RoleManager();
            rmForm.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if ( _SayByeBye && !_MessageBoxShown )
            {
                _MessageBoxShown = true;
                if ( _JestLeave || MsgBox.Show("因為網路連線異常" + ( _ExceptionMessage == "" ? "" : ( "(" + _ExceptionMessage + ")" ) ) + "，系統必須關閉，\n目前正在進行的動作可能失敗。\n\n是否重新登入系統?", "網路連線異常", MessageBoxButtons.YesNo, MessageBoxIcon.Error)
                                == DialogResult.Yes )
                {
                    Application.Restart();
                }
                else
                {
                    Application.Exit();
                }
                _Closed = true;
                this.Close();
                //_MessageBoxShown = false;
            }
            else
            {
                if ( ( _NetWorkingCount <= 0 && _Successed && _MinimalLoadingTime == 0 ) || ( g == null || gshadow == null ) )
                {
                    office2007StartButton2.Image = _NormalImage;
                }
                else
                {
                    if ( _MinimalLoadingTime > 0 ) _MinimalLoadingTime = _MinimalLoadingTime - 1;
                    if ( _Successed )
                    {

                        gshadow.Clear(Color.Transparent);
                        g.Clear(Color.Transparent);
                        gshadow.RotateTransform(_Speed);
                        gshadow.DrawImage(_ShadowImage, -15.0F, -15.0F, 30.0F, 30.0F);
                        gshadow.RotateTransform(180);
                        gshadow.DrawImage(_ShadowImage, -15.0F, -15.0F, 30.0F, 30.0F);

                        g.RotateTransform(_Speed);
                        g.DrawImage(_LoadingImage, -15.0F, -15.0F, 30.0F, 30.0F);
                        g.RotateTransform(180);
                        g.DrawImage(_LoadingImage, -15.0F, -15.0F, 30.0F, 30.0F);
                        office2007StartButton2.Image = b;
                    }
                    else
                    {
                        gshadow.Clear(Color.Transparent);
                        g.Clear(Color.Transparent);

                        gshadow.RotateTransform(_Speed / 2.0F);
                        gshadow.DrawImage(_ShadowImage, -15.0F, -15.0F, 30.0F, 30.0F);
                        gshadow.RotateTransform(180);
                        gshadow.DrawImage(_ShadowImage, -15.0F, -15.0F, 30.0F, 30.0F);

                        g.RotateTransform(_Speed / 2.0F);
                        g.DrawImage(_ErrorImage, -15.0F, -15.0F, 30.0F, 30.0F);
                        g.RotateTransform(180);
                        g.DrawImage(_ErrorImage, -15.0F, -15.0F, 30.0F, 30.0F);
                        office2007StartButton2.Image = b;
                        if ( !_MessageBoxShown )
                            SetBarMessage("網路連線異常" + ( _ExceptionMessage == "" ? "" : ( "(" + _ExceptionMessage + ")" ) ) + "，請檢查您的網路連線狀態。");
                    }
                }
            }
        }

        static internal void SayByeBye(bool jestLeave)
        {
            _SayByeBye = true;
            _JestLeave = jestLeave;
            if (Thread.CurrentThread == _MainThread)
            {
                _Instance.timer1_Tick(null, null);
            }
        }

        static internal void NetWorking()
        {
            _NetWorkingCount++;
            _MinimalLoadingTime = 20;
            //如果是主執行緒就利用DoEvents去讓圈圈轉
            if (_Instance != null && Thread.CurrentThread == _MainThread)
            {
                _Instance.SetWaitCursor();
                _Instance.timer1_Tick(null, null);
                Application.DoEvents();
            }
        }

        static internal void NetWorkFinished(bool successed)
        {
            NetWorkFinished(successed, "");
        }

        static internal void NetWorkFinished(bool successed, string message)
        {
            if (_Instance != null && Thread.CurrentThread == _MainThread)
            {
                _Instance.ResetWaitCursor();
            }
            _NetWorkingCount--;
            _Successed = successed;
            _ExceptionMessage = message;
            if (((!_Successed && _Instance == null) || (!_Successed && _Instance != null && Thread.CurrentThread == _MainThread)) && !_MessageBoxShown)
            {
                _MessageBoxShown = true;
                if (MsgBox.Show("網路連線異常" + (_ExceptionMessage == "" ? "" : ("(" + _ExceptionMessage + ")")) + "，請檢查您的網路連線狀態。\n\n若您的網路暫時無法恢復運作，請按取消系統將自動關閉。", "網路連線異常", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    Application.Exit();
                    _Closed = true;
                    if (_Instance != null) _Instance.Close();
                }
                _MessageBoxShown = false;
            }
        }

        static internal void SetupSpeed(int bytes, int milliseconds)
        {
            _Bytes += (ulong)bytes;
            _CostTimes += (ulong)milliseconds;
            if (_CostTimes > 0)
                _Speed = (((float)_Bytes / 1024F / ((float)_CostTimes / 1000))) / 25F * 15;
            if (_Speed > 35)
                _Speed = 35;
        }

        private void MotherForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _MessageBoxShown = true;
            _Closed = true;
        }

        static internal bool IsClosed
        {
            get { return _Closed; }
        }

        private void btnSchoolInfo_Click(object sender, EventArgs e)
        {
            SchoolInfoEditor editor = new SchoolInfoEditor();
            editor.ShowDialog();
        }

        #region 問題回報與最新消息

        private void btnFeedback_Click(object sender, EventArgs e)
        {
            FeedbackForm form = new FeedbackForm();
            form.Show();
        }

        private void btnNews_Click(object sender, EventArgs e)
        {
            NewsForm form = new NewsForm();
            form.ShowDialog();
        }

        private void btnVote_Click(object sender, EventArgs e)
        {
            VoteForm form = new VoteForm();
            form.Show();
        }

        #endregion

        #region 電子報表
        private void btnEPaperManager_Click(object sender, EventArgs e)
        {
            new SmartSchool.ElectronicPaperImp.ElectronicPaperManager().ShowDialog();
        }
        #endregion

        /// <summary>
        /// 問卷管理。 
        /// </summary>
        private void btnSurveyMan_Click(object sender, EventArgs e)
        {
            new SmartSchool.Survey.SurveyManager().ShowDialog();
        }

        private void ribbonControl1_ExpandedChanged(object sender, EventArgs e)
        {
            UpdatePreference();
        }

        private void buttonChangeStyle_ExpandChange(object sender, EventArgs e)
        {
            if ( buttonChangeStyle.Expanded == false )
                UpdatePreference();
        }

        private void MotherForm_SizeChanged(object sender, EventArgs e)
        {
            UpdatePreference();
        }
    }

    internal class EntityFactory
    {
        internal List<IEntity> BuildEntities()
        {
            List<IEntity> _Entities = new List<IEntity>();

            ////CreateInstance 要先做
            //Student.CreateInstance();
            //Class.CreateInstance();
            //Teacher.CreateInstance();
            //CourseEntity.CreateInstance();
            ////SmartSchool.GraduationPlanRelated.GraduationPlan.CreateInstance();
            ////SmartSchool.ScoreCalcRuleRelated.ScoreCalcRule.CreateInstance();

            //_Entities.Add(Student.Instance);
            //_Entities.Add(Class.Instance);
            //_Entities.Add(Teacher.Instance);
            //_Entities.Add(CourseEntity.Instance);
            ////_Entities.Add(SmartSchool.Configure.Configuration.Instance);
            //SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.SetManager(SmartSchool.Configure.ConfigurationManager.Instance);

            ////設定同步更新事件
            //Student.Instance.SetupSynchronization();
            //Class.Instance.SetupSynchronization();
            //Teacher.Instance.SetupSynchronization();
            //CourseEntity.Instance.SetupSynchronization();


            return _Entities;
        }
    }
}