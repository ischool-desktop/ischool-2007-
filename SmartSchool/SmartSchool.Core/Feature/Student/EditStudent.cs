using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;

namespace SmartSchool.Feature
{
    public class EditStudent : FeatureBase
    {
        /// <summary>
        /// ��s�ǥͰ򥻸��
        /// </summary>
        /// <param name="dsreq">DSRequest���</param>
        /// <returns>DSResponse</returns>
        [QueryRequest()]
        public static DSResponse Update(DSRequest dsreq)
        {
            return CallService("SmartSchool.Student.Update", dsreq);
        }

        [QueryRequest()]
        public static void UpdatePhone(DSXmlHelper phoneNode)
        {
            DSRequest dsreq = new DSRequest(phoneNode);
            CallService("SmartSchool.Student.UpdatePhone", dsreq);
        }

        [QueryRequest()]
        public static void UpdatePhoto(DSRequest dsreq)
        {            
            CallService("SmartSchool.Student.UpdatePhoto", dsreq);
        }

        [QueryRequest()]
        public static void UpdateFreshmanPhoto(string picBase64String, string id)
        {
            DSRequest dsreq = new DSRequest();
            DSXmlHelper helper = new DSXmlHelper("UpdateStudentList");
            helper.AddElement("Student");
            helper.AddElement("Student", "Field");
            helper.AddElement("Student/Field", "FreshmanPhoto");
            helper.AddCDataSection("Student/Field/FreshmanPhoto", picBase64String);
            helper.AddElement("Student", "Condition");
            helper.AddElement("Student/Condition", "ID", id);
            dsreq.SetContent(helper);
            DSResponse dsrsp = CallService("SmartSchool.Student.Update", dsreq);            
        }

        [QueryRequest()]
        public static void UpdateGraduatePhoto(string picBase64String, string id)
        {
            DSRequest dsreq = new DSRequest();
            DSXmlHelper helper = new DSXmlHelper("UpdateStudentList");
            helper.AddElement("Student");
            helper.AddElement("Student", "Field");
            helper.AddElement("Student/Field", "GraduatePhoto");
            helper.AddCDataSection("Student/Field/GraduatePhoto", picBase64String);
            helper.AddElement("Student", "Condition");
            helper.AddElement("Student/Condition", "ID", id);
            dsreq.SetContent(helper);
            DSResponse dsrsp = CallService("SmartSchool.Student.Update", dsreq);
        }

        [QueryRequest()]
        public static void SetExtend(string nameSpace, string field, IDictionary<string, string> list)
        {
            if ( list.Count == 0 ) return;
            foreach ( string var in new string[]{"0","1","2","3","4","5","6","7","8","9"} )
            {
                if ( field.StartsWith(var) )
                    throw new Exception("���W�ٶ}�Y���i���Ʀr");
            }
            DSRequest dsreq = new DSRequest();
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.SetAttribute(".", "Namespace", nameSpace);
            foreach ( string id in list.Keys )
            {
                helper.AddElement("Student");
                helper.SetAttribute("Student", "ID", id);
                if(string.IsNullOrEmpty(list[id]))
                    helper.AddElement("Student", field);
                else
                helper.AddElement("Student", field,list[id]);
            }
            dsreq.SetContent(helper);
            CallService("SmartSchool.Student.SetExtend", dsreq);
        }

        [QueryRequest()]
        public static void ChangeStudentStatus(string StudentID,string newStatus)
        {
            DSRequest dsreq = new DSRequest("<ChangeStatusRequest><Student><Field><Status>" + newStatus + "</Status></Field><Condition><ID>" + StudentID + "</ID></Condition></Student></ChangeStatusRequest>");
            DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Student.ChangeStatus", dsreq);
        }

        [QueryRequest()]
        public static void ChangeStudentStatus(string newStatus, params string[] ids)
        {
            if ( ids.Length > 0 )
            {
                string req = "<ChangeStatusRequest><Student><Field><Status>" + newStatus + "</Status></Field><Condition>";
                foreach ( string id in ids )
                {
                    req += "<ID>" + id + "</ID>";
                }
                req += "</Condition></Student></ChangeStatusRequest>";
                DSRequest dsreq = new DSRequest(req);
                DSResponse dsrsp = CurrentUser.Instance.CallService("SmartSchool.Student.Update", dsreq);
            }
        }

        public static void InsertUpdateRecord(DSRequest dsreq)
        {
            //throw new Exception("The method or operation is not implemented.");
            CallService("SmartSchool.Student.UpdateRecord.Insert", dsreq);
        }

        [QueryRequest()]
        public static void ModifyUpdateRecord(DSRequest dsreq)
        {
            CallService("SmartSchool.Student.UpdateRecord.Update", dsreq);
        }

        [QueryRequest()]
        public static void RemoveUpdateRecord(string updateID)
        {
            DSXmlHelper helper = new DSXmlHelper("DeleteRequest");
            helper.AddElement("UpdateRecord");
            helper.AddElement("UpdateRecord","ID",updateID);
            CallService("SmartSchool.Student.UpdateRecord.Delete", new DSRequest(helper));
        }

        [QueryRequest()]
        public static void ModifyUpdateRecordBatch(DSRequest dSRequest)
        {
            CallService("SmartSchool.Student.UpdateRecord.AuthorizeBatch", dSRequest);
        }

        [QueryRequest()]
        public static void DeleteClassID(string studentid)
        {
            DSRequest dsreq = new DSRequest("<UpdateStudentList><Student><Field><RefClassID></RefClassID></Field><Condition><ID>" + studentid + "</ID></Condition></Student></UpdateStudentList>");
            DSResponse dsrsp = FeatureBase.CallService("SmartSchool.Student.Update", dsreq);
        }
    }
}
