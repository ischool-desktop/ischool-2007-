using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.GovernmentalDocument.NameList
{
    class YianXiouGraduateEntryFormat:AbstractEntryFormat
    {
        #region IEntityFormat 成員

        public override void Initialize(XmlElement element)
        {
            base.Initialize(element);

            // 處理最後學籍核准文號、畢(結)業證書字號
            ColumnInfo column = new ColumnInfo(element.GetAttribute("最後學籍核准文號"), 120);
            _attributes.Add("最後學籍核准文號", column);

            ColumnInfo column2 = new ColumnInfo(element.GetAttribute("畢(結)業證書字號"), 100);
            _attributes.Add("畢(結)業證書字號", column2);

            _attributes.Remove("年級");
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
