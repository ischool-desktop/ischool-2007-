using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.CourseRelated.DetailPaneItem;
using SmartSchool.Common;
using System.Text.RegularExpressions;

namespace SmartSchool.CourseRelated
{
    internal partial class CourseDetailPane : UserControl
    {
        private const string Prefix = "DetailItemVisible_";

        private CourseInformation _current_course;
        private CourseInformation _previous_course;

        private IPalmerwormManager _Manager;

        public IPalmerwormManager Manager
        {
            get { return _Manager; }
            set
            {
                if (_Manager != null)
                    _Manager.Save -= new EventHandler(_Manager_Save);
                _Manager = value;
                if (_Manager != null)
                    _Manager.Save += new EventHandler(_Manager_Save);
                checkToSave(null, null);
            }
        }

        public CourseDetailPane()
        {
            InitializeComponent();

            lblCourseName.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
        }

        private bool _saving = false;

        void _Manager_Save(object sender, EventArgs e)
        {
            _saving = true;
            foreach (Control var in DetailItemPane.Controls)
            {
                if (var is PalmerwormItemContainer)
                {
                    if (((PalmerwormItemContainer)var).PalmerwormItem.SaveButtonVisible)
                    {
                        if (CurrentUser.Acl[((PalmerwormItemContainer)var).PalmerwormItem.GetType()].Editable)
                            ((PalmerwormItemContainer)var).PalmerwormItem.Save();
                    }
                }
            }
            _saving = false;
        }

        private void checkToSave(object sender, EventArgs e)
        {
            if (_Manager != null)
            {
                foreach (Control var in DetailItemPane.Controls)
                {
                    if (var is PalmerwormItemContainer)
                    {
                        if (((PalmerwormItemContainer)var).PalmerwormItem.SaveButtonVisible)
                        {
                            _Manager.EnableSave = true;
                            break;
                        }
                        else
                        {
                            _Manager.EnableSave = false;
                        }
                    }
                }
            }
        }

        private void CourseDetailPane_Load(object sender, EventArgs e)
        {
            //設計模式不執行下列程式碼。
            if (Site != null && Site.DesignMode)
                return;

            /*
             * 註冊您的 Palmerworm(毛毛蟲)
             */

            List<Type> _type_list = new List<Type>(new Type[]{
                typeof(BasicInfo),
                typeof(SCAttendInfo),
                typeof(ElectronicPaperPalmerworm)
            });

            foreach (Type type in _type_list)
            {
                if (CurrentUser.Acl[type].Viewable)
                    RegisterDetailItem(type.GetConstructor(Type.EmptyTypes).Invoke(null) as SmartSchool.Customization.PlugIn.ExtendedContent.IContentItem);
            }

            //RegisterDetailItem(new BasicInfo());
            //RegisterDetailItem(new SCAttendInfo());
            //RegisterDetailItem(new DataGridViewItem());

            //載入 Palmerworm 顯示狀態。
            LoadPreference();
        }

        /// <summary>
        /// 顯示課程詳細資料。
        /// </summary>
        /// <param name="course"></param>
        public void DisplayDetail(CourseInformation course)
        {
            if (_saving) return;

            _current_course = course; //記錄到全域變數。
            Visible = (course != null); //決定是否顯示 DetailPane。
            if (course == null) return; //沒有資料時，以下程式碼不執行。

            lblCourseName.Text = course.CourseName;

            if (_current_course.Equals(_previous_course)) return;

            ReloadDetailItems(course);
            _previous_course = _current_course;
        }

        private void ReloadDetailItems(CourseInformation course)
        {
            foreach (Customization.PlugIn.ExtendedContent.IContentItem each in EachDetailItem)
                each.LoadContent(course.Identity.ToString());
        }

        /// <summary>
        /// 儲存全部項目。
        /// </summary>
        public void SaveDetail()
        {
            foreach (Customization.PlugIn.ExtendedContent.IContentItem each in EachDetailItem)
                each.Save();
        }

        /// <summary>
        /// 註冊 DetailItem 。
        /// </summary>
        /// <param name="item"></param>
        public void RegisterDetailItem(Customization.PlugIn.ExtendedContent.IContentItem item)
        {
            PalmerwormItemContainer container = new PalmerwormItemContainer(item, item.DisplayControl.Width);
            DetailItemPane.Controls.Add(container);

            item.SaveButtonVisibleChanged += new EventHandler(checkToSave);

            AddVisibleCheckItem(container);
            AddNavigationItem(container);
        }

        /// <summary>
        /// 列舉所有 DetailItem(毛毛蟲)。
        /// </summary>
        public IEnumerable<Customization.PlugIn.ExtendedContent.IContentItem> EachDetailItem
        {
            get
            {
                foreach (Control each in DetailItemPane.Controls)
                {
                    if (each is PalmerwormItemContainer)
                    {
                        yield return (each as PalmerwormItemContainer).PalmerwormItem;
                    }
                }
            }
        }

        private IEnumerable<ButtonItem> EachNavigationItem
        {
            get
            {
                foreach (object obj in btnGoto.SubItems)
                {
                    ButtonItem item = obj as ButtonItem;

                    if (item != null)
                        yield return item;
                }
            }
        }

        private IEnumerable<CheckBoxItem> EachVisibleCheckItem
        {
            get
            {
                foreach (CheckBoxItem each in CheckItemLeft.SubItems)
                {
                    yield return each;
                }
                foreach (CheckBoxItem each in CheckItemRight.SubItems)
                {
                    yield return each;
                }
            }
        }

        private void LoadPreference()
        {
            CoursePreference preference = CourseEntity.Instance.Preference;

            foreach (CheckBoxItem each in EachVisibleCheckItem)
            {
                PalmerwormItemContainer container = each.Tag as PalmerwormItemContainer;

                bool visible = preference.GetBoolean(GetIdentityName(container), true);
                each.Checked = visible; //CheckChanged 會同步畫面狀態。
            }
        }

        private static string GetIdentityName(PalmerwormItemContainer container)
        {
            Regex re = new Regex(@"\W");
            return re.Replace(Prefix + container.PalmerwormItem.Title, "_");
        }

        /// <summary>
        /// 新增 CheckItem。
        /// </summary>
        /// <param name="item">要處理的項目。</param>
        private void AddVisibleCheckItem(PalmerwormItemContainer item)
        {
            CheckBoxItem check = new CheckBoxItem();
            check.Checked = true; //這樣會引發 CheckChanged 事件。
            check.AutoCollapseOnClick = false;
            check.CheckedChanged += new CheckBoxChangeEventHandler(Check_CheckedChanged);
            check.Text = item.PalmerwormItem.Title;
            check.Tag = item;

            //容器有左邊與右邊，要一邊放一個。
            if (CheckItemLeft.SubItems.Count <= CheckItemRight.SubItems.Count)
                CheckItemLeft.SubItems.Add(check);
            else
                CheckItemRight.SubItems.Add(check);
        }

        private void Check_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            CheckBoxItem item = sender as CheckBoxItem;
            PalmerwormItemContainer container = item.Tag as PalmerwormItemContainer;

            if (container != null)
                container.Visible = item.Checked;

            CourseEntity.Instance.Preference.SetBoolean(GetIdentityName(container), container.Visible);

            SyncNavigationItemStatus();
        }

        private void SyncNavigationItemStatus()
        {
            //同步 Navigation 上的項目顯示狀態。
            foreach (ButtonItem each in EachNavigationItem)
            {
                PalmerwormItemContainer container = each.Tag as PalmerwormItemContainer;
                if (container != null)
                {
                    each.Visible = container.Visible;
                }
            }
        }

        private void AddNavigationItem(PalmerwormItemContainer item)
        {
            ButtonItem btn = new ButtonItem();
            btn.Text = item.PalmerwormItem.Title;
            btn.Tag = item;
            btn.Click += new EventHandler(Goto_Click);
            btnGoto.SubItems.Add(btn);
        }

        private void Goto_Click(object sender, EventArgs e)
        {
            ButtonItem item = sender as ButtonItem;

            if (item != null)
            {
                PalmerwormItemContainer container = item.Tag as PalmerwormItemContainer;
                DetailItemPane.ScrollControlIntoView(container);
            }
        }

        private void DetailItemPane_MouseEnter(object sender, EventArgs e)
        {
            if (DetailItemPane.TopLevelControl.ContainsFocus && !DetailItemPane.ContainsFocus)
                DetailItemPane.Focus();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ReloadDetailItems(_current_course);
        }
    }
}
