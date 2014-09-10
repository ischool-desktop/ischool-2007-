using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.StudentRelated.RibbonBars.GMap
{
    public class AddressMark
    {
        public AddressMark(GMapHelper helper, AddressInfo markAddress, object markObject)
        {
            _helper = helper;
            _student = markAddress.Student;
            _mark_object = markObject;
            _address = markAddress;
        }

        private GMapHelper _helper;
        private GMapHelper Helper
        {
            get { return _helper; }
        }

        private object _mark_object;
        internal object MarkObject
        {
            get { return _mark_object; }
        }

        private StudentInfo _student;
        public StudentInfo Student
        {
            get { return _student; }
        }

        private AddressInfo _address;
        public AddressInfo Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public void PanToCenter()
        {
            Helper.PanToMark(MarkObject);
        }

        public void OpenInfoWindow(string html)
        {
            Helper.OpenInfoWindow(this.MarkObject, html);
        }

        public bool IsViewable
        {
            get { return Helper.IsViewable(MarkObject); }
        }
    }
}
