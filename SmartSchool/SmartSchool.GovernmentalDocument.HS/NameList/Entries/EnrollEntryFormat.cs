using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.GovernmentalDocument.NameList
{
    /// <summary>
    /// 新生名冊資料格式實作
    /// </summary>
    public class EnrollEntry : AbstractEntryFormat
    {      
        #region IEntityFormat 成員
        
        public override void Initialize(XmlElement element)
        {
            base.Initialize(element);
            
            // 處理入學資格代號
            ColumnInfo column = new ColumnInfo(element.GetAttribute("入學資格代號"), 100);
            _attributes.Add("入學資格代號", column);           
        }

        public override string Group
        {
            get { return Department; }
        }

        #endregion
    }
}
