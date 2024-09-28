using Microsoft.AspNetCore.Mvc;
using TransactionService.Data.Repositories;
using TransactionService.Domain.Interfaces;
using TransactionService.Domain.Models;

namespace TransactionService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        public readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ITransactionRepository transactionRepository, ILogger<TransactionController> logger)
        {
            _transactionRepository = transactionRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            _logger.LogDebug("Fetch All transactions");
            var transactions = await _transactionRepository.GetAllTransactionsAsync();
            _logger.LogDebug($"Number of transactions: {transactions.Count()}");

            return Ok(transactions);
        }

        [HttpPost]
        public async Task<ActionResult> AddTransaction(Transaction transaction)
        {
            _logger.LogDebug("Transaction received: {@Transaction}", transaction);
            await _transactionRepository.AddTransactionAsync(transaction);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTransaction(int id, Transaction transaction)
        {
            _logger.LogDebug("Update transaction {id}: {@Transaction}", id, transaction);
            if (transaction == null)
            {
                _logger.LogError($"Transaction to update is null");
                return BadRequest();
            }

            await _transactionRepository.UpdateTransactionAsync(transaction);
            return Ok(transaction);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTransaction(int id)
        {
            _logger.LogDebug($"Delete transaction {id}");
            await _transactionRepository.DeleteTransactionAsync(id);
            return NoContent();
        }
    }
}
