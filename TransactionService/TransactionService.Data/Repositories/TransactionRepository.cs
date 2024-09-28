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
            if (_dbContext != null)
            {
                _dbContext = dbContext;
            }
            else
            {
                throw new NullReferenceException("Transaction Db Context is null");
            }
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
    }
}
