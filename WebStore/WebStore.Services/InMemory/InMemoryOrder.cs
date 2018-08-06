using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WebStore.Domain.Dto.Order;
using WebStore.DomainNew.Models.Cart;
using WebStore.DomainNew.Models.Order;
using WebStore.Interfaces.Services;

namespace WebStore.Services.InMemory
{
    public class InMemoryOrder : IOrdersService
    {
        private readonly List<OrderDto> _orderDto;

        public InMemoryOrder()
        {
            _orderDto = new List<OrderDto>(3)
            {
                new OrderDto()
                {
                    Id = 1,
                    Address = "Address_1",
                    Date = DateTime.Now,
                    Name = "orderDto_1",

                    OrderItems = new List<OrderItemDto> (2)
                    {
                          new OrderItemDto()
                          {
                              Id = 1,
                              Price = 100,
                              Quantity = 2
                          },
                          new OrderItemDto()
                          {
                              Id = 2,
                              Price = 200,
                              Quantity = 3
                          }
                    },
                    Phone = "777-77-77"
                }
            };
        }

        public OrderDto CreateOrder(
            OrderViewModel orderModel, 
            CartViewModel transformCart, 
            string userName)
        {
            throw new NotImplementedException();
        }

        public OrderDto GetOrderById(int id)
        {
            return _orderDto.Select(o => new OrderDto()
            {
                Id = o.Id,
                Name = o.Name,
                Address = o.Address,
                Date = o.Date,
                Phone = o.Phone,
                OrderItems = o.OrderItems.Select(oi => new OrderItemDto()
                {
                    Id = oi.Id,
                    Price = oi.Price,
                    Quantity = oi.Quantity
                })
            }).FirstOrDefault(o => o.Id.Equals(id));
        }

        public IEnumerable<OrderDto> GetUserOrders(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
