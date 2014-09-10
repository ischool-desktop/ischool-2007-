using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Security;
using SmartSchool.Customization.Data;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature.Class;
using DevComponents.DotNetBar;

namespace SmartSchool.ClassRelated.RibbonBars
{
    public partial class Upgrade : RibbonBarBase
    {
        FeatureAccessControl upgradeCtrl;

        ButtonItemPlugInManager reportManager;

        public Upgrade()
        {
            InitializeComponent();
            #region 設定為 "班級/教務作業" 的外掛處理者
            reportManager = new ButtonItemPlugInManager(itemContainer1);
            reportManager.LayoutMode = LayoutMode.Auto;
            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonItem>.Instance.Add("班級/教務作業", reportManager);
            #endregion

            //權限判斷 - 班級升級
            upgradeCtrl = new FeatureAccessControl("Button0360");
            upgradeCtrl.Inspect(btnUpgrade);
            //Class.Instance.SelectionChanged += new EventHandler(Instance_SelectionChanged);
            SmartSchool.Broadcaster.Events.Items["班級/選取變更"].Handler += delegate
            {
                btnUpgrade.Enabled = (Class.Instance.SelectionClasses.Count > 0);
                upgradeCtrl.Inspect(btnUpgrade);
            };
        }

        //void Instance_SelectionChanged(object sender, EventArgs e)
        //{
        //    btnUpgrade.Enabled = ( Class.Instance.SelectionClasses .Count> 0 );
        //    upgradeCtrl.Inspect(btnUpgrade);
        //}

        private void btnUpgrade_Click(object sender, EventArgs e)
        {
            if (SmartSchool.Common.MsgBox.Show("您確定要對目前選取的班級進行升級嗎？", "班級升級確認", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            List<string> errors = new List<string>();
            MotherForm.Instance.SetWaitCursor();
            //整理升級後全校所有的班級名稱
            Dictionary<ClassInfo, string> className = new Dictionary<ClassInfo, string>();
            foreach (ClassInfo classInfo in Class.Instance.Items)
            {
                className.Add(classInfo, classInfo.ClassName);
            }
            foreach (ClassInfo classInfo in Class.Instance.SelectionClasses)
            {
                int gradeYear = 0;
                if (int.TryParse(classInfo.GradeYear, out gradeYear))
                {
                    if (Class.Instance.ValidateNamingRule(classInfo.NamingRule))
                    {
                        className[classInfo] = Class.Instance.ParseClassName(classInfo.NamingRule, gradeYear + 1);
                    }
                }
                else
                {
                    errors.Add(classInfo.ClassName + "沒有年級。");
                }
            }
            foreach (ClassInfo classInfo in Class.Instance.SelectionClasses)
            {
                foreach (ClassInfo classInfo2 in className.Keys)
                {
                    if (classInfo != classInfo2)
                    {
                        if (className[classInfo] == className[classInfo2])
                        {
                            errors.Add("\"" + classInfo.ClassName + "\"升級後的名稱：\"" + className[classInfo] + "\"已經存在，將會造成衝突。");
                        }
                    }
                }
            }
            if (errors.Count > 0)
            {
                new ErrorViewer("班級升級時發現錯誤", errors).Show();
                MotherForm.Instance.SetBarMessage("班級升級時發現錯誤，已取消作業，沒有資料被改變。");
            }
            else
            {
                Dictionary<string, string> updateLog = new Dictionary<string, string>();
                DSXmlHelper helper = new DSXmlHelper("UpdateRequest");
                List<ClassInfo> classList = Class.Instance.SelectionClasses;
                classList.Sort(sortByNameDependence);
                //classList.Reverse();
                foreach (ClassInfo classInfo in classList)
                {
                    string log = "修改" + (className[classInfo] != classInfo.ClassName ? ("班級名稱由「" + classInfo.ClassName + "」改為「" + className[classInfo] + "」，") : "") + "年級由「" + classInfo.GradeYear + "」改為「" + (int.Parse(classInfo.GradeYear) + 1) + "」";
                    updateLog.Add(classInfo.ClassID, log);
                    DSXmlHelper helper2 = new DSXmlHelper("Class");
                    helper2.AddElement("Field");
                    helper2.AddElement("Field", "ClassName", className[classInfo]);
                    helper2.AddElement("Field", "GradeYear", "" + (int.Parse(classInfo.GradeYear) + 1));
                    helper2.AddElement("Condition");
                    helper2.AddElement("Condition", "ID", classInfo.ClassID);
                    helper.AddElement(".", helper2.BaseElement);
                }
                EditClass.Update(new DSRequest(helper));
                Class.Instance.InvokClassUpdated(new List<string>(updateLog.Keys).ToArray());
                foreach (string id in updateLog.Keys)
                {
                    CurrentUser.Instance.AppLog.Write(SmartSchool.ApplicationLog.EntityType.Class, "班級升級", id, updateLog[id], "班級升級", updateLog[id]);
                }
            }
            MotherForm.Instance.ResetWaitCursor();
        }
        private int sortByNameDependence(ClassInfo c1, ClassInfo c2)
        {
            if (Class.Instance.ValidateNamingRule(c1.NamingRule) && Class.Instance.ParseClassName(c1.NamingRule, int.Parse(c1.GradeYear) + 1) == c2.ClassName)
                return 1;
            if (Class.Instance.ValidateNamingRule(c2.NamingRule) && Class.Instance.ParseClassName(c2.NamingRule, int.Parse(c2.GradeYear) + 1) == c1.ClassName)
                return -1;
            return 0;
        }
    }
}
