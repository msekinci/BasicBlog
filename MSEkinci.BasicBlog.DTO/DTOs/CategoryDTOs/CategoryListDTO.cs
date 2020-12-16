using MSEkinci.BasicBlog.DTO.Interfaces;

namespace MSEkinci.BasicBlog.DTO.DTOs.CategoryDTOs
{
    public class CategoryListDTO : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
