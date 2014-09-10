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
            SmartSchool.Broadcaster.Events.Items["�ǥ�/����ܧ�"].Handler += delegate
            {
                btnDiploma.Enabled = SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count > 0;
                btnPlacing.Enabled = SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count > 0;
                btnEduLevel.Enabled = SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count > 0;

                placeCtrl.Inspect(btnPlacing);
                diplomaCtrl.Inspect(btnDiploma);
                lvlEduCtrl.Inspect(btnEduLevel);
            };
            #region �]�w�� "�ǥ�/�аȧ@�~" ���~���B�z��
            reportManager = new ButtonItemPlugInManager(itemContainer2);
            reportManager.LayoutMode = LayoutMode.Auto;
            reportManager.ItemsChanged += new EventHandler(reportManager_ItemsChanged);
            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonItem>.Instance.Add("�ǥ�/�аȧ@�~", reportManager);
            #endregion

            //�v���P�_ - �ƦW	Button0050
            placeCtrl = new FeatureAccessControl("Button0050");

            //�v���P�_ - �ҮѦr��	Button0090
            diplomaCtrl = new FeatureAccessControl("Button0090");

            //�v���P�_ - �Ш|�{����	Button0092
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
            get { return "�ǥ�"; }
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
