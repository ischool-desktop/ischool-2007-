using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data;
using SmartSchool.API.PlugIn;
using SmartSchool.AccessControl;
using System.Xml;

namespace SmartSchool.StudentRelated.RibbonBars.Import
{
    class ImportExtandField : API.PlugIn.Import.Importer
    {
        #region Importer 成員

        public override string Description
        {
            get { return ""; }
        }

        public override System.Drawing.Image Image
        {
            get { return null; }
        }

        public override void InitializeImport(SmartSchool.API.PlugIn.Import.ImportWizard wizard)
        {
            wizard.PackageLimit = 200;
            //依載入的資料篩選匯入欄位
            #region 依載入的資料篩選匯入欄位
            wizard.LoadSource += delegate(object sender, SmartSchool.API.PlugIn.Import.LoadSourceEventArgs e)
            {
                List<string> _StudentFields = new List<string>(new string[] { "學生系統編號", "學號", "班級", "座號", "科別", "姓名" });
                #region 設訂匯入欄位
                wizard.ImportableFields.Clear();
                foreach ( string field in e.Fields )
                {
                    if ( !_StudentFields.Contains(field) )
                        wizard.ImportableFields.Add(field);
                }
                #endregion
            };
            #endregion
            //驗證匯入資料
            #region 驗證匯入資料
            List<string> validatedID = new List<string>();
            wizard.ValidateStart += delegate { validatedID.Clear(); };
            wizard.ValidateRow += delegate(object sender, SmartSchool.API.PlugIn.Import.ValidateRowEventArgs e)
                {
                    if(validatedID.Contains(e.Data.ID))
                        e.ErrorMessage="此學生重複匯入。";
                    else
                        validatedID.Add(e.Data.ID);
                };
            #endregion
            //匯入資料
            #region 匯入資料
            AccessHelper helper = new AccessHelper();
            wizard.ImportPackage += delegate(object sender, SmartSchool.API.PlugIn.Import.ImportPackageEventArgs e)
            {
                foreach ( string field in e.ImportFields )
                {
                    Dictionary<StudentRecord, string> studentValues = new Dictionary<StudentRecord, string>();
                    foreach ( RowData row in e.Items )
                    {
                        StudentRecord studentRec = helper.StudentHelper.GetStudent(row.ID);
                        studentValues.Add(studentRec, row[field]);
                    }
                    helper.StudentHelper.SetExtensionFields("", field, studentValues);
                }
            };
            #endregion
        }

        public override string Path
        {
            get { return ""; }
        }

        public override string Text
        {
            get { return "匯入自訂欄位"; }
        }

        #endregion
    }
}
