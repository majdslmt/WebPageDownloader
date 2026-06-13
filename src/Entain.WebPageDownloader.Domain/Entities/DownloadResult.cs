using System;
using System.Collections.Generic;
using System.Text;

namespace Entain.WebPageDownloader.Domain.Entities
{
    public class DownloadResult
    {
        public string Url { get; }
        public bool IsSuccess { get; }
        public string? FilePath { get; }
        public string? ErrorMessage { get; }

        private DownloadResult(
            string url,
            bool isSuccess,
            string? filePath,
            string? errorMessage)
        {
            Url = url;
            IsSuccess = isSuccess;
            FilePath = filePath;
            ErrorMessage = errorMessage;
        }

        public static DownloadResult Success(string url, string filePath)
        {
            return new DownloadResult(url, true, filePath, null);
        }

        public static DownloadResult Failure(string url, string errorMessage)
        {
            return new DownloadResult(url, false, null, errorMessage);
        }
    }
}
