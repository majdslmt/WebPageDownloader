using Entain.WebPageDownloader.Application.Interfaces;
using Entain.WebPageDownloader.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entain.WebPageDownloader.Application.Services
{
    public class DownloadManager
    {
        private readonly IWebPageDownloader _webPageDownloader;
        private readonly IFileStorage _fileStorage;

        public DownloadManager(IWebPageDownloader webPageDownloader, IFileStorage fileStorage)
        {
            _webPageDownloader = webPageDownloader;
            _fileStorage = fileStorage;
        }

        public async Task<IReadOnlyList<DownloadResult>> DownloadPagesAsync(IEnumerable<string> urls, int maxConcurrentDownloads, CancellationToken cancellationToken)
        {
            using var semaphore = new SemaphoreSlim(maxConcurrentDownloads);

            var tasks = urls.Select(url =>
                DownloadSinglePageAsync(url, semaphore, cancellationToken));

            return await Task.WhenAll(tasks);
        }

        private async Task<DownloadResult> DownloadSinglePageAsync(string url, SemaphoreSlim semaphore, CancellationToken cancellationToken)
        {
            await semaphore.WaitAsync(cancellationToken);

            try
            {
                string html = await _webPageDownloader.DownloadAsync(url, cancellationToken);

                string filePath = await _fileStorage.SaveAsync(url, html, cancellationToken);

                return DownloadResult.Success(url, filePath);
            }
            catch (Exception ex)
            {
                return DownloadResult.Failure(url, ex.Message);
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
