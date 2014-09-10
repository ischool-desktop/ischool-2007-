//using System;
//using System.Collections.Generic;
//using System.Text;
//using SmartSchool.Common.Validate;

//namespace SmartSchool.StudentRelated.Validate
//{
//    public class ValidateGraduationPlan : AbstractValidateStudent
//    //{
//    //    public ValidateGraduationPlan(AbstractValidateStudent complex) : this()
//    //    {
//    //        this.ComplexValidate = complex;
//    //    }

//    //    public ValidateGraduationPlan()
//    //    {
//    //    }

//    //    protected override bool CheckInfo(BriefStudentData student)
//    //    {
//    //        if (student.RefGraduationPlanID != "")
//    //            return true;
//    //        else
//    //            return false;
//    //    }

//    //    protected override string ErrorResponse
//    //    {
//    //        get { return "課程規劃表資料錯誤"; }
//    //    }
//    {
//        public ValidateGraduationPlan(params IValidater<BriefStudentData>[] extendValidate)
//        {
//            ExtendValidater.AddRange(extendValidate);
//        }
//        protected override bool ValidateStudent(BriefStudentData info)
//        {
//            if (info.RefGraduationPlanID != "")
//            {
//                if (new SmartSchool.GraduationPlanRelated.Validate.ValidateGraduationPlanInfo().Validate(info.GraduationPlanInfo, null))
//                    return true;
//                else
//                {
//                    ErrorMessage = (info.ClassName == "" ? info.StudentNumber : info.ClassName + (info.SeatNo == "" ? "" : "[" + info.SeatNo + "]")) + "\"" + info.Name + "\"的課程規劃表\""+info.GraduationPlanName+"\"驗證失敗";
//                    return false;
//                }
//            }
//            else
//            {
//                ErrorMessage = (info.ClassName==""?info.StudentNumber : info.ClassName + (info.SeatNo == "" ? "" : "[" + info.SeatNo + "]")) + "\"" + info.Name + "\"沒有課程規劃表";
//                return false;
//            }
//        }
//    }
//}
