using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SmartSchool.Common
{
    public static class MsgBox
    {
        public static DialogResult Show(string text)
        {
            return MessageBox.Show(text, "", MessageBoxButtons.OK);
        }

        public static DialogResult Show(string text, string caption)
        {
            return MessageBox.Show(text, caption, MessageBoxButtons.OK);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            return MessageBox.Show(text, caption, buttons);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(text, caption, buttons, icon);
        }
    }
}
