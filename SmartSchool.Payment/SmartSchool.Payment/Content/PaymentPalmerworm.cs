using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using IntelliSchool.DSA30.Util;
using System.Xml;
using System.Threading;

namespace SmartSchool.Payment.Content
{
    public partial class PaymentPalmerworm : UserControl, SmartSchool.Customization.PlugIn.ExtendedContent.IContentItem
    {
        private BackgroundWorker _Loader;

        private string _CurrentID = "";
        private string _RunningID = "";

        private bool _SaveButtonVisible = false;
        private bool _CancelButtonVisible = false;

        public PaymentPalmerworm()
        {
            InitializeComponent();

            _Loader = new BackgroundWorker();
            _Loader.DoWork += new DoWorkEventHandler(_Loader_DoWork);
            _Loader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_Loader_RunWorkerCompleted);
            _Loader.WorkerSupportsCancellation = true;
        }

        private void _Loader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_RunningID != _CurrentID)
            {
                LoadContent(_CurrentID);
                return;
            }

            DSXmlHelper helper = e.Result as DSXmlHelper;

            listViewEx1.SuspendLayout();

            foreach (XmlElement detail in helper.GetElements("PaymentDetail"))
            {
                DSXmlHelper detailHelper = new DSXmlHelper(detail);
                ListViewItem item = new ListViewItem();
                item.Text = detailHelper.GetText("PaymentName");
                item.Tag = detailHelper;
                item.SubItems.Add(detailHelper.GetText("Amount"));
                listViewEx1.Items.Add(item);
            }

            listViewEx1.ResumeLayout();

            picWaiting.Visible = false;
        }

        private void _Loader_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = SmartSchool.Feature.Payment.QueryPayment.GetPaymentDetailsByStudents(_RunningID);
        }

        #region IContentItem 成員

        public bool CancelButtonVisible
        {
            get { return _CancelButtonVisible; }
        }

        public event EventHandler CancelButtonVisibleChanged;

        public Control DisplayControl
        {
            get { return this; }
        }

        public void LoadContent(string id)
        {
            _CurrentID = id;
            if (!_Loader.IsBusy)
            {
                _RunningID = _CurrentID;
                picWaiting.Visible = true;
                listViewEx1.Items.Clear();
                _Loader.RunWorkerAsync();
            }
        }

        public void Save()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool SaveButtonVisible
        {
            get { return _SaveButtonVisible; }
        }

        public event EventHandler SaveButtonVisibleChanged;

        public string Title
        {
            get { return "收費資料"; }
        }

        public void Undo()
        {
            //if (!_bgWorker.IsBusy)
            //LoadContent();
        }

        #endregion

        #region ICloneable 成員

        public object Clone()
        {
            return new PaymentPalmerworm();
        }

        #endregion

        #region ButtonClick

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (listViewEx1.SelectedItems.Count <= 0) return;

            ListViewItem item = listViewEx1.SelectedItems[0];
            if (item.Tag == null) return;

            DSXmlHelper detail = item.Tag as DSXmlHelper;
            DetailEditor editor = new DetailEditor(detail.GetText("PaymentName"), detail.GetText("Amount"), detail.GetText("@ID"));
            editor.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listViewEx1.SelectedItems.Count <= 0) return;

            ListViewItem item = listViewEx1.SelectedItems[0];
            if (item.Tag == null) return;

            DSXmlHelper detail = item.Tag as DSXmlHelper;

            int count = int.Parse(detail.GetText("HistoryCount"));

            string extra = "";
            if (count > 0)
                extra = "目前明細中有 " + count + " 筆繳費記錄";

            if (MessageBox.Show("您確定要刪除「" + detail.GetText("PaymentName") + "」的收費明細嗎？" + extra, "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            try
            {
                //先刪除所有繳費記錄
                SmartSchool.Feature.Payment.EditPayment.DeletePaymentHistory(detail.GetText("@ID"));

                //刪除明細
                SmartSchool.Feature.Payment.EditPayment.DeletePaymentDetail(detail.GetText("@ID"));

                LoadContent(_CurrentID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void listViewEx1_DoubleClick(object sender, EventArgs e)
        {
            if (listViewEx1.SelectedItems.Count <= 0) return;

            ListViewItem item = listViewEx1.SelectedItems[0];
            if (item.Tag == null) return;

            DSXmlHelper detail = item.Tag as DSXmlHelper;
            DetailEditor editor = new DetailEditor(detail.GetText("PaymentName"), detail.GetText("Amount"), detail.GetText("@ID"));
            editor.ShowDialog();
        }
    }
}
