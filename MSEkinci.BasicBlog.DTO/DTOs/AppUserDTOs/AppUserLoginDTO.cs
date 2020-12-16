using MSEkinci.BasicBlog.DTO.Interfaces;

namespace MSEkinci.BasicBlog.DTO.DTOs.AppUserDTOs
{
    public class AppUserLoginDTO : IDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
