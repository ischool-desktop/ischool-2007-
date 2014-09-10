using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Teacher
{
    [QueryRequest()]
    public class EditTeacher
    {
        public static DSResponse Update(DSRequest dsreq)
        {
            return FeatureBase.CallService("SmartSchool.Teacher.Update", dsreq);
        }
    }
}
