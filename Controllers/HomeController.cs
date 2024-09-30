using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace FileDownloadPrototype.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        // Inject HttpClient via constructor (dependency injection)
        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Action to download a file from a 3rd-party API
        public async Task<IActionResult> DownloadFile()
        {
            // Example API URL (replace with the actual file URL)
            var fileUrl = "https://example.com/path-to-your-file";

            try
            {
                // Send HTTP request to get the file
                var response = await _httpClient.GetAsync(fileUrl);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read the file content
                var fileBytes = await response.Content.ReadAsByteArrayAsync();

                // Specify the file name and return the file for download
                var fileName = "downloadedFile.pdf";
                return File(fileBytes, "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                // Handle the error
                ViewBag.Error = $"File download failed: {ex.Message}";
                return View("Error");
            }
        }
    }
}
