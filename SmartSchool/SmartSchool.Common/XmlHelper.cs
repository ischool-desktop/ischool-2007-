/*
 * Create Date：2005/11/21
 * Last Update：2006/2/8
 * Author Name：YaoMing Huang
 */
using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Text;

namespace SmartSchool.Common
{
    using em = ErrorMessage;
    using System.Net;

    /// <summary>
    /// 代表Xml的資料，內容必需是有「根」的Xml內容。
    /// </summary>
    public class XmlHelper : XmlBaseObject
    {

        /// <summary>
        /// 建立一個空的文件，預設會有「根」名稱「Content」。
        /// </summary>
        public XmlHelper()
        {
            BaseNode = XmlHelper.LoadXml("<Content/>");
        }

        /// <summary>
        /// 依XmDocument的內容建立<see cref="DSXmlHelper"/>，XmlDocument物件不可以為Null。
        /// </summary>
        /// <param name="xmlDoc">要依據的XmlDocument物件。</param>
        public XmlHelper(XmlDocument xmlDoc)
        {
            if (xmlDoc == null)
                throw new ArgumentNullException("xmlDoc", em.Get("XmlDocNullReferenceNotSupport"));

            if (xmlDoc.DocumentElement == null)
                throw new Exception(em.Get("EmptyXmlDocument"));

            BaseNode = xmlDoc.DocumentElement;
        }

        /// <summary>
        /// 依XmlElement的內容建立物件。
        /// </summary>
        /// <param name="element">要依據的XmlElement物件。</param>
        public XmlHelper(XmlElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element", em.Get("ElementNullReferenceNotSupport"));

            if (element.NodeType != XmlNodeType.Element)
                throw new ArgumentException(em.Get("NodeTypeNotSupport"), "element");

            BaseNode = element;
        }

        /// <summary>
        /// 依指定的「根」元素名稱建立Document
        /// </summary>
        /// <param name="rootName">根元素的名稱，不可加任何的特殊符號</param>
        public XmlHelper(string rootName)
        {
            BaseNode = XmlHelper.LoadXml("<" + rootName + "/>");
        }

        /// <summary>
        /// 在指定的元素下，新增屬性，並指定值。
        /// </summary>
        /// <param name="xpath">要新增屬性的元素路徑。</param>
        /// <param name="name">屬性名稱。</param>
        /// <param name="value">屬性值。</param>
        /// <returns>XmlAttribute的新實體。</returns>
        public XmlAttribute SetAttribute(string xpath, string name, string value)
        {
            XmlAttribute att = CreateAttribute(name);
            att.InnerText = value;
            return SetAttribute(xpath, att);
        }

        /// <summary>
        /// 在指定的元素下，新增屬性。
        /// </summary>
        /// <param name="xpath">要新增屬性的元素路徑。</param>
        /// <param name="attribute">屬性物件。</param>
        /// <returns>XmlAttribute的新實體</returns>
        public XmlAttribute SetAttribute(string xpath, XmlAttribute attribute)
        {
            XmlElement[] xlElements = GetElements(xpath);
            XmlElement elm = null;

            if (xlElements.Length > 0)
                elm = xlElements[xlElements.Length - 1];
            else
                throw new ArgumentException(em.Get("XPathSyntaxError",
                    new Replace("XPath", xpath)), "xpath");

            elm.Attributes.Append(attribute);
            return attribute;

        }

        /// <summary>
        /// 新增空白元素(Empyt Element)到文件中
        /// </summary>
        /// <param name="newName">新元素名稱。</param>
        /// <returns>代表在<see cref="DSXmlHelper"/>物件中新元素的實體。</returns>
        /// <include file='Util30\LibDocument\DSXmlHelper.xml' path='Documents/Document[@Name="AddElement1"]/*'/>
        public XmlElement AddElement(string newName)
        {
            return AddElement(".", newName, null);
        }

        /// <summary>
        /// 在指定的元素下，新增空白子元素。
        /// </summary>
        /// <param name="xpath">要新增「空白子元素」的「父元素」路徑</param>
        /// <param name="newElement">要新增的元素物件</param>
        /// <returns>代表在<see cref="DSXmlHelper"/>物件中新元素的實體。</returns>
        /// <include file='Util30\LibDocument\DSXmlHelper.xml' path='Documents/Document[@Name="AddElement1"]/*'/>
        public XmlElement AddElement(string xpath, XmlElement newElement)
        {
            if (XmlDocument.ReferenceEquals(BaseNode.OwnerDocument, newElement.OwnerDocument))
                return (XmlElement)GetLastNode(xpath).AppendChild(newElement);
            else
            {
                XmlNode newNode = BaseNode.OwnerDocument.ImportNode(newElement, true);
                return (XmlElement)GetLastNode(xpath).AppendChild(newNode);
            }
        }

        /// <summary>
        /// 在指定的元素下，新增空白子元素。
        /// </summary>
        /// <param name="xpath">要新增「空白子元素」的「父元素」路徑。</param>
        /// <param name="newName">空白子元素名稱。</param>
        /// <returns>代表在<see cref="DSXmlHelper"/>物件中新元素的實體。</returns>
        /// <include file='Util30\LibDocument\DSXmlHelper.xml' path='Documents/Document[@Name="AddElement1"]/*'/>
        public XmlElement AddElement(string xpath, string newName)
        {
            return AddElement(xpath, newName, "");
        }

        /// <summary>
        /// 在指定的元素下，新增子元素，並指定文字資料。
        /// </summary>
        /// <param name="xpath">要新增「子元素」的「父元素」路徑。</param>
        /// <param name="newName">子元素名稱。</param>
        /// <param name="text">子元素文字資料。</param>
        /// <returns>代表在<see cref="DSXmlHelper"/>物件中新元素的實體。</returns>
        /// <include file='Util30\LibDocument\DSXmlHelper.xml' path='Documents/Document[@Name="AddElement1"]/*'/>
        public XmlElement AddElement(string xpath, string newName, string text)
        {
            return AddElement(xpath, newName, text, false);
        }

        /// <summary>
        /// 在指定的元素下，新增子元素，並指定文字資料。
        /// </summary>
        /// <param name="xpath">要新增「子元素」的「父元素」路徑。</param>
        /// <param name="newName">子元素名稱。</param>
        /// <param name="text">子元素文字資料。</param>
        /// <param name="isXmlContent">文字資料是否為 Xml 字串，可以是單一 Node 或 NodeList。</param>
        /// <returns>代表在<see cref="DSXmlHelper"/>物件中新元素的實體。</returns>
        /// <include file='Util30\LibDocument\DSXmlHelper.xml' path='Documents/Document[@Name="AddElement2"]/*'/>
        /// <remarks>其他AddElement範例可參考<see cref="DSXmlHelper.AddElement(string,string)">
        /// DSXmlHelper.AddElement</see>方法說明。</remarks>
        public XmlElement AddElement(string xpath, string newName, string text, bool isXmlContent)
        {
            XmlNodeList nlList = BaseNode.SelectNodes(xpath);
            XmlNode ndTarget = null;

            if (nlList.Count > 0)
                ndTarget = nlList[nlList.Count - 1];
            else
                throw new ArgumentException(em.Get("XPathSyntaxError",
                    new Replace("XPath", xpath)), "xpath");

            //Node to be added
            XmlElement elm = CreateElement(newName);
            if (isXmlContent)
                elm.InnerXml = text;
            else
                elm.InnerText = text;

            //加入這個節點			
            XmlNode newNode = ndTarget.AppendChild(elm);

            return newNode as XmlElement;
        }

        /// <summary>
        /// 增加Xml內容，可以是單一Node或NodeList。
        /// </summary>
        /// <param name="xpath">要新增Xml資料的的「父元素」路徑。</param>
        /// <param name="xmlString">要新增的Xml內容。</param>
        public void AddXmlString(string xpath, string xmlString)
        {
            if (!PathExist(xpath))
                throw new ArgumentException(em.Get("XPathSyntaxError",
                    new Replace("XPath", xpath)), "xpath");

            //XmlNode node = GetElement(xpath);
            //node.InnerXml = xmlString;

            //用這種方法不會把原有的資料清除。
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = true;
            xmldoc.LoadXml("<Root>" + xmlString + "</Root>");

            foreach (XmlNode n in xmldoc.DocumentElement.ChildNodes)
                AddElement(xpath, (XmlElement)n);
        }

        /// <summary>
        /// 在指定的元素下增加文字資料，如果指定的文字已存在，會被取代。
        /// </summary>
        /// <param name="xpath">元素路徑，如果路徑不存在會產生Exception。</param>
        /// <param name="text">要增加的文字資料。</param>
        public void AddText(string xpath, string text)
        {
            XmlText nText = BaseNode.OwnerDocument.CreateTextNode(text);

            GetLastNode(xpath).AppendChild(nText);
        }

        public void SetText(string xpath, string text)
        {
            GetLastNode(xpath).InnerText = text;
        }

        /// <summary>
        /// 取得指定元素下的文字資料。
        /// </summary>
        /// <param name="xpath">元素路徑。</param>
        /// <returns>元素下的文字資料，如果指定的元素不存在則回傳String.Emtpy(空字串)。</returns>
        public string GetText(string xpath)
        {
            XmlNode n = BaseNode.SelectSingleNode(xpath);

            if (n == null) return string.Empty;

            if (n is XmlAttribute)
                return n.Value;
            else
                return n.InnerText;
        }

        public int TryGetInteger(string xpath, int defaultValue)
        {
            XmlNode n = BaseNode.SelectSingleNode(xpath);

            if (n == null) return defaultValue;

            string strValue;
            if (n is XmlAttribute)
                strValue = n.Value;
            else
                strValue = n.InnerText;

            int intValue;
            if (int.TryParse(strValue, out intValue))
                return intValue;
            else
                return defaultValue;
        }

        public float TryGetFloat(string xpath, float defaultValue)
        {
            XmlNode n = BaseNode.SelectSingleNode(xpath);

            if (n == null) return defaultValue;

            string strValue;
            if (n is XmlAttribute)
                strValue = n.Value;
            else
                strValue = n.InnerText;

            float intValue;
            if (float.TryParse(strValue, out intValue))
                return intValue;
            else
                return defaultValue;
        }

        public decimal TryGetDecimal(string xpath, decimal defaultValue)
        {
            XmlNode n = BaseNode.SelectSingleNode(xpath);

            if (n == null) return defaultValue;

            string strValue;
            if (n is XmlAttribute)
                strValue = n.Value;
            else
                strValue = n.InnerText;

            decimal intValue;
            if (decimal.TryParse(strValue, out intValue))
                return intValue;
            else
                return defaultValue;
        }

        public bool TryGetBoolean(string xpath, bool defaultValue)
        {
            XmlNode n = BaseNode.SelectSingleNode(xpath);

            if (n == null) return defaultValue;

            string strValue;
            if (n is XmlAttribute)
                strValue = n.Value;
            else
                strValue = n.InnerText;

            bool intValue;
            if (bool.TryParse(strValue, out intValue))
                return intValue;
            else
                return defaultValue;
        }

        public string TryGetString(string xpath, string defaultValue)
        {
            string strValue = GetText(xpath);

            if (string.IsNullOrEmpty(strValue))
                return defaultValue;
            else
                return strValue;
        }

        /// <summary>
        /// 在指定的元素下增加CDATA文字資料。
        /// </summary>
        /// <param name="xpath">元素路徑。</param>
        /// <param name="text">要增加的CDATA文字資料。</param>
        public void AddCDataSection(string xpath, string text)
        {
            XmlCDataSection cdata = BaseNode.OwnerDocument.CreateCDataSection(text);
            GetLastNode(xpath).AppendChild(cdata);
        }

        /// <summary>
        /// 測試指定的元素是否存在。
        /// </summary>
        /// <param name="xpath">元素路徑。</param>
        /// <returns>Boolean值，已存在回傳True,不存在回傳False。</returns>
        public bool PathExist(string xpath)
        {
            return BaseNode.SelectSingleNode(xpath) != null;
        }

        /// <summary>
        /// 測試指定的元素的內容是否有CDATA。
        /// </summary>
        /// <param name="xpath">元素路徑。</param>
        /// <returns>Boolean值，是CDATA回傳True，非則回傳False。</returns>
        public bool HasCDataSection(string xpath)
        {
            XmlNode nTestNode = GetNode(xpath);

            if (nTestNode.HasChildNodes)
            {
                foreach (XmlNode nCData in nTestNode.ChildNodes)
                    if (nCData.NodeType == XmlNodeType.CDATA)
                        return true;
            }
            else
                return false;
            return false;
        }

        /// <summary>
        /// 取得元素物件，但僅取得符合「元素路徑」的第一個元素。
        /// </summary>
        /// <param name="xpath">元素路徑。</param>
        /// <returns>回傳的XmlElement實體。</returns>
        /// <exception cref="Exception">發生再xpath取出的物件不是元素(Element)時。</exception>
        public XmlElement GetElement(string xpath)
        {
            XmlNode nd = BaseNode.SelectSingleNode(xpath);

            if (nd != null && !(nd is XmlElement))
                throw new Exception("取得的資料不是一個元素(Element)。");

            //如果nd是Null，則會回傳Null(表示as運算失敗)
            return (nd as XmlElement);
        }

        public XmlDataObject GetValueObject(string xpath)
        {
            IXmlDataObject xmldata = new XmlDataObject();

            XmlNode node = GetElement(xpath);

            if (node != null)
                xmldata.Initialize(node);
            else
                return null;

            return xmldata as XmlDataObject;
        }

        public T GetValueObject<T>(string xpath)
            where T : class, IXmlDataObject, new()
        {
            IXmlDataObject xmldata = new T();

            XmlNode node = GetElement(xpath);

            if (node != null)
                xmldata.Initialize(node);
            else
                return null;

            return xmldata as T;
        }

        public XmlDataObject[] GetValueObjects(string xpath)
        {
            List<XmlDataObject> xmldatas = new List<XmlDataObject>();

            foreach (XmlElement element in GetElements(xpath))
            {
                IXmlDataObject data = new XmlDataObject();
                data.Initialize(element);

                xmldatas.Add(data as XmlDataObject);
            }

            return xmldatas.ToArray();
        }

        public T[] GetValueObjects<T>(string xpath)
            where T : class, IXmlDataObject, new()
        {
            List<T> xmldatas = new List<T>();

            foreach (XmlElement element in GetElements(xpath))
            {
                IXmlDataObject data = new XmlDataObject();
                data.Initialize(element);

                xmldatas.Add(data as T);
            }

            return xmldatas.ToArray();
        }

        /// <summary>
        /// 取得元素物件，但僅取得符合「元素路徑」的第一個元素。
        /// </summary>
        /// <param name="xpath">元素路徑。</param>
        /// <returns>回傳的 XmlAttribute 實體。</returns>
        /// <exception cref="Exception">發生在xpath所取得的資料不是屬性時。</exception>
        public string GetAttribute(string xpath)
        {
            XmlNode nd = BaseNode.SelectSingleNode(xpath);

            if (nd != null && !(nd is XmlAttribute))
                throw new Exception("取得的資料不是一個屬性！");

            //如果nd是Null，則會回傳Null(表示as運算失敗)
            return (nd as XmlAttribute).Value;
        }

        /// <summary>
        /// 取得指定元素下的CDATA資料，如果有數個CDATA資料，則會回傳第一個。
        /// </summary>
        /// <param name="xpath">元素路徑。</param>
        /// <returns>如果有CDATA，回傳文字資料，沒有回傳String.Emtpy。</returns>
        public string GetCDataSection(string xpath)
        {
            XmlNode nCData = GetNode(xpath);

            if (!nCData.HasChildNodes)
                return string.Empty;
            else
            {
                foreach (XmlNode n in nCData.ChildNodes)
                    if (n.NodeType == XmlNodeType.CDATA)
                        return n.InnerText;
            }
            return string.Empty;
        }

        /// <summary>
        /// 取得元素物件陣列，將會取得所有符合「元素路徑」的所有元素。
        /// </summary>
        /// <param name="xpath">元素路徑。</param>
        /// <returns>XmlElement的陣列。</returns>
        public XmlElement[] GetElements(string xpath)
        {
            XmlNodeList ndl = BaseNode.SelectNodes(xpath);

            XmlElement[] result = new XmlElement[ndl.Count];
            for (int i = 0; i < ndl.Count; i++)
                result[i] = (XmlElement)ndl[i];
            return result;
        }

        /// <summary>
        /// 移除元素，元素不存在會產生Exception。
        /// </summary>
        /// <param name="xpath">要移除的元素路徑。</param>
        public void RemoveElement(string xpath)
        {
            XmlNode ndToBeRemoved = (XmlNode)(BaseNode.SelectSingleNode(xpath));

            if (ndToBeRemoved == null)
                throw new XmlProcessException("指定的路徑不存在。(" + xpath + ")");

            if (ndToBeRemoved is XmlAttribute)
            {
                XmlAttribute att = ndToBeRemoved as XmlAttribute;
                att.OwnerElement.RemoveAttributeNode(att);
            }
            else
                ndToBeRemoved.ParentNode.RemoveChild(ndToBeRemoved);
        }

        /// <summary>
        /// 取得目前文件的基礎XmlElement物件。
        /// </summary>
        /// <returns>此物件的基本XmlElement物件。</returns>
        public XmlElement BaseElement
        {
            get
            {
                return BaseNode;
            }
        }

        /// <summary>
        /// 文件根名稱。
        /// </summary>
        public string RootName
        {
            get
            {
                if (BaseNode == null) return "";
                return BaseNode.LocalName;
            }
        }

        /// <summary>
        /// 將特定的節點轉換成字串(取InnerXml)。
        /// </summary>
        /// <param name="xpath">XPath路徑。</param>
        /// <returns>Xml字串。</returns>
        public string ToString(string xpath)
        {
            return GetNode(xpath).OuterXml;
        }

        /// <summary>
        /// 回傳完整的Xml字串。
        /// </summary>
        /// <returns>完整的Xml字串。</returns>
        public override string ToString()
        {
            return GetNode(".").OuterXml;
        }

        private XmlNode GetNode(string xpath)
        {
            XmlNode n = BaseNode.SelectSingleNode(xpath);

            if (n == null)
                throw new ArgumentException(em.Get("XPathSyntaxError",
                    new Replace("XPath", xpath)), "xpath");

            return n;
        }

        private XmlNode GetLastNode(string xpath)
        {
            XmlNodeList nlList = BaseNode.SelectNodes(xpath);
            XmlNode ndTarget = null;

            if (nlList.Count > 0)
                ndTarget = nlList[nlList.Count - 1];
            else
                throw new ArgumentException(em.Get("XPathSyntaxError",
                    new Replace("XPath", xpath)), "xpath");

            return ndTarget;
        }

        private XmlAttribute CreateAttribute(string name)
        {
            XmlAttribute xmlbute = BaseNode.OwnerDocument.CreateAttribute(name);
            return xmlbute;
        }

        private XmlElement CreateElement(string name)
        {
            XmlElement xmlent = BaseNode.OwnerDocument.CreateElement(name);
            return xmlent;
        }

        #region
        /// <summary>
        /// 格式化 Xml 內容。
        /// </summary>
        /// <returns></returns>
        public static string Format(string xmlContent)
        {
            MemoryStream ms = new MemoryStream();

            XmlTextWriter writer = new XmlTextWriter(ms, System.Text.Encoding.UTF8);

            writer.Formatting = Formatting.Indented;
            writer.Indentation = 1;
            writer.IndentChar = '\t';

            XmlReader Reader = GetXmlReader(xmlContent);
            writer.WriteNode(Reader, true);
            writer.Flush();
            Reader.Close();

            ms.Position = 0;
            StreamReader sr = new StreamReader(ms, System.Text.Encoding.UTF8);

            string Result = sr.ReadToEnd();
            sr.Close();

            writer.Close();
            ms.Close();

            return Result;
        }

        private static XmlReader GetXmlReader(string XmlData)
        {
            XmlReaderSettings setting = new XmlReaderSettings();
            setting.IgnoreWhitespace = true;

            XmlReader Reader = XmlReader.Create(new StringReader(XmlData), setting);

            return Reader;
        }

        /// <summary>
        /// 複製XmlElement物件，變更其內容不會反應到原來的XmlElement中。
        /// </summary>
        /// <param name="srcElement">要複製的XmlElement物件。</param>
        /// <returns>已複製的XmlElement物件。</returns>
        public static XmlElement CloneElement(XmlElement srcElement)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = true;
            xmldoc.LoadXml(srcElement.OuterXml);

            return (XmlElement)xmldoc.DocumentElement;
        }

        /// <summary>
        /// 將Xml字串加入到XmlNode中，可處理不同XmlDocument物件的XmlNode。
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        public static XmlNode AppendChild(XmlNode parent, XmlNode child)
        {
            if (XmlDocument.ReferenceEquals(parent.OwnerDocument, child.OwnerDocument))
                return parent.AppendChild(child);
            else
            {
                XmlNode nChild = parent.OwnerDocument.ImportNode(child, true);
                return parent.AppendChild(nChild);
            }
        }

        /// <summary>
        /// 將Xml字串加入到XmlNode中。
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="childXmlContent">包含「根」的Xml字串</param>
        public static XmlNode AppendChild(XmlNode parent, string childXmlContent)
        {
            XmlNode nChild = LoadXml(childXmlContent);

            nChild = parent.OwnerDocument.ImportNode(nChild, true);

            return parent.AppendChild(nChild);
        }

        /// <summary>
        /// 載入指定的 Xml 檔案。
        /// </summary>
        /// <param name="fileName">檔案名稱。</param>
        /// <returns><see cref="XmlElement"/>物件。</returns>
        public static XmlElement LoadXml(FileInfo file, bool preserveWhitespace)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = preserveWhitespace;
            xmldoc.Load(file.FullName);

            return xmldoc.DocumentElement;
        }

        /// <summary>
        /// 載入指定的 Xml 檔案。
        /// </summary>
        /// <param name="fileName">檔案名稱。</param>
        /// <returns><see cref="XmlElement"/>物件。</returns>
        public static XmlElement LoadXml(FileInfo file)
        {
            return LoadXml(File.ReadAllText(file.FullName), true);
        }

        /// <summary>
        /// 載入指定的 Xml 資料。
        /// </summary>
        /// <param name="xmlContent">要載入的 Xml 字串資料。</param>
        /// <returns><see cref="XmlElement"/>物件。</returns>
        public static XmlElement LoadXml(string xmlString)
        {
            return LoadXml(xmlString, true);
        }

        /// <summary>
        /// 載入指定的 Xml 資料。
        /// </summary>
        /// <param name="xmlString">要載入的 Xml 字串資料。</param>
        /// <param name="preserveWhitespace">是否保留字串中的泛空白字元。</param>
        /// <returns><see cref="XmlElement"/>物件。</returns>
        public static XmlElement LoadXml(string xmlString, bool preserveWhitespace)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = preserveWhitespace;
            xmldoc.LoadXml(xmlString);

            return xmldoc.DocumentElement;
        }

        /// <summary>
        /// 將指定的 Xml 資料以 UTF-8 的編碼方式儲存到檔案。
        /// </summary>
        /// <param name="fileName">檔案名稱。</param>
        /// <param name="elm">要儲存的 Xml 物件。</param>
        public static void SaveXml(string fileName, XmlNode node)
        {
            SaveXml(fileName, node, Encoding.UTF8);
        }

        /// <summary>
        /// 將指定的 Xml 資料儲存到檔案。
        /// </summary>
        /// <param name="fileName">檔案名稱。</param>
        /// <param name="node">要儲存的 Xml 物件。</param>
        /// <param name="enc">儲存的編碼方式。</param>
        public static void SaveXml(string fileName, XmlNode node, Encoding enc)
        {
            File.WriteAllText(fileName, node.OuterXml, enc);
        }

        /// <summary>
        /// 將指定的 Xml 資料以UTF-8的編碼方式寫入到串流中。
        /// </summary>
        /// <param name="outStream">指定的串流。</param>
        /// <param name="node">要輸出的 Xml 物件。</param>
        public static void SaveXml(Stream outStream, XmlNode node)
        {
            SaveXml(outStream, node, Encoding.UTF8);
        }

        /// <summary>
        /// 將指定的 Xml 資料寫入到串流中。
        /// </summary>
        /// <param name="outStream">指定的串流。</param>
        /// <param name="node">要輸出的 Xml 物件。</param>
        /// <param name="enc">輸出的編碼方式。</param>
        public static void SaveXml(Stream outStream, XmlNode node, Encoding enc)
        {
            StreamWriter sw = new StreamWriter(outStream, enc);
            sw.Write(node.OuterXml);
        }

        /// <summary>
        /// 傳送Xml內容到某個網址。
        /// </summary>
        /// <param name="url">目的URL。</param>
        /// <param name="xmlContent">要傳送的Xml內容。</param>
        /// <returns>回傳的Xml資料。</returns>
        public static string HttpSendTo(string url, string xmlContent)
        {
            return HttpSendTo(url, "POST", xmlContent);
        }

        /// <summary>
        /// 傳送Xml內容到某個網址。
        /// </summary>
        /// <param name="url">目的URL。</param>
        /// <param name="method">傳送的方法(POST、GET)</param>
        /// <param name="xmlContent">要傳送的Xml內容。</param>
        /// <returns>回傳的Xml資料。</returns>
        public static string HttpSendTo(string url, string method, string xmlContent)
        {
            //建立Http連線
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(url);

            //基本設定
            httpReq.Method = method;
            httpReq.ContentType = "text/xml";

            if (method == "POST")
            {
                //寫入Request主體
                StreamWriter reqWriter = new StreamWriter(httpReq.GetRequestStream(), Encoding.UTF8);
                reqWriter.Write(xmlContent);
                reqWriter.Close();
            }
            else if (method == "GET")
            {
                httpReq = (HttpWebRequest)WebRequest.Create(url + "?" + xmlContent);
            }
            else
            {
                throw new InvalidOperationException(em.Get("HttpMethodNotSupported", new Replace("Method", method)));
            }

            //取得Response
            WebResponse httpRsp = null;
            try
            {
                httpRsp = httpReq.GetResponse();
            }
            catch (WebException E)
            {
                throw new ServerFailException(em.Get("GetHttpResponseError"), E);
            }

            StreamReader rspStream;

            try
            {
                rspStream = new StreamReader(httpRsp.GetResponseStream(), Encoding.UTF8);
            }
            catch (Exception e)
            {
                throw new ServerFailException(em.Get("GetHTTPResponseStreamError"), e);
            }

            string result = rspStream.ReadToEnd();
            rspStream.Close(); //這個要記得關閉。

            return result;
        }
        #endregion
    }
}