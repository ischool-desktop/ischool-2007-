using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data;
using SmartSchool.AccessControl;
using SmartSchool.API.PlugIn;

namespace SmartSchool.StudentRelated.RibbonBars.Export
{
    [FeatureCode("Button0205")]
    class ExportCategory : API.PlugIn.Export.Exporter
    {
        #region Exporter 成員

        public override string Description
        {
            get { return ""; }
        }

        public override System.Drawing.Image Image
        {
            get { return null; }
        }

        public override void InitializeExport(SmartSchool.API.PlugIn.Export.ExportWizard wizard)
        {
            AccessHelper helper = new AccessHelper();
            //掃描可匯出欄位
            foreach ( StudentRecord studentRec in helper.StudentHelper.GetSelectedStudent() )
            {
                foreach ( CategoryInfo categoryInfo in studentRec.StudentCategorys )
                {
                    if ( !wizard.ExportableFields.Contains(categoryInfo.Name) )
                        wizard.ExportableFields.Add(categoryInfo.Name);
                }
            }
            wizard.ExportPackage += delegate(object sender, SmartSchool.API.PlugIn.Export.ExportPackageEventArgs e)
            {
                foreach ( StudentRecord studentRec in helper.StudentHelper.GetStudents(e.List) )
                {
                    SmartSchool.API.PlugIn.RowData studentRow = new SmartSchool.API.PlugIn.RowData();
                    studentRow.ID = studentRec.StudentID;
                    foreach ( CategoryInfo categoryInfo in studentRec.StudentCategorys )
                    {
                        if ( e.ExportFields.Contains(categoryInfo.Name) )
                        {
                            if ( categoryInfo.SubCategory == "" )//這個標簽只有一層，匯出的值跟標簽同名
                            {
                                if ( studentRow.ContainsKey(categoryInfo.Name) )
                                    studentRow[categoryInfo.Name] = categoryInfo.Name + "、" + studentRow[categoryInfo.Name];
                                else
                                    studentRow.Add(categoryInfo.Name, categoryInfo.Name);
                            }
                            else
                            {
                                if ( studentRow.ContainsKey(categoryInfo.Name) )
                                    studentRow[categoryInfo.Name] = studentRow[categoryInfo.Name] + "、" + categoryInfo.SubCategory;
                                else
                                    studentRow.Add(categoryInfo.Name, categoryInfo.SubCategory);
                            }
                        }
                    }
                    e.Items.Add(studentRow);
                }
            };
        }

        public override string Path
        {
            get { return ""; }
        }

        public override  string Text
        {
            get { return "匯出學生類別"; }
        }

        #endregion
    }

}
