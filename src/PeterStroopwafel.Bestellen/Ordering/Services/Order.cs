using System.Collections.Generic;
using Ordering;

namespace Ordering.Services
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
