using EfCoreTask5.Models.DTOs;
using EfCoreTask5.Models.Entity;
using EfCoreTask5.Data;
using Microsoft.EntityFrameworkCore;

namespace EfCoreTask5.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeDTO>> GetAllEmployees()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.EmployeeProjects)
                    .ThenInclude(ep => ep.Project)
                .Select(e => new EmployeeDTO
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    Email = e.Email,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.Department.DepartmentName,
                    EmployeeProjects = e.EmployeeProjects.Select(ep => new EmployeeProjectDTO
                    {
                        EmployeeId = ep.EmployeeId,
                        EmployeeName = e.Name,
                        ProjectId = ep.ProjectId,
                        ProjectName = ep.Project.ProjectName,
                        Role = ep.Role
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<EmployeeDTO?> GetEmployeeById(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.EmployeeProjects)
                    .ThenInclude(ep => ep.Project)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null) return null;

            return new EmployeeDTO
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Email = employee.Email,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department.DepartmentName,
                EmployeeProjects = employee.EmployeeProjects.Select(ep => new EmployeeProjectDTO
                {
                    EmployeeId = ep.EmployeeId,
                    EmployeeName = employee.Name,
                    ProjectId = ep.ProjectId,
                    ProjectName = ep.Project.ProjectName,
                    Role = ep.Role
                }).ToList()
            };
        }

        public async Task<EmployeeDTO> CreateEmployee(EmployeeDTO employeeDto)
        {
            var employee = new Employee
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                DepartmentId = employeeDto.DepartmentId
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return await GetEmployeeById(employee.EmployeeId);
        }

        public async Task<bool> UpdateEmployee(int id, EmployeeDTO employeeDto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            employee.Name = employeeDto.Name;
            employee.Email = employeeDto.Email;
            employee.DepartmentId = employeeDto.DepartmentId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
