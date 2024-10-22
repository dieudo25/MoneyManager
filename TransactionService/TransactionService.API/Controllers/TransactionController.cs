using Common.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using TransactionService.Database.Repositories;
using TransactionService.Domain.Helpers;
using TransactionService.Domain.Interfaces;
using TransactionService.Domain.Models;

namespace TransactionService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ITransactionRepository transactionRepository, ILogger<TransactionController> logger)
        {
            _transactionRepository = transactionRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            _logger.LogInformation("Fetch All transactions");
           
            var transactions = await _transactionRepository.GetAllTransactionsAsync();

            _logger.LogInformation($"Number of transactions: {transactions.Count()}");

            return Ok(transactions);
        }

        [HttpGet("account/{id}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionByAccountId(Guid id)
        {
            _logger.LogInformation($"Fetch transactions of account: {id}");

            var transactions = await _transactionRepository.GetTransactionByAccountIdAsync(id);

            _logger.LogInformation($"Number of transactions: {transactions.Count()}");

            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransactionById(Guid id)
        {
            _logger.LogInformation($"Fetch transaction: {id}");

            var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
            
            if (transaction == null)
            {
                _logger.LogError($"Transaction '{id}' not found");
                return NotFound();
            }

            _logger.LogInformation($"Transaction '{id}' fetched successfully");

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult> AddTransaction(Transaction transaction)
        {
            _logger.LogInformation("Add new transaction: {@Transaction}", transaction);
            
            await _transactionRepository.AddTransactionAsync(transaction);
            
            _logger.LogInformation("Transaction added successfully");

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTransaction(Transaction transaction)
        {
            _logger.LogInformation($"Update transaction: {transaction.Id}");
            
            if (transaction == null)
            {
                _logger.LogError($"Transaction is not found");
                return BadRequest();
            }

            await _transactionRepository.UpdateTransactionAsync(transaction);

            _logger.LogInformation("Transaction updated successfully");

            return Ok(transaction);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTransaction(Guid id)
        {
            _logger.LogInformation($"Delete transaction: {id}");
            
            await _transactionRepository.DeleteTransactionAsync(id);
            
            _logger.LogInformation($"Transaction '{id}' deleted successfully");

            return NoContent();
        }
    }
}
