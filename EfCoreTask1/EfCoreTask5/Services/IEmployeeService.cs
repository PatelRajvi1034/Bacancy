using EfCoreTask5.Models.DTOs;

namespace EfCoreTask5.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetAllEmployees();
        Task<EmployeeDTO?> GetEmployeeById(int id);
        Task<EmployeeDTO> CreateEmployee(EmployeeDTO employeeDto);
        Task<bool> UpdateEmployee(int id, EmployeeDTO employeeDto);
        Task<bool> DeleteEmployee(int id);
    }
}
