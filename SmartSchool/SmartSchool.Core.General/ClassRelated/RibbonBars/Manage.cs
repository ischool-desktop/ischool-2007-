using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.Feature.Class;
using SmartSchool.Security;
using SmartSchool.ApplicationLog;

namespace SmartSchool.ClassRelated.RibbonBars
{
    public partial class Manage : RibbonBarBase, IPalmerwormManager
    {
        //權限判斷
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

            //Class.Instance.SelectionChanged += new EventHandler(Instance_SelectionChanged);
            SmartSchool.Broadcaster.Events.Items["班級/選取變更"].Handler += delegate
            {
                btnDeleteClass.Enabled = (Class.Instance.SelectionClasses.Count == 1);

                delCtrl.Inspect(btnDeleteClass);
            };
            //權限判斷
            addCtrl = new FeatureAccessControl("Button0330");
            saveCtrl = new FeatureAccessControl("Button0340");
            delCtrl = new FeatureAccessControl("Button0350");

            addCtrl.Inspect(btnAddClass);
            saveCtrl.Inspect(btnSaveClass);
            delCtrl.Inspect(btnDeleteClass);
        }

        //void Instance_SelectionChanged(object sender, EventArgs e)
        //{
        //    btnDeleteClass.Enabled = (Class.Instance.SelectionClasses.Count == 1);

        //    delCtrl.Inspect(btnDeleteClass);
        //}

        private void buttonItem14_Click(object sender, EventArgs e)
        {
            InsertClassWizard wizard = new InsertClassWizard();
            if (wizard.ShowDialog() == DialogResult.Yes)
                Class.Instance.PopupClassForm(wizard.NewClassID, "輸入班級其餘資訊");
        }



        #region IPalmerwormManager 成員

        public bool EnableSave
        {
            get
            {
                return btnSaveClass.Enabled;
            }
            set
            {
                btnSaveClass.Enabled = value;

                saveCtrl.Inspect(btnSaveClass);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MsgBox.Show("注意！此動作將會使該班學生列入未分班學生之中，您確定將刪除此班級？", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    ClassInfo info = Class.Instance.SelectionClasses[0];

                    DeleteClassEventArgs args = new DeleteClassEventArgs();
                    //args.DeleteClassIDArray.Add(Class.Instance.SelectionClasses[0].ClassID);
                    args.DeleteClassIDArray.Add(info.ClassID);

                    //RemoveClass.DeleteClass(Class.Instance.SelectionClasses[0].ClassID);
                    RemoveClass.DeleteClass(info.ClassID);
                    Class.Instance.InvokClassDeleted(args);

                    //寫入 Log
                    CurrentUser.Instance.AppLog.Write(EntityType.Class, "刪除班級", info.ClassID, string.Format("班級「{0}」已刪除", info.ClassName), "班級", info.ClassID);
                }
                catch (Exception ex)
                {
                    CurrentUser.ReportError(ex);
                    MessageBox.Show("刪除班級失敗，錯誤訊息：" + ex.Message);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save != null)
                Save.Invoke(this, new EventArgs());
        }
    }
}
