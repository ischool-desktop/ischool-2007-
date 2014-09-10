using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using System.Xml;

namespace SmartSchool.Feature
{
    internal class Security : FeatureBase
    {
        [QueryRequest()]
        internal static DSResponse GetLoginDetailList()
        {
            return CallService("SmartSchool.Security.GetLoginDetailList", new DSRequest());
        }

        [QueryRequest()]
        internal static DSResponse GetRoleDetailList()
        {
            return CallService("SmartSchool.Security.GetRoleDetailList", new DSRequest());
        }

        [QueryRequest()]
        internal static void DeleteLogin(string id)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Login");
            helper.AddElement("Login", "ID", id);
            CallService("SmartSchool.Security.DeleteLogin", new DSRequest(helper));
        }

        [QueryRequest()]
        internal static void DeleteLRBelong(string id)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("LRBelong");
            helper.AddElement("LRBelong", "LoginID", id);
            CallService("SmartSchool.Security.DeleteLRBelong", new DSRequest(helper));
        }

        internal static void InsertLRBelong(string loginID, params string[] roleIDs)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            foreach (string roleID in roleIDs)
            {
                helper.AddElement("LRBelong");
                helper.AddElement("LRBelong", "LoginID", loginID);
                helper.AddElement("LRBelong", "RoleID", roleID);
            }
            
            CallService("SmartSchool.Security.InsertLRBelong", new DSRequest(helper));
        }

        internal static void InsertLogin(string name, string password)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Login");
            helper.AddElement("Login", "LoginName", name);
            helper.AddElement("Login", "Password", password);
            CallService("SmartSchool.Security.InsertLogin", new DSRequest(helper));
        }

        [QueryRequest()]
        internal static void UpdateLogin(string name, string password)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Login");
            helper.AddElement("Login", "Password", password);
            helper.AddElement("Login", "Condition");
            helper.AddElement("Login/Condition", "LoginName", name);
            CallService("SmartSchool.Security.UpdateLogin", new DSRequest(helper));
        }

        internal static void InsertRole(string name, string desc)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Role");
            helper.AddElement("Role", "RoleName", name);
            helper.AddElement("Role", "Description", desc);
            CallService("SmartSchool.Security.InsertRole", new DSRequest(helper));
        }

        internal static void InsertRole(string name, string desc, XmlElement permission)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Role");
            helper.AddElement("Role", "RoleName", name);
            helper.AddElement("Role", "Description", desc);
            helper.AddElement("Role", "Permission", permission.OuterXml, true);
            CallService("SmartSchool.Security.InsertRole", new DSRequest(helper));
        }

        [QueryRequest()]
        internal static void DeleteRole(string id)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Role");
            helper.AddElement("Role", "ID", id);
            CallService("SmartSchool.Security.DeleteRole", new DSRequest(helper));
        }

        [QueryRequest()]
        internal static void UpdateRole(string id, XmlElement permission)
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Role");
            helper.AddElement("Role", "Permission", permission.OuterXml, true);
            helper.AddElement("Role", "Condition");
            helper.AddElement("Role/Condition", "ID", id);
            CallService("SmartSchool.Security.UpdateRole", new DSRequest(helper));
        }
    }
}
