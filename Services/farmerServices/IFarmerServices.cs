using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Services.farmerServices
{
    public interface IFarmerServices
    {
        Task<bool> FarmerExistsAsync(string name);
        Task CreateFarmerAsync(string name, string email, string password);
        Task<farmerModel?> GetFarmerByCredentials(string username, string password);
    }
}
