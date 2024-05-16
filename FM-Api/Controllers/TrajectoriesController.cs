using Microsoft.AspNetCore.Mvc;
using FM_Api.DB;
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
        public async Task<IActionResult> ListTrajectories(int id, string plate ,DateTime date, int pageNumber, int pageSize)
        {
            var ListTrajectories = await _dbContext.Trajectories
       .Join(_dbContext.Taxis, // la tabla con la que unir
             tr => tr.TaxiId, // la clave foránea en Trajectories
             tx => tx.Id, // la clave primaria en Taxi
             (tr, tx) => new { Trajectorie = tr, Taxi = tx }) // resultado de la unión
       .Where(t => t.Taxi.Id == id && t.Taxi.Plate == plate && t.Trajectorie.Date == date)
       .Skip((pageNumber - 1) * pageSize)
       .Take(pageSize)
       .Select(t => t.Trajectorie) // selecciona solo la trayectoria
       .ToListAsync();

            return Ok(ListTrajectories);
        }
    }


}

