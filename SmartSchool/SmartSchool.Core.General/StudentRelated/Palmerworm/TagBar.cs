using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DevComponents.DotNetBar;
using SmartSchool.Common;
using SmartSchool.TagManage;
using StudentEntityItem = SmartSchool.TagManage.EntityItem;

namespace SmartSchool.StudentRelated.Palmerworm
{
    internal partial class TagBar : PalmerwormItem
    {
        private const int MarkHeight = 22;

        private TagManager _manager;
        private StudentTagManager _student_tag;
        private StudentEntityItem _entity_item;

        public TagBar()
        {
            InitializeComponent();

            ShowWaitingIcon = false;
        }

        protected override object OnBackgroundWorkerWorking()
        {
            if (!_student_tag.IsSynchronized)
                _student_tag.SynchorizeSelection();

            EntityItemCollection entities = _student_tag.EntityItems;

            if (entities.ContainsKey(int.Parse(RunningID)))
                _entity_item = entities[int.Parse(RunningID)];

            return _entity_item;
        }

        public override void LoadContent(string id)
        {
            if (_manager == null)
                _manager = Student.Instance.TagManager;

            if (_student_tag == null)
            {
                _student_tag = Student.Instance.SelectionTagManager;
                _student_tag.EntityItemsChanged += new EventHandler(StudentTagChanged);
            }

            if (_student_tag != null)
                _student_tag.RefreshSelectionReference();

            mark_container.Controls.Clear();
            base.LoadContent(id);
        }

        protected override void OnBackgroundWorkerCompleted(object result)
        {
            RefreshMarks(_entity_item);
        }

        private void StudentTagChanged(object sender, EventArgs e)
        {
            if (_student_tag.EntityItems.ContainsKey(int.Parse(RunningID)))
            {
                _entity_item = _student_tag.EntityItems[int.Parse(RunningID)];
                RefreshMarks(_entity_item);
            }
        }

        private void RefreshMarks(StudentEntityItem entityItem)
        {
            mark_container.Controls.Clear();

            if (entityItem == null || entityItem.Tags.Count <= 0)
            {
                Size = new Size(Size.Width, 0); //變成看不見。
                return;
            }

            Size size = CalcSize(entityItem.Tags.Count);

            Visible = (entityItem.Tags.Count > 0);

            //偷偷排序
            List<TagInfo> sortedTags = new List<TagInfo>();
            foreach (int each in entityItem.Tags)
                sortedTags.Add(_manager.Tags[each]);
            sortedTags.Sort(CompareDinosByLength);

            foreach (TagInfo tag in sortedTags)
            {
                if (_manager.Tags.ContainsKey(tag.Identity))
                {
                    DisplayMark(tag.FullName, tag.Color, size);
                }
            }

            //foreach (int each in entityItem.Tags)
            //{
            //    if (_manager.Tags.ContainsKey(each))
            //    {
            //        TagInfo tag = _manager.Tags[each];
            //        DisplayMark(tag.FullName, tag.Color, size);
            //    }
            //}

            Size = mark_container.Size;
        }

        private int CompareDinosByLength(TagInfo x, TagInfo y)
        {
            return x.FullName.CompareTo(y.FullName);
        }

        private void DisplayMark(string text, Color color, Size size)
        {
            if (color == Color.Empty)
                color = Color.White;

            PanelEx panel = CreatePanel();
            panel.Text = text;
            tooltip.SetSuperTooltip(panel, new SuperTooltipInfo("", "", text, null, null, DevComponents.DotNetBar.eTooltipColor.System));
            panel.Style.BackColor1.Color = color;
            panel.Style.BackColor2.Color = Color.WhiteSmoke;
            panel.Size = size;

            mark_container.Controls.Add(panel);
        }

        private Size CalcSize(int count)
        {
            if (count <= 0)
                return new Size(mark_container.Width, MarkHeight);

            int width = mark_container.Width / count;

            return new Size(width - 6, MarkHeight);
        }

        private PanelEx CreatePanel()
        {
            PanelEx panel = new PanelEx();

            panel.Dock = DockStyle.Top;
            panel.CanvasColor = System.Drawing.SystemColors.Control;
            panel.Style.Alignment = System.Drawing.StringAlignment.Center;
            panel.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            panel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            panel.Style.GradientAngle = 90;
            panel.Style.Border = eBorderType.SingleLine;
            panel.Style.BorderColor.Color = Color.Black;

            return panel;
        }
    }
}
