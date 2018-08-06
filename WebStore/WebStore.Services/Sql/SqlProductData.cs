using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Dto;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Filters;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Sql
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreContext _context;

        public SqlProductData(WebStoreContext context)
        {
            _context = context;
        }

        public IEnumerable<SectionDto> GetSections()
        {
            return _context.Sections.Select(s => new SectionDto()
            {
                Id = s.Id,
                Name = s.Name,
                Order = s.Order,
                ParentId = s.ParentId
            }).ToList();
        }

        public IEnumerable<BrandDto> GetBrands()
        {
            return _context.Brands.Select(b => new BrandDto() { Id = b.Id, Name = b.Name, Order = b.Order}).ToList();
        }

        public IEnumerable<ProductDto> GetProducts(ProductFilter filter)
        {
            var query = _context.Products.Include("Brand").Include("Section").AsQueryable();
            if (filter.BrandId.HasValue)
                query = query.Where(c => c.BrandId.HasValue && c.BrandId.Value.Equals(filter.BrandId.Value));
            if (filter.SectionId.HasValue)
                query = query.Where(c => c.SectionId.Equals(filter.SectionId.Value));
            return query.Select(p => new ProductDto()
            {
                Brand = new BrandDto() { Id = p.Brand.Id, Name = p.Brand.Name, Order = p.Brand.Order},
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price
            }).ToList();
        }

        public ProductDto GetProductById(int id)
        {
            return _context.Products.Include("Brand").Include("Section").Select(p => new ProductDto()
            {
                Brand = new BrandDto() { Id = p.Brand.Id, Name = p.Brand.Name },
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price
            }).FirstOrDefault(p => p.Id.Equals(id));
        }
    }
}

