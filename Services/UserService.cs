using System.Security.Cryptography;
using System.Text;

namespace JwtAuthenticationProject.Services
{
    public class UserService
    {
        public void CreateSaltedHashPassword(
            string password,
            out byte[] passwordSalt,
            out byte[] passwordHash)
        {
            using var hmc = new HMACSHA512();
            passwordSalt = hmc.Key;
            passwordHash = hmc.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public bool VerifySaltedHashPassword(
                string password,
                byte[] passwordHash,
                byte[] passwordSalt
            )
        {
            using var hmc = new HMACSHA512(passwordSalt);
            var computeHash = hmc.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computeHash.SequenceEqual(passwordHash);
        }
    }
}
