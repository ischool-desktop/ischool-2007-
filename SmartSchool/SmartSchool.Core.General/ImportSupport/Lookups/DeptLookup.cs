using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SmartSchool.Feature.Class;
using SmartSchool.Feature.Department;
using SmartSchool.Feature.GraduationPlan;

namespace SmartSchool.ImportSupport.Lookups
{
    internal class DeptLookup
    {
        private Dictionary<string, string> _items;

        public DeptLookup()
        {
            _items = new Dictionary<string, string>();

            XmlElement xmlRecords = QueryDepartment.GetAbstractList().GetContent().BaseElement;
            foreach (XmlElement each in xmlRecords.SelectNodes("Department"))
            {
                string name = each.SelectSingleNode("Name").InnerText;
                string id = each.GetAttribute("ID");

                _items.Add(name, id);
            }
        }

        public string GetDeptID(string name)
        {
            if (_items.ContainsKey(name))
                return _items[name];
            else
                return string.Empty;
        }

        public bool Contains(string name)
        {
            return _items.ContainsKey(name);
        }

        public static string Name
        {
            get { return "DeptLookup"; }
        }
    }
}
