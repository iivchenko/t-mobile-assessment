using System.Collections.Generic;

namespace Stroopwafels.Application.Domain
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
