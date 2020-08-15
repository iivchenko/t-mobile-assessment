using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stroopwafels.Ordering.Services.SupplierC
{
    public class StroopwafelSupplierCService : IStroopwafelSupplierService
    {
        private readonly ISupplierCClient _client;
        private readonly IMapper _mapper;

        public ISupplier Supplier => new Ordering.SupplierC();

        public bool IsAvailable => true;

        public StroopwafelSupplierCService(ISupplierCClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<Quote> GetQuote(IList<KeyValuePair<StroopwafelType, int>> orderDetails)
        {
            var supplierStroopwafels = await _client.GetStroopwafels();
            var stroopwafels = _mapper.Map<IEnumerable<Stroopwafel>>(supplierStroopwafels);

            var builder = new QuoteBuilder();

            var order = builder.CreateOrder(orderDetails, stroopwafels.ToList(), Supplier);

            return order;
        }

        public async Task Order(IList<KeyValuePair<StroopwafelType, int>> quoteLines)
        {
            var builder = new OrderBuilder();
            var order = builder.CreateOrder(quoteLines);
            var supplierOrder = _mapper.Map<SupplierCOrder>(order);

            await _client.Order(supplierOrder);
        }
    }
}