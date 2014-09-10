using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common.Validate;
using SmartSchool.Common;

namespace SmartSchool.StudentRelated.Validate
{
    public abstract class AbstractValidateStudent : IValidater<BriefStudentData>
    {
        //private AbstractValidateStudent _complexValidate;

        //private string _ErrorMessage;

        //public AbstractValidateStudent ComplexValidate
        //{
        //    get { return _complexValidate; }
        //    set { _complexValidate = value; }
        //}

        //public string ErrorMessage
        //{
        //    get
        //    {
        //        return _ErrorMessage;
        //    }
        //}

        //public bool Validate(BriefStudentData student)
        //{
        //    bool result;

        //    if (CheckInfo(student))
        //    {
        //        _ErrorMessage = "";
        //        result = true;
        //    }
        //    else
        //    {
        //        _ErrorMessage = ErrorResponse;
        //        result = false;
        //    }

        //    if (_complexValidate == null)
        //        return result;
        //    else
        //    {
        //        bool nextResult = _complexValidate.Validate(student);
        //        _ErrorMessage += _ErrorMessage == "" ? "" : "\r\n";
        //        _ErrorMessage += _complexValidate.ErrorMessage;

        //        if (result && nextResult)
        //            return true;
        //        else
        //            return false;
        //    }
        //}

        //protected abstract bool CheckInfo(BriefStudentData student);

        //protected abstract string ErrorResponse { get;}
        private List<IValidater<BriefStudentData>> _ExtendValidater=new List<IValidater<BriefStudentData>>();

        private string _ErrorMessage;

        protected string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }

        protected abstract bool ValidateStudent(BriefStudentData info);

        public bool Validate(BriefStudentData info)
        {
            return Validate(info, null);
        }

        #region IValidater<BriefStudentData> 成員

        public virtual bool Validate(BriefStudentData info, IErrorViewer responseViewer)
        {
            bool pass = true;
            try
            {
                if (ValidateStudent(info))
                {
                    foreach (IValidater<BriefStudentData> extendValidater in _ExtendValidater)
                    {
                        pass &= extendValidater.Validate(info, responseViewer);
                    }
                }
                else
                {
                    if (responseViewer != null)
                        responseViewer.SetMessage(ErrorMessage);
                    pass = false;
                }
            }
            catch (Exception ex)
            {
                if (responseViewer != null)
                    responseViewer.SetMessage("學生：\"" + info.Name + "\"在驗證過程中發生未預期錯誤");
                SmartSchool.ExceptionHandler.BugReporter.ReportException("SmartSchool", CurrentUser.Instance.SystemVersion, ex, false);
                return false;
            }
            return pass;
        }


        public List<IValidater<BriefStudentData>> ExtendValidater
        {
            get { return _ExtendValidater; }
        }

        #endregion
    }
}
