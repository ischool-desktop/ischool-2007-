using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace SmartSchool.Payment.AccountStatedModules
{
    class FTPConnection
    {
        private string _user;

        private string _password;

        public FTPConnection(string url, string user, string password)
        {
            if (url.Substring(url.Length - 1, 1) == "/")
                _url = url;
            else
                _url = url + "/";

            _user = user;
            _password = password;
        }

        private string _url;
        public string Url
        {
            get { return _url; }
        }

        public IEnumerable<string> ListDirectory()
        {
            FtpWebResponse response = GetResponse(string.Empty, WebRequestMethods.Ftp.ListDirectory);

            Stream rspstream = response.GetResponseStream();
            StreamReader reader = new StreamReader(rspstream);

            while (!reader.EndOfStream)
                yield return reader.ReadLine();
        }

        public Stream DownloadFile(string fileName)
        {
            FtpWebResponse response = GetResponse(fileName, WebRequestMethods.Ftp.DownloadFile);

            Stream stream = response.GetResponseStream();
            MemoryStream mstream = new MemoryStream();
            int bufferSize = 1024, readCount, offset;
            byte[] buffer = new byte[bufferSize];

            offset = 0;
            while ((readCount = stream.Read(buffer, 0, bufferSize)) > 0)
            {
                mstream.Write(buffer, 0, readCount);
                offset += readCount;
            }

            stream.Close();
            mstream.Seek(0, SeekOrigin.Begin);
            return mstream;
        }

        public void DeleteFile(string fileName)
        {
            GetDeleteResponse(fileName);
        }

        private FtpWebResponse GetResponse(string fileName,string cmd)
        {
            string ftpUrl = string.Format("{0}{1}", _url, fileName);
            FtpWebRequest request = WebRequest.Create(ftpUrl) as FtpWebRequest;

            request.Method = cmd;
            request.Credentials = new NetworkCredential(_user, _password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            return response;
        }

        private FtpWebResponse GetDeleteResponse(string fileName)
        {
            string ftpUrl = string.Format("{0}{1}", _url, fileName);
            FtpWebRequest request = WebRequest.Create(ftpUrl) as FtpWebRequest;

            request.Method = WebRequestMethods.Ftp.Rename;//先用 Rename 取代 Delete。
            request.Credentials = new NetworkCredential(_user, _password);
            request.RenameTo = "_" + fileName;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            return response;
        }
    }
}
