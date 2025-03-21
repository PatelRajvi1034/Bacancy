using System.ComponentModel.DataAnnotations;

namespace EfCoreTask5.Models.Entity
{
    public class Department
        {
            [Key]
            public int DepartmentId { get; set; }
            [Required]
            public string DepartmentName { get; set; }
            public List<Employee> Employees { get; set; } = new List<Employee>();
        }
}

