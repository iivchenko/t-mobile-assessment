using MediatR;
using Stroopwafels.Ordering;
using System.Collections.Generic;

namespace Stroopwafels.Application.Commands
{
    public sealed class OrderCommand : IRequest<Unit>
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
