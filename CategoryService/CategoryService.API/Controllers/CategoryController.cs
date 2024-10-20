using CategoryService.Domain.Interfaces;
using CategoryService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CategoryService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        public readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryRepository categoryRepository, ILogger<CategoryController> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            _logger.LogDebug("Fetch All categories");

            var categories = await _categoryRepository.GetAllCategoriesAsync();

            _logger.LogDebug($"Number of categories: {categories.Count()}");

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(Guid id)
        {
            _logger.LogInformation($"Fetch category: {id}");

            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                _logger.LogError($"Category '{id}' not found");
                return NotFound();
            }

            _logger.LogInformation($"Category '{id}' fetched successfully");

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(Category category)
        {
            _logger.LogDebug("Category received: {@Category}", category);

            await _categoryRepository.AddCategoryAsync(category);

            _logger.LogInformation("Category added successfully");

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(Category category)
        {
            _logger.LogDebug("Update category: {@Category}", category);

            if (category == null)
            {
                _logger.LogError($"Category to update is not found");
                return BadRequest();
            }

            _logger.LogInformation("Category updated successfully");

            await _categoryRepository.UpdateCategoryAsync(category);

            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            _logger.LogDebug($"Delete category {id}");

            await _categoryRepository.DeleteCategoryAsync(id);

            _logger.LogInformation($"Category '{id}' deleted successfully");

            return NoContent();
        }
    }
}
