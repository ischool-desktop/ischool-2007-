using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.TeacherRelated.TeacherIUD;
using SmartSchool.Feature.Teacher;
using SmartSchool.Common;
using SmartSchool.Security;

namespace SmartSchool.TeacherRelated.RibbonBars
{
    public partial class Manage : RibbonBarBase, IPalmerwormManager
    {
        //�v���P�_
        FeatureAccessControl addCtrl;
        FeatureAccessControl saveCtrl;
        FeatureAccessControl delCtrl;

        static private Manage _Instance;
        static public Manage Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new Manage();
                return _Instance;
            }
        }

        private Manage()
        {
            InitializeComponent();

            Teacher.Instance.SelectionChanged += new EventHandler(Instance_SelectionChanged);


            //�v���P�_
            addCtrl = new FeatureAccessControl("Button0450");
            saveCtrl = new FeatureAccessControl("Button0460");
            delCtrl = new FeatureAccessControl("Button0470");

            addCtrl.Inspect(btnAddTeacher);
            saveCtrl.Inspect(btnSaveTeacher);
            delCtrl.Inspect(btnDeleteTeacher);
        }

        void Instance_SelectionChanged(object sender, EventArgs e)
        {
            btnDeleteTeacher.Enabled = (Teacher.Instance.SelectionTeachers.Count == 1);

            delCtrl.Inspect(btnDeleteTeacher);
        }

        private void buttonItem83_Click(object sender, EventArgs e)
        {
            InsertTeacherWizard wizard = new InsertTeacherWizard();
            if (wizard.ShowDialog() == DialogResult.Yes)
            {
                PopupPalmerwormTeacher.ShowPopupPalmerwormTeacher(wizard.NewTeacherID);
            }
        }

        private void buttonItem85_Click(object sender, EventArgs e)
        {
            if (MsgBox.Show("�O�_�N \"" + Teacher.Instance.SelectionTeachers[0].TeacherName + "\" ���ܤw�R���H", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                RemoveTeacher.DeleteTeacher(Teacher.Instance.SelectionTeachers[0].ID);

                //Log
                CurrentUser.Instance.AppLog.Write(
                    SmartSchool.ApplicationLog.EntityType.Teacher,
                    "�R���Юv",
                    Teacher.Instance.SelectionTeachers[0].ID,
                    "�Юv�u" + Teacher.Instance.SelectionTeachers[0].TeacherName + "�v�w�ܧ󬰧R���C",
                    "�Юv",
                    string.Format("�Юv�m�W�G{0}", Teacher.Instance.SelectionTeachers[0].TeacherName));

                Teacher.Instance.InvokTeacherDataChanged(Teacher.Instance.SelectionTeachers[0].ID);
            }
        }

        private void buttonItem84_Click(object sender, EventArgs e)
        {
            if (Save != null)
                Save.Invoke(this, new EventArgs());
        }

        #region IPalmerwormManager ����

        public bool EnableSave
        {
            get
            {
                return btnSaveTeacher.Enabled;
            }
            set
            {
                btnSaveTeacher.Enabled = value;

                saveCtrl.Inspect(btnSaveTeacher);
            }
        }

        public bool EnableCancel
        {
            get
            {
                return false;
            }
            set
            {

            }
        }

        public event EventHandler Save;

        public event EventHandler Cacel;

        public event EventHandler Reflash;

        #endregion
    }
}

