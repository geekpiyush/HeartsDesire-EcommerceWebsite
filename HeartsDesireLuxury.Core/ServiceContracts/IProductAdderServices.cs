using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface IProductAdderServices
    {
       public Task<ProductResponse> AddProduct(ProductAddRequest? productAddRequest);

    }
}
