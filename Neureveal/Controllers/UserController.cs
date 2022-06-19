using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Neureveal.Data.Models;
using Neureveal.Dtos.Authentication;
using Neureveal.Dtos.Token;
using Neureveal.Helpers;
using System.Security.Claims;

namespace Neureveal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        public UserController(UserManager<User> _userManager)
        {
            userManager = _userManager;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<TokenDTO>> Login(LoginDTO credentials)
        {
            User user = await userManager.FindByNameAsync(credentials.UserName);
            if (user == null) { return BadRequest("User not found"); }
            var userClaims = await userManager.GetClaimsAsync(user);
            bool islocked = await userManager.IsLockedOutAsync(user);
            if (islocked) { return BadRequest("Try again later you have tried 3 times"); }
            bool isAuthenticated = await userManager.CheckPasswordAsync(user, credentials.Password);
            if (!isAuthenticated)
            {
                await userManager.AccessFailedAsync(user);
                return BadRequest("wrong credentials");
            }
            //string secretKey = config.GetValue<string>("SecretKey");
            var token = Helper.generateToken(userClaims);

            return new TokenDTO()
            {
                Token = token,
                ExpireAt = DateTime.Now.AddMinutes(30)
            };
        }
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<string>> Register(RegisterDTO registerDTO)
        {
            User Writer = new User() {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email
            };
            var UserClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, registerDTO.UserName),
                new Claim(ClaimTypes.Email, registerDTO.Email),
                new Claim(ClaimTypes.Role, "Writer")
            };
            var result = await userManager.CreateAsync(Writer, registerDTO.Password);
            await userManager.AddClaimsAsync(Writer, UserClaims);
            if (result.Succeeded) { return Ok("Registeration Completed"); }
            else return BadRequest(result.Errors); ;

        }

        [HttpPost]
        [Route("AdminRegister")]
        public async Task<ActionResult<string>> AdminRegister(RegisterDTO registerDTO)
        {
            User Writer = new User()
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email
            };
            var UserClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, registerDTO.UserName),
                new Claim(ClaimTypes.Email, registerDTO.Email),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var result = await userManager.CreateAsync(Writer, registerDTO.Password);
            await userManager.AddClaimsAsync(Writer, UserClaims);
            if (result.Succeeded) { return Ok("Registeration Completed"); }
            else return BadRequest(result.Errors); ;

        }




    }
}
