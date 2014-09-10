using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using System.ComponentModel;
using SmartSchool.Common;
using System.Xml;

namespace SmartSchool.Feature.Basic
{
    public class Config
    {
        [QueryRequest()]
        public static DSResponse GetNationalityList()
        {
            string serviceName = "Nationality";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("GetNationalityListRequest");
                helper.AddElement("Fields");
                helper.AddElement("Fields", "All");
                request.SetContent(helper);
                DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Config.GetNationalityList", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        public const string LIST_SCHOOL_LOCATION = "GetSchoolLocationList";
        [QueryRequest()]
        public static DSResponse GetSchoolLocationList()
        {
            if (DataCacheManager.Get(LIST_SCHOOL_LOCATION) == null)
            {
                DSXmlHelper helper = new DSXmlHelper("GetSchoolLocationListRequest");
                helper.AddElement("Field");
                helper.AddElement("Field", "All");
                DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Config.GetSchoolLocationList", new DSRequest(helper));
                DataCacheManager.Add(LIST_SCHOOL_LOCATION, dsrsp);
            }
            return DataCacheManager.Get(LIST_SCHOOL_LOCATION);
        }

        [QueryRequest()]
        public static List<string> GetCountyList()
        {
            string serviceName = "CountyTownBelong";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("Request");
                helper.AddElement("Fields");
                helper.AddElement("Fields", "All");
                request.SetContent(helper);
                DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Config.GetCountyTownList", request);
                DataCacheManager.Add(serviceName, dsrsp);

                //BackgroundWorker bgw = new BackgroundWorker();
                //bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);                
                //bgw.RunWorkerAsync(dsrsp.GetContent());
            }
            DSResponse rsp = DataCacheManager.Get(serviceName);
            List<string> countyList = new List<string>();
            foreach (XmlNode node in rsp.GetContent().GetElements("Town"))
            {
                string county = node.Attributes["County"].Value;
                if (!countyList.Contains(county))
                    countyList.Add(county);
            }
            return countyList;
        }

        [QueryRequest()]
        public static DSResponse GetUpdateCodeSynopsis()
        {
            string serviceName = "SmartSchool.Config.GetUpdateCodeSynopsis";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("GetCountyListRequest");
                helper.AddElement("Field");
                helper.AddElement("Field", "異動代號對照表");
                request.SetContent(helper);
                DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Config.GetUpdateCodeSynopsis", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        [QueryRequest()]
        public static XmlElement[] GetTownList(string county)
        {
            DSResponse dsrsp = DataCacheManager.Get("CountyTownBelong");

            if (dsrsp == null)
                return new XmlElement[0];
            else
                return dsrsp.GetContent().GetElements("Town[@County='" + county + "']");
        }

        [QueryRequest()]
        public static DSResponse GetRelationship()
        {
            string serviceName = "Relationship";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("GetRelationshipListRequest");
                helper.AddElement("Fields");
                helper.AddElement("Fields", "All");
                request.SetContent(helper);
                DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Config.GetRelationshipList", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        [QueryRequest()]
        public static DSResponse GetJobList()
        {
            string serviceName = "JobList";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("GetJobListRequest");
                helper.AddElement("Fields");
                helper.AddElement("Fields", "All");
                request.SetContent(helper);
                DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Config.GetJobList", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        [QueryRequest()]
        public static DSResponse GetEduDegreeList()
        {
            string serviceName = "EduDegreeList";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("GetEducationDegreeListRequest");
                helper.AddElement("Fields");
                helper.AddElement("Fields", "All");
                request.SetContent(helper);
                DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Config.GetEducationDegreeList", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        public static DSResponse SendSMS(string number, string message)
        {
            DSRequest request = new DSRequest();
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement(".", "Message", message);
            helper.AddElement(".", "Mobile", number);
            request.SetContent(helper);
            return CurrentUser.Instance.CallService("SmartSchool.Config.SendMessage", request);
        }

        [QueryRequest()]
        public static KeyValuePair<string, string> FindTownByZipCode(string zipCode)
        {
            DSResponse dsrsp = DataCacheManager.Get("CountyTownBelong");
            XmlNode node = dsrsp.GetContent().GetElement("Town[@Code='" + zipCode + "']");
            KeyValuePair<string, string> kvp = new KeyValuePair<string, string>();
            if (node != null)
            {
                kvp = new KeyValuePair<string, string>(node.Attributes["County"].Value, node.Attributes["Name"].Value);
            }
            return kvp;
        }

        [QueryRequest()]
        public static DSResponse GetUpdateCodeList()
        {
            string serviceName = "UpdateCode";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSXmlHelper helper = new DSXmlHelper("GetUpdateCodeResponse");
                DataCacheManager.Add(serviceName, new DSResponse(helper));
            }
            return DataCacheManager.Get(serviceName);
        }

        [QueryRequest()]
        public static DSResponse GetUpdateTypeList(string p)
        {
            string serviceName = "UpdateType";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSXmlHelper helper = new DSXmlHelper("GetUpdateTypeResponse");
                DataCacheManager.Add(serviceName, new DSResponse(helper));
            }
            return DataCacheManager.Get(serviceName);
        }

        [QueryRequest()]
        public static DSResponse GetDepartment()
        {
            string serviceName = "GetDepartment";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("GetDepartmentListRequest");
                helper.AddElement("Field");
                helper.AddElement("Field", "All");
                request.SetContent(helper);
                DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Config.GetDepartment", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        public const string LIST_ABSENCE = "GetAbsenceList";
        public const string LIST_ABSENCE_NUMBER = "36";
        [QueryRequest()]
        public static DSResponse GetAbsenceList()
        {
            string serviceName = "GetAbsenceList";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("GetAbsenceListRequest");
                helper.AddElement("Field");
                helper.AddElement("Field", "All");
                request.SetContent(helper);
                DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Others.GetAbsenceList", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        public const string LIST_PERIODS = "GetPeriodList";
        public const string LIST_PERIODS_NUMBER = "35";
        [QueryRequest()]
        public static DSResponse GetPeriodList()
        {
            string serviceName = LIST_PERIODS;
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("GetPeriodListRequest");
                helper.AddElement("Field");
                helper.AddElement("Field", "All");
                request.SetContent(helper);
                DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Others.GetPeriodList", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        [QueryRequest()]
        public static DSResponse GetDisciplineReasonList()
        {
            string serviceName = "GetDisciplineReasonList";

            // 拿掉快取
            //if (DataCacheManager.Get(serviceName) == null)
            //{

            DSRequest request = new DSRequest();
            DSXmlHelper helper = new DSXmlHelper("GetDisciplineReasonListRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            request.SetContent(helper);
            DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Config.GetDisciplineReasonList", request);
            return dsrsp;

            //DataCacheManager.Add(serviceName, dsrsp);
            //}
            //return DataCacheManager.Get(serviceName);
        }

        [QueryRequest()]
        public static void SetDisciplineReasonList(XmlElement content)
        {
            DSXmlHelper helper = new DSXmlHelper("SetDisciplineReasonRequest");
            helper.AddElement(".", content);
            CurrentUser.Instance.CallService("SmartSchool.Config.SetDisciplineReasonList", new DSRequest(helper));
        }

        [QueryRequest()]
        public static DSResponse GetScoreEntryList()
        {
            string serviceName = "GetScoreEntryList";
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper("GetScoreEntryList");
                helper.AddElement("Field");
                helper.AddElement("Field", "All");
                request.SetContent(helper);
                DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Config.GetScoreEntryList", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        [QueryRequest()]
        public static XmlElement GetSchoolInfo()
        {
            string serviceName = "SmartSchool.Config.GetSchoolInfo";
            DSXmlHelper helper = new DSXmlHelper("GetSchoolInfoRequest");
            helper.AddElement("All");
            DSResponse dsrsp = CurrentUser.Instance.CallService(serviceName, new DSRequest(helper));
            return dsrsp.GetContent().BaseElement;
        }

        [QueryRequest()]
        public static void SetSchoolInfo(XmlElement schoolInfo)
        {
            string serviceName = "SmartSchool.Config.SetSchoolInfo";
            CurrentUser user = CurrentUser.Instance;
            user.CallService(serviceName, new DSRequest(schoolInfo));
        }

        [QueryRequest()]
        public static XmlElement GetSystemConfig()
        {
            string serviceName = "SmartSchool.Config.GetSystemConfig";
            DSXmlHelper helper = new DSXmlHelper("GetSystemConfigRequest");
            helper.AddElement("All");
            DSResponse dsrsp = CurrentUser.Instance.CallService(serviceName, new DSRequest(helper));
            return dsrsp.GetContent().BaseElement;
        }

        [QueryRequest()]
        public static void SetSystemConfig(XmlElement config)
        {
            string serviceName = "SmartSchool.Config.SetSystemConfig";
            CurrentUser user = CurrentUser.Instance;
            user.CallService(serviceName, new DSRequest(config));
        }

        [QueryRequest()]
        public static DSResponse GetMoralCommentCodeList()
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "Content");
            return CurrentUser.Instance.CallService("SmartSchool.Config.GetMoralCommentCodeList", new DSRequest(helper));
        }

        [QueryRequest()]
        public static void SetMoralCommentCodeList(XmlElement content)
        {
            DSXmlHelper helper = new DSXmlHelper("SetMoralCommentCodeRequest");
            helper.AddElement(".", content);
            CurrentUser.Instance.CallService("SmartSchool.Config.SetMoralCommentCodeList", new DSRequest(helper));
        }

        [QueryRequest()]
        public static DSResponse GetWordCommentList()
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "Content");
            return CurrentUser.Instance.CallService("SmartSchool.Config.GetWordCommentList", new DSRequest(helper));
        }

        [QueryRequest()]
        public static void SetWordCommentList(XmlElement content)
        {
            DSXmlHelper helper = new DSXmlHelper("SetWordCommentRequest");
            helper.AddElement(".", content);
            CurrentUser.Instance.CallService("SmartSchool.Config.SetWordCommentList", new DSRequest(helper));
        }

        [QueryRequest()]
        public static DSResponse GetSubjectChineseToEnglishList()
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "Content");
            return CurrentUser.Instance.CallService("SmartSchool.Config.GetSubjectChineseToEnglishList", new DSRequest(helper));
        }

        [QueryRequest()]
        public static void SetSubjectChineseToEnglishList(XmlElement content)
        {
            DSXmlHelper helper = new DSXmlHelper("SetSubjectChineseToEnglishRequest");
            helper.AddElement(".", content);
            CurrentUser.Instance.CallService("SmartSchool.Config.SetSubjectChineseToEnglishList", new DSRequest(helper));
        }

        [QueryRequest()]
        public static void Reset(string serviceName)
        {
            DataCacheManager.Remove(serviceName);
        }

        [QueryRequest()]
        public static void Update(DSRequest request)
        {
            CurrentUser.Instance.CallService("SmartSchool.Config.UpdateList", request);
        }

        public const string LIST_DEGREE = "GetDegreeList";
        public const string LIST_DEGREE_NAME = "德性等第對照表";
        [QueryRequest()]
        public static DSResponse GetDegreeList()
        {
            string serviceName = LIST_DEGREE;
            if (DataCacheManager.Get(serviceName) == null)
            {
                DSRequest request = new DSRequest();
                DSXmlHelper helper = new DSXmlHelper(LIST_DEGREE);
                helper.AddElement("Field");
                helper.AddElement("Field", "All");
                request.SetContent(helper);
                DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Config.GetDegreeList", request);
                DataCacheManager.Add(serviceName, dsrsp);
            }
            return DataCacheManager.Get(serviceName);
        }

        [QueryRequest()]
        public static DSResponse GetMDReduce()
        {
            return CurrentUser.Instance.CallService("SmartSchool.Config.GetMDReduce", new DSRequest());
        }

        [QueryRequest()]
        public static DSResponse GetMoralDiffItemList()
        {
            return CurrentUser.Instance.CallService("SmartSchool.Config.GetMoralDiffItemList", new DSRequest());
        }

        [QueryRequest()]
        public static void SetMoralDiffItemList(params string[] diffItems)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Content");
            foreach (string var in diffItems)
            {

                helper.AddElement("Content", "DiffItem").SetAttribute("Name", var);
            }
            CurrentUser.Instance.CallService("SmartSchool.Config.SetMoralDiffItemList", new DSRequest(helper));
        }

        [QueryRequest()]
        public static XmlElement GetMoralUploadConfig()
        {
            return CurrentUser.Instance.CallService("SmartSchool.Config.GetMoralUploadConfig", new DSRequest()).GetContent().BaseElement;
        }

        [QueryRequest()]
        public static void SetMoralUploadConfig(XmlElement config)
        {
            CurrentUser.Instance.CallService("SmartSchool.Config.SetMoralUploadConfig", new DSRequest(config));
        }

        [QueryRequest()]
        public static decimal GetSupervisedRatingRange()
        {
            DSResponse rsp = CurrentUser.Instance.CallService("SmartSchool.Config.GetSupervisedRatingRange", new DSRequest());
            XmlElement range = rsp.GetContent().GetElement("RatingRange");

            if (range == null || range.InnerText == string.Empty)
                return 32767;
            else
                return decimal.Parse(range.InnerText);
        }

        [QueryRequest()]
        public static DSXmlHelper GetSchoolConfig()
        {
            DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Config.GetSchoolConfig", new DSRequest());
            return dsrsp.GetContent();
        }

        [QueryRequest()]
        public static void SetSchoolConfig(XmlElement config)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement(".", config);
            CurrentUser.Instance.CallService("SmartSchool.Config.SetSchoolConfig", new DSRequest(helper));
        }

        //public static void SetModulePreference(DSXmlHelper request)
        //{
        //    FeatureBase.CallService("SmartSchool.Config.SetModulePreference", new DSRequest(request));
        //}

        //public static DSXmlHelper GetModulePreference()
        //{
        //    return FeatureBase.CallService("SmartSchool.Config.GetModulePreference", new DSRequest()).GetContent();
        //}
    }

    public static class DataCacheManager
    {
        private static Dictionary<string, DSResponse> _dataManager = new Dictionary<string, DSResponse>();

        public static void Add(string name, DSResponse dsrsp)
        {
            lock (_dataManager)
            {
                if (!_dataManager.ContainsKey(name))
                    _dataManager.Add(name, dsrsp);
                else
                    _dataManager[name] = dsrsp;
            }
        }

        public static DSResponse Get(string name)
        {
            lock (_dataManager)
            {
                if (_dataManager.ContainsKey(name))
                    return _dataManager[name];
                return null;
            }
        }

        public static bool Contains(string serviceName)
        {
            return _dataManager.ContainsKey(serviceName);
        }

        public static void Remove(string serviceName)
        {
            if (_dataManager.ContainsKey(serviceName))
            {
                _dataManager.Remove(serviceName);
            }
        }

        public static void Set(string name, DSResponse dsrsp)
        {
            _dataManager[name] = dsrsp;
        }
    }
}
