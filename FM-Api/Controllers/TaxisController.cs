using Microsoft.AspNetCore.Mvc;
using FM_Api.DB;
using FM_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxisController : ControllerBase
    {

        private readonly DBContext _dbContext;

        public TaxisController(DBContext dbContext)
        {
            _dbContext= dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> ListTaxis(int pageNumber, int pageSize)
        {
            List<Taxi> ListTaxi = await _dbContext.Taxis.Skip((pageNumber - 1) * pageSize)
           .Take(pageSize).ToListAsync();

            return Ok(ListTaxi);
        }
    }
}
