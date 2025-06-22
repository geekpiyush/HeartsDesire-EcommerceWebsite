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
    public class ProductAdderService : IProductAdderServices
    {
        //private readonly ApplicationDbContext _db;
        private readonly IProductRepository _productRepository;
        //private readonly IProductCategoryService _productCategoryService;

        public ProductAdderService( /*ApplicationDbContext applicationDbContext*/ /* IProductCategoryService productCategoryService,*/ IProductRepository productRepository)
        {

            //_db = applicationDbContext;
            //_productCategoryService = productCategoryService;
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> AddProduct(ProductAddRequest? productAddRequest)
        {
            if (productAddRequest == null)
            {
                throw new ArgumentNullException(nameof(productAddRequest));
            }

            ValidationHelper.ModelValidation(productAddRequest);

            Products product = productAddRequest.ToProducts();

            // Save Main Image in "MainImages" folder
            if (productAddRequest.MainImage != null)
            {
                string mainImagePath = SaveImage(productAddRequest.MainImage, "MainImages");
                product.MainImagePath = mainImagePath;
            }

            // Save Reference Images in "ReferenceImages" folder
            if (productAddRequest.ReferenceImages != null && productAddRequest.ReferenceImages.Count > 0)
            {
                List<string> imagePaths = new List<string>();
                foreach (var image in productAddRequest.ReferenceImages)
                {
                    string path = SaveImage(image, "ReferenceImages");
                    imagePaths.Add(path);
                }

                product.ReferenceImages = string.Join("\n", imagePaths); 
            }

            ProductResponse productResponse =  product.ToProductResponse();
              //productResponse.Category = _productCategoryService.GetCategoryByCategoryID(productResponse.CategoryID)?.CategoryName;

           await _productRepository.AddProduct(product);
           

            return product.ToProductResponse();
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
