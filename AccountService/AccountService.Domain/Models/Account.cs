using Common.Domain.Dtos;
using Common.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }

        public AccountTypeEnum AccountType { get; set; }

        public List<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
    }
}
