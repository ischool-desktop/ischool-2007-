using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler;
using System.Xml;
using SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.DataType;
using SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.Converter;

namespace SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler
{
    public class ExportField : Field
    {
        private XmlElement _element;

        public XmlElement Element
        {
            get { return _element; }
            set { _element = value; }
        }

        private int _columnIndex;

        public int ColumnIndex
        {
            get { return _columnIndex; }
            set { _columnIndex = value; }
        }

        private string _XPath;

        public string XPath
        {
            get { return _XPath; }
            set { _XPath = value; }
        }

        private string _requestName;

        public string RequestName
        {
            get { return _requestName; }
            set { _requestName = value; }
        }

        private string _converter;

        public string Converter
        {
            get { return _converter; }
            set { _converter = value; }
        }

        private string _dataType;
        public string DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }        
    }
}
