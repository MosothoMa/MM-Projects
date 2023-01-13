using System.Reflection;
using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
// using Core.Entities;
using core.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base
        (options)
        {
            
        }

        public DbSet<Product> Products {get;set;}
        
        public DbSet<ProductBrand> productBrands {get;set;}

        
        public DbSet<ProductType> productTypes {get;set;}

        protected override  void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties  = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

                    foreach (var item in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(item.Name).HasConversion<double>();
                    }
                }
            }
        }
    }
}