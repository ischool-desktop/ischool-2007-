using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.GovernmentalDocument.NameList
{
    /// <summary>
    /// 基本異動紀錄格式
    /// </summary>
    public interface IEntryFormat
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="element">異動紀錄的XmlElement</param>
        void Initialize(XmlElement element);

        #region 基本屬性
        /// <summary>
        /// 異動紀錄編號
        /// </summary>
        string ID { get;}

        /// <summary>
        /// 學號
        /// </summary>
        /// 
        string StudentNumber { get;}
        /// <summary>
        /// 姓名
        /// </summary>
        string Name { get;}
        
        /// <summary>
        /// 性別
        /// </summary>
        string Gender { get;}

        /// <summary>
        /// 科別
        /// </summary>
        string Department { get;}

        /// <summary>
        /// 年級
        /// </summary>
        string GradeYear { get;}

        #endregion
        /// <summary>
        /// 展示欄位
        /// </summary>
        Dictionary<string, ColumnInfo> DisplayColumns { get;}

        /// <summary>
        /// 群組判別屬性
        /// </summary>
        string Group { get;}
    }

    /// <summary>
    /// 欄位資訊
    /// </summary>
    public class ColumnInfo
    {
        private string _value;
        private int _width;

        public ColumnInfo(string value,int width)
        {
            _value = value;
            _width = width;
        }
        /// <summary>
        /// 欄位值
        /// </summary>
        public string Value 
        {
            get { return _value; }
        }
        /// <summary>
        /// 欄寬
        /// </summary>
        public int Width
        {
            get { return _width; }
        }
    }

    /// <summary>
    /// 基本異動紀錄格式
    /// </summary>
    public abstract class AbstractEntryFormat : IEntryFormat
    {
        protected XmlElement _element;
        protected Dictionary<string, ColumnInfo> _attributes;    

        /// <summary>
        /// 包含屬性
        /// </summary>
        public Dictionary<string, ColumnInfo> DisplayColumns
        {
            get { return _attributes; }
            set { _attributes = value; }
        }

        #region IEntityFormat 成員

        public virtual void Initialize(XmlElement element)
        {
            _element = element;
            _attributes = new Dictionary<string, ColumnInfo>();         

            // 學號
            ColumnInfo studentNumber = new ColumnInfo(_element.GetAttribute("學號"), 100);
            _attributes.Add("學號", studentNumber);

            // 姓名
            ColumnInfo name = new ColumnInfo(_element.GetAttribute("姓名"), 100);
            _attributes.Add("姓名", name);

            //  年級
            ColumnInfo gradeYear = new ColumnInfo(_element.ParentNode.SelectSingleNode("@年級").InnerText, 50);
            _attributes.Add("年級", gradeYear);

            // 科別
            ColumnInfo dept = new ColumnInfo(_element.ParentNode.SelectSingleNode("@科別").InnerText, 120);
            _attributes.Add("科別", dept);

        }

        public virtual string ID
        {
            get { return _element.GetAttribute("編號"); }
        }

        public virtual string StudentNumber
        {
            get { return _element.GetAttribute("學號"); }
        }

        public virtual string Name
        {
            get { return _element.GetAttribute("姓名"); }
        }

        public virtual string Gender
        {
            get { return _element.GetAttribute("性別"); }
        }

        public virtual string GradeYear
        {
            get { return _element.ParentNode.SelectSingleNode("@科別").InnerText; }
        }

        public virtual string Department
        {
            get { return _element.ParentNode.SelectSingleNode("@科別").InnerText; }
        }

        public virtual string Group
        {
            get { return GradeYear; }
        }
        #endregion

    }

    /// <summary>
    /// 基本異動紀錄格式
    /// </summary>
    public class BaseUpdateRecordEntry : AbstractEntryFormat
    {  
    }

    /// <summary>
    /// 異動紀錄類別工廠
    /// </summary>
    internal class EntryFormatFactory
    {
        /// <summary>
        /// 創造類別的instance
        /// </summary>
        /// <param name="type">類別名稱</param>
        /// <param name="element">異動紀錄節點</param>
        /// <returns>做好的 instance</returns>
        public static IEntryFormat CreateInstance(string type, XmlElement element)
        {
            IEntryFormat format;
            switch (type)
            {
                case "新生名冊":
                    format = new EnrollEntry();
                    break;
                case "轉入名冊":
                    format = new TransferEntry();
                    break;
                case "學籍異動名冊":
                    format = new PermrecEntryFormat();
                    break;
                case "延修生學籍異動名冊 ":
                    format = new YianXiouUpdateRecordEntryFormat();
                    break;
                case "延修生名冊":
                    format = new YianXiouNameListEntryFormat();
                    break;
                case "延修生畢業名冊 ":
                    format = new YianXiouGraduateEntryFormat();
                    break;
                case "畢業生名冊 ":
                    format = new GraduateEntryFormat();
                    break;
                default:
                    format = new BaseUpdateRecordEntry();
                    break;
            }
            format.Initialize(element);
            return format;
        }
    }
}
