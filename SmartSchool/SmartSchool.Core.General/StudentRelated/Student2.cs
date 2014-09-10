using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using IntelliSchool.DSA30.Util;
using System.Threading;
using SmartSchool.StudentRelated.Divider;
using SmartSchool.Feature;
using System.Xml;
using SmartSchool.StudentRelated.Search;
using SmartSchool.StudentRelated.SourceProvider;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.PlugIn.ExtendedColumn;
using SmartSchool.TagManage;
using SmartSchool.Feature.Tag;
using SmartSchool.API.PlugIn;
using SmartSchool.ClassRelated;
using System.Text.RegularExpressions;
using SmartSchool.StudentRelated.NavigationPlanner;

namespace SmartSchool.StudentRelated
{
    public partial class Student : SmartSchool.GeneralEntityView, IManager<IColumnItem>
    {
        private static Student _Instance = null;

        internal static void CreateInstance()
        {
            _Instance = new Student();
        }

        public static Student Instance
        {
            get { return _Instance; }
        }

        private Student()
        {
            //抓資料
            getBriefData();

            #region Columns
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGridViewImageColumn colStatus = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font(SmartSchool.Common.FontStyles.GeneralFontFamily, 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            //dataGridViewCellStyle2.NullValue = ( (object)( resources.GetObject("dataGridViewCellStyle2.NullValue") ) );
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            colStatus.DefaultCellStyle = dataGridViewCellStyle2;
            colStatus.FillWeight = 1F;
            colStatus.HeaderText = "狀態";
            colStatus.MinimumWidth = 63;
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            colStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            colStatus.ToolTipText = "在校狀態";
            colStatus.Visible = false;
            colStatus.Width = 66;
            DataGridViewTagCellColumn colTag = new SmartSchool.DataGridViewTagCellColumn();

            colTag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            colTag.FillWeight = 65F;
            colTag.HeaderText = "類別";
            colTag.MinimumWidth = 42;
            colTag.Name = "colTag";
            colTag.ReadOnly = true;
            colTag.Resizable = System.Windows.Forms.DataGridViewTriState.True;


            AddNewColumn("ID", 5, 5F, true).Visible = false;
            AddNewColumn(colStatus);
            AddNewColumn("班級", 61, 65).Visible = false;
            AddNewColumn("座號", 63, 30).Visible = false;
            colName = AddNewColumn("姓名", 61, 70, true);
            AddNewColumn("學號", 61, 61.55796F);
            AddNewColumn("性別", 61, 57).Visible = false;
            AddNewColumn("身分證號", 90, 90).Visible = false;
            AddNewColumn("戶籍電話", 90, 80).Visible = false;
            AddNewColumn("聯絡電話", 90, 80).Visible = false;
            AddNewColumn(colTag);
            #endregion

            InitializeComponent();
            this.FiltratedList.ItemsChanged += new EventHandler(FiltratedList_ItemsChanged);
            _ControlInitialized = true;
            FilterChanged();//整理Filter，Function內會自動判斷學生資料級控制項是否皆已準備完成

            _SearchSourceLoader = new BackgroundWorker();
            _SearchSourceLoader.DoWork += new DoWorkEventHandler(_SearchSourceLoader_DoWork);
            _SearchSourceLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_SearchSourceLoader_RunWorkerCompleted);

            this.TemporaSourceChanged += delegate { if ( TemporalChanged != null )TemporalChanged(this, new EventArgs()); };
            this.dgvDisplayList.SortCompare += new DataGridViewSortCompareEventHandler(dgvStudent_SortCompare);
            this.TemporalChanged += new EventHandler(Instance_TemporalChanged);
            dgvDisplayList.CellDoubleClick += new DataGridViewCellEventHandler(dgvStudent_CellDoubleClick);
            SmartSchool.Customization.PlugIn.ExtendedColumn.ExtendStudentColumn.SetManager(this);
            SmartSchool.Customization.PlugIn.ContextMenu.StudentMenuButton.SetManager(this);
            this.SelectedSourceChanged += delegate
            {
                _SelectionList.Clear();
                foreach ( string id in SelectedSource )
                {
                    _SelectionList.Add(this.Items[id]);
                }
                SmartSchool.Broadcaster.Events.Items["學生/選取變更"].Invoke();
                #region 處理毛毛蟲
                //清單模式不用處理
                if ( splitterListDetial.Expanded )
                {
                    if ( SelectedSource.Count > 0 && ( palmerwormStudent1.StudentInfo == null ||!SelectedSource.Contains(palmerwormStudent1.StudentInfo.ID) ) )
                    {
                        palmerwormStudent1.StudentInfo = this.Items[SelectedSource[0]];
                        palmerwormStudent1.Visible = true;
                    }
                    palmerwormStudent1.Visible = SelectedSource.Count > 0;
                }
                #endregion
            };
            Application.Idle += delegate
            {
                if ( _ChangSearchSource && !_SearchSourceLoader.IsBusy )
                {
                    txtSearch.AutoCompleteCustomSource.Clear();
                    _SearchSourceLoader.RunWorkerAsync(new object[] { chkSearchInName.Checked, chkSearchInSSN.Checked, chkSearchInStudentId.Checked, new List<string>(FiltratedList) });
                    _ChangSearchSource = false;
                }
            };
            palmerwormStudent1.Manager = SmartSchool.StudentRelated.Process.StudentIUD.StudentIDUProcess.Instance;
        }

        private void _SearchSourceLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            bool name = (bool)( (object[])e.Argument )[0];
            bool SSN = (bool)( (object[])e.Argument )[1];
            bool studentNumber = (bool)( (object[])e.Argument )[2];
            List<string> list = (List<string>)( (object[])e.Argument )[3];
            List<string> newSource = new List<string>();
            foreach ( string var in list )
            {
                BriefStudentData data = Items[var];
                if ( name && !newSource.Contains(data.Name) )
                    newSource.Add(data.Name);
                if ( SSN && !newSource.Contains(data.IDNumber) )
                    newSource.Add(data.IDNumber);
                if ( studentNumber && !newSource.Contains(data.StudentNumber) )
                    newSource.Add(data.StudentNumber);
            }
            e.Result = newSource.ToArray();
        }

        private void _SearchSourceLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtSearch.AutoCompleteCustomSource.AddRange((string[])e.Result);
        }

        private void FiltratedList_ItemsChanged(object sender, EventArgs e)
        {
            _ChangSearchSource = true;
        }

        private void Instance_ClassUpdated(object sender, UpdateClassEventArgs e)
        {
            Reload();
        }

        private void Instance_ClassInserted(object sender, InsertClassEventArgs e)
        {
            Reload();
        }

        private void Instance_StudentDeleted(object sender, StudentDeletedEventArgs e)
        {
            if ( _StudentList != null )
            {
                _LoadStudent.WaitOne();
                BriefStudentData stuData=_StudentList[e.ID];
                _ClassStudentList[stuData.RefClassID].Remove(stuData);
                _StudentList.Remove(e.ID);                
                Reload();
            }
        }

        private void Instance_ClassDeleted(object sender, DeleteClassEventArgs e)
        {
            List<string> noClassStudentID = new List<string>();

            //foreach ( BriefStudentData var in _StudentList.Values )
            //{
            //    if ( e.DeleteClassIDArray.Contains(var.NonCheckedRefClassID) )
            //        noClassStudentID.Add(var.ID);
            //}
            foreach ( string classID in e.DeleteClassIDArray )
            {
                foreach ( BriefStudentData var in _ClassStudentList[classID] )
                {
                    noClassStudentID.Add(var.ID);
                }
            }

            //InvokBriefDataChanged(noClassStudentID.ToArray());
            SmartSchool.Broadcaster.Events.Items["學生/資料變更"].Invoke(noClassStudentID.ToArray());

            Reload();
        }

        private void Instance_StudentInserted(object sender, EventArgs e)
        {
            getBriefData();
        }
        protected override void Reload()
        {
            FilterChanged();
            foreach ( List<string> source in new List<string>[] { TemporaSource, SelectedSource, DisplaySource } )
            {
                List<string> removeList = new List<string>();
                foreach ( string id in source )
                {
                    if ( !_StudentList.ContainsKey(id) )
                        removeList.Add(id);
                }
                foreach ( string id in removeList )
                {
                    source.Remove(id);
                }
            }
            base.Reload();
        }

        internal List<BriefStudentData> GetClassStudent(string classID)
        {
            _LoadStudent.WaitOne();
            List<BriefStudentData> classList = new List<BriefStudentData>();
            if ( _ClassStudentList.ContainsKey(classID) )
            {
                foreach ( BriefStudentData student in _ClassStudentList[classID] )
                {
                    if ( student.IsNormal )
                    {
                        classList.Add(student);
                    }
                }
            }
            classList.Sort();
            return classList;
        }

        internal void ViewTemporaStudent()
        {
            base.ViewTemp();
        }

        //private TempStudentSourceProvider _TempStudentSourceProvider=new TempStudentSourceProvider();
        public List<BriefStudentData> TemporaStudent
        {
            get
            {
                List<BriefStudentData> list = new List<BriefStudentData>();
                foreach ( string id in TemporaSource )
                {
                    list.Add(Items[id]);
                }
                return list;
            }
            set
            {
                List<string> list = new List<string>();
                foreach ( BriefStudentData var in value )
                {
                    list.Add(var.ID);
                }
                SetTemporaSource(list);
            }
        }

        private TagManager _tag_manager;
        public TagManager TagManager
        {
            get
            {
                if ( _tag_manager == null )
                    _tag_manager = new TagManager(TagCategory.Student);

                return _tag_manager;
            }
        }
        private StudentTagManager _student_tag_manager;
        public StudentTagManager SelectionTagManager
        {
            get
            {
                if ( _student_tag_manager == null )
                    _student_tag_manager = new StudentTagManager(this);

                return _student_tag_manager;
            }
        }
        public event EventHandler TemporalChanged;
        private void TempStudentSourceProvider_SourceChanged(object sender, EventArgs e)
        {
            if ( TemporalChanged != null )
                TemporalChanged(this, EventArgs.Empty);
        }

        public event EventHandler StudentInserted;
        public void InvokStudentInserted(EventArgs e)
        {
            if ( StudentInserted != null )
                StudentInserted.Invoke(this, e);
        }

        public event EventHandler<StudentDeletedEventArgs> StudentDeleted;
        public void InvokStudentDeleted(StudentDeletedEventArgs e)
        {
            if ( StudentDeleted != null )
                StudentDeleted.Invoke(this, e);
        }

        public event EventHandler NewUpdateRecord;
        public void InvokNewUpdateRecord(object sender, EventArgs e)
        {
            if ( NewUpdateRecord != null )
                NewUpdateRecord.Invoke(sender, e);
        }

        public event EventHandler<StudentAttendanceChangedEventArgs> AttendanceChanged;
        public void InvokAttendanceChanged(params string[] studentIDList)
        {
            if ( studentIDList.Length == 0 ) return;
            if ( AttendanceChanged != null )
            {
                BriefStudentData[] students = new BriefStudentData[studentIDList.Length];
                for ( int i = 0 ; i < studentIDList.Length ; i++ )
                {
                    students[i] = Items[studentIDList[i]];
                }
                StudentAttendanceChangedEventArgs args = new StudentAttendanceChangedEventArgs(students);
                AttendanceChanged.Invoke(this, args);
            }
        }

        public event EventHandler<StudentDisciplineChangedEventArgs> DisciplineChanged;
        public void InvokDisciplineChanged(params string[] studentIDList)
        {
            if ( studentIDList.Length == 0 ) return;
            if ( DisciplineChanged != null )
            {
                BriefStudentData[] students = new BriefStudentData[studentIDList.Length];
                for ( int i = 0 ; i < studentIDList.Length ; i++ )
                {
                    students[i] = Items[studentIDList[i]];
                }
                StudentDisciplineChangedEventArgs args = new StudentDisciplineChangedEventArgs(students);
                DisciplineChanged.Invoke(this, args);
            }
        }

        public void InvokTagDefinitionChanged(params int[] tagIDs)
        {
            if ( tagIDs.Length == 0 ) return;
            List<string> studentIDList = new List<string>();
            foreach ( BriefStudentData student in _StudentList.Values )
            {
                foreach ( int id in tagIDs )
                {
                    if ( student.Tags.ContainsKey(id) )
                    {
                        studentIDList.Add(student.ID);
                        break;
                    }
                }
            }
            //InvokBriefDataChanged(studentIDList.ToArray());
            SmartSchool.Broadcaster.Events.Items["學生/資料變更"].Invoke(studentIDList.ToArray());
        }

        void dgvStudent_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ( e.RowIndex >= 0 )
                this.ShowDetail("" + dgvDisplayList.Rows[e.RowIndex].Tag);
        }

        public List<BriefStudentData> SelectionStudents
        {
            get
            {
                return _SelectionList;
            }
        }

        //暫存學生清單
        private Dictionary<string, BriefStudentData> _StudentList=new Dictionary<string,BriefStudentData>();
        private Dictionary<string, List<BriefStudentData>> _ClassStudentList=new Dictionary<string,List<BriefStudentData>>();
        private ManualResetEvent _LoadStudent = new ManualResetEvent(true);
        private BackgroundWorker _BkwBriefDataLoader;
        private BackgroundWorker _SearchSourceLoader;
        private DataGridViewColumn colName;
        private List<BriefStudentData> _SelectionList = new List<BriefStudentData>();
        private bool _ControlInitialized = false;
        private bool _ChangSearchSource = false;
        // 取得學生資料
        private void getBriefData()
        {
            this.ShowLoading = true;
            _LoadStudent.WaitOne();
            //重新取得學生資料
            //_StudentList = null;

            if ( _BkwBriefDataLoader == null )
            {
                _BkwBriefDataLoader = new BackgroundWorker();
                _BkwBriefDataLoader.DoWork += new DoWorkEventHandler(_bkwBriefDataLoader_DoWork);
                _BkwBriefDataLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bkwBriefDataLoader_RunWorkerCompleted);
            }
            if ( !_BkwBriefDataLoader.IsBusy )
                _BkwBriefDataLoader.RunWorkerAsync();
        }
        private void _bkwBriefDataLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FilterChanged();//整理Filter，Function內會自動判斷學生資料級控制項是否皆已準備完成
            this.ShowLoading = false;
        }
        internal void SortItems()
        {
            List<BriefStudentData> studentList = new List<BriefStudentData>();
            studentList.AddRange(_StudentList.Values);
            studentList.Sort();
            _StudentList.Clear();
            foreach ( BriefStudentData var in studentList )
            {
                _StudentList.Add(var.ID, var);
            }
        }
        private void _bkwBriefDataLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            _LoadStudent.Reset();
            _StudentList.Clear();
            _ClassStudentList.Clear();
            try
            {
                List<BriefStudentData> studentList = new List<BriefStudentData>();
                //取得所有狀態為　一般、休學、延修、刪除的學生資料
                DSResponse dsrsp = QueryStudent.GetAbstractList();
                foreach ( XmlElement var in dsrsp.GetContent().GetElements("Student") )
                {
                    BriefStudentData newdata = new BriefStudentData(var);
                    studentList.Add(newdata);
                    if ( !_ClassStudentList.ContainsKey(newdata.RefClassID) )
                        _ClassStudentList.Add(newdata.RefClassID, new List<BriefStudentData>());
                    _ClassStudentList[newdata.RefClassID].Add(newdata);
                }
                studentList.Sort();
                foreach ( BriefStudentData var in studentList )
                {
                    _StudentList.Add(var.ID, var);
                }
            }
            catch
            {
            }
            _LoadStudent.Set();
        }
        protected override void FillSource(bool selectAll)
        {
            base.FillSource(selectAll);
            foreach ( DataGridViewRow row in dgvDisplayList.Rows )
            {
                BriefStudentData var = Items["" + row.Tag];
                foreach ( SmartSchool.TagManage.TagInfo tag in var.Tags )
                {
                    row.Cells[10].ToolTipText += ( row.Cells[10].ToolTipText == "" ? "" : "\n" ) + tag.FullName;
                }
            }
            Instance_TemporalChanged(null, null);
        }
        protected override object[] GetDisplaySource(string id)
        {
            BriefStudentData var = Items[id];
            Image pic = null;
            string statusD = "";
            #region 判斷在學狀態並對應成圖片
            if ( var.IsExtending )
            {
                pic = global::SmartSchool.Properties.Resources.輟學;
                pic.Tag = 2;
                statusD = "延修";
            }
            else if ( var.IsDiscontinued )
            {
                pic = global::SmartSchool.Properties.Resources.畢業;
                pic.Tag = 3;
                statusD = "輟學";
            }
            else if ( var.IsOnLeave )
            {
                pic = global::SmartSchool.Properties.Resources.休學;
                pic.Tag = 2;
                statusD = "休學";
            }
            else if ( var.IsGraduated )
            {
                pic = global::SmartSchool.Properties.Resources.離校;
                pic.Tag = 1;
                statusD = "畢業或離校";
            }
            else if ( var.IsDeleted )
            {
                pic = global::SmartSchool.Properties.Resources.刪除;
                pic.Tag = 4;
                statusD = "已刪除";
            }
            else if ( var.IsNormal )
            {
                pic = global::SmartSchool.Properties.Resources.一般;
                pic.Tag = 0;
                statusD = "在校";
            }
            else
            {
                pic = null;
                statusD = "我也不知道";
            }
            #endregion
            List<object> values = new List<object>();
            values.Add(var.ID);
            values.Add(pic);
            values.Add(var.ClassName);
            values.Add(var.SeatNo);
            values.Add(var.Name);
            values.Add(var.StudentNumber);
            values.Add(var.Gender);
            values.Add(var.IDNumber);
            values.Add(var.PermanentPhone);
            values.Add(var.ContactPhone);
            values.Add(var.Tags);
            return values.ToArray();
        }

        public void ShowDetail(string id)
        {
            PopupPalmerwormStudent.ShowPopupPalmerwormStudent(id);
            dgvDisplayList.Cursor = System.Windows.Forms.Cursors.Default;
        }

        internal void ReloadData()
        {
            getBriefData();
        }

        private void Instance_TemporalChanged(object sender, EventArgs e)
        {
            foreach ( DataGridViewRow row in dgvDisplayList.Rows )
            {
                bool intemp = TemporaSource.Contains("" + row.Tag);
                DataGridViewCell cell = row.Cells[dgvDisplayList.Columns.IndexOf(colName)];
                cell.Style.Font = new Font(dgvDisplayList.Font, intemp ? ( FontStyle.Italic | FontStyle.Underline ) : FontStyle.Regular);
            }
        }

        private void dgvStudent_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            //在學狀態
            if ( e.Column.HeaderText == "狀態" )
            {
                e.SortResult = ( (int)( (Image)e.CellValue1 ).Tag ).CompareTo((int)( (Image)e.CellValue2 ).Tag);
                e.Handled = true;
            }
            //座號
            else if ( e.Column.HeaderText == "座號" )
            {
                e.SortResult = ( int.Parse(e.CellValue1.ToString() == "" ? "0" : e.CellValue1.ToString()) ).CompareTo(int.Parse(e.CellValue2.ToString() == "" ? "0" : e.CellValue2.ToString()));
                e.Handled = true;
            }
            //班級
            else if ( e.Column.HeaderText == "班級" )
            {
                BriefStudentData s1 = Items["" + dgvDisplayList.Rows[e.RowIndex1].Tag];
                BriefStudentData s2 = Items["" + dgvDisplayList.Rows[e.RowIndex2].Tag];
                if ( s1.RefClassID == "" ) e.SortResult = -1;
                else if ( s2.RefClassID == "" ) e.SortResult = 1;
                else e.SortResult = ClassRelated.Class.Instance.Items[s1.RefClassID].CompareTo(ClassRelated.Class.Instance.Items[s2.RefClassID]);
                e.Handled = true;
            }
            return;
        }
        public override void Actived()
        {
            base.Actived();

            this.Planners.AddRange(new StudentDividerProvider(new ClassDivider()),
                new StudentDividerProvider(new CategoryDivider()),
                new StudentDividerProvider(new AttendanceDivider()),
                new StudentDividerProvider(new DisciplineDivider())
                );
        }
        public StudentCollection Items
        {
            get
            {
                _LoadStudent.WaitOne();
                return new StudentCollection(_StudentList);
            }
        }

        internal void EnsureStudent(IEnumerable<string> idlist)
        {
            _LoadStudent.WaitOne();
            _LoadStudent.Reset();
            List<string> loadList = new List<string>();
            foreach ( string id in idlist )
            {
                if ( !_StudentList.ContainsKey(id) && !loadList.Contains(id) )
                    loadList.Add(id);
            }
            if ( loadList.Count > 0 )
            {
                XmlElement[] elements = QueryStudent.GetAbstractInfo(loadList.ToArray()).GetContent().GetElements("Student");
                foreach ( XmlElement ele in elements )
                {
                    BriefStudentData newData = new BriefStudentData(ele);
                    _StudentList.Add(newData.ID, newData);
                    if ( !_ClassStudentList.ContainsKey(newData.RefClassID) )
                        _ClassStudentList.Add(newData.RefClassID, new List<BriefStudentData>());
                    _ClassStudentList[newData.RefClassID].Add(newData);
                }
                SortItems();
            }
            _LoadStudent.Set();
        }

        private void FilterChanged()
        {
            if ( _ControlInitialized && !_BkwBriefDataLoader.IsBusy )
            {
                _LoadStudent.WaitOne();
                List<string> list = new List<string>();
                foreach ( string id in _StudentList.Keys )
                {
                    if ( btn一般生.Checked && Items[id].Status == "一般" ||
                        btn休學生.Checked && Items[id].Status == "休學" ||
                        btn延修生.Checked && Items[id].Status == "延修" ||
                        btn畢業及離校生.Checked && Items[id].Status == "畢業或離校" ||
                        btn刪除.Checked && Items[id].Status == "刪除"
                        )
                    {
                        list.Add(id);
                    }
                }
                bool changed = false;
                if ( FiltratedList.Count == list.Count )
                {
                    foreach ( string id in list )
                    {
                        if ( !FiltratedList.Contains(id) )
                        {
                            changed = true;
                            break;
                        }
                    }
                }
                else
                    changed = true;
                if ( changed )
                {
                    this.FiltratedList.Clear();
                    FiltratedList.AddRange(list);
                }
            }
            else
                this.FiltratedList.Clear();
            string s = "";
            if ( btn一般生.Checked ) s += ( s == "" ? "" : "、" ) + "一般生";
            if ( btn休學生.Checked ) s += ( s == "" ? "" : "、" ) + "休學生";
            if ( btn延修生.Checked ) s += ( s == "" ? "" : "、" ) + "延修生";
            if ( btn畢業及離校生.Checked ) s += ( s == "" ? "" : "、" ) + "畢業及離校生";
            if ( btn刪除.Checked ) s += ( s == "" ? "" : "、" ) + "刪除";
            if ( s == "" ) s = "無";
            btnFilter.Text = "篩選-<b>" + (
                btn所有學生.Checked ? "所有學生" :
                btn在校學生.Checked ? "在校學生" :
                btn學籍在校學生.Checked ? "學籍在校學生" :
                "自訂" ) + "</b>";
        }

        internal void SetupSynchronization()
        {
            //同步更新資料
            //SelectionChanged += new EventHandler(On_SelectionChanged);
            SmartSchool.Broadcaster.Events.Items["學生/選取變更"].Handler += delegate
            {
                MotherForm.Instance.SetBarMessage("已選取" + this.SelectionStudents.Count + "名學生");
            };
            StudentDeleted += new EventHandler<StudentDeletedEventArgs>(Instance_StudentDeleted);
            //BriefDataChanged += new EventHandler<BriefDataChangedEventArgs>(Instance_BriefDataChanged);
            #region 更新快取資料
            SmartSchool.Broadcaster.Events.Items["學生/資料變更"].AddBeforeInvokeHandler(delegate(object sender, SmartSchool.Broadcaster.EventArguments e)
            {
                if ( e.Items.Length == 0 ) return;
                string[] studentIdList = new string[e.Items.Length];
                for ( int i = 0 ; i < e.Items.Length ; i++ )
                {
                    studentIdList[i] = "" + e.Items[i];
                }
                //取得SERVER上最新資料
                XmlElement[] elements = QueryStudent.GetAbstractInfo(studentIdList).GetContent().GetElements("Student");
                _LoadStudent.WaitOne();
                _LoadStudent.Reset();
                foreach ( XmlElement ele in elements )
                {
                    BriefStudentData newData = new BriefStudentData(ele);
                    if ( _StudentList.ContainsKey(newData.ID) )
                    {
                        _ClassStudentList[_StudentList[newData.ID].RefClassID].Remove(_StudentList[newData.ID]);
                        _StudentList[newData.ID] = newData;
                    }
                    else
                        _StudentList.Add(newData.ID, newData);
                    if ( !_ClassStudentList.ContainsKey(newData.RefClassID) )
                        _ClassStudentList.Add(newData.RefClassID, new List<BriefStudentData>());
                    _ClassStudentList[newData.RefClassID].Add(newData);
                }
                    SortItems();
                _LoadStudent.Set();
                    Reload();
                    //FillSource(false);
                    RloadDisplayData();
            });
            #endregion
            StudentInserted += new EventHandler(Instance_StudentInserted);
            Class.Instance.ClassDeleted += new EventHandler<DeleteClassEventArgs>(Instance_ClassDeleted);
            Class.Instance.ClassInserted += new EventHandler<InsertClassEventArgs>(Instance_ClassInserted);
            Class.Instance.ClassUpdated += new EventHandler<UpdateClassEventArgs>(Instance_ClassUpdated);
        }
        bool changing = false;
        private void filterAllStudent_Click(object sender, EventArgs e)
        {
            btnFilter.Expanded = true;
            if ( !changing && btn所有學生.Checked )
            {
                changing = true;
                btn一般生.Checked = true;
                btn休學生.Checked = true;
                btn延修生.Checked = true;
                btn畢業及離校生.Checked = true;
                btn刪除.Checked = true;
                btn在校學生.Checked = false;
                btn學籍在校學生.Checked = false;
                //FilterChanged();
                changing = false;
            }
        }

        private void btn在校學生_Click(object sender, EventArgs e)
        {
            btnFilter.Expanded = true;
            if ( !changing && btn在校學生.Checked )
            {
                changing = true;
                btn一般生.Checked = true;
                btn休學生.Checked = false;
                btn延修生.Checked = true;
                btn畢業及離校生.Checked = false;
                btn刪除.Checked = false;
                btn所有學生.Checked = false;
                btn學籍在校學生.Checked = false;
                //FilterChanged();
                changing = false;
            }
        }

        private void btn學籍在校學生_Click(object sender, EventArgs e)
        {
            btnFilter.Expanded = true;
            if ( !changing && btn學籍在校學生.Checked )
            {
                changing = true;
                btn一般生.Checked = true;
                btn休學生.Checked = true;
                btn延修生.Checked = true;
                btn畢業及離校生.Checked = false;
                btn刪除.Checked = false;
                btn所有學生.Checked = false;
                btn在校學生.Checked = false;
                //FilterChanged();
                changing = false;
            }
        }

        private void statusFilterChanged(object sender, EventArgs e)
        {
            btnFilter.Expanded = true;
            if ( !changing )
            {
                changing = true;
                btn學籍在校學生.Checked = ( btn一般生.Checked == true && btn休學生.Checked == true && btn延修生.Checked == true && btn畢業及離校生.Checked == false && btn刪除.Checked == false );
                btn在校學生.Checked = ( btn一般生.Checked == true && btn休學生.Checked == false && btn延修生.Checked == true && btn畢業及離校生.Checked == false && btn刪除.Checked == false );
                btn所有學生.Checked = ( btn一般生.Checked == true && btn休學生.Checked == true && btn延修生.Checked == true && btn畢業及離校生.Checked == true && btn刪除.Checked == true );
                //FilterChanged();
                changing = false;
            }
        }

        private void btnFilter_PopupClose(object sender, EventArgs e)
        {
            FilterChanged();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string key = txtSearch.Text;
            List<string> list = new List<string>();
            if ( key != "" )
            {
                Regex rx = new Regex(
                    key.Replace("*", ".*").Replace(@"\", @"\\").Replace(@"[", @"\[").Replace(@"(", @"\(").Replace(@"]", @"\]").Replace(@")", @"\)").Replace(@"?", @"\?").Replace(@"+", @"\+")
                    );
                foreach ( string id in FiltratedList )
                {
                    BriefStudentData var = Items[id];
                    if ( chkSearchInName.Checked && rx.IsMatch(var.Name) )
                    {
                        list.Add(id);
                        continue;
                    }
                    if ( chkSearchInStudentId.Checked && rx.IsMatch(var.StudentNumber) )
                    {
                        list.Add(id);
                        continue;
                    }
                    if ( chkSearchInSSN.Checked && rx.IsMatch(var.IDNumber) )
                    {
                        list.Add(id);
                        continue;
                    }
                }
            }
            DisplaySource.Clear();
            DisplaySource.AddRange(list);
            FillSource(list.Count == 1);
        }

        private void buttonExpand_Click(object sender, EventArgs e)
        {
            //palmerwormStudent1.Visible=
        }

        private void splitterListDetial_ExpandedChanged(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            #region 處理毛毛蟲
            //清單模式不用處理
            if ( splitterListDetial.Expanded )
            {
                if ( SelectedSource.Count > 0 && ( palmerwormStudent1.StudentInfo == null || !SelectedSource.Contains(palmerwormStudent1.StudentInfo.ID) ) )
                {
                    palmerwormStudent1.StudentInfo = this.Items[SelectedSource[0]];
                    palmerwormStudent1.Visible = true;
                }
                else
                {
                    palmerwormStudent1.StudentInfo = null;
                    palmerwormStudent1.Visible = false;
                }
            }
            #endregion
        }

        private void chkSearchInName_CheckedChanged(object sender, DevComponents.DotNetBar.CheckBoxChangeEventArgs e)
        {
            _ChangSearchSource = true;
        }
    }
}

