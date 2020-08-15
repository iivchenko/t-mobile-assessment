using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stroopwafels.Application.Domain;
using Stroopwafels.Application.Services;

namespace Stroopwafels.Infrastructure.Services.SupplierC
{
    public class StroopwafelSupplierCService : IStroopwafelSupplierService
    {
        private readonly ISupplierCClient _client;
        private readonly IMapper _mapper;

        public ISupplier Supplier => new Stroopwafels.Application.Domain.SupplierC();

        public bool IsAvailable => true;

        public StroopwafelSupplierCService(ISupplierCClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Stroopwafel>> QueryStroopwafels()
        {
            var supplierStroopwafels = await _client.GetStroopwafels();

            return _mapper.Map<IEnumerable<Stroopwafel>>(supplierStroopwafels);
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