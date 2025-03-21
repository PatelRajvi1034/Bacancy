using EfCoreTask5.Models.DTOs;
using EfCoreTask5.Models.Entity;

namespace EfCoreTask5.Services
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDTO>> GetAllDepartments(int pageNumber, int pageSize);
        Task<DepartmentDTO?> GetDepartmentById(int id);
        Task<Department> CreateDepartment(Department department);
        Task<bool> UpdateDepartment(int id, Department department);
        Task<bool> DeleteDepartment(int id);
    }
}