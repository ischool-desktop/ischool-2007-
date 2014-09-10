using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using System.IO;
using DevComponents.DotNetBar;
using System.Drawing.Imaging;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature;
using Aspose.Cells;
using System.Diagnostics;
//using SmartSchool.Common.Photo;
using SmartSchool.Customization.Data;
using SmartSchool.StudentRelated;
using DevComponents.DotNetBar.Rendering;

namespace SmartSchool.StudentRelated.RibbonBars.Import
{
    public partial class BatchUploadPhotoForm : BaseForm
    {
        private Dictionary<string, string> _photos;
        private List<ErrorMessage> _errorReport;
        private int _picWidth;
        private int _picHeight;
        private int _finished;
        private Size _expandSize;
        private Size _originSize;

        public BatchUploadPhotoForm()
        {
            InitializeComponent();

            #region ���Style�]
            this.groupPanel1.BackColor = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.Default.TopBackground.Start;
            this.groupPanel2.BackColor = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.Default.TopBackground.Start;
            
            //this.ImportWizard.HeaderStyle.ApplyStyle((GlobalManager.Renderer as Office2007Renderer).ColorTable.GetClass(ElementStyleClassKeys.RibbonFileMenuBottomContainerKey));
            //this.ImportWizard.FooterStyle.BackColorGradientAngle = -90;
            //this.ImportWizard.FooterStyle.BackColorGradientType = eGradientType.Linear;
            //this.ImportWizard.FooterStyle.BackColor = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.Default.TopBackground.Start;
            //this.ImportWizard.FooterStyle.BackColor2 = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.Default.TopBackground.End;
            //this.ImportWizard.BackColor = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.Default.TopBackground.Start;
            //this.ImportWizard.BackgroundImage = null;
            //for (int i = 0; i < 5; i++)
            //{
            //    (this.ImportWizard.Controls[1].Controls[i] as ButtonX).ColorTable = eButtonColor.OrangeWithBackground;
            //}
            //(this.ImportWizard.Controls[0].Controls[1] as System.Windows.Forms.Label).ForeColor = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.MouseOver.TitleText;
            //(this.ImportWizard.Controls[0].Controls[2] as System.Windows.Forms.Label).ForeColor = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.Default.TitleText;
            //this.ImportWizard.FooterStyle.BackgroundImage = null;
            #endregion

            _photos = new Dictionary<string, string>();
            _errorReport = new List<ErrorMessage>();
            _picWidth = pictureBox1.Width;
            _picHeight = pictureBox1.Height;
            _expandSize = new Size(403, 386);
            _originSize = new Size(403, 258);
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            lblFile.Text = folderBrowserDialog1.SelectedPath;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblFile.Text.Trim()))
            {
                MsgBox.Show("�п�ܦs��Ӥ�����Ƨ��C");
                return;
            }

            DirectoryInfo dir = null;
            try
            {
                dir = new DirectoryInfo(lblFile.Text);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
                return;
            }

            if (!dir.Exists)
            {
                MsgBox.Show("��Ƨ����s�b", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Size = _expandSize;

            FileInfo[] files1 = dir.GetFiles("*.jpg", SearchOption.AllDirectories);
            FileInfo[] files2 = dir.GetFiles("*.jpeg", SearchOption.AllDirectories);
            _photos.Clear();
            _errorReport.Clear();

            List<FileInfo> files = new List<FileInfo>(files1);
            files.AddRange(files2);


            _finished = 0;
            progressBarX1.Visible = true;
            progressBarX1.Maximum = files.Count;
            progressBarX1.Value = _finished;
            progressBarX1.Text = _finished + "/" + progressBarX1.Maximum;

            foreach (FileInfo file in files)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                worker.RunWorkerAsync(file);
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                _errorReport.Add(new ErrorMessage(string.Empty, "�o�ͤ������~", e.Error.Message));
                _finished++;
                progressBarX1.Value = _finished;
                progressBarX1.Text = _finished + "/" + progressBarX1.Maximum;
                return;
            }
            UploadedFileArgument arg = e.Result as UploadedFileArgument;

            Bitmap showImage = Photo.Resize(arg.File, _picWidth, _picHeight);
            pictureBox1.Image = showImage;
            //labelX2.Text = arg.Message;
            lblMsg.Text = arg.Message;
            Application.DoEvents();


            if (arg.ErrorMessage != null)
                _errorReport.Add(arg.ErrorMessage);

            _finished++;
            progressBarX1.Value = _finished;
            progressBarX1.Text = _finished + "/" + progressBarX1.Maximum;

            if (_finished != progressBarX1.Maximum) return;

            // ������n������
            progressBarX1.Visible = false;
            this.Size = _originSize;
            if (_errorReport.Count == 0)
            {
                MsgBox.Show("�@�~����!");
                return;
            }

            if (MsgBox.Show("�@�~�����A���������ɮ׳B�z���ѡA�O�_�˵����~���i�H", "�o�Ϳ��~", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            Workbook book = new Workbook();
            Worksheet sheet = book.Worksheets[0];
            int index = 2;
            Cell cell = sheet.Cells["A1"];
            cell.PutValue("���~�ɮ�");
            cell = sheet.Cells["B1"];
            cell.PutValue("���~�T��");
            cell = sheet.Cells["C1"];
            cell.PutValue("�ԲӸ�T");

            foreach (ErrorMessage error in _errorReport)
            {
                cell = sheet.Cells["A" + index];
                cell.PutValue(error.Key);

                cell = sheet.Cells["B" + index];
                cell.PutValue(error.Message);

                cell = sheet.Cells["C" + index];
                cell.PutValue(error.Detail);
                index++;
            }
            sheet.AutoFitColumns();
            sheet.Cells.SetColumnWidth(2, 0);

            string path = Path.Combine(Application.StartupPath, "Reports");

            //�p�G�ؿ����s�b�h�إߡC
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            path = Path.Combine(path, "�W�ǷӤ����~���i.xls");
            try
            {
                book.Save(path);
            }
            catch (IOException)
            {
                try
                {
                    FileInfo file = new FileInfo(path);
                    string nameTempalte = file.FullName.Replace(file.Extension, "") + "{0}.xls";
                    int count = 1;
                    string fileName = string.Format(nameTempalte, count);
                    while (File.Exists(fileName))
                        fileName = string.Format(nameTempalte, count++);

                    book.Save(fileName, FileFormatType.Excel2000);
                    path = fileName;
                }
                catch (Exception ex)
                {
                    MsgBox.Show("�ɮ��x�s����:" + ex.Message, "���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show("�ɮ��x�s����:" + ex.Message, "���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                MsgBox.Show("�ɮ׶}�ҥ���:" + ex.Message, "���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        AccessHelper accessHelper = new AccessHelper();
        object obj = new object();
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            lock (obj)
            {
                FileInfo file = e.Argument as FileInfo;
                UploadedFileArgument arg = new UploadedFileArgument(file);
                ErrorMessage error = null;

                string filename = GetName(file);

                lock (_photos)
                {
                    if (_photos.ContainsKey(filename))
                    {
                        error = new ErrorMessage(filename, "�ɮצW�٭���", file.FullName + "�w���ۦP�ɦW���ɮצs�b");
                        arg.ErrorMessage = error;
                        arg.Message = filename + "�w�������ɮ�";
                        e.Result = arg;
                        return;
                    }
                    _photos.Add(filename, string.Empty);
                }


                //���J�Ӥ�                
                string b64 = string.Empty;
                try
                {
                    Bitmap pic = Photo.Resize(file);
                    b64 = Photo.GetBase64Encoding(pic);
                }
                catch (Exception ex)
                {
                    error = new ErrorMessage(filename, "�ഫ���A����", ex.Message);
                    arg.ErrorMessage = error;
                    arg.Message = filename + "�ഫ���A����";
                    e.Result = arg;
                    return;
                }

                // �P�_���ǥͬO�_�s�b            
                DSXmlHelper h = new DSXmlHelper("Request");
                h.AddElement("Field");
                h.AddElement("Field", "ID");
                //h.AddElement("Field", "Name");
                //h.AddElement("Field", "ClassName");
                //h.AddElement("Field", "SeatNo");
                //h.AddElement("Field", "StudentNumber");
                //h.AddElement("Field", "IDNumber");
                h.AddElement("Condition");
                h.AddElement("Condition", rbIDNumber.Checked ? "IDNumber" : "StudentNumber", filename);
                DSResponse dsrsp = QueryStudent.CallService("SmartSchool.Student.GetDetailList", new DSRequest(h));
                if (!dsrsp.HasContent)
                {
                    error = new ErrorMessage(filename, "���o�򥻸�ƥ���", dsrsp.GetFault().Message);
                    arg.ErrorMessage = error;
                    arg.Message = filename + "-���o�򥻸�ƥ���!";
                    e.Result = arg;
                    return;
                }

                h = dsrsp.GetContent();
                if (h.GetElement("Student") == null)
                {
                    error = new ErrorMessage(filename, "�d�L�ŦX���", string.Empty);
                    arg.ErrorMessage = error;
                    arg.Message = filename + "-�d�L�ŦX���!";
                    e.Result = arg;
                    return;
                }
                SmartSchool.Customization.Data.StudentRecord studentRec = accessHelper.StudentHelper.GetStudents(h.GetText("Student/@ID"))[0];
                // ��ܾǥ͸�T
                string info = "�ǥͩm�W�G" + studentRec.StudentName + "<br/>";
                info += "�Ҧb�Z�šG" + studentRec.RefClass==null?"":studentRec.RefClass.ClassName + "<br/>";
                info += "�Z�Ůy���G" + studentRec.SeatNo+ "<br/>";
                info += "�ǡ@�@���G" + studentRec.StudentNumber+ "<br/>";
                info += "�����Ҹ��G" + studentRec.IDNumber;

                //�W�ǷӤ�
                h = new DSXmlHelper("Request");
                h.AddElement("Student");
                h.AddElement("Student", rbGraduate.Checked ? "GraduatePhoto" : "FreshmanPhoto", b64);
                h.AddElement("Student", rbIDNumber.Checked ? "IDNumber" : "StudentNumber", filename);

                try
                {
                    EditStudent.UpdatePhoto(new DSRequest(h));
                }
                catch (Exception ex)
                {
                    error = new ErrorMessage(filename, "�Ӥ��W�ǥ���", ex.Message);
                    arg.ErrorMessage = error;
                    arg.Message = filename + "-�W�ǥ���!";
                    e.Result = arg;
                    return;
                }
                arg = new UploadedFileArgument(file);
                arg.Message = info;
                arg.ErrorMessage = error;
                e.Result = arg;
            }
        }

        private string GetName(FileInfo file)
        {
            if (string.IsNullOrEmpty(file.Extension))
                return file.Name;
            else
                return file.Name.Substring(0, file.Name.Length - file.Extension.Length);
        }

        private void BatchUploadPhotoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void BatchUploadPhotoForm_Load(object sender, EventArgs e)
        {

        }
    }

    class UploadedFileArgument
    {
        public UploadedFileArgument(FileInfo file)
        {
            _file = file;
        }

        private FileInfo _file;

        public FileInfo File
        {
            get { return _file; }
        }
        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private ErrorMessage _errorMessage;

        public ErrorMessage ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }
    }

    class ErrorMessage
    {
        public ErrorMessage(string key, string message, string detail)
        {
            _key = key;
            _message = message;
            _detail = detail;
        }
        private string _key;

        public string Key
        {
            get { return _key; }
        }
        private string _message;

        public string Message
        {
            get { return _message; }
        }
        private string _detail;

        public string Detail
        {
            get { return _detail; }
        }
    }
}