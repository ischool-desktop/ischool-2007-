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
using SmartSchool;
using SmartSchool.Common;
using SmartSchool.Security;

namespace SmartSchool.Others.RibbonBars
{
    public partial class UnfinishScore : RibbonBarBase, SmartSchool.Customization.PlugIn.IManager<ButtonItem>
    {
        FeatureAccessControl openCtrl;

        public UnfinishScore()
        {
            InitializeComponent();
            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonItem>.Instance.Add(@"教務作業/成績作業", this);

            //權限判斷 - 成績作業/評量輸入狀況
            openCtrl = new FeatureAccessControl("Button0660");
            openCtrl.Inspect(btnOpen);
        }

        public override string ProcessTabName
        {
            get
            {
                return "教務作業";
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            UnfinishedList form = new UnfinishedList();
            form.ShowDialog();
        }

        private void MainRibbonBar_Click(object sender, EventArgs e)
        {
            //if (Control.ModifierKeys == Keys.Shift)
            //    btnRank.Enabled = true;
        }

        private void btnRankSemester_Click(object sender, EventArgs e)
        {
            //new SemesterRatingForm().ShowDialog();
        }

        private void btnRankSchoolYear_Click(object sender, EventArgs e)
        {
            //new SchoolYearRatingForm().ShowDialog();
        }

        #region IManager<BaseItem> 成員

        public void Add(ButtonItem instance)
        {
            this.itemContainer1.SubItems.Add(instance, 0);
        }

        public void Remove(ButtonItem instance)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
