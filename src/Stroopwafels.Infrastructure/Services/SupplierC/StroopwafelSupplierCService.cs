using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stroopwafels.Application.Domain;
using Stroopwafels.Application.Services;
using System;

namespace Stroopwafels.Infrastructure.Services.SupplierC
{
    public class StroopwafelSupplierCService : IStroopwafelSupplierService
    {
        private const int ShippingCostPercentage = 5;

        private readonly ISupplierCClient _client;
        private readonly IMapper _mapper;

        public StroopwafelSupplierCService(ISupplierCClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public string Name => "Leverancier C";

        public Task<decimal> CalculateShipingCost(decimal totalPrice)
        {
            var price = totalPrice / 100 * ShippingCostPercentage;

            return Task.FromResult(price);
        }

        public async Task<IEnumerable<Stroopwafel>> QueryStroopwafels()
        {
            var supplierStroopwafels = await _client.GetStroopwafels();

            return _mapper.Map<IEnumerable<Stroopwafel>>(supplierStroopwafels);
        }

        public async Task MakeOrder(Order order)
        {
            var supplierOrder = _mapper.Map<SupplierCOrder>(order);

            await _client.Order(supplierOrder);
        }

        public Task<TimeSpan> GetDeliveryPeriod()
        {
            return Task.FromResult(TimeSpan.FromDays(5));
        }
    }
}