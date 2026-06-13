using System;
using System.Collections.Generic;
using System.Text;

namespace Entain.WebPageDownloader.Application.Interfaces
{
    public interface IFileStorage
    {
        Task<string> SaveAsync(string url, string content, CancellationToken cancellationToken);
    }
}
