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

        #region ���O�W��
        private string _name;

        /// <summary>
        /// ���O�W��
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        } 
        #endregion
        #region ���O����
        private string _hotkey;

        /// <summary>
        ///  ���O����
        /// </summary> 
        public string Hotkey
        {
            get { return _hotkey; }
            set { _hotkey = value; }
        } 
        #endregion
        #region ���O�Y�g
        private string _abbreviation;

        /// <summary>
        /// ���O�Y�g
        /// </summary>
        public string Abbreviation
        {
            get { return _abbreviation; }
            set { _abbreviation = value; }
        } 
        #endregion
        #region ����
        private string _subtract;
        /// <summary>
        /// ����
        /// </summary>
        public string Subtract
        {
            get { return _subtract; }
        } 
        #endregion
        #region �֭p���m�`��
        private string _aggregated;
        /// <summary>
        /// �֭p���m�`��
        /// </summary>
        public string Aggregated
        {
            get { return _aggregated; }
        } 
        #endregion
        #region ���v�T����
        private bool _noabsence;
        /// <summary>
        /// ���v�T����
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
