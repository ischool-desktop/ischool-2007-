using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.Customization.Data.StudentExtension;
using SmartSchool.Customization.Data;
using Aspose.Cells;
using SmartSchool.StudentRelated;
using System.Xml;
using System.Threading;
using IntelliSchool.DSA30.Util;
using System.IO;
using System.Diagnostics;
//using SmartSchool;
//using SmartSchool.SmartPlugIn.Common;

namespace SmartSchool.Others.RibbonBars
{
    public partial class AwardScore : RibbonBarBase
    {
        ButtonItemPlugInManager reportManager;

        public AwardScore()
        {
            InitializeComponent();
            #region 設定為 "學務作業/成績處理" 的外掛處理者
            reportManager = new ButtonItemPlugInManager(itemContainer2);
            reportManager.LayoutMode = LayoutMode.Auto;
            reportManager.ItemsChanged += new EventHandler(reportManager_ItemsChanged);
            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonItem>.Instance.Add("學務作業/成績作業", reportManager);
            #endregion
        }

        void reportManager_ItemsChanged(object sender, EventArgs e)
        {
            this.Visible = itemContainer2.SubItems.Count > 0;
        }

        public override string ProcessTabName
        {
            get
            {
                return "學務作業";
            }
        }
    }
}
