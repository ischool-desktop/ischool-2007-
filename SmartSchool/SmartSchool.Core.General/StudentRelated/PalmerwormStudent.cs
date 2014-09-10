using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.StudentRelated.Palmerworm;
using SmartSchool.Common;
using System.Xml;
using SmartSchool.Feature;
using SmartSchool.StudentRelated.Process.StudentIUD;
using SmartSchool.ApplicationLog;
//using SmartSchool.StudentRelated.Tag;

namespace SmartSchool.StudentRelated
{
    internal partial class PalmerwormStudent : UserControl
    {
        private IPalmerwormManager _Manager;

        private BriefStudentData _StudentInfo;

        public BriefStudentData StudentInfo
        {
            get { return _StudentInfo; }
            set
            {
                if ( _StudentInfo != value )
                {
                    bool loadAll = _StudentInfo != null && value != null && _StudentInfo.ID != value.ID;
                    _StudentInfo = value;
                    if ( _StudentInfo != null )
                    {
                        lblStudent.Text = string.Format("{0}  {1}  {2}", StudentInfo.ClassName, StudentInfo.Name, StudentInfo.StudentNumber);
                        SyncStatus(StudentInfo.Status);

                        //_summary.LoadById(_StudentInfo.ID);
                        _marks.LoadContent(_StudentInfo.ID);
                        foreach ( Control var in palmerwormItemPanel.Controls )
                        {
                            if ( var is PalmerwormItemContainer )
                            {
                                SmartSchool.Customization.PlugIn.ExtendedContent.IContentItem item = ( (PalmerwormItemContainer)var ).PalmerwormItem;
                                if ( loadAll || ( !item.CancelButtonVisible && !item.SaveButtonVisible ) )
                                    item.LoadContent(_StudentInfo.ID);
                            }
                        }
                    }
                }
            }
        }

        //private string _ID;

        //private ShortSummaryPanel _summary;
        private TagBar _marks;

        public PalmerwormStudent()
        {
            Font = FontStyles.General;

            InitializeComponent();
            lblStudent.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            try
            {
                if (CurrentUser.Instance.IsLogined)
                {
                    //_summary = new ShortSummaryPanel();
                    //_summary.Dock = DockStyle.Left;
                    //palmerwormControlPanel.Controls.Add(_summary);

                    _marks = new TagBar();
                    palmerwormItemPanel.Controls.Add(_marks);

                    foreach (Customization.PlugIn.ExtendedContent.IContentItem var in PalmerwormFactory.Instence.Load())
                    {
                        AddPalmerwormItem(var, false);
                    }

                    XmlElement PreferenceData = CurrentUser.Instance.Preference["PalmerwormStudent"];
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
                    PalmerwormFactory.Instence.ItemsChanged += new EventHandler(Instence_ItemsChanged);
                }
            }
            catch ( Exception e ) { SmartSchool.ExceptionHandler.BugReporter.ReportException(e, false); }
        }

        void Instence_ItemsChanged(object sender, EventArgs e)
        {
            Dictionary<Type, BaseItem> items = new Dictionary<Type, BaseItem>();
            foreach ( BaseItem var in itemContainer1.SubItems )
            {
                items.Add(((Customization.PlugIn.ExtendedContent.IContentItem)var.Tag).GetType(), var);
            };
            foreach ( BaseItem var in itemContainer2.SubItems )
            {
                items.Add(((Customization.PlugIn.ExtendedContent.IContentItem)var.Tag).GetType(), var);
            };
            List<Customization.PlugIn.ExtendedContent.IContentItem> newItems = new List<SmartSchool.Customization.PlugIn.ExtendedContent.IContentItem>();

            foreach ( Customization.PlugIn.ExtendedContent.IContentItem var in PalmerwormFactory.Instence.Load() )
            {
                if ( items.ContainsKey(var.GetType()) )
                    items.Remove(var.GetType());
                else
                    newItems.Add(var);
            }

            foreach ( Type var in items.Keys )
            {
                items[var].Parent.SubItems.Remove(items[var]);
            }
            XmlElement PreferenceData = CurrentUser.Instance.Preference["PalmerwormStudent"];
            foreach ( Customization.PlugIn.ExtendedContent.IContentItem var in newItems )
            {
                if ( PreferenceData != null && PreferenceData.SelectSingleNode("ShowItem[@Type='" + var.GetType().Name + "']") != null )
                    AddPalmerwormItem(var, true);
                else
                    AddPalmerwormItem(var, false);
            }
        }

        //public string ID
        //{
        //    get
        //    { return _ID; }
        //    set
        //    {
        //        _ID = value;
        //        if (string.IsNullOrEmpty(_ID)) return;


        //        summary.LoadById(_ID);
        //        _marks.LoadById(_ID);
        //        foreach (Control var in palmerwormItemPanel.Controls)
        //        {
        //            if (var is PalmerwormItemContainer)
        //            {
        //                ((PalmerwormItemContainer)var).PalmerwoemItem.LoadById(_ID);
        //            }
        //        }
        //    }
        //}

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
            XmlElement PreferenceData = CurrentUser.Instance.Preference["PalmerwormStudent"];
            if (PreferenceData == null)
            {
                PreferenceData = new XmlDocument().CreateElement("PalmerwormStudent");
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
                if (_StudentInfo != null && !string.IsNullOrEmpty(_StudentInfo.ID)) palmerwormItem.LoadContent(_StudentInfo.ID);
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

                XmlNode beremove = PreferenceData.SelectSingleNode("ShowItem[@Type='" + palmerwormItem.GetType().Name + "']");
                if (beremove != null) PreferenceData.RemoveChild(beremove);
            }
            CurrentUser.Instance.Preference["PalmerwormStudent"] = PreferenceData;
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

        private void expandablePanel1_ExpandedChanged(object sender, ExpandedChangeEventArgs e)
        {

            //if (!expandablePanel1.Expanded)
            //{
            //    palmerwormControlPanel.Height = 63;
            //}
            //else
            //{
            //    palmerwormControlPanel.Height = 140;
            //}
        }

        private void palmerwormDelete_Click(object sender, EventArgs e)
        {
            if (MsgBox.Show("是否刪除此學生資料？", "", MessageBoxButtons.YesNo) == DialogResult.Yes &&
                MsgBox.Show("是否確定要刪除學生？", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                RemoveStudent.DeleteStudent(_StudentInfo.ID);
                //Student.Instance.InvokBriefDataChanged(_StudentInfo.ID);
                SmartSchool.Broadcaster.Events.Items["學生/資料變更"].Invoke(_StudentInfo.ID);
                //StudentDeletedEventArgs eva = new StudentDeletedEventArgs();
                //eva.ID = _ID;
                //Student.Instance.InvokStudentDeleted(this, eva);
            }
        }

        private void palmerwormItemPanel_MouseEnter(object sender, EventArgs e)
        {
            if (palmerwormItemPanel.TopLevelControl.ContainsFocus && !palmerwormItemPanel.ContainsFocus)
                palmerwormItemPanel.Focus();
        }

        #region Status Manage

        private void pxStatus_Click(object sender, EventArgs e)
        {
            ctxChangeStatus.Popup(Control.MousePosition);
        }

        private void ChangeStatus_Click(object sender, EventArgs e)
        {
            ButtonItem objStatus = sender as ButtonItem;
            string strNewStatus = objStatus.Tag.ToString();
            string strOrigStatus = StudentInfo.Status;

            try
            {
                Feature.EditStudent.ChangeStudentStatus(StudentInfo.ID, strNewStatus);
                SyncStatus(strNewStatus);

                #region 修改學生狀態 Log

                if (!strOrigStatus.Equals(strNewStatus))
                {
                    StringBuilder desc = new StringBuilder("");
                    desc.AppendLine("學生姓名：" + StudentInfo.Name + " ");
                    desc.AppendLine("狀態由「" + strOrigStatus + "」變更為「" + strNewStatus + "」");
                    CurrentUser.Instance.AppLog.Write(EntityType.Student, "變更狀態", StudentInfo.ID, desc.ToString(), "學生", "");
                }

                #endregion

                //Student.Instance.InvokBriefDataChanged(StudentInfo.ID);
                SmartSchool.Broadcaster.Events.Items["學生/資料變更"].Invoke(StudentInfo.ID);
            }
            catch (Exception)
            {
                MsgBox.Show("變更狀態失敗，請檢查服務是否正常。");
            }
        }

        private void SyncStatus(string p)
        {
            Color s;
            string msg = p;
            switch (p)
            {
                case "一般":
                    s = Color.FromArgb(255, 255, 255);
                    break;
                case "畢業或離校":
                    s = Color.FromArgb(156, 220, 128);
                    break;
                case "休學":
                    s = Color.FromArgb(254, 244, 128);
                    break;
                case "延修":
                    s = Color.FromArgb(224, 254, 210);
                    break;
                case "輟學":
                    s = Color.FromArgb(254, 244, 128);
                    break;
                case "刪除":
                    s = Color.FromArgb(254, 128, 155);
                    break;
                default:
                    msg = "未知";
                    s = Color.Transparent;
                    break;
            }

            pxStatus.Text = p;
            pxStatus.Style.BackColor1.Color = s;
            pxStatus.Style.BackColor2.Color = s;
        }

        #endregion

    }
}