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

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransactionById(Guid transactionId)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(transactionId);
            
            if (transaction == null)
            {
                return NotFound();
            }
            
            return Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult> AddTransaction(Transaction transaction)
        {
            _logger.LogDebug("Transaction received: {@Transaction}", transaction);
            
            await _transactionRepository.AddTransactionAsync(transaction);
            
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTransaction(Guid transactionId, Transaction transaction)
        {
            _logger.LogDebug("Update transaction {id}: {@Transaction}", transactionId, transaction);
            
            if (transaction == null || transactionId != transaction.Id)
            {
                _logger.LogError($"Transaction to update is null");
                return BadRequest();
            }

            await _transactionRepository.UpdateTransactionAsync(transaction);
           
            return Ok(transaction);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTransaction(Guid transactionId)
        {
            _logger.LogDebug($"Delete transaction {transactionId}");
            
            await _transactionRepository.DeleteTransactionAsync(transactionId);
            
            return NoContent();
        }
    }
}
