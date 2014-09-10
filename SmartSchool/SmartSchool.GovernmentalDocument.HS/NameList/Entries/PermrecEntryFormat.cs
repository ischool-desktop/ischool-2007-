using System;
using System.Collections.Generic;
using System.Text;
//using SmartPlugIn.StudentRecord.NameList;
using System.Xml;

namespace SmartSchool.GovernmentalDocument.NameList
{    
    /// <summary>
    /// ���y���ʦW�U �榡����
    /// </summary>
    class PermrecEntryFormat : AbstractEntryFormat
    {
        #region IEntityFormat ����

        public override void Initialize(XmlElement element)
        {
            base.Initialize(element);

            // �B�z���ʭ�]�Ψƶ��B���ʤ��
            ColumnInfo column = new ColumnInfo(element.GetAttribute("��]�Ψƶ�"), 120);
            _attributes.Add("���ʭ�]�Ψƶ�", column);

            ColumnInfo column2 = new ColumnInfo(element.GetAttribute("���ʤ��"), 100);
            _attributes.Add("���ʤ��", column2);
        }
        #endregion
    }


    
}