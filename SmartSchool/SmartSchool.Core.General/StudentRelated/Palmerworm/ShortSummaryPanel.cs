using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using IntelliSchool.DSA30.Util;
using System.Xml;
using System.IO;
using SmartSchool.Feature.Basic;
using System.Drawing.Imaging;
using SmartSchool.Common;
using SmartSchool.Feature;
using DevComponents.DotNetBar;

namespace SmartSchool.StudentRelated.Palmerworm
{
    internal partial class ShortSummaryPanel : PalmerwormItem
    {
        private string _studentID;

        public ShortSummaryPanel()
        {
            InitializeComponent();
            picWaiting.VisibleChanged += new EventHandler(picWaiting_VisibleChanged);
            this.Title = "";

            //設定 Context Menu 的字型。
            ctxMenuBar.Font = FontStyles.General;

        }

        void picWaiting_VisibleChanged(object sender, EventArgs e)
        {
            if (picWaiting.Visible)
                picWaiting.Hide();
        }
        protected override object OnBackgroundWorkerWorking()
        {
            DSResponse dsrsp = QueryStudent.GetDetailList(new string[] {"Name","StudentNumber","ID","Status" } , RunningID);
            XmlNode dsrspclass = QueryStudent.GetClassInfo(RunningID);
            DSXmlHelper helper = dsrsp.GetContent();

            //大絕招
            if (dsrspclass.SelectSingleNode("ClassName") != null)
                helper.AddElement("Student", "ClassName", dsrspclass.SelectSingleNode("ClassName").InnerText);

            return helper;
        }
        protected override void OnBackgroundWorkerCompleted(object result)
        {
            DSXmlHelper helper = result as DSXmlHelper;

            lblClass.Text = helper.GetText("Student/ClassName");
            string name = helper.GetText("Student/Name");
            string snum = helper.GetText("Student/StudentNumber");
            _studentID = helper.GetText("Student/@ID");

            SyncStatus(helper.GetText("Student/Status"));

            lblName.Text = name;
            lblSnum.Text = snum;
        }

        private void SyncStatus(string p)
        {
            Color s;
            string msg = p;
            switch (p)
            {
                case "一般":
                    s = Color.FromArgb(255, 255, 255);
                    break;
                case "畢業或離校":
                    s = Color.FromArgb(156, 220, 128);
                    break;
                case "休學":
                    s = Color.FromArgb(254, 244, 128);
                    break;
                case "延修":
                    s = Color.FromArgb(224, 254, 210);
                    break;
                case "輟學":
                    s = Color.FromArgb(254, 244, 128);
                    break;
                case "刪除":
                    s = Color.FromArgb(254, 128, 155);
                    break;
                default:
                    msg = "未知";
                    s = Color.Transparent;
                    break;
            }

            pxStatus.Text = p;
            pxStatus.Style.BackColor1.Color = s;
            pxStatus.Style.BackColor2.Color = s;
        }

        private void pxStatus_Click(object sender, EventArgs e)
        {
            ctxChangeStatus.Popup(Control.MousePosition);
        }

        private void ChangeStatus_Click(object sender, EventArgs e)
        {
            ButtonItem objStatus = sender as ButtonItem;
            string strStatus = objStatus.Tag.ToString();

            try
            {
                Feature.EditStudent.ChangeStudentStatus(_studentID, strStatus);
                SyncStatus(strStatus);

                //Student.Instance.InvokBriefDataChanged(_studentID);
                SmartSchool.Broadcaster.Events.Items["學生/資料變更"].Invoke(_studentID);
            }
            catch (Exception)
            {
                MsgBox.Show("變更狀態失敗，請檢查服務是否正常。");
            }
        }
    }
}
