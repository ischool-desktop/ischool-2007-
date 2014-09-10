using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.PlugIn;
using System.Windows.Forms;
using SmartSchool.Customization.PlugIn.ExtendedContent;

namespace SmartSchool.Payment
{
    public static class ModuleMain
    {
        public const string PreferenceIdentity = "SmartSchool.Payment.ModuleConfig";

        [MainMethod()]
        public static void Main()
        {
            MotherForm.Instance.AddEntity(new PaymentEntity());

            //新的 Process Tab
            MotherForm.Instance.AddProcess(new PaymentRibbonBar());
            //毛毛蟲 (很噁心的那種…)
            ExtendStudentContent.AddItem(new Content.PaymentPalmerworm());
        }
    }
}