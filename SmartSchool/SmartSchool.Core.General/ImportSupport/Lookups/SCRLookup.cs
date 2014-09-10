﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SmartSchool.Feature.Class;
using SmartSchool.Feature.Department;
using SmartSchool.Feature.ScoreCalcRule;

namespace SmartSchool.ImportSupport.Lookups
{
    /// <summary>
    /// 代表計算規則 (ScoreCalcRule) 查照表。
    /// </summary>
    internal class SCRLookup
    {
        private Dictionary<string, string> _items;

        public SCRLookup()
        {
            _items = new Dictionary<string, string>();

            XmlElement xmlRecords = QueryScoreCalcRule.GetScoreCalcRuleList().GetContent().BaseElement;
            foreach (XmlElement each in xmlRecords.SelectNodes("ScoreCalcRule"))
            {
                string name = each.SelectSingleNode("Name").InnerText;
                string id = each.GetAttribute("ID");

                _items.Add(name, id);
            }
        }

        public string GetScoreCalcRuleID(string name)
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
            get { return "SCRLookup"; }
        }
    }
}
