using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feedback.Feature;
using System.Xml;

namespace SmartSchool.Feedback
{
    public partial class VoteViewer : BaseForm
    {
        private DSXmlHelper _helper;
        public VoteViewer(DSXmlHelper helper)
        {
            InitializeComponent();
            _helper = helper;
        }

        private void VoteViewer_Load(object sender, EventArgs e)
        {
            Dictionary<string, int> statistics = new Dictionary<string, int>();

            foreach (XmlElement vote in _helper.GetElements("Vote"))
            {
                DSXmlHelper voteHelper = new DSXmlHelper(vote);
                string user = voteHelper.GetText("User");
                if (!statistics.ContainsKey(user))
                    statistics.Add(user, 0);
                statistics[user]++;
            }

            dataGridViewX1.Rows.Clear();
            dataGridViewX1.SuspendLayout();

            foreach (string user_with_school in statistics.Keys)
            {
                int index = user_with_school.LastIndexOf('/');
                string school = user_with_school.Substring(0, index);
                string user = user_with_school.Substring(index+1);

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewX1, "", school, user, statistics[user_with_school]);
                dataGridViewX1.Rows.Add(row);
            }

            dataGridViewX1.ResumeLayout();
        }
    }
}