using MediatR;
using Stroopwafels.Application.Domain;
using System;
using System.Collections.Generic;

namespace Stroopwafels.Application.Commands.PlaceOrder
{
    public sealed class PlaceOrderCommand : IRequest<Unit>
    {
        public string CustomerName { get; set; }

        public DateTime WishDate { get; set; }

        public IEnumerable<PlaceOrderItem> OrderLines { get; set; }
    }

    public sealed class PlaceOrderItem
    {
        public string Supplier { get; set; }

        public StroopwafelType Type { get; set; }

        public int Amount { get; set; }
    }
}
