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
        private readonly ISupplierBClient _client;
        private readonly IMapper _mapper;

        public ISupplier Supplier => new Stroopwafels.Application.Domain.SupplierB();

        private static readonly IList<DateTime> PublicHolidays = new List<DateTime>
        {
            new DateTime(2016, 1, 1),
            new DateTime(2016, 12, 25),
            new DateTime(2016, 12, 26)
        }; 

        public bool IsAvailable => this.GetAvailability();

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
            if (!this.IsAvailable)
            {
                return null;
            }

            var supplierStroopwafels = await _client.GetStroopwafels();

            return _mapper.Map<IEnumerable<Stroopwafel>>(supplierStroopwafels);
        }

        public async Task Order(IList<KeyValuePair<StroopwafelType, int>> quoteLines)
        {
            var builder = new OrderBuilder();
            var order = builder.CreateOrder(quoteLines);
            var supplierOrder = _mapper.Map<SupplierBOrder>(order);

            await _client.Order(supplierOrder);
        }
    }
}