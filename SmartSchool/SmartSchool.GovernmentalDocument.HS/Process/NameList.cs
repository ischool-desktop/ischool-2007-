using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.GovernmentalDocument.NameList;

namespace SmartSchool.GovernmentalDocument.Process
{
    public partial class NameList : UserControl
    {
        FeatureAccessControl nameListCtrl;

        public NameList()
        {
            InitializeComponent();
            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonItem>.Instance["教務作業/學籍作業"].Add(btnItemNameList);

            //權限判斷 - 學籍作業/函報名冊
            nameListCtrl = new FeatureAccessControl("Button0630");
            nameListCtrl.Inspect(btnItemNameList);
        }

        private void btnItemNameList_Click(object sender, EventArgs e)
        {
            if (ReportBuilderManager.Items["新生名冊"].Count == 0)
                ReportBuilderManager.Items["新生名冊"].Add(new EnrollmentList());
            if (ReportBuilderManager.Items["延修生學籍異動名冊"].Count == 0)
                ReportBuilderManager.Items["延修生學籍異動名冊"].Add(new ExtendingStudentUpdateRecordList());
            if (ReportBuilderManager.Items["學籍異動名冊"].Count == 0)
                ReportBuilderManager.Items["學籍異動名冊"].Add(new StudentUpdateRecordList());
            if (ReportBuilderManager.Items["畢業名冊"].Count == 0)
                ReportBuilderManager.Items["畢業名冊"].Add(new GraduatingStudentList());
            if (ReportBuilderManager.Items["延修生畢業名冊"].Count == 0)
                ReportBuilderManager.Items["延修生畢業名冊"].Add(new ExtendingStudentGraduateList());
            if (ReportBuilderManager.Items["延修生名冊"].Count == 0)
                ReportBuilderManager.Items["延修生名冊"].Add(new ExtendingStudentList());
            if (ReportBuilderManager.Items["轉入學生名冊"].Count == 0)
                ReportBuilderManager.Items["轉入學生名冊"].Add(new TransferringStudentUpdateRecordList());

            new ListForm().ShowDialog();
        }
    }
}
