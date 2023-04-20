using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VpApi.DTOs;
using VpBusinessLogic.Services;
using VpDataAccess.Models;

namespace VpApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDTO productDto)
        {
            try
            {
                if (productDto == null)
                {
                    return BadRequest("Product data is required.");
                }

                if (string.IsNullOrEmpty(productDto.Name))
                {
                    return BadRequest("Product name is required.");
                }

                var product = new Product
                {
                    Name = productDto.Name,
                    Price = productDto.Price
                };

                _productService.AddProduct(product);
                return Ok("Product successfully created.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _productService.GetAllProducts();
                var productDtos = products.Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                });

                return Ok(productDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
