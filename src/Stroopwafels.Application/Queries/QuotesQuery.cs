using MediatR;
using Stroopwafels.Ordering;
using System.Collections.Generic;

namespace Stroopwafels.Application.Queries
{
    public sealed class QuotesQuery : IRequest<IEnumerable<Quote>>
    {
        public IList<KeyValuePair<StroopwafelType, int>> OrderLines { get; }

        public QuotesQuery(IList<KeyValuePair<StroopwafelType, int>> orderLines)
        {
            this.OrderLines = orderLines;
        }
    }
}
