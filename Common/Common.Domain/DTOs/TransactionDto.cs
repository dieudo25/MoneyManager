using Common.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Dtos
{
    public class TransactionDto
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }

        public DateTime CreationDate { get; set; }

        public TransactionTypeEnum TransactionType { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
