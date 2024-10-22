using AccountService.Domain.Interfaces;
using AccountService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountRepository accountRepository, ILogger<AccountController> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            _logger.LogDebug("Fetch All accounts");

            var accounts = await _accountRepository.GetAllAccountsAsync();

            _logger.LogDebug($"Number of accounts: {accounts.Count()}");

            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccountById(Guid id)
        {
            _logger.LogInformation($"Fetch account: {id}");

            var account = await _accountRepository.GetAccountByIdAsync(id);

            if (account == null)
            {
                _logger.LogError($"Account '{id}' not found");
                return NotFound();
            }

            _logger.LogInformation($"Account '{id}' fetched successfully");

            return Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult> AddAccount(Account account)
        {
            _logger.LogDebug("Account received: {@Account}", account);

            await _accountRepository.AddAccountAsync(account);

            _logger.LogInformation("Account added successfully");

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAccount(Account account)
        {
            _logger.LogDebug("Update account: {@Account}", account);

            if (account == null)
            {
                _logger.LogError($"Account to update is not found");
                return BadRequest();
            }

            _logger.LogInformation("Account updated successfully");

            await _accountRepository.UpdateAccountAsync(account);

            return Ok(account);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAccount(Guid id)
        {
            _logger.LogDebug($"Delete account {id}");

            await _accountRepository.DeleteAccountAsync(id);

            _logger.LogInformation($"Account '{id}' deleted successfully");

            return NoContent();
        }
    }
}
