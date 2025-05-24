using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SmartNeighborhoodAPI.Services
{
    public class ImageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> SaveImageAsync(IFormFile file, string destination)
        {
            var context = _httpContextAccessor.HttpContext;
            var baseUrl = $"{context.Request.Scheme}://{context.Request.Host}";

            try
            {
                if (file == null)
                    throw new ArgumentNullException(nameof(file), "The file cannot be null.");

                if (string.IsNullOrWhiteSpace(destination))
                    throw new ArgumentException("Destination path cannot be empty.", nameof(destination));

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(destination, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return $"{baseUrl}/People/{fileName}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return string.Empty;
            }
        }

        public Task<bool> DeleteImageAsync(string imageUrl, string destinationFolder)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(imageUrl))
                    return Task.FromResult(false);

                var fileName = Path.GetFileName(new Uri(imageUrl).AbsolutePath);
                var physicalPath = Path.Combine(destinationFolder, fileName);

                if (File.Exists(physicalPath))
                {
                    File.Delete(physicalPath);
                    return Task.FromResult(true);
                }

                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file: {ex.Message}");
                return Task.FromResult(false);
            }
        }
    }
}
