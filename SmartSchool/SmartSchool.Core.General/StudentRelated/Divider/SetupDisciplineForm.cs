using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SmartSchool.StudentRelated.Divider
{
    public partial class SetupDisciplineForm : SmartSchool.Common.BaseForm, SmartSchool.StudentRelated.Divider.ISetupDisciplineView
    {
        public bool 檢視一般生 { get { return 一般生.Checked; } }
        public bool 檢視延修生 { get { return 延修生.Checked; } }
        public bool 檢視休學學生 { get { return 休學學生.Checked; } }
        public bool 檢視已刪除學生 { get { return 已刪除學生.Checked; } }
        public bool 檢視畢業及離校學生 { get { return 畢業及離校學生.Checked; } }
        public bool 檢視大過 { get { return 大過.Checked; } }
        public bool 檢視小過 { get { return 小過.Checked; } }
        public bool 檢視警告 { get { return 警告.Checked; } }
        public bool 檢視已銷過紀錄 { get { return 已銷過紀錄.Checked; } }
        public bool 檢視大功 { get { return 大功.Checked; } }
        public bool 檢視小功 { get { return 小功.Checked; } }
        public bool 檢視嘉獎 { get { return 嘉獎.Checked; } }
        public bool 檢視留校察看 { get { return 留校察看.Checked; } }

        public SetupDisciplineForm()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            嘉獎.Tag = 嘉獎.Checked;
            警告.Tag = 警告.Checked;
            小功.Tag = 小功.Checked;
            小過.Tag = 小過.Checked;
            大功.Tag = 大功.Checked;
            已銷過紀錄.Tag = 已銷過紀錄.Checked;
            大過.Tag = 大過.Checked;
            留校察看.Tag = 留校察看.Checked;
            畢業及離校學生.Tag = 畢業及離校學生.Checked;
            休學學生.Tag = 休學學生.Checked;
            一般生.Tag = 一般生.Checked;
            已刪除學生.Tag = 已刪除學生.Checked;
            延修生.Tag = 延修生.Checked;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SetupDisciplineForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            嘉獎.Checked = (bool)嘉獎.Tag;
            警告.Checked = (bool)警告.Tag;
            小功.Checked = (bool)小功.Tag;
            小過.Checked = (bool)小過.Tag;
            大功.Checked = (bool)大功.Tag;
            已銷過紀錄.Checked = (bool)已銷過紀錄.Tag;
            大過.Checked = (bool)大過.Tag;
            留校察看.Checked = (bool)留校察看.Tag;
            畢業及離校學生.Checked = (bool)畢業及離校學生.Tag;
            休學學生.Checked = (bool)休學學生.Tag;
            一般生.Checked = (bool)一般生.Tag;
            已刪除學生.Checked = (bool)已刪除學生.Tag;
            延修生.Checked = (bool)延修生.Tag;
        }

        private void SetupDisciplineForm_Shown(object sender, EventArgs e)
        {
            嘉獎.Tag = 嘉獎.Checked;
            警告.Tag = 警告.Checked;
            小功.Tag = 小功.Checked;
            小過.Tag = 小過.Checked;
            大功.Tag = 大功.Checked;
            已銷過紀錄.Tag = 已銷過紀錄.Checked;
            大過.Tag = 大過.Checked;
            留校察看.Tag = 留校察看.Checked;
            畢業及離校學生.Tag = 畢業及離校學生.Checked;
            休學學生.Tag = 休學學生.Checked;
            一般生.Tag = 一般生.Checked;
            已刪除學生.Tag = 已刪除學生.Checked;
            延修生.Tag = 延修生.Checked;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}