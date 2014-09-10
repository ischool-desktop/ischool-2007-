using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.GovernmentalDocument.NameList
{
    class GraduateEntryFormat : AbstractEntryFormat
    {
        #region IEntityFormat ����

        public override void Initialize(XmlElement element)
        {
            base.Initialize(element);

            // �B�z��(��)�~�ҮѦr��
            ColumnInfo column2 = new ColumnInfo(element.GetAttribute("��(��)�~�ҮѦr��"), 100);
            _attributes.Add("��(��)�~�ҮѦr��", column2);

            _attributes.Remove("�~��");
        }

        public override string GradeYear
        {
            get
            {
                return "";
            }
        }

        public override string Group
        {
            get
            {
                return Department;
            }
        }
        #endregion
    }   
}
