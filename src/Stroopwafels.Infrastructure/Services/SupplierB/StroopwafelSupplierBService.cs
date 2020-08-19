using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stroopwafels.Application.Domain;
using Stroopwafels.Application.Services;

namespace Stroopwafels.Infrastructure.Services.SupplierB
{
    public sealed class StroopwafelSupplierBService : IStroopwafelSupplierService
    {
        private const decimal ShippingCostLimit = 50m;
        private const decimal ShippingCostAboveLimit = 0m;
        private const decimal ShippingCostUnderLimit = 5m;

        private readonly ISupplierBClient _client;
        private readonly IMapper _mapper;

        private static readonly IList<DateTime> PublicHolidays = new List<DateTime>
        {
            new DateTime(2016, 1, 1),
            new DateTime(2016, 12, 25),
            new DateTime(2016, 12, 26)
        };

        public string Name => "Leverancier B";

        public Task<decimal> CalculateShipingCost(decimal totalPrice)
        {
            var price = totalPrice > ShippingCostLimit ? ShippingCostAboveLimit : ShippingCostUnderLimit;

            return Task.FromResult(price);
        }

        public StroopwafelSupplierBService(ISupplierBClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        private bool GetAvailability()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            var isHoliday = PublicHolidays.Any(h => h == DateTime.Now.Date);
            return !isHoliday;
        }

        public async Task<IEnumerable<Stroopwafel>> QueryStroopwafels()
        {
            if (!GetAvailability())
            {
                return Enumerable.Empty<Stroopwafel>();
            }

            var supplierStroopwafels = await _client.GetStroopwafels();

            return _mapper.Map<IEnumerable<Stroopwafel>>(supplierStroopwafels);
        }

        public async Task MakeOrder(Order order)
        {
            var supplierOrder = _mapper.Map<SupplierBOrder>(order);

            await _client.Order(supplierOrder);
        }

        public Task<TimeSpan> GetDeliveryPeriod()
        {
            return Task.FromResult(TimeSpan.FromDays(3));
        }
    }
}