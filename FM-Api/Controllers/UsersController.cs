using Microsoft.AspNetCore.Mvc;
using FM_Api.DB;
using FM_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FM_Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {

        private readonly DBContext _dbContext;

        public UsersController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> ListUser(int pageNumber, int pageSize)
        {
            List<Users> ListUser = await _dbContext.Users
                .Skip(pageNumber -1 * pageNumber)
                .Take(pageSize)
                .ToListAsync();
            return Ok(ListUser);
        }

        [HttpPost]  
        public async Task<IActionResult> AddUser(Users users)
        {
            _dbContext.Users.Add(users);
            await _dbContext.SaveChangesAsync();

            return Ok(users);
        }
        [HttpPatch("{id}")]

        public async Task<IActionResult> ModifyUser(int id, [FromBody] Users userParam)
        {
        
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
            {
                
                return NotFound();
            }
           
            user.Email = userParam.Email ?? user.Email;
            user.Role = userParam.Role ?? user.Role;

            await _dbContext.SaveChangesAsync();

            return Ok(user);


        }

    }
}
