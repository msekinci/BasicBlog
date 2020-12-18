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
        public JwtToken GenerateJwt(AppUser user)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfos.SecurityKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: JwtInfos.Issuer,
                audience: JwtInfos.Audience,
                claims: SetClaims(user),
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(JwtInfos.Expires),
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
