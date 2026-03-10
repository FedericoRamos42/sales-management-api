using Application.Services.Categories;
using Application.Services.Categories.Models;
using Application.Services.Categories.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryUseCases _categoryService;
        public CategoryController(CategoryUseCases categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAll.Execute();
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryForRequest request)
        {
            var result = await _categoryService.CreateCategory.Execute(request);
            if (!result.IsSucces)
            {
                return BadRequest(new { errors = result.Errors });
            }
            return CreatedAtAction(
                    nameof(GetAll),
                     new { id = result.Value!.Id },
                    result.Value
                    );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateCategoryForRequest request)
        {
            var result = await _categoryService.Update.Execute(id, request);
            if(!result.IsSucces) 
                return BadRequest(new { errors = result.Errors });

            return Ok(result);
        }

        






    }
}
