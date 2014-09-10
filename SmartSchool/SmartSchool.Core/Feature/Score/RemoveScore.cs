using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;

namespace SmartSchool.Feature.Score
{
    [QueryRequest()]
    public class RemoveScore
    {
        public static void DeleteSemesterEntityScore(params  string[] idlist)
        {
            string req = "<DeleteRequest><SemesterEntryScore>";
            foreach ( string id in idlist )
            {
                req += "<ID>" + id + "</ID>";
            }
            req += "</SemesterEntryScore></DeleteRequest>";
            DSRequest dsreq = new DSRequest(req);
            //DSRequest dsreq = new DSRequest("<DeleteRequest><SemesterEntryScore><ID>"+id+"</ID></SemesterEntryScore></DeleteRequest>");
            CurrentUser.Instance.CallService("SmartSchool.Score.DeleteSemesterEntryScore", dsreq);
        }
        public static void DeleteSemesterSubjectScore(params  string[] idlist)
        {
            string req = "<DeleteRequest><SemesterSubjectScore>";
            foreach ( string id in idlist )
            {
                req += "<ID>" + id + "</ID>";
            }
            req += "</SemesterSubjectScore></DeleteRequest>";
            DSRequest dsreq = new DSRequest(req);

            //DSRequest dsreq = new DSRequest("<DeleteRequest><SemesterSubjectScore><ID>" + id + "</ID></SemesterSubjectScore></DeleteRequest>");
            CurrentUser.Instance.CallService("SmartSchool.Score.DeleteSemesterSubjectScore", dsreq);
        }
        public static void DeleteSchoolYearEntityScore(params  string[] idlist)
        {
            string req = "<DeleteRequest><SchoolYearEntryScore>";
            foreach ( string id in idlist )
            {
                req += "<ID>" + id + "</ID>";
            }
            req += "</SchoolYearEntryScore></DeleteRequest>";
            DSRequest dsreq = new DSRequest(req);

            //DSRequest dsreq = new DSRequest("<DeleteRequest><SchoolYearEntryScore><ID>" + id + "</ID></SchoolYearEntryScore></DeleteRequest>");
            CurrentUser.Instance.CallService("SmartSchool.Score.DeleteSchoolYearEntryScore", dsreq);
        }
        public static void DeleteSchoolYearSubjectScore(params  string[] idlist)
        {
            string req = "<DeleteRequest><SchoolYearSubjectScore>";
            foreach ( string id in idlist )
            {
                req += "<ID>" + id + "</ID>";
            }
            req += "</SchoolYearSubjectScore></DeleteRequest>";
            DSRequest dsreq = new DSRequest(req);

            //DSRequest dsreq = new DSRequest("<DeleteRequest><SchoolYearSubjectScore><ID>" + id + "</ID></SchoolYearSubjectScore></DeleteRequest>");
            CurrentUser.Instance.CallService("SmartSchool.Score.DeleteSchoolYearSubjectScore", dsreq);
        }

        public static void DeleteSemesterMoralScore(DSRequest dSRequest)
        {
            CurrentUser.Instance.CallService("SmartSchool.Score.DeleteSemesterMoralScore", dSRequest);
        }
    }
}
