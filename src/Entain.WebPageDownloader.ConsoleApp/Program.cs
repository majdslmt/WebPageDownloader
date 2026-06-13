using Entain.WebPageDownloader.Application.Interfaces;
using Entain.WebPageDownloader.Application.Services;
using Entain.WebPageDownloader.Infrastructure.Storage;
using Entain.WebPageDownloader.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

var services = new ServiceCollection();

services.AddHttpClient<IWebPageDownloader, HttpWebPageDownloader>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(15);
});

services.AddSingleton<IFileStorage, FileStorage>();
services.AddTransient<DownloadManager>();

ServiceProvider serviceProvider = services.BuildServiceProvider();

var downloadManager = serviceProvider.GetRequiredService<DownloadManager>();

Console.WriteLine("Async Web Page Downloader");
Console.WriteLine("-------------------------");

int numberOfPages = ReadNumberOfPages();

List<string> urls = ReadUrlsFromUser(numberOfPages);

int maxConcurrentDownloads = 5;

using var cancellationTokenSource = new CancellationTokenSource();

IReadOnlyList<Entain.WebPageDownloader.Domain.Entities.DownloadResult> results =
    await downloadManager.DownloadPagesAsync(
        urls,
        maxConcurrentDownloads,
        cancellationTokenSource.Token);

Console.WriteLine();
Console.WriteLine("Download Results");
Console.WriteLine("----------------");

foreach (var result in results)
{
    if (result.IsSuccess)
    {
        Console.WriteLine($"Success: {result.Url}");
        Console.WriteLine($"Saved to: {result.FilePath}");
    }
    else
    {
        Console.WriteLine($"Failed: {result.Url}");
        Console.WriteLine($"Error: {result.ErrorMessage}");
    }

    Console.WriteLine();
}

static int ReadNumberOfPages()
{
    while (true)
    {
        Console.Write("How many pages do you want to download? Enter a number from 1 to 10: ");

        string? input = Console.ReadLine();

        if (int.TryParse(input, out int numberOfPages)
            && numberOfPages >= 1
            && numberOfPages <= 10)
        {
            return numberOfPages;
        }

        Console.WriteLine("Invalid input. Please enter a number between 1 and 10.");
    }
}

static List<string> ReadUrlsFromUser(int numberOfPages)
{
    var urls = new List<string>();

    for (int i = 1; i <= numberOfPages; i++)
    {
        while (true)
        {
            Console.Write($"Enter website URL #{i}: ");

            string? input = Console.ReadLine();

            if (IsValidUrl(input))
            {
                urls.Add(input!);
                break;
            }

            Console.WriteLine("Invalid URL. Please enter a valid URL starting with http:// or https://");
        }
    }

    return urls;
}

static bool IsValidUrl(string? url)
{
    if (string.IsNullOrWhiteSpace(url))
    {
        return false;
    }

    return Uri.TryCreate(url, UriKind.Absolute, out Uri? uri)
           && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
}