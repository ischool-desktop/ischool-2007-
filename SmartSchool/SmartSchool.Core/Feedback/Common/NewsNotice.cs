using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using SmartSchool.Feedback.Feature;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.Common;

namespace SmartSchool.Feedback
{
    internal class NewsNotice
    {
        private BackgroundWorker _newsLoader;
        private DateTime _serverTime = DateTime.MinValue;
        private DateTime _lastViewTime = DateTime.MinValue;

        public NewsNotice()
        {
            _newsLoader = new BackgroundWorker();
            _newsLoader.DoWork += new DoWorkEventHandler(_newsLoader_DoWork);
            _newsLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_newsLoader_RunWorkerCompleted);
            _newsLoader.RunWorkerAsync();
        }

        private void _newsLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (DSXmlHelper each in e.Result as List<DSXmlHelper>)
            {
                DateTime time = DateTime.Parse(each.GetText("PostTime"));

                //MsgBox.Show(each.GetText("Message"), string.Format("張貼時間：{0} {1}", time.ToShortDateString(), time.ToShortTimeString()));

                StringBuilder msg = new StringBuilder("");
                msg.AppendLine(each.GetText("Message"));
                msg.AppendLine("");
                msg.AppendLine(string.Format("張貼時間：{0} {1}", time.ToShortDateString(), time.ToShortTimeString()));
                MsgBox.Show(msg.ToString(), "最新消息通知");
            }

            SavePreference();
        }

        private void _newsLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(5000);

            LoadPreference();
            GetServerDateTime();


            List<DSXmlHelper> newsList = new List<DSXmlHelper>();

            DSXmlHelper helper;
            try
            {
                helper = Service.GetNewsForUsers();
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                helper = new DSXmlHelper("BOOM");
            }

            foreach (XmlElement news in helper.GetElements("News"))
            {
                DSXmlHelper newsHelper = new DSXmlHelper(news);
                if (IsIncorrect(newsHelper.GetText("To"))) continue;

                if (DateTime.Parse(newsHelper.GetText("PostTime")) > _lastViewTime)
                    newsList.Add(newsHelper);
            }

            e.Result = newsList;

        }

        private bool IsIncorrect(string user)
        {
            CurrentUser current = CurrentUser.Instance;
            if (user == "*/*") return false;
            if (user == current.AccessPoint + "/*") return false;
            if (user == current.AccessPoint + "/" + current.UserName) return false;
            return true;
        }

        private void GetServerDateTime()
        {
            try
            {
                DSXmlHelper helper = Service.GetDateTimeNow();
                DateTime a;
                if (!DateTime.TryParse(helper.GetText("DateTime"), out a))
                    _serverTime = DateTime.Now;
                _serverTime = a;
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(ex);
                _serverTime = DateTime.Now;
            }
        }

        private void LoadPreference()
        {
            XmlElement p = CurrentUser.Instance.Preference["News"];
            if (p != null)
            {
                DateTime a;
                if (DateTime.TryParse(p.GetAttribute("LastViewTime"), out a))
                    _lastViewTime = a;
                //_lastViewTime = DateTime.MinValue;
            }
        }

        private void SavePreference()
        {
            XmlElement p = CurrentUser.Instance.Preference["News"];
            if (p == null)
            {
                p = new XmlDocument().CreateElement("News");
            }
            p.SetAttribute("LastViewTime", "" + _serverTime.ToString("yyyy/MM/dd HH:mm:ss"));
            CurrentUser.Instance.Preference["News"] = p;
        }
    }
}
