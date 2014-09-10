using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.StudentRelated.RibbonBars.AttendanceEditor
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
            _subtract = element.GetAttribute("Subtract");
            _aggregated = element.GetAttribute("Aggregated");
            bool b;
            bool.TryParse(element.GetAttribute("Noabsence"), out b);
            _noabsence = b;
        }

        #region 安嘿
        private string _name;

        /// <summary>
        /// 安嘿
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        } 
        #endregion
        #region 安荐龄
        private string _hotkey;

        /// <summary>
        ///  安荐龄
        /// </summary> 
        public string Hotkey
        {
            get { return _hotkey; }
            set { _hotkey = value; }
        } 
        #endregion
        #region 安罽糶
        private string _abbreviation;

        /// <summary>
        /// 安罽糶
        /// </summary>
        public string Abbreviation
        {
            get { return _abbreviation; }
            set { _abbreviation = value; }
        } 
        #endregion
        #region Ιだ
        private string _subtract;
        /// <summary>
        /// Ιだ
        /// </summary>
        public string Subtract
        {
            get { return _subtract; }
        } 
        #endregion
        #region 仓璸胢竊Ω
        private string _aggregated;
        /// <summary>
        /// 仓璸胢竊Ω
        /// </summary>
        public string Aggregated
        {
            get { return _aggregated; }
        } 
        #endregion
        #region ぃ紇臫对
        private bool _noabsence;
        /// <summary>
        /// ぃ紇臫对
        /// </summary>
        public bool Noabsence
        {
            get { return _noabsence; }
        } 
        #endregion

        public static AbsenceInfo Empty
        {
            get
            {
                AbsenceInfo info = new AbsenceInfo();
                info.Name = string.Empty;
                info.Abbreviation = string.Empty;
                info.Hotkey = string.Empty;
                return info;
            }
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
