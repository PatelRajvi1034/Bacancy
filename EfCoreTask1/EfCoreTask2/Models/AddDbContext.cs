﻿using EfCoreTask2.Data;
using Microsoft.EntityFrameworkCore;

namespace EfCoreTask2. Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}