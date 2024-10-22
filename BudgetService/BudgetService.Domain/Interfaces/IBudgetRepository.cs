using BudgetService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetService.Domain.Interfaces
{
    public interface IBudgetRepository
    {
        Task<IEnumerable<Budget>> GetAllBudgetsAsync();
        Task<Budget> GetBudgetByIdAsync(Guid categoryId);
        Task AddBudgetAsync(Budget category);
        Task DeleteBudgetAsync(Guid categoryId);
        Task UpdateBudgetAsync(Budget category);
    }
}
