using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        public StoreContext _storeContext { get; set; }     
        public ProductRepository(StoreContext storeContext) 
        {
            _storeContext = storeContext;
   
        }
       
        public async Task<Product> GetProductByIdAsync(int Id)
        {  
             return await _storeContext.Products
              .Include(p => p.ProductType)
              .Include(p => p.ProductBrand)
              .SingleOrDefaultAsync(p => p.Id == Id);
       
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _storeContext.Products
                    .Include(p => p.ProductType)
                    .Include(p => p.ProductBrand)
                    .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductsBrandsAsync()
        {
            return await _storeContext.productBrands.ToListAsync();
        }

        public async  Task<IReadOnlyList<ProductType>> GetProductsTypesAsync()
        { 
            return await _storeContext.productTypes.ToListAsync();
        }
    }
}