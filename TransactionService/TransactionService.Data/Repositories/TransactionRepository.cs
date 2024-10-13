using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionService.Data.Context;
using TransactionService.Domain.Interfaces;
using TransactionService.Domain.Models;

namespace TransactionService.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionDbContext? _dbContext;
        private readonly ILogger<TransactionRepository> _logger;

        public TransactionRepository(TransactionDbContext dbContext, ILogger<TransactionRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _dbContext.Transactions.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(Guid transactionId)
        {
            var transaction = await _dbContext.Transactions.FindAsync(transactionId);

            if (transaction != null)
            {
                _dbContext.Transactions.Remove(transaction);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Transaction {transactionId} deleted successfully.");
            }
            else
            {
                _logger.LogWarning($"User {transactionId} not found. Delete failed.");
                throw new NullReferenceException($"Transaction {{{transactionId}}} not found. Delete failed");
            }
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _dbContext.Transactions.ToListAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(Guid transactionId)
        {
            return await _dbContext.Transactions.SingleOrDefaultAsync(t => t.Id == transactionId);
        }

        public Task UpdateTransactionAsync(Transaction transaction)
        {
            var existingTransaction = _dbContext.Transactions.FirstOrDefault(t => t.Id == transaction.Id);

            if (existingTransaction != null)
            {
                existingTransaction.Description = transaction.Description;
                existingTransaction.Amount = transaction.Amount;
                existingTransaction.Date = transaction.Date;
            }

            return Task.CompletedTask;
        }
    }
}
