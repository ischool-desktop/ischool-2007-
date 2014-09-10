using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.StudentRelated.RibbonBars.Discipline;
using SmartSchool.Common;
using DevComponents.DotNetBar;
using SmartSchool.StudentRelated.RibbonBars.AttendanceEditor;
//using SmartSchool.SmartPlugIn.Common;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Security;

namespace SmartSchool.StudentRelated.RibbonBars
{
    public partial class Doctrine : RibbonBarBase
    {
        FeatureAccessControl meritCtrl;
        FeatureAccessControl awardCtrl;
        FeatureAccessControl attendanceCtrl;

        ButtonItemPlugInManager reportManager;

        public Doctrine()
        {
            InitializeComponent();

            superTooltip1.DefaultFont = FontStyles.General;

            //SmartSchool.StudentRelated.Student.Instance.SelectionChanged += new EventHandler(Instance_SelectionChanged);
            SmartSchool.Broadcaster.Events.Items["學生/選取變更"].Handler += new EventHandler<SmartSchool.Broadcaster.EventArguments>(Award_Handler);
            #region 設定為 "學生/學務作業" 的外掛處理者
            reportManager = new ButtonItemPlugInManager(itemContainer1);
            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonItem>.Instance.Add("學生/學務作業", reportManager);
            #endregion

            //權限判斷 - 獎勵	Button0060
            meritCtrl = new FeatureAccessControl("Button0060");
            //權限判斷 - 懲戒	Button0070
            awardCtrl = new FeatureAccessControl("Button0070");
            //權限判斷 - 缺曠	Button0080
            attendanceCtrl = new FeatureAccessControl("Button0080");

            meritCtrl.Inspect(btnMerit);
            awardCtrl.Inspect(btnAward);
            attendanceCtrl.Inspect(btnAttendance);

            #region 加入登錄缺曠的右鍵功能
            if ( attendanceCtrl.Executable() )
            {
                ButtonAdapter contexMenuButton = new ButtonAdapter();
                contexMenuButton.Text = "登錄缺曠";
                contexMenuButton.Image = btnAttendance.Image;
                contexMenuButton.OnClick += new EventHandler(btnAttendance_Click);
                SmartSchool.Customization.PlugIn.ContextMenu.StudentMenuButton.AddItem(contexMenuButton);
            }
            #endregion
            #region 加入記功過的右鍵功能
            if ( meritCtrl.Executable() || awardCtrl.Executable() )
            {
                ButtonAdapter contexMenuButton = new ButtonAdapter();
                contexMenuButton.Text = "登錄獎懲";
                contexMenuButton.Image = btnMerit.Image;
                SmartSchool.Customization.PlugIn.ContextMenu.StudentMenuButton.AddItem(contexMenuButton);
            }
            if ( meritCtrl.Executable() )
            {
                ButtonAdapter contexMenuButton = new ButtonAdapter();
                contexMenuButton.Path = "登錄獎懲";
                contexMenuButton.Text = "獎勵";
                contexMenuButton.Image = btnMerit.Image;
                contexMenuButton.OnClick += new EventHandler(bItemMerit_Click);
                SmartSchool.Customization.PlugIn.ContextMenu.StudentMenuButton.AddItem(contexMenuButton);
            }
            if ( awardCtrl.Executable() )
            {
                ButtonAdapter contexMenuButton = new ButtonAdapter();
                contexMenuButton.Path = "登錄獎懲";
                contexMenuButton.Text = "懲戒";
                contexMenuButton.Image = btnAward.Image;
                contexMenuButton.OnClick += new EventHandler(btnDemerit_Click);
                SmartSchool.Customization.PlugIn.ContextMenu.StudentMenuButton.AddItem(contexMenuButton);

                contexMenuButton = new ButtonAdapter();
                contexMenuButton.Path = "登錄獎懲";
                contexMenuButton.Text = "銷過";
                contexMenuButton.OnClick += new EventHandler(btnClearDemerit_Click);
                SmartSchool.Customization.PlugIn.ContextMenu.StudentMenuButton.AddItem(contexMenuButton);
            }
            #endregion
        }

        void Award_Handler(object sender, SmartSchool.Broadcaster.EventArguments e)
        {
            SuperTooltipInfo superTooltipInfo = new SuperTooltipInfo();
            int count = SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count;
            superTooltipInfo.BodyText = "按此編輯學生缺曠紀錄";
            btnAttendance.Enabled = true;
            if ( count == 0 )
            {
                superTooltipInfo.BodyText = "請先選擇學生";
                btnAttendance.Enabled = false;
            }
            superTooltip1.SetSuperTooltip(btnAttendance, superTooltipInfo);

            SuperTooltipInfo info = new SuperTooltipInfo();
            SuperTooltipInfo info2 = new SuperTooltipInfo();
            if ( SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count > 0 )
            {
                btnMerit.Enabled = true;
                btnAward.Enabled = true;
                info.BodyText = "登錄學生獎勵資料";
                info2.BodyText = "登錄學生懲戒資料";
            }
            else
            {
                btnMerit.Enabled = false;
                btnAward.Enabled = false;
                info.BodyText = "請先選擇至少一名學生";
                info2.BodyText = "請先選擇至少一名學生";
            }
            superTooltip1.SetSuperTooltip(btnMerit, info);
            superTooltip1.SetSuperTooltip(btnAward, info2);

            SuperTooltipInfo infoClear = new SuperTooltipInfo();
            if ( SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count == 1 )
            {
                btnClearDemerit.Enabled = true;
                infoClear.BodyText = "處理學生銷過作業";
            }
            else
            {
                btnClearDemerit.Enabled = false;
                infoClear.BodyText = "請選擇一名學生進行銷過作業";
            }
            superTooltip1.SetSuperTooltip(btnClearDemerit, infoClear);

            meritCtrl.Inspect(btnMerit);
            awardCtrl.Inspect(btnAward);
            attendanceCtrl.Inspect(btnAttendance);
        }

        //public void Instance_SelectionChanged(object sender, EventArgs e)
        //{
        //    SuperTooltipInfo superTooltipInfo = new SuperTooltipInfo();
        //    int count = SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count;
        //    superTooltipInfo.BodyText = "按此編輯學生缺曠紀錄";
        //    btnAttendance.Enabled = true;
        //    if (count == 0)
        //    {
        //        superTooltipInfo.BodyText = "請先選擇學生";
        //        btnAttendance.Enabled = false;
        //    }
        //    superTooltip1.SetSuperTooltip(btnAttendance, superTooltipInfo);

        //    SuperTooltipInfo info = new SuperTooltipInfo();
        //    SuperTooltipInfo info2 = new SuperTooltipInfo();
        //    if (SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count > 0)
        //    {
        //        btnMerit.Enabled = true;
        //        btnAward.Enabled = true;
        //        info.BodyText = "登錄學生獎勵資料";
        //        info2.BodyText = "登錄學生懲戒資料";
        //    }
        //    else
        //    {
        //        btnMerit.Enabled = false;
        //        btnAward.Enabled = false;
        //        info.BodyText = "請先選擇至少一名學生";
        //        info2.BodyText = "請先選擇至少一名學生";
        //    }
        //    superTooltip1.SetSuperTooltip(btnMerit, info);
        //    superTooltip1.SetSuperTooltip(btnAward, info2);

        //    SuperTooltipInfo infoClear = new SuperTooltipInfo();
        //    if (SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count == 1)
        //    {
        //        btnClearDemerit.Enabled = true;
        //        infoClear.BodyText = "處理學生銷過作業";
        //    }
        //    else
        //    {
        //        btnClearDemerit.Enabled = false;
        //        infoClear.BodyText = "請選擇一名學生進行銷過作業";
        //    }
        //    superTooltip1.SetSuperTooltip(btnClearDemerit, infoClear);

        //    meritCtrl.Inspect(btnMerit);
        //    awardCtrl.Inspect(btnAward);
        //    attendanceCtrl.Inspect(btnAttendance);
        //}

        public override string ProcessTabName
        {
            get
            {
                return "學生";
            }
        }

        private void bItemMerit_Click(object sender, EventArgs e)
        {
            InsertEditor editor = new InsertEditor(SmartSchool.StudentRelated.Student.Instance.SelectionStudents, true);
            editor.ShowDialog();
        }

        private void btnDemerit_Click(object sender, EventArgs e)
        {
            DemeritEditor editor = new DemeritEditor(SmartSchool.StudentRelated.Student.Instance.SelectionStudents, false);
            editor.ShowDialog();
        }

        private void btnClearDemerit_Click(object sender, EventArgs e)
        {
            if (SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count > 0)
            {
                ClearDemerit cd = new ClearDemerit(SmartSchool.StudentRelated.Student.Instance.SelectionStudents[0]);
                cd.ShowDialog();
            }
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            int count = SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count;
            if (count == 1)
            {
                SingleEditor editor = new SingleEditor(SmartSchool.StudentRelated.Student.Instance.SelectionStudents[0]);
                editor.ShowDialog();
            }
            else
            {
                MutiEditor editor = new MutiEditor(SmartSchool.StudentRelated.Student.Instance.SelectionStudents);
                editor.ShowDialog();
            }
        }
    }
}
