using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Clients.Base;
using WebStore.Domain.Dto.Order;
using WebStore.DomainNew.Models.Cart;
using WebStore.DomainNew.Models.Order;
using WebStore.Interfaces.Services;
using System.Net.Http;

namespace WebStore.Clients.Services.Orders
{
    public class OrderClients : BaseClient, IOrdersService
    {
        public OrderClients(IConfiguration configuration): base(configuration)
        {
            ServiceAddress = "api/orders";
        }

        protected override string ServiceAddress { get; set; }

        public OrderDto CreateOrder(
            OrderViewModel orderModel, 
            CartViewModel transformCart, 
            string userName)
        {
            return Post($"{ServiceAddress}/{userName}", orderModel)
                .Content.ReadAsAsync<OrderDto>().Result;
        }

        public OrderDto GetOrderById(int id)
        {
            return Get<OrderDto>($"{ServiceAddress}/{id}");
        }

        public IEnumerable<OrderDto> GetUserOrders(string userName)
        {
            return Get<List<OrderDto>>($"{ServiceAddress}/user/{userName}");
        }
    }
}
