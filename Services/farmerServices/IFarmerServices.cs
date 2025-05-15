using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Services.farmerServices
{
    public interface IFarmerServices
    {
        Task<farmerModel?> GetFarmerByUserIdAsync(int userId);
        Task<IEnumerable<farmerModel>> GetAllFarmersAsync();
        Task<(bool Success, string? ErrorMessage)> CreateFarmerWithUserAsync(addFarmerModel model);
    }
}
