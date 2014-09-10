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

            //從Resources把Template讀出來
            template.Open(new MemoryStream(Properties.Resources.GraduatingStudentListTemplate), FileFormatType.Excel2003);

            //要產生的excel檔
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(Properties.Resources.GraduatingStudentListTemplate), FileFormatType.Excel2003);

            Worksheet ws = wb.Worksheets[0];

            //頁面間隔幾個row
            int next = 24;

            //索引
            int index = 0;

            //範本範圍
            Range tempRange = template.Worksheets[0].Cells.CreateRange(0, 24, false);

            //總共幾筆異動紀錄
            int count = 0;
            int totalRec = source.SelectNodes("清單/異動紀錄").Count;

            foreach (XmlNode list in source.SelectNodes("清單"))
            {
                //產生清單第一頁
                //for (int row = 0; row < next; row++)
                //{
                //    ws.Cells.CopyRow(template.Worksheets[0].Cells, row, row + index);
                //}
                ws.Cells.CreateRange(index, 24, false).Copy(tempRange);

                //Page
                int currentPage = 1;
                int totalPage = (list.ChildNodes.Count / 18) + 1;

                //寫入名冊類別
                if (source.SelectSingleNode("@類別").InnerText == "延修生畢業名冊")
                    ws.Cells[index, 0].PutValue(ws.Cells[index, 0].StringValue.Replace("□畢業", "■畢業"));
                else
                    ws.Cells[index, 0].PutValue(ws.Cells[index, 0].StringValue.Replace("□結業", "■結業"));

                //寫入代號
                ws.Cells[index, 6].PutValue("代碼：" + source.SelectSingleNode("@學校代號").InnerText + "-" + list.SelectSingleNode("@科別代號").InnerText);

                //寫入校名、學年度、學期、科別
                ws.Cells[index + 2, 0].PutValue("校名：" + source.SelectSingleNode("@學校名稱").InnerText);
                ws.Cells[index + 2, 4].PutValue(source.SelectSingleNode("@學年度").InnerText + "學年度 第" + source.SelectSingleNode("@學期").InnerText + "學期");
                ws.Cells[index + 2, 6].PutValue(list.SelectSingleNode("@科別").InnerText);

                //寫入資料
                int recCount = 0;
                int dataIndex = index + 5;
                for (; currentPage <= totalPage; currentPage++)
                {
                    //複製頁面
                    if (currentPage + 1 <= totalPage)
                    {
                        //for (int row = 0; row < next; row++)
                        //{
                        //    ws.Cells.CopyRow(ws.Cells, row + index, row + index + next);
                        //}
                        ws.Cells.CreateRange(index + next, 24, false).Copy(tempRange);
                    }

                    //填入資料
                    for (int i = 0; i < 18 && recCount < list.ChildNodes.Count; i++, recCount++)
                    {
                        //MsgBox.Show(i.ToString()+" "+recCount.ToString());
                        XmlNode rec = list.SelectNodes("異動紀錄")[recCount];
                        ws.Cells[dataIndex, 0].PutValue(rec.SelectSingleNode("@學號").InnerText + "\n" + rec.SelectSingleNode("@姓名").InnerText);
                        ws.Cells[dataIndex, 1].PutValue(rec.SelectSingleNode("@性別代號").InnerText.ToString());
                        ws.Cells[dataIndex, 2].PutValue(rec.SelectSingleNode("@性別").InnerText);
                        ws.Cells[dataIndex, 3].PutValue(rec.SelectSingleNode("@生日").InnerText + "\n" + rec.SelectSingleNode("@身份證號").InnerText);
                        ws.Cells[dataIndex, 4].PutValue(rec.SelectSingleNode("@最後異動代號").InnerText.ToString());
                        ws.Cells[dataIndex, 5].PutValue(rec.SelectSingleNode("@備查日期").InnerText + "\n" + rec.SelectSingleNode("@備查文號").InnerText);
                        ws.Cells[dataIndex, 6].PutValue(rec.SelectSingleNode("@畢業證書字號").InnerText);
                        ws.Cells[dataIndex, 7].PutValue(rec.SelectSingleNode("@備註").InnerText);
                        dataIndex++;
                        count++;
                    }

                    //計算合計
                    if (currentPage == totalPage)
                    {
                        ws.Cells[index + 22, 0].PutValue("合計");
                        ws.Cells[index + 22, 1].PutValue(list.ChildNodes.Count.ToString());
                    }

                    //分頁
                    ws.Cells[index + 23, 6].PutValue("第 " + currentPage + " 頁，共 " + totalPage + " 頁");
                    ws.HPageBreaks.Add(index + 24, 8);

                    //索引指向下一頁
                    index += next;
                    dataIndex = index + 5;

                    //回報進度
                    ReportProgress((int)(((double)count * 100.0) / ((double)totalRec)));
                }
            }

            //儲存
            wb.Save(location, FileFormatType.Excel2003);
        }

        public override string Copyright
        {
            get { return "IntelliSchool"; }
        }

        public override string Description
        {
            get { return "中部辦公室95年11月編印管理手冊規範格式"; }
        }

        public override string ReportName
        {
            get { return "延修生畢業名冊"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
