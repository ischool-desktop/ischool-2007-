using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data;
using SmartSchool.API.PlugIn;

namespace SmartSchool.StudentRelated.RibbonBars.Export
{
    class ExportExtandField : API.PlugIn.Export.Exporter
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
            List<StudentRecord> selectionStudents = helper.StudentHelper.GetSelectedStudent();
            //helper.StudentHelper.FillExtensionFields(selectionStudents,"");
            SmartSchool.API.Provider.StudentProvider provider=new SmartSchool.API.Provider.StudentProvider();
            provider.AccessHelper = helper;
            provider.CachePool = helper.CachePool;

            Dictionary<SmartSchool.Customization.Data.StudentRecord, Dictionary<string, string>> items=provider.GetExtensionFields(selectionStudents, "",new string[0]);
            //掃描可匯出欄位
            foreach ( Dictionary<string, string> fieldvaluess in items.Values )
            {
                foreach ( string field in fieldvaluess.Keys )
                {
                    if ( !wizard.ExportableFields.Contains(field) )
                        wizard.ExportableFields.Add(field);
                }
            }
            wizard.ExportPackage += delegate(object sender, SmartSchool.API.PlugIn.Export.ExportPackageEventArgs e)
            {
                foreach ( StudentRecord studentRec in helper.StudentHelper.GetStudents(e.List) )
                {
                    SmartSchool.API.PlugIn.RowData studentRow = new SmartSchool.API.PlugIn.RowData();
                    studentRow.ID = studentRec.StudentID;
                    if ( items.ContainsKey(studentRec) )
                    {
                        foreach ( string field in items[studentRec].Keys )
                        {
                            if ( e.ExportFields.Contains(field) )
                                if ( studentRow.ContainsKey(field) )
                                    studentRow[field] = items[studentRec][field];
                                else
                                    studentRow.Add(field, items[studentRec][field]);
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

        public override string Text
        {
            get { return "匯出自訂欄位"; }
        }

        #endregion
    }

}
