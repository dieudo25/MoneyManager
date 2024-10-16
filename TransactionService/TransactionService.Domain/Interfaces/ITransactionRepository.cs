using Common.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionService.Domain.Models;

namespace TransactionService.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<Transaction> GetTransactionByIdAsync(Guid transactionId);
        Task<IEnumerable<TransactionDto>> GetTransactionByAccountIdAsync(Guid id);
        Task AddTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(Guid transactionId);
        Task UpdateTransactionAsync(Transaction transaction);
    }
}
