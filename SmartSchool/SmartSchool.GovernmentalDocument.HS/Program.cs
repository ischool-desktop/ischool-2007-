using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.PlugIn;
using DevComponents.DotNetBar;
using SmartSchool.Customization.PlugIn.ExtendedContent;
using SmartSchool.Customization.Data.StudentExtension;
using System.Xml;

namespace SmartSchool.GovernmentalDocument
{
    public class Program
    {
        [MainMethod()]
        public static void Main()
        {
            if ( System.IO.File.Exists(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "���_�U���U���U�U��")) ) return;
            //�����
            SmartSchool.Customization.PlugIn.ExtendedContent.ExtendStudentContent.AddItem(new Content.UpdatePalmerwormItem());

            //UserControl1 updateRecord = new UserControl1();
            //BaseItem item = c.ProcessRibbon.Items[0];

            //SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<BaseItem>.Instance[@"�ǥ�\���w"].Add(item);
            //SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<BaseItem>.Instance.Add(@"�ǥ�\���y�@�~", updateRecord);
            
            //�W�URibbon
            new Process.NameList();
            new Process.BatchUpdateRecord();

            //�ץX���ʬ���
            SmartSchool.Customization.PlugIn.ImportExport.ExportStudent.AddProcess(new ImportExport.ExportNewStudentsUpdateRecord());
            SmartSchool.Customization.PlugIn.ImportExport.ExportStudent.AddProcess(new ImportExport.ExportTransferSchoolStudentsUpdateRecord());
            SmartSchool.Customization.PlugIn.ImportExport.ExportStudent.AddProcess(new ImportExport.ExprotStudentsUpdateRecord());
            SmartSchool.Customization.PlugIn.ImportExport.ExportStudent.AddProcess(new ImportExport.ExportStudentGraduateUpdateRecord());
            //�פJ���ʬ���
            SmartSchool.Customization.PlugIn.ImportExport.ImportStudent.AddProcess(new ImportExport.ImportNewStudentsUpdateRecord());
            SmartSchool.Customization.PlugIn.ImportExport.ImportStudent.AddProcess(new ImportExport.ImportTransferSchoolStudentsUpdateRecord());
            //SmartSchool.Customization.PlugIn.ImportExport.ImportStudent.AddProcess(new ImportExport.ImportStudentsUpdateRecord());
            SmartSchool.Customization.PlugIn.ImportExport.ImportStudent.AddProcess(new ImportExport.ImportStudentGraduateUpdateRecord());

            SmartSchool.Customization.Data.StudentHelper.FillingUpdateRecord += new EventHandler<SmartSchool.Customization.Data.FillEventArgs<SmartSchool.Customization.Data.StudentRecord>>(StudentHelper_FillingUpdateRecord);
        }

        private const int _PackageLimit = 500;

        private static List<T>[] SplitPackage<T>(List<T> list)
        {
            if (list.Count > 0)
            {
                int packageCount = (list.Count / _PackageLimit + 1);
                int packageSize = list.Count / packageCount + list.Count % packageCount;
                packageCount = 0;
                List<List<T>> packages = new List<List<T>>();
                List<T> packageCurrent = new List<T>();
                foreach (T var in list)
                {
                    packageCurrent.Add(var);
                    packageCount++;
                    if (packageCount == packageSize)
                    {
                        packageCount = 0;
                        packages.Add(packageCurrent);
                    }
                }
                return packages.ToArray();
            }
            else
                return new List<T>[0];
        }

        private static List<T> GetList<T>(IEnumerable<T> items)
        {
            List<T> list = new List<T>();
            list.AddRange(items);
            return list;
        }

        static void StudentHelper_FillingUpdateRecord(object sender, SmartSchool.Customization.Data.FillEventArgs<SmartSchool.Customization.Data.StudentRecord> e)
        {
            //���o�N�X��Ӫ�
            XmlElement updateCodeMappingElement = SmartSchool.Feature.Basic.Config.GetUpdateCodeSynopsis().GetContent().BaseElement;
            //���妸�B�z
            foreach (List<SmartSchool.Customization.Data.StudentRecord> studentList in SplitPackage<SmartSchool.Customization.Data.StudentRecord>(GetList<SmartSchool.Customization.Data.StudentRecord>(e.List)))
            {
                Dictionary<string, List<SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo>> studentUpdateRecords = new Dictionary<string, List<SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo>>();
                //���o�s��
                #region ���o�s��
                string[] idList = new string[studentList.Count];
                for (int i = 0; i < idList.Length; i++)
                {
                    idList[i] = studentList[i].StudentID;
                }
                if (idList.Length == 0)
                    continue;
                #endregion
                //�즨�Z���
                #region �즨�Z���
                foreach (XmlElement element in SmartSchool.Feature.QueryStudent.GetUpdateRecordByStudentIDList(idList).GetContent().GetElements("UpdateRecord"))
                {
                    string RefStudentID = element.GetAttribute("RefStudentID");
                    if (!studentUpdateRecords.ContainsKey(RefStudentID))
                        studentUpdateRecords.Add(RefStudentID, new List<SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo>());
                    studentUpdateRecords[RefStudentID].Add(new UpdateRecord(updateCodeMappingElement, element));
                }
                #endregion
                //��J�ǥͪ����ʸ�ƲM��
                #region ��J�ǥͪ����ʸ�ƲM��
                foreach (SmartSchool.Customization.Data.StudentRecord student in studentList)
                {
                    student.UpdateRecordList.Clear();
                    if (studentUpdateRecords.ContainsKey(student.StudentID))
                    {
                        foreach (SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo updateRecord in studentUpdateRecords[student.StudentID])
                        {
                            student.UpdateRecordList.Add(updateRecord);
                        }
                    }
                }
                #endregion
            }
        }
    }
}
