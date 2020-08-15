using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stroopwafels.Ordering.Services.SupplierA
{
    public sealed class StroopwafelSupplierAService : IStroopwafelSupplierService
    {
        private readonly ISupplierAClient _client;
        private readonly IMapper _mapper;

        public ISupplier Supplier => new Ordering.SupplierA();

        // todo: convert to availability decorator
        public bool IsAvailable => true;

        public StroopwafelSupplierAService(ISupplierAClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<Quote> GetQuote(IList<KeyValuePair<StroopwafelType, int>> orderDetails)
        {
            var supplierStroopwafels = await _client.GetStroopwafels();
            var stroopwafels = _mapper.Map<IEnumerable<Stroopwafel>>(supplierStroopwafels);

            var builder = new QuoteBuilder();

            return builder.CreateOrder(orderDetails, stroopwafels.ToList(), Supplier);
        }

        public async Task Order(IList<KeyValuePair<StroopwafelType, int>> quoteLines)
        {
            var builder = new OrderBuilder();
            var order = builder.CreateOrder(quoteLines);
            var supplierOrder = _mapper.Map<SupplierAOrder>(order);

            await _client.Order(supplierOrder);
        }
    }
}