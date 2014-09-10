using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;
using SmartSchool.Feature.Tag;
using System.ComponentModel;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.TagManage
{
    public class TagManager
    {
        private object ThisLocker = new object();
        private TagCategory _category;
        private BackgroundWorker _bg_worker;
        private bool _first_initial = true;

        public TagManager(TagCategory category)
        {
            _category = category;
            _wait_handle = new ManualResetEvent(true);

            _bg_worker = new BackgroundWorker();
            _bg_worker.DoWork += new DoWorkEventHandler(BGWorker_DoWork);
            _bg_worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGWorker_RunWorkerCompleted);

            _tags = new TagCollection();
            _prefixes = new PrefixCollection();

            Refresh();
        }

        public void Refresh()
        {
            lock ( ThisLocker )
            {
                LockDataAccess();
                if ( !_bg_worker.IsBusy )
                    _bg_worker.RunWorkerAsync();
            }
        }

        private void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DSXmlHelper objTags = new DSXmlHelper(QueryTag.GetDetailList(_category));

                TagCollection tags = new TagCollection();

                foreach (XmlElement each in objTags.GetElements("Tag"))
                {
                    TagInfo info = new TagInfo(each);
                    tags.Add(info.Identity, info);
                }

                lock (ThisLocker)
                {
                    _tags = tags;
                    _prefixes = tags.GroupPrefix();
                }
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
            finally
            {
                UnlockDataAccess();
            }
        }

        private void BGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is Exception)
                throw e.Result as Exception;

            if (!_first_initial) //�Ĥ@�� Initial �ɡA�����ͦ��ƥ�C
                InvokeTagRefresh();
            else
                _first_initial = false;
        }

        /// <summary>
        /// �s�W Tag ��ƨ��Ʈw���C
        /// </summary>
        /// <param name="newTag">�n�s�W��  Tag ����AIdentity�ݩʥ����O  -1�C</param>
        public void Insert(TagInfo newTag)
        {
            if (newTag.Identity == -1)
            {
                string prefix, name;
                int color;

                prefix = newTag.Prefix;
                name = newTag.Name;
                color = newTag.Color.ToArgb();

                EditTag.Insert(prefix, name, color, _category);

                Refresh();
            }
            else
                throw new Exception("�� Tag �w�s�b�A���i�s�W�C");
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update(TagInfo tag)
        {
            if (Tags.ContainsKey(tag.Identity))
            {
                string prefix, name;
                int color;

                prefix = tag.Prefix;
                name = tag.Name;
                color = tag.Color.ToArgb();

                EditTag.Update(tag.Identity, prefix, name, color);

                Refresh();
            }
            else
                throw new Exception("�� Tag ���s�b�A�L�k��s�C");
        }

        public void Delete(TagInfo tag)
        {
            if (Tags.ContainsKey(tag.Identity))
            {
                EditTag.Delete(tag.Identity);

                Refresh();
            }
            else
                throw new Exception("�� Tag ���s�b�A�L�k�R���C");
        }

        private PrefixCollection _prefixes;
        public PrefixCollection Prefixes
        {
            get
            {
                _wait_handle.WaitOne();

                lock (ThisLocker)
                {
                    return _prefixes;
                }
            }
        }

        private TagCollection _tags;
        public TagCollection Tags
        {
            get
            {
                _wait_handle.WaitOne();

                lock (ThisLocker)
                {
                    return _tags;
                }
            }
        }

        private ManualResetEvent _wait_handle;

        public event EventHandler TagRefresh;
        private void InvokeTagRefresh()
        {
            if (TagRefresh != null)
                TagRefresh(this, EventArgs.Empty);
        }

        private void LockDataAccess()
        {
            _wait_handle.Reset();
        }

        private void UnlockDataAccess()
        {
            _wait_handle.Set();
        }
    }
}
