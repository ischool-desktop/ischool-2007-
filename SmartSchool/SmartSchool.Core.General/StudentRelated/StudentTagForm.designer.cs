using SmartSchool.TagManage;
namespace SmartSchool.StudentRelated
{
    partial class StudentTagForm
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cboGroup = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.imgTagColor = new System.Windows.Forms.ImageList(this.components);
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.btnDelete = new DevComponents.DotNetBar.ButtonX();
            this.btnModify = new DevComponents.DotNetBar.ButtonX();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
            this.sdgTagList = new SmartSchool.TagManage.StateDataGridView();
            this.colState = new System.Windows.Forms.DataGridViewImageColumn();
            this.colColor = new System.Windows.Forms.DataGridViewImageColumn();
            this.colCategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.sdgTagList)).BeginInit();
            this.SuspendLayout();
            // 
            // cboGroup
            // 
            this.cboGroup.DisplayMember = "Text";
            this.cboGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboGroup.FormattingEnabled = true;
            this.cboGroup.ItemHeight = 19;
            this.cboGroup.Location = new System.Drawing.Point(51, 7);
            this.cboGroup.Name = "cboGroup";
            this.cboGroup.Size = new System.Drawing.Size(143, 25);
            this.cboGroup.TabIndex = 0;
            this.cboGroup.SelectedIndexChanged += new System.EventHandler(this.cboGroup_SelectedIndexChanged);
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(9, 10);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(34, 19);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "群組";
            // 
            // imgTagColor
            // 
            this.imgTagColor.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgTagColor.ImageSize = new System.Drawing.Size(16, 16);
            this.imgTagColor.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(283, 310);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(62, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "確定";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(351, 310);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.Location = new System.Drawing.Point(351, 40);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(62, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "新增";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.Location = new System.Drawing.Point(351, 69);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(62, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "刪除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnModify
            // 
            this.btnModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModify.BackColor = System.Drawing.Color.Transparent;
            this.btnModify.Location = new System.Drawing.Point(351, 98);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(62, 23);
            this.btnModify.TabIndex = 7;
            this.btnModify.Text = "修改";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.Location = new System.Drawing.Point(202, 7);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(68, 23);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "重新整理";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // sdgTagList
            // 
            this.sdgTagList.AllowUserToAddRows = false;
            this.sdgTagList.AllowUserToDeleteRows = false;
            this.sdgTagList.AllowUserToResizeColumns = false;
            this.sdgTagList.AllowUserToResizeRows = false;
            this.sdgTagList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sdgTagList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.sdgTagList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sdgTagList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sdgTagList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sdgTagList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colState,
            this.colColor,
            this.colCategoryName});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.sdgTagList.DefaultCellStyle = dataGridViewCellStyle1;
            this.sdgTagList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.sdgTagList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.sdgTagList.Location = new System.Drawing.Point(11, 37);
            this.sdgTagList.MultiSelect = false;
            this.sdgTagList.Name = "sdgTagList";
            this.sdgTagList.ReadOnly = true;
            this.sdgTagList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sdgTagList.RowHeadersVisible = false;
            this.sdgTagList.RowTemplate.Height = 24;
            this.sdgTagList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sdgTagList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.sdgTagList.Size = new System.Drawing.Size(334, 266);
            this.sdgTagList.StateColumnIndex = 0;
            this.sdgTagList.TabIndex = 9;
            this.sdgTagList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.sdgTagList_ColumnHeaderMouseClick);
            // 
            // colState
            // 
            this.colState.HeaderText = "";
            this.colState.Name = "colState";
            this.colState.ReadOnly = true;
            this.colState.ToolTipText = "選取狀態";
            this.colState.Width = 25;
            // 
            // colColor
            // 
            this.colColor.HeaderText = "";
            this.colColor.Name = "colColor";
            this.colColor.ReadOnly = true;
            this.colColor.ToolTipText = "色彩";
            this.colColor.Width = 25;
            // 
            // colCategoryName
            // 
            this.colCategoryName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCategoryName.HeaderText = "類別名稱";
            this.colCategoryName.Name = "colCategoryName";
            this.colCategoryName.ReadOnly = true;
            this.colCategoryName.ToolTipText = "類別名稱";
            // 
            // StudentTagForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(419, 339);
            this.Controls.Add(this.sdgTagList);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.cboGroup);
            this.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "StudentTagForm";
            this.Text = "管理學生類別";
            this.Load += new System.EventHandler(this.TagManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sdgTagList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cboGroup;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.ButtonX btnDelete;
        private DevComponents.DotNetBar.ButtonX btnModify;
        private System.Windows.Forms.ImageList imgTagColor;
        private DevComponents.DotNetBar.ButtonX btnRefresh;
        private StateDataGridView sdgTagList;
        private System.Windows.Forms.DataGridViewImageColumn colState;
        private System.Windows.Forms.DataGridViewImageColumn colColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoryName;
    }
}