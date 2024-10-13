using Common.Domain.Enums;
using Newtonsoft.Json;

namespace CategoryService.Domain.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
