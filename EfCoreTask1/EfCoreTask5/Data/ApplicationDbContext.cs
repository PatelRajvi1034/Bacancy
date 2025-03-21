using EfCoreTask5.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace EfCoreTask5.Data
{
   public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // **One-to-Many: Department -> Employees**
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // **Many-to-Many: Employee <-> Project (Using EmployeeProject Join Table)**
            modelBuilder.Entity<EmployeeProject>()
                .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.EmployeeProjects)
                .HasForeignKey(ep => ep.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(ep => ep.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Department>().HasData(
        new Department { DepartmentId = 1, DepartmentName = "Software Development" },
        new Department { DepartmentId = 2, DepartmentName = "Marketing" },
        new Department { DepartmentId = 3, DepartmentName = "Finance" }
    );

            modelBuilder.Entity<Project>().HasData(
                new Project { ProjectId = 1, ProjectName = "E-Commerce Website", StartDate = new DateTime(2025, 3, 1) },
                new Project { ProjectId = 2, ProjectName = "Mobile Banking App", StartDate = new DateTime(2025, 4, 10) },
                new Project { ProjectId = 3, ProjectName = "SEO Campaign", StartDate = new DateTime(2025, 5, 15) },
                new Project { ProjectId = 4, ProjectName = "Cloud Migration", StartDate = new DateTime(2025, 6, 20) },
                new Project { ProjectId = 5, ProjectName = "Cybersecurity Upgrade", StartDate = new DateTime(2025, 7, 5) }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, Name = "Alice Johnson", Email = "alice@example.com", DepartmentId = 1 },
                new Employee { EmployeeId = 2, Name = "Bob Smith", Email = "bob@example.com", DepartmentId = 1 },
                new Employee { EmployeeId = 3, Name = "Charlie Brown", Email = "charlie@example.com", DepartmentId = 2 },
                new Employee { EmployeeId = 4, Name = "Diana Adams", Email = "diana@example.com", DepartmentId = 3 },
                new Employee { EmployeeId = 5, Name = "Ethan White", Email = "ethan@example.com", DepartmentId = 1 }
            );

            modelBuilder.Entity<EmployeeProject>().HasData(
                new EmployeeProject { EmployeeId = 1, ProjectId = 1, Role = "Frontend Developer" },
                new EmployeeProject { EmployeeId = 1, ProjectId = 2, Role = "Backend Developer" },
                new EmployeeProject { EmployeeId = 2, ProjectId = 1, Role = "Full Stack Developer" },
                new EmployeeProject { EmployeeId = 2, ProjectId = 4, Role = "Cloud Architect" },
                new EmployeeProject { EmployeeId = 3, ProjectId = 3, Role = "Marketing Specialist" },
                new EmployeeProject { EmployeeId = 4, ProjectId = 5, Role = "Security Analyst" },
                new EmployeeProject { EmployeeId = 5, ProjectId = 2, Role = "Mobile App Developer" }
            );
        }
    }
}
