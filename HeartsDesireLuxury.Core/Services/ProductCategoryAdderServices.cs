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
    public class ProductCategoryAdderServices : IProductCategoryAdderService
    {
        //private readonly ApplicationDbContext _db;
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductCategoryAdderServices(/*ApplicationDbContext applicationDbContext*/ IProductCategoryRepository productCategoryRepository)
        {
            //_db = applicationDbContext;
            _productCategoryRepository = productCategoryRepository;
        }
        public async Task<ProductCategoryResponse> AddCategory(ProductCategoryAddRequest? productCategoryAddRequest)
        {
            if (productCategoryAddRequest == null)
                throw new ArgumentNullException(nameof(productCategoryAddRequest));

            if(productCategoryAddRequest.CategoryName == null)
            {
                throw new ArgumentException(productCategoryAddRequest.CategoryName);
            }
            
            //if(_.ProductCategories.Where(temp => temp.CategoryName == productCategoryAddRequest.CategoryName).Count()>0)
            //{
            //    throw new ArgumentException("Category Name Already Exists");
            //}

            ProductCategories productCategories = productCategoryAddRequest.ToProductCategories();
            
           await _productCategoryRepository.AddCategory(productCategories);

            //_db.SaveChanges();

            return productCategories.ToProductCategoryResponse();
        }


       
    }
}
