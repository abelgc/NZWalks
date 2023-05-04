using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser>userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequestDTO registerReques)
        {
            var identityUser = new IdentityUser{UserName = registerReques.Username, Email = registerReques.Username};

            var identityResult = await userManager.CreateAsync(identityUser, registerReques.Password);

            if (identityResult.Succeeded)
            {
                if (!registerReques.Roles.Any()) return BadRequest("Some register process went wrong");
                //Add Roles to User
                await userManager.AddToRolesAsync(identityUser, registerReques.Roles);
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
                        var jwToken = "";
                    }
                }

            }
            

            //if password exists get roles

            //if roles not null create token and provide login response

            return BadRequest("Username or password incorrect");
        }
    }
}
