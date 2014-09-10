/*
 * Create Date�G2005/11/21
 * Last Update�G2006/2/8
 * Author Name�GYaoMing Huang
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
    /// �N��Xml����ơA���e���ݬO���u�ڡv��Xml���e�C
    /// </summary>
    public class XmlHelper : XmlBaseObject
    {

        /// <summary>
        /// �إߤ@�ӪŪ����A�w�]�|���u�ڡv�W�١uContent�v�C
        /// </summary>
        public XmlHelper()
        {
            BaseNode = XmlHelper.LoadXml("<Content/>");
        }

        /// <summary>
        /// ��XmDocument�����e�إ�<see cref="DSXmlHelper"/>�AXmlDocument���󤣥i�H��Null�C
        /// </summary>
        /// <param name="xmlDoc">�n�̾ڪ�XmlDocument����C</param>
        public XmlHelper(XmlDocument xmlDoc)
        {
            if (xmlDoc == null)
                throw new ArgumentNullException("xmlDoc", em.Get("XmlDocNullReferenceNotSupport"));

            if (xmlDoc.DocumentElement == null)
                throw new Exception(em.Get("EmptyXmlDocument"));

            BaseNode = xmlDoc.DocumentElement;
        }

        /// <summary>
        /// ��XmlElement�����e�إߪ���C
        /// </summary>
        /// <param name="element">�n�̾ڪ�XmlElement����C</param>
        public XmlHelper(XmlElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element", em.Get("ElementNullReferenceNotSupport"));

            if (element.NodeType != XmlNodeType.Element)
                throw new ArgumentException(em.Get("NodeTypeNotSupport"), "element");

            BaseNode = element;
        }

        /// <summary>
        /// �̫��w���u�ڡv�����W�٫إ�Document
        /// </summary>
        /// <param name="rootName">�ڤ������W�١A���i�[���󪺯S��Ÿ�</param>
        public XmlHelper(string rootName)
        {
            BaseNode = XmlHelper.LoadXml("<" + rootName + "/>");
        }

        /// <summary>
        /// �b���w�������U�A�s�W�ݩʡA�ë��w�ȡC
        /// </summary>
        /// <param name="xpath">�n�s�W�ݩʪ��������|�C</param>
        /// <param name="name">�ݩʦW�١C</param>
        /// <param name="value">�ݩʭȡC</param>
        /// <returns>XmlAttribute���s����C</returns>
        public XmlAttribute SetAttribute(string xpath, string name, string value)
        {
            XmlAttribute att = CreateAttribute(name);
            att.InnerText = value;
            return SetAttribute(xpath, att);
        }

        /// <summary>
        /// �b���w�������U�A�s�W�ݩʡC
        /// </summary>
        /// <param name="xpath">�n�s�W�ݩʪ��������|�C</param>
        /// <param name="attribute">�ݩʪ���C</param>
        /// <returns>XmlAttribute���s����</returns>
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
        /// �s�W�ťդ���(Empyt Element)����
        /// </summary>
        /// <param name="newName">�s�����W�١C</param>
        /// <returns>�N��b<see cref="DSXmlHelper"/>���󤤷s����������C</returns>
        /// <include file='Util30\LibDocument\DSXmlHelper.xml' path='Documents/Document[@Name="AddElement1"]/*'/>
        public XmlElement AddElement(string newName)
        {
            return AddElement(".", newName, null);
        }

        /// <summary>
        /// �b���w�������U�A�s�W�ťդl�����C
        /// </summary>
        /// <param name="xpath">�n�s�W�u�ťդl�����v���u�������v���|</param>
        /// <param name="newElement">�n�s�W����������</param>
        /// <returns>�N��b<see cref="DSXmlHelper"/>���󤤷s����������C</returns>
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
        /// �b���w�������U�A�s�W�ťդl�����C
        /// </summary>
        /// <param name="xpath">�n�s�W�u�ťդl�����v���u�������v���|�C</param>
        /// <param name="newName">�ťդl�����W�١C</param>
        /// <returns>�N��b<see cref="DSXmlHelper"/>���󤤷s����������C</returns>
        /// <include file='Util30\LibDocument\DSXmlHelper.xml' path='Documents/Document[@Name="AddElement1"]/*'/>
        public XmlElement AddElement(string xpath, string newName)
        {
            return AddElement(xpath, newName, "");
        }

        /// <summary>
        /// �b���w�������U�A�s�W�l�����A�ë��w��r��ơC
        /// </summary>
        /// <param name="xpath">�n�s�W�u�l�����v���u�������v���|�C</param>
        /// <param name="newName">�l�����W�١C</param>
        /// <param name="text">�l������r��ơC</param>
        /// <returns>�N��b<see cref="DSXmlHelper"/>���󤤷s����������C</returns>
        /// <include file='Util30\LibDocument\DSXmlHelper.xml' path='Documents/Document[@Name="AddElement1"]/*'/>
        public XmlElement AddElement(string xpath, string newName, string text)
        {
            return AddElement(xpath, newName, text, false);
        }

        /// <summary>
        /// �b���w�������U�A�s�W�l�����A�ë��w��r��ơC
        /// </summary>
        /// <param name="xpath">�n�s�W�u�l�����v���u�������v���|�C</param>
        /// <param name="newName">�l�����W�١C</param>
        /// <param name="text">�l������r��ơC</param>
        /// <param name="isXmlContent">��r��ƬO�_�� Xml �r��A�i�H�O��@ Node �� NodeList�C</param>
        /// <returns>�N��b<see cref="DSXmlHelper"/>���󤤷s����������C</returns>
        /// <include file='Util30\LibDocument\DSXmlHelper.xml' path='Documents/Document[@Name="AddElement2"]/*'/>
        /// <remarks>��LAddElement�d�ҥi�Ѧ�<see cref="DSXmlHelper.AddElement(string,string)">
        /// DSXmlHelper.AddElement</see>��k�����C</remarks>
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

            //�[�J�o�Ӹ`�I			
            XmlNode newNode = ndTarget.AppendChild(elm);

            return newNode as XmlElement;
        }

        /// <summary>
        /// �W�[Xml���e�A�i�H�O��@Node��NodeList�C
        /// </summary>
        /// <param name="xpath">�n�s�WXml��ƪ����u�������v���|�C</param>
        /// <param name="xmlString">�n�s�W��Xml���e�C</param>
        public void AddXmlString(string xpath, string xmlString)
        {
            if (!PathExist(xpath))
                throw new ArgumentException(em.Get("XPathSyntaxError",
                    new Replace("XPath", xpath)), "xpath");

            //XmlNode node = GetElement(xpath);
            //node.InnerXml = xmlString;

            //�γo�ؤ�k���|��즳����ƲM���C
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = true;
            xmldoc.LoadXml("<Root>" + xmlString + "</Root>");

            foreach (XmlNode n in xmldoc.DocumentElement.ChildNodes)
                AddElement(xpath, (XmlElement)n);
        }

        /// <summary>
        /// �b���w�������U�W�[��r��ơA�p�G���w����r�w�s�b�A�|�Q���N�C
        /// </summary>
        /// <param name="xpath">�������|�A�p�G���|���s�b�|����Exception�C</param>
        /// <param name="text">�n�W�[����r��ơC</param>
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
        /// ���o���w�����U����r��ơC
        /// </summary>
        /// <param name="xpath">�������|�C</param>
        /// <returns>�����U����r��ơA�p�G���w���������s�b�h�^��String.Emtpy(�Ŧr��)�C</returns>
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
        /// �b���w�������U�W�[CDATA��r��ơC
        /// </summary>
        /// <param name="xpath">�������|�C</param>
        /// <param name="text">�n�W�[��CDATA��r��ơC</param>
        public void AddCDataSection(string xpath, string text)
        {
            XmlCDataSection cdata = BaseNode.OwnerDocument.CreateCDataSection(text);
            GetLastNode(xpath).AppendChild(cdata);
        }

        /// <summary>
        /// ���ի��w�������O�_�s�b�C
        /// </summary>
        /// <param name="xpath">�������|�C</param>
        /// <returns>Boolean�ȡA�w�s�b�^��True,���s�b�^��False�C</returns>
        public bool PathExist(string xpath)
        {
            return BaseNode.SelectSingleNode(xpath) != null;
        }

        /// <summary>
        /// ���ի��w�����������e�O�_��CDATA�C
        /// </summary>
        /// <param name="xpath">�������|�C</param>
        /// <returns>Boolean�ȡA�OCDATA�^��True�A�D�h�^��False�C</returns>
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
        /// ���o��������A���Ȩ��o�ŦX�u�������|�v���Ĥ@�Ӥ����C
        /// </summary>
        /// <param name="xpath">�������|�C</param>
        /// <returns>�^�Ǫ�XmlElement����C</returns>
        /// <exception cref="Exception">�o�ͦAxpath���X�����󤣬O����(Element)�ɡC</exception>
        public XmlElement GetElement(string xpath)
        {
            XmlNode nd = BaseNode.SelectSingleNode(xpath);

            if (nd != null && !(nd is XmlElement))
                throw new Exception("���o����Ƥ��O�@�Ӥ���(Element)�C");

            //�p�Gnd�ONull�A�h�|�^��Null(���as�B�⥢��)
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
        /// ���o��������A���Ȩ��o�ŦX�u�������|�v���Ĥ@�Ӥ����C
        /// </summary>
        /// <param name="xpath">�������|�C</param>
        /// <returns>�^�Ǫ� XmlAttribute ����C</returns>
        /// <exception cref="Exception">�o�ͦbxpath�Ҩ��o����Ƥ��O�ݩʮɡC</exception>
        public string GetAttribute(string xpath)
        {
            XmlNode nd = BaseNode.SelectSingleNode(xpath);

            if (nd != null && !(nd is XmlAttribute))
                throw new Exception("���o����Ƥ��O�@���ݩʡI");

            //�p�Gnd�ONull�A�h�|�^��Null(���as�B�⥢��)
            return (nd as XmlAttribute).Value;
        }

        /// <summary>
        /// ���o���w�����U��CDATA��ơA�p�G���ƭ�CDATA��ơA�h�|�^�ǲĤ@�ӡC
        /// </summary>
        /// <param name="xpath">�������|�C</param>
        /// <returns>�p�G��CDATA�A�^�Ǥ�r��ơA�S���^��String.Emtpy�C</returns>
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
        /// ���o��������}�C�A�N�|���o�Ҧ��ŦX�u�������|�v���Ҧ������C
        /// </summary>
        /// <param name="xpath">�������|�C</param>
        /// <returns>XmlElement���}�C�C</returns>
        public XmlElement[] GetElements(string xpath)
        {
            XmlNodeList ndl = BaseNode.SelectNodes(xpath);

            XmlElement[] result = new XmlElement[ndl.Count];
            for (int i = 0; i < ndl.Count; i++)
                result[i] = (XmlElement)ndl[i];
            return result;
        }

        /// <summary>
        /// ���������A�������s�b�|����Exception�C
        /// </summary>
        /// <param name="xpath">�n�������������|�C</param>
        public void RemoveElement(string xpath)
        {
            XmlNode ndToBeRemoved = (XmlNode)(BaseNode.SelectSingleNode(xpath));

            if (ndToBeRemoved == null)
                throw new XmlProcessException("���w�����|���s�b�C(" + xpath + ")");

            if (ndToBeRemoved is XmlAttribute)
            {
                XmlAttribute att = ndToBeRemoved as XmlAttribute;
                att.OwnerElement.RemoveAttributeNode(att);
            }
            else
                ndToBeRemoved.ParentNode.RemoveChild(ndToBeRemoved);
        }

        /// <summary>
        /// ���o�ثe��󪺰�¦XmlElement����C
        /// </summary>
        /// <returns>�����󪺰�XmlElement����C</returns>
        public XmlElement BaseElement
        {
            get
            {
                return BaseNode;
            }
        }

        /// <summary>
        /// ���ڦW�١C
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
        /// �N�S�w���`�I�ഫ���r��(��InnerXml)�C
        /// </summary>
        /// <param name="xpath">XPath���|�C</param>
        /// <returns>Xml�r��C</returns>
        public string ToString(string xpath)
        {
            return GetNode(xpath).OuterXml;
        }

        /// <summary>
        /// �^�ǧ��㪺Xml�r��C
        /// </summary>
        /// <returns>���㪺Xml�r��C</returns>
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
        /// �榡�� Xml ���e�C
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
        /// �ƻsXmlElement����A�ܧ�䤺�e���|�������Ӫ�XmlElement���C
        /// </summary>
        /// <param name="srcElement">�n�ƻs��XmlElement����C</param>
        /// <returns>�w�ƻs��XmlElement����C</returns>
        public static XmlElement CloneElement(XmlElement srcElement)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = true;
            xmldoc.LoadXml(srcElement.OuterXml);

            return (XmlElement)xmldoc.DocumentElement;
        }

        /// <summary>
        /// �NXml�r��[�J��XmlNode���A�i�B�z���PXmlDocument����XmlNode�C
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
        /// �NXml�r��[�J��XmlNode���C
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="childXmlContent">�]�t�u�ڡv��Xml�r��</param>
        public static XmlNode AppendChild(XmlNode parent, string childXmlContent)
        {
            XmlNode nChild = LoadXml(childXmlContent);

            nChild = parent.OwnerDocument.ImportNode(nChild, true);

            return parent.AppendChild(nChild);
        }

        /// <summary>
        /// ���J���w�� Xml �ɮסC
        /// </summary>
        /// <param name="fileName">�ɮצW�١C</param>
        /// <returns><see cref="XmlElement"/>����C</returns>
        public static XmlElement LoadXml(FileInfo file, bool preserveWhitespace)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = preserveWhitespace;
            xmldoc.Load(file.FullName);

            return xmldoc.DocumentElement;
        }

        /// <summary>
        /// ���J���w�� Xml �ɮסC
        /// </summary>
        /// <param name="fileName">�ɮצW�١C</param>
        /// <returns><see cref="XmlElement"/>����C</returns>
        public static XmlElement LoadXml(FileInfo file)
        {
            return LoadXml(File.ReadAllText(file.FullName), true);
        }

        /// <summary>
        /// ���J���w�� Xml ��ơC
        /// </summary>
        /// <param name="xmlContent">�n���J�� Xml �r���ơC</param>
        /// <returns><see cref="XmlElement"/>����C</returns>
        public static XmlElement LoadXml(string xmlString)
        {
            return LoadXml(xmlString, true);
        }

        /// <summary>
        /// ���J���w�� Xml ��ơC
        /// </summary>
        /// <param name="xmlString">�n���J�� Xml �r���ơC</param>
        /// <param name="preserveWhitespace">�O�_�O�d�r�ꤤ���x�ťզr���C</param>
        /// <returns><see cref="XmlElement"/>����C</returns>
        public static XmlElement LoadXml(string xmlString, bool preserveWhitespace)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = preserveWhitespace;
            xmldoc.LoadXml(xmlString);

            return xmldoc.DocumentElement;
        }

        /// <summary>
        /// �N���w�� Xml ��ƥH UTF-8 ���s�X�覡�x�s���ɮסC
        /// </summary>
        /// <param name="fileName">�ɮצW�١C</param>
        /// <param name="elm">�n�x�s�� Xml ����C</param>
        public static void SaveXml(string fileName, XmlNode node)
        {
            SaveXml(fileName, node, Encoding.UTF8);
        }

        /// <summary>
        /// �N���w�� Xml ����x�s���ɮסC
        /// </summary>
        /// <param name="fileName">�ɮצW�١C</param>
        /// <param name="node">�n�x�s�� Xml ����C</param>
        /// <param name="enc">�x�s���s�X�覡�C</param>
        public static void SaveXml(string fileName, XmlNode node, Encoding enc)
        {
            File.WriteAllText(fileName, node.OuterXml, enc);
        }

        /// <summary>
        /// �N���w�� Xml ��ƥHUTF-8���s�X�覡�g�J���y���C
        /// </summary>
        /// <param name="outStream">���w����y�C</param>
        /// <param name="node">�n��X�� Xml ����C</param>
        public static void SaveXml(Stream outStream, XmlNode node)
        {
            SaveXml(outStream, node, Encoding.UTF8);
        }

        /// <summary>
        /// �N���w�� Xml ��Ƽg�J���y���C
        /// </summary>
        /// <param name="outStream">���w����y�C</param>
        /// <param name="node">�n��X�� Xml ����C</param>
        /// <param name="enc">��X���s�X�覡�C</param>
        public static void SaveXml(Stream outStream, XmlNode node, Encoding enc)
        {
            StreamWriter sw = new StreamWriter(outStream, enc);
            sw.Write(node.OuterXml);
        }

        /// <summary>
        /// �ǰeXml���e��Y�Ӻ��}�C
        /// </summary>
        /// <param name="url">�ت�URL�C</param>
        /// <param name="xmlContent">�n�ǰe��Xml���e�C</param>
        /// <returns>�^�Ǫ�Xml��ơC</returns>
        public static string HttpSendTo(string url, string xmlContent)
        {
            return HttpSendTo(url, "POST", xmlContent);
        }

        /// <summary>
        /// �ǰeXml���e��Y�Ӻ��}�C
        /// </summary>
        /// <param name="url">�ت�URL�C</param>
        /// <param name="method">�ǰe����k(POST�BGET)</param>
        /// <param name="xmlContent">�n�ǰe��Xml���e�C</param>
        /// <returns>�^�Ǫ�Xml��ơC</returns>
        public static string HttpSendTo(string url, string method, string xmlContent)
        {
            //�إ�Http�s�u
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(url);

            //�򥻳]�w
            httpReq.Method = method;
            httpReq.ContentType = "text/xml";

            if (method == "POST")
            {
                //�g�JRequest�D��
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

            //���oResponse
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
            rspStream.Close(); //�o�ӭn�O�o�����C

            return result;
        }
        #endregion
    }
}