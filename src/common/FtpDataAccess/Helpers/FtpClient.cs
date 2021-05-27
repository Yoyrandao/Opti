using System.Collections.Generic;
using System.IO;
using System.Net;

using FtpDataAccess.Factories;

using File = FtpDataAccess.Models.File;

namespace FtpDataAccess.Helpers
{
    public class FtpClient : IFtpClient
    {
        public FtpClient(IFtpWebRequestFactory requestFactory)
        {
            _requestFactory = requestFactory;
        }

        #region Implementation of IFtpClient

        public void UploadFile(Stream fileStream, string remotePath)
        {
            var request = _requestFactory.CreateFor(remotePath).With(WebRequestMethods.Ftp.UploadFile).Build();

            using var ftpStream = request.GetRequestStream();
            fileStream.CopyTo(ftpStream);
        }

        public void DeleteFile(string remotePath)
        {
            var request = _requestFactory.CreateFor(remotePath).With(WebRequestMethods.Ftp.DeleteFile).Build();
            request.GetResponse();
        }

        public ICollection<File> GetListing(string folder)
        {
            var directories = new List<File>();
            var request = _requestFactory.CreateFor(folder).With(WebRequestMethods.Ftp.ListDirectory).Build();

            var response = (FtpWebResponse) request.GetResponse();
            var streamReader = new StreamReader(response.GetResponseStream());

            var currentElement = streamReader.ReadLine();

            while (!string.IsNullOrEmpty(currentElement))
            {
                var raw = currentElement.Split("/");
                currentElement = raw[^1];

                directories.Add(new File
                {
                    Folder = folder,
                    Name = currentElement
                });

                currentElement = streamReader.ReadLine();
            }

            return directories;
        }

        public void CreateDirectory(string path)
        {
            var request = _requestFactory.CreateFor(path).With(WebRequestMethods.Ftp.MakeDirectory).Build();
            using var response = (FtpWebResponse) request.GetResponse();
        }

        public long GetFileSize(string path)
        {
            var request = _requestFactory.CreateFor(path).With(WebRequestMethods.Ftp.GetFileSize).Build();
            using var response = (FtpWebResponse) request.GetResponse();

            return response.ContentLength;
        }

        public bool IsDirectoryExists(string path)
        {
            var isExists = false;

            try
            {
                var request = _requestFactory.CreateFor(path).With(WebRequestMethods.Ftp.ListDirectory).Build();

                using var response = (FtpWebResponse) request.GetResponse();
                isExists = true;
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {
                    return isExists;
                }

                var response = (FtpWebResponse) e.Response;

                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    return false;
                }
            }

            return isExists;
        }

        #endregion

        private readonly IFtpWebRequestFactory _requestFactory;
    }
}