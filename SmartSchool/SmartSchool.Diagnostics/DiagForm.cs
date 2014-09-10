using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.NetworkInformation;
using System.Net;
using System.Diagnostics;
using System.Management;
using System.IO;
using System.Xml;

namespace SmartSchool.Diagnostics
{
    public partial class DiagForm : Form
    {
        public DiagForm()
        {
            InitializeComponent();
        }
        public void Run()
        {
            string filename="";
            if (Directory.Exists(Environment.CurrentDirectory + "\\Diagnostics"))
                filename = Environment.CurrentDirectory + "\\Diagnostics\\" + DateTime.Now.ToString("") + "-Diagnostics";
            else
                filename = Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Diagnostics";
            StreamWriter txt = new StreamWriter(filename + ".txt");
            XmlWriter xml = XmlWriter.Create(filename + ".xml");
            xml.Settings.Indent = true;
            xml.WriteStartElement("TestSuites");
            //suites
            new HostinfoTestSuite(txt,xml);
            //
            txt.Close();
            StreamReader reader = new StreamReader(filename + ".txt");
            xml.WriteStartElement("Log");
            xml.WriteCData(reader.ReadToEnd());
            xml.WriteEndElement();
            //
            xml.WriteEndElement();
            xml.Close();
        }

        /*

        void genText()
        {
            log("產生文字報表...", true);

            
            foreach (ListViewItem item in lstStatus.Items)
            {
                string result = "";
                foreach (ListViewItem.ListViewSubItem item2 in item.SubItems)
                {
                    result += item2.Text + "\t";
                }
                result += "\r\n";
                file.Write(result);
            }
            file.Close();
        }

        void finishReport()
        {
            if (networkCanConnect)
            {
                //顯示資料已傳回
                log("正在連接伺服器，回傳測試結果...", true);
                MessageBox.Show("診斷測試完成，請耐心等待客服人員回報測試結果，並協助您處理網路問題");
                this.Close();
            }
            else
            {
                //顯示網路無法連接的處理辦法
                MessageBox.Show("本次測試結果的檔案將放置於\r\n" + filename + ".txt及" + filename + ".xml\r\n請將這兩個檔案利用電子郵件傳給客服人員，謝謝！");
            }
        }

        */

    }
}