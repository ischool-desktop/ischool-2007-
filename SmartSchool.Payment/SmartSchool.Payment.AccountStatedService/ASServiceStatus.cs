using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartSchool.Payment.AccountStatedService
{
    public partial class ASServiceStatus : Form
    {
        public ASServiceStatus()
        {
            InitializeComponent();
            srvPayment.ServiceName = ServiceProgram.ServiceName;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (srvPayment.Status == System.ServiceProcess.ServiceControllerStatus.Stopped)
                    srvPayment.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (srvPayment.Status == System.ServiceProcess.ServiceControllerStatus.Running)
                    srvPayment.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            srvPayment.Refresh();
            lblStatus.Text = srvPayment.Status.ToString();
        }
    }
}