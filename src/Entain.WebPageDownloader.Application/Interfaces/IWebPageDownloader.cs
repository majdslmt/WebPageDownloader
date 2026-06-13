using System;
using System.Collections.Generic;
using System.Text;

namespace Entain.WebPageDownloader.Application.Interfaces
{
    public interface IWebPageDownloader
    {
        Task<string> DownloadAsync(string url, CancellationToken cancellationToken);
    }
}
