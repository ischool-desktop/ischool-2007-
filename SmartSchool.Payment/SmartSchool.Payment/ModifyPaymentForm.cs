﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using DevComponents.DotNetBar;
using SmartSchool.Payment.Interfaces;
using SmartSchool.Payment.BankManagement;

namespace SmartSchool.Payment
{
    internal partial class ModifyPaymentForm : BaseForm
    {
        /// <summary>
        /// 單定學年度/學期時用。
        /// </summary>
        public ModifyPaymentForm(GT.Payment payment)
        {
            InitializeComponent();
            InitializeSemester();
            InitializeBankCombobox();

            _target_payment = payment;
            cboSchoolYear.Text = payment.SchoolYear;
            cboSemester.Text = payment.Semester;
            cboSchoolYear.Enabled = false;
            cboSemester.Enabled = false;
            txtPaymentName.Text = payment.Name;
            txtExpiration.Text = payment.Config.DefaultExpiration;
            SelectComboboxBankConfig(payment.Config.BankConfigID);
        }

        private GT.Payment _target_payment;
        internal GT.Payment TargetPayment
        {
            get { return _target_payment; }
        }

        public event CancelEventHandler PaymentSaving;

        #region InitializeSemester
        private void InitializeSemester()
        {
            try
            {
                for (int i = -2; i <= 2; i++) //只顯示前後兩個學年的選項，其他的用手動輸入。
                    cboSchoolYear.Items.Add(CurrentUser.Instance.SchoolYear + i);

                cboSchoolYear.Text = CurrentUser.Instance.SchoolYear.ToString();
                cboSemester.Text = CurrentUser.Instance.Semester.ToString();
            }
            catch (Exception ex)
            {
                CurrentUser.ReportError(new PaymentModuleException("填入學年度學期選項清單時發生錯誤。", ex));
            }
        }
        #endregion

        #region (Process) InitializeBankCombobox
        private void InitializeBankCombobox()
        {
            cboBank.Items.Clear();
            cboBank.ValueMember = "ConfigID";
            cboBank.Items.Add(string.Empty);  //增加一個「空」的銀行選項。

            foreach (BankConfig each in BankConfigManager.GetConfigList())
                cboBank.Items.Add(new ConfigItem(each));
        }

        #region Class ConfigItem
        private class ConfigItem
        {
            public ConfigItem(BankConfig config)
            {
                _config = config;
            }

            private BankConfig _config;
            public BankConfig Config
            {
                get { return _config; }
            }

            public override string ToString()
            {
                return Config.Name;
            }

            public string ConfigID
            {
                get { return Config.ConfigID; }
            }
        }
        #endregion

        #endregion

        private void SelectComboboxBankConfig(string configID)
        {
            foreach (object each in cboBank.Items)
            {
                ConfigItem item = each as ConfigItem;
                if (item != null)
                {
                    if (item.ConfigID == configID)
                        cboBank.SelectedItem = each;
                }
                else
                    cboBank.SelectedItem = string.Empty;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //_target_payment = new GT.Payment();

            #region 檢查收費名稱、學年度、學期
            _errors.Clear();

            if (string.IsNullOrEmpty(txtPaymentName.Text))
            {
                _errors.SetError(txtPaymentName, "收費名稱為必要欄位。");
                return;
            }

            int temp;
            if (string.IsNullOrEmpty(cboSchoolYear.Text) || !int.TryParse(cboSchoolYear.Text, out temp))
            {
                _errors.SetError(cboSchoolYear, "格式錯誤。");
                return;
            }
            if (string.IsNullOrEmpty(cboSemester.Text) || !int.TryParse(cboSemester.Text, out temp))
            {
                _errors.SetError(cboSemester, "格式錯誤。");
                return;
            }

            DateTime dtTemp;
            if (!string.IsNullOrEmpty(txtExpiration.Text) && !DateTime.TryParse(txtExpiration.Text, out dtTemp))
            {
                _errors.SetError(txtExpiration, "繳費截止日期格式不正確。");
                return;
            }

            TargetPayment.Name = txtPaymentName.Text;
            TargetPayment.SchoolYear = cboSchoolYear.Text;
            TargetPayment.Semester = cboSemester.Text;
            //TargetPayment.Config = new SmartSchool.Payment.GT.PaymentConfig();
            TargetPayment.Config.BankConfigID = GetBankConfigID();
            TargetPayment.Config.DefaultExpiration = txtExpiration.Text;

            CancelEventArgs arg = new CancelEventArgs(false);
            if (PaymentSaving != null)
            {
                PaymentSaving(this, arg);
                if (arg.Cancel)
                    return;
            }
            #endregion

            if (GT.Payment.SavePayment(TargetPayment))
                DialogResult = DialogResult.OK;
            else
                DialogResult = DialogResult.None;
        }

        private string GetBankConfigID()
        {
            ConfigItem citem = cboBank.SelectedItem as ConfigItem;

            if (citem == null) return string.Empty;

            return citem.ConfigID;
        }
    }
}