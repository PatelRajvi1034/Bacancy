using EfCoreTask4.Models.DTOs;

namespace EfCoreTask4.Services
{
    public interface IEagerLoadingService
    {
        List<CustomerDTO> GetCustomersWithOrdersAndOrderProducts();
        List<CustomerDTO> GetRecentCustomersWithOrdersAndOrderProducts();
        List<ProductOrderCountDTO> GetProductsWithOrderCount();
        List<OrderSummaryDTO> GetOrdersPlacedInLastMonth();
    }
}
