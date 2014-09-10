using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Security;
//using SmartSchool.Common;

namespace SmartSchool.Others.RibbonBars
{
    public partial class ScoreOpenTime : SmartSchool.Others.RibbonBars.RibbonBarBase
    {
        FeatureAccessControl setupCtrl;

        public ScoreOpenTime()
        {
            InitializeComponent();

            //�v���P�_ - �䥦/�}��ɶ��]�w
            setupCtrl = new FeatureAccessControl("Button0710");
            setupCtrl.Inspect(btnSetup);
        }

        public override string ProcessTabName
        {
            get
            {
                return "�ǰȧ@�~";
            }
        }

        private void btnSetup_Click(object sender, EventArgs e)
        {
            TeacherDiffOpenConfig.Display();
        }
    }
}

