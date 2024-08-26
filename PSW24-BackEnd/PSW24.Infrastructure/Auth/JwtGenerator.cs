using PSW24.API.DTOs;
using PSW24.Core.Domain;
using FluentResults;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using PSW24.Core.Services;
using System.Security.Cryptography;
using System.Resources;
using System.Text.Json;

namespace PSW24.Infrastructure.Auth
{
    public class JwtGenerator : ITokenGenerator
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private const double dInMinutes = 60 * 24;
        public JwtGenerator()
        {
            string filePath = "../PSW24-BackEnd/Resources/JWT.json";

            string jsonString = File.ReadAllText(filePath);
            JWTDto credentials = JsonSerializer.Deserialize<JWTDto>(jsonString);

            _key = credentials.Key;
            _issuer = credentials.Issuer;
            _audience = credentials.Audience;
        }

        public Result<AuthenticationTokensDto> GenerateAccessToken(User user)
        {
            var authenticationResponse = new AuthenticationTokensDto();

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("id", user.Id.ToString()),
                new("username", user.Username),
                new("role", user.GetPrimaryRoleName())
            };

            var jwt = CreateToken(claims, dInMinutes);
            authenticationResponse.Id = user.Id;
            authenticationResponse.AccessToken = jwt;

            return authenticationResponse;
        }

        private string CreateToken(IEnumerable<Claim> claims, double expirationTimeInMinutes)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.Now.AddMinutes(expirationTimeInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
