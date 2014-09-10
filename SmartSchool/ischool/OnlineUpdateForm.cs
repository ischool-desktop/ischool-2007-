using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using OnlineUpdateClient;
using IntelliSchool.DSA30.Util;
using System.IO;
using System.Threading;
using SmartSchool.Common;

namespace ischool
{
    public partial class OnlineUpdateForm : SmartSchool.Common.BaseForm
    {
        private DSConnection _connection;
        private ProductUpdateServer _updateserver;
        private ManifestComparer _comparer;
        private VersionManifest _local_ver, _server_ver;
        private string _update_source = Path.Combine(Application.StartupPath, "_UpdateFiles");
        private string _update_temp = Path.Combine(Application.StartupPath, "_UpdateTemporal");
        private string _update_target = Application.StartupPath;
        private StreamWriter _log;
        private FileDescriptionCollection _news, _replaces, _deletes;
        private bool _download_has_fail = false;
        bool get_internal;

        public OnlineUpdateForm()
        {
            get_internal = (Control.ModifierKeys == Keys.Shift);

            InitializeComponent();

            InitializeLog();

            try //試圖刪除暫存目錄。
            {
                DirectoryInfo tempFolder = new DirectoryInfo(_update_temp);
                tempFolder.Delete(true);

                DirectoryInfo updateFiles = new DirectoryInfo(_update_source);
                updateFiles.Delete(true);
            }
            catch { }

            try
            {
                _connection = new DSConnection("http://beta.smartschool.com.tw/commonserver/updatecenter", "anonymous", "");
                _connection.Connect();
            }
            catch (Exception ex)
            {
                string msg = string.Format("網路連線不正常，請檢查與「線上更新主機」之間的網路連線是否正常。\n主機位置：{0}", "beta.smartschool.com.tw");
                throw new ConnectException(msg, ex);
            }

            _updateserver = new ProductUpdateServer(_connection);

            LoadLocalManifest();

            try
            {
                _server_ver = _updateserver.GetProductLastestVersion("SmartSchool", !get_internal);
            }
            catch (Exception ex)
            {
                throw new SendRequestException("取得系統最新版資訊錯誤，訊息：" + ex.Message, ex);
            }

            _comparer = new ManifestComparer(_local_ver, _server_ver);
        }

        public bool IsUpdateRequired
        {
            get { return _comparer.UpdateRequired; }
        }

        private void Log(string msg)
        {
            if (_log == null) return;

            lock (_log)
            {
                if (_log == null) return;

                _log.WriteLine(msg);
            }
        }

        public void UpdateSystem()
        {
            ShowDialog();
        }

        private void OnlineUpdateForm_Load(object sender, EventArgs e)
        {
            try
            {
                DownloadTask task = new DownloadTask(_update_source);

                _news = _comparer.GetNewFiles();
                _replaces = _comparer.GetUpdateFiles();
                _deletes = _comparer.GetDeleteFiles();

                task.AddRange(_news);
                task.AddRange(_replaces);

                task.DownloadStart += new EventHandler(Task_DownloadStart);
                task.DownloadFinish += new EventHandler(Task_DownloadFinish);
                task.FileStart += new FileStartEventHandler(Task_FileStart);
                task.FileComplete += new FileCompleteEventHandler(Task_FileComplete);
                task.TotalProgressChange += new TotalProgressEventHandler(Task_TotalProgressChange);

                task.Start();
            }
            catch (Exception ex)
            {
                throw new Exception("更新系統時發生錯誤，訊息：" + ex.Message, ex);
            }
        }

        private void Task_TotalProgressChange(object sender, TotalProgressEventArgs e)
        {
            if (e.TotalProgress > pb.Maximum)
                pb.Value = pb.Maximum;
            else
                pb.Value = e.TotalProgress;
        }

        private void Task_FileStart(object sender, FileStartEventArgs e)
        {
            lblMessage.Text = "正在下載檔案：" + e.File.FileName;
            Application.DoEvents();
        }

        private void Task_FileComplete(object sender, FileCompleteEventArgs e)
        {
            string fullName = Path.Combine(e.File.Folder, e.File.FileName);

            if (e.Status == TaskStatus.Fail)
            {
                _download_has_fail = true;
                string msg = "下載檔案失敗：" + fullName + "\n";
                msg += "錯誤訊息：" + e.Error.Message;
                Log(msg);
            }
            else
            {
                Log("下載成功：" + fullName);
            }
        }

        private void Task_DownloadStart(object sender, EventArgs e)
        {
            int total = 0;
            foreach (FileDescription file in _news)
                total += file.Size;
            foreach (FileDescription file in _replaces)
                total += file.Size;

            pb.Minimum = 0;
            pb.Maximum = total;
        }

        private void Task_DownloadFinish(object sender, EventArgs e)
        {
            pb.Value = pb.Maximum;

            if (_download_has_fail)
            {
                MsgBox.Show("有部份檔案下載失敗，系統更新未完成。");
                Log("有部份檔案下載失敗，系統更新未完成。");

                Close();
                return;
            }

            try
            {
                foreach (FileDescription each in _deletes)
                {
                    FileUtil.MoveFileToTemporal(each, _update_target, _update_temp);
                    string file = Path.Combine(each.Folder, each.FileName);
                    Log("已將「" + file + "」移至待刪除目錄，下次啟動系統將會被刪除。");
                    lblMessage.Text = "更新檔案：" + file;
                    Application.DoEvents();
                }

                foreach (FileDescription each in _news)
                {
                    FileUtil.UpdateFile(each, _update_source, _update_target, _update_temp);
                    string file = Path.Combine(each.Folder, each.FileName);
                    Log("已新增「" + file + "」檔案。");
                    lblMessage.Text = "更新檔案：" + file;
                    Application.DoEvents();
                }

                foreach (FileDescription each in _replaces)
                {
                    FileUtil.UpdateFile(each, _update_source, _update_target, _update_temp);
                    string file = Path.Combine(each.Folder, each.FileName);
                    Log("已更新「" + file + "」檔案。");
                    lblMessage.Text = "更新檔案：" + file;
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                Log("有部份檔案更新失敗，系統已停止更新，訊息：" + ex.Message);
                MsgBox.Show("有部份檔案更新失敗，系統已停止更新。");
                Close();
                return;
            }

            DSXmlHelper.SaveXml(Path.Combine(Application.StartupPath, "version.manifest"), _server_ver.GetSerializedXml());

            lblMessage.Text = "更新完成。";
            Application.DoEvents();

            Thread.Sleep(2000);

            Close();
        }

        private void InitializeLog()
        {
            try
            {
                string folder = Path.Combine(_update_target, "Logs");
                string fileName = "UpdateLog_" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".txt";

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                _log = new StreamWriter(Path.Combine(folder, fileName), true, Encoding.UTF8);
            }
            catch { }
        }

        private void LoadLocalManifest()
        {
            try
            {
                _local_ver = new VersionManifest();
                string mfFileName = Path.Combine(Application.StartupPath, "version.manifest");

                //get_internal 是否內部更新。
                if (File.Exists(mfFileName))//存在就載入，不存在就當作是最舊版。
                    _local_ver.LoadFromFile(mfFileName);
                else
                    _local_ver = new VersionManifest();
            }
            catch (Exception ex)
            {
                throw new Exception("載入本機版本資訊錯誤，訊息：" + ex.Message, ex);
            }
        }

        private void OnlineUpdateForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                _log.Close();
            }
            catch { }
        }
    }
}