using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SmartSchool.StudentRelated;
using SmartSchool.TeacherRelated;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.ClassRelated
{
    public class ClassInfo : IComparable<ClassInfo>
    {
        private string _classID;
        private string _className;
        private string _teacherName;
        private string _nickname;
        private string _refTeacherID;
        private string _gradeYear;
        private string _department;
        private string _NamingRule;
        private int _DisplayOrder;

        public ClassInfo(XmlElement element)
        {
            _classID = element.GetAttribute("ID");
            DSXmlHelper helper = new DSXmlHelper(element);
            _className = helper.GetText("ClassName");
            _teacherName = helper.GetText("TeacherName");
            _nickname = helper.GetText("TeacherNickname");
            _refTeacherID = helper.GetText("RefTeacherID");
            _gradeYear = helper.GetText("GradeYear");
            _department = helper.GetText("Department");
            _NamingRule = helper.GetText("NamingRule");
            //_refGraduationPlanID = helper.GetText("RefGraduationPlanID");
            //_graduationPlanName = helper.GetText("GraduationPlanName");
            //_refScoreCalcRuleID = helper.GetText("RefScoreCalcRuleID");
            int x = 0;
            if (int.TryParse(helper.GetText("DisplayOrder"), out x))
                _DisplayOrder = x;
            else
                _DisplayOrder = int.MaxValue;
            if (_gradeYear == "")
                _gradeYear = "未分年級";
        }

        public string ClassID { get { return _classID; } }
        public string ClassName { get { return _className; } }
        public string NamingRule { get { return _NamingRule; } }
        public string StudentCount { get { return Student.Instance.GetClassStudent(_classID).Count.ToString(); } }
        public string TeacherName { get { return ( Teacher.Instance.Items[_refTeacherID]==null || Teacher.Instance.Items[_refTeacherID].Status == "刪除" ) ? "" : Teacher.Instance.Items[_refTeacherID].TeacherName; } }
        public string TeacherNickname { get { return ( Teacher.Instance.Items[_refTeacherID] == null || Teacher.Instance.Items[_refTeacherID].Status == "刪除" ) ? "" : Teacher.Instance.Items[_refTeacherID].Nickname; } }
        public string TeacherUniqName
        {
            get
            {
                if (string.IsNullOrEmpty(TeacherNickname))
                    return TeacherName;
                else
                    return string.Format("{0} ({1})", TeacherName, TeacherNickname);
            }
        }
        public string TeacherID { get { return (_refTeacherID == "" || TeacherRelated.Teacher.Instance.Items[_refTeacherID] == null) ? "" : _refTeacherID; } }
        public string GradeYear { get { return _gradeYear; } }
        public string Department { get { return _department; } }
        //public string RefGraduationPlanID { get { return (_refGraduationPlanID == "" || GraduationPlanRelated.GraduationPlan.Instance.Items[_refGraduationPlanID] == null) ? "" : _refGraduationPlanID; } }
        public int DisplayOrder { get { return _DisplayOrder; } }
        //public string GraduationPlanName { get { return RefGraduationPlanID == "" ? "" : GraduationPlan.Instance.Items[_refGraduationPlanID].Name; } }
        public List<BriefStudentData> Students { get { return Student.Instance.GetClassStudent(_classID); } }
        //public GraduationPlanInfo GraduationPlanInfo { get { return RefGraduationPlanID == "" ? null : GraduationPlan.Instance.Items[_refGraduationPlanID]; } }
        //public string RefScoreCalcRuleID { get { return (_refScoreCalcRuleID == "" || ScoreCalcRuleRelated.ScoreCalcRule.Instance.Items[_refScoreCalcRuleID] == null) ? "" : _refScoreCalcRuleID; } }
        //public ScoreCalcRuleInfo ScoreCalcRuleInfo { get { return RefScoreCalcRuleID == "" ? null : ScoreCalcRule.Instance.Items[_refScoreCalcRuleID]; } }
        //public string ScoreCalcRuleName { get { return RefScoreCalcRuleID == "" ? null : ScoreCalcRule.Instance.Items[_refScoreCalcRuleID].Name; } }


        #region IComparable<ClassInfo> 成員
        private string[] departmentKeys = new string [] { "普通" };
        private string[] gradeYearKeys = new string[] { "1", "2", "3", "4", "5", "6" };
        public int CompareTo(ClassInfo other)
        {
            //比年級
            if (this.GradeYear != other.GradeYear)
                return Common.StringComparer.Comparer(this.GradeYear, other.GradeYear, gradeYearKeys);
            //比Order
            if (this.DisplayOrder != other.DisplayOrder)
                return this.DisplayOrder.CompareTo(other.DisplayOrder);
            //比科別
            if (this.Department != other.Department)
                return Common.StringComparer.Comparer(this.Department, other.Department, departmentKeys);
            //用關鍵字比較班級名稱
            return Common.StringComparer.Comparer(this.ClassName, other.ClassName);
        }

        #endregion
    }
}
