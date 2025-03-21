using EfCoreTask4.Data;
using EfCoreTask4.Models.DTOs;
using Microsoft.EntityFrameworkCore;


namespace EfCoreTask4.Services
{
    public class ExplicitLoadingService : IExplicitLoadingService
    {
        private readonly ApplicationDbContext _context;

        public ExplicitLoadingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public CustomerDTO? GetCustomerWithOrders(int customerId)
        {
            var customer = _context.Customers.Find(customerId);

            if (customer == null)
                return null;

            if (customer.CreatedDate >= DateTime.UtcNow.AddYears(-1))
            {
                _context.Entry(customer).Collection(c => c.Orders).Load();
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
                    CustomerId = o.CustomerId
                }).ToList()
            };
        }

        public List<OrderDTO> GetOrdersWithoutOrderProducts()
        {
            var orders = _context.Orders.ToList();

            return orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                IsDeleted = o.IsDeleted,
                CustomerId = o.CustomerId
            }).ToList();
        }

        public List<ProductDTO> GetProductsWithConditionalOrders()
        {
            var products = _context.Products.ToList();

            return products.Select(p =>
            {
                if (p.Stock < 10)
                {
                    _context.Entry(p).Collection(p => p.OrderProducts).Load();
                }

                return new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    OrderProducts = p.Stock < 10
                        ? p.OrderProducts.Select(op => new OrderProductDTO
                        {
                            OrderId = op.OrderId,
                            ProductId = op.ProductId,
                            Quantity = op.Quantity
                        }).ToList()
                        : null
                };
            }).ToList();
        }

        public List<CustomerDTO> GetCustomersWithOrdersAndLoadOrderProducts()
        {
            var customers = _context.Customers.Include(c => c.Orders).ToList();

            foreach (var customer in customers)
            {
                foreach (var order in customer.Orders)
                {
                    _context.Entry(order).Collection(o => o.OrderProducts).Load();
                }
            }

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
                        ProductId = op.ProductId,
                        Quantity = op.Quantity
                    }).ToList()
                }).ToList()
            }).ToList();
        }
    }
}