using BudgetService.Database.Context;
using BudgetService.Domain.Interfaces;
using BudgetService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetService.Database.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly BudgetDbContext? _dbContext;
        private readonly ILogger<BudgetRepository> _logger;

        public BudgetRepository(BudgetDbContext dbContext, ILogger<BudgetRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task AddBudgetAsync(Budget budget)
        {
            await _dbContext.Budgets.AddAsync(budget);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBudgetAsync(Guid id)
        {
            var budget = await _dbContext.Budgets.FindAsync(id);

            if (budget != null)
            {
                _dbContext.Budgets.Remove(budget);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new NullReferenceException($"Budget {{{id}}} not found. Delete failed");
            }
        }

        public async Task<IEnumerable<Budget>> GetAllBudgetsAsync()
        {
            return await _dbContext.Budgets.ToListAsync();
        }

        public async Task<Budget> GetBudgetByIdAsync(Guid id)
        {
            return await _dbContext.Budgets.SingleOrDefaultAsync(t => t.Id == id);
        }

        public Task UpdateBudgetAsync(Budget budget)
        {
            var existingBudget = _dbContext.Budgets.FirstOrDefault(t => t.Id == budget.Id);

            if (existingBudget != null)
            {
                existingBudget.Name = budget.Name;
                existingBudget.Description = budget.Description;
                existingBudget.Amount = budget.Amount;
                existingBudget.CategoryId = budget.CategoryId;
                existingBudget.StartDate = budget.StartDate;
                existingBudget.EndDate = budget.EndDate;
            }

            return Task.CompletedTask;
        }
    }
}
