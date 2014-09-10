using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.StudentRelated;
using DevComponents.DotNetBar;
using SmartSchool.ClassRelated;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature.Class;
//using SmartSchool.SmartPlugIn.Common;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Security;

namespace SmartSchool.ClassRelated.RibbonBars
{
    public partial class Assign : RibbonBarBase
    {
        FeatureAccessControl assignCtrl;
        //FeatureAccessControl planCtrl;
        //FeatureAccessControl calcCtrl;

        private ButtonItemPlugInManager reportManager;

        public Assign()
        {
            InitializeComponent();
            btnAssignStudent.Enabled = false;
            SmartSchool.StudentRelated.Student.Instance.TemporalChanged += new EventHandler(Instance_TemporalChanged);
            //SmartSchool.ClassRelated.Class.Instance.SelectionChanged += new EventHandler(Instance_SelectionChanged);
            SmartSchool.Broadcaster.Events.Items["班級/選取變更"].Handler += delegate
            {
                IsButtonEnable();

                assignCtrl.Inspect(btnAssignStudent);
            };
            #region 設定為 "班級/指定" 的外掛處理者
            reportManager = new ButtonItemPlugInManager(itemContainer2);
            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonItem>.Instance.Add("班級/指定", reportManager);
            #endregion

            //權限判斷 - 指定/學生
            assignCtrl = new FeatureAccessControl("Button0380");
            //planCtrl = new FeatureAccessControl("Button0390");
            //calcCtrl = new FeatureAccessControl("Button0400");

            assignCtrl.Inspect(btnAssignStudent);
            //planCtrl.Inspect(btnPlan);
            //calcCtrl.Inspect(btnCalcRule);
        }

        //void Instance_SelectionChanged(object sender, EventArgs e)
        //{
        //    IsButtonEnable();

        //    assignCtrl.Inspect(btnAssignStudent);
        //}

        void Instance_TemporalChanged(object sender, EventArgs e)
        {
            IsButtonEnable();

            assignCtrl.Inspect(btnAssignStudent);
        }

        private bool IsButtonEnable()
        {
            btnPlan.Enabled = SmartSchool.ClassRelated.Class.Instance.SelectionClasses.Count > 0;
            btnCalcRule.Enabled = SmartSchool.ClassRelated.Class.Instance.SelectionClasses.Count > 0;
            int sCount = SmartSchool.StudentRelated.Student.Instance.TemporaStudent.Count;
            int cCount = SmartSchool.ClassRelated.Class.Instance.SelectionClasses.Count;
            bool enable = true;
            string tooltip = "";

            if (sCount == 0)
                tooltip = "必須先選擇待處理學生";
            else if (cCount == 0)
                tooltip = "必須先選擇一個班級";
            else if (cCount > 1)
                tooltip = "只能選擇一個班級";

            enable = string.IsNullOrEmpty(tooltip);
            tooltip = enable ? "可按此處將待處理學生分配至所選擇班級" : tooltip;
            btnAssignStudent.Tooltip = tooltip;
            btnAssignStudent.Enabled = enable;
            return enable;
        }

        private void btnAssignStudent_PopupShowing(object sender, EventArgs e)
        {
            List<BriefStudentData> students = SmartSchool.StudentRelated.Student.Instance.TemporaStudent;
            btnAssignStudent.SubItems.Clear();

            if (students.Count > 1)
            {
                ButtonItem btnAll = new ButtonItem("btnAll", "加入所有待處理學生");
                btnAll.AutoCheckOnClick = false;
                btnAll.Tooltip = "將目前待處理中所有學生,指派給所選班級";
                btnAll.Click += new EventHandler(btnAll_Click);
                btnAssignStudent.InsertItemAt(btnAll, 0, true);
            }

            foreach (BriefStudentData student in students)
            {
                ButtonItem item = new ButtonItem(student.ID, "【" + student.ClassName + "】" + student.Name);
                item.AutoCheckOnClick = true;
                item.Tooltip = "將單一學生【" + student.Name + "】指派給所選班級";
                item.Click += new EventHandler(item_Click);
                item.Tag = student;
                btnAssignStudent.InsertItemAt(item, 0, false);
            }

            if (btnAssignStudent.SubItems.Count > 1)
                btnAssignStudent.SubItems[1].BeginGroup = true;
        }

        void item_Click(object sender, EventArgs e)
        {
            ClassInfo classInfo = SmartSchool.ClassRelated.Class.Instance.SelectionClasses[0];
            ButtonItem item = sender as ButtonItem;
            BriefStudentData student = item.Tag as BriefStudentData;
            if (classInfo.ClassID == student.RefClassID) return;

            AssignSeatNoPicker assForm = new AssignSeatNoPicker(classInfo.ClassID,student.ID);        
            assForm.ShowDialog();            
        }

        void btnAll_Click(object sender, EventArgs e)
        {
            ClassInfo classInfo = SmartSchool.ClassRelated.Class.Instance.SelectionClasses[0];
            List<BriefStudentData> list = SmartSchool.StudentRelated.Student.Instance.TemporaStudent;
            AssignConfirm confirm = new AssignConfirm(classInfo, list);
            confirm.StartPosition = FormStartPosition.CenterParent;
            confirm.ShowDialog();
        }


        #region 課程規劃
        private void buttonItem56_PopupOpen(object sender, DevComponents.DotNetBar.PopupOpenEventArgs e)
        {
            //GraduationPlanSelector selector = SmartSchool.GraduationPlanRelated.GraduationPlan.Instance.GetSelector();
            //selector.GraduationPlanSelected += new EventHandler<GraduationPlanSelectedEventArgs>(selector_GraduationPlanSelected);
            //controlContainerItem1.Control = selector;
            ////controlContainerItem1.RecalcSize();
        }

        //void selector_GraduationPlanSelected(object sender, GraduationPlanSelectedEventArgs e)
        //{

        //    if ( SmartSchool.ClassRelated.Class.Instance.SelectionClasses.Count > 0 )
        //    {
        //        string ErrorMessage = "";
        //        List<string> updateClassIDList = new List<string>();
        //        try
        //        {
        //            DSXmlHelper helper = new DSXmlHelper("UpdateRequest");
        //            helper.AddElement("Class");
        //            helper.AddElement("Class", "Field");
        //            helper.AddElement("Class/Field", "RefGraduationPlanID", e.Item.ID);
        //            helper.AddElement("Class", "Condition");
        //            foreach ( ClassInfo classinfo in SmartSchool.ClassRelated.Class.Instance.SelectionClasses )
        //            {
        //                helper.AddElement("Class/Condition", "ID", classinfo.ClassID);
        //                updateClassIDList.Add(classinfo.ClassID);
        //            }
        //            EditClass.Update(new DSRequest(helper));
        //        }
        //        catch
        //        {
        //            MsgBox.Show("設定班級課程規劃表發生錯誤。");
        //            return;
        //        }
        //        SmartSchool.ClassRelated.Class.Instance.InvokClassUpdated(updateClassIDList.ToArray());
        //        MsgBox.Show("課程規劃表設定完成");
        //    }
        //} 
        #endregion

        #region 計算規則

        private void buttonItem65_PopupOpen(object sender, PopupOpenEventArgs e)
        {
            //foreach ( ButtonItem var in buttonItem65.SubItems )
            //{
            //    var.Click -= new EventHandler(item_Click2);
            //}

            //buttonItem65.SubItems.Clear();
            //foreach ( ScoreCalcRuleInfo var in SmartSchool.ScoreCalcRuleRelated.ScoreCalcRule.Instance.Items )
            //{
            //    ButtonItem item = new ButtonItem(var.ID, var.Name);
            //    item.Tag = var;
            //    item.Click += new EventHandler(item_Click2);
            //    buttonItem65.SubItems.Add(item);
            //}
        }

        void item_Click2(object sender, EventArgs e)
        {
            //List<string> idList = new List<string>();
            //try
            //{
            //    foreach ( SmartSchool.ClassRelated.ClassInfo classInfo in SmartSchool.ClassRelated.Class.Instance.SelectionClasses )
            //    {
            //        ButtonItem item = sender as ButtonItem;
            //        DSRequest dsreq = new DSRequest("<UpdateRequest><Class><Field><RefScoreCalcRuleID>" +
            //            ( item.Tag as ScoreCalcRuleInfo ).ID +
            //            "</RefScoreCalcRuleID></Field><Condition><ID>" +
            //            classInfo.ClassID +
            //            "</ID></Condition></Class></UpdateRequest>");
            //        EditClass.Update(dsreq);
            //        idList.Add(classInfo.ClassID);
            //    }
            //}
            //catch
            //{
            //    SmartSchool.ClassRelated.Class.Instance.InvokClassUpdated(idList.ToArray());
            //    MsgBox.Show("設定計算規則發生錯誤。");
            //    return;
            //}
            //SmartSchool.ClassRelated.Class.Instance.InvokClassUpdated(idList.ToArray());
            //MsgBox.Show("計算規則設定完成");
        } 
        #endregion

    }
}
