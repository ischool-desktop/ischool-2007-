using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using IntelliSchool.DSA30.Util;
using System.Xml;
using System.IO;
using System.Runtime.CompilerServices;
using SmartSchool.Common;

namespace SmartSchool.StudentRelated.SmallCard
{
    internal partial class StudentCardPanel : CardPanelEx
    {
        private Dictionary<string, PhotoStudentCard> _ShownList;
        private Dictionary<string, Image> _ImageList;
        private List<BriefStudentData> _DisplayStudents;
        private List<PhotoStudentCard> _CardList;
        private Timer _BatchLoader;
        private bool _TimerRunning;
        private Queue<string> _LoadList;
        private BackgroundWorker _BatchLoaderProvider;
        //private List<BriefStudentData> _LockedStudents;
        private void _BatchLoaderProvider_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = SmartSchool.Feature.QueryStudent.GetDetailList(new string[] { "ID", "FreshmanPhoto", "GraduatePhoto" }, ((List<string>)e.Argument).ToArray());
        }
        private void _BatchLoaderProvider_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DSResponse dsrsp = (DSResponse)e.Result;
            foreach (XmlElement var in dsrsp.GetContent().GetElements("Student"))
            {
                string id = var.SelectSingleNode("@ID").InnerText;
                string FreshmanPhotostring = var.SelectSingleNode("FreshmanPhoto").InnerText;
                string GraduatePhotostring = var.SelectSingleNode("GraduatePhoto").InnerText;
                Image lastImage=null;
                byte[] bs=null;
                if (GraduatePhotostring != "")
                {
                    bs = Convert.FromBase64String(GraduatePhotostring);
                }
                else if (FreshmanPhotostring!="")
                {
                    bs = Convert.FromBase64String(GraduatePhotostring);
                }
                if (bs != null)
                {
                    try
                    {
                    MemoryStream ms = new MemoryStream(bs);
                    
                        lastImage = Bitmap.FromStream(ms);
                    }
                    catch (Exception)
                    {
                        lastImage = null;
                    }
                }
                //if (lastImage != null)
                {
                    if (_ImageList.ContainsKey(id))
                        _ImageList[id] = lastImage;
                    else
                        _ImageList.Add(id, lastImage);
                    if (_ShownList.ContainsKey(id))
                    {
                        _ShownList[id].Photo = lastImage;
                    }
                }
            }
        }
        private void _BatchLoader_Tick(object sender, EventArgs e)
        {
            if (_LoadList.Count == 0)
            {
                _BatchLoader.Stop();
                _TimerRunning = false;
            }
            else
            {
                if (!_BatchLoaderProvider.IsBusy)
                {
                    List<string> idList = new List<string>();                    
                    for (int i =  _LoadList.Count<50?_LoadList.Count:50; i >0  ; i--)
                    {
                        idList.Add(_LoadList.Dequeue());
                    }
                    _BatchLoaderProvider.RunWorkerAsync(idList);
                }
            }
        }
        private void CatchMouseHover(object sender, EventArgs e)
        {
            if (!this.ContainsFocus)
                ((Control)sender).Focus();
        }
        private string[] _Colors = new string[] { "#4B0F00", "#001EB4", "#A50F78" };
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Student.Instance.UnSelectStudent(_DisplayStudents.ToArray());
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //Student.Instance.BatchMode = false;
        }

        public StudentCardPanel()
        {
            InitializeComponent();
            _CardList = new List<PhotoStudentCard>();
            _ShownList = new Dictionary<string, PhotoStudentCard>();
            _ImageList = new Dictionary<string, Image>();
            _LoadList = new Queue<string>();
            _BatchLoaderProvider = new BackgroundWorker();
            _BatchLoaderProvider.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BatchLoaderProvider_RunWorkerCompleted);
            _BatchLoaderProvider.DoWork += new DoWorkEventHandler(_BatchLoaderProvider_DoWork);
            _BatchLoader = new Timer();            
            _BatchLoader.Interval = 500;
            _BatchLoader.Tick += new EventHandler(_BatchLoader_Tick);
            _TimerRunning = false;
        }      
        public void ShowList(List<BriefStudentData> list)
        {
            toolStripMenuItem2.Enabled = (list.Count <= 1);
            _DisplayStudents = list;
            this.SuspendLayout();
            _ShownList = new Dictionary<string, PhotoStudentCard>();
            //有清單則同步顯示沒有則全部不顯示
            if (_DisplayStudents != null)
            {
                #region 當已建立的PhotoStudentCard數量不足時補足
                List<PhotoStudentCard> addCards = new List<PhotoStudentCard>();
                for (int m = _DisplayStudents.Count-_CardList.Count; m >0; m--)
                {                                   
                    PhotoStudentCard Card;
                    //建立新卡片加入到cardPanelEx1及_CardList
                    Card = new PhotoStudentCard();
                    Card.MouseHover+= new EventHandler(CatchMouseHover);
                    Card.Visible = true;
                    addCards.Add(Card);
                    //this.Controls.Add(Card);
                    _CardList.Add(Card);
                }
                if (addCards.Count > 0)
                    this.Controls.AddRange(addCards.ToArray());
                #endregion
                #region 以選取學生的班級作群組
                Dictionary<string, List<BriefStudentData>> classdictionary = new Dictionary<string, List<BriefStudentData>>();
                foreach (BriefStudentData var in list)
                {
                    if (!classdictionary.ContainsKey(var.ClassName))
                    {
                        classdictionary.Add(var.ClassName, new List<BriefStudentData>());
                    }
                    classdictionary[var.ClassName].Add(var);
                }
                #endregion
                #region 同步顯示資料
                int i = 0;
                int j = 0;
                foreach (string  key in classdictionary.Keys)
                {
                    foreach (BriefStudentData student in classdictionary[key])
                    {
                        PhotoStudentCard Card = _CardList[i];
                        Card.Class = student.ClassName;
                        Card.SeatNo = student.SeatNo;
                        Card.StudentID = student.StudentNumber;
                        Card.StudentName = student.Name;
                        Card.Visible = true;
                        Card.Student = student;
                        Card.ColorNumber=_Colors[(j%_Colors.Length)];
                        Card.SetText();
                        #region 已載入照片則顯示否則載入照片
                        if (_ImageList.ContainsKey(student.ID))
                        {
                            if (_ImageList[student.ID] != null)
                            {
                                Card.Photo = _ImageList[student.ID];
                            }
                        }
                        else
                        {
                            Card.Photo = null;
                            _LoadList.Enqueue(student.ID);
                            if (!_TimerRunning)
                            {
                                _TimerRunning = true;
                                _BatchLoader.Start();
                            }
                        }
                        #endregion
                        _ShownList.Add(student.ID, Card);
                        i++;
                    }
                    j++;
                }
                //隱藏多餘的CARD
                for (int q = _CardList.Count-1; i <= q; q--)
                {
                    _CardList[q].Visible = false;
                }
                #endregion
            }
            else
            {
                #region 隱藏所有的小卡片
                foreach (PhotoStudentCard var in _CardList)
                {
                    var.Visible = false;
                } 
                #endregion
            }
            this.ResumeLayout(true);
        }
    }
}
