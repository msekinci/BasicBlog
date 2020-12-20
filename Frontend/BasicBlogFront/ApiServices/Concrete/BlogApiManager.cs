using BasicBlogFront.ApiServices.Interfaces;
using BasicBlogFront.Extensions;
using BasicBlogFront.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BasicBlogFront.ApiServices.Concrete
{
    public class BlogApiManager : IBlogApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BlogApiManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:53449/api/blogs/");
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<BlogListModel>> GetAllAsync()
        {
            var responseMessage = await _httpClient.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<List<BlogListModel>> GetAllByCategoryIdAsync(int categoryId)
        {
            var responseMessage = await _httpClient.GetAsync($"GetAllByCategoryId/{categoryId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<BlogListModel> GetByIdAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync($"{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BlogListModel>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task AddAsync(BlogAddModel blogAddModel) 
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();
            if (blogAddModel.Image != null)
            {
                var stream = new MemoryStream();
                await blogAddModel.Image.CopyToAsync(stream);
                var bytes = stream.ToArray();
                ByteArrayContent byteArrayContent = new ByteArrayContent(bytes);
                byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(blogAddModel.Image.ContentType);
                formData.Add(byteArrayContent, nameof(BlogAddModel.Image), blogAddModel.Image.FileName);
            }

            var user = _httpContextAccessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");
            blogAddModel.AppUserId = user.Id;
            formData.Add(new StringContent(blogAddModel.AppUserId.ToString()), nameof(BlogAddModel.AppUserId));
            formData.Add(new StringContent(blogAddModel.Title.ToString()), nameof(BlogAddModel.Title));
            formData.Add(new StringContent(blogAddModel.ShortDescription.ToString()), nameof(BlogAddModel.ShortDescription));
            formData.Add(new StringContent(blogAddModel.Description.ToString()), nameof(BlogAddModel.Description));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));
            await _httpClient.PostAsync("", formData);
        }

        public async Task UpdateAsync(BlogUpdateModel blogUpdateModel)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();
            if (blogUpdateModel.Image != null)
            {
                var stream = new MemoryStream();
                await blogUpdateModel.Image.CopyToAsync(stream);
                var bytes = stream.ToArray();
                ByteArrayContent byteArrayContent = new ByteArrayContent(bytes);
                byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(blogUpdateModel.Image.ContentType);
                formData.Add(byteArrayContent, nameof(BlogUpdateModel.Image), blogUpdateModel.Image.FileName);
            }

            var user = _httpContextAccessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");
            blogUpdateModel.AppUserId = user.Id;
            formData.Add(new StringContent(blogUpdateModel.Id.ToString()), nameof(BlogUpdateModel.Id));
            formData.Add(new StringContent(blogUpdateModel.AppUserId.ToString()), nameof(BlogUpdateModel.AppUserId));
            formData.Add(new StringContent(blogUpdateModel.Title.ToString()), nameof(BlogUpdateModel.Title));
            formData.Add(new StringContent(blogUpdateModel.ShortDescription.ToString()), nameof(BlogUpdateModel.ShortDescription));
            formData.Add(new StringContent(blogUpdateModel.Description.ToString()), nameof(BlogUpdateModel.Description));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));
            await _httpClient.PutAsync($"{blogUpdateModel.Id}", formData);
        }

        public async Task DeleteAsync(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));
            await _httpClient.DeleteAsync($"{id}");
        }
    }
}
