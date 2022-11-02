using System.Reflection;
using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
// using Core.Entities;
using core.Entities;

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
        }
    }
}