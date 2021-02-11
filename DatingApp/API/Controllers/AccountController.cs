using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        // constructor
        public AccountController(DataContext context, ITokenService tokenService)
        {
            this._context = context;
            this._tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            // if (!ModelState.IsValid) return In
            if (await UserExists(registerDto.UserName))
            {
                return BadRequest("UserName is already taken.");
            }

            //HMAC givess access to all the hashing and salting functionality.
            using (var hmac = new HMACSHA512())
            {
                AppUser user = new AppUser
                {
                    UserName = registerDto.UserName.ToLower(),
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),//this returns a byte[] representing the password
                    PasswordSalt = hmac.Key     // this assigns the randomly generated Key (comes with the HMAC instance) to the salt variable of the user instance,
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();// async woohoo!

                //returning a UserDto instead of a user so you need to transfer the values.
                UserDto userDto = new UserDto
                {
                    UserName = user.UserName,
                    Token = _tokenService.CreateToken(user)// the service' method to create a tokenized user.
                };

                return userDto;
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);

            if (user == null)
            {
                return Unauthorized("Invalid UserName");
            }
            using HMACSHA512 hmac = new HMACSHA512(key: user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            //returning a UserDto instead of a user so you need to transfer the values.
            UserDto userDto = new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)// the service' method to create a tokenized user.
            };

            return userDto;
        }


        private async Task<bool> UserExists(string userName)
        {
            return await _context.Users.AnyAsync(x => x.UserName == userName.ToLower());
        }




    }// end of class
}//end of namespace
