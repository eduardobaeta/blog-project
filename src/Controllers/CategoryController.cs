using Blog.Data;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.src.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
        {
            try
            {
                var categories = await context.Categories.AsNoTracking().ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch (Exception e)
            {
                return BadRequest(new ResultViewModel<List<Category>>("Internal server fail"));
            }
        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] BlogDataContext context, [FromRoute] int id)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return NotFound(new ResultViewModel<Category>("Category not found"));
                return Ok(new ResultViewModel<Category>(category));
            }
            catch (Exception e)
            {
                return BadRequest(new ResultViewModel<List<Category>>("Internal server fail"));
            }
        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync([FromServices] BlogDataContext context, [FromBody] EditorCategoryViewModel model)
        {
            try
            {
                var category = new Category { Name = model.Name, Slug = model.Name.ToLower() };
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
            }

            catch (DbUpdateException e)
            {
                return StatusCode(500, new ResultViewModel<Category>("Not was possible create this category"));
            }

            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<Category>("Internal server fail"));
            }
        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutByIdAsync([FromServices] BlogDataContext context, [FromBody] EditorCategoryViewModel model, [FromRoute] int id)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound(new ResultViewModel<Category>("Category not found"));

                category.Name = model.Name;
                category.Slug = model.Slug;
                context.Update(category);
                await context.SaveChangesAsync();
                return Ok(new ResultViewModel<Category>(category));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteByIdAsync([FromServices] BlogDataContext context, [FromRoute] int id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound(new ResultViewModel<Category>("Category not found"));
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return Ok(new ResultViewModel<Category>(category));
        }
    }
}