using Application.Services.Sales;
using Application.Services.Sales.Features;
using Application.Services.Sales.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly SaleUseCases _services;

        public SaleController(SaleUseCases services)
        {
            _services = services;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _services.GetSale.Execute(id);
            if (!result.IsSucces)
                return NotFound(new {errors = result.Errors});
            
            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _services.GetAllSale.Execute();
            return Ok(result.Value);
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            var fileBytes = await _services.ExportSales.Execute();

            return File(
                fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"ventas_{DateTime.UtcNow:yyyyMMdd_HHmm}.xlsx"
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSaleRequest request)
        {
            var result = await _services.CreateSale.Execute(request);
            if (!result.IsSucces) 
                return BadRequest(new { errors = result.Errors });
            return CreatedAtAction(
                nameof(Get), 
                new { id = result.Value!.Id }, 
                result.Value 
            );
        }

        
        
    }
}
