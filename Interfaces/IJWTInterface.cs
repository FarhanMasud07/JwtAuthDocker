using JwtAuthenticationProject.Models;

namespace JwtAuthenticationProject.Interfaces
{
    public interface IJWTInterface
    {
        public string? CreateToken(UserLogin userLogin);
    }
}
