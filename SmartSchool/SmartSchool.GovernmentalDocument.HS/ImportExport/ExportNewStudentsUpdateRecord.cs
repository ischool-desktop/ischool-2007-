using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.PlugIn.ImportExport;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.Data.StudentExtension;
using SmartSchool.AccessControl;

namespace SmartSchool.GovernmentalDocument.ImportExport
{
    [FeatureCode("Button0200")]
    class ExportNewStudentsUpdateRecord : AbstractExportUpdateRecord
    {

        protected override string[] Fields
        {
            get { return new string[] { "異動科別", "異動學號", "異動姓名", "身份證號", "性別", "生日", "異動代碼", "異動日期", "原因及事項", "入學資格-畢業國中", "入學資格-畢業國中所在地代碼", "核準日期", "核準文號", "備註" }; }
        }

        protected override string Type
        {
            get { return "新生異動"; }
        }
    }
    //class ExportNewStudentsUpdateRecord : ExportProcess
    //{
    //    private AccessHelper _access_helper;

    //    public ExportNewStudentsUpdateRecord()
    //    {
    //        //"異動科別", "年級", "異動學號", "異動姓名", "身份證號", "性別", "生日", "異動種類", "異動代碼", "異動日期", "原因及事項", "新學號", "轉入前學生資料-科別", "轉入前學生資料-年級", "轉入前學生資料-學校", "轉入前學生資料-(備查日期)", "轉入前學生資料-(備查文號)", "轉入前學生資料-學號", "入學資格-畢業國中", "入學資格-畢業國中所在地代碼", "最後異動代碼", "畢(結)業證書字號", "備查日期", "備查文號", "核準日期", "核準文號", "備註"
    //        this.Title = "匯出新生異動";
    //        this.Group = "學籍基本資料";
    //        foreach ( string var in new string[] { "異動科別", "年級", "異動學號", "異動姓名", "身份證號", "性別", "生日", "異動代碼", "異動日期", "原因及事項", "入學資格-畢業國中", "入學資格-畢業國中所在地代碼", "備註" } )
    //        {
    //            this.ExportableFields.Add(var);
    //        }
    //        this.ExportPackage += new EventHandler<ExportPackageEventArgs>(ExportUpdateRecord_ExportPackage);
    //        _access_helper = new AccessHelper();
    //    }

    //    private void ExportUpdateRecord_ExportPackage(object sender, ExportPackageEventArgs e)
    //    {
    //        List<SmartSchool.Customization.Data.StudentRecord> students = _access_helper.StudentHelper.GetStudents(e.List);
    //        _access_helper.StudentHelper.FillUpdateRecord(students);

    //        foreach ( SmartSchool.Customization.Data.StudentRecord stu in students )
    //        {
    //            foreach ( SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo var in stu.UpdateRecordList )
    //            {
    //                if ( var.UpdateRecordType != "新生異動" ) continue;
    //                RowData row = new RowData();
    //                row.ID = stu.StudentID;
    //                foreach ( string field in e.ExportFields )
    //                {
    //                    if ( ExportableFields.Contains(field) )
    //                    {
    //                        switch ( field )
    //                        {
    //                            case "異動科別": row.Add(field, var.Department); break;
    //                            case "年級": row.Add(field, var.GradeYear); break;
    //                            case "異動學號": row.Add(field, var.StudentNumber); break;
    //                            case "異動姓名": row.Add(field, var.Name); break;
    //                            case "身份證號": row.Add(field, var.IDNumber); break;
    //                            case "性別": row.Add(field, var.Gender); break;
    //                            case "生日": row.Add(field, var.BirthDate); break;
    //                            case "異動種類": row.Add(field, var.UpdateRecordType); break;
    //                            case "異動代碼": row.Add(field, var.UpdateCode); break;
    //                            case "異動日期": row.Add(field, var.UpdateDate); break;
    //                            case "原因及事項": row.Add(field, var.UpdateDescription); break;
    //                            case "新學號": row.Add(field, var.NewStudentNumber); break;
    //                            case "轉入前學生資料-科別": row.Add(field, var.PreviousDepartment); break;
    //                            case "轉入前學生資料-年級": row.Add(field, var.PreviousGradeYear); break;
    //                            case "轉入前學生資料-學校": row.Add(field, var.PreviousSchool); break;
    //                            case "轉入前學生資料-(備查日期)": row.Add(field, var.PreviousSchoolLastADDate); break;
    //                            case "轉入前學生資料-(備查文號)": row.Add(field, var.PreviousSchoolLastADNumber); break;
    //                            case "轉入前學生資料-學號": row.Add(field, var.PreviousStudentNumber); break;
    //                            case "入學資格-畢業國中": row.Add(field, var.GraduateSchool); break;
    //                            case "入學資格-畢業國中所在地代碼": row.Add(field, var.GraduateSchoolLocationCode); break;
    //                            case "最後異動代碼": row.Add(field, var.LastUpdateCode); break;
    //                            case "畢(結)業證書字號": row.Add(field, var.GraduateCertificateNumber); break;
    //                            case "備查日期": row.Add(field, var.LastADDate); break;
    //                            case "備查文號": row.Add(field, var.LastADNumber); break;
    //                            case "核準日期": row.Add(field, var.ADDate); break;
    //                            case "核準文號": row.Add(field, var.ADNumber); break;
    //                            case "備註": row.Add(field, var.Comment); break;
    //                        }
    //                    }
    //                }
    //                e.Items.Add(row);
    //            }
    //        }
    //    }
    //}
}
