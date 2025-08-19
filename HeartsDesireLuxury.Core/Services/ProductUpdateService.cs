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
            if (productUpdateRequest == null)
            {
                return null;
            }

            ValidationHelper.ModelValidation(productUpdateRequest);

            Products? matchingProducts = await _productRepository.GetProductsById(productUpdateRequest.ProductID.Value);

            if (matchingProducts == null)
            {
                throw new ArgumentException("Given ID doesn't exist");
            }

            // Handle main image update
            if (productUpdateRequest.MainImage != null)
            {
                string mainImagePath = SaveImage(productUpdateRequest.MainImage, "MainImages");
                matchingProducts.MainImagePath = mainImagePath;
            }

            // Handle reference images update
            if (productUpdateRequest.ReferenceImages != null && productUpdateRequest.ReferenceImages.Count > 0)
            {
                List<string> imagePaths = new List<string>();
                foreach (var image in productUpdateRequest.ReferenceImages)
                {
                    string path = SaveImage(image, "ReferenceImages");
                    imagePaths.Add(path);
                }

                matchingProducts.ReferenceImages = string.Join("\n", imagePaths);
            }

            // Update other fields
            matchingProducts.ProductPrice = productUpdateRequest.ProductPrice;
            matchingProducts.ProductSalePrice = productUpdateRequest.ProductSalePrice;
            matchingProducts.SkuID = productUpdateRequest.SkuID;
            matchingProducts.Stock = productUpdateRequest.Stock;
            matchingProducts.CategoryID = productUpdateRequest.CategoryID;

            await _productRepository.UpdateProduct(matchingProducts);

            return matchingProducts.ToProductResponse();
        }

        private string SaveImage(IFormFile image, string folderName)
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/uploads/{folderName}");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = $"{Guid.NewGuid()}_{image.FileName}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return $"/uploads/{folderName}/{uniqueFileName}";
        }


    }
}
