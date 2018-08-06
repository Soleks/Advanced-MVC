using System;
using System.Collections.Generic;
using System.Text;
using WebStore.DomainNew.Entities.Base.Interfaces;

namespace WebStore.Domain.Dto
{
    public class SectionDto : INamedEntity, IOrderedEntity
    {
        public int? ParentId { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
