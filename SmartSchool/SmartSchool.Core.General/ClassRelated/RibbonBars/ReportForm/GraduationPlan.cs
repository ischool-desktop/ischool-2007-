using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Feature.GraduationPlan;
using System.Xml;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature.Class;
using SmartSchool.StudentRelated;
using SmartSchool.ClassRelated;

namespace SmartSchool.ClassRelated.RibbonBars
{
    public partial class GraduationPlan : SmartSchool.ClassRelated.RibbonBars.RibbonBarBase
    {
        public override DevComponents.DotNetBar.RibbonBar ProcessRibbon
        {
            get
            {
                return base.ProcessRibbon;
            }
        }

        public GraduationPlan()
        {
            InitializeComponent();

            //SmartSchool.ClassRelated.Class.Instance.SelectionChanged += new EventHandler(Instance_SelectionChanged);
            SmartSchool.Broadcaster.Events.Items["班級/選取變更"].Handler += delegate
            {
                buttonItem56.Enabled = SmartSchool.ClassRelated.Class.Instance.SelectionClasses.Count > 0;
            };
        }

        //private void Instance_SelectionChanged(object sender, EventArgs e)
        //{
        //    buttonItem56.Enabled = SmartSchool.ClassRelated.Class.Instance.SelectionClasses.Count > 0;
        //}

        private void buttonItem56_PopupOpen(object sender, DevComponents.DotNetBar.PopupOpenEventArgs e)
        {
            //GraduationPlanSelector selector = SmartSchool.GraduationPlanRelated.GraduationPlan.Instance.GetSelector();
            //selector.GraduationPlanSelected += new EventHandler<GraduationPlanSelectedEventArgs>(selector_GraduationPlanSelected);
            //controlContainerItem1.Control = selector;
            ////controlContainerItem1.RecalcSize();
        }

        //void selector_GraduationPlanSelected(object sender, GraduationPlanSelectedEventArgs e)
        //{

        //    if (SmartSchool.ClassRelated.Class.Instance.SelectionClasses.Count > 0)
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
        //            foreach (ClassInfo classinfo in SmartSchool.ClassRelated.Class.Instance.SelectionClasses)
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
    }
}