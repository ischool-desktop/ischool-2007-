using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartSchool
{
    public class ConfigurationCollection:Customization.Data.ConfigurationCollection
    {

        public ConfigurationCollection()
        {
            CurrentUser.Instance.SchoolConfig.Load();
            RootElement = CurrentUser.Instance.SchoolConfig.Content;
        }

        private XmlElement RootElement;
        public XmlElement this[string Key]
        {
            get
            {
                XmlElement element = (XmlElement)RootElement.SelectSingleNode(Key);
                if (element == null)
                {
                    return RootElement.OwnerDocument.CreateElement(Key);
                }
                else
                    return (XmlElement)element;
            }
            set
            {
                if (value.Name != Key)
                    throw new Exception("ElementName與Key不相同");
                //馬上抓最新版
                CurrentUser.Instance.SchoolConfig.Load();
                //更新
                RootElement = CurrentUser.Instance.SchoolConfig.Content;
                XmlElement element = (XmlElement)RootElement.SelectSingleNode(Key);
                if (element != null)
                {
                    XmlElement e = value;
                    if (element.OwnerDocument != value.OwnerDocument)
                        e = (XmlElement)element.OwnerDocument.ImportNode(value, true);

                    RootElement.ReplaceChild(e, element);
                }
                else
                    RootElement.AppendChild(RootElement.OwnerDocument.ImportNode(value, true));
                //存回去
                CurrentUser.Instance.SchoolConfig.Save();
            }
        }
    }
}
