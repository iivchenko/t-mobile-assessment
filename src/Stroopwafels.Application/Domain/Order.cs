using System.Collections.Generic;

namespace Stroopwafels.Application.Domain
{
    public sealed class Order
    {
        public IEnumerable<OrderLine> ProductsAndAmounts { get; set; }
    }
}
