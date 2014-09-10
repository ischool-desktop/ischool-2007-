using System.Windows.Forms;
namespace SmartSchool
{
    partial class PalmerwormItemContainer
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
            //this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.expandablePanel1 = this;
            this.savebutton = new LinkLabel();
            this.undobutton = new LinkLabel();
            this.SuspendLayout();
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.ButtonImageCollapse = global::SmartSchool.Properties.Resources.expandableButtonImageCollapse;
            this.expandablePanel1.ButtonImageExpand = global::SmartSchool.Properties.Resources.expandableButtonImageExpand;
            this.expandablePanel1.AutoSize = true;
            this.expandablePanel1.ExpandOnTitleClick = true;
            this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel1.ColorScheme.DockSiteBackColorGradientAngle = 0;
            this.expandablePanel1.ColorScheme.ItemDesignTimeBorder = System.Drawing.Color.Black;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.expandablePanel1.Location = new System.Drawing.Point(0, 0);
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.Size = new System.Drawing.Size(600, 54);
            //this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            //this.expandablePanel1.Style.BackColor1.Color = System.Drawing.Color.White;
            //this.expandablePanel1.Style.BackColor2.Color = System.Drawing.Color.White;
            //this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.None;
            //this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            //this.expandablePanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            //this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            //this.expandablePanel1.Style.GradientAngle = 90;
            //this.expandablePanel1.Style.CornerDiameter = 3;
            this.expandablePanel1.TabIndex = 0;
            //this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            //this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            //this.expandablePanel1.TitleStyle.BackColor2.Color = System.Drawing.Color.White;//.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            //this.expandablePanel1.TitleStyle.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Tile;
            //this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.None;
            //this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            //this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText; 
            //this.expandablePanel1.TitleStyle.GradientAngle = 90;
            //this.expandablePanel1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            //this.expandablePanel1.TitleStyle.CornerDiameter=3;
            this.expandablePanel1.TitleText = "Title Bar";
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.BackColor1.Color = System.Drawing.Color.White;
            this.expandablePanel1.Style.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Tile;
            this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel1.Style.BorderColor.Color = System.Drawing.Color.White;
            this.expandablePanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.Style.WordWrap = true;
            this.expandablePanel1.TabIndex = 0;
            this.expandablePanel1.TitleHeight = 30;
            this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.TitleStyle.BackColor1.Color = System.Drawing.Color.Transparent;
            this.expandablePanel1.TitleStyle.BackColor2.Color = System.Drawing.Color.Transparent;
            this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel1.TitleStyle.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.expandablePanel1.TitleStyle.ForeColor.Color = System.Drawing.Color.DimGray;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            this.expandablePanel1.TitleText = "Title Bar";

            this.savebutton.AutoSize = true;
            this.savebutton.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(133)))), ((int)(((byte)(2)))));
            this.savebutton.Location = new System.Drawing.Point(2, 6);
            this.savebutton.Name = "linkLabel1";
            this.savebutton.Size = new System.Drawing.Size(31, 13);
            this.savebutton.TabIndex = 19;
            this.savebutton.TabStop = true;
            this.savebutton.Text = "";//"儲存";
            this.savebutton.Visible = false;
            this.savebutton.Click += new System.EventHandler(savebutton_Click);


            this.undobutton.AutoSize = true;
            this.undobutton.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(133)))), ((int)(((byte)(2)))));
            this.undobutton.Location = new System.Drawing.Point(35, 6);
            this.undobutton.Name = "linkLabel2";
            this.undobutton.Size = new System.Drawing.Size(31, 13);
            this.undobutton.TabIndex = 20;
            this.undobutton.TabStop = true;
            this.undobutton.Text = "";//"儲存";
            this.undobutton.Visible = false;
            this.undobutton.Click += new System.EventHandler(undobutton_Click);

            this.Controls.Add(savebutton);
            this.Controls.Add(undobutton);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private LinkLabel savebutton;
        private LinkLabel undobutton;
    }
}
