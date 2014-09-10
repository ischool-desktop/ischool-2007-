using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.API.StudentExtension
{
    internal class Contact : Customization.Data.StudentExtension.ContactInfo
    {
        private string _ContactPhone;
        private string _PermenantPhone;
        private Address _MailingAddress;
        private Address _PermanentAddress;

        public Contact(string contactPhone, string permenantPhone, XmlElement mailingAddress, XmlElement permanentAddress)
        {
            _ContactPhone = contactPhone;
            _PermenantPhone = permenantPhone;
            _MailingAddress = new Address(mailingAddress);
            _PermanentAddress = new Address(permanentAddress);
        }
        #region ContactInfo 成員

        public string ContactPhone
        {
            get { return _ContactPhone; }
        }

        public SmartSchool.Customization.Data.StudentExtension.AddressInfo MailingAddress
        {
            get { return _MailingAddress; }
        }

        public SmartSchool.Customization.Data.StudentExtension.AddressInfo PermanentAddress
        {
            get { return _PermanentAddress; }
        }

        public string PermenantPhone
        {
            get { return _PermenantPhone; }
        }

        #endregion

        private class Address : Customization.Data.StudentExtension.AddressInfo
        {
            private string _County="";
            private string _DetailAddress = "";
            private string _Town = "";
            private string _ZipCode = "";

            public Address(XmlElement element)
            {
                try
                {
                    DSXmlHelper helper = new DSXmlHelper(element);
                    _County = helper.GetText("County");
                    _DetailAddress = helper.GetText("DetailAddress");
                    _Town = helper.GetText("Town");
                    _ZipCode = helper.GetText("ZipCode");
                }
                catch { }
            }

            #region AddressInfo 成員

            public string County
            {
                get { return _County; }
            }

            public string DetailAddress
            {
                get { return _DetailAddress; }
            }

            public string FullAddress
            {
                get { return (_ZipCode.Trim() == "" ? "" : _ZipCode.Trim() + " ") + _County + _Town + _DetailAddress; }
            }

            public string Town
            {
                get { return _Town; }
            }

            public string ZipCode
            {
                get { return _ZipCode; }
            }

            #endregion
        }
    }
}
