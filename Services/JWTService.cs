using JwtAuthenticationProject.Interfaces;
using JwtAuthenticationProject.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthenticationProject.Services
{
    public class JWTService: IJWTInterface
    {
        private readonly IConfiguration _configuration;
        public JWTService(IConfiguration configuration)
        {
            _configuration= configuration;
        }
        public string? CreateToken(UserLogin userLogin)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, userLogin.Email)
            };

            var jwtKey = _configuration.GetSection("JWT:Key").Value;

            if (jwtKey == null) return null;

            var securedkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var creds = new SigningCredentials(securedkey, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims : claims,
                    expires: DateTime.Now.AddMinutes(3),
                    signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
