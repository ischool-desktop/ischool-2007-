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
                ErrorMessage = "�ǥ͡G\"" +info.StudentNumber+ info.Name + "\"�S���Z��";
                return false;
            }
        }
    }
}
