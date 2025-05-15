using ST10254164_LukeC_GR2_PROG7311_A2.Models;
using ST10254164_LukeC_GR2_PROG7311_A2.Repositories.farmerRepository;
using ST10254164_LukeC_GR2_PROG7311_A2.Repositories.productRepository;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Services.productServices
{
    //------------------------------START OF FILE--------------------------------//
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;

        public ProductServices(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddProductForFarmerAsync(productModel product, int farmerId)
        {
            product.FarmerId = farmerId;
            product.AddedDate = DateTime.UtcNow;
            await _productRepository.AddProductAsync(product);
        }

        public async Task<IEnumerable<productModel>> GetProductsForFarmerAsync(int farmerId)
        {
            return await _productRepository.GetProductsByFarmerIdAsync(farmerId);
        }

        public async Task<productModel?> GetProductByIdAsync(int productId)
        {
            return await _productRepository.GetProductByIdAsync(productId);
        }

        public async Task<IEnumerable<productModel>> GetAllProductsForEmployeeAsync(
            string? productType, DateTime? startDate, DateTime? endDate)
        {
            var allProducts = await _productRepository.GetAllProductsAsync();

            if (!string.IsNullOrWhiteSpace(productType))
            {
                allProducts = allProducts.Where(p =>
                    p.Category.Equals(productType, StringComparison.OrdinalIgnoreCase));
            }
            if (startDate.HasValue)
            {
                allProducts = allProducts.Where(p => p.ProductionDate >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                allProducts = allProducts.Where(p => p.ProductionDate <= endDate.Value);
            }
            return allProducts;
        }


        public async Task UpdateProductAsync(productModel product)
        {

            await _productRepository.UpdateProductAsync(product);
        }
        public async Task<IEnumerable<productModel>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }
        public async Task DeleteProductAsync(int productId)
        {
            await _productRepository.DeleteProductAsync(productId);
        }
    }
}
//------------------------------END OF FILE----------------------------------//