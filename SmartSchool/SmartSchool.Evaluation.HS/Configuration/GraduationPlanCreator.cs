using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SmartSchool.Feature.GraduationPlan;
using System.Xml;
using IntelliSchool.DSA30.Util;
using DevComponents.Editors;
using SmartSchool.Evaluation.GraduationPlan;

namespace SmartSchool.Evaluation.Configuration
{
    public partial class GraduationPlanCreator : SmartSchool.Common.BaseForm
    {
        private BackgroundWorker _BKWGraduationPlanLoader;

        private XmlElement _CopyElement;

        public GraduationPlanCreator()
        {
            InitializeComponent();
            _CopyElement = new XmlDocument().CreateElement("GraduationPlan");
            comboItem1.Tag = _CopyElement;
            comboBoxEx1.SelectedIndex = 0;
            _BKWGraduationPlanLoader = new BackgroundWorker();
            _BKWGraduationPlanLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BKWGraduationPlanLoader_RunWorkerCompleted);
            _BKWGraduationPlanLoader.DoWork += new DoWorkEventHandler(_BKWGraduationPlanLoader_DoWork);
            _BKWGraduationPlanLoader.RunWorkerAsync();
        }

        private void _BKWGraduationPlanLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = SmartSchool.Evaluation.GraduationPlan.GraduationPlan.Instance.Items;
        }

        private void _BKWGraduationPlanLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            comboBoxEx1.Items.Remove(comboItem2);
            GraduationPlanInfoCollection resp = (GraduationPlanInfoCollection)e.Result;
            foreach (GraduationPlanInfo gPlan in resp)
            {
                DevComponents.Editors.ComboItem item = new DevComponents.Editors.ComboItem();
                item.Text = gPlan.Name;
                item.Tag = gPlan.GraduationPlanElement;
                comboBoxEx1.Items.Add(item);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text != "")
            {
                AddGraduationPlan.Insert(textBoxX1.Text, _CopyElement);
                this.Close();
                EventHub.Instance.InvokGraduationPlanInserted();
                //GraduationPlanManager.Instance.LoadGraduationPlan(true);
                //if (GraduationPlanManager.Instance.Visible == false)
                //    GraduationPlanManager.Instance.ShowDialog();
            }
            else
                this.Close();
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEx1.SelectedItem == comboItem2)
                comboBoxEx1.SelectedIndex = 0;
            else
            {
                _CopyElement = (XmlElement)((ComboItem)comboBoxEx1.SelectedItem).Tag;
            }
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBoxX1, "");
            buttonX1.Enabled = true;
            if ( textBoxX1.Text == "" )
            {
                errorProvider1.SetError(textBoxX1, "不可空白。");
                buttonX1.Enabled = false;
                return;
            }
            foreach ( GraduationPlanInfo gPlan in SmartSchool.Evaluation.GraduationPlan.GraduationPlan.Instance.Items )
            {
                if ( gPlan.Name == textBoxX1.Text )
                {
                    errorProvider1.SetError(textBoxX1, "名稱不可重複。");
                    buttonX1.Enabled = false;
                    return;
                }
            }
        }
    }
}