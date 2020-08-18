using MediatR;
using Stroopwafels.Application.Domain;
using Stroopwafels.Application.Services;
using Stroopwafels.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stroopwafels.Application.Queries
{
    public sealed class QuotesQueryHandler : IRequestHandler<QuotesQuery, QuotesQueryResponse>
    {
        private readonly IEnumerable<IStroopwafelSupplierService> _stroopwafelSupplierServices;

        public QuotesQueryHandler(IEnumerable<IStroopwafelSupplierService> stroopwafelSupplierServices)
        {
            _stroopwafelSupplierServices = stroopwafelSupplierServices;
        }

        public Task<QuotesQueryResponse> Handle(QuotesQuery query, CancellationToken cancellationToken)
        {
            var item =
             _stroopwafelSupplierServices
                 .Select(x => RetrievStroopwafels(x, query))
                 .SelectMany(x => x)
                 .GroupBy(x => x.Type)
                 .CartesianProduct()
                 .Where(SatisfyDelivery)
                 .Where(x => SatisfyWishDate(x, query.Customer.WishDate))
                 .Select(CalculatePrice)
                 .Select(AddBenefit)
                 .OrderBy(x => x.price)
                 .FirstOrDefault();

            return Task.FromResult(Pack(item, query.Customer));
        }

        private IEnumerable<Item> RetrievStroopwafels(IStroopwafelSupplierService service, QuotesQuery query)
        {
            var stroopwafels = service.QueryStroopwafels().GetAwaiter().GetResult();

            if (!stroopwafels.Any())
            {
                return Enumerable.Empty<Item>();
            }

            var items = new List<Item>();
            var now = DateTime.UtcNow;

            foreach (var orderLine in query.Items)
            {
                var supplier = service.GetName().GetAwaiter().GetResult();
                var period = service.GetDeliveryPeriod().GetAwaiter().GetResult();
                var stroopwafel = stroopwafels.First(s => s.Type == orderLine.Type);
                var item = new Item
                {
                    Type = orderLine.Type,
                    Supplier = supplier,
                    Amount = orderLine.Amount,
                    ItemPrice = stroopwafel.Price,
                    TotalPrice = orderLine.Amount * stroopwafel.Price,
                    DeliveryDate = now + period,
                };

                items.Add(item);
            }

            return items;
        }

        private bool SatisfyDelivery(IEnumerable<Item> items)
        {
            var periods = items.Select(x => x.DeliveryDate);

            return periods.Max() - periods.Min() <= TimeSpan.FromDays(1);
        }

        private bool SatisfyWishDate(IEnumerable<Item> items, DateTime wishDate)
        {
            return items.Max(x => x.DeliveryDate) < wishDate;
        }

        private (decimal price, IEnumerable<Item> combination) CalculatePrice(IEnumerable<Item> items)
        {
            var suppliers = _stroopwafelSupplierServices.Select(x => (name: x.GetName().GetAwaiter().GetResult(), service: x));

            var price = items
                .GroupBy(x => x.Supplier)
                .Select(x =>
                {
                    var total = x.Sum(y => y.TotalPrice);
                    var shiping = suppliers.Single(y => y.name == x.Key).service.CalculateShipingCost(total).GetAwaiter().GetResult();

                    return total + shiping;
                }).Sum();

            return (price, items);
        }

        private (decimal price, IEnumerable<Item> combination) AddBenefit((decimal, IEnumerable<Item>) item)
        {
            var (price, items) = item;
            return (price + items.Count(), items);
        }

        private QuotesQueryResponse Pack((decimal, IEnumerable<Item>) item, QuotesCustomer customer)
        {
            var (price, items) = item;

            return new QuotesQueryResponse
            {
                CustomerName = customer.Name,
                WishDate = customer.WishDate,
                TotalPrice = price,
                Items = items.Select(x => new QuotesQueryItem
                {
                    TotalPrice = x.TotalPrice,
                    Amount = x.Amount,
                    ItemPrice = x.ItemPrice,
                    Supplier = x.Supplier,
                    Type = x.Type
                })
            };
        }

        private class Item
        {
            public string Supplier { get; set; }

            public StroopwafelType Type { get; set; }

            public int Amount { get; set; }

            public decimal ItemPrice { get; set; }

            public decimal TotalPrice { get; set; }

            public DateTime DeliveryDate { get; set; }
        }
    }
}
