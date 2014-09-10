using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//using SmartPlugIn.StudentRecord.NameList;
using SmartSchool.Common;
//using SmartSchool.SmartPlugIn.Common;
using DevComponents.DotNetBar;
using SmartSchool.Security;

namespace SmartSchool.Others.RibbonBars
{
    public partial class NameList : RibbonBarBase
    {
        FeatureAccessControl fileFreshmanCtrl, fileGraduateCtrl;

        ButtonItemPlugInManager reportManager;

        public NameList()
        {
            InitializeComponent();

            #region 設定為 "教務作業/學籍作業" 的外掛處理者
            reportManager = new ButtonItemPlugInManager(itemContainer2);
            reportManager.ItemsChanged += new EventHandler(reportManager_ItemsChanged);
            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonItem>.Instance.Add("教務作業/學籍作業", reportManager);
            #endregion

            //權限判斷 - 學籍作業/教育程度資料檔 (新生)
            fileFreshmanCtrl = new FeatureAccessControl("Button0640");

            //權限判斷 - 學籍作業/教育程度資料檔 (畢業)
            fileGraduateCtrl = new FeatureAccessControl("Button0650");

            fileFreshmanCtrl.Inspect(buttonItem77);
            fileGraduateCtrl.Inspect(buttonItem79);
        }

        void reportManager_ItemsChanged(object sender, EventArgs e)
        {
            
        }

        public override string ProcessTabName
        {
            get
            {
                return "教務作業";
            }
        }

        // 異動名冊，目前已經搬到 GeneralSHUpdateRecord 裡面
        private void buttonItem78_Click(object sender, EventArgs e)
        {
            //if (SmartSchool.ReportBuilder.ReportBuilderManager.Items["新生名冊"].Count == 0)
            //    SmartSchool.ReportBuilder.ReportBuilderManager.Items["新生名冊"].Add(new SmartReport.EnrollmentList());
            //if (SmartSchool.ReportBuilder.ReportBuilderManager.Items["延修生學籍異動名冊"].Count == 0)
            //    SmartSchool.ReportBuilder.ReportBuilderManager.Items["延修生學籍異動名冊"].Add(new SmartReport.ExtendingStudentUpdateRecordList());
            //if (SmartSchool.ReportBuilder.ReportBuilderManager.Items["學籍異動名冊"].Count == 0)
            //    SmartSchool.ReportBuilder.ReportBuilderManager.Items["學籍異動名冊"].Add(new SmartReport.StudentUpdateRecordList());
            //if (SmartSchool.ReportBuilder.ReportBuilderManager.Items["畢業名冊"].Count == 0)
            //    SmartSchool.ReportBuilder.ReportBuilderManager.Items["畢業名冊"].Add(new SmartReport.GraduatingStudentList());
            //if (SmartSchool.ReportBuilder.ReportBuilderManager.Items["延修生畢業名冊"].Count == 0)
            //    SmartSchool.ReportBuilder.ReportBuilderManager.Items["延修生畢業名冊"].Add(new SmartReport.ExtendingStudentGraduateList());
            //if (SmartSchool.ReportBuilder.ReportBuilderManager.Items["延修生名冊"].Count == 0)
            //    SmartSchool.ReportBuilder.ReportBuilderManager.Items["延修生名冊"].Add(new SmartReport.ExtendingStudentList());
            //if (SmartSchool.ReportBuilder.ReportBuilderManager.Items["轉入學生名冊"].Count == 0)
            //    SmartSchool.ReportBuilder.ReportBuilderManager.Items["轉入學生名冊"].Add(new SmartReport.TransferringStudentUpdateRecordList());

            ////new PrintFreshmanListWizard().ShowDialog();
            //new ListForm().ShowDialog();
        }
    }
}
