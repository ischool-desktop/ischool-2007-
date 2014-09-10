using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceProcess;
using System.Configuration.Install;
using System.ComponentModel;

namespace SmartSchool.Payment.AccountStatedService
{
    [RunInstallerAttribute(true)]
    public class PaymentServiceInstaller : Installer
    {
        private ServiceInstaller paymentTService;
        private ServiceProcessInstaller pinstaller;

        public PaymentServiceInstaller()
        {
            paymentTService = new ServiceInstaller();
            pinstaller = new ServiceProcessInstaller();

            pinstaller.Account = ServiceAccount.LocalSystem;
            paymentTService.StartType = ServiceStartMode.Automatic;
            paymentTService.ServiceName = ServiceProgram.ServiceName;

            Installers.Add(paymentTService);
            Installers.Add(pinstaller);
        }
    }
}
