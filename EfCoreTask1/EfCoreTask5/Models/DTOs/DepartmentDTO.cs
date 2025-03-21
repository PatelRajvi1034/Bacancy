using System.Collections.Generic;

namespace EfCoreTask5.Models.DTOs
{
    public class DepartmentDTO
    {
        
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public List<EmployeeDTO> Employees { get; set; } = new List<EmployeeDTO>();
    }
}

