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

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        [HttpPost]
        public async Task<ActionResult> AddTransaction(Transaction transaction)
        {
            await _transactionRepository.AddTransactionAsync(transaction);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTransaction(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            await _transactionRepository.UpdateTransactionAsync(transaction);
            return Ok(transaction);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTransaction(int id)
        {
            await _transactionRepository.DeleteTransactionAsync(id);
            return NoContent();
        }
    }
}
