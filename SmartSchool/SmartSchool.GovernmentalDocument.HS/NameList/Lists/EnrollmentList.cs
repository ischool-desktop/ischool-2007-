using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;
using System.Xml;
using System.IO;

namespace SmartSchool.GovernmentalDocument.NameList
{
    public class EnrollmentList : ReportBuilder
    {
        protected override void Build(System.Xml.XmlElement source, string location)
        {
            #region 建立 Excel

            //從 Resources 將新生名冊template讀出來
            Workbook template = new Workbook();
            template.Open(new MemoryStream(Properties.Resources.EnrollmentListTemplate), FileFormatType.Excel2003);

            //產生 excel
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(new MemoryStream(Properties.Resources.EnrollmentListTemplate), FileFormatType.Excel2003);

            #endregion

            #region 複製樣式-預設樣式、欄寬

            //設定預設樣式
            wb.DefaultStyle = template.DefaultStyle;

            //複製樣版中前18個 Column(欄寬)
            for (int m = 0; m < 18; m++)
            {
                /*
                 * 複製 template的第一個 Sheet的第 m個 Column
                 * 到 wb的第一個 Sheet的第 m個 Column
                 */
                wb.Worksheets[0].Cells.CopyColumn(template.Worksheets[0].Cells, m, m);
            }

            #endregion

            #region 初始變數
            
                /****************************** 
                * rowi 填入學校資料用
                * rowj 填入學生資料用
                * num 計算清單份數
                * numcount 計算每份清單頁數
                * j 計算所產生清單頁數
                * x 判斷個數是否為20被數用
                ******************************/
                int rowi = 0, rowj = 1, num = source.SelectNodes("清單").Count, numcount = 1, j = 0;
                bool x = false;
                int recCount = 0;
                int totalRec = source.SelectNodes("清單/異動紀錄").Count;
            
            #endregion

            foreach (XmlNode list in source.SelectNodes("清單"))
            {
                int i = 0;

                #region 找出資料總數及判斷

                //找出資料總數方便評估進度
                int count = list.SelectNodes("異動紀錄").Count;

                //判斷個數是否為20被數
                if (count % 20 == 0)
                {
                    x = true;
                } 

                #endregion

                #region 異動紀錄

                //將xml資料填入至excel
                foreach (XmlNode st in list.SelectNodes("異動紀錄"))
                {
                    recCount++;

                    if (i % 20 == 0)
                    {
                        #region 複製樣式-欄高、範圍

                        //複製樣版中前27個 Row(欄高)
                        //for (int m = 0; m < 27; m++)
                        //{
                        //    /*
                        //     * 複製 template的第一個 Sheet的第m個 Row
                        //     * 到 wb的第一個 Sheet的第(j * 27) + m個 Row
                        //     */
                        //    wb.Worksheets[0].Cells.CopyRow(template.Worksheets[0].Cells, m, (j * 27) + m);
                        //}

                        /*
                         * 複製Style(包含儲存格合併的資訊)
                         * 先用CreateRange()選取要複製的Range("A1", "R27")
                         * 再用CopyStyle複製另一個Range中的格式
                         */
                        Range range = template.Worksheets[0].Cells.CreateRange(0, 27, false);
                        int t = j * 27;
                        wb.Worksheets[0].Cells.CreateRange(t, 27, false).Copy(range);

                        #endregion

                        #region 填入學校資料

                        //將學校資料填入適當的位置內
                        wb.Worksheets[0].Cells[rowi, 13].PutValue(source.SelectSingleNode("@學校代號").InnerText);
                        wb.Worksheets[0].Cells[rowi, 16].PutValue(list.SelectSingleNode("@科別代號").InnerText);
                        wb.Worksheets[0].Cells[rowi + 2, 2].PutValue(source.SelectSingleNode("@學校名稱").InnerText);
                        wb.Worksheets[0].Cells[rowi + 2, 6].PutValue(Convert.ToInt32(source.SelectSingleNode("@學年度").InnerText));
                        wb.Worksheets[0].Cells[rowi + 2, 9].PutValue(Convert.ToInt32(source.SelectSingleNode("@學期").InnerText));
                        wb.Worksheets[0].Cells[rowi + 2, 12].PutValue(list.SelectSingleNode("@科別").InnerText);

                        #endregion

                        if (j > 0)
                        {
                            //插入分頁(在 j * 27 跟 (j * 27) +1 中間，R跟S中間)
                            wb.Worksheets[0].HPageBreaks.Add(j * 27, 18);
                            rowj += 7;
                        }
                        else
                        {
                            rowj = 5;
                        }

                        rowi += 27;
                        j++;

                        #region 顯示頁數

                        //顯示頁數
                        if (x != true)
                        {
                            wb.Worksheets[0].Cells[(27 * (j - 1)) + 26, 13].PutValue("第" + numcount + "頁，共" + Math.Ceiling((double)count / 20) + "頁");
                        }
                        else
                        {
                            wb.Worksheets[0].Cells[(27 * (j - 1)) + 26, 13].PutValue("第" + numcount + "頁，共" + (Math.Ceiling((double)count / 20) + 1) + "頁");
                        }
                        numcount++;

                        #endregion
                    }

                    #region 填入學生資料
                    
                        //將學生資料填入適當的位置內
                        wb.Worksheets[0].Cells[rowj, 1].PutValue(st.SelectSingleNode("@學號").InnerText);
                        wb.Worksheets[0].Cells[rowj, 3].PutValue(st.SelectSingleNode("@姓名").InnerText);
                        wb.Worksheets[0].Cells[rowj, 5].PutValue(st.SelectSingleNode("@身分證號").InnerText);
                        
                        try
                        {
                            wb.Worksheets[0].Cells[rowj, 6].PutValue(Convert.ToInt32(st.SelectSingleNode("@性別代號").InnerText));
                        }
                        catch (Exception)
                        {}
                        wb.Worksheets[0].Cells[rowj, 7].PutValue(st.SelectSingleNode("@性別").InnerText);
                        wb.Worksheets[0].Cells[rowj, 8].PutValue(st.SelectSingleNode("@出生年月日").InnerText);
                        wb.Worksheets[0].Cells[rowj, 11].PutValue(st.SelectSingleNode("@畢業國中所在縣市代號").InnerText + "\n" + st.SelectSingleNode("@入學資格代號").InnerText);
                        wb.Worksheets[0].Cells[rowj, 12].PutValue(st.SelectSingleNode("@畢業國中").InnerText);
                        wb.Worksheets[0].Cells[rowj, 14].PutValue(st.SelectSingleNode("@備註").InnerText);
                    
                    #endregion

                    i++;
                    rowj++;

                    //回報進度
                    ReportProgress((int)(((double)recCount * 100.0) / ((double)totalRec)));
                }

                #endregion

                #region 若個數為20倍數，處理單一頁面

                if (x == true)
                {

                    #region 複製樣式-欄高、範圍

                    //複製樣版前27個 Row(欄高)
                    //for (int m = 0; m < 27; m++)
                    //{
                    //    /*
                    //     * 複製 template的第一個 Sheet的第m個 Row
                    //     * 到 wb的第一個 Sheet的第(j * 27) + m個 Row
                    //     */
                    //    wb.Worksheets[0].Cells.CopyRow(template.Worksheets[0].Cells, m, (j * 27) + m);
                    //}

                    /*
                     * 複製Style(包含儲存格合併的資訊)
                     * 先用CreateRange()選取要複製的Range("A1", "R27")
                     * 再用CopyStyle複製另一個Range中的格式
                     */
                    Range range = template.Worksheets[0].Cells.CreateRange(0, 27, false);
                    int t= j * 27;
                    wb.Worksheets[0].Cells.CreateRange(t, 27, false).Copy(range);

                    #endregion

                    #region 填入學校資料

                    //將學校資料填入適當的位置內
                    wb.Worksheets[0].Cells[rowi, 13].PutValue(source.SelectSingleNode("@學校代號").InnerText);
                    wb.Worksheets[0].Cells[rowi, 16].PutValue(list.SelectSingleNode("@科別代號").InnerText);
                    wb.Worksheets[0].Cells[rowi + 2, 2].PutValue(source.SelectSingleNode("@學校名稱").InnerText);
                    wb.Worksheets[0].Cells[rowi + 2, 6].PutValue(Convert.ToInt32(source.SelectSingleNode("@學年度").InnerText));
                    wb.Worksheets[0].Cells[rowi + 2, 9].PutValue(Convert.ToInt32(source.SelectSingleNode("@學期").InnerText));
                    wb.Worksheets[0].Cells[rowi + 2, 12].PutValue(list.SelectSingleNode("@科別").InnerText);

                    #endregion

                    if (j > 0)
                    {
                        //插入分頁(在i跟i+1中間，O跟P中間)
                        wb.Worksheets[0].HPageBreaks.Add(j * 27, 18);
                        rowj += 7;
                    }

                    rowi += 27;
                    j++;

                    #region 顯示頁數

                    //顯示頁數
                    wb.Worksheets[0].Cells[(27 * (j - 1)) + 26, 13].PutValue("第" + numcount + "頁，共" + (Math.Ceiling((double)count / 20) + 1) + "頁");
                    numcount++;

                    #endregion
                } 

                #endregion

                #region 統計人數

                //填入統計人數
                wb.Worksheets[0].Cells[rowj, 1].PutValue("合  計 ");
                wb.Worksheets[0].Cells[rowj, 3].PutValue(count.ToString() + " 名"); 

                #endregion

                wb.Worksheets[0].HPageBreaks.Add(j * 27, 18);

                #region 設定變數

                //調整新清單所使用變數
                numcount = 1;
                rowj = (27 * j) - 2;
                rowi = (27 * j);
                x = false; 

                #endregion
            }

            #region 97中辦格式

            Worksheet mingdao = wb.Worksheets["新生名冊97中辦範本"];
            Worksheet mdws = wb.Worksheets[wb.Worksheets.Add()];
            mdws.Name = "電子格式";

            Range range_header = mingdao.Cells.CreateRange(0, 1, false);
            Range range_row = mingdao.Cells.CreateRange(1, 1, false);

            mdws.Cells.CreateRange(0, 1, false).Copy(range_header);

            int mdws_index = 0;
            foreach (XmlElement record in source.SelectNodes("清單/異動紀錄"))
            {
                mdws_index++;
                mdws.Cells.CreateRange(mdws_index, 1, false).Copy(range_row);

                mdws.Cells[mdws_index, 0].PutValue("");
                mdws.Cells[mdws_index, 1].PutValue((record.ParentNode as XmlElement).GetAttribute("科別代號"));
                mdws.Cells[mdws_index, 2].PutValue(record.GetAttribute("學號"));
                mdws.Cells[mdws_index, 3].PutValue(record.GetAttribute("姓名"));
                mdws.Cells[mdws_index, 4].PutValue(record.GetAttribute("身分證號"));
                mdws.Cells[mdws_index, 5].PutValue("");
                mdws.Cells[mdws_index, 6].PutValue(record.GetAttribute("性別代號"));
                mdws.Cells[mdws_index, 7].PutValue(GetBirthdateWithoutSlash(record.GetAttribute("出生年月日")));
                mdws.Cells[mdws_index, 8].PutValue(record.GetAttribute("備註"));
                mdws.Cells[mdws_index, 9].PutValue(record.GetAttribute("入學資格代號"));
                mdws.Cells[mdws_index, 10].PutValue(record.GetAttribute("畢業國中"));
                mdws.Cells[mdws_index, 11].PutValue("");
                mdws.Cells[mdws_index, 12].PutValue("");
                mdws.Cells[mdws_index, 13].PutValue("");
            }

            mdws.AutoFitColumns();
            mdws.Cells.SetColumnWidth(5, 8.5);
            mdws.Cells.SetColumnWidth(11, 20);

            wb.Worksheets.RemoveAt("新生名冊97中辦範本");
            wb.Worksheets.ActiveSheetIndex = 0;

            #endregion

            wb.Worksheets.ActiveSheetIndex = 0;

            //儲存 Excel
            wb.Save(location, FileFormatType.Excel2003);
        }

        private string GetBirthdateWithoutSlash(string orig)
        {
            if (string.IsNullOrEmpty(orig)) return orig;
            string[] array = orig.Split('/');
            return array[0] + array[1].PadLeft(2, '0') + array[2].PadLeft(2, '0');
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
            get { return "新生名冊"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}
