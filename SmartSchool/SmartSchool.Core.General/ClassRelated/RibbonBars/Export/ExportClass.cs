using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using System.Xml;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler.Formater;
using SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.Connector;
using SmartSchool.ClassRelated;
using SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler;
using SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.Output;
using DevComponents.DotNetBar;
using System.Diagnostics;

namespace SmartSchool.ClassRelated.RibbonBars.Export
{
    public partial class ExportClass : BaseForm
    {
        public ExportClass()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExportClass_Load(object sender, EventArgs e)
        {
            XmlElement element = SmartSchool.Feature.Class.ClassBulkProcess.GetExportDescription();
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
            saveFileDialog1.FileName = "匯出班級基本資料";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)            
            {
                IExportConnector ec = new ExportClassConnector();
                foreach (ClassInfo info  in SmartSchool.ClassRelated.Class.Instance.SelectionClasses)
                {
                    ec.AddCondition(info.ClassID);
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

        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView.Items)
            {
                item.Checked = chkSelect.Checked;
            }
        }    
    }
}