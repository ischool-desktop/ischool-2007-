using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace SmartSchool.StudentRelated.RibbonBars.GMap
{
    public delegate string InfoRequest(string param);
    public delegate void ClickNotification(string token, object args);

    [ComVisible(true)]
    public class GMapHelper
    {
        public GMapHelper(WebBrowser browser)
        {
            _browser = browser;
            Browser.AllowWebBrowserDrop = false;
            Browser.IsWebBrowserContextMenuEnabled = false;
            Browser.WebBrowserShortcutsEnabled = false;
            Browser.ObjectForScripting = this;
        }

        #region ForClient Methods

        private WebBrowser _browser;
        public WebBrowser Browser
        {
            get { return _browser; }
        }

        public void LoadGMap()
        {
            string path = Path.Combine(Application.StartupPath, "GMapContainer.htm");

            FileStream file = CreateTempFile(path);
            Stream stream = GetGMapWebPage();

            byte[] whold = new byte[stream.Length];
            stream.Read(whold, 0, whold.Length);

            file.Write(whold, 0, whold.Length);
            file.Close();

            Browser.Navigate(path);
        }

        public AddressMark CreateMark(AddressInfo address)
        {
            object mark = InvokeScript("ssCreateMark", address.Lat, address.Lng, address.Student.Identity);
            return new AddressMark(this, address, mark);
        }

        internal void SetMapSize(int height, int width)
        {
            InvokeScript("ssSetMapSize", height + "px", width + "px");
        }

        internal void PanToMark(object mark)
        {
            InvokeScript("ssPanToMarker", mark);
        }

        public void PanToPoint(string lat, string lng)
        {
            InvokeScript("ssPanToPoint", lat, lng);
        }

        internal bool IsViewable(object mark)
        {
            return Convert.ToBoolean(InvokeScript("ssIsInBound", mark));
        }

        internal void SetMarkImage(object mark,string url)
        {
            InvokeScript("ssSetMarkImage", mark, url);
        }

        internal void OpenInfoWindow(object mark, string html)
        {
            InvokeScript("ssOpenInfoWindow", mark, html);
        }

        private static FileStream CreateTempFile(string path)
        {
            try
            {
                return new FileStream(path, FileMode.Create);
            }
            catch (Exception ex)
            {
                string msg = string.Format("建立地圖資訊失敗，請確定您有目錄取存權限。({0})", path);
                throw new ArgumentException(msg, ex);
            }
        }

        private Stream GetGMapWebPage()
        {
            //Stream result = GetType().Assembly.GetManifestResourceStream("SmartSchool.SmartPlugIn.Student.GMap.GMapContainer.htm");
            Stream result =new MemoryStream( Properties.Resources.GMapContainer);
            if (result == null)
                throw new InvalidOperationException("讀取地圖程式庫資源失敗。");

            return result;
        }

        #endregion

        #region ForScripting Methods

        public event EventHandler GMapComplete;
        public void RaiseGMapComplete()
        {
            if (GMapComplete != null)
                GMapComplete(this, EventArgs.Empty);
        }

        public event InfoRequest InformationRequest;
        public string WindowInfoRequire(object studentId)
        {
            if (InformationRequest != null)
                return InformationRequest(studentId.ToString());
            else
                return string.Empty;
        }

        public event ClickNotification ObjectClick;
        public void RaiseObjectClick(string token, object args)
        {
            if (ObjectClick != null)
                ObjectClick(token, args);
        }

        public event EventHandler GMapMoved;
        public void RaiseGMapMoved()
        {
            if (GMapMoved != null)
                GMapMoved(this, EventArgs.Empty);
        }
        #endregion

        private object InvokeScript(string name, params object[] paramList)
        {
            try
            {
                return Browser.Document.InvokeScript(name, paramList);
            }
            catch
            {
                return null;
            }
        }
    }
}
