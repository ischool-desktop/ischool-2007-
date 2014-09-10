using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace SmartSchool.Payment.Interfaces
{
    public interface IBankService
    {
        /// <summary>
        /// 取得銀行名稱，用於識別銀行。
        /// </summary>
        string Name { get;}

        /// <summary>
        /// 模組代碼，用於識別銀行模組。
        /// </summary>
        string ModuleCode { get;}

        int GetAmountLimit(BankConfig bankConf);

        BankConfigPane CreateBankConfigPane(BankConfig previousConf);

        IBillCodeGenerator CreateGenerator(BankConfig bankConf, DateTime expireation);
    }
}
