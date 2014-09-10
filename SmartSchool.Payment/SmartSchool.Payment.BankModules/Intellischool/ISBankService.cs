using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Payment.Interfaces;
using System.Xml;

namespace SmartSchool.Payment.BankModules.Intellischool
{
    public class ISBankService //: IBankService
    {
        #region IBankService 成員

        public string Name
        {
            get { return "澔學中央銀行"; }
        }

        public string ModuleCode
        {
            get { return "Intellischool"; }
        }

        public BankConfigPane CreateBankConfigPane(BankConfig previousConf)
        {
            BankConfigPane pane = new ISBankConfigPane();
            pane.SetConfig(previousConf);
            return pane;
        }

        //public BillConfigPane CreateBillConfigPane(BillConfig previousConf)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        public IBillCodeGenerator CreateGenerator(BankConfig bankConf, DateTime expireation)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IBankService 成員

        public int GetAmountLimit(BankConfig bankConf)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
