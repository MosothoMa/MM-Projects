using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int Id);

        Task<IReadOnlyList<Product>> GetProductsAsync();

            Task<IReadOnlyList<ProductBrand>> GetProductsBrandsAsync();

                Task<IReadOnlyList<ProductType>> GetProductsTypesAsync();


    }
}