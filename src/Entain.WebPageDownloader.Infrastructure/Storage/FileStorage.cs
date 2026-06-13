using Entain.WebPageDownloader.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entain.WebPageDownloader.Infrastructure.Storage
{
    public class FileStorage : IFileStorage
    {
        private readonly string _outputDirectory;

        public FileStorage()
        {
            _outputDirectory = "downloaded_pages";
            Directory.CreateDirectory(_outputDirectory);
        }

        public async Task<string> SaveAsync(string url, string content, CancellationToken cancellationToken)
        {
            string fileName = CreateSafeFileName(url);
            string filePath = Path.Combine(_outputDirectory, fileName);

            await File.WriteAllTextAsync(filePath, content, cancellationToken);

            return filePath;
        }

        private static string CreateSafeFileName(string url)
        {
            var uri = new Uri(url);

            string host = uri.Host.Replace(".", "_");

            string path = uri.AbsolutePath
                .Trim('/')
                .Replace("/", "_");

            if (string.IsNullOrWhiteSpace(path))
            {
                path = "index";
            }

            return $"{host}_{path}.html";
        }
    }
}
