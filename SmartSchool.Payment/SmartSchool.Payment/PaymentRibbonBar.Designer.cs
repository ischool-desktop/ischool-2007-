namespace SmartSchool.Payment
{
    partial class PaymentRibbonBar
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該公開 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentRibbonBar));
            this.rbPayment = new DevComponents.DotNetBar.RibbonBar();
            this.btnPaymentManage = new DevComponents.DotNetBar.ButtonItem();
            this.btnCheckPay = new DevComponents.DotNetBar.ButtonItem();
            this.btnBank = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // rbPayment
            // 
            this.rbPayment.AutoOverflowEnabled = true;
            this.rbPayment.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnPaymentManage,
            this.btnCheckPay,
            this.btnBank,
            this.buttonItem1});
            this.rbPayment.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.rbPayment.Location = new System.Drawing.Point(3, 3);
            this.rbPayment.Name = "rbPayment";
            this.rbPayment.Size = new System.Drawing.Size(271, 93);
            this.rbPayment.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.rbPayment.TabIndex = 0;
            this.rbPayment.Text = "收費";
            // 
            // btnPaymentManage
            // 
            this.btnPaymentManage.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnPaymentManage.Image = ((System.Drawing.Image)(resources.GetObject("btnPaymentManage.Image")));
            this.btnPaymentManage.ImageFixedSize = new System.Drawing.Size(40, 40);
            this.btnPaymentManage.ImagePaddingHorizontal = 8;
            this.btnPaymentManage.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnPaymentManage.Name = "btnPaymentManage";
            this.btnPaymentManage.SubItemsExpandWidth = 14;
            this.btnPaymentManage.Text = "收費管理";
            this.btnPaymentManage.Click += new System.EventHandler(this.btnPaymentManage_Click);
            // 
            // btnCheckPay
            // 
            this.btnCheckPay.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnCheckPay.Image = ((System.Drawing.Image)(resources.GetObject("btnCheckPay.Image")));
            this.btnCheckPay.ImageFixedSize = new System.Drawing.Size(40, 40);
            this.btnCheckPay.ImagePaddingHorizontal = 8;
            this.btnCheckPay.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnCheckPay.Name = "btnCheckPay";
            this.btnCheckPay.SubItemsExpandWidth = 14;
            this.btnCheckPay.Text = "對帳";
            this.btnCheckPay.Click += new System.EventHandler(this.btnCheckPay_Click);
            // 
            // btnBank
            // 
            this.btnBank.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnBank.Image = ((System.Drawing.Image)(resources.GetObject("btnBank.Image")));
            this.btnBank.ImageFixedSize = new System.Drawing.Size(40, 40);
            this.btnBank.ImagePaddingHorizontal = 8;
            this.btnBank.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnBank.Name = "btnBank";
            this.btnBank.SubItemsExpandWidth = 14;
            this.btnBank.Text = "銀行設定";
            this.btnBank.Click += new System.EventHandler(this.btnBank_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.TextOnlyAlways;
            this.buttonItem1.ImageFixedSize = new System.Drawing.Size(40, 40);
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.SubItemsExpandWidth = 14;
            this.buttonItem1.Text = "依預估表產生";
            this.buttonItem1.Visible = false;
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // PaymentRibbonBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Controls.Add(this.rbPayment);
            this.Name = "PaymentRibbonBar";
            this.Size = new System.Drawing.Size(292, 167);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonBar rbPayment;
        private DevComponents.DotNetBar.ButtonItem btnPaymentManage;
        private DevComponents.DotNetBar.ButtonItem btnBank;
        private DevComponents.DotNetBar.ButtonItem btnCheckPay;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
    }
}
