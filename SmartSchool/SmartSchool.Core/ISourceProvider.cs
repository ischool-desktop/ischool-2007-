using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Common
{
    public interface ISourceProvider<T, S>
    {
        List<T> Source { get;set;}
        event EventHandler SourceChanged;
        bool DisplaySource { get;}
        bool ImmediatelySearch { get;}
        S SearchProvider { get;}
        string SearchWatermark { get;}
    }
}
