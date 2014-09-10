using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;
using System.Windows.Forms;
using SmartSchool.StudentRelated.Search;
using System.Drawing;

namespace SmartSchool.StudentRelated.SourceProvider
{
    class NormalStudentSourceProvider:DragDropTreeNode,ISourceProvider<BriefStudentData,SmartSchool.StudentRelated.Search.ISearchStudent>
    {
        private List<BriefStudentData> _Source = new List<BriefStudentData>();

        private Search.SearchStudentInMetadata _SearchProvider;

        public NormalStudentSourceProvider()
        {

            this.Text = "在校學生(" + _Source.Count + ")";
        }

        #region ISourceProvider<BriefStudentData,ISearchStudent> 成員
        public bool ImmediatelySearch { get { return true; } }

        public List<BriefStudentData> Source
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
                this.Text = "在校學生(" + _Source.Count + ")";
                if (SourceChanged != null)
                    SourceChanged.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler SourceChanged;

        public SmartSchool.StudentRelated.Search.ISearchStudent SearchProvider
        {
            get
            {
                if (_SearchProvider == null)
                {
                    _SearchProvider = new SmartSchool.StudentRelated.Search.SearchStudentInMetadata();
                }
                _SearchProvider.Source = _Source;
                return _SearchProvider;
            }
        }

        public bool DisplaySource
        {
            get { return false; }
        }

        public string SearchWatermark { get { return "在\"在校學生\"中搜尋"; } }
        #endregion       

        public override System.Windows.Forms.DragDropEffects CheckDragDropEffects(System.Windows.Forms.IDataObject Data, int keyStatus)
        {
            if (Data.GetData(typeof(List<BriefStudentData>)) != null)
            {
                return System.Windows.Forms.DragDropEffects.All;
            }
            else
                return base.CheckDragDropEffects(Data, keyStatus);
        }
        public override void DragDrop(System.Windows.Forms.IDataObject Data, int keyStatus)
        {
            if (Data.GetData(typeof(List<BriefStudentData>)) != null)
            {
                MsgBox.Show("你拖了" + ((List<BriefStudentData>)Data.GetData(typeof(List<BriefStudentData>))).Count + "名學生到\"在校學生\"中\n會發生什麼事請自行想像。");
            }
        }
    }

    class GradeStudentSourceProvider : DragDropTreeNode, ISourceProvider<BriefStudentData, SmartSchool.StudentRelated.Search.ISearchStudent>
    {
        private List<BriefStudentData> _Source = new List<BriefStudentData>();

        private string _Grade;

        private Search.SearchStudentInMetadata _SearchProvider;

        public string Grade
        {
            get 
            {
                return _Grade;
            }
            set
            {
                _Grade = value;
                this.Text = ""+_Grade+"年級(" +(_Source!=null? _Source.Count :0)+ ")";
            }
        }

        #region ISourceProvider<BriefStudentData,ISearchStudent> 成員
        public bool ImmediatelySearch { get { return true; } }


        public List<BriefStudentData> Source
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
                this.Text = "" + _Grade + "年級(" + _Source.Count + ")";
                if (SourceChanged != null)
                    SourceChanged.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler SourceChanged;

        public SmartSchool.StudentRelated.Search.ISearchStudent SearchProvider
        {
            get
            {
                if (_SearchProvider == null)
                {
                    _SearchProvider = new SmartSchool.StudentRelated.Search.SearchStudentInMetadata();
                }
                _SearchProvider.Source = _Source;
                return _SearchProvider;
            }
        }

        public bool DisplaySource
        {
            get { return false; }
        }

        public string SearchWatermark { get { return "在\"" + _Grade + "年級\"中搜尋"; } }
        #endregion
    }

    class ClassStudentSourceProvider : DragDropTreeNode, ISourceProvider<BriefStudentData, SmartSchool.StudentRelated.Search.ISearchStudent>
    {
        private List<BriefStudentData> _Source=new List<BriefStudentData>();

        private string _Grade,_ClassName,_ClassID;

        private Search.SearchStudentInMetadata _SearchProvider;

        public string Grade
        {
            get
            {
                return _Grade;
            }
            set
            {
                _Grade = value;
            }
        }

        public string ClassName
        {
            get { return _ClassName; }
            set
            {
                _ClassName = value;
                this.Text = "" + _ClassName + "(" + (_Source != null ? _Source.Count : 0) + ")";
            }
        }

        public string ClassID
        {
            get
            {
                return _ClassID;
            }
            set
            {
                _ClassID = value;
            }
        }
        #region ISourceProvider<BriefStudentData,ISearchStudent> 成員
        public bool ImmediatelySearch { get { return true; } }

        public List<BriefStudentData> Source
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
                this.Text = "" + _ClassName + "(" + (_Source != null ? _Source.Count : 0) + ")";
                if (SourceChanged != null)
                    SourceChanged.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler SourceChanged;

        public SmartSchool.StudentRelated.Search.ISearchStudent SearchProvider
        {
            get
            {
                if (_SearchProvider == null)
                {
                    _SearchProvider = new SmartSchool.StudentRelated.Search.SearchStudentInMetadata();
                }
                _SearchProvider.Source = _Source;
                return _SearchProvider;
            }
        }

        public bool DisplaySource
        {
            get { return true; }
        }

        public string SearchWatermark { get { return "在\"" + _ClassName + "\"中搜尋"; } }
        #endregion
    }

    class NonClassStudentSourceProvider : DragDropTreeNode, ISourceProvider<BriefStudentData, SmartSchool.StudentRelated.Search.ISearchStudent>
    {
        private List<BriefStudentData> _Source = new List<BriefStudentData>();

        private string _Grade;

        private Search.SearchStudentInMetadata _SearchProvider;

        public string Grade
        {
            get
            {
                return _Grade;
            }
            set
            {
                _Grade = value;
            }
        }

        public NonClassStudentSourceProvider()
        {
            this.Text = "未分班(0)";
        }

        #region ISourceProvider<BriefStudentData,ISearchStudent> 成員
        public bool ImmediatelySearch { get { return true; } }

        public List<BriefStudentData> Source
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
                this.Text = "未分班(" + _Source.Count + ")";
                if (SourceChanged != null)
                    SourceChanged.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler SourceChanged;

        public SmartSchool.StudentRelated.Search.ISearchStudent SearchProvider
        {
            get
            {
                if (_SearchProvider == null)
                {
                    _SearchProvider = new SmartSchool.StudentRelated.Search.SearchStudentInMetadata();
                }
                _SearchProvider.Source = _Source;
                return _SearchProvider;
            }
        }

        public bool DisplaySource
        {
            get { return true; }
        }

        public string SearchWatermark { get { return "在\"未分班\"中搜尋"; } }
        #endregion
    }

    class NonGradeStudentSourceProvider : DragDropTreeNode, ISourceProvider<BriefStudentData, SmartSchool.StudentRelated.Search.ISearchStudent>
    {
        private List<BriefStudentData> _Source = new List<BriefStudentData>();

        private Search.SearchStudentInMetadata _SearchProvider;

        public NonGradeStudentSourceProvider()
        {
            this.Text = "未分年級(0)";
        }

        #region ISourceProvider<BriefStudentData,ISearchStudent> 成員
        public bool ImmediatelySearch { get { return true; } }

        public List<BriefStudentData> Source
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
                this.Text = "未分年級(" + _Source.Count + ")";
                if (SourceChanged != null)
                    SourceChanged.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler SourceChanged;

        public SmartSchool.StudentRelated.Search.ISearchStudent SearchProvider
        {
            get
            {
                if (_SearchProvider == null)
                {
                    _SearchProvider = new SmartSchool.StudentRelated.Search.SearchStudentInMetadata();
                }
                _SearchProvider.Source = _Source;
                return _SearchProvider;
            }
        }

        public bool DisplaySource
        {
            get { return false; }
        }

        public string SearchWatermark { get { return "在\"未分年級\"中搜尋"; } }
        #endregion
    }

    class SearchAllStudentSourceProvider :DragDropTreeNode, ISourceProvider<BriefStudentData, SmartSchool.StudentRelated.Search.ISearchStudent>
    {
        private List<BriefStudentData> _Source = new List<BriefStudentData>();

        private Search.SearchStudentInMetadata _SearchProvider;

        #region ISourceProvider<BriefStudentData,ISearchStudent> 成員
        public bool ImmediatelySearch { get { return true; } }

        public List<BriefStudentData> Source
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
                if (SourceChanged != null)
                    SourceChanged.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler SourceChanged;

        public bool DisplaySource
        {
            get { return false; }
        }

        public SmartSchool.StudentRelated.Search.ISearchStudent SearchProvider
        {
            get
            {
                if ( _SearchProvider == null )
                {
                    _SearchProvider = new SmartSchool.StudentRelated.Search.SearchStudentInMetadata();
                }
                _SearchProvider.Source = new List<BriefStudentData>(Student.Instance.Items);
                return _SearchProvider;
            }
        }

        public string SearchWatermark
        {
            get { return "搜尋所有學生"; }
        }

        #endregion
    }

    class TempStudentSourceProvider:DragDropTreeNode, ISourceProvider<BriefStudentData, SmartSchool.StudentRelated.Search.ISearchStudent>
    {
        private List<BriefStudentData> _Source = new List<BriefStudentData>();

        private Search.SearchStudentInMetadata _SearchProvider;

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Source = new List<BriefStudentData>();
        }

        public TempStudentSourceProvider()
        {
            this.Text = "待處理學生 (0)";
        }
        #region ISourceProvider<BriefStudentData,ISearchStudent> 成員
        public bool ImmediatelySearch { get { return true; } }

        public List<BriefStudentData> Source
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
                this.Text = "待處理學生 (" + _Source.Count + ")";
                if (SourceChanged != null)
                    SourceChanged.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler SourceChanged;

        public SmartSchool.StudentRelated.Search.ISearchStudent SearchProvider
        {
            get
            {
                if (_SearchProvider == null)
                {
                    _SearchProvider = new SmartSchool.StudentRelated.Search.SearchStudentInMetadata();
                }
                _SearchProvider.Source = _Source;
                return _SearchProvider;
            }
        }

        public bool DisplaySource
        {
            get { return true; }
        }

        public string SearchWatermark { get { return "在\"待處理學生 \"中搜尋"; } }
        #endregion
        public override System.Windows.Forms.DragDropEffects CheckDragDropEffects(System.Windows.Forms.IDataObject Data, int keyStatus)
        {
            if (Data.GetData(typeof(List<BriefStudentData>)) != null)
            {
                return System.Windows.Forms.DragDropEffects.All;
            }
            else
                return base.CheckDragDropEffects(Data, keyStatus);
        }
        public override void DragDrop(System.Windows.Forms.IDataObject Data, int keyStatus)
        {
            if (Data.GetData(typeof(List<BriefStudentData>)) != null)
            {                
                List<BriefStudentData> insertList=new List<BriefStudentData>();
                foreach (BriefStudentData var in (List<BriefStudentData>)Data.GetData(typeof(List<BriefStudentData>)))
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

    class OnLeaveStudentSourceProvider:DragDropTreeNode, ISourceProvider<BriefStudentData, SmartSchool.StudentRelated.Search.ISearchStudent>
    {
        private List<BriefStudentData> _Source = new List<BriefStudentData>();

        private Search.SearchStudentInMetadata _SearchProvider;

        public OnLeaveStudentSourceProvider()
        {
            this.Text = "休學學生 (0)";
        }

        #region ISourceProvider<BriefStudentData,ISearchStudent> 成員
        public bool ImmediatelySearch { get { return true; } }

        public List<BriefStudentData> Source
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
                this.Text = "休學學生 (" + _Source.Count + ")";
                if (SourceChanged != null)
                    SourceChanged.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler SourceChanged;

        public SmartSchool.StudentRelated.Search.ISearchStudent SearchProvider
        {
            get
            {
                if (_SearchProvider == null)
                {
                    _SearchProvider = new SmartSchool.StudentRelated.Search.SearchStudentInMetadata();
                }
                _SearchProvider.Source = _Source;
                return _SearchProvider;
            }
        }

        public bool DisplaySource
        {
            get { return true; }
        }

        public string SearchWatermark { get { return "在\"休學學生 \"中搜尋"; } }
        #endregion
    }

    class ExtendingStudentSourceProvider : DragDropTreeNode, ISourceProvider<BriefStudentData, SmartSchool.StudentRelated.Search.ISearchStudent>
    {
        private List<BriefStudentData> _Source = new List<BriefStudentData>();

        private Search.SearchStudentInMetadata _SearchProvider;

        public ExtendingStudentSourceProvider()
        {
            this.Text = "延修學生 (0)";
        }

        #region ISourceProvider<BriefStudentData,ISearchStudent> 成員
        public bool ImmediatelySearch { get { return true; } }

        public List<BriefStudentData> Source
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
                this.Text = "延修學生 (" + _Source.Count + ")";
                if (SourceChanged != null)
                    SourceChanged.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler SourceChanged;

        public SmartSchool.StudentRelated.Search.ISearchStudent SearchProvider
        {
            get
            {
                if (_SearchProvider == null)
                {
                    _SearchProvider = new SmartSchool.StudentRelated.Search.SearchStudentInMetadata();
                }
                _SearchProvider.Source = _Source;
                return _SearchProvider;
            }
        }

        public bool DisplaySource
        {
            get { return true; }
        }

        public string SearchWatermark { get { return "在\"延修學生 \"中搜尋"; } }
        #endregion
    }

    class DeletedStudentSourceProvider : DragDropTreeNode, ISourceProvider<BriefStudentData, SmartSchool.StudentRelated.Search.ISearchStudent>
    {
        private List<BriefStudentData> _Source = new List<BriefStudentData>();

        private Search.SearchStudentInMetadata _SearchProvider;

        public DeletedStudentSourceProvider()
        {
            this.Text = "刪除學生 (0)";
        }

        #region ISourceProvider<BriefStudentData,ISearchStudent> 成員
        public bool ImmediatelySearch { get { return true; } }

        public List<BriefStudentData> Source
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
                this.Text = "刪除學生 (" + _Source.Count + ")";
                if (SourceChanged != null)
                    SourceChanged.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler SourceChanged;

        public SmartSchool.StudentRelated.Search.ISearchStudent SearchProvider
        {
            get
            {
                if (_SearchProvider == null)
                {
                    _SearchProvider = new SmartSchool.StudentRelated.Search.SearchStudentInMetadata();
                }
                _SearchProvider.Source = _Source;
                return _SearchProvider;
            }
        }

        public bool DisplaySource
        {
            get { return true; }
        }

        public string SearchWatermark { get { return "在\"刪除學生 \"中搜尋"; } }
        #endregion
    }

    class GraduatedStudentSourceProvider : DragDropTreeNode, ISourceProvider<BriefStudentData, SmartSchool.StudentRelated.Search.ISearchStudent>
    {
        private Dictionary<string, NormalizeStudentSourceProvider> _SubProviders = new Dictionary<string, NormalizeStudentSourceProvider>();

        private List<BriefStudentData> _Source = new List<BriefStudentData>();

        private Search.SearchStudentInMetadata _SearchProvider;

        public GraduatedStudentSourceProvider()
        {
            this.Text = "畢業及離校學生(0)";
        }

        #region ISourceProvider<BriefStudentData,ISearchStudent> 成員
        public bool ImmediatelySearch { get { return true; } }

        public List<BriefStudentData> Source
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
                this.Text = "畢業及離校學生 (" + _Source.Count + ")";
                this.Nodes.Clear();
                #region 整理離校學年度
                SortedList<int, List<BriefStudentData>> schoolYearStudents = new SortedList<int, List<BriefStudentData>>();
                List<BriefStudentData> noSchoolYearStudents = new List<BriefStudentData>();
                foreach ( BriefStudentData studentData in _Source )
                {
                    int sy = 0;
                    if ( int.TryParse(studentData.LeaveSchoolYear, out sy) )
                    {
                        if ( !schoolYearStudents.ContainsKey(sy) )
                            schoolYearStudents.Add(sy, new List<BriefStudentData>());
                        schoolYearStudents[sy].Add(studentData);
                    }
                    else
                    {
                        noSchoolYearStudents.Add(studentData);
                    }
                }
                foreach ( int sy in schoolYearStudents.Keys )
                {
                    #region 依學年度加入節點
                    string pathY = "" + sy + "學年度";
                    NormalizeStudentSourceProvider pathProviderY;
                    if ( !_SubProviders.ContainsKey(pathY) )
                    {
                        pathProviderY = new NormalizeStudentSourceProvider();
                        pathProviderY.Text = pathY;
                        pathProviderY.DisplaySource = true;
                        _SubProviders.Add(pathY, pathProviderY);
                    }
                    else
                    {
                        pathProviderY = _SubProviders[pathY];
                    }
                    pathProviderY.Nodes.Clear();
                    pathProviderY.Source = schoolYearStudents[sy];
                    this.Nodes.Add(pathProviderY); 
                    #endregion
                    #region 整理離校類別
                    SortedList<string, List<BriefStudentData>> reasonStudents = new SortedList<string, List<BriefStudentData>>();
                    List<BriefStudentData> noReasonStudents = new List<BriefStudentData>();
                    foreach ( BriefStudentData studentData in schoolYearStudents[sy] )
                    {
                        if ( studentData.LeaveReason != "" )
                        {
                            if ( !reasonStudents.ContainsKey(studentData.LeaveReason) )
                                reasonStudents.Add(studentData.LeaveReason, new List<BriefStudentData>());
                            reasonStudents[studentData.LeaveReason].Add(studentData);
                        }
                        else
                        {
                            noReasonStudents.Add(studentData);
                        }
                    }
                    foreach ( string reason in reasonStudents.Keys )
                    {
                        #region 依離校類別加入節點
                        string pathR = "" + sy + "學年度\\"+reason;
                        NormalizeStudentSourceProvider pathProviderR;
                        if ( !_SubProviders.ContainsKey(pathR) )
                        {
                            pathProviderR = new NormalizeStudentSourceProvider();
                            pathProviderR.Text = reason;
                            pathProviderR.DisplaySource = true;
                            _SubProviders.Add(pathR, pathProviderR);
                        }
                        else
                        {
                            pathProviderR = _SubProviders[pathR];
                        }
                        pathProviderR.Nodes.Clear();
                        pathProviderR.Source = reasonStudents[reason];
                        pathProviderY.Nodes.Add(pathProviderR);  
                        #endregion
                        #region 整理科別
                        SortedList<string, List<BriefStudentData>> deptStudents = new SortedList<string, List<BriefStudentData>>();
                        List<BriefStudentData> nodeptStudents = new List<BriefStudentData>();
                        foreach ( BriefStudentData studentData in reasonStudents[reason] )
                        {
                            if ( studentData.LeaveDepartment != "" )
                            {
                                if ( !deptStudents.ContainsKey(studentData.LeaveDepartment) )
                                    deptStudents.Add(studentData.LeaveDepartment, new List<BriefStudentData>());
                                deptStudents[studentData.LeaveDepartment].Add(studentData);
                            }
                            else
                                nodeptStudents.Add(studentData);
                        }
                        foreach ( string dept in deptStudents.Keys )
                        {
                            #region 依離校科別加入節點
                            string pathD = "" + sy + "學年度\\" + reason + "\\" + dept;
                            NormalizeStudentSourceProvider pathProviderD;
                            if ( !_SubProviders.ContainsKey(pathD) )
                            {
                                pathProviderD = new NormalizeStudentSourceProvider();
                                pathProviderD.Text = dept;
                                pathProviderD.DisplaySource = true;
                                _SubProviders.Add(pathD, pathProviderD);
                            }
                            else
                            {
                                pathProviderD = _SubProviders[pathD];
                            }
                            pathProviderD.Nodes.Clear();
                            pathProviderD.Source = deptStudents[dept];
                            pathProviderR.Nodes.Add(pathProviderD);
                            #endregion
                        }
                        if(nodeptStudents.Count>0)
                        {
                            #region 依離校科別加入節點
                            string pathD = "" + sy + "學年度\\" + reason + "\\" + "其他";
                            NormalizeStudentSourceProvider pathProviderD;
                            if ( !_SubProviders.ContainsKey(pathD) )
                            {
                                pathProviderD = new NormalizeStudentSourceProvider();
                                pathProviderD.Text = "其他";
                                pathProviderD.DisplaySource = true;
                                _SubProviders.Add(pathD, pathProviderD);
                            }
                            else
                            {
                                pathProviderD = _SubProviders[pathD];
                            }
                            pathProviderD.Nodes.Clear();
                            pathProviderD.Source = nodeptStudents;
                            pathProviderR.Nodes.Add(pathProviderD);
                            #endregion
                        }
                        #endregion
                    }
                    if ( noReasonStudents.Count > 0 )
                    {
                        #region 加入其他節點
                        string pathR = "" + sy + "學年度\\" + "其他";
                        NormalizeStudentSourceProvider pathProviderR;
                        if ( !_SubProviders.ContainsKey(pathR) )
                        {
                            pathProviderR = new NormalizeStudentSourceProvider();
                            pathProviderR.Text = "其他";
                            pathProviderR.DisplaySource = true;
                            _SubProviders.Add(pathR, pathProviderR);
                        }
                        else
                        {
                            pathProviderR = _SubProviders[pathR];
                        }
                        pathProviderR.Nodes.Clear();
                        pathProviderR.Source = noReasonStudents;
                        pathProviderY.Nodes.Add(pathProviderR);
                        #endregion
                    }
                    #endregion
                }
                if ( noSchoolYearStudents.Count > 0 )
                {
                    #region 加入其他節點
                    string path = "其他";
                    NormalizeStudentSourceProvider pathProvider;
                    if ( !_SubProviders.ContainsKey(path) )
                    {
                        pathProvider = new NormalizeStudentSourceProvider();
                        pathProvider.Text = path;
                        pathProvider.DisplaySource = true;
                        _SubProviders.Add(path, pathProvider);
                    }
                    else
                    {
                        pathProvider = _SubProviders[path];
                    }
                    pathProvider.Nodes.Clear();
                    pathProvider.Source = noSchoolYearStudents;
                    this.Nodes.Add(pathProvider); 
                    #endregion
                }
                #endregion
                if ( SourceChanged != null )
                    SourceChanged.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler SourceChanged;

        public bool DisplaySource
        {
            get { return false; }
        }

        public SmartSchool.StudentRelated.Search.ISearchStudent SearchProvider
        {
            get
            {
                if ( _SearchProvider == null )
                {
                    _SearchProvider = new SmartSchool.StudentRelated.Search.SearchStudentInMetadata();
                }
                _SearchProvider.Source = _Source;
                return _SearchProvider;
            }
        }

        public string SearchWatermark
        {
            get { return "在\"畢業及離校學生 \"中搜尋"; }
        }

        #endregion
    }
}
