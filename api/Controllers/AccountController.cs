using api.Dtos.Account;
using api.models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using api.Interfaces;


namespace api.Controllers
{

    [ApiController]
    [Route("api/account")]
    public class AccountController:ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService= tokenService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null) return Unauthorized("Check your informations again");
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
              if (!result.Succeeded) return Unauthorized("Check your informations again");
            return Ok(new NewUserDto
            {
                UserName = user.UserName!,
                Email =user.Email!,
                Token=_tokenService.CreateToken(user)
            });
        }
        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] RegisterDto registerDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var user = new AppUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,

            };
            var createdUser = await _userManager.CreateAsync(user, registerDto.Password);
            if(createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "User");
                if (roleResult.Succeeded) {
                    return Ok(new NewUserDto
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Token = _tokenService.CreateToken(user)
                    });
                }
                else
                {
                    return StatusCode(500, roleResult.Errors);
                }
              

            }
            else
            {
                return StatusCode(500, createdUser.Errors);
            }


        }
    }
}
