using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Dto.Order;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Models.Cart;
using WebStore.DomainNew.Models.Order;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Sql
{
    public class SqlOrdersService : IOrdersService
    {
        private readonly WebStoreContext _context;
        private readonly UserManager<User> _userManager;

        public SqlOrdersService(WebStoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IEnumerable<OrderDto> GetUserOrders(string userName)
        {
            return _context.Orders.Include("User").Include("OrderItems").Where(o => o.User.UserName.Equals(userName)).Select(o => new OrderDto()
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
            }).ToList();
        }

        public OrderDto GetOrderById(int id)
        {
            return _context.Orders.Include("OrderItems").Select(o => new OrderDto()
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

        public OrderDto CreateOrder(OrderViewModel orderModel, CartViewModel transformCart, string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            using (var transaction = _context.Database.BeginTransaction())
            {
                var order = new Order()
                {
                    Address = orderModel.Address,
                    Name = orderModel.Name,
                    Date = DateTime.Now,
                    Phone = orderModel.Phone,
                    User = user
                };

                _context.Orders.Add(order);

                foreach (var item in transformCart.Items)
                {
                    var productVm = item.Key;
                    var product = _context.Products.FirstOrDefault(p => p.Id.Equals(productVm.Id));
                    if (product == null)
                        throw new InvalidOperationException("Продукт не найден в базе");
                    var orderItem = new OrderItem()
                    {
                        Order = order,
                        Price = product.Price,
                        Quantity = item.Value,
                        Product = product
                    };
                    _context.OrderItems.Add(orderItem);
                }
                _context.SaveChanges();
                transaction.Commit();
                return GetOrderById(order.Id);
            }
        }
    }
}
