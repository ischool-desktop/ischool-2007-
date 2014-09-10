using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Payment.AccountStatedService.Interfaces
{
    public interface ITransactionBridge
    {
        bool Transport(LogWriter log,string workingFolder);
    }
}
