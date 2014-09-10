using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.StudentRelated.Palmerworm;
using SmartSchool.Feature.Basic;
using SmartSchool.Common;
using System.IO;
using System.Drawing.Imaging;
using DevComponents.DotNetBar;
using SmartSchool.Feature;
using SmartSchool.Properties;
using SmartSchool.ApplicationLog;
using SmartSchool.ExceptionHandler;
using SmartSchool.AccessControl;

namespace SmartSchool.StudentRelated.Palmerworm
{
    [FeatureCode("Content0010")]
    internal partial class BaseInfoPalmerwormItem : PalmerwormItem
    {
        private bool _isInitialized = false;
        private EnhancedErrorProvider _errors = new EnhancedErrorProvider();

        public BaseInfoPalmerwormItem()
            : base()
        {
            Font = FontStyles.General;
            InitializeComponent();
            Title = "基本資料";
            _errors.Icon = Resources.error;
        }

        protected override object OnBackgroundWorkerWorking()
        {
            return QueryStudent.GetDetailList(new string[] { "Nationality", "Birthdate", "Name", "EnglishName", "IDNumber", "BirthPlace", "Gender", "FreshmanPhoto", "GraduatePhoto", "SALoginName", "SAPassword", "AccountType" }, RunningID);
        }

        protected override void OnBackgroundWorkerCompleted(object p)
        {
            Initialize();
            _errors.Clear();
            DSResponse dsrsp = p as DSResponse;

            string nationality = dsrsp.GetContent().GetText("Student/Nationality");
            string birthday = dsrsp.GetContent().GetText("Student/Birthdate");
            string name = dsrsp.GetContent().GetText("Student/Name");
            string engFirstName = dsrsp.GetContent().GetText("Student/EnglishName");
            string ssn = dsrsp.GetContent().GetText("Student/IDNumber");
            string birthPlace = dsrsp.GetContent().GetText("Student/BirthPlace");
            string gender = dsrsp.GetContent().GetText("Student/Gender");
            string strpic1 = dsrsp.GetContent().GetText("Student/FreshmanPhoto");
            string strpic2 = dsrsp.GetContent().GetText("Student/GraduatePhoto");
            string saLoginName = dsrsp.GetContent().GetText("Student/SALoginName");
            string saPassword = dsrsp.GetContent().GetText("Student/SAPassword");
            string accountType = dsrsp.GetContent().GetText("Student/AccountType");

            cboNationality.SetComboBoxValue(nationality);
            txtBirthDate.SetDate(birthday);
            birthday = txtBirthDate.DateString;
            txtName.Text = name;
            txtEngName.Text = engFirstName;
            txtSSN.Text = ssn;
            txtBirthPlace.Text = birthPlace;
            txtLoginID.Text = saLoginName;
            txtLoginPwd.Text = saPassword;
            //cboBirthCounty.SetComboBoxValue(birthProvince);
            //cboBirthCity.SetComboBoxValue(birthCounty);
            cboGender.SetComboBoxValue(gender);
            cboAccountType.Text = accountType;

            //byte[] bs = Convert.FromBase64String(strpic1);
            //MemoryStream ms = new MemoryStream(bs);
            try
            {
                //pic1.Image = Bitmap.FromStream(ms);
                pic1String = strpic1;
                pic1.Image = Photo.ConvertFromBase64Encoding(strpic1, pic1.Width, pic1.Height);
            }
            catch (Exception)
            {
                pic1.Image = pic1.InitialImage;
            }

            //bs = Convert.FromBase64String(strpic2);
            //ms = new MemoryStream(bs);
            try
            {
                //pic2.Image = Bitmap.FromStream(ms);
                pic2String = strpic2;
                pic2.Image = Photo.ConvertFromBase64Encoding(strpic2, pic2.Width, pic2.Height);
            }
            catch (Exception)
            {
                pic2.Image = pic2.InitialImage;
            }
            try
            {
                _valueManager.AddValue("Nationality", cboNationality.SelectedText, "國籍");
            }
            catch
            {
            }
            _valueManager.AddValue("Birthdate", txtBirthDate.Text, "生日");
            _valueManager.AddValue("Name", txtName.Text, "姓名");
            _valueManager.AddValue("EnglishName", txtEngName.Text, "英文姓名");
            _valueManager.AddValue("IDNumber", txtSSN.Text, "身分證號");
            _valueManager.AddValue("BirthPlace", birthPlace, "出生地");
            _valueManager.AddValue("Gender", gender, "性別");
            _valueManager.AddValue("SALoginName", saLoginName, "登入帳號");
            _valueManager.AddValue("SAPassword", saPassword, "登入密碼");
            _valueManager.AddValue("AccountType", accountType, "帳號類型");

            SaveButtonVisible = false;
        }

        #region IPalmerwormItem 成員
        public override void Save()
        {
            //if (!CurrentUser.Acl[this.GetType()].Editable)
            //{
            //    MsgBox.Show("爆吧！");
            //    return;
            //}

            if (txtName.Text != "")
            {
                ValidateIDNumber();
                ValidateLoginID();

                if (_errors.HasError)
                {
                    MsgBox.Show("輸入資料未通過驗證，請修正後再儲存");
                    return;
                }

                DSRequest dsreq = _valueManager.GetRequest("StudentList", "Student", "Field", "Condition", "ID", RunningID);

                //計算密碼雜~!
                if (dsreq.GetContent().PathExist("Student/Field/SAPassword"))
                {
                    if (!string.IsNullOrEmpty(dsreq.GetContent().GetElement("Student/Field/SAPassword").InnerText))
                        dsreq.GetContent().GetElement("Student/Field/SAPassword").InnerText = PasswordHash.Compute(dsreq.GetContent().GetElement("Student/Field/SAPassword").InnerText);
                }

                EditStudent.Update(dsreq);

                LogUtility.LogChange(_valueManager, RunningID, GetOriginStudentName());

                //Student.Instance.InvokBriefDataChanged(RunningID);
                SmartSchool.Broadcaster.Events.Items["學生/資料變更"].Invoke(RunningID);
                SaveButtonVisible = false;
            }
            else
            {
                MsgBox.Show("姓名欄不可空白");
            }
        }

        private string GetOriginStudentName()
        {
            try
            {
                BriefStudentData student = Student.Instance.Items[RunningID];
                return student.Name;
            }
            catch (Exception)
            {
                return "<" + RunningID + ">";
            }
        }

        private void Initialize()
        {
            if (_isInitialized) return;
            //載入國家列表
            DSResponse dsrsp = Config.GetNationalityList();
            DSXmlHelper nHelper = dsrsp.GetContent();
            foreach (XmlNode n in nHelper.GetElements("Nationality"))
            {
                this.cboNationality.AddItem(n.InnerText);
            }

            cboGender.AddItem("男");
            cboGender.AddItem("女");
            _isInitialized = true;
        }
        #endregion


        private void txtName_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("Name", this.txtName.Text);
        }

        private void txtEngName_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("EnglishName", this.txtEngName.Text);
        }

        private void txtSSN_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("IDNumber", this.txtSSN.Text);
        }

        private string pic1String = string.Empty, pic2String = string.Empty;

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "所有影像(*.jpg,*.jpeg,*.gif,*.png)|*.jpg;*.jpeg;*.gif;*.png;";
            if (od.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(od.FileName, FileMode.Open);
                    Bitmap orgBmp = new Bitmap(fs);
                    fs.Close();

                    Bitmap newBmp = new Bitmap(orgBmp, pic1.Size);
                    pic1.Image = newBmp;

                    pic1String = ToBase64String(Photo.Resize(new Bitmap(orgBmp)));
                    EditStudent.UpdateFreshmanPhoto(pic1String, RunningID);
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message);
                }
            }
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "所有影像(*.jpg,*.jpeg,*.gif,*.png)|*.jpg;*.jpeg;*.gif;*.png;";
            if (od.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(od.FileName, FileMode.Open);
                    Bitmap orgBmp = new Bitmap(fs);
                    fs.Close();

                    Bitmap newBmp = new Bitmap(orgBmp, pic2.Size);
                    pic2.Image = newBmp;

                    pic1String = ToBase64String(Photo.Resize(new Bitmap(orgBmp)));

                    EditStudent.UpdateGraduatePhoto(pic1String, RunningID);
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message);
                }
            }
        }

        private static string ToBase64String(Bitmap newBmp)
        {
            MemoryStream ms = new MemoryStream();
            newBmp.Save(ms, ImageFormat.Jpeg);
            ms.Seek(0, SeekOrigin.Begin);
            byte[] bytes = new byte[ms.Length];
            ms.Read(bytes, 0, (int)ms.Length);
            ms.Close();

            return Convert.ToBase64String(bytes);
        }

        //另存照片
        private void buttonItem2_Click(object sender, EventArgs e)
        {
            SavePicture(pic1String);
        }

        //另存照片
        private void buttonItem4_Click(object sender, EventArgs e)
        {
            SavePicture(pic2String);
        }

        private void SavePicture(string imageString)
        {
            if (imageString == string.Empty)
                return;

            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "PNG 影像|*.png;";
            sd.FileName = txtSSN.Text + ".png";

            if (sd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(sd.FileName, FileMode.Create);
                    byte[] imageData = Convert.FromBase64String(imageString);
                    fs.Write(imageData, 0, imageData.Length);
                    fs.Close();
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message);
                }
            }
        }

        //private void cboNationality_SelectedIndexChanged_1(object sender, EventArgs e)
        //{
        //    OnValueChanged("Nationality", cboNationality.GetText());
        //}

        private void cboNationality_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("Nationality", cboNationality.GetText());
        }

        private void txtBirthPlace_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("BirthPlace", txtBirthPlace.Text);
        }


        private void cboGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnValueChanged("Gender", cboGender.GetText());
        }

        private void txtBirthDate_Validated_1(object sender, EventArgs e)
        {
            if (!txtBirthDate.IsValid)
                _errors.SetError(txtBirthDate, "請輸入 yyyy/mm/dd 符合日期格式文字");
            else
                _errors.SetError(txtBirthDate, string.Empty);
        }

        private void txtBirthDate_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("Birthdate", txtBirthDate.DateString);
        }

        private void txtSSN_Validating(object sender, CancelEventArgs e)
        {
            ValidateIDNumber();
        }

        private void txtLoginID_Validating(object sender, CancelEventArgs e)
        {
            ValidateLoginID();
        }

        private void ValidateIDNumber()
        {
            if (string.IsNullOrEmpty(txtSSN.Text))
            {
                _errors.SetError(txtSSN, string.Empty);
                return;
            }

            if (QueryStudent.IDNumberExists(RunningID, txtSSN.Text))
                _errors.SetError(txtSSN, "身分證號重覆，請確認資料。");
            else
                _errors.SetError(txtSSN, string.Empty);
        }

        private void ValidateLoginID()
        {
            if (string.IsNullOrEmpty(txtLoginID.Text))
            {
                _errors.SetError(txtLoginID, string.Empty);
                return;
            }

            if (QueryStudent.LoginIDExists(txtLoginID.Text, RunningID))
                _errors.SetError(txtLoginID, "帳號重覆，請重新選擇。");
            else
                _errors.SetError(txtLoginID, string.Empty);
        }

        private void txtLoginID_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("SALoginName", txtLoginID.Text);
        }

        private void txtLoginPwd_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("SAPassword", txtLoginPwd.Text);
        }

        private void cboAccountType_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("AccountType", cboAccountType.Text);
        }

        #region 清除照片
        //清除新生照片
        private void buttonItem5_Click(object sender, EventArgs e)
        {
            if (MsgBox.Show("您確定要清除此學生的照片嗎？", "", MessageBoxButtons.YesNo) == DialogResult.No) return;

            try
            {
                pic1String = string.Empty;
                pic1.Image = pic1.InitialImage;
                EditStudent.UpdateFreshmanPhoto("", RunningID);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
        }

        //清除畢業照片
        private void buttonItem6_Click(object sender, EventArgs e)
        {
            if (MsgBox.Show("您確定要清除此學生的照片嗎？", "", MessageBoxButtons.YesNo) == DialogResult.No) return;

            try
            {
                pic2String = string.Empty;
                pic2.Image = pic2.InitialImage;
                EditStudent.UpdateGraduatePhoto("", RunningID);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
