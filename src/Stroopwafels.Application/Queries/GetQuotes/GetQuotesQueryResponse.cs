using Stroopwafels.Application.Domain;
using System;
using System.Collections.Generic;

namespace Stroopwafels.Application.Queries.GetQuotes
{
    public sealed class GetQuotesQueryResponse
    {
        public string CustomerName { get; set; }

        public DateTime WishDate { get; set; }

        public decimal TotalPrice { get; internal set; }

        public IEnumerable<QuotesQueryItem> Items { get; internal set; }
    }

    public sealed class QuotesQueryItem
    {
        public string Supplier { get; set; }

        public StroopwafelType Type { get; set; }

        public int Amount { get; internal set; }

        public decimal ItemPrice { get; internal set; }

        public decimal TotalPrice { get; internal set; }
    }
}
