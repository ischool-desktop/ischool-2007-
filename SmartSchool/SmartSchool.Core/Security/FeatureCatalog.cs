using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar;

namespace SmartSchool.Security
{
    internal class FeatureCatalog
    {
        private Dictionary<string, ButtonItem> btnMap;
        private DataGridViewX _dgv;
        private ContextMenuBar contextMenuBar;
        private ButtonItem buttonItem1;
        private ButtonItem buttonItem2;
        private ButtonItem buttonItem3;
        private ButtonItem buttonItem4;
        private ButtonItem buttonItem5;

        public event EventHandler PermissionChanged;

        public FeatureCatalog(XmlElement catalogElement, DataGridViewX dgv)
        {
            _features = new Dictionary<string, RowFeature>();
            _dgv = dgv;

            Initialize();

            DSXmlHelper catalogHelper = new DSXmlHelper(catalogElement);
            string[] accessOptions = catalogHelper.GetText("@AccessOptions").Split(',');
            string path = catalogHelper.GetText("@Path");

            if (dgv.Columns[1] is DataGridViewComboBoxColumn)
            {
                DataGridViewComboBoxColumn permissionColumn = dgv.Columns[1] as DataGridViewComboBoxColumn;
                foreach (string strAccessOption in accessOptions)
                {
                    permissionColumn.Items.Add(ParseEnglishToChinese(strAccessOption));
                    buttonItem1.SubItems.Add(btnMap[strAccessOption]);
                }
            }

            foreach (XmlElement itemElement in catalogHelper.GetElements("*"))
            {
                DSXmlHelper itemHelper = new DSXmlHelper(itemElement);

                string name = itemElement.LocalName;
                RowFeature fItem;

                if (name == "ContentItem")
                    fItem = new RowContentItem();
                else if (name == "ButtonItem")
                    fItem = new RowButtonItem();
                else if (name == "ReportItem")
                    fItem = new RowReportItem();
                else if (name == "SystemItem")
                    fItem = new RowSystemItem();
                else
                    fItem = null;

                if (fItem != null)
                {
                    fItem.Tag = path;
                    fItem.FeatureData = itemElement;
                    fItem.RegisterToGridView(dgv, itemElement);
                    _features.Add(fItem.FeatureCode, fItem);
                }
            }
        }

        private void Initialize()
        {
            btnMap = new Dictionary<string, ButtonItem>();

            _dgv.PaintEnhancedSelection = false;
            _dgv.Leave += new EventHandler(dgv_Leave);
            _dgv.Enter += new EventHandler(dgv_Enter);
            _dgv.MouseMove += new MouseEventHandler(dgv_MouseMove);
            _dgv.CellValueChanged += new DataGridViewCellEventHandler(dgv_CellValueChanged);

            contextMenuBar = new ContextMenuBar();

            buttonItem1 = new ButtonItem();
            buttonItem2 = new ButtonItem();
            buttonItem3 = new ButtonItem();
            buttonItem4 = new ButtonItem();
            buttonItem5 = new ButtonItem();

            contextMenuBar.SetContextMenuEx(_dgv, this.buttonItem1);
            contextMenuBar.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            contextMenuBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            buttonItem1});
            contextMenuBar.Location = new System.Drawing.Point(38, 51);
            contextMenuBar.Name = "contextMenuBar1";
            contextMenuBar.Size = new System.Drawing.Size(137, 27);
            contextMenuBar.Stretch = true;
            contextMenuBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            contextMenuBar.TabIndex = 7;
            contextMenuBar.TabStop = false;
            contextMenuBar.Text = "contextMenuBar1";

            buttonItem1.AutoExpandOnClick = true;
            buttonItem1.ImagePaddingHorizontal = 8;
            buttonItem1.Name = "buttonItem1";
            buttonItem1.Text = "權限";

            buttonItem2.ImagePaddingHorizontal = 8;
            buttonItem2.Name = "buttonItem2";
            buttonItem2.Text = "變更權限為「檢視」";
            buttonItem2.Click += new System.EventHandler(buttonItem2_Click);

            buttonItem3.ImagePaddingHorizontal = 8;
            buttonItem3.Name = "buttonItem3";
            buttonItem3.Text = "變更權限為「編輯」";
            buttonItem3.Click += new System.EventHandler(buttonItem3_Click);

            buttonItem4.ImagePaddingHorizontal = 8;
            buttonItem4.Name = "buttonItem4";
            buttonItem4.Text = "變更權限為「執行」";
            buttonItem4.Click += new System.EventHandler(buttonItem4_Click);

            buttonItem5.ImagePaddingHorizontal = 8;
            buttonItem5.Name = "buttonItem5";
            buttonItem5.Text = "變更權限為「無」";
            buttonItem5.Click += new System.EventHandler(buttonItem5_Click);

            btnMap.Add("View", buttonItem2);
            btnMap.Add("Edit", buttonItem3);
            btnMap.Add("Execute", buttonItem4);
            btnMap.Add("None", buttonItem5);
        }

        private object ParseEnglishToChinese(string en)
        {
            switch (en)
            {
                case "View":
                    return "檢視";
                case "Edit":
                    return "編輯";
                case "Execute":
                    return "執行";
                default:
                    return "無";
            }
        }

        private Dictionary<string, RowFeature> _features;
        public Dictionary<string, RowFeature> Features
        {
            get { return _features; }
        }

        public void HideSelection()
        {
            _dgv.PaintEnhancedSelection = false;
        }

        public void ShowSelection()
        {
            _dgv.PaintEnhancedSelection = true;
        }

        private void dgv_Leave(object sender, EventArgs e)
        {
            HideSelection();
        }

        private void dgv_Enter(object sender, EventArgs e)
        {
            ShowSelection();
        }

        private void dgv_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_dgv.Focused)
                _dgv.Focus();
        }

        void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (PermissionChanged != null)
                PermissionChanged.Invoke(this, new EventArgs());
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            ContextMenuClick("檢視");
        }
        private void buttonItem3_Click(object sender, EventArgs e)
        {
            ContextMenuClick("編輯");
        }
        private void buttonItem4_Click(object sender, EventArgs e)
        {
            ContextMenuClick("執行");
        }
        private void buttonItem5_Click(object sender, EventArgs e)
        {
            ContextMenuClick("無");
        }

        private void ContextMenuClick(string perm)
        {
            _dgv.CellValueChanged -= new DataGridViewCellEventHandler(dgv_CellValueChanged);
            foreach (DataGridViewRow each in _dgv.Rows)
                if (each.Selected)
                    each.Cells[1].Value = perm;
            if (PermissionChanged != null)
                PermissionChanged.Invoke(this, new EventArgs());
            _dgv.CellValueChanged += new DataGridViewCellEventHandler(dgv_CellValueChanged);
        }
    }
}
