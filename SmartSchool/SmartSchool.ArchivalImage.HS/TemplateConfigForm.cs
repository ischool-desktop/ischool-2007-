using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;
using Aspose.Words;
using System.Xml;
using SmartSchool.Common;

namespace SmartSchool.ArchivalImage
{
    public partial class TemplateConfigForm : BaseForm
    {
        private int _useTemplateNumber = 1;

        private byte[] _buffer1 = null;
        private byte[] _buffer2 = null;

        private string _base64string1 = null;
        private string _base64string2 = null;

        private bool _isUpload1 = false;
        private bool _isUpload2 = false;

        private int _custodian = 0;
        private int _address = 0;
        private int _phone = 0;

        public TemplateConfigForm(
            int useTemplateNumber,
            byte[] buffer1,
            byte[] buffer2,
            int[] print,
            bool over100,
            string coreSubjectSign,
            string resitSign,
            string retakeSign,
            string schoolYearAdjustSign,
            string manualAdjustSign,
            string failedSign)
        {
            InitializeComponent();

            _useTemplateNumber = useTemplateNumber;
            switch (_useTemplateNumber)
            {
                case 1:
                    radioBtn1.Checked = true;
                    break;
                case 2:
                    radioBtn2.Checked = true;
                    break;
                case 3:
                    radioBtn3.Checked = true;
                    break;
                case 4:
                    radioBtn4.Checked = true;
                    break;
                default:
                    radioBtn1.Checked = true;
                    break;
            }

            _buffer1 = buffer1;
            _buffer2 = buffer2;

            cboRecvIdentity.SelectedIndex = print[0];
            cboRecvAddress.SelectedIndex = print[1];
            cboRecvPhone.SelectedIndex = print[2];
            checkBoxX1.Checked = over100;

            txtCoreSubjectSign.Text = coreSubjectSign;
            txtResitSign.Text = resitSign;
            txtRetakeSign.Text = retakeSign;

            txtFailedSign.Text = failedSign;
            txtSchoolYearAdjustSign.Text = schoolYearAdjustSign;
            txtManualAdjustSign.Text = manualAdjustSign;
        }

        private void radioGroup1_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioBtn1.Checked && !radioBtn2.Checked)
            {
                radioBtn1.Checked = true;
                radioBtn2.Checked = false;
                radioBtn3.Checked = false;
                radioBtn4.Checked = false;
                radioGroup2.Checked = false;
            }
        }

        private void radioGroup2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioGroup2.Checked)
            {
                if (!radioBtn3.Checked && !radioBtn4.Checked)
                {
                    radioBtn1.Checked = false;
                    radioBtn2.Checked = false;
                    radioBtn3.Checked = true;
                    radioBtn4.Checked = false;
                    radioGroup1.Checked = false;
                }
            }
        }

        private void radioBtn1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtn1.Checked)
            {
                radioGroup1.Checked = true;
                radioGroup2.Checked = false;
                radioBtn2.Checked = false;
                radioBtn3.Checked = false;
                radioBtn4.Checked = false;
                _useTemplateNumber = 1;
            }
        }

        private void radioBtn2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtn2.Checked)
            {
                radioGroup1.Checked = true;
                radioGroup2.Checked = false;
                radioBtn1.Checked = false;
                radioBtn3.Checked = false;
                radioBtn4.Checked = false;
                _useTemplateNumber = 2;
            }
        }

        private void radioBtn3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtn3.Checked)
            {
                radioGroup1.Checked = false;
                radioGroup2.Checked = true;
                radioBtn1.Checked = false;
                radioBtn2.Checked = false;
                radioBtn4.Checked = false;
                _useTemplateNumber = 3;
            }
        }

        private void radioBtn4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtn4.Checked)
            {
                radioGroup1.Checked = false;
                radioGroup2.Checked = true;
                radioBtn1.Checked = false;
                radioBtn2.Checked = false;
                radioBtn3.Checked = false;
                _useTemplateNumber = 4;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "�t�s�s��";
            sfd.FileName = "���y��.doc";
            sfd.Filter = "Word�ɮ� (*.doc)|*.doc|�Ҧ��ɮ� (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    fs.Write(Properties.Resources.���y����, 0, Properties.Resources.���y����.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    MsgBox.Show("���w���|�L�k�s���C", "�t�s�ɮץ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "�t�s�s��";
            sfd.FileName = "���y��.doc";
            sfd.Filter = "Word�ɮ� (*.doc)|*.doc|�Ҧ��ɮ� (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    fs.Write(Properties.Resources.���y��¾, 0, Properties.Resources.���y��¾.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    MsgBox.Show("���w���|�L�k�s���C", "�t�s�ɮץ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "�t�s�s��";
            sfd.FileName = "�ۭq���y��.doc";
            sfd.Filter = "Word�ɮ� (*.doc)|*.doc|�Ҧ��ɮ� (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    if (Aspose.Words.Document.DetectFileFormat(new MemoryStream(_buffer1)) == Aspose.Words.LoadFormat.Doc)
                        fs.Write(_buffer1, 0, _buffer1.Length);
                    else
                        fs.Write(Properties.Resources.���y����, 0, Properties.Resources.���y����.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    MsgBox.Show("���w���|�L�k�s���C", "�t�s�ɮץ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "�t�s�s��";
            sfd.FileName = "�ۭq���y��.doc";
            sfd.Filter = "Word�ɮ� (*.doc)|*.doc|�Ҧ��ɮ� (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    if (Aspose.Words.Document.DetectFileFormat(new MemoryStream(_buffer2)) == Aspose.Words.LoadFormat.Doc)
                        fs.Write(_buffer2, 0, _buffer2.Length);
                    else
                        fs.Write(Properties.Resources.���y��¾, 0, Properties.Resources.���y��¾.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    MsgBox.Show("���w���|�L�k�s���C", "�t�s�ɮץ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "��ܦۭq�����y��d��";
            ofd.Filter = "Word�ɮ� (*.doc)|*.doc";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (Document.DetectFileFormat(ofd.FileName) == LoadFormat.Doc)
                    {
                        FileStream fs = new FileStream(ofd.FileName, FileMode.Open);

                        byte[] tempBuffer = new byte[fs.Length];
                        fs.Read(tempBuffer, 0, tempBuffer.Length);
                        _base64string1 = Convert.ToBase64String(tempBuffer);
                        _isUpload1 = true;
                        fs.Close();
                        MsgBox.Show("�W�Ǧ��\�C");
                    }
                    else
                        MsgBox.Show("�W���ɮ׮榡����");
                }
                catch
                {
                    MsgBox.Show("���w���|�L�k�s���C", "�}���ɮץ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "��ܦۭq�����y��d��";
            ofd.Filter = "Word�ɮ� (*.doc)|*.doc";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (Document.DetectFileFormat(ofd.FileName) == LoadFormat.Doc)
                    {
                        FileStream fs = new FileStream(ofd.FileName, FileMode.Open);

                        byte[] tempBuffer = new byte[fs.Length];
                        fs.Read(tempBuffer, 0, tempBuffer.Length);
                        _base64string2 = Convert.ToBase64String(tempBuffer);
                        _isUpload2 = true;
                        fs.Close();
                        MsgBox.Show("�W�Ǧ��\�C");
                    }
                    else
                        MsgBox.Show("�W���ɮ׮榡����");
                }
                catch
                {
                    MsgBox.Show("���w���|�L�k�s���C", "�}���ɮץ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            #region �x�s Preference

            XmlElement config = SmartSchool.Customization.Data.SystemInformation.Preference["�ǥ;��y��"];

            if (config == null)
            {
                config = new XmlDocument().CreateElement("�ǥ;��y��");
            }

            config.SetAttribute("TemplateNumber", _useTemplateNumber.ToString());

            XmlElement customize1 = config.OwnerDocument.CreateElement("CustomizeTemplate1");
            XmlElement customize2 = config.OwnerDocument.CreateElement("CustomizeTemplate2");
            XmlElement print = config.OwnerDocument.CreateElement("Print");

            if (_isUpload1)
            {
                customize1.InnerText = _base64string1;
                config.ReplaceChild(customize1, config.SelectSingleNode("CustomizeTemplate1"));
            }

            if (_isUpload2)
            {
                customize2.InnerText = _base64string2;
                config.ReplaceChild(customize2, config.SelectSingleNode("CustomizeTemplate2"));
            }

            print.SetAttribute("Custodian", cboRecvIdentity.SelectedIndex.ToString());
            print.SetAttribute("Address", cboRecvAddress.SelectedIndex.ToString());
            print.SetAttribute("Phone", cboRecvPhone.SelectedIndex.ToString());
            print.SetAttribute("AllowMoralScoreOver100", checkBoxX1.Checked.ToString());
            print.SetAttribute("CoreSubjectSign", txtCoreSubjectSign.Text);
            print.SetAttribute("ResitSign", txtResitSign.Text);
            print.SetAttribute("RetakeSign", txtRetakeSign.Text);
            print.SetAttribute("SchoolYearAdjustSign", txtSchoolYearAdjustSign.Text);
            print.SetAttribute("ManualAdjustSign", txtManualAdjustSign.Text);
            print.SetAttribute("FailedSign", txtFailedSign.Text);

            config.ReplaceChild(print, config.SelectSingleNode("Print"));

            SmartSchool.Customization.Data.SystemInformation.Preference["�ǥ;��y��"] = config;

            #endregion

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}