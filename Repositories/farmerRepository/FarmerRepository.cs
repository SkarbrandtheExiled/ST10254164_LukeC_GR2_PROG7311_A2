using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Data;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Repositories.farmerRepository
{
    //------------------------------START OF FILE--------------------------------//
    public class FarmerRepository : IFarmerRepository
    {
        private readonly applicationDBContext _context; // This is the database context that will be used to interact with the database

        public FarmerRepository(applicationDBContext context) // Constructor for the FarmerRepository class
        {
            _context = context;
        }

        public async Task<IEnumerable<farmerModel>> GetAllFarmersAsync()
        { // This method retrieves all farmers from the database
            return await _context.Farmers
                .OrderBy(f => f.Name)
                .ToListAsync();
        }


        public async Task<farmerModel?> GetFarmerByUserIdAsync(int userId)
        {
            
            return await _context.Farmers
                .FirstOrDefaultAsync(f => f.UserId == userId);
        }

        public async Task AddFarmerAsync(farmerModel farmer) // This method is used to add a new farmer to the database
        {
            _context.Farmers.Add(farmer);
            await _context.SaveChangesAsync();
        }

    }
}
//------------------------------END OF FILE--------------------------------//