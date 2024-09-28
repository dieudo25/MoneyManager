using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionService.Data.Data;
using TransactionService.Domain.Interfaces;
using TransactionService.Domain.Models;

namespace TransactionService.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionDbContext? _dbContext;

        public TransactionRepository(TransactionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _dbContext.Transactions.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _dbContext.Transactions.FindAsync(id);

            if (transaction != null)
            {
                _dbContext.Transactions.Remove(transaction);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new NullReferenceException($"Transaction '{id}' not found. Delete is impossible");
            }
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _dbContext.Transactions.ToListAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await _dbContext.Transactions.FindAsync(id);
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
