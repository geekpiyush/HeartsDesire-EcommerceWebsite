using Entities;
using Entities.DB;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Products> AddProduct(Products products)
        {
            _db.Products.Add(products);
          await _db.SaveChangesAsync();
            return products;

        }

        public async Task<bool> DeleteProduct(int ProductID)
        {
            _db.Products.RemoveRange(_db.Products.Where(temp=> temp.ProductID == ProductID));
           await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Products>> GetAllProducts()
        {
           return await _db.Products.Include("ProductCategory").ToListAsync();
        }

        public async Task<List<Products>> GetProductByCategoryID(int CategoryID)
        {
           return await _db.Products.Where(p => p.CategoryID == CategoryID).Include("ProductCategory").ToListAsync();
        }

        public async Task<Products?> GetProductsById(int ProductID)
        {
          return await  _db.Products.Include("ProductCategory").FirstOrDefaultAsync(temp=>temp.ProductID == ProductID);
        }

        public async Task<List<Products>> GetTopProducts(int count)
        {
            return await _db.Products
                     .Include(p => p.ProductCategory)
                     .OrderByDescending(p => p.ProductID)
                     .Take(count)
                     .ToListAsync();
        }

        public async Task<Products> UpdateProduct(Products products)
        {
          Products? matchingProduct = await _db.Products.FirstOrDefaultAsync(temp=>temp.ProductID ==  products.ProductID);
            if(matchingProduct == null) 
                return products;
            matchingProduct.ProductPrice = products.ProductPrice;
            matchingProduct.ProductSalePrice = products.ProductSalePrice;
            matchingProduct.ProductName = products.ProductName;
            matchingProduct.Description = products.Description;
            matchingProduct.ShortDescription = products.ShortDescription;
            matchingProduct.MainImagePath = products.MainImagePath;
            matchingProduct.ReferenceImages = products.ReferenceImages;
            matchingProduct.SkuID = products.SkuID;
            matchingProduct.Stock = products.Stock;

          await  _db.SaveChangesAsync();
            return matchingProduct;
        }
    }
}
