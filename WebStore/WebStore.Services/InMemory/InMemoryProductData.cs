using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Dto;
using WebStore.DomainNew.Filters;
using WebStore.Interfaces.Services;

namespace WebStore.Services.InMemory
{
    public class InMemoryProductData : IProductData
    {
        private readonly List<BrandDto> _brandDto;
        private readonly List<ProductDto> _productDto;
        private readonly List<SectionDto> _sectionDto;

        InMemoryProductData()
        {
            _sectionDto = new List<SectionDto>(3)
            {
                new SectionDto()
                {
                   Id = 1,
                   Name = "sectionName_1",
                   Order = 1,
                   ParentId = 1
                },
                new SectionDto()
                {
                   Id = 2,
                   Name = "sectionName_2",
                   Order = 2,
                   ParentId = 2
                },
                new SectionDto()
                {
                   Id = 3,
                   Name = "sectionName_3",
                   Order = 3,
                   ParentId = 3
                }
            };

            _brandDto = new List<BrandDto>(3)
            {
                new BrandDto() { Id = 1, Name = "brandName_1" , Order = 1},
                new BrandDto() { Id = 2, Name = "brandName_2" , Order = 2},
                new BrandDto() { Id = 3, Name = "brandName_3" , Order = 3}
            };

            _productDto = new List<ProductDto>(3)
            {
                new ProductDto()
                {
                    Id = 1,
                    Name = "productName_1",
                    Order = 1,
                    ImageUrl = "",
                    Price = 100,
                    Brand = new BrandDto()
                    {
                        Id = 1,
                        Name = "brandName_1",
                        Order = 1
                    }
                },

                new ProductDto()
                {
                    Id = 2,
                    Name = "productName_2",
                    Order = 2,
                    ImageUrl = "",
                    Price = 200,
                    Brand = new BrandDto()
                    {
                        Id = 2,
                        Name = "brandName_2",
                        Order = 2
                    }
                },

                new ProductDto()
                {
                    Id = 3,
                    Name = "productName_3",
                    Order = 3,
                    ImageUrl = "",
                    Price = 300,
                    Brand = new BrandDto()
                    {
                        Id = 3,
                        Name = "brandName_3",
                        Order = 3
                    }
                }
            };
        }

        public IEnumerable<BrandDto> GetBrands()
        {
            return _brandDto.Select(
                b => new BrandDto()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Order = b.Order
                }).ToList();
        }

        public ProductDto GetProductById(int id)
        {          
            return _productDto.Select(p => new ProductDto()
            {
                Brand = new BrandDto() { Id = p.Brand.Id, Name = p.Brand.Name },
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price
            }).FirstOrDefault(p => p.Id.Equals(id));
        }

        public IEnumerable<ProductDto> GetProducts(ProductFilter filter)
        {
            //var query = _context.Products.Include("Brand").Include("Section").AsQueryable();
            //if (filter.BrandId.HasValue)
            //    query = query.Where(c => c.BrandId.HasValue && c.BrandId.Value.Equals(filter.BrandId.Value));
            //if (filter.SectionId.HasValue)
            //    query = query.Where(c => c.SectionId.Equals(filter.SectionId.Value));
            //return query.Select(p => new ProductDto()

            return _productDto.Select(p => new ProductDto()
            {
                Brand = new BrandDto() { Id = p.Brand.Id, Name = p.Brand.Name, Order = p.Brand.Order },
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price
            }).ToList();
        }

        public IEnumerable<SectionDto> GetSections()
        {
            return _sectionDto.Select(s => new SectionDto()
            {
                Id = s.Id,
                Name = s.Name,
                Order = s.Order,
                ParentId = s.ParentId
            }).ToList();
        }
    }
}
