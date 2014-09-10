namespace SmartSchool.StudentRelated.Palmerworm
{
    partial class AddressPalmerwormItem
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFullAddress = new System.Windows.Forms.Label();
            this.txtZipcode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtAddress = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnAddressType = new DevComponents.DotNetBar.ButtonX();
            this.btnPAddress = new DevComponents.DotNetBar.ButtonItem();
            this.btnFAddress = new DevComponents.DotNetBar.ButtonItem();
            this.btnOAddress = new DevComponents.DotNetBar.ButtonItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLongtitude = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtLatitude = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnMap = new DevComponents.DotNetBar.ButtonX();
            this.cboTown = new SmartSchool.StudentRelated.Palmerworm.ComboBoxAdv();
            this.cboCounty = new SmartSchool.StudentRelated.Palmerworm.ComboBoxAdv();
            this.btnQueryPoint = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "郵遞區號";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 88);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "村里街號";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(181, 54);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "縣　　市";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(357, 54);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "鄉鎮市區";
            // 
            // lblFullAddress
            // 
            this.lblFullAddress.AutoSize = true;
            this.lblFullAddress.Location = new System.Drawing.Point(194, 13);
            this.lblFullAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFullAddress.Name = "lblFullAddress";
            this.lblFullAddress.Size = new System.Drawing.Size(168, 17);
            this.lblFullAddress.TabIndex = 5;
            this.lblFullAddress.Text = "此處顯示完整地址資訊";
            // 
            // txtZipcode
            // 
            // 
            // 
            // 
            this.txtZipcode.Border.Class = "TextBoxBorder";
            this.txtZipcode.Location = new System.Drawing.Point(94, 51);
            this.txtZipcode.Margin = new System.Windows.Forms.Padding(4);
            this.txtZipcode.Name = "txtZipcode";
            this.txtZipcode.Size = new System.Drawing.Size(52, 22);
            this.txtZipcode.TabIndex = 2;
            this.txtZipcode.TextChanged += new System.EventHandler(this.txtZipcode_TextChanged);
            this.txtZipcode.Validated += new System.EventHandler(this.txtZipcode_Validated);
            this.txtZipcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtZipcode_KeyDown);
            // 
            // txtAddress
            // 
            // 
            // 
            // 
            this.txtAddress.Border.Class = "TextBoxBorder";
            this.txtAddress.Location = new System.Drawing.Point(94, 84);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(382, 22);
            this.txtAddress.TabIndex = 5;
            this.txtAddress.TextChanged += new System.EventHandler(this.txtAddress_TextChanged);
            // 
            // btnAddressType
            // 
            this.btnAddressType.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddressType.AutoExpandOnClick = true;
            this.btnAddressType.Location = new System.Drawing.Point(44, 9);
            this.btnAddressType.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddressType.Name = "btnAddressType";
            this.btnAddressType.Size = new System.Drawing.Size(140, 22);
            this.btnAddressType.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnPAddress,
            this.btnFAddress,
            this.btnOAddress});
            this.btnAddressType.TabIndex = 1;
            this.btnAddressType.Text = "戶籍地址";
            // 
            // btnPAddress
            // 
            this.btnPAddress.GlobalItem = false;
            this.btnPAddress.ImagePaddingHorizontal = 8;
            this.btnPAddress.Name = "btnPAddress";
            this.btnPAddress.Text = "戶籍地址";
            this.btnPAddress.Click += new System.EventHandler(this.btnPAddress_Click);
            // 
            // btnFAddress
            // 
            this.btnFAddress.GlobalItem = false;
            this.btnFAddress.ImagePaddingHorizontal = 8;
            this.btnFAddress.Name = "btnFAddress";
            this.btnFAddress.Text = "聯絡地址";
            this.btnFAddress.Click += new System.EventHandler(this.btnFAddress_Click);
            // 
            // btnOAddress
            // 
            this.btnOAddress.GlobalItem = false;
            this.btnOAddress.ImagePaddingHorizontal = 8;
            this.btnOAddress.Name = "btnOAddress";
            this.btnOAddress.Text = "其它地址";
            this.btnOAddress.Click += new System.EventHandler(this.btnOAddress_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 117);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "經　　度";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(238, 117);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "緯　　度";
            // 
            // txtLongtitude
            // 
            // 
            // 
            // 
            this.txtLongtitude.Border.Class = "TextBoxBorder";
            this.txtLongtitude.Location = new System.Drawing.Point(94, 114);
            this.txtLongtitude.Margin = new System.Windows.Forms.Padding(4);
            this.txtLongtitude.Name = "txtLongtitude";
            this.txtLongtitude.Size = new System.Drawing.Size(120, 22);
            this.txtLongtitude.TabIndex = 6;
            this.txtLongtitude.TextChanged += new System.EventHandler(this.txtLongtitude_TextChanged);
            // 
            // txtLatitude
            // 
            // 
            // 
            // 
            this.txtLatitude.Border.Class = "TextBoxBorder";
            this.txtLatitude.Location = new System.Drawing.Point(302, 114);
            this.txtLatitude.Margin = new System.Windows.Forms.Padding(4);
            this.txtLatitude.Name = "txtLatitude";
            this.txtLatitude.Size = new System.Drawing.Size(120, 22);
            this.txtLatitude.TabIndex = 7;
            this.txtLatitude.TextChanged += new System.EventHandler(this.txtLatitude_TextChanged);
            // 
            // btnMap
            // 
            this.btnMap.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMap.Location = new System.Drawing.Point(439, 114);
            this.btnMap.Margin = new System.Windows.Forms.Padding(4);
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(70, 22);
            this.btnMap.TabIndex = 8;
            this.btnMap.Text = "觀看地圖";
            this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
            // 
            // cboTown
            // 
            this.cboTown.DisplayMember = "Text";
            this.cboTown.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTown.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboTown.FormattingEnabled = true;
            this.cboTown.ItemHeight = 16;
            this.cboTown.Location = new System.Drawing.Point(420, 51);
            this.cboTown.Margin = new System.Windows.Forms.Padding(4);
            this.cboTown.Name = "cboTown";
            this.cboTown.Size = new System.Drawing.Size(89, 22);
            this.cboTown.TabIndex = 4;
            this.cboTown.TextChanged += new System.EventHandler(this.cboTown_TextChanged);
            // 
            // cboCounty
            // 
            this.cboCounty.DisplayMember = "Text";
            this.cboCounty.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCounty.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboCounty.FormattingEnabled = true;
            this.cboCounty.ItemHeight = 16;
            this.cboCounty.Location = new System.Drawing.Point(243, 51);
            this.cboCounty.Margin = new System.Windows.Forms.Padding(4);
            this.cboCounty.Name = "cboCounty";
            this.cboCounty.Size = new System.Drawing.Size(103, 22);
            this.cboCounty.TabIndex = 3;
            this.cboCounty.TextChanged += new System.EventHandler(this.cboCounty_TextChanged);
            // 
            // btnQueryPoint
            // 
            this.btnQueryPoint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQueryPoint.Image = global::SmartSchool.Properties.Resources.DefaultSearchImage;
            this.btnQueryPoint.Location = new System.Drawing.Point(483, 83);
            this.btnQueryPoint.Name = "btnQueryPoint";
            this.btnQueryPoint.Size = new System.Drawing.Size(26, 23);
            this.btnQueryPoint.TabIndex = 13;
            this.btnQueryPoint.Tooltip = "查詢此地址座標";
            this.btnQueryPoint.Click += new System.EventHandler(this.btnQueryPoint_Click);
            // 
            // AddressPalmerwormItem
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.btnAddressType);
            this.Controls.Add(this.txtLongtitude);
            this.Controls.Add(this.btnQueryPoint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.cboCounty);
            this.Controls.Add(this.txtLatitude);
            this.Controls.Add(this.btnMap);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboTown);
            this.Controls.Add(this.txtZipcode);
            this.Controls.Add(this.lblFullAddress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "AddressPalmerwormItem";
            this.Size = new System.Drawing.Size(550, 144);
            this.DoubleClick += new System.EventHandler(this.AddressPalmerwormItem_DoubleClick);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.lblFullAddress, 0);
            this.Controls.SetChildIndex(this.txtZipcode, 0);
            this.Controls.SetChildIndex(this.cboTown, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.btnMap, 0);
            this.Controls.SetChildIndex(this.txtLatitude, 0);
            this.Controls.SetChildIndex(this.cboCounty, 0);
            this.Controls.SetChildIndex(this.txtAddress, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnQueryPoint, 0);
            this.Controls.SetChildIndex(this.txtLongtitude, 0);
            this.Controls.SetChildIndex(this.btnAddressType, 0);
            this.Controls.SetChildIndex(this.picWaiting, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblFullAddress;
        private DevComponents.DotNetBar.Controls.TextBoxX txtZipcode;
        private ComboBoxAdv cboCounty;
        private ComboBoxAdv cboTown;
        private DevComponents.DotNetBar.Controls.TextBoxX txtAddress;
        private DevComponents.DotNetBar.ButtonX btnAddressType;
        private DevComponents.DotNetBar.ButtonItem btnPAddress;
        private DevComponents.DotNetBar.ButtonItem btnFAddress;
        private DevComponents.DotNetBar.ButtonItem btnOAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLongtitude;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLatitude;
        private DevComponents.DotNetBar.ButtonX btnMap;
        private DevComponents.DotNetBar.ButtonX btnQueryPoint;
    }
}
