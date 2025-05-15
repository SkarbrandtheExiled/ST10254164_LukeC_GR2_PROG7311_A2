using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Data;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Repositories.farmerRepository
{
    public class FarmerRepository : IFarmerRepository
    {
        private readonly applicationDBContext _context;

        public FarmerRepository(applicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<farmerModel>> GetAllFarmersAsync()
        {
            return await _context.Farmers
                .OrderBy(f => f.Name)
                .ToListAsync();
        }


        public async Task<farmerModel?> GetFarmerByUserIdAsync(int userId)
        {

            return await _context.Farmers
                .FirstOrDefaultAsync(f => f.UserId == userId);
        }

        public async Task AddFarmerAsync(farmerModel farmer)
        {
            _context.Farmers.Add(farmer);
            await _context.SaveChangesAsync();
        }

    }
}
