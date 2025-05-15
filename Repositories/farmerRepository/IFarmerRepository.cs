using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Repositories.farmerRepository
{
    //------------------------------START OF FILE--------------------------------//
    public interface IFarmerRepository
    {
        Task<farmerModel?> GetFarmerByUserIdAsync(int userId);
        Task AddFarmerAsync(farmerModel farmer);

        Task<IEnumerable<farmerModel>> GetAllFarmersAsync();
    }
}
//-----------------------END OF FILE--------------------------------//