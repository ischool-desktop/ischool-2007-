using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.StudentRelated.Palmerworm;
using IntelliSchool.DSA30.Util;
using SmartSchool.Common;
using SmartSchool.Feature.Basic;
using System.Xml;
using SmartSchool.Feature;
using SmartSchool.AccessControl;

namespace SmartSchool.StudentRelated.Palmerworm
{
    [FeatureCode("Content0030")]
    internal partial class ParentInfoPalmerwormItem : PalmerwormItem
    {
        private string _fatherName;
        private string _fatherNationality;
        private string _fatherAlive;
        private string _fatherEduDegree;
        private string _fatherJob;
        private string _fatherPhone;
        private string _fatherIDNumber;
        private XmlNode _fatherOtherInfoNode;

        private string _motherName;
        private string _motherNationality;
        private string _motherAlive;
        private string _motherEduDegree;
        private string _motherJob;
        private string _motherPhone;
        private string _motherIDNumber;
        private XmlNode _motherOtherInfoNode;

        private string _guardianName;
        private string _guardianNationality;
        private string _guardianEduDegree;
        private string _guardianJob;
        private string _guardianRelationship;
        private string _guardianPhone;
        private string _guardianIDNumber;
        private XmlNode _guardianOtherInfoNode;

        private BackgroundWorker _getRelationshipBackgroundWorker;
        private BackgroundWorker _getJobBackgroundWorker;
        private BackgroundWorker _getEduDegreeBackgroundWorker;
        private BackgroundWorker _getNationalityBackgroundWorker;

        //private DSResponse _relationshipList;

        private bool _isInitialized = false;

        public ParentInfoPalmerwormItem()
        {
            InitializeComponent();
            Title = "父母親及監護人資料";

            _getRelationshipBackgroundWorker = new BackgroundWorker();
            _getRelationshipBackgroundWorker.DoWork += new DoWorkEventHandler(_getRelationshipBackgroundWorker_DoWork);
            _getRelationshipBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_getRelationshipBackgroundWorker_RunWorkerCompleted);
            _getRelationshipBackgroundWorker.RunWorkerAsync();

            _getJobBackgroundWorker = new BackgroundWorker();
            _getJobBackgroundWorker.DoWork += new DoWorkEventHandler(_getJobBackgroundWorker_DoWork);
            _getJobBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_getJobBackgroundWorker_RunWorkerCompleted);
            _getJobBackgroundWorker.RunWorkerAsync();

            _getEduDegreeBackgroundWorker = new BackgroundWorker();
            _getEduDegreeBackgroundWorker.DoWork += new DoWorkEventHandler(_getEduDegreeBackgroundWorker_DoWork);
            _getEduDegreeBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_getEduDegreeBackgroundWorker_RunWorkerCompleted);
            _getEduDegreeBackgroundWorker.RunWorkerAsync();

            _getNationalityBackgroundWorker = new BackgroundWorker();
            _getNationalityBackgroundWorker.DoWork += new DoWorkEventHandler(_getNationalityBackgroundWorker_DoWork);
            _getNationalityBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_getNationalityBackgroundWorker_RunWorkerCompleted);
            _getNationalityBackgroundWorker.RunWorkerAsync();
        }

        void _getNationalityBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DSXmlHelper helper = (e.Result as DSResponse).GetContent();

            foreach (XmlNode node in helper.GetElements("Nationality"))
            {
                cboNationality.Items.Add(new KeyValuePair<string, string>(node.InnerText, node.InnerText));
            }
        }

        void _getNationalityBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = Config.GetNationalityList();
        }

        void _getEduDegreeBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DSXmlHelper helper = (e.Result as DSResponse).GetContent();

            foreach (XmlNode node in helper.GetElements("EducationDegree"))
            {
                cboEduDegree.Items.Add(new KeyValuePair<string, string>(node.InnerText, node.InnerText));
            }
        }

        void _getEduDegreeBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = Config.GetEduDegreeList();
        }

        void _getJobBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DSXmlHelper helper = (e.Result as DSResponse).GetContent();

            foreach (XmlNode node in helper.GetElements("Job"))
            {
                cboJob.Items.Add(new KeyValuePair<string, string>(node.InnerText, node.InnerText));
            }
        }

        void _getJobBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = Config.GetJobList();
        }

        void _getRelationshipBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DSXmlHelper helper = (e.Result as DSResponse).GetContent();

            foreach (XmlNode node in helper.GetElements("Relationship"))
            {
                cboRelationship.Items.Add(new KeyValuePair<string, string>(node.InnerText, node.InnerText));
            }
        }

        void _getRelationshipBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = Config.GetRelationship();
        }

        protected override object OnBackgroundWorkerWorking()
        {
            return QueryStudent.GetParentInfo(RunningID).GetContent();
        }

        protected override void OnBackgroundWorkerCompleted(object result)
        {
            Initialize();

            DSXmlHelper helper = result as DSXmlHelper;
            _fatherAlive = helper.GetText("FatherLiving");
            _fatherOtherInfoNode = helper.GetElement("FatherOtherInfo");
            _fatherEduDegree = helper.GetText("FatherOtherInfo/FatherEducationDegree");
            _fatherJob = helper.GetText("FatherOtherInfo/FatherJob");
            _fatherPhone = helper.GetText("FatherOtherInfo/FatherPhone");
            _fatherName = helper.GetText("FatherName");
            _fatherNationality = helper.GetText("FatherNationality");
            _fatherIDNumber = helper.GetText("FatherIDNumber");

            _motherAlive = helper.GetText("MotherLiving");
            _motherOtherInfoNode = helper.GetElement("MotherOtherInfo");
            _motherEduDegree = helper.GetText("MotherOtherInfo/MotherEducationDegree");
            _motherJob = helper.GetText("MotherOtherInfo/MotherJob");
            _motherPhone = helper.GetText("MotherOtherInfo/MotherPhone");
            _motherName = helper.GetText("MotherName");
            _motherNationality = helper.GetText("MotherNationality");
            _motherIDNumber = helper.GetText("MotherIDNumber");

            _guardianOtherInfoNode = helper.GetElement("CustodianOtherInfo");
            _guardianEduDegree = helper.GetText("CustodianOtherInfo/EducationDegree");
            _guardianJob = helper.GetText("CustodianOtherInfo/Job");
            _guardianPhone = helper.GetText("CustodianOtherInfo/Phone");
            _guardianName = helper.GetText("CustodianName");
            _guardianNationality = helper.GetText("CustodianNationality");
            _guardianRelationship = helper.GetText("CustodianRelationship");
            _guardianIDNumber = helper.GetText("CustodianIDNumber");

            _valueManager.AddValue("FatherLiving", _fatherAlive, "父存歿");
            _valueManager.AddValue("FatherEducationDegree", _fatherEduDegree, "父最高學歷");
            _valueManager.AddValue("FatherJob", _fatherJob, "父職業");
            _valueManager.AddValue("FatherName", _fatherName, "父姓名");
            _valueManager.AddValue("FatherNationality", _fatherNationality, "父國籍");
            _valueManager.AddValue("FatherIDNumber", _fatherIDNumber, "父身分證號");

            _valueManager.AddValue("MotherLiving", _motherAlive, "母存歿");
            _valueManager.AddValue("MotherEducationDegree", _motherEduDegree, "母最高學歷");
            _valueManager.AddValue("MotherJob", _motherJob, "母職業");
            _valueManager.AddValue("MotherName", _motherName, "母姓名");
            _valueManager.AddValue("MotherNationality", _motherNationality, "母國籍");
            _valueManager.AddValue("MotherIDNumber", _motherIDNumber, "母身分證號");

            _valueManager.AddValue("CustodianRelationship", _guardianRelationship, "監護人稱謂");
            _valueManager.AddValue("EducationDegree", _guardianEduDegree, "監護人最高學歷");
            _valueManager.AddValue("Job", _guardianJob, "監護人職業");
            _valueManager.AddValue("CustodianName", _guardianName, "監護人姓名");
            _valueManager.AddValue("CustodianNationality", _guardianNationality, "監護人國籍");
            _valueManager.AddValue("CustodianIDNumber", _guardianIDNumber, "監護人身分證號");
            LoadGuardian();
        }

        private void Initialize()
        {
            if (_isInitialized) return;
            //載入稱謂
            //DSResponse dsrsp = Config.GetRelationship();
            //DSXmlHelper helper = dsrsp.GetContent();
            //DSXmlHelper helper = _relationshipList.GetContent();
            //DSXmlHelper helper;
            KeyValuePair<string, string> kvp = new KeyValuePair<string, string>("", "請選擇");
            cboRelationship.Items.Add(kvp);
            //foreach (XmlNode node in helper.GetElements("Relationship"))
            //{
            //    kvp = new KeyValuePair<string, string>(node.InnerText, node.InnerText);
            //    cboRelationship.Items.Add(kvp);
            //}
            cboRelationship.DisplayMember = "Value";
            cboRelationship.ValueMember = "Key";


            //載入職業
            //DSResponse dsrsp = Config.GetJobList();
            //helper = dsrsp.GetContent();
            //kvp = new KeyValuePair<string, string>("", "請選擇");
            cboJob.Items.Add(kvp);
            //foreach (XmlNode node in helper.GetElements("Job"))
            //{
            //    kvp = new KeyValuePair<string, string>(node.InnerText, node.InnerText);
            //    cboJob.Items.Add(kvp);
            //}
            cboJob.DisplayMember = "Value";
            cboJob.ValueMember = "Key";


            //載入學歷
            //dsrsp = Config.GetEduDegreeList();
            //helper = dsrsp.GetContent();
            //kvp = new KeyValuePair<string, string>("", "請選擇");
            cboEduDegree.Items.Add(kvp);
            //foreach (XmlNode node in helper.GetElements("EducationDegree"))
            //{
            //    kvp = new KeyValuePair<string, string>(node.InnerText, node.InnerText);
            //    cboEduDegree.Items.Add(kvp);
            //}
            cboEduDegree.DisplayMember = "Value";
            cboEduDegree.ValueMember = "Key";


            //載入國籍
            //dsrsp = Config.GetNationalityList();
            //helper = dsrsp.GetContent();
            //kvp = new KeyValuePair<string, string>("", "請選擇");
            cboNationality.Items.Add(kvp);
            //foreach (XmlNode node in helper.GetElements("Nationality"))
            //{
            //    kvp = new KeyValuePair<string, string>(node.InnerText, node.InnerText);
            //    cboNationality.Items.Add(kvp);
            //}
            cboNationality.DisplayMember = "Value";
            cboNationality.ValueMember = "Key";



            //載入存殁
            kvp = new KeyValuePair<string, string>("存", "存");
            cboAlive.Items.Add(kvp);
            kvp = new KeyValuePair<string, string>("歿", "歿");
            cboAlive.Items.Add(kvp);
            cboAlive.DisplayMember = "Value";
            cboAlive.ValueMember = "Key";

            _isInitialized = true;
        }

        public override void Save()
        {
            DSXmlHelper helper = new DSXmlHelper("UpdateParentInfoRequest");
            helper.AddElement("Student");
            helper.AddElement("Student", "Field");

            Dictionary<string, string> changes = _valueManager.GetDirtyItems();
            foreach (string key in changes.Keys)
            {
                if (!key.EndsWith("EducationDegree") && !key.EndsWith("Job"))
                    helper.AddElement("Student/Field", key, changes[key]);
            }

            if (_valueManager.IsDirtyItem("FatherEducationDegree"))
            {
                string info = GetEduDegreeResponse(_fatherOtherInfoNode, "FatherEducationDegree", _fatherEduDegree, "FatherOtherInfo");
                _fatherOtherInfoNode = DSXmlHelper.LoadXml(info, true);

            }

            if (_valueManager.IsDirtyItem("MotherEducationDegree"))
            {
                string info = GetEduDegreeResponse(_motherOtherInfoNode, "MotherEducationDegree", _motherEduDegree, "MotherOtherInfo");
                _motherOtherInfoNode = DSXmlHelper.LoadXml(info, true);
            }

            if (_valueManager.IsDirtyItem("EducationDegree"))
            {
                string info = GetEduDegreeResponse(_guardianOtherInfoNode, "EducationDegree", _guardianEduDegree, "CustodianOtherInfo");
                _guardianOtherInfoNode = DSXmlHelper.LoadXml(info, true);
            }

            //if (_valueManager.IsDirtyItem("CustodianRelationship"))
            //{
            //    string info = GetEduDegreeResponse(_guardianOtherInfoNode, "CustodianRelationship", _guardianRelationship, "CustodianOtherInfo");
            //    _guardianOtherInfoNode = DSXmlHelper.LoadXml(info, true);
            //}

            if (_valueManager.IsDirtyItem("FatherJob"))
            {
                string info = GetEduDegreeResponse(_fatherOtherInfoNode, "FatherJob", _fatherJob, "FatherOtherInfo");
                _fatherOtherInfoNode = DSXmlHelper.LoadXml(info, true);
            }

            if (_valueManager.IsDirtyItem("MotherJob"))
            {
                string info = GetEduDegreeResponse(_motherOtherInfoNode, "MotherJob", _motherJob, "MotherOtherInfo");
                _motherOtherInfoNode = DSXmlHelper.LoadXml(info, true);
            }

            if (_valueManager.IsDirtyItem("Job"))
            {
                string info = GetEduDegreeResponse(_guardianOtherInfoNode, "Job", _guardianJob, "CustodianOtherInfo");
                _guardianOtherInfoNode = DSXmlHelper.LoadXml(info, true);
            }
            
            if (_valueManager.IsDirtyItem("FatherJob") || _valueManager.IsDirtyItem("FatherEducationDegree"))
            {
                helper.AddElement("Student/Field", "FatherOtherInfo");
                helper.AddCDataSection("Student/Field/FatherOtherInfo", _fatherOtherInfoNode.OuterXml);
            }

            if (_valueManager.IsDirtyItem("MotherJob") || _valueManager.IsDirtyItem("MotherEducationDegree"))
            {
                helper.AddElement("Student/Field", "MotherOtherInfo");
                helper.AddCDataSection("Student/Field/MotherOtherInfo", _motherOtherInfoNode.OuterXml);
            }

            if (_valueManager.IsDirtyItem("Job") || _valueManager.IsDirtyItem("EducationDegree"))
            {
                helper.AddElement("Student/Field", "CustodianOtherInfo");
                helper.AddCDataSection("Student/Field/CustodianOtherInfo", _guardianOtherInfoNode.OuterXml);
            }

            helper.AddElement("Student", "Condition");
            helper.AddElement("Student/Condition", "ID", RunningID);
            EditStudent.Update(new DSRequest(helper));

            LogUtility.LogChange(_valueManager, RunningID, GetStudentName());

            SaveButtonVisible = false;
        }

        private string GetStudentName()
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

        private void btnGuardian_Click(object sender, EventArgs e)
        {
            LoadGuardian();
        }

        private void btnFather_Click(object sender, EventArgs e)
        {
            btnGuardian.Enabled = true;
            btnFather.Enabled = false;
            btnMother.Enabled = true;

            cboAlive.Visible = true;
            lblAlive.Visible = true;
            cboRelationship.Visible = false;
            lblRelationship.Visible = false;

            btnParentType.Text = btnFather.Text;
            txtParentName.Text = _fatherName;
            txtIDNumber.Text = _fatherIDNumber;

            cboAlive.SetComboBoxValue(_fatherAlive);
            cboNationality.SetComboBoxValue(_fatherNationality);
            cboJob.SetComboBoxValue(_fatherJob);
            cboEduDegree.SetComboBoxValue(_fatherEduDegree);

            lnkCopyGuardian.Visible = true;
            lnkCopyFather.Visible = false;
            lnkCopyMother.Visible = false;
        }

        private void btnMother_Click(object sender, EventArgs e)
        {
            btnGuardian.Enabled = true;
            btnFather.Enabled = true;
            btnMother.Enabled = false;

            cboAlive.Visible = true;
            lblAlive.Visible = true;
            cboRelationship.Visible = false;
            lblRelationship.Visible = false;

            btnParentType.Text = btnMother.Text;
            txtParentName.Text = _motherName;
            txtIDNumber.Text = _motherIDNumber;

            cboAlive.SetComboBoxValue(_motherAlive);
            cboNationality.SetComboBoxValue(_motherNationality);
            cboJob.SetComboBoxValue(_motherJob);
            cboEduDegree.SetComboBoxValue(_motherEduDegree);

            lnkCopyGuardian.Visible = true;
            lnkCopyFather.Visible = false;
            lnkCopyMother.Visible = false;
        }

        private void LoadGuardian()
        {
            btnGuardian.Enabled = false;
            btnFather.Enabled = true;
            btnMother.Enabled = true;

            cboAlive.Visible = false;
            lblAlive.Visible = false;
            cboRelationship.Visible = true;
            lblRelationship.Visible = true;

            btnParentType.Text = btnGuardian.Text;
            txtParentName.Text = _guardianName;
            txtIDNumber.Text = _guardianIDNumber;

            cboRelationship.SetComboBoxValue(_guardianRelationship);
            cboNationality.SetComboBoxValue(_guardianNationality);
            cboJob.SetComboBoxValue(_guardianJob);
            cboEduDegree.SetComboBoxValue(_guardianEduDegree);

            lnkCopyGuardian.Visible = false;
            lnkCopyFather.Visible = true;
            lnkCopyMother.Visible = true;
        }

        private void txtParentName_TextChanged(object sender, EventArgs e)
        {
            string typeName;
            string value = txtParentName.Text;

            if (btnParentType.Text == "監護人")
            {
                typeName = "CustodianName";
                _guardianName = value;
            }
            else if (btnParentType.Text == "父親")
            {
                typeName = "FatherName";
                _fatherName = value;
            }
            else
            {
                typeName = "MotherName";
                _motherName = value;
            }
            OnValueChanged(typeName, value);
        }

        private void cboNationality_SelectedIndexChanged(object sender, EventArgs e)
        {
            string typeName;
            string value = cboNationality.GetValue();

            if (btnParentType.Text == "監護人")
            {
                typeName = "CustodianNationality";
                _guardianNationality = value;
            }
            else if (btnParentType.Text == "父親")
            {
                typeName = "FatherNationality";
                _fatherNationality = value;
            }
            else
            {
                typeName = "MotherNationality";
                _motherNationality = value;
            }
            OnValueChanged(typeName, value);
        }

        private string GetEduDegreeResponse(XmlNode infoNode, string degreeNodeName, string eduDegree, string otherInfoNodeName)
        {
            if (infoNode != null)
            {
                XmlNode node = infoNode.SelectSingleNode(degreeNodeName);
                if (node != null)
                {
                    node.InnerText = _valueManager.GetDirtyItems()[degreeNodeName];
                }
                else
                {
                    XmlNode n = infoNode.OwnerDocument.CreateElement(degreeNodeName);
                    n.InnerText = eduDegree;
                    infoNode.AppendChild(n);
                }
                return infoNode.OuterXml;
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                XmlNode n = doc.CreateElement(otherInfoNodeName);
                XmlNode n1 = doc.CreateElement(degreeNodeName);
                n1.InnerText = eduDegree;
                n.AppendChild(n1);
                return n.OuterXml;
            }
        }

        private void lnkCopyGuardian_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtParentName.Text = _guardianName;
            txtIDNumber.Text = _guardianIDNumber;
            cboNationality.SetComboBoxValue(_guardianNationality);
            cboJob.SetComboBoxValue(_guardianJob);
            cboEduDegree.SetComboBoxValue(_guardianEduDegree);            
        }

        private void lnkCopyFather_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtParentName.Text = _fatherName;
            txtIDNumber.Text = _fatherIDNumber;
            cboNationality.SetComboBoxValue(_fatherNationality);
            cboJob.SetComboBoxValue(_fatherJob);
            cboEduDegree.SetComboBoxValue(_fatherEduDegree);
            if (btnParentType.Text == "監護人")
                cboRelationship.SetComboBoxValue("父");
        }

        private void lnkCopyMother_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtParentName.Text = _motherName;
            txtIDNumber.Text = _motherIDNumber;
            cboNationality.SetComboBoxValue(_motherNationality);
            cboJob.SetComboBoxValue(_motherJob);
            cboEduDegree.SetComboBoxValue(_motherEduDegree);
            if (btnParentType.Text == "監護人")
                cboRelationship.SetComboBoxValue("母");
        }

        private void cboNationality_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string typeName;
            string value = cboNationality.GetValue();

            if (btnParentType.Text == "監護人")
            {
                typeName = "CustodianNationality";
                _guardianNationality = value;
            }
            else if (btnParentType.Text == "父親")
            {
                typeName = "FatherNationality";
                _fatherNationality = value;
            }
            else
            {
                typeName = "MotherNationality";
                _motherNationality = value;
            }
            OnValueChanged(typeName, value);
        }

        private void cboRelationship_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string typeName;
            string value = cboRelationship.GetValue();

            if (btnParentType.Text == "監護人")
            {
                typeName = "CustodianRelationship";
                _guardianRelationship = value;
            }
            else if (btnParentType.Text == "父親")
            {
                return;
            }
            else
            {
                return;
            }
            OnValueChanged(typeName, value);
        }

        private void cboAlive_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string typeName;
            string value = cboAlive.GetValue();

            if (btnParentType.Text == "監護人")
            {
                return;
            }
            else if (btnParentType.Text == "父親")
            {
                typeName = "FatherLiving";
                _fatherAlive = value;
            }
            else
            {
                typeName = "MotherLiving";
                _motherAlive = value;
            }
            OnValueChanged(typeName, value);
        }

        private void cboEduDegree_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string typeName;
            string value = cboEduDegree.GetValue();

            if (btnParentType.Text == "監護人")
            {
                typeName = "EducationDegree";
                _guardianEduDegree = value;
            }
            else if (btnParentType.Text == "父親")
            {
                typeName = "FatherEducationDegree";
                _fatherEduDegree = value;
            }
            else
            {
                typeName = "MotherEducationDegree";
                _motherEduDegree = value;
            }
            OnValueChanged(typeName, value);
        }

        private void cboJob_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string typeName;
            string value = cboJob.GetValue();

            if (btnParentType.Text == "監護人")
            {
                typeName = "Job";
                _guardianJob = value;
            }
            else if (btnParentType.Text == "父親")
            {
                typeName = "FatherJob";
                _fatherJob = value;
            }
            else
            {
                typeName = "MotherJob";
                _motherJob = value;
            }
            OnValueChanged(typeName, value);
        }

        private void txtIDNumber_TextChanged(object sender, EventArgs e)
        {
            string typeName;
            string value = txtIDNumber.Text;

            if (btnParentType.Text == "監護人")
            {
                typeName = "CustodianIDNumber";
                _guardianIDNumber = value;
            }
            else if (btnParentType.Text == "父親")
            {
                typeName = "FatherIDNumber";
                _fatherIDNumber = value;
            }
            else
            {
                typeName = "MotherIDNumber";
                _motherIDNumber = value;
            }
            OnValueChanged(typeName, value);
        }
    }
}
