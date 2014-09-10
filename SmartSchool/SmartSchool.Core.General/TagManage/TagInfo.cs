using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.TagManage
{
    public class TagInfo
    {
        public TagInfo(XmlElement tagData)
        {
            DSXmlHelper xmlTag = new DSXmlHelper(tagData);

            _identity = int.Parse(xmlTag.GetText("@ID"));
            _prefix = xmlTag.GetText("Prefix");
            _name = xmlTag.GetText("Name");
            _color = ParseColor(xmlTag.GetText("Color")); ;
        }

        public TagInfo(int id,string prefix, string name, string color)
        {
            _identity = id;
            _prefix = prefix;
            _name = name;
            _color = ParseColor(color);
        }

        public TagInfo(string prefix, string name, Color color)
        {
            _prefix = prefix;
            _name = name;
            _color = color;
        }

        private int _identity = -1;
        public int Identity
        {
            get { return _identity; }
            set
            {
                _identity = value;
                IsDirty = true;
            }
        }

        private string _prefix;
        public string Prefix
        {
            get { return _prefix; }
            set
            {
                _prefix = value;
                IsDirty = true;
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                IsDirty = true;
            }
        }

        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(Prefix))
                    return Name;
                else
                    return Prefix + ":" + Name;
            }
        }

        private Color _color;
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                IsDirty = true;
            }
        }

        private bool _is_dirty = false;
        public bool IsDirty
        {
            get { return _is_dirty; }
            private set { _is_dirty = value; }
        }

        private Color ParseColor(string pColor)
        {
            string strColor = pColor;
            Color objColor;
            if (string.IsNullOrEmpty(strColor))
                objColor = Color.White;
            else
                objColor = Color.FromArgb(int.Parse(strColor));
            return objColor;
        }
    }
}
