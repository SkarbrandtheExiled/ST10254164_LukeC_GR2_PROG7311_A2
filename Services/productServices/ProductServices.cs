using ST10254164_LukeC_GR2_PROG7311_A2.Models;
using ST10254164_LukeC_GR2_PROG7311_A2.Repositories.productRepository;
using ST10254164_LukeC_GR2_PROG7311_A2.Repositories.farmerRepository;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Services.productServices
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _repo;
        private readonly IFarmerRepository _farmerRepo;
        public ProductServices(IProductRepository repo, IFarmerRepository farmerRepo)
        {
            _repo = repo;
            _farmerRepo = farmerRepo;
        }

        public async Task<List<productModel>> GetFilteredProductsAsync(string farmer, string category, DateTime? from, DateTime? to)
            => await _repo.GetFilteredProductsAsync(farmer, category, from, to);

        public async Task<List<string>> GetFarmerNamesAsync() => await _repo.GetByFarmerAsync(string.Empty)
            .ContinueWith(t => t.Result.Select(p => p.farmerName).Distinct().ToList());
        public async Task<List<string>> GetCategoriesAsync(string farmer = null) => await _repo.GetDistinctCategoriesAsync(farmer);
        public async Task AddProductAsync(productModel product)
        {
            await _repo.AddAsync(product);
            await _repo.SaveAsync();
        }

        public async Task<List<productModel>> GetProductsByFarmerAsync(string farmerName) => await _repo.GetByFarmerAsync(farmerName);
    }
}
