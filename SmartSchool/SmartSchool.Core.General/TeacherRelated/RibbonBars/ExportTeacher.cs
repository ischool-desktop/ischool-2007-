using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using System.Xml;
//using SmartSchool.SmartPlugIn.Student.Export.RequestHandler.Formater;
//using SmartSchool.SmartPlugIn.Student.Export.RequestHandler;
//using SmartSchool.SmartPlugIn.Student.Export.ResponseHandler.Connector;
using SmartSchool.TeacherRelated;
//using SmartSchool.SmartPlugIn.Student.Export.ResponseHandler.Output;
using System.Diagnostics;
//using SmartSchool.SmartPlugIn.Student.Export.ResponseHandler;
using DevComponents.DotNetBar;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler.Formater;
using SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.Connector;
using SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler;
using SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.Output;

namespace SmartSchool.TeacherRelated.RibbonBars
{
    public partial class ExportTeacher : BaseForm
    {
        public ExportTeacher()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExportTeacher_Load(object sender, EventArgs e)
        {
            XmlElement element = SmartSchool.Feature.Teacher.TeacherBulkProcess.GetExportDescription();
            BaseFieldFormater formater = new BaseFieldFormater();
            FieldCollection collection = formater.Format(element);

            foreach (Field field in collection)
            {
                ListViewItem item = listView.Items.Add(field.DisplayText);
                item.Tag = field;
                item.Checked = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (GetSelectedFields().Count == 0)
            {
                MsgBox.Show("必須至少選擇一項匯出欄位!", "欄位空白", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            saveFileDialog1.Filter = "Excel (*.xls)|*.xls|所有檔案 (*.*)|*.*";
            saveFileDialog1.FileName = "匯出教師基本資料";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                IExportConnector ec = new ExportTeacherConnector();
                foreach (BriefTeacherData teacher in SmartSchool.TeacherRelated.Teacher.Instance.SelectionTeachers)
                {
                    ec.AddCondition(teacher.ID);
                }
                ec.SetSelectedFields(GetSelectedFields());
                ExportTable table = ec.Export();

                ExportOutput output = new ExportOutput();
                output.SetSource(table);
                output.Save(saveFileDialog1.FileName);

                if (MsgBox.Show("檔案存檔完成，是否開啟該檔案", "是否開啟", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        Process.Start(saveFileDialog1.FileName);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("開啟檔案發生失敗:" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                this.Close();
            }
        }

        private FieldCollection GetSelectedFields()
        {
            FieldCollection collection = new FieldCollection();
            foreach (ListViewItem item in listView.CheckedItems)
            {
                Field field = item.Tag as Field;
                collection.Add(field);
            }
            return collection;
        }

        private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView.Items)
            {
                item.Checked = chkSelect.Checked;
            }
        }
    }
}