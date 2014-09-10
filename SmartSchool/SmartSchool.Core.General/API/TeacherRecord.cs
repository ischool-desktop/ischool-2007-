using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.API
{
    internal class TeacherRecord:Customization.Data.TeacherRecord
    {
        private string _Gander = "";

        private string _Status = "";

        private string _TeacherID = "";

        private string _TeacherName = "";

        private Dictionary<string, object> _Fields = new Dictionary<string, object>();

        SmartSchool.Customization.Data.CategoryCollection _TeacherCategory = new SmartSchool.Customization.Data.CategoryCollection();

        public TeacherRecord(TeacherRelated.BriefTeacherData teacher)
        {
            _Gander = teacher.Gender;
            _Status = teacher.Status;
            _TeacherID = teacher.ID;
            _TeacherName = teacher.TeacherName;           
        }

        #region TeacherRecord 成員

        public string Gender
        {
            get { return _Gander; }
        }

        public string Status
        {
            get { return _Status; }
        }

        public SmartSchool.Customization.Data.CategoryCollection  TeacherCategory
        {
            get { return _TeacherCategory; }
        }

        public string TeacherID
        {
            get { return _TeacherID; }
        }

        public string TeacherName
        {
            get { return _TeacherName; }
        }

        public Dictionary<string, object> Fields
        {
            get { return _Fields; }
        }

        public string IdentifiableName
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion
    }
}
