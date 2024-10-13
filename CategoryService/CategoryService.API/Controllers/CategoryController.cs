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
        public async Task<ActionResult<Category>> GetCategoryById(Guid Id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(Id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(Category category)
        {
            _logger.LogDebug("Category received: {@Category}", category);

            await _categoryRepository.AddCategoryAsync(category);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(Category category)
        {
            _logger.LogDebug("Update category: {@Category}", category);

            if (category == null)
            {
                _logger.LogError($"Category to update is null");
                return BadRequest();
            }

            await _categoryRepository.UpdateCategoryAsync(category);

            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(Guid categoryId)
        {
            _logger.LogDebug($"Delete category {categoryId}");

            await _categoryRepository.DeleteCategoryAsync(categoryId);

            return NoContent();
        }
    }
}
