using Application.Services.Customers;
using Application.Services.Customers.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly CustomerUseCases _customerService;
        public CustomerController(CustomerUseCases customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _customerService.Get.Execute(id);
            if(!result.IsSucces)
                return  NotFound(new { errors = result.Errors });
            return Ok(result.Value);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> Search(string name)
        {
            var result = await _customerService.Search.Execute(name);

            if(!result.IsSucces)
                return BadRequest(new { errors = result.Errors });

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _customerService.GetAll.Execute();
            return Ok(result.Value);
        }

        [HttpGet("{id}/account/movements")]
        public async Task<IActionResult> GetWithAccountDetails([FromRoute] int id)
        {
            var result = await _customerService.GetCustomerAccount.Execute(id);

            if (!result.IsSucces)
                return NotFound(new {errors = result.Errors });

            return Ok(result.Value);
        }

        [HttpPost("{id}/account-movement")]
        public async Task<IActionResult> CreateMovement(int id,CreateMovementRequest request)
        {
            var result = await _customerService.CreateMovement.Execute(id, request);
            if (!result.IsSucces)
            {
                return BadRequest(new {errors = result.Errors});
            }
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerForRequest request)
        {
            var result = await _customerService.CreateCustomer.Execute(request);
            if (!result.IsSucces) 
                return BadRequest(new {errors =  result.Errors});
            return CreatedAtAction(nameof(Get), new {id = result.Value!.Id}, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] CustomerForRequest request)
        {
            var result = await _customerService.UpdateCustomer.Execute(id,request);
            if (!result.IsSucces)
                return BadRequest(new { errors = result.Errors });
            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerService.DeleteCustomer.Execute(id);
            if (!result.IsSucces) 
                return NotFound(new {errors = result.Errors});
            return NoContent();
        }

    }
}
