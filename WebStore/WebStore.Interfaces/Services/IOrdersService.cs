using System.Collections.Generic;
using WebStore.Domain.Dto.Order;
using WebStore.DomainNew.Models.Cart;
using WebStore.DomainNew.Models.Order;

namespace WebStore.Interfaces.Services
{
    public interface IOrdersService
    {
        IEnumerable<OrderDto> GetUserOrders(string userName);

        OrderDto GetOrderById(int id);

        OrderDto CreateOrder(OrderViewModel orderModel, CartViewModel transformCart, string userName);
    }
}
