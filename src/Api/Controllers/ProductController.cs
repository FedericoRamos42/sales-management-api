using Application.Services.Producto;
using Application.Services.Products.Models.Request;
using Application.Utils.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductUseCases _productService;
        public ProductController(ProductUseCases productService)
        {
            _productService = productService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _productService.GetProduct.Execute(id);

            if(!result.IsSucces)
                return NotFound(new { errors = result.Errors });

            return Ok(result.Value);
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetByPagination([FromQuery] PaginationParams request)
        {
            var result = await _productService.GetByPagination.Execute(request);
            if (!result.IsSucces)
            {
                return BadRequest(new { errors = result.Errors });
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {
            var result = await _productService.CreateProduct.Execute(request);
            if (!result.IsSucces) 
                return BadRequest(new {errors =  result.Errors});
            return CreatedAtAction(nameof(Get), new { id = result.Value!.Id }, result.Value);
        }

        [HttpPut ("{id}/stock")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] UpdateStockRequest request)
        {
            var result = await _productService.UpdateProductStock.Execute(id, request);
            if(!result.IsSucces)
                return BadRequest(new { errors = result.Errors });
            return Ok(result.Value);
        }

        [HttpPut ("{id}/price")]
        public async Task<IActionResult> UpdatePrice(int id, [FromBody] UpdatePriceRequest request)
        {
            var result = await _productService.UpdateProductPrice.Execute(id, request);
            if(!result.IsSucces)
                return BadRequest(new {errors = result.Errors});
            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteProduct.Execute(id);
            if(!result.IsSucces)
                return BadRequest(result.Errors);
            return NoContent();
        }
        
    }
}
