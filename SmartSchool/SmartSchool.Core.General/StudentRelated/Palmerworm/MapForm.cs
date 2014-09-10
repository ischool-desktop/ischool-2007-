using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Web;
using SmartSchool.Common;

namespace SmartSchool.StudentRelated.Palmerworm
{
    internal partial class MapForm : BaseForm
    {
        protected MapForm(string lat, string lng, string html)
        {
            InitializeComponent();

            string param = "<Content><Lat>" + lat + "</Lat><Lng>" + lng + "</Lng><Info>" + html + "</Info></Content>";
            string fullUrl = "http://dsns.blazer.org.tw/GMaps/SimpleMap.aspx?param=" + HttpUtility.UrlEncode(param);

            webBrowser1.Navigate(fullUrl);
        }

        public static void ShowMap(string lat, string lng, string html)
        {
            MapForm map = new MapForm(lat, lng, html);
            map.ShowDialog();
        }

        private void MapForm_Resize(object sender, EventArgs e)
        {
            //Text = Size.ToString();
        }
    }
}