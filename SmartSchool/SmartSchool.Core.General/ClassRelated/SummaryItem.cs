using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;
using System.Xml;
using IntelliSchool.DSA30.Util;
using System.Windows.Forms;
using System.Drawing;
using SmartSchool.Feature.Class;


namespace SmartSchool.ClassRelated
{
    internal class SummaryItem:ClassPalmerwormItem
    {
        private DevComponents.DotNetBar.PanelEx pxStatus;
        private DevComponents.DotNetBar.ButtonItem ctxChangeStatus;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem5;
        private DevComponents.DotNetBar.ButtonItem buttonItem6;
        private DevComponents.DotNetBar.ButtonItem buttonItem7;
        private DevComponents.DotNetBar.ButtonItem buttonItem8;
        private DevComponents.DotNetBar.ButtonItem buttonItem9;
        private DevComponents.DotNetBar.ButtonItem buttonItem10;
        private DevComponents.DotNetBar.ButtonItem buttonItem11;
        private DevComponents.DotNetBar.ButtonItem buttonItem12;
        private DevComponents.DotNetBar.ButtonItem buttonItem13;
        private DevComponents.DotNetBar.ButtonItem buttonItem14;
        private DevComponents.DotNetBar.ButtonItem buttonItem15;
        private DevComponents.DotNetBar.ButtonItem buttonItem16;
        private DevComponents.DotNetBar.ButtonItem buttonItem17;
        internal System.Windows.Forms.Label lblClass;    
        private void InitializeComponent()
        {
            this.lblClass = new System.Windows.Forms.Label();
            this.pxStatus = new DevComponents.DotNetBar.PanelEx();
            this.ctxChangeStatus = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem7 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem8 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem9 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem10 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem11 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem12 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem13 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem14 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem15 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem16 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem17 = new DevComponents.DotNetBar.ButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).BeginInit();
            this.SuspendLayout();
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.BackColor = System.Drawing.Color.Transparent;
            this.lblClass.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblClass.Location = new System.Drawing.Point(0, 0);
            this.lblClass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(56, 17);
            this.lblClass.TabIndex = 182;
            this.lblClass.Text = "園三智";
            // 
            // pxStatus
            // 
            this.pxStatus.CanvasColor = System.Drawing.SystemColors.Control;
            this.pxStatus.ColorScheme.ItemDesignTimeBorder = System.Drawing.Color.Black;
            this.pxStatus.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.pxStatus.Location = new System.Drawing.Point(278, 4);
            this.pxStatus.Margin = new System.Windows.Forms.Padding(4);
            this.pxStatus.Name = "pxStatus";
            this.pxStatus.Size = new System.Drawing.Size(96, 21);
            this.pxStatus.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pxStatus.Style.BackColor1.Color = System.Drawing.Color.LightBlue;
            this.pxStatus.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.pxStatus.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.pxStatus.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.pxStatus.Style.BorderWidth = 0;
            this.pxStatus.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.pxStatus.Style.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.pxStatus.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.pxStatus.Style.GradientAngle = 90;
            this.pxStatus.Style.TextTrimming = System.Drawing.StringTrimming.Word;
            this.pxStatus.TabIndex = 184;
            this.pxStatus.Text = "一般";
            this.pxStatus.Visible = false;
            // 
            // ctxChangeStatus
            // 
            this.ctxChangeStatus.AutoExpandOnClick = true;
            this.ctxChangeStatus.ImagePaddingHorizontal = 8;
            this.ctxChangeStatus.Name = "ctxChangeStatus";
            this.ctxChangeStatus.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem2,
            this.buttonItem3,
            this.buttonItem4,
            this.buttonItem1,
            this.buttonItem5});
            this.ctxChangeStatus.Text = "ChangeStatus";
            // 
            // buttonItem2
            // 
            this.buttonItem2.ImagePaddingHorizontal = 8;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Tag = "一般";
            this.buttonItem2.Text = "一般";
            // 
            // buttonItem3
            // 
            this.buttonItem3.ImagePaddingHorizontal = 8;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Tag = "畢業或離校";
            this.buttonItem3.Text = "畢業或離校";
            // 
            // buttonItem4
            // 
            this.buttonItem4.ImagePaddingHorizontal = 8;
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.Tag = "休學";
            this.buttonItem4.Text = "休學";
            // 
            // buttonItem1
            // 
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Tag = "延修";
            this.buttonItem1.Text = "延修";
            // 
            // buttonItem5
            // 
            this.buttonItem5.ImagePaddingHorizontal = 8;
            this.buttonItem5.Name = "buttonItem5";
            this.buttonItem5.Tag = "輟學";
            this.buttonItem5.Text = "輟學";
            this.buttonItem5.Visible = false;
            // 
            // buttonItem6
            // 
            this.buttonItem6.AutoExpandOnClick = true;
            this.buttonItem6.ImagePaddingHorizontal = 8;
            this.buttonItem6.Name = "buttonItem6";
            this.buttonItem6.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem7,
            this.buttonItem8,
            this.buttonItem9,
            this.buttonItem10,
            this.buttonItem11});
            this.buttonItem6.Text = "ChangeStatus";
            // 
            // buttonItem7
            // 
            this.buttonItem7.ImagePaddingHorizontal = 8;
            this.buttonItem7.Name = "buttonItem7";
            this.buttonItem7.Tag = "一般";
            this.buttonItem7.Text = "一般";
            // 
            // buttonItem8
            // 
            this.buttonItem8.ImagePaddingHorizontal = 8;
            this.buttonItem8.Name = "buttonItem8";
            this.buttonItem8.Tag = "畢業或離校";
            this.buttonItem8.Text = "畢業或離校";
            // 
            // buttonItem9
            // 
            this.buttonItem9.ImagePaddingHorizontal = 8;
            this.buttonItem9.Name = "buttonItem9";
            this.buttonItem9.Tag = "休學";
            this.buttonItem9.Text = "休學";
            // 
            // buttonItem10
            // 
            this.buttonItem10.ImagePaddingHorizontal = 8;
            this.buttonItem10.Name = "buttonItem10";
            this.buttonItem10.Tag = "延修";
            this.buttonItem10.Text = "延修";
            // 
            // buttonItem11
            // 
            this.buttonItem11.ImagePaddingHorizontal = 8;
            this.buttonItem11.Name = "buttonItem11";
            this.buttonItem11.Tag = "輟學";
            this.buttonItem11.Text = "輟學";
            this.buttonItem11.Visible = false;
            // 
            // buttonItem12
            // 
            this.buttonItem12.AutoExpandOnClick = true;
            this.buttonItem12.ImagePaddingHorizontal = 8;
            this.buttonItem12.Name = "buttonItem12";
            this.buttonItem12.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem13,
            this.buttonItem14,
            this.buttonItem15,
            this.buttonItem16,
            this.buttonItem17});
            this.buttonItem12.Text = "ChangeStatus";
            // 
            // buttonItem13
            // 
            this.buttonItem13.ImagePaddingHorizontal = 8;
            this.buttonItem13.Name = "buttonItem13";
            this.buttonItem13.Tag = "一般";
            this.buttonItem13.Text = "一般";
            // 
            // buttonItem14
            // 
            this.buttonItem14.ImagePaddingHorizontal = 8;
            this.buttonItem14.Name = "buttonItem14";
            this.buttonItem14.Tag = "畢業或離校";
            this.buttonItem14.Text = "畢業或離校";
            // 
            // buttonItem15
            // 
            this.buttonItem15.ImagePaddingHorizontal = 8;
            this.buttonItem15.Name = "buttonItem15";
            this.buttonItem15.Tag = "休學";
            this.buttonItem15.Text = "休學";
            // 
            // buttonItem16
            // 
            this.buttonItem16.ImagePaddingHorizontal = 8;
            this.buttonItem16.Name = "buttonItem16";
            this.buttonItem16.Tag = "延修";
            this.buttonItem16.Text = "延修";
            // 
            // buttonItem17
            // 
            this.buttonItem17.ImagePaddingHorizontal = 8;
            this.buttonItem17.Name = "buttonItem17";
            this.buttonItem17.Tag = "輟學";
            this.buttonItem17.Text = "輟學";
            this.buttonItem17.Visible = false;
            // 
            // SummaryItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pxStatus);
            this.Controls.Add(this.lblClass);
            this.Name = "SummaryItem";
            this.Size = new System.Drawing.Size(391, 32);
            this.Controls.SetChildIndex(this.picWaiting, 0);
            this.Controls.SetChildIndex(this.lblClass, 0);
            this.Controls.SetChildIndex(this.pxStatus, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //private string _classID;

        public SummaryItem()
        {
            InitializeComponent();
            picWaiting.VisibleChanged += new EventHandler(picWaiting_VisibleChanged);
            this.Title = "";

            //設定 Context Menu 的字型。
            //ctxMenuBar.Font = FontStyles.General;

        }

        void picWaiting_VisibleChanged(object sender, EventArgs e)
        {
            if (picWaiting.Visible)
                picWaiting.Hide();
        }
        protected override object OnBackgroundWorkerWorking()
        {
            //DSResponse dsrsp = QueryClass.GetClass(RunningID);            
            //DSXmlHelper helper = dsrsp.GetContent();
            //return helper;
            //大絕招
            //if (dsrspclass.SelectSingleNode("ClassName") != null)
            //    helper.AddElement("Student", "ClassName", dsrspclass.SelectSingleNode("ClassName").InnerText);

            //return helper;
            return null;
        }
        protected override void OnBackgroundWorkerCompleted(object result)
        {
            //DSXmlHelper helper = result as DSXmlHelper;

            lblClass.Text = Class.Instance.Items[RunningID].ClassName;//helper.GetText("Class/ClassName");
            //string name = helper.GetText("Student/Name");
            //string snum = helper.GetText("Student/StudentNumber");
            //_classID = helper.GetText("Class/@ID");

            //SyncStatus(helper.GetText("Student/Status"));            
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

        public override bool IsValid()
        {
            return true;
        }
    }
}
