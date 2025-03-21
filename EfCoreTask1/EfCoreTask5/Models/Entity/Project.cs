using System.ComponentModel.DataAnnotations;

namespace EfCoreTask5.Models.Entity
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [Required]
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();

    }
}
