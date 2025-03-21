using EfCoreTask5.Models.DTOs;
using EfCoreTask5.Models.Entity;
using EfCoreTask5.Data;
using Microsoft.EntityFrameworkCore;

namespace EfCoreTask5.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;

        public DepartmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DepartmentDTO>> GetAllDepartments(int pageNumber, int pageSize)
        {
            return await _context.Departments
                .Include(d => d.Employees)
                .Select(d => new DepartmentDTO
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName,
                    Employees = d.Employees.Select(e => new EmployeeDTO
                    {
                        EmployeeId = e.EmployeeId,
                        Name = e.Name,
                        Email = e.Email
                    }).ToList()
                })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<DepartmentDTO?> GetDepartmentById(int id)
        {
            return await _context.Departments
                .Include(d => d.Employees)
                .Where(d => d.DepartmentId == id)
                .Select(d => new DepartmentDTO
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName,
                    Employees = d.Employees.Select(e => new EmployeeDTO
                    {
                        EmployeeId = e.EmployeeId,
                        Name = e.Name,
                        Email = e.Email
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Department> CreateDepartment(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<bool> UpdateDepartment(int id, Department department)
        {
            var existingDept = await _context.Departments.FindAsync(id);
            if (existingDept == null) return false;

            existingDept.DepartmentName = department.DepartmentName;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return false;

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}