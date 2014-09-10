using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Common.Validate
{
    public interface IValidater<T>
    {
        bool Validate(T info,IErrorViewer responseViewer);
        List<IValidater<T>> ExtendValidater { get;}
    }
}
