using MultiTenant.Models;
using MultiTenant.Services.DTOs;

namespace MultiTenant.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product CreateProduct(CreateProductRequest request);

        bool DeleteProduct(int id);
    }
}
