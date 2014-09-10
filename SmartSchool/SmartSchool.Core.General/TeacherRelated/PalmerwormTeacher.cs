using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DevComponents.DotNetBar;
using SmartSchool.TeacherRelated.Palmerworm;
using SmartSchool.Common;
using SmartSchool.Feature.Teacher;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.TeacherRelated
{
    internal partial class PalmerwormTeacher : UserControl
    {
        private BriefTeacherData _TeacherInfo;

        public BriefTeacherData TeacherInfo
        {
            get
            {
                return _TeacherInfo;
            }
            set
            {
                _TeacherInfo = value;
                if (_TeacherInfo != null)
                {
                    lblTeacherName.Visible = pxStatus.Visible = labelX1.Visible = true;
                    lblTeacherName.Text = (_TeacherInfo.UniqName == "" ? "" : _TeacherInfo.UniqName + " 老師");
                    Color s;
                    string txt = _TeacherInfo.Status;
                    switch (txt)
                    {
                        case "一般":
                            s = Color.FromArgb(255, 255, 255);
                            break;
                        case "刪除":
                            s = Color.FromArgb(254, 128, 155);
                            break;
                        default:
                            s = Color.Transparent;
                            break;
                    }
                    pxStatus.Text = txt;
                    pxStatus.Style.BackColor1.Color = s;
                    pxStatus.Style.BackColor2.Color = s;
                    foreach (Control var in palmerwormItemPanel.Controls)
                    {
                        if (var is PalmerwormItemContainer)
                        {
                            ((PalmerwormItemContainer)var).PalmerwormItem.LoadContent(_TeacherInfo.ID);
                        }
                    }
                }
                else
                {
                    lblTeacherName.Visible = pxStatus.Visible = labelX1.Visible = false;
                }
            }
        }

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

        void _Manager_Save(object sender, EventArgs e)
        {
            palmerwormSave_Click(null, null);
        }

        public PalmerwormTeacher()
        {
            InitializeComponent();
            try
            {
                if (CurrentUser.Instance.IsLogined)
                {
                    //foreach (IPalmerwormItem var in PalmerwormFactory.Load())
                    //{
                    //    AddPalmerwormItem(var, false);
                    //}

                    //AddPalmerwormItem(new BaseInfoItem(), false);
                    //AddPalmerwormItem(new TeachStudentItem());
                    //AddPalmerwormItem(new TeachCourseItem());

                    //List<Type> _type_list = new List<Type>(new Type[]{
                    //    typeof(BaseInfoItem),
                    //    typeof(TeachStudentItem),
                    //    //typeof(TeachCourseItem)
                    //});

                    //foreach (Type type in _type_list)
                    //{
                    //    if (CurrentUser.Acl[type].Viewable)
                    //        AddPalmerwormItem(type.GetConstructor(Type.EmptyTypes).Invoke(null) as SmartSchool.Customization.PlugIn.ExtendedContent.IContentItem);
                    //}

                    foreach ( Customization.PlugIn.ExtendedContent.IContentItem var in PalmerwormFactory.Instence.Load() )
                    {
                            AddPalmerwormItem(var, false);
                    }
                }

                XmlElement PreferenceData = CurrentUser.Instance.Preference["PalmerwormTeacher"];
                if (PreferenceData != null)
                {
                    foreach (XmlNode var in PreferenceData.SelectNodes("ShowItem"))
                    {
                        foreach (BaseItem itemToShow in itemContainer1.SubItems)
                        {
                            if (itemToShow.Tag.GetType().Name == var.Attributes["Type"].Value)
                            {
                                ((CheckBoxItem)itemToShow).Checked = true;
                                break;
                            }
                        }
                        foreach (BaseItem itemToShow in itemContainer2.SubItems)
                        {
                            if (itemToShow.Tag.GetType().Name == var.Attributes["Type"].Value)
                            {
                                ((CheckBoxItem)itemToShow).Checked = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void AddPalmerwormItem(Customization.PlugIn.ExtendedContent.IContentItem item)
        {
            AddPalmerwormItem(item, false);
        }

        private void AddPalmerwormItem(Customization.PlugIn.ExtendedContent.IContentItem item, bool show)
        {
            CheckBoxItem itemToShow = new CheckBoxItem(item.Title);
            itemToShow.Text = item.Title;
            itemToShow.Tag = item;
            itemToShow.CheckedChanged += new CheckBoxChangeEventHandler(itemToShow_CheckedChanged);
            itemToShow.AutoCollapseOnClick = false;
            if (itemContainer1.SubItems.Count == itemContainer2.SubItems.Count)
            {
                itemContainer1.SubItems.Add(itemToShow);
            }
            else
            {
                itemContainer2.SubItems.Add(itemToShow);
            }
            item.SaveButtonVisibleChanged += new EventHandler(checkToSave);
            if (show)
                itemToShow.Checked = true;

        }

        private void checkToSave(object sender, EventArgs e)
        {
            if (_Manager != null)
            {
                foreach (Control var in palmerwormItemPanel.Controls)
                {
                    if (var is PalmerwormItemContainer)
                    {
                        if (((PalmerwormItemContainer)var).PalmerwormItem.SaveButtonVisible)
                        {
                            _Manager.EnableSave = palmerwormSave.Enabled = true;
                            break;
                        }
                        else
                        {
                            _Manager.EnableSave = palmerwormSave.Enabled = false;
                        }
                    }
                }
            }
        }

        private void itemToShow_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            XmlElement PreferenceData = CurrentUser.Instance.Preference["PalmerwormTeacher"];
            if (PreferenceData == null)
            {
                PreferenceData = new XmlDocument().CreateElement("PalmerwormTeacher");
            }
            CheckBoxItem item = (CheckBoxItem)sender;
            Customization.PlugIn.ExtendedContent.IContentItem palmerwormItem = (Customization.PlugIn.ExtendedContent.IContentItem)item.Tag;
            if (item.Checked)
            {
                palmerwormItem.DisplayControl.BackColor = System.Drawing.Color.White;
                PalmerwormItemContainer container = new PalmerwormItemContainer(palmerwormItem, palmerwormItem.DisplayControl.Width);
                palmerwormItemPanel.Controls.Add(container);
                ButtonItem gotoButton = new ButtonItem(palmerwormItem.Title);
                gotoButton.Tag = container;
                gotoButton.Text = palmerwormItem.Title;
                gotoButton.Click += new EventHandler(gotoButton_Click);
                if (_TeacherInfo != null) palmerwormItem.LoadContent(_TeacherInfo.ID);
                buttonX3.SubItems.Add(gotoButton);
                if (!buttonX3.Visible)
                    buttonX3.Visible = true;
                if (PreferenceData.SelectSingleNode("ShowItem[@Type='" + palmerwormItem.GetType().Name + "']") == null)
                {
                    XmlElement show = PreferenceData.OwnerDocument.CreateElement("ShowItem");
                    show.SetAttribute("Type", palmerwormItem.GetType().Name);
                    PreferenceData.AppendChild(show);
                }
            }
            else
            {
                foreach (Control var in palmerwormItemPanel.Controls)
                {
                    if (var is PalmerwormItemContainer && ((PalmerwormItemContainer)var).PalmerwormItem == palmerwormItem)
                    {
                        var.Hide();
                        PalmerwormItemContainer container = var as PalmerwormItemContainer;
                        container.Expanded = true;
                        palmerwormItemPanel.Controls.Remove(var);
                        break;
                    }
                }
                foreach (BaseItem var in buttonX3.SubItems)
                {
                    if (var.Tag is PalmerwormItemContainer && var.Text == item.Text)
                    {
                        buttonX3.SubItems.Remove(var);
                        break;
                    }
                }
                if (buttonX3.SubItems.Count == 1)
                    buttonX3.Visible = false;
                PreferenceData.RemoveChild(PreferenceData.SelectSingleNode("ShowItem[@Type='" + palmerwormItem.GetType().Name + "']"));
            }
            CurrentUser.Instance.Preference["PalmerwormTeacher"] = PreferenceData;
        }

        private void gotoButton_Click(object sender, EventArgs e)
        {
            palmerwormItemPanel.ScrollControlIntoView((PalmerwormItemContainer)((BaseItem)sender).Tag);
        }

        internal void palmerwormSave_Click(object sender, EventArgs e)
        {
            foreach (Control var in palmerwormItemPanel.Controls)
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
        }

        internal void palmerwormDelete_Click(object sender, EventArgs e)
        {
            if (MsgBox.Show("是否刪除此教師資料？", "", MessageBoxButtons.YesNo) == DialogResult.Yes && MsgBox.Show("是否確定要刪除教師？", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                RemoveTeacher.DeleteTeacher(_TeacherInfo.ID);
                Teacher.Instance.InvokTeacherDataChanged(_TeacherInfo.ID);
            }
        }

        private void palmerwormItemPanel_MouseEnter(object sender, EventArgs e)
        {
            if (palmerwormItemPanel.TopLevelControl.ContainsFocus && !palmerwormItemPanel.ContainsFocus)
                palmerwormItemPanel.Focus();
        }

        private void 刪除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveTeacher.DeleteTeacher(_TeacherInfo.ID);
            Teacher.Instance.InvokTeacherDataChanged(_TeacherInfo.ID);
        }

        private void 一般ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DSXmlHelper helper = new DSXmlHelper("UpdateRequest");
            helper.AddElement("Teacher");
            helper.AddElement("Teacher", "Field");
            helper.AddElement("Teacher/Field", "Status", "一般");
            helper.AddElement("Teacher", "Condition");
            helper.AddElement("Teacher/Condition", "ID", _TeacherInfo.ID);
            EditTeacher.Update(new DSRequest(helper));
            Teacher.Instance.InvokTeacherDataChanged(_TeacherInfo.ID);
        }
    }
}
