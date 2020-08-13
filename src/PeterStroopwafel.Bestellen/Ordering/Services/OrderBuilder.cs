using System.Collections.Generic;
using System.Linq;

namespace Ordering.Services
{
    public class OrderBuilder
    {
        public Order CreateOrder(IList<KeyValuePair<StroopwafelType, int>> quoteLines)
        {
            var orderLines = quoteLines.Select(line => new OrderLine(line.Value, new OrderProduct(line.Key))).ToList();

            return new Order(orderLines);
        }
    }
}