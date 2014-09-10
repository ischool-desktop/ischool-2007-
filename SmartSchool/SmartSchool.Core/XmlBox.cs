using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool
{
    public partial class XmlBox : SmartSchool.Common.BaseForm
    {
        public XmlBox()
        {
            InitializeComponent();
        }

        public static void ShowXml(XmlElement xml, IWin32Window owner)
        {
            XmlBox box = new XmlBox();
            box.richTextBox1.Text = DSXmlHelper.Format(xml.OuterXml);
            box.ShowDialog(owner);
        }

        public static void ShowXml(XmlElement xml)
        {
            ShowXml(xml, null);
        }

    }
}