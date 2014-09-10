using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TeacherEntity = SmartSchool.TeacherRelated.Teacher;
using SmartSchool.TeacherRelated;
using SmartSchool.ApplicationLog.Forms;
using SmartSchool.ApplicationLog;
using SmartSchool.Common;
using SmartSchool.Security;

namespace SmartSchool.TeacherRelated.RibbonBars
{
    public partial class History : SmartSchool.TeacherRelated.RibbonBars.RibbonBarBase
    {
        FeatureAccessControl historyCtrl;

        public History()
        {
            InitializeComponent();

            SmartSchool.TeacherRelated.Teacher.Instance.SelectionChanged += new EventHandler(Instance_SelectionChanged);

            //權限判斷 - 其它/修改歷程
            historyCtrl = new FeatureAccessControl("Button0510");
            historyCtrl.Inspect(btnHistory);
        }

        void Instance_SelectionChanged(object sender, EventArgs e)
        {
            bool isEnable = SmartSchool.TeacherRelated.Teacher.Instance.SelectionTeachers.Count > 0;
            btnHistory.Enabled = isEnable;

            historyCtrl.Inspect(btnHistory);
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            TeacherEntity te = TeacherEntity.Instance;

            if (te.SelectionTeachers.Count > 0)
            {
                List<string> idlist = new List<string>();
                foreach (BriefTeacherData each in te.SelectionTeachers)
                    idlist.Add(each.ID);

                LogBrowserForm logbro = new LogBrowserForm(EntityType.Teacher, idlist.ToArray());
                logbro.Show();
            }
        }
    }
}

