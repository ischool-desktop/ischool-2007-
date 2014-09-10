using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.GovernmentalDocument.NameList
{
    /// <summary>
    /// ��J�W�U��Ʈ榡��@
    /// </summary>
    public class TransferEntry : AbstractEntryFormat
    {
        #region IEntityFormat ����
        
        public override void Initialize(XmlElement element)
        {
            base.Initialize(element);

            // ��J�e�Ǯ�
            ColumnInfo pci = new ColumnInfo(element.GetAttribute("��J�e�Ǯ�"),100);
            _attributes.Add("��J�e�Ǯ�", pci);

            // ��J��]
            ColumnInfo pcr = new ColumnInfo(element.GetAttribute("��J��]"), 120);
            _attributes.Add("��J��]", pcr);
            
            // ��J���
            ColumnInfo pcd = new ColumnInfo(element.GetAttribute("��J���"), 100);
            _attributes.Add("��J���", pcd);
        }
        #endregion
    }
}
