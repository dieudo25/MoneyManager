using BudgetService.Domain.Interfaces;
using BudgetService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : Controller
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly ILogger<BudgetController> _logger;

        public BudgetController(IBudgetRepository budgetRepository, ILogger<BudgetController> logger)
        {
            _budgetRepository = budgetRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Budget>>> GetBudgets()
        {
            _logger.LogDebug("Fetch All budgets");

            var budgets = await _budgetRepository.GetAllBudgetsAsync();

            _logger.LogDebug($"Number of budgets: {budgets.Count()}");

            return Ok(budgets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Budget>> GetBudgetById(Guid id)
        {
            _logger.LogInformation($"Fetch budget: {id}");

            var budget = await _budgetRepository.GetBudgetByIdAsync(id);

            if (budget == null)
            {
                _logger.LogError($"Budget '{id}' not found");
                return NotFound();
            }

            _logger.LogInformation($"Budget '{id}' fetched successfully");

            return Ok(budget);
        }

        [HttpPost]
        public async Task<ActionResult> AddBudget(Budget budget)
        {
            _logger.LogDebug("Budget received: {@Budget}", budget);

            await _budgetRepository.AddBudgetAsync(budget);

            _logger.LogInformation("Budget added successfully");

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBudget(Budget budget)
        {
            _logger.LogDebug("Update budget: {@Budget}", budget);

            if (budget == null)
            {
                _logger.LogError($"Budget to update is not found");
                return BadRequest();
            }

            _logger.LogInformation("Budget updated successfully");

            await _budgetRepository.UpdateBudgetAsync(budget);

            return Ok(budget);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBudget(Guid id)
        {
            _logger.LogDebug($"Delete budget {id}");

            await _budgetRepository.DeleteBudgetAsync(id);

            _logger.LogInformation($"Budget '{id}' deleted successfully");

            return NoContent();
        }
    }
}
