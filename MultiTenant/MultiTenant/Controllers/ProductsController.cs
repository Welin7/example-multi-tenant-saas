using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiTenant.Services;
using MultiTenant.Services.DTOs;

namespace MultiTenant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]

        public ActionResult GetAllProducts()
        {
            var listProducts = _productService.GetAllProducts();
            
            return Ok(listProducts);
        }

        [HttpPost]

        public ActionResult CreateProduct(CreateProductRequest request)
        {
            var product = _productService.CreateProduct(request);
            
            return Ok(product);
        }

        [HttpDelete]

        public ActionResult DeleteProduct(int id)
        {
            var result = _productService.DeleteProduct(id);
            return Ok(result);
        }
    }
}
