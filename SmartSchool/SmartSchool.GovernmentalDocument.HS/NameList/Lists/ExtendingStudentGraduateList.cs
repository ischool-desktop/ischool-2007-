using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;
using System.Xml;
using System.IO;

namespace SmartSchool.GovernmentalDocument.NameList
{
    public class ExtendingStudentGraduateList : ReportBuilder
    {
        protected override void Build(XmlElement source, string location)
        {
            Workbook template = new Workbook();

            //�qResources��TemplateŪ�X��
            template.Open(new MemoryStream(Properties.Resources.GraduatingStudentListTemplate), FileFormatType.Excel2003);

            //�n���ͪ�excel��
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(Properties.Resources.GraduatingStudentListTemplate), FileFormatType.Excel2003);

            Worksheet ws = wb.Worksheets[0];

            //�������j�X��row
            int next = 24;

            //����
            int index = 0;

            //�d���d��
            Range tempRange = template.Worksheets[0].Cells.CreateRange(0, 24, false);

            //�`�@�X�����ʬ���
            int count = 0;
            int totalRec = source.SelectNodes("�M��/���ʬ���").Count;

            foreach (XmlNode list in source.SelectNodes("�M��"))
            {
                //���ͲM��Ĥ@��
                //for (int row = 0; row < next; row++)
                //{
                //    ws.Cells.CopyRow(template.Worksheets[0].Cells, row, row + index);
                //}
                ws.Cells.CreateRange(index, 24, false).Copy(tempRange);

                //Page
                int currentPage = 1;
                int totalPage = (list.ChildNodes.Count / 18) + 1;

                //�g�J�W�U���O
                if (source.SelectSingleNode("@���O").InnerText == "���ץͲ��~�W�U")
                    ws.Cells[index, 0].PutValue(ws.Cells[index, 0].StringValue.Replace("�����~", "�����~"));
                else
                    ws.Cells[index, 0].PutValue(ws.Cells[index, 0].StringValue.Replace("�����~", "�����~"));

                //�g�J�N��
                ws.Cells[index, 6].PutValue("�N�X�G" + source.SelectSingleNode("@�ǮեN��").InnerText + "-" + list.SelectSingleNode("@��O�N��").InnerText);

                //�g�J�զW�B�Ǧ~�סB�Ǵ��B��O
                ws.Cells[index + 2, 0].PutValue("�զW�G" + source.SelectSingleNode("@�ǮզW��").InnerText);
                ws.Cells[index + 2, 4].PutValue(source.SelectSingleNode("@�Ǧ~��").InnerText + "�Ǧ~�� ��" + source.SelectSingleNode("@�Ǵ�").InnerText + "�Ǵ�");
                ws.Cells[index + 2, 6].PutValue(list.SelectSingleNode("@��O").InnerText);

                //�g�J���
                int recCount = 0;
                int dataIndex = index + 5;
                for (; currentPage <= totalPage; currentPage++)
                {
                    //�ƻs����
                    if (currentPage + 1 <= totalPage)
                    {
                        //for (int row = 0; row < next; row++)
                        //{
                        //    ws.Cells.CopyRow(ws.Cells, row + index, row + index + next);
                        //}
                        ws.Cells.CreateRange(index + next, 24, false).Copy(tempRange);
                    }

                    //��J���
                    for (int i = 0; i < 18 && recCount < list.ChildNodes.Count; i++, recCount++)
                    {
                        //MsgBox.Show(i.ToString()+" "+recCount.ToString());
                        XmlNode rec = list.SelectNodes("���ʬ���")[recCount];
                        ws.Cells[dataIndex, 0].PutValue(rec.SelectSingleNode("@�Ǹ�").InnerText + "\n" + rec.SelectSingleNode("@�m�W").InnerText);
                        ws.Cells[dataIndex, 1].PutValue(rec.SelectSingleNode("@�ʧO�N��").InnerText.ToString());
                        ws.Cells[dataIndex, 2].PutValue(rec.SelectSingleNode("@�ʧO").InnerText);
                        ws.Cells[dataIndex, 3].PutValue(rec.SelectSingleNode("@�ͤ�").InnerText + "\n" + rec.SelectSingleNode("@�����Ҹ�").InnerText);
                        ws.Cells[dataIndex, 4].PutValue(rec.SelectSingleNode("@�̫Ყ�ʥN��").InnerText.ToString());
                        ws.Cells[dataIndex, 5].PutValue(rec.SelectSingleNode("@�Ƭd���").InnerText + "\n" + rec.SelectSingleNode("@�Ƭd�帹").InnerText);
                        ws.Cells[dataIndex, 6].PutValue(rec.SelectSingleNode("@���~�ҮѦr��").InnerText);
                        ws.Cells[dataIndex, 7].PutValue(rec.SelectSingleNode("@�Ƶ�").InnerText);
                        dataIndex++;
                        count++;
                    }

                    //�p��X�p
                    if (currentPage == totalPage)
                    {
                        ws.Cells[index + 22, 0].PutValue("�X�p");
                        ws.Cells[index + 22, 1].PutValue(list.ChildNodes.Count.ToString());
                    }

                    //����
                    ws.Cells[index + 23, 6].PutValue("�� " + currentPage + " ���A�@ " + totalPage + " ��");
                    ws.HPageBreaks.Add(index + 24, 8);

                    //���ޫ��V�U�@��
                    index += next;
                    dataIndex = index + 5;

                    //�^���i��
                    ReportProgress((int)(((double)count * 100.0) / ((double)totalRec)));
                }
            }

            //�x�s
            wb.Save(location, FileFormatType.Excel2003);
        }

        public override string Copyright
        {
            get { return "IntelliSchool"; }
        }

        public override string Description
        {
            get { return "�����줽��95�~11��s�L�޲z��U�W�d�榡"; }
        }

        public override string ReportName
        {
            get { return "���ץͲ��~�W�U"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
