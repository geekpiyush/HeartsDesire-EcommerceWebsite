using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface IProductUpdateServices
    {
        public Task<ProductResponse> UpdateProduct(ProductUpdateRequest? productUpdateRequest);

      

    }
}
