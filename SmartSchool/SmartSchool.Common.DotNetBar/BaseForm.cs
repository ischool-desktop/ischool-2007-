using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SmartSchool.Common
{
    /// <summary>
    /// 美美的Form
    /// </summary>
    public partial class BaseForm : Office2007Form
    {
        private static Px p = new Px();
        /// <summary>
        /// 建構子
        /// </summary>
        public BaseForm()
        {
            Font = FontStyles.General;
            AutoScaleMode = AutoScaleMode.None;

            InitializeComponent();
            this.BackColor = this.GetColorScheme().PanelBackground;
            this.ResizeRedraw = true;
        }
        protected override void OnControlAdded(ControlEventArgs e)
        {
            try
            {
                e.Control.BackColor = System.Drawing.Color.Transparent;
            }
            catch { }
            base.OnControlAdded(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            p.Size = this.Size;
            p.Paint(e);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            p.Size = this.Size;
            p.PaintBackground(e);
        }
        //protected override void OnInvalidated(InvalidateEventArgs e)
        //{
        //    base.OnInvalidated(e);
        //    p.Invalidated(e);
        //}
        //protected override void OnSizeChanged(EventArgs e)
        //{
        //    base.OnSizeChanged(e);
        //    p.Size = this.RestoreBounds.Size;
        //}


        private class Px : PanelEx
        {
            public Px()
            {
                this.CanvasColor = System.Drawing.SystemColors.Control;
                this.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
                this.Dock = System.Windows.Forms.DockStyle.Fill;
                this.Location = new System.Drawing.Point(0, 0);
                this.Name = "panelEx1";
                this.Size = new System.Drawing.Size(404, 298);
                this.Style.Alignment = System.Drawing.StringAlignment.Center;
                this.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
                this.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
                this.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
                this.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
                this.Style.GradientAngle = 90;
                this.TabIndex = 0;
            }

            public void PaintBackground(PaintEventArgs e)
            {
                this.OnPaintBackground(e);
            }

            public new void Paint(PaintEventArgs e)
            {
                this.OnPaint(e);
            }

            public new  void Invalidated(InvalidateEventArgs e)
            {
                this.OnInvalidated(e);
            }
        }
    }
}