using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Services.productServices
{
    public interface IProductServices
    {
        Task<List<productModel>> GetFilteredProductsAsync(string farmer, string category, DateTime? from, DateTime? to);
        Task<List<string>> GetFarmerNamesAsync();
        Task<List<string>> GetCategoriesAsync(string farmerName = null);
        Task AddProductAsync(productModel product);
        Task<List<productModel>> GetProductsByFarmerAsync(string farmerName);
    }
}
