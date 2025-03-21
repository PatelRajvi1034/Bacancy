namespace EfCoreTask4.Models.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsDeleted { get; set; }
        public int CustomerId { get; set; }
        public CustomerDTO Customer { get; set; }
        public List<OrderProductDTO> OrderProducts { get; set; } = new List<OrderProductDTO>();
    }
}
