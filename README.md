# Entain Web Page Downloader

A C#/.NET console application that downloads multiple web pages asynchronously and saves each page as an HTML file.

The application asks the user how many pages they want to download, accepts between 1 and 10 website URLs, downloads the pages concurrently, and saves the downloaded HTML files in the application output folder.

---

## Features

- Download multiple web pages asynchronously
- Ask the user how many pages they want to download
- Allow the user to choose a number from 1 to 10
- Ask the user to enter website URLs
- Validate the website URL format
- Save downloaded pages as `.html` files
- Save output files in the application output folder
- Limit concurrent downloads using `SemaphoreSlim`
- Handle failed downloads without stopping the full application
- Use Clean Architecture principles
- Use Dependency Injection
- Reuse `HttpClient` through `IHttpClientFactory`

---

## Technologies Used

- C#
- .NET
- Console Application
- async/await
- HttpClient
- Dependency Injection
- Clean Architecture

---

## Project Structure

```text
Entain.WebPageDownloader
│
├── Entain.WebPageDownloader.Domain
│   └── Entities
│       └── DownloadResult.cs
│
├── Entain.WebPageDownloader.Application
│   ├── Interfaces
│   │   ├── IWebPageDownloader.cs
│   │   └── IFileStorage.cs
│   │
│   └── Services
│       └── DownloadManager.cs
│
├── Entain.WebPageDownloader.Infrastructure
│   ├── Services
│   │   └── HttpWebPageDownloader.cs
│   │
│   └── Storage
│       └── FileStorage.cs
│
└── Entain.WebPageDownloader.ConsoleApp
    └── Program.cs
```

---

## Output Result

After the downloads finish, the application prints the result for each website in the console.

For a successful download, the output looks like this:

```text
Success: https://example.com
```

The downloaded HTML files will be saved in the Console App output directory:

`WebPageDownloader.ConsoleApp/bin/Debug/{dotnet-version}/downloaded_pages`

Example for .NET 8:

`WebPageDownloader.ConsoleApp/bin/Debug/net8.0/downloaded_pages`
