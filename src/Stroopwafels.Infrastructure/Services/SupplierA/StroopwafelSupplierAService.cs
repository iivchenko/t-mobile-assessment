using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stroopwafels.Application.Domain;
using Stroopwafels.Application.Services;
using System;

namespace Stroopwafels.Infrastructure.Services.SupplierA
{
    public sealed class StroopwafelSupplierAService : IStroopwafelSupplierService
    {
        private readonly ISupplierAClient _client;
        private readonly IMapper _mapper;

        public StroopwafelSupplierAService(ISupplierAClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public string Name => "Leverancier A";

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

        public Task<TimeSpan> GetDeliveryPeriod()
        {
            return Task.FromResult(TimeSpan.FromDays(4));
        }
    }
}