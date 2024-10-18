using LibraryWebApp.Application.Interfaces.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryWebApp.Application.UseCases.Tokens
{
    public class GenerateAccessTokenUseCase : IGenerateAccessTokenUseCase
    {
        private readonly IConfiguration _configuration;

        public GenerateAccessTokenUseCase(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Execute(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:AccessTokenExpiration"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
