using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;
using System.Xml;
using System.IO;

namespace SmartSchool.GovernmentalDocument.NameList
{
    public class ExtendingStudentList : ReportBuilder
    {
        protected override void Build(System.Xml.XmlElement source, string location)
        {
            #region �إ� Excel

            //�q Resources �N���y���ʦW�UtemplateŪ�X��
            Workbook template = new Workbook();
            template.Open(new MemoryStream(Properties.Resources.ExtendingStudentListTemplate), FileFormatType.Excel2003);

            //���� excel
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(Properties.Resources.ExtendingStudentListTemplate), FileFormatType.Excel2003);
                
            #endregion

            #region �ƻs�˦�-�w�]�˦��B��e

            //�]�w�w�]�˦�
            wb.DefaultStyle = template.DefaultStyle;

            //�ƻs�˪����e18�� Column(��e)
            for (int m = 0; m < 18; m++)
            {
                /*
                 * �ƻs template���Ĥ@�� Sheet���� m�� Column
                 * �� wb���Ĥ@�� Sheet���� m�� Column
                 */
                wb.Worksheets[0].Cells.CopyColumn(template.Worksheets[0].Cells, m, m);
            }

            #endregion

            #region ��l�ܼ�
            
                /****************************** 
                * rowi ��J�Ǯո�ƥ�
                * rowj ��J�ǥ͸�ƥ�
                * num �p��M�����
                * numcount �p��C���M�歶��
                * j �p��Ҳ��ͲM�歶��
                * x �P�_�ӼƬO�_��20�Q�ƥ�
                ******************************/
                int rowi = 0, rowj = 1, num = source.SelectNodes("�M��").Count, numcount = 1, j = 0;
                bool x = false;

                int recCount = 0;
                int totalRec = source.SelectNodes("�M��/���ʬ���").Count;
            
            #endregion

            foreach (XmlNode list in source.SelectNodes("�M��"))
            {
                int i = 0;

                #region ��X����`�ƤΧP�_

                //��X����`�Ƥ�K�����i��
                int count = list.SelectNodes("���ʬ���").Count;

                //�P�_�ӼƬO�_��20�Q��
                if (count % 20 == 0)
                {
                    x = true;
                } 

                #endregion
                

                #region ���ʬ���

                //�Nxml��ƶ�J��excel
                foreach (XmlNode st in list.SelectNodes("���ʬ���"))
                {
                    recCount++;
                    if (i % 20 == 0)
                    {
                        #region �ƻs�˦�-�氪�B�d��

                        //�ƻs�˪����e287�� Row(�氪)
                        //for (int m = 0; m < 28; m++)
                        //{
                        //    /*
                        //     * �ƻs template���Ĥ@�� Sheet����m�� Row
                        //     * �� wb���Ĥ@�� Sheet����(j * 28) + m�� Row
                        //     */
                        //    wb.Worksheets[0].Cells.CopyRow(template.Worksheets[0].Cells, m, (j * 28) + m);
                        //}

                        /*
                         * �ƻsStyle(�]�t�x�s��X�֪���T)
                         * ����CreateRange()����n�ƻs��Range("A1", "R28")
                         * �A��CopyStyle�ƻs�t�@��Range�����榡
                         */
                        Range range = template.Worksheets[0].Cells.CreateRange(0, 28, false);
                        int t= j * 28;
                        wb.Worksheets[0].Cells.CreateRange(t,28,false).Copy(range);

                        #endregion

                        #region ��J�Ǯո��

                        //�N�Ǯո�ƶ�J�A����m��
                        wb.Worksheets[0].Cells[rowi, 13].PutValue(source.SelectSingleNode("@�ǮեN��").InnerText);
                        wb.Worksheets[0].Cells[rowi, 16].PutValue(list.SelectSingleNode("@��O�N��").InnerText);
                        wb.Worksheets[0].Cells[rowi + 2, 2].PutValue(source.SelectSingleNode("@�ǮզW��").InnerText);
                        wb.Worksheets[0].Cells[rowi + 2, 7].PutValue(Convert.ToInt32(source.SelectSingleNode("@�Ǧ~��").InnerText)+" �Ǧ~�� �� "+Convert.ToInt32(source.SelectSingleNode("@�Ǵ�").InnerText)+" �Ǵ�");
                        wb.Worksheets[0].Cells[rowi + 2, 12].PutValue(list.SelectSingleNode("@��O").InnerText);
                        wb.Worksheets[0].Cells[rowi + 2, 14].PutValue(list.SelectSingleNode("@�~��").InnerText);

                        #endregion

                        if (j > 0)
                        {
                            //���J����(�b j * 28 �� (j * 28) +1 �����AR��S����)
                            wb.Worksheets[0].HPageBreaks.Add(j * 28, 18);
                            rowj += 8;
                        }
                        else
                        {
                            rowj = 6;
                        }

                        rowi += 28;
                        j++;

                        #region ��ܭ���

                        //��ܭ���
                        if (x != true)
                        {
                            wb.Worksheets[0].Cells[(28 * (j - 1)) + 27, 13].PutValue("��" + numcount + "���A�@" + Math.Ceiling((double)count / 20) + "��");
                        }
                        else
                        {
                            wb.Worksheets[0].Cells[(28 * (j - 1)) + 27, 13].PutValue("��" + numcount + "���A�@" + (Math.Ceiling((double)count / 20) + 1) + "��");
                        }
                        numcount++;

                        #endregion
                    }

                    #region ��J�ǥ͸��
                    
                        //�N�ǥ͸�ƶ�J�A����m��
                        wb.Worksheets[0].Cells[rowj, 1].PutValue(st.SelectSingleNode("@�Ǹ�").InnerText);
                        wb.Worksheets[0].Cells[rowj, 3].PutValue(st.SelectSingleNode("@�m�W").InnerText);
                        wb.Worksheets[0].Cells[rowj, 4].PutValue(st.SelectSingleNode("@�����Ҹ�").InnerText);
                        wb.Worksheets[0].Cells[rowj, 8].PutValue(st.SelectSingleNode("@�Ƭd���").InnerText + "\n" + st.SelectSingleNode("@�Ƭd�帹").InnerText);
                        wb.Worksheets[0].Cells[rowj, 11].PutValue(st.SelectSingleNode("@���ʥN��").InnerText);
                        wb.Worksheets[0].Cells[rowj, 12].PutValue(st.SelectSingleNode("@��]�Ψƶ�").InnerText);
                        if (st.SelectSingleNode("@�s�Ǹ�").InnerText == "")
                        {
                            wb.Worksheets[0].Cells[rowj, 13].PutValue(st.SelectSingleNode("@���ʤ��").InnerText);
                        }
                        else
                        {
                            wb.Worksheets[0].Cells[rowj, 13].PutValue(st.SelectSingleNode("@�s�Ǹ�").InnerText + "\n" + st.SelectSingleNode("@���ʤ��").InnerText);
                        }
                            wb.Worksheets[0].Cells[rowj, 16].PutValue(st.SelectSingleNode("@�Ƶ�").InnerText);

                    #endregion

                    i++;
                    rowj++;

                    //�^���i��
                    ReportProgress((int)(((double)recCount * 100.0) / ((double)totalRec)));
                }

                #endregion

                #region �Y�ӼƬ�20���ơA�B�z��@����

                if (x == true)
                {

                    #region �ƻs�˦�-�氪�B�d��

                    //�ƻs�˪��e28�� Row(�氪)
                    //for (int m = 0; m < 28; m++)
                    //{
                    //    /*
                    //     * �ƻs template���Ĥ@�� Sheet����m�� Row
                    //     * �� wb���Ĥ@�� Sheet����(j * 28) + m�� Row
                    //     */
                    //    wb.Worksheets[0].Cells.CopyRow(template.Worksheets[0].Cells, m, (j * 28) + m);
                    //}

                    /*
                     * �ƻsStyle(�]�t�x�s��X�֪���T)
                     * ����CreateRange()����n�ƻs��Range("A1", "R28")
                     * �A��CopyStyle�ƻs�t�@��Range�����榡
                     */
                    Range range = template.Worksheets[0].Cells.CreateRange(0, 28, false);                    
                    int t= j * 28;
                    wb.Worksheets[0].Cells.CreateRange(t, 28, false).Copy(range);

                    #endregion

                    #region ��J�Ǯո��

                    //�N�Ǯո�ƶ�J�A����m��
                    wb.Worksheets[0].Cells[rowi, 13].PutValue(source.SelectSingleNode("@�ǮեN��").InnerText);
                    wb.Worksheets[0].Cells[rowi, 16].PutValue(list.SelectSingleNode("@��O�N��").InnerText);
                    wb.Worksheets[0].Cells[rowi + 2, 2].PutValue(source.SelectSingleNode("@�ǮզW��").InnerText);
                    wb.Worksheets[0].Cells[rowi + 2, 7].PutValue(Convert.ToInt32(source.SelectSingleNode("@�Ǧ~��").InnerText) + " �Ǧ~�� �� " + Convert.ToInt32(source.SelectSingleNode("@�Ǵ�").InnerText) + " �Ǵ�");
                    wb.Worksheets[0].Cells[rowi + 2, 12].PutValue(list.SelectSingleNode("@��O").InnerText);

                    #endregion

                    if (j > 0)
                    {
                        //���J����(�bi��i+1�����AO��P����)
                        wb.Worksheets[0].HPageBreaks.Add(j * 28, 18);
                        rowj += 8;
                    }

                    rowi += 28;
                    j++;

                    #region ��ܭ���

                    //��ܭ���
                    wb.Worksheets[0].Cells[(28 * (j - 1)) + 27, 13].PutValue("��" + numcount + "���A�@" + (Math.Ceiling((double)count / 20) + 1) + "��");
                    numcount++;

                    #endregion
                } 

                #endregion

                #region �έp�H��

                //��J�έp�H��
                wb.Worksheets[0].Cells.CreateRange(rowj, 1, 1, 2).UnMerge();
                wb.Worksheets[0].Cells.Merge(rowj, 1, 1, 3);
                wb.Worksheets[0].Cells[rowj, 1].PutValue("�X  �p " + count.ToString() + " �W");

                #endregion

                wb.Worksheets[0].HPageBreaks.Add(j * 28, 18);

                #region �]�w�ܼ�

                //�վ�s�M��Ҩϥ��ܼ�
                numcount = 1;
                rowj = (28 * j) - 2;
                rowi = (28 * j);
                x = false; 

                #endregion
            }

            //�x�s Excel
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
            get { return "���ץͦW�U"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
