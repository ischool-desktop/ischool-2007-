//using System;
//using System.Collections.Generic;
//using System.Text;
//using SmartSchool.Common.Validate;

//namespace SmartSchool.StudentRelated.Validate
//{
//    public class ValidateScoreCalcRule : AbstractValidateStudent
//    {
//        public ValidateScoreCalcRule(params IValidater<BriefStudentData>[] extendValidate)
//        {
//            ExtendValidater.AddRange(extendValidate);
//        }
//        protected override bool ValidateStudent(BriefStudentData info)
//        {
//            if (info.RefScoreCalcRuleID != "")
//            {
//                return true;
//            }
//            else
//            {
//                ErrorMessage = (info.ClassName == "" ? info.StudentNumber : info.ClassName + (info.SeatNo == "" ? "" : "[" + info.SeatNo + "]")) + "\"" + info.Name + "\"�S�����Z�p��W�h";
//                return false;
//            }
//        }
//    //    public ValidateScoreCalcRule(AbstractValidateStudent complex) : this()
//    //    {
//    //        this.ComplexValidate= complex;
//    //    }

//    //    public ValidateScoreCalcRule()
//    //    {
//    //    }

//    //    protected override bool CheckInfo(BriefStudentData student)
//    //    {
//    //        if (student.RefScoreCalcRuleID != "")
//    //            return true;
//    //        else
//    //            return false;
//    //    }

//    //    protected override string ErrorResponse
//    //    {
//    //        get { return "���Z�p��W�h��ƿ��~"; }
//    //    }
//    }
//}
