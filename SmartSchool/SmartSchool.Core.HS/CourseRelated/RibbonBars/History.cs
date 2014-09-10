using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.CourseRelated;
using SmartSchool.ApplicationLog.Forms;
using SmartSchool.ApplicationLog;
using SmartSchool.Common;
using SmartSchool.Security;
using System.IO;

namespace SmartSchool.CourseRelated.RibbonBars
{
    public partial class History : SmartSchool.CourseRelated.RibbonBars.RibbonBarBase
    {
        FeatureAccessControl historyCtrl;

        public History()
        {
            InitializeComponent();

            SmartSchool.CourseRelated.CourseEntity.Instance.SelectionChanged += new EventHandler(Instance_SelectionChanged);

            //權限判斷 - 其它/修改歷程
            historyCtrl = new FeatureAccessControl("Button0620");
            historyCtrl.Inspect(btnHistory);

            buttonItem1.Visible = System.IO.File.Exists(Path.Combine(Application.StartupPath, "逃跑吧男孩"));
        }

        void Instance_SelectionChanged(object sender, EventArgs e)
        {
            bool isEnable = SmartSchool.CourseRelated.CourseEntity.Instance.SelectionCourse.Count > 0;
            btnHistory.Enabled =buttonItem1.Enabled= isEnable;
            buttonItem1.Enabled &= SmartSchool.CourseRelated.CourseEntity.Instance.SelectionCourse.Count <= 7;
            historyCtrl.Inspect(btnHistory);
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            CourseEntity ce = CourseEntity.Instance;

            if (ce.SelectionCourse.Count > 0)
            {
                List<string> idlist = new List<string>();
                foreach (CourseInformation each in ce.SelectionCourse)
                    idlist.Add(each.Identity.ToString());

                LogBrowserForm logbro = new LogBrowserForm(EntityType.Course, idlist.ToArray());
                logbro.Show();
            }
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            new SwapAttendStudents().ShowDialog();
        }
    }
}

