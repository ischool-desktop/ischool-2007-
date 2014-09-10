using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartSchool.Diagnostics
{
    public partial class DiagMainForm : Form
    {
        public DiagMainForm()
        {
            InitializeComponent();
        }

        private void btnBenchNetwork_Click(object sender, EventArgs e)
        {
            BenchNetworkForm form = new BenchNetworkForm();
            form.Show();
        }

        private void btnBenchThread_Click(object sender, EventArgs e)
        {
            BenchThreadForm form = new BenchThreadForm();
            form.Show();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            DiagForm form = new DiagForm();
            form.Show();
            form.Run();
        }
    }
}