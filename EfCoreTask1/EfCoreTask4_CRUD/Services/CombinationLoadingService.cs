using EfCoreTask4.Data;
using EfCoreTask4.Models.DTOs;
using Microsoft.EntityFrameworkCore;


namespace EfCoreTask4.Services
{
    public class CombinationLoadingService : ICombinationLoadingService
    {
        private readonly ApplicationDbContext _context;

        public CombinationLoadingService(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Fetch Orders with eager loading, but OrderProducts are lazily loaded only when accessed.
        public List<OrderDTO> GetOrdersWithLazyOrderProducts()
        {
            var orders = _context.Orders
                .Include(o => o.Customer) // Eager loading for Customer
                .ToList(); // Orders are fetched, but OrderProducts are not loaded yet.

            return orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                IsDeleted = o.IsDeleted,
                CustomerId = o.CustomerId,
                OrderProducts = null // OrderProducts will be lazily loaded when accessed
            }).ToList();
        }

        // 2. Fetch Orders eagerly, but explicitly load OrderProducts where Quantity > 5.
        public List<OrderDTO> GetOrdersWithConditionalOrderProducts()
        {
            var orders = _context.Orders
                .Include(o => o.Customer) // Eager loading for Customer
                .ToList();

            foreach (var order in orders)
            {
                _context.Entry(order)
                    .Collection(o => o.OrderProducts)
                    .Query()
                    .Where(op => op.Quantity > 5)
                    .Load(); // Explicitly load only if Quantity > 5
            }

            return orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                IsDeleted = o.IsDeleted,
                CustomerId = o.CustomerId,
                OrderProducts = o.OrderProducts
                    .Where(op => op.Quantity > 5)
                    .Select(op => new OrderProductDTO
                    {
                        Id = op.Id,
                        OrderId = op.OrderId,
                        ProductId = op.ProductId,
                        Quantity = op.Quantity
                    }).ToList()
            }).ToList();
        }

        // 3. Retrieve a Customer’s Orders eagerly and explicitly load OrderProducts only if the Customer is marked as "VIP".
        public CustomerDTO? GetCustomerOrdersWithConditionalOrderProducts(int customerId)
        {
            var customer = _context.Customers
                .Include(c => c.Orders) // Eagerly load Orders
                .FirstOrDefault(c => c.Id == customerId);

            if (customer == null)
                return null;

            if (customer.IsVIP) // Only load OrderProducts explicitly for VIP customers
            {
                foreach (var order in customer.Orders)
                {
                    _context.Entry(order).Collection(o => o.OrderProducts).Load();
                }
            }

            return new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                CreatedDate = customer.CreatedDate,
                Orders = customer.Orders.Select(o => new OrderDTO
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    IsDeleted = o.IsDeleted,
                    CustomerId = o.CustomerId,
                    OrderProducts = customer.IsVIP
                        ? o.OrderProducts.Select(op => new OrderProductDTO
                        {
                            Id = op.Id,
                            OrderId = op.OrderId,
                            ProductId = op.ProductId,
                            Quantity = op.Quantity
                        }).ToList()
                        : null
                }).ToList()
            };
        }
    }

}
