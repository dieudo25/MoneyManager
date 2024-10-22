using AccountService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account> GetAccountByIdAsync(Guid categoryId);
        Task AddAccountAsync(Account category);
        Task DeleteAccountAsync(Guid categoryId);
        Task UpdateAccountAsync(Account category);
    }
}
