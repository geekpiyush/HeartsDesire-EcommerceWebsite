using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace HeartsDesireLuxury.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly IProductCategoryAdderService _productCategoryAdderService;
        private readonly IProductCategoryGetterService _productCategoryGetterService;
        
        public CategoriesController(IProductCategoryAdderService productCategoryService,IProductCategoryGetterService productCategoryGetterService)
        {
            _productCategoryAdderService = productCategoryService;
            _productCategoryGetterService = productCategoryGetterService;
        }

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            List<ProductCategoryResponse> productCategoryResponses = await _productCategoryGetterService.GetAllCategory();
            return View( productCategoryResponses);
        }

        [HttpGet]
        public IActionResult AddCategories()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddCategoriesAsync(ProductCategoryAddRequest productCategoryAddRequest)
        {
            ProductCategoryResponse productCategoryResponse = await _productCategoryAdderService.AddCategory(productCategoryAddRequest);

            return RedirectToAction("Categories", "Categories");
        }
    }
}
