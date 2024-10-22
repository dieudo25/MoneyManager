using Common.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionService.Domain.Models;

namespace TransactionService.Domain.Helpers
{
    public class MappingHelper
    {
        public static TransactionDto ToTransactionDto(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException($"{nameof(transaction)} is null, mapping to DTO object failed.");

            return new TransactionDto
            {
                Id = transaction.Id,
                AccountId = transaction.AccountId,
                Amount = transaction.Amount,
                CreationDate = transaction.CreationDate,
                Description = transaction.Description,
                TransactionType = transaction.TransactionType,
            };
        }

        public static Transaction ToTransactionModel(TransactionDto transactionDto)
        {
            if (transactionDto == null)
                throw new ArgumentNullException($"{nameof(transactionDto)} is null, mapping to model object failed.");

            return new Transaction
            {
                Id = transactionDto.Id,
                AccountId = transactionDto.AccountId,
                Amount = transactionDto.Amount,
                CreationDate = transactionDto.CreationDate,
                Description = transactionDto.Description,
                TransactionType = transactionDto.TransactionType,
            };
        }
    }
}
