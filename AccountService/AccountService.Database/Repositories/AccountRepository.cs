using AccountService.Database.Context;
using AccountService.Domain.Interfaces;
using AccountService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Database.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDbContext? _dbContext;
        private readonly ILogger<AccountRepository> _logger;

        public AccountRepository(AccountDbContext dbContext, ILogger<AccountRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task AddAccountAsync(Account account)
        {
            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(Guid id)
        {
            var account = await _dbContext.Accounts.FindAsync(id);

            if (account != null)
            {
                _dbContext.Accounts.Remove(account);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new NullReferenceException($"Account {{{id}}} not found. Delete failed");
            }
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _dbContext.Accounts.ToListAsync();
        }

        public async Task<Account> GetAccountByIdAsync(Guid id)
        {
            return await _dbContext.Accounts.SingleOrDefaultAsync(t => t.Id == id);
        }

        public Task UpdateAccountAsync(Account account)
        {
            var existingAccount = _dbContext.Accounts.FirstOrDefault(t => t.Id == account.Id);

            if (existingAccount != null)
            {
                existingAccount.AccountType = account.AccountType;
            }

            return Task.CompletedTask;
        }
    }
}
