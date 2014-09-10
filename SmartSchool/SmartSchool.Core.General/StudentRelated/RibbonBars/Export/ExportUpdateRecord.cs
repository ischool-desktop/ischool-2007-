using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.PlugIn.ImportExport;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.Data.StudentExtension;
using SmartSchool.AccessControl;

// Obsolete
namespace SmartSchool.StudentRelated.RibbonBars.Export
{
    [FeatureCode("Button0200")]
    class ExportUpdateRecord : ExportProcess
    {
        private AccessHelper _access_helper;

        public ExportUpdateRecord()
        {
            this.Title = "�ץX���ʬ���";
            this.Group = "���y�򥻸��";
            foreach (string var in new string[] { "���ʬ�O", "�~��", "���ʾǸ�", "���ʩm�W", "�����Ҹ�", "�ʧO", "�ͤ�", "���ʺ���", "���ʥN�X", "���ʤ��", "��]�Ψƶ�", "�s�Ǹ�", "��J�e�ǥ͸��-��O", "��J�e�ǥ͸��-�~��", "��J�e�ǥ͸��-�Ǯ�", "��J�e�ǥ͸��-(�Ƭd���)", "��J�e�ǥ͸��-(�Ƭd�帹)", "��J�e�ǥ͸��-�Ǹ�", "�J�Ǹ��-���~�ꤤ", "�J�Ǹ��-���~�ꤤ�Ҧb�a�N�X", "�̫Ყ�ʥN�X", "��(��)�~�ҮѦr��", "�Ƭd���", "�Ƭd�帹", "�ַǤ��", "�ַǤ帹", "�Ƶ�" })
            {
                this.ExportableFields.Add(var);
            }
            this.ExportPackage += new EventHandler<ExportPackageEventArgs>(ExportUpdateRecord_ExportPackage);
            _access_helper = new AccessHelper();
        }

        private void ExportUpdateRecord_ExportPackage(object sender, ExportPackageEventArgs e)
        {
            List<SmartSchool.Customization.Data.StudentRecord> students = _access_helper.StudentHelper.GetStudents(e.List);
            _access_helper.StudentHelper.FillUpdateRecord(students);

            foreach (SmartSchool.Customization.Data.StudentRecord stu in students)
            {
                foreach ( SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo var in stu.UpdateRecordList )
                {
                    RowData row = new RowData();
                    row.ID = stu.StudentID;
                    foreach (string field in e.ExportFields)
                    {
                        if (ExportableFields.Contains(field))
                        {
                            switch (field)
                            {
                                case "���ʬ�O": row.Add(field, var.Department); break;
                                case "�~��": row.Add(field, var.GradeYear); break;
                                case "���ʾǸ�": row.Add(field, var.StudentNumber); break;
                                case "���ʩm�W": row.Add(field, var.Name); break;
                                case "�����Ҹ�": row.Add(field, var.IDNumber); break;
                                case "�ʧO": row.Add(field, var.Gender); break;
                                case "�ͤ�": row.Add(field, var.BirthDate); break;
                                case "���ʺ���": row.Add(field, var.UpdateRecordType); break;
                                case "���ʥN�X": row.Add(field, var.UpdateCode); break;
                                case "���ʤ��": row.Add(field, var.UpdateDate); break;
                                case "��]�Ψƶ�": row.Add(field, var.UpdateDescription); break;
                                case "�s�Ǹ�": row.Add(field, var.NewStudentNumber); break;
                                case "��J�e�ǥ͸��-��O": row.Add(field, var.PreviousDepartment); break;
                                case "��J�e�ǥ͸��-�~��": row.Add(field, var.PreviousGradeYear); break;
                                case "��J�e�ǥ͸��-�Ǯ�": row.Add(field, var.PreviousSchool); break;
                                case "��J�e�ǥ͸��-(�Ƭd���)": row.Add(field, var.PreviousSchoolLastADDate); break;
                                case "��J�e�ǥ͸��-(�Ƭd�帹)": row.Add(field, var.PreviousSchoolLastADNumber); break;
                                case "��J�e�ǥ͸��-�Ǹ�": row.Add(field, var.PreviousStudentNumber); break;
                                case "�J�Ǹ��-���~�ꤤ": row.Add(field, var.GraduateSchool); break;
                                case "�J�Ǹ��-���~�ꤤ�Ҧb�a�N�X": row.Add(field, var.GraduateSchoolLocationCode); break;
                                case "�̫Ყ�ʥN�X": row.Add(field, var.LastUpdateCode); break;
                                case "��(��)�~�ҮѦr��": row.Add(field, var.GraduateCertificateNumber); break;
                                case "�Ƭd���": row.Add(field, var.LastADDate); break;
                                case "�Ƭd�帹": row.Add(field, var.LastADNumber); break;
                                case "�ַǤ��": row.Add(field, var.ADDate); break;
                                case "�ַǤ帹": row.Add(field, var.ADNumber); break;
                                case "�Ƶ�": row.Add(field, var.Comment); break;
                            }
                        }
                    }
                    e.Items.Add(row);
                }
            }
        }
    }
}
