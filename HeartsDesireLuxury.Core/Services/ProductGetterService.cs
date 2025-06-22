using Entities;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Services
{
    public class ProductGetterService : IProductGetterServices
    {
        //private readonly ApplicationDbContext _db;
        private readonly IProductRepository _productRepository;
        //private readonly IProductCategoryService _productCategoryService;

        public ProductGetterService( /*ApplicationDbContext applicationDbContext*/ /* IProductCategoryService productCategoryService,*/ IProductRepository productRepository)
        {

            //_db = applicationDbContext;
            //_productCategoryService = productCategoryService;
            _productRepository = productRepository;
        }


        public async Task<List<ProductResponse>> GetAllProducts()
        {
          return (await _productRepository.GetAllProducts())
                .Select(temp => temp.ToProductResponse())
                .ToList(); 
        }

        public async Task<ProductResponse> GetProductByProductID(int? productID)
        {
            if(productID == null)
            {
                return null;
            }
           Products? products = await  _productRepository.GetProductsById( productID.Value);

            if(products == null)
            {
                return null;
            }
            return products.ToProductResponse();
        }


        // get product by productCategoryID
        public async Task<List<ProductResponse>> GetProductsByCategoryID(int categoryID)
        {
            var products = await _productRepository.GetProductByCategoryID(categoryID);

            var productResponses = products
                .Select(p => p.ToProductResponse())
                .ToList();

            return productResponses;
        }


        public async Task<List<ProductResponse>> GetTopProducts(int count)
        {
            var products = await _productRepository.GetTopProducts(count);

            return products.Select(p => p.ToProductResponse()).ToList();
        }

    }
}
