using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.API.StudentExtension
{
    internal class Parent:Customization.Data.StudentExtension.ParentInfo
    {
        private string _CustodianName;
        private string _CustodianRelationship;
        private string _FatherName;
        private string _MotherName;
        private bool _FatherLiving;
        private bool _MotherLiving;

        public Parent(string custodianName, string custodianRelationship, string fatherName, string motherName, bool fatherLiving, bool motherLiving)
        {
            _CustodianName = custodianName;
            _CustodianRelationship = custodianRelationship;
            _FatherLiving = fatherLiving;
            _FatherName = fatherName;
            _MotherLiving = motherLiving;
            _MotherName = motherName;
        }

        #region ParentInfo 成員

        public string CustodianName
        {
            get { return _CustodianName; }
        }

        public string CustodianRelationship
        {
            get { return _CustodianRelationship; }
        }

        public bool FatherLiving
        {
            get { return _FatherLiving; }
        }

        public string FatherName
        {
            get { return _FatherName; }
        }

        public bool MotherLiving
        {
            get { return _MotherLiving; }
        }

        public string MotherName
        {
            get { return _MotherName; }
        }

        #endregion
    }
}
