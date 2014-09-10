using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common.Validate;

namespace SmartSchool.StudentRelated.Validate
{
    public class ValidateBasic : AbstractValidateStudent
    {
        public ValidateBasic(params IValidater<BriefStudentData>[] extendValidate)
        {
            ExtendValidater.AddRange(extendValidate);
        }
        protected override bool ValidateStudent(BriefStudentData info)
        {
            return true;
        }
    }
}
