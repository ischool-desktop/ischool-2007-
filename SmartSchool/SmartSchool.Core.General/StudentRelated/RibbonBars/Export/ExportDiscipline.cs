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
            this.Title = "匯出獎懲紀錄";
            this.Group = "缺曠獎懲";
            foreach (string var in new string[] { "學年度", "學期", "日期", "地點", "大功", "小功", "嘉獎", "大過", "小過", "警告", "事由", "是否銷過", "銷過日期", "銷過事由", "留校察看" })
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
                                case "學年度": row.Add(field, var.SchoolYear.ToString()); break;
                                case "學期": row.Add(field, var.Semester.ToString()); break;
                                case "日期": row.Add(field, var.OccurDate.ToShortDateString()); break;
                                case "地點": row.Add(field, var.OccurPlace); break;
                                case "大功": row.Add(field, var.AwardA.ToString()); break;
                                case "小功": row.Add(field, var.AwardB.ToString()); break;
                                case "嘉獎": row.Add(field, var.AwardC.ToString()); break;
                                case "大過": row.Add(field, var.FaultA.ToString()); break;
                                case "小過": row.Add(field, var.FaultB.ToString()); break;
                                case "警告": row.Add(field, var.FaultC.ToString()); break;
                                case "事由": row.Add(field, var.OccurReason); break;
                                case "是否銷過": row.Add(field, (var.Cleared ? "是" : "")); break;
                                case "銷過日期": row.Add(field, (var.Cleared ? var.ClearDate.ToShortDateString() : "")); break;
                                case "銷過事由": row.Add(field, (var.Cleared ? var.ClearReason : "")); break;
                                case "留校察看": row.Add(field, (var.UltimateAdmonition ? "是" : "")); break;
                            }
                        }
                    }
                    e.Items.Add(row);
                }
            }
        }
    }
}
