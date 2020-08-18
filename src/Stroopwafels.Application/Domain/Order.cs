using System.Collections.Generic;

namespace Stroopwafels.Application.Domain
{
    public class Order
    {
        public IEnumerable<OrderLine> ProductsAndAmounts { get; set; }
    }
}
