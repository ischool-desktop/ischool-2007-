using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.Security;
using SmartSchool.StudentRelated.RibbonBars.AcademicAffairs;

namespace SmartSchool.StudentRelated.RibbonBars
{
    public partial class EducationalAdministration : RibbonBarBase
    {
        FeatureAccessControl placeCtrl;
        FeatureAccessControl diplomaCtrl;
        FeatureAccessControl lvlEduCtrl;

        ButtonItemPlugInManager reportManager;

        public EducationalAdministration()
        {
            InitializeComponent();
            //SmartSchool.StudentRelated.Student.Instance.SelectionChanged += new EventHandler(Instance_SelectionChanged);
            SmartSchool.Broadcaster.Events.Items["學生/選取變更"].Handler += delegate
            {
                btnDiploma.Enabled = SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count > 0;
                btnPlacing.Enabled = SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count > 0;
                btnEduLevel.Enabled = SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count > 0;

                placeCtrl.Inspect(btnPlacing);
                diplomaCtrl.Inspect(btnDiploma);
                lvlEduCtrl.Inspect(btnEduLevel);
            };
            #region 設定為 "學生/教務作業" 的外掛處理者
            reportManager = new ButtonItemPlugInManager(itemContainer2);
            reportManager.LayoutMode = LayoutMode.Auto;
            reportManager.ItemsChanged += new EventHandler(reportManager_ItemsChanged);
            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonItem>.Instance.Add("學生/教務作業", reportManager);
            #endregion

            //權限判斷 - 排名	Button0050
            placeCtrl = new FeatureAccessControl("Button0050");

            //權限判斷 - 證書字號	Button0090
            diplomaCtrl = new FeatureAccessControl("Button0090");

            //權限判斷 - 教育程度檔	Button0092
            lvlEduCtrl = new FeatureAccessControl("Button0092");

            placeCtrl.Inspect(btnPlacing);
            diplomaCtrl.Inspect(btnDiploma);
            lvlEduCtrl.Inspect(btnEduLevel);
        }

        void reportManager_ItemsChanged(object sender, EventArgs e)
        {
            itemContainer2.SubItems.Remove(this.btnPlacing);
            itemContainer2.SubItems.Remove(this.btnMetagenesis);
            itemContainer2.SubItems.Add(this.btnPlacing);
            itemContainer2.SubItems.Add(this.btnMetagenesis);
        }

        //void Instance_SelectionChanged(object sender, EventArgs e)
        //{
        //    btnDiploma.Enabled = SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count > 0;
        //    btnPlacing.Enabled = SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count > 0;
        //    btnEduLevel.Enabled = SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count > 0;

        //    placeCtrl.Inspect(btnPlacing);
        //    diplomaCtrl.Inspect(btnDiploma);
        //    lvlEduCtrl.Inspect(btnEduLevel);
        //}

        public override string ProcessTabName
        {
            get { return "學生"; }
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            DiplomaNumberCreator creator = new DiplomaNumberCreator();
            creator.ShowDialog();
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            SmartSchool.StudentRelated.Placing.PlaceForm form = new SmartSchool.StudentRelated.Placing.PlaceForm();
            form.ShowDialog();
        }

        private void btnEduLevel_Click(object sender, EventArgs e)
        {
            LevelOfEducationForm form = new LevelOfEducationForm();
            form.ShowDialog();
        }
    }
}
