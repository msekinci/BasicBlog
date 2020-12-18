using MSEkinci.BasicBlog.Entities.Concrete;

namespace MSEkinci.BasicBlog.Business.Tools.JWTTool
{
    public interface IJwtService
    {
        JwtToken GenerateJwt(AppUser user);
    }
}
