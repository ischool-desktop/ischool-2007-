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
using SmartSchool.ApplicationLog;
using SmartSchool.AccessControl;

namespace SmartSchool.StudentRelated.Palmerworm
{
    [FeatureCode("Content0130")]
    public partial class SemesterHistoryPalmerworm : PalmerwormItemBase
    {
        private string _CurrentID;

        private string _RunningID;

        private bool _Pass;

        private Dictionary<string, List<string>> _semesterValues;

        private BackgroundWorker _Loader = new BackgroundWorker();

        private LogRecorder logger = new LogRecorder();

        public SemesterHistoryPalmerworm()
        {
            InitializeComponent();
            _Loader.DoWork += new DoWorkEventHandler(_Loader_DoWork);
            _Loader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_Loader_RunWorkerCompleted);
            Application.Idle += new EventHandler(Application_Idle);
        }

        void Application_Idle(object sender, EventArgs e)
        {
            bool hasChanged = false;
            #region 驗資料變更
            Dictionary<string, List<string>> semesterValues = new Dictionary<string, List<string>>();
            foreach ( DataGridViewRow row in dataGridViewX1.Rows )
            {
                if ( !row.IsNewRow )
                {
                    string semester = ( "" + row.Cells[0].Value ).Trim() + "_" + ( "" + row.Cells[1].Value ).Trim();
                    if ( !semesterValues.ContainsKey(semester) )
                        semesterValues.Add(semester, new List<string>(new string[] { ( "" + row.Cells[2].Value ).Trim() }));
                    else
                        _Pass &= false;
                }
            }
            hasChanged = ( _semesterValues != null && semesterValues.Count != _semesterValues.Count );
            if ( !hasChanged )
            {
                foreach ( string key in semesterValues.Keys )
                {
                    if ( !_semesterValues.ContainsKey(key) )
                    {
                        hasChanged = true;
                        break;
                    }
                    else
                    {
                        for ( int i = 0 ; i < semesterValues[key].Count ; i++ )
                        {
                            if ( semesterValues[key][i] != _semesterValues[key][i] )
                            {
                                hasChanged = true;
                                break;
                            }
                        }
                    }
                }
            }
            #endregion
            SaveButtonVisible = _Pass & hasChanged;
            CancelButtonVisible = hasChanged;
        }

        void _Loader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ( this.IsDisposed )
                return;
            if (_RunningID != _CurrentID)
            {
                _RunningID = _CurrentID;
                _Loader.RunWorkerAsync(_RunningID);
                return;
            }
            this.WaitingPicVisible = false;
            XmlElement[] elements = (XmlElement[])e.Result;
            _semesterValues = new Dictionary<string, List<string>>();
            foreach (XmlElement element in elements)
            {
                string schoolYear, semester, gradeyear;
                schoolYear = element.GetAttribute("SchoolYear");
                semester = element.GetAttribute("Semester");
                gradeyear = element.GetAttribute("GradeYear");
                dataGridViewX1.Rows.Add(schoolYear, semester, gradeyear);
                _semesterValues.Add(schoolYear.Trim() + "_" + semester.Trim(), new List<string>(new string[] { gradeyear.Trim() }));
                logger.AddBefore(schoolYear.Trim() + "_" + semester.Trim(), gradeyear.Trim());
            }
            dataGridViewX1.EndEdit();
            CheckAll();
        }

        void _Loader_DoWork(object sender, DoWorkEventArgs e)
        {
            string id = ""+e.Argument;
            e.Result = Feature.QueryStudent.GetDetailList(new string[] { "ID", "SemesterHistory" }, id).GetContent().GetElements("Student[@ID='" + id + "']/SemesterHistory/History");
        }
        public override void LoadContent(string id)
        {
            _CurrentID = id;
            dataGridViewX1.EndEdit();
            if(_semesterValues!=null)_semesterValues.Clear();
            dataGridViewX1.Rows.Clear();
            SaveButtonVisible = false;
            CancelButtonVisible = false;
            if (!_Loader.IsBusy)
            {
                _RunningID = _CurrentID;
                _Loader .RunWorkerAsync(_RunningID);
                WaitingPicVisible = true;
            }
        }
        public override void Save()
        {
            dataGridViewX1.EndEdit();
            if (CheckAll())
            {
                DSXmlHelper helper = new DSXmlHelper("UpdateStudentList");
                helper.AddElement("Student");
                helper.AddElement("Student", "Field");
                helper.AddElement("Student/Field", "SemesterHistory");
                helper.AddElement("Student", "Condition");
                helper.AddElement("Student/Condition", "ID", _CurrentID);
                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        XmlElement element = helper.AddElement("Student/Field/SemesterHistory", "History");
                        element.SetAttribute("SchoolYear", "" + row.Cells[0].Value);
                        element.SetAttribute("Semester", "" + row.Cells[1].Value);
                        element.SetAttribute("GradeYear", "" + row.Cells[2].Value);
                        logger.AddAfter(row.Cells[0].Value.ToString() + "_" + row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString());
                    }
                }
                Feature.EditStudent.Update(new DSRequest(helper));

                #region Log

                Dictionary<string, string[]> difference = logger.GetDifference();
                StringBuilder desc = new StringBuilder("");
                desc.AppendLine("學生姓名：" + Student.Instance.Items[_CurrentID].Name + " ");
                    
                foreach (string key in difference.Keys)
                {
                    string schoolyear = key.Split('_')[0];
                    string semester = key.Split('_')[1];
                    string before = difference[key][0];
                    string after = difference[key][1];

                    if (!string.IsNullOrEmpty(before) && !string.IsNullOrEmpty(after))
                    {
                        desc.AppendLine("修改 「" + schoolyear + "」學年度 第「" + semester + "」學期 年級由「" + before + "」年級變更為「" + after + "」年級");
                    }
                    else if (string.IsNullOrEmpty(before) && !string.IsNullOrEmpty(after))
                    {
                        desc.AppendLine("新增 「" + schoolyear + "」學年度 第「" + semester + "」學期 年級為「" + after + "」年級");
                    }
                    else if (!string.IsNullOrEmpty(before) && string.IsNullOrEmpty(after))
                    {
                        desc.AppendLine("刪除 「" + schoolyear + "」學年度 第「" + semester + "」學期「" + before + "」年級");
                    }

                    //「」
                }

                CurrentUser.Instance.AppLog.Write(EntityType.Student, "修改學期對照表", _CurrentID, desc.ToString(), "", "");

                #endregion

                LoadContent(_CurrentID);
            }
            else
                MsgBox.Show("學期歷程資料輸入錯誤，儲存失敗。");
        }
        public override void Undo()
        {
            LoadContent(_CurrentID);
        }

        private void dataGridViewX1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            DataGridViewCell cell = dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string message = "儲存格值：" + cell.Value + "。\n發生錯誤： " + e.Exception.Message + "。";
            if (cell.ErrorText != message)
            {
                cell.ErrorText = message;
                dataGridViewX1.UpdateCellErrorText(e.ColumnIndex, e.RowIndex);
            }
        }

        private void dataGridViewX1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridViewX1.EndEdit();
            //CheckAll();
        }

        private bool CheckAll()
        {
            _Pass = true;
            #region 驗資料輸入正確
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                if (!row.IsNewRow)
                {
                        int x;
                    for (int i = 0; i < 3; i++)
                    {
                        #region 驗空白
                        if ("" + row.Cells[i].Value == "")
                        {
                            row.Cells[i].ErrorText = "不得空白";
                            _Pass &= false;
                            dataGridViewX1.UpdateCellErrorText(i, row.Index);
                        }
                        else if (row.Cells[i].ErrorText == "不得空白")
                        {
                            row.Cells[i].ErrorText = "";
                            dataGridViewX1.UpdateCellErrorText(i, row.Index);
                        }
                        #endregion
                        #region 驗數字
                        if (!int.TryParse("" + row.Cells[i].Value, out x))
                        {
                            row.Cells[i].ErrorText = "必須輸入數字";
                            _Pass &= false;
                            dataGridViewX1.UpdateCellErrorText(i, row.Index);
                        }
                        else if (row.Cells[i].ErrorText == "必須輸入數字")
                        {
                            row.Cells[i].ErrorText = "";
                            dataGridViewX1.UpdateCellErrorText(i, row.Index);
                        }
                        #endregion
                    }
                    #region 檢查學期輸入範圍
                    if (int.TryParse("" + row.Cells[1].Value, out x) &&( x > 2 || x < 1))
                    {
                        row.Cells[1].ErrorText = "只允許1或2";
                        _Pass &= false;
                        dataGridViewX1.UpdateCellErrorText(1, row.Index);
                    }
                    else if (row.Cells[1].ErrorText == "只允許1或2")
                    {
                        row.Cells[1].ErrorText = "";
                        dataGridViewX1.UpdateCellErrorText(1, row.Index);
                    } 
                    #endregion
                }
            }
            #endregion
            return _Pass;
        }

        private void dataGridViewX1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dataGridViewX1.EndEdit();
            CheckAll();
        }

        private void dataGridViewX1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridViewX1.EndEdit();
        }

        private void dataGridViewX1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && dataGridViewX1.SelectedCells.Count == 1)
                dataGridViewX1.BeginEdit(true);
        }

        private void dataGridViewX1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dataGridViewX1.EndEdit();
            CheckAll();
            dataGridViewX1.BeginEdit(false);
        }
    }

    /// <summary>
    /// 記錄 Log 用的
    /// </summary>
    class LogRecorder
    {
        Dictionary<string, string> beforeData = new Dictionary<string, string>();
        Dictionary<string, string> afterData = new Dictionary<string, string>();

        public LogRecorder()
        {
        }

        public void AddBefore(string k, string v)
        {
            if (!beforeData.ContainsKey(k))
                beforeData.Add(k, v);
            else
                beforeData[k] = v;
        }

        public void AddAfter(string k, string v)
        {
            if (!afterData.ContainsKey(k))
                afterData.Add(k, v);
            else
                afterData[k] = v;
        }

        public Dictionary<string, string[]> GetDifference()
        {
            Dictionary<string, string[]> difference = new Dictionary<string, string[]>();

            foreach (string key in beforeData.Keys)
            {
                if (afterData.ContainsKey(key) && beforeData[key] != afterData[key])
                {
                    difference.Add(key, new string[] { beforeData[key], afterData[key] });
                    afterData.Remove(key);
                }
                else if (afterData.ContainsKey(key) && beforeData[key] == afterData[key])
                {
                    afterData.Remove(key);
                }
                else
                {
                    difference.Add(key, new string[] { beforeData[key], "" });
                }
            }

            foreach (string key in afterData.Keys)
            {
                difference.Add(key, new string[] { "", afterData[key] });
            }

            beforeData.Clear();
            afterData.Clear();
            return difference;
        }
    }
}
