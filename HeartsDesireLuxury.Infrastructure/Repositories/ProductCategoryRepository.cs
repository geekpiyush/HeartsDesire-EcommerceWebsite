using Entities;
using Entities.DB;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductCategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ProductCategories> AddCategory(ProductCategories productCategory)
        {
             _db.ProductCategories.Add(productCategory);
              await  _db.SaveChangesAsync();
            return productCategory;
        }

        public async Task<List<ProductCategories>> GetAllCategories()
        {
           return await  _db.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategories?> GetCategoryByCategoryID(int CategoryID)
        {
          return await  _db.ProductCategories.FirstOrDefaultAsync(temp=>temp.CategoryID == CategoryID);
        }
    }
}
