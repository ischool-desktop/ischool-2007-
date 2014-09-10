using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.IO;
using System.Reflection;

namespace SmartSchool.Payment.AccountStatedService
{
    /// <summary>
    /// 對帳單服務。
    /// </summary>
    public partial class ASService : ServiceBase
    {
        private Timer timer;

        public ASService()
        {
            InitializeComponent();
            ServiceName = ServiceProgram.ServiceName;
        }

        protected override void OnStart(string[] args)
        {
            //string basePath = Application.StartupPath;
            //FileInfo file = new FileInfo(Path.Combine(basePath, "ASServiceConfigs.xml"));

            //if (!file.Exists) return;

            //DSXmlHelper configs = new DSXmlHelper(DSXmlHelper.LoadXml(file));

            //lstConfigs.Items.Clear();
            //foreach (XmlElement each in configs.GetElements("ASServiceConfig"))
            //{
            //    ASServiceConfig config = new ASServiceConfig(each);
            //    ListViewItem item = new ListViewItem();
            //    item.Text = config.Name;
            //    item.Tag = config;

            //    lstConfigs.Items.Add(item);
            //}

             //TODO: 在此加入啟動服務的程式碼。
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Interval = 1000;
            timer.Start();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            FileStream stream = new FileStream("C:/bomb.txt", FileMode.Append);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(Assembly.GetExecutingAssembly().Location);
            writer.Close();
        }

        protected override void OnStop()
        {
            // TODO: 在此加入停止服務所需執行的終止程式碼。
        }
    }
}
