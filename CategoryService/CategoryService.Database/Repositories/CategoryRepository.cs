using CategoryService.Database.Context;
using CategoryService.Domain.Interfaces;
using CategoryService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryService.Database.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDbContext? _dbContext;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(CategoryDbContext dbContext, ILogger<CategoryRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            var category = await _dbContext.Categories.FindAsync(categoryId);

            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Category {categoryId} deleted successfully.");
            }
            else
            {
                _logger.LogWarning($"User {categoryId} not found. Delete failed.");
                throw new NullReferenceException($"Category {{{categoryId}}} not found. Delete failed");
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(Guid categoryId)
        {
            return await _dbContext.Categories.SingleOrDefaultAsync(t => t.Id == categoryId);
        }

        public Task UpdateCategoryAsync(Category category)
        {
            var existingCategory = _dbContext.Categories.FirstOrDefault(t => t.Id == category.Id);

            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                existingCategory.Description = category.Description;
                existingCategory.TransactionType = category.TransactionType;
            }

            return Task.CompletedTask;
        }
    }
}
