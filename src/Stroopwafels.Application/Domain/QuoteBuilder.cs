using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stroopwafels.Application.Domain
{
    public class QuoteBuilder
    {
        public Quote CreateOrder(IList<KeyValuePair<StroopwafelType, int>> orderDetails, IList<Stroopwafel> stroopwafels, ISupplier supplier)
        {
            var orderLines = new List<QuoteLine>();

            foreach (var orderLine in orderDetails)
            {
                var stroopwafel = stroopwafels.First(s => s.Type == orderLine.Key);
                orderLines.Add(new QuoteLine(orderLine.Value, stroopwafel));
            }

            return new Quote(orderLines, supplier);
        }

        public Quote CreateOrder(IList<KeyValuePair<StroopwafelType, int>> orderDetails, IList<Stroopwafel> stroopwafels, decimal shipingCost)
        {
            var orderLines = new List<QuoteLine>();

            foreach (var orderLine in orderDetails)
            {
                var stroopwafel = stroopwafels.First(s => s.Type == orderLine.Key);
                orderLines.Add(new QuoteLine(orderLine.Value, stroopwafel));
            }

            return new Quote(orderLines, shipingCost);
        }
    }
}
