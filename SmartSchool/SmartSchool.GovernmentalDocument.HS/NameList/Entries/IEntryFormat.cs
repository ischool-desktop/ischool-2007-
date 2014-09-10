using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool.GovernmentalDocument.NameList
{
    /// <summary>
    /// �򥻲��ʬ����榡
    /// </summary>
    public interface IEntryFormat
    {
        /// <summary>
        /// ��l��
        /// </summary>
        /// <param name="element">���ʬ�����XmlElement</param>
        void Initialize(XmlElement element);

        #region ���ݩ�
        /// <summary>
        /// ���ʬ����s��
        /// </summary>
        string ID { get;}

        /// <summary>
        /// �Ǹ�
        /// </summary>
        /// 
        string StudentNumber { get;}
        /// <summary>
        /// �m�W
        /// </summary>
        string Name { get;}
        
        /// <summary>
        /// �ʧO
        /// </summary>
        string Gender { get;}

        /// <summary>
        /// ��O
        /// </summary>
        string Department { get;}

        /// <summary>
        /// �~��
        /// </summary>
        string GradeYear { get;}

        #endregion
        /// <summary>
        /// �i�����
        /// </summary>
        Dictionary<string, ColumnInfo> DisplayColumns { get;}

        /// <summary>
        /// �s�էP�O�ݩ�
        /// </summary>
        string Group { get;}
    }

    /// <summary>
    /// ����T
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
        /// ����
        /// </summary>
        public string Value 
        {
            get { return _value; }
        }
        /// <summary>
        /// ��e
        /// </summary>
        public int Width
        {
            get { return _width; }
        }
    }

    /// <summary>
    /// �򥻲��ʬ����榡
    /// </summary>
    public abstract class AbstractEntryFormat : IEntryFormat
    {
        protected XmlElement _element;
        protected Dictionary<string, ColumnInfo> _attributes;    

        /// <summary>
        /// �]�t�ݩ�
        /// </summary>
        public Dictionary<string, ColumnInfo> DisplayColumns
        {
            get { return _attributes; }
            set { _attributes = value; }
        }

        #region IEntityFormat ����

        public virtual void Initialize(XmlElement element)
        {
            _element = element;
            _attributes = new Dictionary<string, ColumnInfo>();         

            // �Ǹ�
            ColumnInfo studentNumber = new ColumnInfo(_element.GetAttribute("�Ǹ�"), 100);
            _attributes.Add("�Ǹ�", studentNumber);

            // �m�W
            ColumnInfo name = new ColumnInfo(_element.GetAttribute("�m�W"), 100);
            _attributes.Add("�m�W", name);

            //  �~��
            ColumnInfo gradeYear = new ColumnInfo(_element.ParentNode.SelectSingleNode("@�~��").InnerText, 50);
            _attributes.Add("�~��", gradeYear);

            // ��O
            ColumnInfo dept = new ColumnInfo(_element.ParentNode.SelectSingleNode("@��O").InnerText, 120);
            _attributes.Add("��O", dept);

        }

        public virtual string ID
        {
            get { return _element.GetAttribute("�s��"); }
        }

        public virtual string StudentNumber
        {
            get { return _element.GetAttribute("�Ǹ�"); }
        }

        public virtual string Name
        {
            get { return _element.GetAttribute("�m�W"); }
        }

        public virtual string Gender
        {
            get { return _element.GetAttribute("�ʧO"); }
        }

        public virtual string GradeYear
        {
            get { return _element.ParentNode.SelectSingleNode("@��O").InnerText; }
        }

        public virtual string Department
        {
            get { return _element.ParentNode.SelectSingleNode("@��O").InnerText; }
        }

        public virtual string Group
        {
            get { return GradeYear; }
        }
        #endregion

    }

    /// <summary>
    /// �򥻲��ʬ����榡
    /// </summary>
    public class BaseUpdateRecordEntry : AbstractEntryFormat
    {  
    }

    /// <summary>
    /// ���ʬ������O�u�t
    /// </summary>
    internal class EntryFormatFactory
    {
        /// <summary>
        /// �гy���O��instance
        /// </summary>
        /// <param name="type">���O�W��</param>
        /// <param name="element">���ʬ����`�I</param>
        /// <returns>���n�� instance</returns>
        public static IEntryFormat CreateInstance(string type, XmlElement element)
        {
            IEntryFormat format;
            switch (type)
            {
                case "�s�ͦW�U":
                    format = new EnrollEntry();
                    break;
                case "��J�W�U":
                    format = new TransferEntry();
                    break;
                case "���y���ʦW�U":
                    format = new PermrecEntryFormat();
                    break;
                case "���ץ;��y���ʦW�U ":
                    format = new YianXiouUpdateRecordEntryFormat();
                    break;
                case "���ץͦW�U":
                    format = new YianXiouNameListEntryFormat();
                    break;
                case "���ץͲ��~�W�U ":
                    format = new YianXiouGraduateEntryFormat();
                    break;
                case "���~�ͦW�U ":
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
