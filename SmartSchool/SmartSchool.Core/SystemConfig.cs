using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Feature.Basic;
using IntelliSchool.DSA30.Util;
using SmartSchool.ExceptionHandler;

namespace SmartSchool
{
    internal class SystemConfig
    {
        private enum InfoState
        {
            Success,
            Undefine,
            Exception
        }

        private DSXmlHelper _source;
        private InfoState _info_state;
        private const string RootName = "SystemConfig";

        /// <summary>
        /// 從 Server 讀取資訊(必須已登入)。
        /// </summary>
        public void Load()
        {
            try
            {
                _source = new DSXmlHelper(Config.GetSystemConfig());
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

            Config.SetSystemConfig(Source.BaseElement);
        }

        private void RemoveGarbage()
        {
            if (Source.PathExist("SQL"))
                Source.RemoveElement("SQL");

            if (Source.PathExist("SpendTime"))
                Source.RemoveElement("SpendTime");

            if (Source.PathExist("ParseSQLTime"))
                Source.RemoveElement("ParseSQLTime");
        }

        private DSXmlHelper Source
        {
            get { return _source; }
        }

        /// <summary>
        /// 取得預設學年度。
        /// </summary>
        public int DefaultSchoolYear
        {
            get
            {
                return PreprocessInt("DefaultSchoolYear", 96);
            }
            set
            {
                SetInfo("DefaultSchoolYear", value);
            }
        }

        /// <summary>
        /// 取得預設學期。
        /// </summary>
        public int DefaultSemester
        {
            get
            {
                return PreprocessInt("DefaultSemester", 1);
            }
            set
            {
                SetInfo("DefaultSemester", value);
            }
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

        private int PreprocessInt(string infoName, int defaultValue)
        {
            string val = Preprocess(infoName);

            if (string.IsNullOrEmpty(val))
                return defaultValue;
            else
            {
                int result;
                if (int.TryParse(val, out result))
                    return result;
                else
                    return defaultValue;
            }
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
