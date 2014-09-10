using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.PlugIn.ImportExport;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.Data.StudentExtension;
using SmartSchool.AccessControl;

namespace SmartSchool.StudentRelated.RibbonBars.Export
{
    [FeatureCode("Button0180")]
    class ExportAbsence : ExportProcess
    {
        private AccessHelper _access_helper;

        public ExportAbsence()
        {
            this.Title = "�ץX���m����";
            this.Group = "���m���g";
            foreach (string var in new string[] { "�Ǧ~��", "�Ǵ�", "���", "���m���O", "���m�`��", "�`�����O" })
            {
                this.ExportableFields.Add(var);
            }
            this.ExportPackage += new EventHandler<ExportPackageEventArgs>(ExportAbsence_ExportPackage);
            _access_helper = new AccessHelper();
        }

        private void ExportAbsence_ExportPackage(object sender, ExportPackageEventArgs e)
        {
            List<SmartSchool.Customization.Data.StudentRecord> students = _access_helper.StudentHelper.GetStudents(e.List);
            _access_helper.StudentHelper.FillAttendance(students);

            foreach (SmartSchool.Customization.Data.StudentRecord stu in students)
            {
                foreach (AttendanceInfo var in stu.AttendanceList)
                {
                    RowData row = new RowData();
                    row.ID = stu.StudentID;
                    foreach (string field in e.ExportFields)
                    {
                        if (ExportableFields.Contains(field))
                        {
                            switch (field)
                            {
                                case "�Ǧ~��": row.Add(field, var.SchoolYear.ToString()); break;
                                case "�Ǵ�": row.Add(field, var.Semester.ToString()); break;
                                case "���": row.Add(field, var.OccurDate.ToShortDateString()); break;
                                case "���m���O": row.Add(field, var.Absence); break;
                                case "���m�`��": row.Add(field, var.Period); break;
                                case "�`�����O": row.Add(field, var.PeriodType); break;
                            }
                        }
                    }
                    e.Items.Add(row);
                }
            }
        }
    }
}
