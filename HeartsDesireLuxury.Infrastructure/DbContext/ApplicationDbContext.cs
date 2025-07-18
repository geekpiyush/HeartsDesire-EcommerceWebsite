using Entities.IdentityEntity;
using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DB
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationUserRole,Guid>
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<ProductCategories> ProductCategories { get; set; }

        public DbSet<Orders> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Products>().ToTable("Products");
            modelBuilder.Entity<ProductCategories>().ToTable("ProductCategories");
            modelBuilder.Entity<Inventory>().ToTable("Inventory");
            modelBuilder.Entity<Orders>().ToTable("Orders");


            //modelBuilder.Entity<List<Inventory>>().HasData(new Entities.() { ProductID = 1001, ProductName = "TestProduct", Description = "Test Description", ProductPrice = 999, ProductSalePrice = 699, Stock = 100, SkuID = "Test100ML", ShortDescription = "TestProduct Short Description" });

            modelBuilder.Entity<Inventory>().HasData(new Entities.Inventory() { BarcodeNumber = 546987152, ProductName = "testInventory", Stock = 1500, Price = 270 });



        }


    }  
}
