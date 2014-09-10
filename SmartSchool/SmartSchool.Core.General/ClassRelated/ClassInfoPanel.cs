using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevComponents.DotNetBar;
using SmartSchool.Common;
using SmartSchool.Feature.Class;
using SmartSchool.Feature;
using SmartSchool.StudentRelated;
using IntelliSchool.DSA30.Util;
using System.Xml;

namespace SmartSchool.ClassRelated
{
    public partial class ClassInfoPanel : UserControl
    {
        private List<SmartSchool.Customization.PlugIn.ExtendedContent.IContentItem> _worms;
        //private SummaryItem _summary;
        private bool _saveableStatus = false;
        //private string _classid;
        private ClassInfo _ClassInfo;

        private IPalmerwormManager _Manager;
        //public event EventHandler<SaveableEventArgs> SaveableChanged;

        public ClassInfoPanel()
        {
            InitializeComponent();

            lblClassName.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));

            //_summary = new SummaryItem();
            _worms = ClassPalmerwormFactory.GetItems();

            //this.palmerwormControlPanel.Controls.Add(_summary);
            //_summary.Dock = DockStyle.Left;

            foreach (SmartSchool.Customization.PlugIn.ExtendedContent.IContentItem classWorm in _worms)
            {
                classWorm.SaveButtonVisibleChanged += new EventHandler(classWorm_IsDirtyChanged);
                int i = btnShowItems.SubItems.Add(new CheckBoxItem(classWorm.Title, classWorm.Title));
                CheckBoxItem item = (CheckBoxItem)btnShowItems.SubItems[i];
                item.AutoCollapseOnClick = false;
                item.Checked = false;
                item.CheckedChanged += new CheckBoxChangeEventHandler(item_CheckedChanged);
            }
            XmlElement PreferenceData = CurrentUser.Instance.Preference["PalmerwormClass"];
            if (PreferenceData != null)
            {
                foreach (XmlNode var in PreferenceData.SelectNodes("ShowItem"))
                {
                    try
                    {
                        foreach (BaseItem itemToShow in btnShowItems.SubItems)
                        {
                            if (itemToShow.Text == var.Attributes["Type"].Value)
                            {
                                ((CheckBoxItem)itemToShow).Checked = true;
                                break;
                            }
                        }
                    }
                    catch (Exception e)
                    { 
                        
                    }
                }
            }
            ReloadShownItems();
        }

        internal IPalmerwormManager Manager
        {
            get { return _Manager; }
            set
            {
                if (_Manager != null)
                    _Manager.Save -= new EventHandler(_Manager_Save);
                _Manager = value;
                if (_Manager != null)
                {
                    _Manager.Save += new EventHandler(_Manager_Save);
                    _Manager.EnableSave = CheckToSave();
                }
            }
        }

        void _Manager_Save(object sender, EventArgs e)
        {
            Save();
        }

        void classWorm_IsDirtyChanged(object sender, EventArgs e)
        {
            if(_Manager!=null)
                _Manager.EnableSave = CheckToSave();
            //bool isSaveable = CheckToSave();
            //if (isSaveable != _saveableStatus && SaveableChanged != null)
            //{
            //    SaveableEventArgs args = new SaveableEventArgs();
            //    args.Saveable = isSaveable;
            //    SaveableChanged(this, args);
            //}
            //_saveableStatus = isSaveable;
        }

        void item_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            CheckBoxItem item = (CheckBoxItem)sender;
            XmlElement PreferenceData = CurrentUser.Instance.Preference["PalmerwormClass"];
            if (PreferenceData == null)
            {
                PreferenceData = new XmlDocument().CreateElement("PalmerwormClass");
            }
            string Text = item.Text;
            if (item.Checked)
            {
                if (PreferenceData.SelectSingleNode("ShowItem[@Type='" + item.Text + "']") == null)
                {
                    XmlElement show = PreferenceData.OwnerDocument.CreateElement("ShowItem");
                    show.SetAttribute("Type", item.Text);
                    PreferenceData.AppendChild(show);
                }
            }
            else
            {
                XmlNode beremove = PreferenceData.SelectSingleNode("ShowItem[@Type='" + item.Text + "']");
                if (beremove != null) PreferenceData.RemoveChild(beremove);
            }
            CurrentUser.Instance.Preference["PalmerwormClass"] = PreferenceData;
            ReloadShownItems();
            if (item.Checked && _ClassInfo != null)
            {
                foreach (SmartSchool.Customization.PlugIn.ExtendedContent.IContentItem classWorm in _worms)
                {
                    if (classWorm.Title == item.Text)
                    {
                        classWorm.LoadContent(_ClassInfo.ClassID);
                        break;
                    }
                }
            }
        }

        private void ReloadShownItems()
        {
            palmerwormItemPanel.Controls.Clear();

            foreach (CheckBoxItem item in btnShowItems.SubItems)
            {
                if (item.Checked)
                {
                    foreach (SmartSchool.Customization.PlugIn.ExtendedContent.IContentItem classWorm in _worms)
                    {
                        if (classWorm.Title == item.Text)
                        {
                            PalmerwormItemContainer panel = new PalmerwormItemContainer(classWorm, 550);
                            palmerwormItemPanel.Controls.Add(panel);
                            break;
                        }
                    }
                }
            }
        }

        public void Initialize(string classid)
        {
            //_summary.LoadById(classid);
            //_summary.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            //foreach (IClassPalmerwormItem classWorm in _worms)
            //{
            //    classWorm.LoadById(classid);
            //}
            foreach (CheckBoxItem item in btnShowItems.SubItems)
            {
                if (item.Checked)
                {
                    foreach (SmartSchool.Customization.PlugIn.ExtendedContent.IContentItem classWorm in _worms)
                    {
                        if (classWorm.Title == item.Text)
                        {
                            classWorm.LoadContent(classid);
                            break;
                        }
                    }
                }
            }
        }

        //public string ClassID
        //{
        //    get { return _classid; }
        //    set
        //    {
        //        Initialize(value);
        //    }
        //}

        public ClassInfo ClassInfo
        {
            get { return _ClassInfo; }
            set
            {
                _ClassInfo = value;
                if (_ClassInfo != null)
                {
                    Initialize(_ClassInfo.ClassID);
                    lblClassName.Text = ClassInfo.ClassName;
                }
            }
        }

        internal bool CheckToSave()
        {
            foreach (Control var in palmerwormItemPanel.Controls)
            {
                if (var is PalmerwormItemContainer)
                    return ((PalmerwormItemContainer)var).PalmerwormItem.SaveButtonVisible;
            }
            return false;
        }

        public void Save()
        {
            bool isValid = true;
            foreach (IClassPalmerwormItem classWorm in _worms)
            {
                if (!classWorm.IsValid())
                    isValid = false;
            }
            if (isValid)
            {
                foreach (IClassPalmerwormItem classWorm in _worms)
                {
                    if (CurrentUser.Acl[classWorm.GetType()].Editable)
                        classWorm.Save();
                }
            }
        }

        public bool IsValid()
        {
            bool isValid = true;
            foreach (IClassPalmerwormItem classWorm in _worms)
            {
                if (!classWorm.IsValid())
                    isValid = false;
            }
            return isValid;
        }

        //public void Delete()
        //{
        //    string message = "注意！此動作將會使該班學生列入未分班學生之中，您確定將刪除此班級？";
        //    if (!string.IsNullOrEmpty(_classid) && MsgBox.Show(message, "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //    {
        //        List<string> idList = new List<string>();
        //        foreach (ClassInfo var in Class.Instance.SelectionClasses)
        //        {
        //            idList.Add(var.ClassID);
        //        }
        //        EditClass.Delete(idList.ToArray());
        //        DeleteClassEventArgs args = new DeleteClassEventArgs();
        //        args.DeleteClassIDArray = idList.ToArray();
        //        ClassRelated.Class.Instance.InvokClassDeleted(args);
        //        MsgBox.Show("班級刪除完成");
        //    }
        //}
    }

    //public class SaveableEventArgs : EventArgs
    //{
    //    private bool _saveable;

    //    public bool Saveable
    //    {
    //        get { return _saveable; }
    //        set { _saveable = value; }
    //    }
    //}
}
