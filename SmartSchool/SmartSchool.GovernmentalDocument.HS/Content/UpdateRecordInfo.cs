using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using System.Xml;
using IntelliSchool.DSA30.Util;
using System.Threading;
using DevComponents.DotNetBar.Controls;
using SmartSchool.Feature;
using DevComponents.DotNetBar;
using SmartSchool.ApplicationLog;
using SmartSchool.Customization.Data;

namespace SmartSchool.GovernmentalDocument.Content
{
    public enum UpdateRecordType { 學籍異動, 新生異動, 轉入異動, 畢業異動 }
    public enum UpdateRecordAction { Insert, Update, None }
    public partial class UpdateRecordInfo : UserControl
    {
        private List<string> _DateFields = new List<string>(new string[] { "ADDate", "UpdateDate", "Birthdate", "LastADDate", "PreviousSchoolLastADDate" });

        private List<string> _NonNullFields = new List<string>(new string[] { "UpdateDate", "UpdateCode" });

        private Dictionary<string, List<Control>> _ControlDictionary = new Dictionary<string, List<Control>>();

        private List<Control> _DepartmentControls = new List<Control>();

        private List<Control> _UpdateCodeControls = new List<Control>();

        private ManualResetEvent _UpdateCodeLoadedEvent = new ManualResetEvent(true);

        private ManualResetEvent _UpdateCodeEditingEvent = new ManualResetEvent(true);

        private BackgroundWorker _BWDepartmentCode, _BWUpdateCode;

        private AccessHelper _AccessHelper = new AccessHelper();

        private string _StudentID;

        private string _UpdateRecordID;

        private UpdateRecordType _Style;

        private UpdateRecordAction _Action;

        private Dictionary<Control, ErrorProvider> _errorProviderDictionary = new Dictionary<Control, ErrorProvider>();

        private void SetErrorProvider(Control control, string p, bool isError)
        {
            if (_ErrorControls.Contains(control))
            {
                _ErrorControls.Remove(control);
            }
            if (!_errorProviderDictionary.ContainsKey(control))
            {
                ErrorProvider ep = new ErrorProvider();
                ep.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                ep.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                if (isError)
                {
                    ep.Icon = Properties.Resources.error;
                }
                else
                {
                    ep.Icon = Properties.Resources.warning;
                }
                ep.SetError(control, p);
                _errorProviderDictionary.Add(control, ep);
            }
            if (isError)
            {
                _ErrorControls.Add(control);
            }
        }

        private void ResetErrorProvider(Control control)
        {
            if (_errorProviderDictionary.ContainsKey(control))
            {
                _errorProviderDictionary[control].Clear();
                _errorProviderDictionary.Remove(control);
            }
            if (_ErrorControls.Contains(control))
            {
                _ErrorControls.Remove(control);
            }
        }

        private List<string> _DepartmentList = new List<string>();

        private string[] _AllowedUpdateCode;

        private Dictionary<string, Dictionary<string, string>> _UpdateCodeSynopsis = new Dictionary<string, Dictionary<string, string>>();

        private void Control_TextChanged(object sender, EventArgs e)
        {
            if (_StopEvent) return;
            Control control = (Control)sender;
            //把所有相同意函欄未設成相同值
            if (control.Tag != null)
            {
                SetValue(control.Tag.ToString(), control.Text, !control.Focused);
                if (DataChanged != null)
                {
                    DataChanged.Invoke(this, new EventArgs());
                }
            }
        }

        private bool CheckIsDate(string text)
        {
            if (text.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries).Length != 3)
                return false;
            DateTime d = DateTime.Now;
            return DateTime.TryParse(text, out  d);
        }

        private bool _StopEvent = false;

        private Dictionary<string, string> GetUpdateCodeSynopsis(string style)
        {
            _UpdateCodeEditingEvent.WaitOne();
            Dictionary<string, string> ucsdic = new Dictionary<string, string>();
            if (_UpdateCodeSynopsis.ContainsKey(style))
            {
                if (_AllowedUpdateCode == null || _AllowedUpdateCode.Length == 0)
                {
                    return _UpdateCodeSynopsis[style];
                }
                else
                {
                    Dictionary<string, string> items = _UpdateCodeSynopsis[style];
                    string value = "";
                    foreach (string key in _AllowedUpdateCode)
                    {
                        if (items.TryGetValue(key, out value))
                        {
                            ucsdic.Add(key, value);
                        }
                    }
                    return ucsdic;
                }
            }
            else
                return ucsdic;
        }

        private void ValueValidate(Control control, bool showError)
        {
            #region 如果是異動代號則檢查輸入代號是否在清單中
            if (control.Tag != null && control.Tag.ToString() == "UpdateCode")
            {
                if (GetUpdateCodeSynopsis(_Style.ToString()).Count != 0 && !GetUpdateCodeSynopsis(_Style.ToString()).ContainsKey(control.Text))
                {
                    if (showError)
                        SetErrorProvider(control, "輸入的代號不在指定的代號清單中。", true);
                    return;
                }
                else
                {
                    ResetErrorProvider(control);
                }
            }
            #endregion
            #region 如果是日期欄位檢查輸入值
            if (_DateFields.Contains(control.Tag.ToString()))
            {
                if (control.Text == "" && _NonNullFields.Contains(control.Tag.ToString()))
                {
                    if (showError)
                        SetErrorProvider(control, "此欄為必填欄位，請輸入西元年/月/日。", true); return;
                }
                else
                {
                    if (control.Text != "")
                    {
                        //檢查欄位值
                        if (!CheckIsDate(control.Text))
                        {
                            if (_NonNullFields.Contains(control.Tag.ToString()))
                            {
                                if (showError)
                                    SetErrorProvider(control, "此欄為必填欄位，\n請依照\"西元年/月/日\"格式輸入。", true);
                            }
                            else
                            {
                                if (showError)
                                    SetErrorProvider(control, "輸入格式錯誤，請輸入西元年/月/日。\n此筆錯誤資料將不會被儲存", false);
                            }
                            return;
                        }
                        else
                        {
                            ResetErrorProvider(control);
                        }
                    }
                }
            }
            #endregion
            #region 如果是必填欄位檢查非空值
            if (_NonNullFields.Contains(control.Tag.ToString()) && control.Text == "")
            {
                if (showError)
                    SetErrorProvider(control, "此欄位必須填寫，不允許空值", true);
                return;
            }
            else
            {
                ResetErrorProvider(control);
            }
            #endregion
            #region 如果是年級則檢查輸入資料
            if (control.Tag != null && control.Tag.ToString() == "GradeYear")
            {
                int i = 0;
                if (control.Text != "延修生" && (!int.TryParse(control.Text, out i) || i <= 0))
                {
                    if (showError)
                        SetErrorProvider(control, "年級欄必需依以下格式填寫：\n\t1.若為一般學生請填入學生年級。\n\t2.若為延修生請填入\"延修生\"", true);
                    return;
                }
                else
                {
                    ResetErrorProvider(control);
                }
            }
            #endregion
        }

        private List<Control> _ErrorControls = new List<Control>();

        private void _BWDepartmentCode_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                foreach (XmlElement var in (XmlElement[])e.Result)
                {
                    _DepartmentList.Add(var.SelectSingleNode("Name").InnerText);
                }
                foreach (Control var in _DepartmentControls)
                {
                    DropDownDepartment(var, new EventArgs());
                }
            }
        }

        private void _BWDepartmentCode_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = SmartSchool.Feature.Basic.Config.GetDepartment().GetContent().GetElements("Department");
        }

        //NewData
        private NewDataField _newDataField = null;
        public string NewData { get { return GetValue("NewData"); } set { SetValue("NewData", value); } }
        //public bool NewDataVisible { get { return _newDataField.Visible; } set { _newDataField.Visible = value; } }

        //記錄Log所需要用到的變數
        private Dictionary<string, string> beforeData = new Dictionary<string, string>();
        private Dictionary<string, string> afterData = new Dictionary<string, string>();

        //英漢字典，用來查詢每個欄位的中文名稱
        private Dictionary<string, string> _EnChDict = new Dictionary<string, string>();

        //初始化英漢字典
        private void InitialDict()
        {
            _EnChDict.Add("Department", "科別");
            _EnChDict.Add("ADDate", "核準日期");
            _EnChDict.Add("ADNumber", "核準文號");
            _EnChDict.Add("UpdateDate", "異動日期");
            _EnChDict.Add("UpdateCode", "異動代號");
            _EnChDict.Add("UpdateDescription", "原因及事項");
            _EnChDict.Add("Name", "姓名");
            _EnChDict.Add("StudentNumber", "學號");
            _EnChDict.Add("Gender", "性別");
            _EnChDict.Add("IDNumber", "身份證號");
            _EnChDict.Add("Birthdate", "生日");
            _EnChDict.Add("GradeYear", "年級");
            _EnChDict.Add("LastADDate", "備查日期");
            _EnChDict.Add("LastADNumber", "備查文號");
            _EnChDict.Add("Comment", "備註");
            _EnChDict.Add("LastUpdateCode", "最後異動代號");
            _EnChDict.Add("NewStudentNumber", "新學號");
            _EnChDict.Add("PreviousSchool", "轉入前學生資料-學校");
            _EnChDict.Add("PreviousStudentNumber", "轉入前學生資料-學號");
            _EnChDict.Add("PreviousDepartment", "轉入前學生資料-科別");
            _EnChDict.Add("PreviousSchoolLastADDate", "轉入前學生資料-備查日期");
            _EnChDict.Add("PreviousSchoolLastADNumber", "轉入前學生資料-備查文號");
            _EnChDict.Add("PreviousGradeYear", "轉入前學生資料-年級");
            _EnChDict.Add("GraduateSchoolLocationCode", "入學資格-畢業國中所在地代碼");
            _EnChDict.Add("GraduateSchool", "入學資格-畢業國中");
            _EnChDict.Add("GraduateCertificateNumber", "畢(結)業證書字號");
            _EnChDict.Add("NewData", "更正後資料");
        }

        //用Control的Tag判定欄位
        public void SetValue(string tagName, string value) { SetValue(tagName, value, true); }
        public void SetValue(string tagName, string value, bool showError)
        {
            _StopEvent = true;
            //如果是異動代號欄位則查詢對照表更新UpdateDescription 
            if (tagName == "UpdateCode")
            {
                if (GetUpdateCodeSynopsis(_Style.ToString()).ContainsKey(value) && GetValue("UpdateDescription") != GetUpdateCodeSynopsis(_Style.ToString())[value])
                {
                    SetValue("UpdateDescription", GetUpdateCodeSynopsis(_Style.ToString())[value]);
                    _newDataField.ChangeNewDataText(value);
                }
            }
            //更新相同tagName控制項的值
            if (_ControlDictionary.ContainsKey(tagName))
            {
                foreach (Control var in _ControlDictionary[tagName])
                {
                    var.Text = value;
                    ValueValidate(var, showError);
                }
            }
            _StopEvent = false;
        }

        //用Control的Tag判定欄位
        public string GetValue(string tagName)
        {
            if (_ControlDictionary.ContainsKey(tagName))
            {
                string text = _ControlDictionary[tagName][0].Text;
                //如果是日期欄位且不符合日期格式則傳回空字串
                if (_DateFields.Contains(tagName) && !CheckIsDate(text))
                    return "";
                else
                    return text;
            }
            else
                return "";
        }

        public UpdateRecordInfo()
        {
            _BWUpdateCode = new BackgroundWorker();
            _BWUpdateCode.DoWork += new DoWorkEventHandler(_BWUpdateCode_DoWork);
            _BWUpdateCode.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BWUpdateCode_RunWorkerCompleted);

            _UpdateCodeLoadedEvent.Reset();
            _BWUpdateCode.RunWorkerAsync();

            _BWDepartmentCode = new BackgroundWorker();
            _BWDepartmentCode.DoWork += new DoWorkEventHandler(_BWDepartmentCode_DoWork);
            _BWDepartmentCode.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BWDepartmentCode_RunWorkerCompleted);
            _BWDepartmentCode.RunWorkerAsync();
            InitializeComponent();
            SetupControl(this);

            InitialDict();
            _newDataField = new NewDataField(lblNewData, txtNewData);
        }

        private void _BWUpdateCode_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (Control var in _UpdateCodeControls)
            {
                DropDownUpdateCode(var, ((ComboBoxEx)var).DroppedDown);
            }
        }

        private void _BWUpdateCode_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DSResponse dsrsp = SmartSchool.Feature.Basic.Config.GetUpdateCodeSynopsis();
                _UpdateCodeEditingEvent.Reset();
                foreach (XmlNode var in dsrsp.GetContent().GetElements("異動"))
                {
                    string UpdateCode, UpdateDescription, UpdateType;
                    UpdateCode = var.SelectSingleNode("代號").InnerText;
                    UpdateDescription = var.SelectSingleNode("原因及事項").InnerText;
                    UpdateType = var.SelectSingleNode("分類").InnerText;
                    if (!_UpdateCodeSynopsis.ContainsKey(UpdateType))
                    {
                        _UpdateCodeSynopsis.Add(UpdateType, new Dictionary<string, string>());
                    }
                    _UpdateCodeSynopsis[UpdateType].Add(UpdateCode, UpdateDescription);
                }
                _UpdateCodeEditingEvent.Set();
            }
            catch { }
            finally
            {
                _UpdateCodeLoadedEvent.Set();
            }
        }

        public event EventHandler DataChanged;

        public string StudentID { get { return _StudentID; } set { _StudentID = value; } }

        public UpdateRecordType Style
        {
            get { return _Style; }
            set
            {
                if (value == _Style) return;
                this.Visible = false;
                this.畢業名冊.Visible = false;
                this.新生名冊.Visible = false;
                this.學籍異動.Visible = false;
                this.轉入名冊.Visible = false;
                _Style = value;
                switch (_Style)
                {
                    default:
                    case UpdateRecordType.學籍異動:
                        this.學籍異動.Visible = true;
                        break;
                    case UpdateRecordType.轉入異動:
                        this.轉入名冊.Visible = true;
                        break;
                    case UpdateRecordType.新生異動:
                        this.新生名冊.Visible = true;
                        break;
                    case UpdateRecordType.畢業異動:
                        this.畢業名冊.Visible = true;
                        break;
                }
                foreach (Control var in _UpdateCodeControls)
                {
                    DropDownUpdateCode(var, ((ComboBoxEx)var).DroppedDown);
                }
                SetValue("UpdateCode", "", true);
                this.Visible = true;
            }
        }

        public bool NewStudentNumberVisible
        {
            get
            {
                return textBoxX34.Visible;
            }
            set
            {
                textBoxX34.Visible = labelX53.Visible = value;
            }
        }

        #region 取得或設定各項填入值

        //科別
        public string Department { get { return GetValue("Department"); } set { SetValue("Department", value); } }
        //核準日期(回填)
        public string ADDate { get { return GetValue("ADDate"); } set { SetValue("ADDate", value); } }
        //核準文號(回填)
        public string ADNumber { get { return GetValue("ADNumber"); } set { SetValue("ADNumber", value); } }
        //異動日期
        public string UpdateDate { get { return GetValue("UpdateDate"); } set { SetValue("UpdateDate", value); } }
        //異動代碼
        public string UpdateCode { get { return GetValue("UpdateCode"); } set { SetValue("UpdateCode", value); } }
        //原因及事項
        public string UpdateDescription { get { return GetValue("UpdateDescription"); } set { SetValue("UpdateDescription", value); } }
        //姓名
        public new string Name { get { return GetValue("Name"); } set { SetValue("Name", value); } }
        //學號
        public string StudentNumber { get { return GetValue("StudentNumber"); } set { SetValue("StudentNumber", value); } }
        //性別
        public string Gender { get { return GetValue("Gender"); } set { SetValue("Gender", value); } }
        //身份證號
        public string IDNumber { get { return GetValue("IDNumber"); } set { SetValue("IDNumber", value); } }
        //生日
        public string Birthdate { get { return GetValue("Birthdate"); } set { SetValue("Birthdate", value); } }
        //年級
        public string GradeYear { get { return GetValue("GradeYear"); } set { SetValue("GradeYear", value); } }
        //最後核準日期
        public string LastADDate { get { return GetValue("LastADDate"); } set { SetValue("LastADDate", value); } }
        //最後核準文號
        public string LastADNumber { get { return GetValue("LastADNumber"); } set { SetValue("LastADNumber", value); } }
        //備註
        public string Comment { get { return GetValue("Comment"); } set { SetValue("Comment", value); } }
        //最後核準異動代碼
        public string LastUpdateCode { get { return GetValue("LastUpdateCode"); } set { SetValue("LastUpdateCode", value); } }
        //新學號
        public string NewStudentNumber { get { return GetValue("NewStudentNumber"); } set { SetValue("NewStudentNumber", value); } }
        //轉入前學生資料-學校
        public string PreviousSchool { get { return GetValue("PreviousSchool"); } set { SetValue("PreviousSchool", value); } }
        //轉入前學生資料-學號
        public string PreviousStudentNumber { get { return GetValue("PreviousStudentNumber"); } set { SetValue("PreviousStudentNumber", value); } }
        //轉入前學生資料-科別
        public string PreviousDepartment { get { return GetValue("PreviousDepartment"); } set { SetValue("PreviousDepartment", value); } }
        //轉入前學生資料-(最後核準日期)
        public string PreviousSchoolLastADDate { get { return GetValue("PreviousSchoolLastADDate"); } set { SetValue("PreviousSchoolLastADDate", value); } }
        //轉入前學生資料-(最後核準文號)
        public string PreviousSchoolLastADNumber { get { return GetValue("PreviousSchoolLastADNumber"); } set { SetValue("PreviousSchoolLastADNumber", value); } }
        //轉入前學生資料-年級
        public string PreviousGradeYear { get { return GetValue("PreviousGradeYear"); } set { SetValue("PreviousGradeYear", value); } }
        //入學資格-畢業國中所在地代碼
        public string GraduateSchoolLocationCode { get { return GetValue("GraduateSchoolLocationCode"); } set { SetValue("GraduateSchoolLocationCode", value); } }
        //入學資格-畢業國中
        public string GraduateSchool { get { return GetValue("GraduateSchool"); } set { SetValue("GraduateSchool", value); } }
        //畢(結)業證書字號
        public string GraduateCertificateNumber { get { return GetValue("GraduateCertificateNumber"); } set { SetValue("GraduateCertificateNumber", value); } }

        #endregion
        public XmlElement GetElement()
        {
            XmlDocument doc = new XmlDocument();
            //doc.LoadXml("<root></root>");
            //XmlElement rootElement = doc.DocumentElement;
            //XmlElement infoElement;

            XmlElement rootElement = doc.CreateElement("Root");
            doc.AppendChild(rootElement);
            XmlElement infoElement;
            //科別
            infoElement = doc.CreateElement("Department");
            infoElement.InnerText = GetValue("Department");
            rootElement.AppendChild(infoElement);
            //核準日期(回填)
            infoElement = doc.CreateElement("ADDate");
            infoElement.InnerText = GetValue("ADDate");
            rootElement.AppendChild(infoElement);
            //核準文號(回填)
            infoElement = doc.CreateElement("ADNumber");
            infoElement.InnerText = GetValue("ADNumber");
            rootElement.AppendChild(infoElement);
            //異動日期
            infoElement = doc.CreateElement("UpdateDate");
            infoElement.InnerText = GetValue("UpdateDate");
            rootElement.AppendChild(infoElement);
            //異動代碼
            infoElement = doc.CreateElement("UpdateCode");
            infoElement.InnerText = GetValue("UpdateCode");
            rootElement.AppendChild(infoElement);
            //原因及事項
            infoElement = doc.CreateElement("UpdateDescription");
            infoElement.InnerText = GetValue("UpdateDescription");
            rootElement.AppendChild(infoElement);
            //姓名
            infoElement = doc.CreateElement("Name");
            infoElement.InnerText = GetValue("Name");
            rootElement.AppendChild(infoElement);
            //學號
            infoElement = doc.CreateElement("StudentNumber");
            infoElement.InnerText = GetValue("StudentNumber");
            rootElement.AppendChild(infoElement);
            //性別
            infoElement = doc.CreateElement("Gender");
            infoElement.InnerText = GetValue("Gender");
            rootElement.AppendChild(infoElement);
            //身份證號
            infoElement = doc.CreateElement("IDNumber");
            infoElement.InnerText = GetValue("IDNumber");
            rootElement.AppendChild(infoElement);
            //生日
            infoElement = doc.CreateElement("Birthdate");
            infoElement.InnerText = GetValue("Birthdate");
            rootElement.AppendChild(infoElement);
            //年級
            infoElement = doc.CreateElement("GradeYear");
            infoElement.InnerText = GetValue("GradeYear");
            rootElement.AppendChild(infoElement);
            //最後核準日期
            infoElement = doc.CreateElement("LastADDate");
            infoElement.InnerText = GetValue("LastADDate");
            rootElement.AppendChild(infoElement);
            //最後核準文號
            infoElement = doc.CreateElement("LastADNumber");
            infoElement.InnerText = GetValue("LastADNumber");
            rootElement.AppendChild(infoElement);
            //備註
            infoElement = doc.CreateElement("Comment");
            infoElement.InnerText = GetValue("Comment");
            rootElement.AppendChild(infoElement);
            //最後核準異動代碼
            infoElement = doc.CreateElement("LastUpdateCode");
            infoElement.InnerText = GetValue("LastUpdateCode");
            rootElement.AppendChild(infoElement);
            //新學號
            infoElement = doc.CreateElement("NewStudentNumber");
            infoElement.InnerText = GetValue("NewStudentNumber");
            rootElement.AppendChild(infoElement);
            //轉入前學生資料-學校
            infoElement = doc.CreateElement("PreviousSchool");
            infoElement.InnerText = GetValue("PreviousSchool");
            rootElement.AppendChild(infoElement);
            //轉入前學生資料-學號
            infoElement = doc.CreateElement("PreviousStudentNumber");
            infoElement.InnerText = GetValue("PreviousStudentNumber");
            rootElement.AppendChild(infoElement);
            //轉入前學生資料-科別
            infoElement = doc.CreateElement("PreviousDepartment");
            infoElement.InnerText = GetValue("PreviousDepartment");
            rootElement.AppendChild(infoElement);
            //轉入前學生資料-(最後核準日期)
            infoElement = doc.CreateElement("PreviousSchoolLastADDate");
            infoElement.InnerText = GetValue("PreviousSchoolLastADDate");
            rootElement.AppendChild(infoElement);
            //轉入前學生資料-(最後核準文號)
            infoElement = doc.CreateElement("PreviousSchoolLastADNumber");
            infoElement.InnerText = GetValue("PreviousSchoolLastADNumber");
            rootElement.AppendChild(infoElement);
            //轉入前學生資料-年級
            infoElement = doc.CreateElement("PreviousGradeYear");
            infoElement.InnerText = GetValue("PreviousGradeYear");
            rootElement.AppendChild(infoElement);
            //入學資格-畢業國中所在地代碼
            infoElement = doc.CreateElement("GraduateSchoolLocationCode");
            infoElement.InnerText = GetValue("GraduateSchoolLocationCode");
            rootElement.AppendChild(infoElement);
            //入學資格-畢業國中
            infoElement = doc.CreateElement("GraduateSchool");
            infoElement.InnerText = GetValue("GraduateSchool");
            rootElement.AppendChild(infoElement);
            //畢(結)業證書字號
            infoElement = doc.CreateElement("GraduateCertificateNumber");
            infoElement.InnerText = GetValue("GraduateCertificateNumber");
            rootElement.AppendChild(infoElement);

            //NewData
            infoElement = doc.CreateElement("NewData");
            infoElement.InnerText = GetValue("NewData");
            rootElement.AppendChild(infoElement);

            return rootElement;
        }

        public bool IsValid()
        {
            return (_ErrorControls.Count == 0);
        }

        #region 設定所有Control的事件處理
        private void SetupControl(Control control)
        {
            foreach (Control var in control.Controls)
            {
                string tag = "" + var.Tag;
                if (tag != "")
                {
                    if (!_ControlDictionary.ContainsKey(tag))
                        _ControlDictionary.Add(tag, new List<Control>());
                    _ControlDictionary[tag].Add(var);
                    ValueValidate(var, true);
                    var.TextChanged += new EventHandler(Control_TextChanged);
                    var.LostFocus += new EventHandler(Control_TextChanged);
                    //判斷如果是有下拉式選單則先設定下拉式選單資料
                    #region 判斷如果是有下拉式選單則先設定下拉式選單資料
                    switch (tag)
                    {
                        case "GradeYear":
                            DropDownGradeYear(var, null);
                            break;
                        case "Gender":
                            DropDownGender(var, null);
                            break;
                        //設定同時加入清單，當清單被改變時可以回來更新
                        case "Department":
                            _DepartmentControls.Add(var);
                            DropDownDepartment(var, null);
                            break;
                        //設定同時加入清單，當清單被改變時可以回來更新
                        case "UpdateCode":
                            _UpdateCodeControls.Add(var);
                            DropDownUpdateCode(var, ((ComboBoxEx)var).DroppedDown);
                            ((ComboBoxEx)var).DropDownChange += new ComboBoxEx.OnDropDownChangeEventHandler(this.DropDownUpdateCode);
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
                if (var.Controls.Count > 0)
                {
                    SetupControl(var);
                }
            }
        }

        private void DropDownGradeYear(object sender, EventArgs e)
        {
            ComboBox gradeYear = (ComboBox)sender;
            if (!gradeYear.ContainsFocus)
            {
                gradeYear.SelectedItem = null;
                gradeYear.Items.Clear();
                gradeYear.Items.Add("1");
                gradeYear.Items.Add("2");
                gradeYear.Items.Add("3");
                gradeYear.Items.Add("延修生");
            }
        }

        private void DropDownGender(object sender, EventArgs e)
        {
            ComboBox gander = (ComboBox)sender;
            gander.SelectedItem = null;
            gander.Items.Clear();
            gander.Items.Add("");
            gander.Items.Add("男");
            gander.Items.Add("女");
        }

        private void DropDownUpdateCode(object sender, bool Expand)
        {
            ComboBox updateCode = (ComboBox)sender;
            string s = "" + updateCode.SelectedItem;
            updateCode.DropDownHeight = 350;
            updateCode.DropDownWidth = 250;
            updateCode.SelectedItem = null;
            updateCode.Items.Clear();
            foreach (string var in GetUpdateCodeSynopsis(_Style.ToString()).Keys)
            {
                updateCode.Items.Add(Expand ? var + "-" + GetUpdateCodeSynopsis(_Style.ToString())[var] : var);
            }
            updateCode.SelectedItem = s.Split("-".ToCharArray())[0];
            if (Expand)
            {
                updateCode.Text = "";
            }
        }

        private void DropDownDepartment(object sender, EventArgs e)
        {
            ComboBox department = (ComboBox)sender;
            department.SelectedItem = null;
            department.Items.Clear();
            foreach (string var in _DepartmentList)
            {
                department.Items.Add(var);
            }
        }
        #endregion

        public void SetDefaultValue(string id)
        {
            _Action = UpdateRecordAction.Insert;
            _StudentID = id;

            SetValue("UpdateDate", DateTime.Now.ToShortDateString());
            AccessHelper accessHelper = new AccessHelper();
            StudentRecord studentRec = accessHelper.StudentHelper.GetStudents(id)[0];
            accessHelper.StudentHelper.FillUpdateRecord(studentRec);

            #region 查學籍身分

            SmartSchool.Customization.Data.SystemInformation.getField("SchoolConfig");
            XmlElement config = SmartSchool.Customization.Data.SystemInformation.Fields["SchoolConfig"] as XmlElement;
            XmlElement identityMapping = (XmlElement)config.SelectSingleNode("學籍身分對照表");
            if (identityMapping != null)
            {
                string comment = "";

                List<string> code_list = new List<string>();
                foreach (CategoryInfo info in studentRec.StudentCategorys)
                {
                    foreach (XmlElement each_identity in identityMapping.SelectNodes("Identity[Tag/@FullName='" + info.FullName + "']"))
                    {
                        string each_name = each_identity.GetAttribute("Code");
                        if (!code_list.Contains(each_name))
                            code_list.Add(each_name);
                    }
                }

                foreach (string each_code in code_list)
                {
                    if (!string.IsNullOrEmpty(comment)) comment += ",";
                    comment += each_code;
                }

                SetValue("Comment", comment);
            }

            //test
            //SetValue("Comment", "1");

            #endregion

            SetValue("Name", studentRec.StudentName);
            SetValue("Birthdate", studentRec.Birthday);
            SetValue("Gender", studentRec.Gender);
            SetValue("IDNumber", studentRec.IDNumber);
            SetValue("StudentNumber", studentRec.StudentNumber);
            SetValue("GradeYear", studentRec.Status == "延修" ? "延修生" : studentRec.RefClass == null ? "" : studentRec.RefClass.GradeYear);
            SetValue("Department", studentRec.Department.Contains(":") ? studentRec.Department.Split(':')[0] : studentRec.Department);
            DateTime lastADDate = DateTime.MinValue;
            string lastADNumber = "";
            string lastUpdateCode = "";

            foreach (SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo var in studentRec.UpdateRecordList)
            {
                DateTime d1;
                if (DateTime.TryParse(var.ADDate, out d1) && d1 > lastADDate)
                {
                    lastADDate = d1;
                    lastADNumber = var.ADNumber;
                    lastUpdateCode = var.UpdateCode;
                }
            }
            SetValue("LastADDate", lastADNumber == "" ? "" : lastADDate.ToShortDateString());
            SetValue("LastADNumber", lastADNumber);
            SetValue("LastUpdateCode", lastUpdateCode);

            //DSResponse dsrsp = SmartSchool.Feature.QueryStudent.GetDefaultUpdateRecordInfo(id);
            //SetValue("Name", dsrsp.GetContent().GetText("Student[@ID='" + id + "']/Name"));
            //SetValue("Birthdate", dsrsp.GetContent().GetText("Student[@ID='" + id + "']/Birthdate"));
            //SetValue("Gender", dsrsp.GetContent().GetText("Student[@ID='" + id + "']/Gender"));
            //SetValue("IDNumber", dsrsp.GetContent().GetText("Student[@ID='" + id + "']/IDNumber"));
            //SetValue("StudentNumber", dsrsp.GetContent().GetText("Student[@ID='" + id + "']/StudentNumber"));
            //SetValue("GradeYear", dsrsp.GetContent().GetText("Student[@ID='" + id + "']/Status") == "延修" ? "延修生" : dsrsp.GetContent().GetText("Student[@ID='" + id + "']/GradeYear"));
            //SetValue("Department", dsrsp.GetContent().GetText("Student[@ID='" + id + "']/Department"));
            //SetValue("LastADDate", dsrsp.GetContent().GetText("Student[@ID='" + id + "']/LastADDate"));
            //SetValue("LastADNumber", dsrsp.GetContent().GetText("Student[@ID='" + id + "']/LastADNumber"));
            //SetValue("LastUpdateCode", dsrsp.GetContent().GetText("Student[@ID='" + id + "']/LastUpdateCode"));
            Style = UpdateRecordType.學籍異動;

            //Log，紀錄修改前的資料
            foreach (XmlNode node in GetElement().ChildNodes)
            {
                beforeData.Add(node.Name, node.InnerText);
            }
        }

        public void SetUpdateValue(string updateRecordId)
        {
            _Action = UpdateRecordAction.Update;
            _UpdateRecordID = updateRecordId;

            DSResponse dsrsp = SmartSchool.Feature.QueryStudent.GetUpdateRecord(updateRecordId);
            XmlElement element = dsrsp.GetContent().GetElement("UpdateRecord");

            foreach (XmlNode node in element.ChildNodes)
            {
                if (node.Name != "ContextInfo")
                {
                    if (node.Name == "UpdateCode")
                    {
                        _UpdateCodeLoadedEvent.WaitOne();
                        foreach (UpdateRecordType type in new UpdateRecordType[] { UpdateRecordType.新生異動, UpdateRecordType.轉入異動, UpdateRecordType.學籍異動, UpdateRecordType.畢業異動 })
                        {
                            if (_UpdateCodeSynopsis.ContainsKey(type.ToString()) && _UpdateCodeSynopsis[type.ToString()].ContainsKey(node.InnerText))
                            {
                                Style = type;
                                break;
                            }
                        }
                        _newDataField.ChangeNewDataText(node.InnerText);
                    }
                    this.SetValue(node.Name, node.InnerText);
                }
                else
                {
                    if (node.SelectSingleNode("ContextInfo") != null)
                    {
                        foreach (XmlNode contextInfo in node.SelectSingleNode("ContextInfo").ChildNodes)
                        {
                            this.SetValue(contextInfo.Name, contextInfo.InnerText);
                        }
                    }
                }
            }

            //Log，紀錄修改前的資料
            foreach (XmlNode node in GetElement().ChildNodes)
            {
                beforeData.Add(node.Name, node.InnerText);
            }
        }

        public string[] AllowedUpdateCode
        {
            get { return _AllowedUpdateCode; }
            set { _AllowedUpdateCode = value; }
        }

        public bool Save()
        {
            if (_Action == UpdateRecordAction.None)
                return false;

            bool _saved = false;

            DSXmlHelper helper = new DSXmlHelper("InsertRequest");
            helper.AddElement("UpdateRecord");
            helper.AddElement("UpdateRecord", "Field");
            if (_Action != UpdateRecordAction.Update)
                helper.AddElement("UpdateRecord/Field", "RefStudentID", _StudentID);
            helper.AddElement("UpdateRecord/Field", "ContextInfo");

            XmlDocument contextInfo = new XmlDocument();
            XmlElement root = contextInfo.CreateElement("ContextInfo");
            contextInfo.AppendChild(root);

            foreach (XmlNode node in GetElement().ChildNodes)
            {
                //Log，紀錄修改後的資料
                if (afterData.ContainsKey(node.Name))
                    afterData.Remove(node.Name);
                afterData.Add(node.Name, node.InnerText);

                // 如果是 Previous 開頭的全丟到 ContextInfo 中

                if (node.Name.StartsWith("Previous") ||
                    node.Name.StartsWith("Graduate") ||
                    node.Name == "NewStudentNumber" ||
                    node.Name == "LastUpdateCode" ||
                    node.Name == "NewData")
                {
                    XmlNode importNode = node.Clone();
                    importNode = contextInfo.ImportNode(importNode, true);
                    root.AppendChild(importNode);
                }
                else
                {
                    helper.AddElement("UpdateRecord/Field", node.Name, node.InnerText);
                }
            }

            // 將 contextInfo 這個 document 的資料塞進 ContextInfo 的 CdataSection 裡
            helper.AddXmlString("UpdateRecord/Field/ContextInfo", root.OuterXml);

            if (_Action == UpdateRecordAction.Update)
            {
                // 補上條件
                helper.AddElement("UpdateRecord", "Condition");
                helper.AddElement("UpdateRecord/Condition", "ID", _UpdateRecordID);
            }

            try
            {
                if (IsValid())
                {
                    if (_Action == UpdateRecordAction.Update)
                        EditStudent.ModifyUpdateRecord(new DSRequest(helper));
                    else
                        EditStudent.InsertUpdateRecord(new DSRequest(helper));

                    _saved = true;
                }
            }
            catch (Exception ex)
            {
                if (_Action == UpdateRecordAction.Update)
                    MsgBox.Show("修改異動資料失敗：" + ex.Message);
                else
                    MsgBox.Show("新增異動資料失敗：" + ex.Message);
            }

            #region 處理 Log，寫入 Log

            StringBuilder desc = new StringBuilder("");
            desc.AppendLine("學生姓名：" + ((_AccessHelper.StudentHelper.GetStudents(_StudentID).Count > 0) ? _AccessHelper.StudentHelper.GetStudents(_StudentID)[0].StudentName + " " : "未知 "));

            if (_Action == UpdateRecordAction.Update)
                desc.AppendLine("修改異動紀錄： " + beforeData["UpdateDate"] + " " + beforeData["UpdateDescription"] + " ");
            else
                desc.AppendLine("新增異動紀錄： " + afterData["UpdateDate"] + " " + afterData["UpdateDescription"] + " ");

            foreach (string var in afterData.Keys)
            {
                if (beforeData[var] != afterData[var])
                {
                    if (_Action == UpdateRecordAction.Update)
                        desc.AppendLine("欄位「" + _EnChDict[var] + "」由「" + beforeData[var] + "」變更為「" + afterData[var] + "」");
                    else
                        desc.AppendLine("新增欄位「" + _EnChDict[var] + "」為「" + afterData[var] + "」");
                }
            }

            EntityAction entityAction = EntityAction.Insert;
            if (_Action == UpdateRecordAction.Update)
                entityAction = EntityAction.Update;

            CurrentUser.Instance.AppLog.Write(EntityType.Student, entityAction, _StudentID, desc.ToString(), "異動資料", "");

            #endregion


            #region 之前的儲存做法
            //if (_Action == UpdateRecordAction.Insert)
            //{
            //    DSXmlHelper helper = new DSXmlHelper("InsertRequest");
            //    helper.AddElement("UpdateRecord");
            //    helper.AddElement("UpdateRecord", "Field");
            //    helper.AddElement("UpdateRecord/Field", "RefStudentID", _StudentID);
            //    helper.AddElement("UpdateRecord/Field", "ContextInfo");

            //    XmlDocument contextInfo = new XmlDocument();
            //    XmlElement root = contextInfo.CreateElement("ContextInfo");
            //    contextInfo.AppendChild(root);

            //    foreach (XmlNode node in GetElement().ChildNodes)
            //    {
            //        afterData.Add(node.Name, node.InnerText);

            //        // 如果是 Previous 開頭的全丟到 ContextInfo 中

            //        if (node.Name.StartsWith("Previous") || node.Name.StartsWith("Graduate") || node.Name == "NewStudentNumber" || node.Name == "LastUpdateCode")
            //        {
            //            XmlNode importNode = node.Clone();
            //            importNode = contextInfo.ImportNode(importNode, true);
            //            root.AppendChild(importNode);
            //        }
            //        else
            //        {
            //            helper.AddElement("UpdateRecord/Field", node.Name, node.InnerText);
            //        }
            //    }

            //    // 將 contextInfo 這個 document 的資料塞進 ContextInfo 的 CdataSection 裡
            //    helper.AddXmlString("UpdateRecord/Field/ContextInfo", root.OuterXml);
            //    DSRequest dsreq = new DSRequest(helper);
            //    try
            //    {
            //        if (IsValid())
            //        {
            //            EditStudent.InsertUpdateRecord(dsreq);
            //            _saved = true;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MsgBox.Show("新增異動資料失敗：" + ex.Message);
            //    }
            //}
            //else
            //{
            //    DSXmlHelper helper = new DSXmlHelper("UpdateRequest");
            //    helper.AddElement("UpdateRecord");
            //    helper.AddElement("UpdateRecord", "Field");
            //    helper.AddElement("UpdateRecord/Field", "RefStudentID", _StudentID);
            //    helper.AddElement("UpdateRecord/Field", "ContextInfo");

            //    XmlDocument contextInfo = new XmlDocument();
            //    XmlElement root = contextInfo.CreateElement("ContextInfo");
            //    contextInfo.AppendChild(root);

            //    foreach (XmlNode node in GetElement().ChildNodes)
            //    {
            //        afterData.Add(node.Name, node.InnerText);

            //        if (node.Name.StartsWith("Previous") || node.Name.StartsWith("Graduate") || node.Name == "NewStudentNumber" || node.Name == "LastUpdateCode")
            //        {
            //            XmlNode importNode = node.Clone();
            //            importNode = contextInfo.ImportNode(importNode, true);
            //            root.AppendChild(importNode);
            //        }
            //        else
            //        {
            //            helper.AddElement("UpdateRecord/Field", node.Name, node.InnerText);
            //        }
            //    }

            //    // 將 contextInfo 這個 document 的資料塞進 ContextInfo 的 CdataSection 裡
            //    helper.AddXmlString("UpdateRecord/Field/ContextInfo", root.OuterXml);

            //    // 補上條件
            //    helper.AddElement("UpdateRecord", "Condition");
            //    helper.AddElement("UpdateRecord/Condition", "ID", _UpdateRecordID);

            //    try
            //    {
            //        if (IsValid())
            //        {
            //            EditStudent.ModifyUpdateRecord(new DSRequest(helper));
            //            _saved = true;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MsgBox.Show("修改異動資料失敗：" + ex.Message);
            //    }
            //}
            #endregion

            return _saved;
        }

        internal class NewDataField
        {
            private Dictionary<string, string> _code_text = new Dictionary<string, string>();
            private LabelX _newDataLabel;
            private TextBoxX _newDataText;

            public NewDataField(LabelX newDataLabel, TextBoxX newDataText)
            {
                //_code_text.Add("401", "　　新學號：");
                _code_text.Add("402", "　　新姓名：");
                _code_text.Add("403", "　　新性別：");
                _code_text.Add("404", "　　新籍貫：");
                _code_text.Add("405", "　　新生日：");
                _code_text.Add("407", "新身分證號：");
                _newDataLabel = newDataLabel;
                _newDataText = newDataText;
            }

            public void ChangeNewDataText(string code)
            {
                if (_code_text.ContainsKey(code))
                {
                    _newDataLabel.Text = _code_text[code];
                    Visible = true;
                }
                else
                {
                    Visible = false;
                }
            }

            public bool Visible
            {
                get { return _newDataText.Visible; }
                set { _newDataLabel.Visible = _newDataText.Visible = value; }
            }
        }
    }
}
