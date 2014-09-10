using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.Data;
using SmartSchool.ApplicationLog;
using SmartSchool.Common;
using System.Windows.Forms;
using System.ComponentModel;

namespace SmartSchool.StudentRelated
{
    internal class ChangeStatusBatch
    {
        private static BackgroundWorker _BKW = new BackgroundWorker();

        static ChangeStatusBatch()
        {
            _BKW.DoWork += new DoWorkEventHandler(_BKW_DoWork);
            _BKW.ProgressChanged += new ProgressChangedEventHandler(_BKW_ProgressChanged);
            _BKW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BKW_RunWorkerCompleted);
            _BKW.WorkerReportsProgress = true;
        }

        public static void Init()
        {
            ButtonAdapter btn一般 = new ButtonAdapter();
            btn一般.Path = "變更學生狀態";
            btn一般.Text = "一般";
            btn一般.OnClick += new EventHandler(btn一般_OnClick);
            Customization.PlugIn.ContextMenu.StudentMenuButton.AddItem(btn一般);

            ButtonAdapter btn畢業或離校 = new ButtonAdapter();
            btn畢業或離校.Path = "變更學生狀態";
            btn畢業或離校.Text = "畢業或離校";
            btn畢業或離校.OnClick += new EventHandler(btn畢業或離校_OnClick);
            Customization.PlugIn.ContextMenu.StudentMenuButton.AddItem(btn畢業或離校);

            ButtonAdapter btn休學 = new ButtonAdapter();
            btn休學.Path = "變更學生狀態";
            btn休學.Text = "休學";
            btn休學.OnClick += new EventHandler(btn休學_OnClick);
            Customization.PlugIn.ContextMenu.StudentMenuButton.AddItem(btn休學);

            ButtonAdapter btn延修 = new ButtonAdapter();
            btn延修.Path = "變更學生狀態";
            btn延修.Text = "延修";
            btn延修.OnClick += new EventHandler(btn延修_OnClick);
            Customization.PlugIn.ContextMenu.StudentMenuButton.AddItem(btn延修);
            //ButtonAdapter normalButton = new ButtonAdapter();
            //normalButton.Path = "變更學生狀態";
            //normalButton.Text = "錯學";
            //normalButton.OnClick += new EventHandler(normalButton_OnClick);
            //Customization.PlugIn.ContextMenu.StudentMenuButton.AddItem(normalButton);
        }

        static void btn一般_OnClick(object sender, EventArgs e)
        {
            if ( _BKW.IsBusy )
            {
                MsgBox.Show("系統正在變更學生狀態，\n請等待目前作業完成後，\n再次執行此動作。");
                return;
            }
            DialogResult dr = MsgBox.Show("是否變更學生狀態為\"一般\"？", Application.ProductName, MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                SetStatus("一般");
        }

        static void btn畢業或離校_OnClick(object sender, EventArgs e)
        {
            if ( _BKW.IsBusy )
            {
                MsgBox.Show("系統正在變更學生狀態，\n請等待目前作業完成後，\n再次執行此動作。");
                return;
            }
            DialogResult dr = MsgBox.Show("是否變更學生狀態為\"畢業或離校\"？\n您將無法從\"在校學生\"中找到這些學生。\n", Application.ProductName, MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                SetStatus("畢業或離校");
        }

        static void btn休學_OnClick(object sender, EventArgs e)
        {
            if ( _BKW.IsBusy )
            {
                MsgBox.Show("系統正在變更學生狀態，\n請等待目前作業完成後，\n再次執行此動作。");
                return;
            }
            DialogResult dr = MsgBox.Show("是否變更學生狀態為\"休學\"？\n您將無法從\"在校學生\"中找到這些學生。\n", Application.ProductName, MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                SetStatus("休學");
        }

        static void btn延修_OnClick(object sender, EventArgs e)
        {
            if ( _BKW.IsBusy )
            {
                MsgBox.Show("系統正在變更學生狀態，\n請等待目前作業完成後，\n再次執行此動作。");
                return;
            }
            DialogResult dr = MsgBox.Show("是否變更學生狀態為\"延修\"？", Application.ProductName, MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                SetStatus("延修");
        }

        static void SetStatus(string newStatus)
        {
            _BKW.RunWorkerAsync(newStatus);
        }

        static void _BKW_DoWork(object sender, DoWorkEventArgs e)
        {
            string newStatus = "" + e.Argument;
            List<string> idlist = new List<string>();
            e.Result = idlist;
            decimal totle = StudentRelated.Student.Instance.SelectionStudents.Count;
            if ( totle == 0 )
                totle = 1;
            decimal current = 0;
            _BKW.ReportProgress((int)( current * 100m / totle ));
            Dictionary<string, string> idLog = new Dictionary<string, string>();
            foreach ( BriefStudentData student in StudentRelated.Student.Instance.SelectionStudents )
            {
                current++;
                if ( student.Status != newStatus )
                {
                    idLog.Add(student.ID, string.Format("學生：{0}{1} \n狀態由「{2}」變更為「{3}」",student.StudentNumber.Length>0?"("+student.StudentNumber+")":"", student.Name, student.Status, newStatus));
                    if ( idLog.Count > 100 )
                    {
                        _BKW.ReportProgress((int)( current * 100m / totle ));
                        Feature.EditStudent.ChangeStudentStatus(newStatus,new List<string>(idLog.Keys).ToArray());
                        idlist.AddRange(idLog.Keys);
                        #region 修改學生狀態 Log
                        foreach ( string id in idLog.Keys )
                        {
                            CurrentUser.Instance.AppLog.Write(EntityType.Student, "變更狀態", id, idLog[id], "學生", "");
                        }
                        #endregion
                        idLog.Clear();
                    }
                }
            }
            if ( idLog.Count > 0 )
            {
                _BKW.ReportProgress((int)( current * 100m / totle ));
                Feature.EditStudent.ChangeStudentStatus(newStatus, new List<string>(idLog.Keys).ToArray());
                idlist.AddRange(idLog.Keys);
                #region 修改學生狀態 Log
                foreach ( string id in idLog.Keys )
                {
                    CurrentUser.Instance.AppLog.Write(EntityType.Student, "變更狀態", id, idLog[id], "學生", "");
                }
                #endregion
                idLog.Clear();
            }
        }

        static void _BKW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Customization.PlugIn.Global.SetStatusBarMessage("變更學生狀態...",e.ProgressPercentage);
        }

        static void _BKW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ( e.Error != null )
            {
                MsgBox.Show("變更狀態發生錯誤，可能有部分學生狀態未修改。");
                ExceptionHandler.BugReporter.ReportException("SmartSchool", CurrentUser.Instance.SystemVersion, e.Error, false);
            }
            List<string> idlist = (List<string>)e.Result;
            if ( idlist.Count > 0 )
                //Student.Instance.InvokBriefDataChanged(idlist.ToArray());
                SmartSchool.Broadcaster.Events.Items["學生/資料變更"].Invoke(idlist.ToArray());
        }
    }
}
