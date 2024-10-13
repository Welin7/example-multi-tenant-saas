using Microsoft.EntityFrameworkCore;
using MultiTenant.Models;
using MultiTenant.Services.DTOs;

namespace MultiTenant.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ProductService(ApplicationDbContext applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;       
        }
        public Product CreateProduct(CreateProductRequest request)
        {
            var product = new Product();
            
            product.Name = request.Name;
            product.Description = request.Description;
            
            _applicationDbContext.Products.Add(product);
            _applicationDbContext.SaveChanges();
            
            return product;
        }

        public bool DeleteProduct(int id)
        {
           var product = _applicationDbContext.Products.Where(x => x.Id == id).FirstOrDefault();

            if (product != null)
            {
                _applicationDbContext.Remove(product);
                _applicationDbContext.SaveChanges();
                
                return true;
            }

            return false;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _applicationDbContext.Products.ToList();
            return products;
        }
    }
}
