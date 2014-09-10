using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Xml;
using SmartSchool.Customization.Data;

namespace SmartSchool.ArchivalImage
{
    public partial class TagConfig : Office2007Form
    {
        XmlElement _categories;
        Dictionary<string, string> _tagList;

        public TagConfig(XmlElement categories, Dictionary<string, string> tagList)
        {
            InitializeComponent();
            _categories = categories;
            _tagList = tagList;
        }

        private void TagConfig_Load(object sender, EventArgs e)
        {
            foreach (XmlElement tag in _categories.SelectNodes("Tag"))
            {
                ListViewItem item = new ListViewItem();
                string prefix = tag.SelectSingleNode("Prefix").InnerText;
                string name = tag.SelectSingleNode("Name").InnerText;
                string id = tag.GetAttribute("ID");
                if (!string.IsNullOrEmpty(prefix))
                    name = prefix + ":" + name;
                item.Text = name;
                item.Tag = id;
                if (_tagList.ContainsKey(id))
                    item.Checked = true;
                listViewEx1.Items.Add(item);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            #region 儲存 Preference

            XmlElement config = SmartSchool.Customization.Data.SystemInformation.Preference["學生學籍表"];

            if (config == null)
            {
                config = new XmlDocument().CreateElement("學生學籍表");
            }

            XmlElement newTags = config.OwnerDocument.CreateElement("Tags");
            foreach (ListViewItem item in listViewEx1.Items)
            {
                if (item.Checked == true)
                {
                    XmlElement tag = newTags.OwnerDocument.CreateElement("Tag");
                    tag.SetAttribute("ID", item.Tag as string);
                    tag.SetAttribute("FullName", item.Text);
                    newTags.AppendChild(tag);
                }
            }

            XmlElement tags = (XmlElement)config.SelectSingleNode("Tags");
            if (tags != null)
                config.ReplaceChild(newTags, tags);
            else
                config.AppendChild(newTags);

            SmartSchool.Customization.Data.SystemInformation.Preference["學生學籍表"] = config;

            #endregion

            this.DialogResult = DialogResult.OK;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}