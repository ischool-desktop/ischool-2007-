using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ClassEntity = SmartSchool.ClassRelated.Class;
using SmartSchool.ClassRelated;
using SmartSchool.ApplicationLog.Forms;
using SmartSchool.ApplicationLog;
using SmartSchool.Security;

namespace SmartSchool.ClassRelated.RibbonBars
{
    public partial class History : RibbonBarBase
    {
        FeatureAccessControl historyCtrl, surveyCtrl;

        public History()
        {
            InitializeComponent();

            //SmartSchool.ClassRelated.Class.Instance.SelectionChanged += new EventHandler(Instance_SelectionChanged);

            SmartSchool.Broadcaster.Events.Items["班級/選取變更"].Handler += delegate
            {
                bool isEnabled = SmartSchool.ClassRelated.Class.Instance.SelectionClasses.Count > 0;
                btnHistory.Enabled = isEnabled;
                historyCtrl.Inspect(btnHistory);
            };

            //權限判斷 - 修改歷程
            historyCtrl = new FeatureAccessControl("Button0440");
            historyCtrl.Inspect(btnHistory);

            surveyCtrl = new FeatureAccessControl("Button0445");
            surveyCtrl.Inspect(btnSurvey);
        }

        //void Instance_SelectionChanged(object sender, EventArgs e)
        //{
        //    bool isEnabled = SmartSchool.ClassRelated.Class.Instance.SelectionClasses.Count > 0;
        //    btnHistory.Enabled = isEnabled;
        //    historyCtrl.Inspect(btnHistory);
        //}

        private void btnHistory_Click(object sender, EventArgs e)
        {
            ClassEntity ce = ClassEntity.Instance;

            if (ce.SelectionClasses.Count > 0)
            {
                List<string> idlist = new List<string>();
                foreach (ClassInfo each in ce.SelectionClasses)
                    idlist.Add(each.ClassID);

                LogBrowserForm logbro = new LogBrowserForm(EntityType.Class, idlist.ToArray());
                logbro.Show();
            }
        }

        private void btnSurvey_Click(object sender, EventArgs e)
        {
            new SmartSchool.Survey.SurveyWizard().ShowDialog();
        }
    }
}

