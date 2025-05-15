using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Repositories.productRepository
{
    public interface IProductRepository
    {
        Task<productModel?> GetProductByIdAsync(int productId);
        Task<IEnumerable<productModel>> GetProductsByFarmerIdAsync(int farmerId);
        Task AddProductAsync(productModel product);
        Task UpdateProductAsync(productModel product);
        Task DeleteProductAsync(int productId);
        Task<IEnumerable<productModel>> GetAllProductsAsync();
    }
}
