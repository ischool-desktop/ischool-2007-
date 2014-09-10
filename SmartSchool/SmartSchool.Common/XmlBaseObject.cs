using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace SmartSchool.Common
{
    /// <summary>
    /// �N��H Xml ����¦�����O�A�������A�O�H Xml ���x�s�����O�C
    /// </summary>
    public class XmlBaseObject
    {
        /// <summary>
        /// �N���¦�� Xml ��ơC
        /// </summary>
        protected XmlElement BaseNode = null;
        /// <summary>
        /// �N�� Xml ��ƪ� XmlDocument ����C
        /// </summary>
        protected XmlDocument Owner = null;

        public XmlBaseObject()
        {
        }

        public XmlBaseObject(XmlElement xmlContent)
        {
            BaseNode = xmlContent;
        }

        /// <summary>
        /// ���oXml������ Xml �r��C
        /// </summary>
        /// <returns>���� Xml �r��C</returns>
        public string GetRawXml()
        {
            return BaseNode.OuterXml;
        }

        /// <summary>
        /// ���oXml�������ơA�� UTF-8 ���s�X�覡�H Binary �������^�ǡC
        /// </summary>
        /// <returns>UTF-8 �s�X������ Binary ��ơC</returns>
        public byte[] GetRawBinary()
        {
            return Encoding.UTF8.GetBytes(BaseNode.OuterXml);
        }

        /// <summary>
        /// ���oXml�������ơA�H Binary �������^�ǡC
        /// </summary>
        /// <returns>����Binary��ơC</returns>
        public byte[] GetRawBinary(Encoding enc)
        {
            return enc.GetBytes(BaseNode.OuterXml);
        }

        /// <summary>
        /// �q�ɮ׸��J Xml ��ơC
        /// </summary>
        /// <param name="fileName">�ɮצW�١C </param>
        public virtual void LoadFromFile(string fileName)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = true;
            xmldoc.Load(fileName);
            Load(xmldoc.DocumentElement);
        }

        /// <summary>
        /// �q��y���J Xml ��ơC
        /// </summary>
        /// <param name="inStream">��Ʀ�y�C</param>
        /// <param name="enc">��y���s�X�覡�C</param>
        public void Load(Stream inStream, Encoding enc)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(inStream, enc);
            Load(XmlHelper.LoadXml(sr.ReadToEnd()));
        }

        /// <summary>
        /// �q�ɮ׸��JXml��ơC
        /// </summary>
        /// <param name="xmlContent">�ɮצW�١C</param>
        public void Load(string xmlContent)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = true;
            xmldoc.LoadXml(xmlContent);
            Load(xmldoc.DocumentElement);
        }

        /// <summary>
        /// �qXmlElement����Xml��ơA���e����Envelop�h�|����Exception�C
        /// </summary>
        /// <param name="xmlContent">�n���J��<see cref="XmlElement"/>����C</param>
        public virtual void Load(XmlElement xmlContent)
        {
            BaseNode = xmlContent;
        }

        /// <summary>
        /// �N����Xml��ƥHUTF-8���s�X�覡�x�s���ɮפ��A�p�G�ɮפw�s�b�A�|�мg���ɮסC
        /// </summary>
        /// <param name="fileName">�ɮצW�١C</param>
        public void Save(string fileName)
        {
            Save(fileName, Encoding.UTF8);
        }

        /// <summary>
        /// �N����Xml����x�s���ɮפ��A�p�G�ɮפw�s�b�A�|�мg���ɮסC
        /// </summary>
        /// <param name="fileName">�ɮצW�١C</param>
        /// <param name="enc">�ɮ׽s�X�覡</param>
        public void Save(string fileName, Encoding enc)
        {
            File.WriteAllText(fileName, BaseNode.OuterXml, enc);
        }

        /// <summary>
        /// �N����Xml����x�s���y���C
        /// </summary>
        /// <param name="outStream">��y����C</param>
        /// <param name="enc">�s�X�覡�C</param>
        /// <remarks>����k�b���椧��A���|�۰�������y�C</remarks>
        public void Save(Stream outStream, Encoding enc)
        {
            StreamWriter sw = new StreamWriter(outStream, enc);
            sw.Write(BaseNode.OuterXml);
        }

    }
}
