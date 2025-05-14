using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Repositories.farmerRepository
{
    public interface IFarmerRepository
    {
        Task<farmerModel> GetByNameAsync(string name);
        Task AddAsync(farmerModel farmer);
        Task SaveAsync();
        Task<List<string>> GetDistinctFarmerNamesAsync();

        // Add the missing method definition to fix the error
        Task<farmerModel?> GetFarmerByCredentials(string username, string password);
    }
}
