using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;

namespace SmartSchool
{
    public partial class SkillSchool : SmartSchool.Common.BaseForm
    {
        public SkillSchool()
        {
            InitializeComponent();
        }

        private void SkillSchool_Load(object sender, EventArgs e)
        {
            CurrentUser user = CurrentUser.Instance;

            txtSchoolName.Text = user.SchoolChineseName;
            txtSchoolCode.Text = user.SchoolCode;
            txtSchoolYear.Text = user.SchoolYear.ToString();
            txtSemester.Text = user.Semester.ToString();
            txtTelephone.Text = user.SchoolInfo.Telephone;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CurrentUser user = CurrentUser.Instance;
            SystemConfig config = user.SystemConfig;
            SchoolInfo info = user.SchoolInfo;

            try
            {
                config.DefaultSchoolYear = int.Parse(txtSchoolYear.Text);
                config.DefaultSemester = int.Parse(txtSemester.Text);
                info.ChineseName = txtSchoolName.Text;
                info.Code = txtSchoolCode.Text;
                info.Telephone = txtTelephone.Text;

                config.Save();
                info.Save();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }

            Close();
        }

        //public static void SetConnection(string accessPoint, string userName, string password)
        //{
        //    string name = AppDomain.CurrentDomain.FriendlyName;
        //    if (name == "SmartSchool")
        //        throw new Exception("請修正，這個東西不能這樣用！");
        //    else
        //        CurrentUser.Instance.SetConnection(accessPoint, userName, password);
        //}
    }
}