using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using SmartSchool.StudentRelated;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature;
using System.Xml;
using System.IO;
using DevComponents.DotNetBar;

namespace SmartSchool.StudentRelated.RibbonBars.GMap
{
    public partial class GMapForm : BaseForm
    {
        private List<BriefStudentData> _temp_students;
        private AddressType _address_type;

        public GMapForm(List<BriefStudentData> students, AddressType addressType)
        {
            InitializeComponent();

            _address_type = addressType;
            _temp_students = students;
            _students = new StudentInfoCollection();
        }

        private void GMapForm_Load(object sender, EventArgs e)
        {
            GMapLoading = true;
            AddressLoading = true;

            _helper = new GMapHelper(webBrowser1);
            GMap.GMapComplete += new EventHandler(helper_GMapComplete);
            GMap.InformationRequest += new InfoRequest(GMap_InformationRequest);
            GMap.ObjectClick += new ClickNotification(GMap_ObjectClick);
            GMap.GMapMoved += new EventHandler(GMap_GMapMoved);
            GMap.LoadGMap();
        }

        private void GMap_GMapMoved(object sender, EventArgs e)
        {
            foreach (DataGridViewRow eachRow in dataGridViewX1.Rows)
            {
                StudentInfo student = eachRow.Tag as StudentInfo;
                if (student != null)
                {
                    if (student.MapMark != null)
                    {
                        if (student.MapMark.MarkObject != null)
                        {
                            if (GMap.IsViewable(student.MapMark.MarkObject))
                            {
                                SetCellForeColor(eachRow, Color.Blue);
                            }
                            else
                                SetCellForeColor(eachRow, Color.Black);
                        }
                    }
                }
            }
        }

        private void helper_GMapComplete(object sender, EventArgs e)
        {
            AdjustMapSize();

            if (_temp_students.Count <= 0) return;

            try
            {
                Dictionary<string, BriefStudentData> dicStudents = new Dictionary<string, BriefStudentData>();

                foreach (BriefStudentData eachStudent in _temp_students)
                    dicStudents.Add(eachStudent.ID, eachStudent);

                List<string> strStudents = new List<string>(dicStudents.Keys);
                DSResponse objrsp = QueryStudent.GetAddressWithPhoto(strStudents.ToArray());

                //補足學生資料。
                foreach (XmlElement eachStudent in objrsp.GetContent().GetElements("Student"))
                {
                    string studentId = eachStudent.GetAttribute("ID");
                    StudentInfo student = new StudentInfo(dicStudents[studentId], eachStudent);
                    Students.AddStudent(student);
                }

                string male = Path.Combine(Application.StartupPath, "Temporal/Male.png");
                string female = Path.Combine(Application.StartupPath, "Temporal/FeMale.png");
                CreateGenderImage(male, female);

                //開始顯示座標。
                decimal lat = 0, lng = 0, count = 0;
                foreach (StudentInfo eachStudent in Students.Values)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    BriefStudentData objStudent = eachStudent.BaseInfo;

                    row.CreateCells(dataGridViewX1, objStudent.ClassName, objStudent.SeatNo, objStudent.Name);
                    row.Tag = eachStudent;

                    if (eachStudent.Addresses.ContainsType(_address_type))
                    {
                        AddressInfo address = eachStudent.Addresses[_address_type];

                        if (address.HasCoordinate)
                        {
                            //SetCellColor(row,row.DefaultCellStyle.BackColor);
                            AddressMark mark = GMap.CreateMark(address);
                            eachStudent.MapMark = mark;

                            if (objStudent.Gender == "男")
                                GMap.SetMarkImage(mark.MarkObject, male);
                            else if (objStudent.Gender == "女")
                                GMap.SetMarkImage(mark.MarkObject, female);

                            decimal tLat, tLng;
                            if (decimal.TryParse(address.Lat, out tLat) && decimal.TryParse(address.Lng, out tLng))
                            {
                                lat += tLat;
                                lng += tLng;
                                count++;
                            }
                        }
                        else
                            SetCellColor(row, Color.Silver);
                    }
                    else
                    {
                        SetCellColor(row, Color.Silver);
                        SetCellForeColor(row, Color.Gray);
                    }

                    dataGridViewX1.Rows.Add(row);
                }
                dataGridViewX1.Sort(chSeatNo, ListSortDirection.Ascending);

                if (lat == 0 || lng == 0 || count == 0) { }
                else
                    GMap.PanToPoint((lat / count).ToString(), (lng / count).ToString());

                //flpStatistics
                flpStatistics.Controls.Clear();
                Dictionary<string, StatisticsItem> items = new Dictionary<string, StatisticsItem>();
                StatisticsItem otherItem = new StatisticsItem("其他", "");
                otherItem.Size = new Size(160, 20);
                foreach (StudentInfo eachStudent in Students.Values)
                {
                    if (eachStudent.Addresses.ContainsType(_address_type))
                    {
                        AddressInfo address = eachStudent.Addresses[_address_type];
                        string key = address.County + address.Town;

                        if (items.ContainsKey(key))
                            items[key].AddCount();
                        else
                        {
                            StatisticsItem item = new StatisticsItem(address.County, address.Town);
                            item.Size = new Size(160, 20);
                            item.AddCount();
                            items.Add(key, item);
                        }
                    }
                    else
                        otherItem.AddCount();
                }

                foreach (StatisticsItem eachItem in items.Values)
                {
                    eachItem.Populate();
                    flpStatistics.Controls.Add(eachItem);
                }
                if (otherItem.Count > 0)
                {
                    otherItem.Populate();
                    flpStatistics.Controls.Add(otherItem);
                }

                GMapLoading = false;
                AddressLoading = false;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
        }

        private void CreateGenderImage(string male, string female)
        {
            //Stream maleStream = GetType().Assembly.GetManifestResourceStream("SmartPlugIn.Resources.MapMale.png");
            //Stream femaleStream = GetType().Assembly.GetManifestResourceStream("SmartPlugIn.Resources.MapFemale.png");
            //SaveStreamToFile(maleStream, male);
            //SaveStreamToFile(femaleStream, female);
            
            FileInfo objFile = new FileInfo(male);
            if (!objFile.Directory.Exists) objFile.Directory.Create();

            Properties.Resources.MapMale.Save(male);
            Properties.Resources.MapFemale.Save(female);
        }

        private static void SetCellColor(DataGridViewRow row, Color color)
        {
            row.DefaultCellStyle.BackColor = color;
        }

        private static void SetCellForeColor(DataGridViewRow row, Color color)
        {
            row.DefaultCellStyle.ForeColor = color;
        }

        /// <param name="param">學生編號。</param>
        private string GMap_InformationRequest(string param)
        {
            return new StudentInfoWindow(_students[param]).GetHtml();
        }

        private void GMap_ObjectClick(string token, object args)
        {
            if (token == "ShowStudentContentPane")
                SmartSchool.StudentRelated.Student.Instance.ShowDetail(args.ToString());
        }

        private static void SaveStreamToFile(Stream stream, string file)
        {
            FileInfo objFile = new FileInfo(file);
            if (!objFile.Directory.Exists) objFile.Directory.Create();
            Stream newFileStream = new FileStream(file, FileMode.Create);

            byte[] byteContent = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(byteContent, 0, byteContent.Length);

            newFileStream.Write(byteContent, 0, byteContent.Length);
            newFileStream.Close();
        }

        #region Reisze Adjust Map

        private bool _size_changing = false;
        private void GMapForm_ResizeBegin(object sender, EventArgs e)
        {
            _size_changing = true;
        }

        private void GMapForm_ResizeEnd(object sender, EventArgs e)
        {
            _size_changing = false;
            AdjustMapSize();
        }

        private void GMapForm_SizeChanged(object sender, EventArgs e)
        {
            if (!_size_changing)
                AdjustMapSize();
        }

        private void AdjustMapSize()
        {
            if (GMap != null)
                GMap.SetMapSize(webBrowser1.Height, webBrowser1.Width);
        }

        #endregion

        private void dataGridViewX1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewX1.SelectedRows[0];

                StudentInfo student = row.Tag as StudentInfo;
                if (student != null)
                {
                    if (student.MapMark != null)
                    {
                        student.MapMark.PanToCenter();
                        student.MapMark.OpenInfoWindow(new StudentInfoWindow(student).GetHtml());
                    }
                }
            }
        }

        private GMapHelper _helper;
        public GMapHelper GMap
        {
            get { return _helper; }
        }

        private StudentInfoCollection _students;
        public StudentInfoCollection Students
        {
            get { return _students; }
        }

        public bool GMapLoading
        {
            get { return lblGMapLoading.Visible; }
            set
            {
                lblGMapLoading.Visible = value;

                if (lblGMapLoading.Visible)
                {
                    Size rel = new Size(webBrowser1.Location);
                    Point newLocation = GetCenter(webBrowser1.Size, lblGMapLoading.Size);
                    lblGMapLoading.Location = Point.Add(newLocation, rel);
                }
            }
        }

        private bool AddressLoading
        {
            get { return picAddressLoading.Visible; }
            set
            {
                picAddressLoading.Visible = value;

                if (picAddressLoading.Visible)
                    picAddressLoading.Location = GetCenter(dataGridViewX1.Size, picAddressLoading.Size);
            }
        }

        private Point GetCenter(Size container, Size target)
        {
            Size newSize = Size.Subtract(container, target);
            return new Point(newSize.Width / 2, newSize.Height / 2);
        }

        private class StudentInfoWindow
        {
            private StudentInfo _student;

            public StudentInfoWindow(StudentInfo student)
            {
                _student = student;
            }

            public string GetHtml()
            {
                //Stream stream = GetType().Assembly.GetManifestResourceStream("SmartPlugIn.Student.GMap.InfoWindow.htm");
                Stream stream = new MemoryStream(Properties.Resources.InfoWindow);
                StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("big5"));
                string template = reader.ReadToEnd();
                reader.Close();

                string className = _student.BaseInfo.ClassName;
                string seatNo = _student.BaseInfo.SeatNo;
                string name = _student.BaseInfo.Name;
                string address = _student.MapMark.Address.Full();
                string photoPath = CreatePhoto();

                return string.Format(template, photoPath, className, seatNo, _student.Identity, name, address);
            }

            private string CreatePhoto()
            {
                string photoPath = Path.Combine(Application.StartupPath, "Temporal");
                photoPath = photoPath + string.Format("\\{0}.jpg", _student.Identity);

                try
                {
                    SaveStreamToFile(photoPath);
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message);
                    CurrentUser.ReportError(ex);
                }

                return photoPath;
            }

            private void SaveStreamToFile(string photoPath)
            {

                if (string.IsNullOrEmpty(_student.Photo))
                    Properties.Resources.studentsPic.Save(photoPath);
                else
                {
                    FileInfo photoFile = new FileInfo(photoPath);
                    if (!photoFile.Directory.Exists) photoFile.Directory.Create();
                    Stream photoStream = new FileStream(photoPath, FileMode.Create);
                    byte[] photoContent;

                    photoContent = Convert.FromBase64String(_student.Photo);

                    photoStream.Write(photoContent, 0, photoContent.Length);
                    photoStream.Close();
                }
            }

            //private byte[] ReadEmptyStudentPhoto()
            //{
            //    byte[] photoContent;
            //    //Stream emptyStudent = GetType().Assembly.GetManifestResourceStream("SmartPlugIn.Resources.EmptyStudent.jpg");
            //    Stream emptyStudent = new MemoryStream();
            //    Properties.Resources.EmptyStudent.RawFormat.Save(emptyStudent,System.Drawing.Imaging.ImageFormat.Png);
            //    photoContent = new byte[emptyStudent.Length];
            //    emptyStudent.Read(photoContent, 0, photoContent.Length);
            //    return photoContent;
            //}
        }

        private class StatisticsItem : LabelX
        {
            public StatisticsItem(string county, string town)
            {
                _county = county;
                _town = town;
            }

            private string _county;
            public string County
            {
                get { return _county; }
            }

            private string _town;
            public string Town
            {
                get { return _town; }
            }

            private int _count = 0;
            public int Count
            {
                get { return _count; }
            }

            public void AddCount()
            {
                _count++;
            }

            public void Populate()
            {
                Text = string.Format("{0}{1}  ({2}人)", County, Town, Count);
            }
        }

        private void dataGridViewX1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Name == "chSeatNo")
            {
                int s1, s2;

                if (!int.TryParse(e.CellValue1.ToString(), out s1))
                    s1 = int.MinValue;

                if (!int.TryParse(e.CellValue2.ToString(), out s2))
                    s2 = int.MinValue;

                e.SortResult = s1.CompareTo(s2);
                e.Handled = true;
            }
        }
    }
}