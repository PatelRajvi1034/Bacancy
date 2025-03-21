using EfCoreTask4.Models.DTOs;

namespace EfCoreTask4.Services
{
    public interface IExplicitLoadingService
    {
        CustomerDTO? GetCustomerWithOrders(int customerId);
        List<OrderDTO> GetOrdersWithoutOrderProducts();
        List<ProductDTO> GetProductsWithConditionalOrders();
        List<CustomerDTO> GetCustomersWithOrdersAndLoadOrderProducts();
    }
}
