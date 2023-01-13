using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Helper;
using AutoMapper;
using core.Entities;
using Core.Interfaces;
using Core.specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [EnableCors("CorsPolicy")]
           [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;

        private readonly IGenericRepository<Product> _prodContext;

        private readonly IGenericRepository<ProductBrand> _prodBrandContext;
        private readonly IGenericRepository<ProductType> _prodTypeContext;

        public IMapper _mapper { get; }

        public ProductsController(ILogger<ProductsController> logger, IGenericRepository<Product> productRepo,
                                  IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo,
                                  IMapper mapper)
        {
            _logger = logger;
            _prodContext = productRepo;
            _prodBrandContext = productBrandRepo;
            _prodTypeContext = productTypeRepo;
            _mapper = mapper;
        }

        // [EnableCors()]
        [EnableCors("CorsPolicy")]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToRetunrDTO>>> getProducts([FromQuery]ProductsSpecParams productsSpecParams)
        {
            var spec = new ProductswithTypesandsBrandsSpecification(productsSpecParams);

            var countspec = new ProductWithFiltersForCountSpecifications(productsSpecParams);

            var totalItems =  await _prodContext.CountAsync(countspec);

            var products = await _prodContext.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToRetunrDTO>>(products);
           
            return Ok(new Pagination<ProductToRetunrDTO>(productsSpecParams.PageIndex,productsSpecParams.PageSize, totalItems,data));
            
        }
        
        [EnableCors("CorsPolicy")]
        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductToRetunrDTO>> getProduct(int id)
        {
            var spec = new ProductswithTypesandsBrandsSpecification(id);

            var products = await _prodContext.GetentitywithSpec(spec);
            return _mapper.Map<Product, ProductToRetunrDTO>(products);

        }
        [EnableCors]
        [HttpGet("brand")]
        public async Task<ActionResult<List<ProductBrand>>> getProductBrands()
        {
            var productsbrands = await _prodBrandContext.ListAllSync();
            return Ok(productsbrands);

        }
        [EnableCors]
        [HttpGet("type")]
        public async Task<ActionResult<List<ProductType>>> getProductTypes()
        {
            var productsTypes = await _prodTypeContext.ListAllSync();
            return Ok(productsTypes);
        }
    }
}