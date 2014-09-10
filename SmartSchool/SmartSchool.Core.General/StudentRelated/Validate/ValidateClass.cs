using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common.Validate;

namespace SmartSchool.StudentRelated.Validate
{
    public class ValidateClass : AbstractValidateStudent
    {
        public ValidateClass(params IValidater<BriefStudentData>[] extendValidate)
        {
            ExtendValidater.AddRange(extendValidate);
        }

        protected override bool ValidateStudent(BriefStudentData info)
        {
            if (info.RefClassID != "")
            {
                return true;
            }
            else
            {
                ErrorMessage = "學生：\"" +info.StudentNumber+ info.Name + "\"沒有班級";
                return false;
            }
        }
    }
}
