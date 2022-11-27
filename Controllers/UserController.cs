using JwtAuthenticationProject.Data;
using JwtAuthenticationProject.Interfaces;
using JwtAuthenticationProject.Models;
using JwtAuthenticationProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthenticationProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userSercvice;
        private readonly AuthApiDbContext _authApiContext;
        private readonly IJWTInterface _iIwtService;
        public UserController(UserService userService,AuthApiDbContext authApiDbContext, IJWTInterface iIwtService)
        {
            _userSercvice = userService;
            _authApiContext = authApiDbContext;
            _iIwtService = iIwtService;
        }

        [HttpGet]
        public string get()
        {
            return "Hi Testing";
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> UserRegistration(User user)
        {
            try
            {
                var existingUser = _authApiContext.Users
                .Where(userItem => userItem.Email == user.Email)
                .FirstOrDefault();

                if (existingUser != null) return StatusCode(403, "This user already exists");

                // creating hash algortihm for create passwordhash with salt
                _userSercvice
                    .CreateSaltedHashPassword(
                    user.Password,
                    out byte[] passwordSalt,
                    out byte[] passwordHash);


                // create new user object
                var newRegisterUser = new User
                {
                    UserId = Guid.NewGuid(),
                    UserName = user.UserName,
                    Email = user.Email,
                    IsDarkMode = false,
                    IsSubscribed = false,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    PhoneNumber = user.PhoneNumber,
                    Password = "NULL HAHA YOU FOOL"
                };

                // add user object to EFC
                _authApiContext.Users.Add(newRegisterUser);

                // save to the sql server
                await _authApiContext.SaveChangesAsync();
                return Ok(newRegisterUser);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<string> UserLogin(UserLogin userLogin)
        {
            try
            {
                if (userLogin.Password != userLogin.ConfirmPassword)
                    return StatusCode(401, "Confirm Password & Password not matched");

                var foundUser = _authApiContext.Users
                    .Where(user => user.Email == userLogin.Email).FirstOrDefault();

                if (foundUser == null
                    || foundUser.PasswordHash == null
                    || foundUser.PasswordSalt == null) return StatusCode(401, "User not found");


                var verifiedSaltedHash = _userSercvice
                    .VerifySaltedHashPassword(userLogin.Password, foundUser.PasswordHash, foundUser.PasswordSalt);

                if (!verifiedSaltedHash) return BadRequest("Incorrect password");

                var token = _iIwtService.CreateToken(userLogin);

                return Ok(token);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
