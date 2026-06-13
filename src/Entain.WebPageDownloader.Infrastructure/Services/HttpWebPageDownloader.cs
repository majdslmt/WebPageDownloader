using Entain.WebPageDownloader.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entain.WebPageDownloader.Infrastructure.Services
{
    public class HttpWebPageDownloader : IWebPageDownloader
    {
        private readonly HttpClient _httpClient;

        public HttpWebPageDownloader(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> DownloadAsync(string url, CancellationToken cancellationToken)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }
    }
}
