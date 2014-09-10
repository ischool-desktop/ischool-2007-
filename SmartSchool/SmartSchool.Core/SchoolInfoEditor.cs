using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.AccessControl;

namespace SmartSchool
{
    [FeatureCode("System0030")]
    public partial class SchoolInfoEditor : SmartSchool.Common.BaseForm
    {
        private SchoolInfo _sinfo;
        private SystemConfig _config;
        private DataValueManager _value = new DataValueManager();

        public SchoolInfoEditor()
        {
            InitializeComponent();
            CurrentUser user = CurrentUser.Instance;
            _sinfo = user.SchoolInfo;
            _config = user.SystemConfig;

            txtAddress.Text = _sinfo.Address;
            txtChiName.Text = _sinfo.ChineseName;
            txtEngName.Text = _sinfo.EnglishName;
            txtFax.Text = _sinfo.Fax;
            txtPhone.Text = _sinfo.Telephone;
            txtSchoolCode.Text = _sinfo.Code;
            
            txtSchoolYear.Text = _config.DefaultSchoolYear.ToString();
            txtSemester.Text = _config.DefaultSemester.ToString();

            KeepOriginalInfo();
        }

        private void KeepOriginalInfo()
        {
            _value.AddValue(labelX1.Text, txtSchoolCode.Text);
            _value.AddValue(labelX2.Text, txtChiName.Text);
            _value.AddValue(labelX3.Text, txtEngName.Text);
            _value.AddValue(labelX4.Text, txtAddress.Text);
            _value.AddValue(labelX5.Text, txtPhone.Text);
            _value.AddValue(labelX6.Text, txtFax.Text);
            _value.AddValue(labelX7.Text, txtSchoolYear.Text);
            _value.AddValue(labelX8.Text, txtSemester.Text);
        }

        private void ValueChanged()
        {
            _value.SetValue(labelX1.Text, txtSchoolCode.Text);
            _value.SetValue(labelX2.Text, txtChiName.Text);
            _value.SetValue(labelX3.Text, txtEngName.Text);
            _value.SetValue(labelX4.Text, txtAddress.Text);
            _value.SetValue(labelX5.Text, txtPhone.Text);
            _value.SetValue(labelX6.Text, txtFax.Text);
            _value.SetValue(labelX7.Text, txtSchoolYear.Text);
            _value.SetValue(labelX8.Text, txtSemester.Text);
        }

        private void WriteLog()
        {
            StringBuilder builder = new StringBuilder("");
            string format = "「{0}」由「{1}」變更為「{2}」";
            Dictionary<string, string> dirtyItems = _value.GetDirtyItems();
            foreach (string key in dirtyItems.Keys)
            {
                builder.AppendLine(string.Format(format, key, _value.GetOldValue(key), dirtyItems[key]));
            }
            CurrentUser user = CurrentUser.Instance;
            user.AppLog.Write("變更學校基本資料", builder.ToString(), "", "");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            #region 檢查學年度學期格式
            int value;
            bool error = false;
            errorProvider1.Clear();
            if (!int.TryParse(txtSchoolYear.Text, out value))
            {
                errorProvider1.SetError(txtSchoolYear, "格式錯誤");
                error = true;
            }
            if (!int.TryParse(txtSemester.Text, out value))
            {
                errorProvider1.SetError(txtSemester, "格式錯誤");
                error = true;
            }
            if (int.TryParse(txtSemester.Text, out value) && (int.Parse(txtSemester.Text) < 1 || int.Parse(txtSemester.Text) > 2))
            {
                errorProvider1.SetError(txtSemester, "請輸入 1 或 2");
                error = true;
            }
            if (error == true)
                return;
            #endregion

            _sinfo.Address = txtAddress.Text;
            _sinfo.ChineseName = txtChiName.Text;
            _sinfo.Code = txtSchoolCode.Text;
            _sinfo.EnglishName = txtEngName.Text;
            _sinfo.Fax = txtFax.Text;
            _sinfo.Telephone = txtPhone.Text;
            _sinfo.Save();

            _config.DefaultSchoolYear = int.Parse(txtSchoolYear.Text);
            _config.DefaultSemester = int.Parse(txtSemester.Text);
            _config.Save();

            ValueChanged();
            WriteLog();

            this.Close();
        }
    }
}