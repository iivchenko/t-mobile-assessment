using MediatR;
using Stroopwafels.Ordering;
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

        public Task<IEnumerable<Quote>> Handle(QuotesQuery query, CancellationToken cancellationToken)
        {
            // TODO: Make async
            var quotes = _stroopwafelSupplierServices.Where(service => service.IsAvailable).Select(service => service.GetQuote(query.OrderLines)).ToList();

            return Task.FromResult(quotes.AsEnumerable());
        }
    }
}
