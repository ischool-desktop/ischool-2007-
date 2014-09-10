using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using System.Xml;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar;

namespace SmartSchool.Others.Configuration.MDReduceMapping
{
    public partial class ReduceForm : BaseForm
    {
        public ReduceForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid()) return;
            //獎懲單位換算表
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Reduce");
            doc.AppendChild(root);
            XmlElement element = doc.CreateElement("Merit");
            root.AppendChild(element);
            XmlElement ab = doc.CreateElement("AB");
            element.AppendChild(ab);
            ab.InnerText = txtMAB.Text;
            XmlElement bc = doc.CreateElement("BC");
            element.AppendChild(bc);
            bc.InnerText = txtMBC.Text;

            element = doc.CreateElement("Demerit");
            root.AppendChild(element);
            ab = doc.CreateElement("AB");
            element.AppendChild(ab);
            ab.InnerText = txtDAB.Text;
            bc = doc.CreateElement("BC");
            element.AppendChild(bc);
            bc.InnerText = txtDBC.Text;

            try
            {
                DSXmlHelper helper = new DSXmlHelper("Lists");
                helper.AddElement("List");
                helper.AddElement("List", "Content");
                helper.AddElement("List/Content", doc.DocumentElement);
                helper.AddElement("List", "Condition");
                helper.AddElement("List/Condition", "Name", "獎懲單位換算表");
                SmartSchool.Feature.Basic.Config.Update(new DSRequest(helper));
            }
            catch (Exception ex)
            {
                MsgBox.Show("儲存失敗 : " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Close();
        }

        private bool IsValid()
        {
            error.Clear();
            error.Tag = true;
            ValidInt(txtMAB, lblMAB);
            ValidInt(txtMBC, lblMBC);
            ValidInt(txtDAB, lblDAB);
            ValidInt(txtDBC, lblDBC);
            return bool.Parse(error.Tag.ToString());
        }

        private void ValidInt(TextBoxX txt, LabelX lbl)
        {
            int i;
            if (!int.TryParse(txt.Text, out i))
            {
                error.Tag = false;
                error.SetError(lbl, "必須為數字");
            }
        }

        private void ReduceForm_Load(object sender, EventArgs e)
        {
            DSResponse dsrsp = SmartSchool.Feature.Basic.Config.GetMDReduce();
            if (!dsrsp.HasContent)
            {
                MsgBox.Show("取得對照表失敗 : " + dsrsp.GetFault().Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DSXmlHelper helper = dsrsp.GetContent();
            txtMAB.Text = helper.GetText("Merit/AB");
            txtMBC.Text = helper.GetText("Merit/BC");
            txtDAB.Text = helper.GetText("Demerit/AB");
            txtDBC.Text = helper.GetText("Demerit/BC");
        }
    }
}