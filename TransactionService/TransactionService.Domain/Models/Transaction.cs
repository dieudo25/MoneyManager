using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionService.Domain.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public override string ToString()
        {
            // Serialize the current object to JSON
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
