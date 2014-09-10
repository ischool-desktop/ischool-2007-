using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar.Rendering;

namespace SmartSchool.Common
{
    public class Office2007StyleRadioButton:System.Windows.Forms.RadioButton
    {
        public Office2007StyleRadioButton()
        {
            if ( GlobalManager.Renderer is Office2007Renderer )
            {
                this.ForeColor = ( (Office2007Renderer)GlobalManager.Renderer ).ColorTable.CheckBoxItem.Default.Text;
                ( (Office2007Renderer)GlobalManager.Renderer ).ColorTableChanged += delegate
                {
                    this.ForeColor = ( (Office2007Renderer)GlobalManager.Renderer ).ColorTable.CheckBoxItem.Default.Text;
                };
            }
        }
        [System.ComponentModel.Browsable(false)]
        public override System.Drawing.Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }
    }
}
