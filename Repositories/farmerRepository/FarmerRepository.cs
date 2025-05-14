using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Repositories.farmerRepository
{
    public class FarmerRepository : IFarmerRepository
    {
        private readonly applicationDBContext _context;
        public FarmerRepository(applicationDBContext context) => _context = context;

        public async Task<farmerModel> GetByNameAsync(string name) =>
            await _context.farmers.FirstOrDefaultAsync(f => f.farmerName == name);

        public async Task AddAsync(farmerModel farmer) => await _context.farmers.AddAsync(farmer);
        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public async Task<List<string>> GetDistinctFarmerNamesAsync() =>
            await _context.products.Select(p => p.farmerName).Distinct().ToListAsync();

        public async Task<farmerModel?> GetFarmerByCredentials(string username, string password)
        {
            return await _context.farmers.FirstOrDefaultAsync(f => f.farmerName == username && f.Password == password);
        }

    }
}
