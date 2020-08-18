using System.Collections.Generic;
using System.Linq;

namespace Stroopwafels.Application.Domain
{
    public class OrderBuilder
    {
        public Order CreateOrder(IList<KeyValuePair<StroopwafelType, int>> quoteLines)
        {
            var orderLines = quoteLines.Select(line => new OrderLine(line.Value, new OrderProduct(line.Key))).ToList();

            return new Order { ProductsAndAmounts = orderLines };
        }
    }
}