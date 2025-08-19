using Entities;

namespace RepositoryContracts
{
    public interface IProductRepository
    {
        Task<Products> AddProduct(Products products);
        Task<List<Products>> GetAllProducts();
        Task<Products?> GetProductsById(int ProductID);
        Task<Products> UpdateProduct(Products products);
        Task<bool> DeleteProduct(int ProductID);
        Task<List<Products>> GetProductByCategoryID(int CategoryID);
        Task<List<Products>> GetTopProducts(int count);
    }
}
