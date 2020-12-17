using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSEkinci.BasicBlog.WebApi.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public async Task<UploadModel> UploadFileAsync(IFormFile file, string contentType)
        {
            UploadModel uploadModel = new UploadModel();
            if (file != null)
            {
                if (file.ContentType != contentType)
                {
                    uploadModel.ErrorMessage = "Invalid file type";
                    uploadModel.UploadState = Enums.UploadState.Error;
                }
                else
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", fileName);
                    var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);

                    uploadModel.UploadState = Enums.UploadState.Success;
                    uploadModel.NewName = fileName;
                }
            }
            else
            {
                uploadModel.ErrorMessage = "No found the file";
                uploadModel.UploadState = Enums.UploadState.NotExist;
            }
            return uploadModel;
        }
    }
}
