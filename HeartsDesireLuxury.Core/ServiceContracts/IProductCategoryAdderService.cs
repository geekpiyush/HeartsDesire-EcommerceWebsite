using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IProductCategoryAdderService
    {
       public Task<ProductCategoryResponse> AddCategory(ProductCategoryAddRequest? productCategoryAddRequest);

    }
}
