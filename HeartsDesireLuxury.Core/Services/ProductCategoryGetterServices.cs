using Entities;
using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductCategoryGetterServices : IProductCategoryGetterService
    {
        //private readonly ApplicationDbContext _db;
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductCategoryGetterServices(/*ApplicationDbContext applicationDbContext*/ IProductCategoryRepository productCategoryRepository)
        {
            //_db = applicationDbContext;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<List<ProductCategoryResponse>> GetAllCategory()
        {
            return (await _productCategoryRepository.GetAllCategories()).Select(temp => temp.ToProductCategoryResponse()).ToList();
        }
        public async Task<ProductCategoryResponse> GetCategoryByCategoryID(int? CategoryID)
        {
            if (CategoryID == null)
                return null;
            

             ProductCategories? productCategory = await _productCategoryRepository.GetCategoryByCategoryID(CategoryID.Value);

            return productCategory?.ToProductCategoryResponse();
        }

       
    }
}
