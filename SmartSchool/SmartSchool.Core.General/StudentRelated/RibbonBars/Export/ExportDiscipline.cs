using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.PlugIn.ImportExport;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.Data.StudentExtension;
using SmartSchool.AccessControl;

namespace SmartSchool.StudentRelated.RibbonBars.Export
{
    [FeatureCode("Button0190")]
    class ExportDiscipline : ExportProcess
    {
        private AccessHelper _access_helper;

        public ExportDiscipline()
        {
            this.Title = "�ץX���g����";
            this.Group = "���m���g";
            foreach (string var in new string[] { "�Ǧ~��", "�Ǵ�", "���", "�a�I", "�j�\", "�p�\", "�ż�", "�j�L", "�p�L", "ĵ�i", "�ƥ�", "�O�_�P�L", "�P�L���", "�P�L�ƥ�", "�d�չ��" })
            {
                this.ExportableFields.Add(var);
            }
            this.ExportPackage += new EventHandler<ExportPackageEventArgs>(ExportDiscipline_ExportPackage);
            _access_helper = new AccessHelper();
        }

        private void ExportDiscipline_ExportPackage(object sender, ExportPackageEventArgs e)
        {
            List<SmartSchool.Customization.Data.StudentRecord> students = _access_helper.StudentHelper.GetStudents(e.List);
            _access_helper.StudentHelper.FillReward(students);

            foreach (SmartSchool.Customization.Data.StudentRecord stu in students)
            {
                foreach (RewardInfo var in stu.RewardList)
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
                                case "�a�I": row.Add(field, var.OccurPlace); break;
                                case "�j�\": row.Add(field, var.AwardA.ToString()); break;
                                case "�p�\": row.Add(field, var.AwardB.ToString()); break;
                                case "�ż�": row.Add(field, var.AwardC.ToString()); break;
                                case "�j�L": row.Add(field, var.FaultA.ToString()); break;
                                case "�p�L": row.Add(field, var.FaultB.ToString()); break;
                                case "ĵ�i": row.Add(field, var.FaultC.ToString()); break;
                                case "�ƥ�": row.Add(field, var.OccurReason); break;
                                case "�O�_�P�L": row.Add(field, (var.Cleared ? "�O" : "")); break;
                                case "�P�L���": row.Add(field, (var.Cleared ? var.ClearDate.ToShortDateString() : "")); break;
                                case "�P�L�ƥ�": row.Add(field, (var.Cleared ? var.ClearReason : "")); break;
                                case "�d�չ��": row.Add(field, (var.UltimateAdmonition ? "�O" : "")); break;
                            }
                        }
                    }
                    e.Items.Add(row);
                }
            }
        }
    }
}
