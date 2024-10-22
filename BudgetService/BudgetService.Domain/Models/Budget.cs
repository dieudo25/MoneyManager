using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetService.Domain.Models
{
    public class Budget
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid CategoryId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
