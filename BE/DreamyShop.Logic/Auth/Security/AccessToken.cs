using DreamyShop.Common.Extensions;
using DreamyShop.Domain.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DreamyShop.Logic.Auth.Security
{
    public class AccessToken
    {
        private readonly string _EncriptionKey;

        public AccessToken(IConfiguration Configuration)
        {
            _EncriptionKey = Configuration["AccessTokenEncriptionKey"];
        }

        public string GenerateJwtToken(AuthEntity auth)
        {
            var fullName = auth.FullName;
            var userID = auth.UserID;
            var email = auth.Email;
            var phone = auth.Phone;
            var roleTypesStr = auth.RoleTypes.ToJsonString();

            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_EncriptionKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("FullName", fullName),
                    new Claim("UserID", userID),
                    new Claim("Email", email),
                    new Claim("Phone", phone),
                    new Claim("RoleTypes", roleTypesStr),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public AuthEntity ParseJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_EncriptionKey);
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
