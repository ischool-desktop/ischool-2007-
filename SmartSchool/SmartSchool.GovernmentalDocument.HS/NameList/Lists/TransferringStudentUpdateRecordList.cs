using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;
using System.Xml;
using System.IO;

namespace SmartSchool.GovernmentalDocument.NameList
{
    public class TransferringStudentUpdateRecordList : ReportBuilder
    {
        protected override void Build(XmlElement source, string location)
        {
            Workbook template = new Workbook();

            //�qResources��TemplateŪ�X��
            template.Open(new MemoryStream(Properties.Resources.TransferringStudentUpdateRecordListTemplate), FileFormatType.Excel2003);

            //�n���ͪ�excel��
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(Properties.Resources.TransferringStudentUpdateRecordListTemplate), FileFormatType.Excel2003);

            Worksheet ws = wb.Worksheets[0];

            //�������j�X��row
            int next = 23;

            //�������X��col
            int col = 14;

            //���row�ƥ�
            int dataRow = 16;

            //����
            int index = 0;

            //�d���d��
            Range tempRange = template.Worksheets[0].Cells.CreateRange(0,23,false);

            //�`�@�X�����ʬ���
            int count = 0;
            int totalRec = source.SelectNodes("�M��/���ʬ���").Count;

            foreach (XmlNode list in source.SelectNodes("�M��"))
            {
                //���ͲM��Ĥ@��
                ws.Cells.CreateRange(index, next, false).Copy(tempRange);

                //Page
                int currentPage = 1;
                int totalPage = (list.ChildNodes.Count / dataRow) + 1;


                //�g�J�N��
                ws.Cells[index, 11].PutValue(source.SelectSingleNode("@�ǮեN��").InnerText + "-" + list.SelectSingleNode("@��O�N��").InnerText);

                //�g�J�զW�B�Ǧ~�סB�Ǵ��B��O�B�~��
                ws.Cells[index + 2, 1].PutValue(source.SelectSingleNode("@�ǮզW��").InnerText);
                ws.Cells[index + 2, 5].PutValue(source.SelectSingleNode("@�Ǧ~��").InnerText + " �Ǧ~�� �� " + source.SelectSingleNode("@�Ǵ�").InnerText + " �Ǵ�");
                ws.Cells[index + 2, 8].PutValue(list.SelectSingleNode("@��O").InnerText);
                ws.Cells[index + 2, 12].PutValue(list.SelectSingleNode("@�~��").InnerText + "�~��");

                //�g�J���
                int recCount = 0;
                int dataIndex = index + 6;
                for (; currentPage <= totalPage; currentPage++)
                {
                    //�ƻs����
                    if (currentPage + 1 <= totalPage)
                    {
                        ws.Cells.CreateRange(index + next, next, false).Copy(tempRange);
                    }

                    //��J���
                    for (int i = 0; i < dataRow && recCount < list.ChildNodes.Count; i++, recCount++)
                    {
                        //MsgBox.Show(i.ToString()+" "+recCount.ToString());
                        XmlNode rec = list.SelectNodes("���ʬ���")[recCount];
                        ws.Cells[dataIndex, 0].PutValue(rec.SelectSingleNode("@�s�Ǹ�").InnerText);
                        ws.Cells[dataIndex, 1].PutValue(rec.SelectSingleNode("@�m�W").InnerText);
                        ws.Cells[dataIndex, 2].PutValue(rec.SelectSingleNode("@�����Ҹ�").InnerText.ToString());
                        ws.Cells[dataIndex, 3].PutValue(rec.SelectSingleNode("@�ʧO�N��").InnerText);
                        ws.Cells[dataIndex, 4].PutValue(rec.SelectSingleNode("@�ʧO").InnerText);
                        ws.Cells[dataIndex, 5].PutValue(rec.SelectSingleNode("@�X�ͦ~���").InnerText);
                        ws.Cells[dataIndex, 6].PutValue(rec.SelectSingleNode("@��J�e�ǥ͸��_�Ǯ�").InnerText);
                        ws.Cells[dataIndex, 7].PutValue(rec.SelectSingleNode("@��J�e�ǥ͸��_�Ǹ�").InnerText + "\n" + rec.SelectSingleNode("@��J�e�ǥ͸��_��O").InnerText);
                        ws.Cells[dataIndex, 8].PutValue(rec.SelectSingleNode("@��J�e�ǥ͸��_�Ƭd���").InnerText + "\n" + rec.SelectSingleNode("@��J�e�ǥ͸��_�Ƭd�帹").InnerText);
                        ws.Cells[dataIndex, 9].PutValue(rec.SelectSingleNode("@��J�e�ǥ͸��_�~��").InnerText);
                        ws.Cells[dataIndex, 10].PutValue(rec.SelectSingleNode("@���ʥN��").InnerText);
                        ws.Cells[dataIndex, 11].PutValue(rec.SelectSingleNode("@��]�Ψƶ�").InnerText);
                        ws.Cells[dataIndex, 12].PutValue(rec.SelectSingleNode("@���ʤ��").InnerText);
                        ws.Cells[dataIndex, 13].PutValue(rec.SelectSingleNode("@�Ƶ�").InnerText);
                        dataIndex++;
                        count++;

                        //��J�e�ǥ͸��_�Ǯ�="�|������" ��J�e�ǥ͸��_�Ǹ�="010101" ��J�e�ǥ͸��_��O="��T��" ��J�e�ǥ͸��_�Ƭd���="90/09/09" ��J�e�ǥ͸��_�Ƭd�帹="�Ф��T�r��09200909090��" ��J�e�ǥ͸��_�~��="�@�W"
                    }

                    //�p��X�p
                    if (currentPage == totalPage)
                    {
                        ws.Cells.CreateRange(dataIndex, 0, 1, 2).Merge();
                        ws.Cells[dataIndex, 0].PutValue("�X�p " + list.ChildNodes.Count.ToString() + " �W");
                    }

                    //����
                    ws.Cells[index + next -1, 10].PutValue("�� " + currentPage + " ���A�@ " + totalPage + " ��");
                    ws.HPageBreaks.Add(index + next, col);

                    //���ޫ��V�U�@��
                    index += next;
                    dataIndex = index + 6;

                    //�^���i��
                    ReportProgress((int)(((double)count * 100.0) / ((double)totalRec)));
                }
            }


            #region ��J��,�q�l�榡

            Worksheet TemplateWb = wb.Worksheets["�q�l�榡�d��"];

            Worksheet DyWb = wb.Worksheets[wb.Worksheets.Add()];
            DyWb.Name = "�q�l�榡";

            Range range_H = TemplateWb.Cells.CreateRange(0, 1, false);
            Range range_R = TemplateWb.Cells.CreateRange(1, 1, false);
            DyWb.Cells.CreateRange(0, 1, false).Copy(range_H);

            int DyWb_index = 0;

            foreach (XmlElement Record in source.SelectNodes("�M��/���ʬ���"))
            {
                DyWb_index++;
                //�C�W�[�@��,�ƻs�@��
                DyWb.Cells.CreateRange(DyWb_index, 1, false).Copy(range_R);

                //�Z�O
                DyWb.Cells[DyWb_index, 0].PutValue("");
                //��O�N�X
                DyWb.Cells[DyWb_index, 1].PutValue((Record.ParentNode as XmlElement).GetAttribute("��O�N��"));
                //�Ǹ�
                DyWb.Cells[DyWb_index, 2].PutValue(Record.GetAttribute("�s�Ǹ�"));
                //�m�W
                DyWb.Cells[DyWb_index, 3].PutValue(Record.GetAttribute("�m�W"));
                //�����Ҧr��
                DyWb.Cells[DyWb_index, 4].PutValue(Record.GetAttribute("�����Ҹ�"));
                //��1
                DyWb.Cells[DyWb_index, 5].PutValue("");
                //�ʧO�N�X
                DyWb.Cells[DyWb_index, 6].PutValue(Record.GetAttribute("�ʧO�N��"));
                //�X�ͤ��
                DyWb.Cells[DyWb_index, 7].PutValue(GetBirthdateWithoutSlash(Record.GetAttribute("�X�ͦ~���")));
                //�S�����N�X
                DyWb.Cells[DyWb_index, 8].PutValue(Record.GetAttribute("�Ƶ�"));
                //�~��
                DyWb.Cells[DyWb_index, 9].PutValue((Record.ParentNode as XmlElement).GetAttribute("�~��"));
                //���ʭ�]�N�X
                DyWb.Cells[DyWb_index, 10].PutValue(Record.GetAttribute("���ʥN��"));
                //��J���
                DyWb.Cells[DyWb_index, 11].PutValue(GetBirthdateWithoutSlash(Record.GetAttribute("���ʤ��")));
                //��Ƭd���
                DyWb.Cells[DyWb_index, 12].PutValue(GetBirthdateWithoutSlash(Record.GetAttribute("��J�e�ǥ͸��_�Ƭd���")));
                //��Ƭd��r(*)
                DyWb.Cells[DyWb_index, 13].PutValue(GetNumAndSrt1(Record.GetAttribute("��J�e�ǥ͸��_�Ƭd�帹")));
                //��Ƭd�帹(*)
                DyWb.Cells[DyWb_index, 14].PutValue(GetNumAndSrt2(Record.GetAttribute("��J�e�ǥ͸��_�Ƭd�帹")));
                //��ǮեN�X(*)
                DyWb.Cells[DyWb_index, 15].PutValue(Record.GetAttribute("��J�e�ǥ͸��_�Ǯ�"));
                //���O�N�X
                DyWb.Cells[DyWb_index, 16].PutValue(Record.GetAttribute("��J�e�ǥ͸��_��O"));
                //��Ǹ�
                DyWb.Cells[DyWb_index, 17].PutValue(Record.GetAttribute("��J�e�ǥ͸��_�Ǹ�"));
                //��~��
                DyWb.Cells[DyWb_index, 18].PutValue(Getyear(Record.GetAttribute("��J�e�ǥ͸��_�~��")));
                //��Ǵ�
                DyWb.Cells[DyWb_index, 19].PutValue(Getsemester(Record.GetAttribute("��J�e�ǥ͸��_�~��")));
                //�Ƶ�����
                DyWb.Cells[DyWb_index, 20].PutValue("");
            }

            DyWb.AutoFitColumns();

            wb.Worksheets.RemoveAt("�q�l�榡�d��");

            #endregion

            wb.Worksheets.ActiveSheetIndex = 0;

            //�x�s
            wb.Save(location, FileFormatType.Excel2003);
        }


        #region ������

        //���~��
        #region ���~��

        private string Getyear(string year)
        {

            if (year.Contains("�@"))
            {
                return "1";
            }
            else if (year.Contains("�G"))
            {
                return "2";
            }
            else if (year.Contains("�T"))
            {
                return "3";
            }
            else if (year.Contains("�|"))
            {
                return "4";
            }
            else
            {
                return year;
            }

        }

        #endregion


        //���Ǵ�
        #region ���Ǵ�

        private string Getsemester(string sem)
        {

            if (sem.Contains("�W"))
            {
                return "1";
            }
            else if (sem.Contains("�U"))
            {
                return "2";
            }
            else
            {
                return sem;
            }
        }

        #endregion


        //����r
        #region ����r

        private string GetNumAndSrt1(string fuct)
        {
            if (fuct.Contains("�r"))
            {
                return fuct.Remove(fuct.LastIndexOf("�r"));
            }
            return fuct;
        }

        #endregion

        //���帹
        #region ���帹

        private string GetNumAndSrt2(string fuct)
        {

            if (fuct.Contains("��") && fuct.Contains("��"))
            {
                return fuct.Substring(fuct.LastIndexOf("��") + 1, fuct.LastIndexOf("��") - fuct.LastIndexOf("��") - 1);
            }
            return fuct;

        }

        #endregion

        //�褸�����~
        #region �褸�����~
        private string GetBirthdateWithoutSlash(string orig)
        {
            if (string.IsNullOrEmpty(orig)) return orig;
            string[] array = orig.Split('/');
            int chang;
            if (array[0].Length == 4)
            {
                chang = int.Parse(array[0]) - 1911;
            }
            else
            {
                chang = int.Parse(array[0]);
            }
            return chang.ToString() + array[1].PadLeft(2, '0') + array[2].PadLeft(2, '0');
        }
        #endregion 

        #endregion



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
            get { return "��J�ǥͦW�U"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
