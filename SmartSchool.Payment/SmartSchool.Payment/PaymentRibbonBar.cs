using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Payment.BankManagement;
using SmartSchool.Feature.Payment;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.ImportSupport;

namespace SmartSchool.Payment
{
    public partial class PaymentRibbonBar : UserControl, IProcess
    {
        public PaymentRibbonBar()
        {
            InitializeComponent();
        }

        #region IProcess 成員

        private double level = 7;
        public double Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }

        public DevComponents.DotNetBar.RibbonBar ProcessRibbon
        {
            get { return rbPayment; }
        }

        public string ProcessTabName
        {
            get { return "總務作業"; }
        }

        #endregion

        private void btnPaymentManage_Click(object sender, EventArgs e)
        {
            new PaymentManage().ShowDialog();
        }

        private void btnBank_Click(object sender, EventArgs e)
        {
            ManageForm manage = new ManageForm();
            manage.ShowDialog();
        }

        private void btnCheckPay_Click(object sender, EventArgs e)
        {
            new PayStatusCheck().ShowDialog();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            //string paymentId = GetPaymentID();
            //DSXmlHelper helper = new DSXmlHelper("Request");
            //List<string> studentIdList = new List<string>();

            //while (ImportSource.MoveNext())
            //{
            //    DSXmlHelper eachDetail = new DSXmlHelper(helper.AddElement("PaymentDetail"));
            //    string studentId = StudentLookup.GetStudentID(ImportSource.GetString(ImportSource.IdentifyField));
            //    studentIdList.Add(studentId);

            //    if (Mode == ImportMode.Insert)
            //    {
            //        eachDetail.AddElement(".", "RefStudentID", studentId);
            //        eachDetail.AddElement(".", "RefPaymentID", paymentId);
            //    }
            //    else if (Mode == ImportMode.Update)
            //    {
            //        eachDetail.AddElement("Condition");
            //        eachDetail.AddElement("Condition", "RefStudentID", studentId);
            //        eachDetail.AddElement("Condition", "RefPaymentID", paymentId);
            //    }
            //    else
            //        throw new ArgumentException("沒有這種的啦！");

            //    eachDetail.AddElement(".", "Amount", ImportSource.GetString(TOTAL_AMOUNT_FIELD_NAME));

            //    //PaymentItems 欄位。
            //    DSXmlHelper items = new DSXmlHelper(eachDetail.AddElement("PaymentItems"));
            //    items = new DSXmlHelper(items.AddElement("PaymentItems")); //建立第二層。
            //    foreach (string eachItem in ImportSource.MoneyFields)
            //    {
            //        //「總金額」為特殊欄位，不算在「金額項目」中。
            //        if (eachItem == TOTAL_AMOUNT_FIELD_NAME) continue;

            //        string mergeName = SheetHelper.ToNormalField(eachItem);
            //        string amount = ImportSource.GetString(eachItem);

            //        if (string.IsNullOrEmpty(amount)) continue;

            //        XmlElement item = items.AddElement(".", "Item");
            //        item.SetAttribute("Name", mergeName);
            //        item.SetAttribute("Value", amount);
            //    }

            //    //MergeFields 欄位。
            //    DSXmlHelper mfields = new DSXmlHelper(eachDetail.AddElement("MergeFields"));
            //    mfields = new DSXmlHelper(mfields.AddElement("MergeFields")); //建立第二層。
            //    foreach (string eachItem in ImportSource.MergeFields)
            //    {
            //        string mergeName = SheetHelper.ToNormalField(eachItem);
            //        XmlElement field = mfields.AddElement(".", "Item");
            //        field.SetAttribute("Name", mergeName);
            //        field.SetAttribute("Value", ImportSource.GetString(eachItem));
            //    }
            //}
            //EditPayment.InsertPaymentDetails(helper);
        }
    }
}
