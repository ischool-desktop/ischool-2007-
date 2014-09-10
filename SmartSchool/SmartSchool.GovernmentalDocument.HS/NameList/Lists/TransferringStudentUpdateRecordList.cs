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

            //從Resources把Template讀出來
            template.Open(new MemoryStream(Properties.Resources.TransferringStudentUpdateRecordListTemplate), FileFormatType.Excel2003);

            //要產生的excel檔
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(Properties.Resources.TransferringStudentUpdateRecordListTemplate), FileFormatType.Excel2003);

            Worksheet ws = wb.Worksheets[0];

            //頁面間隔幾個row
            int next = 23;

            //頁面有幾個col
            int col = 14;

            //資料row數目
            int dataRow = 16;

            //索引
            int index = 0;

            //範本範圍
            Range tempRange = template.Worksheets[0].Cells.CreateRange(0,23,false);

            //總共幾筆異動紀錄
            int count = 0;
            int totalRec = source.SelectNodes("清單/異動紀錄").Count;

            foreach (XmlNode list in source.SelectNodes("清單"))
            {
                //產生清單第一頁
                ws.Cells.CreateRange(index, next, false).Copy(tempRange);

                //Page
                int currentPage = 1;
                int totalPage = (list.ChildNodes.Count / dataRow) + 1;


                //寫入代號
                ws.Cells[index, 11].PutValue(source.SelectSingleNode("@學校代號").InnerText + "-" + list.SelectSingleNode("@科別代號").InnerText);

                //寫入校名、學年度、學期、科別、年級
                ws.Cells[index + 2, 1].PutValue(source.SelectSingleNode("@學校名稱").InnerText);
                ws.Cells[index + 2, 5].PutValue(source.SelectSingleNode("@學年度").InnerText + " 學年度 第 " + source.SelectSingleNode("@學期").InnerText + " 學期");
                ws.Cells[index + 2, 8].PutValue(list.SelectSingleNode("@科別").InnerText);
                ws.Cells[index + 2, 12].PutValue(list.SelectSingleNode("@年級").InnerText + "年級");

                //寫入資料
                int recCount = 0;
                int dataIndex = index + 6;
                for (; currentPage <= totalPage; currentPage++)
                {
                    //複製頁面
                    if (currentPage + 1 <= totalPage)
                    {
                        ws.Cells.CreateRange(index + next, next, false).Copy(tempRange);
                    }

                    //填入資料
                    for (int i = 0; i < dataRow && recCount < list.ChildNodes.Count; i++, recCount++)
                    {
                        //MsgBox.Show(i.ToString()+" "+recCount.ToString());
                        XmlNode rec = list.SelectNodes("異動紀錄")[recCount];
                        ws.Cells[dataIndex, 0].PutValue(rec.SelectSingleNode("@新學號").InnerText);
                        ws.Cells[dataIndex, 1].PutValue(rec.SelectSingleNode("@姓名").InnerText);
                        ws.Cells[dataIndex, 2].PutValue(rec.SelectSingleNode("@身分證號").InnerText.ToString());
                        ws.Cells[dataIndex, 3].PutValue(rec.SelectSingleNode("@性別代號").InnerText);
                        ws.Cells[dataIndex, 4].PutValue(rec.SelectSingleNode("@性別").InnerText);
                        ws.Cells[dataIndex, 5].PutValue(rec.SelectSingleNode("@出生年月日").InnerText);
                        ws.Cells[dataIndex, 6].PutValue(rec.SelectSingleNode("@轉入前學生資料_學校").InnerText);
                        ws.Cells[dataIndex, 7].PutValue(rec.SelectSingleNode("@轉入前學生資料_學號").InnerText + "\n" + rec.SelectSingleNode("@轉入前學生資料_科別").InnerText);
                        ws.Cells[dataIndex, 8].PutValue(rec.SelectSingleNode("@轉入前學生資料_備查日期").InnerText + "\n" + rec.SelectSingleNode("@轉入前學生資料_備查文號").InnerText);
                        ws.Cells[dataIndex, 9].PutValue(rec.SelectSingleNode("@轉入前學生資料_年級").InnerText);
                        ws.Cells[dataIndex, 10].PutValue(rec.SelectSingleNode("@異動代號").InnerText);
                        ws.Cells[dataIndex, 11].PutValue(rec.SelectSingleNode("@原因及事項").InnerText);
                        ws.Cells[dataIndex, 12].PutValue(rec.SelectSingleNode("@異動日期").InnerText);
                        ws.Cells[dataIndex, 13].PutValue(rec.SelectSingleNode("@備註").InnerText);
                        dataIndex++;
                        count++;

                        //轉入前學生資料_學校="糕忠高中" 轉入前學生資料_學號="010101" 轉入前學生資料_科別="資訊科" 轉入前學生資料_備查日期="90/09/09" 轉入前學生資料_備查文號="教中三字第09200909090號" 轉入前學生資料_年級="一上"
                    }

                    //計算合計
                    if (currentPage == totalPage)
                    {
                        ws.Cells.CreateRange(dataIndex, 0, 1, 2).Merge();
                        ws.Cells[dataIndex, 0].PutValue("合計 " + list.ChildNodes.Count.ToString() + " 名");
                    }

                    //分頁
                    ws.Cells[index + next -1, 10].PutValue("第 " + currentPage + " 頁，共 " + totalPage + " 頁");
                    ws.HPageBreaks.Add(index + next, col);

                    //索引指向下一頁
                    index += next;
                    dataIndex = index + 6;

                    //回報進度
                    ReportProgress((int)(((double)count * 100.0) / ((double)totalRec)));
                }
            }


            #region 轉入生,電子格式

            Worksheet TemplateWb = wb.Worksheets["電子格式範本"];

            Worksheet DyWb = wb.Worksheets[wb.Worksheets.Add()];
            DyWb.Name = "電子格式";

            Range range_H = TemplateWb.Cells.CreateRange(0, 1, false);
            Range range_R = TemplateWb.Cells.CreateRange(1, 1, false);
            DyWb.Cells.CreateRange(0, 1, false).Copy(range_H);

            int DyWb_index = 0;

            foreach (XmlElement Record in source.SelectNodes("清單/異動紀錄"))
            {
                DyWb_index++;
                //每增加一行,複製一次
                DyWb.Cells.CreateRange(DyWb_index, 1, false).Copy(range_R);

                //班別
                DyWb.Cells[DyWb_index, 0].PutValue("");
                //科別代碼
                DyWb.Cells[DyWb_index, 1].PutValue((Record.ParentNode as XmlElement).GetAttribute("科別代號"));
                //學號
                DyWb.Cells[DyWb_index, 2].PutValue(Record.GetAttribute("新學號"));
                //姓名
                DyWb.Cells[DyWb_index, 3].PutValue(Record.GetAttribute("姓名"));
                //身分證字號
                DyWb.Cells[DyWb_index, 4].PutValue(Record.GetAttribute("身分證號"));
                //註1
                DyWb.Cells[DyWb_index, 5].PutValue("");
                //性別代碼
                DyWb.Cells[DyWb_index, 6].PutValue(Record.GetAttribute("性別代號"));
                //出生日期
                DyWb.Cells[DyWb_index, 7].PutValue(GetBirthdateWithoutSlash(Record.GetAttribute("出生年月日")));
                //特殊身份代碼
                DyWb.Cells[DyWb_index, 8].PutValue(Record.GetAttribute("備註"));
                //年級
                DyWb.Cells[DyWb_index, 9].PutValue((Record.ParentNode as XmlElement).GetAttribute("年級"));
                //異動原因代碼
                DyWb.Cells[DyWb_index, 10].PutValue(Record.GetAttribute("異動代號"));
                //轉入日期
                DyWb.Cells[DyWb_index, 11].PutValue(GetBirthdateWithoutSlash(Record.GetAttribute("異動日期")));
                //原備查日期
                DyWb.Cells[DyWb_index, 12].PutValue(GetBirthdateWithoutSlash(Record.GetAttribute("轉入前學生資料_備查日期")));
                //原備查文字(*)
                DyWb.Cells[DyWb_index, 13].PutValue(GetNumAndSrt1(Record.GetAttribute("轉入前學生資料_備查文號")));
                //原備查文號(*)
                DyWb.Cells[DyWb_index, 14].PutValue(GetNumAndSrt2(Record.GetAttribute("轉入前學生資料_備查文號")));
                //原學校代碼(*)
                DyWb.Cells[DyWb_index, 15].PutValue(Record.GetAttribute("轉入前學生資料_學校"));
                //原科別代碼
                DyWb.Cells[DyWb_index, 16].PutValue(Record.GetAttribute("轉入前學生資料_科別"));
                //原學號
                DyWb.Cells[DyWb_index, 17].PutValue(Record.GetAttribute("轉入前學生資料_學號"));
                //原年級
                DyWb.Cells[DyWb_index, 18].PutValue(Getyear(Record.GetAttribute("轉入前學生資料_年級")));
                //原學期
                DyWb.Cells[DyWb_index, 19].PutValue(Getsemester(Record.GetAttribute("轉入前學生資料_年級")));
                //備註說明
                DyWb.Cells[DyWb_index, 20].PutValue("");
            }

            DyWb.AutoFitColumns();

            wb.Worksheets.RemoveAt("電子格式範本");

            #endregion

            wb.Worksheets.ActiveSheetIndex = 0;

            //儲存
            wb.Save(location, FileFormatType.Excel2003);
        }


        #region 切切切

        //切年級
        #region 切年級

        private string Getyear(string year)
        {

            if (year.Contains("一"))
            {
                return "1";
            }
            else if (year.Contains("二"))
            {
                return "2";
            }
            else if (year.Contains("三"))
            {
                return "3";
            }
            else if (year.Contains("四"))
            {
                return "4";
            }
            else
            {
                return year;
            }

        }

        #endregion


        //切學期
        #region 切學期

        private string Getsemester(string sem)
        {

            if (sem.Contains("上"))
            {
                return "1";
            }
            else if (sem.Contains("下"))
            {
                return "2";
            }
            else
            {
                return sem;
            }
        }

        #endregion


        //切文字
        #region 切文字

        private string GetNumAndSrt1(string fuct)
        {
            if (fuct.Contains("字"))
            {
                return fuct.Remove(fuct.LastIndexOf("字"));
            }
            return fuct;
        }

        #endregion

        //切文號
        #region 切文號

        private string GetNumAndSrt2(string fuct)
        {

            if (fuct.Contains("第") && fuct.Contains("號"))
            {
                return fuct.Substring(fuct.LastIndexOf("第") + 1, fuct.LastIndexOf("號") - fuct.LastIndexOf("第") - 1);
            }
            return fuct;

        }

        #endregion

        //西元轉民國年
        #region 西元轉民國年
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
            get { return "中部辦公室95年11月編印管理手冊規範格式"; }
        }

        public override string ReportName
        {
            get { return "轉入學生名冊"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
