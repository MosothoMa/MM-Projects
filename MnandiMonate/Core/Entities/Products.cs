using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace core.Entities
{
    public class Product : BaseIdentity
    {
        public string? Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureURL { get; set; }

        public ProductType ProductType { get; set; }  

        public int ProductTypeId { get; set; }

        public ProductBrand ProductBrand { get; set; }

        public int ProductBrandId { get; set; }
    }
}