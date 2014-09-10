using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;
using SmartSchool.StudentRelated.Placing.Rule;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Diagnostics;
using SmartSchool.Common;

namespace SmartSchool.StudentRelated.Placing.Export
{
    public class ExcelExporter : IExporter
    {
        private IList<ExcelSheetInfo> _infoList;
        private Workbook _book;

        public ExcelExporter()
        {
            _infoList = new List<ExcelSheetInfo>();
            _book = new Workbook();
        }

        public void Add(ExcelSheetInfo info)
        {
            _infoList.Add(info);
        }

        #region IExporter 成員

        public void Export()
        {
            _book.Worksheets.Clear();
            int sindex = 0;
            foreach (ExcelSheetInfo info in _infoList)
            {
                _book.Worksheets.Add();
                Worksheet sheet = _book.Worksheets[sindex];
                IExportSheetHandler handler = ExportSheetHandlerFactory.CreateInstance(info.SheetType);
                handler.Export(sheet, info);
                sindex++;
            }
        }

        #endregion

        public void Save()
        {
            string path = Path.Combine(Application.StartupPath, "Reports");

            //如果目錄不存在則建立。
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            path = Path.Combine(path, "即時成績排名.xls");
            try
            {
                _book.Save(path);
            }
            catch (IOException)
            {
                try
                {
                    FileInfo file = new FileInfo(path);
                    string nameTempalte = file.FullName.Replace(file.Extension, "") + "{0}.xls";
                    int count = 1;
                    string fileName = string.Format(nameTempalte, count);
                    while (File.Exists(fileName))
                        fileName = string.Format(nameTempalte, count++);

                    _book.Save(fileName, FileFormatType.Excel2000);
                    path = fileName;
                }
                catch (Exception ex)
                {
                    MsgBox.Show("檔案儲存失敗:" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show("檔案儲存失敗:" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                System.Diagnostics .Process.Start(path);
            }
            catch (Exception ex)
            {
                MsgBox.Show("檔案開啟失敗:" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private bool FileExist(string filename)
        {
            FileInfo file = new FileInfo(filename);
            return file.Exists;
        }

        private string GetNewFileName(string filename)
        {
            FileInfo file = new FileInfo(filename);
            if (!file.Exists)
                return filename;

            string ext = file.Extension;
            int li = file.FullName.LastIndexOf('(');
            string name; // = file.FullName.Substring
            if (li == -1)
            {
                int leng = ext.Length + 1;
                name = file.FullName.Substring(0, file.FullName.Length - leng + 1);
                name += "(1)." + ext;
            }
            else
            {
                name = file.FullName.Substring(0, li);
                for (int i = 1; i < int.MaxValue; i++)
                {
                    string newFileName = name + "(" + i + ")." + ext;
                    if (!File.Exists(newFileName))
                    {
                        name = newFileName;
                        break;
                    }
                }
            }
            return name;
        }
    }
}
