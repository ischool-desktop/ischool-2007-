using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;
using System.IO;

namespace SmartSchool.Common
{
    public class XmlDataObject : IXmlDataObject
    {
        private XmlNode _xmlNode;
        private Dictionary<string, string> _values;

        #region IXmlDataObject Members

        void IXmlDataObject.Initialize(XmlNode baseXml)
        {
            _xmlNode = baseXml;

            _values = new Dictionary<string, string>(System.StringComparer.InvariantCultureIgnoreCase);
            foreach (XmlNode node in baseXml.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Element || node.NodeType == XmlNodeType.Attribute)
                {
                    if (_values.ContainsKey(node.LocalName))
                        throw new Exception("欄位名稱重覆。(" + node.LocalName + ")");
                    else
                        _values.Add(node.LocalName, node.InnerText);
                }
            }
        }

        #endregion

        protected XmlNode BaseNode
        {
            get { return _xmlNode; }
        }

        /// <summary>
        /// 取得指定元素的字串資料，如果元素不存在則回傳空字串(string.Empty)。
        /// </summary>
        /// <returns></returns>
        public string GetString(string name)
        {
            return GetString(name, "");
        }

        /// <summary>
        /// 取得指定元素的字串資料。
        /// </summary>
        /// <param name="defaultValue">如果指定的元素不存在時回傳的值。</param>
        /// <returns></returns>
        public string GetString(string name, string defaultValue)
        {
            if (_values.ContainsKey(name))
                return _values[name];
            else
                return defaultValue;
        }

        /// <summary>
        /// 取得指定元素的值，並轉型成數字，如果指定的元素不存在則回傳「0」。
        /// </summary>
        /// <returns></returns>
        public int GetInteger(string name)
        {
            return GetInteger(name, 0);
        }

        /// <summary>
        /// 取得指定元素的值，並轉型成數字，如果指定的元素不存在則回傳「0」。
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public int GetInteger(string name, int defaultValue)
        {
            int result;
            string value = GetString(name, defaultValue.ToString());

            if (int.TryParse(value, out result))
                return result;
            else
                return defaultValue;
        }

        /// <summary>
        /// 取得指定元素的值，並轉型成 Boolean ，如果指定的元素不存在則回傳「false」。
        /// </summary>
        /// <returns></returns>
        public bool GetBoolean(string name)
        {
            return GetBoolean(name, false);
        }

        /// <summary>
        /// 取得指定元素的值，並轉型成 Boolean 。
        /// </summary>
        /// <param name="defaultValue">指定的元素不存在時回傳的值。</param>
        /// <returns></returns>
        public bool GetBoolean(string name, bool defaultValue)
        {
            bool result;
            string value = GetString(name, defaultValue.ToString());

            if (bool.TryParse(value, out result))
                return result;
            else
            {
                switch (value.ToUpper())
                {
                    case "YES":
                        return true;
                    case "NO":
                        return false;
                    case "Y":
                        return true;
                    case "N":
                        return false;
                    case "T":
                        return true;
                    case "F":
                        return false;
                    default:
                        return defaultValue;
                }
            }
        }

        /// <summary>
        /// 取得指定元素的值，並轉換成圖型檔，如果元素不存在或是格式(base64)不正確，將回傳 Null 。
        /// </summary>
        /// <returns></returns>
        public Image GetImage(string name)
        {
            byte[] imgbinary = GetBinary(name);

            if (imgbinary.Length > 0)
            {
                MemoryStream imgstream = new MemoryStream(imgbinary);
                return new Bitmap(imgstream);
            }
            else
                return null;
        }

        /// <summary>
        /// 取得指定元素的值，並轉換成 Binary 資料，如果元素不存在或是格式(base64)錯誤，將回傳空陣列(空陣列不是 Null)。
        /// </summary>
        /// <returns></returns>
        public byte[] GetBinary(string name)
        {
            string value = GetString(name);

            if (string.IsNullOrEmpty(value))
                return new byte[] { };
            else
                return Convert.FromBase64String(value);
        }

        /// <summary>
        /// 回傳完整的 Xml 字串。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return BaseNode.OuterXml;
        }
    }
}
