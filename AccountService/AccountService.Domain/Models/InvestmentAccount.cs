using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Models
{
    public class InvestmentAccount : Account
    {
        public decimal OldBalance { get; private set; }

        public void ManualBalanceUpdate(decimal newBalance)
        {
            OldBalance = Balance; 
            Balance = newBalance;
        }

        public decimal CalculatePercentageChange()
        {
            return OldBalance != 0 ? ((Balance - OldBalance) / OldBalance) * 100 : 0;
        }
    }
}
