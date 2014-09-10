using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler
{
    public class ExportFieldCollection : IEnumerable<ExportField>
    {
        private List<ExportField> _fields;

        public ExportFieldCollection()
        {
            _fields = new List<ExportField>();
        }

        public int Add(ExportField field)
        {
            _fields.Add(field);
            return _fields.Count - 1;
        }

        public int Length
        {
            get { return _fields.Count; }
        }

        public ExportField Find(string fieldName)
        {
            foreach (ExportField field in _fields)
            {
                if (field.FieldName == fieldName)
                    return field;
            }
            return null;
        }

        #region IEnumerable<ExportField> ����

        public IEnumerator<ExportField> GetEnumerator()
        {
            return _fields.GetEnumerator();
        }

        #endregion

        #region IEnumerable ����

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _fields.GetEnumerator();
        }

        #endregion
    }
}
