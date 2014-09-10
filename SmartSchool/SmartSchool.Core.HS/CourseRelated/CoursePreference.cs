using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace SmartSchool.CourseRelated
{
    internal class CoursePreference
    {
        private XmlElement _preference;

        public CoursePreference(XmlElement preference)
        {
            _preference = preference;
        }

        public string GetString(string name, string defaultValue)
        {
            XmlNode node = _preference.SelectSingleNode(name);

            if (node != null)
                return node.InnerText;
            else
                return defaultValue;
        }

        public int GetInteger(string name, int defaultValue)
        {
            XmlNode node = _preference.SelectSingleNode(name);

            if (node != null)
                return int.Parse(node.InnerText);
            else
                return defaultValue;
        }

        public bool GetBoolean(string name, bool defaultValue)
        {
            XmlNode node = _preference.SelectSingleNode(name);

            if (node != null)
                return bool.Parse(node.InnerText);
            else
                return defaultValue;
        }

        public object GetObject(string name)
        {
            XmlNode node = _preference.SelectSingleNode(name);

            if (node != null)
            {
                try
                {
                    BinaryFormatter seriaer = new BinaryFormatter();
                    byte[] binaryData = Convert.FromBase64String(node.InnerText);
                    MemoryStream binaryStream = new MemoryStream(binaryData);

                    object obj = seriaer.Deserialize(binaryStream);

                    binaryStream.Close();

                    return obj;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
                return null;
        }

        public void SetString(string name, string value)
        {
            XmlNode node = _preference.SelectSingleNode(name);

            if (node != null)
                node.InnerText = value;
            else
            {
                XmlNode newNode = _preference.OwnerDocument.CreateElement(name);
                newNode.InnerText = value;
                _preference.AppendChild(newNode);
            }
        }

        public void SetInteger(string name, int value)
        {
            XmlNode node = _preference.SelectSingleNode(name);

            if (node != null)
                node.InnerText = value.ToString();
            else
            {
                XmlNode newNode = _preference.OwnerDocument.CreateElement(name);
                newNode.InnerText = value.ToString();
                _preference.AppendChild(newNode);
            }
        }

        public void SetBoolean(string name, bool value)
        {
            XmlNode node = _preference.SelectSingleNode(name);

            if (node != null)
                node.InnerText = value.ToString();
            else
            {
                XmlNode newNode = _preference.OwnerDocument.CreateElement(name);
                newNode.InnerText = value.ToString();
                _preference.AppendChild(newNode);
            }
        }

        /// <summary>
        /// 要儲存的物件要標上「Serializable」屬性。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        public void SetObject(string name, object obj)
        {
            BinaryFormatter seriaer = new BinaryFormatter();
            MemoryStream binaryStream = new MemoryStream();
            byte[] binaryByte = new byte[1024];
            StringBuilder strBase64 = new StringBuilder();

            seriaer.Serialize(binaryStream, obj);

            binaryStream.Seek(0, SeekOrigin.Begin);
            int read_count = 0;
            while ((read_count = binaryStream.Read(binaryByte, 0, 1024)) > 0)
                strBase64.Append(Convert.ToBase64String(binaryByte, 0, read_count));

            binaryStream.Close();

            XmlNode node = _preference.SelectSingleNode(name);

            if (node != null)
                node.InnerText = strBase64.ToString();
            else
            {
                XmlNode newNode = _preference.OwnerDocument.CreateElement(name);
                newNode.InnerText = strBase64.ToString();
                _preference.AppendChild(newNode);
            }
        }

        public XmlElement GetPreference()
        {
            return _preference;
        }
    }
}
