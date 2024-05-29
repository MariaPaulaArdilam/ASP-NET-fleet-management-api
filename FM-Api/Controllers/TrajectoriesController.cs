using Microsoft.AspNetCore.Mvc;
using FM_Api.DB;
using FM_Api.DTO;
using FM_Api.Models;
using Microsoft.EntityFrameworkCore;


namespace FM_Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TrajectoriesController : Controller
    {
        private readonly DBContext _dbContext;

        public TrajectoriesController(DBContext  dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> ListTrajectories(int id,DateTime date, int pageNumber, int pageSize)
        {
            var searchDate = DateTime.SpecifyKind(date.ToUniversalTime().Date, DateTimeKind.Unspecified);

            List<Trajectorie> ListTrajectories = await _dbContext.Trajectories

                
                .Where<Trajectorie>(t => t.TaxiId == id && t.Date.Date == date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            return Ok(ListTrajectories);
        }

        [HttpGet("LastTrayectory")]
        public async Task<ActionResult<IEnumerable<Trajectorie>>> ListLastTrajectories(int pageNumber, int pageSize)
        {
            var lastTarjectories = await _dbContext.Trajectories
                .Include("Taxi")
                 .GroupBy(t => t.TaxiId)
                 .Select(g => g.OrderByDescending(t => t.Date).FirstOrDefault())
                 .ToListAsync();

            var taxiInfoDtos = lastTarjectories
                .Select(t => new TaxiDTO
                {
                    TaxiId = t.Taxi.Id,
                    Plate = t.Taxi.Plate,
                    Latitude = t.Latitude,
                    Longitude = t.Longitude,
                    Date = t.Date
                }).ToList();

            return Ok(taxiInfoDtos);
        }
    }


}

