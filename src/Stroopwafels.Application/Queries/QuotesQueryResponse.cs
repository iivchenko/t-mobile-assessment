using System.Collections.Generic;

namespace Stroopwafels.Application.Queries
{
    public sealed class QuotesQueryResponse
    {
        public string Supplier { get; internal set; }

        public decimal TotalPrice { get; internal set; }

        public decimal TotalWithoutShippingCost { get; internal set; }  

        public IEnumerable<QuotesQueryItem> Items { get; internal set; }
    }

    public sealed class QuotesQueryItem
    {
        public int Amount { get; internal set; }

        public decimal ItemPrice { get; internal set; }

        public decimal TotalPrice { get; internal set; }
    }
}
