using Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    public class ProductUpdateRequest
    {
        [Required(ErrorMessage = "ProductID is required to update any product")]
        public int? ProductID { get; set; }

        public double ProductPrice { get; set; }
        public double? ProductSalePrice { get; set; }
        public int Stock { get; set; }
        public string? SkuID { get; set; }

        public string? MainImagePath { get; set; }
        public List<string>? ReferenceImagePaths { get; set; }

        public IFormFile? MainImage { get; set; }
        public List<IFormFile>? ReferenceImages { get; set; }

        public int? CategoryID { get; set; }

        public Products ToProduct()
        {
            return new Products()
            {
                ProductID = ProductID ?? 0,
                ProductPrice = ProductPrice,
                ProductSalePrice = ProductSalePrice,
                Stock = Stock,
                SkuID = SkuID,
                MainImagePath = MainImagePath,
                ReferenceImages = ReferenceImagePaths != null ? string.Join("\n", ReferenceImagePaths) : null,
                CategoryID = CategoryID
            };
        }
    }
}
