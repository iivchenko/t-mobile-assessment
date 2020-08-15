using MediatR;
using Stroopwafels.Application.Domain;
using Stroopwafels.Application.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stroopwafels.Application.Queries
{
    public sealed class QuotesQueryHandler : IRequestHandler<QuotesQuery, IEnumerable<Quote>>
    {
        private readonly IEnumerable<IStroopwafelSupplierService> _stroopwafelSupplierServices;

        public QuotesQueryHandler(IEnumerable<IStroopwafelSupplierService> stroopwafelSupplierServices)
        {
            _stroopwafelSupplierServices = stroopwafelSupplierServices;
        }

        public async Task<IEnumerable<Quote>> Handle(QuotesQuery query, CancellationToken cancellationToken)
        {
            var tasks = _stroopwafelSupplierServices.Where(service => service.IsAvailable).Select(async x => await GetQuotes(x, query));
            return await Task.WhenAll(tasks);
        }

        private async Task<Quote> GetQuotes(IStroopwafelSupplierService service, QuotesQuery query)
        {
            var stroopwafels = await service.QueryStroopwafels();

            var builder = new QuoteBuilder();
            return builder.CreateOrder(query.OrderLines, stroopwafels.ToList(), service.Supplier);
        }
    }
}
