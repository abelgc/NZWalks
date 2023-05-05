using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser>userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequestDTO registerRequest)
        {
            var identityUser = new IdentityUser{UserName = registerRequest.Username, Email = registerRequest.Username};

            var identityResult = await userManager.CreateAsync(identityUser, registerRequest.Password);

            if (identityResult.Succeeded)
            {
                if (!registerRequest.Roles.Any()) return BadRequest("Some register process went wrong");
                //Add Roles to User
                await userManager.AddToRolesAsync(identityUser, registerRequest.Roles);
                if (identityResult.Succeeded)
                {
                    return Ok("User registered, proceed to login");
                }

            }

            return BadRequest("Some register process went wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequestDTO loginRequest)
        {
            //find user with byemail async
            var user = await userManager.FindByEmailAsync(loginRequest.Username);
            if (user != null)
            {
                var password = await userManager.CheckPasswordAsync(user, loginRequest.Password);

                if (password)
                {
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles.Any())
                    {
                        var jwToken = _tokenRepository.CreateJWT(user, roles.ToList());

                        var response = new LoginResponseDTO
                        {
                            JwtToken = jwToken
                        };
                        return Ok(response);
                    }
                }

            }

            return BadRequest("Username or password incorrect");
        }
    }
}
