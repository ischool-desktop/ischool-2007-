using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.StudentRelated.Palmerworm.Attendance
{
    public class AbsenceInfo
    {
        public AbsenceInfo()
        { }

        public AbsenceInfo(XmlElement element)
        {
            _name = element.GetAttribute("Name");
            _hotkey = element.GetAttribute("HotKey");
            _abbreviation = element.GetAttribute("Abbreviation");
        }
        private string _name;

        /// <summary>
        /// 假別名稱
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _hotkey;

        /// <summary>
        ///  假別熱鍵
        /// </summary>
        public string Hotkey
        {
            get { return _hotkey; }
            set { _hotkey = value; }
        }
        private string _abbreviation;

        /// <summary>
        /// 假別縮寫
        /// </summary>
        public string Abbreviation
        {
            get { return _abbreviation; }
            set { _abbreviation = value; }
        }

        public AbsenceInfo Clone()
        {
            AbsenceInfo info = new AbsenceInfo();
            info.Abbreviation = _abbreviation;
            info.Hotkey = _hotkey;
            info.Name = _name;
            return info;
        }
    }
}
