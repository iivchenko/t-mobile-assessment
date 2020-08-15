using MediatR;
using Stroopwafels.Application.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stroopwafels.Application.Queries
{
    public sealed class QuotesQueryHandler : IRequestHandler<QuotesQuery, IEnumerable<QuotesQueryResponse>>
    {
        private readonly IEnumerable<IStroopwafelSupplierService> _stroopwafelSupplierServices;

        public QuotesQueryHandler(IEnumerable<IStroopwafelSupplierService> stroopwafelSupplierServices)
        {
            _stroopwafelSupplierServices = stroopwafelSupplierServices;
        }

        public async Task<IEnumerable<QuotesQueryResponse>> Handle(QuotesQuery query, CancellationToken cancellationToken)
        {
            var tasks =
                _stroopwafelSupplierServices
                    .Select(async x => await GetQuotes(x, query))
                    .Where(x => x != null);

            return await Task.WhenAll(tasks);
        }

        private async Task<QuotesQueryResponse> GetQuotes(IStroopwafelSupplierService service, QuotesQuery query)
        {
            var stroopwafels = await service.QueryStroopwafels();

            if(!stroopwafels.Any())
            {
                return null;
            }

            var quoteItems = new List<QuotesQueryItem>();

            foreach (var orderLine in query.OrderLines)
            {
                var stroopwafel = stroopwafels.First(s => s.Type == orderLine.Key);
                var item = new QuotesQueryItem
                {
                    Amount = orderLine.Value,
                    ItemPrice = stroopwafel.Price,
                    TotalPrice = orderLine.Value * stroopwafel.Price
                };

                quoteItems.Add(item);
            }

            var totalPrice = quoteItems.Sum(x => x.TotalPrice);
            var shipingCost = await service.CalculateShipingCost(totalPrice);

            return new QuotesQueryResponse
            {
                Supplier = await service.GetName(),
                Items = quoteItems,
                TotalWithoutShippingCost = totalPrice,
                TotalPrice = shipingCost * totalPrice
            };
        }
    }
}
