using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;
using System.Xml;
using SmartSchool.TagManage;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.StudentRelated
{
    public class BriefStudentData:IComparable<BriefStudentData>
    {
        private readonly string _ID;
        private readonly string _Status;
        private readonly string _SeatNo;
        private readonly string _Name;
        private readonly string _StudentNumber;
        private readonly string _Gender;
        private readonly string _IDNumber;
        private readonly string _PermanentPhone;
        private readonly string _ContactPhone;
        private readonly string _RefClassID;
        private readonly string _Birthday;
        private readonly string _OverrideDepartment;
        private readonly string _LeaveSchoolYear;
        private readonly string _LeaveReason;
        private readonly string _LeaveDepartment;
        private readonly string _LeaveClassName;
        private Dictionary<int,TagInfo> _Tags;

        public string ID { get { return _ID; } }
        public string Status { get { return _Status; } }
        public string SeatNo { get { return _SeatNo; } }
        public string Name { get { return _Name; } }
        public string StudentNumber { get { return _StudentNumber; } }
        public string Gender { get { return _Gender; } }
        public string IDNumber { get { return _IDNumber; } }
        public string PermanentPhone { get { return _PermanentPhone; } }
        public string ContactPhone { get { return _ContactPhone; } }
        public string Birthday { get { return _Birthday; } }
        internal string NonCheckedRefClassID { get { return _RefClassID; } }
        public string RefClassID { get { return (_RefClassID==""||ClassRelated.Class.Instance.Items[_RefClassID]==null) ? "" : _RefClassID; } }
        //public string RefGraduationPlanID { get { return RefClassID == "" ? "" : ClassRelated.Class.Instance.Items[_RefClassID].RefGraduationPlanID; } }
        //public string RefScoreCalcRuleID { get { return RefClassID == "" ? "" : ClassRelated.Class.Instance.Items[_RefClassID].RefScoreCalcRuleID; } }
        public string ClassName { get { return ( _RefClassID == "" || ClassRelated.Class.Instance.Items[_RefClassID] == null ) ? "" : ClassRelated.Class.Instance.Items[_RefClassID].ClassName; } }
        public string GradeYear { get { return ( _RefClassID == "" || ClassRelated.Class.Instance.Items[_RefClassID] == null ) ? "" : ClassRelated.Class.Instance.Items[_RefClassID].GradeYear; } }
        public string Department { get { return _OverrideDepartment==""?((_RefClassID==""||ClassRelated.Class.Instance.Items[_RefClassID]==null)?"":ClassRelated.Class.Instance.Items[_RefClassID].Department):_OverrideDepartment; } }
        //public string GraduationPlanName { get { return RefClassID == "" ? "" : ClassRelated.Class.Instance.Items[_RefClassID].GraduationPlanName; } }
        //public GraduationPlanInfo GraduationPlanInfo { get { return RefClassID == "" ? null : ClassRelated.Class.Instance.Items[_RefClassID].GraduationPlanInfo; } }
        //public ScoreCalcRuleInfo ScoreCalcRuleInfo { get { return RefClassID == "" ? null : ClassRelated.Class.Instance.Items[_RefClassID].ScoreCalcRuleInfo; } }
        public string LeaveSchoolYear { get { return _LeaveSchoolYear; } }
        public string LeaveReason { get { return _LeaveReason; } }
        public string LeaveDepartment { get { return _LeaveDepartment; } }
        public string LeaveClassName { get { return _LeaveClassName; } }
        public ReadOnlyCollection<int, TagInfo> Tags { get { return new ReadOnlyCollection<int, TagInfo>(_Tags); } }

        /// <summary>
        /// 在校學生
        /// </summary>
        public bool IsNormal { get { return (_Status == "一般" || _Status == "延修" || _Status == "輟學"); } }
        /// <summary>
        /// 延修生
        /// </summary>
        public bool IsExtending { get { return (_Status == "延修"); } }
        /// <summary>
        /// 休學生
        /// </summary>
        public bool IsOnLeave { get { return (_Status == "休學"); } }
        /// <summary>
        /// 已刪除學生
        /// </summary>
        public bool IsDeleted { get { return (_Status == "刪除"); } }
        /// <summary>
        /// 畢業或離校生
        /// </summary>
        public bool IsGraduated { get { return (_Status == "畢業或離校"); } }
        /// <summary>
        /// 輟學生
        /// </summary>
        public bool IsDiscontinued { get { return (_Status == "輟學"); } }


        internal BriefStudentData(XmlElement element)
        {
            DSXmlHelper helper = new DSXmlHelper(element);
            _ID = helper.GetText("@ID");
            _Status = helper.GetText("Status");
            _SeatNo = helper.GetText("SeatNo");
            _Name = helper.GetText("Name");
            _StudentNumber = helper.GetText("StudentNumber");
            _Gender = helper.GetText("Gender");
            _IDNumber = helper.GetText("IDNumber");
            _PermanentPhone = helper.GetText("PermanentPhone");
            _ContactPhone = helper.GetText("ContactPhone");
            _RefClassID = helper.GetText("RefClassID");
            _Birthday = helper.GetText("Birthdate");
            _OverrideDepartment = helper.GetText("OverrideDeptName");
            _LeaveSchoolYear = helper.GetText("LeaveInfo/LeaveInfo/@SchoolYear");
            _LeaveReason = helper.GetText("LeaveInfo/LeaveInfo/@Reason");
            _LeaveDepartment = helper.GetText("LeaveInfo/LeaveInfo/@Department");
            _LeaveClassName = helper.GetText("LeaveInfo/LeaveInfo/@ClassName");
            List<TagInfo> tags = new List<TagInfo>();
            foreach (XmlElement tagElement in helper.GetElements("Tags/Tag"))
            {
                string id = tagElement.GetAttribute("ID");
                string color = tagElement.GetAttribute("Color");
                string name = tagElement.GetAttribute("Name");
                string prefix = tagElement.GetAttribute("Prefix");
                int key = 0;
                if (int.TryParse(id,out key))
                {
                    TagInfo newTag = new TagInfo(key, prefix, name, color);
                    tags.Add(newTag);
                }
            }
            tags.Sort(CompareDinosByLength);
            _Tags = new Dictionary<int, TagInfo>();
            foreach (TagInfo tag in tags)
            {
                _Tags.Add(tag.Identity, tag);
            }
        }
        private static int CompareDinosByLength(TagInfo x, TagInfo y)
        {
            return x.FullName.CompareTo(y.FullName);
        }


        #region IComparable<BriefStudentData> 成員

        public int CompareTo(BriefStudentData other)
        {
            if ( this.RefClassID == other.RefClassID )
            {
                if ( this.RefClassID != "" )
                {
                    int seatNo1, seatNo2;
                    int.TryParse(this.SeatNo, out seatNo1);
                    int.TryParse(other.SeatNo, out seatNo2);
                    return seatNo1.CompareTo(seatNo2);
                }
                else
                {
                    return this.StudentNumber.CompareTo(other.StudentNumber);
                }
            }
            else
            {
                if ( this.RefClassID == "" ) return -1;
                if ( other.RefClassID == "" ) return 1;
                return ClassRelated.Class.Instance.Items[this.RefClassID].CompareTo(ClassRelated.Class.Instance.Items[other.RefClassID]);
            }
        }

        #endregion
    }
}
