using Microsoft.AspNetCore.Mvc;
using MSEkinci.BasicBlog.Business.Interfaces;
using MSEkinci.BasicBlog.Business.Tools.JWTTool;
using MSEkinci.BasicBlog.DTO.DTOs.AppUserDTOs;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly IJwtService _jwtService;

        public AuthController(IAppUserService appUserService, IJwtService jwtService)
        {
            _appUserService = appUserService;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserLoginDTO appUserLoginDTO)
        {
            var user = await _appUserService.CheckUser(appUserLoginDTO);

            if (user != null)
            {
                return Created("", _jwtService.GenerateJwt(user));
            }
            return BadRequest("Please check your username and password");
        }
    }
}
