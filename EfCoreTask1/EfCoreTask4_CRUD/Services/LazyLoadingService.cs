using EfCoreTask4.Data;
using EfCoreTask4.Models.DTOs;
using Microsoft.EntityFrameworkCore;


namespace EfCoreTask4.Services
{
    public class LazyLoadingService : ILazyLoadingService
    {
        private readonly ApplicationDbContext _context;

        public LazyLoadingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CustomerDTO> GetCustomersWithOrders()
        {
            var customers = _context.Customers.ToList();

            return customers.Select(c => new CustomerDTO
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                CreatedDate = c.CreatedDate,
                Orders = c.Orders.Select(o => new OrderDTO
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    IsDeleted = o.IsDeleted,
                    CustomerId = o.CustomerId,
                    OrderProducts = o.OrderProducts.Select(op => new OrderProductDTO
                    {
                        Id = op.Id,
                        OrderId = op.OrderId,
                        ProductId = op.ProductId,
                        Quantity = op.Quantity
                    }).ToList()
                }).ToList()
            }).ToList();
        }

        public List<CustomerDTO> GetHighValueCustomers()
        {
            var customers = _context.Customers.ToList();

            return customers
                .Select(c => new CustomerDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    CreatedDate = c.CreatedDate,
                    Orders = c.Orders
                        .Where(o => o.OrderProducts.Sum(op => op.Quantity * op.Product.Price) > 500)
                        .Select(o => new OrderDTO
                        {
                            Id = o.Id,
                            OrderDate = o.OrderDate,
                            IsDeleted = o.IsDeleted,
                            CustomerId = o.CustomerId,
                            OrderProducts = o.OrderProducts.Select(op => new OrderProductDTO
                            {
                                Id = op.Id,
                                OrderId = op.OrderId,
                                ProductId = op.ProductId,
                                Quantity = op.Quantity
                            }).ToList()
                        }).ToList()
                })
                .Where(c => c.Orders.Any())
                .ToList();
        }
    }
}
