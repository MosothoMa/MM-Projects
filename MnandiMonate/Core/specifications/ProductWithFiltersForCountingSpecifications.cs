using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Entities;

namespace Core.specifications
{
    public class ProductWithFiltersForCountSpecifications :basespecifications<Product>
    {
        public ProductWithFiltersForCountSpecifications(ProductsSpecParams productsSpecParams)
            : base(x =>
              (string.IsNullOrEmpty(productsSpecParams.Search) 
                || x.Name.ToLower().Contains(productsSpecParams.Search)) &&
                (!productsSpecParams.BrandId.HasValue || x.ProductBrandId == productsSpecParams.BrandId) &&
                (!productsSpecParams.TypeId.HasValue || x.ProductTypeId == productsSpecParams.TypeId)
            )
        {

        }
    }
}