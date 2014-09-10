using System;
using System.Collections.Generic;
using System.Text;
//using SmartPlugIn.StudentRecord.NameList;
using System.Xml;

namespace SmartSchool.GovernmentalDocument.NameList
{    
    /// <summary>
    /// 學籍異動名冊 格式介面
    /// </summary>
    class PermrecEntryFormat : AbstractEntryFormat
    {
        #region IEntityFormat 成員

        public override void Initialize(XmlElement element)
        {
            base.Initialize(element);

            // 處理異動原因或事項、異動日期
            ColumnInfo column = new ColumnInfo(element.GetAttribute("原因及事項"), 120);
            _attributes.Add("異動原因或事項", column);

            ColumnInfo column2 = new ColumnInfo(element.GetAttribute("異動日期"), 100);
            _attributes.Add("異動日期", column2);
        }
        #endregion
    }


    
}
