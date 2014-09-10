using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common.Validate;

namespace SmartSchool.StudentRelated.Validate
{
    public class ValidateGradeYear : AbstractValidateStudent
    {
        public ValidateGradeYear(params IValidater<BriefStudentData>[] extendValidate)
        {
            ExtendValidater.AddRange(extendValidate);
        }
        protected override bool ValidateStudent(BriefStudentData info)
        {
            int t = 0;
            if (int.TryParse(info.GradeYear, out t))
            {
                return true;
            }
            else
            {
                ErrorMessage = (info.ClassName == "" ? info.StudentNumber : info.ClassName + (info.SeatNo == "" ? "" : "[" + info.SeatNo + "]")) + "\"" + info.Name + "\"沒有年級";
                return false;
            }
        }
    //    public ValidateGradeYear(AbstractValidateStudent complex) : this()
    //    {
    //        this.ComplexValidate = complex;
    //    }

    //    public ValidateGradeYear()
    //    {
    //    }

    //    protected override bool CheckInfo(BriefStudentData student)
    //    {
    //        int a;
    //        if (int.TryParse(student.GradeYear, out a))
    //            return true;
    //        else
    //            return false;
    //    }

    //    protected override string ErrorResponse
    //    {
    //        get { return "年級資料錯誤"; }
    //    }
    }
}
