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
    public class ProductDeleteService : IProductDeleteServices
    {
        //private readonly ApplicationDbContext _db;
        private readonly IProductRepository _productRepository;
        //private readonly IProductCategoryService _productCategoryService;

        public ProductDeleteService( /*ApplicationDbContext applicationDbContext*/ /* IProductCategoryService productCategoryService,*/ IProductRepository productRepository)
        {

            //_db = applicationDbContext;
            //_productCategoryService = productCategoryService;
            _productRepository = productRepository;
        }

        public async Task<bool> DeleteProduct(int? productID)
        {
            if(productID == null)
            {
                throw new ArgumentNullException(nameof(productID));
            }

            Products? product = await _productRepository.GetProductsById( productID.Value);

            if(product == null)
            {
                return false;
            }

            await _productRepository.DeleteProduct(productID.Value);

            //_db.Products.Remove(_db.Products.First( temp => temp.ProductID == productID));
            //_db.SaveChanges();

            return true;

        }

    }
}
