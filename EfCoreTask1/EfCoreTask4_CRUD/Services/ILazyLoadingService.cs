using EfCoreTask4.Models.DTOs;

namespace EfCoreTask4.Services
{
    public interface ILazyLoadingService
    {
        List<CustomerDTO> GetCustomersWithOrders();
        List<CustomerDTO> GetHighValueCustomers();
    }

}
