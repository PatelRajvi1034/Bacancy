using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreTask5.Models.Entity
{
    public class EmployeeProject
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        public string Role { get; set; }
    }

}
