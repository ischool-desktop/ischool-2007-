using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SmartSchool.Payment.AccountStatedModules
{
    class LogDaily
    {
        public LogDaily(string fileName, Stream content)
        {
            _file_name = fileName;

            StreamReader reader = new StreamReader(content);

            _raw_content=reader.ReadToEnd();

            content.Seek(0, SeekOrigin.Begin);
            reader = new StreamReader(content);

            List<string> lines = new List<string>();
            while (reader.Peek() > 0)
                lines.Add(reader.ReadLine());

            //暫時不處理第一行與最後一行。
            List<LogEntry> entries = new List<LogEntry>();
            for (int i = 1; i < lines.Count; i++)
            {
                if (lines[i].Substring(0, 1) == "2") //1:表頭 2:交易記錄 3:表尾
                    entries.Add(new LogEntry(lines[i]));
            }
            reader.Close();

            _entries = entries;
        }

        private string _file_name;
        public string FileName
        {
            get { return _file_name; }
        }

        private List<LogEntry> _entries;
        public List<LogEntry> Entries
        {
            get { return _entries; }
        }

        private string _raw_content;
        public string RawContent
        {
            get { return _raw_content; }
        }
    }
}
