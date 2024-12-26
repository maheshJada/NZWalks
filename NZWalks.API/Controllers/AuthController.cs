using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };
            var identityResult= await userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if (identityResult.Succeeded) {
                // add roles to this user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult= await userManager.AddToRoleAsync(identityUser, registerRequestDto.Roles.ToString());
                    if (identityResult.Succeeded) 
                    {
                        return Ok("User was registerd");
                    }
                }
            }
            return BadRequest("Something Went Wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
           var user= await userManager.FindByEmailAsync(loginRequestDto.UserName);
            if (user != null)
            {
               var checkPasswordResult= await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    //Get Roles for this user
                    var roles= await userManager.GetRolesAsync(user);
                    if(roles != null)
                    {
                       var jwtToken= tokenRepository.CreateJWTToken(user,roles.ToList());
                        var responce = new LoginResponceDto
                        {
                            JWTToken = jwtToken
                        };
                        return Ok(jwtToken);
                    }
                    //create Token

                    
                    return Ok();
                }
            }
            return BadRequest("Username or PAssword is incorrect");

        }
        
    }
}
