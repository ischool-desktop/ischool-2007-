using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;

namespace SmartSchool.GovernmentalDocument
{
    public enum Status { Ready, Busy }
    public interface IReportBuilder
    {
        void BuildReport(XmlElement source,string location);
        event ProgressChangedEventHandler ProgressChanged;
        event RunWorkerCompletedEventHandler Completed;
        Status Status { get; }
        string Description { get;}
        string Version { get;}
        string Copyright { get;}
        string ReportName { get;}
    }
}
