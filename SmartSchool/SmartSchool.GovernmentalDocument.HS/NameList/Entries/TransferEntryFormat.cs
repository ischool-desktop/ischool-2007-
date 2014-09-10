using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.GovernmentalDocument.NameList
{
    /// <summary>
    /// 轉入名冊資料格式實作
    /// </summary>
    public class TransferEntry : AbstractEntryFormat
    {
        #region IEntityFormat 成員
        
        public override void Initialize(XmlElement element)
        {
            base.Initialize(element);

            // 轉入前學校
            ColumnInfo pci = new ColumnInfo(element.GetAttribute("轉入前學校"),100);
            _attributes.Add("轉入前學校", pci);

            // 轉入原因
            ColumnInfo pcr = new ColumnInfo(element.GetAttribute("轉入原因"), 120);
            _attributes.Add("轉入原因", pcr);
            
            // 轉入日期
            ColumnInfo pcd = new ColumnInfo(element.GetAttribute("轉入日期"), 100);
            _attributes.Add("轉入日期", pcd);
        }
        #endregion
    }
}
