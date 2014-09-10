using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.Feature.Basic;
using SmartSchool.ExceptionHandler;

namespace SmartSchool
{
    public class SchoolInfo
    {
        private enum InfoState
        {
            Success,
            Undefine,
            Exception
        }

        private DSXmlHelper _source;
        private InfoState _info_state;
        private const string RootName = "SchoolInformation";

        /// <summary>
        /// 從 Server 讀取資訊(必須已登入)。
        /// </summary>
        public void Load()
        {
            try
            {
                _source = new DSXmlHelper(Config.GetSchoolInfo());
                _info_state = InfoState.Success;

                if (!_source.PathExist(RootName))
                    _info_state = InfoState.Undefine;
            }
            catch (Exception ex)
            {
                _info_state = InfoState.Exception;

                try
                {
                    CurrentUser user = CurrentUser.Instance;
                    BugReporter.ReportException(user.SystemName, user.SystemVersion, ex, false);
                }
                catch { }
            }
        }

        /// <summary>
        /// 儲存資訊到 Server(必須已登入)。
        /// </summary>
        public void Save()
        {
            RemoveGarbage();

            Config.SetSchoolInfo(Source.BaseElement);
        }

        private void RemoveGarbage()
        {
            XmlElement schInfo = Source.GetElement("SchoolInformation");

            DSXmlHelper schoolinfo = new DSXmlHelper("SetSchoolInformationRequest");
            schoolinfo.AddElement(".", schInfo);

            Source = schoolinfo;

            //if (Source.PathExist("SQL"))
            //    Source.RemoveElement("SQL");

            //if (Source.PathExist("SpendTime"))
            //    Source.RemoveElement("SpendTime");

            //if (Source.PathExist("ParseSQLTime"))
            //    Source.RemoveElement("ParseSQLTime");
        }

        private DSXmlHelper Source
        {
            get { return _source; }
            set { _source = value; }
        }

        /// <summary>
        /// 取得或設定學校中文名稱。
        /// </summary>
        public string ChineseName
        {
            get { return Preprocess("ChineseName"); }
            set { SetInfo("ChineseName", value); }
        }

        /// <summary>
        /// 取得或設定學校英文名稱。
        /// </summary>
        public string EnglishName
        {
            get { return Preprocess("EnglishName"); }
            set { SetInfo("EnglishName", value); }
        }

        /// <summary>
        /// 取得或設定學校代碼。
        /// </summary>
        public string Code
        {
            get { return Preprocess("Code"); }
            set { SetInfo("Code", value); }
        }

        /// <summary>
        /// 取得或設定學校電話。
        /// </summary>
        public string Telephone
        {
            get { return Preprocess("Telephone"); }
            set { SetInfo("Telephone", value); }
        }

        /// <summary>
        /// 取得或設定學校傳真。
        /// </summary>
        public string Fax
        {
            get { return Preprocess("Fax"); }
            set { SetInfo("Fax", value); }
        }

        /// <summary>
        /// 取得或設定學校地址。
        /// </summary>
        public string Address
        {
            get { return Preprocess("Address"); }
            set { SetInfo("Address", value); }
        }

        /// <summary>
        /// 取得相關資訊是否準備就緒。
        /// </summary>
        public bool Success
        {
            get { return _info_state == InfoState.Success; }
        }

        private string GetFullPath(string shortName)
        {
            return RootName + "/" + shortName;
        }

        private string Preprocess(string infoName)
        {
            if (_info_state == InfoState.Success)
                return Source.GetText(GetFullPath(infoName));
            else if (_info_state == InfoState.Undefine)
                return "<未定議>";
            else if (_info_state == InfoState.Exception)
                return "<讀取資訊錯誤>";
            else
                throw new Exception("沒有此種狀態");
        }

        private void SetInfo(string infoName, string value)
        {
            if (!Success)
                CreateDocument();

            if (Source.PathExist(GetFullPath(infoName)))
                Source.GetElement(GetFullPath(infoName)).InnerText = value;
            else
            {
                if (!Source.PathExist(RootName))
                    Source.AddElement(RootName);

                Source.AddElement(RootName, infoName, value);
            }

            _info_state = InfoState.Success;
        }

        private void SetInfo(string infoName, int value)
        {
            SetInfo(infoName, value.ToString());
        }

        private void CreateDocument()
        {
            _source = new DSXmlHelper("Request");
        }
    }
}
