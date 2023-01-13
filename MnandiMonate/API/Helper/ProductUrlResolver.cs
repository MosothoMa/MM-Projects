using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using AutoMapper;
using core.Entities;

namespace API.Helper
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToRetunrDTO, string>
    {
        public IConfiguration _Config { get; }
        public ProductUrlResolver(IConfiguration config)
        {
            _Config = config;
            
        }

        public string Resolve(Product source, ProductToRetunrDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureURL))
            {
                return _Config["ApiUrl"] + source.PictureURL;
            }
            return null;
        }
    }
}