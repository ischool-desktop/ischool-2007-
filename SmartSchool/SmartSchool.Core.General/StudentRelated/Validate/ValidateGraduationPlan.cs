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
//    //        get { return "�ҵ{�W�����ƿ��~"; }
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
//                    ErrorMessage = (info.ClassName == "" ? info.StudentNumber : info.ClassName + (info.SeatNo == "" ? "" : "[" + info.SeatNo + "]")) + "\"" + info.Name + "\"���ҵ{�W����\""+info.GraduationPlanName+"\"���ҥ���";
//                    return false;
//                }
//            }
//            else
//            {
//                ErrorMessage = (info.ClassName==""?info.StudentNumber : info.ClassName + (info.SeatNo == "" ? "" : "[" + info.SeatNo + "]")) + "\"" + info.Name + "\"�S���ҵ{�W����";
//                return false;
//            }
//        }
//    }
//}
