using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;
using System.Xml;
//using SmartPlugIn.StudentRecord.NameList;

namespace SmartSchool.GovernmentalDocument.NameList
{
    /// <summary>
    /// �W�U�K�n
    /// </summary>
    internal interface ISummaryProvider
    {
        /// <summary>
        /// �������O
        /// </summary>
        string Type { get;}
        /// <summary>
        /// title
        /// </summary>
        string Title { get;}
        /// <summary>
        /// �]�t��O��T
        /// </summary>
        /// <returns></returns>
        Department[] GetDepartments();
        /// <summary>
        /// �֭�帹
        /// </summary>
        string ADNumber { get;}
        /// <summary>
        /// �֭���
        /// </summary>
        string ADDate { get;}
        /// <summary>
        /// �W�U�s��
        /// </summary>
        string ID { get;}

        IEntryFormat[] GetEntities();

    }

    internal class SummaryProvider : ISummaryProvider
    {
        private Dictionary<string, Department> _depts;
        private string _type;
        private string _adn;
        private string _add;
        private string _title;
        private string _id;
        private List<IEntryFormat> _entities;

        public SummaryProvider(DSXmlHelper helper)
        {
            _depts = new Dictionary<string, Department>();
            _entities = new List<IEntryFormat>();
            _id = helper.GetText("UpdateRecordBatch/@ID");
            _adn = helper.GetText("UpdateRecordBatch/ADNumber");
            _add = helper.GetText("UpdateRecordBatch/ADDate");
            _title = helper.GetText("UpdateRecordBatch/Name");
            XmlElement content = helper.GetElement("UpdateRecordBatch/Content/���ʦW�U");

            // �p�Gcontent ���Y�S���������
            if (content == null) return;

            _type = content.GetAttribute("���O");

            foreach (XmlNode node in content.SelectNodes("�M��/���ʬ���"))
            {
                IEntryFormat format = EntryFormatFactory.CreateInstance(_type, (XmlElement)node);
                _entities.Add(format);

                Department dept = null;
                if (_depts.ContainsKey(format.Department))
                    dept = _depts[format.Department];
                else
                {
                    dept = new Department();
                    dept.Name = format.Department;
                    _depts.Add(dept.Name, dept);
                }
                dept.Total++;

                switch (format.Gender)
                {
                    case "�k":
                        dept.Male++;
                        break;
                    case "�k":
                        dept.Female++;
                        break;
                    default:
                        dept.Unknow++;
                        break;
                }
            }
        }

        #region ISummaryProvider ����

        public string Type
        {
            get { return _type; }
        }

        public string Title
        {
            get { return _title; }
        }

        public Department[] GetDepartments()
        {
            List<Department> ds = new List<Department>();
            foreach (string key in _depts.Keys)
            {
                ds.Add(_depts[key]);
            }
            return ds.ToArray();
        }

        public string ADNumber
        {
            get { return _adn; }
        }

        public string ADDate
        {
            get { return _add; }
        }

        public IEntryFormat[] GetEntities()
        {
            return _entities.ToArray();
        }

        public string ID
        {
            get { return _id; }
        }
        #endregion
    }

    /// <summary>
    /// ��O��T
    /// </summary>
    public class Department
    {
        private string _name;
        /// <summary>
        /// ��O�W��
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _male;
        /// <summary>
        /// �k�ʤH��
        /// </summary>
        public int Male
        {
            get { return _male; }
            set { _male = value; }
        }
        private int _female;
        /// <summary>
        /// �k�ʤH��
        /// </summary>
        public int Female
        {
            get { return _female; }
            set { _female = value; }
        }
        private int _unknow;
        /// <summary>
        /// �����ʧO�H��
        /// </summary>
        public int Unknow
        {
            get { return _unknow; }
            set { _unknow = value; }
        }
        private int _total;
        /// <summary>
        /// �`�H��
        /// </summary>
        public int Total
        {
            get { return _total; }
            set { _total = value; }
        }

        public Department()
        {
            _total = 0;
            _male = 0;
            _female = 0;
            _name = "";
        }
    }
}
