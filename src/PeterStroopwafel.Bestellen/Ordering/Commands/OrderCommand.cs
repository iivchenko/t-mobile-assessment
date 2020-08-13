using System.Collections.Generic;

namespace Ordering.Commands
{
    public class OrderCommand
    {
        public IList<KeyValuePair<StroopwafelType, int>> OrderLines { get; }

        public string Supplier { get; }

        public OrderCommand(IList<KeyValuePair<StroopwafelType, int>> orderLines, string supplier)
        {
            this.OrderLines = orderLines;
            this.Supplier = supplier;
        }
    }
}
