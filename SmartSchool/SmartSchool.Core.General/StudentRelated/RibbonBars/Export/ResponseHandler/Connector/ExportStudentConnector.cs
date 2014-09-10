using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler.Formater;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler.Generator;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler;
using SmartSchool.StudentRelated.RibbonBars.Export.Util;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler.Generator.Condition;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler.Generator.Orders;
using SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.Formater;

namespace SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.Connector
{
    public class ExportStudentConnector : IExportConnector
    {
        //private DSConnection _connection;
        private FieldCollection _selectFields;
        private List<string> _conditions;

        public ExportStudentConnector()
        {
            _conditions = new List<string>();
        }

        #region IExportConnector ����

        public void SetSelectedFields(FieldCollection fields)
        {
            _selectFields = fields;
        }

        public void AddCondition(string studentid)
        {
            _conditions.Add(studentid);
        }



        public ExportTable Export()
        {
            // ���o������Ӫ�
            XmlElement schoolLocationList = SmartSchool.Feature.Basic.Config.GetSchoolLocationList().GetContent().BaseElement;

            // ���o�ץX�W�h�y�z
            XmlElement descElement = SmartSchool.Feature.Student.StudentBulkProcess.GetExportDescription();
            IFieldFormater fieldFormater = new BaseFieldFormater();
            IResponseFormater responseFormater = new ResponseFormater();

            FieldCollection fieldCollection = fieldFormater.Format(descElement);
            ExportFieldCollection exportFields = responseFormater.Format(descElement);

            fieldCollection = FieldUtil.Match(fieldCollection, _selectFields);
            exportFields = FieldUtil.Match(exportFields, _selectFields);

            IRequestGenerator reqGenerator = new ExportStudentRequestGenerator();
            reqGenerator.SetSelectedFields(_selectFields);

            // �w�]��-1, ���M�|�Ǧ^�Ҧ��ǥ�
            ICondition condition = new BaseCondition("ID", "-1");
            reqGenerator.AddCondition(condition);
            foreach (string id in _conditions)
            {
                ICondition condition2 = new BaseCondition("ID", id);
                reqGenerator.AddCondition(condition2);
            }

            reqGenerator.AddOrder(new Order("GradeYear"));
            reqGenerator.AddOrder(new Order("Department"));
            reqGenerator.AddOrder(new Order("RefClassID"));
            reqGenerator.AddOrder(new Order("SeatNo"));

            DSRequest request = reqGenerator.Generate();
            DSResponse response = SmartSchool.Feature.QueryStudent.GetExportList(request);

            ExportTable table = new ExportTable();
            foreach (ExportField field in exportFields)
                table.AddColumn(field);

            foreach (XmlElement record in response.GetContent().GetElements("Student"))
            {
                ExportRow row = table.AddRow();
                foreach (ExportField column in table.Columns)
                {
                    int columnIndex = column.ColumnIndex;
                    ExportCell cell = row.Cells[columnIndex];
                    XmlNode cellNode = record.SelectSingleNode(column.XPath);
                    // CustodianOtherInfo/CustodianOtherInfo[1]/EducationDegree[1]

                    #region �o�q�{���O�B�z�פJ/�ץX�{�����@�P���D
                    if (column.XPath.StartsWith("CustodianOtherInfo/Custodian"))
                    {
                        if (cellNode == null)
                        {
                            string x = column.XPath.Replace("CustodianOtherInfo/Custodian", "CustodianOtherInfo/CustodianOtherInfo");
                            cellNode = record.SelectSingleNode(x);
                            if (cellNode == null)
                            {
                                x = column.XPath.Replace("CustodianOtherInfo/CustodianOtherInfo", "CustodianOtherInfo/Custodian");
                                cellNode = record.SelectSingleNode(x);
                            }
                        }
                    }
                    if (column.XPath.StartsWith("FatherOtherInfo/Father"))
                    {
                        if (cellNode == null)
                        {
                            string x = column.XPath.Replace("FatherOtherInfo/Father", "FatherOtherInfo/FatherOtherInfo");
                            cellNode = record.SelectSingleNode(x);
                            if (cellNode == null)
                            {
                                x = column.XPath.Replace("FatherOtherInfo/FatherOtherInfo", "FatherOtherInfo/Father");
                                cellNode = record.SelectSingleNode(x);
                            }
                        }
                    }
                    if (column.XPath.StartsWith("MotherOtherInfo/Mother"))
                    {
                        if (cellNode == null)
                        {
                            string x = column.XPath.Replace("MotherOtherInfo/Mother", "MotherOtherInfo/MotherOtherInfo");
                            cellNode = record.SelectSingleNode(x);
                            if (cellNode == null)
                            {
                                x = column.XPath.Replace("MotherOtherInfo/MotherOtherInfo", "MotherOtherInfo/Mother");
                                cellNode = record.SelectSingleNode(x);
                            }
                        }
                    }
                    #endregion

                    if (cellNode != null)
                    {
                        if (column.FieldName == "GraduateSchoolLocationCode")
                            cell.Value = GetCounty(schoolLocationList, cellNode.InnerText);
                        else if (column.FieldName == "DeptName") //�B�z��O�~�Ӱ��D�C
                        {
                            //�o����쪺��Ƥ@�w�|�Q�^�ǡA�]���]�w�F Mandatory �ݩʡC
                            XmlNode selfDept = record.SelectSingleNode("SelfDeptName");
                            if (string.IsNullOrEmpty(selfDept.InnerText))
                                cell.Value = cellNode.InnerText;
                            else
                                cell.Value = selfDept.InnerText;
                        }
                        else
                            cell.Value = cellNode.InnerText;
                    }
                }
            }
            return table;
        }

        private string GetCounty(XmlElement list, string code)
        {
            foreach (XmlNode node in list.SelectNodes("Location"))
            {
                XmlElement element = (XmlElement)node;
                if (element.GetAttribute("Code") == code)
                    return element.InnerText;
            }
            return string.Empty;
        }

        #endregion
    }
}
