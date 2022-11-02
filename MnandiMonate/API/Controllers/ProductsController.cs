using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        
        private readonly IGenericRepository<Product> _prodContext;
        
        private readonly IGenericRepository<ProductBrand> _prodBrandContext;
        private readonly IGenericRepository<ProductType> _prodTypeContext;
        
        public ProductsController(ILogger<ProductsController> logger,IGenericRepository<Product> productRepo,
                                  IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo )
        {
            _logger = logger;
            _prodContext = productRepo;
            _prodBrandContext = productBrandRepo;
            _prodTypeContext = productTypeRepo;
        }


        [HttpGet]
        public async Task<ActionResult<List<Product>>> getProducts()
        {
            var products =  await _prodContext.ListAllSync();
            return Ok(products);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<List<Product>>> getProduct(int id)
        {
            var products = await _prodContext.GetByID(id);
            return Ok(products);
        
        }

         [HttpGet("brand")]
        public async Task<ActionResult<List<ProductBrand>>> getProductBrands()
        {
            var productsbrands = await _prodBrandContext.ListAllSync();
            return Ok(productsbrands);
        
        }

         [HttpGet("type")]
        public async Task<ActionResult<List<ProductType>>> getProductTypes()
        {
            var productsTypes = await _prodTypeContext.ListAllSync();
            return Ok(productsTypes);
        
        }
     
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}