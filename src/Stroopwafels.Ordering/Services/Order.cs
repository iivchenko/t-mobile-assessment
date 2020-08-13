using System.Collections.Generic;

namespace Stroopwafels.Ordering.Services
{
    public class Order
    {
        public IList<OrderLine> ProductsAndAmounts { get; }

        public Order(IList<OrderLine> productsAndAmounts)
        {
            this.ProductsAndAmounts = productsAndAmounts;
        }
    }
}
