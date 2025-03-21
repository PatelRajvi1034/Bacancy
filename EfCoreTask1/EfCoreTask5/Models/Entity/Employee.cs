using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EfCoreTask5.Models.Entity
{
    public class Employee
    {
        
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [ForeignKey("DepartmentId")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
    }
}
   
