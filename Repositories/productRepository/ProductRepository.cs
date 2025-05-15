using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Data;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Repositories.productRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly applicationDBContext _context;

        public ProductRepository(applicationDBContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(productModel product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<productModel?> GetProductByIdAsync(int productId)
        {
            return await _context.Products
                .Include(p => p.Farmer)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<IEnumerable<productModel>> GetProductsByFarmerIdAsync(int farmerId)
        {
            return await _context.Products
                .Where(p => p.FarmerId == farmerId)
                .OrderByDescending(p => p.AddedDate)
                .ToListAsync();
        }

        public async Task UpdateProductAsync(productModel product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<productModel>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Farmer)
                .OrderByDescending(p => p.AddedDate)
                .ToListAsync();
        }
    }
}
