using System.Collections.Generic;

namespace Stroopwafels.Ordering.Services
{
    public class Order
    {
        public IEnumerable<OrderLine> ProductsAndAmounts { get; }

        public Order(IList<OrderLine> productsAndAmounts)
        {
            this.ProductsAndAmounts = productsAndAmounts;
        }
    }
}
