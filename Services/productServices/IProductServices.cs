using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Services.productServices
{
    public interface IProductServices
    {
        Task<IEnumerable<productModel>> GetProductsForFarmerAsync(int farmerId);
        Task AddProductForFarmerAsync(productModel product, int farmerId);
        Task<productModel?> GetProductByIdAsync(int productId);
        Task<IEnumerable<productModel>> GetAllProductsForEmployeeAsync(
            string? productType, DateTime? startDate, DateTime? endDate);
        Task UpdateProductAsync(productModel product);
        Task<IEnumerable<productModel>> GetAllProductsAsync();

        Task DeleteProductAsync(int productId);

    }
}