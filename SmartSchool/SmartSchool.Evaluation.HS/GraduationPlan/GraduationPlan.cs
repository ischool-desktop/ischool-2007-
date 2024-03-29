﻿using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Feature.GraduationPlan;
using IntelliSchool.DSA30.Util;
using System.Xml;
using System.ComponentModel;
using System.Threading;
using SmartSchool.Customization.Data;

namespace SmartSchool.Evaluation.GraduationPlan
{
    public class GraduationPlan
    {
        private static GraduationPlan _Instance;

        public static GraduationPlan Instance
        {
            get 
            {
                if (_Instance == null)
                    _Instance = new GraduationPlan();
                return _Instance;
            }
        }

        private AccessHelper accessHelper = new AccessHelper();

        private BackgroundWorker _BKWGraduationPlanLoader;

        private BackgroundWorker _BKWCommonPlanLoader;

        private BackgroundWorker _BKWStudentReferenceLoader;

        private BackgroundWorker _BKWClassReferenceLoader;

        private ManualResetEvent _LoadingEvent;

        private ManualResetEvent _LoadingCommonEvent;

        private ManualResetEvent _LoadingStudentReferenceEvent;

        private ManualResetEvent _LoadingClassReferenceEvent;

        internal Dictionary<string, GraduationPlanInfo> _Items;

        private XmlElement _CommonElement;

        private Dictionary<string, string> _StudentReference;

        private Dictionary<string, string> _ClassReference;

        internal static void CreateInstance()
        {
            _Instance = new GraduationPlan();
        }

        private GraduationPlan()
        {
            _LoadingEvent = new ManualResetEvent(true);
            _LoadingCommonEvent = new ManualResetEvent(true);
            _LoadingStudentReferenceEvent = new ManualResetEvent(true);
            _LoadingClassReferenceEvent = new ManualResetEvent(true);

            _BKWGraduationPlanLoader = new BackgroundWorker();
            _BKWGraduationPlanLoader.DoWork += new DoWorkEventHandler(_BKWGraduationPlanLoader_DoWork);

            _BKWCommonPlanLoader = new BackgroundWorker();
            _BKWCommonPlanLoader.DoWork += new DoWorkEventHandler(_BKWCommonPlanLoader_DoWork);

            _BKWStudentReferenceLoader = new BackgroundWorker();
            _BKWStudentReferenceLoader.DoWork += new DoWorkEventHandler(_BKWStudentReferenceLoader_DoWork);

            _BKWClassReferenceLoader = new BackgroundWorker();
            _BKWClassReferenceLoader.DoWork += new DoWorkEventHandler(_BKWClassReferenceLoader_DoWork);

            LoadGraduationPlan();
            LoadCommonPlan();
            LoadStudentReference();
            LoadClassReference();
        }

        #region 這裡有一個排序用的沒有其他用途的私有class
        private class GPlanXmlSorter : IComparer<XmlElement>
        {
            #region IComparer<XmlElement> 成員

            public int Compare(XmlElement x, XmlElement y)
            {
                return new GraduationPlanInfo(x).Name.CompareTo(new GraduationPlanInfo(y).Name);
            }

            #endregion
        }
        #endregion
        private void _BKWGraduationPlanLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _Items = new Dictionary<string, GraduationPlanInfo>();
                DSResponse resp = QueryGraduationPlan.GetGraduationPlan();
                List<XmlElement> SortList = new List<XmlElement>();
                foreach (XmlElement gPlan in resp.GetContent().GetElements("GraduationPlan"))
                {
                    SortList.Add(gPlan);
                }
                SortList.Sort(new GPlanXmlSorter());
                foreach (XmlElement gPlan in SortList)
                {
                    _Items.Add(gPlan.SelectSingleNode("@ID").InnerText, new GraduationPlanInfo(gPlan));
                }
            }
            catch { }
            _LoadingEvent.Set();
        }

        private void _BKWClassReferenceLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _ClassReference = QueryGraduationPlan.GetClassReference();
            }
            catch { }
            _LoadingClassReferenceEvent.Set();
        }

        private void _BKWStudentReferenceLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _StudentReference = QueryGraduationPlan.GetStudentReference();
            }
            catch { }
            _LoadingStudentReferenceEvent.Set();
        }

        private void _BKWCommonPlanLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _CommonElement = QueryGraduationPlan.GetCommon().GetContent().GetElement("GraduationPlan");
                if (_CommonElement == null)
                {
                    _CommonElement = new XmlDocument().CreateElement("GraduationPlan");
                }
            }
            catch { }
            _LoadingCommonEvent.Set();
        }

        public void LoadGraduationPlan()
        {
            if (!_BKWGraduationPlanLoader.IsBusy)
            {
                _LoadingEvent.Reset();
                _BKWGraduationPlanLoader.RunWorkerAsync();
            }
        }

        public void LoadStudentReference()
        {
            if ( !_BKWStudentReferenceLoader.IsBusy )
            {
                _LoadingStudentReferenceEvent.Reset();
                _BKWStudentReferenceLoader.RunWorkerAsync();
            }
        }

        public void LoadClassReference()
        {
            if ( !_BKWClassReferenceLoader.IsBusy )
            {
                _LoadingClassReferenceEvent.Reset();
                _BKWClassReferenceLoader.RunWorkerAsync();
            }
        }

        public void LoadCommonPlan()
        {
            if (!_BKWCommonPlanLoader.IsBusy)
            {
                _LoadingCommonEvent.Reset();
                _BKWCommonPlanLoader.RunWorkerAsync();
            }
        }

        public GraduationPlanInfoCollection Items
        {
            get
            {
                _LoadingEvent.WaitOne();
                return new GraduationPlanInfoCollection(_Items);
            }
        }

        public XmlElement CommonPlan
        {
            get { _LoadingCommonEvent.WaitOne(); return _CommonElement; }
        }

        public GraduationPlanSelector GetSelector()
        {
            return new GraduationPlanSelector();
        }

        public bool IsStudentOverrided(string id)
        {
            _LoadingStudentReferenceEvent.WaitOne();
            return _StudentReference.ContainsKey(id);
        }

        public GraduationPlanInfo GetStudentGraduationPlan(string id)
        {
            _LoadingStudentReferenceEvent.WaitOne();
            if ( _StudentReference.ContainsKey(id) )//自己有救回傳
                return Items[_StudentReference[id]];
            else
            {
                if ( accessHelper.StudentHelper.GetStudents(id)[0].RefClass != null )//有所屬班級就抓所屬班級的
                    return GetClassGraduationPlan(accessHelper.StudentHelper.GetStudents(id)[0].RefClass.ClassID);
                else
                    return null;//自己沒有也沒有所屬班級就回傳nulll
            }
            return null;
        }

        public GraduationPlanInfo GetClassGraduationPlan(string id)
        {
            _LoadingClassReferenceEvent.WaitOne();
            if ( _ClassReference.ContainsKey(id) )//有救回傳
                return Items[_ClassReference[id]];
            else
                return null;//沒有拉倒
        }

        public void Reflash()
        {
            _Instance = new GraduationPlan();
        }

    }

    public class UpdateGraduationPlanEventArgs : EventArgs
    {
        private GraduationPlanInfo _OldInfo, _NewInfo;
        public UpdateGraduationPlanEventArgs()
        { 
        
        }
        public UpdateGraduationPlanEventArgs(GraduationPlanInfo oldInfo, GraduationPlanInfo newInfo)
        {
            _OldInfo = oldInfo;
            _NewInfo = newInfo;
        }
        public GraduationPlanInfo OldInfo
        {
            get { return _OldInfo; }
            set { _OldInfo = value; }
        }
        public GraduationPlanInfo NewInfo
        {
            get { return _NewInfo; }
            set { _NewInfo = value; }
        }
    }

    public class DeleteGraduationPlanEventArgs : EventArgs
    {
        private string _id;
        public DeleteGraduationPlanEventArgs(string id)
        {
            _id = id;
        }
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
    }

}
