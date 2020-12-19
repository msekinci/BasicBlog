using BasicBlogFront.ApiServices.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BasicBlogFront.ApiServices.Concrete
{
    public class ImageApiManager : IImageApiService
    {
        private readonly HttpClient _httpClient;

        public ImageApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:53449/api/images/");
        }

        public async Task<string> GetBlogImageByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"GetBlogImageById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var bytes = await response.Content.ReadAsByteArrayAsync();
                return $"data:image/jpeg;base64,{Convert.ToBase64String(bytes)}";
            }
            return null;
        }
    }
}
