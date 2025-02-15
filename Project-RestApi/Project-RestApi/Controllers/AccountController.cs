using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_RestApi.Dtos.Account;
using Project_RestApi.Interfaces;
using Project_RestApi.Models;

namespace Project_RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var gameUser = new User
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email
                };

                if (string.IsNullOrEmpty(registerDto.Password))
                    return BadRequest("Password cannot be null or empty");

                var createdUser = await _userManager.CreateAsync(gameUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(gameUser, "User");

                    if (roleResult.Succeeded)
                        return Ok(
                                new NewUserDto
                                {
                                    UserName = gameUser.UserName ?? string.Empty,
                                    Email = gameUser.Email ?? string.Empty,
                                    Token = _tokenService.CreateToken(gameUser)
                                }
                            );
                    else
                        return StatusCode(500, roleResult.Errors);
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password ?? string.Empty, false);

            if (!result.Succeeded) return Unauthorized("Username or password incorrect!");

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }
    }
}
