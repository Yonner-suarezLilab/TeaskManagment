using Microsoft.IdentityModel.Tokens;
using Sentry;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManagment.Shared;
using TaskManagment.Shared.Configuration;

namespace TaskManagment.Infrastructure.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly string _keyPrivate;
        private readonly string _expires;

        public TokenService(string keyPrivate, string expires)
        {
            _keyPrivate = keyPrivate;
            _expires = expires;
        }

        public string GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var llave = Encoding.ASCII.GetBytes(_keyPrivate);

            // Generar un nuevo Guid como identificador único
            var userId = Guid.NewGuid();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddYears(int.Parse(_expires)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        
    }
}
