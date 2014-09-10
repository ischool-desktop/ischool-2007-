using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.ElectronicPaperImp;
using SmartSchool.ePaper;

namespace SmartSchool
{
    public static class Core_Program
    {
        public static void Init_System()
        {
            Customization.Data.SystemInformation.SetProvider(new API.Provider.SystemProvider());

            //電子報表的提供者。
            DispatcherProvider.Register("ischool", new DispatcherImp(), true);
        }
    }
}
