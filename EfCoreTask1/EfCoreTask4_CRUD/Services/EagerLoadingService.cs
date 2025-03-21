using EfCoreTask4.Data;
using EfCoreTask4.Models.DTOs;
using Microsoft.EntityFrameworkCore;


namespace EfCoreTask4.Services

{
    public class EagerLoadingService : IEagerLoadingService
    {
        private readonly ApplicationDbContext _context;

        public EagerLoadingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CustomerDTO> GetCustomersWithOrdersAndOrderProducts()
        {
            return _context.Customers
                .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderProducts)
                        .ThenInclude(op => op.Product)
                .Select(c => new CustomerDTO
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

        public List<CustomerDTO> GetRecentCustomersWithOrdersAndOrderProducts()
        {
            return _context.Customers
                .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderProducts)
                        .ThenInclude(op => op.Product)
                .Select(c => new CustomerDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    CreatedDate = c.CreatedDate,
                    Orders = c.Orders
                        .Where(o => o.OrderDate >= DateTime.Now.AddDays(-30))
                        .Select(o => new OrderDTO
                        {
                            Id = o.Id,
                            OrderDate = o.OrderDate,
                            IsDeleted = o.IsDeleted,
                            CustomerId = o.CustomerId,
                            OrderProducts = o.OrderProducts
                                .Where(op => op.Product.Stock > 20)
                                .Select(op => new OrderProductDTO
                                {
                                    Id = op.Id,
                                    OrderId = op.OrderId,
                                    ProductId = op.ProductId,
                                    Quantity = op.Quantity,
                                    Product = new ProductDTO
                                    {
                                        Id = op.Product.Id,
                                        Name = op.Product.Name,
                                        Price = op.Product.Price,
                                        Stock = op.Product.Stock
                                    }
                                }).ToList()
                        }).ToList()
                })
                .Where(c => c.Orders.Any())
                .ToList();
        }

        public List<ProductOrderCountDTO> GetProductsWithOrderCount()
        {
            return _context.Products
                .Include(p => p.OrderProducts)
                .Select(p => new ProductOrderCountDTO
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    TotalOrders = p.OrderProducts.Count
                })
                .ToList();
        }

        public List<OrderSummaryDTO> GetOrdersPlacedInLastMonth()
        {
            var oneMonthAgo = DateTime.Now.AddMonths(-1);

            return _context.Orders
                .Include(o => o.Customer)
                .Where(o => o.OrderDate >= oneMonthAgo)
                .Select(o => new OrderSummaryDTO
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    Customer = new CustomerBasicDTO
                    {
                        Id = o.Customer.Id,
                        Name = o.Customer.Name,
                        Email = o.Customer.Email
                    }
                })
                .ToList();
        }
    }


    public class ProductOrderCountDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int TotalOrders { get; set; }
    }

    public class OrderSummaryDTO
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public CustomerBasicDTO Customer { get; set; }
    }

    public class CustomerBasicDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
