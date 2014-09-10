using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;
using System.Xml;
using System.IO;

namespace SmartSchool.GovernmentalDocument.NameList
{
    public class GraduatingStudentList : ReportBuilder
    {
        protected override void Build(System.Xml.XmlElement source, string location)
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
            Range tempRange = template.Worksheets[0].Cells.CreateRange(0,24,false);

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
                ws.Cells.CreateRange(index, next, false).Copy(tempRange);

                //Page
                int currentPage = 1;
                int totalPage = (list.ChildNodes.Count / 18) + 1;

                //寫入名冊類別
                if (source.SelectSingleNode("@類別").InnerText == "畢業名冊")
                    ws.Cells[index, 0].PutValue(ws.Cells[index, 0].StringValue.Replace("□畢業", "■畢業"));
                else
                    ws.Cells[index, 0].PutValue(ws.Cells[index, 0].StringValue.Replace("□結業", "■結業"));

                ////寫入代號
                //ws.Cells[index,6].PutValue("代碼："+source.SelectSingleNode("@學校代號").InnerText+"-"+list.SelectSingleNode("@科別代號").InnerText);

                ////寫入校名、學年度、學期、科別
                //ws.Cells[index+2, 0].PutValue("校名：" + source.SelectSingleNode("@學校名稱").InnerText);
                //ws.Cells[index+2, 4].PutValue(source.SelectSingleNode("@學年度").InnerText + "學年度 第" + source.SelectSingleNode("@學期").InnerText + "學期");
                //ws.Cells[index+2, 6].PutValue(list.SelectSingleNode("@科別").InnerText);

                //寫入資料
                int recCount = 0;
                int dataIndex = index + 5;
                for (; currentPage <= totalPage; currentPage++)
                {
                    //寫入代號
                    ws.Cells[index, 6].PutValue("代碼：" + source.SelectSingleNode("@學校代號").InnerText + "-" + list.SelectSingleNode("@科別代號").InnerText);

                    //寫入校名、學年度、學期、科別
                    ws.Cells[index + 2, 0].PutValue("校名：" + source.SelectSingleNode("@學校名稱").InnerText);
                    ws.Cells[index + 2, 4].PutValue(source.SelectSingleNode("@學年度").InnerText + "學年度 第" + source.SelectSingleNode("@學期").InnerText + "學期");
                    ws.Cells[index + 2, 6].PutValue(list.SelectSingleNode("@科別").InnerText);

                    //複製頁面
                    if (currentPage+1 <= totalPage)
                    {
                        ws.Cells.CreateRange(index + next, next, false).Copy(tempRange);

                        //寫入名冊類別
                        if (source.SelectSingleNode("@類別").InnerText == "畢業名冊")
                            ws.Cells[index + next, 0].PutValue(ws.Cells[index + next, 0].StringValue.Replace("□畢業", "■畢業"));
                        else
                            ws.Cells[index + next, 0].PutValue(ws.Cells[index + next, 0].StringValue.Replace("□結業", "■結業"));
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
                        ws.Cells[dataIndex, 5].PutValue(rec.SelectSingleNode("@備查日期").InnerText + "\n" +rec.SelectSingleNode("@備查文號").InnerText);
                        ws.Cells[dataIndex, 6].PutValue(rec.SelectSingleNode("@畢業證書字號").InnerText);
                        ws.Cells[dataIndex, 7].PutValue(rec.SelectSingleNode("@備註").InnerText);
                        dataIndex++;
                        count++;
                    }

                    //計算合計
                    if (currentPage == totalPage)
                    {
                        ws.Cells[dataIndex, 0].PutValue("合計");
                        ws.Cells[dataIndex, 1].PutValue(list.ChildNodes.Count.ToString());
                        //ws.Cells[index + 22, 0].PutValue("合計");
                        //ws.Cells[index + 22, 1].PutValue(list.ChildNodes.Count.ToString());
                    }

                    //分頁
                    ws.Cells[index + 23, 6].PutValue("第 " + currentPage + " 頁，共 " + totalPage + " 頁");
                    ws.HPageBreaks.Add(index+24, 8);

                    //索引指向下一頁
                    index += next;
                    dataIndex = index + 5;

                    //回報進度
                    ReportProgress((int)(((double)count * 100.0) / ((double)totalRec)));
                }
            }


            #region 畢業異動,電子格式

            //範本
            Worksheet TemplateWb = wb.Worksheets["電子格式範本"];
            //實做頁面
            Worksheet DyWb = wb.Worksheets[wb.Worksheets.Add()];
            //名稱
            DyWb.Name = "電子格式";
            //範圍
            Range range_H = TemplateWb.Cells.CreateRange(0, 1, false);
            Range range_R = TemplateWb.Cells.CreateRange(1, 1, false);
            //拷貝range_H
            DyWb.Cells.CreateRange(0, 1, false).Copy(range_H);

            int DyWb_index = 0;

            foreach(XmlElement Record in source.SelectNodes("清單/異動紀錄"))
            {
                DyWb_index++;
                //每增加一行,複製一次
                DyWb.Cells.CreateRange(DyWb_index,1,false).Copy(range_R);

                //班別
                DyWb.Cells[DyWb_index, 0].PutValue("");
                //科別代碼
                DyWb.Cells[DyWb_index, 1].PutValue((Record.ParentNode as XmlElement).GetAttribute("科別代號"));
                //學號
                DyWb.Cells[DyWb_index, 2].PutValue(Record.GetAttribute("學號"));
                //姓名
                DyWb.Cells[DyWb_index, 3].PutValue(Record.GetAttribute("姓名"));
                //身分證字號
                DyWb.Cells[DyWb_index, 4].PutValue(Record.GetAttribute("身份證號"));

                //註1
                DyWb.Cells[DyWb_index,5].PutValue("");

                //性別代碼
                DyWb.Cells[DyWb_index, 6].PutValue(Record.GetAttribute("性別代號"));
                //出生日期
                DyWb.Cells[DyWb_index, 7].PutValue(GetBirthdateWithoutSlash(Record.GetAttribute("生日")));
                //畢業證書字號
                DyWb.Cells[DyWb_index, 8].PutValue(Record.GetAttribute("畢業證書字號"));
                //備註說明
                DyWb.Cells[DyWb_index,9].PutValue("");


            }

            DyWb.AutoFitColumns();

            wb.Worksheets.RemoveAt("電子格式範本");
            #endregion

            wb.Worksheets.ActiveSheetIndex = 0;
            //儲存
            wb.Save(location, FileFormatType.Excel2003);
        }

        //處理符號"/"
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
            get { return "畢業名冊"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
