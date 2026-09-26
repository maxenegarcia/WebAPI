using WebApplication1.Models.Data;
using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<Product> AddAsync(Product product)
        {
            var savedProduct = _context.Product.Add(product).Entity;
            _context.SaveChanges();
            return Task.FromResult(savedProduct);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var product = _context.Product.Find(id);
            if (product is null)
            {
                return Task.FromResult(false);
            }
            else
            {
                _context.Product.Remove(product);
                _context.SaveChanges();
                return Task.FromResult(true);
            }
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = _context.Product.ToList();
            return Task.FromResult<IEnumerable<Product>>(products);
        }

        public Task<Product?> GetByIdAsync(int id)
        {
            var product = _context.Product.Find(id);
            return Task.FromResult(product);
        }

        public Task<bool> UpdateAsync(Product product)
        {
            var existingProduct = _context.Product.Find(product.Id);        
            if (existingProduct is null)
            {
                return Task.FromResult(false);
            }
            else
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.StockQuantity = product.StockQuantity;
                _context.SaveChanges();
                return Task.FromResult(true);
            }
        }
    }
}
