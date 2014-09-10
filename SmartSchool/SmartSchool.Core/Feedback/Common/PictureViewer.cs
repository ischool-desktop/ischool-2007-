using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;

namespace SmartSchool.Feedback
{
    public partial class PictureViewer : BaseForm
    {
        public PictureViewer(Image img)
        {
            InitializeComponent();
            picFullView.Height = img.Height;
            picFullView.Width = img.Width;
            picFullView.Image = img;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picFullView.Image.Save(saveFileDialog1.FileName);
            }
        }
    }
}