using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface IProductGetterServices
    {
       //public Task<ProductResponse> AddProduct(ProductAddRequest? productAddRequest);

       public Task<List<ProductResponse>> GetAllProducts();

       public Task<ProductResponse> GetProductByProductID(int? personID);

       //public Task< ProductResponse>UpdateProduct(ProductUpdateRequest? productUpdateRequest);

       //public Task< bool> DeleteProduct(int? productID);
       public Task< List<ProductResponse>> GetProductsByCategoryID(int categoryID);

       public Task< List<ProductResponse>> GetTopProducts(int count);

    }
}
