using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.ClassRelated;
using SmartSchool.StudentRelated;
using IntelliSchool.DSA30.Util;
using DevComponents.DotNetBar;

namespace SmartSchool.ClassRelated.RibbonBars
{
    public partial class AssignConfirm : BaseForm
    {
        private ClassInfo _classInfo;
        private List<BriefStudentData> _students;
        private bool _keep;
        public AssignConfirm(ClassInfo classInfo, List<BriefStudentData> students)
        {
            _classInfo = classInfo;
            _students = students;
            InitializeComponent();
        }




        private void UpdateStudent()
        {
            List<string> studentidList = new List<string>();
            DSXmlHelper helper = new DSXmlHelper("UpdateRequest");
            helper.AddElement("Student");
            helper.AddElement("Student", "Field");
            helper.AddElement("Student/Field", "RefClassID", _classInfo.ClassID);
            if (!_keep)
                helper.AddElement("Student/Field", "SeatNo", "");
            helper.AddElement("Student", "Condition");
            foreach (BriefStudentData student in _students)
            {
                string id = student.ID;
                if (id == _classInfo.ClassID)
                    continue;
                studentidList.Add(id);
                helper.AddElement("Student/Condition", "ID", id);
            }
            try
            {
                SmartSchool.Feature.EditStudent.Update(new DSRequest(helper));
            }
            catch (Exception ex)
            {
                MsgBox.Show("學生班級分配失敗 : " + ex.Message);
                return;
            }
            MsgBox.Show("學生班級分配完成");
            //SmartSchool.StudentRelated.Student.Instance.InvokBriefDataChanged(studentidList.ToArray());
            SmartSchool.Broadcaster.Events.Items["學生/資料變更"].Invoke(studentidList.ToArray());
            SmartSchool.ClassRelated.Class.Instance.InvokClassUpdated(_classInfo.ClassID);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKeep_Click(object sender, EventArgs e)
        {
            _keep = true;
            UpdateStudent();
            this.Close();
        }

        private void btnDrop_Click(object sender, EventArgs e)
        {
            _keep = false;
            UpdateStudent();
            this.Close();
        }
    }
}