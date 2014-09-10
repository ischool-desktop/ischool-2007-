using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using IntelliSchool.DSA30.Util;
using SmartSchool.AccessControl;

namespace SmartSchool.Security
{
    public class RowFeature : DataGridViewRow
    {
        public void RegisterToGridView(DataGridView gridView, XmlElement featureData)
        {
            DSXmlHelper hlpData = new DSXmlHelper(featureData);

            string code = hlpData.GetText("@FeatureCode");
            string title = hlpData.GetText("@Title");
            //string color = hlpData.GetText("@Color");

            CreateCells(gridView, title, "無", code); //建立相對應的 Cells(DataGridVie 功能)。
            //if (!string.IsNullOrEmpty(color))
            //{
            //    this.Cells[0].Style.SelectionForeColor = Color.FromName(color);
            //    this.Cells[0].Style.BackColor = Color.FromName(color);
            //}
            int newIndex = gridView.Rows.Add(this);

            //這一行目的只是為了「取消」DataGridView 「機車」的列共同機制。
            DataGridViewRow newRow = gridView.Rows[newIndex];

            OnRegister(hlpData);
        }

        public string Title
        {
            get { return Cells[0].Value + ""; }
            set { Cells[0].Value = value; }
        }

        public AccessOptions Permission
        {
            get { return ParseOut(Cells[1].Value + ""); }
            set { Cells[1].Value = ParseIn(value); }
        }

        public string FeatureCode
        {
            get { return Cells[2].Value + ""; }
            private set { Cells[2].Value = value; }
        }

        public virtual void OnRegister(DSXmlHelper featureData)
        {
        }

        public XmlElement FeatureData { get; set; }

        #region Private Methods

        private string ParseIn(AccessOptions value)
        {
            string strAccessOption;
            switch (value)
            {
                case AccessOptions.View:
                    strAccessOption = "檢視";
                    break;
                case AccessOptions.Edit:
                    strAccessOption = "編輯";
                    break;
                case AccessOptions.Execute:
                    strAccessOption = "執行";
                    break;
                default:
                    strAccessOption = "無";
                    break;
            }
            return strAccessOption;
        }

        private AccessOptions ParseOut(string strAccessOption)
        {
            switch (strAccessOption)
            {
                case "View":
                case "檢視":
                    return AccessOptions.View;
                case "Edit":
                case "編輯":
                    return AccessOptions.Edit;
                case "Execute":
                case "執行":
                    return AccessOptions.Execute;
                default:
                    return AccessOptions.None;
            }
        }
        #endregion
    }
}
