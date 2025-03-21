using System;
using System.Collections.Generic;
using EfCoreTask5.Models.DTOs;

namespace EfCoreTask5.Models.DTOs
{
    public class ProjectDTO
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public List<EmployeeProjectDTO> EmployeeProjects { get; set; } = new List<EmployeeProjectDTO>();
    }
}