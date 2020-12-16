using MSEkinci.BasicBlog.WebApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.WebApi.Models
{
    public class UploadModel
    {
        public string NewName { get; set; }
        public UploadState UploadState { get; set; }
        public string ErrorMessage { get; set; }
    }
}
