using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Dto.Order;
using WebStore.DomainNew.Models.Cart;
using WebStore.DomainNew.Models.Order;
using WebStore.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/orders")]
    public class OrderApiConttroller : Controller, IOrdersService
    {
        private readonly IOrdersService _ordersService;

        public OrderApiConttroller(IOrdersService ordersService)
        {
            _ordersService = ordersService; 
        }

        [HttpGet("user/{userName}")]
        public IEnumerable<OrderDto> GetUserOrders(string userName)
        {
            return _ordersService.GetUserOrders(userName);
        }

        [HttpGet("{id}"), ActionName("Get")]
        public OrderDto GetOrderById(int id)
        {
            return _ordersService.GetOrderById(id);
        }

        [HttpPost("{userName?}")]
        public OrderDto CreateOrder(
            OrderViewModel orderModel, 
            CartViewModel transformCart, 
            string userName)
        {
            return _ordersService.CreateOrder(orderModel, transformCart, userName);
        }
    }
}
