using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface IProductDeleteServices
    {

        public Task<bool> DeleteProduct(int? productID);


    }
}
