using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Score.Rating
{
    [QueryRequest()]
    public class EditRating
    {
        public static void UpdateSemesterSubjectRating(DSXmlHelper request)
        {
            DSRequest dsreq = new DSRequest(request);
            CurrentUser.Instance.CallService("SmartSchool.Score.Rating.UpdateSemesterSubjectRating", dsreq);
        }

        public static void UpdateSchoolYearSubjectRating(DSXmlHelper request)
        {
            DSRequest dsreq = new DSRequest(request);
            CurrentUser.Instance.CallService("SmartSchool.Score.Rating.UpdateSchoolYearSubjectRating", dsreq);
        }

        public static void UpdateSemesterEntryRating(DSXmlHelper request)
        {
            DSRequest dsreq = new DSRequest(request);
            CurrentUser.Instance.CallService("SmartSchool.Score.Rating.UpdateSemesterEntryRating", dsreq);
        }

        public static void UpdateSchoolYearEntryRating(DSXmlHelper request)
        {
            DSRequest dsreq = new DSRequest(request);
            CurrentUser.Instance.CallService("SmartSchool.Score.Rating.UpdateSchoolYearEntryRating", dsreq);
        }

    }
}