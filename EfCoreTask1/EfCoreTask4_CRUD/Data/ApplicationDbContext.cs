﻿using EfCoreTask4.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace EfCoreTask4.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer-Order Relationship
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderProduct - Composite Key
            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            // Set Decimal Precision for Price
            modelBuilder.Entity<Product>()
               .Property(p => p.Price)
               .HasColumnType("decimal(18,2)");

            // Seed Data
            modelBuilder.Entity<Customer>().HasData(
    new Customer { Id = 1, Name = "John Doe", Email = "john@example.com", CreatedDate = new DateTime(2025, 3, 1), IsVIP = true },
    new Customer { Id = 2, Name = "Alice Smith", Email = "alice@example.com", CreatedDate = new DateTime(2025, 3, 5), IsVIP = false },
    new Customer { Id = 3, Name = "Michael Johnson", Email = "michael@example.com", CreatedDate = new DateTime(2025, 2, 15), IsVIP = true },
    new Customer { Id = 4, Name = "Emma Brown", Email = "emma@example.com", CreatedDate = new DateTime(2025, 1, 20), IsVIP = false },
    new Customer { Id = 5, Name = "Olivia Wilson", Email = "olivia@example.com", CreatedDate = new DateTime(2025, 3, 10), IsVIP = true }
);


            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 1000m, Stock = 50 },
                new Product { Id = 2, Name = "Phone", Price = 500m, Stock = 30 },
                new Product { Id = 3, Name = "Tablet", Price = 300m, Stock = 20 },
                new Product { Id = 4, Name = "Monitor", Price = 250m, Stock = 15 },
                new Product { Id = 5, Name = "Keyboard", Price = 50m, Stock = 100 }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, OrderDate = new DateTime(2025, 3, 2), CustomerId = 1, IsDeleted = false }, // This month
                new Order { Id = 2, OrderDate = new DateTime(2025, 3, 8), CustomerId = 2, IsDeleted = false }, // This month
                new Order { Id = 3, OrderDate = new DateTime(2025, 2, 20), CustomerId = 3, IsDeleted = false }, // Last month
                new Order { Id = 4, OrderDate = new DateTime(2025, 1, 25), CustomerId = 4, IsDeleted = false }, // 2 months ago
                new Order { Id = 5, OrderDate = new DateTime(2025, 3, 12), CustomerId = 5, IsDeleted = false } // This month
            );

            modelBuilder.Entity<OrderProduct>().HasData(
                new OrderProduct { OrderId = 1, ProductId = 1, Quantity = 2 },
                new OrderProduct { OrderId = 1, ProductId = 2, Quantity = 1 },
                new OrderProduct { OrderId = 2, ProductId = 2, Quantity = 3 },
                new OrderProduct { OrderId = 3, ProductId = 3, Quantity = 2 },
                new OrderProduct { OrderId = 4, ProductId = 4, Quantity = 1 },
                new OrderProduct { OrderId = 5, ProductId = 5, Quantity = 4 }
            );
        }
    }
}
