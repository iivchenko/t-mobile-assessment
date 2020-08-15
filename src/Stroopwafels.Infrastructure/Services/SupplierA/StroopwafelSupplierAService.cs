using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stroopwafels.Application.Domain;
using Stroopwafels.Application.Services;

namespace Stroopwafels.Infrastructure.Services.SupplierA
{
    public sealed class StroopwafelSupplierAService : IStroopwafelSupplierService
    {
        private readonly ISupplierAClient _client;
        private readonly IMapper _mapper;

        // todo: convert to availability decorator; or use circuit braker; or polly
        public bool IsAvailable => true;

        public StroopwafelSupplierAService(ISupplierAClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public Task<string> GetName()
        {
            return Task.FromResult("Leverancier A");
        }

        public Task<decimal> CalculateShipingCost(decimal totalPrice)
        {
            return Task.FromResult(5m);
        }

        public async Task<IEnumerable<Stroopwafel>> QueryStroopwafels()
        {
            var supplierStroopwafels = await _client.GetStroopwafels();

            return _mapper.Map<IEnumerable<Stroopwafel>>(supplierStroopwafels);
        }

        public async Task MakeOrder(Order order)
        {
            var supplierOrder = _mapper.Map<SupplierAOrder>(order);

            await _client.Order(supplierOrder);
        }
    }
}