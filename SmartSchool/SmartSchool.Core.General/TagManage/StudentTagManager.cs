using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated;
using System.ComponentModel;
using SmartSchool.Feature.Tag;
using System.Xml;
using IntelliSchool.DSA30.Util;
using System.Threading;
using System.Windows.Forms;

namespace SmartSchool.TagManage
{
    public class StudentTagManager
    {
        private Student _instance;
        private EntityItemCollection _entity_items;
        private EntityItemCollection _processing_entity_items;
        private BackgroundWorker _bg_worker;
        private ManualResetEvent _wait_handle;
        private List<BriefStudentData> _current_selection;
        private object ThisLocker = new object();
        private bool _cancel_work;

        public StudentTagManager(Student instance)
        {
            _wait_handle = new ManualResetEvent(true);
            _entity_items = new EntityItemCollection();
            _processing_entity_items = _entity_items;
            _instance = instance;
            //instance.SelectionChanged += new EventHandler(Student_SelectionChanged);
            SmartSchool.Broadcaster.Events.Items["學生/選取變更"].Handler+=delegate
            {
                LockSynchorizeSelection();
            };

            _bg_worker = new BackgroundWorker();
            _bg_worker.DoWork += new DoWorkEventHandler(BGWorker_DoWork);
        }

        //private void Student_SelectionChanged(object sender, EventArgs e)
        //{
        //    //RefreshSelection();
        //    LockSynchorizeSelection();
        //}

        private void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            SynchorizeSelectionOfTagData();
            _wait_handle.Set();
        }

        private void LockSynchorizeSelection()
        {
            _wait_handle.Reset();

            if (!_bg_worker.IsBusy)
                _bg_worker.RunWorkerAsync(GetSelectionItem());
        }

        public void RefreshSelectionReference()
        {
            _current_selection = GetSelectionItem();
        }

        private List<BriefStudentData> GetSelectionItem()
        {
            BriefStudentData[] students = new BriefStudentData[_instance.SelectionStudents.Count];
            _instance.SelectionStudents.CopyTo(students);
            return new List<BriefStudentData>(students);
        }

        public void SynchorizeSelection()
        {
            if (_bg_worker.IsBusy)
            {
                _cancel_work = true;
                _wait_handle.WaitOne();
                _cancel_work = false;
            }

            SynchorizeSelectionOfTagData();
            
            InvokeEntityItemsChanged();
        }

        public bool IsSynchronized
        {
            get
            {
                if (_entity_items.Count != _current_selection.Count)
                    return false;

                foreach (BriefStudentData each in _current_selection)
                {
                    if (!_entity_items.ContainsKey(int.Parse(each.ID)))
                        return false;
                }

                return true;
            }
        }

        public EntityItemCollection EntityItems
        {
            get
            {
                return _entity_items;
            }
        }

        public event EventHandler EntityItemsChanged;
        private delegate void ThreadSafeInvoker();
        private void InvokeEntityItemsChanged()
        {
            if (_instance.ContentPanel.InvokeRequired)
            {
                ThreadSafeInvoker method = new ThreadSafeInvoker(InvokeEntityItemsChanged);
                _instance.ContentPanel.Invoke(method);
            }
            else
                if (EntityItemsChanged != null)
                    EntityItemsChanged(this, EventArgs.Empty);
        }

        private void SynchorizeSelectionOfTagData()
        {
            lock (ThisLocker)
            {
                _processing_entity_items = new EntityItemCollection();
                List<BriefStudentData> objStudents = _current_selection;
                List<int> intStudents = new List<int>();

                if (_cancel_work) return;

                foreach (BriefStudentData each in objStudents)
                {
                    int identity = int.Parse(each.ID);
                    intStudents.Add(identity);
                    _processing_entity_items.Add(identity, new EntityItem(identity, each));

                    if (_cancel_work) return;
                }

                if (_cancel_work) return;

                XmlElement xmlTags = QueryTag.GetDetailListByStudent(intStudents);

                if (_cancel_work) return;

                foreach (XmlElement each in xmlTags.SelectNodes("Tag"))
                {
                    int tagId = int.Parse(each.GetAttribute("ID"));
                    int studentId = int.Parse(each.SelectSingleNode("StudentID").InnerText);
                    EntityItem item = _processing_entity_items[studentId];
                    item.Tags.Add(tagId);

                    if (_cancel_work) return;
                }
                _entity_items = _processing_entity_items;
            }
        }

    }
}
