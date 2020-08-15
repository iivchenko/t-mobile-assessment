using MediatR;
using Stroopwafels.Application.Domain;
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
            var tasks = _stroopwafelSupplierServices.Where(service => service.IsAvailable).Select(async service => await service.GetQuote(query.OrderLines));
            return await Task.WhenAll(tasks);
        }
    }
}
