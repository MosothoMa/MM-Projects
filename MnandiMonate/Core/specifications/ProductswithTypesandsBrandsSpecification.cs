using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using core.Entities;

namespace Core.specifications
{
    public class ProductswithTypesandsBrandsSpecification : basespecifications<Product>
    {
        public ProductswithTypesandsBrandsSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }


        public ProductswithTypesandsBrandsSpecification(ProductsSpecParams productsSpecParams)
            : base(x =>
                (string.IsNullOrEmpty(productsSpecParams.Search) 
                || x.Name.ToLower().Contains(productsSpecParams.Search))
                &&
                (!productsSpecParams.BrandId.HasValue || x.ProductBrandId == productsSpecParams.BrandId) &&
                (!productsSpecParams.TypeId.HasValue || x.ProductTypeId == productsSpecParams.TypeId)
            )
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
            AddOrderBy(x => x.Name);
            ApplyPaging(productsSpecParams.PageSize * (productsSpecParams.PageIndex -1),
                         productsSpecParams.PageSize);


            if (!string.IsNullOrEmpty(productsSpecParams.Sort))
            {
                switch (productsSpecParams.Sort)
                {

                    case "priceAsync":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderBydescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }

        }
    }
}