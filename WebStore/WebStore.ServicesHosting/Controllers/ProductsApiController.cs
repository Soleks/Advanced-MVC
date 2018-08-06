using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Dto;
using WebStore.DomainNew.Filters;
using WebStore.Interfaces.Services;

namespace WebStore.ServicesHosting.Controllers
{
    [Route("api/products"), Produces("application/json")]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductData
    {
        private readonly IProductData _productData;

        public ProductsApiController(IProductData productData)
        {
            _productData = productData;
        }

        //api/products/sections GET
        [HttpGet("sections")]
        public IEnumerable<SectionDto> GetSections()
        {
            return _productData.GetSections();
        }

        //api/products/brands GET
        [HttpGet("brands")]
        public IEnumerable<BrandDto> GetBrands()
        {
            return _productData.GetBrands();
        }

        //api/products POST
        [HttpPost, ActionName("Post")]
        public IEnumerable<ProductDto> GetProducts([FromBody]ProductFilter filter)
        {
            return _productData.GetProducts(filter);
        }

        //api/products/id GET
        [HttpGet("{id}"), ActionName("Get")]
        public ProductDto GetProductById(int id)
        {
            var product = _productData.GetProductById(id);
            return product;
        }
    }
}