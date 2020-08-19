using MediatR;
using Stroopwafels.Application.Domain;
using Stroopwafels.Application.Services;
using Stroopwafels.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stroopwafels.Application.Queries.GetQuotes
{
    public sealed class GetQuotesQueryHandler : IRequestHandler<GetQuotesQuery, GetQuotesQueryResponse>
    {
        private readonly IEnumerable<IStroopwafelSupplierService> _stroopwafelSupplierServices;

        public GetQuotesQueryHandler(IEnumerable<IStroopwafelSupplierService> stroopwafelSupplierServices)
        {
            _stroopwafelSupplierServices = stroopwafelSupplierServices;
        }

        public async Task<GetQuotesQueryResponse> Handle(GetQuotesQuery query, CancellationToken cancellationToken)
        {
            var item =
                await
                _stroopwafelSupplierServices
                    .ToAsyncEnumerable()
                    .Select(x => RetrievStroopwafels(x, query))
                    .SelectMany(x => x)
                    .GroupBy(x => x.Type)
                    .CartesianProduct()
                    .WhereAwait(SatisfyDelivery)
                    .WhereAwait(x => SatisfyWishDate(x, query.WishDate))
                    .SelectAwait(CalculatePrice)
                    .SelectAwait(AddBenefit)
                    .OrderBy(x => x.price)
                    .FirstOrDefaultAsync();

            return Pack(item, query.CustomerName, query.WishDate);
        }

        private async IAsyncEnumerable<Item> RetrievStroopwafels(IStroopwafelSupplierService service, GetQuotesQuery query)
        {
            var stroopwafels = await service.QueryStroopwafels();
            var now = DateTime.UtcNow;

            if (stroopwafels == null)
            {
                yield break;
            }

            foreach (var orderLine in query.OrderLines)
            {
                var supplier = service.Name;
                var period = await service.GetDeliveryPeriod();
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

                yield return item;
            }
        }

        private async ValueTask<bool> SatisfyDelivery(IAsyncEnumerable<Item> items)
        {
            var periods = items.Select(x => x.DeliveryDate);

            return (await periods.MaxAsync()) - (await periods.MinAsync()) <= TimeSpan.FromDays(1);
        }

        private async ValueTask<bool> SatisfyWishDate(IAsyncEnumerable<Item> items, DateTime wishDate)
        {
            return await (items.MaxAsync(x => x.DeliveryDate)) < wishDate;
        }

        private async ValueTask<(decimal price, IAsyncEnumerable<Item> combination)> CalculatePrice(IAsyncEnumerable<Item> items)
        {
            var suppliers = _stroopwafelSupplierServices.Select(x => (name: x.Name, service: x));

            var prices = items
                .GroupBy(x => x.Supplier)
                .Select(async x =>
                {
                    var total = await x.SumAsync(y => y.TotalPrice);
                    var shiping = await suppliers.Single(y => y.name == x.Key).service.CalculateShipingCost(total);

                    return total + shiping;
                });

            decimal price = 0;

            await foreach (var task in prices)
            {
                price += await task;
            }

            return (price, items);
        }

        private async ValueTask<(decimal price, IAsyncEnumerable<Item> combination)> AddBenefit((decimal, IAsyncEnumerable<Item>) item)
        {
            var (price, items) = item;
            return (price + await items.CountAsync(), items);
        }

        private GetQuotesQueryResponse Pack((decimal, IAsyncEnumerable<Item>) item, string customerName, DateTime wishDate)
        {
            var (price, items) = item;

            return new GetQuotesQueryResponse
            {
                CustomerName = customerName,
                WishDate = wishDate,
                TotalPrice = price,
                OrderLines = items.Select(x => new QuotesQueryItem
                {
                    TotalPrice = x.TotalPrice,
                    Amount = x.Amount,
                    ItemPrice = x.ItemPrice,
                    Supplier = x.Supplier,
                    Type = x.Type
                }).ToEnumerable()
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
