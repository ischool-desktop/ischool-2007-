using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.ClassRelated.RibbonBars.DeXing;
using SmartSchool.Security;

namespace SmartSchool.ClassRelated.RibbonBars
{
    public partial class TeacherBiasRibbon : RibbonBarBase
    {
        FeatureAccessControl adjustCtrl;
        FeatureAccessControl textCtrl;

        public TeacherBiasRibbon()
        {
            InitializeComponent();
            //SmartSchool.ClassRelated.Class.Instance.SelectionChanged += new EventHandler(Instance_SelectionChanged);
            SmartSchool.Broadcaster.Events.Items["�Z��/����ܧ�"].Handler += delegate
            {
                btnAdjust.Enabled = ( SmartSchool.ClassRelated.Class.Instance.SelectionClasses.Count == 1 );
                adjustCtrl.Inspect(btnAdjust);
                btnWordComment.Enabled = (SmartSchool.ClassRelated.Class.Instance.SelectionClasses.Count == 1);
                textCtrl.Inspect(btnWordComment);
            };

            #region �v��
            //�v���P�_ - �ǰȧ@�~/�w��[���
            adjustCtrl = new FeatureAccessControl("Button0370");
            adjustCtrl.Inspect(btnAdjust);
            //�v���P�_ - �ǰȧ@�~/��r���q
            textCtrl = new FeatureAccessControl("Button0375");
            textCtrl.Inspect(btnWordComment);
            #endregion
        }

        //void Instance_SelectionChanged(object sender, EventArgs e)
        //{
        //    btnAdjust.Enabled = (SmartSchool.ClassRelated.Class.Instance.SelectionClasses.Count == 1);
        //    adjustCtrl.Inspect(btnAdjust);
        //}

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            TeacherBias form = new TeacherBias();
            form.ShowDialog();
        }

        private void btnWordComment_Click(object sender, EventArgs e)
        {
            WordComment form = new WordComment();
            form.ShowDialog();
        }
    }
}
