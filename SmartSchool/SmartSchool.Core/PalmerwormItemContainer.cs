using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SmartSchool
{
    public partial class PalmerwormItemContainer : ExpandablePanel
    {
        Customization.PlugIn.ExtendedContent.IContentItem _IPalmerwormItem;
        private PalmerwormItemContainer()
        {
            Font = SmartSchool.Common.FontStyles.General;
            Style.BackColor1.Color = Color.Red;

            InitializeComponent();            
        }
        public PalmerwormItemContainer(Customization.PlugIn.ExtendedContent.IContentItem palmerwormItem, int width)
            : this()
        {
            this.Width = width;
            this.expandablePanel1.Width = width;
            PalmerwormItem = palmerwormItem;
        }
        public Customization.PlugIn.ExtendedContent.IContentItem PalmerwormItem
        {
            get
            {
                return _IPalmerwormItem;
            }
            set
            {
                _IPalmerwormItem = value;
                Control item = value.DisplayControl;
                expandablePanel1.Size = new Size(expandablePanel1.Width, item.Size.Height + expandablePanel1.TitleHeight);
                item.Dock = DockStyle.Fill;
                this.expandablePanel1.Controls.Add((Control)item);
                this.TitleText = _IPalmerwormItem.Title;
                _IPalmerwormItem.SaveButtonVisibleChanged += new EventHandler(palmerwormItem_IsDirtyChanged);
                _IPalmerwormItem.CancelButtonVisibleChanged += new EventHandler(_IPalmerwormItem_CancelButtonVisibleChanged);
            }
        }

        void _IPalmerwormItem_CancelButtonVisibleChanged(object sender, EventArgs e)
        {
            undobutton.Visible = _IPalmerwormItem.CancelButtonVisible;
            if ( undobutton.Visible )
            {
                undobutton.Text = "¨ú®ø";
            }
            else
            {
                undobutton.Text = "";
            }
        }

        void palmerwormItem_IsDirtyChanged(object sender, EventArgs e)
        {
            savebutton.Visible = _IPalmerwormItem.SaveButtonVisible;
            if (savebutton.Visible)
            {
                savebutton.Text = "Àx¦s";
                savebutton.Enabled = true;
                if (Attribute.IsDefined(_IPalmerwormItem.GetType(), typeof(SmartSchool.AccessControl.FeatureCodeAttribute)))
                {
                    if (!CurrentUser.Acl[_IPalmerwormItem.GetType()].Editable)
                        savebutton.Enabled = false;
                }
            }
            else
            {
                savebutton.Text = "";
            }
        }

        void savebutton_Click(object sender, System.EventArgs e)
        {
            _IPalmerwormItem.Save();
        }
        void undobutton_Click(object sender, System.EventArgs e)
        {
            _IPalmerwormItem.Undo();
        }
    }
}
