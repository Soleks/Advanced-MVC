using WebStore.DomainNew.Models.Order;

namespace WebStore.DomainNew.Models.Cart
{
    public class DetailsViewModel
    {
        public CartViewModel CartViewModel { get; set; }

        public OrderViewModel OrderViewModel { get; set; }
    }
}
