using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization.DataContracts;

namespace FM_Api.DB
{
    public class SeedDB 
    {
        private readonly DBContext _context;

        public SeedDB(DBContext dBContext)
        {
            _context = dBContext;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckTaxisAsync();
            await CheckTrajectoriesAsync();
        }

            private async Task CheckTaxisAsync()
        {
            if (!_context.Taxis.Any())
            {
                string sqlScriptTaxis = await File.ReadAllTextAsync("DB\\taxis.sql");
                await _context.Database.ExecuteSqlRawAsync(sqlScriptTaxis);
            }
        }

        private async Task CheckTrajectoriesAsync()
        {
            if (!_context.Trajectories.Any())
            {
                string sqlScriptTrajectories = await File.ReadAllTextAsync("DB\\trajectories.sql");
                string sqlScriptTrajectories2 = await File.ReadAllTextAsync("DB\\trajectories2.sql");
                string sqlScriptTrajectories3 = await File.ReadAllTextAsync("DB\\trajectories3.sql");
                await _context.Database.ExecuteSqlRawAsync(sqlScriptTrajectories);
                await _context.Database.ExecuteSqlRawAsync(sqlScriptTrajectories2);
                await _context.Database.ExecuteSqlRawAsync(sqlScriptTrajectories3);
            }
        }



    }
}
