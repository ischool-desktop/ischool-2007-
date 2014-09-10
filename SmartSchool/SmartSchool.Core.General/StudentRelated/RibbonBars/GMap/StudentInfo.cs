using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.StudentRelated.RibbonBars.GMap
{
    public class StudentInfo
    {
        public StudentInfo(BriefStudentData student, XmlElement otherData)
        {
            DSXmlHelper hlpData = new DSXmlHelper(otherData);

            _student = student;
            _identity = BaseInfo.ID;
            _photo = hlpData.GetText("FreshmanPhoto");

            _addresses = new AddressCollection();

            AddressInfo oAdd;
            if (hlpData.PathExist("OtherAddresses/AddressList/Address"))
            {
                oAdd = new AddressInfo(this, hlpData.GetElement("OtherAddresses/AddressList/Address"), AddressType.OtherAddresses);
                if (string.IsNullOrEmpty(oAdd.County) && string.IsNullOrEmpty(oAdd.Town))
                { }
                else
                    _addresses.AddAddress(oAdd);
            }

            if (hlpData.PathExist("PermanentAddress/AddressList/Address"))
            {
                oAdd = new AddressInfo(this, hlpData.GetElement("PermanentAddress/AddressList/Address"), AddressType.PermanentAddress);
                if (string.IsNullOrEmpty(oAdd.County) && string.IsNullOrEmpty(oAdd.Town))
                { }
                else
                    _addresses.AddAddress(oAdd);
            }

            if (hlpData.PathExist("MailingAddress/AddressList/Address"))
            {
                oAdd = new AddressInfo(this, hlpData.GetElement("MailingAddress/AddressList/Address"), AddressType.MailingAddress);
                if (string.IsNullOrEmpty(oAdd.County) && string.IsNullOrEmpty(oAdd.Town))
                { }
                else
                    _addresses.AddAddress(oAdd);
            }
        }

        private string _identity;
        public string Identity
        {
            get { return _identity; }
        }

        private BriefStudentData _student;
        public BriefStudentData BaseInfo
        {
            get { return _student; }
        }

        private AddressCollection _addresses;
        public AddressCollection Addresses
        {
            get { return _addresses; }
        }

        private AddressMark _map_mark;
        public AddressMark MapMark
        {
            get { return _map_mark; }
            set { _map_mark = value; }
        }

        private string _photo;
        public string Photo
        {
            get { return _photo; }
            set { _photo = value; }
        }

    }

    public class StudentInfoCollection : Dictionary<string, StudentInfo>
    {
        public void AddStudent(StudentInfo student)
        {
            Add(student.Identity, student);
        }
    }
}
