using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.API.Provider
{
    class SystemProvider : Customization.Data.SystemInformationProvider
    {
        #region SystemInformationProvider 成員

        string Customization.Data.SystemInformationProvider.Address
        {
            get { return CurrentUser.Instance.SchoolInfo.Address; }
        }

        string Customization.Data.SystemInformationProvider.Fax
        {
            get { return CurrentUser.Instance.SchoolInfo.Fax; }
        }

        object Customization.Data.SystemInformationProvider.GetField(string fieldName)
        {
            object result = null;
            switch (fieldName)
            { 
                case "DiffItem":
                    List<string> diffItems=new List<string>();
                    foreach (XmlElement var in SmartSchool.Feature.Basic.Config.GetMoralDiffItemList().GetContent().GetElements("DiffItem"))
                    {
                        string name = var.GetAttribute("Name");
                        if (!diffItems.Contains(name))
                            diffItems.Add(name);
                    }
                    result = diffItems;
                    break;
                case "Degree":
                    Dictionary<string, decimal> degreeLevel = new Dictionary<string, decimal>();
                    DSResponse dsrsp = SmartSchool.Feature.Basic.Config.GetDegreeList();
                    DSXmlHelper helper = dsrsp.GetContent();
                    foreach (XmlElement element in helper.GetElements("Degree"))
                    {
                        decimal low = decimal.MinValue;
                        if (!decimal.TryParse(element.GetAttribute("Low"), out low))
                            low = decimal.MinValue;
                        degreeLevel.Add(element.GetAttribute("Name"), low);
                    }
                    result = degreeLevel;
                    break;
                case "":

                    break;
                case "StudentCategories":
                    XmlElement categoryXml = Feature.Tag.QueryTag.GetDetailList(SmartSchool.Feature.Tag.TagCategory.Student);
                    result = categoryXml;
                    break;
                case "Period":
                    result = SmartSchool.Feature.Basic.Config.GetPeriodList().GetContent().BaseElement;
                    break;
                case "Absence":
                    result = SmartSchool.Feature.Basic.Config.GetAbsenceList().GetContent().BaseElement;
                    break;
                case "SchoolConfig":
                    result = CurrentUser.Instance.SchoolConfig.Content;
                    break;
                case "科別對照表":
                    return SmartSchool.Feature.Department.QueryDepartment.GetAbstractList().GetContent().BaseElement;
                    break;
                case "文字評量對照表":
                    return SmartSchool.Feature.Basic.Config.GetWordCommentList().GetContent().BaseElement;
                    break;
            }
            return result;
        }

        string Customization.Data.SystemInformationProvider.SchoolChineseName
        {
            get { return CurrentUser.Instance.SchoolChineseName; }
        }

        string Customization.Data.SystemInformationProvider.SchoolCode
        {
            get { return CurrentUser.Instance.SchoolCode; }
        }

        string Customization.Data.SystemInformationProvider.SchoolEnglishName
        {
            get { return CurrentUser.Instance.SchoolEnglishName; }
        }

        int Customization.Data.SystemInformationProvider.SchoolYear
        {
            get { return CurrentUser.Instance.SchoolYear; }
        }

        int Customization.Data.SystemInformationProvider.Semester
        {
            get { return CurrentUser.Instance.Semester; }
        }

        string Customization.Data.SystemInformationProvider.Telephone
        {
            get { return CurrentUser.Instance.SchoolInfo.Telephone; }
        }

        SmartSchool.Customization.Data.PreferenceCollection Customization.Data.SystemInformationProvider.Preference
        {
            get { return CurrentUser.Instance.Preference; }
        }


        public SmartSchool.Customization.Data.ConfigurationCollection Configuration
        {
            get { return CurrentUser.Instance.Configuration; }
        }

        #endregion
    }
}
