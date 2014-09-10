using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Payment.AccountStatedModules
{
    class LogEntry
    {
        public LogEntry(string entry)
        {
            _all_field = new Dictionary<string, string>();

            string str = entry;
            _all_field.Add("業者代號", str.Substring(1, 8).Trim());
            _all_field.Add("代收店號", str.Substring(9, 8).Trim());
            _all_field.Add("轉帳類別", str.Substring(17, 3).Trim());
            _all_field.Add("繳費日期", str.Substring(20, 6).Trim());
            _all_field.Add("轉帳代繳帳號", str.Substring(26, 14).Trim());
            _all_field.Add("代收金額", str.Substring(40, 14).Trim());
            _all_field.Add("代收機構代號", str.Substring(54, 8).Trim());
            _all_field.Add("扣繳狀況", str.Substring(62, 2).Trim());
            _all_field.Add("客戶編號", str.Substring(64, 17).Trim());
            _all_field.Add("列帳日期", str.Substring(81, 4).Trim());
            _all_field.Add("扣款人統編/ID", str.Substring(85, 10).Trim());
            _all_field.Add("預計入帳日期", str.Substring(95, 6).Trim());
            _all_field.Add("此筆是否為沖正", str.Substring(101, 1).Trim());
            _all_field.Add("原繳費日期", str.Substring(102, 6).Trim());
            _all_field.Add("超商代收項目/銀行PD子通路/郵局臨櫃交易代號", str.Substring(108, 4).Trim());
            _all_field.Add("通路代收日", str.Substring(112, 6).Trim());
            _all_field.Add("客戶通路手續費", str.Substring(118, 7).Trim());

            _fee = decimal.Parse(AllField["代收金額"]);
            _channel_charge = decimal.Parse(AllField["客戶通路手續費"].Substring(0, 5)) + (decimal.Parse(AllField["客戶通路手續費"].Substring(5, 2)) / 100);
            _va = AllField["客戶編號"];
            _channel_code = AllField["代收機構代號"].Trim(); ;

            int year = int.Parse(AllField["繳費日期"].Substring(0, 2));
            int month = int.Parse(AllField["繳費日期"].Substring(2, 2));
            int day = int.Parse(AllField["繳費日期"].Substring(4, 2));
            _payDate = new DateTime(year + 1911, month, day);
        }

        private Dictionary<string, string> _all_field;
        public Dictionary<string, string> AllField
        {
            get { return _all_field; }
        }

        private decimal _fee;
        public decimal Fee
        {
            get { return _fee; }
        }

        private decimal _channel_charge;
        public decimal ChannelCharge
        {
            get { return _channel_charge; }
        }

        private string _channel_code;
        public string ChannelCode
        {
            get
            {
                if (_channel_code == "2")
                    return "POST";

                if (_channel_code == "3" ||
                    _channel_code == "4" ||
                    _channel_code == "6" ||
                    _channel_code == "7" ||
                    _channel_code == "8")
                    return "SHOP";

                if (_channel_code == "5")
                    return "ATM";

                return "Other";
            }
        }

        private string _va;
        public string VirtualAccount
        {
            get { return _va; }
        }

        private DateTime _payDate;
        public DateTime PayDate
        {
            get { return _payDate; }
        }

        public XmlElement SerialToXml()
        {
            DSXmlHelper helper = new DSXmlHelper("Content");
            foreach (KeyValuePair<string, string> each in AllField)
            {
                XmlElement field = helper.AddElement(".", "Field");
                field.SetAttribute("Name", each.Key);
                field.SetAttribute("Value", each.Value);
            }

            return helper.BaseElement;
        }

        public string GetSchoolCode()
        {
            return VirtualAccount.Substring(9, 1);
        }
    }
}
