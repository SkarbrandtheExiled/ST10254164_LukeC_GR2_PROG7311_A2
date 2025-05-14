using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Repositories.productRepository
{
    public interface IProductRepository
    {
        Task<List<productModel>> GetFilteredProductsAsync(string farmerFilter, string categoryFilter, DateTime? dateFrom, DateTime? dateTo);
        Task<List<string>> GetDistinctCategoriesAsync(string farmerName = null);
        Task AddAsync(productModel product);
        Task<List<productModel>> GetByFarmerAsync(string farmerName);
        Task SaveAsync();
    }
}
