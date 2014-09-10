using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.StudentRelated.RibbonBars.GMap
{
    public class AddressInfo
    {
        public AddressInfo(StudentInfo ownerStudent, XmlElement addressData, AddressType addressType)
        {
            DSXmlHelper hlpAddress = new DSXmlHelper(addressData);

            _student = ownerStudent;
            _add_type = addressType;
            _county = hlpAddress.GetText("County");
            _town = hlpAddress.GetText("Town");
            _detail_address = hlpAddress.GetText("DetailAddress");
            _zip_code = hlpAddress.GetText("ZipCode");
            _lat = hlpAddress.GetText("Latitude");
            _lng = hlpAddress.GetText("Longitude");
        }

        private StudentInfo _student;
        public StudentInfo Student
        {
            get { return _student; }
        }

        private string _county;
        public string County
        {
            get { return _county; }
        }

        private string _town;
        public string Town
        {
            get { return _town; }
        }

        private string _detail_address;
        public string DetailAddress
        {
            get { return _detail_address; }
        }

        private string _zip_code;
        public string ZipCode
        {
            get { return _zip_code; }
        }

        private string _lat;
        public string Lat
        {
            get { return _lat; }
        }

        private string _lng;
        public string Lng
        {
            get { return _lng; }
        }

        public bool HasCoordinate
        {
            get { return !string.IsNullOrEmpty(Lat) && !string.IsNullOrEmpty(Lng); }
        }

        private AddressType _add_type;
        public AddressType AddressType
        {
            get { return _add_type; }
        }

        public static AddressType ParseAddressType(string addressType)
        {
            return (AddressType)Enum.Parse(typeof(AddressType), addressType, true);
        }

        internal string Full()
        {
            return string.Format("[{0}] {1}", ZipCode, County + Town + DetailAddress);
        }
    }

    public enum AddressType
    {
        None,
        PermanentAddress,
        MailingAddress,
        OtherAddresses
    }

    public class AddressCollection
    {
        private Dictionary<AddressType, AddressInfo> _addressList;

        public AddressCollection()
        {
            _addressList = new Dictionary<AddressType, AddressInfo>();
        }

        public void AddAddress(AddressInfo address)
        {
            _addressList.Add(address.AddressType, address);
        }

        public bool ContainsType(AddressType type)
        {
            return _addressList.ContainsKey(type);
        }

        public AddressInfo this[AddressType type]
        {
            get { return _addressList[type]; }
        }
    }
}
