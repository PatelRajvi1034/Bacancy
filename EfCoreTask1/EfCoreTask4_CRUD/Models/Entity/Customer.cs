using System.ComponentModel.DataAnnotations;

namespace EfCoreTask4.Models.Entity
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool IsVIP { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
