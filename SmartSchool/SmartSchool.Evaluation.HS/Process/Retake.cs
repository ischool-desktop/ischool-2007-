using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using SmartSchool;
using SmartSchool.Evaluation.Reports;

namespace SmartSchool.Evaluation.Process
{
    public partial class Retake : SmartSchool.Evaluation.Process.RibbonBarBase
    {
        public Retake()
        {
            InitializeComponent();
            this.Level = 12;
        }
        public override string ProcessTabName
        {
            get
            {
                return "���Z�B�z";
            }
        }
        #region ��ĳ���צW��-�̬�� (��ؤ��ή�W��)
        private void buttonItem1_Click(object sender, EventArgs e)
        {
            new RetakeListBySubject();
        }
        #endregion

        #region ��ĳ���צW��-�̾ǥ� (�ǥͭ��׬�ئW��)
        private void buttonItem2_Click(object sender, EventArgs e)
        {
            new RetakeListByStudent();
        }
        #endregion

        #region ���צ��Z�פJ��
        private void buttonItem3_Click(object sender, EventArgs e)
        {
            new RetakeScoreImport();
        }
        #endregion
    }
}

