using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Payment.AccountStatedService.Interfaces;
using SmartSchool.Payment.AccountStatedService;
using System.Threading;
using System.Xml;
using IntelliSchool.DSA30.Util;
using System.Net;
using System.IO;

namespace SmartSchool.Payment.AccountStatedModules
{
    class CTTransactionBridge : ITransactionBridge
    {
        private ASServiceConfig _config;

        public CTTransactionBridge(ASServiceConfig config)
        {
            _config = config;
        }

        #region ITransactionBridge 成員

        public bool Transport(LogWriter log, string workingFolder)
        {
            DSXmlHelper content = new DSXmlHelper(_config.Content);

            string logFolder = workingFolder;
            string ftpUrl = content.GetText("FTPUrl");
            string ftpAccount = content.GetText("FTPAccount");
            string ftpPassword = content.GetText("FTPPassword");
            string enterpriseCode = content.GetText("EnterpriseCode");
            Dictionary<string, SchoolBridge> schools = new Dictionary<string, SchoolBridge>();

            //讀取學校的相關設定。
            if (!ReadSchools(log, content, schools))
                return false;

            FTPConnection ftpconn = new FTPConnection(ftpUrl, ftpAccount, ftpPassword);

            log.Write(string.Format("準備連線到銀行 FTP：{0}", ftpUrl));
            log.Write("下載交易記錄清單 …");
            LogDailyCollection dailies = LogDailyCollection.GetFTPDailies(ftpconn, enterpriseCode.Split(','));

            if (dailies.Count <= 0)
                log.Write("目前沒有任何交易資料。");
            else
            {
                #region 解析並傳送交易資料
                log.Write(string.Format("解析交易資料 …"));
                DSXmlHelper request = new DSXmlHelper("Request");
                foreach (LogDaily each in dailies)
                {
                    foreach (LogEntry eachEntry in each.Entries)
                    {
                        DSXmlHelper transaction;
                        SchoolBridge school = null;

                        if (schools.ContainsKey(eachEntry.GetSchoolCode()))
                            school = schools[eachEntry.GetSchoolCode()];
                        else
                        {
                            throw new ArgumentException("學校對帳設定有問題，找不到交易記錄的所屬學校。(VirtualAccount："+eachEntry.VirtualAccount+")");
                        }

                        transaction = new DSXmlHelper(school.NewShip());
                        transaction.AddElement(".", "VirtualAccount", eachEntry.VirtualAccount);
                        transaction.AddElement(".", "Fee", eachEntry.Fee.ToString());
                        transaction.AddElement(".", "ChannelCode", eachEntry.ChannelCode);
                        transaction.AddElement(".", "ChannelCharge", eachEntry.ChannelCharge.ToString());
                        transaction.AddElement(".", "PayDate", eachEntry.PayDate.ToString("yyyy/MM/dd"));
                        transaction.AddElement(".", "FullDetail", eachEntry.SerialToXml().OuterXml, true);
                        transaction.AddElement(".", "Comment", each.FileName);
                    }
                }

                foreach (SchoolBridge each in schools.Values)
                {
                    log.Write(string.Format("傳送交易資料到「{0}」主機 …", each.AccessPoint));
                    each.ShipTransactions();
                    //log.Write(string.Format("目前暫時不傳…", each.AccessPoint));
                    log.Write(string.Format("傳送完成。", each.AccessPoint));

                    log.Write(string.Format("儲存原始資料，路徑：{0}", logFolder));
                }

                foreach (LogDaily each in dailies)
                    File.WriteAllText(Path.Combine(logFolder, each.FileName), each.RawContent);

                LogDailyCollection.DeleteFTPDailies(ftpconn, dailies);
                #endregion
            }

            return true;
        }

        private static bool ReadSchools(LogWriter log, DSXmlHelper content, Dictionary<string, SchoolBridge> schools)
        {
            foreach (XmlElement each in content.GetElements("School"))
            {
                string lic = each.GetAttribute("LicenseFile");
                string pin = each.GetAttribute("PinCode");
                string scode = each.GetAttribute("SchoolCode");
                string enabled = each.GetAttribute("Enabled");

                SchoolBridge school = new SchoolBridge(scode, enabled);

                try
                {
                    log.Write(string.Format("讀取授權資訊，學校代碼：{0}。", school.SchoolCode));
                    school.SetLicense(lic, pin);

                    log.Write(string.Format("準備連線到「{0}」主機。", school.AccessPoint));
                    school.OpenBridge();
                    schools.Add(school.SchoolCode, school);
                }
                catch (Exception ex)
                {
                    log.Write(string.Format("錯誤，訊息：{0}", ex.Message));
                    log.Write(string.Format("錯誤，學校代碼：{0}", scode));

                    if (ex.InnerException != null)
                        log.Write(ex.InnerException.Message);

                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
