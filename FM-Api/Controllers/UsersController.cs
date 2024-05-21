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
    }
}
