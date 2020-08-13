using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stroopwafels.Ordering.Queries
{
    public class QuotesQuery
    {
        public IList<KeyValuePair<StroopwafelType, int>> OrderLines { get; }

        public QuotesQuery(IList<KeyValuePair<StroopwafelType, int>> orderLines)
        {
            this.OrderLines = orderLines;
        }
    }
}
