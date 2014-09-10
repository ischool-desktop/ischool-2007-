using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar.Rendering;
using DevComponents.DotNetBar;

namespace SmartSchool.Common
{
    public class Office2007StyleWizard:DevComponents.DotNetBar.Wizard
    {
        public Office2007StyleWizard():base()
        {

            this.BackButtonText = "< 上一步";
            this.NextButtonText = "下一步 >";
            this.CancelButtonText = "取消";
            this.FinishButtonText = "完成";
            // 
            // wizard1
            // 
            this.ButtonStyle = DevComponents.DotNetBar.eWizardStyle.Office2007;
            //this.CancelButtonText = "取消";
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FinishButtonTabIndex = 3;
            //this.FinishButtonText = "匯出";
            this.FooterHeight = 33;
            // 
            // 
            // 
            this.FooterStyle.BackColor = System.Drawing.Color.Transparent;
            this.ForeColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 15 ) ) ) ), ( (int)( ( (byte)( 57 ) ) ) ), ( (int)( ( (byte)( 129 ) ) ) ));
            this.HeaderCaptionFont = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ));
            this.HeaderImageSize = new System.Drawing.Size(48, 48);
            // 
            // 
            // 
            this.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 191 ) ) ) ), ( (int)( ( (byte)( 215 ) ) ) ), ( (int)( ( (byte)( 243 ) ) ) ));
            this.HeaderStyle.BackColor2 = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 219 ) ) ) ), ( (int)( ( (byte)( 241 ) ) ) ), ( (int)( ( (byte)( 254 ) ) ) ));
            this.HeaderStyle.BackColorGradientAngle = 90;
            this.HeaderStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.HeaderStyle.BorderBottomColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 121 ) ) ) ), ( (int)( ( (byte)( 157 ) ) ) ), ( (int)( ( (byte)( 182 ) ) ) ));
            this.HeaderStyle.BorderBottomWidth = 1;
            this.HeaderStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.HeaderStyle.BorderLeftWidth = 1;
            this.HeaderStyle.BorderRightWidth = 1;
            this.HeaderStyle.BorderTopWidth = 1;
            this.HeaderStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.HeaderStyle.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.HelpButtonVisible = false;
            this.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "wizard1";
            this.Size = new System.Drawing.Size(464, 324);
            this.TabIndex = 0;
            #region 設定Wizard會跟著Style跑
            //this.FooterStyle.ApplyStyle(( GlobalManager.Renderer as Office2007Renderer ).ColorTable.GetClass(ElementStyleClassKeys.RibbonFileMenuBottomContainerKey));
            this.HeaderStyle.ApplyStyle(( GlobalManager.Renderer as Office2007Renderer ).ColorTable.GetClass(ElementStyleClassKeys.RibbonFileMenuBottomContainerKey));
            this.FooterStyle.BackColorGradientAngle = -90;
            this.FooterStyle.BackColorGradientType = eGradientType.Linear;
            this.FooterStyle.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.Default.TopBackground.Start;
            this.FooterStyle.BackColor2 = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.Default.TopBackground.End;
            this.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.Default.TopBackground.Start;
            this.BackgroundImage = null;
            for ( int i = 0 ; i < 5 && i < this.Controls[1].Controls.Count ; i++ )
            {
                ( this.Controls[1].Controls[i] as ButtonX ).ColorTable = eButtonColor.OrangeWithBackground;
            }
            ( this.Controls[0].Controls[1] as System.Windows.Forms.Label ).ForeColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.MouseOver.TitleText;
            ( this.Controls[0].Controls[2] as System.Windows.Forms.Label ).ForeColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.Default.TitleText;
            #endregion
        }
    }
}
