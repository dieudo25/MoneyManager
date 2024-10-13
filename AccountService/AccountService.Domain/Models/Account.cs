using AccountService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionService.Domain.Models;

namespace AccountService.Domain.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public AccountTypeEnum AccountType { get; set; }

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public void UpdateBalance()
        {
            Balance = Transactions.Sum(t => t.Amount);
        }
    }
}
