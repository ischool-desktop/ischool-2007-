using System;
using System.Collections.Generic;
using System.Text;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Feature.Tag
{
    public class EditStudentTag : FeatureBase
    {
        /// <summary>
        /// 移除指定學生的所有 Tag。
        /// </summary>
        [QueryRequest()]
        public static void Remove(List<int> studentId)
        {
            SmartSchool.Common.MultiThreadWorker<int> worker = new SmartSchool.Common.MultiThreadWorker<int>();
            worker.MaxThreads = 3;
            worker.PackageSize = 350;
            worker.PackageWorker += new EventHandler<SmartSchool.Common.PackageWorkEventArgs<int>>(worker_PackageWorker);
            worker.Run(studentId);
        }

        [QueryRequest()]
        static void worker_PackageWorker(object sender, SmartSchool.Common.PackageWorkEventArgs<int> e)
        {
            string strServiceName = "SmartSchool.Tag.RemoveStudentTag";
            DSXmlHelper req = new DSXmlHelper("RemoveStudentTagRequest");
            foreach ( int each in e.List )
                req.AddElement(".", "Tag", "<RefStudentID>" + each + "</RefStudentID>", true);

            CallService(strServiceName, new DSRequest(req));
        }

        /// <summary>
        /// 移除指定學生一個 Tag。
        /// </summary>
        /// <param name="tagId">要移除的 Tag。</param>
        /// <param name="studentId">指定的學生。</param>
        [QueryRequest()]
        public static void Remove(List<int> studentId, int tagId)
        {
            string strServiceName = "SmartSchool.Tag.RemoveStudentTag";
            DSXmlHelper request = new DSXmlHelper("RemoveStudentTagRequest");

            foreach (int each in studentId)
            {
                request.AddElement("Tag");
                request.AddElement("Tag", "RefStudentID", each.ToString());
                request.AddElement("Tag", "RefTagID", tagId.ToString());
            }

            CallService(strServiceName, new DSRequest(request));
        }

        [QueryRequest()]
        public static void Remove(params RemoveInfo[] removes)
        {
            SmartSchool.Common.MultiThreadWorker<RemoveInfo> removeWorker = new SmartSchool.Common.MultiThreadWorker<RemoveInfo>();
            removeWorker.MaxThreads = 3;
            removeWorker.PackageSize = 350;
            removeWorker.PackageWorker += new EventHandler<SmartSchool.Common.PackageWorkEventArgs<RemoveInfo>>(removeWorker_PackageWorker);
            removeWorker.Run(removes);
        }

        [QueryRequest()]
        static void removeWorker_PackageWorker(object sender, SmartSchool.Common.PackageWorkEventArgs<RemoveInfo> e)
        {
            bool call_service = false;
            string strServiceName = "SmartSchool.Tag.RemoveStudentTag";
            DSXmlHelper request = new DSXmlHelper("RemoveStudentTagRequest");

            foreach ( RemoveInfo each in e.List )
            {
                foreach ( int eachTag in each.Tags )
                {
                    request.AddElement("Tag");
                    request.AddElement("Tag", "RefStudentID", each.StudentID.ToString());
                    request.AddElement("Tag", "RefTagID", eachTag.ToString());
                    call_service = true;
                }
            }

            if ( call_service )
                CallService(strServiceName, new DSRequest(request));
        }

        /// <summary>
        /// 為多個學生新增一個 Tag。
        /// </summary>
        /// <param name="tagId">要移除的 Tag。</param>
        /// <param name="studentId">要移除 Tag 的學生。</param>
        public static void Add(List<int> studentId, int tagId)
        {
            Add(studentId, new List<int>(new int[] { tagId }));
        }

        /// <summary>
        /// 為多個學生新增多個 Tag。
        /// </summary>
        public static void Add(List<int> students, List<int> tags)
        {
            SmartSchool.Common.MultiThreadWorker<int> addWorker = new SmartSchool.Common.MultiThreadWorker<int>();
            addWorker.MaxThreads = 3;
            addWorker.PackageSize = 350;
            addWorker.PackageWorker += new EventHandler<SmartSchool.Common.PackageWorkEventArgs<int>>(addWorker_PackageWorker);
            addWorker.Run(students, tags);
        }

        static void addWorker_PackageWorker(object sender, SmartSchool.Common.PackageWorkEventArgs<int> e)
        {
            bool call_service = false;
            string strServiceName = "SmartSchool.Tag.AddStudentTag";
            DSXmlHelper request = new DSXmlHelper("AddStudentTagRequest");

            foreach ( int student in e.List )
            {
                foreach ( int tag in (List<int>)e.Argument )
                {
                    request.AddElement("Tag");
                    request.AddElement("Tag", "RefStudentID", student.ToString());
                    request.AddElement("Tag", "RefTagID", tag.ToString());
                    call_service = true;
                }
            }

            if ( call_service )
                CallService(strServiceName, new DSRequest(request));
        }

        public class RemoveInfo
        {
            private int _studentId;
            private List<int> _tags;

            public RemoveInfo(int studentId)
            {
                _studentId = studentId;
                _tags = new List<int>();
            }

            public int StudentID
            {
                get { return _studentId; }
                set { _studentId = value; }
            }

            /// <summary>
            /// 要移除的 Tag  編號。
            /// </summary>
            public List<int> Tags
            {
                get { return _tags; }
            }
        }
    }
}
