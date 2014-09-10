using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace SmartSchool.Common
{
    /// <summary>
    /// 代表以 Xml 為基礎的類別，內部狀態是以 Xml 來儲存的類別。
    /// </summary>
    public class XmlBaseObject
    {
        /// <summary>
        /// 代表基礎的 Xml 資料。
        /// </summary>
        protected XmlElement BaseNode = null;
        /// <summary>
        /// 代表 Xml 資料的 XmlDocument 物件。
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
        /// 取得Xml的完整 Xml 字串。
        /// </summary>
        /// <returns>完整 Xml 字串。</returns>
        public string GetRawXml()
        {
            return BaseNode.OuterXml;
        }

        /// <summary>
        /// 取得Xml的完整資料，用 UTF-8 的編碼方式以 Binary 的型式回傳。
        /// </summary>
        /// <returns>UTF-8 編碼的完整 Binary 資料。</returns>
        public byte[] GetRawBinary()
        {
            return Encoding.UTF8.GetBytes(BaseNode.OuterXml);
        }

        /// <summary>
        /// 取得Xml的完整資料，以 Binary 的型式回傳。
        /// </summary>
        /// <returns>完整Binary資料。</returns>
        public byte[] GetRawBinary(Encoding enc)
        {
            return enc.GetBytes(BaseNode.OuterXml);
        }

        /// <summary>
        /// 從檔案載入 Xml 資料。
        /// </summary>
        /// <param name="fileName">檔案名稱。 </param>
        public virtual void LoadFromFile(string fileName)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = true;
            xmldoc.Load(fileName);
            Load(xmldoc.DocumentElement);
        }

        /// <summary>
        /// 從串流載入 Xml 資料。
        /// </summary>
        /// <param name="inStream">資料串流。</param>
        /// <param name="enc">串流的編碼方式。</param>
        public void Load(Stream inStream, Encoding enc)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(inStream, enc);
            Load(XmlHelper.LoadXml(sr.ReadToEnd()));
        }

        /// <summary>
        /// 從檔案載入Xml資料。
        /// </summary>
        /// <param name="xmlContent">檔案名稱。</param>
        public void Load(string xmlContent)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = true;
            xmldoc.LoadXml(xmlContent);
            Load(xmldoc.DocumentElement);
        }

        /// <summary>
        /// 從XmlElement物件Xml資料，內容不符Envelop則會產生Exception。
        /// </summary>
        /// <param name="xmlContent">要載入的<see cref="XmlElement"/>物件。</param>
        public virtual void Load(XmlElement xmlContent)
        {
            BaseNode = xmlContent;
        }

        /// <summary>
        /// 將內部Xml資料以UTF-8的編碼方式儲存到檔案中，如果檔案已存在，會覆寫該檔案。
        /// </summary>
        /// <param name="fileName">檔案名稱。</param>
        public void Save(string fileName)
        {
            Save(fileName, Encoding.UTF8);
        }

        /// <summary>
        /// 將內部Xml資料儲存到檔案中，如果檔案已存在，會覆寫該檔案。
        /// </summary>
        /// <param name="fileName">檔案名稱。</param>
        /// <param name="enc">檔案編碼方式</param>
        public void Save(string fileName, Encoding enc)
        {
            File.WriteAllText(fileName, BaseNode.OuterXml, enc);
        }

        /// <summary>
        /// 將內部Xml資料儲存到串流中。
        /// </summary>
        /// <param name="outStream">串流物件。</param>
        /// <param name="enc">編碼方式。</param>
        /// <remarks>此方法在執行之後，不會自動關閉串流。</remarks>
        public void Save(Stream outStream, Encoding enc)
        {
            StreamWriter sw = new StreamWriter(outStream, enc);
            sw.Write(BaseNode.OuterXml);
        }

    }
}
