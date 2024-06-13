using FM_Api.DTO;
using FM_Api.Interfaces;
using FM_Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FM_Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Users> _usersManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<Users> _signinManager;
        public AccountController(UserManager<Users> userManager, ITokenService tokenService, SignInManager<Users> signinManager)
        {
            _usersManager = userManager;
            _tokenService = tokenService;
            _signinManager = signinManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _usersManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username!");

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.GetToken(user)
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var Users = new Users
                {
                    UserName = registerDTO.UserName,
                    Email = registerDTO.Email,
                };

                var createdUser = await _usersManager.CreateAsync(Users, registerDTO.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _usersManager.AddToRoleAsync(Users, "user");
                    if (roleResult.Succeeded)
                    {
                        return Ok(new NewUserDto
                        {
                            UserName = Users.UserName,
                            Email = Users.Email,
                            Token = _tokenService.GetToken(Users)
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
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

        }
        
    }
}
