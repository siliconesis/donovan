using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Donovan.Game.Security
{
    public static class TokenHelper
    {
        public static JwtSecurityToken CreateToken(string key, string id, string name)
        {
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id),
                    new Claim(ClaimTypes.Name, name)
                }),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateJwtSecurityToken(descriptor);

            return token;
        }

        public static JwtSecurityToken DecodeToken(string encoded)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(encoded);

            return token;
        }

        public static string EncodeToken(JwtSecurityToken token)
        {
            var handler = new JwtSecurityTokenHandler();
            var encoded = handler.WriteToken(token);

            return encoded;
        }
    }
}
