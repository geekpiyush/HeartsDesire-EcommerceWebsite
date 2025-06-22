using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IProductCategoryRepository
    {
        Task<ProductCategories> AddCategory(ProductCategories productCategory);
        Task<List<ProductCategories>>GetAllCategories();

        Task<ProductCategories?> GetCategoryByCategoryID(int CategoryID);
    }
}
