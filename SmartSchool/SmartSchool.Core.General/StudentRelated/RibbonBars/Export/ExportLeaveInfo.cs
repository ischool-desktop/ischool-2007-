using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data;

namespace SmartSchool.StudentRelated.RibbonBars.Export
{
    class ExportLeaveInfo : API.PlugIn.Export.Exporter
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

            //this.Fields.Add("LeaveClassName", student.LeaveClassName);
            //this.Fields.Add("LeaveDepartment", student.LeaveDepartment);
            //this.Fields.Add("LeaveReason", student.LeaveReason);
            //this.Fields.Add("LeaveSchoolYear", student.LeaveSchoolYear);

            wizard.ExportableFields.Add("離校學年度");
            wizard.ExportableFields.Add("離校類別");
            wizard.ExportableFields.Add("離校科別");
            wizard.ExportableFields.Add("離校班級");

            wizard.ExportPackage += delegate(object sender, SmartSchool.API.PlugIn.Export.ExportPackageEventArgs e)
            {
                foreach (StudentRecord studentRec in helper.StudentHelper.GetStudents(e.List))
                {
                    SmartSchool.API.PlugIn.RowData studentRow = new SmartSchool.API.PlugIn.RowData();
                    studentRow.ID = studentRec.StudentID;
                    studentRow.Add("離校學年度", studentRec.Fields["LeaveSchoolYear"] as string);
                    studentRow.Add("離校類別", studentRec.Fields["LeaveReason"] as string);
                    studentRow.Add("離校科別", studentRec.Fields["LeaveDepartment"] as string);
                    studentRow.Add("離校班級", studentRec.Fields["LeaveClassName"] as string);

                    //foreach (CategoryInfo categoryInfo in studentRec.StudentCategorys)
                    //{
                    //    if (e.ExportFields.Contains(categoryInfo.Name))
                    //    {
                    //        if (categoryInfo.SubCategory == "")//這個標簽只有一層，匯出的值跟標簽同名
                    //        {
                    //            if (studentRow.ContainsKey(categoryInfo.Name))
                    //                studentRow[categoryInfo.Name] = categoryInfo.Name + "、" + studentRow[categoryInfo.Name];
                    //            else
                    //                studentRow.Add(categoryInfo.Name, categoryInfo.Name);
                    //        }
                    //        else
                    //        {
                    //            if (studentRow.ContainsKey(categoryInfo.Name))
                    //                studentRow[categoryInfo.Name] = studentRow[categoryInfo.Name] + "、" + categoryInfo.SubCategory;
                    //            else
                    //                studentRow.Add(categoryInfo.Name, categoryInfo.SubCategory);
                    //        }
                    //    }
                    //}

                    e.Items.Add(studentRow);
                }
            };
        }

        public override string Path
        {
            get { return ""; }
        }

        public override string Text
        {
            get { return "匯出離校資訊"; }
        }

        #endregion
    }
}
