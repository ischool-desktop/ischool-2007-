using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;

namespace SmartSchool.ClassRelated.RibbonBars.DeXing
{    
    public partial class DeXingStatistic : BaseForm
    {
        private string[] _classidList;
        private IDeXingExport _exporter;
        public DeXingStatistic(string[] classidList)
        {
            _classidList = classidList;
            InitializeComponent();
            cboStatisticType.Items.Add("���m�֭p�W��");
            cboStatisticType.Items.Add("���ԾǥͦW��");
            cboStatisticType.Items.Add("�g�ٯS���{");
            cboStatisticType.Items.Add("���y�S���{");
            cboStatisticType.SelectedIndex = 0;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboStatisticType_SelectedIndexChanged(object sender, EventArgs e)
        {            
            switch (cboStatisticType.SelectedItem.ToString())
            {
                case "���m�֭p�W��":
                    _exporter = new AttendanceStatistic(_classidList);
                    break;
                case "���ԾǥͦW��":
                    _exporter = new NoAbsenceStatistic(_classidList);
                    break;
                case "�g�ٯS���{":
                    _exporter = new DemeritStatistic(_classidList);
                    break;
                case "���y�S���{":
                    _exporter = new MeritStatistic(_classidList);
                    break;
                default:
                    _exporter = new AttendanceStatistic(_classidList);
                    break;
            }
            panel1.Controls.Clear();
            Control ctrl = _exporter.MainControl;
            ctrl.Dock = DockStyle.Fill;
            panel1.Controls.Add(ctrl); 
            _exporter.LoadData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            _exporter.Export();
        }
    }
}