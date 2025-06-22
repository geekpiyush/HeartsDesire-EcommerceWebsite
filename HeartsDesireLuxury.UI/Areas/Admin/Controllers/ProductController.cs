using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using System.Threading.Tasks;

namespace HeartsDesireLuxury.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductGetterServices _productGetterService;
        private readonly IProductDeleteServices _productDeleteService;
        private readonly IProductUpdateServices _productUpdateService;
        private readonly IProductAdderServices _productAdderService;
        private readonly IProductCategoryGetterService _productCategoryService;
        public ProductController(IProductGetterServices productGetterServices, IProductCategoryGetterService productCategoryService, IProductDeleteServices productDeleteService, IProductUpdateServices productUpdateService, IProductAdderServices productAdderService)
        {
            _productGetterService = productGetterServices;
            _productCategoryService = productCategoryService;
            _productDeleteService = productDeleteService;
            _productUpdateService = productUpdateService;
            _productAdderService = productAdderService;
        }
        public async Task<IActionResult> AllProduct()
        {
            List<ProductResponse> products =  await _productGetterService.GetAllProducts();

            return View(products);
        
        }

        [HttpGet]
          public async Task<IActionResult> AddProduct()
        {
            List<ProductCategoryResponse> productCategoryResponses = await _productCategoryService.GetAllCategory();

            ViewBag.ProductCategory = productCategoryResponses;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductAddRequest productAddRequest)
        {
           if(!ModelState.IsValid)
            {
                ViewBag.errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return View();
            }

           ProductResponse productResponse = await _productAdderService.AddProduct(productAddRequest); 

            return RedirectToAction("AllProduct","Product");
        }


        [HttpGet]
        [Route("[action]/{productID}")]
        public async Task<IActionResult> EditProduct(int productID)
        {
            ProductResponse productResponse = await  _productGetterService.GetProductByProductID(productID);
            
            if(productResponse ==null)
            {
                return RedirectToAction("AllProduct", "Product");
            }

            ProductUpdateRequest productUpdateRequest = productResponse.ToProductUpdateRequest();

            return View(productUpdateRequest);
        }


        [HttpPost]
        [Route("[action]/{productID}")]
        public async Task<IActionResult> EditProduct(ProductUpdateRequest productUpdateRequest)
        {
            ProductResponse productResponse =await _productGetterService.GetProductByProductID(productUpdateRequest.ProductID);

            if(productResponse == null)
            {
                return RedirectToAction("AllProdcut");
            }

            if(ModelState.IsValid)
            {
                ProductResponse updateProduct =await _productUpdateService.UpdateProduct(productUpdateRequest);

                return RedirectToAction("AllProduct");
            }
            else
            {
                ViewBag.errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return View(productResponse.ToProductUpdateRequest());
            }

        }

        [HttpGet]
        [Route("[action]/{productID}")]
        public async Task<IActionResult> DeleteProduct(int? productID)
        {
            ProductResponse productResponse = await _productGetterService.GetProductByProductID(productID);

            if(productResponse == null)
            {
                return RedirectToAction("AllProduct");
            }

            return View(productResponse);
        }


        [HttpPost]
        [Route("[action]/{productID}")]
        public async Task<IActionResult> DeleteProduct(ProductUpdateRequest productUpdateRequest)
        {
            ProductResponse productResponse = await _productGetterService.GetProductByProductID(productUpdateRequest.ProductID);

            if(productResponse == null)
            {
                return RedirectToAction("AllProduct");
            }

           await _productDeleteService.DeleteProduct(productUpdateRequest.ProductID);

            return RedirectToAction("AllProduct");
        }
    }
}
