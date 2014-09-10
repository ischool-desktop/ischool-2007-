using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Payment.AccountStatedService.Interfaces;
using System.Diagnostics;
using System.IO;

namespace SmartSchool.Payment.AccountStatedService
{
    class ASServiceRunner
    {
        public ASServiceRunner(string logFolder)
        {
            _configs = new List<ASServiceConfig>();
            _time_stamp = DateTime.Now;
            _log_folder = logFolder;
        }

        private DateTime _time_stamp;
        private DateTime Timestamp
        {
            get { return _time_stamp; }
        }

        private string GetTimestampString()
        {
            return Timestamp.ToString("yyyy-MM-dd-HH-mm-ss");
        }

        private string _log_folder;
        public string LogFolder
        {
            get { return _log_folder; }
        }

        private List<ASServiceConfig> _configs;
        public List<ASServiceConfig> Configs
        {
            get { return _configs; }
        }

        public void Add(ASServiceConfig config)
        {
            Configs.Add(config);
        }

        public void Run()
        {
            _time_stamp = DateTime.Now;

            string path = Path.Combine(LogFolder, GetTimestampString());
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists) dir.Create();

            LogWriter log = new LogWriter();
            foreach (ASServiceConfig each in Configs)
            {
                IAccountStatedService service = ASServiceProvider.GetService(each.ModuleCode);
                ITransactionBridge bridge = service.CreateBridge(each);

                string workingFolder = Path.Combine(path, string.Format("{0}-{1}", GetTimestampString(), each.Name));
                if (!Directory.Exists(workingFolder))
                    Directory.CreateDirectory(workingFolder);

                log.Write(string.Format("準備開始傳送交易資料({0})...", each.Name));
                try
                {
                    if (bridge.Transport(log.CreateMessageGroup(service.Name), workingFolder))
                        log.Write("傳送交易資料成功。");
                    else
                        log.Write("傳送交易資料失敗。");
                }
                catch (Exception ex)
                {
                    log.Write("傳送交易發生錯誤：" + ex.Message);
                    log.Write(string.Format("傳送交易中斷({0})。", each.Name));
                    //TODO 傳出去...
                }
            }

            log.GetXmlDocument().Save(Path.Combine(path,
                string.Format("{0}-{1}", GetTimestampString(), "log.xml")));
        }
    }
}
