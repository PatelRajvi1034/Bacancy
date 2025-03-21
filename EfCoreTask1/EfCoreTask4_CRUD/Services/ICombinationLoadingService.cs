using EfCoreTask4.Models.DTOs;

namespace EfCoreTask4.Services
{
    public interface ICombinationLoadingService
    {
        List<OrderDTO> GetOrdersWithLazyOrderProducts();
        List<OrderDTO> GetOrdersWithConditionalOrderProducts();
        CustomerDTO? GetCustomerOrdersWithConditionalOrderProducts(int customerId);
    }
}
