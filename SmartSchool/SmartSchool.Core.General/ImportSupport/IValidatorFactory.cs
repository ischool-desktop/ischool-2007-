using System;
using System.Collections.Generic;
using System.Text;
using DocValidate;

namespace SmartSchool.ImportSupport
{
    public interface IValidatorFactory : IFieldValidatorFactory, IRowValidatorFactory
    {
    }
}
