using System;
using MediatR;
using Stroopwafels.Application.Domain;
using System.Collections.Generic;

namespace Stroopwafels.Application.Queries.GetQuotes
{
    public sealed class GetQuotesQuery : IRequest<GetQuotesQueryResponse>
    {
        public string CustomerName { get; set; }

        public DateTime WishDate { get; set; }

        public IEnumerable<QuotesOrderLine> OrderLines { get; set; }
    }

    public sealed class QuotesOrderLine
    {
        public StroopwafelType Type { get; set; }

        public int Amount { get; set; }
    }
}
