using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MSEkinci.BasicBlog.Business.StringInfos;
using MSEkinci.BasicBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MSEkinci.BasicBlog.Business.Tools.JWTTool
{
    public class JwtManager : IJwtService
    {
        private readonly IOptions<JwtInfos> _optionJwt;
        public JwtManager(IOptions<JwtInfos> optionJwt)
        {
            _optionJwt = optionJwt;
        }
        public JwtToken GenerateJwt(AppUser user)
        {
            var jwtInfo = _optionJwt.Value;
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtInfo.SecurityKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtInfo.Issuer,
                audience: jwtInfo.Audience,
                claims: SetClaims(user),
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(jwtInfo.Expires),
                signingCredentials: signingCredentials
                );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            JwtToken jwtToken = new JwtToken
            {
                Token = handler.WriteToken(jwtSecurityToken)
            };
            return jwtToken;
        }

        private List<Claim> SetClaims(AppUser appUser)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, appUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString())
            };
            return claims;
        }
    }
}
