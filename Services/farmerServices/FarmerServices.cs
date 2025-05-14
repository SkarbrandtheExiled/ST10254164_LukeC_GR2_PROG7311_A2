using ST10254164_LukeC_GR2_PROG7311_A2.Models;
using ST10254164_LukeC_GR2_PROG7311_A2.Repositories.farmerRepository;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Services.farmerServices
{
    public class FarmerServices : IFarmerServices
    {
        private readonly IFarmerRepository _repo;
        public FarmerServices(IFarmerRepository repo) => _repo = repo;

        public async Task<bool> FarmerExistsAsync(string name)
            => await _repo.GetByNameAsync(name) != null;

        public async Task CreateFarmerAsync(string name, string email, string password)
        {
            var farmer = new farmerModel { farmerName = name, Email = email, Password = password };
            await _repo.AddAsync(farmer);
            await _repo.SaveAsync();
        }
        public async Task<farmerModel?> GetFarmerByCredentials(string username, string password)
        {
            return await _repo.GetFarmerByCredentials(username, password);
        }
    }
}
