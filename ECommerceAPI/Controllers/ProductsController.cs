using Microsoft.AspNetCore.Mvc;
using ECommerceAPI.Data;
using ECommerceAPI.Models;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // GET /api/products?search=&category=&minPrice=&maxPrice=&page=1&pageSize=8
        [HttpGet]
        public IActionResult GetProducts(
            string? search,
            string? category,
            decimal? minPrice,
            decimal? maxPrice,
            int page = 1,
            int pageSize = 8)
        {
            var query = ProductRepository.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

            if (minPrice.HasValue) query = query.Where(p => p.Price >= minPrice.Value);
            if (maxPrice.HasValue) query = query.Where(p => p.Price <= maxPrice.Value);

            var totalItems = query.Count();
            var products = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return Ok(new { totalItems, products });
        }

        // GET /api/products/{id}
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = ProductRepository.Products.FirstOrDefault(p => p.Id == id);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // BONUS: GET /api/products/{id}/related
        [HttpGet("{id}/related")]
        public IActionResult GetRelated(int id)
        {
            var product = ProductRepository.Products.FirstOrDefault(p => p.Id == id);
            if (product is null) return NotFound();

            var related = ProductRepository.Products
                .Where(p => p.Category == product.Category && p.Id != id)
                .Take(4)
                .ToList();

            return Ok(related);
        }
    }
}