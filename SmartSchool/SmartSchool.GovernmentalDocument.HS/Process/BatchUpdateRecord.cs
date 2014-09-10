using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Customization.Data;
using IntelliSchool.DSA30.Util;
using DevComponents.DotNetBar;
using System.Xml;
using SmartSchool.Customization.Data.StudentExtension;
using SmartSchool.Common;

namespace SmartSchool.GovernmentalDocument.Process
{
    public partial class BatchUpdateRecord : UserControl
    {
        FeatureAccessControl batchGraduateCtrl;
        public BatchUpdateRecord()
        {
            InitializeComponent();

            SmartSchool.Customization.PlugIn.GeneralizationPluhgInManager<ButtonItem>.Instance["學生/教務作業"].Add(btnStudentUR);
            //SmartSchool.StudentRelated.Student.Instance.SelectionChanged += new EventHandler(Instance_SelectionChanged);
            SmartSchool.Broadcaster.Events.Items["學生/選取變更"].Handler += delegate
            {
                int selectedCount =new AccessHelper().StudentHelper.GetSelectedStudent().Count ;
                btnBatchGraduate.Enabled = selectedCount > 0;
                btnChangeDept.Enabled = btnChangeInfo.Enabled = (selectedCount == 1);
                batchGraduateCtrl.Inspect(btnBatchGraduate);
            };

            //權限判斷 - 學生/批次畢業異動
            batchGraduateCtrl = new FeatureAccessControl("Button0095");
            batchGraduateCtrl.Inspect(btnBatchGraduate);
        }

        //void Instance_SelectionChanged(object sender, EventArgs e)
        //{
        //    btnBatchGraduate.Enabled = SmartSchool.StudentRelated.Student.Instance.SelectionStudents.Count > 0;
        //    batchGraduateCtrl.Inspect(btnBatchGraduate);
        //}

        #region 批次畢業異動
        private void btnBatchGraduate_Click(object sender, EventArgs e)
        {
            MotherForm.Instance.SetBarMessage("檢查學生中，請稍候");

            const string 畢業代碼 = "501";

            AccessHelper accessHelper = new AccessHelper();
            List<StudentRecord> students = accessHelper.StudentHelper.GetSelectedStudent();
            Dictionary<string, string> studentDiplomaNumber = new Dictionary<string, string>();
            Dictionary<string, string> hasUpdateRecordWithoutAD = new Dictionary<string, string>();
            List<string> containADStudents = new List<string>();
            Dictionary<StudentRecord, string> errorList = new Dictionary<StudentRecord, string>();

            accessHelper.StudentHelper.FillField("DiplomaNumber", students);
            accessHelper.StudentHelper.FillUpdateRecord(students);

            SmartSchool.Customization.Data.SystemInformation.getField("SchoolConfig");
            XmlElement config = SmartSchool.Customization.Data.SystemInformation.Fields["SchoolConfig"] as XmlElement;

            int total = students.Count;
            int current = 0;

            #region 檢查學生

            foreach (StudentRecord each_stu in students)
            {
                #region 檢查學生身上的畢業證書字號

                XmlElement dnElement = each_stu.Fields["DiplomaNumber"] as XmlElement;
                string dnString = "";

                if (dnElement != null && dnElement.SelectSingleNode("DiplomaNumber") != null)
                    dnString = dnElement.SelectSingleNode("DiplomaNumber").InnerText;

                if (!studentDiplomaNumber.ContainsKey(each_stu.StudentID))
                    studentDiplomaNumber.Add(each_stu.StudentID, dnString);

                if (string.IsNullOrEmpty(dnString))
                {
                    if (!errorList.ContainsKey(each_stu))
                        errorList.Add(each_stu, "缺少畢業證書字號");
                }

                #endregion

                #region 檢查學生是否有任何異動資料 (不應該一筆都沒有)

                //if (each_stu.UpdateRecordList.Count <= 0)
                //{
                //    if (!errorList.ContainsKey(each_stu))
                //        errorList.Add(each_stu, "沒有任何異動資料");
                //    else
                //        errorList[each_stu] += "; 沒有任何異動資料";
                //}

                #endregion

                #region 檢查學生是否具有 '包含核准文號的畢業異動'

                foreach (UpdateRecordInfo info in each_stu.UpdateRecordList)
                {
                    if (info.UpdateCode == 畢業代碼)
                    {
                        if (!string.IsNullOrEmpty(info.ADNumber))
                        {
                            if (!containADStudents.Contains(each_stu.StudentID))
                                containADStudents.Add(each_stu.StudentID);
                            //if (!errorList.ContainsKey(each_stu))
                            //    errorList.Add(each_stu, "已有包含核准文號的畢業異動");
                            //else
                            //    errorList[each_stu] += "; 已有包含核准文號的畢業異動";
                        }
                        else
                        {
                            if (!hasUpdateRecordWithoutAD.ContainsKey(each_stu.StudentID))
                                hasUpdateRecordWithoutAD.Add(each_stu.StudentID, (info.Detail as XmlElement).GetAttribute("ID"));
                        }
                    }
                }

                #endregion
            }

            MotherForm.Instance.SetBarMessage("檢查學生完成");

            if (errorList.Count > 0)
            {
                ErrorViewer viewer = new ErrorViewer(errorList);
                viewer.ShowDialog();
                return;
            }

            #endregion

            InitialForm form = new InitialForm();
            if (form.ShowDialog() != DialogResult.OK)
                return;

            MotherForm.Instance.SetBarMessage("畢業異動產生中", 0);

            string date = form.Date.ToShortDateString();

            int package_size = 25;
            int count = 0;

            #region 產生 Request，呼叫 Service

            DSXmlHelper urInsertHelper = new DSXmlHelper("InsertRequest"); //新增畢業異動 Helper
            DSXmlHelper urUpdateHelper = new DSXmlHelper("UpdateRequest"); //修改畢業異動 Helper
            DSXmlHelper updateHelper = new DSXmlHelper("UpdateStudentList"); //更新學生資訊 Helper

            foreach (StudentRecord each_stu in students)
            {
                count++;

                //如果學生身上有畢業核准文號，不變更畢業異動。
                if (!containADStudents.Contains(each_stu.StudentID))
                {
                    #region 取得最後異動資訊

                    DateTime lastADDate = DateTime.MinValue;
                    string lastADNumber = "";
                    string lastUpdateCode = "";

                    foreach (UpdateRecordInfo info in each_stu.UpdateRecordList)
                    {
                        if (!string.IsNullOrEmpty(info.ADDate) && info.UpdateCode != 畢業代碼)
                        {
                            DateTime a;
                            if (DateTime.TryParse(info.ADDate, out a) && a > lastADDate)
                            {
                                lastADDate = a;
                                lastADNumber = info.ADNumber;
                                lastUpdateCode = info.UpdateCode;
                            }
                        }
                    }

                    #endregion

                    #region 取得學籍身分代號

                    string comment = "";

                    XmlElement identityMapping = (XmlElement)config.SelectSingleNode("學籍身分對照表");
                    if (identityMapping != null)
                    {
                        List<string> code_list = new List<string>();
                        foreach (CategoryInfo info in each_stu.StudentCategorys)
                        {
                            foreach (XmlElement each_identity in identityMapping.SelectNodes("Identity[Tag/@FullName='" + info.FullName + "']"))
                            {
                                string each_name = each_identity.GetAttribute("Code");
                                if (!code_list.Contains(each_name))
                                    code_list.Add(each_name);
                            }
                        }

                        foreach (string each_code in code_list)
                        {
                            if (!string.IsNullOrEmpty(comment)) comment += ",";
                            comment += each_code;
                        }
                    }

                    #endregion

                    #region 產生異動 Request

                    DSXmlHelper helper = new DSXmlHelper("UpdateRecord");
                    helper.AddElement("Field");

                    helper.AddElement("Field", "RefStudentID", each_stu.StudentID);

                    helper.AddElement("Field", "ContextInfo");
                    helper.AddElement("Field/ContextInfo", "ContextInfo");
                    helper.AddElement("Field/ContextInfo/ContextInfo", "LastUpdateCode", lastUpdateCode);
                    helper.AddElement("Field/ContextInfo/ContextInfo", "NewStudentNumber");
                    helper.AddElement("Field/ContextInfo/ContextInfo", "PreviousSchool");
                    helper.AddElement("Field/ContextInfo/ContextInfo", "PreviousStudentNumber");
                    helper.AddElement("Field/ContextInfo/ContextInfo", "PreviousDepartment");
                    helper.AddElement("Field/ContextInfo/ContextInfo", "PreviousSchoolLastADDate");
                    helper.AddElement("Field/ContextInfo/ContextInfo", "PreviousSchoolLastADNumber");
                    helper.AddElement("Field/ContextInfo/ContextInfo", "PreviousGradeYear");
                    helper.AddElement("Field/ContextInfo/ContextInfo", "GraduateSchoolLocationCode");
                    helper.AddElement("Field/ContextInfo/ContextInfo", "GraduateSchool");
                    helper.AddElement("Field/ContextInfo/ContextInfo", "GraduateCertificateNumber", studentDiplomaNumber[each_stu.StudentID]);

                    helper.AddElement("Field", "Department", each_stu.Department);
                    helper.AddElement("Field", "ADDate", "");
                    helper.AddElement("Field", "ADNumber", "");
                    helper.AddElement("Field", "UpdateDate", date);
                    helper.AddElement("Field", "UpdateCode", 畢業代碼);
                    helper.AddElement("Field", "UpdateDescription", "畢業");
                    helper.AddElement("Field", "Name", each_stu.StudentName);
                    helper.AddElement("Field", "StudentNumber", each_stu.StudentNumber);
                    helper.AddElement("Field", "Gender", each_stu.Gender);
                    helper.AddElement("Field", "IDNumber", each_stu.IDNumber);
                    helper.AddElement("Field", "Birthdate", each_stu.Birthday);
                    helper.AddElement("Field", "GradeYear", (each_stu.RefClass != null) ? each_stu.RefClass.GradeYear : "");
                    helper.AddElement("Field", "LastADDate", (lastADDate == DateTime.MinValue) ? "" : lastADDate.ToShortDateString());
                    helper.AddElement("Field", "LastADNumber", lastADNumber);
                    helper.AddElement("Field", "Comment", comment);

                    if (hasUpdateRecordWithoutAD.ContainsKey(each_stu.StudentID))
                    {
                        helper.AddElement("Condition");
                        helper.AddElement("Condition", "ID", hasUpdateRecordWithoutAD[each_stu.StudentID]);

                        urUpdateHelper.AddElement(".", helper.BaseElement);
                    }
                    else
                        urInsertHelper.AddElement(".", helper.BaseElement);

                    #endregion
                }

                #region 產生學生更新資訊 Request

                DSXmlHelper helper2 = new DSXmlHelper("Student");
                helper2.AddElement("Field");
                helper2.AddElement("Field", "RefClassID");
                helper2.AddElement("Field", "OverrideDeptID");
                helper2.AddElement("Field", "Status", "畢業或離校");

                string format = "<LeaveInfo SchoolYear='{0}' Reason='{1}' ClassName='{2}' Department='{3}'></LeaveInfo>";

                string schoolYear = SmartSchool.Customization.Data.SystemInformation.SchoolYear.ToString();
                string className = (each_stu.RefClass != null) ? each_stu.RefClass.ClassName : ((each_stu.Fields.ContainsKey("LeaveClassName") && (each_stu.Fields["LeaveClassName"] as string) != "") ? each_stu.Fields["LeaveClassName"] as string : "");
                string dept = each_stu.Department + ((each_stu.Fields.ContainsKey("SubDepartment") && (each_stu.Fields["SubDepartment"] as string) != "") ? (":" + each_stu.Fields["SubDepartment"]) : "");

                string leaveInfo = string.Format(format, schoolYear, "畢業", className, dept);

                helper2.AddElement("Field", "LeaveInfo", leaveInfo, true);
                helper2.AddElement("Condition");
                helper2.AddElement("Condition", "ID", each_stu.StudentID);

                updateHelper.AddElement(".", helper2.BaseElement);

                #endregion

                #region 批次寫入
                if (count % package_size == 0 || count >= students.Count)
                {
                    #region 寫入畢業異動

                    if (urInsertHelper.PathExist("UpdateRecord"))
                        SmartSchool.Feature.EditStudent.InsertUpdateRecord(new DSRequest(urInsertHelper));
                    if (urUpdateHelper.PathExist("UpdateRecord"))
                        SmartSchool.Feature.EditStudent.ModifyUpdateRecord(new DSRequest(urUpdateHelper));

                    #endregion
                    #region 寫入離校資訊、脫離班級、變更狀態為"畢業或離校"

                    SmartSchool.Feature.EditStudent.Update(new DSRequest(updateHelper));

                    #endregion

                    urInsertHelper = new DSXmlHelper("InsertRequest");
                    urUpdateHelper = new DSXmlHelper("UpdateRequest");
                    updateHelper = new DSXmlHelper("UpdateStudentList");
                }
                #endregion

                MotherForm.Instance.SetBarMessage("畢業異動產生中", (int)((++current) * 100 / total));
            }

            #endregion

            #region Invoke BriefData Changed

            List<string> idlist = new List<string>();
            count = 0;
            foreach (StudentRecord each_stu in students)
            {
                count++;
                idlist.Add(each_stu.StudentID);
                if (count % package_size == 0 || count >= students.Count)
                {
                    SmartSchool.Broadcaster.Events.Items["學生/資料變更"].Invoke(idlist.ToArray());
                    idlist.Clear();
                }
            }   

            #endregion

            MotherForm.Instance.SetBarMessage("畢業異動產生完成");
            MsgBox.Show("批次產生畢業異動完成。");
        }
        #endregion

        private void btnChangeInfo_Click(object sender, EventArgs e)
        {
            new ProcessWizards.ChangeInfoProcess(new AccessHelper().StudentHelper.GetSelectedStudent()[0].StudentID).ShowDialog();
        }

        private void btnChangeDept_Click(object sender, EventArgs e)
        {
            new ProcessWizards.ChangeDeptProcess(new AccessHelper().StudentHelper.GetSelectedStudent()[0].StudentID).ShowDialog();
        }
    }
}
