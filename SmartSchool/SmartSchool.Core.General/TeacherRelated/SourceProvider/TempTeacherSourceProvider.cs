using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;
using SmartSchool.TeacherRelated.Search;
using System.Windows.Forms;

namespace SmartSchool.TeacherRelated.SourceProvider
{
    class TempTeacherSourceProvider: DragDropTreeNode, ISourceProvider<BriefTeacherData,ISearchTeacher>
    {
        private List<BriefTeacherData> _Source = new List<BriefTeacherData>();

        private SearchTeacherInMetadata _SearchProvider;

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Source = new List<BriefTeacherData>();
        }

        public TempTeacherSourceProvider()
        {
            this.Text = "待處理教師 (0)";
        }
        #region ISourceProvider<BriefTeacherData,ISearchTeacher> 成員

        public bool ImmediatelySearch{ get { return true; } }

        public List<BriefTeacherData> Source
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
                this.Text = "待處理教師 (" + _Source.Count + ")";
                if (SourceChanged != null)
                    SourceChanged.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler SourceChanged;

        public ISearchTeacher SearchProvider
        {
            get
            {
                if (_SearchProvider == null)
                {
                    _SearchProvider = new Search.SearchTeacherInMetadata();
                }
                _SearchProvider.Source = _Source;
                return _SearchProvider;
            }
        }

        public bool DisplaySource
        {
            get { return true; }
        }

        public string SearchWatermark { get { return "在\"待處理教師 \"中搜尋"; } }
        #endregion
        public override System.Windows.Forms.DragDropEffects CheckDragDropEffects(System.Windows.Forms.IDataObject Data, int keyStatus)
        {
            if (Data.GetData(typeof(List<BriefTeacherData>)) != null)
            {
                return System.Windows.Forms.DragDropEffects.All;
            }
            else
                return base.CheckDragDropEffects(Data, keyStatus);
        }
        public override void DragDrop(System.Windows.Forms.IDataObject Data, int keyStatus)
        {
            if (Data.GetData(typeof(List<BriefTeacherData>)) != null)
            {
                List<BriefTeacherData> insertList = new List<BriefTeacherData>();
                foreach (BriefTeacherData var in (List<BriefTeacherData>)Data.GetData(typeof(List<BriefTeacherData>)))
                {
                    if (!_Source.Contains(var))
                        insertList.Add(var);
                }
                _Source.AddRange(insertList);
                Source = _Source;
            }
        }
        public override System.Windows.Forms.ContextMenuStrip ContextMenuStrip
        {
            get
            {
                ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem();
                #region toolStripMenuItem1
                toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
                toolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripMenuItem1.Name = "toolStripMenuItem1";
                toolStripMenuItem1.Size = new System.Drawing.Size(93, 22);
                toolStripMenuItem1.Text = "清空";
                toolStripMenuItem1.Click += new EventHandler(toolStripMenuItem1_Click);
                #endregion

                ContextMenuStrip contextMenuStrip1 = new ContextMenuStrip();
                #region contextMenuStrip1
                contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
                contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                toolStripMenuItem1});
                contextMenuStrip1.Name = "contextMenuStrip1";
                contextMenuStrip1.ShowImageMargin = false;
                contextMenuStrip1.Size = new System.Drawing.Size(94, 26);
                #endregion
                return contextMenuStrip1;
            }
            set
            {
                //base.ContextMenuStrip = value;
            }
        }
        public override string DragOverMessage
        {
            get
            {
                return "加入至待處理區";
            }
        }
    }

}
