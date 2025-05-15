using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Data;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Repositories.productRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly applicationDBContext _context;
        public ProductRepository(applicationDBContext context) => _context = context;

        public async Task<List<productModel>> GetFilteredProductsAsync(string farmer, string category, DateTime? from, DateTime? to)
        {
            var query = _context.products.AsQueryable();
            if (!string.IsNullOrEmpty(farmer)) query = query.Where(p => p.farmerName == farmer);
            if (!string.IsNullOrEmpty(category)) query = query.Where(p => p.Category == category);
            if (from.HasValue) query = query.Where(p => p.productCreationDate >= from);
            if (to.HasValue) query = query.Where(p => p.productCreationDate <= to);
            return await query.ToListAsync();
        }

        public async Task<List<string>> GetDistinctCategoriesAsync(string farmerName = null)
        {
            var query = _context.products.AsQueryable();
            if (!string.IsNullOrEmpty(farmerName))
                query = query.Where(p => p.farmerName == farmerName);
            return await query.Select(p => p.Category).Distinct().ToListAsync();
        }

        public async Task AddAsync(productModel product) => await _context.products.AddAsync(product);
        public async Task<List<productModel>> GetByFarmerAsync(string farmerName) =>
            await _context.products.Where(p => p.farmerName == farmerName).ToListAsync();

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
