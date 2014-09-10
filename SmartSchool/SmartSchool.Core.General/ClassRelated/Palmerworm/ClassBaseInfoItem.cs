using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature.Class;
using SmartSchool.Feature.Teacher;
using System.Xml;
using DevComponents.DotNetBar;
using SmartSchool.ApplicationLog;
using SmartSchool.TeacherRelated;
using SmartSchool.AccessControl;

namespace SmartSchool.ClassRelated.Palmerworm
{
    [FeatureCode("Content0150")]
    internal partial class ClassBaseInfoItem : ClassPalmerwormItem
    {
        private TeacherCollection _teacherList;
        private DSXmlHelper _dept_list;
        private DSXmlHelper _gradeYearList;
        private ErrorProvider epTeacher = new ErrorProvider();
        private ErrorProvider epDept = new ErrorProvider();
        private ErrorProvider epDisplayOrder = new ErrorProvider();
        private ErrorProvider epGradeYear = new ErrorProvider();
        private ErrorProvider epClassName = new ErrorProvider();

        private string _NamingRule = "";
        private bool _StopEvent = false;
        #region Log 需要用到的變數

        private ClassBaseLogMachine machine = new ClassBaseLogMachine();

        #endregion

        public ClassBaseInfoItem()
            : base()
        {
            Font = FontStyles.General;
            InitializeComponent();

            if (Site != null && Site.DesignMode)
                return;

            Initialize();
            Title = "班級基本資料";
        }


        private void Initialize()
        {
            TeacherRelated.Teacher.Instance.TeacherDataChanged += new EventHandler<TeacherDataChangedEventArgs>(Instance_TeacherDataChanged);
            TeacherRelated.Teacher.Instance.TeacherDeleted += new EventHandler<TeacherDeletedEventArgs>(Instance_TeacherDeleted);
            TeacherRelated.Teacher.Instance.TeacherInserted += new EventHandler(Instance_TeacherInserted);

            // 取得所有教師名單
            InitializeTeacherList();

            _dept_list = Feature.Basic.Config.GetDepartment().GetContent();
            InitializeDeptList();

            // 取得所有年級清單
            _gradeYearList = QueryClass.GetGradeYearList().GetContent();
            List<string> gList = new List<string>();
            foreach (XmlNode node in _gradeYearList.GetElements("GradeYear"))
            {
                string gradeYear = node.SelectSingleNode("GradeYear").InnerText;
                if (!gList.Contains(gradeYear))
                {
                    gList.Add(gradeYear);
                    cboGradeYear.Items.Add(gradeYear);
                }
            }
        }

        private void InitializeTeacherList()
        {
            //if (TeacherRelated.Teacher.Instance.Items.Count <= 0)
            //{
            //    //理論上這裡的程式應該不會被執行...

            //    DSXmlHelper hlpTeacher = QueryTeacher.GetTeacherDetailTest().GetContent(); //為什麼是 Test ?
            //    Dictionary<string, BriefTeacherData> dicTeacher = new Dictionary<string, BriefTeacherData>();

            //    foreach (XmlElement eachTeacher in hlpTeacher.GetElements("Teacher"))
            //    {
            //        BriefTeacherData objTeacher = new BriefTeacherData(eachTeacher);

            //        if (objTeacher.Status == "一般")
            //            dicTeacher.Add(objTeacher.ID, objTeacher);
            //    }

            //    _teacherList = new TeacherCollection(dicTeacher);
            //}
            //else
            {
                Dictionary<string, BriefTeacherData> temp = new Dictionary<string, BriefTeacherData>();
                foreach ( BriefTeacherData each in TeacherRelated.Teacher.Instance.Items )
                {
                    if ( each.Status == "一般" )
                        temp.Add(each.ID, each);
                }

                _teacherList = new TeacherCollection(temp);
            }

            cboTeacher.SelectedItem = null;
            cboTeacher.Items.Clear();
            foreach (BriefTeacherData each in _teacherList)
                cboTeacher.Items.Add(each);
        }

        private void Instance_TeacherInserted(object sender, EventArgs e)
        {
            InitializeTeacherList();
        }

        private void Instance_TeacherDeleted(object sender, TeacherDeletedEventArgs e)
        {
            InitializeTeacherList();
        }

        private void Instance_TeacherDataChanged(object sender, TeacherDataChangedEventArgs e)
        {
            InitializeTeacherList();
        }

        private void InitializeDeptList()
        {
            // 取得所有科別清單
            cboDept.SelectedItem = null;
            cboDept.Items.Clear();
            cboDept.Items.Add(new KeyValuePair<string, string>("", ""));
            foreach (XmlElement node in _dept_list.GetElements("Department"))
            {
                string id = node.GetAttribute("ID");
                string name = node.SelectSingleNode("Name").InnerText;
                cboDept.Items.Add(new KeyValuePair<string, string>(id, name));
            }
            cboDept.ValueMember = "Key";
            cboDept.DisplayMember = "Value";
        }

        protected override object OnBackgroundWorkerWorking()
        {
            _dept_list = Feature.Basic.Config.GetDepartment().GetContent();
            return QueryClass.GetClass(RunningID);
        }

        protected override void OnBackgroundWorkerCompleted(object result)
        {
            InitializeDeptList();

            //清除舊資料
            epTeacher.Clear();
            cboTeacher.Tag = null;
            epDept.Clear();
            cboDept.Tag = null;
            epDisplayOrder.Clear();
            txtSortOrder.Tag = null;
            epGradeYear.Clear();
            cboGradeYear.Tag = null;
            epClassName.Clear();
            txtClassName.Tag = null;

            DSResponse dsrsp = result as DSResponse;
            DSXmlHelper helper = dsrsp.GetContent();
            txtClassName.Text = helper.GetText("Class/ClassName");
            _NamingRule = helper.GetText("Class/NamingRule");
            string deptId = helper.GetText("Class/RefDepartmentID");
            string deptName = helper.GetText("Class/Department");
            cboDept.SelectedItem = new KeyValuePair<string, string>(deptId, deptName);

            cboTeacher.SelectedItem = GetTeacherObject(helper.GetText("Class/RefTeacherID"));
            cboGradeYear.Text = helper.GetText("Class/GradeYear");
            txtSortOrder.Text = helper.GetText("Class/DisplayOrder");
            _valueManager.AddValue("ClassName", txtClassName.Text, "班級名稱");
            _valueManager.AddValue("NamingRule", _NamingRule, "命名規則");
            _valueManager.AddValue("RefDepartmentID", GetDepartmentValue(), "科別");
            _valueManager.AddValue("GradeYear", cboGradeYear.Text, "年級");
            _valueManager.AddValue("RefTeacherID", GetTeacherID(cboTeacher.SelectedItem), "班導師");
            _valueManager.AddValue("DisplayOrder", txtSortOrder.Text, "排列序號");
            SaveButtonVisible = false;

            #region Log 記錄修改前的資料

            machine.AddBefore(labelX1.Text.Replace("　", ""), txtClassName.Text);
            machine.AddBefore("命名規則", _NamingRule);
            machine.AddBefore(labelX2.Text.Replace("　", ""), deptName);
            machine.AddBefore(labelX3.Text.Replace("　", ""), cboTeacher.Text);
            machine.AddBefore(labelX4.Text.Replace("　", ""), cboGradeYear.Text);
            machine.AddBefore(labelX6.Text.Replace("　", ""), txtSortOrder.Text);

            #endregion
        }

        private string GetDepartmentValue()
        {
            KeyValuePair<string, string>? kv = cboDept.SelectedItem as KeyValuePair<string, string>?;

            if (kv == null)
                return string.Empty;
            else
                return kv.Value.Key;
        }

        protected string GetTeacherID(object teacher)
        {
            BriefTeacherData objTeacher = teacher as BriefTeacherData;

            if (objTeacher == null)
                return string.Empty;
            else
                return objTeacher.ID;
        }

        protected BriefTeacherData GetTeacherObject(string id)
        {
            if (_teacherList == null)
                return null;

            if (_teacherList.ContainsKey(id))
                return _teacherList[id];
            else
                return null;
        }

        protected void cboTeacher_TextChanged(object sender, EventArgs e)
       {
            int index = cboTeacher.FindStringExact(cboTeacher.Text);
            string id = "";

            if (index >= 0)
                id = GetTeacherID(cboTeacher.Items[index]);
            else
                id = "";
            //cboTeacher.FindString(cboTeacher.Text);
            OnValueChanged("RefTeacherID", id);
        }

        protected void txtClassName_TextChanged(object sender, EventArgs e)
        {
            if ( !_StopEvent )
            {
                if ( txtClassName.ContainsFocus && Class.Instance.ValidateNamingRule(txtClassName.Text) )
                {
                    OnValueChanged("NamingRule", txtClassName.Text);
                    int gradeYear;
                    if(int.TryParse(cboGradeYear.Text,out gradeYear))
                        OnValueChanged("ClassName", Class.Instance.ParseClassName(txtClassName.Text, gradeYear));
                }
                else
                    OnValueChanged("ClassName", txtClassName.Text);

                //如果班級名稱是由其它控制項做更改 (選擇年級，班級名稱自動變換)
                if (!txtClassName.ContainsFocus)
                    txtClassName_Validated(null, null);
            }
        }

        protected void cboDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnValueChanged("RefDepartmentID", GetDepartmentValue());
        }

        protected void cboGradeYear_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("GradeYear", cboGradeYear.Text);
            if ( Class.Instance.ValidateNamingRule(_NamingRule) )
            {
                int gradeYear = 0;
                if ( int.TryParse(cboGradeYear.Text, out gradeYear) )
                {
                    txtClassName.Text = Class.Instance.ParseClassName(_NamingRule, gradeYear);
                }
                else
                    txtClassName.Text = _NamingRule;
            }
        }

        protected void txtSortOrder_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged("DisplayOrder", txtSortOrder.Text);
        }

        public override bool IsValid()
        {
            // 班級名稱驗證
            bool valid = true;
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.Tag != null)
                {
                    if (ctrl.Tag.ToString() == "error")
                        valid = false;
                }
            }
            return valid;
        }

        private void txtClassName_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtClassName.Text))
            {
                epClassName.SetError(txtClassName, "班級名稱不可空白");
                txtClassName.Tag = "error";
                return;
            }
            if (!Class.Instance.ValidClassName(RunningID, txtClassName.Text))
            {
                epClassName.SetError(txtClassName, "班級不可與其它班級重覆");
                txtClassName.Tag = "error";
                return;
            }
            epClassName.Clear();
            txtClassName.Tag = null;
        }

        private void cboTeacher_Validated(object sender, EventArgs e)
        {
            string id = GetTeacherID(cboTeacher.SelectedItem);

            if (!string.IsNullOrEmpty(cboTeacher.Text) && id == "")
            {
                epTeacher.SetError(cboTeacher, "查無此教師");
                cboTeacher.Tag = "error";
                return;
            }
            else
            {
                epTeacher.Clear();
                cboTeacher.Tag = id;
            }
        }

        private void cboTeacher_Validating(object sender, CancelEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            int index = combo.FindStringExact(combo.Text);
            if (index >= 0)
            {
                combo.SelectedItem = combo.Items[index];
            }

        }

        private void cboDept_Validated(object sender, EventArgs e)
        {
            //string name = cboDept.Text;
            //XmlElement element = null;
            //foreach (XmlElement ele in _dept_list.GetElements("Department"))
            //{
            //    if (ele.SelectSingleNode("Name").InnerText == name)
            //        element = ele;
            //}

            //if (!string.IsNullOrEmpty(name) && element == null)
            //{
            //    epDept.Icon = Properties.Resources.warning;
            //    epDept.SetError(cboDept, "查無此科別");
            //    cboDept.Tag = null;
            //    return;
            //}
            //else
            //{
            //    epDept.Clear();
            //    cboDept.Tag = null;
            //    int index = cboDept.FindString(name);
            //}
        }

        private void txtSortOrder_Validated(object sender, EventArgs e)
        {
            string text = txtSortOrder.Text;
            int i;
            if (!string.IsNullOrEmpty(text) && !int.TryParse(text, out i))
            {
                epDisplayOrder.SetError(txtSortOrder, "請輸入整數");
                txtSortOrder.Tag = "error";
                return;
            }
            epDisplayOrder.Clear();
            txtSortOrder.Tag = null;
        }

        private void cboGradeYear_Validated(object sender, EventArgs e)
        {
            string text = cboGradeYear.Text;
            bool hasGradeYear = false;
            foreach (XmlElement ele in _gradeYearList.GetElements("GradeYear"))
            {
                if (ele.SelectSingleNode("GradeYear").InnerText == text)
                    hasGradeYear = true;
            }

            int i;
            if (!string.IsNullOrEmpty(text) && !int.TryParse(text, out i))
            {
                epGradeYear.Icon = Properties.Resources.error;
                epGradeYear.SetError(cboGradeYear, "年級必須為數字");
                cboGradeYear.Tag = "error";
                return;
            }

            if (!string.IsNullOrEmpty(text) && !hasGradeYear)
            {
                epGradeYear.Icon = Properties.Resources.warning;
                epGradeYear.SetError(cboGradeYear, "無此年級");
                cboGradeYear.Tag = null;
            }
            else
            {
                epGradeYear.Clear();
                cboGradeYear.Tag = null;
            }
        }

        public override void Save()
        {
            if (!IsValid())
            {
                MsgBox.Show("輸入資料未通過驗證，請修正後再行儲存");
                return;
            }
            DSXmlHelper helper = new DSXmlHelper("UpdateRequest");
            helper.AddElement("Class");
            helper.AddElement("Class", "Field");
            Dictionary<string, string> items = _valueManager.GetDirtyItems();
            foreach (string key in items.Keys)
                helper.AddElement("Class/Field", key, items[key]);
            helper.AddElement("Class", "Condition");
            helper.AddElement("Class/Condition", "ID", RunningID);
            EditClass.Update(new DSRequest(helper));

            //if (_valueManager.IsDirtyItem("ClassName"))
            //    Class.Instance.InvokClassUpdated(RunningID);

            #region Log

            #region Log 記錄修改後的資料

            machine.AddAfter(labelX1.Text.Replace("　", ""), txtClassName.Text);
            machine.AddAfter("命名規則", _NamingRule);
            machine.AddAfter(labelX2.Text.Replace("　", ""), cboDept.Text);
            machine.AddAfter(labelX3.Text.Replace("　", ""), cboTeacher.Text);
            machine.AddAfter(labelX4.Text.Replace("　", ""), cboGradeYear.Text);
            machine.AddAfter(labelX6.Text.Replace("　", ""), txtSortOrder.Text);

            #endregion

            StringBuilder desc = new StringBuilder("");
            desc.AppendLine("班級名稱：" + Class.Instance.Items[RunningID].ClassName + " ");
            desc.AppendLine(machine.GetDescription());

            CurrentUser.Instance.AppLog.Write(EntityType.Class, EntityAction.Update, RunningID, desc.ToString(), "班級基本資料", "");

            #endregion

            Class.Instance.InvokClassUpdated(RunningID);

            SaveButtonVisible = false;
        }

        private void txtClassName_Enter(object sender, EventArgs e)
        {
            if ( Class.Instance.ValidateNamingRule(_NamingRule) )
            {
                _StopEvent =true;
                txtClassName.Text = _NamingRule;
                _StopEvent = false;
            }
        }

        private void txtClassName_Leave(object sender, EventArgs e)
        {
            if ( Class.Instance.ValidateNamingRule(txtClassName.Text) )
            {
                _NamingRule = txtClassName.Text;
                OnValueChanged("NamingRule", _NamingRule);
                int gradeYear = 0;
                if ( int.TryParse(cboGradeYear.Text, out gradeYear) )
                {
                    txtClassName.Text = Class.Instance.ParseClassName(_NamingRule, gradeYear);
                }
            }
        }

        private void cboDept_TextChanged(object sender, EventArgs e)
        {
            string name = cboDept.Text;
            XmlElement element = null;
            foreach (XmlElement ele in _dept_list.GetElements("Department"))
            {
                if (ele.SelectSingleNode("Name").InnerText == name)
                    element = ele;
            }

            if (!string.IsNullOrEmpty(name) && element == null)
            {
                epDept.Icon = Properties.Resources.warning;
                epDept.SetError(cboDept, "查無此科別");
                cboDept.Tag = null;
                return;
            }
            else
            {
                epDept.Clear();
                cboDept.Tag = null;
                int index = cboDept.FindString(name);
            }
        }


    }

    /// <summary>
    /// 記錄班級基本資料
    /// </summary>
    class ClassBaseLogMachine
    {
        Dictionary<string, string> beforeData = new Dictionary<string, string>();
        Dictionary<string, string> afterData = new Dictionary<string, string>();

        public void AddBefore(string key, string value)
        {
            if (!beforeData.ContainsKey(key))
                beforeData.Add(key, value);
            else
                beforeData[key] = value;
        }

        public void AddAfter(string key, string value)
        {
            if (!afterData.ContainsKey(key))
                afterData.Add(key, value);
            else
                afterData[key] = value;
        }

        public string GetDescription()
        {
            //「」
            StringBuilder desc = new StringBuilder("");

            foreach (string key in beforeData.Keys)
            {
                if (afterData.ContainsKey(key) && afterData[key] != beforeData[key])
                    desc.AppendLine("欄位「" + key + "」由「" + beforeData[key] + "」變更為「" + afterData[key] + "」");
            }

            return desc.ToString();
        }
    }
}
