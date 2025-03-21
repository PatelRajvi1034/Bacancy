using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EfCoreTask4.Models.Entity
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("Customer"), Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
