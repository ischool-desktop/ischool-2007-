using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;

namespace SmartSchool.StudentRelated.RibbonBars.AcademicAffairs
{
    public partial class LevelOfEducationForm : BaseForm
    {
        public LevelOfEducationForm()
        {
            InitializeComponent();

            LoadCodes();
        }

        private void LoadCodes()
        {
            comboBoxEx1.DisplayMember = "Description";
            comboBoxEx1.ValueMember = "Code";

            comboBoxEx1.Items.Add(new EduCodeItem("高中畢業 61", "61"));
            comboBoxEx1.Items.Add(new EduCodeItem("高中肄業 62", "62"));
            comboBoxEx1.Items.Add(new EduCodeItem("高職畢業 71", "71"));
            comboBoxEx1.Items.Add(new EduCodeItem("高職肄業 72", "72"));
            comboBoxEx1.Items.Add(new EduCodeItem("國中畢業 81", "81"));
            comboBoxEx1.Items.Add(new EduCodeItem("國中肄業 82", "82"));
            comboBoxEx1.Items.Add(new EduCodeItem("初職畢業 91", "91"));
            comboBoxEx1.Items.Add(new EduCodeItem("初職肄業 92", "92"));
            comboBoxEx1.Items.Add(new EduCodeItem("國小畢業 01", "01"));
            comboBoxEx1.Items.Add(new EduCodeItem("國小肄業 02", "02"));
            comboBoxEx1.Items.Add(new EduCodeItem("自修　　 03", "03"));
            comboBoxEx1.Items.Add(new EduCodeItem("不識字　 04", "04"));

            comboBoxEx1.SelectedIndex = 0;
        }

        public class EduCodeItem
        {
            private string _code;
            public string Code
            {
                get { return _code; }
                set { _code = value; }
            }

            private string _desc;
            public string Description
            {
                get { return _desc; }
                set { _desc = value; }
            }

            public EduCodeItem(string desc, string code)
            {
                _code = code;
                _desc = desc;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (comboBoxEx1.SelectedItem != null)
            {
                new LevelOfEducation((comboBoxEx1.SelectedItem as EduCodeItem).Code);
                this.Close();
            }
        }
    }


}