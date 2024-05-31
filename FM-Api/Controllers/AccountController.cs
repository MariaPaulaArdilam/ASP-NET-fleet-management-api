using FM_Api.DTO;
using FM_Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FM_Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Users> _usersManager;
        public AccountController(UserManager<Users> userManager)
        {
            _usersManager = userManager;
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
                        return Ok(  "User created");
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
