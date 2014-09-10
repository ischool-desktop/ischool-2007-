using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar.Rendering;

namespace SmartSchool.Common
{

    public class DataGridViewEx : DevComponents.DotNetBar.Controls.DataGridViewX
    {
        public DataGridViewEx()
        {
            this.HighlightSelectedColumnHeaders = false;
        }
    }
}
