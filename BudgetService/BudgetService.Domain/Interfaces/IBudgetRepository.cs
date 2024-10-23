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
        Task<Budget> GetBudgetByIdAsync(Guid budgetId);
        Task AddBudgetAsync(Budget budget);
        Task DeleteBudgetAsync(Guid budgetId);
        Task UpdateBudgetAsync(Budget budget);
    }
}
