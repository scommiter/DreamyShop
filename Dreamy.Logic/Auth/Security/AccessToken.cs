using Dreamy.Domain.Shared.Dtos.Auth;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Dreamy.Common.Utitlities;

namespace Dreamy.Logic.Auth.Security
{
    public class AccessToken
    {
        private readonly IConfiguration _config;

        public AccessToken(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJwtToken(AuthEntity auth)
        {
            var fullName = auth.FullName;
            var userID = auth.UserID;
            var email = auth.Email;
            var phone = auth.Phone;
            var roleTypesStr = auth.RoleTypes.ToJsonString();
            var claims = new[]
            {
                new Claim("FullName", fullName),
                new Claim("UserID", userID),
                new Claim("Email", email),
                new Claim("Phone", phone),
                new Claim("RoleTypes", roleTypesStr)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:AccessTokenEncriptionKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public AuthEntity ParseJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:AccessTokenEncriptionKey"]);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            var fullName = jwtToken.Claims.First(x => x.Type == "FullName").Value;
            var userID = jwtToken.Claims.First(x => x.Type == "UserID").Value;
            var email = jwtToken.Claims.First(x => x.Type == "Email").Value;
            var phone = jwtToken.Claims.First(x => x.Type == "Phone").Value;
            var roleTypesStr = jwtToken.Claims.First(x => x.Type == "RoleTypes").Value;
            var roleTypes = roleTypesStr.ToJsonObject<List<byte>>();

            var authUser = new AuthEntity()
            {
                FullName = fullName,
                UserID = userID,
                Phone = phone,
                RoleTypes = roleTypes,
                Email = string.IsNullOrEmpty(email) ? null : email,
            };

            return authUser;
        }
    }
}
