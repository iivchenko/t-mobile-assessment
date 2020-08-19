using Stroopwafels.Application.Domain;
using System;
using System.Collections.Generic;

namespace Stroopwafels.Application.Queries.GetQuotes
{
    public sealed class GetQuotesQueryResponse
    {
        public string CustomerName { get; set; }

        public DateTime WishDate { get; set; }

        public decimal TotalPrice { get; set; }

        public IEnumerable<QuotesQueryItem> OrderLines { get; set; }
    }

    public sealed class QuotesQueryItem
    {
        public string Supplier { get; set; }

        public StroopwafelType Type { get; set; }

        public int Amount { get; set; }

        public decimal ItemPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
