using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSEkinci.BasicBlog.Business.Interfaces;
using MSEkinci.BasicBlog.Business.Tools.JWTTool;
using MSEkinci.BasicBlog.DTO.DTOs.AppUserDTOs;
using MSEkinci.BasicBlog.WebApi.CustomFilters;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AuthController(IAppUserService appUserService, IJwtService jwtService, IMapper mapper)
        {
            _appUserService = appUserService;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        [HttpPost]
        [ValidModel]
        public async Task<IActionResult> SignIn(AppUserLoginDTO appUserLoginDTO)
        {
            var user = await _appUserService.CheckUserAsync(appUserLoginDTO);

            if (user != null)
            {
                return Created("", _jwtService.GenerateJwt(user));
            }
            return BadRequest("Please check your username and password");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ActiveUser()
        {
            var user = await _appUserService.FindByNameAsync(User.Identity.Name);
            return Ok(_mapper.Map<AppUserDto>(user));
        }
    }
}
