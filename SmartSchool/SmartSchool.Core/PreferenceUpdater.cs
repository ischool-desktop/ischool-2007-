using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SmartSchool
{
    public class PreferenceUpdater
    {
        static private PreferenceUpdater _Instance;
        static public PreferenceUpdater Instance
        {
            get
            {
                if (_Instance == null) _Instance = new PreferenceUpdater();
                return _Instance;
            }
        }

        private PreferenceUpdater()
        {
            Application.ThreadExit += new EventHandler(Application_ThreadExit);
        }
        private List<IPreference> _Items=new List<IPreference>();
        public List<IPreference> Items
        {
            get { return _Items; }
        }
        public void UpdatePreferences()
        {
            foreach (IPreference var in _Items)
            {
                try
                {
                    var.UpdatePreference();
                }
                catch (Exception e)
                {
                    CurrentUser user = CurrentUser.Instance;
                    SmartSchool.ExceptionHandler.BugReporter.ReportException("SmartSchool", user.SystemVersion, e, false);
                }
            }
        }
        static void Application_ThreadExit(object sender, EventArgs e)
        {
            try
            {
                if (CurrentUser.Instance.IsLogined && CurrentUser.Instance.Preference != null)
                {
                    PreferenceUpdater.Instance.UpdatePreferences();
                    CurrentUser.Instance.Preference.SaveToServer();
                    //CurrentUser.Instance.SchoolConfig.Save();
                }
            }
            catch
            { }
        }
    }
}
