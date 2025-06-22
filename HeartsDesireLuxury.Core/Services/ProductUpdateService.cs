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
    public class ProductUpdateService : IProductUpdateServices
    {
        //private readonly ApplicationDbContext _db;
        private readonly IProductRepository _productRepository;
        //private readonly IProductCategoryService _productCategoryService;

        public ProductUpdateService( /*ApplicationDbContext applicationDbContext*/ /* IProductCategoryService productCategoryService,*/ IProductRepository productRepository)
        {

            //_db = applicationDbContext;
            //_productCategoryService = productCategoryService;
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> UpdateProduct(ProductUpdateRequest? productUpdateRequest)
        {
            if(productUpdateRequest == null)
            {
                return null;
            }

            ValidationHelper.ModelValidation(productUpdateRequest);

            Products? matchingProducts = await _productRepository.GetProductsById(productUpdateRequest.ProductID.Value);

            if(matchingProducts == null)
            {
                throw new ArgumentException("Given ID doesn't exist");
            }

            //Update Product Details
            matchingProducts.ProductPrice = productUpdateRequest.ProductPrice;
            matchingProducts.ProductSalePrice = productUpdateRequest.ProductSalePrice;
            matchingProducts.SkuID = productUpdateRequest.SkuID;
            matchingProducts.Stock = productUpdateRequest.Stock;
            matchingProducts.MainImagePath = productUpdateRequest.MainImagePath;
            matchingProducts.CategoryID = productUpdateRequest.CategoryID;
            
           await _productRepository.UpdateProduct(matchingProducts);
           
            return matchingProducts.ToProductResponse();
        }
    }
}
