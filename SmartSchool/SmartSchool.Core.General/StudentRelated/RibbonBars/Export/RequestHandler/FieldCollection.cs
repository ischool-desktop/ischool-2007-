using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler
{
    public class FieldCollection : IEnumerable<Field>
    {
        private IList<Field> _fields;

        public FieldCollection()
        {
            _fields = new List<Field>();
        }

        public void Add(Field field)
        {
            _fields.Add(field);
        }

        public Field Find(string fieldName)
        {
            foreach (Field field in _fields)
            {
                if (field.FieldName == fieldName)
                    return field;
            }
            return null;
        }

        public Field FindByDisplayText(string displayText)
        {
            foreach (Field field in _fields)
            {
                if (field.DisplayText == displayText)
                    return field;
            }
            return null;
        }

        public int Count
        {
            get { return _fields.Count; }
        }

        #region IEnumerable<Field> ����

        public IEnumerator<Field> GetEnumerator()
        {
            return _fields.GetEnumerator();
        }

        #endregion

        #region IEnumerable ����

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _fields.GetEnumerator();
        }

        #endregion
    }
}
