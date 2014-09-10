using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Payment.Properties;

namespace SmartSchool.Payment
{
    public partial class PaymentEntity : Form, IEntity
    {
        public PaymentEntity()
        {
            InitializeComponent();
        }

        #region IEntity 成員

        public void Actived()
        {
        }

        public Panel ContentPanel
        {
            get { return panel1; }
        }

        public DevComponents.DotNetBar.NavigationPanePanel NavPanPanel
        {
            get { return navigationPanePanel1; }
        }

        public Image Picture
        {
            get { return Resources.Payment_Image; }
        }

        public string Title
        {
            get { return "總務作業"; }
        }

        #endregion
    }
}