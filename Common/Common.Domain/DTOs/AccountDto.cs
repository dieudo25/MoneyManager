using Common.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Dtos
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public AccountTypeEnum AccountType { get; set; }
    }
}
