using CategoryService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryService.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriessAsync();
        Task<Category> GetCategoryByIdAsync(Guid categoryId);
        Task AddCategoryAsync(Category category);
        Task DeleteCategoryAsync(Guid categoryId);
        Task UpdateCategoryAsync(Category category);
    }
}
